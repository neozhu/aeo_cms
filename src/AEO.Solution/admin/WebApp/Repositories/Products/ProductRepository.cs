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
/// File: ProductRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/7/30 16:44:59
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class ProductRepository  
    {
                        public static async Task<IEnumerable<ProductFile>>   GetProductFilesByProductIdAsync (this IRepositoryAsync<Product> repository,int productid)
          => await  repository.GetRepositoryAsync<ProductFile>()
                    .Queryable()
                    .Include(x => x.Product)
                    .Where(n => n.ProductId == productid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<ProductPurchaseHistoricalPrice>>   GetProductPurchaseHistoricalPricesByProductIdAsync (this IRepositoryAsync<Product> repository,int productid)
          => await  repository.GetRepositoryAsync<ProductPurchaseHistoricalPrice>()
                    .Queryable()
                    .Include(x => x.Product)
                    .Where(n => n.ProductId == productid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<ProductSalesHistoricalPrice>>   GetProductSalesHistoricalPricesByProductIdAsync (this IRepositoryAsync<Product> repository,int productid)
          => await  repository.GetRepositoryAsync<ProductSalesHistoricalPrice>()
                    .Queryable()
                    .Include(x => x.Product)
                    .Where(n => n.ProductId == productid)
                    .ToListAsync();
        
         
	}
}



