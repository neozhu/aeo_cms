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
  /// File: CustomerWarehousesController.cs
  /// Purpose:客户管理/仓库信息
  /// Created Date: 2020/8/5 11:22:15
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<CustomerWarehouse>, Repository<CustomerWarehouse>>();
  ///    container.RegisterType<ICustomerWarehouseService, CustomerWarehouseService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("CustomerWarehouses")]
  public class CustomerWarehousesController : Controller
  {
    private readonly ICustomerWarehouseService customerWarehouseService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public CustomerWarehousesController(
          ICustomerWarehouseService customerWarehouseService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.customerWarehouseService = customerWarehouseService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: CustomerWarehouses/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "仓库信息", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :CustomerWarehouses/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.customerWarehouseService
                           .Query(new CustomerWarehouseQuery().Withfilter(filters)).Include(c => c.Customer)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         CustomerCustomerCode = n.Customer?.CustomerCode,
                                         Id = n.Id,
                                         WarehouseCode = n.WarehouseCode,
                                         WarehouseName = n.WarehouseName,
                                         WarehouseType = n.WarehouseType,
                                         FactoryGuard = n.FactoryGuard,
                                         WAddress = n.WAddress,
                                         WUser = n.WUser,
                                         WFax = n.WFax,
                                         WMPhone1 = n.WMPhone1,
                                         WMPhone2 = n.WMPhone2,
                                         WEmail1 = n.WEmail1,
                                         Remark = n.Remark,
                                         CustomerId = n.CustomerId,
                                         CustomerCode = n.CustomerCode,
                                         CustomerName = n.CustomerName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByCustomerId(int customerid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.customerWarehouseService
                       .Query(new CustomerWarehouseQuery().ByCustomerIdWithfilter(customerid, filters)).Include(c => c.Customer)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CustomerCustomerCode = n.Customer?.CustomerCode,
                                     Id = n.Id,
                                     WarehouseCode = n.WarehouseCode,
                                     WarehouseName = n.WarehouseName,
                                     WarehouseType = n.WarehouseType,
                                     FactoryGuard = n.FactoryGuard,
                                     WAddress = n.WAddress,
                                     WUser = n.WUser,
                                     WFax = n.WFax,
                                     WMPhone1 = n.WMPhone1,
                                     WMPhone2 = n.WMPhone2,
                                     WEmail1 = n.WEmail1,
                                     Remark = n.Remark,
                                     CustomerId = n.CustomerId,
                                     CustomerCode = n.CustomerCode,
                                     CustomerName = n.CustomerName
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(CustomerWarehouse[] customerwarehouses)
    {
      if (ModelState.IsValid)
      {
        try
        {

          this.customerWarehouseService.ApplyChanges(customerwarehouses);

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
    public async Task<JsonResult> GetCustomers(string q = "")
    {
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      var rows = await customerRepository
                            .Queryable()
                            .Where(n => n.CustomerCode.Contains(q))
                            .OrderBy(n => n.CustomerCode)
                            .Select(n => new { Id = n.Id, CustomerCode = n.CustomerCode })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }


    //GET: CustomerWarehouses/Details/:id
    public ActionResult Details(int id)
    {

      var customerWarehouse = this.customerWarehouseService.Find(id);
      if (customerWarehouse == null)
      {
        return HttpNotFound();
      }
      return View(customerWarehouse);
    }
    //GET: CustomerWarehouses/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var customerWarehouse = await this.customerWarehouseService.FindAsync(id);
      return Json(customerWarehouse, JsonRequestBehavior.AllowGet);
    }
    //GET: CustomerWarehouses/Create
    public ActionResult Create()
    {
      var customerWarehouse = new CustomerWarehouse();
      //set default value
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode");
      return View(customerWarehouse);
    }
    //POST: CustomerWarehouses/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CustomerWarehouse customerWarehouse)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.customerWarehouseService.Insert(customerWarehouse);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a customerWarehouse record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerWarehouse.CustomerId);
      //return View(customerWarehouse);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var customerWarehouse = await Task.Run(() =>
      {
        return new CustomerWarehouse();
      });
      return Json(customerWarehouse, JsonRequestBehavior.AllowGet);
    }


    //GET: CustomerWarehouses/Edit/:id
    public ActionResult Edit(int id)
    {
      var customerWarehouse = this.customerWarehouseService.Find(id);
      if (customerWarehouse == null)
      {
        return HttpNotFound();
      }
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode", customerWarehouse.CustomerId);
      return View(customerWarehouse);
    }
    //POST: CustomerWarehouses/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CustomerWarehouse customerWarehouse)
    {
      if (ModelState.IsValid)
      {
        customerWarehouse.TrackingState = TrackingState.Modified;
        try
        {
          this.customerWarehouseService.Update(customerWarehouse);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a CustomerWarehouse record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //return View(customerWarehouse);
    }
    //删除当前记录
    //GET: CustomerWarehouses/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.customerWarehouseService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.customerWarehouseService.Delete(id);
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
      var fileName = "customerwarehouses_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.customerWarehouseService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }

  }
}
