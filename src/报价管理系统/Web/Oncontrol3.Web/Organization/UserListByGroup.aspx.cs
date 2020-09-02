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

namespace CRM.Web
{
    public partial class UserListByGroup : BaseListPage
    {
        string GroupID = String.Empty;   // 部门id
        private IList<SysUser> users = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupID = RequestData.Get<string>("GroupID", String.Empty);
            switch (RequestActionString)
            {
                default:
                    ICriterion cirt = Expression.Sql("UserID IN (SELECT UserID FROM SysUserGroup WHERE GroupID = ?)", GroupID, NHibernateUtil.String);
                    users = SysUserRule.FindAll(SearchCriterion, cirt);
                    PageState.Add("DataList", users);
                    break;
            }
        }
        private void RemoveOldDeptRelations(IList<string> userIDs)
        {

            foreach (string userId in userIDs)
            {
                DataHelper.ExecSql("delete from SysUserGroup where UserID='" + userId + "'");
                SysUser sysUser = SysUser.Find(userId);
                IEnumerable<SysGroup> group = sysUser.RetrieveAllGroup().Where(en => en.Type == 2);
                if (group.Count() > 0)
                {
                    foreach (SysGroup gp in group)
                        gp.User.Remove(sysUser);
                }
            }
        }
    }
}
