using Aim;
using Aim.Portal;
using Aim.Portal.Model;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (PortalService.CurrentUserInfo != null)
            {
                string userName = PortalService.CurrentUserInfo.Name;
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult SSO_TEST()
        {

            string U = Request.QueryString["u"];
            string TS = Request.QueryString["ts"];
            string N = Request.QueryString["n"];
            string S = Request.QueryString["s"];
            
            int timespan = 600;

            SSOHelper sso = new SSOHelper();

            string Token = sso.GetToken("_TM");
            this.ViewBag.Status = sso.VilidateUrl(U, TS, N, S, Token, timespan);
            //sso.VilidateUrl("012010080", "20170908165700", "3212", "3EF038D41A394B5E0ECB467969142D9F", Token, timespan);
            return View();
        }
    }
}
