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
using NHibernate.Criterion;
using OnControl.Web;
using System.Reflection;
using OnControl.Model;
using Com.Feiliks.QDM.Model;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Net.Mail;
using Com.Feiliks.MDM;
using Oncontrol3.Web.Models;
using System.Linq;

namespace Oncontrol3.Web.Controllers
{
    //[AuthorLogin]
    public class SQM_PURController : BaseController
    {
        //private static DataTable NameDt = new DataTable();
        private Style getStyle(string stylestr)
        {
            Workbook workbook = new Workbook();
            Style style = new Style();
            switch (stylestr)
            {
                case "styleTitle1":
                    Style styleTitle1 = workbook.Styles[workbook.Styles.Add()];
                    styleTitle1.HorizontalAlignment = TextAlignmentType.Center;
                    styleTitle1.Font.Name = "微软雅黑";
                    styleTitle1.Font.Size = 22;
                    styleTitle1.Font.IsBold = true;
                    styleTitle1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleTitle1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleTitle1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleTitle1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    style = styleTitle1;
                    break;
                case "styleTitle2":
                    Style styleTitle2 = workbook.Styles[workbook.Styles.Add()];
                    styleTitle2.HorizontalAlignment = TextAlignmentType.Center;
                    styleTitle2.Font.Name = "Arial";
                    styleTitle2.Font.Size = 22;
                    styleTitle2.Font.IsBold = true;
                    styleTitle2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleTitle2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleTitle2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleTitle2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Double;
                    style = styleTitle2;
                    break;
                case "styleTitle3":
                    Style styleTitle3 = workbook.Styles[workbook.Styles.Add()];
                    styleTitle3.HorizontalAlignment = TextAlignmentType.Center;
                    styleTitle3.VerticalAlignment = TextAlignmentType.Center;
                    styleTitle3.Font.Name = "微软雅黑";
                    styleTitle3.Font.Size = 18;
                    styleTitle3.Font.IsBold = true;
                    styleTitle3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleTitle3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleTitle3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleTitle3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    style = styleTitle3;
                    break;
                case "styleTitle4":
                    Style styleTitle4 = workbook.Styles[workbook.Styles.Add()];
                    styleTitle4.Font.Name = "微软雅黑";
                    styleTitle4.Font.Size = 10;
                    styleTitle4.Font.IsBold = true;
                    style = styleTitle4;
                    styleTitle4.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleTitle4.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleTitle4.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleTitle4.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    break;
                case "styleTitle5":
                    Style styleTitle5 = workbook.Styles[workbook.Styles.Add()];
                    styleTitle5.HorizontalAlignment = TextAlignmentType.Center;
                    styleTitle5.VerticalAlignment = TextAlignmentType.Center;
                    styleTitle5.Font.Name = "微软雅黑";
                    styleTitle5.Font.Size = 10;
                    styleTitle5.Font.IsBold = true;
                    styleTitle5.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleTitle5.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleTitle5.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thick;
                    styleTitle5.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleTitle5;
                    break;
                case "styleSign":
                    Style stylesign = workbook.Styles[workbook.Styles.Add()];
                    stylesign.VerticalAlignment = TextAlignmentType.Center;
                    stylesign.Font.Name = "微软雅黑";
                    stylesign.Font.Size = 12;
                    stylesign.Font.IsBold = true;
                    stylesign.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    stylesign.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    stylesign.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    stylesign.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    style = stylesign;
                    break;
                case "styleContent":
                    Style styleContent1 = workbook.Styles[workbook.Styles.Add()];
                    styleContent1.HorizontalAlignment = TextAlignmentType.Center;// 文字水平居中
                    styleContent1.VerticalAlignment = TextAlignmentType.Center;// 文字垂直居中
                    styleContent1.Font.Name = "微软雅黑";
                    styleContent1.Font.Size = 10;
                    styleContent1.IsTextWrapped = true;//单元格内容自动换行
                    styleContent1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContent1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContent1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContent1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContent1;
                    break;
                case "styleContentLeft":
                    Style styleContent2 = workbook.Styles[workbook.Styles.Add()];
                    styleContent2.HorizontalAlignment = TextAlignmentType.Center;
                    styleContent2.VerticalAlignment = TextAlignmentType.Center;
                    styleContent2.Font.Name = "微软雅黑";
                    styleContent2.Font.Size = 10;
                    styleContent2.IsTextWrapped = true;//单元格内容自动换行
                    styleContent2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thick;
                    styleContent2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContent2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContent2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContent2;
                    break;
                case "styleContentRight":
                    Style styleContent3 = workbook.Styles[workbook.Styles.Add()];
                    styleContent3.HorizontalAlignment = TextAlignmentType.Center;
                    styleContent3.VerticalAlignment = TextAlignmentType.Center;
                    styleContent3.Font.Name = "微软雅黑";
                    styleContent3.Font.Size = 10;
                    styleContent3.IsTextWrapped = true;//单元格内容自动换行
                    styleContent3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContent3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thick;
                    styleContent3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContent3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContent3;
                    break;
                case "styleContentTop":
                    Style styleContent4 = workbook.Styles[workbook.Styles.Add()];
                    styleContent4.HorizontalAlignment = TextAlignmentType.Center;
                    styleContent4.VerticalAlignment = TextAlignmentType.Center;
                    styleContent4.Font.Name = "微软雅黑";
                    styleContent4.Font.Size = 10;
                    styleContent4.IsTextWrapped = true;//单元格内容自动换行
                    styleContent4.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContent4.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContent4.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thick;
                    styleContent4.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContent4;
                    break;
                case "styleContentBottom":
                    Style styleContent5 = workbook.Styles[workbook.Styles.Add()];
                    styleContent5.HorizontalAlignment = TextAlignmentType.Center;
                    styleContent5.VerticalAlignment = TextAlignmentType.Center;
                    styleContent5.Font.Name = "微软雅黑";
                    styleContent5.Font.Size = 10;
                    styleContent5.IsTextWrapped = true;//单元格内容自动换行
                    styleContent5.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContent5.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContent5.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContent5.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;
                    style = styleContent5;
                    break;
                case "styleMemo":
                    Style styleMemo = workbook.Styles[workbook.Styles.Add()];
                    styleMemo.Font.IsBold = true;
                    styleMemo.Font.Name = "微软雅黑";
                    styleMemo.Font.Size = 10;
                    styleMemo.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleMemo.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleMemo.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleMemo.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;
                    style = styleMemo;
                    break;
                case "styleContentR":
                    Style styleContentR = workbook.Styles[workbook.Styles.Add()];
                    styleContentR.Font.Name = "微软雅黑";
                    styleContentR.Font.Size = 10;
                    styleContentR.IsTextWrapped = true;//单元格内容自动换行
                    styleContentR.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleContentR.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thick;
                    styleContentR.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleContentR.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    style = styleContentR;
                    break;
                case "styleContentB":
                    Style styleContentB = workbook.Styles[workbook.Styles.Add()];
                    styleContentB.Font.Name = "微软雅黑";
                    styleContentB.Font.Size = 10;
                    styleContentB.IsTextWrapped = true;//单元格内容自动换行
                    styleContentB.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleContentB.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleContentB.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleContentB.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;
                    style = styleContentB;
                    break;
                case "styleContentRB":
                    Style styleContentRB = workbook.Styles[workbook.Styles.Add()];
                    styleContentRB.Font.Name = "微软雅黑";
                    styleContentRB.Font.Size = 10;
                    styleContentRB.IsTextWrapped = true;//单元格内容自动换行
                    styleContentRB.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleContentRB.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thick;
                    styleContentRB.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleContentRB.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;
                    style = styleContentRB;
                    break;
                case "styleExcelDownTitle":
                    Style styleExcelDownTitle = workbook.Styles[workbook.Styles.Add()];
                    styleExcelDownTitle.Font.Name = "微软雅黑";
                    styleExcelDownTitle.Font.Size = 10;
                    styleExcelDownTitle.Font.IsBold = true;
                    styleExcelDownTitle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownTitle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownTitle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownTitle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownTitle.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    styleExcelDownTitle.Pattern = BackgroundType.Solid;
                    style = styleExcelDownTitle;
                    break;
                case "styleExcelDownContent":
                    Style styleExcelDownContent = workbook.Styles[workbook.Styles.Add()];
                    styleExcelDownContent.Font.Name = "微软雅黑";
                    styleExcelDownContent.Font.Size = 10;
                    styleExcelDownContent.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContent.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContent.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContent.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleExcelDownContent;
                    break;
            }
            return style;
        }
        //
        // GET: /SQM_DJ_PSF/
        public ActionResult Index()
        {
            string kygh = getAppSetting("KYGH");
            string hygh = getAppSetting("HYGH");
            string gylgh = getAppSetting("GYLGH");
            string ysgh = getAppSetting("YSGH");
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
            sql = @"select distinct PRODUCTKEY,SQPRODUCTNAME from SQM_PRD_EXT where SQPRODUCTNAME is not null and STATUS='1'
                and PRODUCTKEY not in(select distinct PRDCODE from SQM_DJ_PSF 
                    where RID in(select distinct FEECALCID from SQM_MODEDJ_VAL where STATUS='1') and PRDCODE is not null)";
            DataTable CopyProdt = DataHelper.QueryDataTable(sql);//没有有效定价的有效产品
            ////根据工号得到用户名 从crm里面取值
            //IDbConnection conn = new OracleConnection();
            //conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            //if (conn.State != ConnectionState.Open)
            //{
            //    conn.Open();
            //}
            //string namesql = "select distinct WORKNO,NAME from SYSUSER";
            //NameDt = DataHelper.QueryDataTable(namesql, conn);
            ViewBag.kygh = kygh;
            ViewBag.hygh = hygh;
            ViewBag.gylgh = gylgh;
            ViewBag.ysgh = ysgh;
            ViewBag.OrgData = Orgdt;
            ViewBag.FeeData = Feedt;
            ViewBag.ProData = Prodt;
            ViewBag.SerData = Serdt;
            ViewBag.CopyProdt = CopyProdt;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        //配置表取数
        //        public ActionResult Lists()
        //        {
        //            //查询条件拼接
        //            var businessorg = Request["BUSINESSORG"].ToString();
        //            string wherestr = " AND A.BUSINESSORG = '" + businessorg + "'";
        //            var productkey = Request["PRODUCTKEY"].ToString();
        //            var servicetype = Request["SERVICETYPE"].ToString();
        //            var feecode = Request["FEECODE"].ToString();
        //            var orgrid = Request["ORGRID"].ToString();
        //            //if (businessorg != "")
        //            //{
        //            //    wherestr += "AND BUSINESSORG = '" + businessorg + "'";
        //            //}
        //            if (productkey != "")
        //            {
        //                wherestr += " AND A.PRODUCTKEY = '" + productkey + "'";
        //            }
        //            if (servicetype != "")
        //            {
        //                wherestr += " AND A.SERVICETYPE = '" + servicetype + "'";
        //            }
        //            if (feecode != "")
        //            {
        //                wherestr += " AND A.FEECODE = '" + feecode + "'";
        //            }
        //            if (orgrid != "")
        //            {
        //                wherestr += " AND A.ORGRID like '%" + orgrid + "%'";
        //            }
        //            string sql_from = @" FROM SQM_PRD_EXT spe
        //                LEFT JOIN MDM_PRD_SRV_REF mpsr ON spe.PRODUCTKEY = mpsr.PRODUCTCODE
        //                LEFT JOIN MDM_SERVICE ms ON mpsr.SERVICETYPECODE = ms.SERVICETYPE
        //                LEFT JOIN MDM_SRV_FEE_REF msfr ON mpsr.SERVICETYPECODE = msfr.SRVRQCD121
        //                LEFT JOIN V_MDM_FEE mf ON msfr.TCET084 = mf.TCET084 
        //                LEFT JOIN SQM_DJ_PSF sdp ON spe.PRODUCTKEY = sdp.prdcode and ms.SERVICETYPE = sdp.srvcode and mf.TCET084 = sdp.feecode and sdp.DJFS is not null 
        //                WHERE spe.STATUS='1' and mf.TEXTDESC is not null ";
        //            string sql_feild = @"SELECT distinct sdp.RID,spe.PRODUCTKEY,spe.SQPRODUCTNAME as PRDNAME,ms.SERVICETYPE as SERVICETYPE,ms.SERVICENAME as SRVNAME,
        //                mf.TCET084 as FEECODE,mf.TEXTDESC as FEENAME,spe.BUSINESSORG,spe.SORD,spe.STATUS,sdp.DJFS,sdp.ORGNAME,sdp.ORGRID,sdp.MODIFYUSER,sdp.MODIFYTIME ";
        //            string sql_order = @"ORDER BY case when sdp.MODIFYTIME is null then 0 else 1 end desc, sdp.MODIFYTIME desc";
        //            string sql_page = string.Format(" WHERE RN between {0} and {1} ", (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
        //            //设置分页
        //            string sql = "With DATASET AS( select A.*,ROWNUM As RN from ({0}{1}{2}) A inner join SQM_SRV_FEE_CONFIG t2 on A.PRODUCTKEY=t2.Prodcode and A.SERVICETYPE=t2.srvcode and A.FEECODE=t2.feecode and t2.FEECATG<>'2' where 1=1 {3}) select * from DATASET ";
        //            sql = string.Format(sql, sql_feild, sql_from, sql_order, wherestr);
        //            string sql_all = sql + sql_page;
        //            //数据数量
        //            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
        //            var rtntotal = DataHelper.QueryValue(countsql);
        //            var rtndata = DataHelper.QueryDataTable(sql_all);
        //            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
        //            return Content(JsonHelper.GetJsonString(obj));
        //        }
        public ActionResult Lists()
        {
            //查询条件拼接
            var businessorg = Request["BUSINESSORG"].ToString();
            string wherestr = " AND BUSINESSORG = '" + businessorg + "'";
            var productkey = Request["PRODUCTKEY"].ToString();
            var servicetype = Request["SERVICETYPE"].ToString();
            var feecode = Request["FEECODE"].ToString();
            var orgrid = Request["ORGRID"].ToString();
            //if (businessorg != "")
            //{
            //    wherestr += "AND BUSINESSORG = '" + businessorg + "'";
            //}
            if (productkey != "")
            {
                wherestr += " AND PRODUCTKEY = '" + productkey + "'";
            }
            if (servicetype != "")
            {
                wherestr += " AND SERVICETYPE = '" + servicetype + "'";
            }
            if (feecode != "")
            {
                wherestr += " AND FEECODE = '" + feecode + "'";
            }
            if (orgrid != "")
            {
                wherestr += " AND ORGRID like '%" + orgrid + "%'";
            }
            string sql_from = @" FROM SQM_PRD_EXT spe
                LEFT JOIN MDM_PRD_SRV_REF mpsr ON spe.PRODUCTKEY = mpsr.PRODUCTCODE
                LEFT JOIN MDM_SERVICE ms ON mpsr.SERVICETYPECODE = ms.SERVICETYPE
                LEFT JOIN MDM_SRV_FEE_REF msfr ON mpsr.SERVICETYPECODE = msfr.SRVRQCD121
                LEFT JOIN V_MDM_FEE mf ON msfr.TCET084 = mf.TCET084 
                LEFT JOIN SQM_DJ_PSF sdp ON spe.PRODUCTKEY = sdp.prdcode and ms.SERVICETYPE = sdp.srvcode and mf.TCET084 = sdp.feecode and sdp.DJFS is not null 
                WHERE spe.STATUS='1' and mf.TEXTDESC is not null ";
            string sql_feild = @"SELECT distinct sdp.RID,spe.PRODUCTKEY,spe.SQPRODUCTNAME as PRDNAME,ms.SERVICETYPE as SERVICETYPE,ms.SERVICENAME as SRVNAME,
                mf.TCET084 as FEECODE,mf.TEXTDESC as FEENAME,spe.BUSINESSORG,spe.SORD,spe.STATUS,sdp.DJFS,sdp.ORGNAME,sdp.ORGRID,sdp.MODIFYUSER,sdp.MODIFYTIME ";
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
        //
        // GET: /SQM_DJ_PSF/
        public ActionResult SaveDjPsf(SQM_DJ_PSF sdp)
        {
            bool rtnflag = true;
            string rtnmsg = "";
            try
            {
                string djrid = System.Guid.NewGuid().ToString();
                SQM_DJ_PSF sdpnew = SQM_DJ_PSF.FindFirstByProperties(SQM_DJ_PSF.Prop_PRDCODE, sdp.PRDCODE, SQM_DJ_PSF.Prop_SRVCODE, sdp.SRVCODE, SQM_DJ_PSF.Prop_FEECODE, sdp.FEECODE);
                if (sdpnew != null)
                {
                    sdpnew.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdpnew.BUSINESSORG = sdp.BUSINESSORG;
                    sdpnew.DoUpdate();
                    rtnmsg = sdpnew.RID;
                }
                else
                {
                    sdp.RID = djrid;
                    sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdp.DJFS = "0";
                    sdp.CREATESOURCE = "费目定价";
                    sdp.DoCreate();
                    rtnmsg = djrid;
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 加组织筛选
        /// </summary>
        /// <param name="sdp"></param>
        /// <returns></returns>
        public ActionResult SaveDjPsfORG(SQM_DJ_PSF sdp)
        {
            bool rtnflag = true;
            string rtnmsg = "";
            try
            {
                string djrid = System.Guid.NewGuid().ToString();
                DataTable dt = DataHelper.QueryDataTable("select * from sqm_dj_psf where prdcode = '" + sdp.PRDCODE + "' and srvcode = '" + sdp.SRVCODE + "' and feecode = '" + sdp.FEECODE + "' and orgrid like '%" + sdp.ORGRID + "%'");
                if (dt.Rows.Count > 0)
                {
                    List<SQM_DJ_PSF> sdpnews = TableToEntity<SQM_DJ_PSF>(dt);
                    SQM_DJ_PSF sdpnew = sdpnews[0];
                    sdpnew.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdpnew.BUSINESSORG = sdp.BUSINESSORG;
                    sdpnew.DoUpdate();
                    rtnmsg = sdpnew.RID;
                }
                else
                {
                    sdp.RID = djrid;
                    sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    string orgname = DataHelper.QueryValue("select distinct ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 and ltrim(OBJID,'0') = '" + sdp.ORGRID + "'") + "";
                    sdp.ORGCODE = orgname + "-" + sdp.ORGRID;
                    sdp.ORGNAME = orgname;
                    sdp.CREATESOURCE = "费目询价";
                    sdp.DoCreate();
                    rtnmsg = djrid;
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
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
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
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
        //
        // POST: /SQM_DJ_PSF/Create
        public ActionResult PurCreate(SQM_DJ_PSF sdp)
        {
            bool rtnflag = true;
            string rtnmsg = "发布成功";
            try
            {
                string rid = Request["RID"].ToString();
                string feecode = Request["FEECODE"].ToString();
                string[] rowIdArr = Request["ROWIDS"].Split(',');
                string orgcode = Request["ORGCODE"].ToString();
                //判断该组织是否做过定价
                //bool doPur = ifDoPur(sdp, orgcode);
                //if (doPur)
                //{
                //    return Content(new JsonMessage { Success = false, Message = "该组织已存在发布的定价信息，请确认！" }.ToString());
                //}
                string orgname = "";
                string nameval = "";
                string codeval = "";
                string sql = "";
                if (sdp.DJFS == "0" && !String.IsNullOrEmpty(Request["ROWIDS"]))
                {
                    for (int i = 0; i < rowIdArr.Length; i++)
                    {
                        string wherestr = "";
                        string wherejc = "";
                        SQM_MODEDJ_VAL smv = SQM_MODEDJ_VAL.Find(rowIdArr[i]);
                        if (smv.DJSTATUS == "1")
                        {
                            continue;//已发布的定价自动跳过
                        }
                        smv.DJSTATUS = "1";
                        smv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        smv.DoUpdate();
                        //无定价报价同步更新数据
                        DataTable dtjc = null;
                        sql = @"select * from SQM_MODEDJ_VAL where RID='" + rowIdArr[i] + "'";
                        DataTable djdt = DataHelper.QueryDataTable(sql);
                        string gdzrid = djdt.Rows[0]["GDZRID"].ToString();
                        string djfsrid = djdt.Rows[0]["DJFSRID"].ToString();
                        DateTime startDate = Convert.ToDateTime(djdt.Rows[0]["STARTDATE"].ToString());
                        DateTime endDate = Convert.ToDateTime(djdt.Rows[0]["ENDDATE"].ToString());
                        decimal maxprice = Convert.ToDecimal(djdt.Rows[0]["MAXPRICE"].ToString());
                        decimal minprice = Convert.ToDecimal(djdt.Rows[0]["MINPRICE"].ToString());
                        if (!String.IsNullOrEmpty(gdzrid))
                        {
                            wherejc = " and GDZRID = '" + gdzrid + "'";
                        }
                        else if (!String.IsNullOrEmpty(djfsrid))
                        {
                            wherejc = " and DJFSRID = '" + djfsrid + "'";
                        }
                        else
                        {
                            wherejc = " and DJFSRID is null";
                        }
                        dtjc = DataHelper.QueryDataTable(string.Format("select VALCOL,VALCOL||'C' as VALCOLC from SQM_FEE_CALC_REF where status = '1' and FEECODE = '{0}' {1} order by VALCOL asc", feecode, wherejc));
                        int j = 1;
                        foreach (DataRow jcdr in dtjc.Rows)
                        {
                            wherestr += " and " + jcdr["VALCOL"] + "='" + djdt.Rows[0]["COLUMN" + j].ToString() + "' and " + jcdr["VALCOLC"] + "='" + djdt.Rows[0]["COLUMN" + j + "C"].ToString() + "'";
                            j++;
                        }
                        DataTable wdjdt = DataHelper.QueryDataTable(string.Format("select RID,STARTDATE,ENDDATE,BJPRICE from SQM_MODEBJ_VAL where WDJBJRID is not null and DJRID='{0}' {1} {2}", rid, wherejc, wherestr));
                        foreach (DataRow wdjdr in wdjdt.Rows)
                        {
                            string bjstatus = "";
                            //无定价报价时间区间在发布的定价区间内才能同步数据
                            decimal wdjbjprice = Convert.ToDecimal(wdjdr["BJPRICE"].ToString());
                            if (wdjbjprice < minprice || wdjbjprice > maxprice)
                            {
                                bjstatus = "4";//报价超限（已保存）
                            }
                            else
                            {
                                bjstatus = "1";//已保存
                            }
                            DateTime wdjstartDate = Convert.ToDateTime(wdjdr["STARTDATE"].ToString());
                            DateTime wdjendDate = Convert.ToDateTime(wdjdr["ENDDATE"].ToString());
                            if (wdjstartDate >= startDate && wdjendDate <= endDate)
                            {
                                SQM_MODEBJ_VAL smvbj = SQM_MODEBJ_VAL.Find(wdjdr["RID"].ToString());
                                smvbj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                smvbj.MEMO = djdt.Rows[0]["MEMO"].ToString();
                                smvbj.DJRID = rowIdArr[i];
                                smvbj.BJSTATUS = bjstatus;
                                smvbj.PURPRICE = Convert.ToDecimal(djdt.Rows[0]["PURPRICE"].ToString());
                                smvbj.COSTPRICE = Convert.ToDecimal(djdt.Rows[0]["COSTPRICE"].ToString());
                                smvbj.MAXPRICE = Convert.ToDecimal(djdt.Rows[0]["MAXPRICE"].ToString());
                                smvbj.MINPRICE = Convert.ToDecimal(djdt.Rows[0]["MINPRICE"].ToString());
                                smvbj.GUIDEPRICE = Convert.ToDecimal(djdt.Rows[0]["GUIDEPRICE"].ToString());
                                smvbj.CALCUNIT = djdt.Rows[0]["CALCUNIT"].ToString();
                                smvbj.CALCTYPE = djdt.Rows[0]["CALCTYPE"].ToString();
                                smvbj.DoUpdate();
                                //变更BJ_PSF表里面的状态
                                string bjstatusstr = "";
                                sql = string.Format("select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='{0}' {1}", smvbj.FEECALCID, wherejc);
                                DataTable bjsta = DataHelper.QueryDataTable(sql);
                                foreach (DataRow bjstadr in bjsta.Rows)
                                {
                                    bjstatusstr += bjstadr["BJSTATUS"].ToString() + ",";
                                }
                                SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(smvbj.FEECALCID);
                                if (bjstatusstr.Contains("3"))
                                {
                                    sbp.BJSTATAUS = "3";
                                }
                                else if (bjstatusstr.Contains("4"))
                                {
                                    sbp.BJSTATAUS = "4";
                                }
                                else
                                {
                                    sbp.BJSTATAUS = "1";
                                }
                                sbp.DoUpdate();
                            }
                        }
                    }
                }
                foreach (string code in orgcode.Split(','))
                {
                    nameval = DataHelper.QueryValue("select ORGNAME from v_mdm_org where ltrim(OBJID,'0')='" + code + "'") + "";
                    orgname += nameval + ",";
                    codeval += nameval + "-" + code + ",";
                }
                SQM_DJ_PSF erd = SQM_DJ_PSF.Find(rid);
                erd.DJFS = sdp.DJFS;
                erd.IFDPDX = sdp.IFDPDX;
                erd.IFCOST = sdp.IFCOST;
                erd.ORGRID = orgcode;
                if (sdp.DJFS != "0")
                {
                    DataHelper.ExecSql("update SQM_MODEDJ_VAL set DJSTATUS='0' where FEECALCID='" + rid + "'");
                }
                erd.ORGNAME = orgname.Trim(',');
                erd.ORGCODE = codeval.Trim(',');
                erd.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                erd.DoUpdate();
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        //
        // GET: /SQM_MODEDJ_VAL/PurEdit
        public ActionResult PurEdit()
        {
            try
            {
                string sql = "";
                string minprice = "";
                string mindata = "";
                string isfssetcustomer = "";//费目 指定客户 （是或否）
                bool gyl = false;
                bool khy = false;
                bool kyyf = false;
                bool min = false;
                bool wjc = true;
                DataTable FCREFdt = null;
                DataTable GDZdt = null;
                DataTable GDZDATAdt = null;
                DataTable JSJCdt = null;
                DataTable DJFSdt = null;
                DataTable BZdt = null;
                DataTable MDM_BPdt = null;
                string id = Request.QueryString["id"];
                string djrid = Request.QueryString["djrid"];
                string gdzkey = Request.QueryString["gdzkey"];
                string gdzrid = Request.QueryString["gdzrid"];
                string calcunit = "";
                string djfsrid = Request.QueryString["djfsrid"];
                string djfs = Request.QueryString["djfs"];
                SQM_DJ_PSF sdp = SQM_DJ_PSF.Find(djrid);
                string businessorg = sdp.BUSINESSORG;
                string sqlMDMBP = "SELECT BPKEY,BPNAME FROM MDM_BP";
                MDM_BPdt = DataHelper.QueryDataTable(sqlMDMBP);
                List<Models.MDM_BP> mdmBPList = ModelConvertHelper<Models.MDM_BP>.ConvertToModel(MDM_BPdt);
                DataTable crmdt = GetCrmCustomers();
                List<Crm_CustomerBase> crmCustomerList = ModelConvertHelper<Crm_CustomerBase>.ConvertToModel(crmdt);
                var BPList = (from a in mdmBPList
                              join b in crmCustomerList
                              on a.BPKey equals b.CustomerNo
                              select a
                              ).ToList();

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
                    sql = @"With DATASET AS(
                           select sfc.RID from SQM_FEE_CALC sfc 
                           left join SQM_DJ_PSF sdf on sfc.FEECODE=sdf.FEECODE
                           where sdf.RID='" + djrid + "') select distinct sfpr.DJFSRID,sfpr.DJFSNAME from DATASET t1 left join SQM_FEE_PUR_REF sfpr on t1.RID=sfpr.feerid  and sfpr.STATUS='1' where DJFSRID is not null order by sfpr.DJFSNAME asc";
                    DJFSdt = DataHelper.QueryDataTable(sql);
                    if (String.IsNullOrEmpty(djfsrid) && DJFSdt.Rows.Count > 0)
                    {
                        djfsrid = DJFSdt.Rows[0]["DJFSRID"].ToString();
                    }
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
                        if (String.IsNullOrEmpty(gdzkey) && GDZDATAdt.Rows.Count > 0)
                        {
                            gdzkey = GDZDATAdt.Rows[0]["GDZKEY"].ToString();
                            gdzrid = GDZDATAdt.Rows[0]["GDZRID"].ToString();
                        }
                        wheredjfs = " and r.DJFSRID='" + djfsrid + "'";
                        if (gdzkey == "0" || String.IsNullOrEmpty(gdzkey))
                        {
                            wheregdz = " and r.GDZRID is null";
                        }
                        else
                        {
                            wheregdz = " and r.GDZRID='" + gdzrid + "'";
                        }
                    }
                    sql = @"select distinct r.CALCNAME||'('|| r.SCALE ||')' CALCNAME,r.VALCOL,r.CALCCODE,r.MSRUNIT,e.MDMTYPE,e.MDMKEY,e.MDMFIELDNAME,e.MDMLOCTYPE,
                        r.SORD,b.CALCTYPE  
                        from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.FEECODE=p.FEECODE
                        left join SQM_CALC_BASE_EXT e on r.CALCCODE=e.CALCCODE
                        left join SQM_CALC_BASE b on r.CALCCODE=b.CALC_BASE
                        where r.STATUS='1' and p.Rid='{0}' {1} {2} order by r.SORD asc";
                    string fcrefsql = string.Format(sql, djrid, wheredjfs, wheregdz);
                    FCREFdt = DataHelper.QueryDataTable(fcrefsql);
                    foreach (DataRow fcrefdr in FCREFdt.Rows)
                    {
                        wjc = false;
                        if (!String.IsNullOrEmpty(fcrefdr["MSRUNIT"].ToString()))
                        {
                            calcunit += fcrefdr["MSRUNIT"].ToString() + "/";
                        }
                    }
                    sql = string.Format("SELECT  DISTINCT r.GDZKEY,case when r.GDZKEY='0' THEN '无' when r.GDZKEY='A' THEN 'A' when r.GDZKEY='H' THEN 'H' when r.GDZKEY='L' THEN 'L' END GDZKEYNAME FROM SQM_FEE_PUR_REF r WHERE r.STATUS='1' and r.FEECODE = '{0}' {1} order by GDZKEY asc", sdp.FEECODE, wheredjfs);
                    GDZdt = DataHelper.QueryDataTable(sql);
                    //                    if (businessorg == "供应链")
                    //                    {
                    //                        sql = @"select r.CALCCODE,r.CALCNAME from SQM_FEE_CALC_REF r
                    //                            left join SQM_DJ_PSF p on r.feecode=p.feecode
                    //                            where r.STATUS='1' and p.Rid='" + djrid + "' and r.ISCNT='是'group by CALCCODE,CALCNAME order by CALCNAME asc";
                    //                        JSJCdt = DataHelper.QueryDataTable(sql);
                    //                        gyl = true;
                    //                    }
                    mindata = DataHelper.QueryValue(string.Format("select r.MIN from SQM_MODEDJ_VAL r where r.FEECALCID='{0}' {1}", djrid, wheredjfs)) + "";
                    BZdt = DataHelper.QueryDataTable("select WAERS,KTEXT from MDM_WAERS");
                    string sqlSetFlag = "SELECT FSSETCUSTOMER FROM SQM_FEE_PUR_REF WHERE FSSETCUSTOMER='1'";
                    DataTable flagdt = DataHelper.QueryDataTable(sqlSetFlag);
                    if (flagdt.Rows.Count > 0)
                    {
                        List<SelectListItem> items = new List<SelectListItem>();
                        foreach (var item in BPList)
                        {
                            items.Add(new SelectListItem
                            {
                                Value = item.BPKey,
                                Text = item.BPName,
                                Selected = true
                            });
                        }
                        //尝试取前100条数据
                        items = (from a in items orderby a.Value descending select a).Take(100).ToList();
                        //ViewBag.BPDataList = items;//客户列表数据，TM与CRM中的客户数据交集
                        ViewBag.BPDataList = null;//客户列表数据，TM与CRM中的客户数据交集
                    }
                    else
                    {
                        ViewBag.BPDataList = null;
                    }

                    string sqlfeerid = "SELECT fssetcustomer FROM SQM_FEE_PUR_REF WHERE  FEECODE='" + sdp.FEECODE + "' and djfsrid='"+ djfsrid + "'";
                    DataTable sqlfeerefdt = DataHelper.QueryDataTable(sqlfeerid);
                    if(sqlfeerefdt.Rows.Count > 0)
                    {
                        var item = sqlfeerefdt.Rows[0];
                        isfssetcustomer = sqlfeerefdt.Rows[0]["FSSETCUSTOMER"].ToString();
                    }
                }

                ViewBag.BZdtData = BZdt;
                ViewBag.gyl = gyl;
                ViewBag.wjc = wjc;
                ViewBag.khy = khy;
                ViewBag.min = min;
                ViewBag.mindata = mindata;
                ViewBag.kyyf = kyyf;
                ViewBag.tbtitle = sdp.FEENAME;
                ViewBag.FCREFData = FCREFdt;
                ViewBag.djfsrid = djfsrid;
                ViewBag.gdzkey = gdzkey;
                ViewBag.gdzrid = gdzrid;
                ViewBag.GDZdt = GDZdt;
                ViewBag.GDZDATAdt = GDZDATAdt;
                ViewBag.DJFSData = DJFSdt;
                ViewBag.calcunit = calcunit.TrimEnd('/');
                ViewBag.JSJCdt = JSJCdt;
                ViewBag.djrid = djrid;
                ViewBag.feecode = sdp.FEECODE;
                ViewBag.djfs = djfs;
                ViewBag.isfssetcustomer = isfssetcustomer;
                if (!String.IsNullOrEmpty(id))
                {
                    SQM_MODEDJ_VAL smv = SQM_MODEDJ_VAL.Find(id);
                    return View("PurEdit", smv);
                }
                else
                {
                    SQM_MODEDJ_VAL smv = new SQM_MODEDJ_VAL();
                    return View("PurEdit", smv);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult getCUSTOMERSNO(String selectInput = "", int curPage = 1, int pageSize = 10)
        {
            var totalCount = 0;
            DataTable MDM_BPdt = null;
            string sqlMDMBP = "SELECT BPKEY,BPNAME FROM MDM_BP where BPKEY like '%" + selectInput + "%' or BPNAME like '%" + selectInput + "%'";
            MDM_BPdt = DataHelper.QueryDataTable(sqlMDMBP);
            List<Models.MDM_BP> mdmBPList = ModelConvertHelper<Models.MDM_BP>.ConvertToModel(MDM_BPdt);
            DataTable crmdt = GetCrmCustomers();
            List<Crm_CustomerBase> crmCustomerList = ModelConvertHelper<Crm_CustomerBase>.ConvertToModel(crmdt);
            var BPList = (from a in mdmBPList
                          join b in crmCustomerList
                          on a.BPKey equals b.CustomerNo
                          select a
                             ).ToList();
            List<Select2ListItem> items = new List<Select2ListItem>();
            totalCount = BPList.Count;
            if (BPList.Count > 10)
            {
                BPList = BPList.OrderBy(x => x.BPKey).Skip(pageSize * (curPage - 1)).Take(pageSize).ToList();
            }
            foreach (var item in BPList)
            {
                items.Add(new Select2ListItem
                {
                    id = item.BPKey,
                    text = item.BPName + "-" + item.BPKey,
                    Selected = true
                });
            }
            //尝试取前100条数据
            //items = (from a in items orderby a.Value descending select a).Take(1000).ToList();
            //ViewBag.BPDataList = items;//客户列表数据，TM与CRM中的客户数据交集
            return Json(new { total = totalCount, incomplete_results = false, items = items }, JsonRequestBehavior.AllowGet);


        }
        //获取CRM库中的客户数据
        public DataTable GetCrmCustomers()
        {
            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string sql = "select  CUSTOMERNO,NAME from crm_customerbase";
            DataTable dt = DataHelper.QueryDataTable(sql, conn);
            return dt;
        }
        [AllowAnonymous]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SQM_MODEDJ_VAL.TryFind(keyValue);
            return Content(JsonHelper.GetJsonString(data));
        }
        public static void SendEmail(string rid, string msg)
        {
            string title = "定价变更";
            string body = "";
            if (msg == "1")
            {
                body = "<br>您好！此价格发生变动，请及时跟进处理。谢谢！";
            }
            else
            {
                body = "<br>您好！该定价已失效，请及时跟进处理。谢谢！";
            }

            string createid = "";
            string SERVICE_NAME = "";
            string PRODUCT_NAME = "";
            string FEE_NAME = "";

            string mailServer = System.Configuration.ConfigurationManager.AppSettings["mailServer"];
            string mailSenderName = System.Configuration.ConfigurationManager.AppSettings["mailSender"];
            string mailAccount = System.Configuration.ConfigurationManager.AppSettings["mailAccount"];
            string mailPass = System.Configuration.ConfigurationManager.AppSettings["mailPassword"];
            string sql = "select distinct sbj.createuser,spsf.SERVICE_NAME,spsf.PRODUCT_NAME,spsf.FEE_NAME from sqm_modebj_val sbj  left join sqm_bj_psf spsf on sbj.FEECALCID=spsf.RID where sbj.djrid='{0}'";
            sql = string.Format(sql, rid);
            DataTable dts = DataHelper.QueryDataTable(sql);
            if (dts != null && dts.Rows.Count > 0)
            {
                foreach (DataRow dr in dts.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["CREATEUSER"].ToString()))
                    {
                        createid = dr["CREATEUSER"] + "";
                    }
                    if (!string.IsNullOrEmpty(dr["PRODUCT_NAME"].ToString()))
                    {
                        PRODUCT_NAME = dr["PRODUCT_NAME"] + "";
                        SERVICE_NAME = dr["SERVICE_NAME"] + "";
                        FEE_NAME = dr["FEE_NAME"] + "";
                    }

                    body = "变动的产品:" + PRODUCT_NAME + ",服务:" + SERVICE_NAME + ",费目:" + FEE_NAME + "。" + body;

                    string crmsql = string.Format("select Email from sysuser where workno ='{0}'", createid);
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    var strmailto = DataHelper.QueryValue(crmsql, conn);

                    System.Net.Mail.SmtpClient client = new SmtpClient();
                    client.Host = mailServer;//163的smtp服务器是 smtp.163.com   

                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(mailAccount, mailPass);

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    string senderDisplayName = mailSenderName;//这个配置的是发件人的要显示在邮件的名称

                    MailAddress mailfrom = new MailAddress(mailAccount, senderDisplayName, encoding);//发件人邮箱地址，名称，编码UTF8
                    if (strmailto == null || strmailto.ToString() == "")
                    {
                        strmailto = "yanyajuan@irongwei.com";
                    }
                    MailAddress mailto = new MailAddress(strmailto.ToString());//收件人邮箱地址，名称，编码UTF8   
                    //创建mailMessage对象   
                    System.Net.Mail.MailMessage message = new MailMessage(mailfrom, mailto);
                    message.Subject = title;
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.BodyEncoding = encoding;
                    message.SubjectEncoding = encoding;

                    client.Send(message);

                }

            }
        }
        //
        // POST: /SQM_FEE_CALC/Create
        public ActionResult PurSave(string postdata, string rid, string djrid, string djfsrid, string gdzrid)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                SQM_MODEDJ_VAL oldsmv = null;
                SQM_MODEDJ_VAL smv = null;
              //  PurEditModel purEdit= JsonConvert.DeserializeObject<PurEditModel>(postdata);//Json数据转为对象
              //  string[] customersArr = purEdit.CUSTOMERSNO;
              //  string customerStr = string.Empty;
              //  for(int i = 0; i < customersArr.Length; i++)
              //  {
              //      customerStr =customerStr+","+ customersArr[i];
              //  }
              // string customerJson= JsonConvert.SerializeObject(customerStr).ToString();//类型转化
                
             //   postdata = postdata.Replace("CUSTOMERSNO", customerJson);
                smv = JsonHelper.GetObject<SQM_MODEDJ_VAL>(postdata);
                
                //供应链当时用的
                //string calcname = DataHelper.QueryValue("select DESCRIPTION from MDM_CALC_BASE where CALC_BASE='" + smv.CALCCODE + "'") + "";
                //smv.CALCNAME = calcname;
                smv.FEECALCID = djrid;
               
                DateTime startDate = (DateTime)smv.STARTDATE;
                DateTime endDate = (DateTime)smv.ENDDATE;
                //获取报价 指定客户的维度
                string[] primaryKeys = getPrimaryKeys(djrid, djfsrid, gdzrid);
                if (!String.IsNullOrEmpty(rid))
                {
                    oldsmv = SQM_MODEDJ_VAL.Find(rid);
                    // 获取原始数据
                    DataTable dt = FindSourceData(smv, primaryKeys);
                    //DataRow[] rows = dt.Select("RID<>'" + oldsmv.RID + "'");
                    foreach (DataRow row in dt.Rows)
                    {//判断与当前修改报价数据所有不同的 同条件的 报价数据 
                        if(row["RID"].ToString() == oldsmv.RID)
                        {// 如果获取的数据 与 当前修改的是同一条数据 则跳过
                            continue;
                        }
                        ////修改 处理存在时间交叉 的数据
                        //if ((startDate > (DateTime)row["STARTDATE"] && startDate <= (DateTime)row["ENDDATE"]) || (endDate >= (DateTime)row["STARTDATE"] && endDate < (DateTime)row["ENDDATE"]))
                        //{
                        //    return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请返回编辑修改！" }.ToString());
                        //}
                        //else if (startDate <= (DateTime)row["STARTDATE"] && endDate >= (DateTime)row["ENDDATE"])
                        if (!(startDate > (DateTime)row["ENDDATE"] || endDate < (DateTime)row["STARTDATE"]))
                        {
                            SQM_MODEDJ_VAL sxsmv = SQM_MODEDJ_VAL.Find(row["RID"]);

                            string CUSTOMERSNO = "";//已存在 报价数据 中的 指定客户
                            if (row["CUSTOMERSNO"] != null)
                            {
                                CUSTOMERSNO = row["CUSTOMERSNO"].ToString();
                            }
                            if (string.IsNullOrEmpty(smv.CUSTOMERSNO))
                            {//修改报价数据 中的 指定客户 
                                smv.CUSTOMERSNO = "";
                            }
                            if (sxsmv.DJSTATUS == "1")//原始区间内已发布的数据给提示
                            {
                                if (CUSTOMERSNO == smv.CUSTOMERSNO)
                                {
                                    return Content(new JsonMessage { Success = false, Message = "所选时间区间存在已发布的定价，请返回编辑修改！" }.ToString());
                                }
                                if (!string.IsNullOrEmpty(smv.CUSTOMERSNO))
                                {//新创建的数据 指定客户不为空的情况
                                    var arr = smv.CUSTOMERSNO.Split(',');
                                    foreach (var item in arr)
                                    {
                                        if (CUSTOMERSNO.Contains(item))
                                        {//指定客户为空时，可以保存
                                            return Content(new JsonMessage { Success = false, Message = "所选时间区间存在已发布的定价，请返回编辑修改！" }.ToString());
                                        }
                                    }
                                }
                            }
                            else //原始区间内未发布的数据
                            {
                                if (CUSTOMERSNO == smv.CUSTOMERSNO)
                                {//未发布的数据中有与指定客户相等的 置为失效
                                    //sxsmv.STATUS = "0";
                                    //sxsmv.DoUpdate();
                                    return Content(new JsonMessage { Success = false, Message = "所选时间区间存在未发布的定价，请返回编辑修改！" }.ToString());
                                }
                                else
                                {//未发布的数据 指定客户不相等
                                    if (!string.IsNullOrEmpty(smv.CUSTOMERSNO))
                                    {//新创建的数据 指定客户不为空的情况
                                        var arr = smv.CUSTOMERSNO.Split(',');
                                        foreach (var item in arr)
                                        {
                                            if (CUSTOMERSNO.Contains(item))
                                            {//指定客户为空时，可以保存
                                                //sxsmv.STATUS = "0";
                                                //sxsmv.DoUpdate();
                                                return Content(new JsonMessage { Success = false, Message = "所选时间区间存在未发布的定价，请返回编辑修改！" }.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    smv.STATUS = oldsmv.STATUS;
                    smv.BJPRICE = oldsmv.BJPRICE;
                    smv.WDJBJRID = oldsmv.WDJBJRID;
                    smv.CREATETIME = oldsmv.CREATETIME;
                    smv.CREATEUSER = oldsmv.CREATEUSER;
                    //该定价方式下面的MIN值一致
                    //DataHelper.ExecSql("update SQM_MODEDJ_VAL set MIN='" + smv.MIN + "' where FEECALCID='" + djrid + "' and DJFSRID='" + djfsrid + "'");
                    //未发布状态直接更新
                    if (oldsmv.DJSTATUS == "0" || oldsmv.DJSTATUS == "")
                    {
                        DataHelper.MergeData<SQM_MODEDJ_VAL>(oldsmv, smv);
                        oldsmv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        oldsmv.DoSave();
                    }
                    else
                    {
                        // 获取原始数据
                        DataRow[] oldrows = dt.Select("RID='" + oldsmv.RID + "'");
                        // 获取原始数据最小起始日期
                        DateTime startDate_old = (DateTime)oldrows[0]["STARTDATE"];
                        // 获取原始数据最大截止日期
                        DateTime endDate_old = (DateTime)oldrows[0]["ENDDATE"];
                        // 处理有效期
                        HandleValidDate(startDate, endDate, startDate_old, endDate_old, oldsmv, smv, oldrows);
                    }
                    //定价 已发布状态，价格改变后发送邮件提醒
                    if (oldsmv.DJSTATUS == "1")
                    {
                        string msg = "1";
                        SQM_PURController.SendEmail(rid, msg);
                    }
                }
                else // 新增
                {
                    // 获取原始数据
                    DataTable dt = FindSourceData(smv, primaryKeys);
                    if (dt.Rows.Count > 0)
                    {
                        //DataRow[] rows = dt.Select("1=1").OrderByDescending(x=>x.ItemArray[""].ToString());
                        foreach (DataRow row in dt.Rows)
                        {
                            //新增处理存在时间交叉 的数据
                            //if ((startDate > (DateTime)row["STARTDATE"] && startDate <= (DateTime)row["ENDDATE"]) || (endDate >= (DateTime)row["STARTDATE"] && endDate < (DateTime)row["ENDDATE"]))
                            if ( !(startDate > (DateTime)row["ENDDATE"] || endDate < (DateTime)row["STARTDATE"]) )
                            {
                                //return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请返回编辑修改！" }.ToString());
                                //}
                                //else //if (startDate <= (DateTime)row["STARTDATE"] && endDate >= (DateTime)row["ENDDATE"])
                                //{
                                SQM_MODEDJ_VAL sxsmv = SQM_MODEDJ_VAL.Find(row["RID"]);

                                string CUSTOMERSNO = "";//已存在 报价数据 中的 指定客户
                                if (row["CUSTOMERSNO"] != null)
                                {
                                    CUSTOMERSNO = row["CUSTOMERSNO"].ToString();
                                }
                                if (string.IsNullOrEmpty(smv.CUSTOMERSNO))
                                {//修改报价数据 中的 指定客户 
                                    smv.CUSTOMERSNO = "";
                                }
                                if (sxsmv.DJSTATUS == "1")//原始区间内已发布的数据给提示
                                {
                                    if (CUSTOMERSNO == smv.CUSTOMERSNO)
                                    {
                                        return Content(new JsonMessage { Success = false, Message = "所选时间区间存在已发布的定价，请返回编辑修改！" }.ToString());
                                    }
                                    if (!string.IsNullOrEmpty(smv.CUSTOMERSNO))
                                    {//新创建的数据 指定客户不为空的情况
                                        var arr = smv.CUSTOMERSNO.Split(',');
                                        foreach (var item in arr)
                                        {
                                            if (CUSTOMERSNO.Contains(item))
                                            {//指定客户为空时，可以保存
                                                return Content(new JsonMessage { Success = false, Message = "所选时间区间存在已发布的定价，请返回编辑修改！" }.ToString());
                                            }
                                        }
                                    }
                                }
                                else //原始区间内未发布的数据
                                {
                                    if (CUSTOMERSNO == smv.CUSTOMERSNO)
                                    {//未发布的数据中有与指定客户相等的 置为失效
                                         //sxsmv.STATUS = "0";
                                         //sxsmv.DoUpdate();
                                        return Content(new JsonMessage { Success = false, Message = "所选时间区间存在未发布的定价，请返回编辑修改！" }.ToString());
                                    }
                                    else
                                    {//未发布的数据 指定客户不相等
                                        if (!string.IsNullOrEmpty(smv.CUSTOMERSNO))
                                        {//新创建的数据 指定客户不为空的情况
                                            var arr = smv.CUSTOMERSNO.Split(',');
                                            foreach (var item in arr)
                                            {
                                                if (CUSTOMERSNO.Contains(item))
                                                {//指定客户为空时，可以保存
                                                    //sxsmv.STATUS = "0";
                                                    //sxsmv.DoUpdate();
                                                    return Content(new JsonMessage { Success = false, Message = "所选时间区间存在未发布的定价，请返回编辑修改！" }.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //该定价方式下面的MIN值一致
                    //DataHelper.ExecSql("update SQM_MODEDJ_VAL set MIN='" + smv.MIN + "' where FEECALCID='" + djrid + "' and DJFSRID='" + djfsrid + "'");
                    oldsmv = smv;
                    oldsmv.DJSTATUS = "0";
                    oldsmv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    oldsmv.DoSave();
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
        public string[] getPrimaryKeys(string djrid, string djfsrid, string gdzrid)
        {
            try
            {
               // string[] primaryKeys = { "CURRENCY", "FEECALCID", "DJFSRID", "GDZRID" };
                string[] primaryKeys = { "CURRENCY", "FEECALCID", "DJFSRID", "GDZRID", "CUSTOMERSNO" };//增加指定客户的维度
                List<string> zjKeys = new List<string>(primaryKeys);
                string where = "";
                if (!String.IsNullOrEmpty(djfsrid))
                {
                    where += " and r.DJFSRID='" + djfsrid + "' ";
                }
                else
                {
                    where += " and r.DJFSRID is null ";
                }
                if (!String.IsNullOrEmpty(gdzrid))
                {
                    where += " and r.GDZRID='" + gdzrid + "' ";
                }
                else
                {
                    where += " and r.GDZRID is null ";
                }
                string sql = @"select r.CALCNAME,r.VALCOL from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.feecode=p.feecode
                        where p.Rid='{0}' and r.STATUS='1' {1} order by r.SORD asc";
                sql = string.Format(sql, djrid, where);
                DataTable FCREFdt = DataHelper.QueryDataTable(sql);
                foreach (DataRow dr in FCREFdt.Rows)
                {
                    zjKeys.Add(dr["VALCOL"].ToString());
                }
                primaryKeys = zjKeys.ToArray();
                return primaryKeys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public string[] getSearchKeys(string djrid, string djfsrid, string calcunit)
        {
            try
            {
                string[] primaryKeys = { "STARTDATE", "ENDDATE", "STATUS", "CALCUNIT" };
                List<string> zjKeys = new List<string>(primaryKeys);
                string where = "";
                if (!String.IsNullOrEmpty(djfsrid))
                {
                    where = " and r.DJFSRID='" + djfsrid + "' ";
                }
                else
                {
                    where = " and r.DJFSRID is null ";
                }
                string sql = @"select r.CALCNAME,r.VALCOL from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.feecode=p.feecode
                        where p.Rid='{0}' and r.CACLUNIT='{1}' and r.STATUS='1' {2} order by r.SORD asc";
                sql = string.Format(sql, djrid, calcunit, where);
                DataTable FCREFdt = DataHelper.QueryDataTable(sql);
                foreach (DataRow dr in FCREFdt.Rows)
                {
                    zjKeys.Add(dr["VALCOL"].ToString());
                }
                primaryKeys = zjKeys.ToArray();
                return primaryKeys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string getFiledKeys(string djrid, string calcunit, bool min, string djfsrid = null)
        {
            try
            {
                string filedkeys = "DJFSRID,CURRENCY,CALCTYPE,";
                string where = "";
                if (!String.IsNullOrEmpty(djfsrid))
                {
                    where = " and r.DJFSRID='" + djfsrid + "' ";
                }
                else
                {
                    where = " and r.DJFSRID is null ";
                }
                string sql = @"select r.CALCCODE,r.CALCNAME,r.VALCOL from SQM_FEE_CALC_REF r
                        left join SQM_DJ_PSF p on r.feecode=p.feecode
                        where p.Rid='{0}' and r.CACLUNIT='{1}' and r.STATUS='1' {2} order by r.SORD asc";
                sql = string.Format(sql, djrid, calcunit, where);
                DataTable FCREFdt = DataHelper.QueryDataTable(sql);
                if (FCREFdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in FCREFdt.Rows)
                    {
                        filedkeys += dr["VALCOL"].ToString() + " as " + dr["CALCCODE"] + ",";
                        //filedkeys += dr["VALCOL"].ToString() + ",";
                    }
                }
                if (min)
                {
                    filedkeys += "MIN,";
                }
                filedkeys += "PURPRICE,COSTPRICE,MAXPRICE,MINPRICE,GUIDEPRICE,to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,DJSTATUS,MEMO";
                return filedkeys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 得到原始数据，用来检测表中是否已存在即将插入的数据   
        /// </summary>
        /// <param name="srcobj">要插入表的数据</param>
        /// <param name="fields">主键</param>
        /// <returns></returns>
        [AllowAnonymous]
        public DataTable FindSourceData(SQM_MODEDJ_VAL srcobj, string[] fields)
        {
           // string sql1 = "select RID,DJSTATUS,STARTDATE,ENDDATE from SQM_MODEDJ_VAL where ";//原SQL语句，不含指定客户字段
            string sql1 = "select RID,DJSTATUS,STARTDATE,ENDDATE,CUSTOMERSNO from SQM_MODEDJ_VAL where ";
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i] == "CUSTOMERSNO")
                {
                    continue;
                }
                if (i < fields.Length - 1)
                {
                    if (srcobj.GetValue(fields[i]) == null) //数字类型
                    {
                        sql1 += fields[i] + " is null and ";
                    }
                    else if (String.IsNullOrEmpty(srcobj.GetValue(fields[i]).ToString()))  //字符串类型
                    {
                        if (fields[i] != "CUSTOMERSNO")
                        {
                            sql1 += fields[i] + " is null and ";
                        }
                    }
                    else
                    {
                        if(fields[i] == "CUSTOMERSNO")
                        {
                            if (srcobj.GetValue(fields[i]) != null) {
                                string customersnostr = "(";
                                var arrval = srcobj.GetValue(fields[i]).ToString().Split(',');
                                foreach (var val in arrval)
                                {
                                    if(customersnostr != "(")
                                    {
                                        customersnostr = customersnostr + " or ";
                                    }
                                    //instr(CUSTOMERSNO, 'AAAAA01') > 0
                                    customersnostr = customersnostr + "instr(" + fields[i] + ", '" + val + "') > 0";
                                }
                                customersnostr += ")";
                                sql1 += customersnostr + " and ";
                            }
                        }
                        else
                        {
                            sql1 += fields[i] + " = '" + srcobj.GetValue(fields[i]) + "' and ";
                        }
                    }
                }
                else
                {
                    if (srcobj.GetValue(fields[i]) == null) //数字类型
                    {
                        sql1 += fields[i] + " is null and STATUS = '1' order by  djstatus desc";//order by STARTDATE  20200423 dz edit
                    }
                    else if (String.IsNullOrEmpty(srcobj.GetValue(fields[i]).ToString()))  //字符串类型
                    {
                        sql1 += fields[i] + " is null and STATUS = '1' order by  djstatus desc ";//order by STARTDATE  20200423 dz edit
                    }
                    else
                    {
                        sql1 += fields[i] + " = '" + srcobj.GetValue(fields[i]) + "' and STATUS = '1' order by  djstatus desc";//order by STARTDATE  20200423 dz edit
                    }
                }
            }
            DataTable dt = DataHelper.QueryDataTable(sql1);
            return dt;
        }
        /// <summary>
        /// 处理有效期，原始数据失效，生成新数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startDate_old"></param>
        /// <param name="endDate_old"></param>
        /// <param name="targetobj"></param>
        /// <param name="srcobj"></param>
        /// <param name="dt"></param>
        [AllowAnonymous]
        public void HandleValidDate(DateTime startDate, DateTime endDate, DateTime startDate_old, DateTime endDate_old, SQM_MODEDJ_VAL targetobj, SQM_MODEDJ_VAL srcobj, DataRow[] oldrows)
        {
            // 以下代码为原始数据有效期 取头去尾
            if (endDate < startDate_old) // 最左 原始数据不失效
            {
                targetobj = srcobj;
                targetobj.DJSTATUS = "0";
                targetobj.STARTDATE = startDate;
                targetobj.ENDDATE = endDate;
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();
            }
            else if (startDate > endDate_old) // 最右 原始数据不失效
            {
                targetobj = srcobj;
                targetobj.DJSTATUS = "0";
                targetobj.STARTDATE = startDate;
                targetobj.ENDDATE = endDate;
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();
            }
            else if (startDate == startDate_old && endDate <= endDate_old) //区间内 全覆盖 
            {
                // 原始数据失效
                foreach (DataRow row in oldrows)
                {
                    targetobj = SQM_MODEDJ_VAL.TryFind(row["RID"]);
                    targetobj.STATUS = "0";
                    targetobj.DJSTATUS = "0";
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoSave();
                }
                // 数据新增
                targetobj = srcobj;
                targetobj.STARTDATE = startDate;
                targetobj.ENDDATE = endDate;
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();
            }
            else if (startDate <= startDate_old && endDate >= endDate_old) //区间内 全覆盖 
            {
                // 原始数据失效
                foreach (DataRow row in oldrows)
                {
                    targetobj = SQM_MODEDJ_VAL.TryFind(row["RID"]);
                    targetobj.STATUS = "0";
                    targetobj.DJSTATUS = "0";
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoSave();
                }
                // 数据新增
                targetobj = srcobj;
                targetobj.STARTDATE = startDate;
                targetobj.ENDDATE = endDate;
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();
            }
            else if (startDate < startDate_old && endDate >= startDate_old && endDate < endDate_old) // 部分覆盖 
            {
                // 原始数据失效
                foreach (DataRow row in oldrows)
                {
                    targetobj = SQM_MODEDJ_VAL.TryFind(row["RID"]);
                    targetobj.STATUS = "0";
                    targetobj.DJSTATUS = "0";
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoSave();
                }
                //targetobj = targetobj;
                //targetobj.DJSTATUS = "1";
                //targetobj.STARTDATE = endDate.AddDays(1);
                //targetobj.ENDDATE = endDate_old;
                //targetobj.DoCreate();

                // 数据新增
                targetobj = srcobj;
                targetobj.DJSTATUS = "0";
                targetobj.STARTDATE = startDate;
                targetobj.ENDDATE = endDate;
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();
            }
            else if (startDate > startDate_old && startDate <= endDate_old) // 部分覆盖 
            {
                // 原始数据失效
                // 所有原始数据失效
                foreach (DataRow row in oldrows)
                {
                    targetobj = SQM_MODEDJ_VAL.TryFind(row["RID"]);
                    targetobj.STATUS = "0";
                    targetobj.DJSTATUS = "0";
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoSave();
                }
                if (startDate < DateTime.Now)
                {
                    // 数据新增
                    targetobj = srcobj;
                    targetobj.DJSTATUS = "0";
                    targetobj.STARTDATE = startDate;
                    targetobj.ENDDATE = endDate;
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoCreate();
                }
                else
                {
                    targetobj = targetobj;
                    targetobj.DJSTATUS = "1";
                    targetobj.STARTDATE = DateTime.Now;
                    //targetobj.STARTDATE = startDate_old;
                    targetobj.ENDDATE = startDate.AddDays(-1);
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoCreate();

                    // 数据新增
                    targetobj = srcobj;
                    targetobj.DJSTATUS = "0";
                    targetobj.STARTDATE = startDate;
                    targetobj.ENDDATE = endDate;
                    targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoCreate();
                }
            }
        }
        [AllowAnonymous]
        public ActionResult PurDelete()
        {
            string[] rowIdArr = Request["rowIds"].Split(',');
            try
            {
                SQM_MODEDJ_VAL[] ents = SQM_MODEDJ_VAL.FindAll(Expression.In("RID", rowIdArr));
                foreach (SQM_MODEDJ_VAL ent in ents)
                {
                    if (ent.DJSTATUS == "1")
                    {
                        string msg = "0";
                        SQM_PURController.SendEmail(ent.RID, msg);
                    }
                    ent.STATUS = "0";
                    ent.DoUpdate();
                }
                return Content("删除成功!");
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
        }
        /// <summary>
        /// 逻辑删除，更新状态为失效
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DeleteRow()
        {
            try
            {
                string id = Request["rowId"];
                SQM_MODEDJ_VAL ent = SQM_MODEDJ_VAL.Find(id);
                if (ent.DJSTATUS == "1")
                {
                    string msg = "0";
                    SQM_PURController.SendEmail(id, msg);
                }
                ent.STATUS = "0";
                ent.DJSTATUS = "0";
                ent.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                ent.DoUpdate();

            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }
        public ActionResult Export()
        {
            string djrid = Request["djrid"];
            string feecode = Request["feecode"];
            string feename = Request["feename"];
            string filePath = "";
            string fileName = "";
            if (string.IsNullOrEmpty(djrid))
            {
                return Content(new JsonMessage { Message = "Excel导出失败：获取定价信息失败！" }.ToString());
            }
            try
            {
                bool isTitle = true;
                string sql_search = "";
                Workbook workbook = new Workbook();
                // 清除默认sheet页
                workbook.Worksheets.Clear();
                workbook.Worksheets.Add(feename);// 新建sheet页
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                // 开始绘制
                int rowIndex = 0;
                // 获取是否多定价方式 如果是"否"，则只有一套基础,如果是"是"，则同时存在"是"和"否"的基础，但是只取"是"的基础
                string muldjfs = DataHelper.QueryValue("select MULBJFS from SQM_FEE_CALC where FEECODE = '" + feecode + "'") + "";
                string djfsrid = "";
                string gdzrid = "";
                worksheet.Name = feename + "|" + feecode;
                // 获取定价方式rid
                if (muldjfs == "1") // 多报价方式：是
                {
                    IList<EasyDictionary> ediclist = DataHelper.QueryDictList("select distinct DJFSRID,DJFSNAME from SQM_FEE_PUR_REF where STATUS='1' and FEECODE='" + feecode + "' order by DJFSNAME");
                    foreach (EasyDictionary ed in ediclist)// 遍历定价方式
                    {
                        isTitle = true;
                        djfsrid = ed.Get("DJFSRID") + "";
                        // 是否高低值
                        IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID,GDZNAME from SQM_FEE_PUR_REF where STATUS='1' and DJFSRID='" + djfsrid + "' order by GDZNAME");
                        if (gdzlist.Count > 0)
                        {
                            // 遍历高低值
                            foreach (EasyDictionary gdz in gdzlist)
                            {
                                isTitle = true;
                                gdzrid = gdz.Get("GDZRID") + "";
                                sql_search = SearchSqlAll(djrid, feecode, "0", djfsrid, gdzrid);
                                CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                            }
                        }
                        else // 无高低值
                        {
                            sql_search = SearchSqlAll(djrid, feecode, "0", djfsrid, "");
                            CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                        }
                    }
                }
                else// 多报价方式：否
                {
                    sql_search = SearchSqlAll(djrid, feecode, "0", "", "");
                    CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                }
                // 列宽自适应
                worksheet.AutoFitColumns();
                // 隐藏列
                worksheet.Cells.HideColumns(0, 4);

                // 生成Excel文件
                fileName = feename + "_定价(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + ".xlsx";
                filePath = System.IO.Path.Combine(Server.MapPath("/Excel/excel_output/"), fileName);
                workbook.Save(filePath);
                return Content(new JsonMessage { Message = "/Excel/excel_output/" + fileName, Success = true }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "Excel下载失败：" + ex.Message, Success = false }.ToString());
            }
        }
        Dictionary<string, string> dictionary = new Dictionary<string, string>(); // 全局变量，Excel下载生成标题时用到
        /// <summary>
        /// 拼查询值表的sql
        /// </summary>
        /// <param name="feeid">值表的费目id</param>
        /// <param name="feecode">费目代码</param>
        /// <returns></returns>
        public string SearchSqlAll(string feeid, string feecode, string status, string djfsrid, string gdzrid)
        {
            string sql_val = "";
            string minprice = "";
            DataTable dt = new DataTable();
            string sql_ref = "";
            if (djfsrid == "")// 是否多报价方式：否
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID = '' or DJFSRID is null)";
                sql_val = "select 0,1,CURRENCY";
            }
            else if (djfsrid != "" && gdzrid == "")// 是否多报价方式：是，无高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and DJFSRID = '" + djfsrid + "'";
                sql_val = "select '" + djfsrid + "',1,CURRENCY";
            }
            else if (djfsrid != "" && gdzrid != "")// 是否多报价方式：是，存在高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and GDZRID = '" + gdzrid + "'";
                sql_val = "select '" + djfsrid + "','" + gdzrid + "',CURRENCY";
            }
            dt = DataHelper.QueryDataTable(sql_ref);

            if (dt.Rows.Count > 0)// 费目没有基础数据不能查询值表数据
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dictionary.ContainsKey(dr["CALCCODE"].ToString()))
                    {
                        dictionary.Add(dr["CALCCODE"].ToString(), dr["CALCNAME"].ToString());
                    }
                    string bd = dr["SCALE"].ToString() + "" == "" ? "*" : dr["SCALE"].ToString() + "";
                    sql_val += "," + dr["VALCOL"].ToString() + " as \"" + dr["CALCCODE"].ToString() + "(" + bd + ")\"";
                }
            }
            if (djfsrid == "")
            {
                //MIN判断
                minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                if (minprice == "1")
                {
                    sql_val += ",MIN";
                }
                sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,COSTPRICE,PURPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,DJSTATUS,MEMO from SQM_MODEDJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "'";
            }
            else if (djfsrid != "" && gdzrid == "")
            {
                //MIN判断
                minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                if (minprice == "1")
                {
                    sql_val += ",MIN";
                }
                sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,COSTPRICE,PURPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,DJSTATUS,MEMO from SQM_MODEDJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "' and DJFSRID ='" + djfsrid + "'";
            }
            else if (djfsrid != "" && gdzrid != "")
            {
                //MIN判断
                minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                if (minprice == "1")
                {
                    sql_val += ",MIN";
                }
                sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,COSTPRICE,PURPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,DJSTATUS,MEMO from SQM_MODEDJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "' and GDZRID ='" + gdzrid + "'";
            }
            sql_val += " ORDER BY CREATETIME desc";
            return sql_val;
        }
        private void CereateExcel2(Cells cells, ref int rowIndex, ref bool isTitle, string sql_search)
        {
            DataTable dtDetail = DataHelper.QueryDataTable(sql_search);
            if (dtDetail.Rows.Count == 0)
            {
                int colIndex = 0;
                // 绘制一行空行
                if (rowIndex != 0)
                {
                    foreach (DataColumn dtcol in dtDetail.Columns)
                    {
                        cells[rowIndex, colIndex].PutValue("");
                        colIndex++;
                    }
                    colIndex = 0;
                    rowIndex++;
                }
                cells[rowIndex, colIndex].PutValue("定价方式ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue(dtDetail.Columns[0].ToString());
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue(dtDetail.Columns[1].ToString());
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("定价方式");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                int ipd = 0;
                foreach (DataColumn dtcol in dtDetail.Columns)
                {
                    if (ipd < 2)
                    {
                        ipd++;
                        continue;
                    }
                    if (dtcol.ColumnName == "CURRENCY")
                    {
                        cells[rowIndex, colIndex].PutValue("币种");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "STARTDATE")
                    {
                        cells[rowIndex, colIndex].PutValue("起始日期");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "ENDDATE")
                    {
                        cells[rowIndex, colIndex].PutValue("截止日期");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "GUIDEPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("指导价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MINPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最低价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MAXPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最高价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "PURPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("采购价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "COSTPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("成本价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "DJSTATUS")
                    {
                        cells[rowIndex, colIndex].PutValue("定价状态");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MEMO")
                    {
                        cells[rowIndex, colIndex].PutValue("备注");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCUNIT")
                    {
                        cells[rowIndex, colIndex].PutValue("计费单位");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCTYPE")
                    {
                        cells[rowIndex, colIndex].PutValue("计算方式");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MIN")
                    {
                        cells[rowIndex, colIndex].PutValue("MIN");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "DJFSRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "GDZRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName.IndexOf("=)") >= 0)
                    {
                        string code = dtcol.ColumnName.Split('(')[0];// 取基础代码，得出基础名称
                        string scale = "(" + dtcol.ColumnName.Split('(')[1]; // 标度
                        cells[rowIndex, colIndex].PutValue(dictionary[code] + "(" + code + ")" + scale);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName.IndexOf("*)") >= 0)
                    {
                        string code = dtcol.ColumnName.Split('(')[0];// 取基础代码，得出基础名称
                        string scale = "()"; // 标度
                        cells[rowIndex, colIndex].PutValue(dictionary[code] + "(" + code + ")" + scale);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue(dtcol.ColumnName);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    colIndex++;
                }
                colIndex = 0;
                rowIndex++;
            }
            else
            {
                foreach (DataRow drDetail in dtDetail.Rows)
                {
                    int colIndex = 0;
                    DrawTitle(cells, ref rowIndex, ref isTitle, dtDetail, ref colIndex);
                    // 绘制内容
                    if (drDetail.Table.Columns.Contains("DJFSRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["DJFSRID"] + "");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    if (drDetail.Table.Columns.Contains("GDZRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["GDZRID"] + "");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 定价方式名称
                    string djfsname = DataHelper.QueryValue("select distinct DJFSNAME from SQM_FEE_PUR_REF where DJFSRID = '" + drDetail["DJFSRID"] + "'") + "";
                    cells[rowIndex, colIndex].PutValue(djfsname);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 高低值名称
                    string gdzname = DataHelper.QueryValue("select distinct GDZNAME from SQM_FEE_PUR_REF where GDZRID is not null and GDZRID = '" + drDetail["GDZRID"] + "'") + "";
                    cells[rowIndex, colIndex].PutValue(gdzname);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    int ipd = 0;
                    foreach (DataColumn dtcol in dtDetail.Columns)
                    {
                        string value = "";
                        if (ipd < 2)
                        {
                            ipd++;
                            continue;
                        }
                        if (dtcol.ColumnName == "DJFSRID")
                        {
                            continue;
                        }
                        if (dtcol.ColumnName == "GDZRID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "CALCTYPE")
                        {
                            if (drDetail[dtcol.ColumnName].ToString() == "A")
                            {
                                value = "绝对值";
                            }
                            else if (drDetail[dtcol.ColumnName].ToString() == "B")
                            {
                                value = "相对值";
                            }
                            cells[rowIndex, colIndex].PutValue(value);
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        }
                        else if (dtcol.ColumnName == "DJSTATUS")
                        {
                            if (drDetail[dtcol.ColumnName].ToString() == "1")
                            {
                                value = "已发布";
                            }
                            else
                            {
                                value = "未发布";
                            }
                            cells[rowIndex, colIndex].PutValue(value);
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        }
                        else
                        {
                            cells[rowIndex, colIndex].PutValue(drDetail[dtcol.ColumnName]);
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        }
                        colIndex++;
                    }
                    rowIndex++;
                }
            }
        }
        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="rowIndex"></param>
        /// <param name="isTitle"></param>
        /// <param name="dtDetail"></param>
        /// <param name="colIndex"></param>
        private void DrawTitle(Cells cells, ref int rowIndex, ref bool isTitle, DataTable dtDetail, ref int colIndex)
        {
            // 绘制标题
            if (isTitle)
            {
                // 绘制一行空行
                if (rowIndex != 0)
                {
                    foreach (DataColumn dtcol in dtDetail.Columns)
                    {
                        cells[rowIndex, colIndex].PutValue("");
                        colIndex++;
                    }
                    colIndex = 0;
                    rowIndex++;
                }
                cells[rowIndex, colIndex].PutValue("定价方式ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue(dtDetail.Columns[0].ToString());
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue(dtDetail.Columns[1].ToString());
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("定价方式");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                int ipd = 0;
                foreach (DataColumn dtcol in dtDetail.Columns)
                {
                    if (ipd < 2)
                    {
                        ipd++;
                        continue;
                    }
                    if (dtcol.ColumnName == "CURRENCY")
                    {
                        cells[rowIndex, colIndex].PutValue("币种");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "STARTDATE")
                    {
                        cells[rowIndex, colIndex].PutValue("起始日期");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "ENDDATE")
                    {
                        cells[rowIndex, colIndex].PutValue("截止日期");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "GUIDEPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("指导价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MINPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最低价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MAXPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最高价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "PURPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("采购价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "COSTPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("成本价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "DJSTATUS")
                    {
                        cells[rowIndex, colIndex].PutValue("定价状态");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MEMO")
                    {
                        cells[rowIndex, colIndex].PutValue("备注");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCUNIT")
                    {
                        cells[rowIndex, colIndex].PutValue("计费单位");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCTYPE")
                    {
                        cells[rowIndex, colIndex].PutValue("计算方式");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MIN")
                    {
                        cells[rowIndex, colIndex].PutValue("MIN");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "DJFSRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "GDZRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName.IndexOf("=)") >= 0)
                    {
                        string code = dtcol.ColumnName.Split('(')[0];// 取基础代码，得出基础名称
                        string scale = "(" + dtcol.ColumnName.Split('(')[1]; // 标度
                        cells[rowIndex, colIndex].PutValue(dictionary[code] + "(" + code + ")" + scale);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName.IndexOf("*)") >= 0)
                    {
                        string code = dtcol.ColumnName.Split('(')[0];// 取基础代码，得出基础名称
                        string scale = "()"; // 标度
                        cells[rowIndex, colIndex].PutValue(dictionary[code] + "(" + code + ")" + scale);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue(dtcol.ColumnName);
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    colIndex++;
                }
                colIndex = 0;
                rowIndex++;
                isTitle = false;
            }
        }
        #region 测试需要，暂时先注释掉
        //[AllowAnonymous]
        //[System.Web.Http.HttpPost]
        //[ValidateInput(false)]
        //public ActionResult PostExcelData()
        //{
        //    string djrid = Request["djrid"];
        //    List<string> colname = new List<string>();
        //    List<string> names = new List<string>();
        //    List<DataSet> listDs = new List<DataSet>();

        //    try
        //    {
        //        //获取客户端上传的文件集合
        //        HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
        //        //判断是否存在文件
        //        if (files.Count > 0 || listDs.Count > 0)
        //        {
        //            ArrayList al = new ArrayList();
        //            if (files.Count > 0)
        //            {
        //                // 获取文件集合中的第一个文件(每次只上传一个文件)
        //                HttpPostedFile file = files[0];
        //                System.IO.Stream stream = file.InputStream;
        //                al = GetDataFromExcel2(stream);
        //            }
        //            if (listDs.Count == 0)
        //            {
        //                listDs = (List<DataSet>)al[0];
        //            }
        //            // 主数据校验 入库时计算基础如果没有code值，自动带出code，并将code放入值表的columnc列。
        //            DataSet ds = listDs[0];
        //            string feeName = "";
        //            string feeCode = "";
        //            if (ds.DataSetName.IndexOf('|') >= 0)
        //            {
        //                feeName = ds.DataSetName.Split('|')[0];
        //                feeCode = ds.DataSetName.Replace(ds.DataSetName.Split('|')[0] + "|", "");
        //            }
        //            else
        //            {
        //                return Content(new JsonMessage { Message = "导入终止：费目代码不正确，请确认！", Code = "1" }.ToString());
        //            }
        //            for (int m = 0; m < ds.Tables.Count; m++)
        //            {
        //                if (ds.Tables[m].Rows.Count == 0)
        //                {
        //                    continue;
        //                    //return Content(new JsonMessage { Message = "导入终止：请维护定价数据！", Code = "1" }.ToString());
        //                }
        //                string gdzrid = "";
        //                if (ds.Tables[m].Columns[3].ToString() != "1")
        //                {
        //                    gdzrid = ds.Tables[m].Columns[3].ToString().TrimEnd('\'').ToLower();
        //                }
        //                // 每个table只有一个定价方式,所以只取第一行数据
        //                string djfsrid = "";
        //                if (ds.Tables[m].Columns[1].ToString() != "0")
        //                {
        //                    djfsrid = ds.Tables[m].Columns[1].ToString().TrimEnd('\'').ToLower();
        //                }
        //                DataTable dtjc = new DataTable();
        //                string mul = DataHelper.QueryValue("select mulbjfs from sqm_fee_calc where feecode = '" + feeCode + "'") + "";
        //                if (mul == "")
        //                {
        //                    return Content(new JsonMessage { Message = "导入终止：费目代码不正确，请确认！", Code = "1" }.ToString());
        //                }
        //                else if (mul == "1")
        //                {
        //                    if (gdzrid != "")
        //                    {
        //                        dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and GDZRID = '" + gdzrid + "' order by VALCOL asc");
        //                    }
        //                    else if (djfsrid != "")
        //                    {
        //                        dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID = '" + djfsrid + "' order by VALCOL asc");
        //                    }
        //                    else
        //                    {
        //                        return Content(new JsonMessage { Message = "导入失败：\"" + ds.Tables[m].Rows[0]["定价方式"] + "\" 的数据未填写\"定价方式ID\"！", Code = "1" }.ToString());
        //                    }
        //                }
        //                else if (mul == "0")
        //                {
        //                    dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID is null order by VALCOL asc");
        //                }
        //                //一个table里面定价方式和高低值一样
        //                string mesval = "";
        //                if (!String.IsNullOrEmpty(ds.Tables[m].Rows[0]["定价方式"].ToString()))
        //                {
        //                    mesval += ds.Tables[m].Rows[0]["定价方式"].ToString() + "->";
        //                }
        //                if (!String.IsNullOrEmpty(ds.Tables[m].Rows[0]["高低值"].ToString()))
        //                {
        //                    mesval += ds.Tables[m].Rows[0]["高低值"].ToString() + "->";
        //                }
        //                string minall = "";
        //                for (int n = ds.Tables[m].Rows.Count - 1; n >= 0; n--)
        //                {
        //                    if (dtjc.Rows.Count > 0)// 普通计费基础
        //                    {
        //                        string djdw = "";
        //                        string djdwval = "";
        //                        string jsfsval = "";
        //                        string djztval = "";
        //                        DataTable maindt = null;
        //                        //币种校验
        //                        string bzval = ds.Tables[m].Rows[n]["币种"].ToString();
        //                        if (String.IsNullOrEmpty(bzval))
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种不能为空！", Code = "1" }.ToString());
        //                        }
        //                        else
        //                        {
        //                            if (bzval.ToUpper() == "RMB")
        //                            {
        //                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种不能为\"" + bzval + "\"", Code = "1" }.ToString());
        //                            }
        //                            maindt = MainDataExist("", bzval, "3");
        //                            if (maindt == null || maindt.Rows.Count == 0)
        //                            {
        //                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种\"" + bzval + "\" 主数据中不存在", Code = "1" }.ToString());
        //                            }
        //                        }
        //                        //判断MIN值是否一样且是必填项
        //                        string minval = "";
        //                        if (ds.Tables[m].Rows[n].Table.Columns.Contains("MIN"))
        //                        {
        //                            minval = ds.Tables[m].Rows[n]["MIN"].ToString();
        //                            if (String.IsNullOrEmpty(minval))
        //                            {
        //                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行MIN不能为空！", Code = "1" }.ToString());
        //                            }
        //                            //else if (!String.IsNullOrEmpty(minall) && minall != minval)
        //                            //{
        //                            //    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行MIN需要维护一样！", Code = "1" }.ToString());
        //                            //}
        //                            else
        //                            {
        //                                minall = minval;
        //                            }
        //                        }
        //                        //计费单位、计算方式、定价状态校验
        //                        djdwval = ds.Tables[m].Rows[n]["计费单位"].ToString();
        //                        jsfsval = ds.Tables[m].Rows[n]["计算方式"].ToString();
        //                        djztval = ds.Tables[m].Rows[n]["定价状态"].ToString();
        //                        if (!(jsfsval == "相对值" || jsfsval == "绝对值"))
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计算方式请维护相对值或绝对值！", Code = "1" }.ToString());
        //                        }
        //                        if (!(djztval == "未发布" || djztval == "已发布"))
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行定价状态请维护未发布或已发布！", Code = "1" }.ToString());
        //                        }
        //                        //价格必填校验
        //                        if (String.IsNullOrEmpty(ds.Tables[m].Rows[n]["最高价"].ToString()) || String.IsNullOrEmpty(ds.Tables[m].Rows[n]["最低价"].ToString()) || String.IsNullOrEmpty(ds.Tables[m].Rows[n]["指导价"].ToString()) || String.IsNullOrEmpty(ds.Tables[m].Rows[n]["成本价"].ToString()) || String.IsNullOrEmpty(ds.Tables[m].Rows[n]["采购价"].ToString()))
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行价格不能为空！", Code = "1" }.ToString());
        //                        }
        //                        //起始日期不能大于截止日期
        //                        DateTime startDate = Convert.ToDateTime(ds.Tables[m].Rows[n]["起始日期"].ToString());
        //                        DateTime endDate = Convert.ToDateTime(ds.Tables[m].Rows[n]["截止日期"].ToString());
        //                        if (startDate > endDate)
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行起始日期不能大于截止日期！", Code = "1" }.ToString());
        //                        }
        //                        // 基础校验
        //                        foreach (DataRow dr in dtjc.Rows)
        //                        {
        //                            if (!String.IsNullOrEmpty(dr["MSRUNIT"].ToString()))
        //                            {
        //                                djdw += dr["MSRUNIT"].ToString() + "/";
        //                            }
        //                            string jsjccode = dr["CALCCODE"].ToString();
        //                            string jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
        //                            // 是否为空校验 如果不是X类型，其他基础不能为空
        //                            if (!CheckIfX(jsjccode))
        //                            {
        //                                if (String.IsNullOrEmpty(jcvalue))
        //                                {
        //                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":数据不能为空！", Code = "1" }.ToString());
        //                                }
        //                            }
        //                            // 主数据校验
        //                            if (NoCheckJc(jsjccode) || jsjccode == "SOURCELOC_ZONE")
        //                            {
        //                                //数据为*的不用校验了
        //                                if (jcvalue == "*")
        //                                {
        //                                    listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
        //                                    continue;
        //                                }
        //                                // 通用主数据 MDM
        //                                if (jsjccode.IndexOf("GJ") >= 0) // 国家
        //                                {
        //                                    if (jcvalue != "")
        //                                    {
        //                                        maindt = MainDataExist("", jcvalue, "1");
        //                                        if (maindt == null || maindt.Rows.Count == 0)
        //                                        {
        //                                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
        //                                        }
        //                                        else// if ((code != jcvalue) && (code != jcvalue.ToUpper()) && (code != jcvalue.ToLower()))
        //                                        {
        //                                            // 基础数据如果不是code则变为code -> code带出来，放入值表columnc列
        //                                            listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                // MDMLOC
        //                                else if (jsjccode.IndexOf("QYG") >= 0 || jsjccode.IndexOf("ZZYG") >= 0 || jsjccode.IndexOf("MDG") >= 0 || jsjccode.IndexOf("ZZG") >= 0 || jsjccode.IndexOf("HX") >= 0)// 港口+航线
        //                                {
        //                                    if (jcvalue != "")
        //                                    {
        //                                        maindt = MainDataExist("", jcvalue, "2");
        //                                        if (maindt == null || maindt.Rows.Count == 0)
        //                                        {
        //                                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
        //                                        }
        //                                        else
        //                                        {
        //                                            listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                // MDMBP
        //                                else if (jsjccode.IndexOf("HKGS") >= 0 || jsjccode.IndexOf("CGS") >= 0)// 航空公司、船公司
        //                                {
        //                                    if (jcvalue != "")
        //                                    {
        //                                        maindt = MainDataExist("", jcvalue, "4");
        //                                        if (maindt == null || maindt.Rows.Count == 0)
        //                                        {
        //                                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
        //                                        }
        //                                        else
        //                                        {
        //                                            listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
        //                                        }
        //                                    }
        //                                }
        //                                // 计算基础 MDMJC
        //                                else
        //                                {
        //                                    try
        //                                    {
        //                                        maindt = MainDataExist(jsjccode, jcvalue, "6");
        //                                        if (maindt == null || maindt.Rows.Count == 0)
        //                                        {
        //                                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
        //                                        }
        //                                        else// if ((code != jcvalue) && (code != jcvalue.ToUpper()) && (code != jcvalue.ToLower()))
        //                                        {
        //                                            listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
        //                                        }
        //                                    }
        //                                    catch (Exception ex)
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + ex.Message + " " + mesval + "数据第" + (++n) + "行基础为: " + jsjccode + "的主数据配置错误！", Code = "1" }.ToString());
        //                                    }
        //                                }
        //                            }
        //                            // 不校验主数据的基础，校验长度
        //                            else
        //                            {
        //                                //不校验主数据的基础code也要存数值
        //                                string maxlen = "";
        //                                if (!String.IsNullOrEmpty(jcvalue))
        //                                {
        //                                    string result = CheckData(jsjccode, jcvalue);
        //                                    string valok = result.Substring(0, 1);//错误代码
        //                                    if (valok == "1" || valok == "2" || valok == "3")
        //                                    {
        //                                        maxlen = result.Substring(1, result.Length - 1);//最大值
        //                                    }
        //                                    if (valok == "1")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，整数位最长" + maxlen + "位", Code = "1" }.ToString());
        //                                    }
        //                                    else if (valok == "2")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，小数位最长" + maxlen + "位", Code = "1" }.ToString());
        //                                    }
        //                                    else if (valok == "3")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，数据最长" + maxlen + "位", Code = "1" }.ToString());
        //                                    }
        //                                    else if (valok == "4")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 请维护数字型数据", Code = "1" }.ToString());
        //                                    }
        //                                    else if (valok == "5")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据应为'X'或空", Code = "1" }.ToString());
        //                                    }
        //                                    else if (valok == "6")
        //                                    {
        //                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据应为'Y'或'N'", Code = "1" }.ToString());
        //                                    }
        //                                    else
        //                                    {
        //                                        listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
        //                                }
        //                            }
        //                        }
        //                        //单位校验
        //                        if (jsfsval == "绝对值" && djdwval != "票")
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为\"" + "票" + "\"", Code = "1" }.ToString());
        //                        }
        //                        else if (jsfsval == "相对值" && djdwval != djdw.TrimEnd('/'))
        //                        {
        //                            if (!String.IsNullOrEmpty(djdw.TrimEnd('/')))
        //                            {
        //                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为\"" + djdw.TrimEnd('/') + "\"", Code = "1" }.ToString());
        //                            }
        //                            else
        //                            {
        //                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为空", Code = "1" }.ToString());
        //                            }
        //                        }
        //                        //excel重复数据判断
        //                        int hbj = 1;//行标记
        //                        bool cfsj = imPortData(listDs[0].Tables[m], dtjc, ref hbj);
        //                        if (cfsj)
        //                        {
        //                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "第" + (++hbj) + "行导入数据有效期存在交叉，请确认！", Code = "1" }.ToString());
        //                        }
        //                    }
        //                    else // 不存在计费基础的
        //                    {

        //                    }
        //                }
        //            }

        //            // 数据入库  入库之前先把表原始数据失效
        //            DeleteDataSource(djrid, "update");
        //            // insert操作
        //            string msg = InsertData(ref colname, listDs, djrid);
        //            if (msg != "T")
        //            {
        //                return Content(new JsonMessage { Message = "导入中止: " + msg, Code = "2" }.ToString());
        //            }
        //            else
        //            {
        //                //DeleteDataSource(mrid, vrid, bjname, version, "delete");// 删除status为0的数据
        //            }
        //            return Content(new JsonMessage { Message = "导入成功", Code = "0" }.ToString());
        //        }
        //        else
        //        {
        //            return Content(new JsonMessage { Message = "导入失败,文件不存在", Code = "1" }.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
        //    }
        //} 
        #endregion
        /// <summary>
        /// 是否为X类型
        /// </summary>
        /// <param name="jsjccode"></param>
        /// <returns></returns>
        private bool CheckIfX(string jsjccode)
        {
            bool ifX = false;
            DataRow[] drs = dtdatalen.Select("calc_base = '" + jsjccode + "'");
            if (drs.Length > 0)
            {
                string x = drs[0]["CALCTYPE"] + "";
                if (x == "X")
                {
                    ifX = true;
                }
            }
            return ifX;
        }
        public static DataTable dataTable;
        public static DataRow dataRow;
        /// <summary>
        /// 是否包含非校验项   改为校验哪些项  基础800多条，给的主数据只有47条
        /// </summary>
        /// <param name="jsjccode"></param>
        /// <returns></returns>
        #region 测试需要，暂时先注释掉
        //private static DataTable mdatadt = DataHelper.QueryDataTable("select distinct mdkey from mdm_calc_value");
        //private static bool NoCheckJc(string jsjccode)
        //{
        //    bool sign = false;
        //    // 港口 国家 航空公司 船公司
        //    string[] mustCheck = { "GJ", "QYG", "MDG", "ZZG", "ZYG", "HKGS", "CGS" };
        //    for (int c = 0; c < mustCheck.Length; c++)
        //    {
        //        if (jsjccode.IndexOf(mustCheck[c]) >= 0)
        //        {
        //            sign = true;
        //            break;
        //        }
        //    }
        //    if (!sign)
        //    {
        //        DataRow[] dr = mdatadt.Select("MDKEY = '" + jsjccode + "'");
        //        if (dr.Length > 0)
        //        {
        //            sign = true;
        //        }
        //    }
        //    return sign;
        //}
        #endregion
        /// <summary>
        /// 原数据删除（数据校验）   导入失败恢复删除数据
        /// </summary>
        /// <param name="mrid"></param>
        /// <param name="vrid"></param>
        /// <param name="bjname"></param>
        /// <param name="version"></param>
        private static void DeleteDataSource(string djrid, string type)
        {
            // 删除sqm_modedj_val表
            string sql_smv = "";
            if (type == "update")
            {
                sql_smv = "update SQM_MODEDJ_VAL set STATUS = '0' where FEECALCID = '" + djrid + "' and STATUS = '1'";
            }
            else if (type == "delete")
            {
                sql_smv = "delete from SQM_MODEDJ_VAL where FEECALCID ='" + djrid + "' and STATUS = '0'";
            }
            DataHelper.ExecSql(sql_smv);
        }

        /// <summary>
        /// 数据入库（Excel上传）
        /// </summary>
        /// <param name="test"></param>
        /// <param name="colname"></param>
        /// <param name="listDs"></param>
        /// <param name="mrid"></param>
        /// <param name="feecalcid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        private string InsertData(ref List<string> colname, List<DataSet> listDs, string djrid)
        {
            string msg = "T";
            try
            {
                foreach (DataSet ds in listDs)
                {
                    string createTime = DateTime.Now.ToString();
                    string createUser = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    string createId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    string feeName = ds.DataSetName.Trim().Split('|')[0];
                    string feeCode = ds.DataSetName.Replace(ds.DataSetName.Split('|')[0] + "|", "");
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<string> col = new List<string>();
                            foreach (DataColumn col1 in dt.Columns)
                            {
                                if (col1.ColumnName.IndexOf("Column") < 0)
                                {
                                    col.Add(col1.ColumnName);
                                }
                            }
                            colname = col;
                            // 报价方式：bjfs  1-At cost、2-单票单询
                            string bjfs = string.Empty;
                            string djstataus = "";
                            string bz = string.Empty;
                            string memo = string.Empty;
                            string min = string.Empty;
                            string calcunit = string.Empty;
                            string val_rid = string.Empty;
                            string djzdj = "";
                            string djzgj = "";
                            string djzhidj = "";
                            string costprice = "";
                            string purprice = "";
                            string calctype = "";
                            string djfsrid = "";
                            string gdzrid = "";
                            string beginDate = "";
                            string endDate = "";

                            if (dt.Columns[1].ToString() != "0")// 每个table只有一个定价方式ID
                            {
                                djfsrid = dt.Columns[1].ToString().TrimEnd('\'').ToLower();
                            }
                            if (dt.Columns[3].ToString() != "1")// 每个table都只有一个高低值ID
                            {
                                gdzrid = dt.Columns[3].ToString().TrimEnd('\'').ToLower();
                            }

                            DataTable dtjc = new DataTable();// 取基础
                            if (gdzrid != "")
                            {
                                dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and GDZRID = '" + gdzrid + "'");
                            }
                            else if (djfsrid != "")
                            {
                                dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID = '" + djfsrid + "'");
                            }
                            else
                            {
                                dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID is null");
                            }
                            // 2.报价值表：sqm_modedj_val 表插数 
                            List<string> sqls = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string djfs = "";
                                string gdz = "";
                                string sql_value_insert = "insert into SQM_MODEDJ_VAL(RID,DJSTATUS,IFBJITEM,CREATETIME,CREATEUSER,CREATEID,FEECALCID,CURRENCY,MEMO,CALCUNIT,MIN,CALCNAME,CALCCODE,MAXPRICE,MINPRICE,GUIDEPRICE,COSTPRICE,PURPRICE,CALCTYPE,STARTDATE,ENDDATE,DJFSRID,GDZRID,STATUS";
                                string sql_value_values = " values(";
                                if (dr.Table.Columns.Contains("币种")) { bz = dr["币种"] + ""; }
                                if (dr.Table.Columns.Contains("备注")) { memo = dr["备注"] + ""; }
                                if (dr.Table.Columns.Contains("计费单位")) { calcunit = dr["计费单位"] + ""; }
                                if (dr.Table.Columns.Contains("MIN")) { min = dr["MIN"] + ""; }
                                if (dr.Table.Columns.Contains("最低价"))
                                {
                                    djzdj = dr["最低价"] + "";
                                }
                                if (dr.Table.Columns.Contains("最高价"))
                                {
                                    djzgj = dr["最高价"] + "";
                                }
                                if (dr.Table.Columns.Contains("指导价"))
                                {
                                    djzhidj = dr["指导价"] + "";
                                }
                                if (dr.Table.Columns.Contains("成本价"))
                                {
                                    costprice = dr["成本价"] + "";
                                }
                                if (dr.Table.Columns.Contains("采购价"))
                                {
                                    purprice = dr["采购价"] + "";
                                }
                                if (dr.Table.Columns.Contains("计算方式"))
                                {
                                    if (dr["计算方式"].ToString() == "相对值")
                                    {
                                        calctype = "B";
                                    }
                                    else if (dr["计算方式"].ToString() == "绝对值")
                                    {
                                        calctype = "A";
                                    }
                                }
                                if (dr.Table.Columns.Contains("定价状态"))
                                {
                                    if (dr["定价状态"].ToString() == "未发布")
                                    {
                                        djstataus = "0";
                                    }
                                    else if (dr["定价状态"].ToString() == "已发布")
                                    {
                                        djstataus = "1";
                                    }
                                }
                                if (dr.Table.Columns.Contains("起始日期"))
                                {
                                    beginDate = dr["起始日期"] + "";
                                }
                                if (dr.Table.Columns.Contains("截止日期"))
                                {
                                    endDate = dr["截止日期"] + "";
                                }
                                if (dr.Table.Columns.Contains("定价方式")) { djfs = dr["定价方式"] + ""; }
                                if (dr.Table.Columns.Contains("高低值")) { gdz = dr["高低值"] + ""; }
                                val_rid = System.Guid.NewGuid().ToString();
                                sql_value_values += "'" + val_rid + "','" + djstataus + "','0',to_date('" + createTime + "','yyyy/mm/dd hh24:mi:ss'),'" + createUser + "','" + createId + "','" + djrid + "','" + bz + "','" + memo + "','" + calcunit + "','" + min + "','" + "" + "','" + "" + "','" + djzgj + "','" + djzdj + "','" + djzhidj + "','" + costprice + "','" + purprice + "','" + calctype + "',to_date('" + beginDate + "','yyyy/mm/dd'),to_date('" + endDate + "','yyyy/mm/dd'),'" + djfsrid + "','" + gdzrid + "','1'";
                                if ((dtjc.Rows.Count > 0) && (bjfs != "1") && (bjfs != "2"))// 普通计费基础
                                {
                                    sql_value_insert += ",";
                                    sql_value_values += ",";
                                    for (int i = 0; i < dtjc.Rows.Count; i++)
                                    {
                                        if (i < dtjc.Rows.Count - 1)
                                        {
                                            string colName = dtjc.Rows[i]["VALCOL"] + ""; // 名称的值表位置  定价值表与报价值表位置相同
                                            string colCode = dtjc.Rows[i]["VALCOL"] + "" + "C"; // 代码的值表位置
                                            sql_value_insert += colName + "," + colCode + ",";
                                            string valueCode = "";
                                            if (dr.Table.Columns.Contains(dtjc.Rows[i]["CALCCODE"] + "")) { valueCode = dr[dtjc.Rows[i]["CALCCODE"] + ""] + ""; }
                                            string value = "";
                                            string code = "";
                                            if (valueCode.IndexOf("&&") >= 0)
                                            {
                                                string[] arr = valueCode.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                                                if (arr.Length == 1)// 只有值没有code
                                                {
                                                    value = arr[0];
                                                }
                                                else if (arr.Length == 2)// 值与code都有
                                                {
                                                    value = arr[0];
                                                    code = arr[1];
                                                }
                                            }
                                            else
                                            {
                                                value = valueCode;
                                            }
                                            sql_value_values += "'" + value + "','" + code + "',";
                                        }
                                        else
                                        {
                                            string colName = dtjc.Rows[i]["VALCOL"] + "";
                                            string colCode = dtjc.Rows[i]["VALCOL"] + "" + "C";
                                            sql_value_insert += colName + "," + colCode + ")";
                                            string valueCode = "";
                                            if (dr.Table.Columns.Contains(dtjc.Rows[i]["CALCCODE"] + "")) { valueCode = dr[dtjc.Rows[i]["CALCCODE"] + ""] + ""; }
                                            string value = "";
                                            string code = "";
                                            if (valueCode.IndexOf("&&") >= 0)
                                            {
                                                string[] arr = valueCode.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                                                if (arr.Length == 1)
                                                {
                                                    value = arr[0];
                                                }
                                                else if (arr.Length == 2)
                                                {
                                                    value = arr[0];
                                                    code = arr[1];
                                                }
                                            }
                                            else
                                            {
                                                value = valueCode;
                                            }
                                            sql_value_values += "'" + value + "','" + code + "')";
                                        }
                                    }
                                }
                                else //if ((bjfs != "1") || (bjfs != "2"))
                                {
                                    sql_value_insert += ")";
                                    sql_value_values += ")";
                                }
                                string sql = sql_value_insert + sql_value_values;
                                sqls.Add(sql);
                            }
                            string sqll = string.Join(";", sqls.ToArray());
                            sqll = "begin " + sqll + ";end;";
                            // 插数
                            DataHelper.ExecSql(sqll);
                        }
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private bool imPortData(DataTable dt, DataTable dtjc, ref int hbj)
        {
            bool cfsj = false;
            foreach (DataRow dr in dt.Rows)
            {
                string dtWhere = "";
                foreach (DataRow jcdr in dtjc.Rows)
                {
                    dtWhere += jcdr["CALCCODE"].ToString() + "='" + dr[jcdr["CALCCODE"].ToString()].ToString() + "' and ";
                }
                DataRow[] drArr = dt.Select(dtWhere.Substring(0, dtWhere.Length - 4));
                if (drArr.Length > 1)
                {
                    //冒泡筛选法
                    for (int i = 0; i < drArr.Length; i++)
                    {
                        DateTime istartDate = Convert.ToDateTime(drArr[i]["起始日期"].ToString());
                        DateTime iendDate = Convert.ToDateTime(drArr[i]["截止日期"].ToString());
                        for (int j = i + 1; j < drArr.Length; j++)
                        {
                            DateTime jstartDate = Convert.ToDateTime(drArr[j]["起始日期"].ToString());
                            DateTime jendDate = Convert.ToDateTime(drArr[j]["截止日期"].ToString());
                            if (!((jendDate < istartDate) || jstartDate > iendDate))
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    hbj++;
                    continue;
                }
            }
            return cfsj;
        }
        private ArrayList GetDataFromExcel2(System.IO.Stream stream)
        {
            ArrayList al = new ArrayList();
            Cells cells;
            Workbook workbook = new Workbook(stream);

            List<DataSet> listDs = new List<DataSet>(); // 创建数据集集合，每个数据集表示一个sheet页
            DataSet excel_ds = new DataSet(workbook.Worksheets[0].Name); //创建数据集
            cells = workbook.Worksheets[0].Cells;
            int rownumber = cells.MaxDataRow;
            string rownum = String.Empty;
            string colnum = String.Empty;
            // 从第0行开始读取Excel，将标题读到DataTable中作为列标题
            for (int k = 0; k < cells.MaxDataRow + 1; k++)
            {
                bool titleRow = false;
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    // 记录位置2
                    rownum = (k + 1) + "";
                    colnum = (j + 1) + "";
                    string cellStr = cells[k, j].StringValue.Trim();
                    // 判断是否标题行
                    if (j == 0 && cellStr == "定价方式ID")
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

                        string title = cellStr.Replace("（", "(").Replace("(", "(").Replace("）", ")");
                        // title格式：起运港（海运）(QYG)()
                        title = title.Replace("()", "");
                        title = title.Replace(")", "");
                        // title格式：起运港（海运）(QYG)(=) => 起运港(海运(QYG(=  先去掉(=) 然后去最后一个“(”之后的内容
                        title = title.Replace("(=", "").Replace("(<=", "").Replace("(>=", ""); // 起运港(海运(QYG(=  => 起运港(海运(QYG
                        if (title.IndexOf("(") >= 0)
                        {
                            title = title.Substring(title.LastIndexOf("(") + 1);
                            dataTable.Columns.Add(title);
                        }
                        else if (title.IndexOf("最低报价") >= 0)
                        {
                            dataTable.Columns.Add("最低报价");
                        }
                        else
                        {
                            // 可能会校验其它标题格式
                            dataTable.Columns.Add(title);
                        }
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
                    DataRow drnew = dataTable.NewRow();// 取数据，row[index]  row[columnName]
                    drnew.ItemArray = dataRow.ItemArray;
                    dataTable.Rows.Add(drnew);
                }
                if (k == cells.MaxDataRow)// 如果是最后一行，把最后一个dataTable添加进dataSet中
                {
                    DataTable dtnew = dataTable.Copy();
                    excel_ds.Tables.Add(dtnew);
                }
            }
            listDs.Add(excel_ds);
            al.Add(listDs);
            return al;
        }
        /// <summary>
        /// 主数据校验
        /// </summary>
        /// <param name="value">校验字段值</param>
        /// <param name="type">校验字段类型：国家、港口</param>
        /// <returns></returns>
        public DataTable MainDataExist(string calccode, string value, string type)
        {
            DataTable maindt = null;
            string namePos = "";
            string codePos = "";
            if (type == "1")
            {
                string gjdm = "T005T";
                namePos = DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LANDX'") + "";
                codePos = DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LAND1'") + "";
                if (!String.IsNullOrEmpty(namePos) && !String.IsNullOrEmpty(codePos))
                {
                    string columnName = "COLUMN" + namePos;
                    string columnCode = "COLUMN" + codePos;
                    // 语言 '1'：中文  'E'：英文 现要求英文大写
                    //string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + gjdm + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = 'E'";
                    string sql = string.Format("SELECT distinct {3} as code,{1} as name FROM MDM_MIAN_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {3} = '{2}')", gjdm, columnName, value, columnCode);
                    maindt = DataHelper.QueryDataTable(sql);
                }
            }
            else if (type == "2")
            {
                if (value.IndexOf("(") >= 0 && value.IndexOf(")") >= 0)// 导出数据会出现 三字代码+英文解释+中文解释的形式
                {
                    value = value.Split('(')[0] + "";
                }
                string sql = "select distinct LOCNO as code,DESCR40 as name from MDM_LOC where DESCR40 = '" + value.ToLower() + "' or DESCR40 = '" + value.ToUpper() + "' or DESCR40 = '" + value + "' or LOCNO = '" + value.ToUpper() + "' or LOCNO = '" + value.ToLower() + "' or LOCNO = '" + value + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            else if (type == "3")//币种
            {
                //string bzmdkey = "WAERS";
                //namePos = DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + bzmdkey + "' AND FIELDNAME = 'KTEXT'") + "";
                //codePos = DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + bzmdkey + "' AND FIELDNAME = 'WAERS'") + "";
                //if (!String.IsNullOrEmpty(namePos) && !String.IsNullOrEmpty(codePos))
                //{
                //    string columnName = "COLUMN" + namePos;
                //    string columnCode = "COLUMN" + codePos;
                //    // 语言 '1'：中文  'E'：英文 现要求英文大写
                //    //string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + bzmdkey + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = 'E'";
                //    string sql = string.Format("SELECT distinct {3} FROM MDM_MIAN_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {3} = '{2}')", bzmdkey, columnName, value, columnCode);
                //    maindt = DataHelper.QueryDataTable(sql);
                //}
                string sql = "select distinct WAERS from MDM_WAERS where WAERS = '" + value.ToLower() + "' or WAERS = '" + value.ToUpper() + "' or WAERS = '" + value + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            else if (type == "4")// 船公司 、 航空公司
            {
                string sql = "select distinct BPKEY as code,BPNAME as name from MDM_BP where BPNAME = '" + value.ToLower() + "' or BPNAME = '" + value.ToUpper() + "' or BPNAME = '" + value + "' or BPKEY = '" + value.ToUpper() + "' or BPKEY = '" + value.ToLower() + "' or BPKEY = '" + value + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            //else if (type == "5")// 码头
            //{
            //    code = "mt";
            //}
            else if (type == "6")// 通用计算基础 fieldname 两个值，1是code，2是value
            {
                // 判断是否A类型  A类型在结构表没有数据，所以不用配置主数据 
                string mdtype = DataHelper.QueryValue("select MDTYPE from mdm_calc_basic where mdkey = '" + calccode + "'") + "";
                string position_code = "";
                string position_value = "";
                if (mdtype == "A")
                {
                    position_code = "COLUMN3";
                    position_value = "COLUMN4";
                }
                else if (mdtype == "C" && calccode.ToUpper() != "FLIGHT_CODE")
                {
                    position_code = "COLUMN2";
                    position_value = "COLUMN3";
                }
                else
                {
                    if (calccode == "SOURCELOC_ZONE")
                    {
                        calccode = "DESTLOC_ZONE";
                    }
                    string sql_ext = "select * from SQM_CALC_BASE_EXT where calccode like '%" + calccode + "%'";
                    DataTable dt = DataHelper.QueryDataTable(sql_ext);
                    if (dt.Rows.Count > 0)
                    {
                        string[] fieldName = (dt.Rows[0]["MDMFIELDNAME"] + "").Split(',');
                        string filedcode = fieldName[0];
                        string fieldvalue = fieldName[1];
                        position_code = DataHelper.QueryValue("select 'COLUMN' || position from mdm_calc_strc where mdkey = '" + calccode + "' and fieldname = '" + filedcode + "'") + "";
                        position_value = DataHelper.QueryValue("select 'COLUMN' || position from mdm_calc_strc where mdkey = '" + calccode + "' and fieldname = '" + fieldvalue + "'") + "";
                    }
                }
                if (position_code != "" && position_value != "")
                {
                    if (calccode == "ZTGFS" || calccode == "ZDZCBJ" || calccode == "COMMODITY_CODE")//个别主数据  数据为03,导入值为3,则检验成功
                    {
                        string sql = "";
                        if (IsDecimal(value))
                        {
                            sql = "select " + position_code + " as code," + position_value + " as name from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or to_char(to_number(" + position_code + ")) = '" + (Convert.ToDecimal(value) + "").ToLower() + "' or to_char(to_number(" + position_code + ")) = '" + (Convert.ToDecimal(value) + "").ToUpper() + "' or to_char(to_number(" + position_code + ")) = '" + Convert.ToDecimal(value) + "" + "' or " + position_value + " = '" + value + "')";
                        }
                        else
                        {
                            sql = "select " + position_code + " as code," + position_value + " as name from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value.ToLower() + "' or " + position_code + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value + "" + "' or " + position_value + " = '" + value + "')";
                        }
                        maindt = DataHelper.QueryDataTable(sql);
                    }
                    else
                    {
                        maindt = DataHelper.QueryDataTable("select " + position_code + " as code," + position_value + " as name from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value.ToLower() + "' or " + position_code + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value + "' or " + position_value + " = '" + value + "')");
                    }
                }
            }
            else if (type == "product")
            {
                string sql = "select count(1) from mdm_product where productkey = '" + value + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            else if (type == "service")
            {
                string sql = "select count(1) from mdm_prd_srv_ref where productcode = '" + value.Split(',')[0] + "' and servicetypecode = '" + value.Split(',')[1] + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            else if (type == "fee")
            {
                string sql = "select count(1) from mdm_srv_fee_ref where srvrqcd121 = '" + value.Split(',')[0] + "' and tcet084 = '" + value.Split(',')[1] + "'";
                maindt = DataHelper.QueryDataTable(sql);
            }
            return maindt;
        }
        [AllowAnonymous]
        public DataTable getExpdt(SQM_MODEDJ_VAL smv, string djrid, string[] SearchKeys, string djfsrid)
        {
            try
            {
                string[] primaryKeys = getPrimaryKeys(djrid, djfsrid, smv.CALCUNIT);
                string sql_field = String.Join(",", primaryKeys);
                sql_field += ",RID,STARTDATE,ENDDATE";
                string sql_query = "";
                if (!String.IsNullOrEmpty(djfsrid))
                {
                    sql_query = @"select " + sql_field + " from SQM_MODEDJ_VAL where FEECALCID='" + djrid + "' and STATUS='1' and DJFSRID='" + djfsrid + "' and ";
                }
                else
                {
                    sql_query = @"select " + sql_field + " from SQM_MODEDJ_VAL where FEECALCID='" + djrid + "' and STATUS='1' and DJFSRID is null and ";
                }
                for (int i = 0; i < SearchKeys.Length; i++)
                {
                    if (SearchKeys[i] == "STARTDATE")
                    {
                        if (!(smv.GetValue(SearchKeys[i]) == null))
                        {
                            sql_query += SearchKeys[i] + " >= to_date('" + smv.GetValue(SearchKeys[i]) + "', 'YYYY/MM/DD HH24:MI:SS') and ";
                        }
                    }
                    else if (SearchKeys[i] == "ENDDATE")
                    {
                        if (!(smv.GetValue(SearchKeys[i]) == null))
                        {
                            sql_query += SearchKeys[i] + " <= to_date('" + smv.GetValue(SearchKeys[i]) + "', 'YYYY/MM/DD HH24:MI:SS') and ";
                        }
                    }
                    else if (!(smv.GetValue(SearchKeys[i]) == null) && !String.IsNullOrEmpty(smv.GetValue(SearchKeys[i]).ToString()))
                    {
                        sql_query += SearchKeys[i] + " = '" + smv.GetValue(SearchKeys[i]) + "' and ";
                    }
                }
                sql_query += "1=1";
                DataTable dt = DataHelper.QueryDataTable(sql_query);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public ActionResult ChangeQZ(string postdata, string djrid, string qsdate, string jzdate, string djfsrid)
        {
            bool rtnflag = true;
            string rtnmsg = "替换成功";
            try
            {
                SQM_MODEDJ_VAL smv = null;
                smv = JsonHelper.GetObject<SQM_MODEDJ_VAL>(postdata);
                string[] SearchKeys = getSearchKeys(djrid, djfsrid, smv.CALCUNIT);
                string[] primaryKeys = getPrimaryKeys(djrid, djfsrid, smv.CALCUNIT);
                DataTable dt = getExpdt(smv, djrid, SearchKeys, djfsrid);
                DateTime startDate = Convert.ToDateTime(qsdate);
                DateTime endDate = Convert.ToDateTime(jzdate);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SQM_MODEDJ_VAL oldsmv = SQM_MODEDJ_VAL.Find(dr["RID"]);
                        if (oldsmv.DJSTATUS == "0")
                        {
                            oldsmv.STARTDATE = startDate;
                            oldsmv.ENDDATE = endDate;
                            oldsmv.DoUpdate();
                        }
                        else
                        {
                            // 获取原始数据
                            DataTable ysdt = FindSourceData(oldsmv, primaryKeys);
                            if (ysdt.Rows.Count > 0)
                            {
                                // 获取原始数据最小起始日期
                                DateTime startDate_old = (DateTime)ysdt.Rows[0]["STARTDATE"];
                                // 获取原始数据最大截止日期
                                DateTime endDate_old = (DateTime)ysdt.Rows[0]["ENDDATE"];
                                // 处理有效期
                                //HandleValidDate(startDate, endDate, startDate_old, endDate_old, oldsmv, oldsmv, ysdt);
                            }
                            else
                            {
                                oldsmv.STARTDATE = startDate;
                                oldsmv.ENDDATE = endDate;
                                oldsmv.DoSave();
                            }
                        }
                    }
                }
                else
                {
                    return Content(new JsonMessage { Success = false, Message = "没有可替换的有效数据！" }.ToString());
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        //[AllowAnonymous]
        private static string costRid = "";// 定价使用成本的rid
        public ActionResult GetPrice(string postdata, string type)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = (Dictionary<string, string>)JsonConvert.DeserializeObject(postdata, dic.GetType());
            string costPrice = string.Empty;
            if (type == "海运费")
            {
                SQM_COST_HY hy = new SQM_COST_HY();
                hy = GetHY(hy, dic);
                string[] primaryKeys = { "AREA", "QYG", "MDG", "BZ", "CGS", "HC", "ZZG", "KHR", "STARTDATE", "ENDDATE" };
                List<string> resultSet = new List<string>();
                // 获取箱型
                if (dic.ContainsKey("ZXX"))
                {
                    resultSet.Add(dic["ZXX"]);
                }
                else if (dic.ContainsKey("EQUIP_TYPE"))
                {
                    resultSet.Add(dic["EQUIP_TYPE"]);
                }
                DataTable dt = SourceData(hy, primaryKeys, resultSet.ToArray());
                if (dt.Rows.Count == 1)
                {
                    costPrice = dt.Rows[0][1].ToString();
                    costRid = dt.Rows[0][0].ToString();
                }
            }
            else if (type == "国内空运费")
            {
                SQM_COST_KYGN kygn = new SQM_COST_KYGN();
                kygn = GetKYGN(kygn, dic);
                string[] primaryKeys = { "AREA", "QYG", "HX", "BZ", "HWLB", "MINPRICE", "HKGS", "HBH", "STARTDATE", "ENDDATE" };
                List<string> resultSet = new List<string>();
                // 获取重量
                resultSet.Add(dic["YZL"]);
                DataTable dt = SourceData(kygn, primaryKeys, resultSet.ToArray());
                if (dt.Rows.Count == 1)
                {
                    costPrice = dt.Rows[0][1].ToString();
                    costRid = dt.Rows[0][0].ToString();
                }
            }
            else if (type == "国际空运费")
            {
                SQM_COST_KYGJ kygj = new SQM_COST_KYGJ();
                kygj = GetKYGJ(kygj, dic);
                string[] primaryKeys = { "AREA", "QYG", "MDG", "BZ", "HWLB", "MIN", "HKGS", "ZZG", "HBH", "STARTDATE", "ENDDATE" };
                List<string> resultSet = new List<string>();
                // 获取重量
                resultSet.Add(dic["YZL"]);
                DataTable dt = SourceData(kygj, primaryKeys, resultSet.ToArray());
                if (dt.Rows.Count == 1)
                {
                    costPrice = dt.Rows[0][1].ToString();
                    costRid = dt.Rows[0][0].ToString();
                }
            }
            return Content(new JsonMessage { Message = costPrice, Success = true, Data = costRid }.ToString());
        }
        /// <summary>
        /// 日期包含于 查询
        /// </summary>
        /// <param name="srcobj">实体类实例对象</param>
        /// <param name="fields">查询条件</param>
        /// <param name="resultSet">查询结果集</param>
        /// <returns></returns>
        [AllowAnonymous]
        public DataTable SourceData(Object srcobj, string[] fields, string[] resultSet)
        {
            string objectName = srcobj.GetType().Name; // 获取类名
            string results = string.Join(",", resultSet); // 获取需要查询的字段
            if (results == "0-45KG")
            {
                results = "WEIGHTXY45";
            }
            else if (results == "+45KG")
            {
                results = "WEIGHTDY45";
            }
            else if (results == "+100KG")
            {
                results = "WEIGHTDY100";
            }
            else if (results == "+500KG")
            {
                results = "WEIGHTDY500";
            }
            else if (results == "+1000KG")
            {
                results = "WEIGHTDY1000";
            }
            else if (results == "20GP")
            {
                results = "GP20";
            }
            else if (results == "40GP")
            {
                results = "GP40";
            }
            else if (results == "40HQ")
            {
                results = "HQ40";
            }
            Type type = srcobj.GetType();
            PropertyInfo property;
            string sql = "select rid,";
            if (objectName == "SQM_COST_KYGN")
            {
                string hwlb = ((OnControl.Model.SQM_COST_KYGN)(srcobj)).HWLB;
                if ((hwlb == "D" || hwlb == "E" || hwlb == "F" || hwlb == "G" || hwlb == "H") && (results == "WEIGHTXY45" || results == "WEIGHTDY45"))
                {
                    sql += "case when " + results + " is null then DEFGPRICE when " + results + " is not null then " + results + " end from " + objectName + " where ";
                }
                else
                {
                    sql += results + " from " + objectName + " where ";
                }
            }
            else
            {
                sql += results + " from " + objectName + " where ";
            }
            for (int i = 0; i < fields.Length; i++)
            {
                property = type.GetProperty(fields[i]);
                if (i < fields.Length - 1)
                {
                    if (fields[i] == "STARTDATE")
                    {
                        if (property.GetValue(srcobj, null) == null)
                        {
                            sql += fields[i] + " is null and ";
                        }
                        else
                        {
                            sql += fields[i] + " <= to_date('" + property.GetValue(srcobj, null).ToString() + "','yyyy/mm/dd hh24:mi:ss') and ";
                        }
                    }
                    else if (property.GetValue(srcobj, null) == null) // 数字类型
                    {
                        sql += fields[i] + " is null and ";
                    }
                    else if (String.IsNullOrEmpty(property.GetValue(srcobj, null).ToString())) // 字符串类型
                    {
                        sql += fields[i] + " is null and ";
                    }
                    else if (!String.IsNullOrEmpty(property.GetValue(srcobj, null).ToString()))
                    {
                        sql += fields[i] + " = '" + property.GetValue(srcobj, null).ToString() + "' and ";
                    }
                }
                else
                {
                    if (property.GetValue(srcobj, null) == null)
                    {
                        sql += fields[i] + " is null and STATUS = '1' order by STARTDATE";
                    }
                    else
                    {
                        sql += fields[i] + " >= to_date('" + property.GetValue(srcobj, null).ToString() + "','yyyy/mm/dd hh24:mi:ss') and STATUS = '1' order by STARTDATE";
                    }
                }
            }
            DataTable dt = DataHelper.QueryDataTable(sql);
            return dt;
        }
        [AllowAnonymous]
        public SQM_COST_HY GetHY(SQM_COST_HY hy, Dictionary<string, string> dic)
        {
            try
            {
                foreach (var item in dic)
                {
                    if (item.Key == getAppSetting("HY_AREA"))
                    {
                        hy.AREA = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_MDG"))
                    {
                        hy.MDG = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_QYG"))
                    {
                        hy.QYG = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_CGS"))
                    {
                        hy.CGS = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_HC"))
                    {
                        hy.HC = Convert.ToDecimal(item.Value);
                    }
                    else if (item.Key == getAppSetting("HY_ZZG"))
                    {
                        hy.ZZG = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_MT"))
                    {
                        hy.MT = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_KHR"))
                    {
                        hy.KHR = item.Value;
                    }
                    else if (item.Key == getAppSetting("HY_BZ"))
                    {
                        hy.BZ = item.Value;
                    }
                    else if (item.Key == "STARTDATE")
                    {
                        hy.STARTDATE = Convert.ToDateTime(item.Value);
                    }
                    else if (item.Key == "ENDDATE")
                    {
                        hy.ENDDATE = Convert.ToDateTime(item.Value);
                    }
                }
            }
            catch (Exception)
            {

            }
            return hy;
        }
        [AllowAnonymous]
        public SQM_COST_KYGN GetKYGN(SQM_COST_KYGN kygn, Dictionary<string, string> dic)
        {
            try
            {
                foreach (var item in dic)
                {
                    if (item.Key == getAppSetting("KN_AREA"))
                    {
                        kygn.AREA = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_QYG"))
                    {
                        kygn.QYG = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_HX"))
                    {
                        kygn.HX = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_BZ"))
                    {
                        kygn.BZ = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_HWLB"))
                    {
                        kygn.HWLB = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_MIN") && !String.IsNullOrEmpty(item.Value))
                    {
                        kygn.MINPRICE = Convert.ToDecimal(item.Value);
                    }
                    else if (item.Key == getAppSetting("KN_HKGS"))
                    {
                        kygn.HKGS = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_HBH"))
                    {
                        kygn.HBH = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_SKB"))
                    {
                        kygn.SKB = item.Value;
                    }
                    else if (item.Key == "STARTDATE")
                    {
                        kygn.STARTDATE = Convert.ToDateTime(item.Value);
                    }
                    else if (item.Key == "ENDDATE")
                    {
                        kygn.ENDDATE = Convert.ToDateTime(item.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return kygn;
        }
        [AllowAnonymous]
        public SQM_COST_KYGJ GetKYGJ(SQM_COST_KYGJ kygj, Dictionary<string, string> dic)
        {
            try
            {
                foreach (var item in dic)
                {
                    if (item.Key == getAppSetting("KJ_AREA"))
                    {
                        kygj.AREA = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_QYG"))
                    {
                        kygj.QYG = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_MDG"))
                    {
                        kygj.MDG = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_BZ"))
                    {
                        kygj.BZ = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_HWLB"))
                    {
                        kygj.HWLB = item.Value;
                    }
                    else if (item.Key == getAppSetting("KN_MIN") && !String.IsNullOrEmpty(item.Value))
                    {
                        kygj.MIN = Convert.ToDecimal(item.Value);
                    }
                    else if (item.Key == getAppSetting("KJ_HKGS"))
                    {
                        kygj.HKGS = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_ZZG"))
                    {
                        kygj.ZZG = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_HBH"))
                    {
                        kygj.HBH = item.Value;
                    }
                    else if (item.Key == getAppSetting("KJ_SKB"))
                    {
                        kygj.SKB = item.Value;
                    }
                    else if (item.Key == "STARTDATE")
                    {
                        kygj.STARTDATE = Convert.ToDateTime(item.Value);
                    }
                    else if (item.Key == "ENDDATE")
                    {
                        kygj.ENDDATE = Convert.ToDateTime(item.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return kygj;
        }
        private string getAppSetting(string str)
        {
            return ConfigHelper.AppSettings(str);
        }
        public ActionResult PurCopyIndex()
        {
            string sql = "";
            string feecode = Request["feecode"].ToString();
            string djrid = Request["djrid"].ToString();
            string prdcode = Request["prdcode"].ToString();
            sql = @"select PRODUCTKEY,SQPRODUCTNAME from SQM_PRD_EXT where SQPRODUCTNAME is not null";
            DataTable Prodt = DataHelper.QueryDataTable(sql);
            sql = @"select SERVICETYPE,SERVICENAME from MDM_SERVICE";
            DataTable Serdt = DataHelper.QueryDataTable(sql);
            ViewBag.ProData = Prodt;
            ViewBag.SerData = Serdt;
            ViewBag.feecode = feecode;
            ViewBag.djrid = djrid;
            ViewBag.prdcode = prdcode;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult PurCopyLists()
        {
            //查询条件拼接
            string wherestr = "";
            string productkey = Request["PRODUCTKEY"].ToString();
            string feecode = Request["FEECODE"].ToString();
            string prdcode = Request["PRDCODE"].ToString();
            string servicetype = Request["SERVICETYPE"].ToString();
            if (servicetype != "")
            {
                wherestr += " and s.servicetype = '" + servicetype + "'";
            }
            if (productkey != "")
            {
                wherestr += " and p.PRODUCTKEY = '" + productkey + "'";
            }
            if (feecode != "")
            {
                wherestr += " and f.tcet084 = '" + feecode + "'";
            }
            if (prdcode != "")
            {
                wherestr += " and p.PRODUCTKEY <> '" + prdcode + "'";
            }
            string sql_from = @" from SQM_PRD_EXT p
                left join MDM_PRD_SRV_REF ps on p.PRODUCTKEY=ps.PRODUCTCODE
                left join MDM_SERVICE s on ps.SERVICETYPECODE=s.SERVICETYPE
                left join MDM_SRV_FEE_REF sf on ps.SERVICETYPECODE=sf.SRVRQCD121
                left join V_MDM_FEE f on sf.TCET084=f.TCET084
                WHERE p.STATUS='1' and p.BUSINESSORG is not null ";
            string sql_feild = @"SELECT distinct p.BUSINESSORG,p.PRODUCTKEY,p.SQPRODUCTNAME,s.SERVICETYPE,s.SERVICENAME,f.TCET084,f.TEXTDESC,p.SORD ";
            string sql_order = @" ORDER BY p.SORD asc ";
            //设置分页
            string sql = "select A.*,ROWNUM As RN from ({0}{1}{2}{3}) A ";
            sql = string.Format(sql, sql_feild, sql_from, wherestr, sql_order);
            string sql_page = string.Format("With DATASET AS({0}) select * from DATASET  WHERE RN between {1} and {2}", sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //数据数量
            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        //定价复制
        public ActionResult IfPurCopy()
        {
            bool rtnflag = true;
            string rtnmsg = "";
            string code = "1";
            try
            {
                string wherestr = "";
                string coppsfids = "";//复制的DJ_PSF  RID
                string dataval = Request["dataval"];
                string ifcop = Request["ifcop"];
                Dictionary<string, string> ifcops = JsonHelper.GetObject<Dictionary<string, string>>(ifcop);
                DataTable djcopdt = new DataTable();
                foreach (var copitem in ifcops)
                {
                    if (copitem.Value == "1")
                    {
                        wherestr += "'" + copitem.Key + "',";
                    }
                }
                djcopdt = JsonHelper.GetObject<DataTable>(dataval);
                //取勾选的产品、服务项
                DataRow[] rows = djcopdt.Select("PRODUCTKEY in(" + wherestr.TrimEnd(',') + ")");
                djcopdt = ToDataTable(rows);
                foreach (DataRow dr in djcopdt.Rows)
                {
                    string rid = System.Guid.NewGuid().ToString();
                    //SQM_DJ_PSF sdpnew = SQM_DJ_PSF.FindFirstByProperties(SQM_DJ_PSF.Prop_PRDCODE, dr["PRODUCTKEY"], SQM_DJ_PSF.Prop_SRVCODE, dr["SERVICETYPE"], SQM_DJ_PSF.Prop_FEECODE, dr["TCET084"]);
                    string psfrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where DJFS is not null and PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}'", dr["PRODUCTKEY"].ToString(), dr["SERVICETYPE"].ToString(), dr["TCET084"].ToString())) + "";
                    if (!String.IsNullOrEmpty(psfrid))
                    {
                        coppsfids += "'" + psfrid + "',";
                    }
                    else
                    {
                        //新建SQM_DJ_PSF数据
                        SQM_DJ_PSF sdp = new SQM_DJ_PSF();
                        sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sdp.RID = rid;
                        sdp.BUSINESSORG = dr["BUSINESSORG"].ToString();
                        sdp.PRDNAME = dr["SQPRODUCTNAME"].ToString();
                        sdp.PRDCODE = dr["PRODUCTKEY"].ToString();
                        sdp.SRVNAME = dr["SERVICENAME"].ToString();
                        sdp.SRVCODE = dr["SERVICETYPE"].ToString();
                        sdp.FEENAME = dr["TEXTDESC"].ToString();
                        sdp.FEECODE = dr["TCET084"].ToString();
                        sdp.DJFS = "0";
                        sdp.CREATESOURCE = "定价复制";
                        sdp.DoCreate();
                        coppsfids += "'" + rid + "',";
                    }
                }
                //string sql = @"select RID from SQM_MODEDJ_VAL where FEECALCID in(" + coppsfids.TrimEnd(',') + ")";
                //DataTable czdt = DataHelper.QueryDataTable(sql);
                //if (czdt.Rows.Count > 0)
                //{
                //    rtnflag = true;
                //    code = "2";
                //}
                rtnmsg = coppsfids.TrimEnd(',');
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Code = code, Message = rtnmsg }.ToString());
        }
        //定价复制
        public ActionResult PurCopy()
        {
            bool rtnflag = true;
            string rtnmsg = "复制成功";
            try
            {
                string djrid = Request["DJRID"].ToString();
                string coppsfrids = Request["COPPSFRIDS"].ToString();
                SQM_MODEDJ_VAL[] smvobjs = SQM_MODEDJ_VAL.FindAll(Expression.Eq(SQM_MODEDJ_VAL.Prop_FEECALCID, djrid), Expression.Eq(SQM_MODEDJ_VAL.Prop_STATUS, "1"));
                //其他费目置为失效
                DataHelper.ExecSql("update SQM_MODEDJ_VAL set STATUS='0',DJSTATUS='0' where FEECALCID in(" + coppsfrids + ")");
                string[] psfridarr = coppsfrids.Split(',');
                foreach (string psfrid in psfridarr)
                {
                    // 值表
                    foreach (SQM_MODEDJ_VAL obj in smvobjs)
                    {
                        obj.FEECALCID = psfrid.TrimStart('\'').TrimEnd('\'');
                        obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        obj.DJSTATUS = "0";
                        obj.DoCreate();
                    }
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        private DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone(); // 复制DataRow的表结构
            foreach (DataRow row in rows)
            {
                tmp.ImportRow(row); // 将DataRow添加到DataTable中
            }
            return tmp;
        }
        /// <summary>
        /// 通过feecode,djfsrid,gdzkey获取高低值数据
        /// 通过feecode,djfsrid获取已经维护的GDZKEY
        /// </summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetGdzData(string feecode, string djfsrid, string gdzkey)
        {
            string sql = string.Format("SELECT  DISTINCT GDZRID, GDZNAME FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' and GDZKEY='{2}' order by GDZNAME asc", feecode, djfsrid, gdzkey);
            var gdzArray = DataHelper.QueryObjectsList(sql);

            object[] data = { gdzArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetGdzKey(string feecode, string djfsrid)
        {
            string sql = string.Format("SELECT  DISTINCT GDZKEY FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' order by GDZKEY asc", feecode, djfsrid);
            var gdzkeyArray = DataHelper.QueryObjectsList(sql);

            object[] data = { gdzkeyArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 主数据长度校验  数字与非数字
        /// </summary>
        /// <param name="calccode"></param>
        /// <param name="dataval"></param>
        /// <returns></returns>
        private static DataTable dtdatalen = DataHelper.QueryDataTable("select CALC_BASE,CALCTYPE,DATALEN,POINTLEN from SQM_CALC_BASE");// 校验数据长度
        public string CheckData(string calccode, string dataval)
        {
            string checkVal = "0";
            int datalen; // 总长度
            int pointlen; // 小数位
            int numlen; // 整数位
            DataRow[] drs = dtdatalen.Select("CALC_BASE = '" + calccode + "'");
            if (drs.Length > 0)
            {
                // 标记校验  CALCTYPE：X  值X/x或空  Y  值Y/y或N/n
                string bjStr = drs[0]["CALCTYPE"] + "";
                if (bjStr == "X")
                {
                    if (dataval == "X" || dataval == "x")
                    {
                        checkVal = "X";
                    }
                    else if (dataval + "" != "")
                    {
                        checkVal = "5";// 标记X校验错误
                    }
                }
                else if (bjStr == "Y")
                {
                    if (dataval == "Y" || dataval == "y")
                    {
                        checkVal = "Y";
                    }
                    else if (dataval == "N" || dataval == "n")
                    {
                        checkVal = "N";
                    }
                    else
                    {
                        checkVal = "6";// 标记Y校验错误
                    }
                }
                else if (dataval != "*" && !String.IsNullOrEmpty(drs[0]["DATALEN"].ToString()))
                {
                    // 数据库基础长度字典
                    datalen = Convert.ToInt32(drs[0]["DATALEN"] + "" == "" ? "0" : drs[0]["DATALEN"].ToString()); // 总长度
                    pointlen = Convert.ToInt32(drs[0]["POINTLEN"] + "" == "" ? "0" : drs[0]["POINTLEN"].ToString()); //  小数位
                    if (IsDecimal(dataval))// 数字校验
                    {
                        numlen = datalen - pointlen;// 整数位
                        string datavalnum = ""; // 整数位数字
                        string datavalpoint = ""; // 小数位数字
                        if (dataval.IndexOf(".") >= 0)// 小数
                        {
                            datavalnum = dataval.Split('.')[0];
                            datavalpoint = dataval.Split('.')[1];
                        }
                        else
                        {
                            datavalnum = dataval;
                        }
                        if (datavalnum.Length > numlen)
                        {
                            checkVal = "1" + numlen;// 整数位长度超限
                        }
                        else if (datavalpoint != "" && datavalpoint.Length > pointlen)
                        {
                            checkVal = "2" + pointlen;// 小数位长度超限
                        }
                    }
                    else // 非数字校验
                    {
                        if (pointlen == 0 && dataval.Length > datalen)
                        {
                            checkVal = "3" + datalen;//数据长度超限
                        }
                        else if (pointlen != 0)
                        {
                            checkVal = "4";//请维护数字型数据
                        }
                    }
                }
            }
            return checkVal;
        }
        /// <summary>
        /// 判断是否是浮点数（是否数字类型）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool IsDecimal(string dataval)
        {
            try
            {
                Regex rexint = new Regex(@"^\d+$");
                Regex rexpoint = new Regex(@"^\d+\.\d+$");
                if (rexint.IsMatch(dataval) || rexpoint.IsMatch(dataval))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 不同组织的定价
        /// </summary>
        /// <param name="djrid"></param>
        /// <returns></returns>
        public ActionResult difOrgPur(string djrid, string alonefee = "")
        {
            bool rtnflag = true;
            string rtnmsg = "添加成功";
            try
            {
                string czrid;
                SQM_DJ_PSF oldsdp = SQM_DJ_PSF.Find(djrid);
                if (alonefee == "1")
                {
                    czrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where FEECODE='{0}' and ALONEFEE='1' and ORGRID is null", oldsdp.FEECODE)) + "";
                }
                else
                {
                    //czrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and BUSINESSORG='{3}' and ORGRID is null", oldsdp.PRDCODE, oldsdp.SRVCODE, oldsdp.FEECODE, oldsdp.BUSINESSORG)) + "";
                    czrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and ORGRID is null", oldsdp.PRDCODE, oldsdp.SRVCODE, oldsdp.FEECODE)) + "";
                }
                if (!String.IsNullOrEmpty(czrid))
                {
                    return Content(new JsonMessage { Success = false, Message = "已添加该费目下不同组织定价，请前往维护组织！" }.ToString());
                }
                string newdjrid = System.Guid.NewGuid().ToString();
                SQM_DJ_PSF newsdp = new SQM_DJ_PSF();
                SQM_MODEDJ_VAL newsmv = new SQM_MODEDJ_VAL();
                newsdp.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                newsdp.RID = newdjrid;
                newsdp.PRDNAME = oldsdp.PRDNAME;
                newsdp.PRDCODE = oldsdp.PRDCODE;
                newsdp.SRVNAME = oldsdp.SRVNAME;
                newsdp.SRVCODE = oldsdp.SRVCODE;
                newsdp.FEENAME = oldsdp.FEENAME;
                newsdp.FEECODE = oldsdp.FEECODE;
                newsdp.DJFS = oldsdp.DJFS;
                if (alonefee == "1")
                {
                    newsdp.ALONEFEE = "1";
                }
                newsdp.BUSINESSORG = oldsdp.BUSINESSORG;
                newsdp.MODIFYTIME = DateTime.Now;
                newsdp.CREATESOURCE = "不同组织定价";
                newsdp.DoCreate();
                SQM_MODEDJ_VAL[] smvobj = SQM_MODEDJ_VAL.FindAllByProperties(SQM_MODEDJ_VAL.Prop_FEECALCID, djrid, SQM_MODEDJ_VAL.Prop_STATUS, "1");
                foreach (SQM_MODEDJ_VAL oldsmv in smvobj)
                {
                    newsmv = oldsmv;
                    newsmv.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    newsmv.FEECALCID = newdjrid;
                    newsmv.MODIFYUSER = "";
                    newsmv.MODIFYTIME = null;
                    newsmv.DJSTATUS = "0";
                    newsmv.DoCreate();
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
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
                                    if (!string.IsNullOrEmpty(row[p.Name].ToString()))
                                    {
                                        p.SetValue(entity, Convert.ToDecimal(row[p.Name]), null);
                                    }
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
        //public string GetName(string workno)
        //{
        //    string name = "";
        //    DataRow[] nameDr = NameDt.Select("WORKNO='"+workno+"'");
        //    name = nameDr[0]["NAME"].ToString();
        //    return JsonHelper.GetJsonString(name);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sblx"></param>
        /// <returns></returns>
        //public string GetSBLXMS(string sblx)
        //{
        //    string sblxms = DataHelper.QueryValue("select column5 from mdm_calc_value where mdkey = 'EQUIP_TYPE' and column4 = '" + sblx + "'") + "";
        //    return sblxms;
        //}
        //产品定价复制
        public ActionResult ProdPurCopy()
        {
            bool rtnflag = true;
            string rtnmsg = "复制成功";
            try
            {
                string businessorg = Request["businessorg"];//事业部
                string prdcode = Request["prdcode"];//复制的源产品
                string copyprdcode = Request["copyprdcode"];//要复制的产品code
                string copyprdname = "";
                //判断事业部是否一样
                SQM_PRD_EXT spe = SQM_PRD_EXT.FindFirstByProperties("BUSINESSORG", businessorg, "STATUS", "1", "PRODUCTKEY", copyprdcode);
                if (spe != null)
                {
                    copyprdname = spe.SQPRODUCTNAME;//要复制的产品name
                }
                else
                {
                    return Content(new JsonMessage { Success = false, Message = "两个产品事业部不一样，请确认！" }.ToString());
                }
                SQM_DJ_PSF[] sdps = SQM_DJ_PSF.FindAllByProperties("PRDCODE", prdcode, "BUSINESSORG", businessorg);
                DataHelper.ExecSql(string.Format(@"delete from SQM_DJ_PSF where PRDCODE='{0}'", copyprdcode));
                foreach (SQM_DJ_PSF sdp in sdps)
                {
                    //新建SQM_DJ_PSF数据
                    string rid = System.Guid.NewGuid().ToString();
                    SQM_DJ_PSF sdpcopy = new SQM_DJ_PSF();
                    sdpcopy.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdpcopy.RID = rid;
                    sdpcopy.BUSINESSORG = sdp.BUSINESSORG;
                    sdpcopy.PRDNAME = copyprdname;
                    sdpcopy.PRDCODE = copyprdcode;
                    sdpcopy.SRVNAME = sdp.SRVNAME;
                    sdpcopy.SRVCODE = sdp.SRVCODE;
                    sdpcopy.FEENAME = sdp.FEENAME;
                    sdpcopy.FEECODE = sdp.FEECODE;
                    sdpcopy.DJFS = sdp.DJFS;
                    sdpcopy.ORGNAME = sdp.ORGNAME;
                    sdpcopy.ORGCODE = sdp.ORGCODE;
                    sdpcopy.IFDPDX = sdp.IFDPDX;
                    sdpcopy.IFCOST = sdp.IFCOST;
                    sdpcopy.ORGRID = sdp.ORGRID;
                    sdpcopy.CREATESOURCE = "产品定价复制";
                    sdpcopy.DoCreate();
                    //同步定价值表数据
                    SQM_MODEDJ_VAL[] smvobjs = SQM_MODEDJ_VAL.FindAll(Expression.Eq(SQM_MODEDJ_VAL.Prop_FEECALCID, sdp.RID), Expression.Eq(SQM_MODEDJ_VAL.Prop_STATUS, "1"));
                    foreach (SQM_MODEDJ_VAL obj in smvobjs)
                    {
                        obj.FEECALCID = rid;
                        obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        obj.DoCreate();
                    }
                }
                //同步SQM_SRV_FEE_CONFIG关系表数据
                //SQM_SRV_FEE_CONFIG[] ssfcs = SQM_SRV_FEE_CONFIG.FindAllByProperties("PRODCODE", prdcode);
                //SQM_SRV_FEE_CONFIG ssfc = SQM_SRV_FEE_CONFIG.FindFirstByProperties("PRODCODE", copyprdcode);
                //if (ssfc == null)
                //{
                //    foreach (SQM_SRV_FEE_CONFIG obj in ssfcs)
                //    {
                //        obj.MEMO = "产品定价复制";
                //        obj.PRODCODE = copyprdcode;
                //        obj.PRODNAME = copyprdname;
                //        obj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                //        obj.DoCreate();
                //    }
                //}
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
    }

    public class Select2ListItem
    {
        public bool Selected { get; set; }
        public string text { get; set; }
        public string id { get; set; }
    }
}

