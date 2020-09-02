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
    public partial class SQM_FBREASONController : BaseController
    {
        //
        // GET: /SQM_FBREASON/
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            //查询条件拼接
            string wherestr = "";
            var reasonname = Request["REASONNAME"].ToString();
            if (reasonname != "")
            {
                wherestr += " AND REASONNAME like '%" + reasonname + "%'";
            }
            string sql_from = @"select * from SQM_FBREASON ";
            string sql_order = @"ORDER BY case when MODIFYTIME is null then 0 else 1 end desc, MODIFYTIME desc";
            string sql_page = string.Format(" WHERE RN between {0} and {1} ", (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //设置分页
            string sql = "With DATASET AS( select A.*,ROWNUM As RN from ({0}{1}) A where 1=1 {2}) select * from DATASET ";
            sql = string.Format(sql, sql_from, sql_order, wherestr);
            string sql_all = sql + sql_page;
            //数据数量
            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_all);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 原因保存
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
                SQM_FBREASON web = JsonHelper.GetObject<SQM_FBREASON>(postdata);
                string type = web.REASONCODE;
                web.STATUS = "1";
                string count = "";
                if (sign == "edit")
                {
                    web.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_FBREASON where REASONCODE = '{0}' and RID <> '{1}'", type, web.RID)) + "";
                }
                else
                {
                    web.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    count += DataHelper.QueryValue(string.Format("select count(*) from SQM_FBREASON where REASONCODE = '{0}'", type)) + "";
                }
                if (count != "0")
                {
                    message = "原因代码已经存在！";
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
        //
        // GET: /SQM_FBREASON/Delete/5
        public ActionResult Delete()
        {
            string mes = "";
            try
            {
                string id = Request.QueryString["id"];
                string flag = Request.QueryString["flag"];
                SQM_FBREASON ent = SQM_FBREASON.Find(id);
                if (flag == "0")
                {
                    ent.STATUS = "0";
                    mes = "停用成功！";
                }
                else
                {
                    ent.STATUS = "1";
                    mes = "启用成功！";
                }
                ent.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                ent.DoUpdate();
            }
            catch (Exception ex)
            {
                return Content("出现异常:" + ex.Message);
            }
            return Content(mes);
        }
        [AllowAnonymous]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SQM_FBREASON.TryFind(keyValue);
            return Content(JsonHelper.GetJsonString(data));
        }
    }
}

