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
/// File: AeoQuestionQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2020/8/11 9:21:11
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class AeoQuestionQuery:QueryObject<AeoQuestion>
   {
		public AeoQuestionQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "Tpl"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Tpl.Contains(rule.value));
						}
						if (rule.field == "AuthType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AuthType.Contains(rule.value));
						}
						if (rule.field == "Category"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Category.Contains(rule.value));
						}
						if (rule.field == "Description"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Description.Contains(rule.value));
						}
						if (rule.field == "Code"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Code.Contains(rule.value));
						}
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Title.Contains(rule.value));
						}
						if (rule.field == "Short"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Short.Contains(rule.value));
						}
						if (rule.field == "StdDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.StdDescription.Contains(rule.value));
						}
						if (rule.field == "Notes"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Notes.Contains(rule.value));
						}
						if (rule.field == "StdScore" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.StdScore == val);
                                break;
                            case "notequal":
                                And(x => x.StdScore != val);
                                break;
                            case "less":
                                And(x => x.StdScore < val);
                                break;
                            case "lessorequal":
                                And(x => x.StdScore <= val);
                                break;
                            case "greater":
                                And(x => x.StdScore > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.StdScore >= val);
                                break;
                            default:
                                And(x => x.StdScore == val);
                                break;
                        }
						}
						if (rule.field == "Score" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Score == val);
                                break;
                            case "notequal":
                                And(x => x.Score != val);
                                break;
                            case "less":
                                And(x => x.Score < val);
                                break;
                            case "lessorequal":
                                And(x => x.Score <= val);
                                break;
                            case "greater":
                                And(x => x.Score > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Score >= val);
                                break;
                            default:
                                And(x => x.Score == val);
                                break;
                        }
						}
						if (rule.field == "ScoreDescription"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ScoreDescription.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Tester"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Tester.Contains(rule.value));
						}
						if (rule.field == "TestDateTime" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.TestDateTime) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.TestDateTime) <= 0);
						    }
						}
						if (rule.field == "TestNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TestNo.Contains(rule.value));
						}
						if (rule.field == "AeoAuthTestId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.AeoAuthTestId == val);
                                break;
                            case "notequal":
                                And(x => x.AeoAuthTestId != val);
                                break;
                            case "less":
                                And(x => x.AeoAuthTestId < val);
                                break;
                            case "lessorequal":
                                And(x => x.AeoAuthTestId <= val);
                                break;
                            case "greater":
                                And(x => x.AeoAuthTestId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.AeoAuthTestId >= val);
                                break;
                            default:
                                And(x => x.AeoAuthTestId == val);
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
         public  AeoQuestionQuery ByAeoAuthTestIdWithfilter(int aeoauthtestid, IEnumerable<filterRule> filters)
         {
            And(x => x.AeoAuthTestId == aeoauthtestid);
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
						if (rule.field == "Tpl"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Tpl == rule.value);
                           } 
                           else
                           {
							And(x => x.Tpl.Contains(rule.value));
						    }
                        }
						if (rule.field == "AuthType"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.AuthType == rule.value);
                           } 
                           else
                           {
							And(x => x.AuthType.Contains(rule.value));
						    }
                        }
						if (rule.field == "Category"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Category == rule.value);
                           } 
                           else
                           {
							And(x => x.Category.Contains(rule.value));
						    }
                        }
						if (rule.field == "Description"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Description == rule.value);
                           } 
                           else
                           {
							And(x => x.Description.Contains(rule.value));
						    }
                        }
						if (rule.field == "Code"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Code == rule.value);
                           } 
                           else
                           {
							And(x => x.Code.Contains(rule.value));
						    }
                        }
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Title == rule.value);
                           } 
                           else
                           {
							And(x => x.Title.Contains(rule.value));
						    }
                        }
						if (rule.field == "Short"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Short == rule.value);
                           } 
                           else
                           {
							And(x => x.Short.Contains(rule.value));
						    }
                        }
						if (rule.field == "StdDescription"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.StdDescription == rule.value);
                           } 
                           else
                           {
							And(x => x.StdDescription.Contains(rule.value));
						    }
                        }
						if (rule.field == "Notes"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Notes == rule.value);
                           } 
                           else
                           {
							And(x => x.Notes.Contains(rule.value));
						    }
                        }
						if (rule.field == "StdScore" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.StdScore == val);
                                break;
                            case "notequal":
                                And(x => x.StdScore != val);
                                break;
                            case "less":
                                And(x => x.StdScore < val);
                                break;
                            case "lessorequal":
                                And(x => x.StdScore <= val);
                                break;
                            case "greater":
                                And(x => x.StdScore > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.StdScore >= val);
                                break;
                            default:
                                And(x => x.StdScore == val);
                                break;
                        }
						}
						if (rule.field == "Score" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Score == val);
                                break;
                            case "notequal":
                                And(x => x.Score != val);
                                break;
                            case "less":
                                And(x => x.Score < val);
                                break;
                            case "lessorequal":
                                And(x => x.Score <= val);
                                break;
                            case "greater":
                                And(x => x.Score > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Score >= val);
                                break;
                            default:
                                And(x => x.Score == val);
                                break;
                        }
						}
						if (rule.field == "ScoreDescription"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ScoreDescription == rule.value);
                           } 
                           else
                           {
							And(x => x.ScoreDescription.Contains(rule.value));
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
						if (rule.field == "Tester"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Tester == rule.value);
                           } 
                           else
                           {
							And(x => x.Tester.Contains(rule.value));
						    }
                        }
						if (rule.field == "TestDateTime" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.TestDateTime) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.TestDateTime) <= 0);
						    }
                        }
						if (rule.field == "TestNo"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.TestNo == rule.value);
                           } 
                           else
                           {
							And(x => x.TestNo.Contains(rule.value));
						    }
                        }
						if (rule.field == "AeoAuthTestId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.AeoAuthTestId == val);
                                break;
                            case "notequal":
                                And(x => x.AeoAuthTestId != val);
                                break;
                            case "less":
                                And(x => x.AeoAuthTestId < val);
                                break;
                            case "lessorequal":
                                And(x => x.AeoAuthTestId <= val);
                                break;
                            case "greater":
                                And(x => x.AeoAuthTestId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.AeoAuthTestId >= val);
                                break;
                            default:
                                And(x => x.AeoAuthTestId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
