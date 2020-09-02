using Aim.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    public class MailHelper
    {
        /// <summary>
        /// 按照webconfig中配置的smtp发送邮件
        /// </summary>
        /// <param name="mailto"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public static void SendMail(string mailto,string title, string body)
        {
            
            string Server = System.Configuration.ConfigurationManager.AppSettings["mailServer"];
            string Sender = System.Configuration.ConfigurationManager.AppSettings["mailSender"];
            string Account = System.Configuration.ConfigurationManager.AppSettings["mailAccount"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["mailPassword"];
            SendWebMail(Sender, mailto, title,
                body, Account, Password, Server);
        }
        public static void SendWebMail(string mailSenderAddress, string mailto, string title, string body, string mailAccount, string mailPass, string mailServer)
        {
            SysEvent evt = new SysEvent();
            evt.AuthName = mailSenderAddress;
            evt.ModuleName = title.Length > 50 ? title.Substring(0, 50) : title;
            evt.DateTime = DateTime.Now;
            evt.Record = body;
            evt.ApplicationName = mailto.Length > 50 ? mailto.Substring(0, 50) : mailto;
            evt.Type = "MailSuccess";
            try
            {
                //实例化MailMessage对象 
                System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
                //定义邮件的发送地址 , 可以随便填一个不存在的地址
                mail.From = mailSenderAddress;
                //定义邮件的接收地址 
                //设置以分号分隔的收件人电子邮件地址列表 
                mail.To = mailto;
                //定义邮件的暗送地址 
                //设置以分号分隔的电子邮件地址列表 
                //mail.Bcc="ddd@sina.com"; 
                //定义邮件的抄送地址 
                //设置以分号分隔的电子邮件地址列表 
                //mail.Cc="ddd@x.cn;ddd@eyou.com 
                //定义邮件的主题 
                mail.Subject = title;
                //设置电子邮件正文的内容类型 
                //在这里我们以HTML的格式发送 
                mail.BodyFormat = System.Web.Mail.MailFormat.Html;
                //设置电子邮件的正文 
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //SMTP服务器 ，因为用的是本机架设的，所以写127.0.0.1 , 如果连接的是其他服务器的话，像163邮箱，要写smpt.163.com
                System.Web.Mail.SmtpMail.SmtpServer = mailServer;
                //说是许多SMTP服务器都需要身份验证 ，防止垃圾邮件，好像叫做扩展smpt协议什么的。
                //但这里连接的是自己的smpt服务器，简单的smpt，所以也没有什么验证了。
                //至于从本机的SMPT服务器再把邮件发送到163或者其他邮箱 的时候要不要验证就不知道了， 实测时邮件时可以发到
                //@163.com , @eyou.com,@x.cn的，也不用什么验证。
                //验证 
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //登陆名 
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", mailAccount);
                //登陆密码 
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", mailPass);
                //发送 
                System.Web.Mail.SmtpMail.Send(mail);
            }
            catch (Exception ex)
            {
                evt.Type = "MailError";
                evt.Record = evt.Record + "{ERROR:}" + ex.Message + ex.StackTrace;
            }
            evt.Save();
        }
    }
}