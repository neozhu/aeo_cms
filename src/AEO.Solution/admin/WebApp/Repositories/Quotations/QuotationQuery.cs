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
/// File: QuotationQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/26 17:51:58
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class QuotationQuery:QueryObject<Quotation>
   {
		public QuotationQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "QpNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.QpNo.Contains(rule.value));
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
						if (rule.field == "CompanyCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CompanyCode.Contains(rule.value));
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
						if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactName.Contains(rule.value));
						}
						if (rule.field == "ContactInfo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactInfo.Contains(rule.value));
						}
						if (rule.field == "QuoteDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.QuoteDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.QuoteDate) <= 0);
						    }
						}
						if (rule.field == "ExpiryDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ExpiryDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ExpiryDate) <= 0);
						    }
						}
						if (rule.field == "LoadingPort"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LoadingPort.Contains(rule.value));
						}
						if (rule.field == "DischargePort"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DischargePort.Contains(rule.value));
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
						if (rule.field == "PriceTerm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PriceTerm.Contains(rule.value));
						}
						if (rule.field == "PayMode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PayMode.Contains(rule.value));
						}
						if (rule.field == "GoodsAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GoodsAmount == val);
                                break;
                            case "notequal":
                                And(x => x.GoodsAmount != val);
                                break;
                            case "less":
                                And(x => x.GoodsAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.GoodsAmount <= val);
                                break;
                            case "greater":
                                And(x => x.GoodsAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GoodsAmount >= val);
                                break;
                            default:
                                And(x => x.GoodsAmount == val);
                                break;
                        }
						}
						if (rule.field == "ChargeAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ChargeAmount == val);
                                break;
                            case "notequal":
                                And(x => x.ChargeAmount != val);
                                break;
                            case "less":
                                And(x => x.ChargeAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.ChargeAmount <= val);
                                break;
                            case "greater":
                                And(x => x.ChargeAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ChargeAmount >= val);
                                break;
                            default:
                                And(x => x.ChargeAmount == val);
                                break;
                        }
						}
						if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TotalAmount == val);
                                break;
                            case "notequal":
                                And(x => x.TotalAmount != val);
                                break;
                            case "less":
                                And(x => x.TotalAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.TotalAmount <= val);
                                break;
                            case "greater":
                                And(x => x.TotalAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TotalAmount >= val);
                                break;
                            default:
                                And(x => x.TotalAmount == val);
                                break;
                        }
						}
						if (rule.field == "FormName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.FormName.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "InquiryNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InquiryNo.Contains(rule.value));
						}
						if (rule.field == "TaskNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaskNo.Contains(rule.value));
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
						if (rule.field == "Initiator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Initiator.Contains(rule.value));
						}
						if (rule.field == "SubmitDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.SubmitDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.SubmitDate) <= 0);
						    }
						}
						if (rule.field == "ToAuditor"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ToAuditor.Contains(rule.value));
						}
						if (rule.field == "Approver"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Approver.Contains(rule.value));
						}
						if (rule.field == "ApprovedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ApprovedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ApprovedDate) <= 0);
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
         public  QuotationQuery ByCompanyIdWithfilter(int companyid, IEnumerable<filterRule> filters)
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
						if (rule.field == "CompanyCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CompanyCode == rule.value);
                           } 
                           else
                           {
							And(x => x.CompanyCode.Contains(rule.value));
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
						if (rule.field == "QuoteDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.QuoteDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.QuoteDate) <= 0);
						    }
                        }
						if (rule.field == "ExpiryDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ExpiryDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ExpiryDate) <= 0);
						    }
                        }
						if (rule.field == "LoadingPort"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.LoadingPort == rule.value);
                           } 
                           else
                           {
							And(x => x.LoadingPort.Contains(rule.value));
						    }
                        }
						if (rule.field == "DischargePort"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.DischargePort == rule.value);
                           } 
                           else
                           {
							And(x => x.DischargePort.Contains(rule.value));
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
						if (rule.field == "PriceTerm"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PriceTerm == rule.value);
                           } 
                           else
                           {
							And(x => x.PriceTerm.Contains(rule.value));
						    }
                        }
						if (rule.field == "PayMode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PayMode == rule.value);
                           } 
                           else
                           {
							And(x => x.PayMode.Contains(rule.value));
						    }
                        }
						if (rule.field == "GoodsAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GoodsAmount == val);
                                break;
                            case "notequal":
                                And(x => x.GoodsAmount != val);
                                break;
                            case "less":
                                And(x => x.GoodsAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.GoodsAmount <= val);
                                break;
                            case "greater":
                                And(x => x.GoodsAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GoodsAmount >= val);
                                break;
                            default:
                                And(x => x.GoodsAmount == val);
                                break;
                        }
						}
						if (rule.field == "ChargeAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ChargeAmount == val);
                                break;
                            case "notequal":
                                And(x => x.ChargeAmount != val);
                                break;
                            case "less":
                                And(x => x.ChargeAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.ChargeAmount <= val);
                                break;
                            case "greater":
                                And(x => x.ChargeAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ChargeAmount >= val);
                                break;
                            default:
                                And(x => x.ChargeAmount == val);
                                break;
                        }
						}
						if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TotalAmount == val);
                                break;
                            case "notequal":
                                And(x => x.TotalAmount != val);
                                break;
                            case "less":
                                And(x => x.TotalAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.TotalAmount <= val);
                                break;
                            case "greater":
                                And(x => x.TotalAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TotalAmount >= val);
                                break;
                            default:
                                And(x => x.TotalAmount == val);
                                break;
                        }
						}
						if (rule.field == "FormName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.FormName == rule.value);
                           } 
                           else
                           {
							And(x => x.FormName.Contains(rule.value));
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
						if (rule.field == "Initiator"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Initiator == rule.value);
                           } 
                           else
                           {
							And(x => x.Initiator.Contains(rule.value));
						    }
                        }
						if (rule.field == "SubmitDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.SubmitDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.SubmitDate) <= 0);
						    }
                        }
						if (rule.field == "ToAuditor"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ToAuditor == rule.value);
                           } 
                           else
                           {
							And(x => x.ToAuditor.Contains(rule.value));
						    }
                        }
						if (rule.field == "Approver"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Approver == rule.value);
                           } 
                           else
                           {
							And(x => x.Approver.Contains(rule.value));
						    }
                        }
						if (rule.field == "ApprovedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ApprovedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ApprovedDate) <= 0);
						    }
                        }
               }
            }
            return this;
         }    
         public  QuotationQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
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
						if (rule.field == "CompanyCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.CompanyCode == rule.value);
                           } 
                           else
                           {
							And(x => x.CompanyCode.Contains(rule.value));
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
						if (rule.field == "QuoteDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.QuoteDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.QuoteDate) <= 0);
						    }
                        }
						if (rule.field == "ExpiryDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ExpiryDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ExpiryDate) <= 0);
						    }
                        }
						if (rule.field == "LoadingPort"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.LoadingPort == rule.value);
                           } 
                           else
                           {
							And(x => x.LoadingPort.Contains(rule.value));
						    }
                        }
						if (rule.field == "DischargePort"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.DischargePort == rule.value);
                           } 
                           else
                           {
							And(x => x.DischargePort.Contains(rule.value));
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
						if (rule.field == "PriceTerm"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PriceTerm == rule.value);
                           } 
                           else
                           {
							And(x => x.PriceTerm.Contains(rule.value));
						    }
                        }
						if (rule.field == "PayMode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PayMode == rule.value);
                           } 
                           else
                           {
							And(x => x.PayMode.Contains(rule.value));
						    }
                        }
						if (rule.field == "GoodsAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GoodsAmount == val);
                                break;
                            case "notequal":
                                And(x => x.GoodsAmount != val);
                                break;
                            case "less":
                                And(x => x.GoodsAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.GoodsAmount <= val);
                                break;
                            case "greater":
                                And(x => x.GoodsAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GoodsAmount >= val);
                                break;
                            default:
                                And(x => x.GoodsAmount == val);
                                break;
                        }
						}
						if (rule.field == "ChargeAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ChargeAmount == val);
                                break;
                            case "notequal":
                                And(x => x.ChargeAmount != val);
                                break;
                            case "less":
                                And(x => x.ChargeAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.ChargeAmount <= val);
                                break;
                            case "greater":
                                And(x => x.ChargeAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ChargeAmount >= val);
                                break;
                            default:
                                And(x => x.ChargeAmount == val);
                                break;
                        }
						}
						if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TotalAmount == val);
                                break;
                            case "notequal":
                                And(x => x.TotalAmount != val);
                                break;
                            case "less":
                                And(x => x.TotalAmount < val);
                                break;
                            case "lessorequal":
                                And(x => x.TotalAmount <= val);
                                break;
                            case "greater":
                                And(x => x.TotalAmount > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TotalAmount >= val);
                                break;
                            default:
                                And(x => x.TotalAmount == val);
                                break;
                        }
						}
						if (rule.field == "FormName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.FormName == rule.value);
                           } 
                           else
                           {
							And(x => x.FormName.Contains(rule.value));
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
						if (rule.field == "Initiator"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Initiator == rule.value);
                           } 
                           else
                           {
							And(x => x.Initiator.Contains(rule.value));
						    }
                        }
						if (rule.field == "SubmitDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.SubmitDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.SubmitDate) <= 0);
						    }
                        }
						if (rule.field == "ToAuditor"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ToAuditor == rule.value);
                           } 
                           else
                           {
							And(x => x.ToAuditor.Contains(rule.value));
						    }
                        }
						if (rule.field == "Approver"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Approver == rule.value);
                           } 
                           else
                           {
							And(x => x.Approver.Contains(rule.value));
						    }
                        }
						if (rule.field == "ApprovedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ApprovedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ApprovedDate) <= 0);
						    }
                        }
               }
            }
            return this;
         }    
    }
}
