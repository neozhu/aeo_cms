using Com.Feiliks.QDM;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;

namespace Foqus
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SQTrackerAttribute : ActionFilterAttribute
    {
        #region Action 监控
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                SQM_TRACKER sqmtracker = new SQM_TRACKER();
                sqmtracker.ACCOUNT = SQMHelper.getStaffKey();
                sqmtracker.ACCESSDT = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
                sqmtracker.CONTROLLERNAME = filterContext.RouteData.Values["controller"] as string;
                sqmtracker.ACTIONNAME = filterContext.RouteData.Values["action"] as string;
                sqmtracker.CREATETIME = DateTime.Now;
                sqmtracker.CREATEUSER = SQMHelper.getStaffKey();
                sqmtracker.DoCreate();
            }
            catch { };
        }
        #endregion
    }
}
