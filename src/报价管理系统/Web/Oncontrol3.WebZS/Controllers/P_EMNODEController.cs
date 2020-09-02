using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Security;
using Castle.ActiveRecord;
using NHibernate.Criterion;

using OnControl.Model;
using Oncontrol3.Web;
using System.Data;

namespace OnControl.Web
{
    [AuthorLogin]
    public partial class P_EMNODEController : BaseController
    {
        //
        // GET: /P_EMNODE/
        public ActionResult Index()
        {
            //ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            return View();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "NAME", "TYPE" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(P_EMNODE).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request.QueryString[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request.QueryString[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CreateDateS"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            SearchCriterion.AutoOrder = false;
            SearchCriterion.SetOrder("TYPE");
            var obj = new { rows = P_EMNODE.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_EMNODE), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_EMNODE>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        public ActionResult ListsOrder()
        {
            DataTable dt = DataHelper.QueryDataTable("select * from P_EMNODE t  order by type,sortindex asc");
            return Content(JsonHelper.GetJsonString(dt));
        }
        public ActionResult GetUserNodes()
        {
            string id = this.Request.QueryString["USERID"];// WebPortalService.CurrentUserSID;

            DataTable dt = DataHelper.QueryDataTable("select * from P_USER_ENODES where USERID='" + id + "'");
            return Content(JsonHelper.GetJsonString(dt));
        }
        public ActionResult SaveUserNodes()
        {
            string id = this.Request.QueryString["USERID"];// WebPortalService.CurrentUserSID;
            string nodes = this.Request.QueryString["NODES"];
            string[] nodeids = nodes.TrimEnd(',').Split(',');
                DataHelper.ExecSql("delete from P_USER_ENODES where USERID='" + id + "'");
                foreach (string nodeid in nodeids)
                {
                    //if (DataHelper.QueryDataTable("select * from P_USER_ENODES where USERID='" + id + "' and EID='" + nodeid + "'").Rows.Count == 0)
                    //{
                        P_EMNODE node = P_EMNODE.Find(nodeid);
                        DataHelper.ExecSql("insert into P_USER_ENODES(ID,USERID,EID,ENAME,SORTINDEX,CREATETIME) values (sys_guid(),'" + id + "','" + nodeid + "','" + node.NAME + "','" + node.SORTINDEX + "',Sysdate)");
                    //}
                }
            return Content("保存成功!");
        }

        //
        // GET: /P_EMNODE/Create

        public ActionResult Create()
        {
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            return View();
        }

        //
        // POST: /P_EMNODE/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(P_EMNODE ent)//多对象form时使用(FormCollection collection)
        {
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            try
            {
                ent.DoCreate();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "名称已被占用,请返回修改。");
                return View(ent);
            }
        }

        //
        // GET: /P_EMNODE/Edit/5

        public ActionResult Edit(string id)
        {
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            P_EMNODE ent = P_EMNODE.Find(id);
            return View("Create", ent);
        }

        //
        // POST: /P_EMNODE/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, P_EMNODE data)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                P_EMNODE ent = this.GetMergedData<P_EMNODE>(data);
                ent.DoUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /P_EMNODE/Delete/5

        public ActionResult Delete()
        {
            try
            {
                string id = Request.QueryString["id"];
                P_EMNODE ent = P_EMNODE.Find(id);
                ent.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }

        //
        // POST: /P_EMNODE/Delete/5

        public ActionResult Reset()
        {
            try
            {
                string id = Request.QueryString["id"];
                P_EMNODE ent = P_EMNODE.Find(id);
                ent.Save();
            }
            catch (Exception ex)
            {
                return Content("重置出现异常:" + ex.Message);
            }
            return Content("重置成功!");
        }

        //
        // GET: /P_EMNODE/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

    }
}

