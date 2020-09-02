using System;
using Castle.ActiveRecord;
using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using System.Web.Mvc;
using Aim.Portal;
using System.Data;
using Oncontrol3.Web.Helpers;
using BaseDLL;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using Aspose.Cells;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Com.Feiliks.QDM;
using System.Web;

namespace Oncontrol3.Web.Controllers
{
    //[AuthorLogin]
    public partial class SQM_CALCController : BaseController
    {
        //
        // GET: /SQM_CALC_BASE_EXT/
        public ActionResult Index()
        {
            //if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            //{
            //    Response.Redirect("/Account/Login");
            //}
            string sql = @"select CALC_BASE,DESCRIPTION from MDM_CALC_BASE where not regexp_like(DESCRIPTION,'([a-z])')";
            DataTable dt = DataHelper.QueryDataTable(sql);
            ViewBag.Data = dt;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "CALCCODE" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_CALC_BASE_EXT).GetProperty(key).PropertyType;
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
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_CALC_BASE_EXT), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_CALC_BASE_EXT>());
            var obj = new { draw = Request["draw"], data = SQM_CALC_BASE_EXT.FindAll(SearchCriterion), recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        //
        // GET: /SQM_CALC_BASE_EXT/Create
        public ActionResult Create()
        {
            //if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            //{
            //    Response.Redirect("/Account/Login");
            //}
            try
            {
                string id = Request.QueryString["id"];
                string sql = @"select CALC_BASE,DESCRIPTION from MDM_CALC_BASE where not regexp_like(DESCRIPTION,'([a-z])')";
                DataTable dt = DataHelper.QueryDataTable(sql);
                ViewBag.Data = dt;
                if (!String.IsNullOrEmpty(id))
                {
                    SQM_CALC_BASE_EXT ent = SQM_CALC_BASE_EXT.Find(id);
                    return View("Create", ent);
                }
                else
                {
                    SQM_CALC_BASE_EXT ent = new SQM_CALC_BASE_EXT();
                    return View("Create", ent);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        //
        // POST: /SQM_CALC_BASE_EXT/Create
        public ActionResult CreateData(SQM_CALC_BASE_EXT ent)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                string rid = Request["id"].ToString();
                if (!String.IsNullOrEmpty(rid))
                {
                    SQM_CALC_BASE_EXT erd = SQM_CALC_BASE_EXT.Find(rid);
                    erd.CALCNAME = ent.CALCNAME;
                    erd.CALCCODE = ent.CALCCODE;
                    erd.MDMTYPE = ent.MDMTYPE;
                    erd.MDMKEY = ent.MDMKEY;
                    erd.MDMFIELDNAME = ent.MDMFIELDNAME;
                    erd.MDMLOCTYPE = ent.MDMLOCTYPE;
                    erd.DoUpdate();
                }
                else
                {
                    var data = SQM_CALC_BASE_EXT.FindAllByProperties(SQM_CALC_BASE_EXT.Prop_CALCCODE,ent.CALCCODE);
                    if (data.Length > 0)
                    {
                        return Content(new JsonMessage { Success = false, Message = "该计算基础已存在，请确认！" }.ToString());
                    }
                    ent.DoCreate();
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        [AllowAnonymous]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SQM_CALC_BASE_EXT.TryFind(keyValue);
            return Content(JsonHelper.GetJsonString(data));
        }
    }
}

