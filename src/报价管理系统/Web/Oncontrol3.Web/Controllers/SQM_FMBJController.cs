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
using NPOI.SS.Formula.Functions;
using NHibernate.Mapping;
using System.Text.RegularExpressions;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class SQM_FMBJController : BaseController
    {
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
        /// <summary>
        /// showPSFCV
        /// </summary>
        /// <param name="keyvalue">报价主表RID</param>
        /// <param name="zver">版本号</param>
        /// <returns></returns>
        public DataTable showPSFCV(string keyvalue, string zver)
        {
            //keyvalue = "67c80c60-5a2d-4b";
            keyvalue = "c1f1aad2-1e0d-4a97-b5d8-3ae3a9af03ac";//飞力环境
            zver = "V1";
            string bjrid = "";
            string prdcode = "";
            string srvcode = "";
            string feecode = "";
            string gdzrid = "";
            string djfsrid = "";
            string sql = "";
            string filedkeys = "";
            bool min = false;
            DataTable zbsjdt = null;
            try
            {
                //首先查询出版本的rid
                var vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver));
                //psf表信息
                sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1'", vrid);
                DataTable dt = DataHelper.QueryDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    bjrid = dr["RID"].ToString();
                    prdcode = dr["PRODUCT_CODE"].ToString();
                    srvcode = dr["SERVICE_CODE"].ToString();
                    feecode = dr["FEE_CODE"].ToString();

                    //获取报价值表的数据
                    sql = @"select * from SQM_MODEBJ_VAL t where FEECALCID='" + bjrid + "'";
                    DataTable zbdt = DataHelper.QueryDataTable(sql);
                    foreach (DataRow zbdr in zbdt.Rows)
                    {
                        djfsrid = zbdr["DJFSRID"].ToString();
                        gdzrid = zbdr["GDZRID"].ToString();
                        //是否有MIN
                        string minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                        if (minprice == "1")
                        {
                            min = true;
                        }
                        string where = "";
                        string wheredt = "";
                        if (!String.IsNullOrEmpty(djfsrid))
                        {
                            where += " and r.DJFSRID='" + djfsrid + "' ";
                            wheredt += " and DJFSRID='" + djfsrid + "' ";
                        }
                        else
                        {
                            where += " and r.DJFSRID is null ";
                            wheredt += " and DJFSRID is null ";
                        }
                        if (!String.IsNullOrEmpty(gdzrid))
                        {
                            where += " and r.GDZRID='" + gdzrid + "' ";
                            wheredt += " and GDZRID='" + gdzrid + "' ";
                        }
                        else
                        {
                            where += " and r.GDZRID is null ";
                            wheredt += " and GDZRID is null ";
                        }
                        filedkeys = getFiledKeys(bjrid, min, where);
                        sql = "select " + filedkeys + " from SQM_MODEBJ_VAL where FEECALCID='{0}' and STATUS='1' {1}";
                        sql = string.Format(sql, bjrid, wheredt);
                        zbsjdt = DataHelper.QueryDataTable(sql);
                    }
                }
                return zbsjdt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string getFiledKeys(string bjrid, bool min, string where)
        {
            try
            {
                string filedkeys = "CALCUNIT,CURRENCY,CALCTYPE,";
                string sql = @"select r.CALCCODE,r.CALCNAME, r.SCALE,r.VALCOL,r.MSRCODE,r.ISCNT from SQM_FEE_CALC_REF r
                        left join SQM_BJ_PSF p on r.feecode=p.fee_code
                        where p.Rid='{0}' and r.STATUS='1' {1} order by r.SORD asc";
                sql = string.Format(sql, bjrid, where);
                DataTable FCREFdt = DataHelper.QueryDataTable(sql);
                if (FCREFdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in FCREFdt.Rows)
                    {
                        filedkeys += "'" + dr["CALCNAME"].ToString() + "',";
                        filedkeys += dr["VALCOL"].ToString() + " as " + dr["CALCCODE"] + ",";
                        filedkeys += dr["VALCOL"].ToString() + "C as " + dr["CALCCODE"] + "CODE,";
                        filedkeys += "'" + dr["SCALE"].ToString() + "'" + " as " + dr["CALCCODE"] + "SCALE,";
                        filedkeys += "'" + dr["MSRCODE"].ToString() + "'" + " as " + dr["CALCCODE"] + "MSRCODE,";
                        filedkeys += "'" + dr["ISCNT"].ToString() + "'" + " as " + dr["CALCCODE"] + "ISCNT,";
                    }
                }
                if (min)
                {
                    filedkeys += "MINBJPRICE,";
                }
                filedkeys += "PURPRICE,COSTPRICE,MAXPRICE,MINPRICE,GUIDEPRICE,to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,BJSTATUS,OVERSTATUS,BJPRICE,MEMO,SPRICE";
                return filedkeys;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CreateDJPSF(DataTable dataTable, string alonefee)
        {
            string djrid = System.Guid.NewGuid().ToString();
            string sql = "";
            DataTable dt = new DataTable();
            dt = dataTable.Copy();
            dt.Columns["FEE_CODE"].ColumnName = "FEECODE";
            dt.Columns["FEE_NAME"].ColumnName = "FEENAME";
            dt.Columns["SERVICE_CODE"].ColumnName = "SRVCODE";
            dt.Columns["SERVICE_NAME"].ColumnName = "SRVNAME";
            dt.Columns["PRODUCT_CODE"].ColumnName = "PRDCODE";
            dt.Columns["PRODUCT_NAME"].ColumnName = "PRDNAME";
            SQM_DJ_PSF sdp = TableToEntity<SQM_DJ_PSF>(dt.Rows[0].Table)[0];
            sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string isalone = DataHelper.QueryValue(string.Format("select distinct ISALONE from SQM_SRV_FEE_CONFIG where FEECODE='{0}' and STATUS='1'", sdp.FEECODE)) + "";
            if (isalone == "1")
            {
                sdp.PRDCODE = "";
                sdp.PRDNAME = "";
                sdp.SRVCODE = "";
                sdp.SRVNAME = "";
            }
            else
            {
                string firstFee = sdp.FEECODE.Substring(0, 1).ToUpper();
                if (firstFee == "A")
                {
                    sdp.BUSINESSORG = "空运";
                }
                else if (firstFee == "O")
                {
                    sdp.BUSINESSORG = "海运";
                }
                else if (firstFee == "S")
                {
                    sdp.BUSINESSORG = "供应链";
                }
                else if (firstFee == "L")
                {
                    sdp.BUSINESSORG = "运输";
                }
            }
            sdp.ALONEFEE = isalone;//绑定关系
            sdp.ORGRID = sdp.ORGCODE;
            sdp.ORGCODE = sdp.ORGNAME;
            if (sdp.ORGNAME != null)
            {
                sdp.ORGNAME = sdp.ORGNAME.Split('-')[0];
            }
            sdp.DJFS = "0";//默认普通定价
            sdp.RID = djrid;
            //判断是否存在该组织下的定价
            if (isalone == "1")
            {
                sql = string.Format("select RID from SQM_DJ_PSF where (STATUS='1' or STATUS is null) and ALONEFEE='1' and FEECODE='{0}' and ORGRID like '%{1}%'", sdp.FEECODE, sdp.ORGRID);
            }
            else
            {
                sql = string.Format("select RID from SQM_DJ_PSF where (STATUS='1' or STATUS is null) and PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and ORGRID like '%{3}%'", sdp.PRDCODE, sdp.SRVCODE, sdp.FEECODE, sdp.ORGRID);
            }
            string czdjrid = DataHelper.QueryValue(sql) + "";
            if (!String.IsNullOrEmpty(czdjrid))
            {
                djrid = czdjrid;
            }
            else
            {
                sdp.CREATESOURCE = "费目无定价报价";
                sdp.DoCreate();
            }
            return djrid;
        }
        /// <summary>
        /// 判断费目是否是期初数据
        /// </summary>
        /// <param name="psfrid"></param>
        /// <returns></returns>
        private bool IsOriginal(string psfrid)
        {
            bool original = false;
            string ori = DataHelper.QueryValue("select distinct original from sqm_bj_main_basic where rid = (select mrid from sqm_bj_psf where rid = '" + psfrid + "')") + "";
            if (ori == "1")
            {
                original = true;
            }
            return original;
        }
        #region 自定义报价
        /// <summary>
        /// 自定义报价方式
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Foqus.SQTracker]
        public ActionResult FMBJZDY(string RID, string alonefee)
        {
            //自定义参数
            DataTable psfdt = null;
            string djrid = "";
            string bjrid = "";
            if (RID == null)
            {
                djrid = Request.QueryString["djrid"];
                bjrid = Request.QueryString["bjrid"];
            }
            else
            {
                string[] rid = JsonHelper.GetObject<string[]>(RID);
                djrid = rid[0];
                bjrid = rid[1];
            }

            string sql = "";
            sql = @"select p.BJSTATAUS,p.BJFS,p.DISCOUNT,p.OTHER_NAME,p.OTHER_NAME_EN,p.STAGETYPE,p.MRID,p.VRID,p.FEE_CODE,p.FEE_NAME,p.ISLSC,p.CONDITION,p.JXJC,p.JSFCODE,p.JSF,p.JSFJS,p.ISLSC,
                    p.MINSTATUS,to_char(p.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(p.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,p.JSFJSCODE,
                    to_char(b.DTFROM, 'yyyy/mm/dd') as DTFROM,to_char(b.DTTO, 'yyyy/mm/dd') as DTTO,b.bjtcurr 
                    from SQM_BJ_PSF p left join SQM_BJ_MAIN_BASIC b on p.MRID=b.RID where p.RID ='" + bjrid + "'";
            psfdt = DataHelper.QueryDataTable(sql);

            ViewBag.PSFDATA = psfdt;
            ViewBag.DJRID = djrid;
            ViewBag.BJRID = bjrid;
            return View();
        }

        // 逻辑批量删除自定义报价明细
        [AllowAnonymous]
        public ActionResult DeleteZDY()
        {
            string[] rowIdArr = Request["rowIds"].Split(',');
            try
            {
                string part1 = "begin ";
                string part2 = " end;";
                string del = "";
                for (int m = 0; m < rowIdArr.Length; m++)
                {
                    del = @"delete from SQM_BJ_CUSTOMIZE where RID='" + rowIdArr[m] + "';";
                    part1 += del;
                }
                DataHelper.ExecSql(part1 + part2);
                return Content("删除成功!");
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
        }
        //自定义报价列表
        [AllowAnonymous]
        public ActionResult SelectFMBJZDY()
        {

            try
            {
                string wherestr = "";
                var bjrid = Request["bjrid"].ToString();
                var SERVICE_NAME = Request["SERVICE_NAME"].ToString();
                var FEE_NAME = Request["FEE_NAME"].ToString();
                if (bjrid != "")
                {
                    wherestr += " AND BJRID = '" + bjrid + "'";
                }
                if (SERVICE_NAME != "")
                {
                    wherestr += " AND SERVICE_NAME like '%" + SERVICE_NAME + "%'";
                }
                if (FEE_NAME != "")
                {
                    wherestr += " AND FEE_NAME like '%" + FEE_NAME + "%'";
                }
                string sql_from = @"select * from SQM_BJ_CUSTOMIZE ";
                string sql_order = @"ORDER BY CREATETIME desc";
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostZDYExcelData()
        {
            string bjrid = Request["bjrid"];
            string status = Request["status"];
            List<string> colname = new List<string>();
            List<string> names = new List<string>();
            List<DataSet> listDs = new List<DataSet>();
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0 || listDs.Count > 0)
                {
                    ArrayList al = new ArrayList();
                    if (files.Count > 0)
                    {
                        // 获取文件集合中的第一个文件(每次只上传一个文件)
                        HttpPostedFile file = files[0];
                        System.IO.Stream stream = file.InputStream;
                        al = GetDataFromExcel2(stream);
                    }
                    if (listDs.Count == 0)
                    {
                        listDs = (List<DataSet>)al[0];
                    }
                    DataSet ds = listDs[0];

                    string part1 = "begin ";
                    string part2 = " end;";
                    string insert = "";
                    string update = "";
                    for (int m = 0; m < ds.Tables.Count; m++)
                    {
                        string xlmc = ds.Tables[m].Columns[0].ToString().Trim();//线路名称
                        string fmmc = ds.Tables[m].Columns[1].ToString().Trim();//费目名称
                        string bjdw = ds.Tables[m].Columns[2].ToString().Trim();//报价单位
                        string djbz = ds.Tables[m].Columns[3].ToString().Trim();//单价/币种
                        string fmsm = ds.Tables[m].Columns[4].ToString().Trim();//费目名称

                        if (xlmc == "线路名称" && fmmc == "费目名称")
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(xlmc))
                        {
                            xlmc = "海运-海外代理-" + xlmc;
                            fmmc = fmmc.Contains("Column") ? "" : fmmc;//费目名称
                            bjdw = bjdw.Contains("Column") ? "" : bjdw;//报价单位
                            djbz = djbz.Contains("Column") ? "" : djbz;//单价/币种
                            fmsm = fmsm.Contains("Column") ? "" : fmsm;//费目名称

                            //插入之前 先检查是否存在
                            string sql = @"select * from SQM_BJ_CUSTOMIZE where BJRID='" + bjrid + "'" +
                                " and SERVICE_NAME='" + xlmc + "' and FEE_NAME='" + fmmc + "'" +
                                " and FEEUNIT='" + bjdw + "'";
                            var rtndata = DataHelper.QueryDataTable(sql);
                            if (rtndata.Rows.Count > 0)
                            {
                                update = @"update SQM_BJ_CUSTOMIZE set PRICEBZ='" + djbz + "',FSFYSM='" + fmsm + "' where RID='" + rtndata.Rows[0]["RID"].ToString() + "';";
                                part1 += update;
                            }
                            else
                            {
                                string guid = Guid.NewGuid().ToString();
                                insert = @"insert into SQM_BJ_CUSTOMIZE(BJRID,RID,SERVICE_NAME,FEE_NAME,FEEUNIT,PRICEBZ,FSFYSM) values ('" + bjrid + "','" + guid + "','" +
                                   xlmc + "','" + fmmc + "','" + bjdw + "','" + djbz + "','" + fmsm + "');";
                                part1 += insert;
                            }
                        }
                    }
                    DataHelper.ExecSql(part1 + part2);
                    //直接确认
                    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2' where rid = '" + bjrid + "'");
                    return Content(new JsonMessage { Message = "导入成功", Code = "0" }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = "导入失败,文件不存在", Code = "1" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="alonefee">从模板里过来是两个值：2-有绑定关系，3-无绑定关系</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Foqus.SQTracker]
        public ActionResult FMBJ(string RID, string alonefee)
        {
            string djrid = "";
            string bjrid = "";
            string sql = "";
            string minprice = "";
            //string djmin = "";
            string jxjc = "";
            string fsfeeunit = "";
            string gdzkey = Request.QueryString["gdzkey"];
            string gdzrid = Request.QueryString["gdzrid"];
            bool djqtfs = false;
            bool min = false;
            bool gdznum = true;
            bool ybjfeeunit = false;
            string businessorg = "";
            string unitwhere = "";
            string bgffeenames = "";
            string jsfflx = "";
            string jsff = "";
            string jtlj = "";
            string fsjdlb = ""; //方式阶段类别
            if (RID == null)
            {
                djrid = Request.QueryString["djrid"];
                bjrid = Request.QueryString["bjrid"];
            }
            else
            {
                string[] rid = JsonHelper.GetObject<string[]>(RID);
                djrid = rid[0];
                bjrid = rid[1];
            }
            // 如果djrid为空
            string sql_bj = "select t1.*,t2.CACLUNIT,t2.PRECOND,t2.RSLBASE,t2.ALLOWCACLOFFER from SQM_BJ_PSF t1 left join SQM_FEE_CALC t2 on t1.FEE_CODE = t2.FEECODE where t1.RID = '" + bjrid + "'";
            DataTable dt = DataHelper.QueryDataTable(sql_bj);
            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(djrid))
                {
                    djrid = CreateDJPSF(dt, alonefee);
                }
                if (alonefee == "1")
                {
                    businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_SRV_FEE_CONFIG where PRODCODE is null and SRVCODE is null and FEECODE='{0}'", dt.Rows[0]["FEE_CODE"].ToString())) + "";
                }
                else
                {
                    businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_SRV_FEE_CONFIG where PRODCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}'", dt.Rows[0]["PRODUCT_CODE"].ToString(), dt.Rows[0]["SERVICE_CODE"].ToString(), dt.Rows[0]["FEE_CODE"].ToString())) + "";
                }
            }
            IList<EasyDictionary> djdict = DataHelper.QueryDictList("select DJFS,IFDPDX,IFCOST from SQM_DJ_PSF where RID='" + djrid + "'");
            string djfs = djdict[0].Get("DJFS").ToString();
            string ifdpdx = djdict[0].Get("IFDPDX").ToString();
            string ifcost = djdict[0].Get("IFCOST").ToString();
            string bjfs = DataHelper.QueryValue("select case when b.BJFS is null then d.DJFS else b.BJFS end as DJFS from SQM_DJ_PSF d,SQM_BJ_PSF b where d.RID='" + djrid + "' and b.RID='" + bjrid + "'") + "";
            if (djfs == "1")
            {
                bjfs = "1";
            }
            if (String.IsNullOrEmpty(bjfs) && !String.IsNullOrEmpty(djfs))
            {
                ViewBag.bjfs = djfs;
            }
            else if (!String.IsNullOrEmpty(bjfs))
            {
                ViewBag.bjfs = bjfs;
            }
            ViewBag.djfs = djfs;
            ViewBag.ifdpdx = ifdpdx;
            ViewBag.ifcost = ifcost;
            //是否修改SQM_BJ_PSF的报价状态
            //SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(bjrid);
            //sql = @"select count(1) from SQM_MODEBJ_VAL where FEECALCID in('" + djrid + "','" + bjrid + "') and BJSTATUS='0'";
            //wbccount = DataHelper.QueryValue(sql) + "";
            //if (wbccount != "0")
            //{
            //    sbp.BJSTATAUS = "0"; // 状态更改为未保存
            //}
            //sbp.DoUpdate();
            string djfsrid = Request.QueryString["djfsrid"];
            DataTable psfdt = null;
            DataTable DJFSdt = null;
            DataTable GDZDATAdt = null;
            DataTable FCREFdt = null;
            sql = @"select p.BJSTATAUS,p.BJFS,p.DISCOUNT,p.OTHER_NAME,p.OTHER_NAME_EN,p.STAGETYPE,p.MRID,p.VRID,p.FEE_CODE,p.FEE_NAME,p.ISLSC,p.CONDITION,p.JXJC,p.JSFCODE,p.JSF,p.JSFJS,p.ISLSC,
                    p.MINSTATUS,to_char(p.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(p.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,p.JSFJSCODE,
                    to_char(b.DTFROM, 'yyyy/mm/dd') as DTFROM,to_char(b.DTTO, 'yyyy/mm/dd') as DTTO,b.bjtcurr 
                    from SQM_BJ_PSF p left join SQM_BJ_MAIN_BASIC b on p.MRID=b.RID where p.RID ='" + bjrid + "'";
            psfdt = DataHelper.QueryDataTable(sql);
            string feecode = psfdt.Rows[0]["FEE_CODE"].ToString();
            if (!String.IsNullOrEmpty(psfdt.Rows[0]["ISLSC"].ToString()) && psfdt.Rows[0]["ISLSC"].ToString() == "1")
            {
                sql = @"select FEE_NAME from SQM_BJ_PSF where BGFZRID='" + bjrid + "' and VRID='" + psfdt.Rows[0]["VRID"].ToString() + "'";
                DataTable bgfdt = DataHelper.QueryDataTable(sql);
                foreach (DataRow bgfdr in bgfdt.Rows)
                {
                    bgffeenames += bgfdr["FEE_NAME"] + ",";
                }
            }
            //定价方式判断
            sql = @"With DATASET AS(
                           select sfc.RID from SQM_FEE_CALC sfc 
                           left join SQM_DJ_PSF sdf on sfc.FEECODE=sdf.FEECODE
                           where sdf.RID='" + djrid + "') select distinct sfpr.DJFSRID,sfpr.DJFSNAME,sfpr.SIGHT_FSFYSM,sfpr.FSSORT,sfpr.FSDISP from DATASET t1 left join SQM_FEE_PUR_REF sfpr on t1.RID=sfpr.feerid  and sfpr.STATUS='1' where sfpr.DJFSRID is not null order by cast(sfpr.FSSORT as int) asc,sfpr.DJFSNAME asc";
            DJFSdt = DataHelper.QueryDataTable(sql);
            foreach (DataRow dr in DJFSdt.Rows)
            {
                if (String.IsNullOrEmpty(djfsrid))
                {
                    djfsrid = DataHelper.QueryValue("select DJFSRID from SQM_MODEBJ_VAL where STATUS='1' and DJFSRID='" + dr["DJFSRID"] + "' and FEECALCID='" + bjrid + "'") + "";
                }
                //string qtfs = "";
                //sql = @"select count(1) as count from SQM_MODEDJ_VAL where DJSTATUS='1' and DJFSRID='" + dr["DJFSRID"] + "' and FEECALCID='" + djrid + "'";
                //qtfs = DataHelper.QueryValue(sql) + "";
                //if (qtfs != "0")
                //{
                //    djqtfs = true;
                //    break;
                //}
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
                minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                if (minprice == "1")
                {
                    min = true;
                }
            }
            else
            {
                //sql = string.Format("SELECT DJFSRID,GDZRID,GDZKEY, GDZNAME,FSMIN FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' order by GDZNAME asc", feecode, djfsrid);
                sql = string.Format("SELECT DJFSRID,GDZRID,GDZKEY, GDZNAME,FSMIN,JSFFLX,JSFF FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' order by GDZNAME asc", feecode, djfsrid);
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
                    gdzkey = GDZDATAdt.Rows[0]["GDZKEY"].ToString();
                    gdzrid = GDZDATAdt.Rows[0]["GDZRID"].ToString();
                }
                wheredjfs = " and r.DJFSRID='" + djfsrid + "'";
                unitwhere = " and DJFSRID<>'" + djfsrid + "'";
                if (gdzkey == "0" || gdzkey == null)
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
                where r.STATUS='1' and p.Rid='{0}' {1} {2} {3}order by r.SORD asc";
            string searsql = string.Format(sql, djrid, wheredjfs, wheregdz, " and r.issearch='是' ");
            string fcrefsql = string.Format(sql, djrid, wheredjfs, wheregdz, " and 1=1 ");
            DataTable SEARFCREFdt = DataHelper.QueryDataTable(searsql);
            FCREFdt = DataHelper.QueryDataTable(fcrefsql);
            ViewBag.SEARFCREFdt = SEARFCREFdt;
            //获取费目的前提条件及定价方式的解析基础
            string qttj = psfdt.Rows[0]["CONDITION"].ToString();
            //sql = @"select distinct c.PRECOND,r.FSRSLBASE from SQM_FEE_CALC c left join SQM_FEE_PUR_REF r on c.FEECODE=r.FEECODE 
            //    where c.FEECODE='" + feecode + "' and r.STATUS='1' " + wheredjfs;
            //sql = @"select distinct c.PRECOND,r.FSRSLBASE,r.JSFFLX,r.JSFF from SQM_FEE_CALC c left join SQM_FEE_PUR_REF r on c.FEECODE=r.FEECODE 
            //    where c.FEECODE='" + feecode + "' and r.STATUS='1' " + wheredjfs;
            //前提条件、计算方式、解析基础、计算方法类型、阶段类别等
            sql = @"select distinct c.PRECOND,c.JSFFZS, r.FSRSLBASE,r.JSFFLX,r.JSFF,r.FSJDLB,r.JTLJ,r.FEEUNIT from SQM_FEE_CALC c left join SQM_FEE_PUR_REF r on c.FEECODE=r.FEECODE 
                where c.FEECODE='" + feecode + "' and r.STATUS='1' " + wheredjfs + wheregdz;
            DataTable fsdt = DataHelper.QueryDataTable(sql);
            if (fsdt.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(qttj))
                {
                    qttj = fsdt.Rows[0]["PRECOND"].ToString();
                }
                //计算方式表中的数据
                jxjc = fsdt.Rows[0]["FSRSLBASE"].ToString();
                jsfflx = fsdt.Rows[0]["JSFFLX"].ToString();
                jsff = fsdt.Rows[0]["JSFF"].ToString();
                jtlj = fsdt.Rows[0]["JTLJ"].ToString(); //阶梯累计
                fsjdlb = fsdt.Rows[0]["FSJDLB"].ToString();//方式阶段类别
                fsfeeunit = fsdt.Rows[0]["FEEUNIT"].ToString();//方式费目单位
                //计算方法展示
                if (fsdt.Rows[0]["JSFFZS"].ToString() == "1")
                {
                    //计算方法的类型列表
                    ViewBag.fftypedt = DataHelper.QueryDataTable("select distinct FFTYPE,FFTYPENAME from SQM_JSFF ORDER BY case when FFTYPE is null then 0 else 1 end asc, FFTYPE asc");
                    ViewBag.jsffzs = "1";
                    if (!string.IsNullOrEmpty(djfsrid) && !string.IsNullOrEmpty(gdzrid))
                    {
                        //当前的计算方法、类型
                        string currentjsff = string.Format("select jsfflx,jsff from SQM_FEE_PUR_REF where djfsrid = '{0}' and gdzrid ='{1}'", djfsrid, gdzrid);
                        DataTable jsfftd = DataHelper.QueryDataTable(currentjsff);
                        if (jsfftd != null && jsfftd.Rows.Count > 0)
                        {
                            if (jsfftd.Rows[0]["JSFFLX"].ToString() != "")
                            {
                                jsfflx = jsfftd.Rows[0]["JSFFLX"].ToString();
                            }
                            if (jsfftd.Rows[0]["JSFF"].ToString() != "")
                            {
                                jsff = jsfftd.Rows[0]["JSFF"].ToString();
                            }
                        }
                    }
                }
            }
            //只要是空、海运的都不能报多个定价方式的报价
            if (businessorg == "空运" || businessorg == "海运")
            {
                sql = string.Format("select RID from SQM_MODEBJ_VAL where FEECALCID='{0}' and STATUS='1' {1}", bjrid, unitwhere);
                DataTable unitDt = DataHelper.QueryDataTable(sql);
                if (unitDt.Rows.Count > 0)
                {
                    ybjfeeunit = true;
                }
            }
            //sql = string.Format("select r.MINBJPRICE,r.JXJC from SQM_MODEBJ_VAL r where r.FEECALCID='{0}' {1} {2}", bjrid, wheredjfs, wheregdz);
            sql = string.Format("select r.MINBJPRICE,r.JXJC,r.JSFFLX,r.JSFF,r.JTLJ,r.FSJDLB from SQM_MODEBJ_VAL r where r.FEECALCID='{0}' {1} {2}", bjrid, wheredjfs, wheregdz);
            DataTable minJcdt = DataHelper.QueryDataTable(sql);
            if (minJcdt.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(minJcdt.Rows[0]["MINBJPRICE"].ToString()))
                {
                    min = true;
                }
                //报价值表中有数据，则覆盖对应的数据
                if (!string.IsNullOrEmpty(minJcdt.Rows[0]["JXJC"].ToString()))
                {
                    jxjc = minJcdt.Rows[0]["JXJC"].ToString();

                }
                if (minJcdt.Rows[0]["JSFFLX"].ToString() != "")
                {
                    jsfflx = minJcdt.Rows[0]["JSFFLX"].ToString();
                }
                if (minJcdt.Rows[0]["JSFF"].ToString() != "")
                {
                    jsff = minJcdt.Rows[0]["JSFF"].ToString();
                }
                if (!string.IsNullOrEmpty(minJcdt.Rows[0]["JTLJ"].ToString()))
                {
                    jtlj = minJcdt.Rows[0]["JTLJ"].ToString();
                }
                if (!string.IsNullOrEmpty(minJcdt.Rows[0]["FSJDLB"].ToString()))
                {
                    fsjdlb = minJcdt.Rows[0]["FSJDLB"].ToString();
                }
            }
            string sql_bpcode = "select bpcode from sqm_bj_bp where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string sql_bp = "select bpname from sqm_bj_bp where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string sqm_sj = "select bizname from sqm_bj_biz where mrid = (select mrid from sqm_bj_psf where rid = '" + bjrid + "')";
            string bpcode = DataHelper.QueryValue(sql_bpcode) + "";
            string bpname = DataHelper.QueryValue(sql_bp) + "";
            string bizname = DataHelper.QueryValue(sqm_sj) + "";

            //阶梯累计

            if (gdzkey == "0" || String.IsNullOrEmpty(gdzkey))
            {
                wheregdz = " and GDZRID is null";
            }
            else
            {
                wheregdz = " and GDZRID='" + gdzrid + "'";
            }
            ////阶梯累计、解析基础、阶段类别
            //string jtljsql = "select JTLJ from SQM_FEE_PUR_REF where DJFSRID='" + djfsrid + "' " + wheregdz;
            //jtlj = DataHelper.QueryValue(jtljsql) + "";
            //获取定价MIN值
            //if (min)
            //{
            //    djmin = DataHelper.QueryValue(string.Format("select distinct MIN from SQM_MODEDJ_VAL where STATUS='1' and FEECALCID='{0}' and DJFSRID='{1}' {2}", djrid, djfsrid, wheregdz)) + "";
            //}

            ViewBag.BPCODE = bpcode;
            ViewBag.BPNAME = bpname;
            ViewBag.BIZNAME = bizname;
            ViewBag.FCREFData = FCREFdt;
            ViewBag.DJFSData = DJFSdt;
            ViewBag.djfsrid = djfsrid;
            ViewBag.gdzkey = gdzkey;
            ViewBag.gdzrid = gdzrid;
            ViewBag.GDZDATAdt = GDZDATAdt;
            ViewBag.DJRID = djrid;
            ViewBag.BJRID = bjrid;
            ViewBag.psfall = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select * from SQM_BJ_PSF where RID = '" + bjrid + "'"));
            ViewBag.PSFDATA = psfdt;
            ViewBag.djqtfs = djqtfs;
            ViewBag.bgffeenames = bgffeenames.TrimEnd(',');
            ViewBag.min = min;
            //ViewBag.djmin = djmin;
            ViewBag.gdznum = gdznum;
            ViewBag.qttj = qttj;
            ViewBag.jxjc = jxjc;
            ViewBag.fsfeeunit = fsfeeunit;
            ViewBag.ybjfeeunit = ybjfeeunit;
            ViewBag.jsfflx = jsfflx;
            ViewBag.jsff = jsff;
            ViewBag.jtlj = jtlj;
            ViewBag.fsjdlb = fsjdlb;
            ViewBag.srvbj = ConfigHelper.AppSettings("srvbj");
            return View();
        }
        [AllowAnonymous]
        public ActionResult SelectFMBJ()
        {
            string[] searchKeys = new string[] { "COLUMN1", "COLUMN2", "COLUMN3", "COLUMN4", "COLUMN5", "COLUMN6", "COLUMN7", "COLUMN8", "COLUMN9", "COLUMN10" };
            try
            {
                string searchStr = "";
                foreach (string key in searchKeys)
                {
                    if (!string.IsNullOrEmpty(Request[key]))
                    {
                        searchStr += " and " + key + "='" + Request[key] + "' ";
                        //Type valueType = typeof(SQM_MODEDJ_VAL).GetProperty(key).PropertyType;
                        //if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                        //{
                        //    searchStr += " and " + key + "='" + int.Parse(Request[key].Trim()) + "'";
                        //    //SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                        //}
                        //else
                        //    searchStr+=" and '"+key+"='"+ Convert.ChangeType(Request[key].Trim(), valueType
                        //    //SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                    }
                }

                string bpcode = Request["bpcode"];
                string ifbj = Request["ifbj"];
                string djrid = Request["djrid"];
                string bjrid = Request["bjrid"];
                string feecode = Request["feecode"];
                string djfsrid = Request["djfsrid"];
                string gdzrid = Request["gdzrid"];
                string gdzkey = Request["gdzkey"];
                string sql_val = "";
                sql_val = SearchBJSql(djrid, bjrid, feecode, ifbj, djfsrid, gdzrid, gdzkey, searchStr, bpcode);
                var total = DataHelper.QueryValue("select count(1) from (" + sql_val + ")");

                string order = "CREATETIME";
                string asc = !string.IsNullOrEmpty(Request["order"]) ? Request["order"] : "desc";
                var a = GetPageData(sql_val, order, asc);
                var obj = new { draw = Request["draw"], data = GetPageData(sql_val, order, asc), recordsTotal = total, recordsFiltered = total };
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SearchBJSql(string djrid, string bjrid, string feecode, string ifbj, string djfsrid, string gdzrid, string gdzkey, string searchStr, string bpcode)
        {
            string sql_val = @"select A.RID,A.CREATETIME,A.FEECALCID,to_char(A.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(A.ENDDATE,'yyyy/mm/dd') as ENDDATE,A.CURRENCY,A.PURPRICE,A.COSTPRICE,A.MINPRICE,A.MAXPRICE,A.GUIDEPRICE,A.BJPRICE,A.IFBJITEM,A.CALCTYPE,A.WDJBJRID,A.CALCUNIT ";
            DataTable dt = new DataTable();
            string sql_ref = "";
            string sql_ifbj = " and A.STATUS='1' ";
            string sql = "";
            string sql_zval = "";
            string searchCustomersSql = "";
            string customersno = string.Empty;
            //高低值比较判断
            if (String.IsNullOrEmpty(djfsrid))
            {
                sql_ifbj += " and A.DJFSRID is null";
                sql_ifbj += " and A.GDZRID is null";
            }
            else
            {
                sql = string.Format("SELECT GDZRID,GDZKEY, GDZNAME FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' order by GDZNAME asc", feecode, djfsrid);
                DataTable GDZDATAdt = DataHelper.QueryDataTable(sql);
                if (String.IsNullOrEmpty(gdzrid) && GDZDATAdt.Rows.Count > 0)
                {
                    gdzrid = GDZDATAdt.Rows[0]["GDZRID"].ToString();
                    gdzkey = GDZDATAdt.Rows[0]["GDZKEY"].ToString();
                }
                sql_ifbj += " and A.DJFSRID='" + djfsrid + "'";
                if (gdzkey == "0" || String.IsNullOrEmpty(gdzrid))
                {
                    sql_ifbj += " and A.GDZRID is null";
                }
                else
                {
                    sql_ifbj += " and A.GDZRID='" + gdzrid + "'";
                }
            }
            sql_ref = string.Format("select A.CALCCODE,A.CALCNAME,A.VALCOL from SQM_FEE_CALC_REF A where A.STATUS='1' and A.FEECODE = '{0}' {1}", feecode, sql_ifbj);
            dt = DataHelper.QueryDataTable(sql_ref);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sql_val += ",A." + dr["VALCOL"].ToString() + ",A." + dr["VALCOL"].ToString() + "C";
                }
            }
            searchCustomersSql = "SELECT  CUSTOMERSNO FROM SQM_MODEDJ_VAL WHERE DJFSRID = '" + djfsrid + "'";
            DataTable customersNodt = DataHelper.QueryDataTable(searchCustomersSql);
            if (customersNodt.Rows.Count > 0)
            {
                customersno = customersNodt.Rows[0]["CUSTOMERSNO"].ToString();
            }
            else
            {
                customersno = string.Empty;
            }
            //是否报价项目
            if (!String.IsNullOrEmpty(ifbj))
            {
                sql_ifbj += " and A.IFBJITEM='" + ifbj + "'";
                if (ifbj == "0")
                {
                    //0 非报价项目，查询定价值表
                    sql_zval = string.Format(sql_val + ",A.MIN,A.MIN as DJMIN,'DJ' as DATASOURCE from SQM_MODEDJ_VAL A where A.DJSTATUS = '1' and A.FEECALCID='{1}' and (instr(a.customersno, '') > 0 or a.customersno is null or a.customersno = '') {0}" + searchStr + " union " + sql_val + ",A.MINBJPRICE as MIN,B.MIN as DJMIN,'BJ' as DATASOURCE from SQM_MODEBJ_VAL A left join SQM_MODEDJ_VAL B on A.DJRID=B.RID where A.FEECALCID='{2}' and (instr(b.customersno, 'ZZZS001')>0 or b.customersno is null or b.customersno = '') {0}", sql_ifbj, djrid, bjrid);
                }
                else
                {
                    sql_zval = string.Format(sql_val + ",A.MIN,A.MIN as DJMIN,'DJ' as DATASOURCE from SQM_MODEDJ_VAL A where A.DJSTATUS = '1' and A.FEECALCID='{1}' and (instr(a.customersno, '') > 0 or a.customersno is null or a.customersno = '') {0}" + " union " + sql_val + ",A.MINBJPRICE as MIN,B.MIN as DJMIN,'BJ' as DATASOURCE from SQM_MODEBJ_VAL A left join SQM_MODEDJ_VAL B on A.DJRID=B.RID where A.FEECALCID='{2}' and (instr(b.customersno, 'ZZZS001')>0 or b.customersno is null or b.customersno = '') {0}" + searchStr, sql_ifbj, djrid, bjrid);
                    //增加指定客户的条件 sql待调整
                   // sql_zval = string.Format(sql_val + ",A.MIN,A.MIN as DJMIN,'DJ' as DATASOURCE from SQM_MODEDJ_VAL A where A.DJSTATUS = '1' and A.FEECALCID='{1}' {0} and A.CUSTOMERSNO IS NULL OR CUSTOMERSNO="+"'"+customersno+"'" + " union " + sql_val + ",A.MINBJPRICE as MIN,B.MIN as DJMIN,'BJ' as DATASOURCE from SQM_MODEBJ_VAL A left join SQM_MODEDJ_VAL B on A.DJRID=B.RID where A.CUSTOMERSNO IS NULL OR A.CUSTOMERSNO="+"'"+customersno+"'"+ "A.FEECALCID='{2}' {0}" + searchStr, sql_ifbj, djrid, bjrid);
                }
                return sql_zval;
            }
            sql_zval = string.Format(sql_val + ",A.MIN,A.MIN as DJMIN,'DJ' as DATASOURCE from SQM_MODEDJ_VAL A where A.DJSTATUS = '1' and A.FEECALCID='{1}' and (instr(a.customersno, '') > 0 or a.customersno is null or a.customersno = '')  {0}" + " union " + sql_val + ",A.MINBJPRICE as MIN,B.MIN as DJMIN,'BJ' as DATASOURCE from SQM_MODEBJ_VAL A left join SQM_MODEDJ_VAL B on A.DJRID=B.RID where A.FEECALCID='{2}' and (instr(b.customersno, 'ZZZS001')>0 or b.customersno is null or b.customersno = '') {0}", sql_ifbj, djrid, bjrid);
            return sql_zval;
        }

        /// <summary>
        /// 通过主键得到产品、服务、费目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 配置表
        //public ActionResult GetAllByRid(string bjrid, string vrid)
        //{
        //    string sql = string.Format("SELECT  DISTINCT sqm_bj_psf.product_code, sqm_bj_psf.product_name,orgcode FROM sqm_bj_psf WHERE sqm_bj_psf. RID = '{0}'", bjrid);
        //    var prdArray = DataHelper.QueryObjectsList(sql);
        //    string prdcodeStr = "";
        //    string orgcode = prdArray[0][2].ToString();//组织相同取第一个

        //    foreach (object[] item in prdArray)
        //    {
        //        prdcodeStr += "'" + item[0] + "',";
        //    }
        //    //string sql2 = string.Format("SELECT distinct MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME,SQM_SRV_EXT.Sord FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE LEFT JOIN SQM_SRV_EXT ON MDM_PRD_SRV_REF.SERVICETYPECODE = SQM_SRV_EXT.SERVICEKEY LEFT JOIN SQM_SRV_FEE_CONFIG ON MDM_PRD_SRV_REF.PRODUCTCODE= SQM_SRV_FEE_CONFIG.PRODCODE AND MDM_PRD_SRV_REF.SERVICETYPECODE=SQM_SRV_FEE_CONFIG.SRVCODE  WHERE MDM_PRD_SRV_REF.PRODUCTCODE IN ({0}) AND SQM_SRV_FEE_CONFIG.SRVDISP='1' order by SQM_SRV_EXT.Sord", prdcodeStr.TrimEnd(','));
        //    string sql2 = string.Format("select distinct c.PRODCODE,c.SRVCODE,c.SRVNAME,e.SORD from SQM_SRV_FEE_CONFIG c left join SQM_SRV_EXT e on c.PRODCODE=e.PRODUCTCODE and c.SRVCODE=e.SERVICEKEY where  c.PRODCODE in ({0}) and c.SRVDISP = '1' order by e.Sord", prdcodeStr.TrimEnd(','));
        //    var srvArray = DataHelper.QueryObjectsList(sql2);
        //    //string feeStr = "";
        //    //for (var i = 0; i < srvArray.Count; i++)
        //    //{
        //    //    feeStr += "'" + srvArray[i][1] + "',";
        //    //}
        //    //feeStr = feeStr.TrimEnd(',');
        //    //string sql3 = string.Format("With DATASET AS(select distinct mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj,qdm_fee_srv_ref.sorid from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join v_mdm_fee mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid where mdm_prd_srv_ref.productcode in ({0}) and mdm_srv_fee_ref.tcet084 not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and (BGFZRID<>'{2}' or BGFZRID is null) and choosestatus = '1') order by qdm_fee_srv_ref.SORID) select t1.*,t2.feecatg from DATASET t1 inner join SQM_SRV_FEE_CONFIG t2 on t1.productcode=t2.Prodcode and t1.servicetypecode=t2.srvcode and t1.tcet084=t2.feecode and t2.feecatg<>'2'", prdcodeStr.TrimEnd(','), vrid, bjrid);
        //    string sql3 = string.Format("select distinct c.PRODCODE,c.PRODNAME,c.SRVCODE,c.SRVNAME,c.FEECODE,c.FEENAME,r.BXBJ,r.SORID,c.FEECATG from SQM_SRV_FEE_CONFIG c left join QDM_FEE_SRV_REF r on c.PRODCODE=r.Productcode and c.SRVCODE=r.SERVICETYPECODE and c.FEECODE=r.FEECODE where c.FEECATG<>'2' and c.PRODCODE in ({0}) and c.FEECODE not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and (BGFZRID<>'{2}' or BGFZRID is null) and choosestatus = '1') order by r.SORID", prdcodeStr.TrimEnd(','), vrid, bjrid);
        //    var feeArray = DataHelper.QueryObjectsList(sql3);
        //    List<object[]> newfeeArr = new List<object[]>(feeArray);
        //    foreach (var fee in feeArray)
        //    {
        //        string feecalcid = "";
        //        //异常费目再根据有效定价来控制是否展现
        //        if (fee[8].ToString() == "1")
        //        {
        //            string sql4 = string.Format("select RID from SQM_DJ_PSF t where PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and ORGRID like'%{3}%'", fee[0].ToString(), fee[2].ToString(), fee[4].ToString(), orgcode);
        //            DataTable djpsfDt = DataHelper.QueryDataTable(sql4);
        //            if (djpsfDt.Rows.Count == 0)
        //            {
        //                newfeeArr.Remove(fee);//SQM_DJ_PSF没有就不显示
        //                continue;
        //            }
        //            else
        //            {
        //                feecalcid = djpsfDt.Rows[0]["RID"].ToString();
        //                string sql5 = string.Format("select RID from SQM_MODEDJ_VAL where FEECALCID='{0}' and STATUS='1' and DJSTATUS='1'", feecalcid);
        //                DataTable djvalDt = DataHelper.QueryDataTable(sql5);
        //                if (djvalDt.Rows.Count == 0)
        //                {
        //                    newfeeArr.Remove(fee);//SQM_MODEDJ_VAL没有就不显示
        //                    continue;
        //                }
        //            }
        //        }
        //    }
        //    object[] data = { prdArray, srvArray, newfeeArr };
        //    return Content(JsonHelper.GetJsonString(data));
        //}
        public ActionResult GetAllByRid(string bjrid, string vrid)
        {
            string sql = string.Format("SELECT  DISTINCT sqm_bj_psf.product_code, sqm_bj_psf.product_name FROM sqm_bj_psf WHERE sqm_bj_psf. RID = '{0}'", bjrid);
            var prdArray = DataHelper.QueryObjectsList(sql);
            string prdcodeStr = "";

            foreach (object[] item in prdArray)
            {
                prdcodeStr += "'" + item[0] + "',";
            }

            string sql2 = string.Format("SELECT distinct MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME,SQM_SRV_EXT.Sord FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE LEFT JOIN SQM_SRV_EXT ON MDM_PRD_SRV_REF.PRODUCTCODE=SQM_SRV_EXT.PRODUCTCODE AND MDM_PRD_SRV_REF.SERVICETYPECODE = SQM_SRV_EXT.SERVICEKEY  WHERE MDM_PRD_SRV_REF.PRODUCTCODE IN ({0}) order by SQM_SRV_EXT.Sord", prdcodeStr.TrimEnd(','));
            var srvArray = DataHelper.QueryObjectsList(sql2);
            //string feeStr = "";
            //for (var i = 0; i < srvArray.Count; i++)
            //{
            //    feeStr += "'" + srvArray[i][1] + "',";
            //}
            //feeStr = feeStr.TrimEnd(',');
            //string sql3 = string.Format("select distinct mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj,qdm_fee_srv_ref.SORID from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join v_mdm_fee mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid where mdm_prd_srv_ref.productcode in ({0}) and mdm_srv_fee_ref.tcet084 not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and (BGFZRID<>'{2}' or BGFZRID is null) and choosestatus = '1') order by qdm_fee_srv_ref.SORID", prdcodeStr.TrimEnd(','), vrid, bjrid);
            //过滤掉已做包干费的费目
            //string sql3 = string.Format("select distinct mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj,qdm_fee_srv_ref.SORID,f.FSFYSMS from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join v_mdm_fee mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid left join SQM_SRV_FEE_CONFIG c on mdm_prd_srv_ref.productcode=c.prodcode and mdm_prd_srv_ref.servicetypecode=c.srvcode and mdm_srv_fee_ref.tcet084=c.feecode left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on mdm_srv_fee_ref.tcet084 = f.FEECODE where mdm_prd_srv_ref.productcode in ({0}) and c.feecatg<>'2' and mdm_srv_fee_ref.tcet084 not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and (BGFZRID<>'{2}' or BGFZRID is null) and choosestatus = '1') order by qdm_fee_srv_ref.SORID", prdcodeStr.TrimEnd(','), vrid, bjrid);
            //报价详细页面包干费可以选atcost费目 2019-8-5 DLC
            string sql3 = string.Format("select distinct mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj,qdm_fee_srv_ref.SORID,f.FSFYSMS from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join V_MDM_FEE mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid left join SQM_SRV_FEE_CONFIG c on mdm_prd_srv_ref.productcode=c.prodcode and mdm_prd_srv_ref.servicetypecode=c.srvcode and mdm_srv_fee_ref.tcet084=c.feecode left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on mdm_srv_fee_ref.tcet084 = f.FEECODE where mdm_prd_srv_ref.productcode in ({0}) and c.feecatg is not null and c.status='1' order by qdm_fee_srv_ref.SORID", prdcodeStr.TrimEnd(','), vrid, bjrid);
            var feeArray = DataHelper.QueryObjectsList(sql3);
            object[] data = { prdArray, srvArray, feeArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult SaveToPSF(string postdata, string bjrid, string vrid, string mrid)
        {
            List<PRD> dataArray = JsonHelper.GetObject<List<PRD>>(postdata);
            var rtnmessage = "保存成功";
            try
            {
                var sqldata = SQM_BJ_PSF.TryFind(bjrid);
                sqldata.BGFZRID = "1";
                sqldata.ISLSC = "1";
                sqldata.STATUS = "1";
                sqldata.DoUpdate();
                DataHelper.ExecSql("delete from SQM_BJ_PSF where BGFZRID='" + bjrid + "'");
                foreach (var p in dataArray)
                {
                    foreach (var s in p.srvcodes)
                    {
                        foreach (var f in s.feecodes)
                        {
                            SQM_BJ_PSF srcobj = new SQM_BJ_PSF();
                            srcobj.MRID = mrid;
                            srcobj.VRID = vrid;
                            srcobj.BGFZRID = bjrid;
                            srcobj.PRODUCT_CODE = p.prdcode;
                            srcobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                            srcobj.SERVICE_CODE = s.srvcode;
                            srcobj.SERVICE_NAME = CODETONAME("srv", s.srvcode);
                            srcobj.ORGNAME = sqldata.ORGNAME;
                            srcobj.ORGCODE = sqldata.ORGCODE;
                            srcobj.BJFS = sqldata.BJFS;
                            srcobj.FEE_CODE = f;
                            srcobj.FEE_NAME = CODETONAME("fee", f);
                            srcobj.BJSTATAUS = sqldata.BJSTATAUS;
                            srcobj.ALOENFEE = sqldata.ALOENFEE;
                            srcobj.MINSTATUS = sqldata.MINSTATUS;
                            srcobj.FEECATG = getCatg(p.prdcode, s.srvcode, f);//被包干的费目就可以认为是正常费目
                            srcobj.ISLSC = "1";
                            srcobj.STATUS = "1";
                            srcobj.CHOOSESTATUS = "1";
                            srcobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            srcobj.DoCreate();
                            DataHelper.ExecSql("delete from SQM_BJ_PSF where BGFZRID is null and PRODUCT_CODE='" + p.prdcode + "' and SERVICE_CODE='" + s.srvcode + "' and FEE_CODE='" + f + "' and VRID='" + vrid + "' and FEECATG='2'");//删除掉服务的atcost费目
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rtnmessage = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(rtnmessage));
        }
        public string showPSF(string bjrid, string vrid)
        {
            //psf表信息
            var sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND BGFZRID = '{1}' and ISLSC='1'", vrid, bjrid);
            var psfList = DataHelper.QueryDictList(sql);

            return JsonHelper.GetJsonString(psfList);
        }
        /// <summary>
        /// 判断是否需要复制，并返回费目id
        /// </summary>
        /// <returns></returns>
        public ActionResult Duplicate()
        {
            string feecode = Request["feecode"] + "";
            string rid = Request["rid"] + "";
            string vrid = DataHelper.QueryValue("select distinct VRID from SQM_BJ_PSF where RID = '" + rid + "'") + "";
            List<string> rids = new List<string>();
            if (vrid != "")
            {
                IList<EasyDictionary> dictList = DataHelper.QueryDictList("select RID from SQM_BJ_PSF where VRID = '" + vrid + "' and FEE_CODE = '" + feecode + "' and RID <> '" + rid + "' and (bgfzrid is null or bgfzrid = '1')");// 去掉包干费
                if (dictList.Count != 0)
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
            try
            {
                string sign = Request["sign"]; // 保存还是确定
                string bjrid = Request["bjrid"];
                string ifbjitem = Request["ifbjitem"];
                string ifbjval = Request["ifbjval"];//报价项值
                string psfdata = Request["psfdata"];
                DataTable dtpsfall = JsonHelper.GetObject<DataTable>(psfdata);
                string feeitems = Request["feeitems"]; // 更新报价费目表数据
                DataTable dtpsf = JsonHelper.GetObject<DataTable>(feeitems);
                foreach (DataColumn col in dtpsf.Columns)
                {
                    dtpsfall.Rows[0][col.ColumnName] = dtpsf.Rows[0][col.ColumnName];
                }
                List<SQM_BJ_PSF> entspsf = TableToEntity<SQM_BJ_PSF>(dtpsfall);
                if (sign == "2" && ifbjitem == "{}" && entspsf[0].BJFS == "0")//普通报价的确认
                {
                    string cznum = DataHelper.QueryValue("select count(1) as count from SQM_MODEBJ_VAL where FEECALCID='" + bjrid + "'") + "";
                    if (cznum == "0" && !(ifbjitem.Contains("\"1\"")))
                    {
                        return Content(new JsonMessage { Message = "没有可确认的报价信息！", Success = false }.ToString());
                    }
                    else if (cznum != "0" && !(ifbjitem.Contains("\"1\"")))
                    {
                        return Content(new JsonMessage { Message = "没有可确认的报价信息！", Success = false }.ToString());
                    }
                }
                string jurid = string.Empty;
                Dictionary<string, string> ifbjitemsju = JsonHelper.GetObject<Dictionary<string, string>>(ifbjitem);

                if (ifbjitemsju != null)
                {
                    foreach (var item in ifbjitemsju)
                    {
                        if (item.Value == "1")
                        {
                            jurid += " '" + item.Key + "', ";
                        }
                    }
                    jurid = !string.IsNullOrEmpty(jurid) ? jurid.Substring(0, jurid.Length - 2) : "";
                }
                string sql = "";
                string message = "保存成功";
                string ifcopy = Request["ifcopy"]; // 是否复制到其他费目
                string mrid = Request["mrid"];
                string djrid = Request["djrid"];
                string djfsrid = Request["djfsrid"];
                string gdzrid = Request["gdzrid"];
                string gdzkey = Request["gdzkey"];
                string calcunit = Request["calcunit"];
                string bjstatus = Request["bjstatus"];
                string ifall = Request["ifall"]; // 全部保存还是全部确定
                string hasbjitem = Request["hasbjitem"]; // 是否有报价项
                string djqtfs = Request["djqtfs"]; // 是否有其他定价方式
                string jsfflx = Request["jsfflx"];//仓租费计算方法、类型
                string jsff = Request["jsff"];
                string fsfeeunit = Request["fsfeeunit"];//方式费目单位
                string jtlj = Request["jtlj"];
                string minstatus = "0";//MIN值状态
                string fsjdlb = Request["fsjdlb"];
                //判断维度与时间
                if (!judgedj(jurid, djfsrid, djrid))
                {
                    return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请确认！" }.ToString());
                }
                //报价项的时候不勾选报价行，删除该方式下的所有报价数据
                if (ifbjval == "1" && ifbjitem != "{}" && !(ifbjitem.Contains("\"1\"")))
                {
                    if (!String.IsNullOrEmpty(gdzrid))
                    {
                        DataHelper.ExecSql(string.Format("delete from SQM_MODEBJ_VAL where FEECALCID='{0}' and GDZRID='{1}'", bjrid, gdzrid));
                    }
                    else
                    {
                        DataHelper.ExecSql(string.Format("delete from SQM_MODEBJ_VAL where FEECALCID='{0}' and DJFSRID='{1}'", bjrid, djfsrid));
                    }
                }
                if (IsOriginal(bjrid))// 期初数据状态始终为“已确认”
                {
                    bjstatus = "2";
                }
                else
                {
                    if (sign == "2")
                    {
                        message = "费目确认成功";
                    }
                    if (sign == "2" && bjstatus == "4")
                    {
                        bjstatus = "5";//已确认（报价超限）
                    }
                    if (sign == "2" && bjstatus == "1")
                    {
                        bjstatus = "2";//已确认
                    }
                }
                Dictionary<string, string> ifbjitems = JsonHelper.GetObject<Dictionary<string, string>>(ifbjitem);
                string djval = Request["djval"]; // 值表数据（定价值表或者报价值表）
                string idguidprice = Request["idguidprice"];//报价
                string idmin = Request["idmin"];//MIN
                //string minval = Request["minval"];//MIN值
                string bjchoose = Request["bjchoose"];//是否有报价项目
                //string idcalctype = Request["idcalctype"];//计算类型
                string WAERS = Request["WAERS"];


                string bjminval = "";
                Dictionary<string, string> idmindc = null;
                DataTable dtDj = new DataTable();
                //普通报价方式才走下面
                if (entspsf[0].BJFS == "0"|| entspsf[0].BJFS == "1")
                {
                    if (djval != "[]")
                    {
                        dtDj = JsonHelper.GetObject<DataTable>(djval);
                        if (!dtDj.Columns.Contains("DJFSRID"))// 定价方式RID
                        {
                            dtDj.Columns.Add("DJFSRID");
                        }
                        if (!dtDj.Columns.Contains("BJPRICE"))
                        {
                            dtDj.Columns.Add("BJPRICE");
                        }
                        if (!String.IsNullOrEmpty(idmin))
                        {
                            idmindc = JsonHelper.GetObject<Dictionary<string, string>>(idmin);
                        }
                        if (!string.IsNullOrEmpty(idguidprice))
                        {
                            Dictionary<string, string> dc = JsonHelper.GetObject<Dictionary<string, string>>(idguidprice);
                            //Dictionary<string, string> calctype = JsonHelper.GetObject<Dictionary<string, string>>(idcalctype);
                            // 将"报价"插入datatable 删除新增数据（报价已经存在）
                            for (int i = dtDj.Rows.Count - 1; i >= 0; i--)
                            {
                                //获取汇率
                                decimal hl = 1;
                                if (dtDj.Rows[i]["CURRENCY"].ToString().Trim() == WAERS.Trim())
                                {
                                    hl = 1;
                                }
                                else
                                {
                                    DataTable dt_Tcurrr = DataHelper.QueryDataTable("select * from (select * from Mdm_Tcurr t order by t.gdate desc) a where a.fcurr='" + dtDj.Rows[i]["CURRENCY"].ToString() + "' and a.tcurr='" + WAERS + "' and rownum=1");

                                    if (dt_Tcurrr.Rows.Count <= 0)
                                    {
                                        return Content(new JsonMessage { Success = false, Message = "没有找到转换汇率" }.ToString());
                                    }
                                    hl = dt_Tcurrr.Rows.Count > 0 ? decimal.Parse(dt_Tcurrr.Rows[0]["UKURS"].ToString()) : 1;
                                }
                                //最大报价
                                Regex rg = new Regex("^[0-9]$");
                                string maxpriceval = rg.IsMatch(dtDj.Rows[i]["MAXPRICE"].ToString()) ? "0" : dtDj.Rows[i]["MAXPRICE"].ToString();
                                string minpriceval = rg.IsMatch(dtDj.Rows[i]["MINPRICE"].ToString()) ? "0" : dtDj.Rows[i]["MINPRICE"].ToString();
                                rg = new Regex("^[0-9]$");
                                string guidepriceval = rg.IsMatch(dtDj.Rows[i]["GUIDEPRICE"].ToString()) ? "0" : dtDj.Rows[i]["GUIDEPRICE"].ToString();

                                if (ifbjitems[dtDj.Rows[i]["RID"].ToString()] == "0")
                                {
                                    DataHelper.ExecSql("delete from SQM_MODEBJ_VAL where RID='" + dtDj.Rows[i]["RID"].ToString() + "'");
                                    continue;
                                }
                                string bjpriceval = (dc[dtDj.Rows[i]["RID"].ToString()] + "").Trim() == "" ? "0" : dc[dtDj.Rows[i]["RID"].ToString()];
                                if (idmindc.Count != 0)
                                {
                                    bjminval = (idmindc[dtDj.Rows[i]["RID"].ToString()] + "").Trim() == "" ? "0" : idmindc[dtDj.Rows[i]["RID"].ToString()];
                                }

                                //string bjcalctype = calctype[dtDj.Rows[i]["RID"].ToString()];
                                if (!String.IsNullOrEmpty(dtDj.Rows[i]["WDJBJRID"].ToString()))
                                {
                                    SQM_MODEBJ_VAL oldsmvbj = SQM_MODEBJ_VAL.FindFirstByProperties(SQM_MODEBJ_VAL.Prop_WDJBJRID, dtDj.Rows[i]["WDJBJRID"], SQM_MODEBJ_VAL.Prop_FEECALCID, bjrid);
                                    oldsmvbj.BJSTATUS = bjstatus;
                                    oldsmvbj.IFBJITEM = ifbjitems[dtDj.Rows[i]["RID"].ToString()];
                                    //oldsmvbj.BJPRICE = Convert.ToDecimal(bjpriceval);
                                    ////oldsmvbj.CALCTYPE = bjcalctype;
                                    //if (!String.IsNullOrEmpty(bjminval))
                                    //{
                                    //    oldsmvbj.MINBJPRICE = Convert.ToDecimal(bjminval);
                                    //}
                                    //仓租费计算方法、类型
                                    oldsmvbj.JSFFLX = jsfflx;
                                    oldsmvbj.JSFF = jsff;
                                    oldsmvbj.FEEUNIT = fsfeeunit;
                                    oldsmvbj.MINSTATUS = minstatus;
                                    oldsmvbj.FSJDLB = fsjdlb;
                                    oldsmvbj.JXJC = entspsf[0].JXJC;

                                    if (hl > 0 )//&& hl != 1
                                    {
                                        oldsmvbj.BJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) * hl), 5);
                                        if (!String.IsNullOrEmpty(bjminval))
                                        {
                                            oldsmvbj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) * hl), 5);
                                        }
                                        if (!string.IsNullOrEmpty(maxpriceval))
                                        {
                                            oldsmvbj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(maxpriceval) * hl), 5);
                                        }
                                        if (!string.IsNullOrEmpty(guidepriceval))
                                        {
                                            oldsmvbj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(Convert.ToDecimal(guidepriceval) * hl)), 5);
                                        }
                                        oldsmvbj.CURRENCY = WAERS;
                                    }
                                    else if (hl < 0)
                                    {
                                        oldsmvbj.BJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) / hl), 5);
                                        if (!String.IsNullOrEmpty(bjminval))
                                        {
                                            oldsmvbj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) / hl), 5);
                                        }
                                        if (!string.IsNullOrEmpty(maxpriceval))
                                        {
                                            oldsmvbj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(maxpriceval) / hl), 5);
                                        }
                                        if (!string.IsNullOrEmpty(guidepriceval))
                                        {
                                            oldsmvbj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(Convert.ToDecimal(guidepriceval) / hl)), 5);
                                        }
                                        oldsmvbj.CURRENCY = WAERS;
                                    }
                                    oldsmvbj.DoUpdate();
                                    continue;
                                }
                                else
                                {
                                    SQM_MODEBJ_VAL srcobj = null;
                                    SQM_MODEBJ_VAL ybjsmvobj = SQM_MODEBJ_VAL.FindFirstByProperties(SQM_MODEBJ_VAL.Prop_DJRID, dtDj.Rows[i]["RID"], SQM_MODEBJ_VAL.Prop_FEECALCID, bjrid);
                                    if (ybjsmvobj != null)
                                    {
                                        continue;
                                    }
                                    SQM_MODEBJ_VAL oldsmvobj = SQM_MODEBJ_VAL.FindFirstByProperties(SQM_MODEBJ_VAL.Prop_RID, dtDj.Rows[i]["RID"], SQM_MODEBJ_VAL.Prop_FEECALCID, bjrid);
                                    if (oldsmvobj == null)
                                    {
                                        srcobj = TableToEntity<SQM_MODEBJ_VAL>(dtDj.Rows[i].Table)[i];
                                        decimal bjpriceval_hl = Convert.ToDecimal(bjpriceval);
                                        if (idmindc.Count != 0)
                                        {
                                            string djmin = DataHelper.QueryValue(string.Format("select MIN from SQM_MODEDJ_VAL where RID='{0}' and DJSTATUS='1'", dtDj.Rows[i]["RID"].ToString())) + "";
                                            djmin = string.IsNullOrEmpty(djmin) ? "0" : djmin;
                                            if (Convert.ToDecimal(bjminval) < Convert.ToDecimal(djmin))
                                            {
                                                minstatus = "1";//MIN变小
                                            }
                                            else if (Convert.ToDecimal(bjminval) > Convert.ToDecimal(djmin))
                                            {
                                                minstatus = "2";//MIN变大
                                            }

                                            if (hl > 0 )//&& hl != 1
                                            {
                                                srcobj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) * hl), 5);
                                                bjpriceval_hl = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) * hl), 5);
                                                srcobj.CURRENCY = WAERS;
                                            }
                                            else if (hl < 0)
                                            {
                                                srcobj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) / hl), 5);
                                                bjpriceval_hl = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) / hl), 5);
                                                srcobj.CURRENCY = WAERS;
                                            }
                                            //srcobj.MINBJPRICE = Convert.ToDecimal(bjminval);
                                        }
                                        else
                                        {
                                            if (hl > 0)// && hl != 1
                                            {
                                                bjpriceval_hl = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) * hl), 5);
                                            }
                                            else if (hl < 0)
                                            {
                                                bjpriceval_hl = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) / hl), 5);
                                            }
                                        }
                                        srcobj.DJRID = dtDj.Rows[i]["RID"].ToString();
                                        srcobj.FEECALCID = dtpsfall.Rows[0]["RID"].ToString();
                                        srcobj.STATUS = "1";
                                        srcobj.IFBJITEM = ifbjitems[dtDj.Rows[i]["RID"].ToString()];
                                        srcobj.BJPRICE = bjpriceval_hl;//Convert.ToDecimal(bjpriceval);
                                        srcobj.DJFSRID = djfsrid;
                                        srcobj.GDZRID = gdzrid;
                                        srcobj.BJSTATUS = bjstatus;
                                        srcobj.MINSTATUS = minstatus;
                                        //srcobj.CALCTYPE = bjcalctype;
                                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                        srcobj.JSFFLX = jsfflx;
                                        srcobj.JSFF = jsff;
                                        srcobj.FEEUNIT = fsfeeunit;
                                        srcobj.JTLJ = jtlj;
                                        srcobj.JXJC = entspsf[0].JXJC;
                                        srcobj.FSJDLB = fsjdlb;
                                        //获取最低值、最高值、指导价等(小数)
                                        //数据重要，待优化
                                        string djval2 = djval.Substring(1, djval.Length - 1);
                                        string row = Regex.Split(djval2, "},")[i];//当前行
                                        string[] cells = row.Split(',');
                                        for (int j = 0; j < cells.Length; j++)
                                        {
                                            string[] cell = cells[j].Split(':');
                                            if (hl > 0) //&& hl != 1
                                            {
                                                if (cell[0].Contains("PURPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.PURPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) * hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("COSTPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.COSTPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) * hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("MINPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.MINPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) * hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("MAXPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) * hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("GUIDEPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) * hl), 5);
                                                    }
                                                }
                                                srcobj.CURRENCY = WAERS;
                                            }
                                            else if (hl < 0)
                                            {
                                                if (cell[0].Contains("PURPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.PURPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) / hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("COSTPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.COSTPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) / hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("MINPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.MINPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) / hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("MAXPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) / hl), 5);
                                                    }
                                                }
                                                else if (cell[0].Contains("GUIDEPRICE"))
                                                {
                                                    if (!string.IsNullOrEmpty(cell[1].ToString()))
                                                    {
                                                        srcobj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(cell[1].ToString()) / hl), 5);
                                                    }
                                                }
                                                srcobj.CURRENCY = WAERS;
                                            }
                                            else
                                            {
                                                if (cell[0].Contains("PURPRICE"))
                                                {
                                                    srcobj.PURPRICE = Convert.ToDecimal(cell[1].ToString());
                                                }
                                                else if (cell[0].Contains("COSTPRICE"))
                                                {
                                                    srcobj.COSTPRICE = Convert.ToDecimal(cell[1].ToString());
                                                }
                                                else if (cell[0].Contains("MINPRICE"))
                                                {
                                                    srcobj.MINPRICE = Convert.ToDecimal(cell[1].ToString());
                                                }
                                                else if (cell[0].Contains("MAXPRICE"))
                                                {
                                                    srcobj.MAXPRICE = Convert.ToDecimal(cell[1].ToString());
                                                }
                                                else if (cell[0].Contains("GUIDEPRICE"))
                                                {
                                                    srcobj.GUIDEPRICE = Convert.ToDecimal(cell[1].ToString());
                                                }
                                            }
                                        }
                                        srcobj.DoSave();
                                    }
                                    else
                                    {
                                        //if (idmindc.Count != 0)
                                        //{
                                        //    string djmin = DataHelper.QueryValue(string.Format("select MIN from SQM_MODEDJ_VAL where RID='{0}' and DJSTATUS='1'", oldsmvobj.DJRID)) + "";
                                        //    if (Convert.ToDecimal(bjminval) < Convert.ToDecimal(djmin))
                                        //    {
                                        //        minstatus = "1";//MIN变小
                                        //    }
                                        //    else if (Convert.ToDecimal(bjminval) > Convert.ToDecimal(djmin))
                                        //    {
                                        //        minstatus = "2";//MIN变大
                                        //    }
                                        //    oldsmvobj.MINBJPRICE = Convert.ToDecimal(bjminval);
                                        //}
                                        oldsmvobj.BJSTATUS = bjstatus;
                                        //oldsmvobj.CALCTYPE = bjcalctype;
                                        oldsmvobj.IFBJITEM = ifbjitems[dtDj.Rows[i]["RID"].ToString()];
                                        //oldsmvobj.BJPRICE = Convert.ToDecimal(bjpriceval);
                                        oldsmvobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                        //仓租费计算方法、类型
                                        oldsmvobj.JSFFLX = jsfflx;
                                        oldsmvobj.JSFF = jsff;
                                        oldsmvobj.FEEUNIT = fsfeeunit;

                                        oldsmvobj.FSJDLB = fsjdlb;
                                        oldsmvobj.JTLJ = jtlj;
                                        oldsmvobj.JXJC = entspsf[0].JXJC;

                                        if (hl > 0)// && hl != 1
                                        {
                                            oldsmvobj.BJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) * hl), 5);
                                            oldsmvobj.MINPRICE = Math.Round(Math.Abs(Convert.ToDecimal(minpriceval) * hl), 5);
                                            if (idmindc.Count != 0)
                                            {
                                                string djmin = DataHelper.QueryValue(string.Format("select MIN from SQM_MODEDJ_VAL where RID='{0}' and DJSTATUS='1'", oldsmvobj.DJRID)) + "";
                                                djmin = string.IsNullOrEmpty(djmin) ? "0" : djmin;
                                                if (Convert.ToDecimal(bjminval) < Convert.ToDecimal(djmin))
                                                {
                                                    minstatus = "1";//MIN变小
                                                }
                                                else if (Convert.ToDecimal(bjminval) > Convert.ToDecimal(djmin))
                                                {
                                                    minstatus = "2";//MIN变大
                                                }
                                                oldsmvobj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) * hl), 5);
                                            }
                                            if (!string.IsNullOrEmpty(maxpriceval))
                                            {
                                                oldsmvobj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(maxpriceval) * hl), 5);
                                            }
                                            if (!string.IsNullOrEmpty(guidepriceval))
                                            {
                                                oldsmvobj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(Convert.ToDecimal(guidepriceval) * hl)), 5);
                                            }
                                            oldsmvobj.CURRENCY = WAERS;
                                        }
                                        else if (hl < 0)
                                        {
                                            oldsmvobj.BJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjpriceval) / hl), 5);
                                            oldsmvobj.MINPRICE = Math.Round(Math.Abs(Convert.ToDecimal(minpriceval) / hl), 5);
                                            if (idmindc.Count != 0)
                                            {
                                                string djmin = DataHelper.QueryValue(string.Format("select MIN from SQM_MODEDJ_VAL where RID='{0}' and DJSTATUS='1'", oldsmvobj.DJRID)) + "";
                                                djmin = string.IsNullOrEmpty(djmin) ? "0" : djmin;
                                                if (Convert.ToDecimal(bjminval) < Convert.ToDecimal(djmin))
                                                {
                                                    minstatus = "1";//MIN变小
                                                }
                                                else if (Convert.ToDecimal(bjminval) > Convert.ToDecimal(djmin))
                                                {
                                                    minstatus = "2";//MIN变大
                                                }
                                                oldsmvobj.MINBJPRICE = Math.Round(Math.Abs(Convert.ToDecimal(bjminval) / hl), 5);
                                            }
                                            if (!string.IsNullOrEmpty(maxpriceval))
                                            {
                                                oldsmvobj.MAXPRICE = Math.Round(Math.Abs(Convert.ToDecimal(maxpriceval) / hl), 5);
                                            }
                                            if (!string.IsNullOrEmpty(guidepriceval))
                                            {
                                                oldsmvobj.GUIDEPRICE = Math.Round(Math.Abs(Convert.ToDecimal(Convert.ToDecimal(guidepriceval) / hl)), 5);
                                            }
                                            oldsmvobj.CURRENCY = WAERS;
                                        }
                                        oldsmvobj.MINSTATUS = minstatus;
                                        oldsmvobj.DoUpdate();
                                    }
                                }
                            }
                        }
                    }
                    //更新定价方式下的MIN值状态
                    //if (bjchoose == "true")
                    //{
                    //    //DataHelper.ExecSql("update SQM_MODEBJ_VAL set MINSTATUS='" + minstatus + "', MINBJPRICE='" + minval + "' where FEECALCID='" + bjrid + "' and DJFSRID='" + djfsrid + "'");
                    //    DataHelper.ExecSql("update SQM_MODEBJ_VAL set MINSTATUS='" + minstatus + "' where FEECALCID='" + bjrid + "' and DJFSRID='" + djfsrid + "'");
                    //}
                    //return Content(new JsonMessage { Message = message, Success = true }.ToString());
                    //if (ifall == "1" || ifall == "2") //全部保存
                    //{
                    //    //其他方式定价信息存入报价值表
                    //    if (!String.IsNullOrEmpty(djfsrid))
                    //    {
                    //        if (gdzkey == "0")
                    //        {
                    //            sql = @"select * from SQM_MODEDJ_VAL where FEECALCID='" + djrid + "' and DJFSRID<>'" + djfsrid + "' and DJSTATUS='1'";
                    //        }
                    //        else
                    //        {
                    //            sql = @"select * from SQM_MODEDJ_VAL where FEECALCID='" + djrid + "' and (GDZRID<>'" + gdzrid + "' or GDZRID is null) and DJSTATUS='1'";
                    //        }
                    //        DataTable qtdjfsdt = DataHelper.QueryDataTable(sql);
                    //        int i = 0;
                    //        foreach (DataRow qtdjdr in qtdjfsdt.Rows)
                    //        {
                    //            SaveBJZB(qtdjdr, bjrid, ifall, i);
                    //            i++;
                    //        }
                    //    }
                    //}
                    //if (ifall == "2" || (ifall == "0" && djqtfs == "False" && sign == "2")) //全部确认
                    //{
                    //    DataHelper.ExecSql("update SQM_MODEBJ_VAL set BJSTATUS='2' where FEECALCID='" + bjrid + "' and CALCUNIT='" + calcunit + "' and BJSTATUS='1'");
                    //    DataHelper.ExecSql("update SQM_MODEBJ_VAL set BJSTATUS='5' where FEECALCID='" + bjrid + "' and CALCUNIT='" + calcunit + "' and BJSTATUS='4'");
                    //}
                }
                else
                {
                    //at cost及单票单询的将普通报价数据置为失效
                    DataHelper.ExecSql("update SQM_MODEBJ_VAL set STATUS='0' where FEECALCID='" + bjrid + "'");
                }
                // 1.将dtpsf更新sqm_bj_psf 只有一行，所以直接更新
                SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(bjrid);
                string oldsta = sbp.BJSTATAUS;
                sbp.BJFS = entspsf[0].BJFS;
                sbp.CONDITION = entspsf[0].CONDITION;
                sbp.DISCOUNT = entspsf[0].DISCOUNT;
                sbp.ISLSC = entspsf[0].ISLSC;
                if (String.IsNullOrEmpty(entspsf[0].ISLSC))
                {
                    DataHelper.ExecSql("delete from SQM_BJ_PSF where MRID='" + mrid + "' and BGFZRID='" + bjrid + "'");
                    sbp.BGFZRID = "";
                }
                //普通报价不存
                //if(entspsf[0].BJFS != "0")
                //{
                sbp.JXJC = entspsf[0].JXJC;
                sbp.STAGETYPE = entspsf[0].STAGETYPE;
                //}
                sbp.JSF = entspsf[0].JSF;
                sbp.JSFCODE = entspsf[0].JSFCODE;
                sbp.JSFJS = entspsf[0].JSFJS;
                sbp.JSFJSCODE = entspsf[0].JSFJSCODE;
                sbp.BJSTARTDATE = entspsf[0].BJSTARTDATE;
                sbp.BJENDDATE = entspsf[0].BJENDDATE;
                sbp.OTHER_NAME = entspsf[0].OTHER_NAME;
                if (sbp.ISCOPY == "1")
                {
                    sbp.FEE_NAME = entspsf[0].OTHER_NAME;
                }                
                sbp.OTHER_NAME_EN = entspsf[0].OTHER_NAME_EN;//英文费目别名
                if (entspsf[0].BJFS == "1" || entspsf[0].BJFS == "2")
                {
                    sbp.BJSTATAUS = sign;
                    sbp.MINSTATUS = minstatus;
                }
                else if (hasbjitem == "1" || oldsta != "0")
                {
                    //判断当前定价方式下的保存状态
                    string[] bjstatusarr = new string[0];
                    string[] minstatusarr = new string[0];
                    List<string> bjstalist = bjstatusarr.ToList();
                    List<string> minstalist = minstatusarr.ToList();
                    //if (!String.IsNullOrEmpty(gdzrid))
                    //{
                    //    sql = string.Format("select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='{0}' and GDZRID='{1}'", bjrid, gdzrid);
                    //}
                    //else if (!String.IsNullOrEmpty(djfsrid))
                    //{
                    //    sql = string.Format("select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='{0}' and DJFSRID='{1}'", bjrid, djfsrid);
                    //}
                    //else
                    //{
                    //    sql = string.Format("select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='{0}' and DJFSRID is null", bjrid);
                    //}
                    //判断这份报价里面明细的状态
                    sql = string.Format("select distinct BJSTATUS,MINSTATUS from SQM_MODEBJ_VAL where FEECALCID='{0}'", bjrid);
                    DataTable bjsta = DataHelper.QueryDataTable(sql);
                    if (bjsta.Rows.Count == 0)
                    {
                        //if (bjstatus == "mb")
                        //{
                        //    sbp.BJSTATAUS = "1";
                        //}
                        //else
                        //{
                        //    sbp.BJSTATAUS = "0";//0-未保存
                        //}
                        sbp.BJSTATAUS = "0";//0-未保存
                        sbp.MINSTATUS = minstatus;
                    }
                    else
                    {
                        foreach (DataRow bjstadr in bjsta.Rows)
                        {
                            if (!bjstalist.Contains(bjstadr["BJSTATUS"]))
                            {
                                bjstalist.Add(bjstadr["BJSTATUS"].ToString());
                            }
                            if (!minstalist.Contains(bjstadr["MINSTATUS"]))
                            {
                                minstalist.Add(bjstadr["MINSTATUS"].ToString());
                            }
                        }
                    }
                    if (sign == "1" && bjstalist.Count > 0)
                    {
                        bjstatusarr = bjstalist.ToArray();
                        if (bjstatusarr.Contains("3"))
                        {
                            sbp.BJSTATAUS = "3";//3-无定价报价（已保存）
                        }
                        else if (bjstatusarr.Contains("4"))
                        {
                            sbp.BJSTATAUS = "4";//4-报价超限（已保存）
                        }
                        else
                        {
                            sbp.BJSTATAUS = "1";//1-已保存
                        }
                    }
                    else if (sign == "2" && bjstalist.Count > 0)
                    {
                        bjstatusarr = bjstalist.ToArray();
                        if (bjstatusarr.Contains("1"))
                        {
                            sbp.BJSTATAUS = "1";//1-已保存
                        }
                        else if (bjstatusarr.Contains("5"))
                        {
                            sbp.BJSTATAUS = "5";//5-报价超限（已确认）
                        }
                        else if (bjstatusarr.Contains("3"))
                        {
                            sbp.BJSTATAUS = "7";//7-无报价（已确认）
                        }
                        else
                        {
                            sbp.BJSTATAUS = "2";//2-已确认
                        }
                    }
                    //报价MIN值状态判断
                    minstatusarr = minstalist.ToArray();
                    if (minstatusarr.Contains("1"))
                    {
                        sbp.MINSTATUS = "1";//报价MIN小于定价MIN
                    }
                    else if (minstatusarr.Contains("2"))
                    {
                        sbp.MINSTATUS = "2";//报价MIN大于定价MIN
                    }
                    else
                    {
                        sbp.MINSTATUS = "0";//报价MIN等于定价MIN
                    }

                    #region
                    //sql = @"select distinct DJFSRID from SQM_MODEDJ_VAL t where FEECALCID='" + djrid + "' and DJSTATUS='1'";
                    //DataTable djfsriddt = DataHelper.QueryDataTable(sql);
                    //if (djfsriddt.Rows[0]["DJFSRID"].ToString() != "")
                    //{
                    //    bool ztpd = true;
                    //    string[] bjstatusarr = new string[0];
                    //    List<string> bjstalist = bjstatusarr.ToList();
                    //    foreach (DataRow dr in djfsriddt.Rows)
                    //    {
                    //        sql = @"select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='" + bjrid + "' and DJFSRID='" + dr["DJFSRID"] + "' order by BJSTATUS asc";
                    //        DataTable bjsta = DataHelper.QueryDataTable(sql);
                    //        if (bjsta.Rows.Count == 0)
                    //        {
                    //            sbp.BJSTATAUS = "0";//0-未保存
                    //            ztpd = false;
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            foreach (DataRow bjstadr in bjsta.Rows)
                    //            {
                    //                if (!bjstalist.Contains(bjstadr["BJSTATUS"]))
                    //                {
                    //                    bjstalist.Add(bjstadr["BJSTATUS"].ToString());
                    //                }
                    //            }
                    //        }
                    //    }
                    //    if (sign == "1" && ztpd)
                    //    {
                    //        bjstatusarr = bjstalist.ToArray();
                    //        if (bjstatusarr.Contains("0"))//0-未保存
                    //        {
                    //            sbp.BJSTATAUS = oldsta;
                    //        }
                    //        else if (bjstatusarr.Contains("3"))
                    //        {
                    //            sbp.BJSTATAUS = "3";//3-无定价报价（已保存）
                    //        }
                    //        else if (bjstatusarr.Contains("4"))
                    //        {
                    //            sbp.BJSTATAUS = "4";//4-报价超限（已保存）
                    //        }
                    //        else
                    //        {
                    //            sbp.BJSTATAUS = bjstatus;//1-已保存
                    //        }
                    //    }
                    //    else if (sign == "2" && ztpd)
                    //    {
                    //        bjstatusarr = bjstalist.ToArray();
                    //        if (bjstatusarr.Contains("0") || bjstatusarr.Contains("1") || bjstatusarr.Contains("3") || bjstatusarr.Contains("4"))
                    //        {
                    //            sbp.BJSTATAUS = oldsta;//还有未确认的其他定价方式
                    //        }
                    //        else if (bjstatusarr.Contains("5"))
                    //        {
                    //            sbp.BJSTATAUS = "5";//5-报价超限（已确认）
                    //        }
                    //        else
                    //        {
                    //            sbp.BJSTATAUS = "2";//2-已确认
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    sql = @"select distinct BJSTATUS from SQM_MODEBJ_VAL where FEECALCID='" + bjrid + "' order by BJSTATUS asc";
                    //    DataTable bjsta = DataHelper.QueryDataTable(sql);
                    //    if (bjsta.Rows.Count == 0)
                    //    {
                    //        sbp.BJSTATAUS = "0";//0-未保存
                    //    }
                    //    else
                    //    {
                    //        foreach (DataRow bjstadr in bjsta.Rows)
                    //        {
                    //            if (bjstadr["BJSTATUS"].ToString() == "0")
                    //            {
                    //                sbp.BJSTATAUS = "0";//0-未保存
                    //                break;
                    //            }
                    //            else if (sign == "1")
                    //            {
                    //                if (bjstadr["BJSTATUS"].ToString() == "4")
                    //                {
                    //                    sbp.BJSTATAUS = "4";//4-报价超限（已保存）
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    sbp.BJSTATAUS = bjstatus;//1-已保存
                    //                }
                    //            }
                    //            else if (sign == "2")
                    //            {
                    //                if (bjstadr["BJSTATUS"].ToString() == "5")
                    //                {
                    //                    sbp.BJSTATAUS = "5";//5-报价超限（已确认）
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    sbp.BJSTATAUS = "2";//2-已确认
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                }
                sbp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                sbp.BJTCURR = WAERS;
                sbp.DoUpdate();
                //更新包干费的信息
                if (entspsf[0].ISLSC == "1")
                {
                    SQM_BJ_PSF bgfzsbp = SQM_BJ_PSF.Find(bjrid);
                    SQM_BJ_PSF[] bgfsbparr = SQM_BJ_PSF.FindAllByProperties(SQM_BJ_PSF.Prop_BGFZRID, bjrid);
                    foreach (SQM_BJ_PSF bgfsbp in bgfsbparr)
                    {
                        bgfsbp.BJFS = bgfzsbp.BJFS;
                        bgfsbp.DISCOUNT = bgfzsbp.DISCOUNT;
                        bgfsbp.STAGETYPE = bgfzsbp.STAGETYPE;
                        bgfsbp.BJSTATAUS = bgfzsbp.BJSTATAUS;
                        bgfsbp.JXJC = bgfzsbp.JXJC;
                        bgfsbp.MINSTATUS = bgfzsbp.MINSTATUS;
                        bgfsbp.DoUpdate();
                    }
                }
                // 3.复制到其他费目
                // 得到值表code字段（值非空）
                //string vrid = DataHelper.QueryValue("select vrid from sqm_bj_psf where rid = '" + bjrid + "'") + "";
                string vrid = sbp.VRID;
                // 复制费目是否存在包干费
                DataTable dtbgffz = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + bjrid + "' and (status <> '0' or status is null)");

                string nowtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string columnName = DataHelper.QueryValue("select wm_concat(column_name) as columnname from user_tab_columns where table_name = 'SQM_MODEBJ_VAL' and column_name like 'COLUMN%' and column_name like '%C' and num_distinct > 0 order by column_id") + "";
                if (ifcopy != "no")
                {
                    string[] feeids = JsonHelper.GetObject<string[]>(ifcopy);// 需要被复制产品服务费目psf表rid
                    for (int i = 0; i < feeids.Length; i++)
                    {
                        string rid = feeids[i] + "";
                        // 被复制费目是否存在包干费，如果存在，删除
                        DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + rid + "' and (status <> '0' or status is null)");
                        if (dtbgf.Rows.Count > 0)//
                        {
                            string part1 = "begin ";
                            string part2 = " end;";
                            string sql_delete = "";
                            foreach (DataRow dr in dtbgf.Rows)
                            {
                                string deleterid = dr["RID"] + "";
                                sql_delete += "delete from sqm_bj_psf where rid = '" + deleterid + "';";
                            }
                            DataHelper.ExecSql(part1 + sql_delete + part2);
                        }
                        // 复制费目是否存在包干费，如果存在，复制
                        string islsc = "";
                        if (dtbgffz.Rows.Count > 0)
                        {
                            islsc = "1";
                            string part1 = "begin ";
                            string part2 = " end;";
                            foreach (DataRow dr in dtbgffz.Rows)
                            {
                                string insert = "";
                                string value = "";
                                string bgfrid = Guid.NewGuid().ToString();
                                foreach (DataColumn column in dtbgf.Columns)
                                {
                                    string colName = column.ColumnName + "";
                                    insert += colName + ",";
                                    if (colName == "CREATETIME" || colName == "MODIFYTIME")
                                    {
                                        value += "to_date('" + dr[colName] + "','yyyy/mm/dd hh24:mi:ss'),";
                                    }
                                    else if (colName == "BJSTARTDATE" || colName == "BJENDDATE")
                                    {
                                        if (dr[colName] + "" != "")
                                        {
                                            value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                                        }
                                        else
                                        {
                                            value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                        }
                                    }
                                    else if (colName == "BGFZRID")
                                    {
                                        value += "'" + rid + "',";
                                    }
                                    else if (colName == "RID")
                                    {
                                        value += "'" + bgfrid + "',";
                                    }
                                    else if (colName == "MRID")
                                    {
                                        value += "'" + mrid + "',";
                                    }
                                    else if (colName == "VRID")
                                    {
                                        value += "'" + vrid + "',";
                                    }
                                    else if (colName == "STATUS")
                                    {
                                        value += "'1',";
                                    }
                                    else if (colName == "ISLSC")
                                    {
                                        value += "'1',";
                                    }
                                    else
                                    {
                                        value += "'" + dr[colName] + "',";
                                    }
                                }
                                part1 += "insert into sqm_bj_psf(" + insert.TrimEnd(',') + ") values(" + value.TrimEnd(',') + ");";
                            }
                            DataHelper.ExecSql(part1 + part2);
                        }
                        // 更新psf 表
                        SQM_BJ_PSF sbfobj = SQM_BJ_PSF.Find(rid);
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
                                    else if (p.Name == "ISLSC")
                                    {
                                        sbfobj.SetValue(p.Name, islsc);
                                    }
                                    else if (p.Name == "BGFZRID")
                                    {
                                        if (islsc == "1")
                                        {
                                            sbfobj.SetValue(p.Name, "1");
                                        }
                                    }
                                    else if (p.Name == "BJSTARTDATE" || p.Name == "BJENDDATE")
                                    {
                                        if (dtpsf.Rows[0][col.ColumnName] + "" != "")
                                        {
                                            sbfobj.SetValue(p.Name, Convert.ToDateTime(dtpsf.Rows[0][col.ColumnName]));
                                        }
                                    }
                                    else
                                    {
                                        sbfobj.SetValue(p.Name, dtpsf.Rows[0][col.ColumnName]);
                                    }
                                }
                            }
                        }
                        sbfobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbfobj.DoUpdate();
                        // 更新值表  
                        Dictionary<string, string> riddic = new Dictionary<string, string>();
                        // 思路：
                        // 已保存数据：遍历原数据，与新数据进行匹配（code）
                        // 未保存数据：遍历定价值表数据，与新数据进行匹配（code）
                        // 能匹配数据，只更新原数据的报价字段/MIN字段
                        // 不能匹配数据，1.将新数据feecalcid置为psfrid  2.将新数据djrid置为dj psf rid  3.将新数据最低价/最高价/指导价置空
                        // 得新数据值表数据
                        DataTable dtnew = DataHelper.QueryDataTable("select rid,bjprice,MINBJPRICE," + columnName + ",DJFSRID,GDZRID from sqm_modebj_val where feecalcid = '" + bjrid + "' and (status <> '0' or status is null)");
                        dtnew.Columns.Add("IFNOTSAVE");// 判断是否是未保存数据
                        DataTable dtoriginal = new DataTable();
                        DataTable dtalldata = new DataTable();
                        string oldbjstatus = DataHelper.QueryValue("select bjstataus from sqm_bj_psf where rid = '" + feeids[i] + "'") + "";
                        if (oldbjstatus == "0")// 未保存
                        {
                            // 得到定价值表数据
                            string djpsfrid = GetDJpsfrid(feeids[i]);
                            dtoriginal = DataHelper.QueryDataTable("select rid," + columnName + ",DJFSRID,GDZRID from sqm_modedj_val where feecalcid = '" + djpsfrid + "' and (status <> '0' or status is null) and djstatus = '1'");// 比较值
                            dtalldata = DataHelper.QueryDataTable("select * from sqm_modedj_val where feecalcid = '" + djpsfrid + "' and (status <> '0' or status is null) and djstatus = '1'");// 定价全部数据
                        }
                        else
                        {
                            // 得到原数据报价值表数据
                            dtoriginal = DataHelper.QueryDataTable("select rid," + columnName + ",DJFSRID,GDZRID from sqm_modebj_val where feecalcid = '" + feeids[i] + "' and (status <> '0' or status is null)");
                        }
                        // 数据匹配
                        MatchData(nowtime, oldbjstatus, dtnew, dtoriginal, riddic, dtalldata);
                        // 得到未匹配的新数据rid  匹配未保存的rid
                        string inrids = "";
                        string inrids2 = "";
                        if (dtnew.Rows.Count > 0)
                        {
                            foreach (DataRow drrid in dtnew.Rows)
                            {
                                if (drrid["IFNOTSAVE"] + "" != "1")// 未匹配
                                {
                                    inrids += "'" + drrid["RID"] + "',";
                                }
                                else
                                {
                                    inrids2 += "'" + drrid["RID"] + "',";
                                }
                            }
                        }
                        inrids = inrids.TrimEnd(',');
                        inrids2 = inrids2.TrimEnd(',');
                        if (inrids != "")
                        {
                            bjstatus = "3";// 无定价报价
                            DataTable dtnewdata = DataHelper.QueryDataTable(string.Format("select * from sqm_modebj_val where rid in ({0})", inrids));
                            if (dtnewdata.Rows.Count > 0)
                            {
                                foreach (DataRow drdata in dtnewdata.Rows)
                                {
                                    // 最低/最高/指导价置空
                                    drdata["MAXPRICE"] = DBNull.Value;
                                    drdata["MINPRICE"] = DBNull.Value;
                                    drdata["GUIDEPRICE"] = DBNull.Value;
                                    // feecalcid置为原数据psfrid
                                    drdata["FEECALCID"] = feeids[i];
                                    // djrid置为dj psfrid 先根据报价psf表rid查找dj psf表rid
                                    string djpsfrid = GetDJpsfrid(feeids[i]);
                                    drdata["DJRID"] = djpsfrid;
                                }
                                List<SQM_MODEBJ_VAL> listobj = TableToEntity<SQM_MODEBJ_VAL>(dtnewdata);
                                foreach (SQM_MODEBJ_VAL obj in listobj)
                                {
                                    obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                    obj.DoCreate();// 会自动生成rid
                                }
                            }
                        }
                        if (inrids2 != "")
                        {
                            DataTable dtnewdata2 = DataHelper.QueryDataTable(string.Format("select * from sqm_modebj_val where rid in ({0})", inrids2));
                            if (dtnewdata2.Rows.Count > 0)
                            {
                                foreach (DataRow drdata in dtnewdata2.Rows)
                                {
                                    // feecalcid置为原数据psfrid
                                    drdata["FEECALCID"] = feeids[i];
                                    string djvalrid = riddic[drdata["RID"] + ""];
                                    drdata["DJRID"] = djvalrid;
                                    drdata["MAXPRICE"] = GetDJPrice(dtalldata, djvalrid)[0];
                                    drdata["MINPRICE"] = GetDJPrice(dtalldata, djvalrid)[1];
                                    drdata["GUIDEPRICE"] = GetDJPrice(dtalldata, djvalrid)[2];
                                }
                            }
                            List<SQM_MODEBJ_VAL> listobj = TableToEntity<SQM_MODEBJ_VAL>(dtnewdata2);
                            foreach (SQM_MODEBJ_VAL obj in listobj)
                            {
                                obj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                obj.DoCreate();// 会自动生成rid
                            }
                        }

                        // 继续更新psf表，改变费目状态
                        bjstatus = GetBjStatus(feeids[i], sign);
                        sbfobj.BJSTATAUS = bjstatus;
                        sbfobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbfobj.DoUpdate();


                    }
                }
                //增加币种
                //entspsf[0].RID

                //var mainobj = SQM_BJ_MAIN_BASIC.TryFind(entspsf[0].MRID);
                //mainobj.BJTCURR = WAERS;
                //mainobj.DoUpdate();
                return Content(new JsonMessage { Message = message, Success = true }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "操作失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 根据定价值表rid获取最高价/最低价/指导价   
        /// </summary>
        /// <param name="djvalrid"></param>
        /// <returns></returns>
        private List<string> GetDJPrice(DataTable dt, string djvalrid)
        {
            List<string> djprice = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RID"] + "" == djvalrid)
                {
                    djprice.Add(dr["MAXPRICE"] + "");
                    djprice.Add(dr["MINPRICE"] + "");
                    djprice.Add(dr["GUIDEPRICE"] + "");
                }
            }
            return djprice;
        }

        /// <summary>
        /// 判断复制时的报价状态
        /// </summary>
        /// <param name="feecalcid"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private string GetBjStatus(string feecalcid, string sign)
        {
            DataTable bjval = DataHelper.QueryDataTable("select * from sqm_modebj_val where feecalcid = '" + feecalcid + "' and (status <> '0' or status is null)");
            string bjstatus = "0";
            foreach (DataRow row in bjval.Rows)
            {
                if ((row["MINPRICE"] + "") == "" && (row["MAXPRICE"] + "" == "") && (row["GUIDEPRICE"] + "" == ""))
                {
                    bjstatus = "3";
                    break;
                }
                else
                {
                    Decimal minPrice = Convert.ToDecimal(row["MINPRICE"] + "");
                    Decimal maxPrice = Convert.ToDecimal(row["MAXPRICE"] + "");
                    Decimal guidePrice = Convert.ToDecimal(row["GUIDEPRICE"] + "");
                    Decimal bjPrice = Convert.ToDecimal(row["BJPRICE"] + "");
                    if (bjPrice < minPrice || bjPrice > maxPrice)
                    {
                        if (sign == "1")
                        {
                            bjstatus = "4";// 已保存点保存-> 已确认（报价超限）
                        }
                        else if (sign == "2")
                        {
                            bjstatus = "5";// 已保存点确认-> 已确认（报价超限）
                        }
                        break;
                    }
                }
            }
            if (bjstatus == "0")
            {
                if (sign == "1")
                {
                    bjstatus = "1";// 已保存
                }
                else if (sign == "2")
                {
                    bjstatus = "2";// 已确认
                }
            }
            return bjstatus;
        }

        /// <summary>
        /// 匹配值表数据进行数据复制操作
        /// </summary>
        /// <param name="nowtime"></param>
        /// <param name="dtnew"></param>
        /// <param name="dtoriginal"></param>
        private static void MatchData(string nowtime, string oldbjstatus, DataTable dtnew, DataTable dtoriginal, Dictionary<string, string> riddic, DataTable dtalldata)
        {
            if (dtoriginal.Rows.Count > 0)
            {
                int count1 = 0;// 遍历次数，也是值表数据行数
                int count2 = 0;// 失效次数，即未匹配成功行数
                // 遍历被复制费目的值表（定价值表或报价值表）
                foreach (DataRow drori in dtoriginal.Rows)
                {
                    count1++;
                    string valueo = "";
                    string rid = drori["RID"] + "";
                    bool isfit = false;
                    // 拼“比较值”，与当前费目值表数据进行比较来进行匹配
                    foreach (DataColumn dcori in dtoriginal.Columns)
                    {
                        if (dcori.ColumnName != "RID")
                        {
                            valueo += drori[dcori] + "|";
                        }
                    }
                    if (dtnew.Rows.Count > 0)
                    {
                        // 倒序遍历当前费目值表（新数据），存在匹配项：1.更新被复制值表的报价/最低报价/修改人/修改时间 2.删掉匹配成功的当前费目值表数据  
                        // 若匹配失败，将被匹配值表数据失效
                        for (int rows = dtnew.Rows.Count - 1; rows >= 0; rows--)
                        {
                            string valuen = "";
                            foreach (DataColumn dcnew in dtnew.Columns)
                            {
                                if (dcnew.ColumnName != "RID" && dcnew.ColumnName != "BJPRICE" && dcnew.ColumnName != "MINBJPRICE" && dcnew.ColumnName != "IFNOTSAVE")
                                {
                                    valuen += dtnew.Rows[rows][dcnew] + "|";
                                }
                            }
                            // 匹配code 
                            if (valueo == valuen)// 匹配成功
                            {
                                isfit = true;
                                if (oldbjstatus == "0")// 未保存
                                {
                                    dtnew.Rows[rows]["IFNOTSAVE"] = "1";
                                    riddic[dtnew.Rows[rows]["RID"] + ""] = rid;// 定价值表rid存入报价值表djrid中，这里先存起来对应关系，在插数时使用
                                }
                                else
                                {
                                    DataHelper.ExecSql("update sqm_modebj_val set BJPRICE = '" + dtnew.Rows[rows]["BJPRICE"] + "',MINBJPRICE = '" + dtnew.Rows[rows]["MINBJPRICE"] + "',MODIFYUSER = '" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "',MODIFYTIME = to_date('" + nowtime + "','yyyy-mm-dd hh24:mi:ss') where rid = '" + rid + "'");
                                    dtnew.Rows.RemoveAt(rows);// 匹配成功删除该匹配新数据
                                }
                                break;
                            }
                        }
                    }
                    if (!isfit)// 匹配失败 置为失效
                    {
                        count2++;
                        DataHelper.ExecSql("update sqm_modebj_val set status = '0',MODIFYUSER = '" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "' where rid = '" + rid + "'");
                    }
                }
                if (count1 == count2)// 全部未匹配成功
                {

                }
            }
        }

        /// <summary>
        /// 通过报价psf表rid 获取 定价psf表rid
        /// </summary>
        /// <param name="bjpsfrid"></param>
        /// <returns></returns>
        private string GetDJpsfrid(string bjpsfrid)
        {
            DataTable dt = DataHelper.QueryDataTable("select fee_code,service_code,product_code from sqm_bj_psf where rid = '" + bjpsfrid + "'");
            string djpsfrid = "";
            if (dt.Rows.Count > 0)
            {
                djpsfrid = DataHelper.QueryValue("select rid from sqm_dj_psf where feecode = '" + dt.Rows[0]["FEE_CODE"] + "' and srvcode = '" + dt.Rows[0]["SERVICE_CODE"] + "' and prdcode = '" + dt.Rows[0]["PRODUCT_CODE"] + "'") + "";
            }
            return djpsfrid;
        }

        public void SaveBJZB(DataRow qtdjdr, string bjrid, string ifall, int i)
        {
            try
            {
                SQM_MODEBJ_VAL smbj = new SQM_MODEBJ_VAL();
                SQM_MODEBJ_VAL oldsmvobjqt = SQM_MODEBJ_VAL.FindFirstByProperties(SQM_MODEBJ_VAL.Prop_DJRID, qtdjdr["RID"].ToString(), SQM_MODEBJ_VAL.Prop_FEECALCID, bjrid);
                decimal maxprice = Convert.ToDecimal(qtdjdr["MAXPRICE"].ToString());
                decimal minprice = Convert.ToDecimal(qtdjdr["MINPRICE"].ToString());
                if (oldsmvobjqt == null)
                {
                    smbj = TableToEntity<SQM_MODEBJ_VAL>(qtdjdr.Table)[i];
                    decimal guideprice = Convert.ToDecimal(qtdjdr["GUIDEPRICE"].ToString());
                    if (ifall == "1")
                    {
                        if (guideprice < minprice || guideprice > maxprice)
                        {
                            smbj.BJSTATUS = "4";
                        }
                        else
                        {
                            smbj.BJSTATUS = "1";
                        }
                    }
                    else
                    {
                        if (guideprice < minprice || guideprice > maxprice)
                        {
                            smbj.BJSTATUS = "5";
                        }
                        else
                        {
                            smbj.BJSTATUS = "2";
                        }
                    }
                    smbj.STATUS = "1";
                    smbj.SORD = 0;
                    smbj.DJRID = qtdjdr["RID"].ToString();
                    smbj.BJPRICE = Convert.ToDecimal(qtdjdr["GUIDEPRICE"].ToString());
                    smbj.MINBJPRICE = Convert.ToDecimal(qtdjdr["GUIDEPRICE"].ToString());
                    smbj.FEECALCID = bjrid;
                    smbj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    smbj.DoCreate();
                }
                else
                {
                    smbj = SQM_MODEBJ_VAL.FindFirstByProperties(SQM_MODEBJ_VAL.Prop_DJRID, qtdjdr["RID"], SQM_MODEBJ_VAL.Prop_FEECALCID, bjrid);
                    if (ifall == "1")
                    {
                        if (smbj.BJPRICE < minprice || smbj.BJPRICE > maxprice)
                        {
                            smbj.BJSTATUS = "4";
                        }
                        else
                        {
                            smbj.BJSTATUS = "1";
                        }
                    }
                    else
                    {
                        if (smbj.BJPRICE < minprice || smbj.BJPRICE > maxprice)
                        {
                            smbj.BJSTATUS = "5";
                        }
                        else
                        {
                            smbj.BJSTATUS = "2";
                        }
                    }
                    smbj.DoUpdate();
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExportExcel()
        {
            string djrid = Request["djrid"];
            string bjrid = Request["bjrid"];
            string status = Request["status"];
            string feecode = Request["feecode"];
            string feename = Request["feename"];
            string filePath = "";
            string fileName = "";
            if (string.IsNullOrEmpty(bjrid))
            {
                return Content(new JsonMessage { Message = "Excel导出失败：获取报价信息失败！" }.ToString());
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
                                if (status == "0")
                                {
                                    sql_search = SearchSqlAll(djrid, feecode, "0", djfsrid, gdzrid);
                                }
                                else
                                {
                                    sql_search = SearchSqlAll(bjrid, feecode, "1", djfsrid, gdzrid);
                                }
                                if (!String.IsNullOrEmpty(sql_search))
                                {
                                    CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                                }
                            }
                        }
                        else // 无高低值
                        {
                            if (status == "0")
                            {
                                sql_search = SearchSqlAll(djrid, feecode, "0", djfsrid, "");
                            }
                            else
                            {
                                sql_search = SearchSqlAll(bjrid, feecode, "1", djfsrid, "");
                            }
                            if (!String.IsNullOrEmpty(sql_search))
                            {
                                CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                            }
                        }
                    }
                }
                else// 多报价方式：否
                {
                    if (status == "0")
                    {
                        sql_search = SearchSqlAll(djrid, feecode, "0", "", "");
                    }
                    else
                    {
                        sql_search = SearchSqlAll(bjrid, feecode, "1", "", "");
                    }
                    if (!String.IsNullOrEmpty(sql_search))
                    {
                        CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search);
                    }
                }
                // 列宽自适应
                worksheet.AutoFitColumns();
                // 隐藏列
                worksheet.Cells.HideColumns(0, 4);

                // 生成Excel文件
                //fileName = feename + "_报价(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + ".xlsx";
                //改 18-8-14  yang  更改费目导出时显示的文件名
                if (string.IsNullOrEmpty(Request["currentbj"]))
                {
                    fileName = feename + "_报价(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + ".xlsx";
                }
                else
                {
                    fileName = feename + "_" + Request["currentbj"] + ".xlsx";
                }
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
        public string SearchSqlAll(string feeid, string feecode, string status, string djfsrid, string gdzrid)
        {
            string sql_val = "";
            string minprice = "";
            DataTable dt = new DataTable();
            string sql_ref = "";
            if (djfsrid == "")// 是否多报价方式：否
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID = '' or DJFSRID is null)";
                sql_val = "select 0,1";
            }
            else if (djfsrid != "" && gdzrid == "")// 是否多报价方式：是，无高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and DJFSRID = '" + djfsrid + "'";
                sql_val = "select '" + djfsrid + "',1";
            }
            else if (djfsrid != "" && gdzrid != "")// 是否多报价方式：是，存在高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and GDZRID = '" + gdzrid + "'";
                sql_val = "select '" + djfsrid + "','" + gdzrid + "'";
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
            if (status == "0")// 查询定价值表
            {
                sql_val += ",RID,CURRENCY";
                if (djfsrid == "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MIN";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEDJ_VAL where STATUS <> '0' and DJSTATUS='1' and FEECALCID = '" + feeid + "'";
                }
                else if (djfsrid != "" && gdzrid == "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MIN";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEDJ_VAL where STATUS <> '0' and DJSTATUS='1' and FEECALCID = '" + feeid + "' and DJFSRID ='" + djfsrid + "'";
                }
                else if (djfsrid != "" && gdzrid != "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MIN";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEDJ_VAL where STATUS <> '0' and DJSTATUS='1' and FEECALCID = '" + feeid + "' and GDZRID ='" + gdzrid + "'";
                }
            }
            else// 查询报价值表
            {
                sql_val += ",DJRID as RID,CURRENCY";
                if (djfsrid == "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MINBJPRICE";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEBJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "'";
                }
                else if (djfsrid != "" && gdzrid == "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MINBJPRICE";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEBJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "' and DJFSRID ='" + djfsrid + "'";
                }
                else if (djfsrid != "" && gdzrid != "")
                {
                    //MIN判断
                    minprice = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where FEECODE='" + feecode + "' and DJFSRID ='" + djfsrid + "'") + "";
                    if (minprice == "1")
                    {
                        sql_val += ",MINBJPRICE";
                    }
                    sql_val += @",to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MAXPRICE,MINPRICE,GUIDEPRICE,
                    CALCUNIT,CALCTYPE,DJFSRID,GDZRID,BJPRICE from SQM_MODEBJ_VAL where STATUS <> '0' and FEECALCID = '" + feeid + "' and GDZRID ='" + gdzrid + "'";
                }
            }
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
                    else if (dtcol.ColumnName == "RID")
                    {
                        cells[rowIndex, colIndex].PutValue("定价标记");
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
                    else if (dtcol.ColumnName == "BJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("报价");
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
                    else if (dtcol.ColumnName == "MINBJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最低报价");
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
                        else
                        {
                            cells[rowIndex, colIndex].PutValue(drDetail[dtcol.ColumnName].ToString());
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        }
                        colIndex++;
                    }
                    rowIndex++;
                }
            }
        }
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
                    else if (dtcol.ColumnName == "RID")
                    {
                        cells[rowIndex, colIndex].PutValue("定价标记");
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
                    else if (dtcol.ColumnName == "BJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("报价");
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
                    else if (dtcol.ColumnName == "MINBJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最低报价");
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
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostExcelData()
        {
            string bjrid = Request["bjrid"];
            string status = Request["status"];
            List<string> colname = new List<string>();
            List<string> names = new List<string>();
            List<DataSet> listDs = new List<DataSet>();

            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0 || listDs.Count > 0)
                {
                    ArrayList al = new ArrayList();
                    if (files.Count > 0)
                    {
                        // 获取文件集合中的第一个文件(每次只上传一个文件)
                        HttpPostedFile file = files[0];
                        System.IO.Stream stream = file.InputStream;
                        al = GetDataFromExcel2(stream);
                    }
                    if (listDs.Count == 0)
                    {
                        listDs = (List<DataSet>)al[0];
                    }
                    // 主数据校验 入库时计算基础如果没有code值，自动带出code，并将code放入值表的columnc列。
                    DataSet ds = listDs[0];
                    string feeName = "";
                    string feeCode = "";
                    if (ds.DataSetName.IndexOf('|') >= 0)
                    {
                        feeName = ds.DataSetName.Split('|')[0];
                        feeCode = ds.DataSetName.Replace(ds.DataSetName.Split('|')[0] + "|", "");
                    }
                    else
                    {
                        return Content(new JsonMessage { Message = "导入终止：费目代码不正确，请确认！", Code = "1" }.ToString());
                    }
                    for (int m = 0; m < ds.Tables.Count; m++)
                    {
                        if (ds.Tables[m].Rows.Count == 0)
                        {
                            continue;
                            //return Content(new JsonMessage { Message = "导入终止：请维护定价数据！", Code = "1" }.ToString());
                        }
                        string gdzrid = "";
                        if (ds.Tables[m].Columns[3].ToString() != "1")
                        {
                            gdzrid = ds.Tables[m].Columns[3].ToString().TrimEnd('\'').ToLower();
                        }
                        // 每个table只有一个定价方式,所以只取第一行数据
                        string djfsrid = "";
                        if (ds.Tables[m].Columns[1].ToString() != "0")
                        {
                            djfsrid = ds.Tables[m].Columns[1].ToString().TrimEnd('\'').ToLower();
                        }
                        DataTable dtjc = new DataTable();
                        string mul = DataHelper.QueryValue("select mulbjfs from sqm_fee_calc where feecode = '" + feeCode + "'") + "";
                        if (mul == "")
                        {
                            return Content(new JsonMessage { Message = "导入终止：费目代码不正确，请确认！", Code = "1" }.ToString());
                        }
                        else if (mul == "1")
                        {
                            if (gdzrid != "")
                            {
                                dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and GDZRID = '" + gdzrid + "' order by VALCOL asc");
                            }
                            else if (djfsrid != "")
                            {
                                dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID = '" + djfsrid + "' order by VALCOL asc");
                            }
                            else
                            {
                                return Content(new JsonMessage { Message = "导入失败：\"" + ds.Tables[m].Rows[0]["定价方式"] + "\" 的数据未填写\"定价方式ID\"！", Code = "1" }.ToString());
                            }
                        }
                        else if (mul == "0")
                        {
                            dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL,MSRUNIT from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID is null order by VALCOL asc");
                        }
                        //一个table里面定价方式和高低值一样
                        string mesval = "";
                        if (!String.IsNullOrEmpty(ds.Tables[m].Rows[0]["定价方式"].ToString()))
                        {
                            mesval += ds.Tables[m].Rows[0]["定价方式"].ToString() + "->";
                        }
                        if (!String.IsNullOrEmpty(ds.Tables[m].Rows[0]["高低值"].ToString()))
                        {
                            mesval += ds.Tables[m].Rows[0]["高低值"].ToString() + "->";
                        }
                        string djbj = "";
                        string minall = "";
                        for (int n = 0; n < ds.Tables[m].Rows.Count; n++)
                        {
                            //判断定价标记是否重复
                            string djbjval = ds.Tables[m].Rows[n]["定价标记"].ToString();
                            if (djbj.IndexOf(djbjval) != -1 && !String.IsNullOrEmpty(djbjval))
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行定价标记重复！", Code = "1" }.ToString());
                            }
                            if (!String.IsNullOrEmpty(djbjval))
                            {
                                djbj += djbjval + ",";
                            }
                            //判断MIN值是否一样且是必填项
                            string minval = "";
                            bool minflag = false;
                            if (ds.Tables[m].Rows[n].Table.Columns.Contains("MIN"))
                            {
                                minval = ds.Tables[m].Rows[n]["MIN"].ToString();
                                minflag = true;
                            }
                            else if (ds.Tables[m].Rows[n].Table.Columns.Contains("最低报价"))
                            {
                                minval = ds.Tables[m].Rows[n]["最低报价"].ToString();
                                minflag = true;
                            }
                            if (minflag && String.IsNullOrEmpty(minval))
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行MIN或最低报价不能为空！", Code = "1" }.ToString());
                            }
                            //else if (minflag && !String.IsNullOrEmpty(minall) && minall != minval)
                            //{
                            //    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行MIN或最低报价需要维护一样！", Code = "1" }.ToString());
                            //}
                            else
                            {
                                minall = minval;
                            }
                            string djdw = "";
                            string djdwval = "";
                            string jsfsval = "";
                            DataTable maindt = null;
                            //币种校验
                            string bzval = ds.Tables[m].Rows[n]["币种"].ToString();
                            if (String.IsNullOrEmpty(bzval))
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种不能为空！", Code = "1" }.ToString());
                            }
                            else
                            {
                                if (bzval.ToUpper() == "RMB")
                                {
                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种不能为\"" + bzval + "\"", Code = "1" }.ToString());
                                }
                                maindt = MainDataExist("", bzval, "3");
                                if (maindt == null || maindt.Rows.Count == 0)
                                {
                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行币种\"" + bzval + "\" 主数据中不存在", Code = "1" }.ToString());
                                }
                            }
                            //计费单位、计算方式、定价状态校验
                            djdwval = ds.Tables[m].Rows[n]["计费单位"].ToString();
                            jsfsval = ds.Tables[m].Rows[n]["计算方式"].ToString();
                            if (!(jsfsval == "相对值" || jsfsval == "绝对值"))
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计算方式请维护相对值或绝对值！", Code = "1" }.ToString());
                            }
                            //价格必填校验
                            if (String.IsNullOrEmpty(ds.Tables[m].Rows[n]["报价"].ToString()))
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行报价不能为空！", Code = "1" }.ToString());
                            }
                            //起始日期不能大于截止日期
                            DateTime startDate = Convert.ToDateTime(ds.Tables[m].Rows[n]["起始日期"].ToString());
                            DateTime endDate = Convert.ToDateTime(ds.Tables[m].Rows[n]["截止日期"].ToString());
                            if (startDate > endDate)
                            {
                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行起始日期不能大于截止日期！", Code = "1" }.ToString());
                            }
                            if (dtjc.Rows.Count > 0)// 普通计费基础
                            {
                                // 基础校验
                                foreach (DataRow dr in dtjc.Rows)
                                {
                                    if (!String.IsNullOrEmpty(dr["MSRUNIT"].ToString()))
                                    {
                                        djdw += dr["MSRUNIT"].ToString() + "/";
                                    }
                                    string jsjccode = dr["CALCCODE"].ToString();
                                    string jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
                                    // 是否为空校验 如果不是X类型，其他基础不能为空
                                    if (!CheckIfX(jsjccode))
                                    {
                                        if (String.IsNullOrEmpty(jcvalue))
                                        {
                                            return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":数据不能为空！", Code = "1" }.ToString());
                                        }
                                    }
                                    // 主数据校验
                                    if (NoCheckJc(jsjccode) || jsjccode == "SOURCELOC_ZONE")
                                    {
                                        //数据为*的不用校验了
                                        if (jcvalue == "*")
                                        {
                                            listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
                                            continue;
                                        }
                                        // 通用主数据 MDM
                                        if (jsjccode.IndexOf("GJ") >= 0) // 国家
                                        {
                                            if (jcvalue != "")
                                            {
                                                maindt = MainDataExist("", jcvalue, "1");
                                                if (maindt == null || maindt.Rows.Count == 0)
                                                {
                                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
                                                }
                                                else// if ((code != jcvalue) && (code != jcvalue.ToUpper()) && (code != jcvalue.ToLower()))
                                                {
                                                    // 基础数据如果不是code则变为code -> code带出来，放入值表columnc列
                                                    listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
                                                }
                                            }
                                        }
                                        // MDMLOC
                                        else if (jsjccode.IndexOf("QYG") >= 0 || jsjccode.IndexOf("ZZYG") >= 0 || jsjccode.IndexOf("MDG") >= 0 || jsjccode.IndexOf("ZZG") >= 0 || jsjccode.IndexOf("HX") >= 0)// 港口+航线
                                        {
                                            if (jcvalue != "")
                                            {
                                                maindt = MainDataExist("", jcvalue, "2");
                                                if (maindt == null || maindt.Rows.Count == 0)
                                                {
                                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
                                                }
                                                else
                                                {
                                                    listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
                                                }
                                            }
                                        }
                                        // MDMBP
                                        else if (jsjccode.IndexOf("HKGS") >= 0 || jsjccode.IndexOf("CGS") >= 0)// 航空公司、船公司
                                        {
                                            if (jcvalue != "")
                                            {
                                                maindt = MainDataExist("", jcvalue, "4");
                                                if (maindt == null || maindt.Rows.Count == 0)
                                                {
                                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
                                                }
                                                else
                                                {
                                                    listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
                                                }
                                            }
                                        }
                                        // 计算基础 MDMJC
                                        else
                                        {
                                            try
                                            {
                                                maindt = MainDataExist(jsjccode, jcvalue, "6");
                                                if (maindt == null || maindt.Rows.Count == 0)
                                                {
                                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 主数据中不存在", Code = "1" }.ToString());
                                                }
                                                else// if ((code != jcvalue) && (code != jcvalue.ToUpper()) && (code != jcvalue.ToLower()))
                                                {
                                                    listDs[0].Tables[m].Rows[n][jsjccode] = maindt.Rows[0]["name"].ToString() + "&&" + maindt.Rows[0]["code"].ToString();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + ex.Message + " " + mesval + "数据第" + (++n) + "行基础为: " + jsjccode + "的主数据配置错误！", Code = "1" }.ToString());
                                            }
                                        }
                                    }
                                    // 不校验主数据的基础，校验长度
                                    else
                                    {
                                        //不校验主数据的基础code也要存数值
                                        string maxlen = "";
                                        if (!String.IsNullOrEmpty(jcvalue))
                                        {
                                            string result = CheckData(jsjccode, jcvalue);
                                            string valok = result.Substring(0, 1);//错误代码
                                            if (valok == "1" || valok == "2" || valok == "3")
                                            {
                                                maxlen = result.Substring(1, result.Length - 1);//最大值
                                            }
                                            if (valok == "1")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，整数位最长" + maxlen + "位", Code = "1" }.ToString());
                                            }
                                            else if (valok == "2")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，小数位最长" + maxlen + "位", Code = "1" }.ToString());
                                            }
                                            else if (valok == "3")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据错误，数据最长" + maxlen + "位", Code = "1" }.ToString());
                                            }
                                            else if (valok == "4")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 请维护数字型数据", Code = "1" }.ToString());
                                            }
                                            else if (valok == "5")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据应为'X'或空", Code = "1" }.ToString());
                                            }
                                            else if (valok == "6")
                                            {
                                                return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行\"" + jsjccode + "\":" + jcvalue + " 数据应为'Y'或'N'", Code = "1" }.ToString());
                                            }
                                            else
                                            {
                                                listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
                                            }
                                        }
                                        else
                                        {
                                            listDs[0].Tables[m].Rows[n][jsjccode] = jcvalue + "&&" + jcvalue;
                                        }
                                    }
                                }
                                //单位校验
                                if (jsfsval == "绝对值" && djdwval != "票")
                                {
                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为\"" + "票" + "\"", Code = "1" }.ToString());
                                }
                                else if (jsfsval == "相对值" && djdwval != djdw.TrimEnd('/'))
                                {
                                    if (!String.IsNullOrEmpty(djdw.TrimEnd('/')))
                                    {
                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为\"" + djdw.TrimEnd('/') + "\"", Code = "1" }.ToString());
                                    }
                                    else
                                    {
                                        return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + (++n) + "行计费单位错误，应为空", Code = "1" }.ToString());
                                    }
                                }
                                //excel重复数据判断
                                int hbj = 1;//行标记
                                bool cfsj = imPortData(listDs[0].Tables[m], dtjc, ref hbj);
                                if (cfsj)
                                {
                                    return Content(new JsonMessage { Message = "导入失败：" + mesval + "数据第" + hbj + "行导入数据有效期存在交叉，请确认！", Code = "1" }.ToString());
                                }
                            }
                            else // 不存在计费基础的
                            {

                            }
                        }
                    }

                    // 数据入库  入库之前先把表原始数据失效
                    DeleteDataSource(bjrid, "update");
                    // insert操作
                    string msg = InsertData(ref colname, listDs, bjrid, status);
                    if (msg != "T")
                    {
                        return Content(new JsonMessage { Message = "导入中止: " + msg, Code = "2" }.ToString());
                    }
                    else
                    {
                        //DeleteDataSource(mrid, vrid, bjname, version, "delete");// 删除status为0的数据
                    }
                    return Content(new JsonMessage { Message = "导入成功", Code = "0" }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = "导入失败,文件不存在", Code = "1" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
            }
        }
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
        private static DataTable mdatadt = DataHelper.QueryDataTable("select distinct mdkey from mdm_calc_value");
        private static bool NoCheckJc(string jsjccode)
        {
            bool sign = false;
            // 港口 国家 航空公司 船公司
            string[] mustCheck = { "GJ", "QYG", "MDG", "ZZG", "ZYG", "HKGS", "CGS" };
            for (int c = 0; c < mustCheck.Length; c++)
            {
                if (jsjccode.IndexOf(mustCheck[c]) >= 0)
                {
                    sign = true;
                    break;
                }
            }
            if (!sign)
            {
                DataRow[] dr = mdatadt.Select("MDKEY = '" + jsjccode + "'");
                if (dr.Length > 0)
                {
                    sign = true;
                }
            }
            return sign;
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
            try
            {
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
                        if (j == 0)// && cellStr == "定价方式ID"
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
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            //for (int k = 0; k < cells.MaxDataRow + 1; k++)
            //{
            //    bool titleRow = false;
            //    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
            //    {
            //        // 记录位置2
            //        rownum = (k + 1) + "";
            //        colnum = (j + 1) + "";
            //        string cellStr = cells[k, j].StringValue.Trim();
            //        // 判断是否标题行
            //        if (j == 0 && cellStr == "定价方式ID")
            //        {
            //            titleRow = true;
            //            if (k == 0)
            //            {
            //                dataTable = new DataTable();
            //            }
            //            else
            //            {
            //                DataTable dtnew = dataTable.Copy(); // 跟datarow一样，datatable也不能同时存进一个dataset（地址相同）
            //                excel_ds.Tables.Add(dtnew);
            //                dataTable = new DataTable();
            //            }
            //            dataRow = dataTable.NewRow();
            //        }
            //        if (titleRow)
            //        {

            //            string title = cellStr.Replace("（", "(").Replace("(", "(").Replace("）", ")");
            //            // title格式：起运港（海运）(QYG)()
            //            title = title.Replace("()", "");
            //            title = title.Replace(")", "");
            //            // title格式：起运港（海运）(QYG)(=) => 起运港(海运(QYG(=  先去掉(=) 然后去最后一个“(”之后的内容
            //            title = title.Replace("(=", "").Replace("(<=", "").Replace("(>=", ""); // 起运港(海运(QYG(=  => 起运港(海运(QYG
            //            if (title.IndexOf("(") >= 0)
            //            {
            //                title = title.Substring(title.LastIndexOf("(") + 1);
            //                dataTable.Columns.Add(title);
            //            }
            //            else if (title.IndexOf("最低报价") >= 0)
            //            {
            //                dataTable.Columns.Add("最低报价");
            //            }
            //            else
            //            {
            //                // 可能会校验其它标题格式
            //                dataTable.Columns.Add(title);
            //            }
            //        }
            //        else
            //        {
            //            // 判断整行是否为空
            //            int count = 0;
            //            if (j == 0)
            //            {
            //                for (int col = 0; col < cells.MaxDataColumn + 1; col++)
            //                {
            //                    if (cells[k, col].StringValue.Trim() == "")
            //                    {
            //                        count++;
            //                    }
            //                }
            //            }
            //            if (count != (cells.MaxDataColumn + 1))
            //            {
            //                dataRow[j] = cellStr;
            //            }
            //            else
            //            {
            //                dataRow[0] = null;
            //            }
            //        }
            //    }
            //    if (!dataRow.IsNull(0))
            //    {
            //        DataRow drnew = dataTable.NewRow();// 取数据，row[index]  row[columnName]
            //        drnew.ItemArray = dataRow.ItemArray;
            //        dataTable.Rows.Add(drnew);
            //    }
            //    if (k == cells.MaxDataRow)// 如果是最后一行，把最后一个dataTable添加进dataSet中
            //    {
            //        DataTable dtnew = dataTable.Copy();
            //        excel_ds.Tables.Add(dtnew);
            //    }
            //}
            listDs.Add(excel_ds);
            al.Add(listDs);
            return al;
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
        private static void DeleteDataSource(string bjrid, string type)
        {
            // 删除sqm_modedj_val表
            string sql_smv = "";
            if (type == "update")
            {
                sql_smv = "update SQM_MODEBJ_VAL set STATUS = '0' where FEECALCID = '" + bjrid + "' and STATUS = '1'";
            }
            else if (type == "delete")
            {
                sql_smv = "delete from SQM_MODEBJ_VAL where FEECALCID ='" + bjrid + "' and STATUS = '0'";
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
        private string InsertData(ref List<string> colname, List<DataSet> listDs, string bjrid, string status)
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
                    // 2.报价值表：sqm_modebj_val 表插数 
                    List<string> sqls = new List<string>();
                    string sql = "";
                    string bjstas = "";
                    string minstas = "";
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
                            string djfsrid = "";
                            string gdzrid = "";
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
                            //未保存时判断定价值表的数据
                            if (status == "0")
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string djrid = string.Empty;
                                    string bjstataus = "1";//默认已保存
                                    string bz = string.Empty;
                                    string minbjprice = string.Empty;
                                    string calcunit = string.Empty;
                                    string val_rid = string.Empty;
                                    string djzdj = "";
                                    string djzgj = "";
                                    string djzhidj = "";
                                    string bjprice = "";
                                    string calctype = "";
                                    string beginDate = "";
                                    string endDate = "";
                                    string djmin = "";
                                    string minstatus = "0";
                                    bool ifdj = true;
                                    djrid = dr["定价标记"].ToString();
                                    SQM_MODEDJ_VAL smvdj = SQM_MODEDJ_VAL.FindFirstByProperties(SQM_MODEDJ_VAL.Prop_RID, djrid, SQM_MODEDJ_VAL.Prop_STATUS, "1", SQM_MODEDJ_VAL.Prop_DJSTATUS, "1");
                                    if (smvdj == null)
                                    {
                                        ifdj = false;
                                        djrid = bjrid;
                                    }
                                    else
                                    {
                                        djmin = smvdj.MIN.ToString();
                                    }
                                    string sql_value_insert = "insert into SQM_MODEBJ_VAL(RID,BJSTATUS,IFBJITEM,CREATETIME,CREATEUSER,CREATEID,FEECALCID,CURRENCY,CALCUNIT,MINBJPRICE,CALCNAME,CALCCODE,MAXPRICE,MINPRICE,GUIDEPRICE,CALCTYPE,STARTDATE,ENDDATE,DJFSRID,GDZRID,STATUS,DJRID,BJPRICE,MINSTATUS";
                                    string sql_value_values = " values(";
                                    if (dr.Table.Columns.Contains("币种")) { bz = dr["币种"] + ""; }
                                    if (dr.Table.Columns.Contains("报价")) { bjprice = dr["报价"] + ""; }
                                    if (dr.Table.Columns.Contains("计费单位")) { calcunit = dr["计费单位"] + ""; }
                                    if (dr.Table.Columns.Contains("MIN")) { minbjprice = dr["MIN"] + ""; }
                                    if (!String.IsNullOrEmpty(djmin))
                                    {
                                        if (Convert.ToDecimal(minbjprice) < Convert.ToDecimal(djmin))
                                        {
                                            minstatus = "1";//变小
                                        }
                                        else if (Convert.ToDecimal(minbjprice) > Convert.ToDecimal(djmin))
                                        {
                                            minstatus = "2";//变大
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("最低价") && ifdj)
                                    {
                                        djzdj = dr["最低价"] + "";
                                    }
                                    if (dr.Table.Columns.Contains("最高价") && ifdj)
                                    {
                                        djzgj = dr["最高价"] + "";
                                    }
                                    decimal maxp = Convert.ToDecimal(djzgj == "" ? "0" : djzgj);
                                    decimal minp = Convert.ToDecimal(djzdj == "" ? "0" : djzdj);
                                    decimal bjp = Convert.ToDecimal(bjprice);
                                    if (!ifdj)
                                    {
                                        bjstataus = "3";//3-无定价报价（已保存）
                                    }
                                    else if (bjp < minp || bjp > maxp)
                                    {
                                        bjstataus = "4";//4-报价超限（已保存）
                                    }
                                    if (dr.Table.Columns.Contains("指导价") && ifdj)
                                    {
                                        djzhidj = dr["指导价"] + "";
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
                                    if (dr.Table.Columns.Contains("起始日期"))
                                    {
                                        beginDate = dr["起始日期"] + "";
                                    }
                                    if (dr.Table.Columns.Contains("截止日期"))
                                    {
                                        endDate = dr["截止日期"] + "";
                                    }
                                    val_rid = System.Guid.NewGuid().ToString();
                                    sql_value_values += "'" + val_rid + "','" + bjstataus + "','1',to_date('" + createTime + "','yyyy/mm/dd hh24:mi:ss'),'" + createUser + "','" + createId + "','" + bjrid + "','" + bz + "','" + calcunit + "','" + minbjprice + "','" + "" + "','" + "" + "','" + djzgj + "','" + djzdj + "','" + djzhidj + "','" + calctype + "',to_date('" + beginDate + "','yyyy/mm/dd'),to_date('" + endDate + "','yyyy/mm/dd'),'" + djfsrid + "','" + gdzrid + "','1','" + djrid + "','" + bjprice + "','" + minstatus + "'";
                                    if (dtjc.Rows.Count > 0)// 普通计费基础
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
                                    else
                                    {
                                        sql_value_insert += ")";
                                        sql_value_values += ")";
                                    }
                                    bjstas += bjstataus + ",";
                                    minstas += minstatus + ",";
                                    sql = sql_value_insert + sql_value_values;
                                    sqls.Add(sql);
                                }
                            }
                            else//保存后判断报价值表的数据
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string bjzbrid = string.Empty;
                                    string bjstataus = "1";//默认已保存
                                    string bz = string.Empty;
                                    string minbjprice = string.Empty;
                                    string calcunit = string.Empty;
                                    string val_rid = string.Empty;
                                    string djzdj = "";
                                    string djzgj = "";
                                    string djzhidj = "";
                                    string bjprice = "";
                                    string calctype = "";
                                    string beginDate = "";
                                    string endDate = "";
                                    string djmin = "";
                                    string minstatus = "0";
                                    bool ifdj = true;
                                    bjzbrid = dr["定价标记"].ToString();
                                    SQM_MODEDJ_VAL smvbj = SQM_MODEDJ_VAL.FindFirstByProperties(SQM_MODEDJ_VAL.Prop_RID, bjzbrid, SQM_MODEDJ_VAL.Prop_STATUS, "1", SQM_MODEDJ_VAL.Prop_DJSTATUS, "1");
                                    if (smvbj == null)
                                    {
                                        ifdj = false;
                                    }
                                    else
                                    {
                                        djmin = smvbj.MIN.ToString();
                                    }
                                    string sql_value_insert = "insert into SQM_MODEBJ_VAL(RID,BJSTATUS,IFBJITEM,CREATETIME,CREATEUSER,CREATEID,FEECALCID,CURRENCY,CALCUNIT,MINBJPRICE,CALCNAME,CALCCODE,MAXPRICE,MINPRICE,GUIDEPRICE,CALCTYPE,STARTDATE,ENDDATE,DJFSRID,GDZRID,STATUS,DJRID,BJPRICE,MINSTATUS";
                                    string sql_value_values = " values(";
                                    if (dr.Table.Columns.Contains("币种")) { bz = dr["币种"] + ""; }
                                    if (dr.Table.Columns.Contains("报价")) { bjprice = dr["报价"] + ""; }
                                    if (dr.Table.Columns.Contains("计费单位")) { calcunit = dr["计费单位"] + ""; }
                                    if (dr.Table.Columns.Contains("最低报价")) { minbjprice = dr["最低报价"] + ""; }
                                    if (dr.Table.Columns.Contains("MIN")) { minbjprice = dr["MIN"] + ""; }
                                    if (!String.IsNullOrEmpty(djmin))
                                    {
                                        if (Convert.ToDecimal(minbjprice) < Convert.ToDecimal(djmin))
                                        {
                                            minstatus = "1";//变小
                                        }
                                        else if (Convert.ToDecimal(minbjprice) > Convert.ToDecimal(djmin))
                                        {
                                            minstatus = "2";//变大
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("最低价") && ifdj)
                                    {
                                        djzdj = dr["最低价"] + "";
                                    }
                                    if (dr.Table.Columns.Contains("最高价") && ifdj)
                                    {
                                        djzgj = dr["最高价"] + "";
                                    }
                                    decimal maxp = Convert.ToDecimal(djzgj == "" ? "0" : djzgj);
                                    decimal minp = Convert.ToDecimal(djzdj == "" ? "0" : djzdj);
                                    decimal bjp = Convert.ToDecimal(bjprice);
                                    if (!ifdj)
                                    {
                                        bjstataus = "3";//3-无定价报价（已保存）
                                    }
                                    else if (bjp < minp || bjp > maxp)
                                    {
                                        bjstataus = "4";//4-报价超限（已保存）
                                    }
                                    if (dr.Table.Columns.Contains("指导价") && ifdj)
                                    {
                                        djzhidj = dr["指导价"] + "";
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
                                    if (dr.Table.Columns.Contains("起始日期"))
                                    {
                                        beginDate = dr["起始日期"] + "";
                                    }
                                    if (dr.Table.Columns.Contains("截止日期"))
                                    {
                                        endDate = dr["截止日期"] + "";
                                    }
                                    val_rid = System.Guid.NewGuid().ToString();
                                    sql_value_values += "'" + val_rid + "','" + bjstataus + "','1',to_date('" + createTime + "','yyyy/mm/dd hh24:mi:ss'),'" + createUser + "','" + createId + "','" + bjrid + "','" + bz + "','" + calcunit + "','" + minbjprice + "','" + "" + "','" + "" + "','" + djzgj + "','" + djzdj + "','" + djzhidj + "','" + calctype + "',to_date('" + beginDate + "','yyyy/mm/dd'),to_date('" + endDate + "','yyyy/mm/dd'),'" + djfsrid + "','" + gdzrid + "','1','" + bjzbrid + "','" + bjprice + "','" + minstatus + "'";
                                    if (dtjc.Rows.Count > 0)// 普通计费基础
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
                                    else
                                    {
                                        sql_value_insert += ")";
                                        sql_value_values += ")";
                                    }
                                    bjstas += bjstataus + ",";
                                    minstas += minstatus + ",";
                                    sql = sql_value_insert + sql_value_values;
                                    sqls.Add(sql);
                                }
                            }
                        }
                    }
                    string psfsta = "1";
                    string minsta = "0";
                    if (bjstas.IndexOf("3") != -1)
                    {
                        psfsta = "3";//3-无定价报价（已保存）
                    }
                    else if (bjstas.IndexOf("4") != -1)
                    {
                        psfsta = "4";//4-报价超限（已保存）
                    }
                    if (minstas.IndexOf("1") != -1)
                    {
                        minsta = "1";
                    }
                    else if (minstas.IndexOf("2") != -1)
                    {
                        minsta = "2";
                    }
                    sql = @"update SQM_BJ_PSF set BJSTATAUS='" + psfsta + "',MINSTATUS='" + minsta + "' where RID='" + bjrid + "'";
                    sqls.Add(sql);
                    string sqll = string.Join(";", sqls.ToArray());
                    sqll = "begin " + sqll + ";end;";
                    // 插数
                    DataHelper.ExecSql(sqll);
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
        [AllowAnonymous]
        public ActionResult DetailAdd()
        {
            try
            {
                string sql = "";
                string minprice = "";
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
                string id = Request.QueryString["id"];
                string djrid = Request.QueryString["djrid"];
                string gdzkey = Request.QueryString["gdzkey"];
                string gdzrid = Request.QueryString["gdzrid"];
                string bjrid = Request.QueryString["bjrid"];
                string jxjc = Request.QueryString["jxjc"];
                ViewBag.DJRID = djrid;
                ViewBag.BJRID = bjrid;
                ViewBag.jxjc = jxjc;
                string calcunit = "";
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
                    sql = string.Format("SELECT  DISTINCT r.GDZKEY,case when r.GDZKEY='0' THEN '无' when r.GDZKEY='A' THEN 'A' when r.GDZKEY='H' THEN 'H' when r.GDZKEY='L' THEN 'L' END GDZKEYNAME FROM SQM_FEE_PUR_REF r WHERE r.STATUS='1' and r.FEECODE = '{0}' {1} order by r.GDZKEY asc", sdp.FEECODE, wheredjfs);
                    GDZdt = DataHelper.QueryDataTable(sql);
                    BZdt = DataHelper.QueryDataTable("select WAERS,KTEXT from MDM_WAERS");
                    //                    if (businessorg == "供应链")
                    //                    {
                    //                        sql = @"select r.CALCCODE,r.CALCNAME from SQM_FEE_CALC_REF r
                    //                            left join SQM_DJ_PSF p on r.feecode=p.feecode
                    //                            where r.STATUS='1' and p.Rid='" + djrid + "' and r.ISCNT='是'group by CALCCODE,CALCNAME order by CALCNAME asc";
                    //                        JSJCdt = DataHelper.QueryDataTable(sql);
                    //                        gyl = true;
                    //                    }
                }
                ViewBag.BZdtData = BZdt;
                ViewBag.gyl = gyl;
                ViewBag.khy = khy;
                ViewBag.min = min;
                ViewBag.wjc = wjc;
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
        /// <param name="postdata"></param>
        /// <param name="rid"></param>
        /// <param name="djrid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PurSave(string postdata, string djrid, string bjrid, string djfsrid, string gdzrid, string jxjc, string ifmb)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                string guid = System.Guid.NewGuid().ToString(); //无定价报价RID
                //无定价报价不存定价值表，定价发布区间内给提示
                //SQM_MODEDJ_VAL oldsmv = null;
                SQM_MODEDJ_VAL smv = null;
                smv = JsonHelper.GetObject<SQM_MODEDJ_VAL>(postdata);
                smv.FEECALCID = djrid;
                DateTime startDate = (DateTime)smv.STARTDATE;
                DateTime endDate = (DateTime)smv.ENDDATE;
                string[] primaryKeys = getPrimaryKeys(djrid, djfsrid, gdzrid);
                // 获取原始数据
                DataTable dt = FindSourceData(smv, primaryKeys, djrid, bjrid);
                foreach (DataRow row in dt.Rows)
                {
                    //定价值表判断区间内时间交叉
                    if (row["SOUR"].ToString() == "DJ")
                    {
                        if (startDate >= (DateTime)row["STARTDATE"] && endDate <= (DateTime)row["ENDDATE"])
                        {
                            return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请确认！" }.ToString());
                        }
                    }
                    else//报价值表判断区间时间交叉
                    {
                        if ((startDate >= (DateTime)row["STARTDATE"] && startDate <= (DateTime)row["ENDDATE"]) || (endDate >= (DateTime)row["STARTDATE"] && endDate <= (DateTime)row["ENDDATE"]))
                        {
                            return Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应报价，请确认！" }.ToString());
                        }
                    }
                }
                SQM_MODEBJ_VAL bjobj = null;
                bjobj = JsonHelper.GetObject<SQM_MODEBJ_VAL>(postdata);
                //oldsmv = smv;
                //oldsmv.DJSTATUS = "0";
                //oldsmv.WDJBJRID = guid;
                //oldsmv.BJPRICE = bjobj.BJPRICE;
                //oldsmv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                //oldsmv.DoSave();
                // 定价值表rid生成是为了插入到报价值表
                // 判断是否为模板进入
                if (ifmb == "1")
                {
                    bjobj.DJRID = "";
                }
                bjobj.WDJBJRID = guid;
                bjobj.IFBJITEM = "1"; //新增无定价报价默认是报价项目
                bjobj.STATUS = "1";
                bjobj.BJSTATUS = "3";//无定价报价（已保存）
                bjobj.FEECALCID = bjrid;
                bjobj.DJFSRID = djfsrid;
                bjobj.GDZRID = gdzrid;
                bjobj.JXJC = jxjc;
                bjobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                bjobj.DoSave();
                SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(bjrid);
                sbp.BJSTATAUS = "3";//无定价报价（已保存）
                sbp.DoUpdate();
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
                string[] primaryKeys = { "CURRENCY", "DJFSRID", "GDZRID" };
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
        public DataTable FindSourceData(SQM_MODEDJ_VAL srcobj, string[] fields, string djrid, string bjrid)
        {
            string sqldj = "select 'DJ' as SOUR,STARTDATE,ENDDATE from SQM_MODEDJ_VAL where FEECALCID='" + djrid + "' and DJSTATUS='1' ";
            string sqlbj = "select 'BJ' as SOUR,STARTDATE,ENDDATE from SQM_MODEBJ_VAL where FEECALCID='" + bjrid + "' and STATUS='1' ";
            string wherestr = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (srcobj.GetValue(fields[i]) == null) //数字类型
                {
                    wherestr += " and " + fields[i] + " is null";
                }
                else if (String.IsNullOrEmpty(srcobj.GetValue(fields[i]).ToString()))  //字符串类型
                {
                    wherestr += " and " + fields[i] + " is null";
                }
                else
                {
                    wherestr += " and " + fields[i] + " = '" + srcobj.GetValue(fields[i]) + "'";
                }
            }
            string sql = sqldj + wherestr + " union " + sqlbj + wherestr + " order by STARTDATE";
            DataTable dt = DataHelper.QueryDataTable(sql);
            return dt;
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

        public class PRD
        {
            public string prdcode;
            public List<SRV> srvcodes;
        }

        public class SRV
        {
            public string srvcode;
            public List<string> feecodes;
        }
        //通过产品服务费目的code得到name
        public string CODETONAME(string type, string code)
        {
            var name = "";
            switch (type)
            {
                case "prd":
                    var prdlist = DataHelper.QueryDictList("SELECT SQM_PRD_EXT.SQPRODUCTNAME,SQM_PRD_EXT.PRODUCTKEY FROM SQM_PRD_EXT");
                    foreach (var prditem in prdlist)
                    {
                        if (prditem.Get("PRODUCTKEY").ToString() == code)
                        {
                            name = prditem.Get("SQPRODUCTNAME").ToString();
                        }
                    }
                    break;
                case "srv":
                    var srvlist = DataHelper.QueryDictList("SELECT MDM_SERVICE.SERVICETYPE,MDM_SERVICE.SERVICENAME FROM MDM_SERVICE");
                    foreach (var srvitem in srvlist)
                    {
                        if (srvitem.Get("SERVICETYPE").ToString() == code)
                        {
                            name = srvitem.Get("SERVICENAME").ToString();
                        }
                    }
                    break;
                case "fee":
                    var feelist = DataHelper.QueryDictList("SELECT MDM_FEE.TCET084,MDM_FEE.TEXTDESC FROM V_MDM_FEE");
                    foreach (var feeitem in feelist)
                    {
                        if (feeitem.Get("TCET084").ToString() == code)
                        {
                            name = feeitem.Get("TEXTDESC").ToString();
                        }
                    }
                    break;

            }
            return name;
        }
        //获取费目类型
        public string getCatg(string prodcode, string srvcode, string feecode)
        {
            string feecatg = DataHelper.QueryValue(string.Format(@"select FEECATG from SQM_SRV_FEE_CONFIG where PRODCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and STATUS='1'", prodcode, srvcode, feecode)) + "";
            return feecatg;
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
        /// 判断每组高低值是否都有报价项
        /// </summary>
        /// <param name="bjrid"></param>
        /// <param name="gdzrid"></param>
        /// <param name="counum"></param>
        /// <returns></returns>
        public string GdzHasAll(string bjrid, string gdzridstr)
        {
            string retstr = "";
            try
            {
                string gdzbjstr = "";
                foreach (string gdzrid in gdzridstr.Split(','))
                {
                    string gdzbj = "";
                    string sql = @"select RID from SQM_MODEBJ_VAL where FEECALCID='" + bjrid + "' and GDZRID=" + gdzrid + "";
                    DataTable gdzdt = DataHelper.QueryDataTable(sql);
                    if (gdzdt.Rows.Count == 0)
                    {
                        gdzbj = "0";
                    }
                    else
                    {
                        gdzbj = "1";
                    }
                    gdzbjstr += gdzbj + ",";
                    if (!gdzbjstr.Contains(gdzbj))
                    {
                        retstr = "2";
                        break;
                    }
                }
                if (gdzbjstr.Contains("0"))
                {
                    retstr = "0";
                }
                else if (gdzbjstr.Contains("1"))
                {
                    retstr = "1";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retstr;
        }

        /// <summary>
        /// 保存仓租相关的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult saveCZXG(SQM_BJ_CZXG czxg)
        {
            var rid = Request["RID"];  //可以取到
            try
            {
                if (czxg != null)
                {
                    if (czxg.DJFSRID != "" && czxg.FEECODE != "")
                    {

                        if (!string.IsNullOrEmpty(rid))
                        {
                            SQM_BJ_CZXG sbc = SQM_BJ_CZXG.Find(rid);
                            sbc.CZBY = czxg.CZBY;
                            sbc.YZD = czxg.YZD;
                            sbc.DTCK = czxg.DTCK;
                            sbc.MZTS = czxg.MZTS;
                            sbc.BYFY = czxg.BYFY;
                            sbc.DoUpdate();
                        }
                        else
                        {
                            czxg.DoCreate();
                        }
                        var obj = new { message = "保存成功" };
                        return Content(JsonHelper.GetJsonString(obj));
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取当前的定价的仓租相关的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult czxgData(string djfsrid, string gdzrid, string feecode, string bjrid)
        {
            string czxgsql = "";
            if (!string.IsNullOrEmpty(gdzrid))
            {
                czxgsql = "select RID,CZBY,YZD,BYFY,DTCK,MZTS from SQM_BJ_CZXG where BJRID='" + bjrid + "' and DJFSRID='" + djfsrid + "' and GDZRID='" + gdzrid + "' and FEECODE='" + feecode + "'";
            }
            else
            {
                czxgsql = "select RID,CZBY,YZD,BYFY,DTCK,MZTS from SQM_BJ_CZXG where BJRID='" + bjrid + "' and DJFSRID='" + djfsrid + "' and FEECODE='" + feecode + "' and GDZRID is null";
            }
            DataTable czxgdt = DataHelper.QueryDataTable(czxgsql);
            if (czxgdt != null && czxgdt.Rows.Count > 0)
            {
                return Content(JsonHelper.GetJsonString(czxgdt));
            }
            return null;
        }
        public ActionResult BjCztj()
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            string code = "0";
            try
            {
                SQM_BJ_CZTJ sbc = new SQM_BJ_CZTJ();
                string feecode = Request["FEECODE"].ToString();
                string djfsrid = Request["DJFSRID"].ToString();
                string gdzrid = Request["GDZRID"].ToString();
                string bjrid = Request["BJRID"].ToString();
                string html = Request["html"].ToString();
                string[] strArr = html.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string refArrs in strArr)
                {
                    string[] refArr = refArrs.Split(',');
                    string tjmc = "";
                    string tjmckey = "";
                    string tjtype = "";
                    string tjtypekey = "";
                    string wheregdz = "";
                    string tjmckeysql = "";
                    if (refArr[1].Trim().IndexOf("--") > 0)
                    {
                        tjmc = refArr[1].Trim().Substring(0, refArr[1].Trim().IndexOf("--"));
                        tjmckey = refArr[1].Trim().Substring(refArr[1].Trim().IndexOf("--") + 2, 2);
                        if (refArr[2].Trim() != "请选择")
                        {
                            tjtype = refArr[2].Trim().Substring(0, refArr[2].Trim().IndexOf("--"));
                            tjtypekey = refArr[2].Trim().Substring(refArr[2].Trim().IndexOf("--") + 2, 2);
                        }
                        if (String.IsNullOrEmpty(gdzrid))
                        {
                            wheregdz = " and GDZRID is null";
                        }
                        else
                        {
                            wheregdz = " and GDZRID='" + gdzrid + "'";
                        }
                        if (!String.IsNullOrEmpty(refArr[0].Trim()))
                        {
                            tjmckeysql = string.Format("select RID from SQM_BJ_CZTJ where STATUS='1' and FEECODE='{0}' and BJRID='{1}' and DJFSRID='{2}' and TJMCKEY='{3}' and RID<>'{4}' {5}", feecode, bjrid, djfsrid, tjmckey, refArr[0].Trim(), wheregdz);
                            code = "1";
                        }
                        else
                        {
                            tjmckeysql = string.Format("select RID from SQM_BJ_CZTJ where STATUS='1' and FEECODE='{0}' and BJRID='{1}' and DJFSRID='{2}' and TJMCKEY='{3}' {4}", feecode, bjrid, djfsrid, tjmckey, wheregdz);
                            code = "2";
                        }
                        DataTable tjmckeydt = DataHelper.QueryDataTable(tjmckeysql);
                        if (tjmckeydt.Rows.Count > 0)
                        {
                            return Content(new JsonMessage { Success = false, Code = code, Message = "条件重复，请确认！" }.ToString());
                        }
                    }
                    else
                    {
                        continue;
                    }
                    if (!String.IsNullOrEmpty(refArr[0].Trim()))
                    {
                        sbc = SQM_BJ_CZTJ.Find(refArr[0].Trim());
                        sbc.FEECODE = feecode;
                        sbc.DJFSRID = djfsrid;
                        sbc.GDZRID = gdzrid;
                        sbc.TJMC = tjmc;
                        sbc.TJMCKEY = tjmckey;
                        sbc.TJTYPE = tjtype;
                        sbc.TJTYPEKEY = tjtypekey;
                        sbc.WDZ = refArr[3].Trim();
                        sbc.DoUpdate();
                    }
                    else
                    {
                        sbc.FEECODE = feecode;
                        sbc.DJFSRID = djfsrid;
                        sbc.GDZRID = gdzrid;
                        sbc.BJRID = bjrid;
                        sbc.STATUS = "1";
                        sbc.TJMC = tjmc;
                        sbc.TJMCKEY = tjmckey;
                        sbc.TJTYPE = tjtype;
                        sbc.TJTYPEKEY = tjtypekey;
                        sbc.WDZ = refArr[3].Trim();
                        sbc.DoCreate();
                    }
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Code = code, Message = rtnmsg }.ToString());
        }
        public ActionResult getCztj()
        {
            try
            {
                string FEECODE = Request["FEECODE"].ToString();
                string DJFSRID = Request["DJFSRID"].ToString();
                string GDZRID = Request["GDZRID"].ToString();
                string BJRID = Request["BJRID"].ToString();
                string wherestr = " and DJFSRID='" + DJFSRID + "'";
                string JsonString = string.Empty;
                if (String.IsNullOrEmpty(GDZRID))
                {
                    wherestr += " and GDZRID is null";
                }
                else
                {
                    wherestr += " and GDZRID='" + GDZRID + "'";
                }
                string sql = @"select RID,TJMC,TJTYPE,WDZ from SQM_BJ_CZTJ where STATUS='1' and FEECODE='" + FEECODE + "' and BJRID='" + BJRID + "' {0} order by TJMC asc";
                DataTable dt = DataHelper.QueryDataTable(string.Format(sql, wherestr));
                JsonString = JsonConvert.SerializeObject(dt);
                return Content(JsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult delCztj()
        {
            bool rtnflag = true;
            string rtnmsg = "删除成功";
            try
            {
                string rids = Request["RIDS"].ToString();
                foreach (string rid in rids.Split(','))
                {
                    DataHelper.ExecSql("delete from SQM_BJ_CZTJ where RID='" + rid + "'");
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        public void IFUpdate(string BjRid)
        {
            SQM_MODEBJ_VAL smv = SQM_MODEBJ_VAL.Find(BjRid);
            if (smv.IFUPDATE == "1")
            {
                smv.IFUPDATE = "";
                smv.DoUpdate();
            }
            SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(smv.FEECALCID);
            if (smv.IFUPDATE == "1")
            {
                sbp.IFBJCX = "2";
                sbp.DoUpdate();
            }

        }
        private bool judgedj(string jurid, string djfsrid, string djrid)
        {
            string str = @"select distinct r.CALCNAME || '(' || r.SCALE || ')' CALCNAME,
                r.VALCOL,
                r.CALCCODE,
                e.MDMTYPE,
                e.MDMKEY,
                e.MDMFIELDNAME,
                e.MDMLOCTYPE,
                r.SORD
  from SQM_FEE_CALC_REF r
  left join SQM_DJ_PSF p
    on r.FEECODE = p.FEECODE
  left join SQM_CALC_BASE_EXT e
    on r.CALCCODE = e.CALCCODE
 where r.STATUS = '1'
   and p.Rid = '" + djrid + @"'
   and r.DJFSRID = '" + djfsrid + @"'
   and r.GDZRID is null
   and 1 = 1
 order by r.SORD asc
";
            DataTable lie = DataHelper.QueryDataTable(str);

            if (!string.IsNullOrEmpty(jurid))
            {
                string getdatesql = @"select * from SQM_MODEBJ_VAL where rid in (" + jurid + ")";
                var getdatedt = DataHelper.QueryDataTable(getdatesql);
                for (int i = 0; i <= getdatedt.Rows.Count - 2; i++)
                {
                    for (int k = 1 + i; k < getdatedt.Rows.Count - 1; k++)
                    {
                        bool allsame = false;
                        int jump = 0;
                        for (int x = 0; x < lie.Rows.Count; x++)
                        {
                            string colname = lie.Rows[x]["VALCOL"] + "";

                            string cell1 = getdatedt.Rows[i][colname].ToString();
                            string cell2 = getdatedt.Rows[k][colname].ToString();
                            if (cell1 != cell2)
                            {
                                break;
                            }
                            jump++;


                            if (x >= jump)
                            {
                                allsame = true;
                            }
                        }
                        if (allsame)
                        {
                            var startdate = DateTime.Parse(getdatedt.Rows[i]["STARTDATE"].ToString());
                            var startdate2 = DateTime.Parse(getdatedt.Rows[k]["STARTDATE"].ToString());
                            var enddate = DateTime.Parse(getdatedt.Rows[i]["ENDDATE"].ToString());
                            var enddate2 = DateTime.Parse(getdatedt.Rows[k]["ENDDATE"].ToString());
                            if (startdate >= startdate2 && startdate <= enddate2 || (enddate >= enddate2 && enddate <= enddate2))
                            {
                                return false;//Content(new JsonMessage { Success = false, Message = "所选时间区间已存在相应定价，请确认！" }.ToString());
                            }
                        }
                    }

                }
            }
            return true;
        }
    }
}
