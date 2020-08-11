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
/// File: AeoAuthTestQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/11 9:27:08
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class AeoAuthTestQuery:QueryObject<AeoAuthTest>
   {
		public AeoAuthTestQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "TradeCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TradeCode.Contains(rule.value));
						}
						if (rule.field == "CreditCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreditCode.Contains(rule.value));
						}
						if (rule.field == "Ctype"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Ctype.Contains(rule.value));
						}
						if (rule.field == "TestNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TestNo.Contains(rule.value));
						}
						if (rule.field == "AuthType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AuthType.Contains(rule.value));
						}
						if (rule.field == "MasterCustom"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.MasterCustom.Contains(rule.value));
						}
						if (rule.field == "RegistDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.RegistDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.RegistDate) <= 0);
						    }
						}
						if (rule.field == "IsForeign"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.IsForeign.Contains(rule.value));
						}
						if (rule.field == "Zone"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Zone.Contains(rule.value));
						}
						if (rule.field == "RegistedTime" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.RegistedTime == val);
                                break;
                            case "notequal":
                                And(x => x.RegistedTime != val);
                                break;
                            case "less":
                                And(x => x.RegistedTime < val);
                                break;
                            case "lessorequal":
                                And(x => x.RegistedTime <= val);
                                break;
                            case "greater":
                                And(x => x.RegistedTime > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.RegistedTime >= val);
                                break;
                            default:
                                And(x => x.RegistedTime == val);
                                break;
                        }
						}
						if (rule.field == "Unit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Unit.Contains(rule.value));
						}
						if (rule.field == "AuthDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.AuthDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.AuthDate) <= 0);
						    }
						}
						if (rule.field == "Tester"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Tester.Contains(rule.value));
						}
						if (rule.field == "Year" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Year == val);
                                break;
                            case "notequal":
                                And(x => x.Year != val);
                                break;
                            case "less":
                                And(x => x.Year < val);
                                break;
                            case "lessorequal":
                                And(x => x.Year <= val);
                                break;
                            case "greater":
                                And(x => x.Year > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Year >= val);
                                break;
                            default:
                                And(x => x.Year == val);
                                break;
                        }
						}
						if (rule.field == "BeginDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.BeginDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.BeginDate) <= 0);
						    }
						}
						if (rule.field == "EndDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.EndDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.EndDate) <= 0);
						    }
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "StdScore" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.StdScore == val);
                                break;
                            case "notequal":
                                And(x => x.StdScore != val);
                                break;
                            case "less":
                                And(x => x.StdScore < val);
                                break;
                            case "lessorequal":
                                And(x => x.StdScore <= val);
                                break;
                            case "greater":
                                And(x => x.StdScore > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.StdScore >= val);
                                break;
                            default:
                                And(x => x.StdScore == val);
                                break;
                        }
						}
						if (rule.field == "Score" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Score == val);
                                break;
                            case "notequal":
                                And(x => x.Score != val);
                                break;
                            case "less":
                                And(x => x.Score < val);
                                break;
                            case "lessorequal":
                                And(x => x.Score <= val);
                                break;
                            case "greater":
                                And(x => x.Score > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Score >= val);
                                break;
                            default:
                                And(x => x.Score == val);
                                break;
                        }
						}
						if (rule.field == "Result"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Result.Contains(rule.value));
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
