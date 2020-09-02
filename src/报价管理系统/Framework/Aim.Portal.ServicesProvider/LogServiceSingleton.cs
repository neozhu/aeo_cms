using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aim.Portal.ServicesProvider
{
    public class LogServiceSingleton
    {
        private static LogService.LogServiceSoapClient _instance = null; 
        private static readonly object _synObject = new object();
        /// <summary>
        ///单例
        /// </summary>
        public static LogService.LogServiceSoapClient Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_synObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new LogService.LogServiceSoapClient();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
