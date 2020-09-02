using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class CommonController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View("Index", "_ContentLayout");
        }

        public ActionResult Lists()
        {
            Dictionary<string, string> dc = ActiveRecordMediator.GetSessionFactoryHolder().GetAllConfigurations()[0].Properties as System.Collections.Generic.Dictionary<string, string>;
            string sqltype = dc.Values.ToList()[1];

            string whereSql = Request["datasql"];
            if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
            {
                if (whereSql.IndexOf("where") > 0)
                    whereSql += " and " + Request.QueryString["searchCol"] + " like '%" + Request.QueryString["Name"] + "%'";
                else
                    whereSql += " where " + Request.QueryString["searchCol"] + " like '%" + Request.QueryString["Name"] + "%'";
            }
            var obj = new { rows = sqltype.IndexOf("SqlClientDriver") >= 0 ? GetPageData(whereSql) : GetPageDataOracle(whereSql), total = SearchCriterion.RecordCount };
            return Content(JsonHelper.GetJsonString(obj));
        }
        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<int>("select count(1) from (" + tempsql + ") t"));
            string order = !string.IsNullOrEmpty(Request.QueryString["sort"]) ? Request.QueryString["sort"] : "CreateTime";
            string asc = !string.IsNullOrEmpty(Request.QueryString["order"]) ? Request.QueryString["order"] : " desc";
            string sql_page = @"With DATASET AS( select A.*,Row_Number() OVER (order by {1} {2}) As RN from ({0}) A) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }

        private IList<EasyDictionary> GetPageDataOracle(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = !string.IsNullOrEmpty(Request.QueryString["sort"]) ? Request.QueryString["sort"] : "CREATETIME";
            string asc = !string.IsNullOrEmpty(Request.QueryString["order"]) ? Request.QueryString["order"] : " desc";
            string sql_page = "With DATASET AS( select \"UserID\",\"Name\",\"LoginName\",ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }

        /// <summary>
        /// 获取后台数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDatas()
        {
            if (!string.IsNullOrEmpty(this.Request["q"]))
            {
                Dictionary<string, string> dc = ActiveRecordMediator.GetSessionFactoryHolder().GetAllConfigurations()[0].Properties as System.Collections.Generic.Dictionary<string, string>;
                string sqltype = dc.Values.ToList()[1];
                string valuefield = this.Request["valueField"];
                string textfield = this.Request["textField"];
                string datasql = this.Request["datasql"];
                if (datasql.ToLower().IndexOf("where") < 0)
                {
                    datasql += " where 1=1 ";
                }
                string where = "";
                if (sqltype.IndexOf("SqlClientDriver") >= 0)
                    where = datasql + " and (" + textfield.ToUpper() + " like '%" + this.Request["q"] + "%' or (" + GetPinyinWhereString(textfield, this.Request["q"]) + "))";
                else
                    where = datasql + " and (" + textfield.ToUpper() + " like '%" + this.Request["q"] + "%' or (" + GetPinyinWhereStringOracleNoFunc(textfield, this.Request["q"]) + "))";

                string content = JsonHelper.GetJsonString(DataHelper.QueryDictList(where));
                return Content(content);
            }
            else
                return Content("");
        }


        public string GetPinyinWhereString(string fieldName, string pinyinIndex)
        {
            string[,] hz = GetHanziScope(pinyinIndex);
            string whereString = "(";
            for (int i = 0; i < hz.GetLength(0); i++)
            {
                //whereString += "(NLSSORT(substr(" + fieldName + ", " + (i + 1) + ", 1), 'NLS_SORT=SCHINESE_PINYIN_M') >= NLSSORT('" + hz[i, 0] + "','NLS_SORT=SCHINESE_PINYIN_M') AND NLSSORT(substr(" + fieldName + ", " + (i + 1) + ", 1), 'NLS_SORT=SCHINESE_PINYIN_M') <= NLSSORT('" + hz[i, 1] + "','NLS_SORT=SCHINESE_PINYIN_M')) AND ";
                whereString += "(SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) >= '" + hz[i, 0] + "' AND SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) <= '" + hz[i, 1] + "') AND ";
            }
            if (whereString.Substring(whereString.Length - 4, 4) == "AND ")
                return whereString.Substring(0, whereString.Length - 4) + ")";
            else
                return "(1=1)";
        }

        public string GetPinyinWhereStringOracleNoFunc(string fieldName, string pinyinIndex)
        {
            string[,] hz = GetHanziScope(pinyinIndex);
            string whereString = "(";
            for (int i = 0; i < hz.GetLength(0); i++)
            {
                whereString += "(NLSSORT(substr(" + fieldName + ", " + (i + 1) + ", 1), 'NLS_SORT=SCHINESE_PINYIN_M') >= NLSSORT('" + hz[i, 0] + "','NLS_SORT=SCHINESE_PINYIN_M') AND NLSSORT(substr(" + fieldName + ", " + (i + 1) + ", 1), 'NLS_SORT=SCHINESE_PINYIN_M') <= NLSSORT('" + hz[i, 1] + "','NLS_SORT=SCHINESE_PINYIN_M')) AND ";
            }
            if (whereString.Substring(whereString.Length - 4, 4) == "AND ")
                return whereString.Substring(0, whereString.Length - 4) + ")";
            else
                return "(1=1)";
        }
        public string[,] GetHanziScope(string pinyinIndex)
        {
            pinyinIndex = pinyinIndex.ToLower();
            string[,] hz = new string[pinyinIndex.Length, 2];
            for (int i = 0; i < pinyinIndex.Length; i++)
            {
                string index = pinyinIndex.Substring(i, 1);
                if (index == "a") { hz[i, 0] = "吖"; hz[i, 1] = "驁"; }
                else if (index == "b") { hz[i, 0] = "八"; hz[i, 1] = "簿"; }
                else if (index == "c") { hz[i, 0] = "嚓"; hz[i, 1] = "錯"; }
                else if (index == "d") { hz[i, 0] = "咑"; hz[i, 1] = "鵽"; }
                else if (index == "e") { hz[i, 0] = "妸"; hz[i, 1] = "樲"; }
                else if (index == "f") { hz[i, 0] = "发"; hz[i, 1] = "猤"; }
                else if (index == "g") { hz[i, 0] = "旮"; hz[i, 1] = "腂"; }
                else if (index == "h") { hz[i, 0] = "妎"; hz[i, 1] = "夻"; }
                else if (index == "j") { hz[i, 0] = "丌"; hz[i, 1] = "攈"; }
                else if (index == "k") { hz[i, 0] = "咔"; hz[i, 1] = "穒"; }
                else if (index == "l") { hz[i, 0] = "垃"; hz[i, 1] = "擽"; }
                else if (index == "m") { hz[i, 0] = "嘸"; hz[i, 1] = "椧"; }
                else if (index == "n") { hz[i, 0] = "拏"; hz[i, 1] = "瘧"; }
                else if (index == "o") { hz[i, 0] = "筽"; hz[i, 1] = "漚"; }
                else if (index == "p") { hz[i, 0] = "妑"; hz[i, 1] = "曝"; }
                else if (index == "q") { hz[i, 0] = "七"; hz[i, 1] = "裠"; }
                else if (index == "r") { hz[i, 0] = "亽"; hz[i, 1] = "鶸"; }
                else if (index == "s") { hz[i, 0] = "仨"; hz[i, 1] = "蜶"; }
                else if (index == "t") { hz[i, 0] = "侤"; hz[i, 1] = "籜"; }
                else if (index == "w") { hz[i, 0] = "屲"; hz[i, 1] = "鶩"; }
                else if (index == "x") { hz[i, 0] = "夕"; hz[i, 1] = "鑂"; }
                else if (index == "y") { hz[i, 0] = "丫"; hz[i, 1] = "韻"; }
                else if (index == "z") { hz[i, 0] = "帀"; hz[i, 1] = "咗"; }
                else { hz[i, 0] = index; hz[i, 1] = index; }
            }
            return hz;
        }
    }
}
