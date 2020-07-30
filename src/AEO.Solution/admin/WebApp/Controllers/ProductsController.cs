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
/// File: ProductsController.cs
/// Purpose:产品主档/产品管理
/// Created Date: 2020/7/30 16:45:01
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Product>, Repository<Product>>();
///    container.RegisterType<IProductService, ProductService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Products")]
	public class ProductsController : Controller
	{
		private readonly IProductService  productService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ProductsController (
          IProductService  productService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.productService  = productService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Products/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "产品管理", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Products/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.productService
						               .Query(new ProductQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductFiles = n.ProductFiles,
    ProductPurchaseHistoricalPrices = n.ProductPurchaseHistoricalPrices,
    ProductSalesHistoricalPrices = n.ProductSalesHistoricalPrices,
    Id = n.Id,
    ProductNo = n.ProductNo,
    Category = n.Category,
    ProductName = n.ProductName,
    ProductEnName = n.ProductEnName,
    Spec = n.Spec,
    CnDescription = n.CnDescription,
    EnDescription = n.EnDescription,
    Remark = n.Remark,
    Status = n.Status,
    Logo = n.Logo,
    HSCODE = n.HSCODE,
    HSADDTAXRATE = n.HSADDTAXRATE,
    HSBACKTAXRATE = n.HSBACKTAXRATE,
    GUIDEPRICE = n.GUIDEPRICE,
    CUSTBASIC = n.CUSTBASIC,
    COUNTRY = n.COUNTRY,
    TAXTYPE = n.TAXTYPE,
    TAXCLASS = n.TAXCLASS,
    Package = n.Package,
    InnerBoxQty = n.InnerBoxQty,
    Unit = n.Unit,
    GWeight = n.GWeight,
    GWUnit = n.GWUnit,
    NWeight = n.NWeight,
    NWUnit = n.NWUnit,
    Volume = n.Volume,
    VUnit = n.VUnit,
    Length = n.Length,
    Width = n.Width,
    High = n.High,
    LUnit = n.LUnit,
    Flag1 = n.Flag1,
    Flag2 = n.Flag2
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(Product[] products)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in products)
               {
                 this.productService.ApplyChanges(item);
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
				
		//GET: Products/Details/:id
		public ActionResult Details(int id)
		{
			
			var product = this.productService.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}
        //GET: Products/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  product = await this.productService.FindAsync(id);
            return Json(product,JsonRequestBehavior.AllowGet);
        }
		//GET: Products/Create
        		public ActionResult Create()
				{
			var product = new Product();
			//set default value
			return View(product);
		}
		//POST: Products/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Product product)
		{
            if (ModelState.IsValid)
			{
				product.TrackingState = TrackingState.Added;   
				foreach (var item in product.ProductFiles)
				{
					item.ProductId = product.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in product.ProductPurchaseHistoricalPrices)
				{
					item.ProductId = product.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in product.ProductSalesHistoricalPrices)
				{
					item.ProductId = product.Id ;
					item.TrackingState = TrackingState.Added;
				}
               try{ 
				this.productService.ApplyChanges(product);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a product record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(product);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var product = await Task.Run(() => {
                return new Product();
                });
            return Json(product, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Products/Edit/:id
		public ActionResult Edit(int id)
		{
			var product = this.productService.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}
		//POST: Products/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				product.TrackingState = TrackingState.Modified;
												foreach (var item in product.ProductFiles)
				{
					item.ProductId = product.Id ;
				}
								foreach (var item in product.ProductPurchaseHistoricalPrices)
				{
					item.ProductId = product.Id ;
				}
								foreach (var item in product.ProductSalesHistoricalPrices)
				{
					item.ProductId = product.Id ;
				}
				 
                try{
				this.productService.ApplyChanges(product);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Product record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(product);
		}
        //删除当前记录
		//GET: Products/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.productService.Queryable().Where(x => x.Id == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
		}
		 
		//Get Detail Row By Id For Edit
		//Get : Products/EditProductFile/:id
		[HttpGet]
				public async Task<ActionResult> EditProductFile(int id)
				{
			var productfileRepository = this.unitOfWork.RepositoryAsync<ProductFile>();
						var productfile = await productfileRepository.FindAsync(id);
									var productRepository = this.unitOfWork.RepositoryAsync<Product>();             
						if (productfile == null)
			{
											ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
											//return HttpNotFound();
				return PartialView("_ProductFileEditForm", new ProductFile());
			}
			else
			{
											 ViewBag.ProductId = new SelectList(await productRepository.Queryable().ToListAsync(), "Id", "ProductNo" , productfile.ProductId );  
										}
			return PartialView("_ProductFileEditForm",  productfile);
		}
		//Get Create Row By Id For Edit
		//Get : Products/CreateProductFile
		[HttpGet]
				public async Task<ActionResult> CreateProductFile(int productid)
				{
		  			  var productRepository = this.unitOfWork.RepositoryAsync<Product>();    
			  			  ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
			  		  			return PartialView("_ProductFileEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Products/DeleteProductFile/:id
		[HttpGet]
				public async Task<ActionResult> DeleteProductFile(int  id)
				{
            try{
			   var productfileRepository = this.unitOfWork.RepositoryAsync<ProductFile>();
			   productfileRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Products/EditProductPurchaseHistoricalPrice/:id
		[HttpGet]
				public async Task<ActionResult> EditProductPurchaseHistoricalPrice(int id)
				{
			var productpurchasehistoricalpriceRepository = this.unitOfWork.RepositoryAsync<ProductPurchaseHistoricalPrice>();
						var productpurchasehistoricalprice = await productpurchasehistoricalpriceRepository.FindAsync(id);
									var productRepository = this.unitOfWork.RepositoryAsync<Product>();             
						if (productpurchasehistoricalprice == null)
			{
											ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
											//return HttpNotFound();
				return PartialView("_ProductPurchaseHistoricalPriceEditForm", new ProductPurchaseHistoricalPrice());
			}
			else
			{
											 ViewBag.ProductId = new SelectList(await productRepository.Queryable().ToListAsync(), "Id", "ProductNo" , productpurchasehistoricalprice.ProductId );  
										}
			return PartialView("_ProductPurchaseHistoricalPriceEditForm",  productpurchasehistoricalprice);
		}
		//Get Create Row By Id For Edit
		//Get : Products/CreateProductPurchaseHistoricalPrice
		[HttpGet]
				public async Task<ActionResult> CreateProductPurchaseHistoricalPrice(int productid)
				{
		  			  var productRepository = this.unitOfWork.RepositoryAsync<Product>();    
			  			  ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
			  		  			return PartialView("_ProductPurchaseHistoricalPriceEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Products/DeleteProductPurchaseHistoricalPrice/:id
		[HttpGet]
				public async Task<ActionResult> DeleteProductPurchaseHistoricalPrice(int  id)
				{
            try{
			   var productpurchasehistoricalpriceRepository = this.unitOfWork.RepositoryAsync<ProductPurchaseHistoricalPrice>();
			   productpurchasehistoricalpriceRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Products/EditProductSalesHistoricalPrice/:id
		[HttpGet]
				public async Task<ActionResult> EditProductSalesHistoricalPrice(int id)
				{
			var productsaleshistoricalpriceRepository = this.unitOfWork.RepositoryAsync<ProductSalesHistoricalPrice>();
						var productsaleshistoricalprice = await productsaleshistoricalpriceRepository.FindAsync(id);
									var productRepository = this.unitOfWork.RepositoryAsync<Product>();             
						if (productsaleshistoricalprice == null)
			{
											ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
											//return HttpNotFound();
				return PartialView("_ProductSalesHistoricalPriceEditForm", new ProductSalesHistoricalPrice());
			}
			else
			{
											 ViewBag.ProductId = new SelectList(await productRepository.Queryable().ToListAsync(), "Id", "ProductNo" , productsaleshistoricalprice.ProductId );  
										}
			return PartialView("_ProductSalesHistoricalPriceEditForm",  productsaleshistoricalprice);
		}
		//Get Create Row By Id For Edit
		//Get : Products/CreateProductSalesHistoricalPrice
		[HttpGet]
				public async Task<ActionResult> CreateProductSalesHistoricalPrice(int productid)
				{
		  			  var productRepository = this.unitOfWork.RepositoryAsync<Product>();    
			  			  ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo" );
			  		  			return PartialView("_ProductSalesHistoricalPriceEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Products/DeleteProductSalesHistoricalPrice/:id
		[HttpGet]
				public async Task<ActionResult> DeleteProductSalesHistoricalPrice(int  id)
				{
            try{
			   var productsaleshistoricalpriceRepository = this.unitOfWork.RepositoryAsync<ProductSalesHistoricalPrice>();
			   productsaleshistoricalpriceRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
       
		//Get : Products/GetProductFilesByProductId/:id
		[HttpGet]
		public async Task<JsonResult> GetProductFilesByProductId(int id)
		{
			var productfiles = await this.productService.GetProductFilesByProductIdAsync(id);
			var rows = productfiles.Select( n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    FileName = n.FileName,
    Size = n.Size,
    Folder = n.Folder,
    FileId = n.FileId,
    Ext = n.Ext,
    FilePath = n.FilePath,
    RelativePath = n.RelativePath,
    Owner = n.Owner,
    Upload = n.Upload.ToString("yyyy-MM-dd HH:mm:ss"),
    ProductId = n.ProductId
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Products/GetProductPurchaseHistoricalPricesByProductId/:id
		[HttpGet]
		public async Task<JsonResult> GetProductPurchaseHistoricalPricesByProductId(int id)
		{
			var productpurchasehistoricalprices = await this.productService.GetProductPurchaseHistoricalPricesByProductIdAsync(id);
			var rows = productpurchasehistoricalprices.Select( n => new { 

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
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Products/GetProductSalesHistoricalPricesByProductId/:id
		[HttpGet]
		public async Task<JsonResult> GetProductSalesHistoricalPricesByProductId(int id)
		{
			var productsaleshistoricalprices = await this.productService.GetProductSalesHistoricalPricesByProductIdAsync(id);
			var rows = productsaleshistoricalprices.Select( n => new { 

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
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteChecked(int[] id) {
           try{
               await this.productService.Delete(id);
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
			var fileName = "products_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.productService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
