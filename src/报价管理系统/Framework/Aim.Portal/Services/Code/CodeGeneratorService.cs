using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity;
using NVelocity.Context;
using Aim.Portal.Model;

namespace Aim.Portal
{
    public class CodeGeneratorService
    {
        #region 成员

        public readonly static CodeGeneratorService Instance = new CodeGeneratorService();

        #endregion

        #region 构造函数

        protected CodeGeneratorService() { }

        #endregion

        #region 获取上下文信息

        /// <summary>
        /// 获取上下文信息
        /// </summary>
        /// <returns></returns>
        public static CodeGeneratorContext GetContext()
        {
            return new CodeGeneratorContext();
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysEnumeration GetSysEnumeration(string code)
        {
            SysEnumeration sysenum = SysEnumeration.Get(code);

            return sysenum;
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysParameter GetSysParameter(string code)
        {
            SysParameter sysparam = SysParameter.Get(code);

            return sysparam;
        }

        /// <summary>
        /// 获取增量代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetIncreaseCode(string snCode)
        {
            string gsn = SysCodeSN.GetCode(snCode);

            return gsn;
        }

        /// <summary>
        /// 获取增量代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetStaticCode(string snCode)
        {
            string gsn = SysCodeSN.GetStaticCode(snCode);
            return gsn;
        }

        /// <summary>
        /// 获取增量代码
        /// </summary>
        /// <returns></returns>
        public string GetIncreasedCode(string incType, string maxSN, int snLength)
        {
            SelfIncreaseGenerator.IncreaseType inctype = ObjectHelper.GetEnum<SelfIncreaseGenerator.IncreaseType>(incType);

            string sn = SelfIncreaseGenerator.GetIncreasedSN(inctype, maxSN, snLength);

            return sn;
        }

        #endregion
    }
}
