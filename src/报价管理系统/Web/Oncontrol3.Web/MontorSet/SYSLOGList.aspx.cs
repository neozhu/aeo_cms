using System;
using System.Collections;
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
using Aim.Portal.Model;

namespace Aim.OnControl.Web
{
    public partial class SYSLOGList : BaseListPage
    {
        private IList<SYSLOG> ents = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            SYSLOG ent = null;
            switch (this.RequestAction)
            {
                case RequestActionEnum.Delete:
                    ent = this.GetTargetData<SYSLOG>();
                    ent.DoDelete();
                    this.SetMessage("删除成功！");
                    break;
                default:
                    if (RequestActionString == "batchdelete")
                    {
                        DoBatchDelete();
                    }
                    else
                    {
                        DoSelect();
                    }
                    break;
            }

        }
        #region 私有方法

        /// <summary>
        /// 查询
        /// </summary>
        private void DoSelect()
        {

            string where = " 1=1 ";
            foreach (var item in SearchCriterion.Searches.Searches)
            {
                switch (item.PropertyName)
                {
                    case "ACTION":
                        if (!string.IsNullOrEmpty(item.Value + "") && item.Value.ToString().Contains("F"))
                        {
                            where += " and  ACTION is null ";

                        }
                        else if (!string.IsNullOrEmpty(item.Value + ""))
                        {
                            where += " and ACTION like '%" + item.Value + "%'  ";
                        }
                        break;
                    case "StartTime":
                        if (!string.IsNullOrEmpty(item.Value + ""))
                        {
                            where += " and  CREATETIME >= to_date('" + item.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
                        }
                        break;
                    case "EndTime":
                        if (!string.IsNullOrEmpty(item.Value + ""))
                        {
                            where += " and  CREATETIME <= to_date('" + item.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
                        }
                        break;
                }
            }
            if (SearchCriterion.Searches.Searches.Count > 0)
            {
                SearchCriterion.Searches.RemoveSearch("ACTION");
                SearchCriterion.Searches.RemoveSearch("StartTime");
                SearchCriterion.Searches.RemoveSearch("EndTime");
            }
			
            SearchCriterion.SetOrder(SYSLOG.Prop_CREATETIME, false);
            ents = SYSLOG.FindAll(SearchCriterion, Expression.Sql(where));
            this.PageState.Add("SYSLOGList", ents);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        [ActiveRecordTransaction]
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");

            if (idList != null && idList.Count > 0)
            {
                SYSLOG.DoBatchDelete(idList.ToArray());
            }
        }

        #endregion
    }
}

