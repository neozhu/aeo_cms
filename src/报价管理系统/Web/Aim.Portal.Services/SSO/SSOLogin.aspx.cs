using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Plat.Model;

namespace Aim.Portal.Services.SSO
{
    public partial class SSOLogin : System.Web.UI.Page
    {
        //http://IP:PORT/path/?staffid=123&nonce=987654&signature=ABCDEDF&system=PM

        //staffid	员工id
        //nonce	随机数
        //signature	签名
        //system	系统标识

        //签名算法
        //MD5(staffid+nonce+system)

        //MD5("123987654PM")
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(JsonHelper.GetJsonString(SSO_SYSTEM.FindAll()));
            Response.End();
        }
    }
}