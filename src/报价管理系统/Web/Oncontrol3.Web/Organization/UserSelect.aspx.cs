using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using NHibernate.Criterion;
using Aim;
using System.Text.RegularExpressions;
namespace CRM.Web
{
    public partial class UserSelect : CRMBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Regex regChina = new Regex("^[^\x00-\xFF]");
            Regex regNum = new Regex("^[0-9]");
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value + ""))
                {
                    if (regNum.IsMatch(item.Value + ""))
                    {
                        SearchCriterion.AddSearch(SysUser.Prop_WorkNo, item.Value);
                    }
                    if (regChina.IsMatch(item.Value + ""))
                    {
                        SearchCriterion.AddSearch(SysUser.Prop_Name, item.Value);
                    }
                }
            }
            IList<SysUser> suEnts = SysUser.FindAll(SearchCriterion);
            PageState.Add("DataList", suEnts);
        }
        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = SearchCriterion.Orders.Count > 0 ? SearchCriterion.Orders[0].PropertyName : "CINDEX";
            string asc = SearchCriterion.Orders.Count <= 0 || SearchCriterion.Orders[0].Ascending ? " desc" : " asc";
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }
    }
}
