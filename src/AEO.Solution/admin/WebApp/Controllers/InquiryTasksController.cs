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
/// File: InquiryTasksController.cs
/// Purpose:出口管理/询价任务
/// Created Date: 2020/8/14 14:42:47
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<InquiryTask>, Repository<InquiryTask>>();
///    container.RegisterType<IInquiryTaskService, InquiryTaskService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("InquiryTasks")]
	public class InquiryTasksController : Controller
	{
		private readonly IInquiryTaskService  inquiryTaskService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public InquiryTasksController (
          IInquiryTaskService  inquiryTaskService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.inquiryTaskService  = inquiryTaskService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: InquiryTasks/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "询价任务", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :InquiryTasks/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.inquiryTaskService
						               .Query(new InquiryTaskQuery().Withfilter(filters)).Include(i => i.Company).Include(i => i.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CompanyName = n.Company?.Name,
                                         CustomerCode = n.Customer?.CustomerCode,
    InquiryTaskProducts = n.InquiryTaskProducts,
    Id = n.Id,
    TaskNo = n.TaskNo,
    Status = n.Status,
    Salesman = n.Salesman,
    CompanyId = n.CompanyId,
    CustomerId = n.CustomerId,
    CustomerName = n.CustomerName,
    Country = n.Country,
    Cur = n.Cur,
    ExchangeRate = n.ExchangeRate,
    ContactName = n.ContactName,
    ContactInfo = n.ContactInfo,
    BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Enddate = n.Enddate.ToString("yyyy-MM-dd HH:mm:ss"),
    Urgency = n.Urgency,
    Demande = n.Demande,
    PreRemind = n.PreRemind,
    Check1 = n.Check1,
    Creator = n.Creator,
    Executor = n.Executor,
    Check2 = n.Check2,
    Check3 = n.Check3,
    Owner = n.Owner
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByCompanyId (int  companyid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.inquiryTaskService
						               .Query(new InquiryTaskQuery().ByCompanyIdWithfilter(companyid,filters)).Include(i => i.Company).Include(i => i.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CompanyName = n.Company?.Name,
                                         CustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    TaskNo = n.TaskNo,
    Status = n.Status,
    Salesman = n.Salesman,
    CompanyId = n.CompanyId,
    CustomerId = n.CustomerId,
    CustomerName = n.CustomerName,
    Country = n.Country,
    Cur = n.Cur,
    ExchangeRate = n.ExchangeRate,
    ContactName = n.ContactName,
    ContactInfo = n.ContactInfo,
    BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Enddate = n.Enddate.ToString("yyyy-MM-dd HH:mm:ss"),
    Urgency = n.Urgency,
    Demande = n.Demande,
    PreRemind = n.PreRemind,
    Check1 = n.Check1,
    Creator = n.Creator,
    Executor = n.Executor,
    Check2 = n.Check2,
    Check3 = n.Check3,
    Owner = n.Owner
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByCustomerId (int  customerid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.inquiryTaskService
						               .Query(new InquiryTaskQuery().ByCustomerIdWithfilter(customerid,filters)).Include(i => i.Company).Include(i => i.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CompanyName = n.Company?.Name,
    CustomerCustomerCode = n.Customer?.CustomerCode,
    InquiryTaskProducts = n.InquiryTaskProducts,
    Id = n.Id,
    TaskNo = n.TaskNo,
    Status = n.Status,
    Salesman = n.Salesman,
    CompanyId = n.CompanyId,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    Country = n.Country,
    Cur = n.Cur,
    ExchangeRate = n.ExchangeRate,
    ContactName = n.ContactName,
    ContactInfo = n.ContactInfo,
    BeginDate = n.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Enddate = n.Enddate.ToString("yyyy-MM-dd HH:mm:ss"),
    Urgency = n.Urgency,
    Demande = n.Demande,
    PreRemind = n.PreRemind,
    Check1 = n.Check1,
    Creator = n.Creator,
    Executor = n.Executor,
    Check2 = n.Check2,
    Check3 = n.Check3,
    Owner = n.Owner
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(InquiryTask[] inquirytasks)
		{
            try{
               this.inquiryTaskService.ApplyChanges( inquirytasks);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetCompanies(string q="")
		{
			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			var rows = await companyRepository
                            .Queryable()
                            .Where(n=>n.Name.Contains(q))
                            .OrderBy(n=>n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
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
		 
				
		//GET: InquiryTasks/Details/:id
		public ActionResult Details(int id)
		{
			
			var inquiryTask = this.inquiryTaskService.Find(id);
			if (inquiryTask == null)
			{
				return HttpNotFound();
			}
			return View(inquiryTask);
		}
        //GET: InquiryTasks/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  inquiryTask = await this.inquiryTaskService.FindAsync(id);
            return Json(inquiryTask,JsonRequestBehavior.AllowGet);
        }
		//GET: InquiryTasks/Create
        		public ActionResult Create()
				{
			var inquiryTask = new InquiryTask();
			//set default value
			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
		   			ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name");
		   			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(inquiryTask);
		}
		//POST: InquiryTasks/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(InquiryTask inquiryTask)
		{
            if (ModelState.IsValid)
			{
				inquiryTask.TrackingState = TrackingState.Added;   
				foreach (var item in inquiryTask.InquiryTaskProducts)
				{
					item.InquiryTaskId = inquiryTask.Id ;
					item.TrackingState = TrackingState.Added;
				}
               try{ 
				this.inquiryTaskService.ApplyChanges(inquiryTask);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a inquiryTask record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			//ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", inquiryTask.CompanyId);
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", inquiryTask.CustomerId);
			//return View(inquiryTask);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var inquiryTask = await Task.Run(() => {
                return new InquiryTask();
                });
            return Json(inquiryTask, JsonRequestBehavior.AllowGet);
        }

         
		//GET: InquiryTasks/Edit/:id
		public ActionResult Edit(int id)
		{
			var inquiryTask = this.inquiryTaskService.Find(id);
			if (inquiryTask == null)
			{
				return HttpNotFound();
			}
			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name", inquiryTask.CompanyId);
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", inquiryTask.CustomerId);
			return View(inquiryTask);
		}
		//POST: InquiryTasks/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(InquiryTask inquiryTask)
		{
			if (ModelState.IsValid)
			{
				inquiryTask.TrackingState = TrackingState.Modified;
												foreach (var item in inquiryTask.InquiryTaskProducts)
				{
					item.InquiryTaskId = inquiryTask.Id ;
				}
				 
                try{
				this.inquiryTaskService.ApplyChanges(inquiryTask);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a InquiryTask record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
												//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(inquiryTask);
		}
        //删除当前记录
		//GET: InquiryTasks/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.inquiryTaskService.Queryable().Where(x => x.Id == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
		}
		 
		//Get Detail Row By Id For Edit
		//Get : InquiryTasks/EditInquiryTaskProduct/:id
		[HttpGet]
				public async Task<ActionResult> EditInquiryTaskProduct(int id)
				{
			var inquirytaskproductRepository = this.unitOfWork.RepositoryAsync<InquiryTaskProduct>();
						var inquirytaskproduct = await inquirytaskproductRepository.FindAsync(id);
									var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();             
						if (inquirytaskproduct == null)
			{
											ViewBag.InquiryTaskId = new SelectList(await inquirytaskRepository.Queryable().OrderBy(n=>n.TaskNo).ToListAsync(), "Id", "TaskNo" );
											//return HttpNotFound();
				return PartialView("_InquiryTaskProductEditForm", new InquiryTaskProduct());
			}
			else
			{
											 ViewBag.InquiryTaskId = new SelectList(await inquirytaskRepository.Queryable().ToListAsync(), "Id", "TaskNo" , inquirytaskproduct.InquiryTaskId );  
										}
			return PartialView("_InquiryTaskProductEditForm",  inquirytaskproduct);
		}
		//Get Create Row By Id For Edit
		//Get : InquiryTasks/CreateInquiryTaskProduct
		[HttpGet]
				public async Task<ActionResult> CreateInquiryTaskProduct(int inquirytaskid)
				{
		  			  var inquirytaskRepository = this.unitOfWork.RepositoryAsync<InquiryTask>();    
			  			  ViewBag.InquiryTaskId = new SelectList(await inquirytaskRepository.Queryable().OrderBy(n=>n.TaskNo).ToListAsync(), "Id", "TaskNo" );
			  		  			return PartialView("_InquiryTaskProductEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : InquiryTasks/DeleteInquiryTaskProduct/:id
		[HttpGet]
				public async Task<ActionResult> DeleteInquiryTaskProduct(int  id)
				{
            try{
			   var inquirytaskproductRepository = this.unitOfWork.RepositoryAsync<InquiryTaskProduct>();
			   inquirytaskproductRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
       
		//Get : InquiryTasks/GetInquiryTaskProductsByInquiryTaskId/:id
		[HttpGet]
		public async Task<JsonResult> GetInquiryTaskProductsByInquiryTaskId(int id)
		{
			var inquirytaskproducts = await this.inquiryTaskService.GetInquiryTaskProductsByInquiryTaskIdAsync(id);
			var rows = inquirytaskproducts.Select( n => new { 

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
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteChecked(int[] id) {
           try{
               await this.inquiryTaskService.Delete(id);
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
			var fileName = "inquirytasks_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.inquiryTaskService.ExportExcelAsync(filterRules,sort, order );
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
        await this.inquiryTaskService.ImportDataTableAsync(data, Auth.GetFullName());
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
