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
using System.Data;

namespace OnControl.Web
{
    //[AuthorLogin]
    public partial class SQM_WEBCONFIGController : BaseController
    {
        /// <summary>
        /// 字典页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            return View();
        }
        /// <summary>
        /// 字典明细页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DictDetail()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            DataTable dt = DataHelper.QueryDataTable("select TCET084,TEXTDESC from V_MDM_FEE");
            ViewBag.Data = dt;
            return View();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "DICNAME", "DICCODE", "SIGN" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_WEBCONFIG).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            SearchCriterion.AddSearch("SIGN", "-1", Aim.Data.SearchModeEnum.NotEqual);
            //if (!string.IsNullOrEmpty(Request.QueryString["CreateDateS"]))
            //{
            //    SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            //}
            //if (!string.IsNullOrEmpty(Request.QueryString["CreateDateE"]))
            //{
            //    SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            //}
            var total = ActiveRecordMediator.Count(typeof(SQM_WEBCONFIG), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_WEBCONFIG>());
            var obj = new { draw = Request["draw"], data = SQM_WEBCONFIG.FindAll(SearchCriterion).OrderBy(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 字典明细表列表数据
        /// </summary>
        /// <returns></returns>
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult DetailLists()
        {
            string[] searchKeys = new string[] { "NAME", "CODE", "MRID" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_HKYDIC).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_HKYDIC), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_HKYDIC>());
            var obj = new { draw = Request["draw"], data = SQM_HKYDIC.FindAll(SearchCriterion).OrderByDescending(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 字典保存
        /// </summary>
        /// <param name="postdata">表单数据</param>
        /// <param name="sign">区分新建还是编辑</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SaveDict(string postdata, string sign)
        {
            bool success = true;
            string message = "";
            try
            {
                SQM_WEBCONFIG web = JsonHelper.GetObject<SQM_WEBCONFIG>(postdata);
                string type = web.DICCODE;
                string count = "";
                if (sign == "edit")
                {
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_WEBCONFIG where diccode = '{0}' and rid <> '{1}'", type, web.RID)) + "";
                }
                else
                {
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_WEBCONFIG where diccode = '{0}'", type)) + "";
                }
                if (count != "0")
                {
                    message = "字典代码已经存在！";
                    success = false;
                }
                else
                {
                    web.DoSave();
                }
            }
            catch (Exception ex)
            {
                message = ex + "";
                success = false;
            }
            return Content(new JsonMessage { Success = success, Message = message }.ToString());
        }

        /// <summary>
        /// 字典明细保存
        /// </summary>
        /// <param name="postdata">表单数据</param>
        /// <param name="sign">区分新建还是编辑</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SaveDictDetail(string postdata, string sign)
        {
            bool success = true;
            string message = "";
            try
            {
                SQM_HKYDIC web = JsonHelper.GetObject<SQM_HKYDIC>(postdata);
                string type = web.CODE;
                string count = "";
                if (sign == "edit")
                {
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_HKYDIC where code = '{0}' and mrid = '{2}' and rid <> '{1}'", type, web.RID, web.MRID)) + "";
                }
                else
                {
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_HKYDIC where code = '{0}' and mrid = '{1}'", type, web.MRID)) + "";
                }
                if (count != "0")
                {
                    message = "代码已经存在！";
                    success = false;
                }
                else
                {
                    web.DoSave();
                }
            }
            catch (Exception ex)
            {
                message = ex + "";
                success = false;
            }
            return Content(new JsonMessage { Success = success, Message = message }.ToString());
        }
        /// <summary>
        /// 删除字典
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string id = Request["rowId"] + "";
                SQM_WEBCONFIG ent = SQM_WEBCONFIG.Find(id);
                ent.SIGN = "-1";
                ent.DoUpdate();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteDetail()
        {
            try
            {
                string id = Request["rowId"] + "";
                SQM_HKYDIC ent = SQM_HKYDIC.Find(id);
                ent.DoDelete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }

        public ActionResult GetBaseData(string feecode)
        {
            try
            {
                DataTable dt = DataHelper.QueryDataTable("select distinct calccode as \"id\",calcname as \"text\" from sqm_fee_calc_ref where feecode = '" + feecode + "'");
                return Content(new JsonMessage { Success = true, Data = dt }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false,Message = ex.Message}.ToString());
            }
        }
    }
}

