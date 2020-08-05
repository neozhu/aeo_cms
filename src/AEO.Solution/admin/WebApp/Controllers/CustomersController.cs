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
/// File: CustomersController.cs
/// Purpose:客户管理/客户信息
/// Created Date: 2020/8/5 11:52:46
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>();
///    container.RegisterType<ICustomerService, CustomerService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Customers")]
	public class CustomersController : Controller
	{
		private readonly ICustomerService  customerService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CustomersController (
          ICustomerService  customerService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.customerService  = customerService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Customers/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "客户信息", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Customers/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.customerService
						               .Query(new CustomerQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    CustomerAttentionProducts = n.CustomerAttentionProducts,
    CustomerBanks = n.CustomerBanks,
    CustomerContacts = n.CustomerContacts,
    CustomerFiles = n.CustomerFiles,
    CustomerFollows = n.CustomerFollows,
    CustomerSales = n.CustomerSales,
    CustomerShares = n.CustomerShares,
    CustomerWarehouses = n.CustomerWarehouses,
    Id = n.Id,
    CustomerCode = n.CustomerCode,
    BaseName = n.BaseName,
    CustomerName = n.CustomerName,
    CustomerType = n.CustomerType,
    Country = n.Country,
    Level = n.Level,
    Source = n.Source,
    Telephone = n.Telephone,
    Fax = n.Fax,
    Owner = n.Owner,
    WebSite = n.WebSite,
    Industry = n.Industry,
    BusinessScope = n.BusinessScope,
    Address = n.Address,
    Remark = n.Remark,
    Payment = n.Payment,
    TradeCode = n.TradeCode,
    MasterCustom = n.MasterCustom,
    CreditCode = n.CreditCode,
    ContactName = n.ContactName,
    Appellation = n.Appellation,
    Sex = n.Sex,
    Job = n.Job,
    Wx = n.Wx,
    PhoneNumber = n.PhoneNumber,
    Email = n.Email,
    ContactRemark = n.ContactRemark,
    Status = n.Status,
    Flag = n.Flag,
    Logo = n.Logo,
    LastContactDate = n.LastContactDate?.ToString("yyyy-MM-dd HH:mm:ss")
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(Customer[] customers)
		{
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in customers)
               {
                 this.customerService.ApplyChanges(item);
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
				
		//GET: Customers/Details/:id
		public ActionResult Details(int id)
		{
			
			var customer = this.customerService.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}
        //GET: Customers/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  customer = await this.customerService.FindAsync(id);
            return Json(customer,JsonRequestBehavior.AllowGet);
        }
		//GET: Customers/Create
        		public ActionResult Create()
				{
			var customer = new Customer();
			//set default value
			return View(customer);
		}
		//POST: Customers/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Customer customer)
		{
            if (ModelState.IsValid)
			{
				customer.TrackingState = TrackingState.Added;   
				foreach (var item in customer.CustomerAttentionProducts)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerBanks)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerContacts)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerFiles)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerFollows)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerSales)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerShares)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
				foreach (var item in customer.CustomerWarehouses)
				{
					item.CustomerId = customer.Id ;
					item.TrackingState = TrackingState.Added;
				}
               try{ 
				this.customerService.ApplyChanges(customer);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a customer record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(customer);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var customer = await Task.Run(() => {
                return new Customer();
                });
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Customers/Edit/:id
		public ActionResult Edit(int id)
		{
			var customer = this.customerService.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}
		//POST: Customers/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Customer customer)
		{
			if (ModelState.IsValid)
			{
				customer.TrackingState = TrackingState.Modified;
												foreach (var item in customer.CustomerAttentionProducts)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerBanks)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerContacts)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerFiles)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerFollows)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerSales)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerShares)
				{
					item.CustomerId = customer.Id ;
				}
								foreach (var item in customer.CustomerWarehouses)
				{
					item.CustomerId = customer.Id ;
				}
				 
                try{
				this.customerService.ApplyChanges(customer);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Customer record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(customer);
		}
        //删除当前记录
		//GET: Customers/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.customerService.Queryable().Where(x => x.Id == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
		}
		 
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerAttentionProduct/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerAttentionProduct(int id)
				{
			var customerattentionproductRepository = this.unitOfWork.RepositoryAsync<CustomerAttentionProduct>();
						var customerattentionproduct = await customerattentionproductRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customerattentionproduct == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerAttentionProductEditForm", new CustomerAttentionProduct());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customerattentionproduct.CustomerId );  
										}
			return PartialView("_CustomerAttentionProductEditForm",  customerattentionproduct);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerAttentionProduct
		[HttpGet]
				public async Task<ActionResult> CreateCustomerAttentionProduct(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerAttentionProductEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerAttentionProduct/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerAttentionProduct(int  id)
				{
            try{
			   var customerattentionproductRepository = this.unitOfWork.RepositoryAsync<CustomerAttentionProduct>();
			   customerattentionproductRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerBank/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerBank(int id)
				{
			var customerbankRepository = this.unitOfWork.RepositoryAsync<CustomerBank>();
						var customerbank = await customerbankRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customerbank == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerBankEditForm", new CustomerBank());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customerbank.CustomerId );  
										}
			return PartialView("_CustomerBankEditForm",  customerbank);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerBank
		[HttpGet]
				public async Task<ActionResult> CreateCustomerBank(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerBankEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerBank/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerBank(int  id)
				{
            try{
			   var customerbankRepository = this.unitOfWork.RepositoryAsync<CustomerBank>();
			   customerbankRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerContact/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerContact(int id)
				{
			var customercontactRepository = this.unitOfWork.RepositoryAsync<CustomerContact>();
						var customercontact = await customercontactRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customercontact == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerContactEditForm", new CustomerContact());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customercontact.CustomerId );  
										}
			return PartialView("_CustomerContactEditForm",  customercontact);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerContact
		[HttpGet]
				public async Task<ActionResult> CreateCustomerContact(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerContactEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerContact/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerContact(int  id)
				{
            try{
			   var customercontactRepository = this.unitOfWork.RepositoryAsync<CustomerContact>();
			   customercontactRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerFile/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerFile(int id)
				{
			var customerfileRepository = this.unitOfWork.RepositoryAsync<CustomerFile>();
						var customerfile = await customerfileRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customerfile == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerFileEditForm", new CustomerFile());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customerfile.CustomerId );  
										}
			return PartialView("_CustomerFileEditForm",  customerfile);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerFile
		[HttpGet]
				public async Task<ActionResult> CreateCustomerFile(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerFileEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerFile/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerFile(int  id)
				{
            try{
			   var customerfileRepository = this.unitOfWork.RepositoryAsync<CustomerFile>();
			   customerfileRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerFollow/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerFollow(int id)
				{
			var customerfollowRepository = this.unitOfWork.RepositoryAsync<CustomerFollow>();
						var customerfollow = await customerfollowRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customerfollow == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerFollowEditForm", new CustomerFollow());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customerfollow.CustomerId );  
										}
			return PartialView("_CustomerFollowEditForm",  customerfollow);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerFollow
		[HttpGet]
				public async Task<ActionResult> CreateCustomerFollow(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerFollowEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerFollow/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerFollow(int  id)
				{
            try{
			   var customerfollowRepository = this.unitOfWork.RepositoryAsync<CustomerFollow>();
			   customerfollowRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerSales/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerSales(int id)
				{
			var customersalesRepository = this.unitOfWork.RepositoryAsync<CustomerSales>();
						var customersales = await customersalesRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customersales == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerSalesEditForm", new CustomerSales());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customersales.CustomerId );  
										}
			return PartialView("_CustomerSalesEditForm",  customersales);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerSales
		[HttpGet]
				public async Task<ActionResult> CreateCustomerSales(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerSalesEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerSales/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerSales(int  id)
				{
            try{
			   var customersalesRepository = this.unitOfWork.RepositoryAsync<CustomerSales>();
			   customersalesRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerShare/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerShare(int id)
				{
			var customershareRepository = this.unitOfWork.RepositoryAsync<CustomerShare>();
						var customershare = await customershareRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customershare == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerShareEditForm", new CustomerShare());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customershare.CustomerId );  
										}
			return PartialView("_CustomerShareEditForm",  customershare);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerShare
		[HttpGet]
				public async Task<ActionResult> CreateCustomerShare(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerShareEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerShare/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerShare(int  id)
				{
            try{
			   var customershareRepository = this.unitOfWork.RepositoryAsync<CustomerShare>();
			   customershareRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
		//Get Detail Row By Id For Edit
		//Get : Customers/EditCustomerWarehouse/:id
		[HttpGet]
				public async Task<ActionResult> EditCustomerWarehouse(int id)
				{
			var customerwarehouseRepository = this.unitOfWork.RepositoryAsync<CustomerWarehouse>();
						var customerwarehouse = await customerwarehouseRepository.FindAsync(id);
									var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();             
						if (customerwarehouse == null)
			{
											ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
											//return HttpNotFound();
				return PartialView("_CustomerWarehouseEditForm", new CustomerWarehouse());
			}
			else
			{
											 ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().ToListAsync(), "Id", "CustomerCode" , customerwarehouse.CustomerId );  
										}
			return PartialView("_CustomerWarehouseEditForm",  customerwarehouse);
		}
		//Get Create Row By Id For Edit
		//Get : Customers/CreateCustomerWarehouse
		[HttpGet]
				public async Task<ActionResult> CreateCustomerWarehouse(int customerid)
				{
		  			  var customerRepository = this.unitOfWork.RepositoryAsync<Customer>();    
			  			  ViewBag.CustomerId = new SelectList(await customerRepository.Queryable().OrderBy(n=>n.CustomerCode).ToListAsync(), "Id", "CustomerCode" );
			  		  			return PartialView("_CustomerWarehouseEditForm");
		}
		//Post Delete Detail Row By Id
		//Get : Customers/DeleteCustomerWarehouse/:id
		[HttpGet]
				public async Task<ActionResult> DeleteCustomerWarehouse(int  id)
				{
            try{
			   var customerwarehouseRepository = this.unitOfWork.RepositoryAsync<CustomerWarehouse>();
			   customerwarehouseRepository.Delete(id);
			   			   var result = await this.unitOfWork.SaveChangesAsync();
			   			return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
			}
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
		}
       
		//Get : Customers/GetCustomerAttentionProductsByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerAttentionProductsByCustomerId(int id)
		{
			var customerattentionproducts = await this.customerService.GetCustomerAttentionProductsByCustomerIdAsync(id);
			var rows = customerattentionproducts.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Category = n.Category,
    CategoryId = n.CategoryId,
    ProductNo = n.ProductNo,
    ProductName = n.ProductName,
    CUR = n.CUR,
    Pric = n.Pric,
    SummaryQuote = n.SummaryQuote,
    SummaryOrders = n.SummaryOrders,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerBanksByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerBanksByCustomerId(int id)
		{
			var customerbanks = await this.customerService.GetCustomerBanksByCustomerIdAsync(id);
			var rows = customerbanks.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    AccountName = n.AccountName,
    Bank = n.Bank,
    AccountNo = n.AccountNo,
    BankUse = n.BankUse,
    SWIFT = n.SWIFT,
    CUR = n.CUR,
    Remark = n.Remark,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerContactsByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerContactsByCustomerId(int id)
		{
			var customercontacts = await this.customerService.GetCustomerContactsByCustomerIdAsync(id);
			var rows = customercontacts.Select( n => new { 

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
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerFilesByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerFilesByCustomerId(int id)
		{
			var customerfiles = await this.customerService.GetCustomerFilesByCustomerIdAsync(id);
			var rows = customerfiles.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    FileName = n.FileName,
    Size = n.Size,
    Folder = n.Folder,
    FilePath = n.FilePath,
    RelativePath = n.RelativePath,
    Owner = n.Owner,
    Upload = n.Upload.ToString("yyyy-MM-dd HH:mm:ss"),
    Ext = n.Ext,
    FileId = n.FileId,
    RefKey = n.RefKey,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerFollowsByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerFollowsByCustomerId(int id)
		{
			var customerfollows = await this.customerService.GetCustomerFollowsByCustomerIdAsync(id);
			var rows = customerfollows.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    ContactName = n.ContactName,
    FollowType = n.FollowType,
    Status = n.Status,
    Owner = n.Owner,
    FollowDate = n.FollowDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Content = n.Content,
    ReminderTime = n.ReminderTime?.ToString("yyyy-MM-dd HH:mm:ss"),
    ReminderContent = n.ReminderContent,
    ReminderTo = n.ReminderTo,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerSalesByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerSalesByCustomerId(int id)
		{
			var customersales = await this.customerService.GetCustomerSalesByCustomerIdAsync(id);
			var rows = customersales.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Status = n.Status,
    Salesman = n.Salesman,
    Dept = n.Dept,
    Assigner = n.Assigner,
    AssignDate = n.AssignDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    StopCase = n.StopCase,
    Remark = n.Remark,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerSharesByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerSharesByCustomerId(int id)
		{
			var customershares = await this.customerService.GetCustomerSharesByCustomerIdAsync(id);
			var rows = customershares.Select( n => new { 

    CustomerCustomerCode = n.Customer?.CustomerCode,
    Id = n.Id,
    Owner = n.Owner,
    Dept = n.Dept,
    ShareTo = n.ShareTo,
    Module = n.Module,
    Searchable = n.Searchable,
    Editable = n.Editable,
    CustomerId = n.CustomerId,
    CustomerCode = n.CustomerCode,
    CustomerName = n.CustomerName
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
		//Get : Customers/GetCustomerWarehousesByCustomerId/:id
		[HttpGet]
		public async Task<JsonResult> GetCustomerWarehousesByCustomerId(int id)
		{
			var customerwarehouses = await this.customerService.GetCustomerWarehousesByCustomerIdAsync(id);
			var rows = customerwarehouses.Select( n => new { 

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
});
			return Json(rows, JsonRequestBehavior.AllowGet);
			
		}
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteChecked(int[] id) {
           try{
               await this.customerService.Delete(id);
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
			var fileName = "customers_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.customerService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		 
	}
}
