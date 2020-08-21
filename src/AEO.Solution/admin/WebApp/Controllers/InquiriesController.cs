using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Z.EntityFramework.Plus;
using TrackableEntities;
using WebApp.Models;
using WebApp.Services;
using WebApp.Repositories;
namespace WebApp.Controllers
{
  /// <summary>
  /// File: InquiriesController.cs
  /// Purpose:出口管理/询价单
  /// Created Date: 2020/8/19 11:03:55
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Inquiry>, Repository<Inquiry>>();
  ///    container.RegisterType<IInquiryService, InquiryService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Inquiries")]
  public class InquiriesController : Controller
  {
    private readonly IInquiryService inquiryService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public InquiriesController(
          IInquiryService inquiryService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.inquiryService = inquiryService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: Inquiries/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "询价单", Order = 1)]
    public ActionResult Index() => this.View();
    //生成询价单
    [HttpPost]
    public async Task<JsonResult> CreateInquiryFromTask(int[] id) {
      try
      {
        var inquiryno = await this.inquiryService.CreateFromTask(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, inquiryno }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //根据明细生成询价单
    [HttpPost]
    public async Task<JsonResult> CreateInquiryFromTaskProduct(int[] id) {
  
        var item = await this.inquiryService.CreateFromTaskProduct(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return new JsonNetResult() { Data =new { success = true, item } };

      
    }
    //Get :Inquiries/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inquiryService
                           .Query(new InquiryQuery().Withfilter(filters)).Include(i => i.Company).Include(i => i.Customer)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         CompanyName = n.Company?.Name,
                                         CustomerCode = n.Customer?.CustomerCode,
                                         CustomerName = n.Customer?.CustomerName,
                                         Id = n.Id,
                                         InquiryNo = n.InquiryNo,
                                         TaskNo = n.TaskNo,
                                         Status = n.Status,
                                         Salesman = n.Salesman,
                                         BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         FeedbackDate = n.FeedbackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Demande = n.Demande,
                                         CustomerId = n.CustomerId,
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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByCustomerId(int customerid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inquiryService
                       .Query(new InquiryQuery().ByCustomerIdWithfilter(customerid, filters)).Include(i => i.Company).Include(i => i.Customer)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CompanyName = n.Company?.Name,
                                     CustomerCode = n.Customer?.CustomerCode,
                                     CustomerName = n.Customer?.CustomerName,
                                     Id = n.Id,
                                     InquiryNo = n.InquiryNo,
                                     TaskNo = n.TaskNo,
                                     Status = n.Status,
                                     Salesman = n.Salesman,
                                     BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     FeedbackDate = n.FeedbackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Demande = n.Demande,
                                     CustomerId = n.CustomerId,
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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByCompanyId(int companyid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inquiryService
                       .Query(new InquiryQuery().ByCompanyIdWithfilter(companyid, filters)).Include(i => i.Company).Include(i => i.Customer)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CompanyName = n.Company?.Name,
                                     CustomerCode = n.Customer?.CustomerCode,
                                     CustomerName = n.Customer?.CustomerName,
                                     Id = n.Id,
                                     InquiryNo = n.InquiryNo,
                                     TaskNo = n.TaskNo,
                                     Status = n.Status,
                                     Salesman = n.Salesman,
                                     BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     FeedbackDate = n.FeedbackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Demande = n.Demande,
                                     CustomerId = n.CustomerId,
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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Inquiry[] inquiries)
    {
      try
      {
        this.inquiryService.ApplyChanges(inquiries);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetCompanies(string q = "")
    {
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      var rows = await companyRepository
                            .Queryable()
                            .Where(n => n.Name.Contains(q))
                            .OrderBy(n => n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }

    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetCustomers(string q = "")
    {
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      var rows = await customerRepository
                            .Queryable()
                            .Where(n => n.CustomerCode.Contains(q))
                            .OrderBy(n => n.CustomerCode)
                            .Select(n => new { Id = n.Id, CustomerCode = n.CustomerCode })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }


    //GET: Inquiries/Details/:id
    public ActionResult Details(int id)
    {

      var inquiry = this.inquiryService.Find(id);
      if (inquiry == null)
      {
        return HttpNotFound();
      }
      return View(inquiry);
    }
    //GET: Inquiries/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var inquiry = await this.inquiryService.FindAsync(id);
      return Json(inquiry, JsonRequestBehavior.AllowGet);
    }
    //GET: Inquiries/Create
    public ActionResult Create()
    {
      var inquiry = new Inquiry();
      //set default value
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name");
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode");
      return View(inquiry);
    }
    //POST: Inquiries/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Inquiry inquiry)
    {
      if (ModelState.IsValid)
      {
        inquiry.TrackingState = TrackingState.Added;
        if (string.IsNullOrEmpty(inquiry.InquiryNo))
        {
          inquiry.InquiryNo =await KeyGenerator.GetRFQNo();
        }
        foreach (var item in inquiry.Inquiryfiles)
        {
          item.InquiryId = inquiry.Id;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in inquiry.Inquiryproducts)
        {
          item.InquiryId = inquiry.Id;
          item.InquiryNo = inquiry.InquiryNo;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in inquiry.Inquiryrefs)
        {
          item.InquiryId = inquiry.Id;
          item.InquiryNo = inquiry.InquiryNo;
          item.TrackingState = TrackingState.Added;
        }
        try
        {
          this.inquiryService.ApplyChanges(inquiry);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a inquiry record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var inquiry = await Task.Run(() =>
      {
        return new Inquiry();
      });
      return Json(inquiry, JsonRequestBehavior.AllowGet);
    }


    //GET: Inquiries/Edit/:id
    public ActionResult Edit(int id)
    {
      var inquiry = this.inquiryService.Find(id);
      if (inquiry == null)
      {
        return HttpNotFound();
      }
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name", inquiry.CompanyId);
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode", inquiry.CustomerId);
      return View(inquiry);
    }
    //POST: Inquiries/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Inquiry inquiry)
    {
      if (ModelState.IsValid)
      {
        inquiry.TrackingState = TrackingState.Modified;
        foreach (var item in inquiry.Inquiryfiles)
        {
          item.InquiryId = inquiry.Id;
        }
        foreach (var item in inquiry.Inquiryproducts)
        {
          item.InquiryId = inquiry.Id;
        }
        foreach (var item in inquiry.Inquiryrefs)
        {
          item.InquiryId = inquiry.Id;
        }

        try
        {
          this.inquiryService.ApplyChanges(inquiry);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Inquiry record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //return View(inquiry);
    }
    //删除当前记录
    //GET: Inquiries/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.inquiryService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get Detail Row By Id For Edit
    //Get : Inquiries/EditInquiryFile/:id
    [HttpGet]
    public async Task<ActionResult> EditInquiryFile(int id)
    {
      var inquiryfileRepository = this.unitOfWork.RepositoryAsync<InquiryFile>();
      var inquiryfile = await inquiryfileRepository.FindAsync(id);
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      if (inquiryfile == null)
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
        //return HttpNotFound();
        return PartialView("_InquiryFileEditForm", new InquiryFile());
      }
      else
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().ToListAsync(), "Id", "InquiryNo", inquiryfile.InquiryId);
      }
      return PartialView("_InquiryFileEditForm", inquiryfile);
    }
    //Get Create Row By Id For Edit
    //Get : Inquiries/CreateInquiryFile
    [HttpGet]
    public async Task<ActionResult> CreateInquiryFile(int inquiryid)
    {
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
      return PartialView("_InquiryFileEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Inquiries/DeleteInquiryFile/:id
    [HttpGet]
    public async Task<ActionResult> DeleteInquiryFile(int id)
    {
      try
      {
        var inquiryfileRepository = this.unitOfWork.RepositoryAsync<InquiryFile>();
        inquiryfileRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : Inquiries/EditInquiryProduct/:id
    [HttpGet]
    public async Task<ActionResult> EditInquiryProduct(int id)
    {
      var inquiryproductRepository = this.unitOfWork.RepositoryAsync<InquiryProduct>();
      var inquiryproduct = await inquiryproductRepository.FindAsync(id);
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      if (inquiryproduct == null)
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
        //return HttpNotFound();
        return PartialView("_InquiryProductEditForm", new InquiryProduct());
      }
      else
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().ToListAsync(), "Id", "InquiryNo", inquiryproduct.InquiryId);
      }
      return PartialView("_InquiryProductEditForm", inquiryproduct);
    }
    //Get Create Row By Id For Edit
    //Get : Inquiries/CreateInquiryProduct
    [HttpGet]
    public async Task<ActionResult> CreateInquiryProduct(int inquiryid)
    {
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
      return PartialView("_InquiryProductEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Inquiries/DeleteInquiryProduct/:id
    [HttpGet]
    public async Task<ActionResult> DeleteInquiryProduct(int id)
    {
      try
      {
        var inquiryproductRepository = this.unitOfWork.RepositoryAsync<InquiryProduct>();
        inquiryproductRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : Inquiries/EditInquiryRef/:id
    [HttpGet]
    public async Task<ActionResult> EditInquiryRef(int id)
    {
      var inquiryrefRepository = this.unitOfWork.RepositoryAsync<InquiryRef>();
      var inquiryref = await inquiryrefRepository.FindAsync(id);
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      if (inquiryref == null)
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
        //return HttpNotFound();
        return PartialView("_InquiryRefEditForm", new InquiryRef());
      }
      else
      {
        ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().ToListAsync(), "Id", "InquiryNo", inquiryref.InquiryId);
      }
      return PartialView("_InquiryRefEditForm", inquiryref);
    }
    //Get Create Row By Id For Edit
    //Get : Inquiries/CreateInquiryRef
    [HttpGet]
    public async Task<ActionResult> CreateInquiryRef(int inquiryid)
    {
      var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
      ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n => n.InquiryNo).ToListAsync(), "Id", "InquiryNo");
      return PartialView("_InquiryRefEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Inquiries/DeleteInquiryRef/:id
    [HttpGet]
    public async Task<ActionResult> DeleteInquiryRef(int id)
    {
      try
      {
        var inquiryrefRepository = this.unitOfWork.RepositoryAsync<InquiryRef>();
        inquiryrefRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get : Inquiries/GetInquiryfilesByInquiryId/:id
    [HttpGet]
    public async Task<JsonResult> GetInquiryfilesByInquiryId(int id)
    {
      var inquiryfiles = await this.inquiryService.GetInquiryfilesByInquiryIdAsync(id);
      var rows = inquiryfiles.Select(n => new
      {

        InquiryInquiryNo = n.Inquiry?.InquiryNo,
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
        Ver = n.Ver,
        InquiryId = n.InquiryId
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : Inquiries/GetInquiryproductsByInquiryId/:id
    [HttpGet]
    public async Task<JsonResult> GetInquiryproductsByInquiryId(int id)
    {
      var inquiryproducts = await this.inquiryService.GetInquiryproductsByInquiryIdAsync(id);
      var rows = inquiryproducts.Select(n => new
      {

        InquiryInquiryNo = n.Inquiry?.InquiryNo,
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
        Executor = n.Executor,
        SupplierCode = n.SupplierCode,
        SupplierName = n.SupplierName,
        SupplierProductNo = n.SupplierProductNo,
        PriceType = n.PriceType,
        Price = n.Price,
        Cur = n.Cur,
        MinQty = n.MinQty,
        PriceDate = n.PriceDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Feedback = n.Feedback,
        Recommended = n.Recommended,
        SamplePic = n.SamplePic,
        InquiryNo = n.InquiryNo,
        TaskNo = n.TaskNo,
        Ver = n.Ver,
        InquiryId = n.InquiryId
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : Inquiries/GetInquiryrefsByInquiryId/:id
    [HttpGet]
    public async Task<JsonResult> GetInquiryrefsByInquiryId(int id)
    {
      var inquiryrefs = await this.inquiryService.GetInquiryrefsByInquiryIdAsync(id);
      var rows = inquiryrefs.Select(n => new
      {

        InquiryInquiryNo = n.Inquiry?.InquiryNo,
        Id = n.Id,
        InquiryNo = n.InquiryNo,
        TaskNo = n.TaskNo,
        Status = n.Status,
        BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
        Salesman = n.Salesman,
        Dept = n.Dept,
        Ver = n.Ver,
        InquiryId = n.InquiryId
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }


    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.inquiryService.Delete(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //导出Excel
    [HttpPost]
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "inquiries_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.inquiryService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    //导入数据
    [HttpPost]
    public async Task<JsonResult> ImportData()
    {
      var watch = new Stopwatch();
      watch.Start();
      var uploadfile = this.Request.Files[0];
      var uploadfilename = uploadfile.FileName;
      var model = this.Request.Form["model"] ?? "model";
      var autosave = Convert.ToBoolean(this.Request.Form["autosave"] ?? "false");
      try
      {

        var ext = Path.GetExtension(uploadfilename);
        var newfileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{uploadfile.FileName.Replace(ext, "")}{ext}";//重组成新的文件名
        var stream = new MemoryStream();
        await uploadfile.InputStream.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        uploadfile.InputStream.Seek(0, SeekOrigin.Begin);
        var data = await NPOIHelper.GetDataTableFromExcelAsync(stream, ext);
        await this.inquiryService.ImportDataTableAsync(data, Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();
        if (autosave)
        {
          var folder = this.Server.MapPath($"/UploadFiles/{model}");
          if (!Directory.Exists(folder))
          {
            Directory.CreateDirectory(folder);
          }
          var savepath = Path.Combine(folder, newfileName);
          uploadfile.SaveAs(savepath);
        }
        watch.Stop();
        //获取当前实例测量得出的总运行时间（以毫秒为单位）
        var elapsedTime = watch.ElapsedMilliseconds.ToString();
        return Json(new { success = true, filename = newfileName, elapsedTime = elapsedTime }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        var message = e.GetMessage();
        this.logger.Error(e, $"导入失败,文件名:{uploadfilename}");
        return this.Json(new { success = false, filename = uploadfilename, message = message }, JsonRequestBehavior.AllowGet);
      }
    }

  }
}
