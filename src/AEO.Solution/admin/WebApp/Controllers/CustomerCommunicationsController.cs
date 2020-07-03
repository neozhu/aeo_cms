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
/// File: CustomerCommunicationsController.cs
/// Purpose:客户中心/沟通记录
/// Created Date: 2020/7/3 13:57:35
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<CustomerCommunication>, Repository<CustomerCommunication>>();
///    container.RegisterType<ICustomerCommunicationService, CustomerCommunicationService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("CustomerCommunications")]
	public class CustomerCommunicationsController : Controller
	{
		private readonly ICustomerCommunicationService  customerCommunicationService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CustomerCommunicationsController (
          ICustomerCommunicationService  customerCommunicationService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.customerCommunicationService  = customerCommunicationService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: CustomerCommunications/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "沟通记录", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :CustomerCommunications/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.customerCommunicationService
						               .Query(new CustomerCommunicationQuery().Withfilter(filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Title = n.Title,
    CommType = n.CommType,
    Status = n.Status,
    Salesman = n.Salesman,
    RefUsers = n.RefUsers,
    BeginDate = n.BeginDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    EndDate = n.EndDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
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
			    var pagerows = (await this.customerCommunicationService
						               .Query(new CustomerCommunicationQuery().ByCustomerIdWithfilter(customerid,filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Title = n.Title,
    CommType = n.CommType,
    Status = n.Status,
    Salesman = n.Salesman,
    RefUsers = n.RefUsers,
    BeginDate = n.BeginDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    EndDate = n.EndDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Remark = n.Remark,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    CustomerId = n.CustomerId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(CustomerCommunication[] customercommunications)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in customercommunications)
               {
                 this.customerCommunicationService.ApplyChanges(item);
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
								//GET: CustomerCommunications/Details/:id
		public ActionResult Details(int id)
		{
			
			var customerCommunication = this.customerCommunicationService.Find(id);
			if (customerCommunication == null)
			{
				return HttpNotFound();
			}
			return View(customerCommunication);
		}
        //GET: CustomerCommunications/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  customerCommunication = await this.customerCommunicationService.FindAsync(id);
            return Json(customerCommunication,JsonRequestBehavior.AllowGet);
        }
		//GET: CustomerCommunications/Create
        		public ActionResult Create()
				{
			var customerCommunication = new CustomerCommunication();
			//set default value
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(customerCommunication);
		}
		//POST: CustomerCommunications/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CustomerCommunication customerCommunication)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.customerCommunicationService.Insert(customerCommunication);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a customerCommunication record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerCommunication.CustomerId);
			//return View(customerCommunication);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var customerCommunication = await Task.Run(() => {
                return new CustomerCommunication();
                });
            return Json(customerCommunication, JsonRequestBehavior.AllowGet);
        }

         
		//GET: CustomerCommunications/Edit/:id
		public ActionResult Edit(int id)
		{
			var customerCommunication = this.customerCommunicationService.Find(id);
			if (customerCommunication == null)
			{
				return HttpNotFound();
			}
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", customerCommunication.CustomerId);
			return View(customerCommunication);
		}
		//POST: CustomerCommunications/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CustomerCommunication customerCommunication)
		{
			if (ModelState.IsValid)
			{
				customerCommunication.TrackingState = TrackingState.Modified;
				                try{
				this.customerCommunicationService.Update(customerCommunication);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a CustomerCommunication record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(customerCommunication);
		}
        //删除当前记录
		//GET: CustomerCommunications/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.customerCommunicationService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.customerCommunicationService.Delete(id);
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
			var fileName = "customercommunications_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.customerCommunicationService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
