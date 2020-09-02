using Aim;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Security;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    [AuthorLogin]
    public class SysUserController : BaseController
    {
        //
        // GET: /SysUser/
        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            return View("Index", "_Layout1");
        }
        public ActionResult smartindex()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View("Indexsmart");
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Listsmart()
        {
            string[] searchKeys = new string[] { "LoginName", "Name", "Status" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SysUser).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (!string.IsNullOrEmpty(Request["CreateDateS"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SysUser), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysUser>());
            var obj = new { draw = Request["draw"], data = SysUser.FindAll(SearchCriterion), recordsTotal = total, recordsFiltered = total };
            //多表关联时根据sql去检索数据
            //string sql = "select * from SysUser  where 1=1 ";
            //因为oracle大小写敏感,新建的表字段最好都统一大写,包括实体类
            //var obj = new { draw = Request["draw"], data = base.GetPageData(sql, SearchCriterion), recordsTotal = SearchCriterion.RecordCount, recordsFiltered = SearchCriterion.RecordCount };
            return Content(JsonHelper.GetJsonString(obj));
        }
        public ActionResult IndexChild()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            return View("IndexChild", "_Layout1");
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "LoginName", "Name", "Status" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(SysUser).GetProperty(key).PropertyType;
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
            var obj = new { rows = SysUser.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SysUser), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysUser>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }
        public ActionResult ListChilds()
        {
            string[] searchKeys = new string[] { "LoginName", "Name", "Status" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(SysUser).GetProperty(key).PropertyType;
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
            //SysUser user = SysUser.Find();
            SearchCriterion.AddSearch("CreateId", PortalService.CurrentUserInfo.UserID);
            var obj = new { rows = SysUser.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(SysUser), SearchCriterion.GetDetachedCriteriaWithoutOrder<SysUser>()) };
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
        // GET: /SysUser/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

        //
        // GET: /SysUser/Create

        public ActionResult Create()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            return View("Create", "_Layout1");
        }
        public ActionResult CreateChild()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            return View("CreateChild", "_Layout1");
        }

        //
        // POST: /SysUser/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SysUser user)//多对象form时使用(FormCollection collection)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            try
            {
                user.WorkNo = "";
                MD5Encrypt encrypt = new MD5Encrypt();
                user.Password = encrypt.GetMD5FromString(user.Password);
                user.CreateId = PortalService.CurrentUserInfo.UserID;
                user.CreateName = PortalService.CurrentUserInfo.Name;
                user.CreateDate = DateTime.Now;
                //修正多选
                user.Ext2 = Request.Form["Ext2"];
                user.Pk_zw = Request.Form["Pk_zw"];
                user.DoCreate();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "登录名已被占用,请返回修改。");
                return View(user);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChild(SysUser user)//多对象form时使用(FormCollection collection)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            try
            {
                user.WorkNo = "";
                MD5Encrypt encrypt = new MD5Encrypt();
                user.Password = encrypt.GetMD5FromString(user.Password);

                SysUser userp = SysUser.Find(PortalService.CurrentUserInfo.UserID);
                user.CreateId = PortalService.CurrentUserInfo.UserID;
                user.CreateName = PortalService.CurrentUserInfo.Name;
                user.CreateDate = DateTime.Now;
                user.Ext1 = userp.Ext1;
                //修正多选
                user.Ext2 = Request.Form["Ext2"];
                user.Pk_zw = Request.Form["Pk_zw"];

                user.DoCreate();

                return RedirectToAction("IndexChild");
            }
            catch
            {
                ModelState.AddModelError("", "登录名已被占用,请返回修改。");
                return View(user);
            }
        }

        //
        // GET: /SysUser/Edit/5

        public ActionResult Edit(string id)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            SysUser user = SysUser.Find(id);
            return View("Create",  "_Layout1",user);
        }
        public ActionResult EditChild(string id)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.BUSINESS = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("BUSINESS"));//追加状态枚举
            ViewBag.LOCATION = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("LOCATION"));//追加状态枚举
            ViewBag.ORDERTYPE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("ORDERTYPE"));//追加状态枚举
            ViewBag.PLANT = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboWithBlankDatas("PLANT"));//追加状态枚举
            ViewBag.CHECKRULE = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("CHECKRULE"));//追加状态枚举
            SysUser user = SysUser.Find(id);
            return View("CreateChild", "_Layout1", user);
        }

        //
        // POST: /SysUser/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, SysUser user)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                SysUser ent = this.GetMergedData<SysUser>(user);
                ent.LastModifiedDate = DateTime.Now;
                ent.WorkNo = "";
                //修正多选
                ent.Ext2 = Request.Form["Ext2"];
                ent.Pk_zw = Request.Form["Pk_zw"];
                ent.DoUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateChild", user);
            }
        }

        [HttpPost]
        public ActionResult EditChild(string id, SysUser user)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                SysUser ent = this.GetMergedData<SysUser>(user);
                ent.LastModifiedDate = DateTime.Now;
                ent.WorkNo = "";
                //修正多选
                ent.Ext2 = Request.Form["Ext2"];
                ent.Pk_zw = Request.Form["Pk_zw"];
                ent.DoUpdate();

                return RedirectToAction("IndexChild");
            }
            catch
            {
                return View("CreateChild", user);
            }
        }
        //
        // GET: /SysUser/Delete/5

        public ActionResult Delete()
        {
            try
            {
                string id = Request.QueryString["id"];
                SysUser user = SysUser.Find(id);
                user.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }

        //
        // POST: /SysUser/Delete/5

        public ActionResult Reset()
        {
            try
            {
                string id = Request.QueryString["id"];
                SysUser user = SysUser.Find(id);
                user.Password = "";
                user.Save();
            }
            catch (Exception ex)
            {
                return Content("重置出现异常:" + ex.Message);
            }
            return Content("重置成功!");
        }

        public ActionResult ListSels()
        {
            DataTable datas = DataHelper.QueryDataTable("select* from P_CUSTOMER where ID in (Select CUSTOMERID from P_CUSTOMERSYSUSERREF where UserID='" + Request.QueryString["id"] + "')");
            var obj = new { rows = datas, total = datas.Rows.Count };
            return Content(JsonHelper.GetJsonString(datas));
        }

        public ActionResult SaveCustomerRef()
        {
            try
            {
                string id = Request.QueryString["userid"];
                string cusids = Request.QueryString["customerids"];
                string[] cus = cusids.TrimEnd(',').Split(',');
                DataTable dts = DataHelper.QueryDataTable("select CUSTOMERID from P_CUSTOMERSYSUSERREF where USERID='" + id + "'");
                foreach (string cusid in cus)
                {
                    if (dts.Select("CUSTOMERID='" + cusid + "'").Length == 0)
                    {
                        DataHelper.ExecSql("insert into P_CUSTOMERSYSUSERREF values('" + cusid + "','" + id + "')");
                    }
                }
                cusids = cusids.TrimEnd(',').Replace(",", "','");
                if (cusids != "")
                    DataHelper.ExecSql("delete from P_CUSTOMERSYSUSERREF where CUSTOMERID not in ('" + cusids + "')");
            }
            catch (Exception ex)
            {
                return Content("保存失败:" + ex.Message);
            }
            return Content("保存成功!");
        }

    }
}
