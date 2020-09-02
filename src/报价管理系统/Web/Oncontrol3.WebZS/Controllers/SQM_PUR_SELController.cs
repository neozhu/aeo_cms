using System;
using Castle.ActiveRecord;
using Aim;
using Aim.Data;
using Aim.Portal.Web;
using System.Web.Mvc;
using System.Data;
using Com.Feiliks.QDM;

namespace Oncontrol3.Web.Controllers
{
    //[AuthorLogin]
    public class SQM_PUR_SELController : BaseController
    {
        public ActionResult Index()
        {
            string sql = @"select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 order by ltrim(OBJID,'0')";
            DataTable Orgdt = DataHelper.QueryDataTable(sql);
            sql = @"select TCET084,TEXTDESC from V_MDM_FEE";
            //sql = @"select distinct FEECODE,FEENAME from SQM_SRV_FEE_CONFIG where FEECATG<>'2'";
            DataTable Feedt = DataHelper.QueryDataTable(sql);
            sql = @"select PRODUCTKEY,SQPRODUCTNAME from SQM_PRD_EXT where SQPRODUCTNAME is not null";
            DataTable Prodt = DataHelper.QueryDataTable(sql);
            sql = @"select SERVICETYPE,SERVICENAME from MDM_SERVICE";
            //sql = @"select distinct SRVCODE,SRVNAME from SQM_SRV_FEE_CONFIG where SRVDISP='1'";
            DataTable Serdt = DataHelper.QueryDataTable(sql);
            ViewBag.OrgData = Orgdt;
            ViewBag.FeeData = Feedt;
            ViewBag.ProData = Prodt;
            ViewBag.SerData = Serdt;
            return View();
        }
        public ActionResult Lists()
        {
            //查询条件拼接
            var businessorg = Request["BUSINESSORG"].ToString();
            string wherestr = "";
            var productkey = Request["PRODUCTKEY"].ToString();
            var servicetype = Request["SERVICETYPE"].ToString();
            var feecode = Request["FEECODE"].ToString();
            var orgrid = Request["ORGRID"].ToString();
            if (businessorg != "")
            {
                wherestr += "AND BUSINESSORG = '" + businessorg + "'";
            }
            if (productkey != "")
            {
                wherestr += " AND PRDCODE = '" + productkey + "'";
            }
            if (servicetype != "")
            {
                wherestr += " AND SRVCODE = '" + servicetype + "'";
            }
            if (feecode != "")
            {
                wherestr += " AND FEECODE = '" + feecode + "'";
            }
            if (orgrid != "")
            {
                wherestr += " AND ORGRID like '%" + orgrid + "%'";
            }
            string sql_from = @" FROM SQM_DJ_PSF sdp 
                where sdp.RID in(select distinct FEECALCID from SQM_MODEDJ_VAL where STATUS='1' and nvl(DJSTATUS,'0')='1')";
            string sql_feild = @"SELECT sdp.RID,sdp.PRDNAME,sdp.PRDCODE,sdp.SRVNAME,sdp.SRVCODE,sdp.FEENAME,sdp.FEECODE,
                sdp.BUSINESSORG,sdp.DJFS,sdp.ORGNAME,sdp.ORGRID,sdp.MODIFYUSER,sdp.MODIFYTIME ";
            string sql_order = @"ORDER BY case when sdp.MODIFYTIME is null then 0 else 1 end desc, sdp.MODIFYTIME desc";
            string sql_page = string.Format(" WHERE RN between {0} and {1} ", (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //设置分页
            string sql = "With DATASET AS( select A.*,ROWNUM As RN from ({0}{1}{2}) A where 1=1 {3}) select * from DATASET ";
            sql = string.Format(sql, sql_feild, sql_from, sql_order, wherestr);
            string sql_all = sql + sql_page;
            //数据数量
            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_all);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        public ActionResult PurIndex()
        {
            try
            {
                string sql = "";
                string minprice = "";
                bool gyl = false;
                bool kyyf = false;
                bool min = false;
                bool gdznum = true;
                bool czfee = false;
                DataTable FCREFdt = null;
                DataTable SEARFCREFdt = null;
                DataTable DJFSdt = null;
                DataTable GDZDATAdt = null;
                string djrid = Request.QueryString["djrid"];
                string gdzkey = Request.QueryString["gdzkey"];
                string gdzrid = Request.QueryString["gdzrid"];
                string djfsrid = Request.QueryString["djfsrid"];
                string djfs = Request.QueryString["djfs"];
                string jtlj = "";
                SQM_DJ_PSF sdp = SQM_DJ_PSF.Find(djrid);
                //限制组织的取值范围，相同产品-服务-费目的同一组织只能有一个定价
                if (sdp.ALONEFEE == "1")
                {
                    sql = string.Format("select ORGRID from SQM_DJ_PSF where FEECODE='{0}' and RID<>'{1}' and ORGRID is not null", sdp.FEECODE, djrid);
                }
                else
                {
                    sql = string.Format("select ORGRID from SQM_DJ_PSF where FEECODE='{0}' and RID<>'{1}' and PRDCODE='{2}' and SRVCODE='{3}' and BUSINESSORG='{4}' and ORGRID is not null", sdp.FEECODE, djrid, sdp.PRDCODE, sdp.SRVCODE, sdp.BUSINESSORG);
                }
                DataTable orgriddt = DataHelper.QueryDataTable(sql);
                string ydjorgrid = "";
                string orgwhere = "";
                foreach (DataRow orgdr in orgriddt.Rows)
                {
                    ydjorgrid += orgdr["ORGRID"].ToString().Replace(",", "','") + "','";
                }
                if (!String.IsNullOrEmpty(ydjorgrid))
                {
                    ydjorgrid = ydjorgrid.TrimEnd('\'').TrimEnd(',');
                    orgwhere = " and RID not in ('" + ydjorgrid + ")";
                }
                string businessorg = sdp.BUSINESSORG;
                if (sdp.FEECODE == "AGNKYF" || sdp.FEECODE == "XGJKYF")
                {
                    kyyf = true;
                }
                if (!String.IsNullOrEmpty(djrid))
                {
                    //定价方式判断
                    sql = @"With DATASET AS(
                           select sfc.RID from SQM_FEE_CALC sfc 
                           left join SQM_DJ_PSF sdf on sfc.FEECODE=sdf.FEECODE
                           where sdf.RID='" + djrid + "') select distinct sfpr.DJFSRID,sfpr.DJFSNAME,sfpr.FSSORT from DATASET t1 left join SQM_FEE_PUR_REF sfpr on t1.RID=sfpr.feerid  and sfpr.STATUS='1' where DJFSRID is not null order by cast(sfpr.FSSORT as int) asc,sfpr.DJFSNAME asc";
                    DJFSdt = DataHelper.QueryDataTable(sql);
                    if (DJFSdt.Rows.Count > 0)
                    {
                        czfee = true;
                    }
                    if (String.IsNullOrEmpty(djfsrid) && DJFSdt.Rows.Count > 0)
                    {
                        djfsrid = DJFSdt.Rows[0]["DJFSRID"].ToString();
                    }
                    //高低值比较判断
                    string wheredjfs = "";
                    string wheregdz = "";
                    if (djfsrid == "" || djfsrid == "undefined")
                    {
                        wheredjfs = " and r.DJFSRID is null";
                        wheregdz = " and r.GDZRID is null";
                        //MIN判断
                        minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + sdp.FEECODE + "'") + "";
                        if (minprice == "1")
                        {
                            min = true;
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT GDZRID,GDZKEY, GDZNAME,FSMIN FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' order by GDZNAME asc", sdp.FEECODE, djfsrid);
                        GDZDATAdt = DataHelper.QueryDataTable(sql);
                        //MIN判断
                        if (GDZDATAdt.Rows.Count > 0)
                        {
                            minprice = GDZDATAdt.Rows[0]["FSMIN"].ToString();
                            if (minprice == "1")
                            {
                                min = true;
                            }
                        }
                        if (String.IsNullOrEmpty(gdzrid) && GDZDATAdt.Rows.Count > 0)
                        {
                            gdzrid = GDZDATAdt.Rows[0]["GDZRID"].ToString();
                            gdzkey = GDZDATAdt.Rows[0]["GDZKEY"].ToString();
                        }
                        wheredjfs = " and r.DJFSRID='" + djfsrid + "'";
                        if (gdzkey == "0" || String.IsNullOrEmpty(gdzkey))
                        {
                            wheregdz = " and r.GDZRID is null";
                        }
                        else
                        {
                            wheregdz = " and r.GDZRID='" + gdzrid + "'";
                            if (GDZDATAdt.Rows.Count < 2)
                            {
                                gdznum = false;
                            }
                        }
                    }
                    sql = @"select distinct r.CALCNAME||'('|| r.SCALE ||')' CALCNAME,r.VALCOL,r.CALCCODE,e.MDMTYPE,e.MDMKEY,e.MDMFIELDNAME,e.MDMLOCTYPE,r.SORD
                        from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.FEECODE=p.FEECODE
                        left join SQM_CALC_BASE_EXT e on r.CALCCODE=e.CALCCODE
                        where r.STATUS='1' and p.Rid='{0}' {1} {2} {3} order by r.SORD asc";
                    string searsql = string.Format(sql, djrid, wheredjfs, wheregdz, " and r.issearch='是' ");
                    string fcrefsql = string.Format(sql, djrid, wheredjfs, wheregdz, " and 1=1 ");
                    SEARFCREFdt = DataHelper.QueryDataTable(searsql);
                    FCREFdt = DataHelper.QueryDataTable(fcrefsql);
                    //if (businessorg == "供应链")
                    //{
                    //    gyl = true;
                    //}

                    if (gdzkey == "0" || String.IsNullOrEmpty(gdzkey))
                    {
                        wheregdz = " and GDZRID is null";
                    }
                    else
                    {
                        wheregdz = " and GDZRID='" + gdzrid + "'";
                    }

                    string jtljsql = "select JTLJ from SQM_FEE_PUR_REF where DJFSRID='" + djfsrid + "' " + wheregdz;
                    jtlj = DataHelper.QueryValue(jtljsql) + "";

                }
                ViewBag.min = min;
                ViewBag.gdznum = gdznum;
                ViewBag.czfee = czfee;
                ViewBag.FCREFData = FCREFdt;
                ViewBag.SEARFCREFData = SEARFCREFdt;
                ViewBag.gyl = gyl;
                ViewBag.kyyf = kyyf;
                ViewBag.djfsrid = djfsrid;
                ViewBag.gdzkey = gdzkey;
                ViewBag.gdzrid = gdzrid;
                ViewBag.DJFSData = DJFSdt;
                ViewBag.GDZDATAdt = GDZDATAdt;
                ViewBag.djfs = djfs;
                sql = string.Format("select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 {0} order by ltrim(OBJID,'0')", orgwhere);
                DataTable dt = DataHelper.QueryDataTable(sql);
                ViewBag.Data = dt;
                ViewBag.jtlj = jtlj;
                return View(sdp);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PurLists()
        {
            string cqcolname = Request["CQCOL"] + "";// 询价使用， 船期 在值表中的位置 
            string sblxcolname = Request["SBLXCOL"] + "";// 询价使用，设备类型代码 在值表中的位置
            string[] searchKeys = new string[] { "COLUMN1", "COLUMN2", "COLUMN3", "COLUMN4", "COLUMN5", "COLUMN6", "COLUMN7", "COLUMN8", "COLUMN9", "COLUMN10", "CALCUNIT", "DJSTATUS", "DJFSRID", "GDZRID" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_MODEDJ_VAL).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else if (key == cqcolname)
                    {
                        string[] arr = Request[key].Trim().Split(',');
                        SearchCriterion.AddSearch(key, arr, Aim.Data.SearchModeEnum.In);
                    }
                    else if (key == sblxcolname)
                    {
                        string[] arr = Request[key].Trim().Split(',');
                        SearchCriterion.AddSearch(key, arr, Aim.Data.SearchModeEnum.In);
                    }
                    else
                    {
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request["FEECALCID"]))
            {
                SearchCriterion.AddSearch("FEECALCID", Request["FEECALCID"], Aim.Data.SearchModeEnum.Equal);
            }
            if (!string.IsNullOrEmpty(Request["STATUS"]))
            {
                SearchCriterion.AddSearch("STATUS", Request["STATUS"], Aim.Data.SearchModeEnum.Equal);
            }
            if (!string.IsNullOrEmpty(Request["STARTDATE"]))
            {
                SearchCriterion.AddSearch("STARTDATE", DateTime.Parse(Request["STARTDATE"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["ENDDATE"]))
            {
                SearchCriterion.AddSearch("ENDDATE", DateTime.Parse(Request["ENDDATE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_MODEDJ_VAL), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_MODEDJ_VAL>());
            var obj = new { draw = Request["draw"], data = SQM_MODEDJ_VAL.FindAll(SearchCriterion), recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
    }
}

