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
/// File: OpportunityStagesController.cs
/// Purpose:客户管理/商机进展记录
/// Created Date: 2020/8/12 15:03:26
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<OpportunityStage>, Repository<OpportunityStage>>();
///    container.RegisterType<IOpportunityStageService, OpportunityStageService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("OpportunityStages")]
	public class OpportunityStagesController : Controller
	{
		private readonly IOpportunityStageService  opportunityStageService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public OpportunityStagesController (
          IOpportunityStageService  opportunityStageService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.opportunityStageService  = opportunityStageService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: OpportunityStages/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "商机进展记录", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :OpportunityStages/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.opportunityStageService
						               .Query(new OpportunityStageQuery().Withfilter(filters)).Include(o => o.BusinessOpportunity)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BusinessOpportunityName = n.BusinessOpportunity?.Name,
    Id = n.Id,
    Stage = n.Stage,
    SuccessRate = n.SuccessRate,
    ConfirmDate = n.ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
    BusinessOpportunityId = n.BusinessOpportunityId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByBusinessOpportunityId (int  businessopportunityid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.opportunityStageService
						               .Query(new OpportunityStageQuery().ByBusinessOpportunityIdWithfilter(businessopportunityid,filters)).Include(o => o.BusinessOpportunity)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BusinessOpportunityName = n.BusinessOpportunity?.Name,
    Id = n.Id,
    Stage = n.Stage,
    SuccessRate = n.SuccessRate,
    ConfirmDate = n.ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
    BusinessOpportunityId = n.BusinessOpportunityId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(OpportunityStage[] opportunitystages)
		{
            try{
               this.opportunityStageService.ApplyChanges( opportunitystages);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetBusinessOpportunities(string q="")
		{
			var businessopportunityRepository = this.unitOfWork.RepositoryAsync<BusinessOpportunity>();
			var rows = await businessopportunityRepository
                            .Queryable()
                            .Where(n=>n.Name.Contains(q))
                            .OrderBy(n=>n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: OpportunityStages/Details/:id
		public ActionResult Details(int id)
		{
			
			var opportunityStage = this.opportunityStageService.Find(id);
			if (opportunityStage == null)
			{
				return HttpNotFound();
			}
			return View(opportunityStage);
		}
        //GET: OpportunityStages/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  opportunityStage = await this.opportunityStageService.FindAsync(id);
            return Json(opportunityStage,JsonRequestBehavior.AllowGet);
        }
		//GET: OpportunityStages/Create
        		public ActionResult Create()
				{
			var opportunityStage = new OpportunityStage();
			//set default value
			var businessopportunityRepository = this.unitOfWork.RepositoryAsync<BusinessOpportunity>();
		   			ViewBag.BusinessOpportunityId = new SelectList(businessopportunityRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name");
		   			return View(opportunityStage);
		}
		//POST: OpportunityStages/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(OpportunityStage opportunityStage)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.opportunityStageService.Insert(opportunityStage);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a opportunityStage record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var businessopportunityRepository = this.unitOfWork.RepositoryAsync<BusinessOpportunity>();
			//ViewBag.BusinessOpportunityId = new SelectList(await businessopportunityRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", opportunityStage.BusinessOpportunityId);
			//return View(opportunityStage);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var opportunityStage = await Task.Run(() => {
                return new OpportunityStage();
                });
            return Json(opportunityStage, JsonRequestBehavior.AllowGet);
        }

         
		//GET: OpportunityStages/Edit/:id
		public ActionResult Edit(int id)
		{
			var opportunityStage = this.opportunityStageService.Find(id);
			if (opportunityStage == null)
			{
				return HttpNotFound();
			}
			var businessopportunityRepository = this.unitOfWork.RepositoryAsync<BusinessOpportunity>();
			ViewBag.BusinessOpportunityId = new SelectList(businessopportunityRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name", opportunityStage.BusinessOpportunityId);
			return View(opportunityStage);
		}
		//POST: OpportunityStages/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(OpportunityStage opportunityStage)
		{
			if (ModelState.IsValid)
			{
				opportunityStage.TrackingState = TrackingState.Modified;
				                try{
				this.opportunityStageService.Update(opportunityStage);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a OpportunityStage record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var businessopportunityRepository = this.unitOfWork.RepositoryAsync<BusinessOpportunity>();
												//return View(opportunityStage);
		}
        //删除当前记录
		//GET: OpportunityStages/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.opportunityStageService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.opportunityStageService.Delete(id);
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
			var fileName = "opportunitystages_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.opportunityStageService.ExportExcelAsync(filterRules,sort, order );
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
        await this.opportunityStageService.ImportDataTableAsync(data, Auth.GetFullName());
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
