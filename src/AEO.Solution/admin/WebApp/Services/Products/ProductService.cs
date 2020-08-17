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
using AutoMapper;

namespace WebApp.Services
{
  /// <summary>
  /// File: ProductService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/7/30 16:45:00
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class ProductService : Service<Product>, IProductService
  {
    private readonly IRepositoryAsync<Product> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IAttachmentService attachmentService;
    private readonly IProductPrictureService productPrictureService;
    private readonly IActionLogService actionLogService;
    private readonly NLog.ILogger logger;
    private readonly IMapper mapper;
    public ProductService(
      IProductPrictureService productPrictureService,
      IAttachmentService attachmentService,
      IRepositoryAsync<Product> repository,
      IDataTableImportMappingService mappingservice,
      IActionLogService actionLogService,
      IMapper mapper,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
      this.attachmentService = attachmentService;
      this.mapper = mapper;
      this.productPrictureService = productPrictureService;
      this.actionLogService = actionLogService;
    }
    public async Task<IEnumerable<ProductFile>> GetProductFilesByProductIdAsync(int productid) => await repository.GetProductFilesByProductIdAsync(productid);
    public async Task<IEnumerable<ProductPurchaseHistoricalPrice>> GetProductPurchaseHistoricalPricesByProductIdAsync(int productid) => await repository.GetProductPurchaseHistoricalPricesByProductIdAsync(productid);
    public async Task<IEnumerable<ProductSalesHistoricalPrice>> GetProductSalesHistoricalPricesByProductIdAsync(int productid) => await repository.GetProductSalesHistoricalPricesByProductIdAsync(productid);



    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "Product" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到Product对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
          var item = new Product();
          var producttype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = producttype.GetProperty(field.FieldName);
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
              var propertyInfo = producttype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "Product")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var products = this.Query(new ProductQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = products.Select(n => new
      {

        ProductFiles = n.ProductFiles,
        ProductPurchaseHistoricalPrices = n.ProductPurchaseHistoricalPrices,
        ProductSalesHistoricalPrices = n.ProductSalesHistoricalPrices,
        Id = n.Id,
        ProductNo = n.ProductNo,
        Category = n.Category,
        ProductName = n.ProductName,
        ProductEnName = n.ProductEnName,
        Spec = n.Spec,
        CnDescription = n.CnDescription,
        EnDescription = n.EnDescription,
        Remark = n.Remark,
        Status = n.Status,
        Logo = n.Logo,
        HSCODE = n.HSCODE,
        HSADDTAXRATE = n.HSADDTAXRATE,
        HSBACKTAXRATE = n.HSBACKTAXRATE,
        GUIDEPRICE = n.GUIDEPRICE,
        CUSTBASIC = n.CUSTBASIC,
        COUNTRY = n.COUNTRY,
        TAXTYPE = n.TAXTYPE,
        TAXCLASS = n.TAXCLASS,
        Package = n.Package,
        InnerBoxQty = n.InnerBoxQty,
        Unit = n.Unit,
        GWeight = n.GWeight,
        GWUnit = n.GWUnit,
        NWeight = n.NWeight,
        NWUnit = n.NWUnit,
        Volume = n.Volume,
        VUnit = n.VUnit,
        Length = n.Length,
        Width = n.Width,
        High = n.High,
        LUnit = n.LUnit,
        Flag1 = n.Flag1,
        Flag2 = n.Flag2
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("产品管理", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable()
        .Where(x => id.Contains(x.Id))
        .ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task Create(CreateProductViewModel product)
    {
      var item = this.mapper.Map<Product>(product);
      if (string.IsNullOrEmpty(item.ProductNo))
      {
        item.ProductNo = KeyGenerator.GetProductKey();
      }
      if (product.Pictures != null)
      {
        foreach (var fileid in product.Pictures)
        {
          var att = await this.attachmentService.Queryable()
            .Where(x => x.FileId == fileid).FirstOrDefaultAsync();
          if (att != null)
          {
            var picture = this.mapper.Map<ProductPricture>(att);
            picture.Product = item;
            picture.ProductId = item.Id;
            item.ProductPrictures.Add(picture);
          }
        }
      }
      this.Insert(item);
      var actionlog = new ActionLog()
      {
        Action = "新增",
        Content = "新增产品:" + item.ProductNo,
        ActionDateTime = DateTime.Now,
        RekKey = item.ProductNo,
        RefId = item.Id,
        User = Auth.GetFullName()
      };
      this.actionLogService.Insert(actionlog);

    }
    public async Task DeleteFile(string id) {
      var item =await this.attachmentService.Queryable().Where(x => x.FileId == id).FirstOrDefaultAsync();
      if (item != null)
      {
        this.attachmentService.Delete(item);
       }
      var pricture = await this.productPrictureService.Queryable()
        .Where(x => x.FileId == id).FirstOrDefaultAsync();
      if (pricture != null)
      {
        this.productPrictureService.Delete(pricture);
       }
      }
    public async Task AddPrictures(int id, string[] fileId) {
      var attachments =await this.attachmentService.Queryable()
        .Where(x =>fileId.Contains(x.FileId)).ToListAsync();
      foreach(var attachment in attachments)
      {
        var picture = this.mapper.Map<ProductPricture>(attachment);
        picture.ProductId = id;
        this.productPrictureService.Insert(picture);
       }
    }
  }
}