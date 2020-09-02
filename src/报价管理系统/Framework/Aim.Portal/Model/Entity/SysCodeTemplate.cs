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
	public partial class SysCodeTemplate
    {
        #region 成员变量

        #endregion

        #region 成员属性
        
        #endregion

        #region 公共方法

        /// <summary>
        /// 获取相关序列号
        /// </summary>
        public SysCodeSN[] GetCodeSNs()
        {
            SysCodeSN[] sns = SysCodeSN.FindAllByProperty(SysCodeSN.Prop_TemplateID, this.CodeTemplateID);

            return sns;
        }

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复编号
            if (!this.IsPropertyUnique(SysCodeTemplate.Prop_Code))
            {
                throw new RepeatedKeyException("存在重复的编号 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void DoSave()
        {
            if (String.IsNullOrEmpty(CodeTemplateID))
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

            this.CreaterID = PortalService.CurrentUserInfo.UserID;
            this.CreaterName = PortalService.CurrentUserInfo.Name;
            this.CreatedDate = DateTime.Now;

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
        public static SysCodeTemplate Get(string code)
        {
            SysCodeTemplate[] tmpls = SysCodeTemplate.FindAllByProperty(SysCodeTemplate.Prop_Code, code);

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
			SysCodeTemplate[] tents = SysCodeTemplate.FindAll(Expression.In("CodeTemplateID", args));

			foreach (SysCodeTemplate tent in tents)
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
            SysCodeTemplate tmpl = SysCodeTemplate.Get(code);

            NumberGenerator numGenerator = new NumberGenerator
            {
                TemplateString = tmpl.TemplateString
            };

            string gcode = numGenerator.Generate();

            return gcode;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="code"></param>
        public static string GetStaticCode(string code)
        {
            SysCodeTemplate tmpl = SysCodeTemplate.Get(code);

            NumberGenerator numGenerator = new NumberGenerator
            {
                TemplateString = tmpl.TemplateString
            };

            string gcode = numGenerator.Generate();

            return gcode;
        }
        
        #endregion

    } // SysCodeTemplate
}


