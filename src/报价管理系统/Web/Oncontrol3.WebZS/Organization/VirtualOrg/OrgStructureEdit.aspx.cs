using System;
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
using CRM.Model;

namespace CRM.Web.Virtual
{
    public partial class OrgStructureEdit : BasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id

        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");

            CRM_VIRTUALSYSGROUP ent = null;

            switch (this.RequestAction)
            {
                case RequestActionEnum.Update:
                    ent = this.GetMergedData<CRM_VIRTUALSYSGROUP>();
                    ent.DoUpdate();
                    this.SetMessage("修改成功！");
                    return;
                default:
                    if (RequestActionString == "createsub")
                    {
                        ent = this.GetPostedData<CRM_VIRTUALSYSGROUP>();
                        if (!CRM_VIRTUALSYSGROUP.Exists("Name=? and Type=? and ParentID = ?", ent.NAME, ent.TYPE, id))
                        {
                            ent.ParentID = id;
                            ent.IsLeaf = 1;
                            CRM_VIRTUALSYSGROUP pent = CRM_VIRTUALSYSGROUP.Find(id);
                            ent.PATH = pent.GROUPID + "." + pent.PATH;
                            ent.PATHLEVEL = pent.PATHLEVEL + 1;
                            pent.IsLeaf = 0;
                            pent.DoUpdate();
                            ent.DoSave();
                        }
                        else
                        {
                            PageState.Add("error", "名称已存在！");
                        }
                        return;
                    }
                    break;
            }

            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = CRM_VIRTUALSYSGROUP.Find(id);
                }

                this.SetFormData(ent);
            }
            else
            {
                PageState.Add("CREATENAME", UserInfo.Name);
                PageState.Add("CREATETIME", DateTime.Now);
            }

            //初始化名称选择
            string type = RequestData.Get<string>("type");
            if (string.IsNullOrEmpty(type) && ent != null)
            {
                type = Convert.ToInt32(ent.TYPE).ToString();
            }
            /*litname.Text = "<input id='NAME' name='NAME' class='validate[required]' />";
            if (type == "3")
            {
                litname.Text = @"<select id='NAME' aimctrl='select' name='NAME' enum='VirtualRole' style='width: 100%;'
                            class='aim-input-select validate[required]'>
                        </select>";
                PageState.Add("VirtualRole", SysEnumeration.GetEnumDict("VirtualRole"));
            }*/
        }
    }
}

