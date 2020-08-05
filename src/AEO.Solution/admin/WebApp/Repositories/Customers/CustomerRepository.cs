using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: CustomerRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/5 11:52:45
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class CustomerRepository  
    {
                        public static async Task<IEnumerable<CustomerAttentionProduct>>   GetCustomerAttentionProductsByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerAttentionProduct>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerBank>>   GetCustomerBanksByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerBank>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerContact>>   GetCustomerContactsByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerContact>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerFile>>   GetCustomerFilesByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerFile>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerFollow>>   GetCustomerFollowsByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerFollow>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerSales>>   GetCustomerSalesByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerSales>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerShare>>   GetCustomerSharesByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerShare>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CustomerWarehouse>>   GetCustomerWarehousesByCustomerIdAsync (this IRepositoryAsync<Customer> repository,int customerid)
          => await  repository.GetRepositoryAsync<CustomerWarehouse>()
                    .Queryable()
                    .Include(x => x.Customer)
                    .Where(n => n.CustomerId == customerid)
                    .ToListAsync();
        
         
	}
}



