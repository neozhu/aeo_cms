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
    public partial class FrmCustomBaseSelect : CRMListPage
    {
        public FrmCustomBaseSelect()
        {
            IsCheckLogon = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Ops = RequestData.Get<string>("Ops");
            string FilterWhere = " nvl(CANCELLATIONSTATE,' ')=' ' and nvl(BLACKSTATE,' ')=' ' and nvl(ELIMINATION,' ')=' ' ";

            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (item.Value + "" != "")
                {
                    FilterWhere += " and " + item.PropertyName + " like '%" + item.Value + "%' ";
                }
            }

            string sql = @"select ID,CUSTOMERNO,NAME as CNNAME,ENNAME,SIMPLENAME,CASE WHEN nvl(NAME,' ')=' ' THEN enname ELSE NAME END AS NAME,CREATETIME from crm_customerbase where " + FilterWhere;
            this.PageState.Add("dt", GetPageData(sql));
        }


        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = SearchCriterion.Orders.Count > 0 ? SearchCriterion.Orders[0].PropertyName : "CREATETIME";
            string asc = SearchCriterion.Orders.Count <= 0 || !SearchCriterion.Orders[0].Ascending ? " desc" : " asc";
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }
    }
}

