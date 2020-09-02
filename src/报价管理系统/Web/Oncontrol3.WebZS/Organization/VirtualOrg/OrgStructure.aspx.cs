using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;

using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using CRM.Model;
using System.Data;
using Aim;

namespace CRM.Web.Virtual
{
    public partial class OrgStructure : BaseListPage
    {
        private CRM_VIRTUALSYSGROUP[] ents = null;
        string id = String.Empty;   // 对象id
        IList<string> ids = null;   // 节点列表
        IList<string> pids = null;   // 父节点列表   
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            ids = RequestData.GetList<string>("ids");
            pids = RequestData.GetList<string>("pids");
            switch (this.RequestAction)
            {
                case RequestActionEnum.Custom:
                    if (RequestActionString == "querychildren")
                    {
                        if (String.IsNullOrEmpty(id))
                        {
                            ents = CRM_VIRTUALSYSGROUP.FindAll("FROM CRM_VIRTUALSYSGROUP as ent WHERE ParentId is null and (Type = 2 or Type = 3) Order By GroupID asc");
                        }
                        else
                        {
                            ents = CRM_VIRTUALSYSGROUP.FindAll("FROM CRM_VIRTUALSYSGROUP as ent WHERE ParentId = '" + id + "' and (Type = 2 or Type = 3) Order By GroupID asc");
                        }

                        this.PageState.Add("DtList", ents);
                    }
                    else if (RequestActionString == "batchdelete")
                    {
                        IList<object> idList = RequestData.GetList<object>("IdList");
                        if (idList != null && idList.Count > 0)
                        {
                            //先删除关系表
                            string delsql = "begin ";
                            foreach (object objId in idList)
                            {
                                delsql += "delete CRM_VIRTUALSYSUSERGROUP where GroupId='" + objId + "'; ";
                                delsql += "delete crm_vir2Actual where virroleid='" + objId + "'; ";
                            }
                            delsql += "end;";
                            DataHelper.ExecSql(delsql);

                            //CRM_VIRTUALSYSGROUP.DoBatchDelete(idList.ToArray());
                            CRM_VIRTUALSYSGROUP[] tents = CRM_VIRTUALSYSGROUP.FindAll(NHibernate.Criterion.Expression.In("GROUPID", idList.ToArray()));
                            foreach (CRM_VIRTUALSYSGROUP tent in tents)
                            {
                                tent.DoDelete();
                                if (Convert.ToInt32(DataHelper.QueryValue("select count(1) from CRM_VIRTUALSYSGROUP where ParentId='" + tent.ParentID + "'")) == 0)
                                {
                                    CRM_VIRTUALSYSGROUP pent = CRM_VIRTUALSYSGROUP.Find(tent.ParentID);
                                    pent.IsLeaf = 1;
                                    pent.DoUpdate();
                                }
                            }
                        }
                    }
                    else if (RequestActionString == "deleterolenames")
                    {
                        string roleId = this.RequestData.Get<string>("id");
                        string roleids = this.RequestData.Get<string>("roleids").TrimEnd(',');

                        string delsql = "delete from crm_vir2Actual where VIRROLEID='" + roleId + "' and RoleID in ('" + roleids.Replace(",", "','") + "') ";
                        DataHelper.ExecSql(delsql);
                    }
                    else if (RequestActionString == "setrolenames")
                    {
                        string roleId = this.RequestData.Get<string>("id");
                        string names = this.RequestData.Get<string>("names");
                        string roleids = this.RequestData.Get<string>("ids");
                        string insertTpl = "insert into crm_vir2Actual (ID,RoleID,RoleName,VIRROLEID) values (SYS_GUID(),'{0}','{1}','" + roleId + "') ";
                        string[] namearry = names.Split(',');
                        string[] idarry = roleids.Split(',');

                        for (int i = 0; i < namearry.Length; i++)
                        {
                            DataHelper.ExecSql(string.Format(insertTpl, idarry[i], namearry[i]));
                        }
                    }
                    break;
                default:
                    //CRM_VIRTUALSYSGROUP[] grpList = CRM_VIRTUALSYSGROUP.FindAll("From CRM_VIRTUALSYSGROUP as ent where \"ParentID\" is null and nvl(Status,'')<>0 Order By SortIndex");
                    DataTable grpList = DataHelper.QueryDataTable("select GROUPID,ParentID as \"ParentID\",IsLeaf as \"IsLeaf\",NAME,CODE,TYPE,STATUS,SORTINDEX,REMARK,CREATETIME from CRM_VIRTUALSYSGROUP where ParentID is null Order By SortIndex");
                    PageState.Add("DtList", grpList);

                    string sql = "SELECT * FROM crm_vir2Actual where  VIRROLEID ='" + RequestData.Get<string>("Rolid") + "'";
                    this.PageState.Add("ActualRoleList", GetPageData(sql, SearchCriterion));
                    break;
            }
        }

        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue("select count(1) from (" + sql + ") t"));
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "RoleName";
            string asc = search.Orders.Count <= 0 || search.Orders[0].Ascending ? " asc" : " desc";

            string pagsql = @"select * from(
                    select rownum r,t.* from (
                    select * from ({2})  order by  {0} {1}
                    )t)
                    where r between {3} and {4}";

            pagsql = string.Format(pagsql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pagsql);
            return dicts;
        }
    }
}
