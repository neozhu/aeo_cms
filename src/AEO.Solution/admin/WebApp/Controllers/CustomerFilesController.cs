using System;
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
/// File: CustomerFilesController.cs
/// Purpose:客户中心/附件管理
/// Created Date: 2020/7/3 14:09:50
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<CustomerFile>, Repository<CustomerFile>>();
///    container.RegisterType<ICustomerFileService, CustomerFileService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("CustomerFiles")]
	public class CustomerFilesController : Controller
	{
		private readonly ICustomerFileService  customerFileService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CustomerFilesController (
          ICustomerFileService  customerFileService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.customerFileService  = customerFileService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: CustomerFiles/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "客户附件管理", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :CustomerFiles/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.customerFileService
						               .Query(new CustomerFileQuery().Withfilter(filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
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
    RefKey = n.RefKey,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    CustomerId = n.CustomerId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByCustomerId (int  customerid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.customerFileService
						               .Query(new CustomerFileQuery().ByCustomerIdWithfilter(customerid,filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
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
    RefKey = n.RefKey,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    CustomerId = n.CustomerId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(CustomerFile[] customerfiles)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in customerfiles)
               {
                 this.customerFileService.ApplyChanges(item);
               }
			   var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		    }
            else
            {
                var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
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
								//GET: CustomerFiles/Details/:id
		public ActionResult Details(int id)
		{
			
			var customerFile = this.customerFileService.Find(id);
			if (customerFile == null)
			{
				return HttpNotFound();
			}
			return View(customerFile);
		}
        //GET: CustomerFiles/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  customerFile = await this.customerFileService.FindAsync(id);
            return Json(customerFile,JsonRequestBehavior.AllowGet);
        }
		//GET: CustomerFiles/Create
        		public ActionResult Create()
				{
			var customerFile = new CustomerFile();
			//set default value
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(customerFile);
		}
		//POST: CustomerFiles/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CustomerFile customerFile)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.customerFileService.Insert(customerFile);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a customerFile record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerFile.CustomerId);
			//return View(customerFile);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var customerFile = await Task.Run(() => {
                return new CustomerFile();
                });
            return Json(customerFile, JsonRequestBehavior.AllowGet);
        }

         
		//GET: CustomerFiles/Edit/:id
		public ActionResult Edit(int id)
		{
			var customerFile = this.customerFileService.Find(id);
			if (customerFile == null)
			{
				return HttpNotFound();
			}
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", customerFile.CustomerId);
			return View(customerFile);
		}
		//POST: CustomerFiles/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CustomerFile customerFile)
		{
			if (ModelState.IsValid)
			{
				customerFile.TrackingState = TrackingState.Modified;
				                try{
				this.customerFileService.Update(customerFile);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a CustomerFile record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(customerFile);
		}
        //删除当前记录
		//GET: CustomerFiles/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.customerFileService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.customerFileService.Delete(id);
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
			var fileName = "customerfiles_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.customerFileService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
