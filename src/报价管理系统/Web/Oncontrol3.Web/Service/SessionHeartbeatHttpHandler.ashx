<%@ WebHandler Language="C#" Class="SessionHeartbeatHttpHandler" %>
using System;
using System.Web;
using System.Web.SessionState;

public class SessionHeartbeatHttpHandler : IHttpHandler, IRequiresSessionState
{
    public bool IsReusable { get { return false; } }

    public void ProcessRequest(HttpContext context)
    {
        context.Session["Heartbeat"] = DateTime.Now;
        System.Web.Security.FormsAuthentication.SetAuthCookie(Oncontrol3.Web.Helpers.SQMHelper.getStaffKey(), false);
        context.Response.Write("{Heartbeat:'" + DateTime.Now.ToString("yyyyMMddHHmmss") + "'}");
    }
}