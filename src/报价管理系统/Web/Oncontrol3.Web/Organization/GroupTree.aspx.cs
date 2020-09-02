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
using Aim.Common;
using Aim;

namespace CRM.Web
{
    public partial class GroupTree : BaseListPage
    {
        private SysGroup[] ents = null;
        string id = String.Empty;   // 对象id
        IList<string> ids = null;   // 节点列表
        IList<string> pids = null;   // 父节点列表   
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            ids = RequestData.GetList<string>("ids");
            pids = RequestData.GetList<string>("pids");
            switch (RequestActionString)
            {
                case "querydescendant":
                    if (!String.IsNullOrEmpty(id))
                    {
                        ents = SysGroup.FindAll("FROM SysGroup as ent WHERE ParentId = '" + id + "' and (Type = 2 or Type = 3) and nvl(Status,'')<>0 Order By SortIndex asc");
                    }
                    //WebHelper.ExtTreeNode pnode = new WebHelper.ExtTreeNode();
                    //pnode["id"] = id;
                    //pnode[""]
                    string jsonString = JsonHelper.GetJsonString(this.ToExtTreeCollection(ents.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate), null));
                    Response.Write(jsonString);
                    Response.End();
                    break;
                default:
                    //SearchCriterion.AddSearch("ParentID", "1001");
                    //SysGroup[] grpList = SysGroup.FindAll(SearchCriterion);
                    ////  string jsonString = JsonHelper.GetJsonString(this.ToExtTreeCollection(grpList.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate), null));
                    //PageState.Add("DataTree", grpList);
                    break;
            }
        }
        private WebHelper.ExtTreeNodeCollection ToExtTreeCollection(IEnumerable<SysGroup> ents, WebHelper.ExtTreeNode pnode)
        {
            //string parentID = (pnode == null) ? null : (pnode["id"] == null ? null : pnode["id"].ToString());
            //IEnumerable<SysGroup> rtnents = null;
            WebHelper.ExtTreeNodeCollection nodes = new WebHelper.ExtTreeNodeCollection();
            //if (ents != null)
            //{
            //if (String.IsNullOrEmpty(parentID))
            //{
            //    rtnents = ents.Where(ent => (ent.ParentID == null || ent.ParentID == String.Empty));
            //}
            //else
            //{
            //    rtnents = ents.Where(ent => ent.ParentID == parentID);
            //}
            //rtnents = rtnents.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate);rtnrtn

            //if (ents.Count() > 0)
            //{
            //    if (pnode != null)
            //    {
            //        pnode["leaf"] = false;
            //    }
            foreach (SysGroup tent in ents)
            {
                WebHelper.ExtTreeNode node = new WebHelper.ExtTreeNode();
                node["id"] = tent.GroupID;
                node["text"] = tent.Name;
                //node["GroupID"] = tent.GroupID;
                node["ParentID"] = tent.ParentID;
                //  node["Type"] = tent.Type;
                node["Name"] = tent.Name;
                //node["Code"] = tent.Code;
                //node["Path"] = tent.Path;
                // node["PathLevel"] = tent.PathLevel;
                //node["Status"] = tent.Status;
                // node["SortIndex"] = tent.SortIndex;
                //node["LastModifiedDate"] = tent.LastModifiedDate;
                // node["CreateDate"] = tent.CreateDate;
                //  node["Description"] = tent.Description;
                node["leaf"] = tent.IsLeaf;
                //node["children"] = ToExtTreeCollection(ents, node);
                nodes.Add(node);
            }
            // }
            //else
            //{
            //    if (pnode != null)
            //    {
            //        pnode["leaf"] = true;

            //        if (pnode["children"] == null)
            //        {
            //            pnode.Remove("children");
            //        }
            //    }
            //}
            // }

            return nodes;
        }
        [ActiveRecordTransaction]
        private void DoCreateSubByRole()
        {
            IList<string> idList = RequestData.GetList<string>("IdList");
            SysGroup tent = SysGroup.Find(id);

            if (idList != null && idList.Count > 0)
            {
                SysRole[] duties = SysRole.FindAllByPrimaryKeys(idList.ToArray());

                foreach (SysRole tduty in duties)
                {
                    if (!SysGroup.Exists("Name=? and Type=21 and ParentID = ?", tduty.Name, tent.ID))
                    {
                        SysGroup tgrp = new SysGroup()
                        {
                            Name = tduty.Name,
                            Code = tduty.Code,
                            Type = 3,   //角色
                            Status = 1,
                            SortIndex = 9999,
                            CreateDate = DateTime.Now
                        };

                        tgrp.CreateAsChild(tent);
                    }
                }
            }
        }
    }
}
