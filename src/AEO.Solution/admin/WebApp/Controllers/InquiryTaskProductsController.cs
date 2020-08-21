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
  /// File: InquiryTaskProductsController.cs
  /// Purpose:出口管理/运价任务产品明细
  /// Created Date: 2020/8/14 14:39:53
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<InquiryTaskProduct>, Repository<InquiryTaskProduct>>();
  ///    container.RegisterType<IInquiryTaskProductService, InquiryTaskProductService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("InquiryTaskProducts")]
  public class InquiryTaskProductsController : Controller
  {
    private readonly IInquiryTaskProductService inquiryTaskProductService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public InquiryTaskProductsController(
          IInquiryTaskProductService inquiryTaskProductService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.inquiryTaskProductService = inquiryTaskProductService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: InquiryTaskProducts/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "运价任务产品明细", Order = 1)]
    public ActionResult Index() => this.View();
    //上传图片
    [HttpPost]
    public async Task<JsonResult> UploadPicture()
    {

      try
      {
        var id = Convert.ToInt32(this.Request.Form["id"] ?? "0");
        var file = this.Request.Files[0];
        var dt = DateTime.Now.ToString("yyyyMMdd");
        var folder = this.Server.MapPath("~/UploadFiles/InquiryTaskProductFiles/" + dt);
        var relpath = "/UploadFiles/InquiryTaskProductFiles/" + dt + "/";
        var path = this.inquiryTaskProductService.UploadPicture(id, file, folder, relpath);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { path }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        throw e;
      }

    }
    //Get :InquiryTaskProducts/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inquiryTaskProductService
                           .Query(new InquiryTaskProductQuery().Withfilter(filters))
                           .Include(i => i.InquiryTask)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         InquiryTaskTaskNo = n.InquiryTask?.TaskNo,
                                         CustomerName = n.InquiryTask?.CustomerName,
                                         CompanyId=n.InquiryTask?.CompanyId,
                                         CompanyName=n.InquiryTask?.CompanyName,
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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByInquiryTaskId(int inquirytaskid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inquiryTaskProductService
                       .Query(new InquiryTaskProductQuery().ByInquiryTaskIdWithfilter(inquirytaskid, filters)).Include(i => i.InquiryTask)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(InquiryTaskProduct[] inquirytaskproducts)
    {
      try
      {
        this.inquiryTaskProductService.ApplyChanges(inquirytaskproducts);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetInquiryTasks(string q = "")
    {
      var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();
      var rows = await inquirytaskRepository
                            .Queryable()
                            .Where(n => n.TaskNo.Contains(q))
                            .OrderBy(n => n.TaskNo)
                            .Select(n => new { Id = n.Id, TaskNo = n.TaskNo })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }


    //GET: InquiryTaskProducts/Details/:id
    public ActionResult Details(int id)
    {

      var inquiryTaskProduct = this.inquiryTaskProductService.Find(id);
      if (inquiryTaskProduct == null)
      {
        return HttpNotFound();
      }
      return View(inquiryTaskProduct);
    }
    //GET: InquiryTaskProducts/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var inquiryTaskProduct = await this.inquiryTaskProductService.FindAsync(id);
      return Json(inquiryTaskProduct, JsonRequestBehavior.AllowGet);
    }
    //GET: InquiryTaskProducts/Create
    public ActionResult Create()
    {
      var inquiryTaskProduct = new InquiryTaskProduct();
      //set default value
      var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();
      ViewBag.InquiryTaskId = new SelectList(inquirytaskRepository.Queryable().OrderBy(n => n.TaskNo), "Id", "TaskNo");
      return View(inquiryTaskProduct);
    }
    //POST: InquiryTaskProducts/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(InquiryTaskProduct inquiryTaskProduct)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.inquiryTaskProductService.Insert(inquiryTaskProduct);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a inquiryTaskProduct record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();
      //ViewBag.InquiryTaskId = new SelectList(await inquirytaskRepository.Queryable().OrderBy(n=>n.TaskNo).ToListAsync(), "Id", "TaskNo", inquiryTaskProduct.InquiryTaskId);
      //return View(inquiryTaskProduct);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var inquiryTaskProduct = await Task.Run(() =>
      {
        return new InquiryTaskProduct();
      });
      return Json(inquiryTaskProduct, JsonRequestBehavior.AllowGet);
    }


    //GET: InquiryTaskProducts/Edit/:id
    public ActionResult Edit(int id)
    {
      var inquiryTaskProduct = this.inquiryTaskProductService.Find(id);
      if (inquiryTaskProduct == null)
      {
        return HttpNotFound();
      }
      var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();
      ViewBag.InquiryTaskId = new SelectList(inquirytaskRepository.Queryable().OrderBy(n => n.TaskNo), "Id", "TaskNo", inquiryTaskProduct.InquiryTaskId);
      return View(inquiryTaskProduct);
    }
    //POST: InquiryTaskProducts/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(InquiryTaskProduct inquiryTaskProduct)
    {
      if (ModelState.IsValid)
      {
        inquiryTaskProduct.TrackingState = TrackingState.Modified;
        try
        {
          this.inquiryTaskProductService.Update(inquiryTaskProduct);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a InquiryTaskProduct record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();
      //return View(inquiryTaskProduct);
    }
    //删除当前记录
    //GET: InquiryTaskProducts/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.inquiryTaskProductService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }




    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.inquiryTaskProductService.Delete(id);
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
      var fileName = "inquirytaskproducts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.inquiryTaskProductService.ExportExcelAsync(filterRules, sort, order);
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
        await this.inquiryTaskProductService.ImportDataTableAsync(data, Auth.GetFullName());
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
