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
/// File: InquiryRefQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/19 10:57:48
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class InquiryRefQuery:QueryObject<InquiryRef>
   {
		public InquiryRefQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "InquiryNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InquiryNo.Contains(rule.value));
						}
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaskNo.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
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
						if (rule.field == "Salesman"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Salesman.Contains(rule.value));
						}
						if (rule.field == "Dept"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Dept.Contains(rule.value));
						}
						if (rule.field == "Ver" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Ver == val);
                                break;
                            case "notequal":
                                And(x => x.Ver != val);
                                break;
                            case "less":
                                And(x => x.Ver < val);
                                break;
                            case "lessorequal":
                                And(x => x.Ver <= val);
                                break;
                            case "greater":
                                And(x => x.Ver > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Ver >= val);
                                break;
                            default:
                                And(x => x.Ver == val);
                                break;
                        }
						}
						if (rule.field == "InquiryId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InquiryId == val);
                                break;
                            case "notequal":
                                And(x => x.InquiryId != val);
                                break;
                            case "less":
                                And(x => x.InquiryId < val);
                                break;
                            case "lessorequal":
                                And(x => x.InquiryId <= val);
                                break;
                            case "greater":
                                And(x => x.InquiryId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InquiryId >= val);
                                break;
                            default:
                                And(x => x.InquiryId == val);
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
         public  InquiryRefQuery ByInquiryIdWithfilter(int inquiryid, IEnumerable<filterRule> filters)
         {
            And(x => x.InquiryId == inquiryid);
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
						if (rule.field == "InquiryNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.InquiryNo == rule.value);
                           } 
                           else
                           {
							And(x => x.InquiryNo.Contains(rule.value));
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
						if (rule.field == "Dept"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Dept == rule.value);
                           } 
                           else
                           {
							And(x => x.Dept.Contains(rule.value));
						    }
                        }
						if (rule.field == "Ver" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Ver == val);
                                break;
                            case "notequal":
                                And(x => x.Ver != val);
                                break;
                            case "less":
                                And(x => x.Ver < val);
                                break;
                            case "lessorequal":
                                And(x => x.Ver <= val);
                                break;
                            case "greater":
                                And(x => x.Ver > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Ver >= val);
                                break;
                            default:
                                And(x => x.Ver == val);
                                break;
                        }
						}
						if (rule.field == "InquiryId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InquiryId == val);
                                break;
                            case "notequal":
                                And(x => x.InquiryId != val);
                                break;
                            case "less":
                                And(x => x.InquiryId < val);
                                break;
                            case "lessorequal":
                                And(x => x.InquiryId <= val);
                                break;
                            case "greater":
                                And(x => x.InquiryId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InquiryId >= val);
                                break;
                            default:
                                And(x => x.InquiryId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
