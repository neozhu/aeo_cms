using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity;

namespace Aim.Portal
{
    public class CodeGeneratorContext : VelocityContext
    {
        public const string DATE_TIME = "Date";
        public const string PORTAL_USER_INFO = "UserInfo";
        public const string PORTAL_SYSTEM_CONTEXT = "SystemContext";
        public const string PORTAL_USER_CONTEXT = "UserContext";
        public const string PORTAL_CODE_GENERATOR_SERVICE = "Service";

        #region 构造函数

        public CodeGeneratorContext()
        {
            this.Put(DATE_TIME, DateTime.Now);
            this.Put(PORTAL_USER_INFO, PortalService.CurrentUserInfo);
            this.Put(PORTAL_SYSTEM_CONTEXT, PortalService.SystemContext);
            this.Put(PORTAL_USER_CONTEXT, PortalService.CurrentUserContext);
            this.Put(PORTAL_CODE_GENERATOR_SERVICE, CodeGeneratorService.Instance);
        }

        #endregion
    }
}
