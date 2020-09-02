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
namespace Aim.Portal.Web.EPC.Procurement
{
    public partial class ProcTrack : BasePage
    {
        public ProcTrack()
        {
            IsCheckLogon = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAsyncRequest)
            {
                if (this.Request.QueryString["enumkey"] != null)
                {
                    string sql = "select * from SysEnumeration where ParentId=(select EnumerationID from SysEnumeration where Code='" + this.Request.QueryString["enumkey"] + "') order by SortIndex,CreatedDate";
                    IList<EasyDictionary> dicts = DataHelper.QueryDictList(sql);
                    this.PageState.Add("FlowEnum", dicts);
                }
            }
        }
    }
}
