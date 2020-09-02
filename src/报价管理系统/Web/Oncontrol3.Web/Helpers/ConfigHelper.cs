using System;
using System.Configuration;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    /// <summary>
    ///  Config配置文件 公共帮助类
    /// 版本：2.0
    /// <author>
    ///		<name>MR.BiG</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string AppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString().Trim();
            }
            catch { return String.Empty; }
        }
    }
}
