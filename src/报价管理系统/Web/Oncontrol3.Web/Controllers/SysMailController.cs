using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Security;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    [AuthorLogin]
    public class SysMailController : BaseController
    {
        //
        // GET: /SYSMAILTEMPLATE/

        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "TITLE", "NAME" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(SYSMAILTEMPLATE).GetProperty(key).PropertyType;
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
            var obj = new { rows = SYSMAILTEMPLATE.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SYSMAILTEMPLATE), SearchCriterion.GetDetachedCriteriaWithoutOrder<SYSMAILTEMPLATE>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }


        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult RoleLists()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["RoleName"]))
            {
                SearchCriterion.AddSearch("RoleName", Request.QueryString["RoleName"], Aim.Data.SearchModeEnum.Like);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
            {
                SearchCriterion.AddSearch("Code", Request.QueryString["Code"], Aim.Data.SearchModeEnum.Like);
            }
            var obj = new { rows = SysRole.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SysRole), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysRole>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        //
        // GET: /SYSMAILTEMPLATE/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

        //
        // GET: /SYSMAILTEMPLATE/Create

        public ActionResult Create()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            return View();
        }

        //
        // POST: /SYSMAILTEMPLATE/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SYSMAILTEMPLATE user)//多对象form时使用(FormCollection collection)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            try
            {
                user.CREATETIME = DateTime.Now;
                user.CREATEID = WebPortalService.CurrentUserInfo.UserID;
                user.NAME = WebPortalService.CurrentUserInfo.Name;
                user.DoCreate();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "模板编号已被占用,请修改编号。");
                return View(user);
            }
        }

        //
        // GET: /SYSMAILTEMPLATE/Edit/5

        public ActionResult Edit(string id)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            SYSMAILTEMPLATE user = SYSMAILTEMPLATE.Find(id);
            return View("Create", user);
        }

        //
        // POST: /SYSMAILTEMPLATE/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string id, SYSMAILTEMPLATE user)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                SYSMAILTEMPLATE ent = this.GetMergedData<SYSMAILTEMPLATE>(user);
                //ent.LastModifiedDate = DateTime.Now;
                ent.DoUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SYSMAILTEMPLATE/Delete/5

        public ActionResult Delete()
        {
            try
            {
                string id = Request.QueryString["id"];
                SYSMAILTEMPLATE user = SYSMAILTEMPLATE.Find(id);
                user.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }

        //
        // POST: /SYSMAILTEMPLATE/Delete/5

        public ActionResult Reset()
        {
            try
            {
                string id = Request.QueryString["id"];
                SYSMAILTEMPLATE user = SYSMAILTEMPLATE.Find(id);
                user.Save();
            }
            catch (Exception ex)
            {
                return Content("重置出现异常:" + ex.Message);
            }
            return Content("重置成功!");
        }

    }
}
