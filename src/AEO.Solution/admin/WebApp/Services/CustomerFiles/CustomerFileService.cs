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
using System.Text.RegularExpressions;
using WebApp.Models;
using WebApp.Repositories;
using System.Web;

namespace WebApp.Services
{
  /// <summary>
  /// File: CustomerFileService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/8/5 11:04:37
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class CustomerFileService : Service<CustomerFile>, ICustomerFileService
  {
    private readonly IRepositoryAsync<CustomerFile> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IActionLogService actionLogService;
    private readonly NLog.ILogger logger;
    public CustomerFileService(
      IActionLogService actionLogService,
      IRepositoryAsync<CustomerFile> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.actionLogService = actionLogService;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
    }
    public async Task<IEnumerable<CustomerFile>> GetByCustomerIdAsync(int customerid) => await repository.GetByCustomerIdAsync(customerid);



    private async Task<int> getCustomerIdByCustomerCodeAsync(string customercode)
    {
      var customerRepository = this.repository.GetRepositoryAsync<Customer>();
      var customer = await customerRepository.Queryable().Where(x => x.CustomerCode == customercode).FirstOrDefaultAsync();
      if (customer == null)
      {
        throw new Exception("not found ForeignKey:CustomerId with " + customercode);
      }
      else
      {
        return customer.Id;
      }
    }
    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "CustomerFile" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到CustomerFile对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
          var item = new CustomerFile();
          var customerfiletype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = customerfiletype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "CustomerId":
                  var customer_customercode = row[field.SourceFieldName].ToString();
                  var customerid = await this.getCustomerIdByCustomerCodeAsync(customer_customercode);
                  propertyInfo.SetValue(item, Convert.ChangeType(customerid, propertyInfo.PropertyType), null);
                  break;
                default:
                  var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                  var safeValue = Convert.ChangeType(row[field.SourceFieldName], safetype);
                  propertyInfo.SetValue(item, safeValue, null);
                  break;
              }
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var propertyInfo = customerfiletype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "CustomerFile")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var customerfiles = await this.Query(new CustomerFileQuery().Withfilter(filters)).Include(p => p.Customer).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = customerfiles.Select(n => new
      {

        CustomerCustomerCode = n.Customer?.CustomerCode,
        Id = n.Id,
        FileName = n.FileName,
        Size = n.Size,
        Folder = n.Folder,
        FilePath = n.FilePath,
        RelativePath = n.RelativePath,
        Owner = n.Owner,
        Upload = n.Upload.ToString("yyyy-MM-dd HH:mm:ss"),
        Ext = n.Ext,
        FileId = n.FileId,
        RefKey = n.RefKey,
        CustomerId = n.CustomerId,
        CustomerCode = n.CustomerCode,
        CustomerName = n.CustomerName
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("客户附件信息", datarows, expcolopts);
    }
    public async Task Delete(int[] id, string user)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id))
        .Include(x=>x.Customer)
        .ToListAsync();
      foreach (var item in items)
      {
        if (File.Exists(item.FilePath))
        {
          File.Delete(item.FilePath);
        }
        var log = new ActionLog()
        {
          Action = "编辑",
          Content = "删除附件:" + item.FileName,
          RefId = item.CustomerId,
          RekKey = item.Customer.CustomerCode,
          ActionDateTime = DateTime.Now,
          User = user
        };
        this.actionLogService.Insert(log);

        this.Delete(item);
      }

    }
    public void AddFile(int customerId, string customerCode, HttpPostedFileBase file, string folder, string relpath, string user)
    {
      var filename = file.FileName;
      var fileId = Guid.NewGuid().ToString();
      var ext = Path.GetExtension(filename);
      var relativepath = relpath + fileId + ext;
      var size = file.ContentLength;
      if (!Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }
      var filepath = Path.Combine(folder, fileId + ext);
      if (!File.Exists(filepath))
      {
        file.SaveAs(filepath);
      }
      var item = new CustomerFile()
      {
        Ext = ext,
        FileName = filename,
        FilePath = filepath,
        RelativePath = relativepath,
        Size = size,
        FileId = fileId,
        Owner = user,
        Upload = DateTime.Now,
        CustomerId = customerId,
        CustomerCode= customerCode

      };
      this.Insert(item);
      var log = new ActionLog()
      {
        Action = "编辑",
        Content = "添加附件:" + filename,
        RefId = customerId,
        RekKey = customerCode,
        ActionDateTime = DateTime.Now,
        User = user
      };
      this.actionLogService.Insert(log);
    }
  }
}