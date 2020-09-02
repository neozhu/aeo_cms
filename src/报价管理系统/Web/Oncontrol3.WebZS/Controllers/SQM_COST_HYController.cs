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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using Aspose.Cells;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.OracleClient;
using System.Net.Mail;
using Oncontrol3.Web.Helpers;

namespace OnControl.Web
{
    public class JsonMessage
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public Object Data { get; set; }
        /// <summary>
        /// 结果编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }
    }
    public partial class SQM_COST_HYController : BaseController
    {
        public static string ReturnMessage(string rid,string type)
        {
            string msg = "";
            string tableName = "";
            if (type == "hy")
            {
                tableName = "sqm_cost_hy";
            }
            else if (type == "kygj")
            {
                tableName = "sqm_cost_kygj";
            }
            else if (type == "kygn")
            {
                tableName = "sqm_cost_kygn";
            }
            DataTable dt = DataHelper.QueryDataTable("select * from " + tableName + " where rid = '" + rid + "'");
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    foreach(DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "AREA")
                        {
                            msg += "国家/地区：" + dr["AREA"] + ";";
                        }
                        else if (col.ColumnName == "QYG")
                        {
                            msg += "起运港：" + dr["QYG"] + ";";
                        }
                        else if (col.ColumnName == "MDG")
                        {
                            msg += "目的港：" + dr["MDG"] + ";";
                        }
                        else if (col.ColumnName == "CGS")
                        {
                            msg += "船公司：" + dr["CGS"] + ";";
                        }
                        else if (col.ColumnName == "BZ")
                        {
                            msg += "币种：" + dr["BZ"] + ";";
                        }
                        else if (col.ColumnName == "HC")
                        {
                            msg += "航程：" + dr["HC"] + ";";
                        }
                        else if (col.ColumnName == "ZZG")
                        {
                            msg += "中转港：" + dr["ZZG"] + ";";
                        }
                        else if (col.ColumnName == "KHR")
                        {
                            msg += "开航日：" + dr["KHR"] + ";";
                        }
                        else if (col.ColumnName == "MT")
                        {
                            msg += "码头：" + dr["MT"] + ";";
                        }
                        else if (col.ColumnName == "SKB")
                        {
                            msg += "时刻表：" + dr["SKB"] + ";";
                        }
                        else if (col.ColumnName == "地区")
                        {
                            msg += "起运港：" + dr["QYG"] + ";";
                        }
                        else if (col.ColumnName == "HKGS")
                        {
                            msg += "航空公司：" + dr["HKGS"] + ";";
                        }
                        else if (col.ColumnName == "HWLB")
                        {
                            msg += "货物类别：" + dr["HWLB"] + ";";
                        }
                        else if (col.ColumnName == "HBH")
                        {
                            msg += "航班号：" + dr["QYG"] + ";";
                        }
                        else if (col.ColumnName == "HX")
                        {
                            msg += "航线：" + dr["HX"] + ";";
                        }
                        else if (col.ColumnName == "STARTDATE")
                        {
                            msg += "起始日期：" + dr["STARTDATE"].ToString().Substring(0, 10) + ";";
                        }
                        else if (col.ColumnName == "ENDDATE")
                        {
                            msg += "截止日期：" + dr["ENDDATE"].ToString().Substring(0,10) + "";
                        }
                    }
                }
            }
            return msg;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            //国家和地区数据
            DataTable GJDQdt = DataHelper.QueryDataTable(" select scbe.calccode,scbe.mdmtype,scbe.mdmfieldname,scbe.mdmkey from sqm_calc_base_ext  scbe where scbe.calccode='DESTLOC_CNTRY'");
            ViewBag.GJDQdtData = GJDQdt;
            return View();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "MDG", "AREA", "QYG", "MT", "KHR", "CGS" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_COST_HY).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (!string.IsNullOrEmpty(Request["Status"]))
            {
                SearchCriterion.AddSearch("STATUS", Request["Status"], Aim.Data.SearchModeEnum.Equal);
            }
            if (!string.IsNullOrEmpty(Request["STARTDATE"]))
            {
                SearchCriterion.AddSearch("STARTDATE", DateTime.Parse(Request["STARTDATE"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["ENDDATE"]))
            {
                SearchCriterion.AddSearch("ENDDATE", DateTime.Parse(Request["ENDDATE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_COST_HY), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_COST_HY>());
            var obj = new { draw = Request["draw"], data = SQM_COST_HY.FindAll(SearchCriterion).OrderByDescending(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Edit(string id)
        {
            DataTable BZdt = DataHelper.QueryDataTable("select WAERS,KTEXT from MDM_WAERS");
            ViewBag.BZdtData = BZdt;
            //国家和地区数据
            DataTable GJDQdt = DataHelper.QueryDataTable(" select scbe.calccode,scbe.mdmtype,scbe.mdmfieldname,scbe.mdmkey from sqm_calc_base_ext  scbe where scbe.calccode='DESTLOC_CNTRY'");
            ViewBag.GJDQdtData = GJDQdt;
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            if (!string.IsNullOrEmpty(id))
            {
                SQM_COST_KYGJ ent = SQM_COST_KYGJ.Find(id);
                return View("Edit", ent);
            }
            else
            {
                return View("Edit");
            }
        }
        /// <summary>
        /// 逻辑删除，更新状态为失效
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Delete()
        {
            try
            {
                string id = Request["rowId"] + "";
                // 判断是否定价
                string count = DataHelper.QueryValue("select count(*) from sqm_modedj_val where costrid = '" + id + "'") + "";
                if(count == "0")
                {
                    SQM_COST_HY ent = SQM_COST_HY.Find(id);
                    ent.STATUS = "0";
                    ent.DoUpdate();
                }
                else
                {
                    return Content("删除失败:该成本已被定价！");
                }
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }
        /// <summary>
        /// 逻辑批量删除，更新状态为失效
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DeleteBatch()
        {
            string[] rowIdArr = Request["rowIds"].Split(',');
            try
            {
                SQM_COST_HY[] ents = SQM_COST_HY.FindAll(Expression.In("RID", rowIdArr));
                foreach (SQM_COST_HY ent in ents)
                {
                    // 判断是否定价
                    string count = DataHelper.QueryValue("select count(*) from sqm_modedj_val where costrid = '" + ent.RID + "'") + "";
                    if(count == "0")
                    {
                        ent.STATUS = "0";
                        ent.DoUpdate();
                    }
                    else
                    {
                        return Content("删除失败:该成本已被定价！");
                    }
                }
                return Content("删除成功!");
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
        }
        // 主键字段
        string[] primaryKeys = { "AREACODE", "QYGCODE", "MDGCODE", "BZCODE", "CGSCODE", "HC", "ZZGCODE", "MTCODE", "KHR" };
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SubmitForm(string postdata, string keyValue)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";

            SQM_COST_HY targetobj = null;
            SQM_COST_HY srcobj = null;
            try
            {
                srcobj = JsonHelper.GetObject<SQM_COST_HY>(postdata);
                // 主数据校验
                string areacode = srcobj.AREACODE + "";
                string qygcode = srcobj.QYGCODE + "";
                string mdgcode = srcobj.MDGCODE + "";
                string zzgcode = srcobj.ZZGCODE + "";
                string cgscode = srcobj.CGSCODE + "";
                string mtcode = srcobj.MTCODE + "";

                //string area = (srcobj.AREA + "");
                //string qyg = (srcobj.QYG + "").Replace(")", "");
                //if (qyg.IndexOf("(") >= 0) { qyg = qyg.Substring(qyg.LastIndexOf("(") + 1); }
                //string mdg = (srcobj.MDG + "").Replace(")", "");
                //if (mdg.IndexOf("(") >= 0) { mdg = mdg.Substring(mdg.LastIndexOf("(") + 1); }
                //string zzg = (srcobj.ZZG + "").Replace(")", "");
                //if (zzg.IndexOf("(") >= 0) { zzg = zzg.Substring(zzg.LastIndexOf("(")); }
                //string cgs = (srcobj.CGS + "");
                //string mt = (srcobj.MT + "").Replace(")", "");
                //mt = mt.Substring(mt.LastIndexOf("("));

                //if ((areacode != MainDataExist(area, "1") || areacode == "") && area != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,请选择正确的地区数据!" }.ToString());
                //}
                //else
                //{
                //    srcobj.AREACODE = MainDataExist(area, "1");
                //}
                //if ((qygcode != MainDataExist(qyg, "2") || qygcode == "") && qyg != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,请选择正确的起运港数据!" }.ToString());
                //}
                //else
                //{
                //    srcobj.QYGCODE = MainDataExist(qyg, "2");
                //}
                //if ((mdgcode != MainDataExist(mdg, "2") || mdgcode == "") && mdg != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,请选择正确的目的港数据!" }.ToString());
                //}
                //else
                //{
                //    srcobj.MDGCODE = MainDataExist(mdg, "2");
                //}
                //if ((zzgcode != MainDataExist(zzg, "2") || zzgcode == "") && zzg != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,请选择正确的中转港数据!" }.ToString());
                //}
                //else
                //{
                //    srcobj.ZZGCODE = MainDataExist(zzg, "2");
                //}
                //if ((cgscode != MainDataExist(cgs, "4") || cgscode == "") && cgs != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,请选择正确的船公司数据!" }.ToString());
                //}
                //else
                //{
                //    srcobj.CGSCODE = MainDataExist(cgs, "4");
                //}
                //else if (mt != MainDataExist(mt, "4") && mt != "")
                //{
                //    return Content(new JsonMessage { Success = false, Code = "check", Message = "保存失败,码头数据有误!" }.ToString());
                //}

                if (!string.IsNullOrEmpty(keyValue)) // 修改
                {
                    // 获取本页原始数据
                    targetobj = SQM_COST_HY.TryFind(keyValue);
                    // 获取修改有效期
                    DateTime startDate = (DateTime)srcobj.STARTDATE;
                    // 如果起始日期小于当前日期，暂定禁止保存
                    //if (startDate <= DateTime.Now.AddDays(-1))
                    //{
                    //    return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "修改失败,起始日期小于当前日期!" }.ToString());
                    //}
                    DateTime endDate = (DateTime)srcobj.ENDDATE;
                    if ((startDate != targetobj.STARTDATE || endDate != targetobj.ENDDATE) && (srcobj.STATUS == targetobj.STATUS)) // 有效期修改,若记录已定价或报价，则邮件通知相应定价、报价人员
                    {
                        if (startDate >= endDate)
                        {
                            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "修改失败，截止日期不能小于或等于起始日期" }.ToString());
                        }
                        // 获取原始数据
                        DataTable srcDt = FindSourceData(srcobj, primaryKeys);
                        if (srcDt.Rows.Count > 0)// 只修改了有效期
                        {
                            // 处理有效期
                            foreach (DataRow dr in srcDt.Rows)
                            {
                                DateTime startDate_old = (DateTime)dr["STARTDATE"];
                                DateTime endDate_old = (DateTime)dr["ENDDATE"];
                                HandleValidDate(startDate, endDate, startDate_old, endDate_old, targetobj, srcobj, dr);
                            }
                        }
                        else// 可能修改了其他基础字段数据：船公司、航程、中转港、码头
                        {
                            srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            srcobj.DoCreate();
                            //return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "修改失败，数据异常" }.ToString());
                        }
                    }
                    else if ((startDate != targetobj.STARTDATE || endDate != targetobj.ENDDATE) && (srcobj.STATUS != targetobj.STATUS))
                    {
                        targetobj.STATUS = "0";
                        targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        targetobj.DoSave();
                    }
                    else if (startDate == targetobj.STARTDATE && endDate == targetobj.ENDDATE && (targetobj.ZZG != srcobj.ZZG || targetobj.HC != srcobj.HC || targetobj.CGS != srcobj.CGS || targetobj.MT != srcobj.MT))// 不修改有效期，只修改其他可修改字段
                    {
                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        srcobj.DoUpdate();
                    }
                    else // 报价、备注等非基础项修改
                    {
                        DataHelper.MergeData<SQM_COST_HY>(targetobj, srcobj);
                        targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        targetobj.DoSave();
                    }
                }
                else // 新增
                {
                    // 获取新增有效期
                    DateTime startDate = (DateTime)srcobj.STARTDATE;
                    // 如果起始日期小于当前日期，暂定禁止保存
                    if (startDate <= DateTime.Now.AddDays(-1))
                    {
                        return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "新增失败,起始日期小于当前日期!" }.ToString());
                    }
                    DateTime endDate = (DateTime)srcobj.ENDDATE;
                    if (startDate >= endDate)
                    {
                        return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "新增失败,截止日期不能小于或等于起始日期" }.ToString());
                    }
                    // 获取原始数据
                    DataTable srcDt = FindSourceData(srcobj, primaryKeys);
                    if (srcDt.Rows.Count > 0)
                    {
                        // 处理有效期
                        foreach (DataRow dr in srcDt.Rows)
                        {
                            DateTime startDate_old = (DateTime)dr["STARTDATE"];
                            DateTime endDate_old = (DateTime)dr["ENDDATE"];
                            HandleValidDate(startDate, endDate, startDate_old, endDate_old, targetobj, srcobj, dr);
                        }
                    }
                    else
                    {
                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        srcobj.DoSave();
                    }
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            //发送邮件
            if (keyValue != "")
            {
               
                string HYGJmessage = "";
                string title = "成本变更";
                string body = "<br>您好！此价格发生变动，请及时跟进处理。谢谢！";
                string CreateUser = "";

                string mailServer = System.Configuration.ConfigurationManager.AppSettings["mailServer"];
                string mailSenderName = System.Configuration.ConfigurationManager.AppSettings["mailSender"];
                string mailAccount = System.Configuration.ConfigurationManager.AppSettings["mailAccount"];
                string mailPass = System.Configuration.ConfigurationManager.AppSettings["mailPassword"];

                string sql = "select distinct sch.Createuser,sch.qyg,sch.mdg from sqm_cost_hy sch left join sqm_modedj_val smv on sch.rid=smv.costrid where smv.costrid='{0}'";
                sql = string.Format(sql, keyValue);
                DataTable dataTable = DataHelper.QueryDataTable(sql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        CreateUser = row["CREATEUSER"].ToString();
                        string crmsql = string.Format("select Email from sysuser where workno ='{0}'", CreateUser);
                        IDbConnection conn = new OracleConnection();
                        conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        var EamilAddress = DataHelper.QueryValue(crmsql, conn);
                        
                        HYGJmessage = SQM_COST_HYController.ReturnMessage(keyValue, "hy");
                        body =  HYGJmessage + body;
                        System.Net.Mail.SmtpClient client = new SmtpClient();
                        client.Host = mailServer;//163的smtp服务器是 smtp.163.com   

                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(mailAccount, mailPass);

                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        string senderDisplayName = mailSenderName;//这个配置的是发件人的要显示在邮件的名称
                        if (EamilAddress == null || EamilAddress.ToString() == "")
                        {
                            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = "保存成功！没有邮箱地址,无法发送提醒。" }.ToString());
                        }

                        MailAddress mailfrom = new MailAddress(mailAccount, senderDisplayName, encoding);//发件人邮箱地址，名称，编码UTF8

                        MailAddress mailto = new MailAddress(EamilAddress.ToString());//收件人邮箱地址，名称，编码UTF8   
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
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 得到原始数据，用来检测表中是否已存在即将插入的数据   
        /// </summary>
        /// <param name="srcobj">要插入表的数据</param>
        /// <param name="fields">主键code</param>
        /// <returns></returns>
        [AllowAnonymous]
        public DataTable FindSourceData(SQM_COST_HY srcobj, string[] fields)
        {
            string sql1 = "select RID,STARTDATE,ENDDATE from SQM_COST_HY where ";
            for (int i = 0; i < fields.Length; i++)
            {
                if (i < fields.Length - 1)
                {
                    if (srcobj.GetValue(fields[i]) is string) // 字符串类型
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "") // 是否为空
                        {
                            sql1 += fields[i] + " = '" + srcobj.GetValue(fields[i]) + "' and ";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and ";
                        }
                    }
                    else if (srcobj.GetValue(fields[i]) is DateTime) // 日期类型
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "")
                        {
                            sql1 += fields[i] + " = to_date('" + srcobj.GetValue(fields[i]) + "','yyyy/mm/dd hh24:mi:ss') and ";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and ";
                        }
                    }
                    else
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "")
                        {
                            sql1 += fields[i] + " = " + srcobj.GetValue(fields[i]) + " and ";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and ";
                        }
                    }
                }
                else
                {
                    if (srcobj.GetValue(fields[i]) is string) // 字符串类型
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "")
                        {
                            sql1 += fields[i] + " = '" + srcobj.GetValue(fields[i]) + "' and STATUS = '1' order by STARTDATE";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and STATUS = '1' order by STARTDATE";
                        }
                    }
                    else if (srcobj.GetValue(fields[i]) is DateTime) // 日期类型
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "")
                        {
                            sql1 += fields[i] + " = to_date('" + srcobj.GetValue(fields[i]) + "','yyyy/mm/dd hh24:mi:ss') and STATUS = '1' order by STARTDATE";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and ";
                        }
                    }
                    else
                    {
                        if (srcobj.GetValue(fields[i]) + "" != "")
                        {
                            sql1 += fields[i] + " = " + srcobj.GetValue(fields[i]) + " and STATUS = '1' order by STARTDATE";
                        }
                        else
                        {
                            sql1 += fields[i] + " is null and STATUS = '1' order by STARTDATE";
                        }
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
        public void HandleValidDate(DateTime startDate, DateTime endDate, DateTime startDate_old, DateTime endDate_old, SQM_COST_HY targetobj, SQM_COST_HY srcobj, DataRow dr)
        {
            SQM_COST_HY newobj = new SQM_COST_HY();
            // 主键字段
            string[] PKs = { "AREACODE", "QYGCODE", "MDGCODE", "BZCODE", "CGSCODE", "HC", "ZZGCODE", "MTCODE", "KHR", "STARTDATE", "ENDDATE" };
            // 以下代码为原始数据有效期 取头去尾
            if (endDate < startDate_old) // 最左 原始数据不失效
            {
                // 数据新增 判断 库里 是否已经存在相同数据，存在则不新增
                DataTable dt = FindSourceData(srcobj, PKs);
                if (dt.Rows.Count == 0)
                {
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoCreate();
                }
            }
            else if (startDate > endDate_old) // 最右 原始数据不失效
            {
                // 数据新增 判断 库里 是否已经存在相同数据，存在则不新增
                DataTable dt = FindSourceData(srcobj, PKs);
                if (dt.Rows.Count == 0)
                {
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoCreate();
                }
            }
            else if (startDate <= startDate_old && endDate > startDate_old) // 全覆盖 
            {
                // 原始数据失效
                targetobj = SQM_COST_HY.TryFind(dr["RID"]);
                targetobj.STATUS = "0";
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoSave();

                // 数据新增 判断 库里 是否已经存在相同数据，存在则不新增
                DataTable dt = FindSourceData(srcobj, PKs);
                if (dt.Rows.Count == 0)
                {
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoCreate();
                }

            }
            else if (startDate > startDate_old && startDate <= endDate_old) // 部分覆盖 
            {
                targetobj = SQM_COST_HY.TryFind(dr["RID"]);
                // 原始数据复制
                DataHelper.MergeData<SQM_COST_HY>(newobj, targetobj);
                // 原始数据失效
                targetobj.STATUS = "0";
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoSave();

                // 数据新增
                targetobj = newobj;
                targetobj.STARTDATE = startDate_old;
                targetobj.ENDDATE = startDate.AddDays(-1);
                targetobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                targetobj.DoCreate();

                // 数据新增 判断 库里 是否已经存在相同数据，存在则不新增
                DataTable dt = FindSourceData(srcobj, PKs);
                if (dt.Rows.Count == 0)
                {
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoCreate();
                }
            }
        }
        [AllowAnonymous]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SQM_COST_HY.TryFind(keyValue);
            DataTable data2 = null;
            if (data != null)
            {
                //根据工号得到用户名 从crm里面取值
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string namesql = "select loginname as workno,name from sysuser where loginname='" + data.CREATEUSER + "' or loginname='" + data.MODIFYUSER + "'";
                ///whereStr += " loginname='" + data.Rows[0]["CREATEUSER"] + "' or loginname='" + data.Rows[0]["MODIFYUSER"] + "'";
                data2 = DataHelper.QueryDataTable(namesql, conn);
            }
            return Content(JsonHelper.GetJsonString(new { data, data2 }));
            //return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// Excel模板数据导入
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostExcelData()
        {
            string info = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    //获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    System.IO.Stream stream = file.InputStream;
                    ArrayList al = new ArrayList();
                    al = GetDataFromExcel(stream);
                    if (al.Count > 1)// 初步校验结果
                    {
                        ds = (DataSet)al[0];
                        // 获取sheet名称
                        string sheetName = ds.Tables[int.Parse(al[2].ToString())].TableName;
                        // 获取校验结果
                        string result = al[1].ToString();
                        if (result == "列数超限")
                        {
                            return Content(new JsonMessage { Code = "1", Message = "导入失败：sheet表\"" + sheetName + "\" 列数不符合模板规则" }.ToString());
                        }
                        else if (result == "数据为空")
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
                        ds = (DataSet)al[0];
                        DataSet dsnew = new DataSet();
                        // 主数据校验 国家、起运港、目的港、中转港、船公司、码头
                        string existStr = String.Empty;
                        foreach (DataTable dt in ds.Tables)
                        {
                            dt.Columns.Add("RID");
                            dt.Columns.Add("CREATETIME");
                            dt.Columns.Add("CREATEID");
                            dt.Columns.Add("CREATEUSER");
                            dt.Columns.Add("MODIFYTIME");
                            dt.Columns.Add("MODIFYID");
                            dt.Columns.Add("MODIFYUSER");
                            dt.Columns.Add("STATUS");
                            dt.Columns.Add("BZ");
                            dt.Columns.Add("DESCR");
                            dt.Columns.Add("EXT1");
                            dt.Columns.Add("EXT2");
                            dt.Columns.Add("EXT3");
                            dt.Columns.Add("AREACODE");
                            dt.Columns.Add("QYGCODE");
                            dt.Columns.Add("MDGCODE");
                            dt.Columns.Add("CGSCODE");
                            dt.Columns.Add("ZZGCODE");
                            dt.Columns.Add("MTCODE");
                            dt.Columns.Add("BZCODE");
                            foreach (DataRow dr in dt.Rows)
                            {
                                string area = dr["AREA"].ToString().Replace(")", "");
                                string qyg = dr["QYG"].ToString().Replace(")", "");
                                string mdg = dr["MDG"].ToString().Replace(")", "");
                                string zzg = dr["ZZG"].ToString().Replace(")", "");
                                string cgs = dr["CGS"].ToString().Replace(")", "");
                                string mt = dr["MT"].ToString().Replace(")", "");

                                // 所有出现的中文、英文、代码都进行校验，如果得到code则通过，如果得不到则不通过
                                //string areaCode = GetCode(area, "1"); ;
                                string areaCode = GetCode(area, "7"); ;

                                string qygCode = GetCode(qyg, "2");

                                string mdgCode = GetCode(mdg, "2");

                                string zzgCode = GetCode(zzg, "2");

                                string cgsCode = GetCode(cgs, "4");

                                string mtCode = GetCode(mt, "5");

                                // 1：国家   2：港口   5：码头   4：船公司
                                area = areaCode == "" ? "国家:" + area + "," : "";
                                qyg = qygCode == "" ? "起运港:" + qyg + "," : "";
                                mdg = mdgCode == "" ? "目的港:" + mdg + "," : "";
                                zzg = zzgCode == "" ? "中转港:" + zzg + "," : "";
                                cgs = cgsCode == "" ? "船公司:" + cgs + "," : "";
                                mt = mtCode == "" ? "码头:" + mt + "," : "";
                                existStr = area + qyg + mdg + zzg + cgs + mt;
                                if (existStr.IndexOf(":") >= 0)
                                {
                                    string fieldName = existStr.Split(',')[0].Split(':')[0];
                                    string fieldValue = existStr.Split(',')[0].Split(':')[1];
                                    return Content(new JsonMessage { Message = "导入失败：sheet表\"" + dt.TableName + "\" \"" + fieldName + "\":" + fieldValue + " 主数据中不存在", Code = "1" }.ToString());
                                }
                                else
                                {
                                    existStr = "";
                                    dr["AREACODE"] = areaCode;
                                    dr["QYGCODE"] = qygCode;
                                    dr["MDGCODE"] = mdgCode;
                                    dr["ZZGCODE"] = zzgCode;
                                    dr["CGSCODE"] = cgsCode;
                                    dr["MTCODE"] = mtCode;
                                }
                            }
                            dsnew.Tables.Add(dt.Copy());
                        }
                        foreach (DataTable dt in dsnew.Tables)
                        {
                            // 数据导入（实体方式导入数据，每导入一条创建一个对象，速度非常慢）
                            if (existStr == "")
                            {
                                // DataTable转实例对象
                                List<SQM_COST_HY> ents = TableToEntity<SQM_COST_HY>(dt);
                                SQM_COST_HY targetobj = new SQM_COST_HY();
                                foreach (SQM_COST_HY srcobj in ents)
                                {
                                    srcobj.BZ = "USD";
                                    srcobj.BZCODE = "USD";
                                    srcobj.STATUS = "1";
                                    // 获取新增有效期
                                    DateTime startDate = (DateTime)srcobj.STARTDATE;
                                    DateTime endDate = (DateTime)srcobj.ENDDATE;

                                    // 获取原始数据
                                    DataTable srcDt = FindSourceData(srcobj, primaryKeys);
                                    if (srcDt.Rows.Count > 0)
                                    {
                                        // 获取原始数据最小起始日期
                                        //DateTime startDate_old = (DateTime)srcDt.AsEnumerable().First()["STARTDATE"];
                                        //// 获取原始数据最大截止日期
                                        //DateTime endDate_old = (DateTime)srcDt.AsEnumerable().Last()["ENDDATE"];
                                        // 处理有效期
                                        foreach (DataRow dr in srcDt.Rows)
                                        {
                                            DateTime startDate_old = (DateTime)dr["STARTDATE"];
                                            DateTime endDate_old = (DateTime)dr["ENDDATE"];
                                            HandleValidDate(startDate, endDate, startDate_old, endDate_old, targetobj, srcobj, dr);
                                        }
                                    }
                                    else
                                    {
                                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                        srcobj.DoSave();
                                    }
                                }
                            }
                        }
                        return Content(new JsonMessage { Message = "导入成功", Code = "0" }.ToString());
                    }
                }
                else
                {
                    return Content(new JsonMessage { Message = "导入失败", Code = "1" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = "导入异常" + ex.Message, Code = "2" }.ToString());
            }
        }
        public string GetCode(string strs, string type)
        {
            string code = "";
            string[] arr = strs.Split('(');
            foreach (string str in arr)
            {
                code = MainDataExist(str, type);
                if (code != "")
                {
                    break;
                }
            }
            return code;
        }
        // 属性名称
        string[] filedName = { "AREA", "QYG", "MDG", "CGS", "GP20", "GP40", "HQ40", "DL20", "DL40", "HC", "ZZG", "MT", "KHR", "STARTDATE", "ENDDATE", "MEMO" };
        /// <summary>
        /// 获取excel全部数据
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>DataSet</returns>
        private ArrayList GetDataFromExcel(System.IO.Stream stream)
        {
            ArrayList al = new ArrayList();
            Cells cells;
            string[] khrArr = new string[7]; // 存储开航日
            string[] khr = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
            Workbook workbook = new Workbook(stream);
            DataSet excel_ds = new DataSet("myDS"); //创建数据集
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                cells = workbook.Worksheets[i].Cells;
                DataTable dtnew = new DataTable(workbook.Worksheets[i].Name); //创建数据表
                // 判断最大数据列数是否符合
                if (cells.MaxDataColumn > 15)
                {
                    excel_ds.Tables.Add(dtnew);
                    al.Add(excel_ds);
                    al.Add("列数超限");
                    al.Add(i);
                    return al;
                }
                else
                {
                    for (int f = 0; f < filedName.Length; f++)
                    {
                        dtnew.Columns.Add(new DataColumn(filedName[f], typeof(string)));
                    }
                }
                excel_ds.Tables.Add(dtnew); // 把数据表添加到数据集中
                DataRow dr;
                string oldCol1 = String.Empty;
                string oldCol2 = String.Empty;
                string oldCol3 = String.Empty;
                string rownum = String.Empty;
                string colnum = String.Empty;
                // 判断数据是否为空
                if (cells.MaxDataRow + 1 == 8)
                {
                    al.Add(excel_ds);
                    al.Add("数据为空");
                    al.Add(i);
                    return al;
                }
                // k值为第k+1行开始读取Excel
                for (int k = 8; k < cells.MaxDataRow + 1; k++)
                {
                    dr = dtnew.NewRow();
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        // 记录位置
                        rownum = (k + 1) + "";
                        colnum = (j + 1) + "";
                        string cellStr = cells[k, j].StringValue.Trim();
                        // 前三列单行拆多行
                        switch (j)
                        {
                            case 0:
                                if (cellStr == "")
                                {
                                    cellStr = oldCol1;
                                }
                                else
                                {
                                    //cellStr = cellStr.ToUpper();
                                    oldCol1 = cellStr;
                                }
                                break;
                            case 1:
                                if (cellStr == "")
                                {
                                    cellStr = oldCol2;
                                }
                                else
                                {
                                    oldCol2 = cellStr;
                                }
                                break;
                            case 2:
                                if (cellStr == "")
                                {
                                    cellStr = oldCol3;
                                }
                                else
                                {
                                    oldCol3 = cellStr;
                                }
                                break;
                        }
                        // 判断必填项是否为空
                        if (j == 3 || j == 4 || j == 5 || j == 6 || j == 9 || j == 10 || j == 11 || j == 12 || j == 13 || j == 14)
                        {
                            if (cellStr == "")
                            {
                                string location = rownum + "," + colnum + ",必填项为空";
                                al.Add(excel_ds);
                                al.Add(location);
                                al.Add(i);
                                return al;
                            }
                            else
                            {
                                if (j == 12)
                                {
                                    string[] khrs = cellStr.Split('/');
                                    for (int x = 0; x < khrs.Count(); x++)
                                    {
                                        int count = 0;
                                        for (int kArr = 0; kArr < khr.Count(); kArr++)
                                        {
                                            if (khrs[x] == khr[kArr])
                                            {
                                                count++;
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            string location = rownum + "," + colnum + ",存在不规则的开航日";
                                            al.Add(excel_ds);
                                            al.Add(location);
                                            al.Add(i);
                                            return al;
                                        }
                                        else
                                        {
                                            dr[j] = cellStr;
                                        }
                                    }
                                }
                                else if (j == 13)
                                {
                                    string startDate = cellStr.Replace(":", "/").Replace("-", "/").Replace("：", "/");
                                    // 判断起始日期是否小于当前日期
                                    if (Convert.ToDateTime(startDate) < DateTime.Now.AddDays(-1))
                                    {
                                        string location = rownum + "," + colnum + ",起始日期小于当前日期";
                                        al.Add(excel_ds);
                                        al.Add(location);
                                        al.Add(i);
                                        return al;
                                    }
                                    else
                                    {
                                        dr[j] = startDate;
                                    }
                                }
                                else if (j == 14)
                                {
                                    string startDate = dr[13].ToString().Replace(":", "/").Replace("-", "/").Replace("：", "/");
                                    string endDate = cellStr.Replace(":", "/").Replace("-", "/").Replace("：", "/");
                                    // 判断截止日期是否小于或等于起始日期
                                    if (Convert.ToDateTime(startDate) >= Convert.ToDateTime(endDate))
                                    {
                                        string location = rownum + "," + colnum + ",截止日期小于或等于起始日期";
                                        al.Add(excel_ds);
                                        al.Add(location);
                                        al.Add(i);
                                        return al;
                                    }
                                    else
                                    {
                                        dr[j] = endDate;
                                    }
                                }
                                else if (j == 4 || j == 5 || j == 6 || j == 9) // 判断decimal类型是否符合
                                {
                                    string numbers = cellStr.Replace(",", "").Replace("，", "").Replace("`", "");
                                    bool isTrue = CheckDecimal(numbers);
                                    if (isTrue)
                                    {
                                        dr[j] = numbers;
                                    }
                                    else
                                    {
                                        string location = rownum + "," + colnum + ",数字格式不符";
                                        al.Add(excel_ds);
                                        al.Add(location);
                                        al.Add(i);
                                        return al;
                                    }
                                }
                                else
                                {
                                    dr[j] = cellStr;
                                }
                            }
                        }
                        else if (j == 7 || j == 8)
                        {
                            string numbers = cellStr.Replace(",", "");
                            bool isTrue = CheckDecimal(numbers);
                            if (isTrue)
                            {
                                dr[j] = numbers;
                            }
                            else
                            {
                                string location = rownum + "," + colnum + ",数字格式不符";
                                al.Add(excel_ds);
                                al.Add(location);
                                al.Add(i);
                                return al;
                            }
                        }
                        else
                        {
                            dr[j] = cellStr;
                        }
                    }
                    // 开航日拆分
                    if (dr[12].ToString().IndexOf('/') >= 0)
                    {
                        khrArr = (string[])dr[12].ToString().Split('/').Clone();
                        for (int m = 0; m < khrArr.Length; m++)
                        {
                            DataRow drArr = dtnew.NewRow();
                            drArr.ItemArray = dr.ItemArray;
                            drArr[12] = khrArr[m];
                            excel_ds.Tables[workbook.Worksheets[i].Name].Rows.Add(drArr);
                        }
                    }
                    else
                    {
                        excel_ds.Tables[workbook.Worksheets[i].Name].Rows.Add(dr);
                    }
                }
            }
            al.Add(excel_ds);
            return al;
        }
        /// <summary>
        /// 导出Excel 
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExc()
        {
            string filePath = "";
            string rowIdStr = Request["rowIds"];
            string[] rowIdArr = JsonHelper.GetObject<string[]>(rowIdStr);
            string whereStr = "";
            for (int i = 0; i < rowIdArr.Length; i++)
            {
                whereStr += "'" + rowIdArr[i] + "',";
            }

            try
            {
                whereStr = whereStr.TrimEnd(',');
                DataTable dt = DataHelper.QueryDataTable(string.Format("select AREA,QYG,MDG,CGS,GP20,GP40,HQ40,DL20,DL40,HC,ZZG,MT,KHR,to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,MEMO from SQM_COST_HY where RID in({0})", whereStr));
                dt.TableName = "HY";
                // 导出Excel
                //filePath = GenerateAttachment(dt);
                filePath = DataTableToExcel(dt);
                if (filePath.IndexOf("海运成本导出") < 0)
                {
                    return Content(new JsonMessage { Message = filePath, Success = false }.ToString());
                }
                else
                {
                    return Content(new JsonMessage { Message = filePath, Success = true }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Message = ex.Message, Success = false }.ToString());
            }
        }
        public string DataTableToExcel(DataTable dt)
        {
            string filePath = "";
            // 创建一个workbookdesigner对象
            WorkbookDesigner designer = new WorkbookDesigner();
            // 加载Excel模板
            string strTempPath = Server.MapPath("/Excel/Templete/hycostexport.xlsx");
            designer.Open(strTempPath);
            designer.SetDataSource(dt);
            // 根据数据源处理生成报表内容
            designer.Process();
            // 保存(生成)Excel
            string timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Server.MapPath("/Excel/cost_hy_output/海运成本导出" + timeStr + ".xlsx");
            var fullPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            designer.Save(path, FileFormatType.Xlsx);
            // 打开Excel文件
            //System.Diagnostics.Process.Start(path);
            // 合并Excel
            //Workbook sourcebook1 = new Workbook();
            //sourcebook1.Open(Server.MapPath("/Excel/Templete/hycostexport.xlsx"));
            //Workbook sourcebook2 = new Workbook();
            //sourcebook2.Open(Server.MapPath("/Excel/cost_hy_output/海运成本导出" + timeStr + ".xlsx"));
            //sourcebook1.Combine(sourcebook2);
            //string pathhb = Server.MapPath("/Excel/cost_hy_output/海运成本导出合并" + timeStr + ".xlsx");
            //sourcebook1.Save(pathhb);
            //return "/Excel/cost_hy_output/海运成本导出合并" + timeStr + ".xlsx";
            filePath = "/Excel/cost_hy_output/海运成本导出" + timeStr + ".xlsx";
            return filePath;
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
                            if (row[p.Name] is Int64)
                            {
                                p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                                continue;
                            }
                            if (p.PropertyType.FullName.ToString().IndexOf("String") >= 0)
                            {
                                p.SetValue(entity, row[p.Name].ToString() == "" ? null : row[p.Name], null);
                            }
                            if (p.PropertyType.FullName.ToString().IndexOf("DateTime") >= 0)
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
                            if (p.PropertyType.FullName.ToString().IndexOf("Decimal") >= 0)
                            {
                                if (!string.IsNullOrEmpty(row[p.Name].ToString()))
                                {
                                    p.SetValue(entity, Convert.ToDecimal(row[p.Name]), null);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                list.Add(entity);
            }
            return list;
        }
        /// <summary>
        /// 主数据校验
        /// </summary>
        /// <param name="value">校验字段值</param>
        /// <param name="type">校验字段类型：国家、港口、码头</param>
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
                string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + gjdm + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = 'E'";
                string sql = string.Format("SELECT distinct {4} FROM MDM_MIAN_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {4} = '{2}') AND {3}", gjdm, columnName, value, langucolumns, columnCode);
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            else if (type == "2")
            {
                string sql = "select distinct locno from MDM_LOC where DESCR40 like '%" + value.ToLower() + "%' or DESCR40 like '%" + value.ToUpper() + "%' or DESCR40 like '%" + value + "%' or LOCNO like '%" + value.ToUpper() + "%' or LOCNO like '%" + value.ToLower() + "%' or LOCNO like '%" + value + "%' and LOCTYPE = '1100'";
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            else if (type == "4")// 船公司
            {
                string sql = "select distinct BPKEY from MDM_BP where BPNAME like '%" + value.ToLower() + "%' or BPNAME like '%" + value.ToUpper() + "%' or BPNAME like '%" + value + "%' or BPKEY like '%" + value.ToUpper() + "%' or BPKEY like '%" + value.ToLower() + "%' or BPKEY like '%" + value + "%'";
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            else if (type == "5")// 码头
            {
                code = "mt";
            }
            else if (type == "6")
            {
                //code = true;
            }
            //国家/地区
            else if (type == "7")
            {
                string gjdm = "DESTLOC_CNTRY";
                string columnName = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_CALC_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LANDX'").ToString();
                string columnCode = "COLUMN" + DataHelper.QueryValue("select POSITION from MDM_CALC_STRC where mdkey = '" + gjdm + "' AND FIELDNAME = 'LAND1'").ToString();
                // 语言 '1'：中文  'E'：英文 现要求中文
                string langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_CALC_STRC where mdkey = '" + gjdm + "' and fieldname in ( SELECT distinct fieldname FROM MDM_CALC_STRC where ddtext = '语言代码' ) ").ToString() + " = '1'";
                string sql = string.Format("SELECT distinct {4} FROM MDM_CALC_VALUE WHERE mdkey = '{0}' AND ({1} = '{2}' OR {4} = '{2}') AND {3}", gjdm, columnName, value, langucolumns, columnCode);
                if (!string.IsNullOrEmpty((string)DataHelper.QueryValue(sql)))
                {
                    code = DataHelper.QueryValue(sql).ToString();
                }
            }
            return code;
        }
        /// <summary>
        /// 判断数字字段是否含有非十进制字符
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckDecimal(string number)
        {
            string ifSigned = @"^-{1}"; // 是否是负数
            string ifNotDec = @"\D"; // 是否存在非十进制字符
            if (Regex.IsMatch(number, ifSigned))
            {
                number = number.Replace("-", "");
                if (number.IndexOf(".") >= 0)
                {
                    number = number.Replace(".", "");
                    if (Regex.IsMatch(number, ifNotDec))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (Regex.IsMatch(number, ifNotDec))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (number.IndexOf(".") >= 0)
                {
                    number = number.Replace(".", "");
                    if (Regex.IsMatch(number, ifNotDec))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (Regex.IsMatch(number, ifNotDec))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        /// <summary>
        /// 加载Excel模板并写入数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public string GenerateAttachment(DataTable dt)
        //{
        //    //需要添加 Microsoft.Office.Interop.Excel引用 
        //    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        //    if (app == null)//服务器上缺少Excel组件，需要安装Office软件
        //    {
        //        return "服务器上缺少Excel组件";
        //    }
        //    app.Visible = false;
        //    app.UserControl = true;
        //    string strTempPath = Server.MapPath("/Excel/Templete/hycostexport.xlsx");
        //    //string strTempPath = app.StartupPath + "\\Excel\\Templete\\hycostexport.xlsx";
        //    Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        //    Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Add(strTempPath); //加载模板
        //    Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1); // 第一个工作薄。
        //    if (worksheet == null)// 工作薄中没有工作表
        //    {
        //        return "工作薄中没有工作表";
        //    }

        //    //1、获取数据
        //    int rowCount = dt.Rows.Count;
        //    if (rowCount < 1)//没有取到数据
        //    {
        //        return "没有取到数据";
        //    }

        //    // 2、写入数据，Excel索引从9开始  
        //    // px microsoft.office.interop.excel 与npoi和aspose不一样，cells 的索引是从 1 开始的！！！
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        int row_ = 9 + i;  //Excel模板上表头占了8行
        //        worksheet.Cells[row_, 1] = dt.Rows[i]["AREA"].ToString();
        //        worksheet.Cells[row_, 2] = dt.Rows[i]["QYG"].ToString();
        //        worksheet.Cells[row_, 3] = dt.Rows[i]["MDG"].ToString();
        //        worksheet.Cells[row_, 4] = dt.Rows[i]["CGS"].ToString();
        //        worksheet.Cells[row_, 5] = dt.Rows[i]["GP20"].ToString();
        //        worksheet.Cells[row_, 6] = dt.Rows[i]["GP40"].ToString();
        //        worksheet.Cells[row_, 7] = dt.Rows[i]["HQ40"].ToString();
        //        worksheet.Cells[row_, 8] = dt.Rows[i]["DL20"].ToString();
        //        worksheet.Cells[row_, 9] = dt.Rows[i]["DL40"].ToString();
        //        worksheet.Cells[row_, 10] = dt.Rows[i]["HC"].ToString();
        //        worksheet.Cells[row_, 11] = dt.Rows[i]["ZZG"].ToString();
        //        worksheet.Cells[row_, 12] = dt.Rows[i]["MT"].ToString();
        //        worksheet.Cells[row_, 13] = dt.Rows[i]["KHR"].ToString();
        //        worksheet.Cells[row_, 14] = dt.Rows[i]["STARTDATE"].ToString();
        //        worksheet.Cells[row_, 15] = dt.Rows[i]["ENDDATE"].ToString();
        //        worksheet.Cells[row_, 16] = dt.Rows[i]["MEMO"].ToString();
        //    }
        //    //调整Excel的样式。
        //    Microsoft.Office.Interop.Excel.Range rg = worksheet.Range[worksheet.Cells[9, 1], worksheet.Cells[rowCount + 8, 16]];// ["A9",[0,0]]
        //    rg.Borders.LineStyle = 1; //单元格加边框
        //    //rg.Borders.Color = 0;
        //    //rg.Borders.Weight = 2;
        //    worksheet.Columns.AutoFit(); //自动调整列宽

        //    //隐藏某一行
        //    //选中部分单元格，把选中的单元格所在的行的Hidden属性设为true
        //    //worksheet.get_Range(app.Cells[2, 1], app.Cells[2, 32]).EntireRow.Hidden = true;

        //    //删除某一行
        //    //worksheet.get_Range(app.Cells[2, 1], app.Cells[2, 32]).EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);


        //    //3、保存生成的Excel文件
        //    //Missing在System.Reflection命名空间下
        //    string timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    //string savePath = app.StartupPath + "\\Excel\\cost_hy_output\\海运成本导出" + timeStr + ".xlsx";
        //    string savePath = Server.MapPath("/Excel/cost_hy_output/海运成本导出" + timeStr + ".xlsx");
        //    workbook.SaveAs(savePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //    //4、按顺序释放资源
        //    NAR(worksheet);
        //    NAR(sheets);
        //    NAR(workbook);
        //    NAR(workbooks);
        //    app.Quit();
        //    NAR(app);
        //    return "/Excel/cost_hy_output/海运成本导出" + timeStr + ".xlsx";
        //}
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="o"></param>
        public static void NAR(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.ToString());
            }
            finally
            {
                o = null;
            }
        }
        public string GetEn(string str)
        {
            string en = "";
            StringBuilder sb = new StringBuilder();
            foreach (char a in str)
            {
                if ((a >= 'a' && a < 'z') || (a > 'A' && a < 'Z'))
                {
                    sb.Append(a);
                }
            }
            en = sb.ToString();
            return en;
        }

        public string GetZh(string str)
        {
            string zh = "";
            return zh;
        }
    }
}

