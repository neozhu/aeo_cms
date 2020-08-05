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
/// File: CustomerSharesController.cs
/// Purpose:客户管理/共享记录
/// Created Date: 2020/8/5 11:20:13
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<CustomerShare>, Repository<CustomerShare>>();
///    container.RegisterType<ICustomerShareService, CustomerShareService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("CustomerShares")]
	public class CustomerSharesController : Controller
	{
		private readonly ICustomerShareService  customerShareService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CustomerSharesController (
          ICustomerShareService  customerShareService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.customerShareService  = customerShareService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: CustomerShares/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "共享记录", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :CustomerShares/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.customerShareService
						               .Query(new CustomerShareQuery().Withfilter(filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Owner = n.Owner,
    Dept = n.Dept,
    ShareTo = n.ShareTo,
    Module = n.Module,
    Searchable = n.Searchable,
    Editable = n.Editable,
    CustomerId = n.CustomerId,
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
			    var pagerows = (await this.customerShareService
						               .Query(new CustomerShareQuery().ByCustomerIdWithfilter(customerid,filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Owner = n.Owner,
    Dept = n.Dept,
    ShareTo = n.ShareTo,
    Module = n.Module,
    Searchable = n.Searchable,
    Editable = n.Editable,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(CustomerShare[] customershares)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in customershares)
               {
                 this.customerShareService.ApplyChanges(item);
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
		 
				
		//GET: CustomerShares/Details/:id
		public ActionResult Details(int id)
		{
			
			var customerShare = this.customerShareService.Find(id);
			if (customerShare == null)
			{
				return HttpNotFound();
			}
			return View(customerShare);
		}
        //GET: CustomerShares/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  customerShare = await this.customerShareService.FindAsync(id);
            return Json(customerShare,JsonRequestBehavior.AllowGet);
        }
		//GET: CustomerShares/Create
        		public ActionResult Create()
				{
			var customerShare = new CustomerShare();
			//set default value
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(customerShare);
		}
		//POST: CustomerShares/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CustomerShare customerShare)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.customerShareService.Insert(customerShare);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a customerShare record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerShare.CustomerId);
			//return View(customerShare);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var customerShare = await Task.Run(() => {
                return new CustomerShare();
                });
            return Json(customerShare, JsonRequestBehavior.AllowGet);
        }

         
		//GET: CustomerShares/Edit/:id
		public ActionResult Edit(int id)
		{
			var customerShare = this.customerShareService.Find(id);
			if (customerShare == null)
			{
				return HttpNotFound();
			}
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", customerShare.CustomerId);
			return View(customerShare);
		}
		//POST: CustomerShares/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CustomerShare customerShare)
		{
			if (ModelState.IsValid)
			{
				customerShare.TrackingState = TrackingState.Modified;
				                try{
				this.customerShareService.Update(customerShare);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a CustomerShare record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(customerShare);
		}
        //删除当前记录
		//GET: CustomerShares/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.customerShareService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.customerShareService.Delete(id);
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
			var fileName = "customershares_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.customerShareService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
