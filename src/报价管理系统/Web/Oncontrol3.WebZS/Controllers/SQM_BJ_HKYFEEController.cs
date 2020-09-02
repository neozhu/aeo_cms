using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.QDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oncontrol3.Web.ServiceReference1;
using System.Collections;
using Aspose.Cells;
using System.Reflection;
using Com.Feiliks.QDM.Model;
using NHibernate.Criterion;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class SQM_BJ_HKYFEEController : BaseController
    {
        /// <summary>
        /// 获取定价psf表rid
        /// 无定价费目要在定价表新增这个费目
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string CreateDJPSF(DataTable dataTable, string type)
        {
            string djrid = "";
            DataTable dt = new DataTable();
            dt = dataTable.Copy();
            dt.Columns["FEE_CODE"].ColumnName = "FEECODE";
            dt.Columns["FEE_NAME"].ColumnName = "FEENAME";
            dt.Columns["SERVICE_CODE"].ColumnName = "SRVCODE";
            dt.Columns["SERVICE_NAME"].ColumnName = "SRVNAME";
            dt.Columns["PRODUCT_CODE"].ColumnName = "PRDCODE";
            dt.Columns["PRODUCT_NAME"].ColumnName = "PRDNAME";
            SQM_DJ_PSF sdp = TableToEntity<SQM_DJ_PSF>(dt.Rows[0].Table)[0];
            sdp.STATUS = "0";
            if (type == "HY")
            {
                sdp.BUSINESSORG = "海运";
            }
            else if (type == "KY")
            {
                sdp.BUSINESSORG = "空运";
            }
            sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            djrid = Guid.NewGuid().ToString();
            sdp.ORGCODE = djrid;
            sdp.CREATESOURCE = "空海运报价";
            sdp.DoCreate();
            string sql = "update sqm_dj_psf set rid = '" + djrid + "' where orgcode = '" + djrid + "'";
            DataHelper.ExecSql(sql);
            DataHelper.ExecSql("update sqm_dj_psf set orgcode = '' where rid = '" + djrid + "'");
            return djrid;
        }
        [AllowAnonymous]
        public ActionResult SQM_HYYF(string RID)
        {
            string[] rid = JsonHelper.GetObject<string[]>(RID);
            string djrid = rid[0];
            string bjrid = rid[1];
            string sql = "select t1.*,t2.CACLUNIT,t2.PRECOND,t2.RSLBASE,t2.ALLOWCACLOFFER from SQM_BJ_PSF t1 left join SQM_FEE_CALC t2 on t1.FEE_CODE = t2.FEECODE where t1.RID = '" + bjrid + "'";
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (djrid == "")
            {
                if (dt.Rows.Count > 0)
                {
                    djrid = CreateDJPSF(dt, "KY");
                }
            }
            ViewBag.DJRID = djrid;
            ViewBag.BJRID = bjrid;
            string sql_bp = "select bpname from sqm_bj_bp where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string sqm_sj = "select bizname from sqm_bj_biz where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string bpname = DataHelper.QueryValue(sql_bp) + "";
            ViewBag.BPNAME = bpname;
            string bizname = DataHelper.QueryValue(sqm_sj) + "";
            ViewBag.BIZNAME = bizname;
            if (dt != null)
            {
                ViewBag.psfall = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select * from SQM_BJ_PSF where RID = '" + bjrid + "'"));
                ViewBag.PSFDATA = dt;
            }
            return View();
        }
        public ActionResult SQM_KYGNYF(string RID)
        {
            string[] rid = JsonHelper.GetObject<string[]>(RID);
            string djrid = rid[0];
            string bjrid = rid[1];
            string sql = "select t1.*,t2.CACLUNIT,t2.PRECOND,t2.RSLBASE,t2.ALLOWCACLOFFER from SQM_BJ_PSF t1 left join SQM_FEE_CALC t2 on t1.FEE_CODE = t2.FEECODE where t1.RID = '" + bjrid + "'";
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (djrid == "")
            {
                if (dt.Rows.Count > 0)
                {
                    djrid = CreateDJPSF(dt, "KY");
                }
            }
            ViewBag.DJRID = djrid;
            ViewBag.BJRID = bjrid;

            string sql_bp = "select bpname from sqm_bj_bp where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string sqm_sj = "select bizname from sqm_bj_biz where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string bpname = DataHelper.QueryValue(sql_bp) + "";
            ViewBag.BPNAME = bpname;
            string bizname = DataHelper.QueryValue(sqm_sj) + "";
            ViewBag.BIZNAME = bizname;
            if (dt != null)
            {
                ViewBag.psfall = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select * from SQM_BJ_PSF where RID = '" + bjrid + "'"));
                ViewBag.PSFDATA = dt;
            }
            return View();
        }
        public ActionResult SQM_KYGJYF(string RID)
        {
            string[] rid = JsonHelper.GetObject<string[]>(RID);
            string djrid = rid[0];
            string bjrid = rid[1];
            string sql = "select t1.*,t2.CACLUNIT,t2.PRECOND,t2.RSLBASE,t2.ALLOWCACLOFFER from SQM_BJ_PSF t1 left join SQM_FEE_CALC t2 on t1.FEE_CODE = t2.FEECODE where t1.RID = '" + bjrid + "'";
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (djrid == "")
            {
                if(dt.Rows.Count > 0)
                {
                    djrid = CreateDJPSF(dt, "KY");
                }
            }
            ViewBag.DJRID = djrid;
            ViewBag.BJRID = bjrid;

            string sql_bp = "select bpname from sqm_bj_bp where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string sqm_sj = "select bizname from sqm_bj_biz where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string bpname = DataHelper.QueryValue(sql_bp) + "";
            ViewBag.BPNAME = bpname;
            string bizname = DataHelper.QueryValue(sqm_sj) + "";
            ViewBag.BIZNAME = bizname;
            if (dt != null)
            {
                ViewBag.psfall = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select * from SQM_BJ_PSF where RID = '" + bjrid + "'"));
                ViewBag.PSFDATA = dt;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult SelectHKYDJ()
        {
            try
            {
                string ifbj = Request["ifbj"];
                string feeid = Request["rid"];
                string feecode = Request["feecode"];
                string[] unit = Request["unit"].Split(',');
                string bjstatus = Request["status"];
                if (string.IsNullOrEmpty(feeid))
                {
                    feeid = "";
                }
                string sql_val = "";
                for (int i = 0; i < unit.Length; i++)
                {
                    sql_val = SearchSql(feeid, feecode, unit[i], bjstatus, ifbj);
                }
                var total = DataHelper.QueryValue("select count(1) from (" + sql_val + ")");

                string order = !string.IsNullOrEmpty(Request["sort"]) ? Request["sort"] : "CREATETIME";
                string asc = !string.IsNullOrEmpty(Request["order"]) ? Request["order"] : "desc";
                var obj = new { draw = Request["draw"], data = GetPageData(sql_val, order, asc), recordsTotal = total, recordsFiltered = total };
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 空海运拼查询值表的sql（字段是写死的）
        /// </summary>
        /// <param name="feeid">值表的费目id</param>
        /// <param name="feecode">费目代码</param>
        /// <returns></returns>
        public string SearchSql(string feeid, string feecode, string unit, string status, string ifbj)
        {
            string sql_val = "select RID,CREATETIME,FEECALCID";
            DataTable dt = new DataTable();
            string sql_ref = "";
            sql_ref = "select CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where ISCNT = '否' and CACLUNIT = '" + unit + "' and FEECODE = '" + feecode + "'";
            dt = DataHelper.QueryDataTable(sql_ref);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CALCNAME"].ToString().IndexOf("国家") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as GJ";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("起运港") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as QYG";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("目的港") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as MDG";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("船公司") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as CGS";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("箱") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as XX";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("航程") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as HC";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("中转港") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as ZZG";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("开航日") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as KHR";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("码头") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as MT";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("时刻表") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as SKB";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("地区") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as DQ";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("航空公司") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as HKGS";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("重量") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as ZL";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("货物类别") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as HWLB";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("航班号") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as HBH";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                    else if (dr["CALCNAME"].ToString().IndexOf("航线") >= 0)
                    {
                        sql_val += "," + dr["VALCOL"].ToString() + " as HX";
                        sql_val += "," + dr["VALCOL"].ToString();
                    }
                }
            }
            string sql_ifbj = "";
            if (!string.IsNullOrEmpty(ifbj))
            {
                sql_ifbj = " and IFBJITEM = '" + ifbj + "'";
            }
            else
            {
                sql_ifbj = " and (IFBJITEM = '1' or IFBJITEM = '0')";
            }
            if (status == "0")
            {
                sql_val += ",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,CURRENCY,MINPRICE,MAXPRICE,GUIDEPRICE,STATUS,MEMO,SORD,CALCUNIT,CALCCODE,CALCNAME,IFBJITEM,'null' as ZVERSION,'null' as OVERSTATUS,'null' as JSFCODE,BJPRICE,MIN,'null' as CONDITION,'null' as JXJC,'null' as BJFS,'null' as DJRID,SPRICE,1 as \"定价\" from SQM_MODEDJ_VAL where (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "' and CALCUNIT = '" + unit + "'";
            }
            else
            {
                sql_val += ",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,CURRENCY,MINPRICE,MAXPRICE,GUIDEPRICE,STATUS,MEMO,SORD,CALCUNIT,CALCCODE,CALCNAME,IFBJITEM,ZVERSION,OVERSTATUS,JSFCODE,BJPRICE,MINBJPRICE as MIN,CONDITION,JXJC,BJFS,DJRID,SPRICE,1 as \"报价\" from SQM_MODEBJ_VAL where FEECALCID = '" + feeid + "' and CALCUNIT = '" + unit + "'" + sql_ifbj;
            }
            return sql_val;
        }
        /// <summary>
        /// 拼查询值表的sql（通用）
        /// </summary>
        /// <param name="feeid">值表的费目id</param>
        /// <param name="feecode">费目代码</param>
        /// <returns></returns>
        public string SearchSqlAll(string feeid, string feecode, string unit, string status)
        {
            string sql_val = "select RID,CREATETIME,FEECALCID";
            DataTable dt = new DataTable();
            string sql_ref = "";
            sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where ISCNT = '否' and CACLUNIT = '" + unit + "' and FEECODE = '" + feecode + "'";
            dt = DataHelper.QueryDataTable(sql_ref);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sql_val += "," + dr["VALCOL"].ToString() + " as " + dr["CALCCODE"];
                    sql_val += "," + dr["VALCOL"].ToString();
                }
            }
            if (status == "0")
            {
                sql_val += ",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,CURRENCY,MINPRICE,MAXPRICE,GUIDEPRICE,STATUS,MEMO,SORD,CALCUNIT,CALCCODE,CALCNAME,IFBJITEM,MIN,SPRICE,BJPRICE,1 as \"定价\" from SQM_MODEDJ_VAL where (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "' and CALCUNIT = '" + unit + "'";
            }
            else
            {
                sql_val += ",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,CURRENCY,MINPRICE,MAXPRICE,GUIDEPRICE,STATUS,MEMO,SORD,CALCUNIT,CALCCODE,CALCNAME,IFBJITEM,ZVERSION,OVERSTATUS,JSFCODE,BJPRICE,MINBJPRICE,CONDITION,JXJC,BJFS,DJRID from SQM_MODEBJ_VAL where FEECALCID = '" + feeid + "' and CALCUNIT = '" + unit + "'";
            }
            return sql_val;
        }
        /// <summary>
        /// 判断是否需要复制，并返回费目id
        /// </summary>
        /// <returns></returns>
        public ActionResult Duplicate()
        {
            string feecode = Request["feecode"];
            string rid = Request["rid"];
            string vrid = DataHelper.QueryValue("select VRID from SQM_BJ_PSF where RID = '" + rid + "'") + "";
            List<string> rids = new List<string>();
            if (vrid != "")
            {
                IList<EasyDictionary> dictList = DataHelper.QueryDictList("select RID from SQM_BJ_PSF where VRID = '" + vrid + "' and FEE_CODE = '" + feecode + "' and RID <> '" + rid + "'");
                if (dictList.Count == 0)
                {
                    return Content("0");
                }
                else
                {
                    foreach (EasyDictionary easydict in dictList)
                    {
                        rids.Add(easydict.Get("RID").ToString());
                    }
                }
            }
            return Content(JsonHelper.GetJsonString(rids));
        }
        /// <summary>
        /// 海运运费保存/确定
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DoSave()
        {
            string message = "保存成功";
            string ifcopy = Request["ifcopy"]; // 是否复制到其他费目
            string bjstatus = Request["bjstatus"]; // 报价状态
            string feecalcid = Request["feecalcid"];
            string sign = Request["sign"]; // 保存还是确定
            if (sign == "2")
            {
                message = "费目确认成功";
            }
            string feeitems = Request["feeitems"]; // 更新报价费目表数据
            string psfdata = Request["psfdata"];
            string ifbjitem = Request["ifbjitem"];
            bool ifdj = false;
            DataTable dtpsf = JsonHelper.GetObject<DataTable>(feeitems);
            DataTable dtpsfall = JsonHelper.GetObject<DataTable>(psfdata);
            Dictionary<string, string> ifbjitems = JsonHelper.GetObject<Dictionary<string, string>>(ifbjitem);
            string djval = Request["djval"]; // 值表数据（定价值表或者报价值表）
            string idguidprice = Request["idguidprice"];
            DataTable dtDj = new DataTable();
            if (!string.IsNullOrEmpty(djval))
            {
                dtDj = JsonHelper.GetObject<DataTable>(djval);
                // 将“BJPRICE”列转为string列
                dtDj.Columns.Remove("BJPRICE"); // 删除
                dtDj.Columns.Add("BJPRICE", typeof(string)); // 增加
                dtDj.Columns.Add("pricenew");
                if (dtDj.Columns.Contains("定价"))// 定价值表数据
                {
                    ifdj = true;
                }
                if (!string.IsNullOrEmpty(idguidprice))
                {
                    Dictionary<string, string> dc = JsonHelper.GetObject<Dictionary<string, string>>(idguidprice);
                    // 将"报价"插入datatable 
                    for (int i = dtDj.Rows.Count - 1; i >= 0; i--)
                    {
                        dtDj.Rows[i]["BJPRICE"] = dc[dtDj.Rows[i]["RID"].ToString()] + "";
                    }
                    if (ifdj)
                    {
                        dtDj.Columns["RID"].ColumnName = "DJRID";
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["DJRID"].ToString()];
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["RID"].ToString()];
                        }
                    }
                }
            }
            try
            {
                // 1.将dtpsf更新sqm_bj_psf 只有一行，所以直接更新
                foreach (DataColumn col in dtpsf.Columns)
                {
                    dtpsfall.Rows[0][col.ColumnName] = dtpsf.Rows[0][col.ColumnName];
                }
                List<SQM_BJ_PSF> entspsf = TableToEntity<SQM_BJ_PSF>(dtpsfall);
                foreach (SQM_BJ_PSF srcobj in entspsf)
                {
                    if (sign == "1")
                    {
                        srcobj.BJSTATAUS = bjstatus; // 状态更改为保存
                    }
                    else if (sign == "2")
                    {
                        if (bjstatus == "4")
                        {
                            srcobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            srcobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                        
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 2.将dtDj插入sqm_modebj_val
                List<SQM_MODEBJ_VAL> ents = TableToEntity<SQM_MODEBJ_VAL>(dtDj);
                foreach (SQM_MODEBJ_VAL srcobj in ents)
                {
                    srcobj.FEECALCID = dtpsfall.Rows[0]["RID"].ToString();
                    srcobj.STATUS = "1"; // 数据启用，物理删除:status = "0"
                    if (sign == "1")
                    {
                        srcobj.BJSTATUS = "1"; // 状态更改为已保存
                    }
                    else if (sign == "2")
                    {
                        srcobj.BJSTATUS = "2"; // 状态更改为已确认
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 3.复制到其他费目
                if (ifcopy != "no")
                {
                    string[] feeids = JsonHelper.GetObject<string[]>(ifcopy);
                    SQM_MODEBJ_VAL[] smvobjs = SQM_MODEBJ_VAL.FindAll(Expression.Eq(SQM_MODEBJ_VAL.Prop_FEECALCID, feecalcid));
                    for (int i = 0; i < feeids.Length; i++)
                    {
                        // psf 表
                        SQM_BJ_PSF sbfobj = SQM_BJ_PSF.Find(feeids[i]);
                        foreach (DataColumn col in dtpsf.Columns)
                        {
                            foreach (PropertyInfo p in sbfobj.GetType().GetProperties())
                            {
                                if (p.Name == col.ColumnName)
                                {
                                    if (p.Name == "DISCOUNT")
                                    {
                                        sbfobj.SetValue(p.Name, Convert.ToDecimal(dtpsf.Rows[0][col.ColumnName]));
                                    }
                                    else
                                    {
                                        sbfobj.SetValue(p.Name, dtpsf.Rows[0][col.ColumnName]);
                                    }
                                }
                            }
                        }
                        if (bjstatus == "4")
                        {
                            sbfobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            sbfobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                        sbfobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbfobj.DoUpdate();
                        // 值表
                        foreach (SQM_MODEBJ_VAL obj in smvobjs)
                        {
                            obj.FEECALCID = feeids[i];
                            obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            obj.DoCreate();
                        }
                    }
                }
                return Content(new JsonMessage { Message = message, Success = true }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "操作失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 国内空运费保存/确定
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DoSaveKYGN()
        {
            string message = "保存成功";
            string ifcopy = Request["ifcopy"]; // 是否复制到其他费目
            string bjstatus = Request["bjstatus"]; // 报价状态
            string feecalcid = Request["feecalcid"];
            string sign = Request["sign"]; // 保存还是确定
            if (sign == "2")
            {
                message = "费目确认成功";
            }
            string feeitems = Request["feeitems"]; // 更新报价费目表数据
            string psfdata = Request["psfdata"];
            string ifbjitem = Request["ifbjitem"];
            bool ifdj = false;
            DataTable dtpsf = JsonHelper.GetObject<DataTable>(feeitems);
            DataTable dtpsfall = JsonHelper.GetObject<DataTable>(psfdata);
            Dictionary<string, string> ifbjitems = JsonHelper.GetObject<Dictionary<string, string>>(ifbjitem);
            string djval = Request["djval"]; // 值表数据（定价值表或者报价值表）
            string idguidprice = Request["idguidprice"];
            string idminprice = Request["idminprice"];
            DataTable dtDj = new DataTable();  
            if (!string.IsNullOrEmpty(djval))
            {
                dtDj = JsonHelper.GetObject<DataTable>(djval);
                // 将“BJPRICE”列转为string列
                dtDj.Columns.Remove("BJPRICE"); // 删除
                dtDj.Columns.Add("BJPRICE", typeof(string)); // 增加
                dtDj.Columns.Remove("MIN"); // 删除
                dtDj.Columns.Add("MIN", typeof(string)); // 增加
                if (dtDj.Columns.Contains("定价"))// 定价值表数据
                {
                    ifdj = true;
                }
                if (!string.IsNullOrEmpty(idguidprice) && !string.IsNullOrEmpty(idminprice))
                {
                    Dictionary<string, string> dc = JsonHelper.GetObject<Dictionary<string, string>>(idguidprice);
                    Dictionary<string, string> dcmin = JsonHelper.GetObject<Dictionary<string, string>>(idminprice);
                    // 将"报价"插入datatable 删除新增数据（报价已经存在）
                    for (int i = dtDj.Rows.Count - 1; i >= 0; i--)
                    {
                        dtDj.Rows[i]["BJPRICE"] = dc[dtDj.Rows[i]["RID"].ToString()] + "";
                        if (dtDj.Columns.Contains("MIN"))
                        {
                            dtDj.Columns["MIN"].ColumnName = "MINBJPRICE";
                        }
                        dtDj.Rows[i]["MINBJPRICE"] = dcmin[dtDj.Rows[i]["RID"].ToString()] + "";
                    }
                    if (ifdj)
                    {
                        dtDj.Columns["RID"].ColumnName = "DJRID";
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["DJRID"].ToString()];
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["RID"].ToString()];
                        }
                    }
                }
            }
            try
            {
                // 1.将dtpsf更新sqm_bj_psf 只有一行，所以直接更新
                foreach (DataColumn col in dtpsf.Columns)
                {
                    dtpsfall.Rows[0][col.ColumnName] = dtpsf.Rows[0][col.ColumnName];
                }
                List<SQM_BJ_PSF> entspsf = TableToEntity<SQM_BJ_PSF>(dtpsfall);
                foreach (SQM_BJ_PSF srcobj in entspsf)
                {
                    if (sign == "1")
                    {
                        if (sign == "1")
                        {
                            srcobj.BJSTATAUS = bjstatus; // 状态更改为保存
                        }
                        else
                        {
                            srcobj.BJSTATAUS = "1"; // 状态更改为保存
                        }
                    }
                    else if (sign == "2")
                    {
                        if (bjstatus == "4")
                        {
                            srcobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            srcobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 2.将dtDj插入sqm_modebj_val
                List<SQM_MODEBJ_VAL> ents = TableToEntity<SQM_MODEBJ_VAL>(dtDj);
                foreach (SQM_MODEBJ_VAL srcobj in ents)
                {
                    srcobj.FEECALCID = dtpsfall.Rows[0]["RID"].ToString();
                    srcobj.STATUS = "1"; // 数据启用，物理删除:status = "0"
                    if (sign == "1")
                    {
                        srcobj.BJSTATUS = "1"; // 状态更改为已保存
                    }
                    else if (sign == "2")
                    {
                        srcobj.BJSTATUS = "2"; // 状态更改为已确认
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 3.复制到其他费目
                if (ifcopy != "no")
                {
                    string[] feeids = JsonHelper.GetObject<string[]>(ifcopy);
                    SQM_MODEBJ_VAL[] smvobjs = SQM_MODEBJ_VAL.FindAll(Expression.Eq(SQM_MODEBJ_VAL.Prop_FEECALCID, feecalcid));
                    for (int i = 0; i < feeids.Length; i++)
                    {
                        // psf 表
                        SQM_BJ_PSF sbfobj = SQM_BJ_PSF.Find(feeids[i]);
                        foreach (DataColumn col in dtpsf.Columns)
                        {
                            foreach (PropertyInfo p in sbfobj.GetType().GetProperties())
                            {
                                if (p.Name == col.ColumnName)
                                {
                                    if (p.Name == "DISCOUNT")
                                    {
                                        sbfobj.SetValue(p.Name, Convert.ToDecimal(dtpsf.Rows[0][col.ColumnName]));
                                    }
                                    else
                                    {
                                        sbfobj.SetValue(p.Name, dtpsf.Rows[0][col.ColumnName]);
                                    }
                                }
                            }
                        }
                        if (bjstatus == "4")
                        {
                            sbfobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            sbfobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                        sbfobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbfobj.DoUpdate();
                        // 值表
                        foreach (SQM_MODEBJ_VAL obj in smvobjs)
                        {
                            obj.FEECALCID = feeids[i];
                            obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            obj.DoCreate();
                        }
                    }
                }
                return Content(new JsonMessage { Message = message, Success = true }.ToString());
            }
            catch (Exception ex)
            {

                return Content(new JsonMessage { Message = "操作失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 国际空运费保存/确定
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DoSaveKYGJ()
        {
            string message = "保存成功";
            string ifcopy = Request["ifcopy"]; // 是否复制到其他费目
            string bjstatus = Request["bjstatus"]; // 报价状态
            string feecalcid = Request["feecalcid"];
            string sign = Request["sign"]; // 保存还是确定
            if (sign == "2")
            {
                message = "费目确认成功";
            }
            string feeitems = Request["feeitems"]; // 更新报价费目表数据
            string psfdata = Request["psfdata"];
            string ifbjitem = Request["ifbjitem"];
            bool ifdj = false;
            DataTable dtpsf = JsonHelper.GetObject<DataTable>(feeitems);
            DataTable dtpsfall = JsonHelper.GetObject<DataTable>(psfdata);
            Dictionary<string, string> ifbjitems = JsonHelper.GetObject<Dictionary<string, string>>(ifbjitem);
            string djval = Request["djval"]; // 值表数据（定价值表或者报价值表）
            string idguidprice = Request["idguidprice"];
            string idminprice = Request["idminprice"];
            DataTable dtDj = new DataTable();
            if (!string.IsNullOrEmpty(djval))
            {
                dtDj = JsonHelper.GetObject<DataTable>(djval);
                // 将“BJPRICE”列转为string列
                dtDj.Columns.Remove("BJPRICE"); // 删除
                dtDj.Columns.Add("BJPRICE", typeof(string)); // 增加
                dtDj.Columns.Remove("MIN"); // 删除
                dtDj.Columns.Add("MIN", typeof(string)); // 增加
                if (dtDj.Columns.Contains("定价"))// 定价值表数据
                {
                    ifdj = true;
                }
                if (!string.IsNullOrEmpty(idguidprice) && !string.IsNullOrEmpty(idminprice))
                {
                    Dictionary<string, string> dc = JsonHelper.GetObject<Dictionary<string, string>>(idguidprice);
                    Dictionary<string, string> dcmin = JsonHelper.GetObject<Dictionary<string, string>>(idminprice);
                    // 将"报价"插入datatable 删除新增数据（报价已经存在）
                    for (int i = dtDj.Rows.Count - 1; i >= 0; i--)
                    {
                        dtDj.Rows[i]["BJPRICE"] = dc[dtDj.Rows[i]["RID"].ToString()] + "";
                        if (dtDj.Columns.Contains("MIN"))
                        {
                            dtDj.Columns["MIN"].ColumnName = "MINBJPRICE";
                        }
                        dtDj.Rows[i]["MINBJPRICE"] = dcmin[dtDj.Rows[i]["RID"].ToString()] + "";
                    }
                    if (ifdj)
                    {
                        dtDj.Columns["RID"].ColumnName = "DJRID";
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["DJRID"].ToString()];
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dtDj.Rows)
                        {
                            dr["IFBJITEM"] = ifbjitems[dr["RID"].ToString()];
                        }
                    }
                }
            }
            try
            {
                // 1.将dtpsf更新sqm_bj_psf 只有一行，所以直接更新
                foreach (DataColumn col in dtpsf.Columns)
                {
                    dtpsfall.Rows[0][col.ColumnName] = dtpsf.Rows[0][col.ColumnName];
                }
                List<SQM_BJ_PSF> entspsf = TableToEntity<SQM_BJ_PSF>(dtpsfall);
                foreach (SQM_BJ_PSF srcobj in entspsf)
                {
                    if (sign == "1")
                    {
                        if (sign == "1")
                        {
                            srcobj.BJSTATAUS = bjstatus; // 状态更改为保存
                        }
                        else
                        {
                            srcobj.BJSTATAUS = "1"; // 状态更改为保存
                        }
                    }
                    else if (sign == "2")
                    {
                        if (bjstatus == "4")
                        {
                            srcobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            srcobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 2.将dtDj插入sqm_modebj_val
                List<SQM_MODEBJ_VAL> ents = TableToEntity<SQM_MODEBJ_VAL>(dtDj);
                foreach (SQM_MODEBJ_VAL srcobj in ents)
                {
                    srcobj.FEECALCID = dtpsfall.Rows[0]["RID"].ToString();
                    srcobj.STATUS = "1"; // 数据启用，物理删除:status = "0"
                    if (sign == "1")
                    {
                        srcobj.BJSTATUS = "1"; // 状态更改为已保存
                    }
                    else if (sign == "2")
                    {
                        srcobj.BJSTATUS = "2"; // 状态更改为已确认
                    }
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoSave();
                }
                // 3.复制到其他费目
                if (ifcopy != "no")
                {
                    string[] feeids = JsonHelper.GetObject<string[]>(ifcopy);
                    SQM_MODEBJ_VAL[] smvobjs = SQM_MODEBJ_VAL.FindAll(Expression.Eq(SQM_MODEBJ_VAL.Prop_FEECALCID, feecalcid));
                    for (int i = 0; i < feeids.Length; i++)
                    {
                        // psf 表
                        SQM_BJ_PSF sbpobj = SQM_BJ_PSF.Find(feeids[i]);
                        foreach (DataColumn col in dtpsf.Columns)
                        {
                            foreach (PropertyInfo p in sbpobj.GetType().GetProperties())
                            {
                                if (p.Name == col.ColumnName)
                                {
                                    if (p.Name == "DISCOUNT")
                                    {
                                        sbpobj.SetValue(p.Name, Convert.ToDecimal(dtpsf.Rows[0][col.ColumnName]));
                                    }
                                    else
                                    {
                                        sbpobj.SetValue(p.Name, dtpsf.Rows[0][col.ColumnName]);
                                    }
                                }
                            }
                        }
                        if (bjstatus == "4")
                        {
                            sbpobj.BJSTATAUS = "5"; // 状态更改为确认(报价超限)
                        }
                        else
                        {
                            sbpobj.BJSTATAUS = "2"; // 状态更改为确认
                        }
                        sbpobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbpobj.DoUpdate();
                        // 值表
                        foreach (SQM_MODEBJ_VAL obj in smvobjs)
                        {
                            obj.FEECALCID = feeids[i];
                            obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            obj.DoCreate();
                        }
                    }
                }
                return Content(new JsonMessage { Message = message, Success = true }.ToString());
            }
            catch (Exception ex)
            {

                return Content(new JsonMessage { Message = "操作失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExportExcel()
        {
            string DJRID = Request["DJRID"];
            string BJRID = Request["BJRID"];
            string feename = Request["feename"];
            string feecode = Request["feecode"];
            string unit = Request["unit"];
            string[] units = JsonHelper.GetObject<string[]>(unit);
            string status = Request["status"];
            string sql = "";
            EasyDictionary easydict = new EasyDictionary();
            List<DataTable> dtlist = new List<DataTable>();
            try
            {
                for (int i = 0; i < units.Length; i++)
                {
                    if (status == "0")
                    {
                        sql = SearchSqlAll(DJRID, feecode, units[i], "0");
                    }
                    else if (status == "1")
                    {
                        sql = SearchSqlAll(BJRID, feecode, units[i], "1");
                    }
                    else if (status == "2")
                    {
                        sql = SearchSqlAll(BJRID, feecode, units[i], "2");
                    }
                    sql += " order by CREATETIME desc";
                    DataTable dt = DataHelper.QueryDataTable(sql);
                    string sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where ISCNT = '否' and FEECODE = '" + feecode + "' and CACLUNIT ='" + units[i] + "'";
                    easydict = DataHelper.QueryDict(sql_ref);
                    //string a = easydict.Get("ZMDG") + "";//{[ZMDG, 目的港]}
                    // 删除列1：CREATETIME,FEECALCID,STATUS,MEMO,SORD,CALCUNIT,CALCCODE,CALCNAME  
                    if (status == "0")
                    {
                        dt.Columns.Remove("CREATETIME");
                        dt.Columns.Remove("FEECALCID");
                        dt.Columns.Remove("STATUS");
                        dt.Columns.Remove("MEMO");
                        dt.Columns.Remove("SORD");
                        dt.Columns.Remove("CALCCODE");
                        dt.Columns.Remove("CALCNAME");
                        dt.Columns.Add("BJPRICE");
                    }
                    // 删除列2：ZVERSION,OVERSTATUS,JSFCODE,BJPRICE,MINBJPRICE,CONDITION,JXJC,DJRID +1
                    else
                    {
                        dt.Columns.Remove("CREATETIME");
                        dt.Columns.Remove("FEECALCID");
                        dt.Columns.Remove("STATUS");
                        dt.Columns.Remove("MEMO");
                        dt.Columns.Remove("SORD");
                        dt.Columns.Remove("CALCCODE");
                        dt.Columns.Remove("CALCNAME");
                        dt.Columns.Remove("ZVERSION");
                        dt.Columns.Remove("OVERSTATUS");
                        dt.Columns.Remove("JSFCODE");
                        dt.Columns.Remove("CONDITION");
                        dt.Columns.Remove("JXJC");
                        dt.Columns.Remove("BJFS");
                        dt.Columns.Remove("DJRID");
                    }
                    dtlist.Add(dt);
                }

                // 创建Excel
                Workbook workbook = new Workbook();
                #region   样式
                // 设置背景颜色
                //style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                //style.Pattern = BackgroundType.Solid;
                //为标题设置样式    
                Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
                styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                styleTitle.Font.Name = "宋体";//文字字体
                styleTitle.Font.Size = 10;//文字大小
                styleTitle.Font.IsBold = true;//粗体 
                styleTitle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                styleTitle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                styleTitle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                styleTitle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                styleTitle.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                styleTitle.Pattern = BackgroundType.Solid;

                //内容样式1
                Style styleContent = workbook.Styles[workbook.Styles.Add()];
                styleContent.Font.Name = "宋体";
                styleContent.Font.Size = 10;
                //styleContent.IsTextWrapped = true;//单元格内容自动换行
                styleContent.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                styleContent.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                styleContent.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                styleContent.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                //内容样式2
                Style styleContent2 = workbook.Styles[workbook.Styles.Add()];
                styleContent2.Font.Name = "宋体";
                styleContent2.Font.Size = 10;
                styleContent2.Font.IsBold = true;//粗体
                styleContent2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                //styleContent.IsTextWrapped = true;//单元格内容自动换行
                styleContent2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                styleContent2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                styleContent2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                styleContent2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                #endregion

                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.Name = feename + "-" + feecode;
                int rowIndex = 0;
                foreach (DataTable dt in dtlist)
                {

                    int cell = 0;
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName.IndexOf("COLUMN") < 0 && col.ColumnName.IndexOf("RN") < 0)
                        {
                            if (col.ColumnName == "STARTDATE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("起始日期");
                            }
                            else if (col.ColumnName == "ENDDATE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("截止日期");
                            }
                            else if (col.ColumnName == "MINPRICE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("最低价");
                            }
                            else if (col.ColumnName == "MAXPRICE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("最高价");
                            }
                            else if (col.ColumnName == "GUIDEPRICE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("指导价");
                            }
                            else if (col.ColumnName == "RID")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("RID");
                            }
                            else if (col.ColumnName == "CALCUNIT")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("报价单位");
                            }
                            else if (col.ColumnName == "BJPRICE")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("报价");
                            }
                            else if (col.ColumnName == "IFBJITEM")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("是否报价");
                            }
                            else if (col.ColumnName == "CURRENCY")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("币种");
                            }
                            else if (col.ColumnName == "MIN")
                            {
                                worksheet.Cells[rowIndex, cell].PutValue("MIN");
                            }
                            else
                            {
                                worksheet.Cells[rowIndex, cell].PutValue(easydict.Get(col.ColumnName));
                            }
                            worksheet.Cells[rowIndex, cell].SetStyle(styleTitle);
                            cell++;
                        }
                    }
                    rowIndex++;
                    foreach (DataRow row in dt.Rows)
                    {
                        int colIndex = 0;
                        foreach (DataColumn col in row.Table.Columns)
                        {
                            if (col.ColumnName.IndexOf("COLUMN") < 0 && col.ColumnName.IndexOf("RN") < 0)
                            {
                                if (col.ColumnName == "IFBJITEM")
                                {
                                    string value = "";
                                    if (row[col.ColumnName].ToString() == "0")
                                    {
                                        value = "否";
                                    }
                                    else if (row[col.ColumnName].ToString() == "1")
                                    {
                                        value = "是";
                                    }
                                    else
                                    {
                                        value = "待定";
                                    }
                                    worksheet.Cells[rowIndex, colIndex].PutValue(value);
                                    worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent2);
                                    colIndex++;
                                }
                                else if (col.ColumnName == "BJPRICE")
                                {
                                    if (row[col.ColumnName].ToString() == "" || row[col.ColumnName].ToString() == "0")
                                    {
                                        if (row["GUIDEPRICE"] + "" == "0" || row["GUIDEPRICE"] + "" == "")
                                        {
                                            worksheet.Cells[rowIndex, colIndex].PutValue("");
                                        }
                                        else
                                        {
                                            worksheet.Cells[rowIndex, colIndex].PutValue(row["GUIDEPRICE"]);
                                        }
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue(row["BJPRICE"]);
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                }
                                else if (col.ColumnName == "MIN")
                                {
                                    if (row[col.ColumnName].ToString() == "" || row[col.ColumnName].ToString() == "0")
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue("");
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue(row["MIN"]);
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                }
                                else if (col.ColumnName == "GUIDEPRICE")
                                {
                                    if (row[col.ColumnName].ToString() == "" || row[col.ColumnName].ToString() == "0")
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue("");
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue(row["GUIDEPRICE"]);
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                }
                                else if (col.ColumnName == "MINPRICE")
                                {
                                    if (row[col.ColumnName].ToString() == "" || row[col.ColumnName].ToString() == "0")
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue("");
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue(row["MINPRICE"]);
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                }
                                else if (col.ColumnName == "MAXPRICE")
                                {
                                    if (row[col.ColumnName].ToString() == "" || row[col.ColumnName].ToString() == "0")
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue("");
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, colIndex].PutValue(row["MAXPRICE"]);
                                        worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                        colIndex++;
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, colIndex].PutValue(row[col.ColumnName].ToString());
                                    worksheet.Cells[rowIndex, colIndex].SetStyle(styleContent);
                                    colIndex++;
                                }
                            }
                        }
                        rowIndex++;
                    }
                    worksheet.Cells[rowIndex, 0].PutValue("");
                    rowIndex++;
                }

                worksheet.AutoFitColumns();
                string newXls = feename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string filePath = System.IO.Path.Combine(Server.MapPath("/Excel/output/"), newXls);
                workbook.Save(filePath);
                return Content(new JsonMessage { Message = "/Excel/output/" + newXls, Success = true }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "导出失败：" + ex.Message, Success = true }.ToString());
            }
        }
        public List<string> ImportCheck(List<string> oldrids, List<string> newrids)
        {
            List<string> rids = new List<string>();
            // 是否重复，返回重复rids
            for (var i = 0; i < newrids.Count; i++)
            {
                var temp = newrids[i];
                var count = 0;
                for (var j = i + 1; j < newrids.Count; j++)
                {
                    if (temp == newrids[j])
                    {
                        count++;
                    }
                }
                if (count == 1)
                {
                    rids.Add(newrids[i]);
                }
            }
            if (rids.Count > 0)
            {
                rids.Add("重复");
                return rids;
            }
            // 是否减少，返回减少rids
            if (newrids.Count < oldrids.Count)
            {
                for (var i = 0; i < oldrids.Count; i++)
                {
                    var temp = oldrids[i];
                    var count = 0;
                    for (var j = 0; j < newrids.Count; j++)
                    {
                        if (temp == newrids[j])
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        rids.Add(temp);
                    }
                }
                rids.Add("减少");
            }
            return rids;
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostExcelData()
        {
            string DJRID = Request["DJRID"];
            string BJRID = Request["BJRID"];
            string status = Request["status"];
            string feecode = Request["feecode"];
            string unit = Request["unit"];
            string[] units = JsonHelper.GetObject<string[]>(unit);
            string sql = "";
            string feecalcid = "";
            //EasyDictionary easydict = new EasyDictionary();
            List<DataTable> dtlist = new List<DataTable>();
            List<DataTable> dtnewlist = new List<DataTable>();

            // 获取原始数据
            for (int i = 0; i < units.Length; i++)
            {
                if (status == "0")
                {
                    feecalcid = DJRID;
                    sql = SearchSqlAll(DJRID, feecode, units[i], "0");
                }
                else if (status == "1")
                {
                    feecalcid = BJRID;
                    sql = SearchSqlAll(BJRID, feecode, units[i], "1");
                }
                else if (status == "2")
                {
                    feecalcid = BJRID;
                    sql = SearchSqlAll(BJRID, feecode, units[i], "2");
                }
                sql += " order by CREATETIME desc";
                DataTable dt = DataHelper.QueryDataTable(sql);
                dtlist.Add(dt);
            }
            DataSet sheetset = new DataSet();
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    // 获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    System.IO.Stream stream = file.InputStream;
                    ArrayList al = new ArrayList();
                    al = GetDataFromExcel(stream);
                    if (al.Count > 1)// 初步校验结果
                    {
                        sheetset = (DataSet)al[0];
                        // 获取sheet名称
                        string sheetName = sheetset.DataSetName;
                        // 获取校验结果
                        string result = al[1].ToString();
                        if (result == "数据为空")
                        {
                            return Content(new JsonMessage { Code = "1", Message = "导入失败：sheet表\"" + sheetName + "\" 数据为空" }.ToString());
                        }
                        else if (result.IndexOf(",") >= 0)
                        {
                            return Content(new JsonMessage { Code = "1", Message = "导入失败：sheet表\"" + sheetName + "\" 行" + result.Split(',')[0] + "列" + result.Split(',')[1] + " " + result.Split(',')[2] }.ToString());
                        }
                        else
                        {
                            return Content(new JsonMessage { Code = "1", Message = "sheet表\"" + sheetName + "\"导入失败" }.ToString());
                        }
                    }
                    else
                    {
                        sheetset = (DataSet)al[0];
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        // 导入之前先判断RID是否重复、是否减少
                        List<string> oldrids = new List<string>();
                        List<string> newrids = new List<string>();
                        foreach (DataTable dt in dtlist)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    oldrids.Add(dr["RID"].ToString());
                                }
                            }
                        }
                        foreach (DataTable dt in sheetset.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    newrids.Add(dr["RID"].ToString());
                                }
                            }
                        }
                        List<string> rids = ImportCheck(oldrids, newrids);
                        if (rids.Count > 0)
                        {
                            if (rids[rids.Count - 1] == "重复")
                            {
                                return Content(new JsonMessage { Message = "导入失败：RID值违反唯一性", Code = "1" }.ToString());
                            }
                        }
                        foreach (DataTable dt in dtlist)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (rids.Count > 0)
                                    {
                                        // 遍历减少的rid
                                        if (rids[rids.Count - 1] == "减少")
                                        {
                                            for (int x = 0; x < rids.Count; x++)
                                            {
                                                // 删除列，是否报价 -> 否
                                                if (dt.Rows[i]["RID"].ToString() == rids[x])
                                                {
                                                    dt.Rows[i]["IFBJITEM"] = "0";
                                                }
                                            }
                                        }
                                    }
                                    // 遍历导入数据
                                    foreach (DataTable dtres in sheetset.Tables)
                                    {
                                        if (dtres.Rows.Count > 0)
                                        {
                                            foreach (DataRow drres in dtres.Rows)
                                            {
                                                // 如果匹配原始数据
                                                if (dt.Rows[i]["RID"].ToString() == drres["RID"].ToString())
                                                {
                                                    dt.Rows[i]["BJPRICE"] = Convert.ToDecimal(drres["报价"] + "");
                                                    dt.Rows[i]["MINBJPRICE"] = Convert.ToDecimal(drres["最低报价"] + "0");
                                                    if (drres["是否报价"] + "" == "是")
                                                    {
                                                        dt.Rows[i]["IFBJITEM"] = "1";
                                                    }
                                                    else if (drres["是否报价"] + "" == "否")
                                                    {
                                                        dt.Rows[i]["IFBJITEM"] = "0";
                                                    }
                                                }
                                                // 导入增加数据
                                                if (drres["RID"] + "" == "")
                                                {
                                                    if (drres["报价单位"] + "" == "")
                                                    {
                                                        return Content(new JsonMessage { Message = "导入失败,新增数据报价单位为空", Code = "1" }.ToString());
                                                    }
                                                    if (dic.Count == 0)// 只执行一次
                                                    {
                                                        // 获取关系 
                                                        IList<EasyDictionary> easydicList = DataHelper.QueryDictList("select distinct CALCNAME,VALCOL from sqm_fee_calc_ref where FEECODE = '" + feecode + "' and CACLUNIT = '" + drres["报价单位"].ToString() + "'");
                                                        foreach (DataColumn col in drres.Table.Columns)
                                                        {
                                                            foreach (EasyDictionary easydic in easydicList)
                                                            {
                                                                if (col.ColumnName == easydic.Get("CALCNAME").ToString())
                                                                {
                                                                    dic.Add(easydic.Get("VALCOL").ToString(), drres[col.ColumnName] + "");
                                                                }
                                                            }
                                                        }
                                                        dic.Add("CALCUNIT", drres["报价单位"] + "");
                                                        dic.Add("STARTDATE", drres["起始日期"] + "");
                                                        dic.Add("ENDDATE", drres["截止日期"] + "");
                                                        dic.Add("CURRENCY", drres["币种"] + "");
                                                        dic.Add("IFBJITEM", drres["是否报价"] + "");
                                                        dic.Add("BJPRICE", drres["报价"] + "");
                                                        dic.Add("MINBJPRICE", drres["最低报价"] + "");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // 是否数据新增
                                if (dic.Count > 0)
                                {
                                    PurSave(JsonHelper.GetJsonString(dic), DJRID, BJRID,"0");// ifdj ： 0-报价，1-定价
                                }

                                List<SQM_MODEBJ_VAL> ents = TableToEntity<SQM_MODEBJ_VAL>(dt);
                                foreach (SQM_MODEBJ_VAL srcobj in ents)
                                {
                                    srcobj.FEECALCID = feecalcid;
                                    srcobj.STATUS = "1";
                                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                    srcobj.DoSave();
                                }
                            }
                        }
                        return Content(new JsonMessage { Message = "导入成功", Code = "0" }.ToString());
                    }
                }
                else
                {
                    return Content(new JsonMessage { Message = "导入失败,文件不存在", Code = "1" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "导入异常：" + ex.Message, Code = "2" }.ToString());
            }
        }
        public static DataTable dataTable;
        public static DataRow dataRow;
        private ArrayList GetDataFromExcel(System.IO.Stream stream)
        {
            ArrayList al = new ArrayList();
            Cells cells;
            Workbook workbook = new Workbook(stream);
            DataSet excel_ds = new DataSet();
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                excel_ds = new DataSet(workbook.Worksheets[i].Name); //创建数据集
                cells = workbook.Worksheets[i].Cells;
                int rownumber = cells.MaxDataRow;
                string rownum = String.Empty;
                string colnum = String.Empty;

                int maxdatarow = cells.MaxDataRow;
                // 从第0行开始读取Excel，将标题读到DataTable中作为列标题
                for (int k = 0; k < cells.MaxDataRow + 1; k++)
                {
                    bool titleRow = false;
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        // 记录位置
                        rownum = (k + 1) + "";
                        colnum = (j + 1) + "";
                        string cellStr = cells[k, j].StringValue.Trim();
                        // 判断是否标题行
                        if (j == 0 && cellStr == "RID")
                        {
                            titleRow = true;
                            if (k == 0)
                            {
                                dataTable = new DataTable();
                            }
                            else
                            {
                                DataTable dtnew = dataTable.Copy(); // 跟datarow一样，datatable也不能同时存进一个dataset（地址相同）
                                excel_ds.Tables.Add(dtnew);
                                dataTable = new DataTable();
                            }
                            dataRow = dataTable.NewRow();
                        }
                        if (titleRow)
                        {
                            dataTable.Columns.Add(cellStr);
                        }
                        else
                        {
                            // 判断整行是否为空
                            int count = 0;
                            if (j == 0)
                            {
                                for (int col = 0; col < cells.MaxDataColumn + 1; col++)
                                {
                                    if (cells[k, col].StringValue.Trim() == "")
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (count != (cells.MaxDataColumn + 1))
                            {
                                dataRow[j] = cellStr;
                            }
                            else
                            {
                                dataRow[0] = null;
                            }
                        }
                    }
                    if (!dataRow.IsNull(0))
                    {
                        DataRow drnew = dataTable.NewRow();
                        drnew.ItemArray = dataRow.ItemArray;
                        dataTable.Rows.Add(drnew);
                    }
                    if (k == cells.MaxDataRow)// 如果是最后一行，把最后一个dataTable添加进dataSet中
                    {
                        DataTable dtnew = dataTable.Copy();
                        excel_ds.Tables.Add(dtnew);
                    }
                }
            }
            al.Add(excel_ds);
            return al;
        }
        /// <summary>
        /// 分页 
        /// </summary>
        /// <param name="tempsql"></param>
        /// <returns></returns>
        [AllowAnonymous]
        private IList<EasyDictionary> GetPageData(string tempsql, string order, string asc)
        {
            SearchCriterion.RecordCount = int.Parse(Convert.ToString(DataHelper.QueryValue("select count(1) from (" + tempsql + ")")));
            string sql_page = @"with m1 as(select a.*,rownum as rn from ({0}) a order by {1} {2}) select * from m1 where rn between {3} and {4}";
            sql_page = string.Format(sql_page, tempsql, order, asc, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            return DataHelper.QueryDictList(sql_page);
        }
        /// <summary>
        /// DataTable转换成实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    try
                    {
                        if (p.GetSetMethod() != null) // 包含set方法的属性执行赋值
                        {
                            if (dt.Columns.Contains(p.Name))// DataTable是否包含该属性
                            {
                                //if (row[p.Name] is Int64)
                                //{
                                //    p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                                //    continue;
                                //}
                                if (p.PropertyType.FullName.ToString().IndexOf("String") >= 0)
                                {
                                    p.SetValue(entity, row[p.Name].ToString() == "" ? null : row[p.Name], null);
                                }
                                else if (p.PropertyType.FullName.ToString().IndexOf("DateTime") >= 0)
                                {
                                    if (row[p.Name].ToString() == "")
                                    {
                                        p.SetValue(entity, null, null);
                                    }
                                    else
                                    {
                                        p.SetValue(entity, Convert.ToDateTime(row[p.Name]), null);
                                    }
                                }
                                else if (p.PropertyType.FullName.ToString().IndexOf("Decimal") >= 0)
                                {
                                    p.SetValue(entity, Convert.ToDecimal(row[p.Name].ToString() == "" ? null : row[p.Name]), null);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                list.Add(entity);
            }
            return list;
        }
        [AllowAnonymous]
        public ActionResult DetailAdd()
        {
            try
            {
                bool gyl = false;
                bool khy = false;
                bool kyyf = false;
                DataTable FCREFdt = null;
                DataTable UNITdt = null;
                DataTable JSJCdt = null;
                DataTable DJFSdt = null;
                string id = Request.QueryString["id"];
                string djrid = Request.QueryString["djrid"];
                string bjrid = Request.QueryString["bjrid"];
                string ifdj = Request.QueryString["ifdj"];
                ViewBag.DJRID = djrid;
                ViewBag.BJRID = bjrid;
                ViewBag.IFDJ = ifdj;
                string calcunit = Request.QueryString["calcunit"];
                string djfsrid = Request.QueryString["djfsrid"];
                SQM_DJ_PSF sdp = SQM_DJ_PSF.Find(djrid);
                string businessorg = sdp.BUSINESSORG;
                if (sdp.FEECODE == "OHYF" || sdp.FEECODE == "AGNKYF" || sdp.FEECODE == "XGJKYF")
                {
                    khy = true;
                }
                if (sdp.FEECODE == "AGNKYF" || sdp.FEECODE == "XGJKYF")
                {
                    kyyf = true;
                }
                if (!String.IsNullOrEmpty(djrid))
                {
                    string sql = @"select r.CACLUNIT from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.feecode=p.feecode
                        where p.Rid='" + djrid + "' group by CACLUNIT";
                    UNITdt = DataHelper.QueryDataTable(sql);
                    if (String.IsNullOrEmpty(calcunit) && UNITdt.Rows.Count > 0)
                    {
                        calcunit = UNITdt.Rows[0]["CACLUNIT"].ToString();
                    }
                    sql = @"With DATASET AS(
                           select sfc.RID from SQM_FEE_CALC sfc
                           left join SQM_DJ_PSF sdf on sfc.FEECODE=sdf.FEECODE
                           where sdf.RID='" + djrid + "' and sfc.CACLUNIT='" + calcunit + "') select sfpr.DJFSRID,sfpr.DJFSNAME from DATASET t1 left join SQM_FEE_PUR_REF sfpr on t1.RID=sfpr.feerid  and sfpr.STATUS='1'";
                    DJFSdt = DataHelper.QueryDataTable(sql);
                    if (String.IsNullOrEmpty(djfsrid) && DJFSdt.Rows.Count > 0)
                    {
                        djfsrid = DJFSdt.Rows[0]["DJFSRID"].ToString();
                    }
                    if (djfsrid == "" || djfsrid == "undefined")
                    {
                        sql = @"select r.CALCNAME,r.VALCOL,r.CALCCODE,e.MDMTYPE,e.MDMKEY,e.MDMFIELDNAME from SQM_FEE_CALC_REF r
                            left join SQM_DJ_PSF p on r.FEECODE=p.FEECODE
                            left join SQM_CALC_BASE_EXT e on r.CALCCODE=e.CALCCODE
                            where p.Rid='" + djrid + "' and r.ISCNT='否' and r.CACLUNIT='" + calcunit + "' and r.STATUS='1' order by r.SORD asc";
                    }
                    else
                    {
                        sql = @"select r.CALCNAME,r.VALCOL,r.CALCCODE,e.MDMTYPE,e.MDMKEY,e.MDMFIELDNAME from SQM_FEE_CALC_REF r
                            left join SQM_DJ_PSF p on r.FEECODE=p.FEECODE
                            left join SQM_CALC_BASE_EXT e on r.CALCCODE=e.CALCCODE
                            where p.Rid='" + djrid + "' and r.ISCNT='否' and r.CACLUNIT='" + calcunit + "' and r.djfsrid='" + djfsrid + "' and r.STATUS='1' order by r.SORD asc";
                    }
                    FCREFdt = DataHelper.QueryDataTable(sql);
                    if (businessorg == "供应链")
                    {
                        sql = @"select r.CALCCODE,r.CALCNAME from SQM_FEE_CALC_REF r
                            left join SQM_DJ_PSF p on r.feecode=p.feecode
                            where p.Rid='" + djrid + "' and r.ISCNT='是'group by CALCCODE,CALCNAME order by CALCNAME asc";
                        JSJCdt = DataHelper.QueryDataTable(sql);
                        gyl = true;
                    }
                }
                ViewBag.gyl = gyl;
                ViewBag.khy = khy;
                ViewBag.kyyf = kyyf;
                ViewBag.tbtitle = sdp.FEENAME;
                ViewBag.FCREFData = FCREFdt;
                ViewBag.djfsrid = djfsrid;
                ViewBag.UNITData = UNITdt;
                ViewBag.DJFSData = DJFSdt;
                ViewBag.calcunit = calcunit;
                ViewBag.JSJCdt = JSJCdt;
                ViewBag.djrid = djrid;
                if (!String.IsNullOrEmpty(id))
                {
                    SQM_MODEDJ_VAL smv = SQM_MODEDJ_VAL.Find(id);
                    return View("DetailAdd", smv);
                }
                else
                {
                    SQM_MODEDJ_VAL smv = new SQM_MODEDJ_VAL();
                    return View("DetailAdd", smv);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 明细保存，先入定价值表，再入报价值表
        /// </summary>
        /// <param name="postdata">新增数据</param>
        /// <param name="rid"></param>
        /// <param name="djrid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PurSave(string postdata, string djrid, string bjrid,string ifdj)
        {
            SQM_PURController sp = new SQM_PURController();
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                SQM_MODEDJ_VAL oldsmv = new SQM_MODEDJ_VAL();
                SQM_MODEDJ_VAL smv = new SQM_MODEDJ_VAL();
                SQM_MODEBJ_VAL bjobj = new SQM_MODEBJ_VAL();
                smv = JsonHelper.GetObject<SQM_MODEDJ_VAL>(postdata);
                string calcname = DataHelper.QueryValue("select DESCRIPTION from MDM_CALC_BASE where CALC_BASE='" + smv.CALCCODE + "'") + "";
                smv.CALCNAME = calcname;
                smv.FEECALCID = djrid;
                DateTime startDate = (DateTime)smv.STARTDATE;
                DateTime endDate = (DateTime)smv.ENDDATE;
                string[] primaryKeys = sp.getPrimaryKeys(djrid, null, smv.CALCUNIT);
                // 获取原始数据
                DataTable dt = sp.FindSourceData(smv, primaryKeys);
                if (dt.Rows.Count > 0)
                {
                    DataRow[] rows = dt.Select("1=1");
                    foreach (DataRow row in rows)
                    {
                        //处理时间交叉
                        if ((startDate > (DateTime)row["STARTDATE"] && startDate <= (DateTime)row["ENDDATE"]) || (endDate >= (DateTime)row["STARTDATE"] && endDate < (DateTime)row["ENDDATE"]))
                        {
                            return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请返回编辑修改！" }.ToString());
                        }
                        else if (startDate <= (DateTime)row["STARTDATE"] && endDate >= (DateTime)row["ENDDATE"])
                        {
                            SQM_MODEDJ_VAL sxsmv = SQM_MODEDJ_VAL.Find(row["RID"]);
                            if (sxsmv.DJSTATUS == "0")
                            {
                                sxsmv.STATUS = "0";
                                sxsmv.DoUpdate();
                            }
                            else if (sxsmv.DJSTATUS == "1")
                            {
                                return Content(new JsonMessage { Success = false, Message = "所选时间区间存在已发布的定价，请返回编辑修改！" }.ToString());
                            }
                        }
                    }
                }
                oldsmv = smv;
                oldsmv.DJSTATUS = "0";// 未发布
                // 定价值表rid生成是为了插入到报价值表
                string guid = System.Guid.NewGuid().ToString();
                oldsmv.IFBJITEM = guid;
                oldsmv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                oldsmv.DoSave();
                DataHelper.ExecSql("update sqm_modedj_val set rid = '" + guid + "' where ifbjitem = '" + guid + "'");
                if(ifdj == "0")
                {
                    // 报价值表插数
                    bjobj = JsonHelper.GetObject<SQM_MODEBJ_VAL>(postdata);
                    bjobj.DJRID = guid;
                    bjobj.FEECALCID = bjrid;
                    bjobj.IFBJITEM = "1";
                    bjobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    bjobj.DoSave();
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
                return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
            }
            //return View();
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 主数据校验
        /// </summary>
        /// <param name="value">校验字段值</param>
        /// <param name="type">校验字段类型：国家、港口</param>
        /// <returns></returns>
        public string MainDataExist(string value, string type)
        {
            string code = "";
            if (type == "1")
            {
                string gjdm = "T005T";
                string columnName = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LANDX'").ToString();
                string columnCode = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LAND1'").ToString();
                // 语言 '1'：中文  'E'：英文 现要求英文大写
                //string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + gjdm + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = 'E'";
                string sql = string.Format("SELECT distinct {3} FROM MDM_MIAN_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {3} = '{2}')", gjdm, columnName, value, columnCode);
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            else if (type == "2")
            {
                string sql = "select distinct locno from MDM_LOC where DESCR40 = '" + value.ToLower() + "' or DESCR40 = '" + value.ToUpper() + "' or DESCR40 = '" + value + "'" + "' or LOCNO = '" + value.ToUpper() + "'" + "' or LOCNO = '" + value.ToLower() + "'" + "' or LOCNO = '" + value + "'";
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            else if (type == "3")
            {
                //code = true;
            }
            else if (type == "4")// 船公司
            {
                code = "cgs";
            }
            else if (type == "5")// 码头
            {
                code = "mt";
            }
            else if (type == "6")
            {
                //code = true;
            }
            return code;
        }
        /// <summary>
        /// 当DataTable中有值时，是不允许修改列的DataType
        /// 修改数据表DataTable某一列的数据类型和记录值
        /// </summary>
        /// <param name="argDataTable">数据表DataTable</param>
        /// <returns>数据表DataTable</returns>
        private DataTable UpdateDataTable(DataTable argDataTable)
        {
            DataTable dtResult = new DataTable();
            //克隆表结构
            dtResult = argDataTable.Clone();
            //修改数据列类型
            foreach (DataColumn col in dtResult.Columns)
            {
                if (col.ColumnName == "BJPRICE")
                {
                    col.DataType = typeof(String);
                }

                if (col.ColumnName == "HasBumiputera")
                {
                    col.DataType = typeof(String);
                }
            }
            foreach (DataRow row in argDataTable.Rows)
            {
                DataRow rowNew = dtResult.NewRow();
                rowNew["A"] = row["A"];
                rowNew["B"] = row["B"];
                rowNew["C"] = row["C"];
                rowNew["D"] = row["D"];
                rowNew["E"] = row["E"];
                rowNew["F"] = row["F"];
            }
            return dtResult;
        }
    }
}
