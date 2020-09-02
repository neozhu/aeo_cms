using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aim.Portal.Model;

namespace Aim.OnControl.Web.DbTblMM
{
    interface ITableInfo
    {
        /// <summary>
        /// 获取指定表的信息
        /// </summary>
        /// <param name="TblName">表名</param>
        /// <param name="DataBaseName">数据库上下文</param>
        /// <returns>SYSTBLMM</returns>
        SYSTBLMM GetTableInfo(string TblName, string DbNameOrOwner);

        /// <summary>
        /// 获取指定表的字段信息
        /// </summary>
        /// <param name="TblName">表名</param>
        /// <param name="DataBaseName">数据库上下文</param>
        /// <returns></returns>
        List<SYSTBLCLNSMM> GetAllTBLFiledInfo(string TblName, string DbNameOrOwner);

        /// <summary>
        /// 获取所有表的对象
        /// </summary>
        /// <param name="DataBaseName">数据上下文</param>
        /// <returns></returns>
        List<SYSTBLMM> GetAllTableObject(string DbNameOrOwner);

    }

    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DbTypeEnum { oralce, mssql }
}
