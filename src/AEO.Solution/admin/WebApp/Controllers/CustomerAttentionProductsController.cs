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
/// File: CustomerAttentionProductsController.cs
/// Purpose:客户管理/关注产品信息
/// Created Date: 2020/8/5 11:41:13
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<CustomerAttentionProduct>, Repository<CustomerAttentionProduct>>();
///    container.RegisterType<ICustomerAttentionProductService, CustomerAttentionProductService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("CustomerAttentionProducts")]
	public class CustomerAttentionProductsController : Controller
	{
		private readonly ICustomerAttentionProductService  customerAttentionProductService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CustomerAttentionProductsController (
          ICustomerAttentionProductService  customerAttentionProductService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.customerAttentionProductService  = customerAttentionProductService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: CustomerAttentionProducts/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "关注产品信息", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :CustomerAttentionProducts/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.customerAttentionProductService
						               .Query(new CustomerAttentionProductQuery().Withfilter(filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Category = n.Category,
    CategoryId = n.CategoryId,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    CUR = n.CUR,
    Pric = n.Pric,
    SummaryQuote = n.SummaryQuote,
    SummaryOrders = n.SummaryOrders,
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
			    var pagerows = (await this.customerAttentionProductService
						               .Query(new CustomerAttentionProductQuery().ByCustomerIdWithfilter(customerid,filters)).Include(c => c.Customer)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Category = n.Category,
    CategoryId = n.CategoryId,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    CUR = n.CUR,
    Pric = n.Pric,
    SummaryQuote = n.SummaryQuote,
    SummaryOrders = n.SummaryOrders,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(CustomerAttentionProduct[] customerattentionproducts)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in customerattentionproducts)
               {
                 this.customerAttentionProductService.ApplyChanges(item);
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
		 
				
		//GET: CustomerAttentionProducts/Details/:id
		public ActionResult Details(int id)
		{
			
			var customerAttentionProduct = this.customerAttentionProductService.Find(id);
			if (customerAttentionProduct == null)
			{
				return HttpNotFound();
			}
			return View(customerAttentionProduct);
		}
        //GET: CustomerAttentionProducts/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  customerAttentionProduct = await this.customerAttentionProductService.FindAsync(id);
            return Json(customerAttentionProduct,JsonRequestBehavior.AllowGet);
        }
		//GET: CustomerAttentionProducts/Create
        		public ActionResult Create()
				{
			var customerAttentionProduct = new CustomerAttentionProduct();
			//set default value
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
		   			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode");
		   			return View(customerAttentionProduct);
		}
		//POST: CustomerAttentionProducts/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CustomerAttentionProduct customerAttentionProduct)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.customerAttentionProductService.Insert(customerAttentionProduct);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a customerAttentionProduct record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			//ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerAttentionProduct.CustomerId);
			//return View(customerAttentionProduct);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var customerAttentionProduct = await Task.Run(() => {
                return new CustomerAttentionProduct();
                });
            return Json(customerAttentionProduct, JsonRequestBehavior.AllowGet);
        }

         
		//GET: CustomerAttentionProducts/Edit/:id
		public ActionResult Edit(int id)
		{
			var customerAttentionProduct = this.customerAttentionProductService.Find(id);
			if (customerAttentionProduct == null)
			{
				return HttpNotFound();
			}
			var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
			ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n=>n.CustomerCode), "Id", "CustomerCode", customerAttentionProduct.CustomerId);
			return View(customerAttentionProduct);
		}
		//POST: CustomerAttentionProducts/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CustomerAttentionProduct customerAttentionProduct)
		{
			if (ModelState.IsValid)
			{
				customerAttentionProduct.TrackingState = TrackingState.Modified;
				                try{
				this.customerAttentionProductService.Update(customerAttentionProduct);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a CustomerAttentionProduct record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
												//return View(customerAttentionProduct);
		}
        //删除当前记录
		//GET: CustomerAttentionProducts/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.customerAttentionProductService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.customerAttentionProductService.Delete(id);
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
			var fileName = "customerattentionproducts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.customerAttentionProductService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
