using System;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Text.RegularExpressions;

namespace WebApp.Services
{
  /// <summary>
  /// File: CustomerService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/7/3 14:23:06
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class CustomerService : Service<Customer>, ICustomerService
  {
    private readonly IRepositoryAsync<Customer> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly NLog.ILogger logger;
    public CustomerService(
      IRepositoryAsync<Customer> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
    }
    public async Task<IEnumerable<CustomerAttentionProduct>> GetCustomerAttentionProductsByCustomerIdAsync(int customerid) => await repository.GetCustomerAttentionProductsByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerBank>> GetCustomerBanksByCustomerIdAsync(int customerid) => await repository.GetCustomerBanksByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerCommunication>> GetCustomerCommunicationsByCustomerIdAsync(int customerid) => await repository.GetCustomerCommunicationsByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerContact>> GetCustomerContactsByCustomerIdAsync(int customerid) => await repository.GetCustomerContactsByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerFile>> GetCustomerFilesByCustomerIdAsync(int customerid) => await repository.GetCustomerFilesByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerFollow>> GetCustomerFollowsByCustomerIdAsync(int customerid) => await repository.GetCustomerFollowsByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerInvoice>> GetCustomerInvoicesByCustomerIdAsync(int customerid) => await repository.GetCustomerInvoicesByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerSales>> GetCustomerSalesByCustomerIdAsync(int customerid) => await repository.GetCustomerSalesByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerShare>> GetCustomerSharesByCustomerIdAsync(int customerid) => await repository.GetCustomerSharesByCustomerIdAsync(customerid);
    public async Task<IEnumerable<CustomerWarehouse>> GetCustomerWarehousesByCustomerIdAsync(int customerid) => await repository.GetCustomerWarehousesByCustomerIdAsync(customerid);



    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "Customer" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到Customer对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null ||
              ( !row.IsNull(requiredfield) &&
               !string.IsNullOrEmpty(row[requiredfield].ToString())
              )
            )
        {
          var item = new Customer();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var customertype = item.GetType();
              var propertyInfo = customertype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = Convert.ChangeType(row[field.SourceFieldName], safetype);
              if (!string.IsNullOrEmpty(field.RegularExpression))
              {
                var isValid = Regex.IsMatch(safeValue.ToString(), field.RegularExpression);
                if (!isValid)
                {
                  throw new Exception($"{field.SourceFieldName}:{safeValue} 表达式验证不匹配:{field.RegularExpression}");
                }
              }
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var customertype = item.GetType();
              var propertyInfo = customertype.GetProperty(field.FieldName);
              if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && ( propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>) ))
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
              else if (string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
              }
              else if (string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, username, null);
              }
              else
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(defval, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
            }
          }
          this.Insert(item);
        }
      }
    }
    public async Task<Stream> ExportExcelAsync(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var expcolopts = await this.mappingservice.Queryable()
             .Where(x => x.EntitySetName == "Customer")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var customers = this.Query(new CustomerQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = customers.Select(n => new
      {

        CustomerAttentionProducts = n.CustomerAttentionProducts,
        CustomerBanks = n.CustomerBanks,
        CustomerCommunications = n.CustomerCommunications,
        CustomerContacts = n.CustomerContacts,
        CustomerFiles = n.CustomerFiles,
        CustomerFollows = n.CustomerFollows,
        CustomerInvoices = n.CustomerInvoices,
        CustomerSales = n.CustomerSales,
        CustomerShares = n.CustomerShares,
        CustomerWarehouses = n.CustomerWarehouses,
        Id = n.Id,
        CustomerCode = n.CustomerCode,
        CustomerName = n.CustomerName,
        CustomerEName = n.CustomerEName,
        CustomerType = n.CustomerType,
        Overseas = n.Overseas,
        CustomerType3 = n.CustomerType3,
        Capital = n.Capital,
        CURR = n.CURR,
        TaxProperty = n.TaxProperty,
        ParentOrg = n.ParentOrg,
        CustomMaster = n.CustomMaster,
        TradeCode = n.TradeCode,
        Country = n.Country,
        Zone = n.Zone,
        Scale = n.Scale,
        Level = n.Level,
        Value = n.Value,
        CreditRating = n.CreditRating,
        Source = n.Source,
        Industry = n.Industry,
        Cash = n.Cash,
        CashCURR = n.CashCURR,
        SDesc = n.SDesc,
        Remark = n.Remark,
        CProvinces1 = n.CProvinces1,
        CCity1 = n.CCity1,
        CCounty1 = n.CCounty1,
        CAddress1 = n.CAddress1,
        CProvinces2 = n.CProvinces2,
        CCity2 = n.CCity2,
        CCounty2 = n.CCounty2,
        CAddress2 = n.CAddress2,
        EAddress1 = n.EAddress1,
        EAddress2 = n.EAddress2,
        PostCode = n.PostCode,
        WebSite = n.WebSite,
        BusinessScope = n.BusinessScope,
        Remark1 = n.Remark1,
        Status = n.Status,
        Status1 = n.Status1,
        Status2 = n.Status2,
        Status3 = n.Status3,
        Status4 = n.Status4,
        Status5 = n.Status5,
        Logo = n.Logo,
        CompanyCode = n.CompanyCode,
        CompanyName = n.CompanyName
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("客户信息", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }
  }
}



