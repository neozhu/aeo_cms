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
/// Created Date: 2020/8/5 11:52:45
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
						if (rule.field == "BaseName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.BaseName.Contains(rule.value));
						}
						if (rule.field == "CustomerName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerName.Contains(rule.value));
						}
						if (rule.field == "CustomerType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerType.Contains(rule.value));
						}
						if (rule.field == "Country"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Country.Contains(rule.value));
						}
						if (rule.field == "Level"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Level.Contains(rule.value));
						}
						if (rule.field == "Source"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Source.Contains(rule.value));
						}
						if (rule.field == "Telephone"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Telephone.Contains(rule.value));
						}
						if (rule.field == "Fax"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Fax.Contains(rule.value));
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
						if (rule.field == "WebSite"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WebSite.Contains(rule.value));
						}
						if (rule.field == "Industry"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Industry.Contains(rule.value));
						}
						if (rule.field == "BusinessScope"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.BusinessScope.Contains(rule.value));
						}
						if (rule.field == "Address"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Address.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Payment"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Payment.Contains(rule.value));
						}
						if (rule.field == "TradeCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TradeCode.Contains(rule.value));
						}
						if (rule.field == "MasterCustom"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.MasterCustom.Contains(rule.value));
						}
						if (rule.field == "CreditCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreditCode.Contains(rule.value));
						}
						if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactName.Contains(rule.value));
						}
						if (rule.field == "Appellation"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Appellation.Contains(rule.value));
						}
						if (rule.field == "Sex"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Sex.Contains(rule.value));
						}
						if (rule.field == "Job"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Job.Contains(rule.value));
						}
						if (rule.field == "Wx"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Wx.Contains(rule.value));
						}
						if (rule.field == "PhoneNumber"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PhoneNumber.Contains(rule.value));
						}
						if (rule.field == "Email"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Email.Contains(rule.value));
						}
						if (rule.field == "ContactRemark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactRemark.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Flag" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Flag == boolval);
						}
						if (rule.field == "Logo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Logo.Contains(rule.value));
						}
						if (rule.field == "LastContactDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LastContactDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LastContactDate) <= 0);
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
    }
}
