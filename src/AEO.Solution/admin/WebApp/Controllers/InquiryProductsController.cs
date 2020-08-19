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
/// File: InquiryProductsController.cs
/// Purpose:出口管理/询价单产品信息
/// Created Date: 2020/8/19 10:56:19
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<InquiryProduct>, Repository<InquiryProduct>>();
///    container.RegisterType<IInquiryProductService, InquiryProductService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("InquiryProducts")]
	public class InquiryProductsController : Controller
	{
		private readonly IInquiryProductService  inquiryProductService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public InquiryProductsController (
          IInquiryProductService  inquiryProductService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.inquiryProductService  = inquiryProductService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: InquiryProducts/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "询价单产品信息", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :InquiryProducts/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.inquiryProductService
						               .Query(new InquiryProductQuery().Withfilter(filters)).Include(i => i.Inquiry)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    InquiryInquiryNo = n.Inquiry?.InquiryNo,
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
    Executor = n.Executor,
    SupplierCode = n.SupplierCode,
    SupplierName = n.SupplierName,
    SupplierProductNo = n.SupplierProductNo,
    PriceType = n.PriceType,
    Price = n.Price,
    Cur = n.Cur,
    MinQty = n.MinQty,
    PriceDate = n.PriceDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Feedback = n.Feedback,
    Recommended = n.Recommended,
    SamplePic = n.SamplePic,
    InquiryNo = n.InquiryNo,
    TaskNo = n.TaskNo,
    Ver = n.Ver,
    InquiryId = n.InquiryId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByInquiryId (int  inquiryid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.inquiryProductService
						               .Query(new InquiryProductQuery().ByInquiryIdWithfilter(inquiryid,filters)).Include(i => i.Inquiry)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    InquiryInquiryNo = n.Inquiry?.InquiryNo,
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
    Executor = n.Executor,
    SupplierCode = n.SupplierCode,
    SupplierName = n.SupplierName,
    SupplierProductNo = n.SupplierProductNo,
    PriceType = n.PriceType,
    Price = n.Price,
    Cur = n.Cur,
    MinQty = n.MinQty,
    PriceDate = n.PriceDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    Feedback = n.Feedback,
    Recommended = n.Recommended,
    SamplePic = n.SamplePic,
    InquiryNo = n.InquiryNo,
    TaskNo = n.TaskNo,
    Ver = n.Ver,
    InquiryId = n.InquiryId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(InquiryProduct[] inquiryproducts)
		{
            try{
               this.inquiryProductService.ApplyChanges( inquiryproducts);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetInquiries(string q="")
		{
			var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
			var rows = await inquiryRepository
                            .Queryable()
                            .Where(n=>n.InquiryNo.Contains(q))
                            .OrderBy(n=>n.InquiryNo)
                            .Select(n => new { Id = n.Id, InquiryNo = n.InquiryNo })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: InquiryProducts/Details/:id
		public ActionResult Details(int id)
		{
			
			var inquiryProduct = this.inquiryProductService.Find(id);
			if (inquiryProduct == null)
			{
				return HttpNotFound();
			}
			return View(inquiryProduct);
		}
        //GET: InquiryProducts/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  inquiryProduct = await this.inquiryProductService.FindAsync(id);
            return Json(inquiryProduct,JsonRequestBehavior.AllowGet);
        }
		//GET: InquiryProducts/Create
        		public ActionResult Create()
				{
			var inquiryProduct = new InquiryProduct();
			//set default value
			var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
		   			ViewBag.InquiryId = new SelectList(inquiryRepository.Queryable().OrderBy(n=>n.InquiryNo), "Id", "InquiryNo");
		   			return View(inquiryProduct);
		}
		//POST: InquiryProducts/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(InquiryProduct inquiryProduct)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.inquiryProductService.Insert(inquiryProduct);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a inquiryProduct record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
			//ViewBag.InquiryId = new SelectList(await inquiryRepository.Queryable().OrderBy(n=>n.InquiryNo).ToListAsync(), "Id", "InquiryNo", inquiryProduct.InquiryId);
			//return View(inquiryProduct);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var inquiryProduct = await Task.Run(() => {
                return new InquiryProduct();
                });
            return Json(inquiryProduct, JsonRequestBehavior.AllowGet);
        }

         
		//GET: InquiryProducts/Edit/:id
		public ActionResult Edit(int id)
		{
			var inquiryProduct = this.inquiryProductService.Find(id);
			if (inquiryProduct == null)
			{
				return HttpNotFound();
			}
			var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
			ViewBag.InquiryId = new SelectList(inquiryRepository.Queryable().OrderBy(n=>n.InquiryNo), "Id", "InquiryNo", inquiryProduct.InquiryId);
			return View(inquiryProduct);
		}
		//POST: InquiryProducts/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(InquiryProduct inquiryProduct)
		{
			if (ModelState.IsValid)
			{
				inquiryProduct.TrackingState = TrackingState.Modified;
				                try{
				this.inquiryProductService.Update(inquiryProduct);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a InquiryProduct record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var inquiryRepository = this.unitOfWork.RepositoryAsync<Inquiry>();
												//return View(inquiryProduct);
		}
        //删除当前记录
		//GET: InquiryProducts/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.inquiryProductService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.inquiryProductService.Delete(id);
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
			var fileName = "inquiryproducts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.inquiryProductService.ExportExcelAsync(filterRules,sort, order );
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
        await this.inquiryProductService.ImportDataTableAsync(data, Auth.GetFullName());
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
