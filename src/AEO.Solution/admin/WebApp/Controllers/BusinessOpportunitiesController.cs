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
/// File: BusinessOpportunitiesController.cs
/// Purpose:客户管理/商机管理
/// Created Date: 2020/8/12 15:15:17
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<BusinessOpportunity>, Repository<BusinessOpportunity>>();
///    container.RegisterType<IBusinessOpportunityService, BusinessOpportunityService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("BusinessOpportunities")]
	public class BusinessOpportunitiesController : Controller
	{
		private readonly IBusinessOpportunityService  businessOpportunityService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public BusinessOpportunitiesController (
          IBusinessOpportunityService  businessOpportunityService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.businessOpportunityService  = businessOpportunityService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: BusinessOpportunities/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "商机管理", Order = 1)]
		public ActionResult Index() => this.View();

    //删除阶段
    public async Task<JsonResult> DeleteStage(int id) {
      try
      {
        await this.businessOpportunityService.DeleteStage(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //新增当前阶段
    public async Task<JsonResult> AddStage(OpportunityStage stage) {
      try
      {
        await this.businessOpportunityService.AddStage(stage);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }

    }


		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.businessOpportunityService
						               .Query(new BusinessOpportunityQuery().Withfilter(filters)).Include(b => b.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Name = n.Name,
    Owner = n.Owner,
    CustomerId = n.CustomerId,
    ContactName = n.ContactName,
    OpDate = n.OpDate.ToString("yyyy-MM-dd HH:mm:ss"),
    ProvidePeople = n.ProvidePeople,
    Source = n.Source,
    MarketAction = n.MarketAction,
    Status = n.Status,
    Curr = n.Curr,
    PrDate = n.PrDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Amount = n.Amount,
    Content = n.Content,
    Stage = n.Stage,
    StageDate = n.StageDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByCustomerId (int  customerid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.businessOpportunityService
						               .Query(new BusinessOpportunityQuery().ByCustomerIdWithfilter(customerid,filters)).Include(b => b.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Name = n.Name,
    Owner = n.Owner,
    CustomerId = n.CustomerId,
    ContactName = n.ContactName,
    OpDate = n.OpDate.ToString("yyyy-MM-dd HH:mm:ss"),
    ProvidePeople = n.ProvidePeople,
    Source = n.Source,
    MarketAction = n.MarketAction,
    Status = n.Status,
    Curr = n.Curr,
    PrDate = n.PrDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Amount = n.Amount,
    Content = n.Content,
    Stage = n.Stage,
    StageDate = n.StageDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(BusinessOpportunity[] businessopportunities)
		{
            try{
               this.businessOpportunityService.ApplyChanges( businessopportunities);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetCustomers(string q="")
		{
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			var rows = await customerRepository
                            .Queryable()
                            .Where(n=>n.CustomerCode.Contains(q))
                            .OrderBy(n=>n.CustomerCode)
                            .Select(n => new { Id = n.Id, CustomerCode = n.CustomerCode })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: BusinessOpportunities/Details/:id
		public ActionResult Details(int id)
		{
			
			var businessOpportunity = this.businessOpportunityService.Find(id);
			if (businessOpportunity == null)
			{
				return HttpNotFound();
			}
			return View(businessOpportunity);
		}
        //GET: BusinessOpportunities/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  businessOpportunity = await this.businessOpportunityService.FindAsync(id);
            return Json(businessOpportunity,JsonRequestBehavior.AllowGet);
        }
		//GET: BusinessOpportunities/Create
        		public ActionResult Create()
				{
			var businessOpportunity = new BusinessOpportunity();
			//set default value
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(businessOpportunity);
		}
		//POST: BusinessOpportunities/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(BusinessOpportunity businessOpportunity)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.businessOpportunityService.Insert(businessOpportunity);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a businessOpportunity record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", businessOpportunity.CustomerId);
			//return View(businessOpportunity);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var businessOpportunity = await Task.Run(() => {
                return new BusinessOpportunity();
                });
            return Json(businessOpportunity, JsonRequestBehavior.AllowGet);
        }

         
		//GET: BusinessOpportunities/Edit/:id
		public ActionResult Edit(int id)
		{
			var businessOpportunity = this.businessOpportunityService.Find(id);
			if (businessOpportunity == null)
			{
				return HttpNotFound();
			}
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", businessOpportunity.CustomerId);
			return View(businessOpportunity);
		}
		//POST: BusinessOpportunities/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(BusinessOpportunity businessOpportunity)
		{
			if (ModelState.IsValid)
			{
				businessOpportunity.TrackingState = TrackingState.Modified;
				                try{
				this.businessOpportunityService.Update(businessOpportunity);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a BusinessOpportunity record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(businessOpportunity);
		}
        //删除当前记录
		//GET: BusinessOpportunities/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.businessOpportunityService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.businessOpportunityService.Delete(id);
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
			var fileName = "businessopportunities_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.businessOpportunityService.ExportExcelAsync(filterRules,sort, order );
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
        await this.businessOpportunityService.ImportDataTableAsync(data, Auth.GetFullName());
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
