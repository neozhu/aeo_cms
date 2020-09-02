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

using Com.Feiliks.QDM.Model;
using Oncontrol3.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;

namespace Com.Feiliks.QDM.Web
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
            return JsonConvert.SerializeObject(this, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }); ;
        }
    }
    [AllowAnonymous]
    public class SQM_ENQUIRY_HKYController : BaseController
    {
        [AllowAnonymous]
        public ActionResult QuiryHyIndex()
        {
            DataTable dtmyfs = DataHelper.QueryDataTable("select distinct column2 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'ZJFMYFS'");// 贸易方式
            ViewBag.MYFS = JsonHelper.GetJsonString(dtmyfs);
            DataTable dtpm = DataHelper.QueryDataTable("select distinct column3 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'YPM'");// 品名
            ViewBag.PM = JsonHelper.GetJsonString(dtpm);// 转成json格式传回前台  对象集合（数组）
            DataTable dtsblx = DataHelper.QueryDataTable("select distinct column4 as \"id\",'(' || column4 || ')' || column5 as \"text\" from mdm_calc_value where mdkey = 'EQUIP_TYPE'");// 设备类型 取code
            ViewBag.SBLX = JsonHelper.GetJsonString(dtsblx);// 转成json格式传回前台  对象集合（数组）
            DataTable Orgdt = DataHelper.QueryDataTable("select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 order by ltrim(OBJID,'0')");
            ViewBag.ORG = Orgdt;
            DataTable displayfee = DataHelper.QueryDataTable("select distinct code from SQM_HKYDIC where type in('1','0')");// 设备类型 取code
            ViewBag.DisplayFeeCode = JsonHelper.GetJsonString(displayfee);// 转成json格式传回前台  对象集合（数组）
            return View();
        }
        [AllowAnonymous]
        public ActionResult QuiryPURIndex()
        {
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult HyLists()
        {
            string createuser = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string[] searchKeys = new string[] { "MDG", "QYG" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_ENQUIRY_HKY).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            SearchCriterion.AddSearch("TYPE", "0", Aim.Data.SearchModeEnum.Equal);// 添加where条件 海运筛选
            SearchCriterion.AddSearch("CREATEUSER", createuser, Aim.Data.SearchModeEnum.Equal);// 登陆人筛选
            var total = ActiveRecordMediator.Count(typeof(SQM_ENQUIRY_HKY), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_ENQUIRY_HKY>());
            // 新增询价明细表，所以采用多表关联的方式进行数据查询
            DataTable dt = DataHelper.QueryDataTable("select t1.*,t2.DJZBRID as MXDJZBRID,t2.DJFSRID as MXDJFSRID,t2.GDZRID as MXGDZRID,t2.DJRID as MXDJRID from SQM_ENQUIRY_HKY t1 left join SQM_ENQUIRY_RESP t2 on t1.RID = t2.MainRID where t1.TYPE = '0' and t1.CREATEUSER = '" + createuser + "' order by t1.CREATETIME desc");
            //var obj = new { draw = Request["draw"], data = SQM_ENQUIRY_HKY.FindAll(SearchCriterion).OrderByDescending(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult KyLists()
        {
            string createuser = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string[] searchKeys = new string[] { "MDG", "QYG" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_ENQUIRY_HKY).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            SearchCriterion.AddSearch("TYPE", "1", Aim.Data.SearchModeEnum.Equal);
            SearchCriterion.AddSearch("CREATEUSER", createuser, Aim.Data.SearchModeEnum.Equal);
            var total = ActiveRecordMediator.Count(typeof(SQM_ENQUIRY_HKY), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_ENQUIRY_HKY>());
            // 新增询价明细表，所以采用多表关联的方式进行数据查询
            DataTable dt = DataHelper.QueryDataTable("select t1.*,t2.DJZBRID as MXDJZBRID,t2.DJFSRID as MXDJFSRID,t2.GDZRID as MXGDZRID,t2.DJRID as MXDJRID from SQM_ENQUIRY_HKY t1 left join SQM_ENQUIRY_RESP t2 on t1.RID = t2.MainRID where t1.TYPE = '1' and t1.CREATEUSER = '" + createuser + "' order by t1.CREATETIME desc");
            //var obj = new { draw = Request["draw"], data = SQM_ENQUIRY_HKY.FindAll(SearchCriterion).OrderByDescending(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        [AllowAnonymous]
        public ActionResult PURLists()
        {
            string[] searchKeys = new string[] { "MDG", "QYG","TYPE", "STATUS", "CREATEUSER" };
            string whereString = "";
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    whereString += " and t1." + key + " like '%" + Request[key] + "%' ";
                    Type valueType = typeof(SQM_ENQUIRY_HKY).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                    {
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                    }
                }
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_ENQUIRY_HKY), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_ENQUIRY_HKY>());
            // 新增询价明细表，所以采用多表关联的方式进行数据查询
            DataTable dt = DataHelper.QueryDataTable(string.Format("select t1.*,t2.DJZBRID as MXDJZBRID,t2.DJFSRID as MXDJFSRID,t2.GDZRID as MXGDZRID,t2.DJRID as MXDJRID from SQM_ENQUIRY_HKY t1 left join SQM_ENQUIRY_RESP t2 on t1.RID = t2.MainRID where 1=1 {0} order by t1.CREATETIME desc", whereString));
            //var obj = new { draw = Request["draw"], data = SQM_ENQUIRY_HKY.FindAll(SearchCriterion).OrderByDescending(en => en.CREATETIME), recordsTotal = total, recordsFiltered = total };
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = total, recordsFiltered = total };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 海运新增/修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult HyEdit(string code)
        {
            var arr = code.Split(',');
            ViewBag.feecode = arr[2];
            ViewBag.srvcode = arr[1];
            ViewBag.prdcode = arr[0];
            ViewBag.orgcode = arr[3];
            DataTable dtpm = DataHelper.QueryDataTable("select distinct column3 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'YPM'");// 品名
            ViewBag.PM = dtpm;
            DataTable dtsblx = DataHelper.QueryDataTable("select distinct column4 as \"id\",'(' || column4 || ')' || column5 as \"text\" from mdm_calc_value where mdkey = 'EQUIP_TYPE'");// 设备类型 取code
            ViewBag.SBLX = JsonHelper.GetJsonString(dtsblx);
            return View("QuiryHyCreate");
        }
        /// <summary>
        /// 空运新增/修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult KyEdit(string code)
        {
            var arr = code.Split(',');
            ViewBag.feecode = arr[2];
            ViewBag.srvcode = arr[1];
            ViewBag.prdcode = arr[0];
            ViewBag.orgcode = arr[3];
            DataTable dtmyfs = DataHelper.QueryDataTable("select distinct column2 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'ZJFMYFS'");// 贸易方式
            ViewBag.MYFS = dtmyfs;
            DataTable dtpm = DataHelper.QueryDataTable("select distinct column3 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'YPM'");// 品名
            ViewBag.PM = dtpm;
            return View("QuiryKyCreate");
        }
        /// <summary>
        /// 查看明细访问该action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QuiryHyCreate()
        {
            DataTable dtpm = DataHelper.QueryDataTable("select distinct column3 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'YPM'");// 品名
            ViewBag.PM = dtpm;
            DataTable dtsblx = DataHelper.QueryDataTable("select distinct column4 as \"id\",'(' || column4 || ')' || column5 as \"text\" from mdm_calc_value where mdkey = 'EQUIP_TYPE'");// 设备类型 取code
            ViewBag.SBLX = JsonHelper.GetJsonString(dtsblx);
            return View("QuiryHyCreate");
        }
        /// <summary>
        /// 插看明细访问该action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QuiryKyCreate()
        {
            DataTable dtmyfs = DataHelper.QueryDataTable("select distinct column2 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'ZJFMYFS'");// 贸易方式
            ViewBag.MYFS = dtmyfs;
            DataTable dtpm = DataHelper.QueryDataTable("select distinct column3 as CODE,column4 as NAME from mdm_calc_value where mdkey = 'YPM'");// 品名
            ViewBag.PM = dtpm;
            return View("QuiryKyCreate");
        }
        [AllowAnonymous]
        public ActionResult SubmitForm(string postdata)
        {
            bool rtnflag = true;
            string rtnmsg = "询价提交成功";
            SQM_ENQUIRY_HKY srcobj = null;
            try
            {
                srcobj = JsonHelper.GetObject<SQM_ENQUIRY_HKY>(postdata);
                srcobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                srcobj.STATUS = "1"; //设置状态为未答复
                string feecode = srcobj.FEECODE + "";
                if(feecode != "")
                {
                    string feename = DataHelper.QueryValue("select textdesc from v_mdm_fee where tcet084 = '" + feecode + "'") + "";
                    srcobj.FEENAME = feename;
                }
                string srvcode = srcobj.SRVCODE + "";
                if (srvcode != "")
                {
                    string srvname = DataHelper.QueryValue("select servicename from mdm_service where servicetype = '" + srvcode + "'") + "";
                    srcobj.SRVNAME = srvname;
                }
                string prdcode = srcobj.PRDCODE + "";
                if (prdcode != "")
                {
                    string prdname = DataHelper.QueryValue("select sqproductname from SQM_PRD_EXT where productkey = '" + prdcode + "'") + "";
                    srcobj.PRDNAME = prdname;
                }
                // 流水号
                srcobj.SERIALNUMBER = "A" + DateTime.Now.ToString("yyMMdd") + DataHelper.QueryValue("select seq_enquiry_number.NEXTVAL from dual");
                srcobj.DoSave();
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 查看明细
        /// </summary>
        /// <param name="rid">表主键</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string rid)
        {
            var data = SQM_ENQUIRY_HKY.TryFind(rid);
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        ///  产品/服务/费目
        /// </summary>
        /// <param name="sybcode"></param>
        /// <returns></returns>
        public ActionResult GetAllByRid(string sybcode)
        {
            string businesstype = "";
            if (sybcode == "01")
            {
                businesstype = "空运";
            }
            else if (sybcode == "02")
            {
                businesstype = "海运";
            }

            string sql = string.Format("SELECT  DISTINCT PRODUCTKEY,SQPRODUCTNAME FROM SQM_PRD_EXT WHERE BUSINESSORG = '{0}'", businesstype);
            var prdArray = DataHelper.QueryObjectsList(sql);
            string prdcodeStr = "";

            foreach (object[] item in prdArray)
            {
                prdcodeStr += "'" + item[0] + "',";
            }

            string sql2 = string.Format("SELECT t1.PRODUCTCODE,t1.SERVICETYPECODE,t2.SERVICENAME FROM MDM_PRD_SRV_REF t1 LEFT JOIN MDM_SERVICE t2 ON t1.SERVICETYPECODE = t2.SERVICETYPE WHERE t1.PRODUCTCODE IN ({0})", prdcodeStr.TrimEnd(','));
            var srvArray = DataHelper.QueryObjectsList(sql2);
            string feeStr = "";
            for (var i = 0; i < srvArray.Count; i++)
            {
                feeStr += "'" + srvArray[i][1] + "',";
            }
            feeStr = feeStr.TrimEnd(',');
            string sql3 = string.Format(@"select t1.productcode,
       t3.sqproductname,
       t1.servicetypecode,
       t4.servicename, 
       t2.tcet084,
       t5.textdesc,
       t6.bxbj,
       t7.type
from mdm_prd_srv_ref t1
left join mdm_srv_fee_ref t2 on t2.srvrqcd121 = t1.servicetypecode 
left join sqm_prd_ext t3 on t1.productcode = t3.productkey 
left join mdm_service t4 on t1.servicetypecode = t4.servicetype 
left join v_mdm_fee t5 on t2.tcet084 = t5.tcet084 
left join qdm_fee_srv_ref t6 on t6.rid = t2.rid 
left join sqm_hkydic t7 on t7.code = t5.tcet084
where t1.productcode in ({0}) 
order by t6.SORID", prdcodeStr.TrimEnd(','));
            var feeArray = DataHelper.QueryObjectsList(sql3);
            object[] data = { prdArray, srvArray, feeArray };
            return Content(JsonHelper.GetJsonString(data));
        }

        /// <summary>
        ///  询价答复
        /// </summary>
        ///
        [AllowAnonymous]
        public ActionResult ResponseQuery(SQM_ENQUIRY_HKY seh)
        {
            seh.STATUS = "2";  //已回复
            seh.SPONSEDATE = DateTime.Now; //当前回复时间
            string[] djzbrids = (Request["DJZBRIDS"] + "").Split(',');// 多个rid
            //string djzbrid = seh.DJZBRID;
            // 查询DJFSRID/GDZRID/DJRID
            string djfsrid = "";
            string gdzrid = "";
            string djrid = "";
            DataTable dt = DataHelper.QueryDataTable("select djfsrid,gdzrid,feecalcid from sqm_modedj_val where rid = '" + djzbrids[0] + "'");// 虽然多行数据，但是只有一个高低值或者定价方式
            if(dt.Rows.Count > 0)
            {
                djfsrid = dt.Rows[0]["DJFSRID"] + "";
                gdzrid = dt.Rows[0]["GDZRID"] + "";
                djrid = dt.Rows[0]["FEECALCID"] + "";
            }
            // 更新询价主表
            string sql = "update sqm_enquiry_hky set STATUS='{0}',SPONSEDATE=TO_DATE('{1}','yyyy-mm-dd hh24:mi:ss') where RID='{2}'";
            string updateStr = String.Format(sql, seh.STATUS, seh.SPONSEDATE, seh.RID);
            DataHelper.ExecSql(updateStr);
            // 新增询价明细表数据
            string sql_insert = "";
            for(int i = 0; i < djzbrids.Length; i++)
            {
                string guid = System.Guid.NewGuid().ToString();
                sql_insert += string.Format("insert into SQM_ENQUIRY_RESP(RID,MAINRID,DJZBRID,DJFSRID,GDZRID,DJRID) values('{0}','{1}','{2}','{3}','{4}','{5}');", guid, seh.RID, djzbrids[i], djfsrid, gdzrid, djrid);
            }
            sql_insert = "begin " + sql_insert + " end;";
            DataHelper.ExecSql(sql_insert);

            var result= DataHelper.ExecSql(String.Format("select count(*) from sqm_enquiry_hky where RID='{0}' and status='2'",seh.RID));
            return Content(result.ToString());
        }

        /// <summary>
        /// 获取未答复的数量
        /// </summary>
        /// <returns></returns>
        public ActionResult getQueryNum()
        {
            //string queryNum ="";
            string sql = "select count(status) as queryNum from sqm_enquiry_hky where status='1'";
            var queryNum = DataHelper.QueryDataTable(sql);
            return Content(JsonHelper.GetJsonString( queryNum));
        }
        /// <summary>
        /// 空运主页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QuiryKyIndex()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            return View();
        }
    }
}

