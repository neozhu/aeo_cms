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
/// File: ProductPackQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/7/30 16:19:20
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class ProductPackQuery:QueryObject<ProductPack>
   {
		public ProductPackQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "Height" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Height == val);
                                break;
                            case "notequal":
                                And(x => x.Height != val);
                                break;
                            case "less":
                                And(x => x.Height < val);
                                break;
                            case "lessorequal":
                                And(x => x.Height <= val);
                                break;
                            case "greater":
                                And(x => x.Height > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Height >= val);
                                break;
                            default:
                                And(x => x.Height == val);
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
						if (rule.field == "TwentyQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TwentyQtc == val);
                                break;
                            case "notequal":
                                And(x => x.TwentyQtc != val);
                                break;
                            case "less":
                                And(x => x.TwentyQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.TwentyQtc <= val);
                                break;
                            case "greater":
                                And(x => x.TwentyQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TwentyQtc >= val);
                                break;
                            default:
                                And(x => x.TwentyQtc == val);
                                break;
                        }
						}
						if (rule.field == "FortyQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.FortyQtc == val);
                                break;
                            case "notequal":
                                And(x => x.FortyQtc != val);
                                break;
                            case "less":
                                And(x => x.FortyQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.FortyQtc <= val);
                                break;
                            case "greater":
                                And(x => x.FortyQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.FortyQtc >= val);
                                break;
                            default:
                                And(x => x.FortyQtc == val);
                                break;
                        }
						}
						if (rule.field == "FortyHQQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.FortyHQQtc == val);
                                break;
                            case "notequal":
                                And(x => x.FortyHQQtc != val);
                                break;
                            case "less":
                                And(x => x.FortyHQQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.FortyHQQtc <= val);
                                break;
                            case "greater":
                                And(x => x.FortyHQQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.FortyHQQtc >= val);
                                break;
                            default:
                                And(x => x.FortyHQQtc == val);
                                break;
                        }
						}
						if (rule.field == "Default" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Default == boolval);
						}
						if (rule.field == "ProductId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ProductId == val);
                                break;
                            case "notequal":
                                And(x => x.ProductId != val);
                                break;
                            case "less":
                                And(x => x.ProductId < val);
                                break;
                            case "lessorequal":
                                And(x => x.ProductId <= val);
                                break;
                            case "greater":
                                And(x => x.ProductId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ProductId >= val);
                                break;
                            default:
                                And(x => x.ProductId == val);
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
         public  ProductPackQuery ByProductIdWithfilter(int productid, IEnumerable<filterRule> filters)
         {
            And(x => x.ProductId == productid);
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
						if (rule.field == "Package"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Package == rule.value);
                           } 
                           else
                           {
							And(x => x.Package.Contains(rule.value));
						    }
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
						if (rule.field == "Height" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Height == val);
                                break;
                            case "notequal":
                                And(x => x.Height != val);
                                break;
                            case "less":
                                And(x => x.Height < val);
                                break;
                            case "lessorequal":
                                And(x => x.Height <= val);
                                break;
                            case "greater":
                                And(x => x.Height > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Height >= val);
                                break;
                            default:
                                And(x => x.Height == val);
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
						if (rule.field == "TwentyQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TwentyQtc == val);
                                break;
                            case "notequal":
                                And(x => x.TwentyQtc != val);
                                break;
                            case "less":
                                And(x => x.TwentyQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.TwentyQtc <= val);
                                break;
                            case "greater":
                                And(x => x.TwentyQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TwentyQtc >= val);
                                break;
                            default:
                                And(x => x.TwentyQtc == val);
                                break;
                        }
						}
						if (rule.field == "FortyQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.FortyQtc == val);
                                break;
                            case "notequal":
                                And(x => x.FortyQtc != val);
                                break;
                            case "less":
                                And(x => x.FortyQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.FortyQtc <= val);
                                break;
                            case "greater":
                                And(x => x.FortyQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.FortyQtc >= val);
                                break;
                            default:
                                And(x => x.FortyQtc == val);
                                break;
                        }
						}
						if (rule.field == "FortyHQQtc" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.FortyHQQtc == val);
                                break;
                            case "notequal":
                                And(x => x.FortyHQQtc != val);
                                break;
                            case "less":
                                And(x => x.FortyHQQtc < val);
                                break;
                            case "lessorequal":
                                And(x => x.FortyHQQtc <= val);
                                break;
                            case "greater":
                                And(x => x.FortyHQQtc > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.FortyHQQtc >= val);
                                break;
                            default:
                                And(x => x.FortyHQQtc == val);
                                break;
                        }
						}
						if (rule.field == "Default" && !string.IsNullOrEmpty(rule.value) && rule.value.IsBool())
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.Default == boolval);
						}
						if (rule.field == "ProductId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ProductId == val);
                                break;
                            case "notequal":
                                And(x => x.ProductId != val);
                                break;
                            case "less":
                                And(x => x.ProductId < val);
                                break;
                            case "lessorequal":
                                And(x => x.ProductId <= val);
                                break;
                            case "greater":
                                And(x => x.ProductId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ProductId >= val);
                                break;
                            default:
                                And(x => x.ProductId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
