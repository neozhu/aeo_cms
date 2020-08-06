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
  /// File: CustomerContactsController.cs
  /// Purpose:客户管理/联系人信息
  /// Created Date: 2020/8/5 10:49:29
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<CustomerContact>, Repository<CustomerContact>>();
  ///    container.RegisterType<ICustomerContactService, CustomerContactService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("CustomerContacts")]
  public class CustomerContactsController : Controller
  {
    private readonly ICustomerContactService customerContactService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public CustomerContactsController(
          ICustomerContactService customerContactService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.customerContactService = customerContactService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: CustomerContacts/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "联系人信息", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :CustomerContacts/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.customerContactService
                           .Query(new CustomerContactQuery().Withfilter(filters)).Include(c => c.Customer)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         CustomerCustomerCode = n.Customer?.CustomerCode,
                                         Id = n.Id,
                                         Name = n.Name,
                                         Appellation = n.Appellation,
                                         Sex = n.Sex,
                                         Status = n.Status,
                                         Owner = n.Owner,
                                         Job = n.Job,
                                         Wx = n.Wx,
                                         MobilePhone = n.MobilePhone,
                                         PhoneNumber = n.PhoneNumber,
                                         Fax = n.Fax,
                                         Email = n.Email,
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
      var pagerows = ( await this.customerContactService
                       .Query(new CustomerContactQuery().ByCustomerIdWithfilter(customerid, filters)).Include(c => c.Customer)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CustomerCustomerCode = n.Customer?.CustomerCode,
                                     Id = n.Id,
                                     Name = n.Name,
                                     Appellation = n.Appellation,
                                     Sex = n.Sex,
                                     Status = n.Status,
                                     Owner = n.Owner,
                                     Job = n.Job,
                                     Wx = n.Wx,
                                     MobilePhone = n.MobilePhone,
                                     PhoneNumber = n.PhoneNumber,
                                     Fax = n.Fax,
                                     Email = n.Email,
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
    public async Task<JsonResult> AcceptChanges(CustomerContact[] customercontacts)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.customerContactService.ApplyChanges(customercontacts);
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


    //GET: CustomerContacts/Details/:id
    public ActionResult Details(int id)
    {

      var customerContact = this.customerContactService.Find(id);
      if (customerContact == null)
      {
        return HttpNotFound();
      }
      return View(customerContact);
    }
    //GET: CustomerContacts/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var customerContact = await this.customerContactService.FindAsync(id);
      return Json(customerContact, JsonRequestBehavior.AllowGet);
    }
    //GET: CustomerContacts/Create
    public ActionResult Create()
    {
      var customerContact = new CustomerContact();
      //set default value
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode");
      return View(customerContact);
    }
    //POST: CustomerContacts/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CustomerContact customerContact)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.customerContactService.Insert(customerContact);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a customerContact record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode", customerContact.CustomerId);
      //return View(customerContact);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var customerContact = await Task.Run(() =>
      {
        return new CustomerContact();
      });
      return Json(customerContact, JsonRequestBehavior.AllowGet);
    }


    //GET: CustomerContacts/Edit/:id
    public ActionResult Edit(int id)
    {
      var customerContact = this.customerContactService.Find(id);
      if (customerContact == null)
      {
        return HttpNotFound();
      }
      var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      ViewBag.CustomerId = new SelectList(customerRepository.Queryable().OrderBy(n => n.CustomerCode), "Id", "CustomerCode", customerContact.CustomerId);
      return View(customerContact);
    }
    //POST: CustomerContacts/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CustomerContact customerContact)
    {
      if (ModelState.IsValid)
      {
        customerContact.TrackingState = TrackingState.Modified;
        try
        {
          this.customerContactService.Update(customerContact);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a CustomerContact record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();
      //return View(customerContact);
    }
    //删除当前记录
    //GET: CustomerContacts/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.customerContactService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.customerContactService.Delete(id);
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
      var fileName = "customercontacts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.customerContactService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }

  }
}
