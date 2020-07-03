using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.IO;
namespace WebApp.Services
{
/// <summary>
/// File: ICustomerService.cs
/// Purpose: Service interfaces. Services expose a service interface
/// to which all inbound messages are sent. You can think of a service interface
/// as a façade that exposes the business logic implemented in the application
/// Created Date: 2020/7/3 14:23:06
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public interface ICustomerService:IService<Customer>
    {
         Task<IEnumerable<CustomerAttentionProduct>>   GetCustomerAttentionProductsByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerBank>>   GetCustomerBanksByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerCommunication>>   GetCustomerCommunicationsByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerContact>>   GetCustomerContactsByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerFile>>   GetCustomerFilesByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerFollow>>   GetCustomerFollowsByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerInvoice>>   GetCustomerInvoicesByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerSales>>   GetCustomerSalesByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerShare>>   GetCustomerSharesByCustomerIdAsync (int customerid);
         Task<IEnumerable<CustomerWarehouse>>   GetCustomerWarehousesByCustomerIdAsync (int customerid);
 
		Task ImportDataTableAsync(DataTable datatable,string username="");
		Task<Stream> ExportExcelAsync( string filterRules = "",string sort = "Id", string order = "asc");
	    Task Delete(int[] id);
    }
}