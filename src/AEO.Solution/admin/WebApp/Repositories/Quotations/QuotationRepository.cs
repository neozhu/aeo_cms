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
/// File: QuotationRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/26 17:51:58
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class QuotationRepository  
    {
                 public static async Task<IEnumerable<Quotation>> GetByCompanyIdAsync(this IRepositoryAsync<Quotation> repository, int companyid)
          => await repository
                .Queryable()
                .Where(x => x.CompanyId==companyid).ToListAsync();
              
          
                 public static async Task<IEnumerable<Quotation>> GetByCustomerIdAsync(this IRepositoryAsync<Quotation> repository, int customerid)
          => await repository
                .Queryable()
                .Where(x => x.CustomerId==customerid).ToListAsync();
              
          
                        public static async Task<IEnumerable<QuotationFile>>   GetQuotationFilesByQuotationIdAsync (this IRepositoryAsync<Quotation> repository,int quotationid)
          => await  repository.GetRepositoryAsync<QuotationFile>()
                    .Queryable()
                    .Include(x => x.Quotation)
                    .Where(n => n.QuotationId == quotationid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<QuotationProduct>>   GetQuotationProductsByQuotationIdAsync (this IRepositoryAsync<Quotation> repository,int quotationid)
          => await  repository.GetRepositoryAsync<QuotationProduct>()
                    .Queryable()
                    .Include(x => x.Quotation)
                    .Where(n => n.QuotationId == quotationid)
                    .ToListAsync();
        
         
	}
}



