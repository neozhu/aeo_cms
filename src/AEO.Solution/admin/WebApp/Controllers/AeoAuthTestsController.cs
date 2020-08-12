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
using WebApp.Models.Dto;

namespace WebApp.Controllers
{
  /// <summary>
  /// File: AeoAuthTestsController.cs
  /// Purpose:AEO高认自测/自测记录
  /// Created Date: 2020/8/11 9:27:09
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<AeoAuthTest>, Repository<AeoAuthTest>>();
  ///    container.RegisterType<IAeoAuthTestService, AeoAuthTestService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("AeoAuthTests")]
  public class AeoAuthTestsController : Controller
  {
    private readonly IAeoAuthTestService aeoAuthTestService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public AeoAuthTestsController(
          IAeoAuthTestService aeoAuthTestService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.aeoAuthTestService = aeoAuthTestService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: AeoAuthTests/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "自测记录", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :AeoAuthTests/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    public async Task<JsonResult> GetTestNo() {

      var result =await KeyGenerator.GetTestNo();
      return Json(result, JsonRequestBehavior.AllowGet);
      }

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.aeoAuthTestService
                           .Query(new AeoAuthTestQuery().Withfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Aeoquestions = n.Aeoquestions,
                                         Id = n.Id,
                                         Name = n.Name,
                                         TradeCode = n.TradeCode,
                                         CreditCode = n.CreditCode,
                                         Ctype = n.Ctype,
                                         TestNo = n.TestNo,
                                         AuthType = n.AuthType,
                                         MasterCustom = n.MasterCustom,
                                         RegistDate = n.RegistDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         IsForeign = n.IsForeign,
                                         Zone = n.Zone,
                                         RegistedTime = n.RegistedTime,
                                         Unit = n.Unit,
                                         AuthDate = n.AuthDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Tester = n.Tester,
                                         Year = n.Year,
                                         BeginDate = n.BeginDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         EndDate = n.EndDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Remark = n.Remark,
                                         Status = n.Status,
                                         StdScore = n.StdScore,
                                         Score = n.Score,
                                         Result = n.Result
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(AeoAuthTest[] aeoauthtests)
    {
      try
      {
        this.aeoAuthTestService.ApplyChanges(aeoauthtests);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //GET: AeoAuthTests/Details/:id
    public ActionResult Details(int id)
    {

      var aeoAuthTest = this.aeoAuthTestService.Find(id);
      if (aeoAuthTest == null)
      {
        return HttpNotFound();
      }
      return View(aeoAuthTest);
    }
    //GET: AeoAuthTests/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var aeoAuthTest = await this.aeoAuthTestService.FindAsync(id);
      return Json(aeoAuthTest, JsonRequestBehavior.AllowGet);
    }
    //GET: AeoAuthTests/Create
    public ActionResult Create()
    {
      var aeoAuthTest = new AeoAuthTest();
      //set default value
      return View(aeoAuthTest);
    }
    //POST: AeoAuthTests/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateAeoQuestionTestDto aeoAuthTest)
    {
      if (ModelState.IsValid)
      {
        
        try
        {
          await this.aeoAuthTestService.CreateTest(aeoAuthTest);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a aeoAuthTest record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(aeoAuthTest);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var aeoAuthTest = await Task.Run(() =>
      {
        return new AeoAuthTest();
      });
      return Json(aeoAuthTest, JsonRequestBehavior.AllowGet);
    }


    //GET: AeoAuthTests/Edit/:id
    public ActionResult Edit(int id)
    {
      var aeoAuthTest = this.aeoAuthTestService.Find(id);
      if (aeoAuthTest == null)
      {
        return HttpNotFound();
      }
      return View(aeoAuthTest);
    }
    //POST: AeoAuthTests/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(AeoAuthTest aeoAuthTest)
    {
      if (ModelState.IsValid)
      {
        aeoAuthTest.TrackingState = TrackingState.Modified;
        foreach (var item in aeoAuthTest.Aeoquestions)
        {
          item.AeoAuthTestId = aeoAuthTest.Id;
        }

        try
        {
          this.aeoAuthTestService.ApplyChanges(aeoAuthTest);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a AeoAuthTest record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(aeoAuthTest);
    }
    //删除当前记录
    //GET: AeoAuthTests/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.aeoAuthTestService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get Detail Row By Id For Edit
    //Get : AeoAuthTests/EditAeoQuestion/:id
    [HttpGet]
    public async Task<ActionResult> EditAeoQuestion(int id)
    {
      var aeoquestionRepository = this.unitOfWork.RepositoryAsync<AeoQuestion>();
      var aeoquestion = await aeoquestionRepository.FindAsync(id);
      var aeoauthtestRepository = this.unitOfWork.RepositoryAsync<AeoAuthTest>();
      if (aeoquestion == null)
      {
        ViewBag.AeoAuthTestId = new SelectList(await aeoauthtestRepository.Queryable().OrderBy(n => n.Name).ToListAsync(), "Id", "Name");
        //return HttpNotFound();
        return PartialView("_AeoQuestionEditForm", new AeoQuestion());
      }
      else
      {
        ViewBag.AeoAuthTestId = new SelectList(await aeoauthtestRepository.Queryable().ToListAsync(), "Id", "Name", aeoquestion.AeoAuthTestId);
      }
      return PartialView("_AeoQuestionEditForm", aeoquestion);
    }
    //Get Create Row By Id For Edit
    //Get : AeoAuthTests/CreateAeoQuestion
    [HttpGet]
    public async Task<ActionResult> CreateAeoQuestion(int aeoauthtestid)
    {
      var aeoauthtestRepository = this.unitOfWork.RepositoryAsync<AeoAuthTest>();
      ViewBag.AeoAuthTestId = new SelectList(await aeoauthtestRepository.Queryable().OrderBy(n => n.Name).ToListAsync(), "Id", "Name");
      return PartialView("_AeoQuestionEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : AeoAuthTests/DeleteAeoQuestion/:id
    [HttpGet]
    public async Task<ActionResult> DeleteAeoQuestion(int id)
    {
      try
      {
        var aeoquestionRepository = this.unitOfWork.RepositoryAsync<AeoQuestion>();
        aeoquestionRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get : AeoAuthTests/GetAeoquestionsByAeoAuthTestId/:id
    [HttpGet]
    public async Task<JsonResult> GetAeoquestionsByAeoAuthTestId(int id)
    {
      var aeoquestions = await this.aeoAuthTestService.GetAeoquestionsByAeoAuthTestIdAsync(id);
      var rows = aeoquestions.Select(n => new
      {

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
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }


    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.aeoAuthTestService.Delete(id);
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
      var fileName = "aeoauthtests_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.aeoAuthTestService.ExportExcelAsync(filterRules, sort, order);
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
        await this.aeoAuthTestService.ImportDataTableAsync(data, Auth.GetFullName());
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
