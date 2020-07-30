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
/// File: ProductPurchaseHistoricalPricesController.cs
/// Purpose:产品主档/历史采购价格
/// Created Date: 2020/7/30 16:30:25
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ProductPurchaseHistoricalPrice>, Repository<ProductPurchaseHistoricalPrice>>();
///    container.RegisterType<IProductPurchaseHistoricalPriceService, ProductPurchaseHistoricalPriceService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ProductPurchaseHistoricalPrices")]
	public class ProductPurchaseHistoricalPricesController : Controller
	{
		private readonly IProductPurchaseHistoricalPriceService  productPurchaseHistoricalPriceService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ProductPurchaseHistoricalPricesController (
          IProductPurchaseHistoricalPriceService  productPurchaseHistoricalPriceService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.productPurchaseHistoricalPriceService  = productPurchaseHistoricalPriceService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ProductPurchaseHistoricalPrices/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "历史采购价格", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :ProductPurchaseHistoricalPrices/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.productPurchaseHistoricalPriceService
						               .Query(new ProductPurchaseHistoricalPriceQuery().Withfilter(filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    SupplierCode = n.SupplierCode,
    SupplierName = n.SupplierName,
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
			    var pagerows = (await this.productPurchaseHistoricalPriceService
						               .Query(new ProductPurchaseHistoricalPriceQuery().ByProductIdWithfilter(productid,filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    SupplierCode = n.SupplierCode,
    SupplierName = n.SupplierName,
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
		public async Task<JsonResult> AcceptChanges(ProductPurchaseHistoricalPrice[] productpurchasehistoricalprices)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in productpurchasehistoricalprices)
               {
                 this.productPurchaseHistoricalPriceService.ApplyChanges(item);
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
		 
				
		//GET: ProductPurchaseHistoricalPrices/Details/:id
		public ActionResult Details(int id)
		{
			
			var productPurchaseHistoricalPrice = this.productPurchaseHistoricalPriceService.Find(id);
			if (productPurchaseHistoricalPrice == null)
			{
				return HttpNotFound();
			}
			return View(productPurchaseHistoricalPrice);
		}
        //GET: ProductPurchaseHistoricalPrices/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  productPurchaseHistoricalPrice = await this.productPurchaseHistoricalPriceService.FindAsync(id);
            return Json(productPurchaseHistoricalPrice,JsonRequestBehavior.AllowGet);
        }
		//GET: ProductPurchaseHistoricalPrices/Create
        		public ActionResult Create()
				{
			var productPurchaseHistoricalPrice = new ProductPurchaseHistoricalPrice();
			//set default value
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
		   			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo");
		   			return View(productPurchaseHistoricalPrice);
		}
		//POST: ProductPurchaseHistoricalPrices/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ProductPurchaseHistoricalPrice productPurchaseHistoricalPrice)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.productPurchaseHistoricalPriceService.Insert(productPurchaseHistoricalPrice);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a productPurchaseHistoricalPrice record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			//ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo", productPurchaseHistoricalPrice.ProductId);
			//return View(productPurchaseHistoricalPrice);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var productPurchaseHistoricalPrice = await Task.Run(() => {
                return new ProductPurchaseHistoricalPrice();
                });
            return Json(productPurchaseHistoricalPrice, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ProductPurchaseHistoricalPrices/Edit/:id
		public ActionResult Edit(int id)
		{
			var productPurchaseHistoricalPrice = this.productPurchaseHistoricalPriceService.Find(id);
			if (productPurchaseHistoricalPrice == null)
			{
				return HttpNotFound();
			}
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo", productPurchaseHistoricalPrice.ProductId);
			return View(productPurchaseHistoricalPrice);
		}
		//POST: ProductPurchaseHistoricalPrices/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ProductPurchaseHistoricalPrice productPurchaseHistoricalPrice)
		{
			if (ModelState.IsValid)
			{
				productPurchaseHistoricalPrice.TrackingState = TrackingState.Modified;
				                try{
				this.productPurchaseHistoricalPriceService.Update(productPurchaseHistoricalPrice);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a ProductPurchaseHistoricalPrice record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
												//return View(productPurchaseHistoricalPrice);
		}
        //删除当前记录
		//GET: ProductPurchaseHistoricalPrices/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.productPurchaseHistoricalPriceService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.productPurchaseHistoricalPriceService.Delete(id);
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
			var fileName = "productpurchasehistoricalprices_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.productPurchaseHistoricalPriceService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
