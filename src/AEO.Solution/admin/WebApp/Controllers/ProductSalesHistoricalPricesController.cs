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
/// File: ProductSalesHistoricalPricesController.cs
/// Purpose:产品主档/历史销售价格
/// Created Date: 2020/7/30 16:27:47
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ProductSalesHistoricalPrice>, Repository<ProductSalesHistoricalPrice>>();
///    container.RegisterType<IProductSalesHistoricalPriceService, ProductSalesHistoricalPriceService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ProductSalesHistoricalPrices")]
	public class ProductSalesHistoricalPricesController : Controller
	{
		private readonly IProductSalesHistoricalPriceService  productSalesHistoricalPriceService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ProductSalesHistoricalPricesController (
          IProductSalesHistoricalPriceService  productSalesHistoricalPriceService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.productSalesHistoricalPriceService  = productSalesHistoricalPriceService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ProductSalesHistoricalPrices/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "历史销售价格", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :ProductSalesHistoricalPrices/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.productSalesHistoricalPriceService
						               .Query(new ProductSalesHistoricalPriceQuery().Withfilter(filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    ThirdProductNo = n.ThirdProductNo,
    QuoteDate = n.QuoteDate.ToString("yyyy-MM-dd HH:mm:ss"),
    CUR = n.CUR,
    UnitPrice = n.UnitPrice,
    Qty = n.Qty,
    Source = n.Source,
    DocNo = n.DocNo,
    Remark = n.Remark,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    ProductId = n.ProductId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByProductId (int  productid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.productSalesHistoricalPriceService
						               .Query(new ProductSalesHistoricalPriceQuery().ByProductIdWithfilter(productid,filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName,
    ThirdProductNo = n.ThirdProductNo,
    QuoteDate = n.QuoteDate.ToString("yyyy-MM-dd HH:mm:ss"),
    CUR = n.CUR,
    UnitPrice = n.UnitPrice,
    Qty = n.Qty,
    Source = n.Source,
    DocNo = n.DocNo,
    Remark = n.Remark,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    ProductId = n.ProductId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(ProductSalesHistoricalPrice[] productsaleshistoricalprices)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in productsaleshistoricalprices)
               {
                 this.productSalesHistoricalPriceService.ApplyChanges(item);
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
		public async Task<JsonResult> GetProducts(string q="")
		{
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			var rows = await productRepository
                            .Queryable()
                            .Where(n=>n.ProductNo.Contains(q))
                            .OrderBy(n=>n.ProductNo)
                            .Select(n => new { Id = n.Id, ProductNo = n.ProductNo })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: ProductSalesHistoricalPrices/Details/:id
		public ActionResult Details(int id)
		{
			
			var productSalesHistoricalPrice = this.productSalesHistoricalPriceService.Find(id);
			if (productSalesHistoricalPrice == null)
			{
				return HttpNotFound();
			}
			return View(productSalesHistoricalPrice);
		}
        //GET: ProductSalesHistoricalPrices/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  productSalesHistoricalPrice = await this.productSalesHistoricalPriceService.FindAsync(id);
            return Json(productSalesHistoricalPrice,JsonRequestBehavior.AllowGet);
        }
		//GET: ProductSalesHistoricalPrices/Create
        		public ActionResult Create()
				{
			var productSalesHistoricalPrice = new ProductSalesHistoricalPrice();
			//set default value
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
		   			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo");
		   			return View(productSalesHistoricalPrice);
		}
		//POST: ProductSalesHistoricalPrices/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ProductSalesHistoricalPrice productSalesHistoricalPrice)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.productSalesHistoricalPriceService.Insert(productSalesHistoricalPrice);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a productSalesHistoricalPrice record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			//ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo", productSalesHistoricalPrice.ProductId);
			//return View(productSalesHistoricalPrice);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var productSalesHistoricalPrice = await Task.Run(() => {
                return new ProductSalesHistoricalPrice();
                });
            return Json(productSalesHistoricalPrice, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ProductSalesHistoricalPrices/Edit/:id
		public ActionResult Edit(int id)
		{
			var productSalesHistoricalPrice = this.productSalesHistoricalPriceService.Find(id);
			if (productSalesHistoricalPrice == null)
			{
				return HttpNotFound();
			}
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo", productSalesHistoricalPrice.ProductId);
			return View(productSalesHistoricalPrice);
		}
		//POST: ProductSalesHistoricalPrices/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ProductSalesHistoricalPrice productSalesHistoricalPrice)
		{
			if (ModelState.IsValid)
			{
				productSalesHistoricalPrice.TrackingState = TrackingState.Modified;
				                try{
				this.productSalesHistoricalPriceService.Update(productSalesHistoricalPrice);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a ProductSalesHistoricalPrice record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
												//return View(productSalesHistoricalPrice);
		}
        //删除当前记录
		//GET: ProductSalesHistoricalPrices/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.productSalesHistoricalPriceService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.productSalesHistoricalPriceService.Delete(id);
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
			var fileName = "productsaleshistoricalprices_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.productSalesHistoricalPriceService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
