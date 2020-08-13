using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using System.Web.WebPages;
using WebApp.Models;

namespace WebApp.Repositories
{
/// <summary>
/// File: MarketActQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/13 11:31:38
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class MarketActQuery:QueryObject<MarketAct>
   {
		public MarketActQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Id == val);
                                break;
                            case "notequal":
                                And(x => x.Id != val);
                                break;
                            case "less":
                                And(x => x.Id < val);
                                break;
                            case "lessorequal":
                                And(x => x.Id <= val);
                                break;
                            case "greater":
                                And(x => x.Id > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Name.Contains(rule.value));
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "ActType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ActType.Contains(rule.value));
						}
						if (rule.field == "PlanStartDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PlanStartDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PlanStartDate) <= 0);
						    }
						}
						if (rule.field == "PlanFinishDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PlanFinishDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PlanFinishDate) <= 0);
						    }
						}
						if (rule.field == "BudgetExpense" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BudgetExpense == val);
                                break;
                            case "notequal":
                                And(x => x.BudgetExpense != val);
                                break;
                            case "less":
                                And(x => x.BudgetExpense < val);
                                break;
                            case "lessorequal":
                                And(x => x.BudgetExpense <= val);
                                break;
                            case "greater":
                                And(x => x.BudgetExpense > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BudgetExpense >= val);
                                break;
                            default:
                                And(x => x.BudgetExpense == val);
                                break;
                        }
						}
						if (rule.field == "Cur"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Cur.Contains(rule.value));
						}
						if (rule.field == "Address"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Address.Contains(rule.value));
						}
						if (rule.field == "PlanDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PlanDesc.Contains(rule.value));
						}
						if (rule.field == "ActualStartDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ActualStartDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ActualStartDate) <= 0);
						    }
						}
						if (rule.field == "ActualFinishDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ActualFinishDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ActualFinishDate) <= 0);
						    }
						}
						if (rule.field == "ActExpense" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ActExpense == val);
                                break;
                            case "notequal":
                                And(x => x.ActExpense != val);
                                break;
                            case "less":
                                And(x => x.ActExpense < val);
                                break;
                            case "lessorequal":
                                And(x => x.ActExpense <= val);
                                break;
                            case "greater":
                                And(x => x.ActExpense > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ActExpense >= val);
                                break;
                            default:
                                And(x => x.ActExpense == val);
                                break;
                        }
						}
						if (rule.field == "Income" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Income == val);
                                break;
                            case "notequal":
                                And(x => x.Income != val);
                                break;
                            case "less":
                                And(x => x.Income < val);
                                break;
                            case "lessorequal":
                                And(x => x.Income <= val);
                                break;
                            case "greater":
                                And(x => x.Income > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Income >= val);
                                break;
                            default:
                                And(x => x.Income == val);
                                break;
                        }
						}
						if (rule.field == "ExecDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ExecDesc.Contains(rule.value));
						}
						if (rule.field == "SumaryDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SumaryDesc.Contains(rule.value));
						}
						if (rule.field == "EffectDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EffectDesc.Contains(rule.value));
						}
						if (rule.field == "CreatedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.CreatedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.CreatedDate) <= 0);
						    }
						}
						if (rule.field == "CreatedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreatedBy.Contains(rule.value));
						}
						if (rule.field == "LastModifiedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LastModifiedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LastModifiedDate) <= 0);
						    }
						}
						if (rule.field == "LastModifiedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LastModifiedBy.Contains(rule.value));
						}
						if (rule.field == "TenantId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TenantId == val);
                                break;
                            case "notequal":
                                And(x => x.TenantId != val);
                                break;
                            case "less":
                                And(x => x.TenantId < val);
                                break;
                            case "lessorequal":
                                And(x => x.TenantId <= val);
                                break;
                            case "greater":
                                And(x => x.TenantId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TenantId >= val);
                                break;
                            default:
                                And(x => x.TenantId == val);
                                break;
                        }
						}
     
               }
           }
            return this;
        }
    }
}
