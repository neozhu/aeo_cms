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
  /// File: QuotationsController.cs
  /// Purpose:出口管理/报价单
  /// Created Date: 2020/8/26 17:51:59
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Quotation>, Repository<Quotation>>();
  ///    container.RegisterType<IQuotationService, QuotationService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Quotations")]
  public class QuotationsController : Controller
  {
    private readonly IQuotationService quotationService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public QuotationsController(
          IQuotationService quotationService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.quotationService = quotationService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: Quotations/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "报价单", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :Quotations/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.quotationService
                           .Query(new QuotationQuery().Withfilter(filters)).Include(q => q.Company).Include(q => q.Customer)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         CompanyName = n.Company?.Name,
                                         CustomerCode = n.Customer?.CustomerCode,
                                         Id = n.Id,
                                         QpNo = n.QpNo,
                                         Status = n.Status,
                                         Salesman = n.Salesman,
                                         CompanyId = n.CompanyId,
                                         CompanyCode = n.CompanyCode,
                                         CustomerId = n.CustomerId,
                                         CustomerName = n.CustomerName,
                                         Country = n.Country,
                                         ContactName = n.ContactName,
                                         ContactInfo = n.ContactInfo,
                                         QuoteDate = n.QuoteDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LoadingPort = n.LoadingPort,
                                         DischargePort = n.DischargePort,
                                         Cur = n.Cur,
                                         ExchangeRate = n.ExchangeRate,
                                         PriceTerm = n.PriceTerm,
                                         PayMode = n.PayMode,
                                         GoodsAmount = n.GoodsAmount,
                                         ChargeAmount = n.ChargeAmount,
                                         TotalAmount = n.TotalAmount,
                                         FormName = n.FormName,
                                         Remark = n.Remark,
                                         InquiryNo = n.InquiryNo,
                                         TaskNo = n.TaskNo,
                                         Ver = n.Ver,
                                         Initiator = n.Initiator,
                                         SubmitDate = n.SubmitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ToAuditor = n.ToAuditor,
                                         Approver = n.Approver,
                                         ApprovedDate = n.ApprovedDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Quotation[] quotations)
    {
      try
      {
        this.quotationService.ApplyChanges(quotations);
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


    //GET: Quotations/Details/:id
    public ActionResult Details(int id)
    {

      var quotation = this.quotationService.Find(id);
      if (quotation == null)
      {
        return HttpNotFound();
      }
      return View(quotation);
    }
    //GET: Quotations/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var quotation = await this.quotationService.FindAsync(id);
      return Json(quotation, JsonRequestBehavior.AllowGet);
    }
    //GET: Quotations/Create
    public ActionResult Create()
    {
      var quotation = new Quotation();
      //set default value
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name");
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode");
      return View(quotation);
    }
    //POST: Quotations/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Quotation quotation)
    {
      if (ModelState.IsValid)
      {
        if (string.IsNullOrEmpty(quotation.QpNo))
        {
          quotation.QpNo =await KeyGenerator.GetQPNo();
        }
        quotation.TrackingState = TrackingState.Added;
        foreach (var item in quotation.QuotationFiles)
        {
          item.QuotationId = quotation.Id;
          item.QpNo = quotation.QpNo;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in quotation.QuotationProducts)
        {
          item.QuotationId = quotation.Id;
          item.QpNo = quotation.QpNo;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in quotation.QuotationCharges)
        {
          item.QuotationId = quotation.Id;
          item.TrackingState = TrackingState.Added;
        }
        try
        {
          this.quotationService.ApplyChanges(quotation);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a quotation record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", quotation.CompanyId);
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", quotation.CustomerId);
      //return View(quotation);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var quotation = await Task.Run(() =>
      {
        return new Quotation();
      });
      return Json(quotation, JsonRequestBehavior.AllowGet);
    }


    //GET: Quotations/Edit/:id
    public ActionResult Edit(int id)
    {
      var quotation = this.quotationService.Find(id);
      if (quotation == null)
      {
        return HttpNotFound();
      }
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name", quotation.CompanyId);
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode", quotation.CustomerId);
      return View(quotation);
    }
    //POST: Quotations/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Quotation quotation)
    {
      if (ModelState.IsValid)
      {
        quotation.TrackingState = TrackingState.Modified;
        foreach (var item in quotation.QuotationFiles)
        {
          item.QuotationId = quotation.Id;
        }
        foreach (var item in quotation.QuotationProducts)
        {
          item.QuotationId = quotation.Id;
        }

        try
        {
          this.quotationService.ApplyChanges(quotation);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Quotation record");
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
      //return View(quotation);
    }
    //删除当前记录
    //GET: Quotations/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.quotationService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get Detail Row By Id For Edit
    //Get : Quotations/EditQuotationFile/:id
    [HttpGet]
    public async Task<ActionResult> EditQuotationFile(int id)
    {
      var quotationfileRepository = this.unitOfWork.RepositoryAsync<QuotationFile>();
      var quotationfile = await quotationfileRepository.FindAsync(id);
      var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
      if (quotationfile == null)
      {
        ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n => n.QpNo).ToListAsync(), "Id", "QpNo");
        //return HttpNotFound();
        return PartialView("_QuotationFileEditForm", new QuotationFile());
      }
      else
      {
        ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().ToListAsync(), "Id", "QpNo", quotationfile.QuotationId);
      }
      return PartialView("_QuotationFileEditForm", quotationfile);
    }
    //Get Create Row By Id For Edit
    //Get : Quotations/CreateQuotationFile
    [HttpGet]
    public async Task<ActionResult> CreateQuotationFile(int quotationid)
    {
      var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
      ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n => n.QpNo).ToListAsync(), "Id", "QpNo");
      return PartialView("_QuotationFileEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Quotations/DeleteQuotationFile/:id
    [HttpGet]
    public async Task<ActionResult> DeleteQuotationFile(int id)
    {
      try
      {
        var quotationfileRepository = this.unitOfWork.RepositoryAsync<QuotationFile>();
        quotationfileRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : Quotations/EditQuotationProduct/:id
    [HttpGet]
    public async Task<ActionResult> EditQuotationProduct(int id)
    {
      var quotationproductRepository = this.unitOfWork.RepositoryAsync<QuotationProduct>();
      var quotationproduct = await quotationproductRepository.FindAsync(id);
      var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
      if (quotationproduct == null)
      {
        ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n => n.QpNo).ToListAsync(), "Id", "QpNo");
        //return HttpNotFound();
        return PartialView("_QuotationProductEditForm", new QuotationProduct());
      }
      else
      {
        ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().ToListAsync(), "Id", "QpNo", quotationproduct.QuotationId);
      }
      return PartialView("_QuotationProductEditForm", quotationproduct);
    }
    //Get Create Row By Id For Edit
    //Get : Quotations/CreateQuotationProduct
    [HttpGet]
    public async Task<ActionResult> CreateQuotationProduct(int quotationid)
    {
      var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
      ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n => n.QpNo).ToListAsync(), "Id", "QpNo");
      return PartialView("_QuotationProductEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Quotations/DeleteQuotationProduct/:id
    [HttpGet]
    public async Task<ActionResult> DeleteQuotationProduct(int id)
    {
      try
      {
        var quotationproductRepository = this.unitOfWork.RepositoryAsync<QuotationProduct>();
        quotationproductRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get : Quotations/GetQuotationFilesByQuotationId/:id
    [HttpGet]
    public async Task<JsonResult> GetQuotationFilesByQuotationId(int id)
    {
      var quotationfiles = await this.quotationService.GetQuotationFilesByQuotationIdAsync(id);
      var rows = quotationfiles.Select(n => new
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
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : Quotations/GetQuotationProductsByQuotationId/:id
    [HttpGet]
    public async Task<JsonResult> GetQuotationProductsByQuotationId(int id)
    {
      var quotationproducts = await this.quotationService.GetQuotationProductsByQuotationIdAsync(id);
      var rows = quotationproducts.Select(n => new
      {

        QuotationQpNo = n.Quotation?.QpNo,
        Id = n.Id,
        ProductNo = n.ProductNo,
        ProductName = n.ProductName,
        CategoryName = n.CategoryName,
        ProductEnName = n.ProductEnName,
        CnDescription = n.CnDescription,
        EnDescription = n.EnDescription,
        HSCODE = n.HSCODE,
        HSADDTAXRATE = n.HSADDTAXRATE,
        HSBACKTAXRATE = n.HSBACKTAXRATE,
        CUSTBASIC = n.CUSTBASIC,
        GUIDEPRICE = n.GUIDEPRICE,
        Remark = n.Remark,
        ThirdProductNo = n.ThirdProductNo,
        Qty = n.Qty,
        Unit = n.Unit,
        Price = n.Price,
        Cur = n.Cur,
        Amount = n.Amount,
        USDAmount = n.USDAmount,
        RMBAmount = n.RMBAmount,
        BrightcmsRate = n.BrightcmsRate,
        BrightcmsFcy = n.BrightcmsFcy,
        DarkcmsRate = n.DarkcmsRate,
        DarkcmsFcy = n.DarkcmsFcy,
        Executor = n.Executor,
        Logo = n.Logo,
        QpNo = n.QpNo,
        QuotationId = n.QuotationId,
        Ver = n.Ver
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }


    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.quotationService.Delete(id);
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
      var fileName = "quotations_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.quotationService.ExportExcelAsync(filterRules, sort, order);
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
        await this.quotationService.ImportDataTableAsync(data, Auth.GetFullName());
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
