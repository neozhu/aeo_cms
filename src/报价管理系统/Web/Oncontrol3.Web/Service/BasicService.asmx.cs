using Aim.Data;
using NHibernate.Criterion;
using OnControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Oncontrol3.Web.Service
{
    /// <summary>
    /// BasicService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BasicService : System.Web.Services.WebService
    {

        [WebMethod]
        public string CustomerInfo(P_CUSTOMER cus)
        {
            try
            {
                if (P_CUSTOMER.FindAll(Expression.Eq(P_CUSTOMER.Prop_STORERKEY, cus.STORERKEY)).Length > 0)
                {
                    P_CUSTOMER target = P_CUSTOMER.FindAll(Expression.Eq(P_CUSTOMER.Prop_STORERKEY, cus.STORERKEY))[0];
                    target = DataHelper.MergeData(target, cus);
                    target.LASTMODIFIEDDATE = DateTime.Now;
                    target.Update();
                }
                else
                {
                    cus.Create();
                }
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
            return "Success";
        }
    }
}
