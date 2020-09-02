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
/// File: QuotationFilesController.cs
/// Purpose:出口管理/报价单附件
/// Created Date: 2020/8/26 17:35:24
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<QuotationFile>, Repository<QuotationFile>>();
///    container.RegisterType<IQuotationFileService, QuotationFileService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("QuotationFiles")]
	public class QuotationFilesController : Controller
	{
		private readonly IQuotationFileService  quotationFileService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public QuotationFilesController (
          IQuotationFileService  quotationFileService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.quotationFileService  = quotationFileService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: QuotationFiles/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "报价单附件", Order = 1)]
		public ActionResult Index() => this.View();


    //接收上传文件
    public async Task<ActionResult> Upload()
    {
      try
      {
        var file = this.Request.Files[0];
        var user = (string)ViewBag.GivenName;
        var tags = this.Request.Form["tags"];
        var name = this.Request.Form["name"];
        var quotationid = Convert.ToInt32(this.Request.Form["quotationid"]);
        var ver = Convert.ToInt32(this.Request.Form["ver"]);
        var qpno = this.Request.Form["qpno"];
        var folder = this.Server.MapPath("~/UploadFiles/Inquiry/Files/" + qpno);
        var relpath = "/UploadFiles/Quotation/Files/" + qpno + "/";
        this.quotationFileService.AddFile(ver, quotationid, qpno, file, folder, relpath, user);
        await this.unitOfWork.SaveChangesAsync();
        return Content($"{file.FileName}:上传成功", "text/plain");
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    //Get :QuotationFiles/GetData
    //For Index View datagrid datasource url

    [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.quotationFileService
						               .Query(new QuotationFileQuery().Withfilter(filters)).Include(q => q.Quotation)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByQuotationId (int  quotationid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.quotationFileService
						               .Query(new QuotationFileQuery().ByQuotationIdWithfilter(quotationid,filters)).Include(q => q.Quotation)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(QuotationFile[] quotationfiles)
		{
            try{
               this.quotationFileService.ApplyChanges( quotationfiles);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetQuotations(string q="")
		{
			var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
			var rows = await quotationRepository
                            .Queryable()
                            .Where(n=>n.QpNo.Contains(q))
                            .OrderBy(n=>n.QpNo)
                            .Select(n => new { Id = n.Id, QpNo = n.QpNo })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: QuotationFiles/Details/:id
		public ActionResult Details(int id)
		{
			
			var quotationFile = this.quotationFileService.Find(id);
			if (quotationFile == null)
			{
				return HttpNotFound();
			}
			return View(quotationFile);
		}
        //GET: QuotationFiles/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  quotationFile = await this.quotationFileService.FindAsync(id);
            return Json(quotationFile,JsonRequestBehavior.AllowGet);
        }
		//GET: QuotationFiles/Create
        		public ActionResult Create()
				{
			var quotationFile = new QuotationFile();
			//set default value
			var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
		   			ViewBag.QuotationId = new SelectList(quotationRepository.Queryable().OrderBy(n=>n.QpNo), "Id", "QpNo");
		   			return View(quotationFile);
		}
		//POST: QuotationFiles/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(QuotationFile quotationFile)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.quotationFileService.Insert(quotationFile);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a quotationFile record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
			//ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n=>n.QpNo).ToListAsync(), "Id", "QpNo", quotationFile.QuotationId);
			//return View(quotationFile);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var quotationFile = await Task.Run(() => {
                return new QuotationFile();
                });
            return Json(quotationFile, JsonRequestBehavior.AllowGet);
        }

         
		//GET: QuotationFiles/Edit/:id
		public ActionResult Edit(int id)
		{
			var quotationFile = this.quotationFileService.Find(id);
			if (quotationFile == null)
			{
				return HttpNotFound();
			}
			var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
			ViewBag.QuotationId = new SelectList(quotationRepository.Queryable().OrderBy(n=>n.QpNo), "Id", "QpNo", quotationFile.QuotationId);
			return View(quotationFile);
		}
		//POST: QuotationFiles/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(QuotationFile quotationFile)
		{
			if (ModelState.IsValid)
			{
				quotationFile.TrackingState = TrackingState.Modified;
				                try{
				this.quotationFileService.Update(quotationFile);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a QuotationFile record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
												//return View(quotationFile);
		}
        //删除当前记录
		//GET: QuotationFiles/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.quotationFileService.Queryable().Where(x => x.Id == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
		}
		 
       
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteChecked(int[] id) {
           try{
        var user = (string)ViewBag.GivenName;
        await this.quotationFileService.Delete(id, user);
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
		public async Task<ActionResult> ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "quotationfiles_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.quotationFileService.ExportExcelAsync(filterRules,sort, order );
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
        await this.quotationFileService.ImportDataTableAsync(data, Auth.GetFullName());
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
