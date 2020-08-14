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
/// File: InquiryTaskRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/14 14:42:46
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class InquiryTaskRepository  
    {
                 public static async Task<IEnumerable<InquiryTask>> GetByCompanyIdAsync(this IRepositoryAsync<InquiryTask> repository, int companyid)
          => await repository
                .Queryable()
                .Where(x => x.CompanyId==companyid).ToListAsync();
              
          
                 public static async Task<IEnumerable<InquiryTask>> GetByCustomerIdAsync(this IRepositoryAsync<InquiryTask> repository, int customerid)
          => await repository
                .Queryable()
                .Where(x => x.CustomerId==customerid).ToListAsync();
              
          
                        public static async Task<IEnumerable<InquiryTaskProduct>>   GetInquiryTaskProductsByInquiryTaskIdAsync (this IRepositoryAsync<InquiryTask> repository,int inquirytaskid)
          => await  repository.GetRepositoryAsync<InquiryTaskProduct>()
                    .Queryable()
                    .Include(x => x.InquiryTask)
                    .Where(n => n.InquiryTaskId == inquirytaskid)
                    .ToListAsync();
        
         
	}
}



