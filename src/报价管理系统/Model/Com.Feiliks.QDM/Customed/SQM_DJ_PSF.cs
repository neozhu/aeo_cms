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
	
namespace Com.Feiliks.QDM
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
	public partial class SQM_DJ_PSF
    {
        #region 成员变量

        #endregion

        #region 成员属性
        
        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            /*if (!this.IsPropertyUnique("UniqueKey"))
            {
                throw new RepeatedKeyException("存在重复的 UniqueKey “" + this.UniqueKey + "”");
            }*/
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void DoSave()
        {
            if (String.IsNullOrEmpty(RID))
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

            //this.CREATEID = Aim.Portal.Web.WebPortalService.CurrentUserInfo.UserID;
            this.CREATEUSER = MODIFYUSER;//前台传过来的工号，modifyuser只是一个数据载体
            this.MODIFYUSER = "";//创建的时候没有更新人
            this.CREATETIME = DateTime.Now;

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

            //this.MODIFYID = Aim.Portal.Web.WebPortalService.CurrentUserInfo.UserID;
            //this.MODIFYUSER = Aim.Portal.Web.WebPortalService.CurrentUserInfo.LoginName;
            this.MODIFYTIME = DateTime.Now;

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
        /// 批量删除操作
        /// </summary>
        public static void DoBatchDelete(params object[] args)
        {
			SQM_DJ_PSF[] tents = SQM_DJ_PSF.FindAll(Expression.In("RID", args));

			foreach (SQM_DJ_PSF tent in tents)
			{
				tent.DoDelete();
			}
        }
        
        #endregion

    } // SQM_DJ_PSF
}


