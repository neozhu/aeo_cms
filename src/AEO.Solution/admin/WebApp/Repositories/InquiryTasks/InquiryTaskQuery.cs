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
/// File: InquiryTaskQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/14 14:42:46
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class InquiryTaskQuery:QueryObject<InquiryTask>
   {
		public InquiryTaskQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaskNo.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Salesman"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Salesman.Contains(rule.value));
						}
						if (rule.field == "CompanyId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CompanyId == val);
                                break;
                            case "notequal":
                                And(x => x.CompanyId != val);
                                break;
                            case "less":
                                And(x => x.CompanyId < val);
                                break;
                            case "lessorequal":
                                And(x => x.CompanyId <= val);
                                break;
                            case "greater":
                                And(x => x.CompanyId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CompanyId >= val);
                                break;
                            default:
                                And(x => x.CompanyId == val);
                                break;
                        }
						}
						if (rule.field == "CompanyName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CompanyName.Contains(rule.value));
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
						if (rule.field == "CustomerCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerCode.Contains(rule.value));
						}
						if (rule.field == "CustomerName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerName.Contains(rule.value));
						}
						if (rule.field == "Country"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Country.Contains(rule.value));
						}
						if (rule.field == "Cur"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Cur.Contains(rule.value));
						}
						if (rule.field == "ExchangeRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ExchangeRate == val);
                                break;
                            case "notequal":
                                And(x => x.ExchangeRate != val);
                                break;
                            case "less":
                                And(x => x.ExchangeRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.ExchangeRate <= val);
                                break;
                            case "greater":
                                And(x => x.ExchangeRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ExchangeRate >= val);
                                break;
                            default:
                                And(x => x.ExchangeRate == val);
                                break;
                        }
						}
						if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactName.Contains(rule.value));
						}
						if (rule.field == "ContactInfo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactInfo.Contains(rule.value));
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
						if (rule.field == "Enddate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.Enddate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.Enddate) <= 0);
						    }
						}
						if (rule.field == "Urgency"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Urgency.Contains(rule.value));
						}
						if (rule.field == "Demande"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Demande.Contains(rule.value));
						}
						if (rule.field == "PreRemind" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PreRemind == val);
                                break;
                            case "notequal":
                                And(x => x.PreRemind != val);
                                break;
                            case "less":
                                And(x => x.PreRemind < val);
                                break;
                            case "lessorequal":
                                And(x => x.PreRemind <= val);
                                break;
                            case "greater":
                                And(x => x.PreRemind > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PreRemind >= val);
                                break;
                            default:
                                And(x => x.PreRemind == val);
                                break;
                        }
						}
						if (rule.field == "Check1" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check1 == boolval);
						}
						if (rule.field == "Creator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Creator.Contains(rule.value));
						}
						if (rule.field == "Executor"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Executor.Contains(rule.value));
						}
						if (rule.field == "Check2" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check2 == boolval);
						}
						if (rule.field == "Check3" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check3 == boolval);
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
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
         public  InquiryTaskQuery ByCompanyIdWithfilter(int companyid, IEnumerable<filterRule> filters)
         {
            And(x => x.CompanyId == companyid);
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
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.TaskNo == rule.value);
                           } 
                           else
                           {
							And(x => x.TaskNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Status == rule.value);
                           } 
                           else
                           {
							And(x => x.Status.Contains(rule.value));
						    }
                        }
						if (rule.field == "Salesman"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Salesman == rule.value);
                           } 
                           else
                           {
							And(x => x.Salesman.Contains(rule.value));
						    }
                        }
						if (rule.field == "CompanyId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CompanyId == val);
                                break;
                            case "notequal":
                                And(x => x.CompanyId != val);
                                break;
                            case "less":
                                And(x => x.CompanyId < val);
                                break;
                            case "lessorequal":
                                And(x => x.CompanyId <= val);
                                break;
                            case "greater":
                                And(x => x.CompanyId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CompanyId >= val);
                                break;
                            default:
                                And(x => x.CompanyId == val);
                                break;
                        }
						}
						if (rule.field == "CompanyName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CompanyName == rule.value);
                           } 
                           else
                           {
							And(x => x.CompanyName.Contains(rule.value));
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
						if (rule.field == "Country"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Country == rule.value);
                           } 
                           else
                           {
							And(x => x.Country.Contains(rule.value));
						    }
                        }
						if (rule.field == "Cur"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Cur == rule.value);
                           } 
                           else
                           {
							And(x => x.Cur.Contains(rule.value));
						    }
                        }
						if (rule.field == "ExchangeRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ExchangeRate == val);
                                break;
                            case "notequal":
                                And(x => x.ExchangeRate != val);
                                break;
                            case "less":
                                And(x => x.ExchangeRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.ExchangeRate <= val);
                                break;
                            case "greater":
                                And(x => x.ExchangeRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ExchangeRate >= val);
                                break;
                            default:
                                And(x => x.ExchangeRate == val);
                                break;
                        }
						}
						if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ContactName == rule.value);
                           } 
                           else
                           {
							And(x => x.ContactName.Contains(rule.value));
						    }
                        }
						if (rule.field == "ContactInfo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ContactInfo == rule.value);
                           } 
                           else
                           {
							And(x => x.ContactInfo.Contains(rule.value));
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
						if (rule.field == "Enddate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.Enddate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.Enddate) <= 0);
						    }
                        }
						if (rule.field == "Urgency"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Urgency == rule.value);
                           } 
                           else
                           {
							And(x => x.Urgency.Contains(rule.value));
						    }
                        }
						if (rule.field == "Demande"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Demande == rule.value);
                           } 
                           else
                           {
							And(x => x.Demande.Contains(rule.value));
						    }
                        }
						if (rule.field == "PreRemind" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PreRemind == val);
                                break;
                            case "notequal":
                                And(x => x.PreRemind != val);
                                break;
                            case "less":
                                And(x => x.PreRemind < val);
                                break;
                            case "lessorequal":
                                And(x => x.PreRemind <= val);
                                break;
                            case "greater":
                                And(x => x.PreRemind > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PreRemind >= val);
                                break;
                            default:
                                And(x => x.PreRemind == val);
                                break;
                        }
						}
						if (rule.field == "Check1" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check1 == boolval);
						}
						if (rule.field == "Creator"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Creator == rule.value);
                           } 
                           else
                           {
							And(x => x.Creator.Contains(rule.value));
						    }
                        }
						if (rule.field == "Executor"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Executor == rule.value);
                           } 
                           else
                           {
							And(x => x.Executor.Contains(rule.value));
						    }
                        }
						if (rule.field == "Check2" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check2 == boolval);
						}
						if (rule.field == "Check3" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check3 == boolval);
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Owner == rule.value);
                           } 
                           else
                           {
							And(x => x.Owner.Contains(rule.value));
						    }
                        }
               }
            }
            return this;
         }    
         public  InquiryTaskQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
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
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.TaskNo == rule.value);
                           } 
                           else
                           {
							And(x => x.TaskNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Status == rule.value);
                           } 
                           else
                           {
							And(x => x.Status.Contains(rule.value));
						    }
                        }
						if (rule.field == "Salesman"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Salesman == rule.value);
                           } 
                           else
                           {
							And(x => x.Salesman.Contains(rule.value));
						    }
                        }
						if (rule.field == "CompanyId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CompanyId == val);
                                break;
                            case "notequal":
                                And(x => x.CompanyId != val);
                                break;
                            case "less":
                                And(x => x.CompanyId < val);
                                break;
                            case "lessorequal":
                                And(x => x.CompanyId <= val);
                                break;
                            case "greater":
                                And(x => x.CompanyId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CompanyId >= val);
                                break;
                            default:
                                And(x => x.CompanyId == val);
                                break;
                        }
						}
						if (rule.field == "CompanyName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CompanyName == rule.value);
                           } 
                           else
                           {
							And(x => x.CompanyName.Contains(rule.value));
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
						if (rule.field == "Country"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Country == rule.value);
                           } 
                           else
                           {
							And(x => x.Country.Contains(rule.value));
						    }
                        }
						if (rule.field == "Cur"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Cur == rule.value);
                           } 
                           else
                           {
							And(x => x.Cur.Contains(rule.value));
						    }
                        }
						if (rule.field == "ExchangeRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ExchangeRate == val);
                                break;
                            case "notequal":
                                And(x => x.ExchangeRate != val);
                                break;
                            case "less":
                                And(x => x.ExchangeRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.ExchangeRate <= val);
                                break;
                            case "greater":
                                And(x => x.ExchangeRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ExchangeRate >= val);
                                break;
                            default:
                                And(x => x.ExchangeRate == val);
                                break;
                        }
						}
						if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ContactName == rule.value);
                           } 
                           else
                           {
							And(x => x.ContactName.Contains(rule.value));
						    }
                        }
						if (rule.field == "ContactInfo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ContactInfo == rule.value);
                           } 
                           else
                           {
							And(x => x.ContactInfo.Contains(rule.value));
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
						if (rule.field == "Enddate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.Enddate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.Enddate) <= 0);
						    }
                        }
						if (rule.field == "Urgency"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Urgency == rule.value);
                           } 
                           else
                           {
							And(x => x.Urgency.Contains(rule.value));
						    }
                        }
						if (rule.field == "Demande"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Demande == rule.value);
                           } 
                           else
                           {
							And(x => x.Demande.Contains(rule.value));
						    }
                        }
						if (rule.field == "PreRemind" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PreRemind == val);
                                break;
                            case "notequal":
                                And(x => x.PreRemind != val);
                                break;
                            case "less":
                                And(x => x.PreRemind < val);
                                break;
                            case "lessorequal":
                                And(x => x.PreRemind <= val);
                                break;
                            case "greater":
                                And(x => x.PreRemind > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PreRemind >= val);
                                break;
                            default:
                                And(x => x.PreRemind == val);
                                break;
                        }
						}
						if (rule.field == "Check1" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check1 == boolval);
						}
						if (rule.field == "Creator"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Creator == rule.value);
                           } 
                           else
                           {
							And(x => x.Creator.Contains(rule.value));
						    }
                        }
						if (rule.field == "Executor"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Executor == rule.value);
                           } 
                           else
                           {
							And(x => x.Executor.Contains(rule.value));
						    }
                        }
						if (rule.field == "Check2" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check2 == boolval);
						}
						if (rule.field == "Check3" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Check3 == boolval);
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Owner == rule.value);
                           } 
                           else
                           {
							And(x => x.Owner.Contains(rule.value));
						    }
                        }
               }
            }
            return this;
         }    
    }
}
