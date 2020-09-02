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

namespace Aim.Portal.Web.CommonPages
{
    public partial class GrpSelView : BaseListPage
    {
        #region 变量

        string op = String.Empty;
        string id = String.Empty;
        string cid = String.Empty;   // 类型标识
        string type = String.Empty; // 查询类型
        string ctype = String.Empty; // 分类类型

        private IList<SysGroup> ents = new List<SysGroup>();

        #endregion

        #region 构造函数

        public GrpSelView()
        {
            IsCheckLogon = false;
            SearchCriterion.CurrentPageIndex = 1;
            SearchCriterion.PageSize = 100; // 一次最多显示100个角色
        }

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            // 类型标识
            cid = RequestData.Get<string>("cid", String.Empty);
            type = RequestData.Get<string>("type", String.Empty).ToLower();
            ctype = RequestData.Get<string>("ctype", "role").ToLower();

            string CompanyId = RequestData.Get<string>("CompanyId");
            string ParentId = RequestData.Get<string>("ParentId");

            if (!IsAsyncRequest)
            {
                SysGroup[] typeList = null;
                if (!string.IsNullOrEmpty(CompanyId))
                {
                    typeList = SysGroup.FindAllByProperties("GroupID", CompanyId);
                    typeList[0].ParentID = "";
                }
                else if (!string.IsNullOrEmpty(ParentId))
                {
                    typeList = SysGroup.FindAllByProperties("GroupID", ParentId);
                }
                else
                {
                    typeList = SysGroup.FindAllByProperties("GroupID", "1001");
                    if (typeList.Length == 0)
                    {
                        typeList = SysGroup.FindAllByProperties("GroupID", "7368C6F5-608F-4BA4-B810-1BA2448CDF57");
                    }
                }
                this.PageState.Add("DtList", typeList);
            }
            else
            {
                switch (this.RequestAction)
                {
                    case RequestActionEnum.Custom:
                        if (RequestActionString == "querychildren")
                        {
                            string showrole = RequestData.Get<string>("showrole");
                            if (showrole == "true")
                            {
                                ents = SysGroup.FindAll("FROM SysGroup as ent WHERE ent.ParentID = ? and Status=1 order by SortIndex ", id);
                            }
                            else
                            {
                                ents = SysGroup.FindAll("FROM SysGroup as ent WHERE ent.ParentID = ? and Status=1 and ent.Type=2 order by SortIndex ", id);
                            }
                            this.PageState.Add("DtList", ents);
                        }
                        break;
                }
            }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
