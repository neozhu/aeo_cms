using System;
using System.Linq;

using Aim.Data;
using Aim.Utilities;
using Aim;

namespace CRM.Web.CommonPages.Data
{
    public partial class CustomerData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["cmd"] != null && this.Request["cmd"] == "GetCustomer")
            {
                string input = this.Request["query"];
                //if (input != "" && !Regex.IsMatch(input, @"^[\u4e00-\u9fa5]+$"))
                if (true)
                {
                    string sql = Request["selsql"];
                    string selColName = Request["selColName"];
                    string SelData = Request["SelData"];
                    //string db = System.Configuration.ConfigurationManager.AppSettings["PurchaseDB"];

                    string where = "";
                    if (selColName.Contains(','))
                    {
                        if (!sql.Contains("where"))
                        {
                            where = sql + " where ";
                        }
                        else
                        {
                            where = sql + " and ";
                        }
                        int i = 0;
                        where += " ( ";
                        foreach (string streach in selColName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (i == 0)
                            {
                                where += streach + " like '%" + input + "%' ";
                            }
                            else
                            {
                                where += " or " + streach + " like '%" + input + "%' ";
                            }
                            i++;
                        }
                        where += " ) ";
                    }
                    else
                    {
                        if (!sql.Contains("where"))
                        {
                            where = sql + " where " + selColName + " like '%" + input + "%' ";
                        }
                        else
                        {
                            where = sql + " and " + selColName + " like '%" + input + "%' ";
                        }
                    }
                    Response.Write("{success:true,rows:" + JsonHelper.GetJsonString(DataHelper.QueryDictList(where)) + "}");
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
            string[,] hz = Tool.GetHanziScope(pinyinIndex);
            string whereString = "(";
            for (int i = 0; i < hz.GetLength(0); i++)
            {
                whereString += "(SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) >= '" + hz[i, 0] + "' AND SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) <= '" + hz[i, 1] + "') AND ";
            }
            if (whereString.Substring(whereString.Length - 4, 4) == "AND ")
                return whereString.Substring(0, whereString.Length - 4) + ")";
            else
                return "(1=1)";
        }
    }
}
