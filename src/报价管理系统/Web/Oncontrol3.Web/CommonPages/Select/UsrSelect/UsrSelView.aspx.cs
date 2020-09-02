using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Utilities;
using System.Data;

namespace Aim.Portal.Web.CommonPages
{
    public partial class UsrSelView : BaseListPage
    {
        #region 变量

        string op = String.Empty;
        string id = String.Empty;   // 对象id
        string type = String.Empty; // 查询类型
        string ctype = String.Empty; // 分类类型

        private IList<SysUser> users = new List<SysUser>();

        #endregion

        #region 构造函数

        public UsrSelView()
        {
            IsCheckLogon = false;

            SearchCriterion.CurrentPageIndex = 1;
            SearchCriterion.PageSize = 100; // 一次最多显示100人
        }

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            type = RequestData.Get<string>("type", String.Empty).ToLower();
            ctype = RequestData.Get<string>("ctype", "user").ToLower();

            string deptsx = "";
            string DeptId = RequestData.Get<string>("DeptId");
            if (!string.IsNullOrEmpty(DeptId))
            {
                deptsx = " Pk_deptdoc='" + DeptId + "' ";
            }

            if (!IsAsyncRequest)
                SearchCriterion.PageSize = 40;
            if (ctype == "group")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ICriterion cirt = null;

                    if (type == "gtype")
                    {
                        cirt = Expression.Sql(" and Status='1' and UserID IN (SELECT UserID FROM SysUserGroup WHERE GroupID IN (SELECT GroupID FROM SysGroup WHERE Type = ?))", id, NHibernateUtil.String);
                    }
                    else
                    {
                        // 应该同时获取子组用户
                        cirt = Expression.Sql(" and Status='1' and UserID IN (SELECT UserID FROM SysUserGroup WHERE GroupID IN (SELECT GroupID FROM SysGroup WHERE GroupID = ? OR Path LIKE '%" + id + "%'))",
                            id, NHibernateUtil.String);
                    }
                    SearchCriterion.AutoOrder = false;
                    SearchCriterion.SetOrder(SysUser.Prop_WorkNo);
                    users = SysUserRule.FindAll(SearchCriterion, cirt);
                    this.PageState.Add("UsrList", users);
                }
            }
            else
            {
                SearchCriterion.AutoOrder = false;

                string dName = SearchCriterion.GetSearchValue<string>("Name");
                string workNo = SearchCriterion.GetSearchValue<string>("WorkNo");
                
                string where = " where Status='1' ";
                if (dName != null && dName.Trim() != "")
                {
                    where += " and Name like '%" + dName + "%' ";
                }

                if (workNo != null && workNo.Trim() != "")
                {
                    where += " and WorkNo like '%" + workNo + "%' ";
                }

                if (!string.IsNullOrEmpty(deptsx))
                {
                    where += " and " + deptsx;
                }

                string sql = "select UserID as \"UserID\",Name as \"Name\",LoginName as \"LoginName\",WorkNo,Status as \"Status\",Email as \"Email\",Remark as \"Remark\",CreateDate as \"CreateDate\" from SysUser " + where;
                PageState.Add("UsrList", GetPageData(sql));
            }

        }
        #endregion

        private IList<EasyDictionary> GetPageData(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = SearchCriterion.Orders.Count > 0 ? SearchCriterion.Orders[0].PropertyName : " WorkNo ";
            string asc = SearchCriterion.Orders.Count <= 0 || SearchCriterion.Orders[0].Ascending ? " asc" : " desc";
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }

    }
}
