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
/// File: CustomerContactQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/5 10:49:28
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class CustomerContactQuery:QueryObject<CustomerContact>
   {
		public CustomerContactQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "Appellation"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Appellation.Contains(rule.value));
						}
						if (rule.field == "Sex"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Sex.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
						if (rule.field == "Job"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Job.Contains(rule.value));
						}
						if (rule.field == "Wx"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Wx.Contains(rule.value));
						}
						if (rule.field == "MobilePhone"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.MobilePhone.Contains(rule.value));
						}
						if (rule.field == "PhoneNumber"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PhoneNumber.Contains(rule.value));
						}
						if (rule.field == "Fax"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Fax.Contains(rule.value));
						}
						if (rule.field == "Email"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Email.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
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
         public  CustomerContactQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
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
						if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Name == rule.value);
                           } 
                           else
                           {
							And(x => x.Name.Contains(rule.value));
						    }
                        }
						if (rule.field == "Appellation"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Appellation == rule.value);
                           } 
                           else
                           {
							And(x => x.Appellation.Contains(rule.value));
						    }
                        }
						if (rule.field == "Sex"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Sex == rule.value);
                           } 
                           else
                           {
							And(x => x.Sex.Contains(rule.value));
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
						if (rule.field == "Job"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Job == rule.value);
                           } 
                           else
                           {
							And(x => x.Job.Contains(rule.value));
						    }
                        }
						if (rule.field == "Wx"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Wx == rule.value);
                           } 
                           else
                           {
							And(x => x.Wx.Contains(rule.value));
						    }
                        }
						if (rule.field == "MobilePhone"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.MobilePhone == rule.value);
                           } 
                           else
                           {
							And(x => x.MobilePhone.Contains(rule.value));
						    }
                        }
						if (rule.field == "PhoneNumber"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.PhoneNumber == rule.value);
                           } 
                           else
                           {
							And(x => x.PhoneNumber.Contains(rule.value));
						    }
                        }
						if (rule.field == "Fax"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Fax == rule.value);
                           } 
                           else
                           {
							And(x => x.Fax.Contains(rule.value));
						    }
                        }
						if (rule.field == "Email"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Email == rule.value);
                           } 
                           else
                           {
							And(x => x.Email.Contains(rule.value));
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
               }
            }
            return this;
         }    
    }
}
