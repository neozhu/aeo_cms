using Aim.Data;
using Aim.Portal.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRM.Web.Organization
{
    public static class RoleHelper
    {
        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="ModuleName">模块名</param>
        /// <param name="UserId">人员Id</param>
        /// <param name="type">按钮权限 or 查询权限</param>
        /// <returns>权限集合</returns>
        public static List<string> getPermissions(string ModuleName, string UserId, string type)
        {
            List<string> listPermiss = new List<string>();
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = WebPortalService.CurrentUserInfo.UserID;
            }

            //根据模块名称获取权限
            string sql = "SELECT substr(permisname," + (ModuleName.Length + 2) + ") AS PerMisName FROM CRM_ROLEPERMIS WHERE RoleId=(SELECT RoleId FROM SYSUSERROLE WHERE userid='" + UserId
                + "') AND permisname LIKE '" + ModuleName + "-%' and type='" + type + "'";
            DataTable dtPermis = DataHelper.QueryDataTable(sql);
            foreach (DataRow row in dtPermis.Rows)
            {
                listPermiss.Add(row["PerMisName"] + "");
            }
            return listPermiss;
        }

        public static string getWhere(string ModuleName, string UserId)
        {
            string where = "";
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = WebPortalService.CurrentUserInfo.UserID;
            }

            //根据模块名称获取权限
            string sql = "SELECT substr(permisname," + (ModuleName.Length + 2) + ") AS PerMisName FROM CRM_ROLEPERMIS WHERE RoleId=(SELECT RoleId FROM SYSUSERROLE WHERE userid='" + UserId
                + "') AND permisname LIKE '" + ModuleName + "-%' and type='查询权限'";
            DataTable dtPermis = DataHelper.QueryDataTable(sql);
            if (dtPermis.Rows.Count > 0)
            {
                if (dtPermis.Rows[0]["PerMisName"] + "" == "本部门")
                {

                }
                else if (dtPermis.Rows[0]["PerMisName"] + "" == "无权限")
                {
                    where = "1=2";
                }
            }
            return where;
        }
    }
}