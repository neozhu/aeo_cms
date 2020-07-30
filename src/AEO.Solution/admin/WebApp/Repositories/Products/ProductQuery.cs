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
/// File: ProductQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/30 16:45:00
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class ProductQuery:QueryObject<Product>
   {
		public ProductQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "Category"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Category.Contains(rule.value));
						}
						if (rule.field == "ProductName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductName.Contains(rule.value));
						}
						if (rule.field == "ProductEnName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductEnName.Contains(rule.value));
						}
						if (rule.field == "Spec"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Spec.Contains(rule.value));
						}
						if (rule.field == "CnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CnDescription.Contains(rule.value));
						}
						if (rule.field == "EnDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EnDescription.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Logo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Logo.Contains(rule.value));
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
						if (rule.field == "CUSTBASIC"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CUSTBASIC.Contains(rule.value));
						}
						if (rule.field == "COUNTRY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.COUNTRY.Contains(rule.value));
						}
						if (rule.field == "TAXTYPE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TAXTYPE.Contains(rule.value));
						}
						if (rule.field == "TAXCLASS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TAXCLASS.Contains(rule.value));
						}
						if (rule.field == "Package"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Package.Contains(rule.value));
						}
						if (rule.field == "InnerBoxQty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.InnerBoxQty == val);
                                break;
                            case "notequal":
                                And(x => x.InnerBoxQty != val);
                                break;
                            case "less":
                                And(x => x.InnerBoxQty < val);
                                break;
                            case "lessorequal":
                                And(x => x.InnerBoxQty <= val);
                                break;
                            case "greater":
                                And(x => x.InnerBoxQty > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.InnerBoxQty >= val);
                                break;
                            default:
                                And(x => x.InnerBoxQty == val);
                                break;
                        }
						}
						if (rule.field == "Unit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Unit.Contains(rule.value));
						}
						if (rule.field == "GWeight" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GWeight == val);
                                break;
                            case "notequal":
                                And(x => x.GWeight != val);
                                break;
                            case "less":
                                And(x => x.GWeight < val);
                                break;
                            case "lessorequal":
                                And(x => x.GWeight <= val);
                                break;
                            case "greater":
                                And(x => x.GWeight > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GWeight >= val);
                                break;
                            default:
                                And(x => x.GWeight == val);
                                break;
                        }
						}
						if (rule.field == "GWUnit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GWUnit.Contains(rule.value));
						}
						if (rule.field == "NWeight" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.NWeight == val);
                                break;
                            case "notequal":
                                And(x => x.NWeight != val);
                                break;
                            case "less":
                                And(x => x.NWeight < val);
                                break;
                            case "lessorequal":
                                And(x => x.NWeight <= val);
                                break;
                            case "greater":
                                And(x => x.NWeight > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.NWeight >= val);
                                break;
                            default:
                                And(x => x.NWeight == val);
                                break;
                        }
						}
						if (rule.field == "NWUnit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.NWUnit.Contains(rule.value));
						}
						if (rule.field == "Volume" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Volume == val);
                                break;
                            case "notequal":
                                And(x => x.Volume != val);
                                break;
                            case "less":
                                And(x => x.Volume < val);
                                break;
                            case "lessorequal":
                                And(x => x.Volume <= val);
                                break;
                            case "greater":
                                And(x => x.Volume > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Volume >= val);
                                break;
                            default:
                                And(x => x.Volume == val);
                                break;
                        }
						}
						if (rule.field == "VUnit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.VUnit.Contains(rule.value));
						}
						if (rule.field == "Length" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Length == val);
                                break;
                            case "notequal":
                                And(x => x.Length != val);
                                break;
                            case "less":
                                And(x => x.Length < val);
                                break;
                            case "lessorequal":
                                And(x => x.Length <= val);
                                break;
                            case "greater":
                                And(x => x.Length > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Length >= val);
                                break;
                            default:
                                And(x => x.Length == val);
                                break;
                        }
						}
						if (rule.field == "Width" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Width == val);
                                break;
                            case "notequal":
                                And(x => x.Width != val);
                                break;
                            case "less":
                                And(x => x.Width < val);
                                break;
                            case "lessorequal":
                                And(x => x.Width <= val);
                                break;
                            case "greater":
                                And(x => x.Width > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Width >= val);
                                break;
                            default:
                                And(x => x.Width == val);
                                break;
                        }
						}
						if (rule.field == "High" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.High == val);
                                break;
                            case "notequal":
                                And(x => x.High != val);
                                break;
                            case "less":
                                And(x => x.High < val);
                                break;
                            case "lessorequal":
                                And(x => x.High <= val);
                                break;
                            case "greater":
                                And(x => x.High > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.High >= val);
                                break;
                            default:
                                And(x => x.High == val);
                                break;
                        }
						}
						if (rule.field == "LUnit"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LUnit.Contains(rule.value));
						}
						if (rule.field == "Flag1" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Flag1 == boolval);
						}
						if (rule.field == "Flag2" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Flag2 == boolval);
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
