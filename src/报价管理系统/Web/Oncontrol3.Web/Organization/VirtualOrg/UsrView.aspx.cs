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
using System.Data;

namespace CRM.Web.Virtual
{
    public partial class UsrView : BaseListPage
    {
        private IList<SysUser> users = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string groupid = RequestData.Get<string>("GroupID");

            if (RequestActionString == "adduserbycc")
            {
                if (!String.IsNullOrEmpty(groupid))
                {
                    IList<string> userIDs = RequestData.GetList<string>("UserIDs");
                    string sql = "begin ";
                    foreach (string uid in userIDs)
                    {
                        sql += "insert into CRM_VIRTUALSYSUSERGROUP (Id,UserId,GroupId,Status,StartTime) values ('" + Guid.NewGuid().ToString() + "','" + uid + "','" + groupid + "','启用',sysdate); ";
                    }
                    sql += "end;";
                    DataHelper.ExecSql(sql);
                }
            }
            else if (RequestActionString == "delgrpuser")
            {
                IList<string> userIDs = RequestData.GetList<string>("UserIDs");
                string delsql = "begin ";
                foreach (string uid in userIDs)
                {
                    delsql += "delete CRM_VIRTUALSYSUSERGROUP where UserId='" + uid + "' and GroupId='" + groupid + "'; ";
                }
                delsql += "end;";
                DataHelper.ExecSql(delsql);
            }
            else if (RequestActionString == "enabled")
            {
                IList<string> userIDs = RequestData.GetList<string>("UserIDs");
                string delsql = "begin ";
                foreach (string uid in userIDs)
                {
                    delsql += "update CRM_VIRTUALSYSUSERGROUP set Status='启用' where UserId='" + uid + "' and GroupId='" + groupid + "'; ";
                }
                delsql += "end;";
                DataHelper.ExecSql(delsql);
            }
            else if (RequestActionString == "disabled")
            {
                IList<string> userIDs = RequestData.GetList<string>("UserIDs");
                string delsql = "begin ";
                foreach (string uid in userIDs)
                {
                    delsql += "update CRM_VIRTUALSYSUSERGROUP set Status='停用',stoptime=sysdate where UserId='" + uid + "' and GroupId='" + groupid + "'; ";
                }
                delsql += "end;";
                DataHelper.ExecSql(delsql);
            }
            else if (!String.IsNullOrEmpty(groupid))
            {
                string where = "";
                if (SearchCriterion.Searches.FTSearches.Count > 0)
                {
                    where += " and (Name like '%" + SearchCriterion.Searches.FTSearches[0].Value + "%' or WorkNo like '%" + SearchCriterion.Searches.FTSearches[0].Value + "%')";
                    SearchCriterion.Searches.FTSearches.Clear();
                }

                //string sql = "select UserID as \"UserID\",Name as \"Name\",LoginName as \"LoginName\",WorkNo as \"WorkNo\",Status as \"Status\",Email as \"Email\",Remark as \"Remark\",CreateDate as \"CreateDate\" from SysUser where UserID in (SELECT UserID FROM CRM_VIRTUALSYSUSERGROUP WHERE GroupID = '" + groupid + "') " + where;
                string sql = "select u.UserID as \"UserID\",Name as \"Name\",LoginName as \"LoginName\",WorkNo as \"WorkNo\",m.Status as \"Status\",Email as \"Email\",STARTTIME,STOPTIME "
                + "from SysUser u inner join CRM_VIRTUALSYSUSERGROUP m ON u.userid=m.userid WHERE groupid='" + groupid + "' " + where;
                DataTable dtUser = DataHelper.QueryDataTable(sql);
                PageState.Add("UsrList", dtUser);
            }
        }
    }
}
