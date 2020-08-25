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
/// File: ApproveHistoriesController.cs
/// Purpose:出口管理/审批历史记录
/// Created Date: 2020/8/25 10:18:34
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ApproveHistory>, Repository<ApproveHistory>>();
///    container.RegisterType<IApproveHistoryService, ApproveHistoryService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ApproveHistories")]
	public class ApproveHistoriesController : Controller
	{
		private readonly IApproveHistoryService  approveHistoryService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ApproveHistoriesController (
          IApproveHistoryService  approveHistoryService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.approveHistoryService  = approveHistoryService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ApproveHistories/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "审批历史记录", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :ApproveHistories/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.approveHistoryService
						               .Query(new ApproveHistoryQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    Id = n.Id,
    RefId = n.RefId,
    RekKey = n.RefKey,
    Status = n.Status,
    Initiator = n.Initiator,
    SubmitDate = n.SubmitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    ToAuditor = n.ToAuditor,
    Approver = n.Approver,
    ApprovedDate = n.ApprovedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Result = n.Result,
    Comment = n.Comment,
    Remark = n.Remark
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
    //easyui datagrid post acceptChanges 

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByRefId(int refid,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.approveHistoryService
                           .Query(new ApproveHistoryQuery().WithRefIdfilter(refid,filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new {

                                         Id = n.Id,
                                         RefId = n.RefId,
                                         RekKey = n.RefKey,
                                         Status = n.Status,
                                         Initiator = n.Initiator,
                                         SubmitDate = n.SubmitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ToAuditor = n.ToAuditor,
                                         Approver = n.Approver,
                                         ApprovedDate = n.ApprovedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Result = n.Result,
                                         Comment = n.Comment,
                                         Remark = n.Remark
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 

    [HttpPost]
		public async Task<JsonResult> AcceptChanges(ApproveHistory[] approvehistories)
		{
            try{
               this.approveHistoryService.ApplyChanges( approvehistories);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				
		//GET: ApproveHistories/Details/:id
		public ActionResult Details(int id)
		{
			
			var approveHistory = this.approveHistoryService.Find(id);
			if (approveHistory == null)
			{
				return HttpNotFound();
			}
			return View(approveHistory);
		}
        //GET: ApproveHistories/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  approveHistory = await this.approveHistoryService.FindAsync(id);
            return Json(approveHistory,JsonRequestBehavior.AllowGet);
        }
		//GET: ApproveHistories/Create
        		public ActionResult Create()
				{
			var approveHistory = new ApproveHistory();
			//set default value
			return View(approveHistory);
		}
		//POST: ApproveHistories/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ApproveHistory approveHistory)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.approveHistoryService.Insert(approveHistory);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a approveHistory record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(approveHistory);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var approveHistory = await Task.Run(() => {
                return new ApproveHistory();
                });
            return Json(approveHistory, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ApproveHistories/Edit/:id
		public ActionResult Edit(int id)
		{
			var approveHistory = this.approveHistoryService.Find(id);
			if (approveHistory == null)
			{
				return HttpNotFound();
			}
			return View(approveHistory);
		}
		//POST: ApproveHistories/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ApproveHistory approveHistory)
		{
			if (ModelState.IsValid)
			{
				approveHistory.TrackingState = TrackingState.Modified;
				                try{
				this.approveHistoryService.Update(approveHistory);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a ApproveHistory record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(approveHistory);
		}
        //删除当前记录
		//GET: ApproveHistories/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.approveHistoryService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.approveHistoryService.Delete(id);
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
			var fileName = "approvehistories_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.approveHistoryService.ExportExcelAsync(filterRules,sort, order );
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
        await this.approveHistoryService.ImportDataTableAsync(data, Auth.GetFullName());
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
