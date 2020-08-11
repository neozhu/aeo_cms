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
/// File: AeoQuestionService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 2020/8/11 9:21:11
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class AeoQuestionService : Service< AeoQuestion >, IAeoQuestionService
    {
        private readonly IRepositoryAsync<AeoQuestion> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  AeoQuestionService(
          IRepositoryAsync< AeoQuestion> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                 public async  Task<IEnumerable<AeoQuestion>> GetByAeoAuthTestIdAsync(int  aeoauthtestid) => await repository.GetByAeoAuthTestIdAsync(aeoauthtestid);
                   
        
        		 
                private async Task<int> getAeoAuthTestIdByNameAsync(string name)
        {
            var aeoauthtestRepository = this.repository.GetRepositoryAsync<AeoAuthTest>();
            var aeoauthtest = await  aeoauthtestRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
            if (aeoauthtest == null)
            {
                throw new Exception("not found ForeignKey:AeoAuthTestId with " + name);
            }
            else
            {
                return aeoauthtest.Id;
            }
        }
                public async Task ImportDataTableAsync(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "AeoQuestion" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到AeoQuestion对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
                    var item = new AeoQuestion();
                    var aeoquestiontype = item.GetType();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
						{
							var propertyInfo = aeoquestiontype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "AeoAuthTestId":
                                     var aeoauthtest_name =  row[field.SourceFieldName].ToString();
                                     var aeoauthtestid = await this.getAeoAuthTestIdByNameAsync(aeoauthtest_name);
                                     propertyInfo.SetValue(item, Convert.ChangeType(aeoauthtestid, propertyInfo.PropertyType), null);
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
							var propertyInfo = aeoquestiontype.GetProperty(field.FieldName);
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
                   .Where(x => x.EntitySetName == "AeoQuestion")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var aeoquestions  = await this.Query(new AeoQuestionQuery().Withfilter(filters)).Include(p => p.AeoAuthTest).OrderBy(n=>n.OrderBy(sort,order)).SelectAsync();
            
            var datarows = aeoquestions .Select(  n => new { 

    AeoAuthTestName = n.AeoAuthTest?.Name,
    Id = n.Id,
    Tpl = n.Tpl,
    AuthType = n.AuthType,
    Category = n.Category,
    Description = n.Description,
    Code = n.Code,
    Title = n.Title,
    Short = n.Short,
    StdDescription = n.StdDescription,
    Notes = n.Notes,
    StdScore = n.StdScore,
    Score = n.Score,
    ScoreDescription = n.ScoreDescription,
    Remark = n.Remark,
    Tester = n.Tester,
    TestDateTime = n.TestDateTime?.ToString("yyyy-MM-dd HH:mm:ss"),
    TestNo = n.TestNo,
    AeoAuthTestId = n.AeoAuthTestId
}).ToList();
            return await NPOIHelper.ExportExcelAsync("测试题目", datarows,expcolopts);
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