using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Aim.Portal.Services.Log;
using Aim.Portal.Model;

namespace Aim.Portal.Services
{
    /// <summary>
    /// LogService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class LogService : System.Web.Services.WebService
    {

        [WebMethod]
        public void LogEntity(string entityName, string action, string message, string userId, string userName, DateTime createTime)
        {
            SYSLOG log = new SYSLOG();
            log.TABLEEN = entityName;
            log.ACTION = action;
            log.CONTENT = message;
            log.CREATEID = userId;
            log.CREATENAME = userName;
            log.CREATETIME = createTime;
            AimLog.Instance.LogWrite(log);
        }
        [WebMethod]
        public void LogProperty(string entityName, string propertyName, string oldValue, string newValue, string userId, string userName, DateTime createTime)
        {
            SYSLOG log = new SYSLOG();
            log.TABLEEN = entityName;
            log.COLUMNEN = propertyName;
            log.OLDVALUE = oldValue;
            log.NEWVALUE = newValue;
            log.CREATEID = userId;
            log.CREATENAME = userName;
            log.CREATETIME = createTime;
            AimLog.Instance.LogWrite(log);
        }
    }
}
