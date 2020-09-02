using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Portal.Web.UI;

using Aim.Data;
using Aim.Portal.Web;
using Aim.Portal.Model;

namespace Aim.OnControl.Web.MontorSet
{
    public partial class RecordDetail : BasePage
    {
        private SYSLOG ent = null;
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty; // 对象id
        string type = String.Empty; // 对象类型

        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            switch (this.RequestAction)
            {
                default:
                    DoSelect();
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void DoSelect()
        {
            if (!String.IsNullOrEmpty(id))
            {
                ent = SYSLOG.Find(id);
            }
            this.SetFormData(ent);
        }

    }
}