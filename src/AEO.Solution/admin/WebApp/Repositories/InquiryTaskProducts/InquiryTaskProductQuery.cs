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
/// File: InquiryTaskProductQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/14 14:39:52
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class InquiryTaskProductQuery:QueryObject<InquiryTaskProduct>
   {
		public InquiryTaskProductQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "ProductNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductNo.Contains(rule.value));
						}
						if (rule.field == "ProductName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductName.Contains(rule.value));
						}
						if (rule.field == "CategoryName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CategoryName.Contains(rule.value));
						}
						if (rule.field == "ProductEnName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductEnName.Contains(rule.value));
						}
						if (rule.field == "CnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CnDescription.Contains(rule.value));
						}
						if (rule.field == "EnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EnDescription.Contains(rule.value));
						}
						if (rule.field == "ThirdProductNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ThirdProductNo.Contains(rule.value));
						}
						if (rule.field == "Qty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Qty == val);
                                break;
                            case "notequal":
                                And(x => x.Qty != val);
                                break;
                            case "less":
                                And(x => x.Qty < val);
                                break;
                            case "lessorequal":
                                And(x => x.Qty <= val);
                                break;
                            case "greater":
                                And(x => x.Qty > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Qty >= val);
                                break;
                            default:
                                And(x => x.Qty == val);
                                break;
                        }
						}
						if (rule.field == "Unit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Unit.Contains(rule.value));
						}
						if (rule.field == "PriceType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PriceType.Contains(rule.value));
						}
						if (rule.field == "Price" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Price == val);
                                break;
                            case "notequal":
                                And(x => x.Price != val);
                                break;
                            case "less":
                                And(x => x.Price < val);
                                break;
                            case "lessorequal":
                                And(x => x.Price <= val);
                                break;
                            case "greater":
                                And(x => x.Price > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Price >= val);
                                break;
                            default:
                                And(x => x.Price == val);
                                break;
                        }
						}
						if (rule.field == "Executor"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Executor.Contains(rule.value));
						}
						if (rule.field == "SupplierCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SupplierCode.Contains(rule.value));
						}
						if (rule.field == "SupplierName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SupplierName.Contains(rule.value));
						}
						if (rule.field == "SamplePic"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SamplePic.Contains(rule.value));
						}
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaskNo.Contains(rule.value));
						}
						if (rule.field == "InquiryTaskId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InquiryTaskId == val);
                                break;
                            case "notequal":
                                And(x => x.InquiryTaskId != val);
                                break;
                            case "less":
                                And(x => x.InquiryTaskId < val);
                                break;
                            case "lessorequal":
                                And(x => x.InquiryTaskId <= val);
                                break;
                            case "greater":
                                And(x => x.InquiryTaskId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InquiryTaskId >= val);
                                break;
                            default:
                                And(x => x.InquiryTaskId == val);
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
         public  InquiryTaskProductQuery ByInquiryTaskIdWithfilter(int inquirytaskid, IEnumerable<filterRule> filters)
         {
            And(x => x.InquiryTaskId == inquirytaskid);
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
						if (rule.field == "CategoryName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CategoryName == rule.value);
                           } 
                           else
                           {
							And(x => x.CategoryName.Contains(rule.value));
						    }
                        }
						if (rule.field == "ProductEnName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ProductEnName == rule.value);
                           } 
                           else
                           {
							And(x => x.ProductEnName.Contains(rule.value));
						    }
                        }
						if (rule.field == "CnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CnDescription == rule.value);
                           } 
                           else
                           {
							And(x => x.CnDescription.Contains(rule.value));
						    }
                        }
						if (rule.field == "EnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.EnDescription == rule.value);
                           } 
                           else
                           {
							And(x => x.EnDescription.Contains(rule.value));
						    }
                        }
						if (rule.field == "ThirdProductNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ThirdProductNo == rule.value);
                           } 
                           else
                           {
							And(x => x.ThirdProductNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "Qty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Qty == val);
                                break;
                            case "notequal":
                                And(x => x.Qty != val);
                                break;
                            case "less":
                                And(x => x.Qty < val);
                                break;
                            case "lessorequal":
                                And(x => x.Qty <= val);
                                break;
                            case "greater":
                                And(x => x.Qty > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Qty >= val);
                                break;
                            default:
                                And(x => x.Qty == val);
                                break;
                        }
						}
						if (rule.field == "Unit"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Unit == rule.value);
                           } 
                           else
                           {
							And(x => x.Unit.Contains(rule.value));
						    }
                        }
						if (rule.field == "PriceType"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PriceType == rule.value);
                           } 
                           else
                           {
							And(x => x.PriceType.Contains(rule.value));
						    }
                        }
						if (rule.field == "Price" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Price == val);
                                break;
                            case "notequal":
                                And(x => x.Price != val);
                                break;
                            case "less":
                                And(x => x.Price < val);
                                break;
                            case "lessorequal":
                                And(x => x.Price <= val);
                                break;
                            case "greater":
                                And(x => x.Price > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Price >= val);
                                break;
                            default:
                                And(x => x.Price == val);
                                break;
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
						if (rule.field == "SupplierCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.SupplierCode == rule.value);
                           } 
                           else
                           {
							And(x => x.SupplierCode.Contains(rule.value));
						    }
                        }
						if (rule.field == "SupplierName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.SupplierName == rule.value);
                           } 
                           else
                           {
							And(x => x.SupplierName.Contains(rule.value));
						    }
                        }
						if (rule.field == "SamplePic"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.SamplePic == rule.value);
                           } 
                           else
                           {
							And(x => x.SamplePic.Contains(rule.value));
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
						if (rule.field == "InquiryTaskId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InquiryTaskId == val);
                                break;
                            case "notequal":
                                And(x => x.InquiryTaskId != val);
                                break;
                            case "less":
                                And(x => x.InquiryTaskId < val);
                                break;
                            case "lessorequal":
                                And(x => x.InquiryTaskId <= val);
                                break;
                            case "greater":
                                And(x => x.InquiryTaskId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InquiryTaskId >= val);
                                break;
                            default:
                                And(x => x.InquiryTaskId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
