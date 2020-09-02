using Aim.Aop;
using Aim.Portal;
using OnControl.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Com.Feiliks.QDM;
using Com.Feiliks.MDM;

namespace Oncontrol3.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            InitApplication();

            LogAttribute.del += WriteLog;
        }

        //private static log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
        //private static readonly ILog applicationInfoLog = LogManager.GetLogger("ApplicationInfoLog");
        private void InitApplication()
        {
            // 初始化PortalService"Aim.WorkFlow",
            PortalService.Initialize(new string[] { "OnControl.Model", "Com.Feiliks.MDM", "Com.Feiliks.QDM" }, typeof(OnControlModelBase<>), typeof(MdmModelBase<>), typeof(QdmModelBase<>));

            log4net.Config.XmlConfigurator.Configure();
            //检验序列号是否有效
            //CheckSystemValid();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        protected void WriteLog(string source, string method, string sourceStr)
        {
            string userId = "";
            string usrName = "";
            if (Aim.Portal.PortalService.CurrentUserInfo != null && Aim.Portal.PortalService.CurrentUserInfo.UserID != null)
            {
                userId = Aim.Portal.PortalService.CurrentUserInfo.UserID;
                usrName = Aim.Portal.PortalService.CurrentUserInfo.Name;
            }
            Aim.Portal.ServicesProvider.LogServiceSingleton.Instance.BeginLogEntity(source, method, sourceStr, userId, usrName, DateTime.Now, null, null);
        }
    }
}