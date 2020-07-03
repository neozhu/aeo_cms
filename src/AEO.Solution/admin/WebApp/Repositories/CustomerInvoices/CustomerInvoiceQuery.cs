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
/// File: CustomerInvoiceQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/3 14:13:15
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class CustomerInvoiceQuery:QueryObject<CustomerInvoice>
   {
		public CustomerInvoiceQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "InvName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InvName.Contains(rule.value));
						}
						if (rule.field == "InvType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InvType.Contains(rule.value));
						}
						if (rule.field == "InvCountry"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InvCountry.Contains(rule.value));
						}
						if (rule.field == "InvTax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InvTax == val);
                                break;
                            case "notequal":
                                And(x => x.InvTax != val);
                                break;
                            case "less":
                                And(x => x.InvTax < val);
                                break;
                            case "lessorequal":
                                And(x => x.InvTax <= val);
                                break;
                            case "greater":
                                And(x => x.InvTax > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InvTax >= val);
                                break;
                            default:
                                And(x => x.InvTax == val);
                                break;
                        }
						}
						if (rule.field == "TaxNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TaxNo.Contains(rule.value));
						}
						if (rule.field == "InvUse"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.InvUse.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "CustomerCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerCode.Contains(rule.value));
						}
						if (rule.field == "CustomerName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerName.Contains(rule.value));
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
         public  CustomerInvoiceQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
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
						if (rule.field == "InvName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.InvName == rule.value);
                           } 
                           else
                           {
							And(x => x.InvName.Contains(rule.value));
						    }
                        }
						if (rule.field == "InvType"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.InvType == rule.value);
                           } 
                           else
                           {
							And(x => x.InvType.Contains(rule.value));
						    }
                        }
						if (rule.field == "InvCountry"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.InvCountry == rule.value);
                           } 
                           else
                           {
							And(x => x.InvCountry.Contains(rule.value));
						    }
                        }
						if (rule.field == "InvTax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InvTax == val);
                                break;
                            case "notequal":
                                And(x => x.InvTax != val);
                                break;
                            case "less":
                                And(x => x.InvTax < val);
                                break;
                            case "lessorequal":
                                And(x => x.InvTax <= val);
                                break;
                            case "greater":
                                And(x => x.InvTax > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InvTax >= val);
                                break;
                            default:
                                And(x => x.InvTax == val);
                                break;
                        }
						}
						if (rule.field == "TaxNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.TaxNo == rule.value);
                           } 
                           else
                           {
							And(x => x.TaxNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "InvUse"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.InvUse == rule.value);
                           } 
                           else
                           {
							And(x => x.InvUse.Contains(rule.value));
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
