using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Utilities;
using System.Data;

namespace CRM.Web.CommonPages.Data
{
    public partial class UserData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["cmd"] != null && this.Request["cmd"] == "GETUSERS")
            {
                if (this.Request["query"] != "")
                {
                    string seltype = Request["seltype"];
                    string where = " (" + GetPinyinWhereString("NAME", this.Request["query"]);
                    where += " or WORKNO like '%" + this.Request["query"] + "%')";
                    where += " and STATUS='1' ";
                    if (seltype == "lizhi")
                    {
                        where = " (" + GetPinyinWhereString("NAME", this.Request["query"]);
                        where += " or WORKNO like '%" + this.Request["query"] + "%') ";
                    }

                    //SysUser[] users = SysUser.FindAll(Expression.Sql(where));
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                    string sql = "select USERID \"UserID\",NAME \"Name\",PHONE \"Phone\",WORKNO \"WorkNo\" from SysUser  where " + where;
                    //where  (((NLSSORT(substr(NAME, 1, 1), 'NLS_SORT=SCHINESE_PINYIN_M') >= NLSSORT(to_char('屲'),'NLS_SORT=SCHINESE_PINYIN_M') AND NLSSORT(substr(NAME, 1, 1), 'NLS_SORT=//SCHINESE_PINYIN_M') <= NLSSORT(to_char('鶩'),'NLS_SORT=SCHINESE_PINYIN_M')) ) or WORKNO like '%w%') and STATUS='1'";
                    //DataSet ds = Oracle.OracleHelper.ExecuteDataset(conStr, CommandType.Text, sql);
                    //Response.Write("{success:true,rows:" + JsonHelper.GetJsonString(DataHelper.DataTableToDictList(ds.Tables[0])) + "}");
                    //DataTable dt = DataHelper.QueryDataTable("select * from SysUser where F_PinYin(Name) like '%" + this.Request["query"] + "%' and Rownum<=20");
                    Response.End();
                }
                else
                {
                    Response.Write("{success:true,rows:[]}");
                    Response.End();
                }
            }
        }
        public string GetPinyinWhereString(string fieldName, string pinyinIndex)
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

        public string GetPinyinWhereStringOracle(string fieldName, string pinyinIndex)
        {
            pinyinIndex = pinyinIndex.ToUpper();
            string whereString = "(";
            for (int i = 0; i < pinyinIndex.Length; i++)
            {
                whereString += "system.F_PINYIN(substr(NAME,1,1))='" + pinyinIndex[i] + "' AND ";
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
