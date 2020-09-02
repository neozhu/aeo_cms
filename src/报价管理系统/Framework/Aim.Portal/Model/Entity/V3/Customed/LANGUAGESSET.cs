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
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
    public partial class LANGUAGESSET
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
            if (String.IsNullOrEmpty(ID))
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

            //其他语言设置里也添加
            var EntDic = SysEnumeration.GetEnumDictList("Languages");
            foreach (var item in EntDic)
            {
                LANGUAGESSET LangSet = new LANGUAGESSET();
                // LangSet = this;
                LangSet.COMMENTS = this.COMMENTS;
                LangSet.DATAKEY = this.DATAKEY;
                LangSet.PREFIXCODE = this.PREFIXCODE;
                LangSet.PREFIXURL = this.PREFIXURL;
                LangSet.EXT1 = this.EXT1;
                LangSet.EXT2 = this.EXT2;

                //---
                LangSet.CREATEID = this.UserInfo.UserID;
                LangSet.CREATENAME = this.UserInfo.Name;
                LangSet.CREATETIME = DateTime.Now;

                LangSet.DATAVAL = (this.LANGCODE == item.Value) ? this.DATAVAL : null;
                LangSet.MIXDATAKEY = this.MIXDATAKEY.Replace(this.LANGCODE, item.Value);
                LangSet.LANGCODE = item.Value;
                LangSet.CreateAndFlush();
            }

            // 事务开始
            //  this.CreateAndFlush();
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();


            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            //  t.prefixurl||'_'||t.prefixcode
            LANGUAGESSET[] Ents = LANGUAGESSET.FindAll(
                Expression.Sql("MIXDATAKEY like '%" + (this.PREFIXURL + "_" + this.PREFIXCODE) + "' "));
            foreach (var item in Ents)
            {
                item.Delete();
            }
            this.Delete();
        }

        #endregion

        #region 静态成员

        /// <summary>
        /// 批量删除操作
        /// </summary>
        public static void DoBatchDelete(params object[] args)
        {
            LANGUAGESSET[] tents = LANGUAGESSET.FindAll(Expression.In("ID", args));

            foreach (LANGUAGESSET tent in tents)
            {
                tent.DoDelete();
            }
        }

        #endregion

    } // LANGUAGESSET
}


