using Aim;
using Aim.Common;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web.UI;
using CRM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace CRM.Web
{
    public partial class SearchAuthTree : BasePage
    {
        string op = String.Empty;
        string id = String.Empty;   // 对象id
        string type = String.Empty; // 查询类型

        protected void Page_Load(object sender, EventArgs e)
        {
            string RoleId = RequestData.Get<string>("roleId");
            id = (RequestData.ContainsKey("id") ? RequestData["id"].ToString() : String.Empty);
            type = (RequestData.ContainsKey("type") ? RequestData["type"].ToString() : String.Empty).ToLower();

            if (RequestActionString == "querydescendant")
            {
                DataTable dtChild = DataHelper.QueryDataTable("SELECT ENUMERATIONID as \"id\",CODE,NAME as \"text\",ISLEAF as \"leaf\" FROM SYSENUMERATION WHERE ParentID='" + id + "' ORDER BY sortindex");

                string jsonString = JsonHelper.GetJsonString(dtChild);
                Response.Write(jsonString);
                Response.End();
            }
            else if (RequestActionString == "savechanges")
            {
                string[] addedName = (RequestData["addedName"] + "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] adds = (RequestData["added"] + "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < adds.Length; i++)
                {
                    new CRM_ROLEPERMIS
                    {
                        ROLEID = RoleId,
                        PERMISID = adds[i],
                        PERMISNAME = addedName[i],
                        TYPE = "查询权限"
                    }.DoCreate();
                }
                string removeIds = RequestData["removed"] + "";
                DataHelper.ExecSql("DELETE CRM_RolePermis WHERE roleid='" + RoleId + "' AND permisId IN ('" + removeIds.Replace(",", "','") + "')");
            }
            else
            {
                DataTable dtPer = DataHelper.QueryDataTable("SELECT ENUMERATIONID as \"id\",CODE,NAME as \"text\",ISLEAF as \"leaf\" FROM SYSENUMERATION WHERE NAME='查询权限' ORDER BY sortindex");
                this.PageState.Add("DtList", dtPer);

                // 获取权限列表
                List<string> strList = new List<string>();
                var dtyy = DataHelper.QueryObjectsList("select permisId from CRM_RolePermis where RoleId='" + RoleId + "'");
                this.PageState.Add("AtList", dtyy);
            }
        }
    }
}
