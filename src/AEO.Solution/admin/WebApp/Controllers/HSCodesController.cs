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
/// File: HSCodesController.cs
/// Purpose:出口管理/海关商品编码库
/// Created Date: 2020/8/26 13:40:29
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<HSCode>, Repository<HSCode>>();
///    container.RegisterType<IHSCodeService, HSCodeService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("HSCodes")]
	public class HSCodesController : Controller
	{
		private readonly IHSCodeService  hSCodeService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public HSCodesController (
          IHSCodeService  hSCodeService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.hSCodeService  = hSCodeService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: HSCodes/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "海关商品编码库", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :HSCodes/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.hSCodeService
						               .Query(new HSCodeQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    Id = n.Id,
    hscode = n.hscode,
    cn_name = n.cn_name,
    en_name = n.en_name,
    g_model = n.g_model,
    unit_code = n.unit_code,
    unit_name = n.unit_name,
    unit2_code = n.unit2_code,
    unit2_name = n.unit2_name,
    control_ma = n.control_ma,
    ciq_ma = n.ciq_ma,
    im_low_rate = n.im_low_rate,
    im_normal_rate = n.im_normal_rate,
    im_temp_rate = n.im_temp_rate,
    im_tax_rate = n.im_tax_rate,
    im_consume_rate = n.im_consume_rate,
    ex_return_rate = n.ex_return_rate,
    ex_normal_rate = n.ex_normal_rate,
    ex_temp_rate = n.ex_temp_rate,
    ex_special_rate = n.ex_special_rate,
    ex_tax_rate = n.ex_tax_rate,
    remark = n.remark
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(HSCode[] hscodes)
		{
            try{
               this.hSCodeService.ApplyChanges( hscodes);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				
		//GET: HSCodes/Details/:id
		public ActionResult Details(int id)
		{
			
			var hSCode = this.hSCodeService.Find(id);
			if (hSCode == null)
			{
				return HttpNotFound();
			}
			return View(hSCode);
		}
        //GET: HSCodes/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  hSCode = await this.hSCodeService.FindAsync(id);
            return Json(hSCode,JsonRequestBehavior.AllowGet);
        }
		//GET: HSCodes/Create
        		public ActionResult Create()
				{
			var hSCode = new HSCode();
			//set default value
			return View(hSCode);
		}
		//POST: HSCodes/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(HSCode hSCode)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.hSCodeService.Insert(hSCode);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a hSCode record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(hSCode);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var hSCode = await Task.Run(() => {
                return new HSCode();
                });
            return Json(hSCode, JsonRequestBehavior.AllowGet);
        }

         
		//GET: HSCodes/Edit/:id
		public ActionResult Edit(int id)
		{
			var hSCode = this.hSCodeService.Find(id);
			if (hSCode == null)
			{
				return HttpNotFound();
			}
			return View(hSCode);
		}
		//POST: HSCodes/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(HSCode hSCode)
		{
			if (ModelState.IsValid)
			{
				hSCode.TrackingState = TrackingState.Modified;
				                try{
				this.hSCodeService.Update(hSCode);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a HSCode record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(hSCode);
		}
        //删除当前记录
		//GET: HSCodes/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.hSCodeService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               await this.hSCodeService.Delete(id);
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
			var fileName = "hscodes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.hSCodeService.ExportExcelAsync(filterRules,sort, order );
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
        await this.hSCodeService.ImportDataTableAsync(data, Auth.GetFullName());
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
