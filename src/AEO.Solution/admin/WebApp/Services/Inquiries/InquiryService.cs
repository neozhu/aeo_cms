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

namespace WebApp.Services
{
/// <summary>
/// File: InquiryService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 2020/8/19 11:03:55
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class InquiryService : Service< Inquiry >, IInquiryService
    {
        private readonly IRepositoryAsync<Inquiry> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  InquiryService(
          IRepositoryAsync< Inquiry> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                 public async  Task<IEnumerable<Inquiry>> GetByCustomerIdAsync(int  customerid) => await repository.GetByCustomerIdAsync(customerid);
                  public async  Task<IEnumerable<Inquiry>> GetByCompanyIdAsync(int  companyid) => await repository.GetByCompanyIdAsync(companyid);
                          public async Task<IEnumerable<InquiryFile>>   GetInquiryfilesByInquiryIdAsync (int inquiryid) => await repository.GetInquiryfilesByInquiryIdAsync(inquiryid);
                public async Task<IEnumerable<InquiryProduct>>   GetInquiryproductsByInquiryIdAsync (int inquiryid) => await repository.GetInquiryproductsByInquiryIdAsync(inquiryid);
                public async Task<IEnumerable<InquiryRef>>   GetInquiryrefsByInquiryIdAsync (int inquiryid) => await repository.GetInquiryrefsByInquiryIdAsync(inquiryid);
         
        
        		 
                private async Task<int> getCompanyIdByNameAsync(string name)
        {
            var companyRepository = this.repository.GetRepositoryAsync<Company>();
            var company = await  companyRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
            if (company == null)
            {
                throw new Exception("not found ForeignKey:CompanyId with " + name);
            }
            else
            {
                return company.Id;
            }
        }
                private async Task<int> getCustomerIdByCustomerCodeAsync(string customercode)
        {
            var customerRepository = this.repository.GetRepositoryAsync<Customer>();
            var customer = await  customerRepository.Queryable().Where(x => x.CustomerCode == customercode).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("not found ForeignKey:CustomerId with " + customercode);
            }
            else
            {
                return customer.Id;
            }
        }
                public async Task ImportDataTableAsync(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "Inquiry" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到Inquiry对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
            }
            foreach (DataRow row in datatable.Rows)
            {
                
                var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled==true && x.DefaultValue==null).FirstOrDefault()?.SourceFieldName;
                if (requiredfield != null ||
                      (!row.IsNull(requiredfield) &&
                       !string.IsNullOrEmpty(row[requiredfield].ToString())
                      )
                    )
                {
                    var item = new Inquiry();
                    var inquirytype = item.GetType();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
						{
							var propertyInfo = inquirytype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "CompanyId":
                                     var company_name =  row[field.SourceFieldName].ToString();
                                     var companyid = await this.getCompanyIdByNameAsync(company_name);
                                     propertyInfo.SetValue(item, Convert.ChangeType(companyid, propertyInfo.PropertyType), null);
                                     break;
                                                                case "CustomerId":
                                     var customer_customercode =  row[field.SourceFieldName].ToString();
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
							var propertyInfo = inquirytype.GetProperty(field.FieldName);
							if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && (propertyInfo.PropertyType ==typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>)))
                            {
                                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                                propertyInfo.SetValue(item, safeValue, null);
                            }
                            else if(string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
                            {
                                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
                            }
                            else if(string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
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
				public async Task<Stream> ExportExcelAsync(string filterRules = "",string sort = "Id", string order = "asc")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            var expcolopts= await this.mappingservice.Queryable()
                   .Where(x => x.EntitySetName == "Inquiry")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var inquiries  = await this.Query(new InquiryQuery().Withfilter(filters)).Include(p => p.Company).Include(p => p.Customer).OrderBy(n=>n.OrderBy(sort,order)).SelectAsync();
            
            var datarows = inquiries .Select(  n => new { 

    CompanyName = n.Company?.Name,
    CustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    InquiryNo = n.InquiryNo,
    TaskNo = n.TaskNo,
    Status = n.Status,
    Salesman = n.Salesman,
    BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
    FeedbackDate = n.FeedbackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Demande = n.Demande,
    CustomerId = n.CustomerId,
    CustomerName = n.CustomerName,
    Country = n.Country,
    Cur = n.Cur,
    ExchangeRate = n.ExchangeRate,
    ContactName = n.ContactName,
    ContactInfo = n.ContactInfo,
    EndDate = n.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Urgency = n.Urgency,
    PreRemind = n.PreRemind,
    Check1 = n.Check1,
    Creator = n.Creator,
    Executor = n.Executor,
    Check2 = n.Check2,
    Check3 = n.Check3,
    Owner = n.Owner,
    CompanyId = n.CompanyId,

    Ver = n.Ver
}).ToList();
            return await NPOIHelper.ExportExcelAsync("询价单", datarows,expcolopts);
        }
        public async Task Delete(int[] id) {
            var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
            foreach (var item in items)
            {
               this.Delete(item);
            }

        }
    }
}