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
/// File: QuotationProductRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/26 17:40:51
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class QuotationProductRepository  
    {
                 public static async Task<IEnumerable<QuotationProduct>> GetByQuotationIdAsync(this IRepositoryAsync<QuotationProduct> repository, int quotationid)
          => await repository
                .Queryable()
                .Where(x => x.QuotationId==quotationid).ToListAsync();
              
          
                 
	}
}



