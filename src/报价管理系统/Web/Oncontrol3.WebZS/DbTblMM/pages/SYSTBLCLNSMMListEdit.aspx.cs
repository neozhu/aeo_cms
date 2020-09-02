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
    public partial class SYSTBLCLNSMMListEdit : BasePage
    {
        #region 变量

        private IList<SYSTBLCLNSMM> ents = null;

        #endregion

        #region 构造函数

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            SYSTBLCLNSMM ent = null;
            switch (this.RequestAction)
            {
                case RequestActionEnum.Delete:
                    ent = this.GetTargetData<SYSTBLCLNSMM>();
                    ent.DoDelete();
                    this.SetMessage("删除成功！");
                    break;
                default:
                    if (RequestActionString == "batchdelete")
                    {
                        DoBatchDelete();
                    }
                    else if (RequestActionString == "batchsave")
                    {
                        DoBatchSave();
                    }
                    else if (RequestActionString == "afterEdit")
                    {
                        string recJson = RequestData.Get("recJson") + "";
                        SYSTBLCLNSMM TblEnt = JsonHelper.GetObject<SYSTBLCLNSMM>(recJson) as SYSTBLCLNSMM;
                        if (TblEnt != null)
                        {
                            TblEnt.Update();
                            this.PageState.Add("state", 1);
                        }
                    }
                    else
                    {
                        DoSelect();
                    }
                    break;
            }

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 查询
        /// </summary>
        private void DoSelect()
        {
            string reftblkey = RequestData.Get("reftblkey") + "";
            SearchCriterion.AddSearch(SYSTBLCLNSMM.Prop_REFTBLKEY, reftblkey);
            ents = SYSTBLCLNSMM.FindAll(SearchCriterion);
            this.PageState.Add("SYSTBLCLNSMMList", ents);

        }

        /// <summary>
        /// 批量保存
        /// </summary>
        [ActiveRecordTransaction]
        private void DoBatchSave()
        {
            IList<string> entStrList = RequestData.GetList<string>("data");

            if (entStrList != null && entStrList.Count > 0)
            {
                IList<SYSTBLCLNSMM> ents = entStrList.Select(tent => JsonHelper.GetObject<SYSTBLCLNSMM>(tent) as SYSTBLCLNSMM).ToList();

                foreach (SYSTBLCLNSMM ent in ents)
                {
                    if (ent != null)
                    {
                        SYSTBLCLNSMM tent = ent;

                        if (String.IsNullOrEmpty(tent.ID))
                        {
                            tent.CREATEID = UserInfo.UserID;
                            tent.CREATENAME = UserInfo.Name;
                        }
                        else
                        {
                            tent = DataHelper.MergeData(SYSTBLCLNSMM.Find(tent.ID), tent);
                        }

                        tent.DoSave();
                    }
                }
            }
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
                SYSTBLCLNSMM.DoBatchDelete(idList.ToArray());
            }
        }

        #endregion
    }
}

