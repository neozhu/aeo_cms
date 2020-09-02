using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Oncontrol3.Web.Helpers
{
    public class SSOHelper
    {


        public bool VilidateUrl(string U, string TS, string N, string S, string Token, int timespan)
        {
            DateTime now_dt = DateTime.Now;
            DateTime firstDateTemp;
            //判断是否可以转换成日期
            try
            {
                firstDateTemp = DateTime.ParseExact(TS, "yyyyMMddHHmmss", new System.Globalization.CultureInfo("zh-CN", true));
            }
            catch
            {
                return false;
            }
            TimeSpan span = now_dt.Subtract(firstDateTemp);

            if ((span.TotalSeconds > timespan) || !string.Equals(S, md5(U + TS + N + Token)))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取Token
        /// </summary>
        /// <param name="_prefixToken"></param>
        /// <returns></returns>
        public string GetToken(string _prefixToken)
        {
            string token = System.Configuration.ConfigurationManager.AppSettings["SSO_TOKEN" + _prefixToken];
            return token;
        }

        public string md5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper();
        }

        //public bool VilidateUrl(string _url, string _prefixToken = "TM_")
        //{
        //    string Tm_Token = GetToken(_prefixToken);
        //    IDictionary<string, string> pList = new Dictionary<string, string>();

        //    string pageURL = _url;

        //    Uri uri = new Uri(pageURL);
        //    string queryString = uri.Query;
        //    NameValueCollection col = GetQueryString(queryString);

        //    string U = col["u"];
        //    string TS = col["ts"];
        //    string N = col["n"];
        //    string S = col["s"];


        //    pList.Add("U", U);
        //    pList.Add("TS", TS);
        //    pList.Add("N", N);
        //    //pList.Add("S", S);


        //    string ReSignature = getSignature(pList, Tm_Token);

        //    //string now_dt = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    //string firstDate = "20170906111500";

        //    DateTime now_dt = DateTime.Now;
        //    DateTime firstDateTemp = DateTime.ParseExact(TS, "yyyyMMddHHmmss", new System.Globalization.CultureInfo("zh-CN", true));
        //    //DateTime secondDateTemp = DateTime.ParseExact(secondDate, "yyyyMMddHHmmss", new System.Globalization.CultureInfo("zh-CN", true));
        //    TimeSpan span = now_dt.Subtract(firstDateTemp);


        //    if ((span.TotalSeconds > 600) || !S.Equals(ReSignature))
        //    {
        //        return false;
        //    }
        //    return true;
        //}



        ///// <summary>
        ///// 将查询字符串解析转换为名值集合.
        ///// </summary>
        ///// <param name="queryString"></param>
        ///// <param name="encoding"></param>
        ///// <param name="isEncoded"></param>
        ///// <returns></returns>
        //public static NameValueCollection GetQueryString(string queryString)
        //{
        //    queryString = queryString.Replace("?", "");
        //    NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
        //    if (!string.IsNullOrEmpty(queryString))
        //    {
        //        int count = queryString.Length;
        //        for (int i = 0; i < count; i++)
        //        {
        //            int startIndex = i;
        //            int index = -1;
        //            while (i < count)
        //            {
        //                char item = queryString[i];
        //                if (item == '=')
        //                {
        //                    if (index < 0)
        //                    {
        //                        index = i;
        //                    }
        //                }
        //                else if (item == '&')
        //                {
        //                    break;
        //                }
        //                i++;
        //            }
        //            string key = null;
        //            string value = null;
        //            if (index >= 0)
        //            {
        //                key = queryString.Substring(startIndex, index - startIndex);
        //                value = queryString.Substring(index + 1, (i - index) - 1);
        //            }
        //            else
        //            {
        //                key = queryString.Substring(startIndex, i - startIndex);
        //            }
        //            result[key] = value;
        //            if ((i == (count - 1)) && (queryString[i] == '&'))
        //            {
        //                result[key] = string.Empty;
        //            }
        //        }
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 计算参数签名(MD5加密)
        ///// </summary>
        ///// <param name="params">请求参数集，所有参数必须已转换为字符串类型</param>
        ///// <param name="secret">签名密钥</param>
        ///// <returns>签名</returns>
        //public static string getSignature(IDictionary<string, string> parameters, string secret)
        //{
        //    // 先将参数以其参数名的字典序升序进行排序
        //    IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
        //    IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

        //    // 遍历排序后的字典，将所有参数按"key=value"格式拼接在一起
        //    StringBuilder basestring = new StringBuilder();
        //    while (iterator.MoveNext())
        //    {
        //        string key = iterator.Current.Key;
        //        string value = iterator.Current.Value;
        //        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
        //        {
        //            basestring.Append(key).Append("=").Append(value);
        //        }
        //    }
        //    basestring.Append(secret);

        //    // 使用MD5对待签名串求签
        //    MD5 md5 = MD5.Create();
        //    byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

        //    // 将MD5输出的二进制结果转换为小写的十六进制
        //    StringBuilder result = new StringBuilder();
        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        string hex = bytes[i].ToString("x");
        //        if (hex.Length == 1)
        //        {
        //            result.Append("0");
        //        }
        //        result.Append(hex);
        //    }

        //    return result.ToString();
        //}
    }
}