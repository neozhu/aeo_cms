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
  /// File: ProductPricturesController.cs
  /// Purpose:产品主档/产品图片
  /// Created Date: 2020/7/30 15:30:52
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<ProductPricture>, Repository<ProductPricture>>();
  ///    container.RegisterType<IProductPrictureService, ProductPrictureService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("ProductPrictures")]
  public class ProductPricturesController : Controller
  {
    private readonly IProductPrictureService productPrictureService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public ProductPricturesController(
          IProductPrictureService productPrictureService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.productPrictureService = productPrictureService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: ProductPrictures/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "产品图片", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :ProductPrictures/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.productPrictureService
                           .Query(new ProductPrictureQuery().Withfilter(filters)).Include(p => p.Product)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         ProductProductNo = n.Product?.ProductNo,
                                         Id = n.Id,
                                         FileName = n.FileName,
                                         Description = n.Description,
                                         LineNo = n.LineNo,
                                         Size = n.Size,
                                         Folder = n.Folder,
                                         FileId = n.FileId,
                                         FilePath = n.FilePath,
                                         RelativePath = n.RelativePath,
                                         ProductId = n.ProductId
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByProductId(int productid)
    {
      var result = ( await this.productPrictureService
                       .Query(new ProductPrictureQuery().ByProductIdWithfilter(productid, null)).Include(p => p.Product)
                     .OrderBy(n => n.OrderBy(y => y.Id))
                     .SelectAsync() )
                                   .Select(n => new
                                   {

                                     ProductProductNo = n.Product?.ProductNo,
                                     Id = n.Id,
                                     FileName = n.FileName,
                                     Description = n.Description,
                                     LineNo = n.LineNo,
                                     Size = n.Size,
                                     Folder = n.Folder,
                                     FileId = n.FileId,
                                     FilePath = n.FilePath,
                                     RelativePath = n.RelativePath,
                                     ProductId = n.ProductId
                                   }).ToList();

      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(ProductPricture[] productprictures)
    {
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in productprictures)
          {
            this.productPrictureService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
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
    public async Task<JsonResult> GetProducts(string q = "")
    {
      var productRepository = this.unitOfWork.RepositoryAsync<Product>();
      var rows = await productRepository
                            .Queryable()
                            .Where(n => n.ProductNo.Contains(q))
                            .OrderBy(n => n.ProductNo)
                            .Select(n => new { Id = n.Id, ProductNo = n.ProductNo })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }


    //GET: ProductPrictures/Details/:id
    public ActionResult Details(int id)
    {

      var productPricture = this.productPrictureService.Find(id);
      if (productPricture == null)
      {
        return HttpNotFound();
      }
      return View(productPricture);
    }
    //GET: ProductPrictures/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var productPricture = await this.productPrictureService.FindAsync(id);
      return Json(productPricture, JsonRequestBehavior.AllowGet);
    }
    //GET: ProductPrictures/Create
    public ActionResult Create()
    {
      var productPricture = new ProductPricture();
      //set default value
      var productRepository = this.unitOfWork.RepositoryAsync<Product>();
      ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n => n.ProductNo), "Id", "ProductNo");
      return View(productPricture);
    }
    //POST: ProductPrictures/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(ProductPricture productPricture)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.productPrictureService.Insert(productPricture);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a productPricture record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var productRepository = this.unitOfWork.RepositoryAsync<Product>();
      //ViewBag.ProductId = new SelectList(await productRepository.Queryable().OrderBy(n=>n.ProductNo).ToListAsync(), "Id", "ProductNo", productPricture.ProductId);
      //return View(productPricture);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var productPricture = await Task.Run(() =>
      {
        return new ProductPricture();
      });
      return Json(productPricture, JsonRequestBehavior.AllowGet);
    }


    //GET: ProductPrictures/Edit/:id
    public ActionResult Edit(int id)
    {
      var productPricture = this.productPrictureService.Find(id);
      if (productPricture == null)
      {
        return HttpNotFound();
      }
      var productRepository = this.unitOfWork.RepositoryAsync<Product>();
      ViewBag.ProductId = new SelectList(productRepository.Queryable().OrderBy(n => n.ProductNo), "Id", "ProductNo", productPricture.ProductId);
      return View(productPricture);
    }
    //POST: ProductPrictures/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(ProductPricture productPricture)
    {
      if (ModelState.IsValid)
      {
        productPricture.TrackingState = TrackingState.Modified;
        try
        {
          this.productPrictureService.Update(productPricture);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a ProductPricture record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var productRepository = this.unitOfWork.RepositoryAsync<Product>();
      //return View(productPricture);
    }
    //删除当前记录
    //GET: ProductPrictures/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.productPrictureService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }




    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.productPrictureService.Delete(id);
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
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "productprictures_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.productPrictureService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }

  }
}
