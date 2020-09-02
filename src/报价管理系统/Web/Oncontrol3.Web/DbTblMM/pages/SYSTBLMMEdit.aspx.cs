using System;
using System.Collections;
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
using Aim.Portal.Model;

namespace Aim.OnControl.Web
{
    public partial class SYSTBLMMEdit : BasePage
    {
        #region 变量

        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string type = String.Empty; // 对象类型

        SYSTBLMM ent = null;
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            type = RequestData.Get<string>("type");

            switch (this.RequestAction)
            {
                case RequestActionEnum.Update:
                    ent = this.GetMergedData<SYSTBLMM>();
                    ent.DoUpdate();
                    break;
                case RequestActionEnum.Insert:
                case RequestActionEnum.Create:
                    ent = this.GetPostedData<SYSTBLMM>();
                    ent.DoCreate();

                    break;
                case RequestActionEnum.Delete:
                    ent = this.GetTargetData<SYSTBLMM>();
                    ent.DoDelete();
                    return;
                default:
                    DoSelect();
                    break;
            }
        }

        private void DoSelect()
        {
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = SYSTBLMM.Find(id);
                    var clnEnts = SYSTBLCLNSMM.FindAllByProperty(SYSTBLCLNSMM.Prop_REFTBLKEY, ent.ID);
                    this.PageState.Add("DataList", clnEnts);
                }
                this.SetFormData(ent);
            }
        }

    }
}

