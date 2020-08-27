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
/// File: QuotationChargesController.cs
/// Purpose:出口管理/报价单其它费用明细
/// Created Date: 2020/8/27 13:52:29
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<QuotationCharge>, Repository<QuotationCharge>>();
///    container.RegisterType<IQuotationChargeService, QuotationChargeService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("QuotationCharges")]
	public class QuotationChargesController : Controller
	{
		private readonly IQuotationChargeService  quotationChargeService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public QuotationChargesController (
          IQuotationChargeService  quotationChargeService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.quotationChargeService  = quotationChargeService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: QuotationCharges/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "报价单其它费用明细", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :QuotationCharges/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.quotationChargeService
						               .Query(new QuotationChargeQuery().Withfilter(filters)).Include(q => q.Quotation)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    QuotationQpNo = n.Quotation?.QpNo,
    Id = n.Id,
    Name = n.Name,
    EName = n.EName,
    Amount = n.Amount,
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
			    var pagerows = (await this.quotationChargeService
						               .Query(new QuotationChargeQuery().ByQuotationIdWithfilter(quotationid,filters)).Include(q => q.Quotation)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    QuotationQpNo = n.Quotation?.QpNo,
    Id = n.Id,
    Name = n.Name,
    EName = n.EName,
    Amount = n.Amount,
    QuotationId = n.QuotationId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(QuotationCharge[] quotationcharges)
		{
            try{
               this.quotationChargeService.ApplyChanges( quotationcharges);
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
		 
				
		//GET: QuotationCharges/Details/:id
		public ActionResult Details(int id)
		{
			
			var quotationCharge = this.quotationChargeService.Find(id);
			if (quotationCharge == null)
			{
				return HttpNotFound();
			}
			return View(quotationCharge);
		}
        //GET: QuotationCharges/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  quotationCharge = await this.quotationChargeService.FindAsync(id);
            return Json(quotationCharge,JsonRequestBehavior.AllowGet);
        }
		//GET: QuotationCharges/Create
        		public ActionResult Create()
				{
			var quotationCharge = new QuotationCharge();
			//set default value
			var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
		   			ViewBag.QuotationId = new SelectList(quotationRepository.Queryable().OrderBy(n=>n.QpNo), "Id", "QpNo");
		   			return View(quotationCharge);
		}
		//POST: QuotationCharges/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(QuotationCharge quotationCharge)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.quotationChargeService.Insert(quotationCharge);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a quotationCharge record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
			//ViewBag.QuotationId = new SelectList(await quotationRepository.Queryable().OrderBy(n=>n.QpNo).ToListAsync(), "Id", "QpNo", quotationCharge.QuotationId);
			//return View(quotationCharge);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var quotationCharge = await Task.Run(() => {
                return new QuotationCharge();
                });
            return Json(quotationCharge, JsonRequestBehavior.AllowGet);
        }

         
		//GET: QuotationCharges/Edit/:id
		public ActionResult Edit(int id)
		{
			var quotationCharge = this.quotationChargeService.Find(id);
			if (quotationCharge == null)
			{
				return HttpNotFound();
			}
			var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
			ViewBag.QuotationId = new SelectList(quotationRepository.Queryable().OrderBy(n=>n.QpNo), "Id", "QpNo", quotationCharge.QuotationId);
			return View(quotationCharge);
		}
		//POST: QuotationCharges/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(QuotationCharge quotationCharge)
		{
			if (ModelState.IsValid)
			{
				quotationCharge.TrackingState = TrackingState.Modified;
				                try{
				this.quotationChargeService.Update(quotationCharge);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a QuotationCharge record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var quotationRepository = this.unitOfWork.RepositoryAsync<Quotation>();
												//return View(quotationCharge);
		}
        //删除当前记录
		//GET: QuotationCharges/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.quotationChargeService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.quotationChargeService.Delete(id);
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
			var fileName = "quotationcharges_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.quotationChargeService.ExportExcelAsync(filterRules,sort, order );
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
        await this.quotationChargeService.ImportDataTableAsync(data, Auth.GetFullName());
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
