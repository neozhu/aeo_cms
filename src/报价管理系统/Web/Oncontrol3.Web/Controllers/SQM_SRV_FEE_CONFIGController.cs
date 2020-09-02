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
    public partial class SQM_SRV_FEE_CONFIGController : BaseController
    {
        //
        // GET: /SQM_SRV_FEE_CONFIG/
        public ActionResult Index()
        {
            string sql = @"select distinct PRODUCTKEY,PRODUCTNAME from MDM_PRODUCT";
            DataTable prodt = DataHelper.QueryDataTable(sql);
            sql = @"select distinct SERVICETYPE,SERVICENAME from MDM_SERVICE";
            DataTable srvdt = DataHelper.QueryDataTable(sql);
            sql = @"select distinct TCET084,TEXTDESC from V_MDM_FEE";
            DataTable feedt = DataHelper.QueryDataTable(sql);
            ViewBag.ProData = prodt;
            ViewBag.SrvData = srvdt;
            ViewBag.FeeData = feedt;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            //查询条件拼接
            string wherestr = "";
            var prodcode = Request["PRODCODE"].ToString();
            var srvcode = Request["SRVCODE"].ToString();
            var feecode = Request["FEECODE"].ToString();
            if (prodcode != "")
            {
                wherestr += " AND PRODCODE = '" + prodcode + "'";
            }
            if (srvcode != "")
            {
                wherestr += " AND SRVCODE = '" + srvcode + "'";
            }
            if (feecode != "")
            {
                wherestr += " AND FEECODE = '" + feecode + "'";
            }
            string sql_from = @"select * from SQM_SRV_FEE_CONFIG ";
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
        //
        // GET: /SQM_SRV_FEE_CONFIG/Create
        public ActionResult Create(string id)
        {
            try
            {
                SQM_SRV_FEE_CONFIG ent = new SQM_SRV_FEE_CONFIG();
                if (!String.IsNullOrEmpty(id))
                {
                    ent = SQM_SRV_FEE_CONFIG.Find(id);
                }
                string sql = @"select distinct PRODUCTKEY,PRODUCTNAME from MDM_PRODUCT";
                DataTable prodt = DataHelper.QueryDataTable(sql);
                sql = @"select distinct SERVICETYPE,SERVICENAME from MDM_SERVICE";
                DataTable srvdt = DataHelper.QueryDataTable(sql);
                sql = @"select distinct TCET084,TEXTDESC from V_MDM_FEE";
                DataTable feedt = DataHelper.QueryDataTable(sql);
                ViewBag.ProData = prodt;
                ViewBag.SrvData = srvdt;
                ViewBag.FeeData = feedt;
                return View("Create", ent);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        // POST: /SQM_SRV_FEE_CONFIG/Create
        [HttpPost]
        public ActionResult Create(SQM_SRV_FEE_CONFIG ent)//多对象form时使用(FormCollection collection)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                string rid = Request["id"].ToString();
                //更新该费目下的所有绑定关系
                DataHelper.ExecSql(string.Format("update SQM_SRV_FEE_CONFIG set ISALONE='{0}' where FEECODE='{1}' and STATUS='1'", ent.ISALONE, ent.FEECODE));
                if (!String.IsNullOrEmpty(rid))
                {
                    SQM_SRV_FEE_CONFIG erd = SQM_SRV_FEE_CONFIG.Find(rid);
                    DataHelper.MergeData<SQM_SRV_FEE_CONFIG>(erd, ent);
                    erd.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    erd.DoUpdate();
                }
                else
                {
                    SQM_SRV_FEE_CONFIG ssfc = new SQM_SRV_FEE_CONFIG();
                    if (!String.IsNullOrEmpty(ent.PRODCODE) && !String.IsNullOrEmpty(ent.SRVCODE))
                    {
                        ssfc = SQM_SRV_FEE_CONFIG.FindFirstByProperties(SQM_SRV_FEE_CONFIG.Prop_PRODCODE, ent.PRODCODE, SQM_SRV_FEE_CONFIG.Prop_SRVCODE, ent.SRVCODE, SQM_SRV_FEE_CONFIG.Prop_FEECODE, ent.FEECODE, SQM_SRV_FEE_CONFIG.Prop_STATUS, "1");
                    }
                    else
                    {
                        ssfc = SQM_SRV_FEE_CONFIG.FindFirstByProperties(SQM_SRV_FEE_CONFIG.Prop_FEECODE, ent.FEECODE, SQM_SRV_FEE_CONFIG.Prop_STATUS, "1");
                    }
                    if (ssfc != null && !String.IsNullOrEmpty(ssfc.RID))
                    {
                        return Content(new JsonMessage { Success = false, Message = "该产品、服务、费目关系已存在，请确认！" }.ToString());
                    }
                    ent.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
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
        //
        // GET: /SQM_SRV_FEE_CONFIG/Delete/5
        public ActionResult Delete()
        {
            string mes = "";
            try
            {
                string id = Request.QueryString["id"];
                string flag = Request.QueryString["flag"];
                SQM_SRV_FEE_CONFIG ent = SQM_SRV_FEE_CONFIG.Find(id);
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
            var data = SQM_SRV_FEE_CONFIG.TryFind(keyValue);
            return Content(JsonHelper.GetJsonString(data));
        }
    }
}

