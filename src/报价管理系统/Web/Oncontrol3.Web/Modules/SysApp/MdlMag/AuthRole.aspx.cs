using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;

using Aim.Data;
using Aim.Common;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;


namespace Aim.Portal.Web.Modules.SysApp.MdlMag
{
    public partial class AuthRole : BaseListPage
    {
        private SysRole[] ents = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsAsyncRequest)
            {
                string id = (RequestData.ContainsKey("ID") ? RequestData["ID"].ToString() : String.Empty);
                switch (this.RequestAction)
                {
                    case RequestActionEnum.Custom:
                        if (RequestActionString == "querychildren")
                        {
                            string type = RequestData["Type"].ToString().ToLower();

                            if (RequestData.ContainsKey("Type"))
                            {
                                if (type == "rtype")
                                {
                                    id = (RequestData.ContainsKey("RoleTypeID") ? RequestData["RoleTypeID"].ToString() : String.Empty);
                                    ents = SysRole.FindAll("FROM SysRole as ent WHERE ent.Type = ? and ROWNUM<=200", id);

                                    this.PageState.Add("DtList", ents);
                                }
                            }
                        }
                        break;

                        /*if (RequestActionString == "querychildren")
                        {
                            IList<EasyDictionary> dicts = DataHelper.QueryDictList("select RoleID,pk_deptdoc,'' as ParentID,Name,Code,'Role' as DataType FROM SysRole WHERE pk_deptdoc = '" + id + "' union all select GroupID,'',ParentID,Name,Code,'Dept' as DataType FROM SysGroup WHERE ParentID = '" + id + "' and Type='2' ");
                            //IList<EasyDictionary> dicts = DataHelper.QueryDictList("select GroupID,ParentID,Name,Type as DataType FROM SysGroup WHERE ParentID = '" + id + "'");
                            //ents = SysRole.FindAll("FROM SysRole as ent WHERE pk_deptdoc = '" + id + "'");
                            //if (dicts.Count == 0)
                            //{
                            //    SysGroup[] grpList2 = SysGroup.FindAll("From SysGroup as ent where ParentID='" + id + "' ");
                            //    this.PageState.Add("DtList", grpList2);
                            //}
                            //else
                            //{
                            this.PageState.Add("DtList", dicts);
                            //}
                        }*/
                        break;
                    default:
                        SysGroup[] grpList = SysGroup.FindAll("From SysGroup as ent where nvl(ParentId,'')='' ");

                        this.PageState.Add("DtList", grpList);
                        break;
                }
            }
            else
            {
                SysRoleType[] typeList = SysRoleTypeRule.FindAll();
                this.PageState.Add("DtList", typeList);
            }
        }
    }
}
