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
/// File: CustomerAttentionProductQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/3 13:52:54
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class CustomerAttentionProductQuery:QueryObject<CustomerAttentionProduct>
   {
		public CustomerAttentionProductQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "CustomerCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerCode.Contains(rule.value));
						}
						if (rule.field == "CustomerName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerName.Contains(rule.value));
						}
						if (rule.field == "ProductNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductNo.Contains(rule.value));
						}
						if (rule.field == "ProductName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductName.Contains(rule.value));
						}
						if (rule.field == "CUR"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CUR.Contains(rule.value));
						}
						if (rule.field == "Pric" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Pric == val);
                                break;
                            case "notequal":
                                And(x => x.Pric != val);
                                break;
                            case "less":
                                And(x => x.Pric < val);
                                break;
                            case "lessorequal":
                                And(x => x.Pric <= val);
                                break;
                            case "greater":
                                And(x => x.Pric > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Pric >= val);
                                break;
                            default:
                                And(x => x.Pric == val);
                                break;
                        }
						}
						if (rule.field == "SummaryQuote" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SummaryQuote == val);
                                break;
                            case "notequal":
                                And(x => x.SummaryQuote != val);
                                break;
                            case "less":
                                And(x => x.SummaryQuote < val);
                                break;
                            case "lessorequal":
                                And(x => x.SummaryQuote <= val);
                                break;
                            case "greater":
                                And(x => x.SummaryQuote > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SummaryQuote >= val);
                                break;
                            default:
                                And(x => x.SummaryQuote == val);
                                break;
                        }
						}
						if (rule.field == "SummaryOrders" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SummaryOrders == val);
                                break;
                            case "notequal":
                                And(x => x.SummaryOrders != val);
                                break;
                            case "less":
                                And(x => x.SummaryOrders < val);
                                break;
                            case "lessorequal":
                                And(x => x.SummaryOrders <= val);
                                break;
                            case "greater":
                                And(x => x.SummaryOrders > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SummaryOrders >= val);
                                break;
                            default:
                                And(x => x.SummaryOrders == val);
                                break;
                        }
						}
						if (rule.field == "CustomerId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CustomerId == val);
                                break;
                            case "notequal":
                                And(x => x.CustomerId != val);
                                break;
                            case "less":
                                And(x => x.CustomerId < val);
                                break;
                            case "lessorequal":
                                And(x => x.CustomerId <= val);
                                break;
                            case "greater":
                                And(x => x.CustomerId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CustomerId >= val);
                                break;
                            default:
                                And(x => x.CustomerId == val);
                                break;
                        }
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
         public  CustomerAttentionProductQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
         {
            And(x => x.CustomerId == customerid);
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
						if (rule.field == "CustomerCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CustomerCode == rule.value);
                           } 
                           else
                           {
							And(x => x.CustomerCode.Contains(rule.value));
						    }
                        }
						if (rule.field == "CustomerName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CustomerName == rule.value);
                           } 
                           else
                           {
							And(x => x.CustomerName.Contains(rule.value));
						    }
                        }
						if (rule.field == "ProductNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ProductNo == rule.value);
                           } 
                           else
                           {
							And(x => x.ProductNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "ProductName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ProductName == rule.value);
                           } 
                           else
                           {
							And(x => x.ProductName.Contains(rule.value));
						    }
                        }
						if (rule.field == "CUR"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CUR == rule.value);
                           } 
                           else
                           {
							And(x => x.CUR.Contains(rule.value));
						    }
                        }
						if (rule.field == "Pric" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Pric == val);
                                break;
                            case "notequal":
                                And(x => x.Pric != val);
                                break;
                            case "less":
                                And(x => x.Pric < val);
                                break;
                            case "lessorequal":
                                And(x => x.Pric <= val);
                                break;
                            case "greater":
                                And(x => x.Pric > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Pric >= val);
                                break;
                            default:
                                And(x => x.Pric == val);
                                break;
                        }
						}
						if (rule.field == "SummaryQuote" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SummaryQuote == val);
                                break;
                            case "notequal":
                                And(x => x.SummaryQuote != val);
                                break;
                            case "less":
                                And(x => x.SummaryQuote < val);
                                break;
                            case "lessorequal":
                                And(x => x.SummaryQuote <= val);
                                break;
                            case "greater":
                                And(x => x.SummaryQuote > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SummaryQuote >= val);
                                break;
                            default:
                                And(x => x.SummaryQuote == val);
                                break;
                        }
						}
						if (rule.field == "SummaryOrders" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SummaryOrders == val);
                                break;
                            case "notequal":
                                And(x => x.SummaryOrders != val);
                                break;
                            case "less":
                                And(x => x.SummaryOrders < val);
                                break;
                            case "lessorequal":
                                And(x => x.SummaryOrders <= val);
                                break;
                            case "greater":
                                And(x => x.SummaryOrders > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SummaryOrders >= val);
                                break;
                            default:
                                And(x => x.SummaryOrders == val);
                                break;
                        }
						}
						if (rule.field == "CustomerId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CustomerId == val);
                                break;
                            case "notequal":
                                And(x => x.CustomerId != val);
                                break;
                            case "less":
                                And(x => x.CustomerId < val);
                                break;
                            case "lessorequal":
                                And(x => x.CustomerId <= val);
                                break;
                            case "greater":
                                And(x => x.CustomerId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CustomerId >= val);
                                break;
                            default:
                                And(x => x.CustomerId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
