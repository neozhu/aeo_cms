using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aim;
using Aim.Data;
using Aim.Portal.Web.UI;
using CRM.Model;
using System.Data;

namespace CRM.Web
{
    public partial class FrmPublicSelect : CRMListPage
    {
        public FrmPublicSelect()
        {
            IsCheckLogon = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCriterion.PageSize = 40;
            string FilterWhere = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (item.Value + "" != "")
                {
                    FilterWhere += " and " + item.PropertyName + " like '%" + item.Value + "%' ";
                }
            }

            string sql = "";
            string gettype = RequestData.Get<string>("gettype");
            string enumcode = RequestData.Get<string>("enumcode");
            if (gettype == "enum")
            {
                sql = "SELECT eNUMERATIONId AS ID,NAME FROM SYSENUMERATION WHERE parentid=(SELECT ENUMERATIONID FROM sysenumeration WHERE CODE='" + enumcode + "') ";
                sql += FilterWhere;
                sql += "order by sortindex";
            }
            else if (gettype == "getjiguan")
            {
                sql = "SELECT 'id'||ci.id as ID,p.name||c.name||ci.name AS NAME from s_City c inner JOIN s_PROVINCE p ON p.id=c.s_PROVINCEid LEFT JOIN s_county ci ON ci.S_CITYID=c.id";
                FilterWhere = FilterWhere.Replace("NAME", "p.name||c.name||ci.name");
                sql += " where 1=1 " + FilterWhere;
            }
            else if (gettype == "getjiguan2")
            {
                sql = "SELECT 'id'||c.id as ID,p.name||c.name AS NAME from s_City c inner JOIN s_PROVINCE p ON p.id=c.s_PROVINCEid";
                FilterWhere = FilterWhere.Replace("NAME", "p.name||c.name");
                sql += " where 1=1 " + FilterWhere;
            }

            if (sql != "")
            {
                this.PageState.Add("dt", GetPageData(sql));
            }
        }

        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = SearchCriterion.Orders.Count > 0 ? SearchCriterion.Orders[0].PropertyName : "NAME";
            string asc = SearchCriterion.Orders.Count <= 0 || !SearchCriterion.Orders[0].Ascending ? " desc" : " asc";
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }
    }
}

