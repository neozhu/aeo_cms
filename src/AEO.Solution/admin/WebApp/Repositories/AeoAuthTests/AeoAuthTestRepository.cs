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
/// File: AeoAuthTestRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2020/8/11 9:27:08
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class AeoAuthTestRepository  
    {
                        public static async Task<IEnumerable<AeoQuestion>>   GetAeoquestionsByAeoAuthTestIdAsync (this IRepositoryAsync<AeoAuthTest> repository,int aeoauthtestid)
          => await  repository.GetRepositoryAsync<AeoQuestion>()
                    .Queryable()
                    .Include(x => x.AeoAuthTest)
                    .Where(n => n.AeoAuthTestId == aeoauthtestid)
                    .ToListAsync();
        
         
	}
}



