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
  /// File: HSCodeService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/8/26 13:40:28
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class HSCodeService : Service<HSCode>, IHSCodeService
  {
    private readonly IRepositoryAsync<HSCode> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly NLog.ILogger logger;
    public HSCodeService(
      IRepositoryAsync<HSCode> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
    }



    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "HSCode" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到HSCode对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null &&
             !row.IsNull(requiredfield) &&
             !string.IsNullOrEmpty(row[requiredfield].ToString())
             
            )
        {
          var item = new HSCode();
          var hscodetype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = hscodetype.GetProperty(field.FieldName);
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
              var propertyInfo = hscodetype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "HSCode")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var hscodes = this.Query(new HSCodeQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = hscodes.Select(n => new
      {

        Id = n.Id,
        hscode = n.hscode,
        cn_name = n.cn_name,
        en_name = n.en_name,
        g_model = n.g_model,
        unit_code = n.unit_code,
        unit_name = n.unit_name,
        unit2_code = n.unit2_code,
        unit2_name = n.unit2_name,
        control_ma = n.control_ma,
        ciq_ma = n.ciq_ma,
        im_low_rate = n.im_low_rate,
        im_normal_rate = n.im_normal_rate,
        im_temp_rate = n.im_temp_rate,
        im_tax_rate = n.im_tax_rate,
        im_consume_rate = n.im_consume_rate,
        ex_return_rate = n.ex_return_rate,
        ex_normal_rate = n.ex_normal_rate,
        ex_temp_rate = n.ex_temp_rate,
        ex_special_rate = n.ex_special_rate,
        ex_tax_rate = n.ex_tax_rate,
        remark = n.remark
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("海关商品编码库", datarows, expcolopts);
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