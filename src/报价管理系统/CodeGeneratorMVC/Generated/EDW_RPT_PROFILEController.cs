using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.ActiveRecord;
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

using OnControl.Model;
using Oncontrol3.Web;

namespace OnControl.Web
{
    [AuthorLogin]
    public partial class EDW_RPT_PROFILEController : BaseController
    {
        //
        // GET: /EDW_RPT_PROFILE/
        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            return View();
        }
        
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "Code", "Name" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(EDW_RPT_PROFILE).GetProperty(key).PropertyType;
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
            var obj = new { rows = EDW_RPT_PROFILE.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(EDW_RPT_PROFILE), SearchCriterion.GetDetachedCriteriaWithoutOrder<EDW_RPT_PROFILE>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        //
        // GET: /EDW_RPT_PROFILE/Create

        public ActionResult Create()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            return View();
        }

        //
        // POST: /EDW_RPT_PROFILE/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EDW_RPT_PROFILE ent)//多对象form时使用(FormCollection collection)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            try
            {
                ent.DoCreate();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "名称已被占用,请返回修改。");
                return View(user);
            }
        }

        //
        // GET: /EDW_RPT_PROFILE/Edit/5

        public ActionResult Edit(string id)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            EDW_RPT_PROFILE ent = EDW_RPT_PROFILE.Find(id);
            return View("Create", ent);
        }

        //
        // POST: /EDW_RPT_PROFILE/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, EDW_RPT_PROFILE data)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                EDW_RPT_PROFILE ent = this.GetMergedData<EDW_RPT_PROFILE>(data);
                ent.DoUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EDW_RPT_PROFILE/Delete/5

        public ActionResult Delete()
        {
            try
            {
                string id = Request.QueryString["id"];
                EDW_RPT_PROFILE ent = EDW_RPT_PROFILE.Find(id);
                ent.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }

        //
        // POST: /EDW_RPT_PROFILE/Delete/5

        public ActionResult Reset()
        {
            try
            {
                string id = Request.QueryString["id"];
                EDW_RPT_PROFILE ent = EDW_RPT_PROFILE.Find(id);
                ent.Save();
            }
            catch (Exception ex)
            {
                return Content("重置出现异常:" + ex.Message);
            }
            return Content("重置成功!");
        }
        
        //
        // GET: /EDW_RPT_PROFILE/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

    }
}

