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
/// File: HSCodeQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/26 13:40:27
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class HSCodeQuery:QueryObject<HSCode>
   {
		public HSCodeQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "hscode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.hscode.Contains(rule.value));
						}
						if (rule.field == "cn_name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.cn_name.Contains(rule.value));
						}
						if (rule.field == "en_name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.en_name.Contains(rule.value));
						}
						if (rule.field == "g_model"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.g_model.Contains(rule.value));
						}
						if (rule.field == "unit_code"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.unit_code.Contains(rule.value));
						}
						if (rule.field == "unit_name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.unit_name.Contains(rule.value));
						}
						if (rule.field == "unit2_code"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.unit2_code.Contains(rule.value));
						}
						if (rule.field == "unit2_name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.unit2_name.Contains(rule.value));
						}
						if (rule.field == "control_ma"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.control_ma.Contains(rule.value));
						}
						if (rule.field == "ciq_ma"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ciq_ma.Contains(rule.value));
						}
						if (rule.field == "im_low_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.im_low_rate.Contains(rule.value));
						}
						if (rule.field == "im_normal_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.im_normal_rate.Contains(rule.value));
						}
						if (rule.field == "im_temp_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.im_temp_rate.Contains(rule.value));
						}
						if (rule.field == "im_tax_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.im_tax_rate.Contains(rule.value));
						}
						if (rule.field == "im_consume_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.im_consume_rate.Contains(rule.value));
						}
						if (rule.field == "ex_return_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ex_return_rate.Contains(rule.value));
						}
						if (rule.field == "ex_normal_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ex_normal_rate.Contains(rule.value));
						}
						if (rule.field == "ex_temp_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ex_temp_rate.Contains(rule.value));
						}
						if (rule.field == "ex_special_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ex_special_rate.Contains(rule.value));
						}
						if (rule.field == "ex_tax_rate"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ex_tax_rate.Contains(rule.value));
						}
						if (rule.field == "remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.remark.Contains(rule.value));
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
