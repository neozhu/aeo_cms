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
  /// File: QuotationFileService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/8/26 17:35:24
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class QuotationFileService : Service<QuotationFile>, IQuotationFileService
  {
    private readonly IRepositoryAsync<QuotationFile> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly NLog.ILogger logger;
    private readonly IActionLogService actionLogService;
    public QuotationFileService(
      IRepositoryAsync<QuotationFile> repository,
      IDataTableImportMappingService mappingservice,
      IActionLogService actionLogService,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
      this.actionLogService = actionLogService;
    }
    public async Task<IEnumerable<QuotationFile>> GetByQuotationIdAsync(int quotationid) => await repository.GetByQuotationIdAsync(quotationid);



    private async Task<int> getQuotationIdByQpNoAsync(string qpno)
    {
      var quotationRepository = this.repository.GetRepositoryAsync<Quotation>();
      var quotation = await quotationRepository.Queryable().Where(x => x.QpNo == qpno).FirstOrDefaultAsync();
      if (quotation == null)
      {
        throw new Exception("not found ForeignKey:QuotationId with " + qpno);
      }
      else
      {
        return quotation.Id;
      }
    }
    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "QuotationFile" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到QuotationFile对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
          var item = new QuotationFile();
          var quotationfiletype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = quotationfiletype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "QuotationId":
                  var quotation_qpno = row[field.SourceFieldName].ToString();
                  var quotationid = await this.getQuotationIdByQpNoAsync(quotation_qpno);
                  propertyInfo.SetValue(item, Convert.ChangeType(quotationid, propertyInfo.PropertyType), null);
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
              var propertyInfo = quotationfiletype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "QuotationFile")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var quotationfiles = await this.Query(new QuotationFileQuery().Withfilter(filters)).Include(p => p.Quotation).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = quotationfiles.Select(n => new
      {

        QuotationQpNo = n.Quotation?.QpNo,
        Id = n.Id,
        FileName = n.FileName,
        Size = n.Size,
        Folder = n.Folder,
        FileId = n.FileId,
        Ext = n.Ext,
        FilePath = n.FilePath,
        RelativePath = n.RelativePath,
        RefKey = n.RefKey,
        Owner = n.Owner,
        Upload = n.Upload.ToString("yyyy-MM-dd HH:mm:ss"),
        QpNo = n.QpNo,
        QuotationId = n.QuotationId
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("报价单附件", datarows, expcolopts);
    }
    public async Task Delete(int[] id,string user)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id))
         .Include(x => x.Quotation).ToListAsync();
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
          RefId = item.QuotationId,
          RekKey = item.QpNo,
          ActionDateTime = DateTime.Now,
          User = user
        };
        this.actionLogService.Insert(log);

        this.Delete(item);
      }

    }

    public void AddFile(int ver, int quotationid, string qpno, HttpPostedFileBase file, string folder, string relpath, string user)
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
      var item = new  QuotationFile()
      {
        Ext = ext,
        FileName = filename,
        FilePath = filepath,
        RelativePath = relativepath,
        Size = size,
        FileId = fileId,
        Owner = user,
        Upload = DateTime.Now,
         QpNo=qpno,
          QuotationId=quotationid,
           RefKey= qpno,
            Folder= qpno

      };
      this.Insert(item);
      var log = new ActionLog()
      {
        Action = "编辑",
        Content = "添加附件:" + filename,
        RefId = quotationid,
        RekKey = qpno,
        ActionDateTime = DateTime.Now,
        User = user
      };
      this.actionLogService.Insert(log);
    }
  }
}