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
/// File: ProductFilesController.cs
/// Purpose:产品主档/产品附件
/// Created Date: 2020/7/30 15:29:46
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ProductFile>, Repository<ProductFile>>();
///    container.RegisterType<IProductFileService, ProductFileService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ProductFiles")]
	public class ProductFilesController : Controller
	{
		private readonly IProductFileService  productFileService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ProductFilesController (
          IProductFileService  productFileService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.productFileService  = productFileService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ProductFiles/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "产品附件", Order = 1)]
		public ActionResult Index() => this.View();
    //接收上传文件
    public async Task<ActionResult> Upload()
    {
      try
      {
        var file = this.Request.Files[0];
        var user = (string)ViewBag.GivenName;
        var tags = this.Request.Form["tags"];
        var name = this.Request.Form["name"];
        var productId= Convert.ToInt32(this.Request.Form["productId"]);
        var productNo = this.Request.Form["productNo"];
        var folder = this.Server.MapPath("~/UploadFiles/Product/Files/" + productNo);
        var relpath = "/UploadFiles/Product/Files/" + productNo + "/";
        this.productFileService.AddFile(productId,productNo,file,folder, relpath, user);
        await this.unitOfWork.SaveChangesAsync();
        return Content($"{file.FileName}:上传成功", "text/plain");
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    //Get :ProductFiles/GetData
    //For Index View datagrid datasource url

    [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.productFileService
						               .Query(new ProductFileQuery().Withfilter(filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByProductId (int  productid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.productFileService
						               .Query(new ProductFileQuery().ByProductIdWithfilter(productid,filters)).Include(p => p.Product)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(ProductFile[] productfiles)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in productfiles)
               {
                 this.productFileService.ApplyChanges(item);
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
		 
				
		//GET: ProductFiles/Details/:id
		public ActionResult Details(int id)
		{
			
			var productFile = this.productFileService.Find(id);
			if (productFile == null)
			{
				return HttpNotFound();
			}
			return View(productFile);
		}
        //GET: ProductFiles/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  productFile = await this.productFileService.FindAsync(id);
            return Json(productFile,JsonRequestBehavior.AllowGet);
        }
		//GET: ProductFiles/Create
        		public ActionResult Create()
				{
			var productFile = new ProductFile();
			//set default value
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
		   			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo");
		   			return View(productFile);
		}
		//POST: ProductFiles/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ProductFile productFile)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.productFileService.Insert(productFile);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a productFile record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			//ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo", productFile.ProductId);
			//return View(productFile);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var productFile = await Task.Run(() => {
                return new ProductFile();
                });
            return Json(productFile, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ProductFiles/Edit/:id
		public ActionResult Edit(int id)
		{
			var productFile = this.productFileService.Find(id);
			if (productFile == null)
			{
				return HttpNotFound();
			}
			var productRepository = this.unitOfWork.RepositoryAsync<Product>();
			ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n=>n.ProductNo), "Id", "ProductNo", productFile.ProductId);
			return View(productFile);
		}
		//POST: ProductFiles/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ProductFile productFile)
		{
			if (ModelState.IsValid)
			{
				productFile.TrackingState = TrackingState.Modified;
				                try{
				this.productFileService.Update(productFile);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a ProductFile record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var productRepository = this.unitOfWork.RepositoryAsync<Product>();
												//return View(productFile);
		}
        //删除当前记录
		//GET: ProductFiles/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.productFileService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        var user = (string)ViewBag.GivenName;
        await this.productFileService.Delete(id, user);
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
			var fileName = "productfiles_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.productFileService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
