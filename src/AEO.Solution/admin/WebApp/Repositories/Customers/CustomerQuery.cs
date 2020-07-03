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
/// File: CustomerQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/3 14:23:06
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class CustomerQuery:QueryObject<Customer>
   {
		public CustomerQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "CustomerEName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerEName.Contains(rule.value));
						}
						if (rule.field == "CustomerType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerType.Contains(rule.value));
						}
						if (rule.field == "Overseas" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Overseas == boolval);
						}
						if (rule.field == "CustomerType3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerType3.Contains(rule.value));
						}
						if (rule.field == "Capital" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Capital == val);
                                break;
                            case "notequal":
                                And(x => x.Capital != val);
                                break;
                            case "less":
                                And(x => x.Capital < val);
                                break;
                            case "lessorequal":
                                And(x => x.Capital <= val);
                                break;
                            case "greater":
                                And(x => x.Capital > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Capital >= val);
                                break;
                            default:
                                And(x => x.Capital == val);
                                break;
                        }
						}
						if (rule.field == "CURR"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CURR.Contains(rule.value));
						}
						if (rule.field == "TaxProperty"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaxProperty.Contains(rule.value));
						}
						if (rule.field == "ParentOrg"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ParentOrg.Contains(rule.value));
						}
						if (rule.field == "CustomMaster"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomMaster.Contains(rule.value));
						}
						if (rule.field == "TradeCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TradeCode.Contains(rule.value));
						}
						if (rule.field == "Country"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Country.Contains(rule.value));
						}
						if (rule.field == "Zone"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Zone.Contains(rule.value));
						}
						if (rule.field == "Scale"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Scale.Contains(rule.value));
						}
						if (rule.field == "Level"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Level.Contains(rule.value));
						}
						if (rule.field == "Value"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Value.Contains(rule.value));
						}
						if (rule.field == "CreditRating"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreditRating.Contains(rule.value));
						}
						if (rule.field == "Source"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Source.Contains(rule.value));
						}
						if (rule.field == "Industry"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Industry.Contains(rule.value));
						}
						if (rule.field == "Cash" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Cash == val);
                                break;
                            case "notequal":
                                And(x => x.Cash != val);
                                break;
                            case "less":
                                And(x => x.Cash < val);
                                break;
                            case "lessorequal":
                                And(x => x.Cash <= val);
                                break;
                            case "greater":
                                And(x => x.Cash > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Cash >= val);
                                break;
                            default:
                                And(x => x.Cash == val);
                                break;
                        }
						}
						if (rule.field == "CashCURR"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CashCURR.Contains(rule.value));
						}
						if (rule.field == "SDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SDesc.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "CProvinces1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CProvinces1.Contains(rule.value));
						}
						if (rule.field == "CCity1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CCity1.Contains(rule.value));
						}
						if (rule.field == "CCounty1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CCounty1.Contains(rule.value));
						}
						if (rule.field == "CAddress1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CAddress1.Contains(rule.value));
						}
						if (rule.field == "CProvinces2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CProvinces2.Contains(rule.value));
						}
						if (rule.field == "CCity2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CCity2.Contains(rule.value));
						}
						if (rule.field == "CCounty2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CCounty2.Contains(rule.value));
						}
						if (rule.field == "CAddress2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CAddress2.Contains(rule.value));
						}
						if (rule.field == "EAddress1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EAddress1.Contains(rule.value));
						}
						if (rule.field == "EAddress2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EAddress2.Contains(rule.value));
						}
						if (rule.field == "PostCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PostCode.Contains(rule.value));
						}
						if (rule.field == "WebSite"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WebSite.Contains(rule.value));
						}
						if (rule.field == "BusinessScope"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.BusinessScope.Contains(rule.value));
						}
						if (rule.field == "Remark1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark1.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Status1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status1.Contains(rule.value));
						}
						if (rule.field == "Status2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status2.Contains(rule.value));
						}
						if (rule.field == "Status3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status3.Contains(rule.value));
						}
						if (rule.field == "Status4"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status4.Contains(rule.value));
						}
						if (rule.field == "Status5"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status5.Contains(rule.value));
						}
						if (rule.field == "Logo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Logo.Contains(rule.value));
						}
						if (rule.field == "CompanyCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CompanyCode.Contains(rule.value));
						}
						if (rule.field == "CompanyName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CompanyName.Contains(rule.value));
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
