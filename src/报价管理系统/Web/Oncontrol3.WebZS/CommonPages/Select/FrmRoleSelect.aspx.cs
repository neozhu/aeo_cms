using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal.Web.UI;
using Aim.Utilities;
using System.Data.SqlClient;
using NHibernate.Criterion;
using Aim;

namespace CRM.Web
{
    public partial class FrmRoleSelect : CRMListPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCriterion.AutoOrder = false;

            string where = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (item.Value + "" != "")
                {
                    where = " where NAME like '%" + item.Value + "%' ";
                }
            }

            string sql = "SELECT SYS_GUID() as ID,NAME FROM (SELECT  DISTINCT NAME FROM sysrole " + where + ") t";
            this.PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }

        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue("select count(1) from (" + sql + ") t"));
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "NAME";
            string asc = search.Orders.Count <= 0 || search.Orders[0].Ascending ? " asc" : " desc";

            string pagsql = @"select * from(
                    select rownum r,t.* from (
                    select * from ({2})  order by  {0} {1}
                    )t)
                    where r between {3} and {4}";

            pagsql = string.Format(pagsql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pagsql);
            return dicts;
        }
    }
}
