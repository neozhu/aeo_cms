using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aim;
using Aim.Data;
using Aim.Portal.Web.UI;
using OnControl.Model;
using System.Data;

namespace CRM.Web
{
    public partial class BusinessUserSelect : CRMListPage
    {
        public BusinessUserSelect()
        {
            IsCheckLogon = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Ops = RequestData.Get<string>("Ops");
            string FilterWhere = " where 1=1 ";

            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (item.PropertyName == "USERNAME") { 
                    FilterWhere +=" and t.NAME LIKE '%" + item.Value + "%'";
                }
                else if (item.Value + "" != "")
                {
                    FilterWhere += " and " + item.PropertyName + " like '%" + item.Value + "%' ";
                }
                
            }
            string sql = @"select * from (
select NAME,WORKNO,USERID from  sysuser where pk_gw in ( select roleid from sysrole where name in ('大客户销售','大客户经理', '销售经理','销售代表','客户经理','销售专员','销售管理专员','科长','经理','副经理','总经理','助理总经理','总裁','总监','副总经理')  )
or  workno = '012003028' or workno = '012008066' or  workno = '202011027' or  workno = 'A0909008' or  workno = '012013903' or workno='012012103' or  workno = '012006039' 
) t " + FilterWhere;
            this.PageState.Add("dt", GetPageData(sql));
        }


        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = SearchCriterion.Orders.Count > 0 ? SearchCriterion.Orders[0].PropertyName : "workno";
            string asc = SearchCriterion.Orders.Count <= 0 || !SearchCriterion.Orders[0].Ascending ? " desc" : " asc";
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }
    }
}

