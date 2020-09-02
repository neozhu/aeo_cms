using Aim;
using Aim.Common;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Newtonsoft.Json.Linq;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    [AuthorLogin]
    public class SystemController : BaseController
    {
        //
        // GET: /System/

        private SysModule[] modules = null;
        private DataEnum moduleTypeEnum = null;
        public ActionResult Index()
        {
            ViewBag.StatusCombo = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//追加状态枚举
            return View("Index", "_Layout1");
        }

        public ActionResult RoleList()
        {
            return View("Index", "_Layout1");
        }
        public ActionResult RoleCreate()
        {
            return View("Index", "_Layout1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(SysRole user)//多对象form时使用(FormCollection collection)
        {
            try
            {
                user.DoCreate();

                return RedirectToAction("RoleList");
            }
            catch
            {
                ModelState.AddModelError("", "角色已被占用,请返回修改。");
                return View("RoleCreate", "_Layout1",user);
            }
        }

        //
        // GET: /SysUser/Edit/5

        public ActionResult RoleEdit(string id)
        {
            SysRole user = SysRole.Find(id);
            return View("RoleCreate", "_Layout1", user);
        }

        //
        // POST: /SysUser/Edit/5

        [HttpPost]
        public ActionResult RoleEdit(string id, SysRole user)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                SysRole ent = this.GetMergedData<SysRole>(user);
                ent.LastModifiedDate = DateTime.Now;
                ent.DoUpdate();

                return RedirectToAction("RoleList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RoleDelete()
        {
            try
            {
                string id = Request.QueryString["id"];
                SysRole user = SysRole.Find(id);
                user.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }
        public ActionResult RoleListData()
        {
            string[] searchKeys = new string[] { "Code", "Name" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(SysRole).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request.QueryString[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                    {
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request.QueryString[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                    }
                }
            }
            var obj = new { rows = SysRole.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SysRole), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysRole>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        public ActionResult RoleUser()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View("RoleUser", "_ContentLayout");
        }
        public ActionResult RoleUserData(string id)
        {
            IList<SysUser> users = null;
            using (new Castle.ActiveRecord.SessionScope())
            {
                SysRole role = SysRole.Find(id);
                users = role.User;
            }
            return Content(JsonHelper.GetJsonString(users));
        }
        public ActionResult AddUserRole()
        {
            string id = Request["roleid"];
            string userid = Request["userid"];
            using (new SessionScope())
            {
                SysRole role = SysRole.Find(id);
                role.User.Add(SysUser.Find(userid));
            }
            return Content("添加成功!");
        }
        public ActionResult DeleteUserRole()
        {
            string id = Request["roleid"];
            string userid = Request["userid"];
            using (new SessionScope())
            {
                SysRole role = SysRole.Find(id);
                role.User.Remove(SysUser.Find(userid));
            }
            return Content("移除成功!");
        }

        //
        // GET: /System/Details/5

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                //SysApplication[] sysApps = SysApplication.FindAll(Expression.Eq(SysApplication.Prop_Status, 1));
                //sysApps = sysApps.OrderBy(ent => ent.SortIndex).ToArray();

                return Content(JsonHelper.GetJsonString(DataHelper.QueryDictList("Select CODE as \"Code\",NAME as \"Name\",STATUS as \"Status\",SORTINDEX as \"SortIndex\",CREATEDATE as \"CreateDate\",APPLICATIONID as \"ModuleID\",'closed' as \"state\" from SYSAPPLICATION a ORDER BY SORTINDEX")));
            }
            else
            {
                SysModule[] mdls = null;
                if (!string.IsNullOrEmpty(Request["Type"]))
                {
                    if (Request["level"] == "1")
                    {
                        return Content(JsonHelper.GetJsonString(DataHelper.QueryDictList("Select MODULEID as \"ModuleID\", CODE as \"Code\",NAME as \"Name\",STATUS as \"Status\",SORTINDEX as \"SortIndex\",CREATEDATE as \"CreateDate\",URL as \"Url\",'closed' as \"state\" from SYSMODULE s where APPLICATIONID='" + id + "' and PATHLEVEL=0 ORDER BY SORTINDEX")));
                    }
                    else
                        mdls = SysModule.FindAll("FROM SysModule as mdl WHERE mdl.ParentID = ?", id);

                }
                else
                {
                    mdls = SysModule.FindAll("FROM SysModule as mdl WHERE mdl.ApplicationID = ? AND  mdl.ParentID is null", id);
                }

                mdls = mdls.OrderBy(ent => ent.SortIndex).ToArray();

                return Content(JsonHelper.GetJsonString(mdls));
            }
        }

        //
        // POST: /System/Create

        [HttpPost]
        public ActionResult SaveApp(FormCollection collection)
        {
            try
            {
                string data = "";
                // TODO: Add insert logic here
                if (collection["level"] == "1")
                {
                    SysApplication app = new SysApplication();
                    if (SysApplication.FindAll(Expression.Eq(SysApplication.Prop_Code, collection["Code"])).Length > 0 && string.IsNullOrEmpty(collection["ModuleID"]))
                    {
                        return Content("保存出错,已存在此编号的应用!");
                    }
                    else if (!string.IsNullOrEmpty(collection["ModuleID"]))
                        app = SysApplication.Find(collection["ModuleID"]);
                    //app.Code = collection["Code"];
                    app.Name = collection["Name"];
                    app.Status = int.Parse(collection["Status"]);
                    app.SortIndex = int.Parse(collection["SortIndex"]);
                    app.Url = collection["Url"];
                    if (!string.IsNullOrEmpty(collection["ModuleID"]))
                        app.DoUpdate();
                    else
                    {
                        app.Code = collection["Code"];
                        app.DoCreate();
                    }
                    return Content("{'message':'保存成功!','data':" + JsonHelper.GetJsonString(app) + "}");
                }
                else
                {
                    SysModule app = new SysModule();
                    if (SysModule.FindAll(Expression.Eq(SysModule.Prop_Code, collection["Code"])).Length > 0 && string.IsNullOrEmpty(collection["ModuleID"]))
                    {
                        return Content("保存出错,已存在此编号的模块!");
                    }
                    else if (!string.IsNullOrEmpty(collection["ModuleID"]))
                        app = SysModule.Find(collection["ModuleID"]);
                    app.Name = collection["Name"];
                    app.ApplicationID = collection["ApplicationID"];
                    app.Type = 2;
                    app.Status = int.Parse(collection["Status"]);
                    app.SortIndex = int.Parse(collection["SortIndex"]);
                    app.Url = collection["Url"];
                    if (!string.IsNullOrEmpty(collection["ParentID"]))
                    {
                        app.ParentID = collection["ParentID"];
                        if (!string.IsNullOrEmpty(collection["ModuleID"]))
                            app.DoUpdate();
                        else
                        {
                            app.Code = collection["Code"];
                            app.CreateAsSub(collection["ParentID"]);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(collection["ModuleID"]))
                            app.DoUpdate();
                        else
                        { 
                            app.Code = collection["Code"];
                            app.CreateAsTop();
                        }
                    }
                    return Content("{'message':'保存成功!','data':" + JsonHelper.GetJsonString(app) + "}");
                }
            }
            catch
            {
                return Content("保存出错");
            }
        }

        //
        // GET: /System/Modify/5

        public ActionResult Modify(string id)
        {
            try
            {
                if (Request["level"] == "1")
                {
                    SysApplication app = SysApplication.Find(id);
                    return Content(JsonHelper.GetJsonString(app));
                }
                else
                {
                    SysModule app = SysModule.Find(id);
                    return Content(JsonHelper.GetJsonString(app));
                }
            }
            catch
            {
                return Content("出错");
            }
        }
        //
        // GET: /System/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                if (Request["level"] == "1")
                {
                    SysApplication app = SysApplication.Find(id);
                    app.DoDelete();
                }
                else
                {
                    SysModule app = SysModule.Find(id);
                    app.DoDelete();

                }
                return Content("删除成功!");
            }
            catch
            {
                return Content("删除出错");
            }
        }

        public ActionResult Refresh()
        {
            try
            {
                PortalService.RefreshSysModules();
                return Content("刷新成功!");
            }
            catch
            {
                return Content("刷新出错");
            }
        }

        public ActionResult AuthManage()
        {
            return View("AuthManage", "_Layout1");
        }
        public ActionResult AuthManageChild()
        {
            return View("AuthManageChild", "_Layout1");
        }
        public ActionResult AuthData()
        {
            SysAuth[] ents = SysAuth.FindAll("FROM SysAuth as ent WHERE type=1");
            string jsonString = JsonHelper.GetJsonString(this.ToExtTreeCollection(ents.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate), null));
            return Content(jsonString);
        }
        public ActionResult AuthDataChild()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);
            SysAuth[] ents = user.Auth.Distinct().ToArray();
            //SysAuth[] ents = SysAuth.FindAll("FROM SysAuth as ent WHERE type=1");
            string jsonString = JsonHelper.GetJsonString(this.ToExtTreeCollection(ents.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate), null));
            return Content(jsonString);
        }
        public ActionResult OrgData()
        {
            SysGroup[] ents = SysGroup.FindAll("FROM SysGroup as ent WHERE Status=1 and type<>3");
            string jsonString = JsonHelper.GetJsonString(this.ToExtTreeCollectionGroup(ents.OrderBy(v => v.SortIndex).ThenBy(v => v.CreateDate), null));
            return Content(jsonString);
        }
        public ActionResult GetUserAuths()
        {

            IEnumerable<string> authIDs = null;
            string type = Request.QueryString["type"];
            string id = Request.QueryString["id"];
            using (new Castle.ActiveRecord.SessionScope())
            {
                if (type == "user" && !String.IsNullOrEmpty(id))
                {
                    SysUser user = SysUser.Find(id);
                    authIDs = (user.Auth).Select((ent) => { return ent.AuthID; });
                }
                else if (type == "group" && !String.IsNullOrEmpty(id))
                {
                    SysGroup group = SysGroup.Find(id);
                    authIDs = (group.Auth).Select((ent) => { return ent.AuthID; });
                }
                else if (type == "role" && !String.IsNullOrEmpty(id))
                {
                    SysRole role = SysRole.Find(id);
                    authIDs = (role.Auth).Select((ent) => { return ent.AuthID; });
                }
            }
            return Content(string.Join(",", authIDs));
        }

        public ActionResult SaveAuth()
        {
            string id = Request.Form["id"];
            string type = Request.Form["type"];
            string add = Request["added[]"] ?? "";
            string removed = Request.Form["removed[]"] ?? "";

            if (type == "user" && !String.IsNullOrEmpty(id))
            {
                SysAuthRule.GrantAuthToUser(add.Split(','), id);
                SysAuthRule.RevokeAuthFromUser(removed.Split(','), id);
            }
            else if (type == "group" && !String.IsNullOrEmpty(id))
            {
                SysAuthRule.GrantAuthToGroup(add.Split(','), id);
                SysAuthRule.RevokeAuthFromGroup(removed.Split(','), id);
            }
            else if (type == "role" && !String.IsNullOrEmpty(id))
            {
                SysAuthRule.GrantAuthToRole(add.Split(','), id);
                SysAuthRule.RevokeAuthFromRole(removed.Split(','), id);
            }
            return Content("保存成功!");
        }
        /// <summary>
        /// 生成ExtTree
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private WebHelper.ExtTreeNodeCollection ToExtTreeCollection(IEnumerable<SysAuth> ents, WebHelper.ExtTreeNode pnode)
        {
            string parentID = (pnode == null) ? null : (pnode["id"] == null ? null : pnode["id"].ToString());

            IEnumerable<SysAuth> rtnents = null;

            WebHelper.ExtTreeNodeCollection nodes = new WebHelper.ExtTreeNodeCollection();

            if (ents != null)
            {
                if (String.IsNullOrEmpty(parentID))
                {
                    rtnents = ents.Where(ent => (ent.ParentID == null || ent.ParentID == String.Empty));
                }
                else
                {
                    rtnents = ents.Where(ent => ent.ParentID == parentID);
                }

                if (rtnents.Count() > 0)
                {
                    if (pnode != null)
                    {
                        pnode["leaf"] = false;
                    }

                    foreach (SysAuth tent in rtnents)
                    {
                        WebHelper.ExtTreeNode node = new WebHelper.ExtTreeNode();
                        node["id"] = tent.AuthID;
                        node["text"] = tent.Name;
                        node["AuthID"] = tent.AuthID;
                        node["ParentID"] = tent.ParentID;
                        node["ModuleID"] = tent.ModuleID;
                        node["Type"] = tent.Type;
                        node["Name"] = tent.Name;
                        node["Code"] = tent.Code;
                        node["Data"] = tent.Data;
                        node["Path"] = tent.Path;
                        node["PathLevel"] = tent.PathLevel;
                        node["SortIndex"] = tent.SortIndex;
                        node["LastModifiedDate"] = tent.LastModifiedDate;
                        node["CreateDate"] = tent.CreateDate;
                        node["Description"] = tent.Description;
                        node["children"] = ToExtTreeCollection(ents, node);

                        nodes.Add(node);
                    }
                }
                else
                {
                    if (pnode != null)
                    {
                        pnode["leaf"] = true;

                        if (pnode["children"] == null)
                        {
                            pnode.Remove("children");
                        }
                    }
                }
            }

            return nodes;
        }

        private WebHelper.ExtTreeNodeCollection ToExtTreeCollectionGroup(IEnumerable<SysGroup> ents, WebHelper.ExtTreeNode pnode)
        {
            string parentID = (pnode == null) ? null : (pnode["id"] == null ? null : pnode["id"].ToString());

            IEnumerable<SysGroup> rtnents = null;

            WebHelper.ExtTreeNodeCollection nodes = new WebHelper.ExtTreeNodeCollection();

            if (ents != null)
            {
                if (String.IsNullOrEmpty(parentID))
                {
                    rtnents = ents.Where(ent => (ent.ParentID == null || ent.ParentID == String.Empty));
                }
                else
                {
                    rtnents = ents.Where(ent => ent.ParentID == parentID);
                }

                if (rtnents.Count() > 0)
                {
                    if (pnode != null)
                    {
                        pnode["leaf"] = false;
                    }

                    foreach (SysGroup tent in rtnents)
                    {
                        WebHelper.ExtTreeNode node = new WebHelper.ExtTreeNode();
                        node["id"] = tent.GroupID;
                        node["text"] = tent.Name;
                        node["GroupID"] = tent.GroupID;
                        node["ParentID"] = tent.ParentID;
                        node["Type"] = tent.Type;
                        node["Name"] = tent.Name;
                        node["Code"] = tent.Code;
                        node["Path"] = tent.Path;
                        node["PathLevel"] = tent.PathLevel;
                        node["SortIndex"] = tent.SortIndex;
                        node["LastModifiedDate"] = tent.LastModifiedDate;
                        node["CreateDate"] = tent.CreateDate;
                        node["Description"] = tent.Description;
                        node["children"] = ToExtTreeCollectionGroup(ents, node);

                        nodes.Add(node);
                    }
                }
                else
                {
                    if (pnode != null)
                    {
                        pnode["leaf"] = true;

                        if (pnode["children"] == null)
                        {
                            pnode.Remove("children");
                        }
                    }
                }
            }

            return nodes;
        }

        /// <summary>
        /// 邮件发送view
        /// </summary>
        /// <returns></returns>
        public ActionResult MailList()
        {
            return View();
        }
        /// <summary>
        /// 邮件发送历史数据
        /// </summary>
        /// <returns></returns>
        public ActionResult MailListData()
        {
            string[] searchKeys = new string[] { "ApplicationName", "ModuleName" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(SysEvent).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request.QueryString[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request.QueryString[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            SearchCriterion.AddSearch(SysEvent.Prop_Type, "Mail", SearchModeEnum.Like);
            var obj = new { rows = SysEvent.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SysEvent), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysEvent>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        /// <summary>
        /// 不用实体类,使用sql获取分页数据
        /// </summary>
        /// <param name="tempsql"></param>
        /// <returns></returns>
        private IList<EasyDictionary> GetPageDataOracle(string tempsql)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue<decimal>("select count(1) from (" + tempsql + ") t"));
            string order = !string.IsNullOrEmpty(Request.QueryString["sort"]) ? Request.QueryString["sort"] : "CREATETIME";
            string asc = !string.IsNullOrEmpty(Request.QueryString["order"]) ? Request.QueryString["order"] : " desc";
            string sql_page = "With DATASET AS( select USERID \"UserID\",NAME \"Name\",LOGINNAME \"LoginName\",STATUS \"Status\",ROWNUM As RN from ({0}) A order by {1} {2}) select * from DATASET  WHERE RN between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }

    }
}
