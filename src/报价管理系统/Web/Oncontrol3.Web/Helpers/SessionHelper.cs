using Oncontrol3.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Oncontrol3.Web.Helpers
{
    public class DESCrypto
    {
        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标值</param>
        /// <param name="key">密钥</param>
        /// <returns>加密值</returns>
        public static string Encrypt(string targetValue, string key = "rwfld")
        {
            try
            {
                if (string.IsNullOrEmpty(targetValue))
                {
                    return string.Empty;
                }

                var returnValue = new StringBuilder();
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(targetValue);
                // 通过两次哈希密码设置对称算法的初始化向量   
                des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                        (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                            Substring(0, 8), "sha1").Substring(0, 8));
                // 通过两次哈希密码设置算法的机密密钥   
                des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                        (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                            .Substring(0, 8), "md5").Substring(0, 8));
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                foreach (byte b in ms.ToArray())
                {
                    returnValue.AppendFormat("{0:X2}", b);
                }
                return returnValue.ToString();
            }
            catch
            { return ""; }
        }

        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string targetValue, string key = "rwfld")
        {
            try
            {
                if (string.IsNullOrEmpty(targetValue))
                {
                    return string.Empty;
                }
                // 定义DES加密对象
                var des = new DESCryptoServiceProvider();
                int len = targetValue.Length / 2;
                var inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(targetValue.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                // 通过两次哈希密码设置对称算法的初始化向量   
                des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                        (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                            Substring(0, 8), "sha1").Substring(0, 8));
                // 通过两次哈希密码设置算法的机密密钥   
                des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                        (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                            .Substring(0, 8), "md5").Substring(0, 8));
                // 定义内存流
                var ms = new MemoryStream();
                // 定义加密流
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }
    public class SQMHelper
    {
        public static string getStaffKey()
        { 
            string ssk = SessionHelper.GetSessionUser<FLD_QO_USER>().staffkey;
            string csk = CookieHelper.GetCookieValue();

            if (!string.IsNullOrEmpty(ssk))
            {
                return ssk;
            }
            else if (!string.IsNullOrEmpty(ssk) && ssk != csk)
            {
                return "";
            }
            else
            {
                return csk;
            }

        }
    }

    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename = "FLD_QO_SESSION_USER")
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename = "FLD_QO_SESSION_USER")
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = DESCrypto.Decrypt(cookie.Value);
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookievalue, string cookiename = "FLD_QO_SESSION_USER")
        {
            SetCookie(cookievalue, DateTime.Now.AddDays(1.0),cookiename);
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookievalue, DateTime expires, string cookiename = "FLD_QO_SESSION_USER")
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = DESCrypto.Encrypt(cookievalue),
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public class SessionHelper
    {
        private static readonly string SessionUser = "FLD_QO_SESSION_USER";
        public static void AddSessionUser<T>(T user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session.Timeout = 600;
            rq.Session[SessionUser] = user;
        }
        public static T GetSessionUser<T>() where T : FLD_QO_USER
        {
            try
            {
                HttpContext rq = HttpContext.Current;
                //return (T)rq.Session[SessionUser];
                T rtn = (T)rq.Session[SessionUser];
                if (null != rtn)
                {
                    return rtn;
                }
                return (T)new FLD_QO_USER();
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return (T)new FLD_QO_USER(); 
            }
        }

        public static void Clear()
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[SessionUser] = null;
        }
    }
}