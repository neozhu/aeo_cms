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
/// File: CustomerWarehouseQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/3 14:14:50
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class CustomerWarehouseQuery:QueryObject<CustomerWarehouse>
   {
		public CustomerWarehouseQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "WarehouseCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WarehouseCode.Contains(rule.value));
						}
						if (rule.field == "WarehouseName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WarehouseName.Contains(rule.value));
						}
						if (rule.field == "WarehouseType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WarehouseType.Contains(rule.value));
						}
						if (rule.field == "FactoryGuard" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.FactoryGuard == boolval);
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Provinces"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Provinces.Contains(rule.value));
						}
						if (rule.field == "City"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.City.Contains(rule.value));
						}
						if (rule.field == "County"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.County.Contains(rule.value));
						}
						if (rule.field == "WAddress"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WAddress.Contains(rule.value));
						}
						if (rule.field == "EAddress1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EAddress1.Contains(rule.value));
						}
						if (rule.field == "Remark1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark1.Contains(rule.value));
						}
						if (rule.field == "WUser"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WUser.Contains(rule.value));
						}
						if (rule.field == "WDept"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WDept.Contains(rule.value));
						}
						if (rule.field == "WTitle"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WTitle.Contains(rule.value));
						}
						if (rule.field == "WSex"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WSex.Contains(rule.value));
						}
						if (rule.field == "WPhone"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WPhone.Contains(rule.value));
						}
						if (rule.field == "WFax"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WFax.Contains(rule.value));
						}
						if (rule.field == "WMPhone1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WMPhone1.Contains(rule.value));
						}
						if (rule.field == "WMPhone2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WMPhone2.Contains(rule.value));
						}
						if (rule.field == "WEmail1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WEmail1.Contains(rule.value));
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
         public  CustomerWarehouseQuery ByCustomerIdWithfilter(int customerid, IEnumerable<filterRule> filters)
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
						if (rule.field == "WarehouseCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WarehouseCode == rule.value);
                           } 
                           else
                           {
							And(x => x.WarehouseCode.Contains(rule.value));
						    }
                        }
						if (rule.field == "WarehouseName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WarehouseName == rule.value);
                           } 
                           else
                           {
							And(x => x.WarehouseName.Contains(rule.value));
						    }
                        }
						if (rule.field == "WarehouseType"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WarehouseType == rule.value);
                           } 
                           else
                           {
							And(x => x.WarehouseType.Contains(rule.value));
						    }
                        }
						if (rule.field == "FactoryGuard" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.FactoryGuard == boolval);
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
						if (rule.field == "Provinces"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Provinces == rule.value);
                           } 
                           else
                           {
							And(x => x.Provinces.Contains(rule.value));
						    }
                        }
						if (rule.field == "City"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.City == rule.value);
                           } 
                           else
                           {
							And(x => x.City.Contains(rule.value));
						    }
                        }
						if (rule.field == "County"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.County == rule.value);
                           } 
                           else
                           {
							And(x => x.County.Contains(rule.value));
						    }
                        }
						if (rule.field == "WAddress"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WAddress == rule.value);
                           } 
                           else
                           {
							And(x => x.WAddress.Contains(rule.value));
						    }
                        }
						if (rule.field == "EAddress1"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.EAddress1 == rule.value);
                           } 
                           else
                           {
							And(x => x.EAddress1.Contains(rule.value));
						    }
                        }
						if (rule.field == "Remark1"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Remark1 == rule.value);
                           } 
                           else
                           {
							And(x => x.Remark1.Contains(rule.value));
						    }
                        }
						if (rule.field == "WUser"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WUser == rule.value);
                           } 
                           else
                           {
							And(x => x.WUser.Contains(rule.value));
						    }
                        }
						if (rule.field == "WDept"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WDept == rule.value);
                           } 
                           else
                           {
							And(x => x.WDept.Contains(rule.value));
						    }
                        }
						if (rule.field == "WTitle"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WTitle == rule.value);
                           } 
                           else
                           {
							And(x => x.WTitle.Contains(rule.value));
						    }
                        }
						if (rule.field == "WSex"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WSex == rule.value);
                           } 
                           else
                           {
							And(x => x.WSex.Contains(rule.value));
						    }
                        }
						if (rule.field == "WPhone"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WPhone == rule.value);
                           } 
                           else
                           {
							And(x => x.WPhone.Contains(rule.value));
						    }
                        }
						if (rule.field == "WFax"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WFax == rule.value);
                           } 
                           else
                           {
							And(x => x.WFax.Contains(rule.value));
						    }
                        }
						if (rule.field == "WMPhone1"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WMPhone1 == rule.value);
                           } 
                           else
                           {
							And(x => x.WMPhone1.Contains(rule.value));
						    }
                        }
						if (rule.field == "WMPhone2"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WMPhone2 == rule.value);
                           } 
                           else
                           {
							And(x => x.WMPhone2.Contains(rule.value));
						    }
                        }
						if (rule.field == "WEmail1"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.WEmail1 == rule.value);
                           } 
                           else
                           {
							And(x => x.WEmail1.Contains(rule.value));
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
