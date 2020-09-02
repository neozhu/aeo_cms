using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Oncontrol3.Web.Models;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Security;
using NHibernate.Criterion;
using Oncontrol3.Web;
using System.Security.Principal;
using System.Runtime.Remoting.Contexts;

namespace Oncontrol3.Web.Controllers
{
    [AuthorLogin]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                //{
                string sid = PortalService.AuthUser(model.UserName, model.Password, false);
                if (!String.IsNullOrEmpty(sid))
                {
                    string url = FormsAuthentication.GetRedirectUrl(model.UserName, true);
                    return RedirectToLocal(Request["ReturnUrl"] == "" ? url : Request["ReturnUrl"]);
                }
                //}
            }
            catch (Exception ex)
            {
                // 如果我们进行到这一步时某个地方出错，则重新显示表单
                ModelState.AddModelError("", "提供的用户名或密码不正确。");
            }
            return View(model);
        }
        /// <summary>
        /// SSO验证通过后,内部绕过密码模拟系统登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SSOLogin()
        {
            string returnUrl = Request["ReturnUrl"] + "";
            string RID = Request["RID"] + "";
            if (!string.IsNullOrEmpty(RID))
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["SSO_TOKEN_OA"];
                string hash = FormsAuthentication.HashPasswordForStoringInConfigFile((RID + token), "MD5").ToUpper();
                SSOService login = new SSOService();
                login.Url = System.Configuration.ConfigurationManager.AppSettings["SSO_Service_URL"];
                SSO_LOGIN user = login.GetLoginInfo(hash);
                if (user == null)
                {
                    return View("Login");
                }
                else
                {

                    

                    FormsAuthentication.SetAuthCookie(user.STAFFKEY.ToString(), false);
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        string strLeft = "";
                        string strRight = "";
                        string contorlName = "";
                        string funcName = "";
                        if (returnUrl.Contains("?"))
                        {
                            strLeft = returnUrl.Substring(1, returnUrl.IndexOf("?") - 1);
                            strRight = returnUrl.Substring(returnUrl.IndexOf("?") + 1);
                            contorlName = strLeft.Substring(0, strLeft.IndexOf("/"));
                            funcName = strLeft.Substring(strLeft.IndexOf("/") + 1);
                            //string para = strRight.Substring(0, strRight.IndexOf("="));
                            return RedirectToAction(funcName, contorlName, new { REPORTKEY = strRight.Substring(strRight.IndexOf("=") + 1) });
                        }
                        else
                        {
                            strLeft = returnUrl.Substring(1);
                            contorlName = strLeft.Substring(0, strLeft.IndexOf("/"));
                            funcName = strLeft.Substring(strLeft.IndexOf("/") + 1);
                            return RedirectToAction(funcName, contorlName);
                        }
                    }
                    else
                    {
                        Response.Redirect("/Home/Index");
                    }
                }
            }
            else
            {
                return View("Login");
            }
            return View("Login");
        }
        [AllowAnonymous]
        public ActionResult BSC_Redirect()
        {
            string userid= User.Identity.Name.ToString();

            #region BSC_SSO_登录
            BSC_SSO_LOGIN.SsoService bsc_sso = new BSC_SSO_LOGIN.SsoService();
            string bsc_so_login_url = System.Configuration.ConfigurationManager.AppSettings["BSC_SSO_Service_URL"];
            string bsc_redirect_url = System.Configuration.ConfigurationManager.AppSettings["BSC_SSO_Redirect_URL"];
            bsc_sso.Url = bsc_so_login_url;

            string jm_userid = bsc_sso.Encrypt(userid); //测试用的("012008007");////调用接口加密
            string url = bsc_redirect_url + jm_userid;
            return Redirect(url);
            #endregion
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            PortalService.Logout();
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            }
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "你的密码已更改。"
                : message == ManageMessageId.SetPasswordSuccess ? "已设置你的邮箱。"
                : "";
            ViewBag.HasLocalPassword = true;// OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            LocalPasswordModel model = new LocalPasswordModel();
            string id = PortalService.CurrentUserInfo.UserID;
            SysUser user = SysUser.Find(id);
            model.Email = user.Email;
            return View(model);
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = true;
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        MD5Encrypt encrypt = new MD5Encrypt();
                        string encryPassword = String.Empty;
                        encryPassword = encrypt.GetMD5FromString(model.OldPassword);
                        // 验证用户
                        SysUser user = SysUserRule.Authenticate(PortalService.CurrentUserInfo.LoginName, encryPassword);
                        if (user != null && !String.IsNullOrEmpty(model.NewPassword))
                        {

                            string newEncryPwd = encrypt.GetMD5FromString(model.NewPassword);
                            user.Password = newEncryPwd;
                            user.Email = model.Email;
                            user.Update();
                            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                        }
                        if (user != null)
                        {
                            user.Email = model.Email;
                            user.Update();
                            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    ModelState.AddModelError("", "当前密码不正确。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }


        //
        // GET: /Account/resetpwd

        [AllowAnonymous]
        public ActionResult resetpwd()
        {
            string username = Request.QueryString["uname"];
            SysUser user = SysUser.FindFirst(Expression.Eq(SysUser.Prop_LoginName, username));
            string mail = user.Email;
            string host = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/Account/Details/" + user.UserID;
            Oncontrol3.Web.Helpers.MailHelper.SendMail(mail, "重置门户系统密码", "您好:" + user.Name + "! 您已申请重置密码,请点击链接地址进行重置(如无法打开请复制地址到浏览器中访问): " + host);
            return Content("邮件已发送成功!");
        }
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            SysUser user = SysUser.Find(id);
            MD5Encrypt encrypt = new MD5Encrypt();
            user.Password = encrypt.GetMD5FromString("000000");
            user.Save();
            return Content("重置成功,您的密码已重置为6个0,请登陆后立即修改密码!");
        }

        #region 帮助程序
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                //OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入其他用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入其他电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        #endregion
    }
}
