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
using NPOI.SS.Converter;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Threading;
//using System.Transactions;

namespace Oncontrol3.Web.Controllers
{
    public class SQM_BJ_IMP_EXPController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Excel单元格 样式 Style
        /// </summary>
        /// <param name="stylestr"></param>
        /// <returns></returns>
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
                case "styleContent2":
                    Style styleContent2 = workbook.Styles[workbook.Styles.Add()];
                    styleContent2.Font.Name = "微软雅黑";
                    styleContent2.Font.Size = 10;
                    styleContent2.IsTextWrapped = true;//单元格内容自动换行
                    styleContent2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                    styleContent2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                    styleContent2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                    styleContent2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                    style = styleContent2;
                    break;
                case "styleContentLeftJustify":// 左对齐
                    Style styleContentJustify = workbook.Styles[workbook.Styles.Add()];
                    //styleContentJustify.VerticalAlignment = TextAlignmentType.Center;// 文字垂直居中
                    styleContentJustify.VerticalAlignment = TextAlignmentType.Top;
                    styleContentJustify.Font.Name = "微软雅黑";
                    styleContentJustify.Font.Size = 10;
                    styleContentJustify.IsTextWrapped = true;//单元格内容自动换行
                    styleContentJustify.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContentJustify.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContentJustify.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContentJustify.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContentJustify;
                    break;
                case "styleContentYellow":// 高亮标识_黄
                    Style styleContentYellow = workbook.Styles[workbook.Styles.Add()];
                    styleContentYellow.HorizontalAlignment = TextAlignmentType.Center;// 文字水平居中
                    styleContentYellow.VerticalAlignment = TextAlignmentType.Center;// 文字垂直居中
                    styleContentYellow.Font.Name = "微软雅黑";
                    styleContentYellow.Font.Size = 10;
                    styleContentYellow.IsTextWrapped = true;//单元格内容自动换行
                    styleContentYellow.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContentYellow.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContentYellow.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContentYellow.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    styleContentYellow.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    styleContentYellow.Pattern = BackgroundType.Solid;
                    style = styleContentYellow;
                    break;
                case "styleContentGrey":// 高亮标识_灰
                    Style styleContentGrey = workbook.Styles[workbook.Styles.Add()];
                    styleContentGrey.HorizontalAlignment = TextAlignmentType.Center;// 文字水平居中
                    styleContentGrey.VerticalAlignment = TextAlignmentType.Center;// 文字垂直居中
                    styleContentGrey.Font.Name = "微软雅黑";
                    styleContentGrey.Font.Size = 10;
                    styleContentGrey.IsTextWrapped = true;//单元格内容自动换行
                    styleContentGrey.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleContentGrey.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContentGrey.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContentGrey.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    styleContentGrey.ForegroundColor = System.Drawing.Color.FromArgb(190, 190, 190);
                    styleContentGrey.Pattern = BackgroundType.Solid;
                    style = styleContentGrey;
                    break;
                case "styleContentLeft":
                    Style styleContentLeft = workbook.Styles[workbook.Styles.Add()];
                    styleContentLeft.HorizontalAlignment = TextAlignmentType.Center;
                    styleContentLeft.VerticalAlignment = TextAlignmentType.Center;
                    styleContentLeft.Font.Name = "微软雅黑";
                    styleContentLeft.Font.Size = 10;
                    styleContentLeft.IsTextWrapped = true;//单元格内容自动换行
                    styleContentLeft.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thick;
                    styleContentLeft.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleContentLeft.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleContentLeft.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleContentLeft;
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
                    styleMemo.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
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
                case "styleExcelDownContentBJPrice":
                    Style styleExcelDownContentBJPrice = workbook.Styles[workbook.Styles.Add()];
                    styleExcelDownContentBJPrice.IsLocked = false;
                    styleExcelDownContentBJPrice.Font.Name = "微软雅黑";
                    styleExcelDownContentBJPrice.Font.Size = 10;
                    styleExcelDownContentBJPrice.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContentBJPrice.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContentBJPrice.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    styleExcelDownContentBJPrice.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style = styleExcelDownContentBJPrice;
                    break;
            }
            return style;
        }
        /// <summary>
        /// 导出报价文件
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult ExportMB()
        {
            string main_id = Request["main_id"] + "";
            string ver_id = Request["ver_id"] + "";
            string type = Request["type"] + "";
            if (string.IsNullOrEmpty(main_id) || string.IsNullOrEmpty(ver_id))
            {
                return Content(new JsonMessage { Message = "导出失败：获取报价失败！" }.ToString());
            }
            else
            {
                // 判断是否有勾选产品
                string count = DataHelper.QueryValue("select count(*) from sqm_bj_psf where choosestatus = '1' and (status <> '0' or status is null) and vrid = '" + ver_id + "' and (bgfzrid is null or bgfzrid = '1')") + "";
                if (count != "0")
                {
                    // 判断是否可生成报价文件（所有费目为“已确认”状态）
                    string bjstatus = "";
                    DataTable dt = DataHelper.QueryDataTable("select distinct bjstataus from sqm_bj_psf where mrid = '" + main_id + "' and vrid = '" + ver_id + "' and (status <> '0' or status is null) and choosestatus = '1' and (bgfzrid is null or bgfzrid = '1')");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["BJSTATAUS"] + "" == "0")
                            {
                                bjstatus = "0";
                                break;
                            }
                            else if (dr["BJSTATAUS"] + "" == "1")
                            {
                                bjstatus = "1";
                                break;
                            }
                            else if (dr["BJSTATAUS"] + "" == "3")
                            {
                                bjstatus = "3";
                                break;
                            }
                            else if (dr["BJSTATAUS"] + "" == "4")
                            {
                                bjstatus = "4";
                                break;
                            }
                        }
                        if (bjstatus == "0" || bjstatus == "1" || bjstatus == "3" || bjstatus == "4")
                        {
                            return Content(new JsonMessage { Message = "生成失败：存在未确认费目！" }.ToString());
                        }
                    }
                }
                else// 未选产品
                {
                    return Content(new JsonMessage { Message = "生成失败：未选产品！" }.ToString());
                }
            }
            try
            {
                string fileName;
                string filePath;
                string msg;
                if (type == "0")
                {
                    CreateExcel(main_id, ver_id, out fileName, out filePath, out msg, "mb");
                }
                else
                {
                    CreateExcelEng(main_id, ver_id, out fileName, out filePath, out msg, "mb");
                }

                //CreateExcelByMB(ver_id, out fileName, out filePath, out msg, "mb");
                if (msg == "生成成功")
                {
                    return Content(new JsonMessage { Message = "/Excel/output/" + fileName + ".xlsx", Success = true }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = msg, Success = false, Code = "0" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "生成失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 导出模板文件
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult ExportBJMB()
        {
            string ver_id = Request["ver_id"] + "";
            if (string.IsNullOrEmpty(ver_id))
            {
                return Content(new JsonMessage { Message = "导出失败：获取模板失败！" }.ToString());
            }
            else
            {
                // 判断是否有勾选产品
                string count = DataHelper.QueryValue("select count(*) from sqm_bj_psf where choosestatus = '1' and (status <> '0' or status is null) and vrid = '" + ver_id + "' and (bgfzrid is null or bgfzrid = '1')") + "";
                if (count == "0")
                {
                    return Content(new JsonMessage { Message = "生成失败：未选产品！" }.ToString());
                }
            }
            try
            {
                string fileName;
                string filePath;
                string msg;
                CreateExcel("", ver_id, out fileName, out filePath, out msg, "bjmb");
                if (msg == "生成成功")
                {
                    return Content(new JsonMessage { Message = "/Excel/output/" + fileName + ".xlsx", Success = true }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = msg, Success = false, Code = "0" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "生成失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 报价预览
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PreviewExc()
        {
            string main_id = Request["main_id"] + "";
            string ver_id = Request["ver_id"] + "";
            string type = Request["type"] + "";
            if (string.IsNullOrEmpty(main_id) || string.IsNullOrEmpty(ver_id))
            {
                return Content(new JsonMessage { Message = "数据异常", Success = true, Code = "0" }.ToString());
            }
            else
            {
                // 判断是否有勾选产品
                string count = DataHelper.QueryValue("select count(*) from sqm_bj_psf where choosestatus = '1' and (status <> '0' or status is null) and vrid = '" + ver_id + "' and (bgfzrid is null or bgfzrid = '1')") + "";
                if (count == "0")
                {
                    return Content(new JsonMessage { Message = "未选则产品", Success = true, Code = "0" }.ToString());
                }
            }
            try
            {
                string fileName;
                string filePath;
                string msg;
                // 生成Excel
                //CreateExcel(main_id, ver_id, out fileName, out filePath, out msg, "");
                if (type == "0")
                {
                    CreateExcel(main_id, ver_id, out fileName, out filePath, out msg, "");
                }
                else
                {
                    CreateExcelEng(main_id, ver_id, out fileName, out filePath, out msg, "");
                }
                if (msg == "生成成功")
                {
                    // Excel转html
                    string path = ExcelToHtml(fileName + ".xlsx", filePath);
                    return Content(new JsonMessage { Message = path, Success = true, Code = "1" }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = msg, Success = false, Code = "0" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "预览失败：" + ex.Message, Success = true, Code = "0" }.ToString());
            }
        }
        /// <summary>
        /// 模板预览
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PreviewMB()
        {
            string ver_id = Request["ver_id"] + "";
            if (string.IsNullOrEmpty(ver_id))
            {
                return Content(new JsonMessage { Message = "数据异常", Success = true, Code = "0" }.ToString());
            }
            else
            {
                // 判断是否有勾选产品
                string count = DataHelper.QueryValue("select count(*) from sqm_bj_psf where choosestatus = '1' and (status <> '0' or status is null) and vrid = '" + ver_id + "' and (bgfzrid is null or bgfzrid = '1')") + "";
                if (count == "0")
                {
                    return Content(new JsonMessage { Message = "未选则产品", Success = true, Code = "0" }.ToString());
                }
            }
            try
            {
                string fileName;
                string filePath;
                string msg;
                // 生成Excel
                CreateExcel("", ver_id, out fileName, out filePath, out msg, "bjmb");
                if (msg == "生成成功")
                {
                    // Excel转html
                    string path = ExcelToHtml(fileName + ".xlsx", filePath);
                    return Content(new JsonMessage { Message = path, Success = true, Code = "1" }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = msg, Success = false, Code = "0" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "预览失败：" + ex.Message, Success = true, Code = "0" }.ToString());
            }
        }
        /// <summary>
        /// 使用Excel模板生成报价模板，取数逻辑复杂，弃用
        /// 使用模板多sheet页的思路，使用combine方法，将生成的Excel合并，每个Excel文件都相当于一个sheet页
        /// </summary>
        /// <param name="vrid"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        //private void CreateExcelByMB(string vrid, out string fileName, out string filePath, out string msg, string type)
        //{
        //    List<string> productList = new List<string>();
        //    List<string> feeList = new List<string>();
        //    string feecodes = "";
        //    // 根据vrid查找产品/服务/费目
        //    string sql_psf = string.Format("select * from sqm_bj_psf where vrid = '{0}' and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1') and choosestatus = '1'", vrid);// 有效 非被包干费 勾选
        //    DataTable dtpsf = DataHelper.QueryDataTable(sql_psf);
        //    if (dtpsf.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtpsf.Rows)
        //        {
        //            // 获取产品代码，循环产品生成Excel，最后合并
        //            if (!productList.Contains(dr["PRODUCT_CODE"] + ""))
        //            {
        //                productList.Add(dr["PRODUCT_CODE"] + "");
        //            }
        //            // 获取费目代码，根据费目code查询基础得到值表位置
        //            if (!feeList.Contains(dr["FEE_CODE"] + ""))
        //            {
        //                feeList.Add(dr["FEE_CODE"] + "");
        //            }
        //        }
        //    }
        //    foreach (string feecode in feeList)
        //    {
        //        feecodes += "'" + feecode + "',";
        //    }
        //    // 查找计费因子 在sqm_modebj_val中的位置
        //    string sql_col = string.Format(@"with m1 as (
        //     select rid,
        //            feecode,
        //            calccode,
        //            calcname,
        //            valcol,
        //            case when (gdzrid is not null or gdzrid <> '') then gdzrid else djfsrid end as djfsrid
        //     from sqm_fee_calc_ref
        //     where 1 = 1
        //     and feecode in ({0})
        //     and status <> '0'
        //     order by feecode asc,djfsrid asc,valcol asc
        // )
        // select feecode,
        //        djfsrid,
        //        wm_concat(to_char(valcol) || ':' || to_char(calcname)) as colname
        //from m1
        //group by feecode,djfsrid", feecodes.TrimEnd(','));

        //}
        /// <summary>
        /// 生成Excel 拼每一个cell单元格
        /// </summary>
        /// <param name="main_id"></param>
        /// <param name="ver_id"></param>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">文件路径</param>
        private void CreateExcel(string main_id, string ver_id, out string fileName, out string filePath, out string msg, string type)
        {
            try
            {
                // 通过mrid vrid 获取fwa编号（OA报价序列号）
                DataTable dtfwa = DataHelper.QueryDataTable("select fwa from sqm_fwa_ref where mrid = '" + main_id + "' and zver = (select zver from sqm_bj_ver where rid = '" + ver_id + "') order by fwa desc");
                string fwa = "";
                string fwaS = "";// 供应链
                string fwaO = "";// 海运
                string fwaA = "";// 空运
                string fwaY = "";// 运输
                if (dtfwa.Rows.Count > 0)
                {
                    string fwaCharOld = "";
                    foreach (DataRow drfwa in dtfwa.Rows)
                    {
                        string fwaChar = (drfwa["FWA"] + "").Substring(0, 1);// 取协议号首字母
                        if (fwaCharOld == "")
                        {
                            fwa = drfwa["FWA"] + "";// 取第一个最大流水号
                            switch (fwaChar)
                            {
                                case "S": fwaS = fwa; break;
                                case "O": fwaO = fwa; break;
                                case "A": fwaA = fwa; break;
                                case "Y": fwaY = fwa; break;
                            }
                            fwaCharOld = fwaChar;
                        }
                        if (fwaCharOld != fwaChar)
                        {
                            fwa = drfwa["FWA"] + "";// 取不同首字母最大流水号
                            switch (fwaChar)
                            {
                                case "S": fwaS = fwa; break;
                                case "O": fwaO = fwa; break;
                                case "A": fwaA = fwa; break;
                                case "Y": fwaY = fwa; break;
                            }
                            fwaCharOld = fwaChar;
                        }
                    }
                }
                Workbook workbook = new Workbook();
                // 清除默认sheet页
                workbook.Worksheets.Clear();
                // 绘制Excel，每个sheet页为一个产品 每个table为一个服务
                // 得到报价信息、报价名称+客户信息+组织信息+版本信息
                string sql_bporg = @"select t1.original,t1.bjname as ""报价名称"",t2.bpname as ""客户名称"",t2.bpcode as ""客户代码"",t3.orgname as ""报价组织"",t3.orgcode as ""组织代码"",to_char(t4.dtfrom,'yyyy/mm/dd') as ""起始日期"",to_char(t4.dtto,'yyyy/mm/dd') as ""截止日期"",t4.CONTRSCTNUM
from SQM_BJ_MAIN_BASIC t1 left join sqm_bj_bp t2 on t1.rid = t2.mrid left join sqm_bj_org t3 on t1.rid = t3.mrid left join sqm_bj_ver t4 on t1.rid = t4.mrid where t1.rid = '" + main_id + "'";
                DataTable bjDt = DataHelper.QueryDataTable(sql_bporg);
                string bnname = "";
                string bpcode = "";
                string orgname = "";
                string orgcode = "";
                string original = "";
                if (bjDt.Rows.Count > 0)
                {
                    bnname = bjDt.Rows[0]["客户名称"] + "";
                    bpcode = bjDt.Rows[0]["客户代码"] + "";
                    orgname = bjDt.Rows[0]["报价组织"] + "";
                    orgcode = bjDt.Rows[0]["组织代码"] + "";
                    original = bjDt.Rows[0]["ORIGINAL"] + "";
                }
                if (orgcode.IndexOf("-") >= 0)
                {
                    orgcode = orgcode.Split('-')[0];
                }

                // 从crm得到客户联系人姓名以及联系方式
                string lxrname = "";
                string lxrphone = "";
                string namephone = "";
                if (type != "bjmb")
                {
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    DataTable dtcrmbp = DataHelper.QueryDataTable(string.Format(@"select t1.name,wm_concat(to_char(t2.content)) as telephone
from CRM_CUS_CONTACTER t1 left
join CRM_CUS_CON_CONTACTWAY t2 on t1.id = t2.CONTACTERID
where t1.CUSTOMERNO = '{0}'
and t2.communtools in ('手机', '座机')
group by t1.name", bpcode), conn);
                    if (dtcrmbp.Rows.Count > 0)
                    {
                        lxrname = dtcrmbp.Rows[0]["NAME"] + "";
                        lxrphone = dtcrmbp.Rows[0]["TELEPHONE"] + "";
                        namephone = lxrname + "/" + lxrphone;
                    }
                }
                // 得到产品信息***
                string templateName = "";// 报价模板名称
                string sql_pro = "";
                if (type == "bjmb")
                {
                    sql_pro = "select distinct PRODUCT_CODE,PRODUCT_NAME from sqm_bj_psf where vrid ='" + ver_id + "' and choosestatus = '1' and (bgfzrid is null or bgfzrid = '1')";
                }
                else
                {
                    sql_pro = "select distinct PRODUCT_CODE,PRODUCT_NAME from sqm_bj_psf where vrid ='" + ver_id + "' and choosestatus = '1' and bjstataus <> '0' and (bgfzrid is null or bgfzrid = '1')";
                }
                IList<EasyDictionary> listpro = DataHelper.QueryDictList(sql_pro);
                int originalCount = listpro.Count;// sheet页页数，如果存在附页就originalCount增加--适应新需求：异常费目跟Atcost费目单独列出来一个页面 => 现在改为atcost才放在附页，修改时间为2018-12-28
                int prdCount = listpro.Count;
                string gylsign = "0";// 供应链服务：0-存在一种  1-同时存在
                List<int> prdindex = new List<int>();// 记录产生附页时的索引，服务中异常服务也可采用这种方式，但是采用的是第二种方式：遍历时无论有无异常均给服务列表加一，相当于容量扩大一倍，然后第一次遍历只查询常规费目，第二次只查询异常费目，这种方法也可达成预期，只是程序执行时间加倍
                for (int p = 0; p < prdCount; p++)
                {
                    int sequence = 1;// 第一列的序号
                    workbook.Worksheets.Add("sheet");// 新建sheet页
                    Worksheet worksheet = workbook.Worksheets[p];
                    Cells cells = worksheet.Cells;
                    string product_name = "";
                    string product_code = "";
                    string businessorg = "";// 根据产品code获取事业部
                    bool iffy = false;// 是否附页
                    if (p >= originalCount)// 有附页--有Atcost费目,或者供应链有增值服务
                    {
                        iffy = true;
                        int index = prdindex[p - originalCount];
                        product_name = (listpro[index].Get("PRODUCT_NAME") + "").Replace("/", "&").Replace("*", "|"); // 这种写法有问题，如果有两个产品，第一个产品不存在附页，第二个产品存在附页，此时这种方式会生成第一个产品的附页。解决方法：产品页有附页产生时，List记录当前索引，当遍历完原产品时，依次遍历存到List里的索引
                        product_code = listpro[index].Get("PRODUCT_CODE") + "";
                        businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", product_code)) + "";// 根据产品code获取事业部
                        if (businessorg == "供应链")// 供应链的附页都是增值服务
                        {
                            worksheet.Name = product_name + "(增值)";// 改sheet名称
                        }
                        else
                        {
                            worksheet.Name = product_name + "(附页)";// 改sheet名称
                        }
                    }
                    else
                    {
                        product_name = (listpro[p].Get("PRODUCT_NAME") + "").Replace("/", "&").Replace("*", "|");
                        product_code = listpro[p].Get("PRODUCT_CODE") + "";
                        businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", product_code)) + "";// 根据产品code获取事业部

                        if (businessorg == "供应链")// 如果该产品是供应链，则判断是常规还是增值，如果只有常规或者只有增值sheet页数不变，如果同时存在则sheet页加1
                        {
                            string count = DataHelper.QueryValue(string.Format("select count(*) from sqm_bj_psf t1,sqm_hkydic t2 where t1.service_code = t2.code and t2.type = 'gyl' and t1.product_code = '{0}' and t1.vrid = '{1}'", product_code, ver_id)) + "";
                            if (count != "0")// 有常规服务
                            {
                                string countsbf = DataHelper.QueryValue(string.Format("select count(*) from sqm_bj_psf where vrid = '{0}' and product_code = '{1}'", ver_id, product_code)) + "";
                                if (count == countsbf)// 只有常规服务
                                {
                                    gylsign = "0";
                                    worksheet.Name = product_name + "(常规)";// 改sheet名称
                                }
                                else // 同时存在，sheet+1
                                {
                                    gylsign = "1";
                                    prdindex.Add(p);
                                    prdCount += 1;
                                    worksheet.Name = product_name + "(常规)";// 改sheet名称
                                }
                            }
                            else// 只有增值
                            {
                                gylsign = "0";
                                worksheet.Name = product_name + "(增值)";// 改sheet名称
                            }
                        }
                        else// 如果是非供应链，则判断是否有atcost费目，如果存在atcost则sheet页数加1，这里不存在只有atcost费目的情况
                        {
                            // 判断该产品下是否存在Atcost费目
                            string sql_config = @"select count(*) from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' ";
                            string count = DataHelper.QueryValue(string.Format(sql_config + " and t2.feecatg = '2' ", ver_id)) + "";// and (t2.feecatg = '1' or t2.feecatg = '2')  1-异常费目 2-atcost
                            if (count != "0")// 有附页
                            {
                                prdindex.Add(p);
                                prdCount += 1;
                            }
                            worksheet.Name = product_name;// 改sheet名称
                        }
                    }

                    // 绘制表头
                    // 插入图片
                    //string sURL = "";
                    //System.Net.WebClient objWebClient = new System.Net.WebClient();
                    //System.IO.MemoryStream objImage = new MemoryStream(objWebClient.DownloadData(sURL));
                    //string height;
                    //string width;
                    //System.IO.Stream objImage = getStream(Server.MapPath("/Excel/Templete/toptitle.png"), out height, out width);
                    //Aspose.Cells.Drawing.PictureCollection picture = worksheet.Pictures;
                    //worksheet.Pictures.Add(0, 0, 2, 8, Server.MapPath("/Excel/Templete/toptitle.png"));
                    //picture.Add(0, 0, 2, 8, objImage);  // 以流的方式插入图片
                    //picture.Add(0, 0, 2, 8, Server.MapPath("/Excel/Templete/toptitle.png"));// 直接使用图片路径
                    //cells.Merge(0, 0, 2, 8);// 合并列

                    // 开始绘制
                    #region 绘制台头信息
                    int rowIndex = 0;
                    if (type == "bjmb")
                    {
                        // 查询模板名称
                        templateName = DataHelper.QueryValue("select distinct templatename from sqm_bjmb where verid = '" + ver_id + "'") + "";
                        // 表头需要合并单元格
                        cells.Merge(0, 0, 2, 8);// 合并列 从第0行开始  模板名称
                        //设置列宽
                        cells.SetColumnWidth(0, 6);
                        cells.SetColumnWidth(1, 16);
                        cells.SetColumnWidth(2, 18);
                        cells.SetColumnWidth(3, 6.5);
                        cells.SetColumnWidth(4, 6.5);
                        cells.SetColumnWidth(5, 6.5);
                        cells.SetColumnWidth(6, 14);
                        cells.SetColumnWidth(7, 13);
                        // 设置表头值
                        cells[0, 0].PutValue(templateName);
                        cells[0, 0].SetStyle(getStyle("styleTitle1"));
                        rowIndex = 4;
                    }
                    else
                    {
                        // 表头需要合并单元格
                        cells.Merge(2, 0, 2, 8);// 合并列 从第3行开始
                        cells.Merge(4, 0, 2, 8);
                        cells.Merge(6, 0, 2, 8);
                        cells.Merge(8, 0, 1, 4); cells.Merge(8, 4, 1, 4);
                        cells.Merge(9, 0, 1, 4); cells.Merge(9, 4, 1, 4);
                        cells.Merge(10, 0, 1, 8); // 报价编号
                        cells.Merge(11, 0, 1, 4); cells.Merge(11, 4, 1, 4);
                        cells.Merge(12, 0, 1, 8);
                        cells.Merge(13, 0, 1, 8);
                        //设置列宽
                        cells.SetColumnWidth(0, 6);
                        cells.SetColumnWidth(1, 12);
                        cells.SetColumnWidth(2, 13);
                        cells.SetColumnWidth(3, 6.5);
                        cells.SetColumnWidth(4, 11);
                        cells.SetColumnWidth(5, 6.5);
                        cells.SetColumnWidth(6, 14);
                        cells.SetColumnWidth(7, 13);
                        // 设置表头值
                        cells[2, 0].PutValue("");
                        cells[4, 0].PutValue(bjDt.Rows[0]["报价名称"] + "");
                        cells[6, 0].PutValue("");
                        cells[8, 0].PutValue("客户名称:" + bnname); cells[8, 4].PutValue("客户联系人及电话:" + namephone);
                        cells[9, 0].PutValue("报价方:" + orgname); cells[9, 4].PutValue("报价人及电话:");
                        // 判断事业部
                        if (businessorg == "空运")
                        {
                            cells[10, 0].PutValue("报价编号:" + fwaA);
                        }
                        else if (businessorg == "海运")
                        {
                            cells[10, 0].PutValue("报价编号:" + fwaO);
                        }
                        else if (businessorg == "供应链")
                        {
                            cells[10, 0].PutValue("报价编号:" + fwaS);
                        }
                        else if (businessorg == "运输")
                        {
                            cells[10, 0].PutValue("报价编号:" + fwaY);
                        }
                        else
                        {
                            cells[10, 0].PutValue("报价编号:");
                        }
                        //cells[10, 4].PutValue("报价日期:" + DateTime.Now.ToString("yyyy/MM/dd"));
                        if (bjDt.Rows[0]["起始日期"] + "" != "" && bjDt.Rows[0]["截止日期"] + "" != "")
                        {
                            cells[11, 0].PutValue("报价执行日期:" + bjDt.Rows[0]["起始日期"] + "-" + bjDt.Rows[0]["截止日期"]);
                        }
                        else
                        {
                            cells[11, 0].PutValue("报价执行日期:");
                        }
                        cells[11, 4].PutValue("付款期:");
                        cells[12, 0].PutValue("币种：人民币");
                        cells[13, 0].PutValue("  ");
                        for (var i = 0; i < 8; i++)
                        {
                            cells[2, i].SetStyle(getStyle("styleTitle1"));
                            cells[3, i].SetStyle(getStyle("styleTitle1"));
                            cells[4, i].SetStyle(getStyle("styleTitle1"));
                            cells[5, i].SetStyle(getStyle("styleTitle1"));
                            cells[6, i].SetStyle(getStyle("styleTitle3"));
                            cells[7, i].SetStyle(getStyle("styleTitle3"));
                            cells[8, i].SetStyle(getStyle("styleTitle4"));
                            cells[9, i].SetStyle(getStyle("styleTitle4"));
                            cells[10, i].SetStyle(getStyle("styleTitle4"));
                            cells[11, i].SetStyle(getStyle("styleTitle4"));
                            cells[12, i].SetStyle(getStyle("styleTitle4"));
                        }
                        rowIndex = 14;
                    }
                    #endregion
                    // 绘制服务
                    // 得到服务信息***
                    string sql_ser = @"select distinct t1.SERVICE_CODE,t1.SERVICE_NAME from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' ";
                    string sql_ser_gyl = @"select distinct SERVICE_CODE,SERVICE_NAME from sqm_bj_psf where vrid = '" + ver_id + "' and product_code = '" + product_code + "' and service_code is not null and service_name is not null ";
                    // 2018-12-28新需求，标准费目sheet页展示异常费目，与之前只在附页展示异常费目不同，而且，如果有附页，还可能是供应链增值服务
                    if (iffy)
                    {
                        if (businessorg == "供应链")// 只有增值
                        {
                            sql_ser_gyl += " and service_code not in(select code from sqm_hkydic where type = 'gyl')";
                        }
                        else
                        {
                            sql_ser += " and t1.product_code = '" + product_code + "' and t1.service_code is not null and t1.service_name is not null and (t1.bgfzrid is null or t1.bgfzrid = '1') and t2.feecatg = '2'";
                        }
                    }
                    else
                    {
                        if (businessorg == "供应链")// 只有常规或者只有增值
                        {
                            if (gylsign == "1")// 同时存在时，只取常规服务
                            {
                                sql_ser_gyl += " and service_code in(select code from sqm_hkydic where type = 'gyl')";
                            }
                        }
                        else
                        {
                            sql_ser += " and t1.product_code = '" + product_code + "' and t1.service_code is not null and t1.service_name is not null and (t1.bgfzrid is null or t1.bgfzrid = '1') and (t2.feecatg = '0' or t2.feecatg = '1')";
                        }
                    }
                    sql_ser = businessorg == "供应链" ? sql_ser_gyl : sql_ser;
                    IList<EasyDictionary> listser = DataHelper.QueryDictList(string.Format(sql_ser, ver_id));
                    bool ifTitle = true;// 只绘制一次标题
                    // 填数之前填空行
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    // 绘制标题
                    cells[rowIndex, 0].PutValue("序号");
                    cells[rowIndex, 1].PutValue("服务");
                    cells[rowIndex, 2].PutValue("费目");
                    cells[rowIndex, 3].PutValue("报价单位");
                    cells[rowIndex, 4].PutValue("单价");
                    cells[rowIndex, 5].PutValue("最低收费");
                    cells[rowIndex, 6].PutValue("费用说明");
                    cells[rowIndex, 7].PutValue("包干说明");
                    for (int colIndex = 0; colIndex < 8; colIndex++)
                    {
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleTitle5"));
                    }
                    //cells.SetRowHeight(rowIndex, 20);//设置行高
                    rowIndex++;
                    int origisrvcount = listser.Count;
                    int srvcount = listser.Count;
                    for (int s = 0; s < srvcount; s++)
                    {
                        bool ifatcost = false;
                        string service_code = "";
                        if (s >= origisrvcount)
                        {
                            service_code = listser[s - origisrvcount].Get("SERVICE_CODE") + "";
                            ifatcost = true;// 开关关闭，表示第二次经过该服务（服务下费目是异常费目），需要打印输出
                        }
                        else
                        {
                            service_code = listser[s].Get("SERVICE_CODE") + "";
                        }
                        string sql = string.Empty;
                        //rowIndex++;
                        int lastRowIndex = rowIndex;// 记录上一个服务开始的行索引，进行合并单元格操作  // 服务列与费用说明列相同的合并规则
                        // 得到费目信息***
                        string sql_common = @" from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' 
and t1.product_code = '{1}'
and t1.service_code = '{2}' ";
                        string sql_gyl = "select RID, FEE_CODE, FEE_NAME, BJSTATAUS from sqm_bj_psf where vrid = '{0}' and product_code = '{1}' and service_code = '{2}'";
                        string sql_fee = "select t1.RID, t1.FEE_CODE, t1.FEE_NAME, t1.BJSTATAUS " + sql_common;
                        string sql_check = "select distinct t2.feecatg " + sql_common;
                        if (iffy)// 产品附页展示  2018-12-18新需求：附页只展示atcost 
                        {
                            if (businessorg == "供应链")
                            {
                                sql_gyl += " and service_code not in(select code from sqm_hkydic where type = 'gyl') ";
                            }
                            else
                            {
                                DataTable dt = DataHelper.QueryDataTable(string.Format(sql_check + " and t2.feecatg = '2'", ver_id, product_code, service_code));
                                if (dt.Rows.Count > 0)
                                {
                                    sql_fee += " and t2.feecatg = '2'";
                                }
                            }

                        }
                        else
                        {
                            if (businessorg == "供应链")
                            {
                                if (gylsign == "1")// 同时存在时，只取常规服务
                                {
                                    sql_gyl += " and service_code in(select code from sqm_hkydic where type = 'gyl')";
                                }
                            }
                            else
                            {
                                // 服务长度加倍，第一次遍历只遍历常规费目，第二次只遍历异常费目
                                DataTable dt = DataHelper.QueryDataTable(string.Format(sql_check + " and (t2.feecatg = '1' or t2.feecatg = '0')", ver_id, product_code, service_code));
                                if (dt.Rows.Count > 0)
                                {
                                    if (ifatcost)// 第二次（遍历服务）只查异常
                                    {
                                        sql_fee += " and t2.feecatg = '1'";
                                    }
                                    else// 第一次只查标准
                                    {
                                        srvcount += 1;
                                        sql_fee += " and t2.feecatg = '0'";
                                    }
                                }
                                else
                                {
                                    sql_fee += " and (t2.feecatg = '0' or t2.feecatg = '1')";
                                }
                            }
                        }
                        sql_fee = businessorg == "供应链" ? sql_gyl : sql_fee;
                        IList<EasyDictionary> listpsf = DataHelper.QueryDictList(string.Format(sql_fee, ver_id, product_code, service_code));
                        // 判断是否有“运输路线”列  为了生成的Excel列数对齐（同一服务，不同计费基础的费目保持列数一致，这需求真特么不合理） 改版，最新版本中无“运输路线”
                        //int colnum = 6;
                        foreach (EasyDictionary dic in listpsf)
                        {
                            int lastRowIndexFee = rowIndex;
                            string bjstatus = dic.Get("BJSTATAUS") + "";// 是否保存到报价值表，预览数据至少是已保存数据
                            if (bjstatus == "0")
                            {
                                continue;
                            }
                            string feecode = dic.Get("FEE_CODE") + "";
                            string bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where product_code = '" + product_code + "' and service_code = '" + service_code + "' and fee_code = '" + feecode + "' and vrid = '" + ver_id + "'") + "";
                            if (string.IsNullOrEmpty(bjfs))
                            {
                                bjfs = "0";
                            }
                            string djfsrid = "";
                            string gdzrid = "";
                            IList<EasyDictionary> ediclist = DataHelper.QueryDictList("select distinct DJFSRID from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID <> '' or DJFSRID is not null)");// DJFSRID不为空
                            DataTable dtdjfswjc = DataHelper.QueryDataTable("select distinct djfsrid from sqm_fee_pur_ref where djfsrid not in(select djfsrid from sqm_fee_calc_ref where feecode = '" + feecode + "') and feecode = '" + feecode + "'");// 定价方式无基础
                            if ((ediclist.Count > 0 && bjfs == "0") || (dtdjfswjc.Rows.Count > 0 && bjfs == "0"))
                            {
                                foreach (EasyDictionary ed in ediclist)// 遍历定价方式
                                {
                                    djfsrid = ed.Get("DJFSRID") + "";
                                    // 是否高低值
                                    IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID from SQM_FEE_PUR_REF where STATUS = '1' and DJFSRID = '" + djfsrid + "'");
                                    if (gdzlist.Count > 0)
                                    {
                                        // 遍历高低值
                                        foreach (EasyDictionary gdz in gdzlist)
                                        {
                                            gdzrid = gdz.Get("GDZRID") + "";
                                            if (gdzrid != "")
                                            {
                                                sql = getFeeSql(ver_id, service_code, product_code, feecode, "", djfsrid, gdzrid);
                                                if (sql != "")
                                                {
                                                    DrawExcel(cells, ref rowIndex, sql, ref ifTitle, 0);
                                                }
                                            }
                                            else // 无高低值
                                            {
                                                sql = getFeeSql(ver_id, service_code, product_code, feecode, "", djfsrid, "");
                                                if (sql != "")
                                                {
                                                    DrawExcel(cells, ref rowIndex, sql, ref ifTitle, 0);
                                                }
                                            }
                                        }
                                    }
                                    else // 无高低值
                                    {
                                        sql = getFeeSql(ver_id, service_code, product_code, feecode, "", djfsrid, "");
                                        if (sql != "")
                                        {
                                            DrawExcel(cells, ref rowIndex, sql, ref ifTitle, 0);
                                        }
                                    }
                                }
                                foreach (DataRow dr in dtdjfswjc.Rows)
                                {
                                    string djfsrid2 = dr["DJFSRID"] + "";
                                    sql = getFeeSql(ver_id, service_code, product_code, feecode, "", djfsrid2, "");
                                    if (sql != "")
                                    {
                                        DrawExcel(cells, ref rowIndex, sql, ref ifTitle, 0);
                                    }
                                }
                            }
                            else// 定价方式为空（正式库应该没有定价方式为空的数据）  或者是 定价方式没有基础的费目 或者是 ATCOST 单票单询
                            {
                                sql = getFeeSql(ver_id, service_code, product_code, feecode, "", "", "");
                                if (sql != "")
                                {
                                    DrawExcel(cells, ref rowIndex, sql, ref ifTitle, 0);
                                }
                            }
                            // 添加包干费数据-添加行的方式，弃用
                            //DrawExcelForBGF(cells,ver_id, service_code, product_code, feecode, ref rowIndex);
                            // 合并单元格 费目合并 ，包干费合并
                            if (rowIndex - lastRowIndexFee > 1)// 1行合并单元格导致行高不能自适应，内容压缩，所以只有1行时不执行合并单元格操作
                            {
                                cells.Merge(lastRowIndexFee, 2, rowIndex - lastRowIndexFee, 1);
                                cells.Merge(lastRowIndexFee, 7, rowIndex - lastRowIndexFee, 1);
                            }
                            // 行高自动
                            //Thread.Sleep((int)(500));
                            //AutoFitterOptions ao = new AutoFitterOptions();
                            //ao.AutoFitMergedCells = false;
                            //worksheet.AutoFitRows(ao);
                        }
                        // 合并单元格 服务合并 序号合并
                        if (rowIndex - lastRowIndex > 1)
                        {
                            cells.Merge(lastRowIndex, 0, rowIndex - lastRowIndex, 1);// 合并序号列  
                            cells[lastRowIndex, 0].PutValue(NumberToChinese(sequence));// 处理序号
                            sequence++;// 使用一次，自增一次
                            cells.Merge(lastRowIndex, 1, rowIndex - lastRowIndex, 1);// 合并服务列
                        }
                        else if (rowIndex - lastRowIndex == 1)// 如果相等则说明该服务下没有“已保存”数据，则序号不增加
                        {
                            cells[lastRowIndex, 0].PutValue(NumberToChinese(sequence));// 处理序号
                            sequence++;// 使用一次，自增一次
                        }
                    }
                    // 绘制尾部
                    // 填空行
                    for (int i = 0; i < 9; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    // 备注标题
                    cells[rowIndex, 0].PutValue("备注：");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    for (int i = 0; i < 5; i++)
                    {
                        cells[rowIndex, i].SetStyle(getStyle("styleMemo"));
                    }
                    rowIndex++;
                    // 备注内容
                    cells[rowIndex, 0].PutValue("1.此报价含税");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("2.上述报价未提及的服务项目,如在操作中实际发生,须双方另行商议确定");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    //10-24  DLC 屏蔽
                    //cells[rowIndex, 0].PutValue("3.运费支付方式为票结30天；账期以出具账单日开始计算,如超过账期未支付,则加收3%的滞纳金");
                    //cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    //cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    //rowIndex++;

                    cells[rowIndex, 0].PutValue("3.此报价单经双方书面同意后执行,并等同于双方合同,具备同等法律效力");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("4.因本报价单引发的所有争议双方协商不成的,提交江苏飞力达国际物流股份有限公司所在地法院审理");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("5.如贵司所接受的服务当中包含内陆运输项目的,我司单次最高赔付的金额为人民币五百万元;贵司涉及内陆运输的项目单次货物价值高于五百万元的,应当向我司书面申报确定;贵司应当就超额货物自行购买单票险或支付保费由我司代买;贵司如既不同意支付额外保费又拒绝自行购买保险的,我司将按照单次运输最高五百万元来赔付,并因此享有责任免除");
                    cells.Merge(rowIndex, 0, 4, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    // 签名
                    cells[rowIndex, 0].PutValue("报价方盖章：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    if (orgcode != "")
                    {
                        string fileExistPath = Server.MapPath("/Excel/Templete/" + orgcode + ".png");
                        if (CheckFileExist(fileExistPath))
                        {
                            worksheet.Pictures.Add(rowIndex, 0, fileExistPath);// 盖章 根据组织代码盖章
                        }
                    }
                    cells[rowIndex, 4].PutValue("客户盖章：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("签  字：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    cells[rowIndex, 4].PutValue("签  字：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("日  期：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    cells[rowIndex, 4].PutValue("日  期：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    rowIndex++;

                    // 转pdf 中文变成四方块解决方法  必须设置字体 具体原因不详
                    for (int i = 2; i <= rowIndex; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            string str = cells[i, j].Value + "";
                            if (str.Length >= 1)
                            {
                                FontSetting charactor = cells[i, j].Characters(0, str.Length);
                                charactor.Font.Name = "宋体";// 微软雅黑 不行  黑体、宋体
                            }
                        }
                    }
                    if (type != "bjmb")
                    {
                        worksheet.Pictures.Add(0, 6, Server.MapPath("/Excel/Templete/toptitle.png"));// 插图 Excel97-2003 Excel生成文件损坏，不知道是电脑的原因还是什么原因，改用xlsx
                    }
                    // 设置页眉页脚 添加picture失败，可能是 读出来的byte[]不对，也可能是因为用了盗版Aspose T_T
                    //Aspose.Cells.PageSetup pageSetup = worksheet.PageSetup;
                    //pageSetup.SetHeaderPicture(2, getBytes(Server.MapPath("/Excel/Templete/Righttitle.png")));
                    //pageSetup.SetHeader(0, "&N");
                    // 行高自动--按未合并行来自适应，所以最后一列合并行内容无法自适应
                    AutoFitterOptions ao = new AutoFitterOptions();
                    //ao.AutoFitMergedCells = true;
                    //ao.IgnoreHidden = true;
                    ao.OnlyAuto = true;
                    worksheet.AutoFitRows(ao);
                }

                // 1 生成Excel文件
                string createTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string createPath = Server.MapPath("/Excel/output/");
                if (type == "bjmb")
                {
                    fileName = RegexReplace(templateName) + createTime;
                }
                else
                {
                    fileName = RegexReplace(bjDt.Rows[0]["报价名称"] + "") + createTime;
                }
                string createName = fileName + ".pdf";
                filePath = System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".xlsx");
                //workbook.Save(filePath, SaveFormat.Excel97To2003);
                workbook.Save(filePath, SaveFormat.Xlsx);
                // 2 生成pdf option
                Aspose.Cells.PdfSaveOptions xlsSaveOption = new Aspose.Cells.PdfSaveOptions();
                #region 设置字体
                //xlsSaveOption.DefaultFont = "MingLiu";
                //xlsSaveOption.DefaultFont = "MS Gothic";
                //xlsSaveOption.DefaultFont = "Microsoft YaHei";
                //xlsSaveOption.DefaultFont = "FangSong_GB2312";
                //xlsSaveOption.SecurityOptions = new Aspose.Cells.Rendering.PdfSecurity.PdfSecurityOptions();
                #endregion
                #region pdf 加密
                //Set the user password
                //xlsSaveOption.SecurityOptions.UserPassword = "1111";
                //Set the owner password
                //xlsSaveOption.SecurityOptions.OwnerPassword = "1111";
                //Disable extracting content permission
                //xlsSaveOption.SecurityOptions.ExtractContentPermission = false;
                //Disable print permission
                //xlsSaveOption.SecurityOptions.PrintPermission = false;
                #endregion
                xlsSaveOption.OnePagePerSheet = true;// 一个sheet页一张pdf页
                xlsSaveOption.ValidateMergedAreas = true;

                string filePath1 = System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".pdf");

                // 生成pdf
                // 1
                //workbook.Save(filePath1, xlsSaveOption);
                // 2
                //Workbook wb = new Workbook(filePath);
                //wb.Save(System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".pdf"), SaveFormat.Pdf);
                // 3
                Workbook workbook2 = new Workbook();
                workbook2.Open(filePath, FileFormatType.Xlsx);
                foreach (Worksheet worksheet in workbook2.Worksheets)
                {
                    worksheet.AutoFitRows();
                }
                workbook2.Save(Path.ChangeExtension(filePath, ".pdf"), SaveFormat.Pdf);
                DealWithPdf(filePath1);// 处理空白页--没有图片的都被视为空白页了

                // 存路径
                if (type == "mb" || type == "bjmb")
                {
                    DataHelper.ExecSql("update sqm_bj_ver set UPLOADTIME = to_date('" + createTime + "','yyyy-mm-dd hh24:mi:ss'),UPLOADNAME = '" + createName + "',UPLOADURL = '" + createPath + "',SHOWMODE = '0' where rid = '" + ver_id + "'");
                    msg = "生成成功";
                }
                else
                {
                    msg = "生成成功";
                }
            }
            catch (Exception ex)
            {
                fileName = "";
                filePath = "";
                msg = ex.Message;
            }
        }
        private void CreateExcelEng(string main_id, string ver_id, out string fileName, out string filePath, out string msg, string type)
        {
            try
            {
                // 通过mrid vrid 获取fwa编号（OA报价序列号）
                DataTable dtfwa = DataHelper.QueryDataTable("select fwa from sqm_fwa_ref where mrid = '" + main_id + "' and zver = (select zver from sqm_bj_ver where rid = '" + ver_id + "') order by fwa desc");
                string fwa = "";
                string fwaS = "";// 供应链
                string fwaO = "";// 海运
                string fwaA = "";// 空运
                string fwaY = "";// 运输
                if (dtfwa.Rows.Count > 0)
                {
                    string fwaCharOld = "";
                    foreach (DataRow drfwa in dtfwa.Rows)
                    {
                        string fwaChar = (drfwa["FWA"] + "").Substring(0, 1);// 取协议号首字母
                        if (fwaCharOld == "")
                        {
                            fwa = drfwa["FWA"] + "";// 取第一个最大流水号
                            switch (fwaChar)
                            {
                                case "S": fwaS = fwa; break;
                                case "O": fwaO = fwa; break;
                                case "A": fwaA = fwa; break;
                                case "Y": fwaY = fwa; break;
                            }
                            fwaCharOld = fwaChar;
                        }
                        if (fwaCharOld != fwaChar)
                        {
                            fwa = drfwa["FWA"] + "";// 取不同首字母最大流水号
                            switch (fwaChar)
                            {
                                case "S": fwaS = fwa; break;
                                case "O": fwaO = fwa; break;
                                case "A": fwaA = fwa; break;
                                case "Y": fwaY = fwa; break;
                            }
                            fwaCharOld = fwaChar;
                        }
                    }
                }
                Workbook workbook = new Workbook();
                // 清除默认sheet页
                workbook.Worksheets.Clear();
                // 绘制Excel，每个sheet页为一个产品 每个table为一个服务
                // 得到报价信息、报价名称+客户信息+组织信息+版本信息
                string sql_bporg = @"select t1.original,t1.bjname as ""报价名称"",t2.bpname as ""客户名称"",t2.bpcode as ""客户代码"",t3.orgname as ""报价组织"",t3.orgcode as ""组织代码"",to_char(t4.dtfrom,'yyyy/mm/dd') as ""起始日期"",to_char(t4.dtto,'yyyy/mm/dd') as ""截止日期"",t4.CONTRSCTNUM
from SQM_BJ_MAIN_BASIC t1 left join sqm_bj_bp t2 on t1.rid = t2.mrid left join sqm_bj_org t3 on t1.rid = t3.mrid left join sqm_bj_ver t4 on t1.rid = t4.mrid where t1.rid = '" + main_id + "'";
                DataTable bjDt = DataHelper.QueryDataTable(sql_bporg);
                string bnname = "";
                string bpcode = "";
                string orgname = "";
                string orgcode = "";
                string original = "";
                if (bjDt.Rows.Count > 0)
                {
                    bnname = bjDt.Rows[0]["客户名称"] + "";
                    bpcode = bjDt.Rows[0]["客户代码"] + "";
                    string bjzzsql = @"select * from mdm_org where ORGKEY='" + orgcode + "' and LANGTYPE='E'";
                    var bjzz = DataHelper.QueryDataTable(bjzzsql);
                    if (bjzz.Rows.Count > 0)
                    {
                        orgname = bjzz.Rows[0]["ORGNAME"] + "";
                    }
                    else
                    {
                        orgname = bjDt.Rows[0]["报价组织"] + "";
                    }
                    orgcode = bjDt.Rows[0]["组织代码"] + "";
                    original = bjDt.Rows[0]["ORIGINAL"] + "";
                }
                if (orgcode.IndexOf("-") >= 0)
                {
                    orgcode = orgcode.Split('-')[0];
                }

                // 从crm得到客户联系人姓名以及联系方式
                string lxrname = "";
                string lxrphone = "";
                string namephone = "";
                if (type != "bjmb")
                {
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    DataTable dtcrmbp = DataHelper.QueryDataTable(string.Format(@"select t1.name,wm_concat(to_char(t2.content)) as telephone
from CRM_CUS_CONTACTER t1 left
join CRM_CUS_CON_CONTACTWAY t2 on t1.id = t2.CONTACTERID
where t1.CUSTOMERNO = '{0}'
and t2.communtools in ('手机', '座机')
group by t1.name", bpcode), conn);
                    if (dtcrmbp.Rows.Count > 0)
                    {
                        lxrname = dtcrmbp.Rows[0]["NAME"] + "";
                        lxrphone = dtcrmbp.Rows[0]["TELEPHONE"] + "";
                        namephone = lxrname + "/" + lxrphone;
                    }
                }
                // 得到产品信息***
                string templateName = "";// 报价模板名称
                string sql_pro = "";
                if (type == "bjmb")
                {
                    sql_pro = "select distinct PRODUCT_CODE,PRODUCT_NAME from sqm_bj_psf where vrid ='" + ver_id + "' and choosestatus = '1' and (bgfzrid is null or bgfzrid = '1')";
                }
                else
                {
                    sql_pro = "select distinct PRODUCT_CODE,PRODUCT_NAME from sqm_bj_psf where vrid ='" + ver_id + "' and choosestatus = '1' and bjstataus <> '0' and (bgfzrid is null or bgfzrid = '1')";
                }
                IList<EasyDictionary> listpro = DataHelper.QueryDictList(sql_pro);
                int originalCount = listpro.Count;// sheet页页数，如果存在附页就originalCount增加--适应新需求：异常费目跟Atcost费目单独列出来一个页面 => 现在改为atcost才放在附页，修改时间为2018-12-28
                int prdCount = listpro.Count;
                string gylsign = "0";// 供应链服务：0-存在一种  1-同时存在
                List<int> prdindex = new List<int>();// 记录产生附页时的索引，服务中异常服务也可采用这种方式，但是采用的是第二种方式：遍历时无论有无异常均给服务列表加一，相当于容量扩大一倍，然后第一次遍历只查询常规费目，第二次只查询异常费目，这种方法也可达成预期，只是程序执行时间加倍
                for (int p = 0; p < prdCount; p++)
                {
                    int sequence = 1;// 第一列的序号
                    workbook.Worksheets.Add("sheet");// 新建sheet页
                    Worksheet worksheet = workbook.Worksheets[p];
                    Cells cells = worksheet.Cells;
                    string product_name = "";
                    string product_code = "";
                    string businessorg = "";// 根据产品code获取事业部
                    bool iffy = false;// 是否附页
                    if (p >= originalCount)// 有附页--有Atcost费目,或者供应链有增值服务
                    {
                        iffy = true;
                        int index = prdindex[p - originalCount];
                        product_name = (listpro[index].Get("PRODUCT_NAME") + "").Replace("/", "&").Replace("*", "|"); // 这种写法有问题，如果有两个产品，第一个产品不存在附页，第二个产品存在附页，此时这种方式会生成第一个产品的附页。解决方法：产品页有附页产生时，List记录当前索引，当遍历完原产品时，依次遍历存到List里的索引
                        product_code = listpro[index].Get("PRODUCT_CODE") + "";
                        businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", product_code)) + "";// 根据产品code获取事业部
                        if (businessorg == "供应链")// 供应链的附页都是增值服务
                        {
                            worksheet.Name = product_name + "(增值)";// 改sheet名称
                        }
                        else
                        {
                            worksheet.Name = product_name + "(附页)";// 改sheet名称
                        }
                    }
                    else
                    {
                        product_name = (listpro[p].Get("PRODUCT_NAME") + "").Replace("/", "&").Replace("*", "|");
                        product_code = listpro[p].Get("PRODUCT_CODE") + "";
                        businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", product_code)) + "";// 根据产品code获取事业部

                        if (businessorg == "供应链")// 如果该产品是供应链，则判断是常规还是增值，如果只有常规或者只有增值sheet页数不变，如果同时存在则sheet页加1
                        {
                            string count = DataHelper.QueryValue(string.Format("select count(*) from sqm_bj_psf t1,sqm_hkydic t2 where t1.service_code = t2.code and t2.type = 'gyl' and t1.product_code = '{0}' and t1.vrid = '{1}'", product_code, ver_id)) + "";
                            if (count != "0")// 有常规服务
                            {
                                string countsbf = DataHelper.QueryValue(string.Format("select count(*) from sqm_bj_psf where vrid = '{0}' and product_code = '{1}'", ver_id, product_code)) + "";
                                if (count == countsbf)// 只有常规服务
                                {
                                    gylsign = "0";
                                    worksheet.Name = product_name + "(常规)";// 改sheet名称
                                }
                                else // 同时存在，sheet+1
                                {
                                    gylsign = "1";
                                    prdindex.Add(p);
                                    prdCount += 1;
                                    worksheet.Name = product_name + "(常规)";// 改sheet名称
                                }
                            }
                            else// 只有增值
                            {
                                gylsign = "0";
                                worksheet.Name = product_name + "(增值)";// 改sheet名称
                            }
                        }
                        else// 如果是非供应链，则判断是否有atcost费目，如果存在atcost则sheet页数加1，这里不存在只有atcost费目的情况
                        {
                            // 判断该产品下是否存在Atcost费目
                            string sql_config = @"select count(*) from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' ";
                            string count = DataHelper.QueryValue(string.Format(sql_config + " and t2.feecatg = '2' ", ver_id)) + "";// and (t2.feecatg = '1' or t2.feecatg = '2')  1-异常费目 2-atcost
                            if (count != "0")// 有附页
                            {
                                prdindex.Add(p);
                                prdCount += 1;
                            }
                            worksheet.Name = product_name;// 改sheet名称
                        }
                    }

                    // 绘制表头
                    // 插入图片
                    //string sURL = "";
                    //System.Net.WebClient objWebClient = new System.Net.WebClient();
                    //System.IO.MemoryStream objImage = new MemoryStream(objWebClient.DownloadData(sURL));
                    //string height;
                    //string width;
                    //System.IO.Stream objImage = getStream(Server.MapPath("/Excel/Templete/toptitle.png"), out height, out width);
                    //Aspose.Cells.Drawing.PictureCollection picture = worksheet.Pictures;
                    //worksheet.Pictures.Add(0, 0, 2, 8, Server.MapPath("/Excel/Templete/toptitle.png"));
                    //picture.Add(0, 0, 2, 8, objImage);  // 以流的方式插入图片
                    //picture.Add(0, 0, 2, 8, Server.MapPath("/Excel/Templete/toptitle.png"));// 直接使用图片路径
                    //cells.Merge(0, 0, 2, 8);// 合并列

                    // 开始绘制
                    #region 绘制台头信息
                    int rowIndex = 0;
                    if (type == "bjmb")
                    {
                        // 查询模板名称
                        templateName = DataHelper.QueryValue("select distinct templatename from sqm_bjmb where verid = '" + ver_id + "'") + "";
                        // 表头需要合并单元格
                        cells.Merge(0, 0, 2, 8);// 合并列 从第0行开始  模板名称
                        //设置列宽
                        cells.SetColumnWidth(0, 6);
                        cells.SetColumnWidth(1, 16);
                        cells.SetColumnWidth(2, 18);
                        cells.SetColumnWidth(3, 6.5);
                        cells.SetColumnWidth(4, 6.5);
                        cells.SetColumnWidth(5, 6.5);
                        cells.SetColumnWidth(6, 14);
                        cells.SetColumnWidth(7, 13);
                        // 设置表头值
                        cells[0, 0].PutValue(templateName);
                        cells[0, 0].SetStyle(getStyle("styleTitle1"));
                        rowIndex = 4;
                    }
                    else
                    {
                        // 表头需要合并单元格
                        cells.Merge(2, 0, 2, 8);// 合并列 从第3行开始
                        cells.Merge(4, 0, 2, 8);
                        cells.Merge(6, 0, 2, 8);
                        cells.Merge(8, 0, 1, 4); cells.Merge(8, 4, 1, 4);
                        cells.Merge(9, 0, 1, 4); cells.Merge(9, 4, 1, 4);
                        cells.Merge(10, 0, 1, 8); // 报价编号
                        cells.Merge(11, 0, 1, 4); cells.Merge(11, 4, 1, 4);
                        cells.Merge(12, 0, 1, 8);
                        cells.Merge(13, 0, 1, 8);
                        //设置列宽
                        cells.SetColumnWidth(0, 6);
                        cells.SetColumnWidth(1, 12);
                        cells.SetColumnWidth(2, 13);
                        cells.SetColumnWidth(3, 6.5);
                        cells.SetColumnWidth(4, 11);
                        cells.SetColumnWidth(5, 6.5);
                        cells.SetColumnWidth(6, 14);
                        cells.SetColumnWidth(7, 13);
                        // 设置表头值
                        cells[2, 0].PutValue("");
                        cells[4, 0].PutValue(bjDt.Rows[0]["报价名称"] + "");
                        cells[6, 0].PutValue("");
                        cells[8, 0].PutValue("Customer Name:" + bnname); cells[8, 4].PutValue("Contact information:" + namephone);
                        cells[9, 0].PutValue("Offered by:" + orgname); cells[9, 4].PutValue("Contact information:");
                        // 判断事业部
                        if (businessorg == "空运")
                        {
                            cells[10, 0].PutValue("Reference Number:" + fwaA);
                        }
                        else if (businessorg == "海运")
                        {
                            cells[10, 0].PutValue("Reference Number:" + fwaO);
                        }
                        else if (businessorg == "供应链")
                        {
                            cells[10, 0].PutValue("Reference Number:" + fwaS);
                        }
                        else if (businessorg == "运输")
                        {
                            cells[10, 0].PutValue("Reference Number:" + fwaY);
                        }
                        else
                        {
                            cells[10, 0].PutValue("Reference Number:");
                        }
                        //cells[10, 4].PutValue("报价日期:" + DateTime.Now.ToString("yyyy/MM/dd"));
                        if (bjDt.Rows[0]["起始日期"] + "" != "" && bjDt.Rows[0]["截止日期"] + "" != "")
                        {
                            cells[11, 0].PutValue("Effective Date:" + bjDt.Rows[0]["起始日期"] + "-" + bjDt.Rows[0]["截止日期"]);
                        }
                        else
                        {
                            cells[11, 0].PutValue("Effective Date:");
                        }
                        cells[11, 4].PutValue("Payment Term:");
                        // cells[12, 0].PutValue("币种：人民币");
                        cells[13, 0].PutValue("  ");
                        for (var i = 0; i < 8; i++)
                        {
                            cells[2, i].SetStyle(getStyle("styleTitle1"));
                            cells[3, i].SetStyle(getStyle("styleTitle1"));
                            cells[4, i].SetStyle(getStyle("styleTitle1"));
                            cells[5, i].SetStyle(getStyle("styleTitle1"));
                            cells[6, i].SetStyle(getStyle("styleTitle3"));
                            cells[7, i].SetStyle(getStyle("styleTitle3"));
                            cells[8, i].SetStyle(getStyle("styleTitle4"));
                            cells[9, i].SetStyle(getStyle("styleTitle4"));
                            cells[10, i].SetStyle(getStyle("styleTitle4"));
                            cells[11, i].SetStyle(getStyle("styleTitle4"));
                            cells[12, i].SetStyle(getStyle("styleTitle4"));
                        }
                        rowIndex = 14;
                    }
                    #endregion
                    // 绘制服务
                    // 得到服务信息***
                    string sql_ser = @"select distinct t1.SERVICE_CODE,t1.SERVICE_NAME from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' ";
                    string sql_ser_gyl = @"select distinct SERVICE_CODE,SERVICE_NAME from sqm_bj_psf where vrid = '" + ver_id + "' and product_code = '" + product_code + "' and service_code is not null and service_name is not null ";
                    // 2018-12-28新需求，标准费目sheet页展示异常费目，与之前只在附页展示异常费目不同，而且，如果有附页，还可能是供应链增值服务
                    if (iffy)
                    {
                        if (businessorg == "供应链")// 只有增值
                        {
                            sql_ser_gyl += " and service_code not in(select code from sqm_hkydic where type = 'gyl')";
                        }
                        else
                        {
                            sql_ser += " and t1.product_code = '" + product_code + "' and t1.service_code is not null and t1.service_name is not null and (t1.bgfzrid is null or t1.bgfzrid = '1') and t2.feecatg = '2'";
                        }
                    }
                    else
                    {
                        if (businessorg == "供应链")// 只有常规或者只有增值
                        {
                            if (gylsign == "1")// 同时存在时，只取常规服务
                            {
                                sql_ser_gyl += " and service_code in(select code from sqm_hkydic where type = 'gyl')";
                            }
                        }
                        else
                        {
                            sql_ser += " and t1.product_code = '" + product_code + "' and t1.service_code is not null and t1.service_name is not null and (t1.bgfzrid is null or t1.bgfzrid = '1') and (t2.feecatg = '0' or t2.feecatg = '1')";
                        }
                    }
                    sql_ser = businessorg == "供应链" ? sql_ser_gyl : sql_ser;
                    IList<EasyDictionary> listser = DataHelper.QueryDictList(string.Format(sql_ser, ver_id));
                    bool ifTitle = true;// 只绘制一次标题
                    // 填数之前填空行
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    // 绘制标题
                    cells[rowIndex, 0].PutValue("No.");
                    cells[rowIndex, 1].PutValue("Service");
                    cells[rowIndex, 2].PutValue("Item");
                    cells[rowIndex, 3].PutValue("Unit");
                    cells[rowIndex, 4].PutValue("Price");
                    cells[rowIndex, 5].PutValue("Min.Charge(Per Order)");
                    cells[rowIndex, 6].PutValue("Cost description");
                    cells[rowIndex, 7].PutValue("Remarks");
                    for (int colIndex = 0; colIndex < 8; colIndex++)
                    {
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleTitle5"));
                    }
                    //cells.SetRowHeight(rowIndex, 20);//设置行高
                    rowIndex++;
                    int origisrvcount = listser.Count;
                    int srvcount = listser.Count;
                    for (int s = 0; s < srvcount; s++)
                    {
                        bool ifatcost = false;
                        string service_code = "";
                        if (s >= origisrvcount)
                        {
                            service_code = listser[s - origisrvcount].Get("SERVICE_CODE") + "";
                            ifatcost = true;// 开关关闭，表示第二次经过该服务（服务下费目是异常费目），需要打印输出
                        }
                        else
                        {
                            service_code = listser[s].Get("SERVICE_CODE") + "";
                        }
                        string sql = string.Empty;
                        //rowIndex++;
                        int lastRowIndex = rowIndex;// 记录上一个服务开始的行索引，进行合并单元格操作  // 服务列与费用说明列相同的合并规则
                        // 得到费目信息***
                        string sql_common = @" from sqm_bj_psf t1,sqm_srv_fee_config t2 
where t1.product_code = t2.prodcode
and t1.service_code = t2.srvcode
and t1.fee_code = t2.feecode
and t1.vrid = '{0}' 
and t1.product_code = '{1}'
and t1.service_code = '{2}' ";
                        string sql_gyl = "select RID, FEE_CODE, FEE_NAME, BJSTATAUS from sqm_bj_psf where vrid = '{0}' and product_code = '{1}' and service_code = '{2}'";
                        string sql_fee = "select t1.RID, t1.FEE_CODE, t1.FEE_NAME, t1.BJSTATAUS " + sql_common;
                        string sql_check = "select distinct t2.feecatg " + sql_common;
                        if (iffy)// 产品附页展示  2018-12-18新需求：附页只展示atcost 
                        {
                            if (businessorg == "供应链")
                            {
                                sql_gyl += " and service_code not in(select code from sqm_hkydic where type = 'gyl') ";
                            }
                            else
                            {
                                DataTable dt = DataHelper.QueryDataTable(string.Format(sql_check + " and t2.feecatg = '2'", ver_id, product_code, service_code));
                                if (dt.Rows.Count > 0)
                                {
                                    sql_fee += " and t2.feecatg = '2'";
                                }
                            }

                        }
                        else
                        {
                            if (businessorg == "供应链")
                            {
                                if (gylsign == "1")// 同时存在时，只取常规服务
                                {
                                    sql_gyl += " and service_code in(select code from sqm_hkydic where type = 'gyl')";
                                }
                            }
                            else
                            {
                                // 服务长度加倍，第一次遍历只遍历常规费目，第二次只遍历异常费目
                                DataTable dt = DataHelper.QueryDataTable(string.Format(sql_check + " and (t2.feecatg = '1' or t2.feecatg = '0')", ver_id, product_code, service_code));
                                if (dt.Rows.Count > 0)
                                {
                                    if (ifatcost)// 第二次（遍历服务）只查异常
                                    {
                                        sql_fee += " and t2.feecatg = '1'";
                                    }
                                    else// 第一次只查标准
                                    {
                                        srvcount += 1;
                                        sql_fee += " and t2.feecatg = '0'";
                                    }
                                }
                                else
                                {
                                    sql_fee += " and (t2.feecatg = '0' or t2.feecatg = '1')";
                                }
                            }
                        }
                        sql_fee = businessorg == "供应链" ? sql_gyl : sql_fee;
                        IList<EasyDictionary> listpsf = DataHelper.QueryDictList(string.Format(sql_fee, ver_id, product_code, service_code));
                        // 判断是否有“运输路线”列  为了生成的Excel列数对齐（同一服务，不同计费基础的费目保持列数一致，这需求真特么不合理） 改版，最新版本中无“运输路线”
                        //int colnum = 6;
                        foreach (EasyDictionary dic in listpsf)
                        {
                            int lastRowIndexFee = rowIndex;
                            string bjstatus = dic.Get("BJSTATAUS") + "";// 是否保存到报价值表，预览数据至少是已保存数据
                            if (bjstatus == "0")
                            {
                                continue;
                            }
                            string feecode = dic.Get("FEE_CODE") + "";
                            string bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where product_code = '" + product_code + "' and service_code = '" + service_code + "' and fee_code = '" + feecode + "' and vrid = '" + ver_id + "'") + "";
                            if (string.IsNullOrEmpty(bjfs))
                            {
                                bjfs = "0";
                            }
                            string djfsrid = "";
                            string gdzrid = "";
                            IList<EasyDictionary> ediclist = DataHelper.QueryDictList("select distinct DJFSRID from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID <> '' or DJFSRID is not null)");// DJFSRID不为空
                            DataTable dtdjfswjc = DataHelper.QueryDataTable("select distinct djfsrid from sqm_fee_pur_ref where djfsrid not in(select djfsrid from sqm_fee_calc_ref where feecode = '" + feecode + "') and feecode = '" + feecode + "'");// 定价方式无基础
                            if ((ediclist.Count > 0 && bjfs == "0") || (dtdjfswjc.Rows.Count > 0 && bjfs == "0"))
                            {
                                foreach (EasyDictionary ed in ediclist)// 遍历定价方式
                                {
                                    djfsrid = ed.Get("DJFSRID") + "";
                                    // 是否高低值
                                    IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID from SQM_FEE_PUR_REF where STATUS = '1' and DJFSRID = '" + djfsrid + "'");
                                    if (gdzlist.Count > 0)
                                    {
                                        // 遍历高低值
                                        foreach (EasyDictionary gdz in gdzlist)
                                        {
                                            gdzrid = gdz.Get("GDZRID") + "";
                                            if (gdzrid != "")
                                            {
                                                sql = getFeeSqlEng(ver_id, service_code, product_code, feecode, "", djfsrid, gdzrid);
                                                if (sql != "")
                                                {
                                                    DrawExcelEng(cells, ref rowIndex, sql, ref ifTitle, 0);
                                                }
                                            }
                                            else // 无高低值
                                            {
                                                sql = getFeeSqlEng(ver_id, service_code, product_code, feecode, "", djfsrid, "");
                                                if (sql != "")
                                                {
                                                    DrawExcelEng(cells, ref rowIndex, sql, ref ifTitle, 0);
                                                }
                                            }
                                        }
                                    }
                                    else // 无高低值
                                    {
                                        sql = getFeeSqlEng(ver_id, service_code, product_code, feecode, "", djfsrid, "");
                                        if (sql != "")
                                        {
                                            DrawExcelEng(cells, ref rowIndex, sql, ref ifTitle, 0);
                                        }
                                    }
                                }
                                foreach (DataRow dr in dtdjfswjc.Rows)
                                {
                                    string djfsrid2 = dr["DJFSRID"] + "";
                                    sql = getFeeSqlEng(ver_id, service_code, product_code, feecode, "", djfsrid2, "");
                                    if (sql != "")
                                    {
                                        DrawExcelEng(cells, ref rowIndex, sql, ref ifTitle, 0);
                                    }
                                }
                            }
                            else// 定价方式为空（正式库应该没有定价方式为空的数据）  或者是 定价方式没有基础的费目 或者是 ATCOST 单票单询
                            {
                                sql = getFeeSqlEng(ver_id, service_code, product_code, feecode, "", "", "");
                                if (sql != "")
                                {
                                    DrawExcelEng(cells, ref rowIndex, sql, ref ifTitle, 0);
                                }
                            }
                            // 添加包干费数据-添加行的方式，弃用
                            //DrawExcelForBGF(cells,ver_id, service_code, product_code, feecode, ref rowIndex);
                            // 合并单元格 费目合并 ，包干费合并
                            if (rowIndex - lastRowIndexFee > 1)// 1行合并单元格导致行高不能自适应，内容压缩，所以只有1行时不执行合并单元格操作
                            {
                                cells.Merge(lastRowIndexFee, 2, rowIndex - lastRowIndexFee, 1);
                                cells.Merge(lastRowIndexFee, 7, rowIndex - lastRowIndexFee, 1);
                            }
                            // 行高自动
                            //Thread.Sleep((int)(500));
                            //AutoFitterOptions ao = new AutoFitterOptions();
                            //ao.AutoFitMergedCells = false;
                            //worksheet.AutoFitRows(ao);
                        }
                        // 合并单元格 服务合并 序号合并
                        if (rowIndex - lastRowIndex > 1)
                        {
                            cells.Merge(lastRowIndex, 0, rowIndex - lastRowIndex, 1);// 合并序号列  
                            cells[lastRowIndex, 0].PutValue(NumberToChinese(sequence));// 处理序号
                            sequence++;// 使用一次，自增一次
                            cells.Merge(lastRowIndex, 1, rowIndex - lastRowIndex, 1);// 合并服务列
                        }
                        else if (rowIndex - lastRowIndex == 1)// 如果相等则说明该服务下没有“已保存”数据，则序号不增加
                        {
                            cells[lastRowIndex, 0].PutValue(NumberToChinese(sequence));// 处理序号
                            sequence++;// 使用一次，自增一次
                        }
                    }
                    // 绘制尾部
                    // 填空行
                    for (int i = 0; i < 9; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    // 备注标题
                    cells[rowIndex, 0].PutValue("Memo：");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    for (int i = 0; i < 5; i++)
                    {
                        cells[rowIndex, i].SetStyle(getStyle("styleMemo"));
                    }
                    rowIndex++;
                    // 备注内容
                    cells[rowIndex, 0].PutValue("1.This quotation includes VAT.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("2.The service which is not mentioned in above offer, it must be determined by the parties in consultation if it actually occurs in operation");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    //10-24  DLC 屏蔽
                    //cells[rowIndex, 0].PutValue("3.运费支付方式为票结30天；账期以出具账单日开始计算,如超过账期未支付,则加收3%的滞纳金");
                    //cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    //cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    //rowIndex++;

                    cells[rowIndex, 0].PutValue("3.This quotation shall be executed upon the written consent of both parties and shall be equal to the contract of both parties and have the same legal effect.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("4. For all disputes arising in connection with this quotation which cannot be negotiable,it shall be submitted to the court where Jiangsu FEILIKS International Logistics Inc. is located.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    cells[rowIndex, 0].PutValue("5. The Maximum amount of single compensation of Feiliks is RMB 5 million if the service includes inland transporation project.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("If the value of a single item of goods involving is higher than RMB 5 million, it should be repored to FEILIKS in written for confirmation.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("Customer shall purchase single insurance or pays premium when Feiliks buys insurance on behalf of customer for the excess cargo.");
                    cells.Merge(rowIndex, 0, 1, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("If customer does not agree to pay additional premium, and also refuses to purchase insurance accordingly , FEILIKS will pay compensation according to the maximum amount of RMB 5 million for a single transportation, and therefore FEILIKS is entitled to exemption from responsibility.");
                    cells.Merge(rowIndex, 0, 4, 8);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleContent2"));
                    rowIndex++;

                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    for (int i = 0; i < 8; i++)
                    {
                        cells[rowIndex, i].PutValue("");
                    }
                    rowIndex++;
                    // 签名
                    cells[rowIndex, 0].PutValue("Offerer：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    if (orgcode != "")
                    {
                        string fileExistPath = Server.MapPath("/Excel/Templete/" + orgcode + ".png");
                        if (CheckFileExist(fileExistPath))
                        {
                            worksheet.Pictures.Add(rowIndex, 0, fileExistPath);// 盖章 根据组织代码盖章
                        }
                    }
                    cells[rowIndex, 4].PutValue("Customer：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("Stamped signature ：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    cells[rowIndex, 4].PutValue("Stamped signature ：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    cells[rowIndex, 0].PutValue("Date：");
                    cells.Merge(rowIndex, 0, 1, 4);// 合并列
                    cells[rowIndex, 0].SetStyle(getStyle("styleSign"));
                    cells[rowIndex, 4].PutValue("Date：");
                    cells.Merge(rowIndex, 4, 1, 4);// 合并列
                    cells[rowIndex, 4].SetStyle(getStyle("styleSign"));
                    rowIndex++;
                    rowIndex++;

                    // 转pdf 中文变成四方块解决方法  必须设置字体 具体原因不详
                    for (int i = 2; i <= rowIndex; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            string str = cells[i, j].Value + "";
                            if (str.Length >= 1)
                            {
                                FontSetting charactor = cells[i, j].Characters(0, str.Length);
                                charactor.Font.Name = "宋体";// 微软雅黑 不行  黑体、宋体
                            }
                        }
                    }
                    if (type != "bjmb")
                    {
                        worksheet.Pictures.Add(0, 6, Server.MapPath("/Excel/Templete/toptitle.png"));// 插图 Excel97-2003 Excel生成文件损坏，不知道是电脑的原因还是什么原因，改用xlsx
                    }
                    // 设置页眉页脚 添加picture失败，可能是 读出来的byte[]不对，也可能是因为用了盗版Aspose T_T
                    //Aspose.Cells.PageSetup pageSetup = worksheet.PageSetup;
                    //pageSetup.SetHeaderPicture(2, getBytes(Server.MapPath("/Excel/Templete/Righttitle.png")));
                    //pageSetup.SetHeader(0, "&N");
                    // 行高自动--按未合并行来自适应，所以最后一列合并行内容无法自适应
                    AutoFitterOptions ao = new AutoFitterOptions();
                    //ao.AutoFitMergedCells = true;
                    //ao.IgnoreHidden = true;
                    ao.OnlyAuto = true;
                    worksheet.AutoFitRows(ao);
                }

                // 1 生成Excel文件
                string createTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string createPath = Server.MapPath("/Excel/output/");
                if (type == "bjmb")
                {
                    fileName = RegexReplace(templateName) + createTime;
                }
                else
                {
                    fileName = RegexReplace(bjDt.Rows[0]["报价名称"] + "") + createTime;
                }
                string createName = fileName + ".pdf";
                filePath = System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".xlsx");
                //workbook.Save(filePath, SaveFormat.Excel97To2003);
                workbook.Save(filePath, SaveFormat.Xlsx);
                // 2 生成pdf option
                Aspose.Cells.PdfSaveOptions xlsSaveOption = new Aspose.Cells.PdfSaveOptions();
                #region 设置字体
                //xlsSaveOption.DefaultFont = "MingLiu";
                //xlsSaveOption.DefaultFont = "MS Gothic";
                //xlsSaveOption.DefaultFont = "Microsoft YaHei";
                //xlsSaveOption.DefaultFont = "FangSong_GB2312";
                //xlsSaveOption.SecurityOptions = new Aspose.Cells.Rendering.PdfSecurity.PdfSecurityOptions();
                #endregion
                #region pdf 加密
                //Set the user password
                //xlsSaveOption.SecurityOptions.UserPassword = "1111";
                //Set the owner password
                //xlsSaveOption.SecurityOptions.OwnerPassword = "1111";
                //Disable extracting content permission
                //xlsSaveOption.SecurityOptions.ExtractContentPermission = false;
                //Disable print permission
                //xlsSaveOption.SecurityOptions.PrintPermission = false;
                #endregion
                xlsSaveOption.OnePagePerSheet = true;// 一个sheet页一张pdf页
                xlsSaveOption.ValidateMergedAreas = true;

                string filePath1 = System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".pdf");

                // 生成pdf
                // 1
                //workbook.Save(filePath1, xlsSaveOption);
                // 2
                //Workbook wb = new Workbook(filePath);
                //wb.Save(System.IO.Path.Combine(Server.MapPath("/Excel/output/"), fileName + ".pdf"), SaveFormat.Pdf);
                // 3
                Workbook workbook2 = new Workbook();
                workbook2.Open(filePath, FileFormatType.Xlsx);
                foreach (Worksheet worksheet in workbook2.Worksheets)
                {
                    worksheet.AutoFitRows();
                }
                workbook2.Save(Path.ChangeExtension(filePath, ".pdf"), SaveFormat.Pdf);
                DealWithPdf(filePath1);// 处理空白页--没有图片的都被视为空白页了

                // 存路径
                if (type == "mb" || type == "bjmb")
                {
                    DataTable dtt = DataHelper.QueryDataTable(@"select * from sqm_bj_ver_eng where rid= '" + ver_id + "'");
                    if (dtt.Rows.Count > 0)
                    {
                        //DataHelper.ExecSql("update sqm_bj_ver set UPLOADTIME = to_date('" + createTime + "','yyyy-mm-dd hh24:mi:ss'),UPLOADNAME = '" + createName + "',UPLOADURL = '" + createPath + "',SHOWMODE = '0' where rid = '" + ver_id + "'");
                        string sql = "update sqm_bj_ver_eng set UPLOADTIME = to_date('" + createTime + "','yyyy-mm-dd hh24:mi:ss'),UPLOADNAME = '" + createName + "',UPLOADURL = '" + createPath + "',SHOWMODE = '0' where rid = '" + ver_id + "'";
                        DataHelper.ExecSql(sql);
                    }
                    else
                    {
                        DataHelper.ExecSql(@"insert into sqm_bj_ver_eng(UPLOADTIME,UPLOADNAME,UPLOADURL,SHOWMODE,RID,mrid) values(to_date('" + createTime + "','yyyy-mm-dd hh24:mi:ss'), '" + createName + "', '" + createPath + "','0', '" + ver_id + "','" + main_id + "')");
                    }
                    //DataHelper.ExecSql("update sqm_bj_ver set UPLOADTIME = to_date('" + createTime + "','yyyy-mm-dd hh24:mi:ss'),UPLOADNAME = '" + createName + "',UPLOADURL = '" + createPath + "',SHOWMODE = '0' where rid = '" + ver_id + "'");
                    msg = "生成成功";
                }
                else
                {
                    msg = "生成成功";
                }
            }
            catch (Exception ex)
            {
                fileName = "";
                filePath = "";
                msg = ex.Message;
            }
        }
        public string getFeeSqlEng(string ver_id, string service_code, string product_code, string fee_code, string unit, string djfsrid, string gdzrid)
        {
            string sql_val_mb = "";
            string columns = "";
            string sql_val = "";
            // 获取数据源
            sql_val = getSourceSql(fee_code, service_code, product_code, ver_id, unit, "mb", djfsrid, gdzrid);
            string bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where fee_code = '" + fee_code + "' and service_code = '" + service_code + "' and product_code = '" + product_code + "' and vrid = '" + ver_id + "'") + "";
            if (bjfs != "1" && bjfs != "2")// At cost 不处理计费基础
            {
                // 获取计费基础字段名,是否在模板中加入以及如何加入这些基础
                columns = getColumns(fee_code, unit, djfsrid, gdzrid);
                // 处理别名过长
                columns = HandleColumn(columns);
            }
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(columns))
            {
                // 去掉英文代码
                columns = ClearENcode(columns);
                // 取别名
                if (columns.IndexOf(',') >= 0)
                {
                    string[] columnNames = columns.Split(',');
                    for (int c = 0; c < columnNames.Length; c++)
                    {
                        string asafter = columnNames[c].Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        list.Add(asafter);
                    }
                }
                else
                {
                    string asafter = columns.Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                    list.Add(asafter);
                }
            }
            // 处理计费因子的英文(有中文的保留，没有的加上，即计费因子最后都是中文形式)
            ConvertIntoChinese(list);
            // 报价文件数据
            if (sql_val != "")
            {
                sql_val_mb = @"select ""序号"",""服务"",""费目"",";
                if (IFYSFee(fee_code))
                {
                    sql_val_mb += @"""报价单位"" as ""计费单位"",""报价"" as ""单价"",""最低收费"",""包干费"",""服务代码"",""费目代码"",";
                    sql_val_mb = getJFYZ(sql_val_mb, list, fee_code, djfsrid, gdzrid);// 运输费的车型吨位/运单计费重量 + 单整车标记  运输类单价属性
                    sql_val_mb = getOtherBase(sql_val_mb, list, fee_code, djfsrid, gdzrid);// 运输类（ys）费目费用说明列
                    sql_val_mb += " from (";
                    sql_val_mb += sql_val + ")";
                    sql_val_mb = @"select ""序号"",""服务"",""费目"",""计费单位"",
case when ""计费因子"" is not null then ""单价"" || '/' || to_char(""计费因子"") else ""单价"" end as ""单价"",""最低收费"",""费用说明"",""包干费"",""服务代码"",""费目代码"" from (" + sql_val_mb + ")";
                }
                else
                {
                    sql_val_mb += @"""报价单位"" as ""计费单位"",""报价"" as ""单价"",""最低收费"",""费用说明"",""包干费"",""服务代码"",""费目代码"" from (";
                    sql_val_mb += sql_val + ")";
                }

            }
            return sql_val_mb;
        }
        private void DrawExcelEng(Cells cells, ref int rowIndex, string sql, ref bool ifTitle, int colnum)
        {
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                int colIndex = 0;
                // 内容填数
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dr.Table.Columns)
                    {
                        string bjcx_grey = "(报价超限1)";
                        string bjcx_yellow = "(报价超限2)";
                        string ifbjcx = dr[col.ColumnName] + "";
                        if (ifbjcx != "")
                        {
                            if (ifbjcx.IndexOf("(报价超限1)") >= 0)
                            {
                                //cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx.Replace(bjcx_grey, "")) + "");
                                cells[rowIndex, colIndex].PutValue(ifbjcx.Replace(bjcx_grey, "") + "");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleContentGrey"));
                                colIndex++;
                            }
                            else if (ifbjcx.IndexOf("(报价超限2)") >= 0)
                            {
                                //cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx.Replace(bjcx_yellow, "")) + "");
                                cells[rowIndex, colIndex].PutValue(ifbjcx.Replace(bjcx_yellow, "") + "");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleContentYellow"));
                                colIndex++;
                            }
                            else
                            {
                                if (colIndex == 7)//包干费
                                {
                                    cells[rowIndex, 7].PutValue(ifbjcx);
                                    cells[rowIndex, 7].SetStyle(getStyle("styleContentLeftJustify"));
                                    colIndex++;
                                    break;
                                }
                                else if (colIndex == 6)// 计费因子列左对齐
                                {
                                    cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContentLeftJustify"));
                                    colIndex++;
                                }
                                else if (colIndex == 4)// 单价
                                {
                                    if (IfDecimal(ifbjcx))
                                    {
                                        cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx) + "");
                                    }
                                    else// 处理设备类型过长，将name值换成code值
                                    {
                                        ifbjcx = HandleSBLX(ifbjcx);
                                        cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    }
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                    colIndex++;
                                }
                                else if (colIndex == 1)//服务
                                {
                                    try
                                    {
                                        string sqlfw = @"select DESCRIPTION from MDM_SRVRQCD where SRVRQCD='" + dr["服务代码"] + "' and SPRAS='E'";
                                        string fw = DataHelper.QueryValue(sqlfw) + "";
                                        if (fw.Length > 0)
                                        {
                                            cells[rowIndex, colIndex].PutValue(fw);
                                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        }
                                        else
                                        {
                                            cells[rowIndex, colIndex].PutValue(ifbjcx);
                                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        cells[rowIndex, colIndex].PutValue(ifbjcx);
                                        cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        colIndex++;
                                    }
                                    colIndex++;
                                }
                                else if (colIndex == 2)//费目
                                {
                                    try
                                    {
                                        string sqlfm = @"select TEXTDESC from mdm_fee where TCET084='" + dr["费目代码"] + "' and LANGTYPE='E'";
                                        string fm = DataHelper.QueryValue(sqlfm) + "";
                                        if (fm.Length > 0)
                                        {
                                            cells[rowIndex, colIndex].PutValue(fm);
                                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        }
                                        else
                                        {
                                            cells[rowIndex, colIndex].PutValue(ifbjcx);
                                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        cells[rowIndex, colIndex].PutValue(ifbjcx);
                                        cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                        colIndex++;
                                    }
                                    colIndex++;
                                }
                                //else if (colIndex == 10)//结算方
                                //{
                                //    string sqlorg = @"select * from mdm_org where ORGKEY='" + ifbjcx + "' and LANGTYPE='E'";
                                //    cells[rowIndex, colIndex].PutValue(ifbjcx);
                                //    cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                //    colIndex++;
                                //}
                                else if (colIndex > 7)
                                {
                                    continue;
                                }
                                else
                                {
                                    cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                    colIndex++;
                                }
                            }
                        }
                        else if (colIndex > 8)
                        {
                            continue;
                        }
                        else
                        {
                            cells[rowIndex, colIndex].PutValue(ifbjcx);
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                            colIndex++;
                        }
                    }
                    rowIndex++;
                    colIndex = 0;
                }
            }
        }
        /// <summary>
        /// 添加费目包干费--行级，弃用
        /// </summary>
        /// <param name="ver_id"></param>
        /// <param name="service_code"></param>
        /// <param name="product_code"></param>
        /// <param name="feecode"></param>
        /// <param name="rowIndex"></param>
        private void DrawExcelForBGF(Cells cells,string ver_id, string service_code, string product_code, string feecode, ref int rowIndex)
        {
            DataTable dt = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = (select rid from sqm_bj_psf where vrid = '" + ver_id + "' and fee_code = '" + feecode + "' and service_code = '" + service_code + "' and product_code = '" + product_code + "' and (bgfzrid = '1' or nvl(bgfzrid,' ') = ' '))");
            if (dt.Rows.Count > 0)
            {
                DataTable dt2 = DataHelper.QueryDataTable("select service_name,fee_name from sqm_bj_psf where vrid = '" + ver_id + "' and service_code = '" + service_code + "' and fee_code = '" + feecode + "' and product_code = '" + product_code + "'");

                string service_name = dt2.Rows[0]["SERVICE_NAME"] + "";
                string fee_name = dt2.Rows[0]["FEE_NAME"] + "";
                string bgf = "";
                foreach (DataRow dr in dt.Rows)
                {
                    bgf += (dr["SERVICE_NAME"] + "").Replace("（", "(").Replace("）", ")") + "--" + (dr["FEE_NAME"] + "").Replace("（", "(").Replace("）", ")") + "，";
                }
                bgf = bgf.Substring(0, bgf.Length - 1);

                cells[rowIndex, 0].PutValue("一");
                cells[rowIndex, 0].SetStyle(getStyle("styleContent"));
                cells[rowIndex, 1].PutValue(service_name);
                cells[rowIndex, 1].SetStyle(getStyle("styleContent"));
                cells[rowIndex, 2].PutValue(fee_name);
                cells[rowIndex, 2].SetStyle(getStyle("styleContent"));
                cells[rowIndex, 3].PutValue("包干费：" + bgf);
                cells[rowIndex, 3].SetStyle(getStyle("styleContent"));

                cells[rowIndex, 4].SetStyle(getStyle("styleContent"));
                cells[rowIndex, 5].SetStyle(getStyle("styleContent"));
                cells[rowIndex, 6].SetStyle(getStyle("styleContent"));
                cells.Merge(rowIndex, 3, 1, 4);
                rowIndex++;
            }
        }

        /// <summary>
        /// “报价预览” “生成报价文件” Excel绘制
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="rowIndex"></param>
        /// <param name="sql"></param>
        /// <param name="ifTitle"></param>
        /// <param name="colnum"></param>
        private void DrawExcel(Cells cells, ref int rowIndex, string sql, ref bool ifTitle, int colnum)
        {
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                int colIndex = 0;
                // 内容填数
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dr.Table.Columns)
                    {
                        string bjcx_grey = "(报价超限1)";
                        string bjcx_yellow = "(报价超限2)";
                        string ifbjcx = dr[col.ColumnName] + "";
                        if (ifbjcx != "")
                        {
                            if (ifbjcx.IndexOf("(报价超限1)") >= 0)
                            {
                                //cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx.Replace(bjcx_grey, "")) + "");
                                cells[rowIndex, colIndex].PutValue(ifbjcx.Replace(bjcx_grey, "") + "");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleContentGrey"));
                                colIndex++;
                            }
                            else if (ifbjcx.IndexOf("(报价超限2)") >= 0)
                            {
                                //cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx.Replace(bjcx_yellow, "")) + "");
                                cells[rowIndex, colIndex].PutValue(ifbjcx.Replace(bjcx_yellow, "") + "");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleContentYellow"));
                                colIndex++;
                            }
                            else
                            {
                                if (colIndex == 7)//包干费
                                {
                                    cells[rowIndex, 7].PutValue(ifbjcx);
                                    cells[rowIndex, 7].SetStyle(getStyle("styleContentLeftJustify"));
                                    colIndex++;
                                    break;
                                }
                                else if (colIndex == 6)// 计费因子列左对齐
                                {
                                    cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContentLeftJustify"));
                                    colIndex++;
                                }
                                else if (colIndex == 4)// 单价
                                {
                                    if (IfDecimal(ifbjcx))
                                    {
                                        cells[rowIndex, colIndex].PutValue(Convert.ToDecimal(ifbjcx) + "");
                                    }
                                    else// 处理设备类型过长，将name值换成code值
                                    {
                                        ifbjcx = HandleSBLX(ifbjcx);
                                        cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    }
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                    colIndex++;
                                }
                                else
                                {
                                    cells[rowIndex, colIndex].PutValue(ifbjcx);
                                    cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                                    colIndex++;
                                }
                            }
                        }
                        else
                        {
                            cells[rowIndex, colIndex].PutValue(ifbjcx);
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleContent"));
                            colIndex++;
                        }
                    }
                    rowIndex++;
                    colIndex = 0;
                }
            }
        }
        /// <summary>
        /// 设备类型 名称转成代码
        /// </summary>
        DataTable dtSBLX = DataHelper.QueryDataTable("select column4,column5 from mdm_calc_value where mdkey = 'EQUIP_TYPE'");
        public string HandleSBLX(string value)
        {
            var valueArr = value.Split('/');
            List<string> newValue = new List<string>();
            foreach(string v in valueArr)
            {
                DataRow[] drs = dtSBLX.Select("column5 = '" + v + "'");
                if (drs.Length > 0)
                {
                    string nv = drs[0]["COLUMN4"] + "";
                    newValue.Add(nv);
                }
                else
                {
                    newValue.Add(v);
                }
            }
            return String.Join("/",newValue.ToArray());
        }
        /// <summary>
        /// 将英文标题换成 中文 + ',' + 英文形式
        /// </summary>
        /// <param name="dt"></param>
        DataTable dtcodename = DataHelper.QueryDataTable("select calc_base,description from mdm_calc_base");
        private void ConvertIntoChinese(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                // 如果没有汉字
                if (!IfChinese(list[i]))
                {
                    string code = list[i].Replace("\"", "");
                    DataRow[] drs = dtcodename.Select("calc_base = '" + list[i].Replace("\"", "") + "'");
                    if (drs.Length > 0)
                    {
                        list[i] = "\"" + drs[0]["DESCRIPTION"] + "\"," + code;
                    }
                }
            }
        }
        /// <summary>
        /// 判断是否有“运输路线”列 弃用
        /// </summary>
        /// <param name="listpsf"></param>
        /// <returns></returns>
        public int getColumnNum(IList<EasyDictionary> listpsf, string djfsrid, string gdzrid)
        {
            int colnum = 5;
            foreach (EasyDictionary dic in listpsf)
            {
                string feecode = dic.Get("FEE_CODE") + "";
                string sql = "";
                if (gdzrid != "")
                {
                    sql = "select calcname from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and (calcname like '%起运港%' or calcname like '%目的港%') and gdzrid ='" + gdzrid + "'";
                }
                else if (djfsrid != "")
                {
                    sql = "select calcname from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and (calcname like '%起运港%' or calcname like '%目的港%') and djfsrid = '" + djfsrid + "'";
                }
                else
                {
                    sql = "select calcname from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and (calcname like '%起运港%' or calcname like '%目的港%') and djfsrid is null";
                }
                string calcname = DataHelper.QueryValue(sql) + "";
                if (calcname != "")
                {
                    colnum = 6;
                }
            }
            return colnum;
        }
        /// <summary>
        /// NPOI
        /// Excel转Html
        /// </summary>
        /// <param name="filePath"></param>
        public string ExcelToHtml(string fileName, string filePath)
        {
            IWorkbook workbook = null;
            string filename = fileName;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (filename.IndexOf(".xlsx") > 0) // 2007版本
            {
                workbook = new XSSFWorkbook(fs);
                filename = filename.Replace(".xlsx", "");
            }
            else if (filename.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook(fs);
                filename = filename.Replace(".xls", "");
            }
            ExcelToHtmlConverter excelToHtmlConverter = new ExcelToHtmlConverter();
            // 设置输出参数
            excelToHtmlConverter.OutputColumnHeaders = false;
            excelToHtmlConverter.OutputHiddenColumns = false;
            excelToHtmlConverter.OutputHiddenRows = false;
            excelToHtmlConverter.OutputLeadingSpacesAsNonBreaking = true;
            excelToHtmlConverter.OutputRowNumbers = false;
            excelToHtmlConverter.UseDivsToSpan = false;
            // 处理的Excel文件
            excelToHtmlConverter.ProcessWorkbook(workbook);
            // 添加表格样式
            //        excelToHtmlConverter.Document.InnerXml =
            //            excelToHtmlConverter.Document.InnerXml.Insert(
            //                excelToHtmlConverter.Document.InnerXml.IndexOf("<head>", 0) + 6,
            //                @"<style>*{margin: auto;}body.b1{white-space-collapsing:preserve;}body{width: 950px;}h2{line-height: 46px;}
            //table{margin-bottom: 40px;}
            //"
            //            );
            //

            // 输出的html文件
            var htmlFile = System.Web.HttpContext.Current.Server.MapPath("/Excel/") + "preview\\" + filename + ".html";
            excelToHtmlConverter.Document.Save(htmlFile);
            string path = "/Excel/preview/" + filename + ".html";
            return path;
        }
        /// <summary>
        /// 导出模板查询值表sql
        /// </summary>
        /// <param name="ver_id"></param>
        /// <param name="service_code"></param>
        /// <param name="product_code"></param>
        /// <returns></returns>
        public string getFeeSql(string ver_id, string service_code, string product_code, string fee_code, string unit, string djfsrid, string gdzrid)
        {
            string sql_val_mb = "";
            string columns = "";
            string sql_val = "";
            // 获取数据源
            sql_val = getSourceSql(fee_code, service_code, product_code, ver_id, unit, "mb", djfsrid, gdzrid);
            string bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where fee_code = '" + fee_code + "' and service_code = '" + service_code + "' and product_code = '" + product_code + "' and vrid = '" + ver_id + "'") + "";
            if (bjfs != "1" && bjfs != "2")// At cost 不处理计费基础
            {
                // 获取计费基础字段名,是否在模板中加入以及如何加入这些基础
                columns = getColumns(fee_code, unit, djfsrid, gdzrid);
                // 处理别名过长
                columns = HandleColumn(columns);
            }
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(columns))
            {
                // 去掉英文代码
                columns = ClearENcode(columns);
                // 取别名
                if (columns.IndexOf(',') >= 0)
                {
                    string[] columnNames = columns.Split(',');
                    for (int c = 0; c < columnNames.Length; c++)
                    {
                        string asafter = columnNames[c].Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        list.Add(asafter);
                    }
                }
                else
                {
                    string asafter = columns.Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                    list.Add(asafter);
                }
            }
            // 处理计费因子的英文(有中文的保留，没有的加上，即计费因子最后都是中文形式)
            ConvertIntoChinese(list);
            // 报价文件数据
            if (sql_val != "")
            {
                sql_val_mb = @"select ""序号"",""服务"",""费目"",";
                if (IFYSFee(fee_code))
                {
                    sql_val_mb += @"""报价单位"" as ""计费单位"",""报价"" as ""单价"",""最低收费"",""包干费"",";
                    sql_val_mb = getJFYZ(sql_val_mb, list, fee_code, djfsrid, gdzrid);// 运输费的车型吨位/运单计费重量 + 单整车标记  运输类单价属性
                    sql_val_mb = getOtherBase(sql_val_mb, list, fee_code, djfsrid, gdzrid);// 运输类（ys）费目费用说明列
                    sql_val_mb += " from (";
                    sql_val_mb += sql_val + ")";
                    sql_val_mb = @"select ""序号"",""服务"",""费目"",""计费单位"",
case when ""计费因子"" is not null then ""单价"" || '/' || to_char(""计费因子"") else ""单价"" end as ""单价"",""最低收费"",""费用说明"",""包干费"" from (" + sql_val_mb + ")";
                }
                else
                {
                    sql_val_mb += @"""报价单位"" as ""计费单位"",""报价"" as ""单价"",""最低收费"",""费用说明"",""包干费"" from (";
                    sql_val_mb += sql_val + ")";
                }

            }
            return sql_val_mb;
        }
        /// <summary>
        /// 拼单价所加属性，如：车型吨位/运单计费重量/单整车标记/设备类型（柜）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string getJFYZ(string sql_val_mb, List<string> list, string feecode,string djfsrid,string gdzrid)
        {
            List<string> newList = new List<string>(list);
            // 得到配置表运输类 单价属性信息
            List<string> ysbase = new List<string>();
            string property_cn = GetPriceProperty(feecode);
            if(property_cn != "")
            {
                ysbase = property_cn.Split(',').ToList();
            }
            if (ysbase.Count > 0)
            {
                // 筛选配置基础
                for (int i = newList.Count - 1; i >= 0; i--)
                {
                    string baseL = newList[i].Replace("\"", "");
                    bool match = false;
                    if (baseL.IndexOf(",") >= 0)// 有英文
                    {
                        baseL = baseL.Split(',')[0];
                    }
                    foreach (string ys in ysbase)
                    {
                        if (baseL == ys)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)// 未匹配，从list中删除该元素
                    {
                        newList.RemoveAt(i);
                    }
                }
                if (newList.Count > 0)
                {
                    string prop = "";
                    string prop_bd = "";
                    for (int i = 0; i < newList.Count; i++)
                    {
                        if(i == newList.Count - 1)// 最后一个
                        {
                            if (newList[i].IndexOf(",") >= 0)
                            {
                                prop_bd += GetDJProperty(newList[i].Split(',')[0],feecode,djfsrid,gdzrid);
                                prop += prop_bd + newList[i].Split(',')[1];
                            }
                            else
                            {
                                prop_bd += GetDJProperty(newList[i], feecode, djfsrid, gdzrid);
                                prop += prop_bd + newList[i];
                            }
                        }
                        else
                        {
                            if (newList[i].IndexOf(",") >= 0)
                            {
                                prop_bd += GetDJProperty(newList[i].Split(',')[0], feecode, djfsrid, gdzrid);
                                prop += prop_bd + newList[i].Split(',')[1] + " || '/' || ";
                            }
                            else
                            {
                                prop_bd += GetDJProperty(newList[i], feecode, djfsrid, gdzrid);
                                prop += prop_bd + newList[i] + " || '/' || ";
                            }
                        }
                    }
                    prop += " as \"计费因子\",";
                    sql_val_mb += prop;
                }
                else
                {
                    sql_val_mb += "'' as \"计费因子\",";
                }
            }
            else
            {
                sql_val_mb += "'' as \"计费因子\",";
            }
            return sql_val_mb;
        }

        /// <summary>
        /// 导出Excel数据源    At cost 时，分两种情况：值表有数据/值表无数据
        /// </summary>
        /// <param name="unit">报价单位</param>
        /// <param name="feecode">费目代码</param>
        /// <returns></returns>
        public string getSourceSql(string feecode, string srvcode, string prdcode, string vrid, string unit, string type, string djfsrid, string gdzrid)
        {
            string currency_cn = "人民币";
            string currency_en = "CNY";
            // 获取计费基础字段名
            string columns = getColumns(feecode, unit, djfsrid, gdzrid);
            if (columns != "")
            {
                // 处理别名过长
                columns = HandleColumn(columns);
                // 去掉基础的英文代码，只保留中文    给客户看的正规报价文件去掉代码，导出excel不去
                if (type == "mb")
                {
                    columns = ClearENcode(columns);
                }
            }
            // 判断值表是否有数据
            string feecalcid = "";
            string bjrid = "";
            DataTable dt = DataHelper.QueryDataTable("select distinct t1.rid as psfrid,t2.rid as bjrid from sqm_bj_psf t1 left join sqm_modebj_val t2 on t1.rid = t2.feecalcid where t1.fee_code = '" + feecode + "' and t1.service_code = '" + srvcode + "' and t1.product_code = '" + prdcode + "' and t1.vrid = '" + vrid + "'");
            if (dt.Rows.Count > 0)
            {
                feecalcid = dt.Rows[0]["PSFRID"] + "";
                bjrid = dt.Rows[0]["BJRID"] + "";
            }
            string bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where fee_code = '" + feecode + "' and service_code = '" + srvcode + "' and product_code = '" + prdcode + "' and vrid = '" + vrid + "'") + "";
            string sql_val = "";
            if (bjrid != "")// 非at cost 非单票单询
            {
                string wheredjfs = "";
                string wherepur = "";
                if (gdzrid != "")
                {
                    wheredjfs = " and gdzrid = '" + gdzrid + "' ";
                    wherepur = " and gdzrid = '" + gdzrid + "' ";
                }
                else if (djfsrid != "")
                {
                    wheredjfs = " and djfsrid = '" + djfsrid + "' ";
                    wherepur = " and djfsrid = '" + djfsrid + "' ";
                }
                else
                {
                    wheredjfs = " and djfsrid is null or djfsrid = '' ";
                }
                // 查询值表 值表有数据
                // 运输类费目（配置表sqm_hkydic，类型为'ys'的费目，不局限于运输类）
                // 保留两位小数，去掉小数点前面多余的0 to_char(t2.BJPRICE,'9999999990.99')
                if (IFYSFee(feecode))
                {
                    sql_val += @"select m1.*,m2.FEEUNIT as ""报价单位"",t3.bgf as ""包干费""
        from (select '' as ""序号"",t1.rid,t1.vrid,t1.product_name as ""产品名称"", t1.product_code as ""产品代码"",
        t1.service_name as ""服务"",
        t1.service_code as ""服务代码"",
        case when t1.OTHER_NAME <> '' or t1.OTHER_NAME is not null then t1.OTHER_NAME else t1.fee_name end as ""费目"",
        t1.fee_code as ""费目代码"",
        t1.JSFCODE as ""结算方代码"",
        t1.JSF as ""结算方"",
        t2.CURRENCY as ""币种"",
        case when t1.BJFS = '1' then 'At COST' 
             when t1.BJFS = '2' then '单票单询' 
             when (t2.BJPRICE < t2.minprice or t2.bjprice > t2.maxprice) and IFUPDATE = '1' then to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end) || '(报价超限1)'
             when t2.BJPRICE < t2.minprice or t2.bjprice > t2.maxprice and (IFUPDATE = '2' or IFUPDATE is null) then to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end) || '(报价超限2)'
             else to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end)
        end as ""报价"",
        case when t2.MINSTATUS='1' then
            to_char(t2.MINBJPRICE) || '(报价超限2)'
        else to_char(t2.MINBJPRICE) end as ""最低收费"",
        case when (t2.GDZRID is not null or t2.GDZRID <> '') then t2.GDZRID else t2.DJFSRID end as DJFSRID,
        t2.MEMO as ""备注""";
                    if (!string.IsNullOrEmpty(columns))
                    {
                        sql_val += "," + columns;
                    }
                    sql_val = string.Format(sql_val, currency_cn, currency_en);
                }
                else// 费用说明取费目设置的说明列数据
                {
                    sql_val += @"select m1.*,m2.FEEUNIT as ""报价单位"",t3.bgf as ""包干费"",
        m2.FSFYSM as ""费用说明"" 
        from (select '' as ""序号"",t1.rid,t1.vrid,t1.product_name as ""产品名称"", t1.product_code as ""产品代码"",
        t1.service_name as ""服务"",
        t1.service_code as ""服务代码"",
        case when t1.OTHER_NAME <> '' or t1.OTHER_NAME is not null then t1.OTHER_NAME else t1.fee_name end as ""费目"",
        t1.fee_code as ""费目代码"",
        t1.JSFCODE as ""结算方代码"",
        t1.JSF as ""结算方"",
        t2.CURRENCY as ""币种"",
        case when t1.BJFS = '1' then 'At COST' 
             when t1.BJFS = '2' then '单票单询' 
             when (t2.BJPRICE < t2.minprice or t2.bjprice > t2.maxprice) and IFUPDATE = '1' then to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end) || '(报价超限1)'
             when t2.BJPRICE < t2.minprice or t2.bjprice > t2.maxprice and (IFUPDATE = '2' or IFUPDATE is null) then to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end) || '(报价超限2)'
             else to_char(case when t2.CURRENCY = '{0}' or upper(t2.CURRENCY) = '{1}' then trim('.' from to_char(t2.BJPRICE,'fm999999990.99999')) else (to_char(nvl(t2.CURRENCY,'')) || trim('.' from to_char(t2.BJPRICE,'fm999999990.99999'))) end)
        end as ""报价"",
        case when t2.MINSTATUS='1' then
            to_char(t2.MINBJPRICE) || '(报价超限2)' 
        else to_char(t2.MINBJPRICE) end as ""最低收费"",
        case when (t2.GDZRID is not null or t2.GDZRID <> '') then t2.GDZRID else t2.DJFSRID end as DJFSRID,
        t2.MEMO as ""备注""";
                    sql_val = string.Format(sql_val, currency_cn, currency_en);
                }

                sql_val += @" from SQM_BJ_PSF t1,SQM_MODEBJ_VAL t2
        where t1.rid = t2.feecalcid 
        and t2.status = '1'
        and t1.bjstataus <> '0'
        and t1.rid = '" + feecalcid + "' ";
                sql_val += wheredjfs;
                sql_val += @") m1 left join 
        (select distinct feeunit,fsfysm,case when (gdzrid is not null or gdzrid <> '') then gdzrid else djfsrid end as djfsrid from sqm_fee_pur_ref) m2 on m1.djfsrid = m2.djfsrid ";
                sql_val += string.Format(@"left join (select case when gdzrid is not null or gdzrid <> '' then gdzrid else djfsrid end as djfsrid,
       replace(to_char(wm_concat(to_char(memo))),',',';') as memo
from sqm_fee_calc_ref where 1 = 1 and status = '1'
{0}
group by djfsrid,gdzrid) m3 on m1.djfsrid = m3.djfsrid 
left join (select bgfzrid,wm_concat(to_char(service_name) || '--' || to_char(fee_name)) as bgf from sqm_bj_psf where bgfzrid = '" + feecalcid + @"' group by bgfzrid) t3 on t3.bgfzrid = m1.rid
order by to_number(regexp_substr(""报价"",'\d+(.\d+)?',1)) ", wherepur);//replace(wm_concat(to_char(calcname || ':' || memo)),',',chr(10)) as memo
            }
            else if (bjfs == "1" || bjfs == "2")// 值表无数据，报价方式为 At cost或“单票单询”
            {
                string bjfsname = "";
                if (bjfs == "1")
                {
                    bjfsname += "AT COST";
                }
                else
                {
                    bjfsname += "单票单询";
                }
                sql_val += @"select '一' as ""序号"",t1.vrid,t1.product_name as ""产品名称"", t1.product_code as ""产品代码"",
        t1.service_name as ""服务"",
        t1.service_code as ""服务代码"",
        case when t1.OTHER_NAME <> '' or t1.OTHER_NAME is not null then t1.OTHER_NAME else t1.fee_name end as ""费目"",
        t1.fee_code as ""费目代码"",
        t1.JSFCODE as ""结算方代码"",
        t1.JSF as ""结算方"",
        '' as ""报价单位"",
        '' as ""包干费"",
        '" + bjfsname + "' ";
                sql_val += @"as ""报价"",'' as ""最低收费"", t1.REMARK as ""备注"",'' as ""费用说明"" from SQM_BJ_PSF t1 where (t1.bjstataus <> '0' or t1.bjstataus is not null) and t1.rid = '" + feecalcid + "' and nvl(t1.bgfzrid,'0') = '0'";
            }
            return sql_val;
        }
        /// <summary>
        /// 判断是否运输类费目
        /// </summary>
        /// <param name="feecode"></param>
        /// <returns></returns>
        private bool IFYSFee(string feecode)
        {
            bool ys = false;
            string count = DataHelper.QueryValue("select count(*) from SQM_HKYDIC where type = 'ys' and code = '" + feecode + "'") + "";
            if (count != "0")
            {
                ys = true;
            }
            return ys;
        }
        /// <summary>
        /// 处理数据源中的“起运港”、“目的港”别名 ：起运港（海运）=> 起运港
        /// </summary>
        /// <param name="sql_val"></param>
        /// <returns></returns>
        private static string Handleqymd(string sql_val)
        {
            string[] sql_str = { };
            if (sql_val.IndexOf("起运港") >= 0 && sql_val.IndexOf("目的港") >= 0)
            {
                sql_str = sql_val.Split(new string[] { "起运港", "目的港" }, StringSplitOptions.RemoveEmptyEntries);
                int len1 = sql_str[1].IndexOf("\"");
                int len2 = sql_str[2].IndexOf("\"");
                if (len1 > 0)
                {
                    string replace1 = sql_str[1].Substring(0, len1);
                    sql_val = sql_val.Replace(replace1, "");
                }
                if (len2 > 0)
                {
                    string replace2 = sql_str[2].Substring(0, len2);
                    sql_val = sql_val.Replace(replace2, "");
                }
            }
            else if (sql_val.IndexOf("起运港") >= 0 && sql_val.IndexOf("目的港") < 0)
            {
                sql_str = sql_val.Split(new string[] { "起运港" }, StringSplitOptions.RemoveEmptyEntries);
                int len1 = sql_str[1].IndexOf("\"");
                if (len1 > 0)
                {
                    string replace1 = sql_str[1].Substring(0, len1);
                    sql_val = sql_val.Replace(replace1, "");
                }
            }
            else if (sql_val.IndexOf("起运港") < 0 && sql_val.IndexOf("目的港") >= 0)
            {
                sql_str = sql_val.Split(new string[] { "目的港" }, StringSplitOptions.RemoveEmptyEntries);
                int len1 = sql_str[1].IndexOf("\"");
                if (len1 > 0)
                {
                    string replace1 = sql_str[1].Substring(0, len1);
                    sql_val = sql_val.Replace(replace1, "");
                }
            }
            return sql_val;
        }
        /// <summary>
        /// 拼计费因子 运输类费目（配置表类型为ys的费用）费用说明列
        /// </summary>
        /// <param name="sql_val_mb"></param>
        /// <param name="list"></param>
        /// <param name="feecode"></param>
        /// <param name="djrid"></param>
        /// <param name="gdzrid"></param>
        /// <returns></returns>
        private static string getOtherBase(string sql_val_mb, List<string> list, string feecode,string djfsrid,string gdzrid)
        {
            // 得到配置表运输类 费用说明信息
            string basedata_cn = GetBaseData(feecode);
            List<string> ysbase = new List<string>();
            if(basedata_cn != "")
            {
                ysbase = basedata_cn.Split(',').ToList();
            }
            if(ysbase.Count > 0)
            {
                // 筛选配置基础
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    string baseL = list[i].Replace("\"", "");
                    bool match = false;
                    if (baseL.IndexOf(",") >= 0)// 有英文
                    {
                        baseL = baseL.Split(',')[0];
                    }
                    foreach (string ys in ysbase)
                    {
                        if (baseL == ys)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)// 未匹配，从list中删除该元素
                    {
                        list.RemoveAt(i);
                    }
                }
                // 获取 中文基础名称:基础数据 形式的费用说明数据
                if (list.Count == 1)
                {
                    string baseName = "";
                    if (list[0].IndexOf(",") >= 0)// 只有英文的标题转成了 中文+','+英文 的格式，所以要拆分
                    {

                        baseName = Handleqymd(list[0]).Replace("\"", "").Split(',')[0];
                        baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                        sql_val_mb += baseName + list[0].Split(',')[1] + " as \"费用说明\"";
                    }
                    else
                    {
                        baseName = Handleqymd(list[0]).Replace("\"", "");
                        baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                        sql_val_mb += baseName + list[0] + " as \"费用说明\"";
                    }
                }
                else if (list.Count == 0)
                {
                    sql_val_mb += "' ' as \"费用说明\"";
                }
                else
                {
                    string baseName = "";
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i < list.Count - 1)
                        {
                            if (list[i].IndexOf(",") >= 0)
                            {
                                baseName = Handleqymd(list[i]).Replace("\"", "").Split(',')[0];
                                baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                                sql_val_mb += baseName + list[i].Split(',')[1] + " || '\r' || ";
                            }
                            else
                            {
                                baseName = Handleqymd(list[i]).Replace("\"", "");
                                baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                                sql_val_mb += baseName + list[i] + " || '\r' || ";
                            }
                        }
                        else
                        {
                            if (list[i].IndexOf(",") >= 0)
                            {
                                baseName = Handleqymd(list[i]).Replace("\"", "").Split(',')[0];
                                baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                                sql_val_mb += baseName + list[i].Split(',')[1] + " as \"费用说明\"";
                            }
                            else
                            {
                                baseName = Handleqymd(list[i]).Replace("\"", "");
                                baseName = GetBaseNameForFile(baseName, feecode, djfsrid, gdzrid);
                                sql_val_mb += baseName + list[i] + " as \"费用说明\"";
                            }
                        }
                    }
                }
                sql_val_mb = ChangeStr(sql_val_mb);
            }
            else
            {
                sql_val_mb += "' ' as \"费用说明\"";
            }
            return sql_val_mb;
        }
        /// <summary>
        /// 费用说明列中，含有“计费重量”字样的基础，基础名称不出现，同时加上标度
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="feecode"></param>
        /// <returns></returns>
        private static string GetBaseNameForFile(string baseName,string feecode, string djfsrid, string gdzrid)
        {
            string bd = "<=";
            // 根据定价方式获取标度
            if (!string.IsNullOrEmpty(gdzrid))
            {
                bd = DataHelper.QueryValue(string.Format("select distinct scale from sqm_fee_calc_ref where feecode = '{0}' and gdzrid = '{1}' and calcname = '{2}'", feecode, gdzrid, baseName)) + "";
            }
            else if (!string.IsNullOrEmpty(djfsrid))
            {
                bd = DataHelper.QueryValue(string.Format("select distinct scale from sqm_fee_calc_ref where feecode = '{0}' and djfsrid = '{1}' and calcname = '{2}'", feecode, djfsrid, baseName)) + "";
            }
            // 计费重量...，本仓库内天数特殊处理
            if (baseName.IndexOf("计费重量") >= 0)
            {
                baseName = "'" + bd + "' || ";
            }
            else if (baseName.IndexOf("本仓库内天数") >= 0)
            {
                baseName = "'(天)" + bd + "' || ";
            }
            else
            {
                baseName = "'" + baseName + ":' || ";
            }
            return baseName;
        }

        /// <summary>
        /// 单价属性中，含有“计费重量”或者“本仓库内天数”字样的基础，加上标度
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="feecode"></param>
        /// <returns></returns>
        private static string GetDJProperty(string baseName, string feecode, string djfsrid, string gdzrid)
        {
            string bd = "";
            baseName = baseName.Replace("\"", "");
            // 计费重量...，本仓库内天数特殊处理
            if (baseName.IndexOf("计费重量") >= 0 || baseName.IndexOf("本仓库内天数") >= 0)
            {
                // 根据定价方式获取标度
                if (!string.IsNullOrEmpty(gdzrid))
                {
                    bd += DataHelper.QueryValue(string.Format("select distinct scale from sqm_fee_calc_ref where feecode = '{0}' and gdzrid = '{1}' and calcname = '{2}'", feecode, gdzrid, baseName)) + "";
                    bd = "'" + bd + "' || ";
                }
                else if (!string.IsNullOrEmpty(djfsrid))
                {
                    bd += DataHelper.QueryValue(string.Format("select distinct scale from sqm_fee_calc_ref where feecode = '{0}' and djfsrid = '{1}' and calcname = '{2}'", feecode, djfsrid, baseName)) + "";
                    bd = "'" + bd + "' || ";
                }
            }
            return bd;
        }

        /// <summary>
        /// 得到配置表运输类费用说明信息
        /// </summary>
        /// <param name="feecode"></param>
        /// <returns></returns>
        private static string GetBaseData(string feecode)
        {
            string basedata = "";
            basedata = DataHelper.QueryValue("select EXT1 from sqm_hkydic where type = 'ys' and code = '" + feecode + "'") + "";// 获取基础中文
            return basedata;
        }
        /// <summary>
        /// 得到配置表运输类单价属性信息
        /// </summary>
        /// <param name="feecode"></param>
        /// <returns></returns>
        private static string GetPriceProperty(string feecode)
        {
            string basedata = "";
            basedata = DataHelper.QueryValue("select EXT3 from sqm_hkydic where type = 'ys' and code = '" + feecode + "'") + "";// 获取基础中文
            return basedata;
        }
        /// <summary>
        /// 名称改变：源位置的运输区域=>起始地   目标位置的运输区域=> 目的地
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static string ChangeStr(string str)
        {
            str = str.Replace("源位置的运输区域", "起始地").Replace("目标位置的运输区域", "目的地");
            return str;
        }
        /// <summary>
        /// 去掉字段的英文代码
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string ClearENcode(string columns)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(columns))
            {
                if (columns.IndexOf(',') >= 0)
                {
                    string[] columnNames = columns.Split(',');
                    for (int c = 0; c < columnNames.Length; c++)
                    {
                        string asbefore = columnNames[c].Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        string asafter = columnNames[c].Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim().Replace("\"", "");
                        if (asafter.IndexOf("(") >= 0)
                        {
                            string colname = asafter.Split('(')[0];
                            list.Add(asbefore + " as " + "\"" + colname + "\"");
                        }
                        else
                        {
                            list.Add(asbefore + " as " + "\"" + asafter + "\"");
                        }
                    }
                }
                else
                {
                    string asbefore = columns.Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();// 值表字段名：column1,column2
                    string asafter = columns.Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim().Replace("\"", "");// 别名
                    if (asafter.IndexOf("(") >= 0)
                    {
                        string colname = asafter.Split('(')[0];
                        list.Add(asbefore + " as " + "\"" + colname + "\"");
                    }
                    else
                    {
                        list.Add(asbefore + " as " + "\"" + asafter + "\"");
                    }
                }
            }
            return String.Join(",", list.ToArray());
        }
        /// <summary>
        /// 从关系表查询费目对应基础
        /// </summary>
        /// <param name="feecode"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        private static string getColumns(string feecode, string unit, string djfsrid, string gdzrid)
        {
            string sql_col = "";
            if (gdzrid != "")
            {
                sql_col = @"select wm_concat(to_char(valcol) || ' as ""' || to_char(calcname) || '(' || to_char(calccode) || ')""') from (select distinct valcol,calcname,calccode from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and gdzrid = '" + gdzrid + "')";
            }
            else if (djfsrid != "")
            {
                sql_col = @"select wm_concat(to_char(valcol) || ' as ""' || to_char(calcname) || '(' || to_char(calccode) || ')""') from (select distinct valcol,calcname,calccode from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and djfsrid = '" + djfsrid + "')";
            }
            else
            {
                sql_col = @"select wm_concat(to_char(valcol) || ' as ""' || to_char(calcname) || '(' || to_char(calccode) || ')""') from (select distinct valcol,calcname,calccode from sqm_fee_calc_ref where status = '1' and feecode = '" + feecode + "' and (djfsrid = '' or djfsrid is null))";
            }
            // 查询字段表
            string columns = DataHelper.QueryValue(sql_col) + "";
            return columns;
        }
        /// <summary>
        /// 处理别名过长 去掉中文描述，只保留英文描述
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string HandleColumn(string columns)
        {
            // 处理别名过长（超过30个字节）
            if (!string.IsNullOrEmpty(columns))
            {
                int len = 0;
                string[] arr;
                if (columns.IndexOf(",") >= 0)
                {
                    arr = columns.Split(',');
                }
                else
                {
                    arr = new string[] { columns };
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    string asafter = arr[i].Split(new string[] { "as" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim().Replace("\"", "");
                    len = getStringLength(asafter);
                    if (len >= 30)
                    {
                        //if (asafter.IndexOf("（") >= 0)
                        //{
                        //    columns = columns.Replace(asafter, asafter.Split('（')[0]);
                        //}
                        //else
                        //{
                            columns = columns.Replace(asafter, asafter.Split('(')[1].Split(')')[0]);
                        //}
                    }
                }
            }
            return columns;
        }
        /// <summary>
        /// 计算一个字符串的字节数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int getStringLength(string str)
        {
            if (str.Equals(string.Empty))
            {
                return 0;
            }
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i < strBytes.Length; i++)
            {
                // Oracle 字符集 select userenv('language') from dual;
                //如果显示如下，一个汉字占用两个字节
                //SIMPLIFIED CHINESE_CHINA.ZHS16GBK
                //如果显示如下，一个汉字占用三个字节
                //SIMPLIFIED CHINESE_CHINA.AL32UTF8

                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号 中文一个汉字两个字节
                {
                    strlen += 2;
                }
                strlen++;
            }
            return strlen;
        }
        /// <summary>
        /// 导入Excel--原始数据
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostExcelData()
        {
            // 将数据存放进内存
            DataTable dtjc = DataHelper.QueryDataTable("select feecode,djfsrid,gdzrid,wm_concat(to_char(calccode)) as jccode from sqm_fee_calc_ref where status = '1' group by djfsrid,gdzrid,feecode");

            List<string> jcErrorLog = new List<string>();// 基础错误
            List<string> dataErrorLog = new List<string>();// 主数据错误
            string arraylist = Request["alist"];
            string test = "";
            List<string> colname = new List<string>();
            List<string> names = new List<string>();
            string info = string.Empty;
            List<DataSet> listDs = new List<DataSet>();
            ArrayList alist = new ArrayList();
            // 从配置文件获取废弃费目代码
            List<string> feecodeArr = GetCodeFromConfig("FEECODE");
            // 从配置文件获取废弃费目代码
            List<string> calccodeArr = GetCodeFromConfig("CALCCODE");
            // 从配置文件获取错误费目代码更正
            Dictionary<string, string> wrongfeecode = GetWrongCodeFromConfig("WRONGFEECODE");

            if (!string.IsNullOrEmpty(arraylist))
            {
                alist = JsonHelper.GetObject<ArrayList>(arraylist);
                listDs = JsonHelper.GetObject<List<DataSet>>(JsonHelper.GetJsonString(alist[0]));
                names = JsonHelper.GetObject<List<string>>(JsonHelper.GetJsonString(alist[1]));
                for (int i = 0; i < names.Count; i++)
                {
                    listDs[i].DataSetName = names[i];
                }
            }
            //DataSet ds = new DataSet();
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
                        al = GetDataFromExcel(stream);
                    }
                    if (al.Count > 1)// 初步校验结果
                    {
                        listDs = (List<DataSet>)al[0];
                        // 获取sheet名称
                        string sheetName = listDs[int.Parse(al[2].ToString())].DataSetName;
                        // 获取校验结果
                        string result = al[1].ToString();
                        if (result == "数据为空")
                        {
                            return Content(new JsonMessage { Code = "1", Message = "导入失败：sheet表：\"" + sheetName + "\" 数据为空" }.ToString());
                        }
                        else if (result.IndexOf(",") >= 0)
                        {
                            return Content(new JsonMessage { Code = "1", Message = "导入失败：sheet表：\"" + sheetName + "\" 行" + result.Split(',')[0] + "列" + result.Split(',')[1] + " " + result.Split(',')[2] }.ToString());
                        }
                        else
                        {
                            return Content(new JsonMessage { Code = "1", Message = "sheet表：\"" + sheetName + "\"导入失败" }.ToString());
                        }
                    }
                    else
                    {
                        if (listDs.Count == 0)
                        {
                            listDs = (List<DataSet>)al[0];
                        }
                        string bpcodeStr = "";
                        // 主数据校验 入库时计算基础如果没有code值，自动带出code，并将code放入值表的columnc列。
                        // 产品code校验、产品下对应服务code校验，服务下对应费目code校验
                        for (int i = 0; i < listDs.Count; i++)
                        {
                            if (listDs[i].DataSetName == "客户及组织")
                            {
                                bpcodeStr = listDs[i].Tables[0].Rows[0]["客户代码"].ToString();
                                string bjname = listDs[i].Tables[0].Rows[0]["报价名称"].ToString();
                                string bpcode = listDs[i].Tables[0].Rows[0]["客户代码"].ToString();
                                string orgcode = listDs[i].Tables[0].Rows[0]["组织代码"].ToString();
                                if (string.IsNullOrEmpty(arraylist))
                                {
                                    int count = Convert.ToInt32(DataHelper.ExecSql("select count(*) from sqm_bj_main_basic t1,sqm_bj_bp t2,sqm_bj_org t3 where t1.rid = t2.mrid and t1.rid = t3.mrid and t1.bjname = '" + bjname + "' and t2.bpcode = '" + bpcode + "' and t3.orgcode = '" + orgcode + "'"));
                                    if (count > 0)
                                    {
                                        for (int n = 0; n < listDs.Count; n++)
                                        {
                                            names.Add(listDs[n].DataSetName);
                                        }
                                        ArrayList setAndName = new ArrayList();
                                        setAndName.Add((List<DataSet>)al[0]);
                                        setAndName.Add(names);
                                        return Content(new JsonMessage { Message = "报价名称为\"" + bjname + "\"，客户代码为\"" + bpcode + "\",组织代码为\"" + orgcode + "\"的报价已存在！点\"继续\"按钮将覆盖原始数据，是否继续？", Code = "ifexist", Data = setAndName }.ToString());
                                    }
                                }
                                else // 先删除原有数据
                                {
                                    // 根据excel信息（报价名称 + 客户code + 组织code）得到主表rid
                                    string sqlll = "select t1.rid from sqm_bj_main_basic t1, sqm_bj_bp t2, sqm_bj_org t3 where t1.rid = t2.mrid and t1.rid = t3.mrid and t1.bjname = '" + bjname + "' and t2.bpcode = '" + bpcode + "' and t3.orgcode = '" + orgcode + "'";
                                    string mrid_delete = DataHelper.QueryValue(sqlll) + "";
                                    // 得到feecalcid
                                    IList<EasyDictionary> listeasy = DataHelper.QueryDictList("select RID from sqm_bj_psf where mrid ='" + mrid_delete + "'");
                                    // 删除sqm_modebj_val表
                                    List<string> delete = new List<string>();
                                    for (int m = 0; m < listeasy.Count; m++)
                                    {
                                        string sql_smv = "delete from sqm_modebj_val where feecalcid ='" + listeasy[m].Get("RID") + "'";
                                        delete.Add(sql_smv);
                                    }
                                    string sqls = string.Join(";", delete.ToArray()) + ";";
                                    // 删除sqm_bj_main_basic、sqm_bj_ver、sqm_bj_bp、sqm_bj_org、sqm_bj_psf表
                                    if (mrid_delete != "" && sqls.Trim() != ";")
                                    {
                                        string sql = "begin delete from sqm_bj_main_basic where rid = '" + mrid_delete + "';delete from sqm_bj_ver where mrid ='" + mrid_delete + "';delete from sqm_bj_bp where mrid ='" + mrid_delete + "';delete from sqm_bj_org where mrid ='" + mrid_delete + "';delete from sqm_bj_psf where mrid ='" + mrid_delete + "';" + sqls + "end;";
                                        DataHelper.ExecSql(sql);
                                    }
                                }
                            }
                            else
                            {
                                DataSet ds = listDs[i];
                                string[] arr = ds.DataSetName.Trim().Split('-');
                                string productCode = ds.DataSetName.Trim().Split('-')[arr.Length - 1];// 如果出现产品名称中带"-"的，第二个不一定是产品代码  原始代码
                                string producntName = ds.DataSetName.Trim().Substring(0, ds.DataSetName.Trim().LastIndexOf("-")); // 原始名称
                                // 转换成系统代码
                                string productCodesys = DataHelper.QueryValue("select NAME from SQM_HKYDIC where type = 'code' and CODE = '" + productCode + "' or NAME = '" + productCode + "'") + "";
                                // 判断产品code主数据是否存在
                                string prdname = MainDataExist(productCodesys, productCode, "product");
                                if (prdname == "&&")
                                {
                                    return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + producntName + "-" + productCode + "\"，产品：\"" + productCode + "\" 主数据不存在！", Code = "1" }.ToString());
                                }
                                else
                                {
                                    ds.DataSetName = prdname.Replace("&&", "") + "-" + productCodesys;
                                }
                                // 基础匹配 主数据校验
                                for (int m = ds.Tables.Count - 1; m >= 0; m--)// 删除dt，所以倒序
                                {
                                    // 费目代码不能重复 同一个Table不能有两个及以上费目
                                    // 一个Table非第一行不能是At cost
                                    if (ds.Tables[m].Rows.Count > 1)
                                    {
                                        List<string> list = new List<string>();
                                        list = CheckDJRID(ds.Tables[m], "2");
                                        if (list[0] == "代码重复")
                                        {
                                            dataErrorLog.Add("表：" + m + "，sheet表：\"" + producntName + "-" + productCode + "\"，费目为：\"" + ds.Tables[m].Rows[0]["费目"].ToString() + "\" 的数据，费目代码冲突，违反填表规则！");
                                        }
                                        if (list[1] == "COST")
                                        {
                                            dataErrorLog.Add("表：" + m + "，sheet表：\"" + producntName + "-" + productCode + "\"，费目为：\"" + ds.Tables[m].Rows[0]["费目"].ToString() + "\" 的数据，报价方式冲突，请确认！");
                                        }
                                    }
                                    bool ifjump = false;
                                    string jccode = "";
                                    string serviceName = ds.Tables[m].Rows[0]["服务"].ToString();
                                    string serviceCode = ds.Tables[m].Rows[0]["服务代码"].ToString();
                                    string feeName = ds.Tables[m].Rows[0]["费目"].ToString();
                                    string feeCode = ds.Tables[m].Rows[0]["费目代码"].ToString();
                                    // 跳过费目：出现以下费目，数据不入库，从dt中删除
                                    foreach (string feec in feecodeArr)
                                    {
                                        if (feec == feeCode)
                                        {
                                            ifjump = true;
                                            break;
                                        }
                                    }
                                    if (ifjump)
                                    {
                                        ds.Tables.RemoveAt(m);// 删除dt
                                        continue;
                                    }
                                    // 兼容调整
                                    // Excel费目对应代码错误
                                    foreach (KeyValuePair<string, string> keyValue in wrongfeecode)
                                    {
                                        if (feeName.Replace("（", "(").Replace("）", ")") == keyValue.Key)
                                        {
                                            ds.Tables[m].Rows[0]["费目代码"] = keyValue.Value;
                                            feeCode = keyValue.Value;
                                            break;
                                        }
                                    }
                                    // Excel费目对应服务代码错误
                                    if (feeName == "分货费（监管）" || feeName == "分货费(监管)")
                                    {
                                        ds.Tables[m].Rows[0]["服务代码"] = "A00003";
                                    }
                                    // 纠正AT COSY
                                    if ((ds.Tables[m].Rows[0]["报价"] + "").IndexOf("COSY") >= 0)
                                    {
                                        ds.Tables[m].Rows[0]["报价"] = "AT COST";
                                    }
                                    // 基础匹配，基础不匹配也没必要校验主数据
                                    bool ifmatch = false;
                                    if ((ds.Tables[m].Rows[0]["报价"] + "").IndexOf("COST") >= 0 || (ds.Tables[m].Rows[0]["报价"] + "").IndexOf("Cost") >= 0 || (ds.Tables[m].Rows[0]["报价"] + "").IndexOf("cost") >= 0 || (ds.Tables[m].Rows[0]["报价"] + "" == "单票单询"))// || (ds.Tables[m].Rows[0]["报价"] + "" == "")
                                    {
                                        ifmatch = true;
                                    }
                                    else
                                    {
                                        List<string> excelArr = new List<string>();// Excel的基础code
                                        foreach (DataColumn col in ds.Tables[m].Columns)
                                        {
                                            if (col.ColumnName.IndexOf("olumn") < 0)
                                            {
                                                if (!Regex.IsMatch(col.ColumnName, @"[\u4E00-\u9FA5]+$") && col.ColumnName.IndexOf("最高报价") < 0)//基础名称为非汉字类型   最高报价/MAX 不会被过滤掉
                                                {
                                                    // 忽略已废弃基础
                                                    bool ifstop = false;
                                                    foreach (string calccode in calccodeArr)
                                                    {
                                                        if (col.ColumnName == calccode)
                                                        {
                                                            ifstop = true;
                                                            break;
                                                        }
                                                    }
                                                    if (!ifstop)
                                                    {
                                                        excelArr.Add(col.ColumnName);
                                                    }
                                                }
                                            }
                                        }
                                        DataRow[] drsjc = dtjc.Select("feecode = '" + feeCode + "'");
                                        DataTable drswjc = DataHelper.QueryDataTable("select * from sqm_fee_pur_ref where djfsrid not in(select djfsrid from sqm_fee_calc_ref where feecode = '" + feeCode + "') and feecode = '" + feeCode + "'");
                                        string djfsrid = "";
                                        string gdzrid = "";
                                        // 给DataTable 添加DJFSRID/GDZRID两列
                                        ds.Tables[m].Columns.Add("DJFSRID");
                                        ds.Tables[m].Columns.Add("GDZRID");
                                        if (drsjc.Length > 0 && excelArr.Count != 0)// 库里有基础 ,Excel有基础
                                        {
                                            foreach (DataRow dr in drsjc)
                                            {
                                                djfsrid = dr["DJFSRID"] + "";
                                                gdzrid = dr["GDZRID"] + "";
                                                jccode = dr["JCCODE"] + "";
                                                if (CompareArr(excelArr.ToArray(), jccode.Split(',')))// 如果匹配到，则用这套基础，循环结束
                                                {
                                                    //ds.Tables[m].Rows[0]["DJFSRID"] = djfsrid;
                                                    //ds.Tables[m].Rows[0]["GDZRID"] = gdzrid;
                                                    ifmatch = true;
                                                    break;
                                                }
                                            }
                                            for (int d = 0; d < ds.Tables[m].Rows.Count; d++)
                                            {
                                                ds.Tables[m].Rows[d]["DJFSRID"] = djfsrid;
                                                ds.Tables[m].Rows[d]["GDZRID"] = gdzrid;
                                            }
                                        }
                                        else if (drswjc.Rows.Count > 0 && excelArr.Count == 0)// 库里无基础（有定价方式），Excel无基础（按票算）
                                        {
                                            ds.Tables[m].Rows[0]["DJFSRID"] = drswjc.Rows[0]["DJFSRID"];// 任意取一个，就取第一个
                                            ifmatch = true;
                                        }
                                    }
                                    if (!ifmatch)
                                    {
                                        // 记录sheet 服务 费目 
                                        jcErrorLog.Add("表：" + m + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务：\"" + serviceName + "\"，费目：\"" + feeName + "\"，无此定价方式，数据无法导入！");
                                        continue;
                                    }
                                    // 判断产品服务费目是否有关系--暂时去掉该功能
                                    //string proserrel = MainDataExist(productCode + "," + serviceCode, "service");
                                    //if (proserrel == "0")
                                    //{
                                    //    return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 产品代码为 \"" + productCode + "\" 的产品中不存在服务代码 \"" + serviceCode + "\"", Code = "1" }.ToString());
                                    //}
                                    //string serfeerel = MainDataExist(serviceCode + "," + feeCode, "fee");
                                    //if (serfeerel == "0")
                                    //{
                                    //    return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务代码为 \"" + serviceCode + "\" 的服务中不存在费目代码 \"" + feeCode + "\"", Code = "1" }.ToString());
                                    //}
                                    for (int n = ds.Tables[m].Rows.Count - 1; n >= 0; n--)
                                    {
                                        // 报价校验
                                        if (ds.Tables[m].Rows[n]["报价"] + "" == "")
                                        {
                                            ds.Tables[m].Rows.RemoveAt(n);// 因为要删除row，所以倒序遍历dt，保证未删行索引可用
                                            continue;
                                        }
                                        // 跳过费目：ATCOST 单票单询
                                        if ((ds.Tables[m].Rows[n]["报价"] + "").IndexOf("COST") >= 0 || (ds.Tables[m].Rows[n]["报价"] + "").IndexOf("cost") >= 0 || (ds.Tables[m].Rows[n]["报价"] + "").IndexOf("Cost") >= 0 || (ds.Tables[m].Rows[n]["报价"] + "").IndexOf("单票单询") >= 0)
                                        {
                                            continue;
                                        }
                                        // Excel基础为空（可能按票算）且数据库中未匹配到基础，即定价方式为空
                                        if (jccode == "")
                                        {
                                            continue;
                                        }
                                        // 基础
                                        string[] jcArr = jccode.Split(',');
                                        // 结算方代码校验
                                        if (ds.Tables[m].Rows[n]["结算方代码"] + "" == "")
                                        {
                                            listDs[i].Tables[m].Rows[n]["结算方代码"] = bpcodeStr;
                                        }
                                        if (jcArr.Length > 0)// 普通计费基础
                                        {
                                            // 基础校验
                                            foreach (string jsjccode in jcArr)
                                            {
                                                // 是否为空校验 如果不是X类型，其他基础不能为空
                                                if (!CheckIfX(jsjccode))
                                                {
                                                    if (ds.Tables[m].Rows[n][jsjccode] + "" == "")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"\" 数据为空");
                                                    }
                                                }
                                                // 主数据校验
                                                if (CheckJc(jsjccode) || jsjccode == "SOURCELOC_ZONE")
                                                {
                                                    // 通用主数据 MDM
                                                    if (jsjccode.IndexOf("GJ") >= 0) // 国家
                                                    {
                                                        string result = "";
                                                        if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                        {
                                                            result = MainDataCheck(listDs, i, ds, m, n, jsjccode, "1");
                                                        }
                                                        else
                                                        {
                                                            ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                        }
                                                        if (result == "0")
                                                        {
                                                            dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        }
                                                    }
                                                    // MDMLOC
                                                    else if ((jsjccode.IndexOf("QYG") >= 0 || jsjccode.IndexOf("MDG") >= 0 || jsjccode.IndexOf("ZZG") >= 0 || jsjccode.IndexOf("ZYG") >= 0) && jsjccode != "ZZGFS")// 港口
                                                    {
                                                        string result = "";
                                                        if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                        {
                                                            result = MainDataCheck(listDs, i, ds, m, n, jsjccode, "2");
                                                        }
                                                        else
                                                        {
                                                            ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                        }
                                                        if (result == "0")
                                                        {
                                                            dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        }
                                                    }
                                                    // MDMBP
                                                    else if (jsjccode.IndexOf("CGS") >= 0)// 航空公司、船公司  jsjccode.IndexOf("HKGS") >= 0 || 航空公司在基础主数据校验
                                                    {
                                                        string result = "";
                                                        if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                        {
                                                            result = MainDataCheck(listDs, i, ds, m, n, jsjccode, "4");
                                                        }
                                                        else
                                                        {
                                                            ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                        }
                                                        if (result == "0")
                                                        {
                                                            dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        }
                                                    }
                                                    // 计算基础 MDMJC
                                                    else
                                                    {
                                                        string jcvalue = "";
                                                        // 基础有的，模板没有的进行校验
                                                        if (ds.Tables[m].Columns.Contains(jsjccode))
                                                        {
                                                            jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
                                                        }
                                                        if (jcvalue != "" && jcvalue != "null")
                                                        {
                                                            try
                                                            {
                                                                string codeName = "";
                                                                if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                                {
                                                                    codeName = MainDataExist(jsjccode, ds.Tables[m].Rows[n][jsjccode].ToString(), "6");
                                                                }
                                                                else
                                                                {
                                                                    codeName = "*&&*";
                                                                }
                                                                if (codeName.Trim() == "&&")
                                                                {
                                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                                }
                                                                else
                                                                {
                                                                    listDs[i].Tables[m].Rows[n][jsjccode] = codeName;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dataErrorLog.Add("导入失败：" + ex.Message + " sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的数据，基础为: " + jsjccode + "的主数据配置错误");
                                                            }
                                                        }
                                                        else// 主数据里null值校验
                                                        {
                                                            try
                                                            {
                                                                string codeName = MainDataExist(jsjccode, "null", "6");
                                                                if (codeName.Trim() == "&&")
                                                                {
                                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                                }
                                                                else
                                                                {
                                                                    listDs[i].Tables[m].Rows[n][jsjccode] = codeName;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dataErrorLog.Add("导入失败：" + ex.Message + " sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的数据，基础为: " + jsjccode + "的主数据配置错误！");
                                                            }
                                                        }
                                                    }
                                                }
                                                // 不校验主数据的基础，校验长度
                                                else
                                                {
                                                    string result = CheckData(jsjccode, ds.Tables[m].Rows[n][jsjccode] + "");
                                                    if (result == "1")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 整数位位数超限");
                                                    }
                                                    else if (result == "2")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 小数位位数超限");
                                                    }
                                                    else if (result == "3")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 字数超限");
                                                    }
                                                    else if (result == "4")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 应为'X'或空");
                                                    }
                                                    else if (result == "5")
                                                    {
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + producntName + "-" + productCode + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 应为'Y'或'N'");
                                                    }
                                                    else if (result == "X")// 校正x小写
                                                    {
                                                        listDs[i].Tables[m].Rows[n][jsjccode] = "X";
                                                    }
                                                    else if (result == "Y")// 校正y小写
                                                    {
                                                        listDs[i].Tables[m].Rows[n][jsjccode] = "Y";
                                                    }
                                                    else if (result == "N")// 校正n小写
                                                    {
                                                        listDs[i].Tables[m].Rows[n][jsjccode] = "N";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (jcErrorLog.Count > 0)// 存在定价方式不存在，不予入库，导入失败
                        {
                            return Content(new JsonMessage { Message = string.Join("<BR>", jcErrorLog.ToArray()), Code = "1" }.ToString());
                        }
                        else if (dataErrorLog.Count > 0)// 主数据校验问题，不予入库，导入失败
                        {
                            return Content(new JsonMessage { Message = string.Join("<BR>", dataErrorLog.ToArray()), Code = "1" }.ToString());
                        }
                        // 数据入库
                        string mrid = string.Empty;
                        List<string> feecalcid = new List<string>();
                        string verrid = string.Empty;
                        string startdate = string.Empty;
                        string enddate = string.Empty;
                        try
                        {
                            verrid = System.Guid.NewGuid().ToString();
                            foreach (DataSet ds in listDs)
                            {
                                string createTime = DateTime.Now.ToString();
                                string createUser = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                string createId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                string bpcodeasjsfdm = "";
                                if (ds.DataSetName == "客户及组织")
                                {
                                    mrid = System.Guid.NewGuid().ToString();
                                    string dtfrom = ds.Tables[0].Rows[0]["起始日期"].ToString();
                                    startdate = dtfrom;
                                    string dtto = ds.Tables[0].Rows[0]["截止日期"].ToString();
                                    enddate = dtto;
                                    string bpname = ds.Tables[0].Rows[0]["客户名称"].ToString();
                                    string bpcode = ds.Tables[0].Rows[0]["客户代码"].ToString();
                                    string bjname = ds.Tables[0].Rows[0]["报价名称"].ToString();
                                    bpcodeasjsfdm = bpcode;
                                    string orgname = ds.Tables[0].Rows[0]["报价组织"].ToString();
                                    string orgcode = ds.Tables[0].Rows[0]["组织代码"].ToString();
                                    string sql_main = "insert into sqm_bj_main_basic(RID,CREATETIME,CREATEUSER,CREATEID,BJNAME,DTFROM,DTTO,ORIGINAL) values('{0}',to_date('{1}','yyyy/mm/dd hh24:mi:ss'),'{2}','{3}','{4}',to_date('{5}','yyyy/mm/dd'),to_date('{6}','yyyy/mm/dd'),'{7}')";
                                    string sql_ver = "insert into sqm_bj_ver(RID,MRID,CREATETIME,CREATEUSER,CREATEID,DTFROM,DTTO,ZVER) values('{0}','{1}',to_date('{2}','yyyy/mm/dd hh24:mi:ss'),'{3}','{4}',to_date('{5}','yyyy/mm/dd'),to_date('{6}','yyyy/mm/dd'),'{7}')";
                                    string sql_bp = "insert into sqm_bj_bp(RID,MRID,CREATETIME,CREATEUSER,CREATEID,BPNAME,BPCODE) values('{0}','{1}',to_date('{2}','yyyy/mm/dd hh24:mi:ss'),'{3}','{4}','{5}','{6}')";
                                    string sql_org = "insert into sqm_bj_org(RID,MRID,CREATETIME,CREATEUSER,CREATEID,ORGNAME,ORGCODE) values('{0}','{1}',to_date('{2}','yyyy/mm/dd hh24:mi:ss'),'{3}','{4}','{5}','{6}')";
                                    // 1.主表:sqm_bj_main_basic 插数
                                    DataHelper.ExecSql(String.Format(sql_main, mrid, createTime, createUser, createId, bjname, dtfrom, dtto, "1"));
                                    // 2.版本信息表:sqm_bj_ver 插数
                                    string zver = "V1";
                                    DataHelper.ExecSql(String.Format(sql_ver, verrid, mrid, createTime, createUser, createId, dtfrom, dtto, zver));
                                    // 3.客户表:sqm_bj_bp 插数
                                    string bprid = System.Guid.NewGuid().ToString();
                                    DataHelper.ExecSql(String.Format(sql_bp, bprid, mrid, createTime, createUser, createId, bpname, bpcode));
                                    // 4.组织表:sqm_bj_org 插数
                                    string orgrid = System.Guid.NewGuid().ToString();
                                    DataHelper.ExecSql(String.Format(sql_org, orgrid, mrid, createTime, createUser, createId, orgname, orgcode));
                                }
                                else
                                {
                                    string[] sheetname = ds.DataSetName.Trim().Split('-');
                                    string productCode = ds.DataSetName.Trim().Split('-')[sheetname.Length - 1];
                                    string productName = ds.DataSetName.Trim().Substring(0, ds.DataSetName.Trim().LastIndexOf("-") - 0);
                                    foreach (DataTable dt in ds.Tables)
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
                                        if (dt.Rows.Count > 0)
                                        {
                                            // 每一个DataTable都是一个费目，所以取第一行的服务、费目即可  
                                            // 报价方式：bjfs  1-At cost、2-单票单询
                                            string bjfs = "0";
                                            string serviceName = dt.Rows[0]["服务"].ToString();
                                            string serviceCode = dt.Rows[0]["服务代码"].ToString();
                                            string feeName = dt.Rows[0]["费目"].ToString();
                                            string feeCode = dt.Rows[0]["费目代码"].ToString();
                                            test = feeCode;
                                            string bjstataus = "2";// 导入后的数据为“确认”状态报价数据
                                            string psf_rid = string.Empty;
                                            string val_rid = string.Empty;
                                            string bz = string.Empty;
                                            string jsf = string.Empty;
                                            string memo = string.Empty;
                                            string bjprice = string.Empty;
                                            string minbjprice = string.Empty;// 只取第一行最低报价（一个方式只能有一个最低报价，即一个datatable只能有一个最低报价）
                                            string jxjc = string.Empty;
                                            string jsjcms = string.Empty;
                                            string jsjccode = string.Empty;
                                            string jsjcqt = string.Empty;
                                            string calcunit = string.Empty;
                                            string original = "1";// 历史数据标志
                                            string djfsrid = "";
                                            string gdzrid = "";
                                            // 5.产品服务费目表：sqm_bj_psf 插数 
                                            psf_rid = DataHelper.QueryValue("select RID from sqm_bj_psf where fee_code = '" + feeCode + "' and service_code = '" + serviceCode + "' and product_code ='" + productCode + "' and vrid ='" + verrid + "'") + "";
                                            string sql_psf = "";
                                            if (string.IsNullOrEmpty(psf_rid))// 如果psf没有数据则插入数据
                                            {
                                                sql_psf = "insert into sqm_bj_psf(RID,MRID,VRID,BJSTATAUS,CREATETIME,CREATEUSER,PRODUCT_NAME,PRODUCT_CODE,SERVICE_NAME,SERVICE_CODE,FEE_NAME,FEE_CODE,CREATEID,BJFS,STATUS,BJSTARTDATE,BJENDDATE,FEECATG) values('{0}','{1}','{2}','{3}',to_date('{4}','yyyy/mm/dd hh24:mi:ss'),'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','1',to_date('{14}','yyyy/mm/dd'),to_date('{15}','yyyy/mm/dd'),'{16}')";
                                                psf_rid = System.Guid.NewGuid().ToString();
                                                feecalcid.Add(psf_rid);
                                                bjprice = dt.Rows[0]["报价"].ToString();
                                                if (bjprice.IndexOf("COST") >= 0 || bjprice.IndexOf("Cost") >= 0 || bjprice.IndexOf("cost") >= 0)
                                                {
                                                    bjfs = "1";
                                                }
                                                if (bjprice.IndexOf("单票单询") >= 0)
                                                {
                                                    bjfs = "2";
                                                }
                                                sql_psf = String.Format(sql_psf, psf_rid, mrid, verrid, bjstataus, createTime, createUser, productName, productCode, serviceName, serviceCode, feeName, feeCode, createId, bjfs, startdate, enddate, bjfs == "1" ? "2" : "");
                                                DataHelper.ExecSql(sql_psf);
                                            }
                                            // 获取计费基础，原始数据导入基础
                                            DataTable dtdata = new DataTable();
                                            if (dt.Columns.Contains("DJFSRID")) { djfsrid = dt.Rows[0]["DJFSRID"] + ""; } // 每个table只有一个定价方式ID
                                            if (dt.Columns.Contains("GDZRID")) { gdzrid = dt.Rows[0]["GDZRID"] + ""; } // 每个table都只有一个高低值ID
                                            if (gdzrid != "")
                                            {
                                                dtdata = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and GDZRID = '" + gdzrid + "'");
                                            }
                                            else if (djfsrid != "")
                                            {
                                                dtdata = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID = '" + djfsrid + "'");
                                            }
                                            else
                                            {
                                                dtdata = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID is null");
                                            }
                                            // 6.报价值表：sqm_modebj_val 表插数  atcost跟单票单询不插数据
                                            if (bjfs != "1" && bjfs != "2")
                                            {
                                                List<string> sqls = new List<string>();
                                                foreach (DataRow dr in dt.Rows)
                                                {
                                                    string sql_value_insert = "insert into sqm_modebj_val(RID,BJSTATUS,CREATETIME,CREATEUSER,CREATEID,FEECALCID,CURRENCY,JSFCODE,MEMO,BJPRICE,CALCUNIT,MINBJPRICE,JXJC,CALCNAME,CALCCODE,BJFS,CONDITION,ORIGINAL,STARTDATE,ENDDATE,STATUS,DJFSRID,GDZRID,IFBJITEM";
                                                    string sql_value_values = " values(";
                                                    if (dr.Table.Columns.Contains("币种")) { bz = dr["币种"].ToString(); }
                                                    if (dr.Table.Columns.Contains("结算方代码")) { jsf = dr["结算方代码"].ToString(); }
                                                    if (dr.Table.Columns.Contains("备注")) { memo = dr["备注"].ToString(); }
                                                    if (dr.Table.Columns.Contains("报价")) { bjprice = dr["报价"].ToString(); }
                                                    if (dr.Table.Columns.Contains("报价单位")) { calcunit = dr["报价单位"].ToString(); }
                                                    if (bjprice.IndexOf("COST") >= 0 || bjprice.IndexOf("Cost") >= 0 || bjprice.IndexOf("cost") >= 0)
                                                    {
                                                        bjfs = "1";
                                                        bjprice = "";
                                                    }
                                                    if (bjprice.IndexOf("单票单询") >= 0)
                                                    {
                                                        bjfs = "2";
                                                        bjprice = "";
                                                    }
                                                    if (string.IsNullOrEmpty(minbjprice))// 如果不为空，则说明已经取到第一行的值
                                                    {
                                                        if (dr.Table.Columns.Contains("最低报价")) { minbjprice = dr["最低报价"].ToString(); }
                                                    }
                                                    if (dr.Table.Columns.Contains("建议解析基础")) { jxjc = dr["建议解析基础"].ToString(); }
                                                    if (dr.Table.Columns.Contains("计算基础描述")) { jsjcms = dr["计算基础描述"].ToString(); }
                                                    if (dr.Table.Columns.Contains("计算基础代码")) { jsjccode = dr["计算基础代码"].ToString(); }
                                                    if (dr.Table.Columns.Contains("计算基础前提条件")) { jsjcqt = dr["计算基础前提条件"].ToString(); }
                                                    val_rid = System.Guid.NewGuid().ToString();
                                                    sql_value_values += "'" + val_rid + "','" + bjstataus + "',to_date('" + createTime + "','yyyy/mm/dd hh24:mi:ss'),'" + createUser + "','" + createId + "','" + psf_rid + "','" + bz + "','" + jsf + "','" + memo + "','" + bjprice + "','" + calcunit + "','" + minbjprice + "','" + jxjc + "','" + jsjcms + "','" + jsjccode + "','" + bjfs + "','" + jsjcqt + "','" + original + "',to_date('" + startdate + "','yyyy/mm/dd'),to_date('" + enddate + "','yyyy/mm/dd'),'1','" + djfsrid + "','" + gdzrid + "','1'";
                                                    if ((dtdata.Rows.Count > 0) && (bjfs != "1") && (bjfs != "2"))// 普通计费基础且报价不是"单票单询"和"At Cost"
                                                    {
                                                        sql_value_insert += ",";
                                                        sql_value_values += ",";
                                                        for (int i = 0; i < dtdata.Rows.Count; i++)
                                                        {
                                                            string colName = dtdata.Rows[i]["VALCOL"] + "";
                                                            string colCode = dtdata.Rows[i]["VALCOL"] + "" + "C";
                                                            string valueCode = "";
                                                            if (dr.Table.Columns.Contains(dtdata.Rows[i]["CALCCODE"] + "")) { valueCode = dr[dtdata.Rows[i]["CALCCODE"] + ""] + ""; }
                                                            string value = "";
                                                            string code = "";
                                                            string[] arr = valueCode.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                                                            if (arr.Length == 2)// 做主数据校验
                                                            {
                                                                code = arr[0];
                                                                value = arr[1];
                                                            }
                                                            else if (arr.Length == 1)
                                                            {
                                                                code = arr[0];
                                                                value = arr[0];
                                                            }
                                                            if (i < dtdata.Rows.Count - 1)
                                                            {
                                                                sql_value_insert += colName + "," + colCode + ",";
                                                                sql_value_values += "'" + value + "','" + code + "',";
                                                            }
                                                            else// 最后一行
                                                            {
                                                                sql_value_insert += colName + "," + colCode + ")";
                                                                sql_value_values += "'" + value + "','" + code + "')";
                                                            }
                                                        }
                                                    }
                                                    else // 没有计费基础:1费目没有定价方式 2费目有无基础定价方式
                                                    {
                                                        sql_value_insert += ")";
                                                        sql_value_values += ")";
                                                    }
                                                    string sql = sql_value_insert + sql_value_values;
                                                    sqls.Add(sql);
                                                }
                                                string sqll = "";
                                                if (sqls.Count > 1)
                                                {
                                                    sqll = string.Join(";", sqls.ToArray());
                                                }
                                                else
                                                {
                                                    sqll = sqls[0];
                                                }
                                                sqll = "begin " + sqll + ";end;";
                                                DataHelper.ExecSql(sqll);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // 数据回滚操作
                            // 删除sqm_modebj_val表
                            List<string> delete = new List<string>();
                            for (int i = 0; i < feecalcid.Count; i++)
                            {
                                string sql_smv = "delete from sqm_modebj_val where feecalcid ='" + feecalcid[i] + "'";
                                delete.Add(sql_smv);
                            }
                            string sqls = string.Join(";", delete.ToArray()) + ";";
                            // 删除sqm_bj_main_basic、sqm_bj_ver、sqm_bj_bp、sqm_bj_org、sqm_bj_psf表
                            string sql = "begin delete from sqm_bj_main_basic where rid = '" + mrid + "';delete from sqm_bj_ver where mrid ='" + mrid + "';delete from sqm_bj_bp where mrid ='" + mrid + "';delete from sqm_bj_org where mrid ='" + mrid + "';delete from sqm_bj_psf where mrid ='" + mrid + "';" + sqls + "end;";
                            if (sql != ";")
                            {
                                DataHelper.ExecSql(sql);
                            }
                            return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "feecode:" + test + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
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
                return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "feecode:" + test + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
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
        /// <summary>
        /// 从配置文件获取废弃费目代码/废弃基础代码
        /// </summary>
        /// <returns></returns>
        private static List<string> GetCodeFromConfig(string str)
        {
            List<string> list = new List<string>();
            string feecodes = ConfigurationManager.AppSettings[str];
            if (feecodes.IndexOf(",") >= 0)
            {
                list = feecodes.Split(',').ToList<string>();
            }
            else
            {
                list.Add(feecodes);
            }
            return list;
        }
        /// <summary>
        /// 从配置文件获取 错误费目代码更正/错误基础名称更正/错误基础代码更正
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetWrongCodeFromConfig(string str)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<string> list = new List<string>();
            string feecodes = ConfigurationManager.AppSettings[str];
            if (feecodes.IndexOf(",") >= 0)
            {
                list = feecodes.Split(',').ToList<string>();
            }
            else
            {
                list.Add(feecodes);
            }
            foreach (string kayValue in list)
            {
                dictionary.Add(kayValue.Split(':')[0], kayValue.Split(':')[1]);
            }
            return dictionary;
        }
        /// <summary>
        /// 比较两个数组内容是否相同  linq查询，完全懵逼~~~
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static bool CompareArr(string[] arr1, string[] arr2)
        {
            var q = from a in arr1 join b in arr2 on a equals b select a;// query语句 IEnumerable ss = from score in scores  where ...  select score 
            bool flag = arr1.Length == arr2.Length && q.Count() == arr1.Length;
            return flag;//内容相同返回true,反之返回false。
        }
        /// <summary>
        /// 主数据校验 将“导入值”变成 “code+name”形式，适用于原始数据导入
        /// </summary>
        /// <param name="listDs"></param>
        /// <param name="i"></param>
        /// <param name="ds"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="jsjccode"></param>
        /// <returns></returns>
        private string MainDataCheck(List<DataSet> listDs, int i, DataSet ds, int m, int n, string jsjccode, string type)
        {
            string result = "1";
            string jcvalue = "";
            // 基础有的，模板没有的进行校验
            if (ds.Tables[m].Columns.Contains(jsjccode))
            {
                jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
            }
            if (jcvalue != "")
            {
                string codeName = MainDataExist("", ds.Tables[m].Rows[n][jsjccode].ToString(), type);
                if (codeName.Trim() == "&&")
                {
                    result = "0";
                }
                else
                {
                    listDs[i].Tables[m].Rows[n][jsjccode] = codeName;
                }
            }
            return result;
        }
        /// <summary>
        /// 主数据校验 将“导入值”变成 “导入值+code+name”形式，适用于Excel上传
        /// </summary>
        /// <param name="listDs"></param>
        /// <param name="i"></param>
        /// <param name="ds"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="jsjccode"></param>
        /// <returns></returns>
        private string MainDataCheck2(List<DataSet> listDs, int i, DataSet ds, int m, int n, string jsjccode, string type)
        {
            string result = "1";
            string jcvalue = "";
            // 基础有的，模板没有的进行校验
            if (ds.Tables[m].Columns.Contains(jsjccode))
            {
                jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
            }
            if (jcvalue != "")
            {
                string codeName = MainDataExist("", ds.Tables[m].Rows[n][jsjccode].ToString(), type);
                if (codeName.Trim() == "&&")
                {
                    result = "0";
                }
                else
                {
                    listDs[i].Tables[m].Rows[n][jsjccode] = ds.Tables[m].Rows[n][jsjccode].ToString() + "&&" + codeName; // 导入值&&code&&name
                }
            }
            return result;
        }
        public static DataTable dataTable;
        public static DataRow dataRow;
        private ArrayList GetDataFromExcel(System.IO.Stream stream)
        {
            ArrayList al = new ArrayList();
            Cells cells;
            Workbook workbook = new Workbook(stream);
            // 从配置文件获取错误基础代码更正
            Dictionary<string, string> wrongcalccode = GetWrongCodeFromConfig("WRONGCALCCODE");
            // 创建数据集集合，每个数据集表示一个sheet页
            List<DataSet> listDs = new List<DataSet>();
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                if (workbook.Worksheets[i].Name == "填制说明" || workbook.Worksheets[i].Name == "代码表")
                {
                    continue;
                }
                DataSet excel_ds = new DataSet(workbook.Worksheets[i].Name); //创建数据集
                cells = workbook.Worksheets[i].Cells;
                int rownumber = cells.MaxDataRow;
                string rownum = String.Empty;
                string colnum = String.Empty;
                if (workbook.Worksheets[i].Name == "客户及组织" || workbook.Worksheets[i].Name == "客户组织" || workbook.Worksheets[i].Name == "组织及客户" || workbook.Worksheets[i].Name == "组织客户") // 客户组织sheet
                {
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
                            if (j == 0 && cellStr == "报价名称")
                            {
                                titleRow = true;
                                dataTable = new DataTable();
                                dataRow = dataTable.NewRow();
                            }
                            if (titleRow)
                            {
                                dataTable.Columns.Add(cellStr);
                            }
                            else
                            {
                                dataRow[j] = cellStr;
                            }
                        }
                        if (!dataRow.IsNull(0))
                        {
                            DataRow drnew = dataTable.NewRow();
                            drnew.ItemArray = dataRow.ItemArray;
                            dataTable.Rows.Add(drnew);
                        }
                    }
                    excel_ds.Tables.Add(dataTable);
                    listDs.Add(excel_ds);
                }
                else // 产品sheet
                {
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
                            string cellStr = cells[k, j].StringValue.Trim();// 去首尾空格
                            // 判断是否标题行
                            if (j == 0 && cellStr == "服务" && cells[k, j + 1].StringValue.Trim() == "服务代码" && cells[k, j + 2].StringValue.Trim() == "费目")
                            {
                                titleRow = true;
                                if (k == 0)
                                {
                                    dataTable = new DataTable();
                                }
                                else
                                {
                                    DataTable dtnew = dataTable.Copy(); // 跟datarow一样，datatable也不能同时存进一个dataset（地址相同）
                                    // 处理有“标度”数据
                                    //dtnew = DealBD(dtnew);
                                    excel_ds.Tables.Add(dtnew);
                                    dataTable = new DataTable();
                                }
                                dataRow = dataTable.NewRow();
                            }
                            if (titleRow)
                            {
                                cellStr = cellStr.Replace("（", "(").Replace("）", ")");
                                // 处理错误标题
                                if (cellStr.IndexOf("(") >= 0)
                                {
                                    foreach (KeyValuePair<string, string> keyValue in wrongcalccode)
                                    {
                                        if (cellStr == keyValue.Key)
                                        {
                                            cellStr = keyValue.Value;
                                            break;
                                        }
                                    }
                                }
                                string title = cellStr.Replace(")", "");
                                if (title.IndexOf("(") >= 0)// 基础
                                {
                                    title = title.Split('(')[1];
                                    dataTable.Columns.Add(title);
                                }
                                else if (title.IndexOf("最低报价") >= 0 || title.IndexOf("最低收费") >= 0)// 非基础
                                {
                                    dataTable.Columns.Add("最低报价");
                                }
                                else // 非基础
                                {
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
                            // 处理标度数据，并标记是否为带标度数据
                            List<int> listArr = new List<int>();
                            for (int item = 0; item < drnew.ItemArray.Length; item++)
                            {
                                // 如果有< ,去掉
                                if ((drnew.ItemArray[item] + "").IndexOf("<") >= 0)
                                {
                                    drnew[item] = (drnew.ItemArray[item] + "").Replace("<", "");
                                }
                                // 如果有> ,去掉
                                if ((drnew.ItemArray[item] + "").IndexOf(">") >= 0)
                                {
                                    //drnew[item] = (drnew.ItemArray[item] + "").Replace(">", "");
                                    drnew[item] = "999999";
                                }
                                // 如果有 "[" 、 "]" ,将"-"换成",",找到需要拆分的数据列
                                if ((drnew.ItemArray[item] + "").IndexOf("[") >= 0 && (drnew.ItemArray[item] + "").IndexOf("]") >= 0)
                                {
                                    drnew[item] = (drnew.ItemArray[item] + "").Replace("-", ",").Replace("，", ",").Replace("[", "").Replace("]", "");
                                    listArr.Add(item);
                                }
                            }
                            if (listArr.Count > 0)
                            {
                                // 数据行拆分 递归拆行，不会，只能穷举，卧槽，好低端好丢人（仅支持两列数据拆分）
                                //List<DataRow> listRows = new List<DataRow>();
                                //SplitRows(listArr, drnew, listRows);
                                if (listArr.Count == 1)
                                {
                                    string allStr = drnew.ItemArray[listArr[0]].ToString().Trim();
                                    string beginStr = allStr.Split(',')[0];
                                    string endStr = allStr.Split(',')[1];

                                    if (beginStr != "0")
                                    {
                                        DataRow beginRow = dataTable.NewRow();
                                        beginRow.ItemArray = drnew.ItemArray;
                                        beginRow[listArr[0]] = beginStr;
                                        dataTable.Rows.Add(beginRow);
                                    }
                                    DataRow endRow = dataTable.NewRow();
                                    endRow.ItemArray = drnew.ItemArray;
                                    endRow[listArr[0]] = endStr;
                                    dataTable.Rows.Add(endRow);
                                }
                                if (listArr.Count == 2)
                                {
                                    string allStr1 = drnew.ItemArray[listArr[0]].ToString().Trim();
                                    string allStr2 = drnew.ItemArray[listArr[1]].ToString().Trim();
                                    string beginStr1 = allStr1.Split(',')[0];
                                    string endStr1 = allStr1.Split(',')[1];
                                    string beginStr2 = allStr2.Split(',')[0];
                                    string endStr2 = allStr2.Split(',')[1];
                                    if (beginStr1 != "0" && beginStr2 != "0")
                                    {
                                        // 1
                                        DataRow row1 = dataTable.NewRow();
                                        row1.ItemArray = drnew.ItemArray;
                                        row1[listArr[0]] = beginStr1;
                                        row1[listArr[1]] = beginStr2;
                                        dataTable.Rows.Add(row1);
                                        // 2
                                        DataRow row2 = dataTable.NewRow();
                                        row2.ItemArray = drnew.ItemArray;
                                        row2[listArr[0]] = beginStr1;
                                        row2[listArr[1]] = endStr2;
                                        dataTable.Rows.Add(row2);
                                        // 3
                                        DataRow row3 = dataTable.NewRow();
                                        row3.ItemArray = drnew.ItemArray;
                                        row3[listArr[0]] = endStr1;
                                        row3[listArr[1]] = beginStr2;
                                        dataTable.Rows.Add(row3);
                                        // 4
                                        DataRow row4 = dataTable.NewRow();
                                        row4.ItemArray = drnew.ItemArray;
                                        row4[listArr[0]] = endStr1;
                                        row4[listArr[1]] = endStr2;
                                        dataTable.Rows.Add(row4);
                                    }
                                    if (beginStr1 == "0" && beginStr2 != "0")
                                    {
                                        // 1
                                        DataRow row1 = dataTable.NewRow();
                                        row1.ItemArray = drnew.ItemArray;
                                        row1[listArr[0]] = endStr1;
                                        row1[listArr[1]] = beginStr2;
                                        dataTable.Rows.Add(row1);
                                        // 2
                                        DataRow row2 = dataTable.NewRow();
                                        row2.ItemArray = drnew.ItemArray;
                                        row2[listArr[0]] = endStr1;
                                        row2[listArr[1]] = endStr2;
                                        dataTable.Rows.Add(row2);
                                    }
                                    if (beginStr1 != "0" && beginStr2 == "0")
                                    {
                                        // 1
                                        DataRow row1 = dataTable.NewRow();
                                        row1.ItemArray = drnew.ItemArray;
                                        row1[listArr[0]] = beginStr1;
                                        row1[listArr[1]] = endStr2;
                                        dataTable.Rows.Add(row1);
                                        // 2
                                        DataRow row2 = dataTable.NewRow();
                                        row2.ItemArray = drnew.ItemArray;
                                        row2[listArr[0]] = endStr1;
                                        row2[listArr[1]] = endStr2;
                                        dataTable.Rows.Add(row2);
                                    }
                                }
                            }
                            else
                            {
                                dataTable.Rows.Add(drnew);
                            }
                        }
                        if (k == cells.MaxDataRow)// 如果是最后一行，把最后一个dataTable添加进dataSet中
                        {
                            DataTable dtnew = dataTable.Copy();
                            excel_ds.Tables.Add(dtnew);
                        }
                    }
                    listDs.Add(excel_ds);
                }
            }
            al.Add(listDs);
            return al;
        }
        ///// <summary>
        ///// 数据行拆分
        ///// </summary>
        ///// <param name="dtnew"></param>
        ///// <returns></returns>
        //public DataRow SplitRows(List<int> listArr, DataRow drnew, List<DataRow> listRows)
        //{
        //    int i = listArr.Count - 1;
        //    if(i == 0)
        //    {
        //        string allStr = drnew.ItemArray[listArr[i]].ToString().Trim();
        //        string beginStr = allStr.Split(',')[0];
        //        string endStr = allStr.Split(',')[1];

        //        if (beginStr != "0")
        //        {
        //            DataRow beginRow = dataTable.NewRow();
        //            beginRow.ItemArray = drnew.ItemArray;
        //            beginRow[listArr[i]] = beginStr;

        //            //dataTable.Rows.Add(beginRow);
        //        }
        //        DataRow endRow = dataTable.NewRow();
        //        endRow.ItemArray = drnew.ItemArray;
        //        endRow[listArr[i]] = endStr;
        //        dataTable.Rows.Add(endRow);

        //    }
        //    else
        //    {
        //        SplitRows(listArr.RemoveAt(i),,)
        //    }

        //}
        /// <summary>
        /// Excel上传--报价编辑页的“Excel上传”
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PriceImport(string currentbj, string currentbjrid)
        {
            List<string> dataErrorLog = new List<string>();// 主数据错误
            string arraylist = Request["alist"];// 选择“继续”从前台回传的数据。
            string test = "";
            List<string> colname = new List<string>();
            List<string> names = new List<string>();
            string info = string.Empty;
            List<DataSet> listDs = new List<DataSet>();
            ArrayList alist = new ArrayList();

            // 导入时，继承之前的报价主表与版本表信息 所以应用之前的mrid与vrid
            string mrid = "";
            string vrid = "";
            string bjname = "";
            string bjrid = "";
            string version = "";
            string iforiginal = "";//是否原始数据
            // 新数据在psf表存了哪写rid
            List<string> feecalcid = new List<string>();

            if (!string.IsNullOrEmpty(arraylist))
            {
                alist = JsonHelper.GetObject<ArrayList>(arraylist);
                listDs = JsonHelper.GetObject<List<DataSet>>(JsonHelper.GetJsonString(alist[0]));
                names = JsonHelper.GetObject<List<string>>(JsonHelper.GetJsonString(alist[1]));
                for (int i = 0; i < names.Count; i++)
                {
                    listDs[i].DataSetName = names[i];
                }
            }
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
                    // 主数据校验 
                    // 产品code校验、产品下对应服务code校验，服务下对应费目code校验
                    for (int i = 0; i < listDs.Count; i++)
                    {
                        if (listDs[i].DataSetName == "报价信息")
                        {
                            bjname = listDs[i].Tables[0].Rows[0]["报价名称"] + "";
                            bjrid = listDs[i].Tables[0].Rows[0]["RID"] + "";
                            if ((currentbj != bjname && string.IsNullOrEmpty(arraylist)) || (currentbjrid != bjrid && string.IsNullOrEmpty(arraylist)))
                            {
                                return Content(new JsonMessage { Message = "当前报价与导入报价文件不符，请确认导入数据是否符合当前报价数据", Code = "1", Success = false }.ToString());
                            }
                            string ridori = DataHelper.QueryValue("select distinct rid || ',' || original from sqm_bj_main_basic where bjname = '" + bjname + "' and rid = '" + bjrid + "'") + "";
                            mrid = ridori.Split(',')[0];
                            // 是否原始数据
                            iforiginal = ridori.Split(',')[1];
                            version = listDs[i].Tables[0].Rows[0]["版本号"] + "";
                            vrid = DataHelper.QueryValue("select rid from sqm_bj_ver where mrid = '" + mrid + "' and zver = '" + version + "'") + "";
                            if (string.IsNullOrEmpty(arraylist))// 报价在数据库不存在
                            {
                                DataTable dtoriginal = DataHelper.QueryDataTable("select distinct t1.rid,t1.original from sqm_bj_main_basic t1,sqm_bj_ver t2 where t1.rid = t2.mrid and t1.bjname = '" + bjname + "' and t2.zver = '" + version + "'");
                                if (dtoriginal.Rows.Count > 0)
                                {
                                    // 是否原始数据
                                    iforiginal = dtoriginal.Rows[0]["ORIGINAL"] + "";
                                    // dataSet 传到前台后再回传，set的name会被冲掉，所以把name也一并传到前台
                                    for (int n = 0; n < listDs.Count; n++)
                                    {
                                        names.Add(listDs[n].DataSetName);
                                    }
                                    ArrayList setAndName = new ArrayList();
                                    setAndName.Add((List<DataSet>)al[0]);
                                    setAndName.Add(names);
                                    return Content(new JsonMessage { Message = "报价名称为\"" + bjname + "\"，版本号为\"" + version + "\"的报价已存在！点\"继续\"按钮将覆盖原报价数据，是否继续？", Code = "ifexist", Data = setAndName, Success = false }.ToString());
                                }
                            }
                            else // 先删除原有值表数据 psf表数据 逻辑删除
                            {
                                DeleteDataSource(mrid, vrid, bjname, version, "update", null);
                            }
                        }
                        else
                        {
                            DataSet ds = listDs[i];
                            string[] arr = ds.DataSetName.Split('-');
                            string productCode = ds.DataSetName.Split('-')[arr.Length - 1];// 如果出现产品名称中带"-"的，第二个不一定是产品代码                                                   
                            // 判断产品code主数据是否存在
                            string prdname = MainDataExist("", productCode, "product");
                            if (prdname == "&&")
                            {
                                DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 产品 \"" + productCode + "\" 主数据不存在！", Code = "1", Success = false }.ToString());
                            }
                            else
                            {
                                ds.DataSetName = prdname.Replace("&&", "") + "-" + productCode;
                            }
                            for (int m = 0; m < ds.Tables.Count; m++)
                            {
                                // 定价标记不能重复
                                if (ds.Tables[m].Rows.Count > 1)
                                {
                                    List<string> list = new List<string>();
                                    list = CheckDJRID(ds.Tables[m], "");
                                    if (list[1] != "1" && list[2] != "2" && list[3] != "3")
                                    {
                                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                        dataErrorLog.Add("表：" + m + "，sheet表：\"" + listDs[i].DataSetName + "\"，费目为：\"" + list[2] + "\" 的数据\"定价标记\"列重复值！");
                                        //return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 费目为 \"" + list[2] + "\" 的数据\"定价标记\"列重复值！", Code = "1", Success = false }.ToString());
                                    }
                                }
                                string serviceName = ds.Tables[m].Rows[0]["服务"].ToString();
                                string serviceCode = ds.Tables[m].Rows[0]["服务代码"].ToString();
                                string feeName = ds.Tables[m].Rows[0]["费目"].ToString();
                                string feeCode = ds.Tables[m].Rows[0]["费目代码"].ToString();
                                string bjPrice = ds.Tables[m].Rows[0]["报价"].ToString();
                                string bjfs = "";
                                if (bjPrice.IndexOf("Cost") >= 0 || bjPrice.IndexOf("COST") >= 0 || bjPrice.IndexOf("cost") >= 0)
                                {
                                    bjfs = "1";
                                }
                                else if (bjPrice.IndexOf("单票单询") >= 0)
                                {
                                    bjfs = "2";
                                }
                                // 判断产品服务费目是否有关系--暂时去掉该功能
                                //string proserrel = MainDataExist(productCode + "," + serviceCode, "service");
                                //if (proserrel == "0")
                                //{
                                //    return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 产品代码为 \"" + productCode + "\" 的产品中不存在服务代码 \"" + serviceCode + "\"", Code = "1" }.ToString());
                                //}
                                //string serfeerel = MainDataExist(serviceCode + "," + feeCode, "fee");
                                //if (serfeerel == "0")
                                //{
                                //    return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务代码为 \"" + serviceCode + "\" 的服务中不存在费目代码 \"" + feeCode + "\"", Code = "1" }.ToString());
                                //}
                                string gdzrid = "";
                                if (ds.Tables[m].Columns.Contains("高低值ID"))
                                {
                                    gdzrid = ds.Tables[m].Rows[0]["高低值ID"] + "";
                                } // 每个table只有一个定价方式,所以只取第一行数据
                                string djfsrid = "";
                                if (ds.Tables[m].Columns.Contains("定价方式ID"))
                                {
                                    djfsrid = ds.Tables[m].Rows[0]["定价方式ID"] + "";
                                } // 每个table只有一个定价方式
                                DataTable dtjc = new DataTable();
                                if (gdzrid != "")
                                {
                                    dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and GDZRID = '" + gdzrid + "'");
                                }
                                else if (djfsrid != "")// 可能是无基础定价方式，这种定价方式不进行主数据校验，按票算
                                {
                                    dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID = '" + djfsrid + "'");
                                }
                                else
                                {
                                    dtjc = DataHelper.QueryDataTable("select FEECODE,CALCCODE,CALCNAME,VALCOL from SQM_FEE_CALC_REF where status = '1' and FEECODE = '" + feeCode + "' and DJFSRID is null");
                                    //if (dtjc.Rows.Count == 0)
                                    //{
                                    //    // 费目：1有基础  2无基础  3有基础Atcost 4无基础Atcost 没有基础的费目可能有这几种情况：无基础费目  Atcost  单票单询
                                    //    // 判断该费目是否未Atcost，如果不是，则
                                    //    string bjprice = ds.Tables[m].Rows[0]["报价"] + "";
                                    //    if (!(bjprice.IndexOf("cost") >= 0 || bjprice.IndexOf("Cost") >= 0 || bjprice.IndexOf("COST") >= 0 || bjprice.IndexOf("单票单询") >= 0))
                                    //    {
                                    //        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                    //        return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 费目代码为： \"" + feeCode + "\" 的数据未填写\"定价方式ID\"或\"高低值ID\"！", Code = "1" }.ToString());
                                    //    }
                                    //}
                                }
                                for (int n = ds.Tables[m].Rows.Count - 1; n >= 0; n--)
                                {
                                    // 判断是否是新增数据 先判断是否为单票单询，Atcost
                                    if (bjfs == "")
                                    {
                                        string djbj = ds.Tables[m].Rows[n]["定价标记"] + "";// 判断定价标记是否为空，如果是空则为无定价报价
                                        string count = DataHelper.QueryValue("select count(*) from sqm_modedj_val where rid = '" + djbj + "'") + "";
                                        if (count == "0")
                                        {
                                            string psfrid = ds.Tables[m].Rows[n]["PSFRID"] + "";
                                            ds.Tables[m].Rows[n]["定价标记"] = psfrid;
                                            ds.Tables[m].Rows[n]["最高价"] = "";
                                            ds.Tables[m].Rows[n]["最低价"] = "";
                                            ds.Tables[m].Rows[n]["指导价"] = "";
                                        }
                                    }
                                    if (dtjc.Rows.Count > 0)// 普通计费基础
                                    {
                                        // 基础校验
                                        foreach (DataRow dr in dtjc.Rows)
                                        {
                                            string jsjccode = dr["CALCCODE"].ToString();
                                            // 是否为空校验 如果不是X类型，其他基础不能为空
                                            if (!CheckIfX(jsjccode))
                                            {
                                                if (ds.Tables[m].Rows[n][jsjccode] + "" == "")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"\" 数据为空");
                                                }
                                            }
                                            // 主数据校验
                                            if (CheckJc(jsjccode) || jsjccode == "SOURCELOC_ZONE")
                                            {
                                                // 通用主数据 MDM
                                                if (jsjccode.IndexOf("GJ") >= 0) // 国家
                                                {
                                                    string result = "";
                                                    if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                    {
                                                        result = MainDataCheck2(listDs, i, ds, m, n, jsjccode, "1");
                                                    }
                                                    else
                                                    {
                                                        ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                    }
                                                    if (result == "0")
                                                    {
                                                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        //return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务为：\"" + serviceCode + "\" 费目代码为：：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":" + ds.Tables[m].Rows[n][jsjccode].ToString() + " 主数据中不存在", Code = "1", Success = false }.ToString());
                                                    }
                                                }
                                                // MDMLOC
                                                else if ((jsjccode.IndexOf("QYG") >= 0 || jsjccode.IndexOf("MDG") >= 0 || jsjccode.IndexOf("ZZG") >= 0 || jsjccode.IndexOf("ZYG") >= 0) && jsjccode != "ZZGFS")// 港口
                                                {
                                                    string result = "";
                                                    if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                    {
                                                        result = MainDataCheck2(listDs, i, ds, m, n, jsjccode, "2");
                                                    }
                                                    else
                                                    {
                                                        ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                    }
                                                    if (result == "0")
                                                    {
                                                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        //return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务为：\"" + serviceCode + "\" 费目代码为：：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":" + ds.Tables[m].Rows[n][jsjccode].ToString() + " 主数据中不存在", Code = "1", Success = false }.ToString());
                                                    }
                                                }
                                                // MDMBP
                                                else if (jsjccode.IndexOf("CGS") >= 0)// 船公司  jsjccode.IndexOf("HKGS") >= 0 || 航空公司在基础主数据中校验
                                                {
                                                    string result = "";
                                                    if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                    {
                                                        result = MainDataCheck2(listDs, i, ds, m, n, jsjccode, "4");
                                                    }
                                                    else
                                                    {
                                                        ds.Tables[m].Rows[n][jsjccode] = "*&&*";
                                                    }
                                                    if (result == "0")
                                                    {
                                                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                        dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                        //return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务为：\"" + serviceCode + "\" 费目代码为：：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":" + ds.Tables[m].Rows[n][jsjccode].ToString() + " 主数据中不存在", Code = "1", Success = false }.ToString());
                                                    }
                                                }
                                                // 计算基础 MDMJC
                                                else
                                                {
                                                    string jcvalue = "";
                                                    // 基础有的，模板没有的进行校验
                                                    if (ds.Tables[m].Columns.Contains(jsjccode))
                                                    {
                                                        jcvalue = ds.Tables[m].Rows[n][jsjccode].ToString();
                                                    }
                                                    if (jcvalue != "" && jcvalue != "null" && jcvalue != "Null" && jcvalue != "NULL")
                                                    {
                                                        try
                                                        {
                                                            string codeName = "";
                                                            if (ds.Tables[m].Rows[n][jsjccode].ToString() != "*")
                                                            {
                                                                codeName = MainDataExist(jsjccode, ds.Tables[m].Rows[n][jsjccode].ToString(), "6");
                                                            }
                                                            else
                                                            {
                                                                codeName = "*&&*";
                                                            }
                                                            if (codeName.Trim() == "&&")
                                                            {
                                                                DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                                //return Content(new JsonMessage { Message = "导入失败：sheet表：\"" + listDs[i].DataSetName + "\" 服务为：\"" + serviceCode + "\" 费目代码为：：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":" + ds.Tables[m].Rows[n][jsjccode].ToString() + " 主数据中不存在", Code = "1", Success = false }.ToString());
                                                                dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                            }
                                                            else
                                                            {
                                                                listDs[i].Tables[m].Rows[n][jsjccode] = ds.Tables[m].Rows[n][jsjccode].ToString() + "&&" + codeName;
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                            dataErrorLog.Add("导入失败：" + ex.Message + " sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的数据，基础为: " + jsjccode + "的主数据配置错误！");
                                                            //return Content(new JsonMessage { Message = "导入失败：" + ex.Message + " sheet表：\"" + listDs[i].DataSetName + "\" 服务为：\"" + serviceCode + "\" 费目代码为： \"" + feeCode + "\" 的数据，基础为: " + jsjccode + "的主数据配置错误！", Code = "1", Success = false }.ToString());
                                                        }
                                                    }
                                                    else// 主数据里null值校验
                                                    {
                                                        try
                                                        {
                                                            string codeName = MainDataExist(jsjccode, "null", "6");
                                                            if (codeName.Trim() == "&&")
                                                            {
                                                                DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                                dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 主数据中不存在");
                                                            }
                                                            else
                                                            {
                                                                listDs[i].Tables[m].Rows[n][jsjccode] = "null&&" + codeName;
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                                                            dataErrorLog.Add("导入失败：" + ex.Message + " sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的数据，基础为: " + jsjccode + "的主数据配置错误！");
                                                        }
                                                    }
                                                }
                                            }
                                            else// 不校验主数据的基础，校验长度
                                            {
                                                string result = CheckData(jsjccode, ds.Tables[m].Rows[n][jsjccode] + "");
                                                if (result == "1")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 整数位位数超限");
                                                }
                                                else if (result == "2")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 小数位位数超限");
                                                }
                                                else if (result == "3")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 字数超限");
                                                }
                                                else if (result == "4")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 应为'X'或空");
                                                }
                                                else if (result == "5")
                                                {
                                                    dataErrorLog.Add("表：" + m + ", 行：" + n + "，sheet表：\"" + listDs[i].DataSetName + "\"，服务代码为：\"" + serviceCode + "\"，费目代码为：\"" + feeCode + "\" 的基础 ==> \"" + jsjccode + "\":\"" + ds.Tables[m].Rows[n][jsjccode].ToString() + "\" 应为'Y'或'N'");
                                                }
                                                else if (result == "X")// 校正x小写
                                                {
                                                    listDs[i].Tables[m].Rows[n][jsjccode] = "X";
                                                }
                                                else if (result == "Y")// 校正y小写
                                                {
                                                    listDs[i].Tables[m].Rows[n][jsjccode] = "Y";
                                                }
                                                else if (result == "N")// 校正n小写
                                                {
                                                    listDs[i].Tables[m].Rows[n][jsjccode] = "N";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (dataErrorLog.Count > 0)// 主数据校验问题，不予入库，导入失败D:\客户price\0SourceCode\报价管理系统\Web\Oncontrol3.Web\Controllers\SQM_BJ_IMP_EXPController.cs
                    {
                        return Content(new JsonMessage { Message = string.Join("<BR>", dataErrorLog.ToArray()), Code = "1", Success = false }.ToString());
                    }
                    // 原数据有哪些是导入的，以便区分哪些是未导入的
                    List<string> impRid = new List<string>();
                    // 数据入库
                    string msg = InsertData(ref test, ref colname, listDs, feecalcid, mrid, vrid, ref bjname, ref version, iforiginal, impRid, iforiginal);
                    if (msg.IndexOf("F") >= 0)
                    {
                        // 恢复原状态，导入新数据失效
                        DataRollBack(vrid, feecalcid);
                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);
                        string djfs = "";
                        string gdz = "";
                        if (msg.IndexOf(",") >= 0)
                        {
                            djfs = msg.Replace("F", "").Trim().Split(',')[0];
                            gdz = msg.Replace("F", "").Trim().Split(',')[1];
                        }
                        return Content(new JsonMessage { Message = "导入中止：费目代码：" + test + "，定价方式：" + djfs + "，高低值：" + gdz + "，存在计算基础信息修改！", Code = "2", Success = false }.ToString());
                    }
                    else if (msg.IndexOf("F") < 0 && msg != "T")
                    {
                        // 数据入库异常
                        // 恢复原状态，导入新数据失效
                        DataRollBack(vrid, feecalcid);// 新插数据删除
                        DeleteDataSource(mrid, vrid, bjname, version, "recover", null);// 置status = 1
                        return Content(new JsonMessage { Message = "导入异常:" + msg + "feecode:" + test + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2", Success = false }.ToString());
                    }
                    else// 导入成功 置psf表bjstatau为0的数据为已保存状态
                    {
                        TurnToValid(mrid, vrid);
                        DeleteDataSource(mrid, vrid, bjname, version, "delete", impRid);// 删除status为0的数据
                    }
                    return Content(new JsonMessage { Message = "导入成功", Success = true, Code = "0" }.ToString());
                }
                else
                {
                    // 恢复原状态，导入新数据失效
                    DataRollBack(vrid, feecalcid);// 新插数据删除
                    DeleteDataSource(mrid, vrid, bjname, version, "recover", null);// 置status = 1
                    return Content(new JsonMessage { Message = "导入失败,文件不存在", Success = false, Code = "1" }.ToString());
                }
            }
            catch (Exception ex)
            {
                // 恢复原状态，导入新数据失效
                DataRollBack(vrid, feecalcid);// 新插数据删除
                DeleteDataSource(mrid, vrid, bjname, version, "recover", null);// 置status = 1
                return Content(new JsonMessage { Message = "导入异常:" + ex.Message + "feecode:" + test + "。  模板字段:" + string.Join(",", colname.ToArray()), Code = "2" }.ToString());
            }
        }
        /// <summary>
        /// 是否包含非校验项   改为校验哪些项  基础800多条，给的主数据只有47条
        /// </summary>
        /// <param name="jsjccode"></param>
        /// <returns></returns>
        private static DataTable mdatadt = DataHelper.QueryDataTable("select distinct mdkey from mdm_calc_value");
        private static bool CheckJc(string jsjccode)
        {
            bool sign = false;
            // 港口 国家 航空公司 船公司
            string[] mustCheck = { "GJ", "QYG", "MDG", "ZZG", "ZYG", "CGS" };
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
        /// <summary>
        /// 原数据删除（数据校验） 导入时置数据无效，导入失败恢复无效数据，导入成功删除无效数据
        /// </summary>
        /// <param name="mrid"></param>
        /// <param name="vrid"></param>
        /// <param name="bjname"></param>
        /// <param name="version"></param>
        private static void DeleteDataSource(string mrid, string vrid, string bjname, string version, string type, List<string> impRids)
        {
            // 根据excel信息得到主表rid
            string mrid_delete = mrid;
            IList<EasyDictionary> listeasy = new List<EasyDictionary>();
            if (type == "update")
            {
                listeasy = DataHelper.QueryDictList("select RID from sqm_bj_psf where mrid ='" + mrid_delete + "' and (status <> '0' or status is null) and vrid = '" + vrid + "' and choosestatus = '1'");// 找有效数据
            }
            else if (type == "recover")
            {
                listeasy = DataHelper.QueryDictList("select RID from sqm_bj_psf where mrid ='" + mrid_delete + "' and status = '0' and vrid = '" + vrid + "' and choosestatus = '1'");// 找无效数据
            }
            else
            {
                listeasy = DataHelper.QueryDictList("select RID from sqm_bj_psf where mrid ='" + mrid_delete + "' and status = '0' and vrid ='" + vrid + "' and choosestatus = '1'");// 找无效数据
            }

            // 判断是否导入成功  导入成功后删除原数据，但是未进行导入的费目要删除掉
            List<string> unImpRid = new List<string>();
            if (impRids != null && type == "delete")
            {
                string rid = "";
                if (listeasy.Count > 0)
                {
                    foreach (EasyDictionary edict in listeasy)
                    {
                        rid = edict.Get("RID") + "";
                        foreach (string impRid in impRids)
                        {
                            if (rid != impRid)
                            {
                                unImpRid.Add(rid);
                            }
                        }
                    }
                    string updateStatus = "";
                    if (unImpRid.Count > 0)
                    {
                        foreach (string id in unImpRid)
                        {
                            //updateStatus += "update sqm_bj_psf set status = '1' where rid = '" + id + "';";
                            updateStatus += "delete from sqm_bj_psf where rid = '" + id + "';";
                        }
                        string sql_us = "begin " + updateStatus + " end;";
                        DataHelper.ExecSql(sql_us);
                    }
                }
            }

            List<string> delete = new List<string>();
            string sql_smv = "";
            if (type == "update")
            {
                for (int m = 0; m < listeasy.Count; m++)
                {
                    sql_smv += "update sqm_bj_psf set status = '0' where rid = '" + listeasy[m].Get("RID") + "';";
                    sql_smv += "update sqm_modebj_val set status = '0' where feecalcid = '" + listeasy[m].Get("RID") + "' and status = '1';";
                }
            }
            else if (type == "recover")
            {
                for (int m = 0; m < listeasy.Count; m++)
                {
                    sql_smv += "update sqm_bj_psf set status = '1' where rid = '" + listeasy[m].Get("RID") + "';";
                    sql_smv += "update sqm_modebj_val set status = '1' where feecalcid = '" + listeasy[m].Get("RID") + "' and status = '0';";
                }
            }
            else
            {
                foreach (string rid in impRids)
                {
                    sql_smv += "delete from sqm_bj_psf where rid = '" + rid + "';";
                    sql_smv += "delete from sqm_modebj_val where feecalcid = '" + rid + "' and status = '0';";
                }
            }
            //string sqls = string.Join(";", delete.ToArray());
            if (listeasy.Count > 0)
            {
                string sql = "begin " + sql_smv + "end;";
                DataHelper.ExecSql(sql);
            }
        }
        /// <summary>
        /// 报错数据回滚（数据入库）
        /// </summary>
        /// <param name="vrid"></param>
        /// <param name="feecalcid"></param>
        private static void DataRollBack(string vrid, List<string> feecalcid)
        {
            // 删除sqm_modebj_val表  删除sqm_bj_psf表
            List<string> delete = new List<string>();
            for (int i = 0; i < feecalcid.Count; i++)
            {
                string sql_smv = "delete from sqm_modebj_val where feecalcid = '" + feecalcid[i] + "' and status = '1'";
                delete.Add(sql_smv);
                string sql_psf = "delete from sqm_bj_psf where rid = '" + feecalcid[i] + "'";
                delete.Add(sql_psf);
            }
            string sqls = string.Join(";", delete.ToArray());
            if (delete.Count > 0)
            {
                string sql_delete = "begin " + sqls + ";end;";
                DataHelper.ExecSql(sql_delete);
            }
        }
        /// <summary>
        /// 置bjstatau为0的数据为已保存状态
        /// </summary>
        /// <param name="mrid"></param>
        /// <param name="vrid"></param>
        private static void TurnToValid(string mrid, string vrid)
        {
            DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '1' where mrid = '" + mrid + "' and vrid = '" + vrid + "' and bjstataus = '0'");
        }
        /// <summary>
        /// Excel上传 判断“定价标记”是否重复/是否无定价报价
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<string> CheckDJRID(DataTable dt, string type)
        {
            List<string> list = new List<string>();
            list.Add("0");
            list.Add("1");
            list.Add("2");
            list.Add("3");// 固定四个长度，分别存“无定价报价”或“报价超限”  “定价标价” “服务” “费目” 
            // 判断是否重复
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (type == "1")// 判断是否无定价报价 + 判断是否报价超限
                {
                    if (!list.Contains("无定价报价"))
                    {
                        string count = DataHelper.QueryValue("select count(*) from sqm_modedj_val where rid = '" + dt.Rows[i]["定价标记"] + "'") + "";
                        if (count == "0")// 报价新增（无定价报价）
                        {
                            list[0] = "无定价报价";
                        }
                        else if (!list.Contains("报价超限"))
                        {
                            if (!((dt.Rows[i]["报价"] + "").IndexOf("Cost") >= 0 || (dt.Rows[i]["报价"] + "").IndexOf("cost") >= 0 || (dt.Rows[i]["报价"] + "").IndexOf("COST") >= 0 || (dt.Rows[i]["报价"] + "").IndexOf("单票单询") >= 0))
                            {
                                Decimal bjPrice = Convert.ToDecimal(dt.Rows[i]["报价"] + "");
                                Decimal minPrice = Convert.ToDecimal(dt.Rows[i]["最低价"] + "");
                                Decimal maxPrice = Convert.ToDecimal(dt.Rows[i]["最高价"] + "");
                                if (bjPrice < minPrice || bjPrice > maxPrice)
                                {
                                    list[0] = "报价超限";
                                }
                            }
                        }
                    }
                }
                else if (type == "2")// 判断同一个table是否有两个及以上feecode
                {
                    string feecode = dt.Rows[i]["费目代码"] + "";
                    string feecode2 = "";
                    bool repeat = false;
                    bool atcost = false;
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        feecode2 = dt.Rows[j]["费目代码"] + "";
                        if (feecode != feecode2)
                        {
                            repeat = true;
                        }
                        // 第二行及以后有Atcost 或者单票单询
                        string price = dt.Rows[j]["报价"] + "";
                        if (price.IndexOf("Cost") >= 0 || price.IndexOf("COST") >= 0 || price.IndexOf("cost") >= 0 || price.IndexOf("单票单询") >= 0)
                        {
                            atcost = true;
                        }
                    }
                    if (repeat)
                    {
                        list[0] = "代码重复";
                    }
                    else if (atcost)
                    {
                        list[1] = "COST";
                    }
                }
                else// 判断定价标记是否重复
                {
                    string djbj1 = dt.Rows[i]["定价标记"] + "";
                    string count1 = DataHelper.QueryValue("select count(*) from sqm_modedj_val where rid = '" + djbj1 + "'") + "";
                    if (djbj1 != "" && count1 != "0")// 非无定价报价
                    {
                        for (int j = i + 1; j < dt.Rows.Count; j++)
                        {
                            string djbj2 = dt.Rows[j]["定价标记"] + "";
                            string count2 = DataHelper.QueryValue("select count(*) from sqm_modedj_val where rid = '" + djbj2 + "'") + "";
                            if (count2 != "0")// 非无定价报价
                            {
                                if ((djbj2 != "") && (dt.Rows[i]["定价标记"] + "" == djbj2))
                                {
                                    list[1] = dt.Rows[i]["定价标记"] + "";
                                    list[2] = dt.Rows[i]["服务"] + "";
                                    list[3] = dt.Rows[i]["费目"] + "";
                                    return list;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 数据入库（Excel上传）  与原始数据上传不同，原始数据全部为无定价报价，所以无需校验数据是否在导入前更改，
        /// 所以原始 数据检验时 可以直接将“导入值”变成“code+name”的形式，无需保留“导入值”，
        /// Excel上传则不同，需要将“导入值”“code+name”值在校验时都保留，以便使用“导入值”判断是否数据更改
        /// </summary>
        /// <param name="test"></param>
        /// <param name="colname"></param>
        /// <param name="listDs"></param>
        /// <param name="mrid"></param>
        /// <param name="feecalcid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        private string InsertData(ref string test, ref List<string> colname, List<DataSet> listDs, List<string> feecalcid, string mrid, string vrid, ref string bjname, ref string version, string iforiginal, List<string> impRid, string original)
        {
            string msg = "T";
            try
            {
                string orgcode = "";
                string orgname = "";
                foreach (DataSet ds in listDs)
                {
                    string createTime = DateTime.Now.ToString();
                    string createUser = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    string createId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    // 读取组织code与组织name
                    if (ds.DataSetName == "报价信息")
                    {
                        orgcode = ds.Tables[0].Rows[0]["组织代码"] + "";
                        orgname = ds.Tables[0].Rows[0]["组织名称"] + "";
                    }
                    else if (ds.DataSetName != "报价信息")
                    {
                        string[] sheetname = ds.DataSetName.Trim().Split('-');
                        string productCode = ds.DataSetName.Trim().Split('-')[sheetname.Length - 1];
                        string productName = ds.DataSetName.Trim().Substring(0, ds.DataSetName.Trim().LastIndexOf("-") - 0);
                        string oldFeeCode = "";// 判断这个费目的报价状态
                        int count3 = 0;
                        int count4 = 0;
                        foreach (DataTable dt in ds.Tables)
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
                            if (dt.Rows.Count > 0)
                            {
                                // 每一个DataTable都是一个定价方式（肯定只有一个费目），所以取第一行的服务、费目即可  
                                // 报价方式：bjfs  1-At cost、2-单票单询
                                string bjfs = "0";// 报价方式必须有值，费目报价中用到
                                string serviceName = dt.Rows[0]["服务"].ToString();
                                string serviceCode = dt.Rows[0]["服务代码"].ToString();
                                string feeName = dt.Rows[0]["费目"].ToString();
                                string feeCode = dt.Rows[0]["费目代码"].ToString();
                                if (oldFeeCode != feeCode)// 一个费目可能有多个定价方式，高低值等，遍历完之后才能确认其“状态”，此操作记录费目是否遍历完（所有方式高低值）
                                {
                                    oldFeeCode = feeCode;
                                    count3 = 0;
                                    count4 = 0;
                                }
                                test = feeCode;
                                string bjstartdate = dt.Rows[0]["费目起始日期"] + "";// psf表
                                string bjenddate = dt.Rows[0]["费目截止日期"] + "";// psf表
                                string contidion = dt.Rows[0]["前提条件"].ToString();// psf表
                                string stagetype = dt.Rows[0]["阶段类别"].ToString();// psf表  -> 只有Atcost 和 单票单询放在psf表，普通报价放在值表的FSJDLB字段
                                string bjstatus = "0";// 初始状态：0-未保存，1-未保存（非报价状态），为了比较值是否修改使用
                                string psf_rid = string.Empty;
                                string val_rid = string.Empty;
                                string bz = string.Empty;
                                string jsf = string.Empty;
                                string memo = string.Empty;
                                string bjprice = string.Empty;
                                string minbjprice = string.Empty;// 最低报价只取datatable第一行
                                string jxjc = "";// 报价值表
                                string jsjcms = string.Empty;
                                string jsjccode = string.Empty;
                                string calcunit = string.Empty;
                                string djrid = "";
                                string djzdj = "";// 最低价
                                string djzgj = "";// 最高价
                                string djzhidj = "";// 指导价
                                string djfsrid = "";// 定价方式rid
                                string gdzrid = "";// 高低值rid
                                string beginDate = "";// 值表明细 起始日期
                                string endDate = "";// 值表明细 结束日期
                                string ifsave = "";// 报价状态：0-未保存，1-已保存，2-已确认，3-报价无定价，4-报价超限
                                string psfbj_rid = "";
                                string calctype = "";
                                string ifbgf = "";// 1 - 包干费主费
                                string oldpsfrid = "";
                                // 1.产品服务费目表：sqm_bj_psf 插数 插数之前原来的数据已经全部置为失效状态
                                // 根据产品/服务/费目code检验sqm_bj_psf是否有数据 如果有，则是多定价方式
                                // 查询原数据的报价状态 是否包干费，未保存  已保存（确认）
                                //var listrid = DataHelper.QueryObjectsList("select bgfzrid,bjstataus,rid from sqm_bj_psf where fee_code = '" + feeCode + "' and service_code = '" + serviceCode + "' and product_code = '" + productCode + "' and vrid = '" + vrid + "' and status = '0'");
                                //if (listrid.Count > 0)
                                //{
                                //    ifsave = listrid[1] + "";
                                //}
                                string statusRid = DataHelper.QueryValue("select bjstataus || ',' || rid || ',' || bgfzrid from sqm_bj_psf where fee_code = '" + feeCode + "' and service_code = '" + serviceCode + "' and product_code ='" + productCode + "' and vrid ='" + vrid + "' and status = '0'") + "";
                                if (statusRid != "")
                                {
                                    ifsave = statusRid.Split(',')[0] + "";// 获取费目原报价状态
                                    oldpsfrid = statusRid.Split(',')[1] + "";
                                    if (oldpsfrid != "")
                                    {
                                        impRid.Add(oldpsfrid);// 获取哪些费目进行了入库 以区分Excel导入的与未导入的产品服务费目
                                    }
                                    ifbgf = statusRid.Split(',')[2] + "";// 获取费目是否选包干费
                                }
                                if (ifsave != "0" && ifsave != "")// 已保存
                                {
                                    bjstatus = "1";
                                    psfbj_rid = statusRid.Split(',')[1] + "";
                                }
                                string ridstatus = DataHelper.QueryValue("select bjstataus || ',' || rid from sqm_bj_psf where fee_code = '" + feeCode + "' and service_code = '" + serviceCode + "' and product_code ='" + productCode + "' and vrid ='" + vrid + "' and status = '1'") + "";
                                string ifsave2 = "";
                                if (ridstatus != "")
                                {
                                    psf_rid = ridstatus.Split(',')[1] + "";
                                    ifsave2 = ridstatus.Split(',')[0] + "";
                                }
                                string sql_psf = "";
                                if (string.IsNullOrEmpty(psf_rid))// 一个费目多个方式，所以只插一次就行
                                {
                                    sql_psf = "insert into sqm_bj_psf(RID,MRID,VRID,BJSTATAUS,CREATETIME,CREATEUSER,PRODUCT_NAME,PRODUCT_CODE,SERVICE_NAME,SERVICE_CODE,FEE_NAME,FEE_CODE,CREATEID,BJFS,STATUS,CONDITION,STAGETYPE,BJSTARTDATE,BJENDDATE,CHOOSESTATUS,ISLSC,BGFZRID,ORGCODE,ORGNAME,FEECATG) values('{0}','{1}','{2}','{3}',to_date('{4}','yyyy/mm/dd hh24:mi:ss'),'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',to_date('{17}','yyyy/mm/dd'),to_date('{18}','yyyy/mm/dd'),'{19}','{20}','{21}','{22}','{23}','{24}')";
                                    psf_rid = System.Guid.NewGuid().ToString();
                                    feecalcid.Add(psf_rid);
                                    bjprice = dt.Rows[0]["报价"].ToString();
                                    if (bjprice.IndexOf("COST") >= 0 || bjprice.IndexOf("Cost") >= 0 || bjprice.IndexOf("cost") >= 0)
                                    {
                                        bjfs = "1";
                                    }
                                    else if (bjprice.IndexOf("单票单询") >= 0)
                                    {
                                        bjfs = "2";
                                    }
                                    if (bjfs == "0" && original != "1")
                                    {
                                        List<string> list = CheckDJRID(dt, "1");
                                        if (list.Contains("无定价报价"))
                                        {
                                            ifsave = "3";
                                            count3++;
                                        }
                                        else if (list.Contains("报价超限"))
                                        {
                                            ifsave = "4";
                                            count4++;
                                        }
                                    }
                                    else if (original == "1")
                                    {
                                        ifsave = "2";
                                    }
                                    sql_psf = String.Format(sql_psf, psf_rid, mrid, vrid, ifsave, createTime, createUser, productName, productCode, serviceName, serviceCode, feeName, feeCode, createId, bjfs, "1", contidion, stagetype, bjstartdate, bjenddate, '1', ifbgf, ifbgf, orgcode, orgname, bjfs == "1" ? "2" : "");
                                    DataHelper.ExecSql(sql_psf);
                                    // 插入包干费
                                    if (ifbgf == "1")
                                    {
                                        DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + oldpsfrid + "' and status = '0'");
                                        if (dtbgf.Rows.Count > 0)
                                        {
                                            string part1 = "begin ";
                                            string part2 = " end;";
                                            foreach (DataRow dr in dtbgf.Rows)
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
                                                        value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                                    }
                                                    else if (colName == "BGFZRID")
                                                    {
                                                        value += "'" + psf_rid + "',";
                                                    }
                                                    else if (colName == "RID")
                                                    {
                                                        value += "'" + bgfrid + "',";
                                                    }
                                                    else if (colName == "STATUS")
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
                                    }
                                }
                                else// 如果已经存在且报价状态为“已确认”或者“已保存”，则更新表（状态）
                                {
                                    bjprice = dt.Rows[0]["报价"].ToString();
                                    if (bjprice.IndexOf("COST") >= 0 || bjprice.IndexOf("Cost") >= 0 || bjprice.IndexOf("cost") >= 0)
                                    {
                                        bjfs = "1";
                                    }
                                    else if (bjprice.IndexOf("单票单询") >= 0)
                                    {
                                        bjfs = "2";
                                    }
                                    if (bjfs == "0")
                                    {
                                        List<string> list = CheckDJRID(dt, "1");
                                        if (list.Contains("无定价报价"))
                                        {
                                            ifsave = "3";
                                            count3++;
                                        }
                                        else if (list.Contains("报价超限"))
                                        {
                                            ifsave = "4";
                                            count4++;
                                        }
                                    }
                                    if (count3 > 0)// 无定价报价级别高 加count3与count4是为了判定整个费目的报价状态而不是一个Table的
                                    {
                                        ifsave = "3";
                                        DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '" + ifsave + "' where rid = '" + psf_rid + "'");
                                    }
                                    else if (count4 > 0)
                                    {
                                        ifsave = "4";
                                        DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '" + ifsave + "' where rid = '" + psf_rid + "'");
                                    }
                                    else if (ifsave == "3" || ifsave == "4")// 导入之前是“无定价报价”“报价超限”状态
                                    {
                                        ifsave = "1";
                                        DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '" + ifsave + "' where rid = '" + psf_rid + "'");
                                    }
                                }
                                if (dt.Columns.Contains("定价方式ID")) { djfsrid = dt.Rows[0]["定价方式ID"] + ""; } // 每个table只有一个定价方式ID
                                if (dt.Columns.Contains("高低值ID")) { gdzrid = dt.Rows[0]["高低值ID"] + ""; } // 每个table都只有一个高低值ID
                                // 获取阶梯累计
                                string jtlj = GetJTLJ(djfsrid, gdzrid);// 一个table只有一个阶梯累计
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
                                // 2.报价值表：sqm_modebj_val 表插数 
                                List<string> sqls = new List<string>();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string djfs = "";
                                    string gdz = "";
                                    string sql_value_insert = "insert into sqm_modebj_val(RID,CREATETIME,CREATEUSER,CREATEID,FEECALCID,CURRENCY,JSFCODE,MEMO,BJPRICE,CALCUNIT,MINBJPRICE,JXJC,CALCNAME,CALCCODE,BJFS,CONDITION,ORIGINAL,DJRID,MAXPRICE,MINPRICE,GUIDEPRICE,STARTDATE,ENDDATE,DJFSRID,GDZRID,STATUS,CALCTYPE,IFBJITEM,FSJDLB,JTLJ";
                                    string sql_value_values = " values(";
                                    if (dr.Table.Columns.Contains("币种")) { bz = dr["币种"] + ""; }
                                    if (dr.Table.Columns.Contains("结算方代码")) { jsf = dr["结算方代码"] + ""; }
                                    if (dr.Table.Columns.Contains("备注")) { memo = dr["备注"] + ""; }
                                    if (dr.Table.Columns.Contains("报价")) { bjprice = dr["报价"] + ""; }
                                    if (dr.Table.Columns.Contains("报价单位")) { calcunit = dr["报价单位"] + ""; }
                                    if (dr.Table.Columns.Contains("定价方式")) { djfs = dr["定价方式"] + ""; }
                                    if (dr.Table.Columns.Contains("高低值")) { gdz = dr["高低值"] + ""; }
                                    if (dr.Table.Columns.Contains("解析基础")) { jxjc = dr["解析基础"] + ""; }
                                    string str = GetReturnStr(djfs, gdz);
                                    if (bjprice.IndexOf("COST") >= 0)
                                    {
                                        bjfs = "1";
                                        bjprice = "";
                                    }
                                    if (bjprice.IndexOf("单票单询") >= 0)
                                    {
                                        bjfs = "2";
                                        bjprice = "";
                                    }
                                    if (string.IsNullOrEmpty(minbjprice))// 为空时赋值
                                    {
                                        if (dr.Table.Columns.Contains("最低报价")) { minbjprice = dr["最低报价"] + ""; }
                                    }
                                    if (dr.Table.Columns.Contains("定价标记"))
                                    {
                                        djrid = dr["定价标记"] + "";
                                    }
                                    if (dr.Table.Columns.Contains("最低价"))
                                    {
                                        djzdj = dr["最低价"] + "";
                                        if (!CompareData(djzdj, "MINPRICE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("最高价"))
                                    {
                                        djzgj = dr["最高价"] + "";
                                        if (!CompareData(djzgj, "MAXPRICE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("指导价"))
                                    {
                                        djzhidj = dr["指导价"] + "";
                                        if (!CompareData(djzhidj, "GUIDEPRICE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("起始日期"))
                                    {
                                        beginDate = dr["起始日期"] + "";
                                        if (!CompareData(beginDate, "STARTDATE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("结束日期"))
                                    {
                                        endDate = dr["结束日期"] + "";
                                        if (!CompareData(endDate, "ENDDATE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    if (dr.Table.Columns.Contains("计算方式"))
                                    {
                                        calctype = dr["计算方式"] + "";
                                        if (calctype == "绝对值")
                                        {
                                            calctype = "A";
                                        }
                                        else
                                        {
                                            calctype = "B";
                                        }
                                        if (!CompareData(calctype, "CALCTYPE", djrid, bjstatus, psf_rid, psfbj_rid))
                                        {
                                            return "F " + str;
                                        }
                                    }
                                    val_rid = System.Guid.NewGuid().ToString();
                                    sql_value_values += "'" + val_rid + "',to_date('" + createTime + "','yyyy/mm/dd hh24:mi:ss'),'" + createUser + "','" + createId + "','" + psf_rid + "','" + bz + "','" + jsf + "','" + memo + "','" + bjprice + "','" + calcunit + "','" + minbjprice + "','" + jxjc + "','" + jsjcms + "','" + jsjccode + "','" + bjfs + "','" + contidion + "','" + "" + "','" + djrid + "','" + djzgj + "','" + djzdj + "','" + djzhidj + "',to_date('" + beginDate + "','yyyy/mm/dd'),to_date('" + endDate + "','yyyy/mm/dd'),'" + djfsrid + "','" + gdzrid + "','1','" + calctype + "','1','" + stagetype + "','" + jtlj + "'";
                                    if ((dtjc.Rows.Count > 0) && (bjfs != "1") && (bjfs != "2"))// 普通计费基础
                                    {
                                        sql_value_insert += ",";
                                        sql_value_values += ",";
                                        for (int i = 0; i < dtjc.Rows.Count; i++)
                                        {
                                            string colName = dtjc.Rows[i]["VALCOL"] + "";  // 名称的值表位置  定价值表与报价值表位置相同
                                            string colCode = dtjc.Rows[i]["VALCOL"] + "" + "C"; // 代码的值表位置
                                            string valueCode = "";
                                            if (dr.Table.Columns.Contains(dtjc.Rows[i]["CALCCODE"] + "")) { valueCode = dr[dtjc.Rows[i]["CALCCODE"] + ""] + ""; }
                                            string originalV = "";
                                            string value = "";
                                            string code = "";
                                            string[] arr = valueCode.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (arr.Length == 1)// 不做主数据校验的数据
                                            {
                                                originalV = arr[0];
                                                // 与定价值表相同位置数据进行判断，导入的Excel里的值有没有修改过，如果有修改过则导入失败并提示
                                                if (!CompareData(originalV, colName, djrid, bjstatus, psf_rid, psfbj_rid))
                                                {
                                                    return "F " + str;
                                                }
                                                value = originalV;
                                                code = originalV;
                                            }
                                            else if (arr.Length == 3)// 做校验的数据
                                            {
                                                originalV = arr[0];
                                                value = arr[2];
                                                code = arr[1];
                                                if (!CompareData(originalV, colName, djrid, bjstatus, psf_rid, psfbj_rid))
                                                {
                                                    return "F " + str;
                                                }
                                            }
                                            else if (arr.Length == 2) // 主数据没有code，或者主数据没有name 这样的数据code跟name都存导入值 待解决
                                            {
                                                originalV = arr[0];
                                                value = arr[1];
                                                code = arr[1];
                                                if (!CompareData(originalV, colName, djrid, bjstatus, psf_rid, psfbj_rid))
                                                {
                                                    return "F " + str;
                                                }
                                            }
                                            if (i < dtjc.Rows.Count - 1)
                                            {
                                                sql_value_insert += colName + "," + colCode + ",";
                                                sql_value_values += "'" + value + "','" + code + "',";
                                            }
                                            else
                                            {
                                                sql_value_insert += colName + "," + colCode + ")";
                                                sql_value_values += "'" + value + "','" + code + "')";
                                            }
                                        }
                                    }
                                    else
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
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 获取阶梯累计
        /// </summary>
        /// <param name="djfsrid"></param>
        /// <param name="gdzrid"></param>
        /// <returns></returns>
        private string GetJTLJ(string djfsrid, string gdzrid)
        {
            try
            {
                string sql = "";
                if (gdzrid != "")
                {
                    sql = string.Format("select distinct jtlj from sqm_fee_pur_ref where gdzrid = '{0}'", gdzrid);
                }
                else
                {
                    sql = string.Format("select distinct jtlj from sqm_fee_pur_ref where djfsrid = '{0}'", djfsrid);
                }
                string jtlj = DataHelper.QueryValue(sql) + "";
                return jtlj;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 得到返回字符串
        /// </summary>
        /// <param name="djfs"></param>
        /// <param name="gdz"></param>
        /// <returns></returns>
        private static string GetReturnStr(string djfs, string gdz)
        {
            string str = "";
            if (djfs != "" && gdz != "")
            {
                str = djfs + "," + gdz;
            }
            else if (djfs != "")
            {
                str = djfs + "," + "无";
            }
            return str;
        }
        /// <summary>
        /// 值表数据比较
        /// </summary>
        /// <param name="bjValue"></param>
        /// <param name="position"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        private bool CompareData(string bjValue, string position, string rid, string ifsave, string feecalcid, string feecalcidbj)
        {
            bool sign = true;
            string value = "";
            string valueC = "";
            if (DataHelper.QueryValue("select count(*) from sqm_modedj_val where rid = '" + rid + "'") + "" != "0")// 非无定价报价；无定价报价不进行比较
            {
                if (position == "ENDDATE" || position == "STARTDATE")
                {
                    value = DataHelper.QueryValue("select to_char(" + position + ",'yyyy/mm/dd') from SQM_MODEDJ_VAL where RID = '" + rid + "'") + "";
                }
                else
                {
                    string tableName = "";
                    if (ifsave == "0")
                    {
                        tableName = "SQM_MODEDJ_VAL";
                        value = DataHelper.QueryValue("select " + position + " from " + tableName + " where RID = '" + rid + "'") + "";
                        if (position.IndexOf("COLUMN") >= 0)
                        {
                            valueC = DataHelper.QueryValue("select " + position + "C from " + tableName + " where RID = '" + rid + "'") + "";
                        }
                    }
                    else
                    {
                        tableName = "SQM_MODEBJ_VAL";
                        value = DataHelper.QueryValue("select " + position + " from " + tableName + " where DJRID = '" + rid + "' and FEECALCID = '" + feecalcidbj + "'") + "";
                        if (position.IndexOf("COLUMN") >= 0)
                        {
                            valueC = DataHelper.QueryValue("select " + position + "C from " + tableName + " where DJRID = '" + rid + "' and FEECALCID = '" + feecalcidbj + "'") + "";
                        }
                    }
                    if (IfDecimal(value))
                    {
                        value = Convert.ToDecimal(value).ToString("#0.######");// 去掉decimal的无效0
                    }
                }
                if (valueC != "")// 基础
                {
                    if (bjValue.IndexOf("(") >= 0 && bjValue.IndexOf(")") >= 0)// 导入值为港口（老数据）,将导入值做处理，变成代码或者描述两者中的一个
                    {
                        bjValue = bjValue.Replace(")", "").Split('(')[0];// 可能是代码 中文解释 或者英文解释（现在可能没有英文解释了）
                    }
                    if (value.IndexOf("(") >= 0 && value.IndexOf(")") >= 0)// 数据库值为港口（老数据）,将值做处理，变成代码或者描述两者中的一个，以匹配变更过的导入值
                    {
                        value = value.Replace(")", "").Split('(')[0];// 可能是代码 中文解释 或者英文解释（现在可能没有英文解释了）
                    }
                    if (value != bjValue && valueC != bjValue)
                    {
                        sign = false;
                    }
                }
                else// 最高价 最低价 指导价
                {
                    if (value != bjValue)
                    {
                        sign = false;
                    }
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
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                DataSet excel_ds = new DataSet(workbook.Worksheets[i].Name.Replace("&", "/").Replace("|", "*")); //创建数据集
                cells = workbook.Worksheets[i].Cells;
                int rownumber = cells.MaxDataRow;
                string rownum = String.Empty;
                string colnum = String.Empty;
                if (workbook.Worksheets[i].Name == "报价信息") // 客户组织sheet
                {
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
                            if (j == 0 && cellStr == "报价名称")
                            {
                                titleRow = true;
                                dataTable = new DataTable();
                                dataRow = dataTable.NewRow();
                            }
                            if (titleRow)
                            {
                                dataTable.Columns.Add(cellStr);
                            }
                            else
                            {
                                dataRow[j] = cellStr;
                            }
                        }
                        if (!dataRow.IsNull(0))
                        {
                            DataRow drnew = dataTable.NewRow();
                            drnew.ItemArray = dataRow.ItemArray;
                            dataTable.Rows.Add(drnew);
                        }
                    }
                    excel_ds.Tables.Add(dataTable);
                    listDs.Add(excel_ds);
                }
                else // 产品sheet
                {
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
                            if (j == 0 && cellStr == "服务" && cells[k, j + 1].StringValue.Trim() == "服务代码" && cells[k, j + 2].StringValue.Trim() == "费目")
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
                                    if (IfDecimal(cellStr))
                                    {
                                        dataRow[j] = Convert.ToDecimal(cellStr);
                                    }
                                    else
                                    {
                                        dataRow[j] = cellStr;
                                    }
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
                }
            }
            al.Add(listDs);
            return al;
        }
        /// <summary>
        /// Excel下载--报价编辑页“Excel下载”
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceExport()
        {
            string main_id = Request["main_id"] + "";
            string version = Request["version"] + "";
            string filePath = "";
            string fileName = "";
            if (string.IsNullOrEmpty(main_id) || string.IsNullOrEmpty(version))
            {
                return Content(new JsonMessage { Message = "Excel下载失败：获取报价失败！" }.ToString());
            }
            try
            {
                // 报价名称
                DataTable dtbj = DataHelper.QueryDataTable("select BJNAME,RID from SQM_BJ_MAIN_BASIC where RID = '" + main_id + "'");
                string bjname = dtbj.Rows[0]["BJNAME"] + "";
                string rid = dtbj.Rows[0]["RID"] + "";
                // 查询版本 rid
                DataTable dt = new DataTable();
                //string vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", main_id, version)) + "";
                IList<object[]> list = DataHelper.QueryObjectsList(string.Format("SELECT distinct t1.RID,t1.ORGRID,t2.ORGNAME FROM SQM_BJ_VER t1,SQM_BJ_PSF t2 WHERE t1.rid = t2.vrid and t1.MRID = '{0}' AND t1.ZVER = '{1}'", main_id, version));// 对象数组
                string vrid = "";
                string orgcode = "";
                string orgname = "";// 插入psf表要用
                if (list.Count > 0)
                {
                    vrid = list[0][0] + "";
                    orgcode = list[0][1] + "";
                    orgname = list[0][2] + "";
                    Workbook workbook = new Workbook();
                    // 清除默认sheet页
                    workbook.Worksheets.Clear();
                    // 绘制sheet1--“报价信息”
                    workbook.Worksheets.Add("报价信息");// 新建sheet页
                    Worksheet worksheet = workbook.Worksheets[0];
                    Cells cells = worksheet.Cells;
                    worksheet.Name = "报价信息";
                    cells[0, 0].PutValue("报价名称");
                    cells[0, 0].SetStyle(getStyle("styleExcelDownTitle"));
                    cells[0, 1].PutValue("版本号");
                    cells[0, 1].SetStyle(getStyle("styleExcelDownTitle"));
                    cells[0, 2].PutValue("组织代码");
                    cells[0, 2].SetStyle(getStyle("styleExcelDownTitle"));
                    cells[0, 3].PutValue("RID");
                    cells[0, 3].SetStyle(getStyle("styleExcelDownTitle"));
                    cells[0, 4].PutValue("组织名称");
                    cells[0, 4].SetStyle(getStyle("styleExcelDownTitle"));
                    cells[1, 0].PutValue(bjname);
                    cells[1, 0].SetStyle(getStyle("styleExcelDownContent"));
                    cells[1, 1].PutValue(version);
                    cells[1, 1].SetStyle(getStyle("styleExcelDownContent"));
                    cells[1, 2].PutValue(orgcode);
                    cells[1, 2].SetStyle(getStyle("styleExcelDownContent"));
                    cells[1, 3].PutValue(rid);
                    cells[1, 3].SetStyle(getStyle("styleExcelDownContent"));
                    cells[1, 4].PutValue(orgname);
                    cells[1, 4].SetStyle(getStyle("styleExcelDownContent"));
                    // 列宽自适应
                    worksheet.AutoFitColumns();
                    // 隐藏列
                    worksheet.Cells.HideColumns(3, 2);
                    // sheet 保护
                    worksheet.Protect(ProtectionType.Contents, "111111", "");// 取消保护密码
                    // 得到产品信息
                    string sql_pro = "select distinct PRODUCT_CODE,PRODUCT_NAME from sqm_bj_psf where vrid ='" + vrid + "' and choosestatus = '1'";
                    IList<EasyDictionary> listpro = DataHelper.QueryDictList(sql_pro);
                    for (int p = 0; p < listpro.Count; p++)
                    {
                        workbook.Worksheets.Add("sheet");// 新建sheet页
                        worksheet = workbook.Worksheets[p + 1];// 第一个sheet页为“报价信息”
                        cells = worksheet.Cells;
                        string product_name = (listpro[p].Get("PRODUCT_NAME") + "").Replace("/", "&").Replace("*", "|");
                        string product_code = (listpro[p].Get("PRODUCT_CODE") + "").Replace("/", "&").Replace("*", "|");
                        worksheet.Name = product_name + "-" + product_code;

                        // 得到服务信息--其实不用得到服务信息
                        string sql_ser = "select distinct SERVICE_CODE,SERVICE_NAME from sqm_bj_psf where vrid ='" + vrid + "' and product_code = '" + product_code + "' and CHOOSESTATUS = '1'";
                        IList<EasyDictionary> listser = DataHelper.QueryDictList(sql_ser);
                        // 开始绘制
                        int rowIndex = 0;
                        for (int s = 0; s < listser.Count; s++)
                        {
                            string service_code = listser[s].Get("SERVICE_CODE") + "";
                            string sql = string.Empty;
                            // 得到费目信息
                            string sql_fee = "select * from sqm_bj_psf where vrid = '" + vrid + "' and product_code = '" + product_code + "' and service_code = '" + service_code + "' and CHOOSESTATUS = '1' and (bgfzrid is null or bgfzrid = '1')";// 被包干费不进行导出处理
                            DataTable dtFee = new DataTable();
                            dtFee = DataHelper.QueryDataTable(sql_fee);
                            if (dtFee.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtFee.Rows)
                                {
                                    bool isTitle = true;
                                    string sql_search = "";
                                    string feecalcid = "";
                                    string djfsrid = "";
                                    string gdzrid = "";
                                    string feecode = dr["FEE_CODE"] + "";
                                    string feename = dr["FEE_NAME"] + "";
                                    string srvcode = dr["SERVICE_CODE"] + "";
                                    string srvname = dr["SERVICE_NAME"] + "";
                                    string prdcode = dr["PRODUCT_CODE"] + "";
                                    string prdname = dr["PRODUCT_NAME"] + "";
                                    string bjfs = dr["BJFS"] + "";
                                    if (dr["BJSTATAUS"] + "" != "0")// 非未保存状态
                                    {
                                        feecalcid = dr["RID"] + "";
                                    }
                                    else
                                    {
                                        bjfs = DataHelper.QueryValue("select djfs from sqm_dj_psf where prdcode = '" + prdcode + "' and srvcode = '" + srvcode + "' and feecode = '" + feecode + "' and orgrid like '%" + orgcode + "%'") + "";
                                    }
                                    isTitle = true;
                                    // 是否普通报价
                                    if (string.IsNullOrEmpty(bjfs))
                                    {
                                        bjfs = "0";
                                    }
                                    // 获取定价方式rid  从sqm_fee_calc_ref获取  然后从sqm_fee_pur_ref再获取按票计算的定价方式
                                    IList<EasyDictionary> ediclist = DataHelper.QueryDictList("select distinct DJFSRID from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID <> '' or DJFSRID is not null)");
                                    DataTable dtdjfswjc = DataHelper.QueryDataTable("select distinct djfsrid from sqm_fee_pur_ref where djfsrid not in(select djfsrid from sqm_fee_calc_ref where feecode = '" + feecode + "') and feecode = '" + feecode + "'");
                                    if ((ediclist.Count > 0 && bjfs == "0") || (dtdjfswjc.Rows.Count > 0 && bjfs == "0"))// 有基础定价方式与无基础定价方式
                                    {
                                        // 遍历有基础定价方式
                                        foreach (EasyDictionary ed in ediclist)
                                        {
                                            isTitle = true;
                                            djfsrid = ed.Get("DJFSRID") + "";
                                            // 是否高低值
                                            IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID from SQM_FEE_PUR_REF where DJFSRID = '" + djfsrid + "' and (GDZRID is not null or GDZRID <> '')");
                                            if (gdzlist.Count > 0)
                                            {
                                                // 遍历高低值
                                                foreach (EasyDictionary gdz in gdzlist)
                                                {
                                                    isTitle = true;
                                                    gdzrid = gdz.Get("GDZRID") + "";
                                                    if (feecalcid == "") // 定价 status = 0
                                                    {
                                                        sql_search = SearchSqlAll("", feecode, orgcode, "0", djfsrid, "", gdzrid, srvcode, prdcode, "");
                                                        if (sql_search != "")
                                                        {
                                                            CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                        }
                                                    }
                                                    else // 报价 status = 1
                                                    {
                                                        sql_search = SearchSqlAll(feecalcid, feecode, orgcode, "1", djfsrid, "", gdzrid, srvcode, prdcode, "");
                                                        if (sql_search != "")
                                                        {
                                                            CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                        }
                                                    }
                                                }
                                            }
                                            else // 无高低值
                                            {
                                                if (feecalcid == "") // 定价 status = 0
                                                {
                                                    sql_search = SearchSqlAll("", feecode, orgcode, "0", djfsrid, "", "", srvcode, prdcode, "");
                                                    if (sql_search != "")
                                                    {
                                                        CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                    }
                                                }
                                                else // 报价 status = 1
                                                {
                                                    sql_search = SearchSqlAll(feecalcid, feecode, orgcode, "1", djfsrid, "", "", srvcode, prdcode, "");
                                                    if (sql_search != "")
                                                    {
                                                        CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                    }
                                                }
                                            }
                                        }
                                        // 遍历无基础定价方式
                                        foreach (DataRow drdjfs in dtdjfswjc.Rows)
                                        {
                                            isTitle = true;
                                            if (feecalcid == "") // 定价 status = 0
                                            {
                                                sql_search = SearchSqlAll("", feecode, orgcode, "0", "", drdjfs["DJFSRID"] + "", "", srvcode, prdcode, "");
                                                if (sql_search != "")
                                                {
                                                    CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                }
                                            }
                                            else // 报价 status = 1
                                            {
                                                sql_search = SearchSqlAll(feecalcid, feecode, orgcode, "1", "", drdjfs["DJFSRID"] + "", "", srvcode, prdcode, "");
                                                if (sql_search != "")
                                                {
                                                    CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                                }
                                            }
                                        }
                                    }
                                    else // 无基础的费目 无定价方式 At cost 单票单询
                                    {
                                        if (ediclist.Count > 0)
                                        {
                                            djfsrid = ediclist[0].Get("DJFSRID") + "";// 取第一个
                                        }
                                        else if (dtdjfswjc.Rows.Count > 0)
                                        {
                                            djfsrid = dtdjfswjc.Rows[0]["DJFSRID"] + "";// 取第一个
                                        }
                                        if (feecalcid == "") // 定价 status = 0
                                        {
                                            sql_search = SearchSqlAll("", feecode, orgcode, "0", "", "", "", srvcode, prdcode, djfsrid);
                                            if (sql_search != "")
                                            {
                                                CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                            }
                                        }
                                        else // 报价 status = 1
                                        {
                                            sql_search = SearchSqlAll(feecalcid, feecode, orgcode, "1", "", "", "", srvcode, prdcode, "");
                                            if (sql_search != "")
                                            {
                                                CereateExcel2(cells, ref rowIndex, ref isTitle, sql_search, feecode, feename, srvcode, srvname);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // 列宽自适应
                        worksheet.AutoFitColumns();
                        // 隐藏列
                        worksheet.Cells.HideColumns(11, 3);
                        // 锁定无效，不知道这个锁定样式怎么用
                        //int maxRows = worksheet.Cells.MaxRow;
                        //Style style = workbook.Styles[workbook.Styles.Add()];
                        //style.IsLocked = true;
                        //Range range = worksheet.Cells.CreateRange(0, 5, maxRows, 3);
                        //range.SetStyle(style);
                    }
                    // 生成Excel文件
                    fileName = RegexReplace(bjname) + "(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + ".xlsx";
                    filePath = System.IO.Path.Combine(Server.MapPath("/Excel/excel_output/"), fileName);
                    workbook.Save(filePath);
                }
                return Content(new JsonMessage { Message = "/Excel/excel_output/" + fileName, Success = true }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "Excel下载失败：" + ex.Message, Success = false }.ToString());
            }
        }
        /// <summary>
        /// 绘制内容
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="rowIndex"></param>
        /// <param name="isTitle"></param>
        /// <param name="sql_search"></param>
        /// <param name="feecode"></param>
        /// <param name="feename"></param>
        /// <param name="srvcode"></param>
        /// <param name="srvname"></param>
        private void CereateExcel2(Cells cells, ref int rowIndex, ref bool isTitle, string sql_search, string feecode, string feename, string srvcode, string srvname)
        {
            DataTable dtDetail = DataHelper.QueryDataTable(sql_search);
            if (dtDetail.Rows.Count == 0)// 如果没有数据，是否要画空架子，待定
            {

            }
            else
            {
                foreach (DataRow drDetail in dtDetail.Rows)
                {
                    int colIndex = 0;
                    // 绘制标题
                    DrawTitle(cells, ref rowIndex, ref isTitle, dtDetail, ref colIndex);
                    // 绘制内容  
                    // 固定列（位置）
                    // 服务
                    cells[rowIndex, colIndex].PutValue(srvname);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 服务代码
                    cells[rowIndex, colIndex].PutValue(srvcode);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 费目
                    cells[rowIndex, colIndex].PutValue(feename);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 费目代码
                    cells[rowIndex, colIndex].PutValue(feecode);
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 结算方代码
                    if (drDetail.Table.Columns.Contains("JSFCODE"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["JSFCODE"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 费目起始日期
                    if (drDetail.Table.Columns.Contains("BJSTARTDATE"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["BJSTARTDATE"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 费目截止日期
                    if (drDetail.Table.Columns.Contains("BJENDDATE"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["BJENDDATE"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 解析基础
                    if (drDetail.Table.Columns.Contains("JXJC"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["JXJC"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 前提条件
                    if (drDetail.Table.Columns.Contains("QTTJ"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["QTTJ"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 阶段类别
                    if (drDetail.Table.Columns.Contains("STAGETYPE"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["STAGETYPE"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 定价标记
                    if (drDetail.Table.Columns.Contains("RID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["RID"] + "");
                    }
                    else if (drDetail.Table.Columns.Contains("DJRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["DJRID"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // PSFRID
                    if (drDetail.Table.Columns.Contains("PSFRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["PSFRID"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 定价方式ID
                    if (drDetail.Table.Columns.Contains("DJFSRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["DJFSRID"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 高低值ID
                    if (drDetail.Table.Columns.Contains("GDZRID"))
                    {
                        cells[rowIndex, colIndex].PutValue(drDetail["GDZRID"] + "");
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                    }
                    cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                    colIndex++;
                    // 定价方式  高低值
                    if (drDetail.Table.Columns.Contains("DJFSRID") && drDetail.Table.Columns.Contains("GDZRID"))
                    {
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
                    }
                    else
                    {
                        cells[rowIndex, colIndex].PutValue("");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        colIndex++;

                        cells[rowIndex, colIndex].PutValue("");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        colIndex++;
                    }
                    // 非固定列（将固定列字段continue掉）  币种为非固定列，但为非固定列第一列
                    foreach (DataColumn dtcol in dtDetail.Columns)
                    {
                        if (dtcol.ColumnName == "DJFSRID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "GDZRID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "DJRID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "JSFCODE")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "RID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "PSFRID")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "JXJC")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "QTTJ")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "STAGETYPE")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "BJSTARTDATE")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "BJENDDATE")
                        {
                            continue;
                        }
                        else if (dtcol.ColumnName == "BJPRICE")
                        {
                            if (drDetail["BJPRICE"] + "" != "")
                            {
                                cells[rowIndex, colIndex].PutValue(drDetail["BJPRICE"]);
                            }
                            else
                            {
                                cells[rowIndex, colIndex].PutValue(drDetail["GUIDEPRICE"]);
                            }
                            cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                        }
                        else if (dtcol.ColumnName == "CALCTYPE")
                        {
                            if (drDetail[dtcol.ColumnName] + "" == "A")
                            {
                                cells[rowIndex, colIndex].PutValue("绝对值");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                            }
                            else
                            {
                                cells[rowIndex, colIndex].PutValue("相对值");
                                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownContent"));
                            }
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
                cells[rowIndex, colIndex].PutValue("服务");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("服务代码");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("费目");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                cells[rowIndex, colIndex].PutValue("费目代码");
                colIndex++;
                cells[rowIndex, colIndex].PutValue("结算方代码");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("费目起始日期");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("费目截止日期");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("解析基础");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("前提条件");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("阶段类别");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("定价标记");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("PSFRID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("定价方式ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值ID");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("定价方式");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                cells[rowIndex, colIndex].PutValue("高低值");
                cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                colIndex++;
                foreach (DataColumn dtcol in dtDetail.Columns)
                {
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
                        cells[rowIndex, colIndex].PutValue("结束日期");
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
                    else if (dtcol.ColumnName == "MEMO")
                    {
                        cells[rowIndex, colIndex].PutValue("备注");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCUNIT")
                    {
                        cells[rowIndex, colIndex].PutValue("报价单位");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "BJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("报价");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MINBJPRICE")
                    {
                        cells[rowIndex, colIndex].PutValue("最低报价/MIN");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "MIN")
                    {
                        cells[rowIndex, colIndex].PutValue("最低报价/MIN");
                        cells[rowIndex, colIndex].SetStyle(getStyle("styleExcelDownTitle"));
                    }
                    else if (dtcol.ColumnName == "CALCTYPE")
                    {
                        cells[rowIndex, colIndex].PutValue("计算方式");
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
                    else if (dtcol.ColumnName == "DJRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "RID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "JSFCODE")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "PSFRID")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "JXJC")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "QTTJ")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "STAGETYPE")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "BJSTARTDATE")
                    {
                        continue;
                    }
                    else if (dtcol.ColumnName == "BJENDDATE")
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
                    else if (dtcol.ColumnName.IndexOf("*)") >= 0)// 标度为空
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
        Dictionary<string, string> dictionary = new Dictionary<string, string>(); // 全局变量，Excel下载生成标题时用到
        private static DataTable dtjxjc = DataHelper.QueryDataTable("select djfsrid,djfsname,gdzrid,feecode,fsprecond,fsrslbase from sqm_fee_pur_ref where status = '1'");
        private static DataTable dtqttj = DataHelper.QueryDataTable("select feecode,precond from sqm_fee_calc");
        /// <summary>
        /// 拼查询值表的sql
        /// </summary>
        /// <param name="feeid">值表的费目id</param>
        /// <param name="feecode">费目代码</param>
        /// <returns></returns>
        public string SearchSqlAll(string feeid, string feecode, string orgcode, string status, string djfsrid, string djfsrid2, string gdzrid, string srvcode, string prdcode, string djfsrid3)
        {
            string bjfs = "";
            string bjpsfjxjc = "";
            string bjpsfqttj = "";
            string psfrid = "";// 查定价psf表的rid，用这个字段给无定价报价 填“定价标记”字段，定价同步数据会用到
            DataTable dtA = DataHelper.QueryDataTable("select distinct rid,djfs from sqm_dj_psf where feecode = '" + feecode + "' and srvcode = '" + srvcode + "' and prdcode = '" + prdcode + "' and orgrid like '%" + orgcode + "%'");
            if (dtA.Rows.Count > 0)
            {
                psfrid = dtA.Rows[0]["RID"] + "";
            }
            if (feeid == "")
            {
                if (dtA.Rows.Count > 0)
                {
                    feeid = dtA.Rows[0]["RID"] + "";
                    bjfs = dtA.Rows[0]["DJFS"] + "";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                bjfs = DataHelper.QueryValue("select bjfs from sqm_bj_psf where rid = '" + feeid + "'") + "";
            }
            string sql_val = "select t1.CURRENCY";
            DataTable dt = new DataTable();
            string sql_ref = "";
            if (djfsrid == "")
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID = '' or DJFSRID is null)";
            }
            else if (djfsrid != "" && gdzrid == "")// 无高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and DJFSRID = '" + djfsrid + "'";
            }
            else if (djfsrid != "" && gdzrid != "")// 存在高低值
            {
                sql_ref = "select distinct CALCCODE,CALCNAME,VALCOL,SCALE from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and GDZRID = '" + gdzrid + "'";
            }
            dt = DataHelper.QueryDataTable(sql_ref);
            // 普通报价  要获取基础，从而再值表中查询数据
            if (string.IsNullOrEmpty(bjfs) || bjfs == "0")
            {
                if (dt.Rows.Count > 0)
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
            }
            string jxjc = "";
            string qttj = "";
            if (status == "0")// 查询定价值表
            {
                DataRow[] drsqttj = dtqttj.Select("feecode = '" + feecode + "'");
                if (drsqttj.Length > 0)
                {
                    qttj = drsqttj[0]["PRECOND"] + "";// 前提条件
                }
                if (djfsrid == "" && djfsrid2 == "")
                {
                    DataRow[] drs = dtjxjc.Select("djfsrid = '" + djfsrid3 + "'");
                    if (drs.Length > 0)
                    {
                        jxjc = drs[0]["FSRSLBASE"] + "";
                    }
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,t1.CALCUNIT,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MEMO,t1.RID,t1.MIN,case when t2.djfs = '1' then 'AT COST' when t2.djfs = '2' then '单票单询' else to_char(t1.GUIDEPRICE,'9999999990.99') end as BJPRICE,t1.CALCTYPE,t2.RID as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from SQM_MODEDJ_VAL t1,sqm_dj_psf t2 where t2.rid = t1.feecalcid and t1.STATUS <> '0' and (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "'";
                    }
                    else if (bjfs == "2")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'单票单询' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";// 值表没数据，所以如果是‘单票单询’和‘AT COST’就得做一行数据，而且列名符合导出规格
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'AT COST' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                }
                else if (djfsrid != "" && gdzrid == "")
                {
                    DataRow[] drs = dtjxjc.Select("djfsrid = '" + djfsrid + "'");
                    if (drs.Length > 0)
                    {
                        jxjc = drs[0]["FSRSLBASE"] + "";
                    }
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,t1.CALCUNIT,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MEMO,t1.RID,t1.MIN,case when t2.djfs = '1' then 'AT COST' when t2.djfs = '2' then '单票单询' else to_char(t1.GUIDEPRICE,'9999999990.99') end as BJPRICE,t1.CALCTYPE,t2.RID as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from SQM_MODEDJ_VAL t1,sqm_dj_psf t2 where t2.rid = t1.feecalcid and t1.STATUS <> '0' and (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "' and DJFSRID ='" + djfsrid + "'";
                    }
                    else if (bjfs == "2")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'单票单询' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'AT COST' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                }
                else if (djfsrid != "" && gdzrid != "")
                {
                    DataRow[] drs = dtjxjc.Select("gdzrid = '" + gdzrid + "'");
                    if (drs.Length > 0)
                    {
                        jxjc = drs[0]["FSRSLBASE"] + "";
                    }
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,t1.CALCUNIT,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MEMO,t1.RID,t1.MIN,case when t2.djfs = '1' then 'AT COST' when t2.djfs = '2' then '单票单询' else to_char(t1.GUIDEPRICE,'9999999990.99') end as BJPRICE,t1.CALCTYPE,t2.RID as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from SQM_MODEDJ_VAL t1,sqm_dj_psf t2 where t2.rid = t1.feecalcid and t1.STATUS <> '0' and (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "' and GDZRID ='" + gdzrid + "'";
                    }
                    else if (bjfs == "2")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'单票单询' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'AT COST' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                }
                else if (djfsrid2 != "")// 无基础，有定价方式
                {
                    DataRow[] drs = dtjxjc.Select("djfsrid = '" + djfsrid2 + "'");
                    if (drs.Length > 0)
                    {
                        jxjc = drs[0]["FSRSLBASE"] + "";
                    }
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,t1.CALCUNIT,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MEMO,t1.RID,t1.MIN,case when t2.djfs = '1' then 'AT COST' when t2.djfs = '2' then '单票单询' else to_char(t1.GUIDEPRICE,'9999999990.99') end as BJPRICE,t1.CALCTYPE,t2.RID as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from SQM_MODEDJ_VAL t1,sqm_dj_psf t2 where t2.rid = t1.feecalcid and t1.STATUS <> '0' and (DJSTATUS = '1' or (IFBJITEM like '%-%')) and FEECALCID = '" + feeid + "' and DJFSRID ='" + djfsrid2 + "'";
                    }
                    else if (bjfs == "2")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'单票单询' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,'' as CALCUNIT,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MEMO,'' as MIN,'' as RID,'AT COST' as BJPRICE,'' as CALCTYPE,'' as PSFRID,'" + jxjc + "' as JXJC,'" + qttj + "' as QTTJ,'' as BJSTARTDATE,'' as BJENDDATE from dual";
                    }
                }
            }
            else // 查询报价值表
            {
                if (djfsrid == "" && djfsrid2 == "")
                {
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,to_char(t2.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(t2.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,t1.CALCUNIT,case when (t2.JSFCODE is not null or t2.JSFCODE <> '') then t2.JSFCODE else t2.JSF end as JSFCODE,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MINBJPRICE,case when t2.bjfs = '1' then 'AT COST' when t2.bjfs = '2' then '单票单询' else to_char(t1.BJPRICE,'9999999990.99') end as BJPRICE,t1.MEMO,case when (t1.GUIDEPRICE is not null or t1.GUIDEPRICE <> '') then t1.DJRID else to_number('') end as DJRID,t1.CALCTYPE,'" + psfrid + "' as PSFRID,t1.JXJC,t2.CONDITION as QTTJ,t1.FSJDLB as STAGETYPE from SQM_MODEBJ_VAL t1,sqm_bj_psf t2 where ifbjitem = '1' and t1.status = '1' and t2.rid = t1.feecalcid and FEECALCID = '" + feeid + "'";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'AT COST' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                    else
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'单票单询' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                }
                else if (djfsrid != "" && gdzrid == "")
                {
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,to_char(t2.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(t2.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,t1.CALCUNIT,case when (t2.JSFCODE is not null or t2.JSFCODE <> '') then t2.JSFCODE else t2.JSF end as JSFCODE,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MINBJPRICE,case when t2.bjfs = '1' then 'AT COST' when t2.bjfs = '2' then '单票单询' else to_char(t1.BJPRICE,'9999999990.99') end as BJPRICE,t1.MEMO,case when (t1.GUIDEPRICE is not null or t1.GUIDEPRICE <> '') then t1.DJRID else to_number('') end as DJRID,t1.CALCTYPE,'" + psfrid + "' as PSFRID,t1.JXJC,t2.CONDITION as QTTJ,t1.FSJDLB as STAGETYPE from SQM_MODEBJ_VAL t1,sqm_bj_psf t2 where ifbjitem = '1' and t1.status = '1' and t2.rid = t1.feecalcid and FEECALCID = '" + feeid + "' and DJFSRID = '" + djfsrid + "'";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'AT COST' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                    else
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'单票单询' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                }
                else if (djfsrid != "" && gdzrid != "")
                {
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,to_char(t2.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(t2.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,t1.CALCUNIT,case when (t2.JSFCODE is not null or t2.JSFCODE <> '') then t2.JSFCODE else t2.JSF end as JSFCODE,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MINBJPRICE,case when t2.bjfs = '1' then 'AT COST' when t2.bjfs = '2' then '单票单询' else to_char(t1.BJPRICE,'9999999990.99') end as BJPRICE,t1.MEMO,case when (t1.GUIDEPRICE is not null or t1.GUIDEPRICE <> '') then t1.DJRID else to_number('') end as DJRID,t1.CALCTYPE,'" + psfrid + "' as PSFRID,t1.JXJC,t2.CONDITION as QTTJ,t1.FSJDLB as STAGETYPE from SQM_MODEBJ_VAL t1,sqm_bj_psf t2 where ifbjitem = '1' and t1.status = '1' and t2.rid = t1.feecalcid and FEECALCID = '" + feeid + "' and GDZRID ='" + gdzrid + "'";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'AT COST' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                    else
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'单票单询' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                }
                else if (djfsrid2 != "")// 无基础，有定价方式
                {
                    if (bjfs == "0" || string.IsNullOrEmpty(bjfs))
                    {
                        sql_val += ",to_char(t1.STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(t1.ENDDATE,'yyyy/mm/dd') as ENDDATE,to_char(t2.BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(t2.BJENDDATE,'yyyy/mm/dd') as BJENDDATE,t1.CALCUNIT,case when (t2.JSFCODE is not null or t2.JSFCODE <> '') then t2.JSFCODE else t2.JSF end as JSFCODE,t1.MAXPRICE,t1.MINPRICE,t1.GUIDEPRICE,t1.DJFSRID,t1.GDZRID,t1.MINBJPRICE,case when t2.bjfs = '1' then 'AT COST' when t2.bjfs = '2' then '单票单询' else to_char(t1.BJPRICE,'9999999990.99') end as BJPRICE,t1.MEMO,case when (t1.GUIDEPRICE is not null or t1.GUIDEPRICE <> '') then t1.DJRID else to_number('') end as DJRID,t1.CALCTYPE,'" + psfrid + "' as PSFRID,t1.JXJC,t2.CONDITION as QTTJ,t1.FSJDLB as STAGETYPE from SQM_MODEBJ_VAL t1,sqm_bj_psf t2 where ifbjitem = '1' and t1.status = '1' and t2.rid = t1.feecalcid and FEECALCID = '" + feeid + "' and DJFSRID = '" + djfsrid2 + "'";
                    }
                    else if (bjfs == "1")
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'AT COST' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                    else
                    {
                        sql_val = "select '' as CURRENCY,'' as STARTDATE,'' as ENDDATE,to_char(BJSTARTDATE,'yyyy/mm/dd') as BJSTARTDATE,to_char(BJENDDATE,'yyyy/mm/dd') as BJENDDATE,'' as CALCUNIT,case when (JSFCODE is not null or JSFCODE <> '') then JSFCODE else JSF end as JSFCODE,'' as MAXPRICE,'' as MINPRICE,'' as GUIDEPRICE,'' as DJFSRID,'' as GDZRID,'' as MINBJPRICE,'单票单询' as BJPRICE,'' as MEMO,'' as DJRID,'' as CALCTYPE,'' as PSFRID,jxjc as JXJC,condition as QTTJ,STAGETYPE from sqm_bj_psf where rid = '" + feeid + "' and (status <> '0' or status is null)";
                    }
                }
            }
            return sql_val;
        }
        /// <summary>
        /// 主数据校验
        /// </summary>
        /// <param name="value">校验字段值</param>
        /// <param name="type">校验字段类型：国家、港口</param>
        /// <returns></returns>
        private static DataTable dttype = DataHelper.QueryDataTable("select distinct MDTYPE,MDKEY from mdm_calc_basic");
        private static DataTable dtsettings = DataHelper.QueryDataTable("select * from SQM_CALC_BASE_EXT");// where calccode like '%" + calccode + "%'
        private static DataTable dtmainstrc = DataHelper.QueryDataTable("select * from mdm_calc_strc");
        public string MainDataExist(string calccode, string value, string type)
        {
            string code = "";
            string name = "";
            if (type == "1")// 国家
            {
                string gjdm = "T005T";
                string columnName = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LANDX'").ToString();
                string columnCode = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_MAIN_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LAND1'").ToString();
                // 语言 '1'：中文  'E'：英文 现要求英文大写
                //string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + gjdm + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = 'E'";
                string sql = string.Format("SELECT distinct {3},{1} FROM MDM_MIAN_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {3} = '{2}')", gjdm, columnName, value, columnCode);
                //if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                //{
                //    code = DataHelper.QueryValue(sql).ToString();
                //}
                DataTable dt = DataHelper.QueryDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    code = dt.Rows[0][columnCode] + "";
                    name = dt.Rows[0][columnName] + "";
                }
            }
            else if (type == "2")// 港口
            {
                if (value.IndexOf("(") >= 0 && value.IndexOf(")") >= 0)// 导出数据会出现 三字代码+英文解释+中文解释的形式
                {
                    value = value.Replace(")", "").Split('(')[0] + "";
                }
                string sql = "select distinct locno,DESCR40 from MDM_LOC where DESCR40 = '" + value.ToLower() + "' or DESCR40 = '" + value.ToUpper() + "' or DESCR40 = '" + value + "' or LOCNO = '" + value.ToUpper() + "' or LOCNO = '" + value.ToLower() + "' or LOCNO = '" + value + "' and loctype in('1110','1100')";
                //if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                //{
                //    code = DataHelper.QueryValue(sql).ToString();
                //}
                DataTable dt = DataHelper.QueryDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    code = dt.Rows[0]["LOCNO"] + "";
                    name = dt.Rows[0]["DESCR40"] + "";
                }
            }
            else if (type == "3")
            {
                //code = true;
            }
            else if (type == "4")// 船公司 、 航空公司
            {
                //if (value.IndexOf("(") >= 0 && value.IndexOf(")") >= 0)// 导出数据会出现 三字代码+英文解释+中文解释的形式
                //{
                //    value = value.Replace(")", "").Split('(')[0] + "";// 取第一个，不管是代码还是描述
                //}
                string sql = "select distinct BPKEY,BPNAME from MDM_BP where BPNAME = '" + value.ToLower() + "' or BPNAME = '" + value.ToUpper() + "' or BPNAME = '" + value + "' or BPKEY = '" + value.ToUpper() + "' or BPKEY = '" + value.ToLower() + "' or BPKEY = '" + value + "'";
                DataTable dt = DataHelper.QueryDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    code = dt.Rows[0]["BPKEY"] + "";
                    name = dt.Rows[0]["BPNAME"] + "";
                }
            }
            else if (type == "5")// 码头
            {
                code = "mt";
            }
            else if (type == "6")// 通用计算基础 fieldname 两个值，1是code，2是value
            {
                string position_code = "";
                string position_value = "";
                // 判断是否A类型  A类型在结构表没有数据，所以不用配置主数据 
                string mdtype = "";
                DataRow[] drstype = dttype.Select("mdkey = '" + calccode + "'");
                if (drstype.Length > 0)
                {
                    mdtype = drstype[0]["MDTYPE"] + "";
                }
                if (mdtype == "A")
                {
                    position_code = "COLUMN3";
                    position_value = "COLUMN4";
                }
                else if (mdtype == "C")
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
                    DataRow[] drssettings = dtsettings.Select("CALCCODE LIKE '%" + calccode + "%'");
                    if (drssettings.Length > 0)
                    {
                        string[] fieldName = (drssettings[0]["MDMFIELDNAME"] + "").Split(',');
                        string filedcode = fieldName[0];
                        string fieldvalue = fieldName[1];
                        DataRow[] drscol1 = dtmainstrc.Select("MDKEY = '" + calccode + "' and FIELDNAME = '" + filedcode + "'");
                        DataRow[] drscol2 = dtmainstrc.Select("MDKEY = '" + calccode + "' and FIELDNAME = '" + fieldvalue + "'");
                        if (drscol1.Length > 0 && drscol2.Length > 0)
                        {
                            position_code = "COLUMN" + drscol1[0]["POSITION"];
                            position_value = "COLUMN" + drscol2[0]["POSITION"];
                        }
                    }
                }
                if (position_code != "" && position_value != "")
                {
                    DataTable dt = new DataTable();
                    if (calccode == "ZTGFS" || calccode == "ZDZCBJ" || calccode == "COMMODITY_CODE" || calccode == "ZCXDW" || calccode == "ZTDHLX")//个别主数据  数据为03,导入值为3,则检验成功
                    {
                        string sql = "";
                        if (IfDecimal(value))
                        {
                            sql = "select " + position_code + "," + position_value + " from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or to_char(to_number(" + position_code + ")) = '" + (Convert.ToDecimal(value) + "").ToLower() + "' or to_char(to_number(" + position_code + ")) = '" + (Convert.ToDecimal(value) + "").ToUpper() + "' or to_char(to_number(" + position_code + ")) = '" + Convert.ToDecimal(value) + "" + "' or " + position_value + " = '" + value + "')";
                        }
                        else
                        {
                            sql = "select " + position_code + "," + position_value + " from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value.ToLower() + "' or " + position_code + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value + "" + "' or " + position_value + " = '" + value + "')";
                        }
                        dt = DataHelper.QueryDataTable(sql);
                    }
                    else if (value != "null")
                    {
                        dt = DataHelper.QueryDataTable("select " + position_code + "," + position_value + " from mdm_calc_value where mdkey = '" + calccode + "' and (" + position_value + " = '" + value.ToLower() + "' or " + position_value + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value.ToLower() + "' or " + position_code + " = '" + value.ToUpper() + "' or " + position_code + " = '" + value + "' or " + position_value + " = '" + value + "')");
                    }
                    else
                    {
                        dt = DataHelper.QueryDataTable("select case when " + position_code + " is null then 'null' else to_char(" + position_code + ") end as \"" + position_code + "\",case when " + position_value + " is null then 'null' else to_char(" + position_value + ") end as \"" + position_value + "\" from mdm_calc_value where mdkey = '" + calccode + "' and ((" + position_value + " is null and " + position_code + " is not null) or (" + position_value + " is not null and " + position_code + " is null))");
                    }
                    if (dt.Rows.Count > 0)
                    {
                        code = dt.Rows[0][position_code] + "";
                        name = dt.Rows[0][position_value] + "";
                    }
                }
            }
            else if (type == "product")
            {
                string sql = "select case when t2.sqproductname = '' or t2.sqproductname is null then t1.productname else t2.sqproductname end as productname from mdm_product t1 left join sqm_prd_ext t2 on t1.productkey = t2.productkey where t1.productkey = '" + value + "' or t1.productkey = '" + value.ToUpper() + "' or t1.productkey = '" + calccode + "' or t1.productkey = '" + calccode.ToUpper() + "'";
                string sql2 = "select case when t2.sqproductname = '' or t2.sqproductname is null then t1.productname else t2.sqproductname end as productname from mdm_product t1 left join sqm_prd_ext t2 on t1.productkey = t2.productkey where t1.productkey = '" + value + "' or t1.productkey = '" + value.ToUpper() + "' or t1.productkey = '" + calccode + "' or t1.productkey = '" + calccode.ToUpper() + "'";
                if (!string.IsNullOrEmpty(DataHelper.QueryValue(sql) + ""))
                {
                    code = DataHelper.QueryValue(sql) + "";
                }
                else if (!string.IsNullOrEmpty(DataHelper.QueryValue(sql2) + ""))
                {
                    code = DataHelper.QueryValue(sql2) + "";
                }
            }
            else if (type == "service")
            {
                string sql = "select count(*) from mdm_prd_srv_ref where productcode = '" + value.Split(',')[0] + "' and servicetypecode = '" + value.Split(',')[1] + "'";
                if (!string.IsNullOrEmpty(DataHelper.QueryValue(sql) + ""))
                {
                    code = DataHelper.QueryValue(sql) + "";
                }
            }
            else if (type == "fee")
            {
                string sql = "select count(*) from mdm_srv_fee_ref where srvrqcd121 = '" + value.Split(',')[0] + "' and tcet084 = '" + value.Split(',')[1] + "'";
                if (!string.IsNullOrEmpty(DataHelper.QueryValue(sql) + ""))
                {
                    code = DataHelper.QueryValue(sql) + "";
                }
            }
            return code + "&&" + name;
        }
        /// <summary>
        /// 主数据长度校验  数字与非数字
        /// </summary>
        /// <param name="calccode"></param>
        /// <param name="dataval"></param>
        /// <returns></returns>
        private static DataTable dtdatalen = DataHelper.QueryDataTable("select * from sqm_calc_base");// 校验数据长度
        public string CheckData(string calccode, string dataval)
        {
            string checkVal = "0";
            int datalen; // 总长度
            int pointlen; // 小数位
            int numlen; // 整数位
            DataRow[] drs = dtdatalen.Select("calc_base = '" + calccode + "'");
            if (drs.Length > 0)
            {
                // 位数校验
                if (IfDecimal(dataval))// 数字校验
                {
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
                    // 数据库基础长度字典
                    datalen = Convert.ToInt32(drs[0]["DATALEN"] + "" == "" ? "0" : drs[0]["DATALEN"].ToString()); // 总长度
                    pointlen = Convert.ToInt32(drs[0]["POINTLEN"] + "" == "" ? "0" : drs[0]["POINTLEN"].ToString()); //  小数位
                    numlen = datalen - pointlen;// 整数位

                    if (datavalnum.Length > numlen)
                    {
                        checkVal = "1";// 整数位的长度超限
                    }
                    else if (datavalpoint != "" && datavalpoint.Length > pointlen)
                    {
                        checkVal = "2";// 小数位长度超限
                    }
                }
                else // 非数字校验
                {
                    datalen = Convert.ToInt32(drs[0]["DATALEN"] + "" == "" ? "0" : drs[0]["DATALEN"].ToString()); // 总长度
                    pointlen = Convert.ToInt32(drs[0]["POINTLEN"] + "" == "" ? "0" : drs[0]["POINTLEN"].ToString()); // 小数位
                    if (pointlen == 0 && dataval.Length > datalen)
                    {
                        checkVal = "3";
                    }
                }
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
                        checkVal = "4";// 标记X校验错误
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
                        checkVal = "5";// 标记Y校验错误
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
        private bool IfDecimal(string message)
        {
            //if (Regex.IsMatch(message, @"^\d+$"))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            try
            {
                Convert.ToDecimal(message);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断是否有汉字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IfChinese(string text)
        {
            bool isChinese = false;
            foreach (char t in text)
            {
                if ((int)t > 127)
                {
                    isChinese = true;
                    break;
                }
            }
            return isChinese;
        }

        /// <summary>
        /// webservice 获取图片的流，并且返回图片的高度宽度
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private System.IO.Stream getStream(string filename, out string height, out string width)
        {
            //System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(filename);
            //webRequest.Method = "GET";
            //System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            //System.IO.Stream s = webResponse.GetResponseStream();
            System.Net.FileWebRequest webRequest = (System.Net.FileWebRequest)System.Net.WebRequest.Create(filename);
            webRequest.Method = "GET";
            System.Net.FileWebResponse webResponse = (System.Net.FileWebResponse)webRequest.GetResponse();
            Stream s = webResponse.GetResponseStream();
            List<byte> list = new List<byte>();
            while (true)
            {
                int data = s.ReadByte();
                if (data == -1)
                    break;
                else
                {
                    byte b = (byte)data;
                    list.Add(b);
                }
            }
            byte[] bb = list.ToArray();
            System.IO.Stream stream = new System.IO.MemoryStream(bb);
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(stream);
            height = img.Height.ToString();
            width = img.Width.ToString();
            return stream;
        }
        /// <summary>
        /// 得到文件的 byte
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private byte[] getBytes(string filename)
        {
            System.Net.FileWebRequest webRequest = (System.Net.FileWebRequest)System.Net.WebRequest.Create(filename);
            webRequest.Method = "GET";
            System.Net.FileWebResponse webResponse = (System.Net.FileWebResponse)webRequest.GetResponse();
            Stream s = webResponse.GetResponseStream();
            List<byte> list = new List<byte>();
            while (true)
            {
                int data = s.ReadByte();
                if (data == -1)
                    break;
                else
                {
                    byte b = (byte)data;
                    list.Add(b);
                }
            }
            byte[] bb = list.ToArray();
            return bb;
        }
        /// <summary>
        /// 数字转汉字(整数才有意义，且最大支持到100)  0123456789 -> 零一二三四五六七八九
        /// 23 -> 二十三  20 -> 二十
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string NumberToChinese(int num)
        {
            string chinese = "";
            string numberStr = "123456789";
            string chineseStr = "一二三四五六七八九";
            if (num == 0)
            {
                chinese = "零";
            }
            else
            {
                char[] c = num.ToString().ToCharArray();
                if (c.Length == 1)
                {
                    chinese = chineseStr.ToCharArray()[numberStr.IndexOf(c[0])].ToString();

                }
                else if (c.Length == 2)
                {
                    string char1 = c[0].ToString();
                    string char2 = c[1].ToString();
                    if (char2 == "0")
                    {
                        if (char1 == "1")
                        {
                            chinese = "十";
                        }
                        else
                        {
                            chinese = chineseStr.ToCharArray()[numberStr.IndexOf(char1)].ToString() + "十";
                        }
                    }
                    else
                    {
                        string str1 = chineseStr.ToCharArray()[numberStr.IndexOf(char1)].ToString();
                        string str2 = chineseStr.ToCharArray()[numberStr.IndexOf(char2)].ToString();
                        chinese = str1 + "十" + str2;
                    }
                }
            }
            return chinese;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool CheckFileExist(string filePath)
        {
            bool exist = false;
            if (System.IO.File.Exists(filePath))
            {
                exist = true;
            }
            return exist;
        }
        /// <summary>
        /// 替换文件名中的特殊字符
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string RegexReplace(string fileName)
        {
            return Regex.Replace(fileName, @"[\\/*<>?？:：""]", "").Replace(" ", ""); // [\\\\/:*?\"<>|]
        }
        /// <summary>
        /// 处理pdf空白页
        /// </summary>
        /// <param name="filepath"></param>
        public void DealWithPdf(string filepath)
        {
            StartProcess(AppDomain.CurrentDomain.BaseDirectory + @"\PatchPDFBlank\DealWithBlankPDF.exe", new string[] { filepath });
        }

        public bool StartProcess(string runFilePath, params string[] args)
        {
            string s = "";
            foreach (string arg in args)
            {
                s = s + arg + " ";
            }
            s = s.Trim();
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(runFilePath, s);
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }
        //public void DealWithPdf(string filepath)
        //{
        //    Foqus.ProcessHelper.StartProcess(AppDomain.CurrentDomain.BaseDirectory + @"/PatchPDFBlank/PatchPDFBlank.exe", new string[] { filepath });
        //}
    }
}
