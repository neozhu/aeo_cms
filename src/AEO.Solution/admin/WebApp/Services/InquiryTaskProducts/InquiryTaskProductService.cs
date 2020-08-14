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
/// File: InquiryTaskProductService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 2020/8/14 14:39:53
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class InquiryTaskProductService : Service< InquiryTaskProduct >, IInquiryTaskProductService
    {
        private readonly IRepositoryAsync<InquiryTaskProduct> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  InquiryTaskProductService(
          IRepositoryAsync< InquiryTaskProduct> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                 public async  Task<IEnumerable<InquiryTaskProduct>> GetByInquiryTaskIdAsync(int  inquirytaskid) => await repository.GetByInquiryTaskIdAsync(inquirytaskid);
                   
        
        		 
                private async Task<int> getInquiryTaskIdByTaskNoAsync(string taskno)
        {
            var inquirytaskRepository = this.repository.GetRepositoryAsync<InquiryTask>();
            var inquirytask = await  inquirytaskRepository.Queryable().Where(x => x.TaskNo == taskno).FirstOrDefaultAsync();
            if (inquirytask == null)
            {
                throw new Exception("not found ForeignKey:InquiryTaskId with " + taskno);
            }
            else
            {
                return inquirytask.Id;
            }
        }
                public async Task ImportDataTableAsync(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "InquiryTaskProduct" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到InquiryTaskProduct对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
                    var item = new InquiryTaskProduct();
                    var inquirytaskproducttype = item.GetType();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
						{
							var propertyInfo = inquirytaskproducttype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "InquiryTaskId":
                                     var inquirytask_taskno =  row[field.SourceFieldName].ToString();
                                     var inquirytaskid = await this.getInquiryTaskIdByTaskNoAsync(inquirytask_taskno);
                                     propertyInfo.SetValue(item, Convert.ChangeType(inquirytaskid, propertyInfo.PropertyType), null);
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
							var propertyInfo = inquirytaskproducttype.GetProperty(field.FieldName);
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
                   .Where(x => x.EntitySetName == "InquiryTaskProduct")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var inquirytaskproducts  = await this.Query(new InquiryTaskProductQuery().Withfilter(filters)).Include(p => p.InquiryTask).OrderBy(n=>n.OrderBy(sort,order)).SelectAsync();
            
            var datarows = inquirytaskproducts .Select(  n => new { 

    InquiryTaskTaskNo = n.InquiryTask?.TaskNo,
    Id = n.Id,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    CategoryName = n.CategoryName,
    ProductEnName = n.ProductEnName,
    CnDescription = n.CnDescription,
    EnDescription = n.EnDescription,
    ThirdProductNo = n.ThirdProductNo,
    Qty = n.Qty,
    Unit = n.Unit,
    PriceType = n.PriceType,
    Price = n.Price,
    Executor = n.Executor,
    SupplierCode = n.SupplierCode,
    SupplierName = n.SupplierName,
    SamplePic = n.SamplePic,
    TaskNo = n.TaskNo,
    InquiryTaskId = n.InquiryTaskId
}).ToList();
            return await NPOIHelper.ExportExcelAsync("运价任务产品明细", datarows,expcolopts);
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