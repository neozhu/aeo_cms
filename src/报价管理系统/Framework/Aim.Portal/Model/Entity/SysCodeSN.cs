using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
	public partial class SysCodeSN
    {
        #region 成员变量

        #endregion

        #region 成员属性

        [JsonIgnore]
        public SelfIncreaseGenerator.IncreaseType SNIncreaseType
        {
            get
            {
                return ObjectHelper.GetEnum<SelfIncreaseGenerator.IncreaseType>(this.IncreaseType);
            }

            set
            {
                this.IncreaseType = value.ToString();
            }
        }
        
        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复编号
            if (!this.IsPropertyUnique(SysCodeSN.Prop_Code))
            {
                throw new RepeatedKeyException("存在重复的编号 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void DoSave()
        {
            if (String.IsNullOrEmpty(CodeSNID))
            {
                this.DoCreate();
            }
            else
            {
                this.DoUpdate();
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();


            // 事务开始
            this.CreateAndFlush();
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();
                        
            this.LastModifiedDate = DateTime.Now;

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            this.Delete();
        }

        #endregion

        #region 静态成员

        /// <summary>
        /// 由编号获取编码模板
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SysCodeSN Get(string code)
        {
            SysCodeSN[] tmpls = SysCodeSN.FindAllByProperty(SysCodeSN.Prop_Code, code);

            if (tmpls.Length > 0)
            {
                return tmpls[0];
            }

            return null;
        }
        
        /// <summary>
        /// 批量删除操作
        /// </summary>
        public static void DoBatchDelete(params object[] args)
        {
			SysCodeSN[] tents = SysCodeSN.FindAll(Expression.In("CodeSNID", args));

			foreach (SysCodeSN tent in tents)
			{
				tent.DoDelete();
			}
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="code"></param>
        public static string GetCode(string code)
        {
            SysCodeSN tmpl = SysCodeSN.Get(code);

            SelfIncreaseGenerator incGenerator = new SelfIncreaseGenerator(
                tmpl.SNIncreaseType, 
                tmpl.SN, 
                tmpl.Length.GetValueOrDefault() > 0 ? tmpl.Length.Value : tmpl.SN.Length);

            string gsn = incGenerator.Generate();

            // 更新序列为当前序列
            tmpl.SN = gsn;
            tmpl.DoUpdate();

            return gsn;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="code"></param>
        public static string GetStaticCode(string code)
        {
            SysCodeSN tmpl = SysCodeSN.Get(code);

            SelfIncreaseGenerator incGenerator = new SelfIncreaseGenerator(
                tmpl.SNIncreaseType,
                tmpl.SN,
                tmpl.Length.GetValueOrDefault() > 0 ? tmpl.Length.Value : tmpl.SN.Length);

            string gsn = incGenerator.Generate();

            // 更新序列为当前序列
            //tmpl.SN = gsn;
            //tmpl.DoUpdate();

            return gsn;
        }
        #endregion

    } // SysCodeSN
}


