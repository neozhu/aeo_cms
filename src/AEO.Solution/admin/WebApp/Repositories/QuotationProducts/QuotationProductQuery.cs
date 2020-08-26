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
/// File: QuotationProductQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/26 17:40:51
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class QuotationProductQuery:QueryObject<QuotationProduct>
   {
		public QuotationProductQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "HSCODE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.HSCODE.Contains(rule.value));
						}
						if (rule.field == "HSADDTAXRATE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.HSADDTAXRATE == val);
                                break;
                            case "notequal":
                                And(x => x.HSADDTAXRATE != val);
                                break;
                            case "less":
                                And(x => x.HSADDTAXRATE < val);
                                break;
                            case "lessorequal":
                                And(x => x.HSADDTAXRATE <= val);
                                break;
                            case "greater":
                                And(x => x.HSADDTAXRATE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.HSADDTAXRATE >= val);
                                break;
                            default:
                                And(x => x.HSADDTAXRATE == val);
                                break;
                        }
						}
						if (rule.field == "HSBACKTAXRATE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.HSBACKTAXRATE == val);
                                break;
                            case "notequal":
                                And(x => x.HSBACKTAXRATE != val);
                                break;
                            case "less":
                                And(x => x.HSBACKTAXRATE < val);
                                break;
                            case "lessorequal":
                                And(x => x.HSBACKTAXRATE <= val);
                                break;
                            case "greater":
                                And(x => x.HSBACKTAXRATE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.HSBACKTAXRATE >= val);
                                break;
                            default:
                                And(x => x.HSBACKTAXRATE == val);
                                break;
                        }
						}
						if (rule.field == "CUSTBASIC"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CUSTBASIC.Contains(rule.value));
						}
						if (rule.field == "GUIDEPRICE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GUIDEPRICE == val);
                                break;
                            case "notequal":
                                And(x => x.GUIDEPRICE != val);
                                break;
                            case "less":
                                And(x => x.GUIDEPRICE < val);
                                break;
                            case "lessorequal":
                                And(x => x.GUIDEPRICE <= val);
                                break;
                            case "greater":
                                And(x => x.GUIDEPRICE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GUIDEPRICE >= val);
                                break;
                            default:
                                And(x => x.GUIDEPRICE == val);
                                break;
                        }
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
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
						if (rule.field == "Cur"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Cur.Contains(rule.value));
						}
						if (rule.field == "Amount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Amount == val);
                                break;
                            case "notequal":
                                And(x => x.Amount != val);
                                break;
                            case "less":
                                And(x => x.Amount < val);
                                break;
                            case "lessorequal":
                                And(x => x.Amount <= val);
                                break;
                            case "greater":
                                And(x => x.Amount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Amount >= val);
                                break;
                            default:
                                And(x => x.Amount == val);
                                break;
                        }
						}
						if (rule.field == "USDAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.USDAmount == val);
                                break;
                            case "notequal":
                                And(x => x.USDAmount != val);
                                break;
                            case "less":
                                And(x => x.USDAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.USDAmount <= val);
                                break;
                            case "greater":
                                And(x => x.USDAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.USDAmount >= val);
                                break;
                            default:
                                And(x => x.USDAmount == val);
                                break;
                        }
						}
						if (rule.field == "RMBAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.RMBAmount == val);
                                break;
                            case "notequal":
                                And(x => x.RMBAmount != val);
                                break;
                            case "less":
                                And(x => x.RMBAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.RMBAmount <= val);
                                break;
                            case "greater":
                                And(x => x.RMBAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.RMBAmount >= val);
                                break;
                            default:
                                And(x => x.RMBAmount == val);
                                break;
                        }
						}
						if (rule.field == "BrightcmsRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BrightcmsRate == val);
                                break;
                            case "notequal":
                                And(x => x.BrightcmsRate != val);
                                break;
                            case "less":
                                And(x => x.BrightcmsRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.BrightcmsRate <= val);
                                break;
                            case "greater":
                                And(x => x.BrightcmsRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BrightcmsRate >= val);
                                break;
                            default:
                                And(x => x.BrightcmsRate == val);
                                break;
                        }
						}
						if (rule.field == "BrightcmsFcy" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BrightcmsFcy == val);
                                break;
                            case "notequal":
                                And(x => x.BrightcmsFcy != val);
                                break;
                            case "less":
                                And(x => x.BrightcmsFcy < val);
                                break;
                            case "lessorequal":
                                And(x => x.BrightcmsFcy <= val);
                                break;
                            case "greater":
                                And(x => x.BrightcmsFcy > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BrightcmsFcy >= val);
                                break;
                            default:
                                And(x => x.BrightcmsFcy == val);
                                break;
                        }
						}
						if (rule.field == "DarkcmsRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.DarkcmsRate == val);
                                break;
                            case "notequal":
                                And(x => x.DarkcmsRate != val);
                                break;
                            case "less":
                                And(x => x.DarkcmsRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.DarkcmsRate <= val);
                                break;
                            case "greater":
                                And(x => x.DarkcmsRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.DarkcmsRate >= val);
                                break;
                            default:
                                And(x => x.DarkcmsRate == val);
                                break;
                        }
						}
						if (rule.field == "DarkcmsFcy" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.DarkcmsFcy == val);
                                break;
                            case "notequal":
                                And(x => x.DarkcmsFcy != val);
                                break;
                            case "less":
                                And(x => x.DarkcmsFcy < val);
                                break;
                            case "lessorequal":
                                And(x => x.DarkcmsFcy <= val);
                                break;
                            case "greater":
                                And(x => x.DarkcmsFcy > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.DarkcmsFcy >= val);
                                break;
                            default:
                                And(x => x.DarkcmsFcy == val);
                                break;
                        }
						}
						if (rule.field == "Executor"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Executor.Contains(rule.value));
						}
						if (rule.field == "Logo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Logo.Contains(rule.value));
						}
						if (rule.field == "QpNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.QpNo.Contains(rule.value));
						}
						if (rule.field == "QuotationId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.QuotationId == val);
                                break;
                            case "notequal":
                                And(x => x.QuotationId != val);
                                break;
                            case "less":
                                And(x => x.QuotationId < val);
                                break;
                            case "lessorequal":
                                And(x => x.QuotationId <= val);
                                break;
                            case "greater":
                                And(x => x.QuotationId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.QuotationId >= val);
                                break;
                            default:
                                And(x => x.QuotationId == val);
                                break;
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
         public  QuotationProductQuery ByQuotationIdWithfilter(int quotationid, IEnumerable<filterRule> filters)
         {
            And(x => x.QuotationId == quotationid);
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
						if (rule.field == "HSCODE"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.HSCODE == rule.value);
                           } 
                           else
                           {
							And(x => x.HSCODE.Contains(rule.value));
						    }
                        }
						if (rule.field == "HSADDTAXRATE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.HSADDTAXRATE == val);
                                break;
                            case "notequal":
                                And(x => x.HSADDTAXRATE != val);
                                break;
                            case "less":
                                And(x => x.HSADDTAXRATE < val);
                                break;
                            case "lessorequal":
                                And(x => x.HSADDTAXRATE <= val);
                                break;
                            case "greater":
                                And(x => x.HSADDTAXRATE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.HSADDTAXRATE >= val);
                                break;
                            default:
                                And(x => x.HSADDTAXRATE == val);
                                break;
                        }
						}
						if (rule.field == "HSBACKTAXRATE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.HSBACKTAXRATE == val);
                                break;
                            case "notequal":
                                And(x => x.HSBACKTAXRATE != val);
                                break;
                            case "less":
                                And(x => x.HSBACKTAXRATE < val);
                                break;
                            case "lessorequal":
                                And(x => x.HSBACKTAXRATE <= val);
                                break;
                            case "greater":
                                And(x => x.HSBACKTAXRATE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.HSBACKTAXRATE >= val);
                                break;
                            default:
                                And(x => x.HSBACKTAXRATE == val);
                                break;
                        }
						}
						if (rule.field == "CUSTBASIC"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CUSTBASIC == rule.value);
                           } 
                           else
                           {
							And(x => x.CUSTBASIC.Contains(rule.value));
						    }
                        }
						if (rule.field == "GUIDEPRICE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GUIDEPRICE == val);
                                break;
                            case "notequal":
                                And(x => x.GUIDEPRICE != val);
                                break;
                            case "less":
                                And(x => x.GUIDEPRICE < val);
                                break;
                            case "lessorequal":
                                And(x => x.GUIDEPRICE <= val);
                                break;
                            case "greater":
                                And(x => x.GUIDEPRICE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GUIDEPRICE >= val);
                                break;
                            default:
                                And(x => x.GUIDEPRICE == val);
                                break;
                        }
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Remark == rule.value);
                           } 
                           else
                           {
							And(x => x.Remark.Contains(rule.value));
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
						if (rule.field == "Amount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Amount == val);
                                break;
                            case "notequal":
                                And(x => x.Amount != val);
                                break;
                            case "less":
                                And(x => x.Amount < val);
                                break;
                            case "lessorequal":
                                And(x => x.Amount <= val);
                                break;
                            case "greater":
                                And(x => x.Amount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Amount >= val);
                                break;
                            default:
                                And(x => x.Amount == val);
                                break;
                        }
						}
						if (rule.field == "USDAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.USDAmount == val);
                                break;
                            case "notequal":
                                And(x => x.USDAmount != val);
                                break;
                            case "less":
                                And(x => x.USDAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.USDAmount <= val);
                                break;
                            case "greater":
                                And(x => x.USDAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.USDAmount >= val);
                                break;
                            default:
                                And(x => x.USDAmount == val);
                                break;
                        }
						}
						if (rule.field == "RMBAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.RMBAmount == val);
                                break;
                            case "notequal":
                                And(x => x.RMBAmount != val);
                                break;
                            case "less":
                                And(x => x.RMBAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.RMBAmount <= val);
                                break;
                            case "greater":
                                And(x => x.RMBAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.RMBAmount >= val);
                                break;
                            default:
                                And(x => x.RMBAmount == val);
                                break;
                        }
						}
						if (rule.field == "BrightcmsRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BrightcmsRate == val);
                                break;
                            case "notequal":
                                And(x => x.BrightcmsRate != val);
                                break;
                            case "less":
                                And(x => x.BrightcmsRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.BrightcmsRate <= val);
                                break;
                            case "greater":
                                And(x => x.BrightcmsRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BrightcmsRate >= val);
                                break;
                            default:
                                And(x => x.BrightcmsRate == val);
                                break;
                        }
						}
						if (rule.field == "BrightcmsFcy" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BrightcmsFcy == val);
                                break;
                            case "notequal":
                                And(x => x.BrightcmsFcy != val);
                                break;
                            case "less":
                                And(x => x.BrightcmsFcy < val);
                                break;
                            case "lessorequal":
                                And(x => x.BrightcmsFcy <= val);
                                break;
                            case "greater":
                                And(x => x.BrightcmsFcy > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BrightcmsFcy >= val);
                                break;
                            default:
                                And(x => x.BrightcmsFcy == val);
                                break;
                        }
						}
						if (rule.field == "DarkcmsRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.DarkcmsRate == val);
                                break;
                            case "notequal":
                                And(x => x.DarkcmsRate != val);
                                break;
                            case "less":
                                And(x => x.DarkcmsRate < val);
                                break;
                            case "lessorequal":
                                And(x => x.DarkcmsRate <= val);
                                break;
                            case "greater":
                                And(x => x.DarkcmsRate > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.DarkcmsRate >= val);
                                break;
                            default:
                                And(x => x.DarkcmsRate == val);
                                break;
                        }
						}
						if (rule.field == "DarkcmsFcy" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.DarkcmsFcy == val);
                                break;
                            case "notequal":
                                And(x => x.DarkcmsFcy != val);
                                break;
                            case "less":
                                And(x => x.DarkcmsFcy < val);
                                break;
                            case "lessorequal":
                                And(x => x.DarkcmsFcy <= val);
                                break;
                            case "greater":
                                And(x => x.DarkcmsFcy > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.DarkcmsFcy >= val);
                                break;
                            default:
                                And(x => x.DarkcmsFcy == val);
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
						if (rule.field == "Logo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Logo == rule.value);
                           } 
                           else
                           {
							And(x => x.Logo.Contains(rule.value));
						    }
                        }
						if (rule.field == "QpNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.QpNo == rule.value);
                           } 
                           else
                           {
							And(x => x.QpNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "QuotationId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.QuotationId == val);
                                break;
                            case "notequal":
                                And(x => x.QuotationId != val);
                                break;
                            case "less":
                                And(x => x.QuotationId < val);
                                break;
                            case "lessorequal":
                                And(x => x.QuotationId <= val);
                                break;
                            case "greater":
                                And(x => x.QuotationId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.QuotationId >= val);
                                break;
                            default:
                                And(x => x.QuotationId == val);
                                break;
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
               }
            }
            return this;
         }    
    }
}
