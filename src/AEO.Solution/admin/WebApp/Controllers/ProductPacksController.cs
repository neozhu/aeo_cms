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
/// File: ProductPacksController.cs
/// Purpose:产品主档/产品包装
/// Created Date: 2020/7/30 16:19:22
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ProductPack>, Repository<ProductPack>>();
///    container.RegisterType<IProductPackService, ProductPackService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ProductPacks")]
	public class ProductPacksController : Controller
	{
		private readonly IProductPackService  productPackService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ProductPacksController (
          IProductPackService  productPackService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.productPackService  = productPackService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ProductPacks/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "产品包装", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :ProductPacks/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.productPackService
						               .Query(new ProductPackQuery().Withfilter(filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    Package = n.Package,
    InnerBoxQty = n.InnerBoxQty,
    Length = n.Length,
    Width = n.Width,
    Height = n.Height,
    Unit = n.Unit,
    GWeight = n.GWeight,
    NWeight = n.NWeight,
    Volume = n.Volume,
    TwentyQtc = n.TwentyQtc,
    FortyQtc = n.FortyQtc,
    FortyHQQtc = n.FortyHQQtc,
    Default = n.Default,
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
			    var pagerows = (await this.productPackService
						               .Query(new ProductPackQuery().ByProductIdWithfilter(productid,filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ProductProductNo = n.Product?.ProductNo,
    Id = n.Id,
    Package = n.Package,
    InnerBoxQty = n.InnerBoxQty,
    Length = n.Length,
    Width = n.Width,
    Height = n.Height,
    Unit = n.Unit,
    GWeight = n.GWeight,
    NWeight = n.NWeight,
    Volume = n.Volume,
    TwentyQtc = n.TwentyQtc,
    FortyQtc = n.FortyQtc,
    FortyHQQtc = n.FortyHQQtc,
    Default = n.Default,
    ProductId = n.ProductId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(ProductPack[] productpacks)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in productpacks)
               {
                 this.productPackService.ApplyChanges(item);
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
		 
				
		//GET: ProductPacks/Details/:id
		public ActionResult Details(int id)
		{
			
			var productPack = this.productPackService.Find(id);
			if (productPack == null)
			{
				return HttpNotFound();
			}
			return View(productPack);
		}
        //GET: ProductPacks/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  productPack = await this.productPackService.FindAsync(id);
            return Json(productPack,JsonRequestBehavior.AllowGet);
        }
		//GET: ProductPacks/Create
        		public ActionResult Create()
				{
			var productPack = new ProductPack();
			//set default value
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
		   			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo");
		   			return View(productPack);
		}
		//POST: ProductPacks/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ProductPack productPack)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.productPackService.Insert(productPack);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a productPack record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			//ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo", productPack.ProductId);
			//return View(productPack);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var productPack = await Task.Run(() => {
                return new ProductPack();
                });
            return Json(productPack, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ProductPacks/Edit/:id
		public ActionResult Edit(int id)
		{
			var productPack = this.productPackService.Find(id);
			if (productPack == null)
			{
				return HttpNotFound();
			}
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo", productPack.ProductId);
			return View(productPack);
		}
		//POST: ProductPacks/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ProductPack productPack)
		{
			if (ModelState.IsValid)
			{
				productPack.TrackingState = TrackingState.Modified;
				                try{
				this.productPackService.Update(productPack);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a ProductPack record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
												//return View(productPack);
		}
        //删除当前记录
		//GET: ProductPacks/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.productPackService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.productPackService.Delete(id);
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
			var fileName = "productpacks_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.productPackService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
