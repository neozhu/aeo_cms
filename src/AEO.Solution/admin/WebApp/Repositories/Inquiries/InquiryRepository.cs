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
/// File: InquiryRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/19 11:03:54
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class InquiryRepository  
    {
                 public static async Task<IEnumerable<Inquiry>> GetByCustomerIdAsync(this IRepositoryAsync<Inquiry> repository, int customerid)
          => await repository
                .Queryable()
                .Where(x => x.CustomerId==customerid).ToListAsync();
              
          
                 public static async Task<IEnumerable<Inquiry>> GetByCompanyIdAsync(this IRepositoryAsync<Inquiry> repository, int companyid)
          => await repository
                .Queryable()
                .Where(x => x.CompanyId==companyid).ToListAsync();
              
          
                        public static async Task<IEnumerable<InquiryFile>>   GetInquiryfilesByInquiryIdAsync (this IRepositoryAsync<Inquiry> repository,int inquiryid)
          => await  repository.GetRepositoryAsync<InquiryFile>()
                    .Queryable()
                    .Include(x => x.Inquiry)
                    .Where(n => n.InquiryId == inquiryid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<InquiryProduct>>   GetInquiryproductsByInquiryIdAsync (this IRepositoryAsync<Inquiry> repository,int inquiryid)
          => await  repository.GetRepositoryAsync<InquiryProduct>()
                    .Queryable()
                    .Include(x => x.Inquiry)
                    .Where(n => n.InquiryId == inquiryid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<InquiryRef>>   GetInquiryrefsByInquiryIdAsync (this IRepositoryAsync<Inquiry> repository,int inquiryid)
          => await  repository.GetRepositoryAsync<InquiryRef>()
                    .Queryable()
                    .Include(x => x.Inquiry)
                    .Where(n => n.InquiryId == inquiryid)
                    .ToListAsync();
        
         
	}
}



