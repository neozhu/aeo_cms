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
  public class InquiryService : Service<Inquiry>, IInquiryService
  {
    private readonly IApproveHistoryService approveHistoryService;
    private readonly IRepositoryAsync<Inquiry> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IInquiryTaskService inquiryTaskService;
    private readonly IInquiryTaskProductService inquiryTaskProductService;
    private readonly NLog.ILogger logger;
    private readonly IMapper mapper;
    public InquiryService(
      IMapper mapper,
      IApproveHistoryService approveHistoryService,
      IRepositoryAsync<Inquiry> repository,
      IDataTableImportMappingService mappingservice,
      IInquiryTaskService inquiryTaskService,
      IInquiryTaskProductService inquiryTaskProductService,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.approveHistoryService = approveHistoryService;
      this.mapper = mapper;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.inquiryTaskService = inquiryTaskService;
      this.logger = logger;
      this.inquiryTaskProductService = inquiryTaskProductService;
    }
    public async Task<IEnumerable<Inquiry>> GetByCustomerIdAsync(int customerid) => await repository.GetByCustomerIdAsync(customerid);
    public async Task<IEnumerable<Inquiry>> GetByCompanyIdAsync(int companyid) => await repository.GetByCompanyIdAsync(companyid);
    public async Task<IEnumerable<InquiryFile>> GetInquiryfilesByInquiryIdAsync(int inquiryid) => await repository.GetInquiryfilesByInquiryIdAsync(inquiryid);
    public async Task<IEnumerable<InquiryProduct>> GetInquiryproductsByInquiryIdAsync(int inquiryid) => await repository.GetInquiryproductsByInquiryIdAsync(inquiryid);
    public async Task<IEnumerable<InquiryRef>> GetInquiryrefsByInquiryIdAsync(int inquiryid) => await repository.GetInquiryrefsByInquiryIdAsync(inquiryid);



    private async Task<int> getCompanyIdByNameAsync(string name)
    {
      var companyRepository = this.repository.GetRepositoryAsync<Company>();
      var company = await companyRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
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
                        .Where(x => x.EntitySetName == "Inquiry" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到Inquiry对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
              switch (field.FieldName)
              {
                case "CompanyId":
                  var company_name = row[field.SourceFieldName].ToString();
                  var companyid = await this.getCompanyIdByNameAsync(company_name);
                  propertyInfo.SetValue(item, Convert.ChangeType(companyid, propertyInfo.PropertyType), null);
                  break;
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
              var propertyInfo = inquirytype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "Inquiry")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var inquiries = await this.Query(new InquiryQuery().Withfilter(filters)).Include(p => p.Company).Include(p => p.Customer).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = inquiries.Select(n => new
      {

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
      return await NPOIHelper.ExportExcelAsync("询价单", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task<string> CreateFromTask(int[] taskId)
    {
      var tasks = await this.inquiryTaskService.Queryable().Where(x => taskId.Contains(x.Id))
       .Include(x => x.InquiryTaskProducts)
       .ToListAsync();

      var order = this.mapper.Map<Inquiry>(tasks.First());
      order.InquiryNo = await KeyGenerator.GetRFQNo();
      order.Status = "草拟";
      foreach (var task in tasks)
      {
        foreach (var detail in task.InquiryTaskProducts)
        {
          var product = this.mapper.Map<InquiryProduct>(detail);
          product.InquiryId = order.Id;
          product.InquiryNo = order.InquiryNo;
          order.Inquiryproducts.Add(product);
        }
        var refitem = new Models.InquiryRef()
        {
          BeginDate = task.BeginDate,
          Dept = "",
          InquiryId = order.Id,
          InquiryNo = order.InquiryNo,
          Salesman = order.Salesman,
          Status = task.Status,
          TaskNo = task.TaskNo,
          Ver = order.Ver,

        };
        order.Inquiryrefs.Add(refitem);
      }
      this.Insert(order);
      return order.InquiryNo;
    }

    public async Task<Inquiry> CreateFromTaskProduct(int[] taskproductId)
    {
      var details = await this.inquiryTaskProductService.Queryable()
        .Where(x => taskproductId.Contains(x.Id))
        .Include(x => x.InquiryTask)
        .ToListAsync();
      var order = this.mapper.Map<Inquiry>(details.First().InquiryTask);
      order.InquiryNo = await KeyGenerator.GetRFQNo();
      order.Status = "草拟";
      foreach (var detail in details)
      {
        var product = this.mapper.Map<InquiryProduct>(detail);
        product.InquiryId = order.Id;
        product.InquiryNo = order.InquiryNo;
        order.Inquiryproducts.Add(product);
      }
      var heads = details.Select(x => x.InquiryTask).Distinct();
      foreach (var head in heads)
      {
        var refitem = new Models.InquiryRef()
        {
          BeginDate = head.BeginDate,
          Dept = "",
          InquiryId = order.Id,
          InquiryNo = order.InquiryNo,
          Salesman = order.Salesman,
          Status = head.Status,
          TaskNo = head.TaskNo,
          Ver = order.Ver,

        };
        order.Inquiryrefs.Add(refitem);
      }
      this.Insert(order);
      return order;
    }

    public async Task<(bool success, string msg)> VaildateApprove(int[] id)
    {
      var heads = await this.Queryable().Where(x => id.Contains(x.Id))
       .Include(x => x.Inquiryproducts).ToListAsync();
      var msg = "";
      foreach (var head in heads)
      {
        if (head.FeedbackDate == null)
        {
          msg += ( msg.IndexOf(head.InquiryNo) < 0 ) ? $"{head.InquiryNo}:反馈日期为空 " : "反馈日期为空 ";
        }
        foreach (var body in head.Inquiryproducts)
        {
          if (string.IsNullOrEmpty(body.SupplierCode))
          {
            msg += ( msg.IndexOf(head.InquiryNo) < 0 ) ? $"{head.InquiryNo}供应商为空 " : "供应商为空 ";
          }
          if (body.Qty == 0)
          {
            msg += ( msg.IndexOf(head.InquiryNo) < 0 ) ? $"{head.InquiryNo}询价数量必须大于0 " : "询价数量必须大于0 ";
          }
          if (body.Price == null || body.Price.Value == 0)
          {
            msg += ( msg.IndexOf(head.InquiryNo) < 0 ) ? $"{head.InquiryNo}询价单价必须大于0 " : "询价单价必须大于0 ";
          }
          if (string.IsNullOrEmpty(body.PriceType))
          {
            msg += ( msg.IndexOf(head.InquiryNo) < 0 ) ? $"{head.InquiryNo}询价价格类型为空 " : "询价价格类型为空 ";
          }
        }
      }

      return (string.IsNullOrEmpty(msg), msg);
    }

    public async Task SubmitApprove(int[] id, string to, string comment, string givenName)
    {
      var orders = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      var tousers = to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
      foreach (var order in orders)
      {
        order.Initiator = givenName;
        order.SubmitDate = DateTime.Now;
        order.ToAuditor = to;
        order.Status = "待审";
        foreach (var user in tousers)
        {
          var app = new ApproveHistory()
          {
            RefId = order.Id,
            RefKey = order.InquiryNo,
            Comment = comment,
            Initiator = givenName,
            Status = "待审",
            SubmitDate = DateTime.Now,
            ToAuditor = user
          };
          this.approveHistoryService.Insert(app);
          
        }
        this.Update(order);
      }
    }

    public async Task UndoApprove(int[] id)
    {
      var orders = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var order in orders)
      {
        order.Initiator = null;
        order.SubmitDate = null;
        order.ToAuditor = null;
        order.Status = "草拟";
        var apps =await this.approveHistoryService.Queryable()
          .Where(x => x.RefId == order.Id &&
           x.Status == "待审"
          ).ToListAsync();
        foreach (var app in apps)
        {
          this.approveHistoryService.Delete(app);
        }
        this.Update(order);
      }
    }

    public async Task TodoApprove(int[] id, string status, string result,string approver)
    {
      var orders = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var order in orders)
      {
        order.Status = status;
        order.ApprovedDate = DateTime.Now;
        order.Approver = approver;
        var apps = await this.approveHistoryService.Queryable()
          .Where(x => x.RefId == order.Id &&
           x.Status == "待审" &&
          x.ToAuditor==approver
         ).ToListAsync();
        foreach (var app in apps)
        {
          app.Status = status;
          app.Approver = approver;
          app.ApprovedDate = order.ApprovedDate;
          app.Result = result;
          this.approveHistoryService.Update(app);
        }
        //删除其它

        var notapps = await this.approveHistoryService.Queryable()
         .Where(x => x.RefId == order.Id &&
         x.Status=="待审" &&
         x.ToAuditor != approver
        ).ToListAsync();
        foreach (var app in notapps)
        {
          this.approveHistoryService.Delete(app);
        }
        this.Update(order);
      }
    }
  }
}