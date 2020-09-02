using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.QDM;
using Com.Feiliks.MDM;
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
using Com.Feiliks.QDM.Model;
using System.Collections;
using System.IO;
using System.Web.Security;
using System.Data.OracleClient;
using Oncontrol3.Web.RATE601;
using System.Net.Mail;
using System.Text.RegularExpressions;
using OA_WS.OAWorkflowServiceXml;
using Oncontrol3.Web.FWA701;
using System.Reflection;
using Oncontrol3.Web.FWA702;
using Oncontrol3.Web.FWA703;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class QM_Price_NXController : BaseController
    {

        /// <summary>
        /// 保存 产品报价进入
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="zversion"></param>
        /// <param name="cuscode"></param>
        /// <param name="cusname"></param>
        /// <param name="busArray"></param>
        /// <param name="orgcode"></param>
        /// <param name="orgname"></param>
        /// <param name="priceName"></param>
        /// <param name="dtfrom"></param>
        /// <param name="dtto"></param>
        /// <param name="memo"></param>
        /// <param name="bpcode9"></param>
        /// <returns></returns>
        public ActionResult SaveSJBJ(string keyvalue, string zversion, string dtfrom, string dtto, string contrsctnum, string memo, string bpcode9)
        {
            var flag = true;
            var rtnmessga = "保存成功";
            string code = "1";
            string vrid = "";
            try
            {
                //报价版本信息 DLC
                SQM_BJ_VER versrcobj = SQM_BJ_VER.FindFirstByProperties(SQM_BJ_VER.Prop_MRID, keyvalue, SQM_BJ_VER.Prop_ZVER, zversion);
                vrid = versrcobj.RID;
                versrcobj.DTFROM = DateTime.Parse(dtfrom);
                versrcobj.DTTO = DateTime.Parse(dtto);
                //if (zversion == "V1")
                //{
                //    versrcobj.CONTRSCTNUM = contrsctnum;
                //}
                versrcobj.BPCODE9 = bpcode9;
                versrcobj.MEMO = memo;
                versrcobj.MODIFYTIME = DateTime.Now;
                versrcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                versrcobj.DoUpdate();


                //报价主信息
                var mainobj = SQM_BJ_MAIN_BASIC.TryFind(keyvalue);
                mainobj.DTFROM = DateTime.Parse(dtfrom);
                mainobj.DTTO = DateTime.Parse(dtto);
                //mainobj.BJNAME = priceName;
                mainobj.MEMO = memo;
                mainobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();

                //报价PSF表里更改每个单一费目的有效期
                bool ifjx = false;
                List<string> dataLog = new List<string>();// 有效期存在交叉的费目
                DataTable bjdt = DataHelper.QueryDataTable(string.Format("select RID,BJSTARTDATE,BJENDDATE from SQM_BJ_PSF where MRID='{0}' and (STATUS <> '0' or STATUS is null) order by PRODUCT_CODE, SERVICE_CODE, FEE_CODE", keyvalue));
                foreach (DataRow bjdr in bjdt.Rows)
                {
                    if (String.IsNullOrEmpty(bjdr["BJSTARTDATE"].ToString()) || String.IsNullOrEmpty(bjdr["BJENDDATE"].ToString()))
                    {
                        var sbpodj = SQM_BJ_PSF.TryFind(bjdr["RID"].ToString());
                        sbpodj.BJSTARTDATE = DateTime.Parse(dtfrom);
                        sbpodj.BJENDDATE = DateTime.Parse(dtto);
                        sbpodj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbpodj.DoUpdate();
                    }
                    else if (DateTime.Parse(dtfrom) <= (DateTime)bjdr["BJSTARTDATE"] && DateTime.Parse(dtto) >= (DateTime)bjdr["BJENDDATE"])
                    {
                        continue;
                    }
                    else if (DateTime.Parse(dtfrom) > (DateTime)bjdr["BJSTARTDATE"] || DateTime.Parse(dtto) < (DateTime)bjdr["BJENDDATE"])
                    {
                        ifjx = true;
                        var sbpodj = SQM_BJ_PSF.TryFind(bjdr["RID"].ToString());
                        sbpodj.BJSTARTDATE = DateTime.Parse(dtfrom);
                        sbpodj.BJENDDATE = DateTime.Parse(dtto);
                        sbpodj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sbpodj.DoUpdate();
                        dataLog.Add(sbpodj.PRODUCT_NAME + "-" + sbpodj.SERVICE_NAME + "-" + sbpodj.FEE_NAME + "-" + "有效期与报价有效期存在交叉，已更改！");
                    }
                }
                if (ifjx)
                {
                    code = "2";
                    rtnmessga = string.Join("<BR>", dataLog.ToArray());
                }

                if (!string.IsNullOrEmpty(mainobj.XSYBJID))
                {
                    #region  销售易更新 报价接口 审批中
                    BJWriteBackUpdate.UpdateQuotation uwb = new Web.BJWriteBackUpdate.UpdateQuotation();
                    BJWriteBackUpdate.phUpdateQuotation uhead = new BJWriteBackUpdate.phUpdateQuotation();
                    uhead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                    uhead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                    uhead.msgId = Guid.NewGuid().ToString();

                    BJWriteBackUpdate.pbUpdateQuotation _ubodys = new BJWriteBackUpdate.pbUpdateQuotation();
                    BJWriteBackUpdate.pbUpdateQuotation[] ubodys = new BJWriteBackUpdate.pbUpdateQuotation[1];
                    BJWriteBackUpdate.pbUpdateQuotationData[] ubody = new BJWriteBackUpdate.pbUpdateQuotationData[1];
                    BJWriteBackUpdate.pbUpdateQuotationData _ubody = new BJWriteBackUpdate.pbUpdateQuotationData();

                    _ubody.id = mainobj.XSYBJID;
                    _ubody.customItem3__c = zversion;//报价版本 //测试
                    _ubody.customItem4__c = "0";//报价状态

                    ubody[0] = _ubody;
                    _ubodys.data = ubody;
                    ubodys[0] = _ubodys;
                    uwb.CallUpdateQuotation(uhead, ubodys);
                    #endregion
                }
                else
                {
                    #region  销售易创建 报价接口 保存
                    BJWriteback.CreateQuotation wb = new Web.BJWriteback.CreateQuotation();
                    BJWriteback.phCreateQuotation head = new BJWriteback.phCreateQuotation();
                    head.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                    head.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                    head.msgId = Guid.NewGuid().ToString();


                    BJWriteback.pbCreateQuotation _bodys = new BJWriteback.pbCreateQuotation();
                    BJWriteback.pbCreateQuotation[] bodys = new BJWriteback.pbCreateQuotation[1];
                    BJWriteback.pbCreateQuotationData[] body = new BJWriteback.pbCreateQuotationData[1];
                    BJWriteback.pbCreateQuotationData _body = new BJWriteback.pbCreateQuotationData();

                    SQM_BJ_ORG orgobj = SQM_BJ_ORG.FindFirstByProperties(SQM_BJ_ORG.Prop_MRID, keyvalue);
                    SQM_BJ_BIZ busobj = SQM_BJ_BIZ.FindFirstByProperties(SQM_BJ_BIZ.Prop_MRID, keyvalue);
                    SQM_BJ_BP cusobj = SQM_BJ_BP.FindFirstByProperties(SQM_BJ_BP.Prop_MRID, keyvalue);
                    //_body.customItem1__c = mainobj.BJNAME;//报价编号  不填

                    //_body.quotationTitle = "";报价名称
                    _body.customItem3__c = zversion;//报价版本 
                    _body.customItem4__c = "0";//报价状态
                    _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "QM_Price_N/QM_PriceEdit?keyValue=" + keyvalue;//报价地址
                    _body.customItem6__c = orgobj.ORGCODE;//mainobj.BJNAME;//操作组织 
                    _body.customItem7__c = keyvalue;//mainobj.BJNAME;//报价ID
                    _body.ownerId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey(); //mainobj.BJNAME;//所有人
                    _body.entityType = "855577209438913";//业务类型写死
                    _body.quotationEntityRelAccount = cusobj.BPCODE;//客户编码
                    _body.quoteTime = DateToTicks(DateTime.Now).ToString();//报价时间
                                                                           //_body.dimDepart = mainobj.BJNAME;待增加
                    _body.totalDiscountAmount = "0";//总折扣额
                    _body.quotationEntityRelOpportunity = busobj.BIZID;//待增加 商机编码

                    body[0] = _body;

                    _bodys.data = body;
                    bodys[0] = _bodys;
                    BJWriteback.msgResponse msg = wb.CallCreateQuotation(head, bodys);

                    string xsybjid = "";
                    var jos = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(msg.list[0].originMessage);
                    foreach (var itms in jos)
                    {
                        var jo = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString());
                        foreach (var itm in jo)
                        {
                            if (itm.Key == "id")
                            {
                                xsybjid = itm.Value.ToString();
                            }
                        }
                    }

                    mainobj.XSYBJID = xsybjid;
                    mainobj.DoUpdate();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                rtnmessga = ex.Message;
                flag = false;
            }
            return Content(new JsonMessage { Success = flag, Message = rtnmessga, Code = code }.ToString());
        }

        public long DateToTicks(DateTime? time)
        {
            return ((time.HasValue ? time.Value.Ticks : DateTime.Parse("1990-01-01").Ticks) - 621355968000000000) / 10000;
        }
        #region

        #endregion

        #region 费率表、协议
        public ActionResult FWALists()
        {
            return View("FWALists");
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "FWA" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_FWA_REF).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (!string.IsNullOrEmpty(Request["CreateDateS"]))
            {
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_FWA_REF), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_FWA_REF>());
            var obj = new { draw = Request["draw"], data = SQM_FWA_REF.FindAll(SearchCriterion), recordsTotal = total, recordsFiltered = total };
            //多表关联时根据sql去检索数据
            //string sql = "select * from SysUser  where 1=1 ";
            //因为oracle大小写敏感,新建的表字段最好都统一大写,包括实体类
            //var obj = new { draw = Request["draw"], data = base.GetPageData(sql, SearchCriterion), recordsTotal = SearchCriterion.RecordCount, recordsFiltered = SearchCriterion.RecordCount };
            return Content(JsonHelper.GetJsonString(obj));
        }

        public ActionResult DeleteFWA(string keyvalue)
        {
            bool rtnflag = true;
            string code = "1";

            try
            {
                SQM_FWA_REF fwa = SQM_FWA_REF.TryFind(keyvalue);
                fwa.DoDelete();
            }
            catch (Exception ex)
            {
                rtnflag = false;
                code = "-1";
            }
            return Content(new JsonMessage { Success = rtnflag, Data = null, Code = code, Message = "删除协议成功" }.ToString());

        }
        private bool getSubdot(string number, out int roffset, out decimal ret)
        {
            bool bflag = false;
            roffset = 0;
            ret = 1m;
            if (!string.IsNullOrEmpty(number))
            {
                char[] point = { '.' };
                string[] seperated = number.Split(point);
                if (seperated.Length > 1 && seperated[1].TrimEnd('0').Length > 2)
                {
                    bflag = true;
                    roffset = (seperated[1].TrimEnd('0').Length - 2);
                    for (int i = 0; i < roffset; i++)
                    {
                        ret = ret * 10m;
                    }
                }
            }
            return bflag;
        }

        private bool IsBJZero(string bj)
        {
            return string.IsNullOrEmpty(bj) || string.IsNullOrEmpty(bj.Replace("0", "").Replace(".", ""));
        }

        private void Rate601Patched(ref Z2FM_SQ_RATE_CREATE rate601)
        {
            try
            {
                foreach (Z2FM_SQ_RATE_CREATEIT_RATE it_rate in rate601.IT_RATE)
                {
                    if (null != it_rate.VALIDITY)
                    {
                        foreach (Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity in it_rate.VALIDITY)
                        {
                            if (null != validity.RATES_DIM)
                            {
                                foreach (Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim in validity.RATES_DIM)
                                {
                                    if (IsBJZero(rates_dim.RATE))
                                    {
                                        rates_dim.RATE = "";
                                        rates_dim.ZERO_RATE = "X";
                                        continue;
                                    }

                                    int roffset;
                                    decimal ret;
                                    if (getSubdot(rates_dim.RATE, out roffset, out ret))
                                    {
                                        rates_dim.RATE = (decimal.Parse(rates_dim.RATE) * ret).ToString("#0.00");
                                        if (null != validity.CALCRULEREF)
                                        {
                                            foreach (Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref in validity.CALCRULEREF)
                                            {
                                                long qv = int.Parse(calcruleref.QUANTITY.QTY_VALUE);
                                                for (int i = 0; i < roffset; i++)
                                                {
                                                    qv = qv * 10;
                                                }
                                                calcruleref.QUANTITY.QTY_VALUE = qv.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void FWA701Patched(ref List<Z2FM_SQ_FWA_CREATEIT_FWA> fwalist)
        {
            try
            {
                foreach (Z2FM_SQ_FWA_CREATEIT_FWA fwa in fwalist)
                {
                    if (null != fwa.FAG_ITEM)
                    {
                        foreach (Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM fag_item in fwa.FAG_ITEM)
                        {
                            if (null != fag_item.TCCS_ROOT && null != fag_item.TCCS_ROOT.TCCS_ITEM)
                            {
                                foreach (Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_item in fag_item.TCCS_ROOT.TCCS_ITEM)
                                {
                                    int roffset;
                                    decimal ret;
                                    if (getSubdot(tccs_item.AMOUNT, out roffset, out ret))
                                    {
                                        tccs_item.AMOUNT = (decimal.Parse(tccs_item.AMOUNT) * ret).ToString("#0.00");
                                        if (null != tccs_item.ITEM_CALCRULE)
                                        {
                                            foreach (Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule in tccs_item.ITEM_CALCRULE)
                                            {
                                                long qv = int.Parse(item_calcrule.QTY_VALUE);
                                                for (int i = 0; i < roffset; i++)
                                                {
                                                    qv = qv * 10;
                                                }
                                                item_calcrule.QTY_VALUE = qv.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void FWA702Patched(ref List<Z2FM_SQ_FWA_MODIFYIT_FWA> fwalist)
        {
            try
            {
                foreach (Z2FM_SQ_FWA_MODIFYIT_FWA fwa in fwalist)
                {
                    if (null != fwa.FAG_ITEM)
                    {
                        foreach (Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM fag_item in fwa.FAG_ITEM)
                        {
                            if (null != fag_item.TCCS_ROOT && null != fag_item.TCCS_ROOT.TCCS_ITEM)
                            {
                                foreach (Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_item in fag_item.TCCS_ROOT.TCCS_ITEM)
                                {
                                    int roffset;
                                    decimal ret;
                                    if (getSubdot(tccs_item.AMOUNT, out roffset, out ret))
                                    {
                                        tccs_item.AMOUNT = (decimal.Parse(tccs_item.AMOUNT) * ret).ToString("#0.00");
                                        if (null != tccs_item.ITEM_CALCRULE)
                                        {
                                            foreach (Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule in tccs_item.ITEM_CALCRULE)
                                            {
                                                long qv = int.Parse(item_calcrule.QTY_VALUE);
                                                for (int i = 0; i < roffset; i++)
                                                {
                                                    qv = qv * 10;
                                                }
                                                item_calcrule.QTY_VALUE = qv.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void GetContractcode(string keyvalue, string zver)
        {
            try
            {
                string BizId = "";
                string ContractCode = "";
                string sqlbiz = string.Format("SELECT sqm_bj_biz.bizid FROM sqm_bj_main_basic LEFT JOIN sqm_bj_biz ON sqm_bj_main_basic.rid = sqm_bj_biz.mrid LEFT JOIN SQM_BJ_VER ON sqm_bj_main_basic.rid= SQM_BJ_VER.MRID  WHERE sqm_bj_main_basic.rid = '{0}' and SQM_BJ_VER.ZVER='{1}' and sqm_bj_biz.bizid is not null", keyvalue, zver);
                DataTable dt = DataHelper.QueryDataTable(sqlbiz);
                if (null != dt && dt.Rows.Count > 0 && dt.Rows != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        BizId += dr["BIZID"] + ",";
                    }
                }
                BizId = BizId.TrimEnd(',');
                if (!string.IsNullOrEmpty(BizId))
                {
                    string sqlcc = string.Format("select CONTRACTSTARTDATE,CONTRACTENDDATE,CONTRACTCODE from CRM_SALESCONTRACT where BUSINESSID like '%{0}%'  order by createtime desc", BizId);
                    //string sqlcc = string.Format("select CONTRACTSTARTDATE,CONTRACTENDDATE,CONTRACTCODE from CRM_SALESCONTRACT where BUSINESSID in('{0}')  order by CONTRACTCODE desc", BizId.Replace(",", "','"));
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    DataTable data = DataHelper.QueryDataTable(sqlcc, conn);
                    if (null != data && data.Rows.Count > 0)
                    {
                        ContractCode = data.Rows[0]["CONTRACTCODE"] + "";
                        if (!string.IsNullOrEmpty(ContractCode))
                        {
                            string update = string.Format("update SQM_BJ_VER set CONTRSCTNUM='{0}' where MRID = '{1}' AND ZVER = '{2}'", ContractCode, keyvalue, zver);
                            DataHelper.ExecSql(update);
                        }
                    }
                }
            }
            catch { }
        }

        static readonly string STR_JTLJ = "1";
        static readonly string STR_ZZCFTYZ = "ZZCFTYZ";//分摊因子直接参考行

        static readonly string ACTION_C = "C";
        static readonly string ACTION_U = "U";
        static readonly string ACTION_D = "D";

        static readonly string STR_VERSION_NO = "0000";

        static readonly List<string> A2S_PRDS = new List<string>() { "AA12", "AA13", "AA14", "AA17" };  // 这些空运产品特殊处理：协议号走供应链规则，逻辑仍然走空运

        static readonly List<string> CZJSJS_FEES = new List<string>() { "S1000CZF0001" };

        public ActionResult RATEFWATM(string keyvalue, string zver)
        {
            List<string> fwafagrmntid044List = new List<string>();
            try
            {
                string bjrid = "";
                string prdcode = "";
                string srvcode = "";
                string feecode = "";
                string feename = "";
                string gdzrid = "";
                string djfsrid = "";
                string sql = "";
                string fieldkeys = "";
                bool bMIN = false;
                string js_obj = "";
                string js_role = "";
                string rtnfwa = "";
                string rtnmsg = "";
                bool rtnflag = true;
                bool rateflag = true;
                string ratemsg = "";
                bool fwaflag = true;
                string fwamsg = "";
                bool a2sflag = false;
                bool hadA2S = false;
                List<string> a2sprdslist = new List<string>();// 特殊处理的空运产品集合：协议号走供应链规则，逻辑仍然走空运

                bool bsbmt = DataHelper.QueryValue(string.Format("SELECT COUNT(1) FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' AND ( STATUS = '2' OR  STATUS = '4' ) ", keyvalue, zver)) + "" == "1";
                if (!bsbmt)
                {
                    rtnmsg = "审批通过的才能提交TM";
                    rtnflag = false;
                    goto rtnLabel;
                }
                bsbmt = DataHelper.QueryValue(string.Format("SELECT COUNT(1) FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' AND STATUS = '4'", keyvalue, zver)) + "" == "1";
                if (!bsbmt)
                {
                    rtnmsg = "已发送客户的才能提交TM";
                    rtnflag = false;
                    goto rtnLabel;
                }
                //获取crm销售合同编号
                GetContractcode(keyvalue, zver);
                bsbmt = !string.IsNullOrEmpty(DataHelper.QueryValue(string.Format("SELECT CONTRSCTNUM FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' AND STATUS = '4'", keyvalue, zver)) + "");
                if (!bsbmt)
                {
                    rtnmsg = "具有有效合同才能提交TM";
                    rtnflag = false;
                    goto rtnLabel;
                }
            A2SLabel:
                //首先查询出版本的rid
                var vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver));
                //psf表信息
                sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1' AND ( BJSTATAUS = '2' OR BJSTATAUS = '5' ) ORDER BY PRODUCT_CODE, SERVICE_CODE, FEE_CODE ", vrid);
                //过滤掉at cost且被包干的费目
                //sql = string.Format(@"SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1' AND ( BJSTATAUS = '2' OR BJSTATAUS = '5' ) and not (BGFZRID is not null and BGFZRID<>'1' and FEECATG='2') ORDER BY PRODUCT_CODE, SERVICE_CODE, FEE_CODE ", vrid);

                string sybsql = string.Format("select distinct substr(PRODUCT_CODE, 0, 1) syb from  ( SELECT PRODUCT_CODE FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1' AND ( BJSTATAUS = '2' OR BJSTATAUS = '5' )  ORDER BY PRODUCT_CODE, SERVICE_CODE, FEE_CODE )", vrid);
                DataTable dtpsfsyb = DataHelper.QueryDataTable(sybsql);//报价产品代码首字母
                List<string> sybproductcodeslist = new List<string>();//事业部集合
                foreach (DataRow prsyb in dtpsfsyb.Rows)
                {
                    if (!sybproductcodeslist.Contains(prsyb["SYB"] + ""))
                    {
                        sybproductcodeslist.Add(prsyb["SYB"] + "");
                    }
                }

                DataTable dtpsf = DataHelper.QueryDataTable(sql);//本份报价SQM_BJ_PSF信息
                if (a2sflag && a2sprdslist.Count > 0)
                {
                    sybproductcodeslist.Clear();
                    sybproductcodeslist.Add("A");
                }
                foreach (string strSyb in sybproductcodeslist)
                {
                    string strFWAFAGRMNTID044 = "";
                    int line_auto = 10000;
                    List<string> productcodeslist = new List<string>();//报价所有产品集合
                    if (!a2sflag)
                    {
                        foreach (DataRow prow in dtpsf.Rows)
                        {
                            if (!productcodeslist.Contains(prow["PRODUCT_CODE"] + "") && (prow["PRODUCT_CODE"] + "").StartsWith(strSyb))
                            {
                                if (A2S_PRDS.Contains(prow["PRODUCT_CODE"] + ""))
                                {
                                    hadA2S = true;
                                    if (!a2sprdslist.Contains(prow["PRODUCT_CODE"] + ""))
                                    {
                                        a2sprdslist.Add(prow["PRODUCT_CODE"] + "");
                                    }
                                }
                                else
                                {
                                    if (!productcodeslist.Contains(prow["PRODUCT_CODE"] + ""))
                                    {
                                        productcodeslist.Add(prow["PRODUCT_CODE"] + "");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        productcodeslist.Clear();
                        productcodeslist.AddRange(a2sprdslist);
                    }

                    if (productcodeslist.Count < 1)
                    {
                        continue;
                    }

                    string bjmbsql = string.Format(" SELECT * FROM SQM_BJ_MAIN_BASIC WHERE RID = '{0}' ", keyvalue);
                    DataTable dtBJMB = DataHelper.QueryDataTable(bjmbsql);//报价主数据信息

                    string bjbvsql = string.Format(" SELECT DTFROM, DTTO FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' ", keyvalue, zver);
                    DataTable dtBJBV = DataHelper.QueryDataTable(bjbvsql);//该报价版本下的起止日期

                    string prdextsql = string.Format(" select BUSINESSORG from sqm_prd_ext where productkey = '{0}' ", productcodeslist[0] + "");
                    string strBUSINESSORG = (string)DataHelper.QueryValue(prdextsql);//报价产品的事业部
                    string strsyb = "";

                    BizTalk_RFC_TM_CRM_701_Orchestration_InboundSoapClient fwa701service = new BizTalk_RFC_TM_CRM_701_Orchestration_InboundSoapClient();
                    fwa701service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);//协议创建接口
                    List<Z2FM_SQ_FWA_CREATEIT_FWA> fwalist = new List<Z2FM_SQ_FWA_CREATEIT_FWA>();
                    Z2FM_SQ_FWA_CREATEIT_FWA fwa = new Z2FM_SQ_FWA_CREATEIT_FWA();
                    //协议类型    供应链Z101、Z102、Z103   海运Z201  空运Z301  运输Z401  根据产品的事业部
                    if (strBUSINESSORG.Contains("空运"))
                    {
                        fwa.FAGTYPEID103 = "Z301";//协议类型
                        strsyb = "空运";
                        strFWAFAGRMNTID044 = "A";//协议
                    }
                    else if (strBUSINESSORG.Contains("海运"))
                    {
                        fwa.FAGTYPEID103 = "Z201";
                        strsyb = "海运";
                        strFWAFAGRMNTID044 = "O";
                    }
                    else if (strBUSINESSORG.Contains("运输"))
                    {
                        fwa.FAGTYPEID103 = "Z401";
                        strsyb = "运输";
                        strFWAFAGRMNTID044 = "Y";
                    }
                    else if (strBUSINESSORG.Contains("供应链"))
                    {
                        fwa.FAGTYPEID103 = "Z101";
                        strsyb = "供应链";
                        strFWAFAGRMNTID044 = "S";
                    }
                    else
                    {
                        fwa.FAGTYPEID103 = strBUSINESSORG;
                    }

                    fwa.FAGUSAGEID105 = "1";//协议使用   传1：1客户，2供应商
                    //fwa.FAGRMNTID044 = "";//按规则生成，不传自动生成
                    //fwa.VERSION_NO = "0";
                    fwa.EXTERNAL_FA_ID = dtBJMB.Rows[0]["BJNAME"] + "";//外部参考编号 
                    fwa.DIM_WT_PROFILE = dtBJMB.Rows[0]["MVFILECODE"] + "";//体积重量参数文件  报价台头取
                    fwa.VALIDITY_START = DateTime.Parse(dtBJMB.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");//有效期开始日期   报价有效期
                    fwa.VALIDITY_END = DateTime.Parse(dtBJMB.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");//有效期结束日期 报价有效期
                    fwa.DOC_CURRENCY = "CNY";//凭证货币    报价货币

                    if (ConfigHelper.AppSettings("connection_crm").Replace(" ", "").ToUpper().Contains("USERID=CRM2;"))
                    {
                        fwa.ZXSY = SQMHelper.getStaffKey().PadLeft(10, '0');
                    }
                    else
                    {
                        fwa.ZXSY = "0010000076";//销售员    CRM商机跟进人    0010000076  0010000086  0010000353  0010000729
                    }
                    fwa.ZBJLX = "01";//报价类型 01 标准报价，02 非标报价
                    List<Z2FM_SQ_FWA_CREATEIT_FWAROOT_TEXT> root_textlist = new List<Z2FM_SQ_FWA_CREATEIT_FWAROOT_TEXT>();//ROOT_TEXT导入表类型 
                    Z2FM_SQ_FWA_CREATEIT_FWAROOT_TEXT root_text = new Z2FM_SQ_FWA_CREATEIT_FWAROOT_TEXT();

                    root_text.DESCRIPTION = dtBJMB.Rows[0]["BJNAME"] + ""; //报价的描述
                    root_textlist.Add(root_text);
                    fwa.ROOT_TEXT = root_textlist.ToArray();
                    List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS> contractorslist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS>();//CONTRACTORS导入表类型 
                    //待定
                    string orgsql = string.Format("select * from sqm_bj_org where mrid = '{0}'", keyvalue);
                    DataTable dtOrgs = DataHelper.QueryDataTable(orgsql);//报价销售组织1100-921
                    List<string> divisioncodeslist = new List<string>();
                    foreach (DataRow dro in dtOrgs.Rows)
                    {
                        string[] sArray = dro["ORGCODE"].ToString().Split('-');
                        //Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS contractors = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS();
                        //contractors.ORG_UNIT = sArray[0];//组织单位
                        //contractors.ORG_EXT_ID = sArray[0];//销售组织外部标识
                        IDbConnection crmconn = new OracleConnection();
                        crmconn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                        if (crmconn.State != ConnectionState.Open)
                        {
                            crmconn.Open();
                        }
                        DataTable dtsyb = DataHelper.QueryDataTable("select sap_code,divisioncode from divisionmapping_bj where sap_code='" + sArray[0] + "' and divisionname='" + strsyb + "'", crmconn);//销售组织代码1100、1101
                        foreach (DataRow rsyb in dtsyb.Rows)
                        {
                            string divcode = rsyb["divisioncode"] + "";
                            if (!divisioncodeslist.Contains(divcode))
                            {
                                divisioncodeslist.Add(divcode);
                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS contractors = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_CONTRACTORS();//CONTRACTORS导入表类型
                                contractors.ORG_UNIT = divcode;//组织单位
                                contractors.ORG_EXT_ID = divcode;//销售组织外部标识
                                contractorslist.Add(contractors);
                            }
                        }
                    }
                    fwa.FAG_CONTRACTORS = contractorslist.ToArray();
                    List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY> partylist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY>();//PARTY导入表类型 
                    //CRM商机的客户
                    string bpssql = string.Format("select * from sqm_bj_bp where mrid = '{0}'", keyvalue);
                    string bpsql = "";
                    DataTable dtBP = DataHelper.QueryDataTable(bpssql);
                    string sdrbp = "";
                    bool bNBGS = false;
                    string nbgssql = "";
                    foreach (DataRow drbp in dtBP.Rows)
                    {
                        sdrbp = drbp["BPCODE"] + "";
                        if (!bNBGS)
                        {
                            nbgssql = string.Format("SELECT * FROM CRM_CUSTOMERBASE WHERE CUSTOMTYPE = '内部客户' AND CUSTOMERNO = '{0}'", sdrbp);
                            IDbConnection nbgscrmconn = new OracleConnection();
                            nbgscrmconn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                            if (nbgscrmconn.State != ConnectionState.Open)
                            {
                                nbgscrmconn.Open();
                            }
                            DataTable dtnbgs = DataHelper.QueryDataTable(nbgssql, nbgscrmconn);
                            if (null != dtnbgs && dtnbgs.Rows.Count > 0)
                            { bNBGS = true; }
                        }
                        if (sdrbp.Length == 4)
                        {
                            sdrbp = sdrbp.PadLeft(10, '0');//内部客户补满10位
                        }
                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY party = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY();//PARTY导入表类型
                        party.PTYINTID_ID133_I = sdrbp;//业务伙伴编号
                        bpsql = string.Format(" select * from mdm_bp where BPKEY = '{0}' ", sdrbp);
                        string bpkey = DataHelper.QueryValue(bpsql) + "";
                        if (string.IsNullOrEmpty(bpkey))
                        {
                            rtnmsg = "未找到BP客户" + sdrbp;
                            rtnflag = false;
                            goto rtnLabel;
                        }
                        party.UUID001 = DBKeyHelper.HexToBytes(bpkey);
                        partylist.Add(party);
                    }
                    if (strBUSINESSORG.Contains("供应链"))
                    {
                        if (!bNBGS && A2S_PRDS.Intersect(productcodeslist).Count() < 1)
                        {
                            string bpcode9 = DataHelper.QueryValue(string.Format("SELECT BPCODE9 FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver)) + "";
                            if (!string.IsNullOrEmpty(bpcode9))
                            {
                                foreach (string sbp in bpcode9.TrimStart(',').TrimEnd(',').Split(','))
                                {
                                    Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY party = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_PARTY();
                                    party.PTYINTID_ID133_I = sbp;//业务伙伴编号
                                    bpsql = string.Format(" select * from mdm_bp where BPKEY = '{0}' ", sbp);
                                    string bpkey = DataHelper.QueryValue(bpsql) + "";
                                    if (string.IsNullOrEmpty(bpkey))
                                    {
                                        rtnmsg = "未找到9位码BP客户" + sbp;
                                        rtnflag = false;
                                        goto rtnLabel;
                                    }
                                    party.UUID001 = DBKeyHelper.HexToBytes(bpkey);
                                    partylist.Add(party);
                                }
                            }
                            else
                            {
                                rtnmsg = "供应链在提交前需要选择9位码";
                                rtnflag = false;
                                goto rtnLabel;
                            }
                        }
                    }
                    fwa.FAG_PARTY = partylist.ToArray();//PARTY导入表类型 
                    if (a2sflag && a2sprdslist.Count > 0)
                    {
                        strFWAFAGRMNTID044 = "SA";
                    }
                    strFWAFAGRMNTID044 += fwa.FAG_PARTY[0].PTYINTID_ID133_I;

                    string sqlhadfwa = string.Format(" SELECT FWA FROM SQM_FWA_REF WHERE MRID = '{0}' AND ZVER = '{1}' and FWA LIKE '{2}%' and ROWNUM<=1 ", keyvalue, zver, strFWAFAGRMNTID044);
                    string strFWA703 = DataHelper.QueryValue(sqlhadfwa) + "";//该版本是否已经提交TM生成了协议
                    if (!string.IsNullOrEmpty(strFWA703))
                    {
                        return Content(new JsonMessage { Success = false, Data = null, Code = "1", Message = "已有生成协议，请勿重复提交TM" }.ToString());
                    }
                    string sqlhadfwapre = string.Format(" SELECT * FROM ( SELECT FWA FROM SQM_FWA_REF WHERE MRID = '{0}' and FWA LIKE '{1}%' ORDER BY FWA DESC ) WHERE  ROWNUM<=1  ", keyvalue, strFWAFAGRMNTID044);
                    strFWA703 = DataHelper.QueryValue(sqlhadfwapre) + "";
                    if (!string.IsNullOrEmpty(strFWA703))//先判断报价是否生成过协议，是则先获取已有协议，进行修改
                    {
                        try
                        {
                            List<string> KeysList = new List<string>();
                            //获取协议
                            BizTalk_RFC_TM_CRM_703_Orchestration_InboundSoapClient fwa703service = new BizTalk_RFC_TM_CRM_703_Orchestration_InboundSoapClient();
                            fwa703service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);
                            Z2FM_SQ_FWA_SEARCH fwasrch703 = new Z2FM_SQ_FWA_SEARCH();
                            fwasrch703.IV_FAGRMNTID044 = strFWA703;
                            fwasrch703.IV_VERSION_NO = STR_VERSION_NO;
                            Z2FM_SQ_FWA_SEARCH_RESET_FWA[] fwasrch;
                            try
                            {
                                fwasrch = fwa703service.Operation_1(fwasrch703);
                            }
                            catch (Exception ex703)
                            {
                                return Content(new JsonMessage { Success = false, Data = null, Code = "1", Message = "获取协议失败：" + ex703.Message }.ToString());
                            }

                            //协议修改
                            BizTalk_RFC_TM_CRM_702_Orchestration_InboundSoapClient fwa702service = new BizTalk_RFC_TM_CRM_702_Orchestration_InboundSoapClient();
                            fwa702service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);
                            List<Z2FM_SQ_FWA_MODIFYIT_FWA> fwa702list = new List<Z2FM_SQ_FWA_MODIFYIT_FWA>();
                            Z2FM_SQ_FWA_MODIFYIT_FWA fwa702 = new Z2FM_SQ_FWA_MODIFYIT_FWA();
                            fwa702 = JsonHelper.GetObject<Z2FM_SQ_FWA_MODIFYIT_FWA>(JsonHelper.GetJsonString(fwasrch[0]));
                            fwa702.VALIDITY_START = DateTime.Parse(dtBJMB.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");//有效期开始日期   报价有效期
                            fwa702.VALIDITY_END = DateTime.Parse(dtBJMB.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");//有效期结束日期 报价有效期
                            if (null != fwa702.ROOT_TEXT)//ROOT_TEXT导入表类型 
                            {
                                foreach (var item2 in fwa702.ROOT_TEXT)
                                {
                                    item2.ACTION = ACTION_U;
                                }
                            }
                            if (null != fwa702.FAG_CONTRACTORS)//CONTRACTORS导入表类型 
                            {
                                foreach (var item2 in fwa702.FAG_CONTRACTORS)
                                {
                                    item2.ACTION = ACTION_U;
                                }
                            }
                            if (null != fwa702.FAG_PARTY)//PARTY导入表类型 
                            {
                                foreach (var item2 in fwa702.FAG_PARTY)
                                {
                                    item2.ACTION = ACTION_U;
                                }
                            }
                            if (null != fwa702.FAG_ITEM)//ITEMS导入表类型
                            {
                                int itemno = 0;
                                int rownum = 0;
                                string strfrom = "";
                                string strto = "";
                                foreach (var item2 in fwa702.FAG_ITEM)
                                {
                                    item2.ACTION = ACTION_U;
                                    //"0000-00-00"->"00000000"  2019-8-5    DLC放开注释
                                    if (!string.IsNullOrEmpty(item2.VALIDITY_START))
                                    {
                                        if (item2.VALIDITY_START != "0000-00-00")
                                        {
                                            item2.VALIDITY_START = DateTime.Parse(item2.VALIDITY_START).ToString("yyyyMMdd");
                                        }
                                        else
                                        {
                                            item2.VALIDITY_START = "00000000";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(item2.VALIDITY_END))
                                    {
                                        if (item2.VALIDITY_END != "0000-00-00")
                                        {
                                            item2.VALIDITY_END = DateTime.Parse(item2.VALIDITY_END).ToString("yyyyMMdd");
                                        }
                                        else
                                        {
                                            item2.VALIDITY_END = "00000000";
                                        }
                                    }

                                    itemno = int.Parse(item2.ITEM_NO); //协议项目编号
                                    if (itemno % 100 == 0) // 行项目版本以100一步，e.g.  100  200  300 400 ... 
                                    {
                                        rownum += 1;
                                        string zversql = string.Format("  SELECT ZVER FROM ( SELECT ROWNUM AS RN, ZVER FROM ( SELECT ZVER FROM SQM_FWA_REF WHERE FWA = '{0}' ORDER BY CREATETIME ASC ) ) WHERE RN = {1}", strFWA703, rownum + "");
                                        string szver = DataHelper.QueryValue(zversql) + "";

                                        string dtfromto = string.Format(" SELECT DTFROM, DTTO FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' ", keyvalue, szver);
                                        DataTable fromtodt = DataHelper.QueryDataTable(dtfromto);

                                        strfrom = fromtodt.Rows[0]["DTFROM"] + "";
                                        strto = fromtodt.Rows[0]["DTTO"] + "";

                                        item2.VALIDITY_START = DateTime.Parse(strfrom).ToString("yyyyMMdd");
                                        item2.VALIDITY_END = DateTime.Parse(strto).ToString("yyyyMMdd");  //只改有效期
                                    }
                                    //else
                                    //{
                                    //    item2.VALIDITY_START = DateTime.Parse(strfrom).ToString("yyyyMMdd");
                                    //    item2.VALIDITY_END = DateTime.Parse(strto).ToString("yyyyMMdd");
                                    //}


                                    if (null != item2.INS_DETAIL)//指令DETAILS导入表类型
                                    {
                                        foreach (var item3 in item2.INS_DETAIL)
                                        {
                                            item3.ACTION = ACTION_U;
                                        }
                                    }
                                    if (null != item2.TCCS_ROOT && null != item2.TCCS_ROOT.TCCS_ITEM)//计算单ROOT导入表类型 
                                    {
                                        item2.TCCS_ROOT.ACTION = ACTION_U;
                                        foreach (var item4 in item2.TCCS_ROOT.TCCS_ITEM)//计算单ITEM导入表类型
                                        {
                                            item4.ACTION = ACTION_U;
                                            if (null != item4.ITEM_CALCRULE)//ITEM_CALCRULE导入表结构
                                            {
                                                foreach (var item5 in item4.ITEM_CALCRULE)
                                                {
                                                    item5.ACTION = ACTION_U;
                                                }
                                            }
                                            if (null != item4.ITEM_COST)//ITEM_COST导入表结构 
                                            {
                                                foreach (var item5 in item4.ITEM_COST)
                                                {
                                                    item5.ACTION = ACTION_U;
                                                }
                                            }
                                            if (null != item4.ITEM_DYFWLX)//ITEM_DYFWLX导入表类型 
                                            {
                                                foreach (var item5 in item4.ITEM_DYFWLX)
                                                {
                                                    item5.ACTION = ACTION_U;
                                                }
                                            }
                                            if (null != item4.TCCS_ZCZXG)//ZCZXG导入表类型 
                                            {
                                                foreach (var item5 in item4.TCCS_ZCZXG)
                                                {
                                                    item5.ACTION = ACTION_U;
                                                }
                                            }
                                            if (null != item4.ITEM_TEXT)//ITEM_TEXT导入表类型 
                                            {
                                                foreach (var item5 in item4.ITEM_TEXT)
                                                {
                                                    item5.ACTION = ACTION_U;
                                                }
                                            }
                                        }
                                    }
                                    KeysList.Add(item2.KEY);
                                }
                            }

                            List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM> fag_itemlist702 = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM>();
                            if (null != fwa702.FAG_ITEM)//ITEMS导入表类型
                            {
                                foreach (var item702mod in fwa702.FAG_ITEM)
                                {
                                    fag_itemlist702.Add(item702mod);
                                }
                            }
                            foreach (string product_code in productcodeslist)
                            {
                                if (A2S_PRDS.Contains(product_code))
                                {
                                    fwa.FAGTYPEID103 = "Z301";
                                }
                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM fag_item100702 = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM();
                                fag_item100702.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                                fag_item100702.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                                fag_item100702.ACTION = ACTION_C;
                                string strHex32 = SQMTMInterface.genITEMKEY();
                                fag_item100702.KEY = strHex32;//创建时自己标识
                                //不传，自动生成 fag_item.ITEM_NO = "";//协议项目编号
                                if (fwa.FAGTYPEID103 == "Z101")
                                {
                                    try
                                    {
                                        SQM_ITEMTYPE_REF itemtype = SQM_ITEMTYPE_REF.FindAllByProperty(SQM_ITEMTYPE_REF.Prop_PRODUCT, product_code).FirstOrDefault();
                                        fag_item100702.ITEM_TYPE = itemtype.ITEMTYPE;
                                    }
                                    catch
                                    {
                                        rtnmsg = "未找到产品" + product_code + "对应的项目类型";
                                        rtnflag = false;
                                        goto rtnLabel;
                                    }
                                }
                                else
                                {
                                    fag_item100702.SERVICE_PRODUCT_ID = product_code;//服务产品
                                }
                                //fag_item100.MTR = "";
                                //fag_item100.SERVICE_TYPE = "";//服务类型   报价的服务代码
                                //fag_item100.ZTGFS = "";//通关方式
                                //fag_item100.PAR_KEY = ;//上层ITEM KEY    即FAG_ITEM的KEY
                                fag_itemlist702.Add(fag_item100702);

                                if (fwa.FAGTYPEID103 == "Z101")
                                {
                                    foreach (DataRow dr in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                                    {
                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM fag_item702 = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM();
                                        fag_item100702.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                                        fag_item100702.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                                        fag_item702.ACTION = ACTION_C;
                                        prdcode = dr["PRODUCT_CODE"] + "";
                                        srvcode = dr["SERVICE_CODE"] + "";

                                        //fag_item.SERVICE_PRODUCT_ID = prdcode;//服务产品 报价的产品代码（供应链不传）
                                        fag_item702.SERVICE_TYPE = srvcode;//服务类型   报价的服务代码
                                        fag_item702.PAR_KEY = strHex32;// fag_item100.KEY;//上层ITEM KEY    即FAG_ITEM的KEY

                                        List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL> ins_list = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL>();
                                        string sqlins = string.Format("SELECT INS_ID FROM MDM_INSASN WHERE INSSET_ID IN ( SELECT INS_SET_ID FROM MDM_TSR WHERE SRVRQCD121 = '{0}' ) ", srvcode);
                                        int seq = 100;
                                        DataTable dtins = DataHelper.QueryDataTable(sqlins);
                                        if (null != dtins && dtins.Rows.Count > 0)
                                        {
                                            foreach (DataRow drins in dtins.Rows)
                                            {
                                                seq += 10;
                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL();
                                                ins.ACTION = ACTION_C;
                                                ins.SEQ_NUMBER = seq.ToString();
                                                ins.INS_ID = drins["INS_ID"] + "";
                                                ins_list.Add(ins);
                                            }
                                        }
                                        foreach (DataRow drps in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'" + " AND " + "SERVICE_CODE = '" + srvcode + "'"))
                                        {
                                            List<string> feeins = getFeeIns(drps["FEE_CODE"] + "", keyvalue, zver);
                                            if (null != feeins && feeins.Count > 0)
                                            {
                                                foreach (string fins in feeins)
                                                {
                                                    seq += 10;
                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL();
                                                    ins.ACTION = ACTION_C;
                                                    ins.SEQ_NUMBER = seq.ToString();
                                                    ins.INS_ID = fins;
                                                    ins_list.Add(ins);
                                                }
                                            }
                                        }
                                        if (ins_list.Count > 0)
                                        {
                                            fag_item702.INS_DETAIL = ins_list.ToArray();
                                        }

                                        fag_itemlist702.Add(fag_item702);
                                    }
                                }

                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOT tccs_root = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOT();
                                List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM> tccs_itemlist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM>();

                                List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST> item_costlist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST>();
                                foreach (DataRow drpsf in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                                {
                                    bjrid = drpsf["RID"] + "";
                                    prdcode = drpsf["PRODUCT_CODE"] + "";
                                    srvcode = drpsf["SERVICE_CODE"] + "";
                                    feecode = drpsf["FEE_CODE"] + "";
                                    feename = drpsf["FEE_NAME"] + "";
                                    js_obj = drpsf["JSFCODE"] + "";
                                    js_role = drpsf["JSFJSCODE"] + "";

                                    bool islsc = drpsf["ISLSC"] + "" == "1";
                                    List<string> bgflist = new List<string>();
                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX> item_dyfwlxlist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX>();
                                    if (islsc)//是否包干费
                                    {
                                        bgflist.Clear();
                                        string sqllsc = string.Format("SELECT * FROM SQM_BJ_PSF WHERE  VRID = '{0}' AND BGFZRID = '{1}' ORDER BY PRODUCT_CODE, SERVICE_CODE, FEE_CODE ", drpsf["VRID"] + "", drpsf["RID"] + "");
                                        DataTable dtbgf = DataHelper.QueryDataTable(sqllsc);
                                        if (null != dtbgf && dtbgf.Rows.Count > 0)
                                        {
                                            int ZLINE_NO = 0;
                                            foreach (DataRow drbgf in dtbgf.Rows)
                                            {
                                                string bgfsc = drbgf["SERVICE_CODE"] + "";
                                                if (!bgflist.Contains(bgfsc))
                                                {
                                                    bgflist.Add(bgfsc);
                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX item_dyfwlx = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX();
                                                    ZLINE_NO += 10;
                                                    item_dyfwlx.ACTION = ACTION_C;
                                                    item_dyfwlx.ZLINE_NO = ZLINE_NO.ToString();
                                                    item_dyfwlx.ZTRANSSRVREQ_CODE = bgfsc;
                                                    item_dyfwlxlist.Add(item_dyfwlx);
                                                }
                                            }
                                        }
                                    }

                                    string sqlJSFFZS = string.Format(" select JSFFZS from sqm_fee_calc where feecode = '{0}' ", feecode);
                                    bool bJSFFZS = DataHelper.QueryValue(sqlJSFFZS) + "" == "1";

                                    string sqlfeecalcnt = string.Format("select DISTINCT  DJFSRID from SQM_MODEBJ_VAL where FEECALCID='{0}' and ifbjitem = '1' ", bjrid);
                                    DataTable feecalcntdt = DataHelper.QueryDataTable(sqlfeecalcnt);

                                    if ((drpsf["BJFS"] + "" != "1") && null != feecalcntdt && feecalcntdt.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < feecalcntdt.Rows.Count; i++)
                                        {
                                            string strdjfsid = feecalcntdt.Rows[i]["DJFSRID"] + "";

                                            //获取报价值表的数据
                                            sql = @"select * from SQM_MODEBJ_VAL t where FEECALCID='" + bjrid + "' and ifbjitem = '1' ";
                                            if (!string.IsNullOrEmpty(strdjfsid))
                                            {
                                                sql += " and djfsrid = '" + strdjfsid + "'";
                                            }
                                            else
                                            {
                                                sql += " and djfsrid is null ";
                                            }
                                            DataTable zbvaldt = DataHelper.QueryDataTable(sql);

                                            if (string.IsNullOrEmpty(zbvaldt.Rows[0]["GDZRID"] + ""))
                                            {
                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_item = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                tccs_item.ACTION = ACTION_C;
                                                tccs_item.KEY = SQMTMInterface.genITEMKEY();
                                                tccs_item.LINENR = line_auto.ToString();
                                                line_auto++;
                                                //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                {
                                                    tccs_item.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                }
                                                else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                {
                                                    tccs_item.TCCALCRESINS040 = "COST";//指令类型 
                                                    tccs_item.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                    tccs_item.COST_PULL_STRATEGY = "2";
                                                    tccs_item.SOURCE_CHARGE = "3";
                                                }
                                                else
                                                {
                                                    tccs_item.TCCALCRESINS040 = "STND";//指令类型 
                                                }

                                                if (CZJSJS_FEES.Contains(feecode))
                                                {
                                                    tccs_item.ZCZJS_ROLE = js_role;//仓租结算角色   报价系统费目新增
                                                }
                                                else
                                                {
                                                    tccs_item.ZSETTLE_ROLE = js_role;//结算角色   报价费目（与结算方互斥）
                                                }
                                                tccs_item.ZSETTLE_OBJ = js_obj;//结算方    报价费目
                                                //tccs_item.OPERATIONCD102 = "";//费用项目操作   比较高低值时H、L，顺序检查
                                                //tccs_item.COST_PULL_STRATEGY = "";//成本拉式策略   COST时传2，其他不传
                                                //tccs_item.SOURCE_CHARGE = "";//费用源    COST时传3，其他不传
                                                //if ("STND" == tccs_item.TCCALCRESINS040)
                                                //{
                                                if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                {
                                                    tccs_item.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                }
                                                //}
                                                //tccs_item.CLCRESBAS036 = "";//计算解析基础    PKG、SERVICE等
                                                tccs_item.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                //tccs_item.RULE101 = "";//前提条件规则
                                                //tccs_item.AMOUNT = "";//简单报价金额（跟费率表 、费率表确定规则 互斥）
                                                tccs_item.CURRCODE016 = "CNY";//货币 币种

                                                //tccs_item.RATE_ID = "";//费率表 601
                                                //tccs_item.MIN_AMOUNT = "";//费目的最小值
                                                //tccs_item.MAX_AMOUNT = "";//费目的最高值
                                                //tccs_item.RULE099 = "";//费率表确定规则    报价费目
                                                //tccs_item.TARGET_ITEM_KEY = "";//目标ITEM KEY    自己编关系

                                                if (drpsf["BJFS"] + "" == "1")// AT COST
                                                {

                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                    item_cost.ACTION = ACTION_C;
                                                    item_cost.TCET084 = feecode;

                                                    item_costlist.Add(item_cost);

                                                    //tccs_item.ITEM_COST = item_costlist.ToArray();
                                                    //tccs_itemlist.Add(tccs_item);
                                                    continue;
                                                }
                                                else if (drpsf["BJFS"] + "" == "2")
                                                {
                                                    //tccs_item.TCET084 = feecode;
                                                    //tccs_itemlist.Add(tccs_item);
                                                    continue;
                                                }
                                                else
                                                {
                                                    tccs_item.TCET084 = feecode;
                                                }

                                                List<string> hadrate = new List<string>();

                                                foreach (DataRow zbdr in zbvaldt.Rows)
                                                {
                                                    djfsrid = zbdr["DJFSRID"].ToString();
                                                    gdzrid = zbdr["GDZRID"].ToString();

                                                    if (hadrate.Contains(djfsrid + gdzrid))
                                                    {
                                                        continue;
                                                    }

                                                    hadrate.Add(djfsrid + gdzrid);

                                                    if (!string.IsNullOrEmpty(gdzrid))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //是否有MIN
                                                        string minpriceSQL = "";
                                                        if (!string.IsNullOrEmpty(djfsrid))
                                                        {
                                                            minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID = '{1}'", feecode, djfsrid);
                                                        }
                                                        else
                                                        {
                                                            minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID is null ", feecode);
                                                        }
                                                        string minprice = DataHelper.QueryValue(minpriceSQL) + "";
                                                        bMIN = false;
                                                        if (minprice == "1")
                                                        {
                                                            bMIN = true;
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

                                                        SQMTMInterface sqmtminterface = new SQMTMInterface();

                                                        List<string> calccodestrc = new List<string>();//费率表标度
                                                        List<string> calccodestrcref = new List<string>();//参考行标度，两个或以上是，第二个作为参考行
                                                        fieldkeys = sqmtminterface.getFieldKeys(bjrid, bMIN, where, ref calccodestrc);
                                                        sql = "select " + fieldkeys + " from SQM_MODEBJ_VAL where ifbjitem = '1' and FEECALCID='{0}' and STATUS='1' {1}";
                                                        sql = string.Format(sql, bjrid, wheredt);
                                                        DataTable bjvalsdt = null;
                                                        bjvalsdt = DataHelper.QueryDataTable(sql);

                                                        if (calccodestrc.Count == 0)//没有标度，走计算单
                                                        {
                                                            if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                            {
                                                                continue;
                                                            }
                                                        }

                                                        if (bMIN)
                                                        {


                                                            if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1)
                                                            {
                                                                if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                                {
                                                                    continue;
                                                                }

                                                                List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                                item_calcrule.ACTION = ACTION_C;
                                                                item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                                item_calcrule.QTY_VALUE = "1";
                                                                item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                                if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                {
                                                                    item_calcrule.QTY_UNIT_C = "EA";
                                                                }
                                                                item_calcrulelist.Add(item_calcrule);
                                                                tccs_item.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                                tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                                if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                                {
                                                                    tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                }

                                                                if (bJSFFZS)
                                                                {
                                                                    tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                    tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                    string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                    string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                    if (!String.IsNullOrEmpty(djfsrid))
                                                                    {
                                                                        czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                        cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                    }
                                                                    else
                                                                    {
                                                                        czcxsql += " and DJFSRID is null ";
                                                                        cztjsql += " and DJFSRID is null ";
                                                                    }
                                                                    if (!String.IsNullOrEmpty(gdzrid))
                                                                    {
                                                                        czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                        cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                    }
                                                                    else
                                                                    {
                                                                        czcxsql += " and GDZRID is null ";
                                                                        cztjsql += " and GDZRID is null ";
                                                                    }
                                                                    DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                    DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                    if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                    {
                                                                        tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                        tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                        tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                        tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                        tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                    }

                                                                    if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                    {
                                                                        List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                        foreach (DataRow drcztj in dtcztj.Rows)
                                                                        {
                                                                            Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                            cztj.ACTION = ACTION_C;
                                                                            cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                            cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                            cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                            tccs_zczxglist.Add(cztj);
                                                                        }
                                                                        if (tccs_zczxglist.Count > 0)
                                                                        {
                                                                            tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                        }
                                                                    }
                                                                }

                                                                tccs_itemlist.Add(tccs_item);

                                                                continue;
                                                            }
                                                        }
                                                        if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1
                                                            && bjvalsdt.Rows[0][calccodestrc[0] + "ISCNT"] + "" == "是")
                                                        {
                                                            if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                            {
                                                                continue;
                                                            }

                                                            List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                            Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                            item_calcrule.ACTION = ACTION_C;
                                                            item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                            item_calcrule.QTY_VALUE = "1";
                                                            item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                            if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                            {
                                                                item_calcrule.QTY_UNIT_C = "EA";
                                                            }
                                                            item_calcrulelist.Add(item_calcrule);
                                                            tccs_item.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                            tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                            tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                            tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                            if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                            {
                                                                tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                            }

                                                            if (bJSFFZS)
                                                            {
                                                                tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                if (!String.IsNullOrEmpty(djfsrid))
                                                                {
                                                                    czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                    cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and DJFSRID is null ";
                                                                    cztjsql += " and DJFSRID is null ";
                                                                }
                                                                if (!String.IsNullOrEmpty(gdzrid))
                                                                {
                                                                    czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                    cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and GDZRID is null ";
                                                                    cztjsql += " and GDZRID is null ";
                                                                }
                                                                DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                {
                                                                    tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                    tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                    tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                    tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                    tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                }

                                                                if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                {
                                                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                    foreach (DataRow drcztj in dtcztj.Rows)
                                                                    {
                                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                        cztj.ACTION = ACTION_C;
                                                                        cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                        cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                        cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                        tccs_zczxglist.Add(cztj);
                                                                    }
                                                                    if (tccs_zczxglist.Count > 0)
                                                                    {
                                                                        tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                    }
                                                                }
                                                            }

                                                            tccs_itemlist.Add(tccs_item);

                                                            continue;
                                                        }

                                                        if (calccodestrc.Count > 0)
                                                        {
                                                            BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                            rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                            Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                            Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                            string str_rate_id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                            rate601.RATE_ID = str_rate_id;//费率表ID，长度20
                                                            rate601.TCUSAGECD085 = "3";
                                                            rate601.TIMEZONE = "UTC+8";
                                                            rate601.TCET = feecode; //"费目代码"
                                                            rate601.VAL_INDICATOR = "A"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                            rate601.RATE_TAB_TYPE = "ZFW1";
                                                            rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                            rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                            Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                            orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                            orgdatalist.Add(orgdata);
                                                            rate601.ORG_DATA = orgdatalist.ToArray();

                                                            List<string> calcexcludelist = new List<string>();
                                                            List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                            int indx = 0;
                                                            int bcntadd = 0;

                                                            bool multiMin = bMIN && bjvalsdt.DefaultView.ToTable(true, "MINBJPRICE").Rows.Count > 1;

                                                            foreach (var ccoders in calccodestrc)
                                                            {
                                                                if (STR_ZZCFTYZ == ccoders)
                                                                {
                                                                    calccodestrcref.Add(STR_ZZCFTYZ);
                                                                    continue;
                                                                }

                                                                if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是")
                                                                {
                                                                    bcntadd += 1;
                                                                    if (bcntadd > 1)
                                                                    {
                                                                        continue;
                                                                    }
                                                                }

                                                                bool isex = true;
                                                                string strlast = "";
                                                                foreach (DataRow bjcd in bjvalsdt.Rows)
                                                                {
                                                                    if (string.IsNullOrEmpty(strlast))
                                                                    {
                                                                        strlast = bjcd[ccoders + "CODE"] + "";
                                                                    }
                                                                    else if (strlast != bjcd[ccoders + "CODE"] + "")
                                                                    {
                                                                        isex = false;
                                                                        break;
                                                                    }
                                                                    if (STR_ZZCFTYZ != ccoders)
                                                                    {
                                                                        if ("A" == sqmtminterface.getCACL_TYP(bjcd[ccoders + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                        //仓租费计费数量为“是”的传到标度值里
                                                                        if (feecode == "S1000CZF0001" && bjcd[ccoders + "ISCNT"] + "" == "是")
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                if (isex && !multiMin)
                                                                {
                                                                    calcexcludelist.Add(ccoders);
                                                                    continue;
                                                                }

                                                                indx++;

                                                                Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                                ratescale.DIMENSION_INDX = indx.ToString(); //标度维数

                                                                ratescale.CALC_BASE = ccoders; //"计算基础代码";
                                                                if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                {
                                                                    ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                                }
                                                                else
                                                                {
                                                                    ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                                }
                                                                ratescale.SCALE_UOM = bjvalsdt.Rows[0][ccoders + "MSRCODE"] + "";

                                                                ratescale.INITVAL_SUPPORT = "X";
                                                                //ratescale.MINVAL_SUPPORTED = "X";
                                                                //ratescale.MAXVAL_SUPPORTED = "X";
                                                                if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是" && STR_JTLJ == bjvalsdt.Rows[0]["JTLJ"] + "")
                                                                {
                                                                    ratescale.REL_FOR_WGTBRK = "X";
                                                                    tccs_item.CALC_METH_CODE = "2";
                                                                }
                                                                ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                                if ("COMMODITY_CODE" == ccoders)
                                                                {
                                                                    ratescale.CCODE_TYPE = "IN";
                                                                }
                                                                ratescalelist.Add(ratescale);
                                                            }
                                                            rate601.RATESCALE = ratescalelist.ToArray();

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                            //foreach
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                            //validity.ZNUMBER = "";//待定
                                                            validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                            validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                            validity.CURRENCY = bjvalsdt.Rows[0]["CURRENCY"] + "";//货币


                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                            int iscnt = 0;
                                                            foreach (var ccodecrr in calccodestrc)
                                                            {
                                                                if (STR_ZZCFTYZ == ccodecrr)
                                                                {
                                                                    continue;
                                                                }

                                                                if (bjvalsdt.Rows[0][ccodecrr + "ISCNT"] + "" == "是")
                                                                {
                                                                    iscnt++;
                                                                    if (iscnt > 1)
                                                                    {
                                                                        calccodestrcref.Add(ccodecrr);
                                                                        continue;
                                                                    }
                                                                    //foreach
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                                    calcruleref.CALC_BASE_CODE = ccodecrr;//定价模块-费目标准报价方式-标识
                                                                    quantity.QTY_UNIT_C = bjvalsdt.Rows[0][ccodecrr + "MSRCODE"] + ""; //计量单位
                                                                    quantity.QTY_VALUE = "1";//默认传1
                                                                    calcruleref.QUANTITY = quantity;
                                                                    //calcruleref.ROUND_RULE = "";//默认为空
                                                                    //不传 calcruleref.FOR_REL_SCLITM = "";
                                                                    calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                                    calcrulereflist.Add(calcruleref);
                                                                }
                                                            }
                                                            validity.CALCRULEREF = calcrulereflist.ToArray();


                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                            //foreach
                                                            if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow drval in bjvalsdt.Rows)
                                                                {
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dimMin = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                    int cnt = 0;
                                                                    foreach (var ccoderd in calccodestrc)
                                                                    {
                                                                        if (calcexcludelist.Contains(ccoderd) && !multiMin)
                                                                        {
                                                                            continue;
                                                                        }
                                                                        if (calccodestrcref.Contains(ccoderd))
                                                                        {
                                                                            continue;
                                                                        }

                                                                        cnt++;

                                                                        switch (cnt)
                                                                        {
                                                                            case 1:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                //scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM1 = scale_item1;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 2:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM2 = scale_item2;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 3:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM3 = scale_item3;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 4:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM4 = scale_item4;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 5:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM5 = scale_item5;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 6:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM6 = scale_item6;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 7:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM7 = scale_item7;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 8:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM8 = scale_item8;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 9:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM9 = scale_item9;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 10:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM10 = scale_item10;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 11:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM11 = scale_item11;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 12:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM12 = scale_item12;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 13:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM13 = scale_item13;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 14:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                                scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM14 = scale_item14;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            default:
                                                                                break;
                                                                        }

                                                                        if (multiMin)
                                                                        {
                                                                            switch (cnt)
                                                                            {
                                                                                case 1:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                    scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM1 = scale_item1;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item1.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item1.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 2:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                    scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM2 = scale_item2;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item2.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item2.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 3:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                    scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM3 = scale_item3;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item3.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item3.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 4:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                    scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM4 = scale_item4;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item4.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item4.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 5:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                    scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM5 = scale_item5;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item5.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item5.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 6:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                    scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM6 = scale_item6;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item6.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item6.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 7:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                    scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM7 = scale_item7;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item7.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item7.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 8:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                    scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM8 = scale_item8;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item8.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item8.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 9:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                    scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM9 = scale_item9;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item9.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item9.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 10:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                    scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM10 = scale_item10;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item10.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item10.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 11:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                    scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM11 = scale_item11;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item11.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item11.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 12:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                    scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM12 = scale_item12;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item12.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item12.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 13:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                    scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM13 = scale_item13;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item13.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item13.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                case 14:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                                    scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dimMin.SCALE_ITEM14 = scale_item14;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                    if (scale_item14.CALC_TYP == "B")
                                                                                    {
                                                                                        scale_item14.SCALE_ITEM = "1-";
                                                                                    }
                                                                                    break;
                                                                                default:
                                                                                    break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (multiMin)
                                                                    {
                                                                        foreach (Z2FM_SQ_RATE_CREATEIT_RATERATESCALE rs in rate601.RATESCALE)
                                                                        {
                                                                            if (rs.SCATYP == "B")
                                                                            {
                                                                                rs.MINVAL_SUPPORTED = "X";
                                                                            }
                                                                        }
                                                                        rates_dimlist.Add(rates_dimMin);
                                                                    }
                                                                    rates_dimlist.Add(rates_dim);
                                                                }
                                                            }
                                                            validity.RATES_DIM = rates_dimlist.ToArray();

                                                            validitylist.Add(validity);

                                                            rate601.VALIDITY = validitylist.ToArray();

                                                            rate601list.Add(rate601);

                                                            rate601create.IT_RATE = rate601list.ToArray();

                                                            Rate601Patched(ref rate601create);
                                                            Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);//费率表创建

                                                            if (resrate != null && resrate.Count() > 0)
                                                            {
                                                                foreach (var rr in resrate)
                                                                {
                                                                    if (null != rr.MSG)
                                                                    {
                                                                        foreach (var rm in rr.MSG)
                                                                        {
                                                                            if ("E" == rm.MSG_TYPE)
                                                                            {
                                                                                rateflag = false;
                                                                                ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                            }
                                                                        }

                                                                        if (!rateflag)
                                                                        {
                                                                            goto rtnLabel;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            tccs_item.RATE_ID = str_rate_id;//费率表 601
                                                        }
                                                        else
                                                        {
                                                            tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                        }

                                                        if (tccs_item.TCCALCRESINS040 == "STND")
                                                        {
                                                            tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                            tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                        }
                                                        //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                        if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                        {
                                                            tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                        }
                                                        if (calccodestrcref.Count > 0)
                                                        {
                                                            tccs_item.ANALYTICRELEV = "X";
                                                        }
                                                        if (bJSFFZS)
                                                        {
                                                            tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                            tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                            string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                            string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                            if (!String.IsNullOrEmpty(djfsrid))
                                                            {
                                                                czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and DJFSRID is null ";
                                                                cztjsql += " and DJFSRID is null ";
                                                            }
                                                            if (!String.IsNullOrEmpty(gdzrid))
                                                            {
                                                                czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and GDZRID is null ";
                                                                cztjsql += " and GDZRID is null ";
                                                            }
                                                            DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                            DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                            if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                            {
                                                                tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                            }

                                                            if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                            {
                                                                List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                foreach (DataRow drcztj in dtcztj.Rows)
                                                                {
                                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                    cztj.ACTION = ACTION_C;
                                                                    cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                    cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                    cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                    tccs_zczxglist.Add(cztj);
                                                                }
                                                                if (tccs_zczxglist.Count > 0)
                                                                {
                                                                    tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                }
                                                            }
                                                        }
                                                        tccs_itemlist.Add(tccs_item);

                                                        if (calccodestrcref.Count() > 0)
                                                        {
                                                            foreach (string calcref in calccodestrcref)
                                                            {
                                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemref = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                                tccs_itemref.ACTION = ACTION_C;
                                                                tccs_itemref.KEY = SQMTMInterface.genITEMKEY();
                                                                tccs_itemref.LINENR = line_auto.ToString();
                                                                line_auto++;
                                                                tccs_itemref.CALC_REF_LINE_NO = tccs_item.LINENR;
                                                                tccs_itemref.CALC_REF_TO_NO = tccs_item.LINENR;

                                                                //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                                if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                                {
                                                                    tccs_itemref.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                                }
                                                                else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                                {
                                                                    tccs_itemref.TCCALCRESINS040 = "COST";//指令类型 
                                                                    tccs_itemref.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                                    tccs_itemref.COST_PULL_STRATEGY = "2";
                                                                    tccs_itemref.SOURCE_CHARGE = "3";
                                                                }
                                                                else
                                                                {
                                                                    tccs_itemref.TCCALCRESINS040 = "STND";//指令类型 
                                                                }

                                                                if (drpsf["BJFS"] + "" == "1")// AT COST
                                                                {

                                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_costref = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                                    item_costref.ACTION = ACTION_C;
                                                                    item_costref.TCET084 = feecode;

                                                                    item_costlist.Add(item_costref);

                                                                    //tccs_itemref.ITEM_COST = item_costlist.ToArray();
                                                                }
                                                                else
                                                                {
                                                                    tccs_itemref.TCET084 = feecode;
                                                                }

                                                                if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                                {
                                                                    tccs_itemref.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                                }
                                                                tccs_itemref.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                                tccs_itemref.CURRCODE016 = "%";//货币 币种

                                                                bool isex = true;
                                                                string strlast = "";
                                                                foreach (DataRow bjcd in bjvalsdt.Rows)
                                                                {
                                                                    if (string.IsNullOrEmpty(strlast))
                                                                    {
                                                                        strlast = bjcd[calcref + "CODE"] + "";
                                                                    }
                                                                    else if (strlast != bjcd[calcref + "CODE"] + "")
                                                                    {
                                                                        isex = false;
                                                                        break;
                                                                    }
                                                                    if (STR_ZZCFTYZ != calcref)
                                                                    {
                                                                        if ("A" == sqmtminterface.getCACL_TYP(bjcd[calcref + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                if (isex)
                                                                {
                                                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                                    item_calcrule.ACTION = ACTION_C;
                                                                    item_calcrule.CALC_BASE_CODE = calcref;
                                                                    item_calcrule.QTY_VALUE = "1";
                                                                    item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";
                                                                    if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                    {
                                                                        item_calcrule.QTY_UNIT_C = "EA";
                                                                    }
                                                                    item_calcrulelist.Add(item_calcrule);
                                                                    tccs_itemref.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                    tccs_itemref.AMOUNT = "100";
                                                                    //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                    tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                    tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                                    if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                                    {
                                                                        tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                    }

                                                                    tccs_itemlist.Add(tccs_itemref);

                                                                    continue;
                                                                }
                                                                //tccs_itemref.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                                BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                                rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                                Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                                Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                                string str_rate_id_ref = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                                rate601.RATE_ID = str_rate_id_ref;//费率表ID，长度20
                                                                rate601.TCUSAGECD085 = "3";
                                                                rate601.TIMEZONE = "UTC+8";
                                                                rate601.TCET = feecode; //"费目代码"
                                                                rate601.VAL_INDICATOR = "P"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                                rate601.RATE_TAB_TYPE = "ZFW1";
                                                                rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                                rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                                Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                                orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : "";  //代运组织（根据产品事业部）
                                                                orgdatalist.Add(orgdata);
                                                                rate601.ORG_DATA = orgdatalist.ToArray();

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                                Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                                ratescale.DIMENSION_INDX = "1"; //标度维数

                                                                ratescale.CALC_BASE = calcref; //"计算基础代码";
                                                                if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                {
                                                                    ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                                }
                                                                else
                                                                {
                                                                    ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                                }
                                                                ratescale.SCALE_UOM = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";

                                                                ratescale.INITVAL_SUPPORT = "X";
                                                                //ratescale.MINVAL_SUPPORTED = "X";
                                                                //ratescale.MAXVAL_SUPPORTED = "X";
                                                                ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                                if ("COMMODITY_CODE" == calcref)
                                                                {
                                                                    ratescale.CCODE_TYPE = "IN";
                                                                }
                                                                ratescalelist.Add(ratescale);
                                                                rate601.RATESCALE = ratescalelist.ToArray();

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                                //foreach
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                                //validity.ZNUMBER = "";//待定
                                                                validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                                validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                                validity.CURRENCY = "%";


                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                                //foreach
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                                calcruleref.CALC_BASE_CODE = calcref;//定价模块-费目标准报价方式-标识
                                                                quantity.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + ""; //计量单位
                                                                quantity.QTY_VALUE = "1";//默认传1
                                                                calcruleref.QUANTITY = quantity;
                                                                //calcruleref.ROUND_RULE = "";//默认为空
                                                                //不传 calcruleref.FOR_REL_SCLITM = "";
                                                                calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                                calcrulereflist.Add(calcruleref);
                                                                validity.CALCRULEREF = calcrulereflist.ToArray();


                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                                //foreach
                                                                if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                {
                                                                    List<string> hadvallist = new List<string>();
                                                                    foreach (DataRow drval in bjvalsdt.Rows)
                                                                    {
                                                                        if (!hadvallist.Contains(drval[calcref + "CODE"] + ""))
                                                                        {
                                                                            hadvallist.Add(drval[calcref + "CODE"] + "");
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                            scale_item1.SCALE_ITEM = drval[calcref + "CODE"] + "";//标度值字符
                                                                            scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(calcref, scale_item1.SCALE_ITEM);
                                                                            scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[calcref + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = "100";//金额
                                                                            rates_dim.SCALE_ITEM1 = scale_item1;
                                                                            rates_dimlist.Add(rates_dim);
                                                                        }
                                                                    }
                                                                }
                                                                validity.RATES_DIM = rates_dimlist.ToArray();

                                                                validitylist.Add(validity);

                                                                rate601.VALIDITY = validitylist.ToArray();

                                                                rate601list.Add(rate601);

                                                                rate601create.IT_RATE = rate601list.ToArray();

                                                                Rate601Patched(ref rate601create);
                                                                Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                                if (resrate != null && resrate.Count() > 0)
                                                                {
                                                                    foreach (var rr in resrate)
                                                                    {
                                                                        if (null != rr.MSG)
                                                                        {
                                                                            foreach (var rm in rr.MSG)
                                                                            {
                                                                                if ("E" == rm.MSG_TYPE)
                                                                                {
                                                                                    rateflag = false;
                                                                                    ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                                }
                                                                            }

                                                                            if (!rateflag)
                                                                            {
                                                                                goto rtnLabel;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                tccs_itemref.RATE_ID = str_rate_id_ref;

                                                                if (tccs_itemref.TCCALCRESINS040 == "STND")
                                                                {
                                                                    tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                    tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                                }
                                                                //tccs_itemref.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                                {
                                                                    tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                }

                                                                tccs_itemlist.Add(tccs_itemref);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                List<string> gdzidlist = new List<string>();
                                                foreach (DataRow drzbval in zbvaldt.Rows)
                                                {
                                                    string ss = drzbval["GDZRID"] + "";
                                                    if (!gdzidlist.Contains(ss))
                                                    {
                                                        gdzidlist.Add(ss);
                                                    }
                                                }


                                                string strHLAsql = string.Format("select GDZKEY from sqm_fee_pur_ref where GDZRID = '{0}' ", gdzidlist[0]);
                                                string strHLA = (string)DataHelper.QueryValue(strHLAsql);

                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemeval = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                tccs_itemeval.ACTION = ACTION_C;
                                                tccs_itemeval.KEY = SQMTMInterface.genITEMKEY();
                                                tccs_itemeval.LINENR = line_auto.ToString();
                                                line_auto++;
                                                tccs_itemeval.TCCALCRESINS040 = "EVAL";
                                                tccs_itemeval.OPERATIONCD102 = strHLA;

                                                tccs_itemlist.Add(tccs_itemeval);

                                                foreach (string strgdzid in gdzidlist)
                                                {
                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemsum = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                    tccs_itemsum.ACTION = ACTION_C;
                                                    tccs_itemsum.KEY = SQMTMInterface.genITEMKEY();
                                                    tccs_itemsum.LINENR = line_auto.ToString();
                                                    line_auto++;
                                                    tccs_itemsum.TCCALCRESINS040 = "SUM";
                                                    tccs_itemsum.TARGET_ITEM_KEY = tccs_itemeval.KEY;
                                                    tccs_itemlist.Add(tccs_itemsum);

                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemgdz = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                    tccs_itemgdz.ACTION = ACTION_C;
                                                    tccs_itemgdz.KEY = SQMTMInterface.genITEMKEY();
                                                    tccs_itemgdz.LINENR = line_auto.ToString();
                                                    line_auto++;
                                                    tccs_itemgdz.TARGET_ITEM_KEY = tccs_itemsum.KEY;//目标ITEM KEY    自己编关系
                                                    //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                    if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                    {
                                                        tccs_itemgdz.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                    }
                                                    else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                    {
                                                        tccs_itemgdz.TCCALCRESINS040 = "COST";//指令类型 
                                                        tccs_itemgdz.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                        tccs_itemgdz.COST_PULL_STRATEGY = "2";
                                                        tccs_itemgdz.SOURCE_CHARGE = "3";
                                                    }
                                                    else
                                                    {
                                                        tccs_itemgdz.TCCALCRESINS040 = "STND";//指令类型 
                                                    }

                                                    if (CZJSJS_FEES.Contains(feecode))
                                                    {
                                                        tccs_itemgdz.ZCZJS_ROLE = js_role;//仓租结算角色   报价系统费目新增
                                                    }
                                                    else
                                                    {
                                                        tccs_itemgdz.ZSETTLE_ROLE = js_role;//结算角色   报价费目（与结算方互斥）
                                                    }
                                                    tccs_itemgdz.ZSETTLE_OBJ = js_obj;//结算方    报价费目
                                                    //tccs_item.OPERATIONCD102 = "";//费用项目操作   比较高低值时H、L，顺序检查
                                                    //tccs_item.COST_PULL_STRATEGY = "";//成本拉式策略   COST时传2，其他不传
                                                    //tccs_item.SOURCE_CHARGE = "";//费用源    COST时传3，其他不传
                                                    //if ("STND" == tccs_item.TCCALCRESINS040)
                                                    //{
                                                    if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                    {
                                                        tccs_itemgdz.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                    }
                                                    //}
                                                    //tccs_item.CLCRESBAS036 = "";//计算解析基础    PKG、SERVICE等
                                                    tccs_itemgdz.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                    //tccs_item.RULE101 = "";//前提条件规则
                                                    //tccs_item.AMOUNT = "";//简单报价金额（跟费率表 、费率表确定规则 互斥）
                                                    tccs_itemgdz.CURRCODE016 = "CNY";//货币 币种

                                                    //tccs_item.RATE_ID = "";//费率表 601
                                                    //tccs_item.MIN_AMOUNT = "";//费目的最小值
                                                    //tccs_item.MAX_AMOUNT = "";//费目的最高值
                                                    //tccs_item.RULE099 = "";//费率表确定规则    报价费目

                                                    if (drpsf["BJFS"] + "" == "1")// AT COST
                                                    {
                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                        item_cost.ACTION = ACTION_C;
                                                        item_cost.TCET084 = feecode;

                                                        item_costlist.Add(item_cost);

                                                        //tccs_itemgdz.ITEM_COST = item_costlist.ToArray();
                                                        //tccs_itemlist.Add(tccs_itemgdz);
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        tccs_itemgdz.TCET084 = feecode;
                                                    }

                                                    List<string> hadrate = new List<string>();

                                                    foreach (DataRow zbdr in zbvaldt.Rows)
                                                    {
                                                        djfsrid = zbdr["DJFSRID"].ToString();
                                                        gdzrid = zbdr["GDZRID"].ToString();

                                                        if (gdzrid != strgdzid)
                                                        { continue; }

                                                        if (hadrate.Contains(djfsrid + gdzrid))
                                                        {
                                                            continue;
                                                        }

                                                        hadrate.Add(djfsrid + gdzrid);

                                                        if (string.IsNullOrEmpty(gdzrid))
                                                        {

                                                        }
                                                        else
                                                        {
                                                            string minpriceSQL = "";
                                                            if (!string.IsNullOrEmpty(djfsrid))
                                                            {
                                                                minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID = '{1}'", feecode, djfsrid);
                                                            }
                                                            else
                                                            {
                                                                minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID is null ", feecode);
                                                            }
                                                            string minprice = DataHelper.QueryValue(minpriceSQL) + "";
                                                            bMIN = false;
                                                            if (minprice == "1")
                                                            {
                                                                bMIN = true;
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

                                                            SQMTMInterface sqmtminterface = new SQMTMInterface();

                                                            List<string> calccodestrc = new List<string>();
                                                            List<string> calccodestrcref = new List<string>();
                                                            fieldkeys = sqmtminterface.getFieldKeys(bjrid, bMIN, where, ref calccodestrc);
                                                            sql = "select " + fieldkeys + " from SQM_MODEBJ_VAL where ifbjitem = '1' and FEECALCID='{0}' and STATUS='1' {1}";
                                                            sql = string.Format(sql, bjrid, wheredt);
                                                            DataTable bjvalsdt = null;
                                                            bjvalsdt = DataHelper.QueryDataTable(sql);

                                                            if (calccodestrc.Count == 0)
                                                            {
                                                                if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                                {
                                                                    continue;
                                                                }
                                                            }

                                                            if (bMIN)
                                                            {
                                                                if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1)
                                                                {
                                                                    if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                                    {
                                                                        continue;
                                                                    }

                                                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                                    item_calcrule.ACTION = ACTION_C;
                                                                    item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                                    item_calcrule.QTY_VALUE = "1";
                                                                    item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                                    if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                    {
                                                                        item_calcrule.QTY_UNIT_C = "EA";
                                                                    }
                                                                    item_calcrulelist.Add(item_calcrule);
                                                                    tccs_itemgdz.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                    tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                                    tccs_itemgdz.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                    tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                    tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                                    if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                                    {
                                                                        tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                    }
                                                                    if (bJSFFZS)
                                                                    {
                                                                        tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                        tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                        string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                        string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                        if (!String.IsNullOrEmpty(djfsrid))
                                                                        {
                                                                            czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                            cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                        }
                                                                        else
                                                                        {
                                                                            czcxsql += " and DJFSRID is null ";
                                                                            cztjsql += " and DJFSRID is null ";
                                                                        }
                                                                        if (!String.IsNullOrEmpty(gdzrid))
                                                                        {
                                                                            czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                            cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                        }
                                                                        else
                                                                        {
                                                                            czcxsql += " and GDZRID is null ";
                                                                            cztjsql += " and GDZRID is null ";
                                                                        }
                                                                        DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                        DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                        if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                        {
                                                                            tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                            tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                            tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                            tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                            tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                        }

                                                                        if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                        {
                                                                            List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                            foreach (DataRow drcztj in dtcztj.Rows)
                                                                            {
                                                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                                cztj.ACTION = ACTION_C;
                                                                                cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                                cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                                cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                                tccs_zczxglist.Add(cztj);
                                                                            }
                                                                            if (tccs_zczxglist.Count > 0)
                                                                            {
                                                                                tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                            }
                                                                        }
                                                                    }
                                                                    tccs_itemlist.Add(tccs_itemgdz);

                                                                    continue;
                                                                }
                                                            }
                                                            if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1
                                                                && bjvalsdt.Rows[0][calccodestrc[0] + "ISCNT"] + "" == "是")
                                                            {
                                                                if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                                {
                                                                    continue;
                                                                }

                                                                List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                                item_calcrule.ACTION = ACTION_C;
                                                                item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                                item_calcrule.QTY_VALUE = "1";
                                                                item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                                if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                {
                                                                    item_calcrule.QTY_UNIT_C = "EA";
                                                                }
                                                                item_calcrulelist.Add(item_calcrule);
                                                                tccs_itemgdz.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                                tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                                if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                                {
                                                                    tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                }
                                                                if (bJSFFZS)
                                                                {
                                                                    tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                    tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                    string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                    string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                    if (!String.IsNullOrEmpty(djfsrid))
                                                                    {
                                                                        czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                        cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                    }
                                                                    else
                                                                    {
                                                                        czcxsql += " and DJFSRID is null ";
                                                                        cztjsql += " and DJFSRID is null ";
                                                                    }
                                                                    if (!String.IsNullOrEmpty(gdzrid))
                                                                    {
                                                                        czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                        cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                    }
                                                                    else
                                                                    {
                                                                        czcxsql += " and GDZRID is null ";
                                                                        cztjsql += " and GDZRID is null ";
                                                                    }
                                                                    DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                    DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                    if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                    {
                                                                        tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                        tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                        tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                        tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                        tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                    }

                                                                    if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                    {
                                                                        List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                        foreach (DataRow drcztj in dtcztj.Rows)
                                                                        {
                                                                            Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                            cztj.ACTION = ACTION_C;
                                                                            cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                            cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                            cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                            tccs_zczxglist.Add(cztj);
                                                                        }
                                                                        if (tccs_zczxglist.Count > 0)
                                                                        {
                                                                            tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                        }
                                                                    }
                                                                }
                                                                tccs_itemlist.Add(tccs_itemgdz);

                                                                continue;
                                                            }

                                                            if (calccodestrc.Count > 0)
                                                            {
                                                                BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                                rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                                Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                                Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                                string str_rate_id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                                rate601.RATE_ID = str_rate_id;//费率表ID，长度20
                                                                rate601.TCUSAGECD085 = "3";
                                                                rate601.TIMEZONE = "UTC+8";
                                                                rate601.TCET = feecode; //"费目代码"
                                                                rate601.VAL_INDICATOR = "A"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                                rate601.RATE_TAB_TYPE = "ZFW1";
                                                                rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                                rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                                Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                                orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                                orgdatalist.Add(orgdata);
                                                                rate601.ORG_DATA = orgdatalist.ToArray();

                                                                List<string> calcexcludelist = new List<string>();
                                                                List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                                int indx = 0;
                                                                int bcntadd = 0;

                                                                bool multiMin = bMIN && bjvalsdt.DefaultView.ToTable(true, "MINBJPRICE").Rows.Count > 1;

                                                                foreach (var ccoders in calccodestrc)
                                                                {
                                                                    if (STR_ZZCFTYZ == ccoders)
                                                                    {
                                                                        calccodestrcref.Add(STR_ZZCFTYZ);
                                                                        continue;
                                                                    }

                                                                    if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是")
                                                                    {
                                                                        bcntadd += 1;
                                                                        if (bcntadd > 1)
                                                                        {
                                                                            continue;
                                                                        }
                                                                    }

                                                                    bool isex = true;
                                                                    string strlast = "";
                                                                    foreach (DataRow bjcd in bjvalsdt.Rows)
                                                                    {
                                                                        if (string.IsNullOrEmpty(strlast))
                                                                        {
                                                                            strlast = bjcd[ccoders + "CODE"] + "";
                                                                        }
                                                                        else if (strlast != bjcd[ccoders + "CODE"] + "")
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                        if (STR_ZZCFTYZ != ccoders)
                                                                        {
                                                                            if ("A" == sqmtminterface.getCACL_TYP(bjcd[ccoders + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                            {
                                                                                isex = false;
                                                                                break;
                                                                            }
                                                                        }
                                                                        //仓租费计费数量为“是”的传到标度值里
                                                                        if (feecode == "S1000CZF0001" && bjcd[ccoders + "ISCNT"] + "" == "是")
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (isex && !multiMin)
                                                                    {
                                                                        calcexcludelist.Add(ccoders);
                                                                        continue;
                                                                    }

                                                                    indx++;

                                                                    Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                                    ratescale.DIMENSION_INDX = indx.ToString(); //标度维数

                                                                    ratescale.CALC_BASE = ccoders; //"计算基础代码";
                                                                    if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                    {
                                                                        ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                                    }
                                                                    else
                                                                    {
                                                                        ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                                    }
                                                                    ratescale.SCALE_UOM = bjvalsdt.Rows[0][ccoders + "MSRCODE"] + "";

                                                                    ratescale.INITVAL_SUPPORT = "X";
                                                                    //ratescale.MINVAL_SUPPORTED = "X";
                                                                    //ratescale.MAXVAL_SUPPORTED = "X";
                                                                    if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是" && STR_JTLJ == bjvalsdt.Rows[0]["JTLJ"] + "")//阶梯累计特殊处理
                                                                    {
                                                                        ratescale.REL_FOR_WGTBRK = "X";
                                                                        tccs_itemgdz.CALC_METH_CODE = "2";
                                                                    }

                                                                    ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                                    if ("COMMODITY_CODE" == ccoders)
                                                                    {
                                                                        ratescale.CCODE_TYPE = "IN";
                                                                    }
                                                                    ratescalelist.Add(ratescale);
                                                                }
                                                                rate601.RATESCALE = ratescalelist.ToArray();

                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                                //foreach
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                                //validity.ZNUMBER = "";//待定
                                                                validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                                validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                                validity.CURRENCY = bjvalsdt.Rows[0]["CURRENCY"] + "";//货币


                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                                int iscnt = 0;
                                                                foreach (var ccodecrr in calccodestrc)
                                                                {
                                                                    if (STR_ZZCFTYZ == ccodecrr)
                                                                    {
                                                                        continue;
                                                                    }

                                                                    if (bjvalsdt.Rows[0][ccodecrr + "ISCNT"] + "" == "是")
                                                                    {
                                                                        iscnt++;
                                                                        if (iscnt > 1)
                                                                        {
                                                                            calccodestrcref.Add(ccodecrr);
                                                                            continue;
                                                                        }

                                                                        //foreach
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                                        calcruleref.CALC_BASE_CODE = ccodecrr;//定价模块-费目标准报价方式-标识
                                                                        quantity.QTY_UNIT_C = bjvalsdt.Rows[0][ccodecrr + "MSRCODE"] + ""; //计量单位
                                                                        quantity.QTY_VALUE = "1";//默认传1
                                                                        calcruleref.QUANTITY = quantity;
                                                                        //calcruleref.ROUND_RULE = "";//默认为空
                                                                        //不传 calcruleref.FOR_REL_SCLITM = "";
                                                                        calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                                        calcrulereflist.Add(calcruleref);
                                                                    }
                                                                }
                                                                validity.CALCRULEREF = calcrulereflist.ToArray();


                                                                List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();

                                                                //foreach
                                                                if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                {
                                                                    foreach (DataRow drval in bjvalsdt.Rows)
                                                                    {
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dimMin = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                        int cnt = 0;
                                                                        foreach (var ccoderd in calccodestrc)
                                                                        {
                                                                            if (calcexcludelist.Contains(ccoderd) && !multiMin)
                                                                            {
                                                                                continue;
                                                                            }
                                                                            if (calccodestrcref.Contains(ccoderd))
                                                                            {
                                                                                continue;
                                                                            }

                                                                            cnt++;

                                                                            switch (cnt)
                                                                            {
                                                                                case 1:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                    //scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                    scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM1 = scale_item1;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 2:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                    scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM2 = scale_item2;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 3:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                    scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM3 = scale_item3;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 4:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                    scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM4 = scale_item4;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 5:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                    scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM5 = scale_item5;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 6:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                    scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM6 = scale_item6;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 7:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                    scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM7 = scale_item7;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 8:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                    scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM8 = scale_item8;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 9:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                    scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM9 = scale_item9;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 10:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                    scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM10 = scale_item10;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 11:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                    scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM11 = scale_item11;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 12:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                    scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM12 = scale_item12;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 13:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                    scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM13 = scale_item13;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 14:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                    if (feecode == "S1000CZF0001")
                                                                                    {
                                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    }
                                                                                    scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                                    scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM14 = scale_item14;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                default:
                                                                                    break;
                                                                            }

                                                                            if (multiMin)
                                                                            {
                                                                                switch (cnt)
                                                                                {
                                                                                    case 1:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                        scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM1 = scale_item1;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item1.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item1.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 2:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                        scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM2 = scale_item2;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item2.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item2.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 3:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                        scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM3 = scale_item3;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item3.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item3.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 4:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                        scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM4 = scale_item4;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item4.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item4.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 5:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                        scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM5 = scale_item5;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item5.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item5.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 6:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                        scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM6 = scale_item6;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item6.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item6.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 7:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                        scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM7 = scale_item7;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item7.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item7.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 8:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                        scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM8 = scale_item8;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item8.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item8.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 9:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                        scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM9 = scale_item9;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item9.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item9.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 10:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                        scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM10 = scale_item10;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item10.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item10.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 11:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                        scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM11 = scale_item11;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item11.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item11.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 12:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                        scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM12 = scale_item12;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item12.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item12.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 13:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                        scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM13 = scale_item13;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item13.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item13.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    case 14:
                                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                        if (feecode == "S1000CZF0001")
                                                                                        {
                                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                        }
                                                                                        scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                                        scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                        rates_dimMin.SCALE_ITEM14 = scale_item14;
                                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                        rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                        if (scale_item14.CALC_TYP == "B")
                                                                                        {
                                                                                            scale_item14.SCALE_ITEM = "1-";
                                                                                        }
                                                                                        break;
                                                                                    default:
                                                                                        break;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (multiMin)
                                                                        {
                                                                            foreach (Z2FM_SQ_RATE_CREATEIT_RATERATESCALE rs in rate601.RATESCALE)
                                                                            {
                                                                                if (rs.SCATYP == "B")
                                                                                {
                                                                                    rs.MINVAL_SUPPORTED = "X";
                                                                                }
                                                                            }
                                                                            rates_dimlist.Add(rates_dimMin);
                                                                        }
                                                                        rates_dimlist.Add(rates_dim);
                                                                    }
                                                                }
                                                                validity.RATES_DIM = rates_dimlist.ToArray();

                                                                validitylist.Add(validity);

                                                                rate601.VALIDITY = validitylist.ToArray();

                                                                rate601list.Add(rate601);

                                                                rate601create.IT_RATE = rate601list.ToArray();

                                                                Rate601Patched(ref rate601create);
                                                                Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                                if (resrate != null && resrate.Count() > 0)
                                                                {
                                                                    foreach (var rr in resrate)
                                                                    {
                                                                        if (null != rr.MSG)
                                                                        {
                                                                            foreach (var rm in rr.MSG)
                                                                            {
                                                                                if ("E" == rm.MSG_TYPE)
                                                                                {
                                                                                    rateflag = false;
                                                                                    ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                                }
                                                                            }

                                                                            if (!rateflag)
                                                                            {
                                                                                goto rtnLabel;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                tccs_itemgdz.RATE_ID = str_rate_id;//费率表 601
                                                            }
                                                            else
                                                            {
                                                                tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                            }

                                                            if (tccs_itemgdz.TCCALCRESINS040 == "STND")
                                                            {
                                                                tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                            }
                                                            //tccs_itemgdz.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                            if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                            {
                                                                tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                            }
                                                            if (calccodestrcref.Count > 0)
                                                            {
                                                                tccs_itemgdz.ANALYTICRELEV = "X";
                                                            }
                                                            if (bJSFFZS)
                                                            {
                                                                tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                if (!String.IsNullOrEmpty(djfsrid))
                                                                {
                                                                    czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                    cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and DJFSRID is null ";
                                                                    cztjsql += " and DJFSRID is null ";
                                                                }
                                                                if (!String.IsNullOrEmpty(gdzrid))
                                                                {
                                                                    czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                    cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and GDZRID is null ";
                                                                    cztjsql += " and GDZRID is null ";
                                                                }
                                                                DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                {
                                                                    tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                    tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                    tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                    tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                    tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                }

                                                                if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                {
                                                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                    foreach (DataRow drcztj in dtcztj.Rows)
                                                                    {
                                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                        cztj.ACTION = ACTION_C;
                                                                        cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                        cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                        cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                        tccs_zczxglist.Add(cztj);
                                                                    }
                                                                    if (tccs_zczxglist.Count > 0)
                                                                    {
                                                                        tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                    }
                                                                }
                                                            }
                                                            tccs_itemlist.Add(tccs_itemgdz);

                                                            if (calccodestrcref.Count() > 0)
                                                            {
                                                                foreach (string calcref in calccodestrcref)
                                                                {
                                                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemref = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                                    tccs_itemref.ACTION = ACTION_C;
                                                                    tccs_itemref.KEY = SQMTMInterface.genITEMKEY();
                                                                    tccs_itemref.LINENR = line_auto.ToString();
                                                                    line_auto++;
                                                                    tccs_itemref.CALC_REF_LINE_NO = tccs_itemgdz.LINENR;
                                                                    tccs_itemref.CALC_REF_TO_NO = tccs_itemgdz.LINENR;
                                                                    //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                                    if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                                    {
                                                                        tccs_itemref.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                                    }
                                                                    else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                                    {
                                                                        tccs_itemref.TCCALCRESINS040 = "COST";//指令类型 
                                                                        tccs_itemref.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                                        tccs_itemref.COST_PULL_STRATEGY = "2";
                                                                        tccs_itemref.SOURCE_CHARGE = "3";
                                                                    }
                                                                    else
                                                                    {
                                                                        tccs_itemref.TCCALCRESINS040 = "STND";//指令类型 
                                                                    }

                                                                    if (drpsf["BJFS"] + "" == "1")// AT COST
                                                                    {
                                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_costref = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                                        item_costref.ACTION = ACTION_C;
                                                                        item_costref.TCET084 = feecode;

                                                                        item_costlist.Add(item_costref);

                                                                        //tccs_itemref.ITEM_COST = item_costlistref.ToArray();
                                                                    }
                                                                    else
                                                                    {
                                                                        tccs_itemref.TCET084 = feecode;
                                                                    }

                                                                    if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                                    {
                                                                        tccs_itemref.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                                    }
                                                                    tccs_itemref.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                                    tccs_itemref.CURRCODE016 = "%";//货币 币种
                                                                    //tccs_itemref.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                                    bool isex = true;
                                                                    string strlast = "";
                                                                    foreach (DataRow bjcd in bjvalsdt.Rows)
                                                                    {
                                                                        if (string.IsNullOrEmpty(strlast))
                                                                        {
                                                                            strlast = bjcd[calcref + "CODE"] + "";
                                                                        }
                                                                        else if (strlast != bjcd[calcref + "CODE"] + "")
                                                                        {
                                                                            isex = false;
                                                                            break;
                                                                        }
                                                                        if (STR_ZZCFTYZ != calcref)
                                                                        {
                                                                            if ("A" == sqmtminterface.getCACL_TYP(bjcd[calcref + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                            {
                                                                                isex = false;
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (isex)
                                                                    {
                                                                        List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();
                                                                        item_calcrule.ACTION = ACTION_C;
                                                                        item_calcrule.CALC_BASE_CODE = calcref;
                                                                        item_calcrule.QTY_VALUE = "1";
                                                                        item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";
                                                                        if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                        {
                                                                            item_calcrule.QTY_UNIT_C = "EA";
                                                                        }
                                                                        item_calcrulelist.Add(item_calcrule);
                                                                        tccs_itemref.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                        tccs_itemref.AMOUNT = "100";
                                                                        //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                        tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                        tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                                        if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                                        {
                                                                            tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                        }
                                                                        tccs_itemlist.Add(tccs_itemref);

                                                                        continue;
                                                                    }

                                                                    BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                                    rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                                    Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                                    Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                                    string str_rate_id_ref = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                                    rate601.RATE_ID = str_rate_id_ref;//费率表ID，长度20
                                                                    rate601.TCUSAGECD085 = "3";
                                                                    rate601.TIMEZONE = "UTC+8";
                                                                    rate601.TCET = feecode; //"费目代码"
                                                                    rate601.VAL_INDICATOR = "P"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                                    rate601.RATE_TAB_TYPE = "ZFW1";
                                                                    rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                                    rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                                    orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                                    orgdatalist.Add(orgdata);
                                                                    rate601.ORG_DATA = orgdatalist.ToArray();

                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                                    ratescale.DIMENSION_INDX = "1"; //标度维数

                                                                    ratescale.CALC_BASE = calcref; //"计算基础代码";
                                                                    if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                    {
                                                                        ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                                    }
                                                                    else
                                                                    {
                                                                        ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                                    }
                                                                    ratescale.SCALE_UOM = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";

                                                                    ratescale.INITVAL_SUPPORT = "X";
                                                                    //ratescale.MINVAL_SUPPORTED = "X";
                                                                    //ratescale.MAXVAL_SUPPORTED = "X";
                                                                    ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                                    if ("COMMODITY_CODE" == calcref)
                                                                    {
                                                                        ratescale.CCODE_TYPE = "IN";
                                                                    }
                                                                    ratescalelist.Add(ratescale);
                                                                    rate601.RATESCALE = ratescalelist.ToArray();

                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                                    //foreach
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                                    //validity.ZNUMBER = "";//待定
                                                                    validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                                    validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                                    validity.CURRENCY = "%";


                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                                    //foreach
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                                    calcruleref.CALC_BASE_CODE = calcref;//定价模块-费目标准报价方式-标识
                                                                    quantity.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + ""; //计量单位
                                                                    quantity.QTY_VALUE = "1";//默认传1
                                                                    calcruleref.QUANTITY = quantity;
                                                                    //calcruleref.ROUND_RULE = "";//默认为空
                                                                    //不传 calcruleref.FOR_REL_SCLITM = "";
                                                                    calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                                    calcrulereflist.Add(calcruleref);
                                                                    validity.CALCRULEREF = calcrulereflist.ToArray();


                                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                                    //foreach
                                                                    if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                                    {
                                                                        List<string> hadvallist = new List<string>();
                                                                        foreach (DataRow drval in bjvalsdt.Rows)
                                                                        {
                                                                            if (!hadvallist.Contains(drval[calcref + "CODE"] + ""))
                                                                            {
                                                                                hadvallist.Add(drval[calcref + "CODE"] + "");
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                scale_item1.SCALE_ITEM = drval[calcref + "CODE"] + "";//标度值字符
                                                                                scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(calcref, scale_item1.SCALE_ITEM);
                                                                                scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[calcref + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = "100";//金额
                                                                                rates_dim.SCALE_ITEM1 = scale_item1;
                                                                                rates_dimlist.Add(rates_dim);
                                                                            }
                                                                        }
                                                                    }
                                                                    validity.RATES_DIM = rates_dimlist.ToArray();

                                                                    validitylist.Add(validity);

                                                                    rate601.VALIDITY = validitylist.ToArray();

                                                                    rate601list.Add(rate601);

                                                                    rate601create.IT_RATE = rate601list.ToArray();

                                                                    Rate601Patched(ref rate601create);
                                                                    Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                                    if (resrate != null && resrate.Count() > 0)
                                                                    {
                                                                        foreach (var rr in resrate)
                                                                        {
                                                                            if (null != rr.MSG)
                                                                            {
                                                                                foreach (var rm in rr.MSG)
                                                                                {
                                                                                    if ("E" == rm.MSG_TYPE)
                                                                                    {
                                                                                        rateflag = false;
                                                                                        ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                                    }
                                                                                }

                                                                                if (!rateflag)
                                                                                {
                                                                                    goto rtnLabel;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    tccs_itemref.RATE_ID = str_rate_id_ref;

                                                                    if (tccs_itemref.TCCALCRESINS040 == "STND")
                                                                    {
                                                                        tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                        tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                                    }
                                                                    //tccs_itemref.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                    if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                                    {
                                                                        tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                    }
                                                                    tccs_itemlist.Add(tccs_itemref);
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (drpsf["BJFS"] + "" == "1")
                                    {
                                        //Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemcost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                        //tccs_itemcost.KEY = SQMTMInterface.genITEMKEY();
                                        //tccs_itemcost.LINENR = line_auto.ToString();
                                        //line_auto++;
                                        //tccs_itemcost.TCCALCRESINS040 = "COST";//指令类型 
                                        ////tccs_itemcost.CLCRESBAS036 = drpsf["JXJC"] + "";
                                        //tccs_itemcost.COST_PULL_STRATEGY = "2";
                                        //tccs_itemcost.SOURCE_CHARGE = "3";

                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                        item_cost.ACTION = ACTION_C;
                                        item_cost.TCET084 = feecode;

                                        item_costlist.Add(item_cost);

                                        //tccs_itemcost.ITEM_COST = item_costlist.ToArray();

                                        //tccs_itemlist.Add(tccs_itemcost);
                                    }
                                }
                                if (item_costlist.Count > 0)
                                {
                                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemcost = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                    tccs_itemcost.ACTION = ACTION_C;
                                    tccs_itemcost.KEY = SQMTMInterface.genITEMKEY();
                                    tccs_itemcost.LINENR = line_auto.ToString();
                                    line_auto++;
                                    tccs_itemcost.TCCALCRESINS040 = "COST";//指令类型 
                                    //tccs_itemcost.CLCRESBAS036 = drpsf["JXJC"] + "";
                                    tccs_itemcost.COST_PULL_STRATEGY = "2";
                                    tccs_itemcost.SOURCE_CHARGE = "3";

                                    List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST> item_costlistfinal = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST>();
                                    foreach (Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST costorg in item_costlist)
                                    {
                                        if (item_costlistfinal.Where(x => x.TCET084 == costorg.TCET084).Count() < 1)
                                        {
                                            item_costlistfinal.Add(costorg);
                                            string costrefsql = string.Format(" SELECT FEECODE FROM SQM_COST_REF WHERE GROUPID IN (SELECT GROUPID FROM SQM_COST_REF WHERE FEECODE = '{0}') AND FEECODE != '{0}' ", costorg.TCET084);
                                            DataTable dtcostref = DataHelper.QueryDataTable(costrefsql);
                                            if (null != dtcostref && dtcostref.Rows.Count > 0)
                                                foreach (DataRow drothcost in dtcostref.Rows)
                                                {
                                                    if (item_costlistfinal.Where(x => x.TCET084 == drothcost["FEECODE"] + "").Count() < 1)
                                                    {
                                                        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST costfinal = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                        costfinal.ACTION = ACTION_C;
                                                        costfinal.CHRGCATCD021_I = costorg.CHRGCATCD021_I;
                                                        costfinal.KEY = costorg.KEY;
                                                        costfinal.TCCLASS037 = costorg.TCCLASS037;
                                                        costfinal.TCET084 = drothcost["FEECODE"] + "";
                                                        item_costlistfinal.Add(costfinal);
                                                    }
                                                }
                                        }
                                    }
                                    //tccs_itemcost.ITEM_COST = item_costlist.ToArray();
                                    tccs_itemcost.ITEM_COST = item_costlistfinal.ToArray();
                                    tccs_itemlist.Add(tccs_itemcost);
                                }
                                tccs_root.TCCS_ITEM = tccs_itemlist.ToArray();
                                fag_item100702.TCCS_ROOT = tccs_root;
                            }

                            fwa702.FAG_ITEM = fag_itemlist702.ToArray();
                            fwa702list.Add(fwa702);

                            FWA702Patched(ref fwa702list);
                            Z2FM_SQ_FWA_MODIFY_RES resfwa702 = fwa702service.Exec(fwa702list.ToArray());//协议修改

                            if (resfwa702 != null && resfwa702.ET_MSG != null)
                            {
                                if (resfwa702 != null && resfwa702.ET_MSG != null)
                                {
                                    foreach (var fm in resfwa702.ET_MSG)
                                    {
                                        if ("E" == fm.TYPE)
                                        {
                                            fwaflag = false;
                                            fwamsg += fm.MESSAGE + "<br>";
                                        }
                                    }
                                }
                            }

                            if (!fwaflag)
                            {
                                return Content(new JsonMessage { Success = false, Data = null, Code = "1", Message = fwamsg }.ToString());
                            }

                            //写入ItemNO
                            Z2FM_SQ_FWA_MODIFY_RESET_MSG[] MSG702 = resfwa702.ET_MSG;
                            string item_no_modifylist = "";

                            if (MSG702 == null)
                            {

                                Z2FM_SQ_FWA_MODIFY_RESET_FWA[] res702 = resfwa702.ET_FWA;
                                //写入ItemNO
                                foreach (var items in res702)
                                {
                                    foreach (var itm in items.FAG_ITEM)
                                    {
                                        if (!KeysList.Contains(itm.KEY))
                                        {
                                            if (!string.IsNullOrEmpty(itm.ITEM_TYPE) || !string.IsNullOrEmpty(itm.SERVICE_PRODUCT_ID))
                                            {
                                                item_no_modifylist += itm.ITEM_NO.TrimStart('0') + ",";
                                            }
                                        }
                                    }
                                }
                            }
                            item_no_modifylist = item_no_modifylist.TrimEnd(',');
                            SQM_FWA_REF sqm_fwa_ref702 = new SQM_FWA_REF();
                            sqm_fwa_ref702.MRID = keyvalue;
                            sqm_fwa_ref702.ZVER = zver;
                            sqm_fwa_ref702.FWA = fwa702.FAGRMNTID044;
                            sqm_fwa_ref702.CREATEUSER = SQMHelper.getStaffKey();
                            sqm_fwa_ref702.ITEMNO = item_no_modifylist;
                            sqm_fwa_ref702.DoCreate();
                            fwamsg += "修改协议成功：" + fwa702.FAGRMNTID044 + "<br>";
                            continue;
                            //return Content(new JsonMessage { Success = true, Data = null, Code = "1", Message = "修改协议成功" }.ToString());
                        }
                        catch (Exception ex702)
                        {
                            return Content(new JsonMessage { Success = false, Data = null, Code = "1", Message = ex702.Message }.ToString());
                        }
                    }

                    strFWAFAGRMNTID044 += DateTime.Now.ToString("yyyy").Substring(2, 2);

                    string bj_creatFwa = SQMTMInterface.GenerateFWASerial(strFWAFAGRMNTID044);
                    fwafagrmntid044List.Clear();
                    fwafagrmntid044List.Add(bj_creatFwa);//(sqm_fwa_ref.FWA);
                    fwa.FAGRMNTID044 = bj_creatFwa;//sqm_fwa_ref.FWA;

                    List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM> fag_itemlist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM>();

                    foreach (string product_code in productcodeslist)
                    {
                        if (A2S_PRDS.Contains(product_code))
                        {
                            fwa.FAGTYPEID103 = "Z301";
                        }
                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM fag_item100 = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM();
                        fag_item100.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                        fag_item100.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                        string strHex32 = SQMTMInterface.genITEMKEY();
                        fag_item100.KEY = strHex32;//创建时自己标识
                        //不传，自动生成 fag_item.ITEM_NO = "";//协议项目编号
                        if (fwa.FAGTYPEID103 == "Z101")
                        {
                            try
                            {
                                SQM_ITEMTYPE_REF itemtype = SQM_ITEMTYPE_REF.FindAllByProperty(SQM_ITEMTYPE_REF.Prop_PRODUCT, product_code).FirstOrDefault();
                                fag_item100.ITEM_TYPE = itemtype.ITEMTYPE;
                            }
                            catch
                            {
                                rtnmsg = "未找到产品" + product_code + "对应的项目类型";
                                rtnflag = false;
                                goto rtnLabel;
                            }
                        }
                        else
                        {
                            fag_item100.SERVICE_PRODUCT_ID = product_code;
                        }
                        //fag_item100.MTR = "";
                        //fag_item100.SERVICE_TYPE = "";//服务类型   报价的服务代码
                        //fag_item100.ZTGFS = "";//通关方式
                        //fag_item100.PAR_KEY = ;//上层ITEM KEY    即FAG_ITEM的KEY
                        fag_itemlist.Add(fag_item100);

                        if (fwa.FAGTYPEID103 == "Z101")
                        {
                            foreach (DataRow dr in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                            {
                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM fag_item = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM();
                                fag_item100.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                                fag_item100.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                                prdcode = dr["PRODUCT_CODE"] + "";
                                srvcode = dr["SERVICE_CODE"] + "";

                                //fag_item.SERVICE_PRODUCT_ID = prdcode;//服务产品 报价的产品代码（供应链不传）
                                fag_item.SERVICE_TYPE = srvcode;//服务类型   报价的服务代码
                                fag_item.PAR_KEY = strHex32;// fag_item100.KEY;//上层ITEM KEY    即FAG_ITEM的KEY

                                List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL> ins_list = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL>();
                                string sqlins = string.Format("SELECT INS_ID FROM MDM_INSASN WHERE INSSET_ID IN ( SELECT INS_SET_ID FROM MDM_TSR WHERE SRVRQCD121 = '{0}' ) ", srvcode);
                                int seq = 100;
                                DataTable dtins = DataHelper.QueryDataTable(sqlins);
                                if (null != dtins && dtins.Rows.Count > 0)
                                {
                                    foreach (DataRow drins in dtins.Rows)
                                    {
                                        seq += 10;
                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL();
                                        ins.SEQ_NUMBER = seq.ToString();
                                        ins.INS_ID = drins["INS_ID"] + "";
                                        ins_list.Add(ins);
                                    }
                                }
                                foreach (DataRow drps in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'" + " AND " + "SERVICE_CODE = '" + srvcode + "'"))
                                {
                                    List<string> feeins = getFeeIns(drps["FEE_CODE"] + "", keyvalue, zver);
                                    if (null != feeins && feeins.Count > 0)
                                    {
                                        foreach (string fins in feeins)
                                        {
                                            seq += 10;
                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL();
                                            ins.SEQ_NUMBER = seq.ToString();
                                            ins.INS_ID = fins;
                                            ins_list.Add(ins);
                                        }
                                    }
                                }
                                if (ins_list.Count > 0)
                                {
                                    fag_item.INS_DETAIL = ins_list.ToArray();
                                }

                                fag_itemlist.Add(fag_item);
                            }
                        }

                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOT tccs_root = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOT();
                        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM> tccs_itemlist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM>();

                        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST> item_costlist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST>();
                        foreach (DataRow drpsf in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                        {
                            bjrid = drpsf["RID"] + "";
                            prdcode = drpsf["PRODUCT_CODE"] + "";
                            srvcode = drpsf["SERVICE_CODE"] + "";
                            feecode = drpsf["FEE_CODE"] + "";
                            feename = drpsf["FEE_NAME"] + "";
                            js_obj = drpsf["JSFCODE"] + "";
                            js_role = drpsf["JSFJSCODE"] + "";

                            bool islsc = drpsf["ISLSC"] + "" == "1";
                            List<string> bgflist = new List<string>();
                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX> item_dyfwlxlist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX>();
                            if (islsc)
                            {
                                bgflist.Clear();
                                string sqllsc = string.Format("SELECT * FROM SQM_BJ_PSF WHERE  VRID = '{0}' AND BGFZRID = '{1}' ORDER BY PRODUCT_CODE, SERVICE_CODE, FEE_CODE ", drpsf["VRID"] + "", drpsf["RID"] + "");
                                DataTable dtbgf = DataHelper.QueryDataTable(sqllsc);
                                if (null != dtbgf && dtbgf.Rows.Count > 0)
                                {
                                    int ZLINE_NO = 0;
                                    foreach (DataRow drbgf in dtbgf.Rows)
                                    {
                                        string bgfsc = drbgf["SERVICE_CODE"] + "";
                                        if (!bgflist.Contains(bgfsc))
                                        {
                                            bgflist.Add(bgfsc);
                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX item_dyfwlx = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_DYFWLX();
                                            ZLINE_NO += 10;
                                            item_dyfwlx.ZLINE_NO = ZLINE_NO.ToString();
                                            item_dyfwlx.ZTRANSSRVREQ_CODE = bgfsc;
                                            item_dyfwlxlist.Add(item_dyfwlx);
                                        }
                                    }
                                }
                            }

                            string sqlJSFFZS = string.Format(" select JSFFZS from sqm_fee_calc where feecode = '{0}' ", feecode);
                            bool bJSFFZS = DataHelper.QueryValue(sqlJSFFZS) + "" == "1";

                            string sqlfeecalcnt = string.Format("select DISTINCT  DJFSRID from SQM_MODEBJ_VAL where FEECALCID='{0}' and ifbjitem = '1' ", bjrid);
                            DataTable feecalcntdt = DataHelper.QueryDataTable(sqlfeecalcnt);

                            if ((drpsf["BJFS"] + "" != "1") && null != feecalcntdt && feecalcntdt.Rows.Count > 0)
                            {
                                for (int i = 0; i < feecalcntdt.Rows.Count; i++)
                                {
                                    string strdjfsid = feecalcntdt.Rows[i]["DJFSRID"] + "";

                                    //获取报价值表的数据
                                    sql = @"select * from SQM_MODEBJ_VAL t where FEECALCID='" + bjrid + "' and ifbjitem = '1' ";
                                    if (!string.IsNullOrEmpty(strdjfsid))
                                    {
                                        sql += " and djfsrid = '" + strdjfsid + "'";
                                    }
                                    else
                                    {
                                        sql += " and djfsrid is null ";
                                    }
                                    DataTable zbvaldt = DataHelper.QueryDataTable(sql);

                                    if (string.IsNullOrEmpty(zbvaldt.Rows[0]["GDZRID"] + ""))
                                    {
                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_item = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                        tccs_item.KEY = SQMTMInterface.genITEMKEY();
                                        tccs_item.LINENR = line_auto.ToString();
                                        line_auto++;
                                        //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                        if (drpsf["BJFS"] + "" == "0")// 普通报价
                                        {
                                            tccs_item.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                        }
                                        else if (drpsf["BJFS"] + "" == "1")// AT COST
                                        {
                                            tccs_item.TCCALCRESINS040 = "COST";//指令类型 
                                            tccs_item.CLCRESBAS036 = drpsf["JXJC"] + "";
                                            tccs_item.COST_PULL_STRATEGY = "2";
                                            tccs_item.SOURCE_CHARGE = "3";
                                        }
                                        else
                                        {
                                            tccs_item.TCCALCRESINS040 = "STND";//指令类型 
                                        }

                                        if (CZJSJS_FEES.Contains(feecode))
                                        {
                                            tccs_item.ZCZJS_ROLE = js_role;//仓租结算角色   报价系统费目新增
                                        }
                                        else
                                        {
                                            tccs_item.ZSETTLE_ROLE = js_role;//结算角色   报价费目（与结算方互斥）
                                        }
                                        tccs_item.ZSETTLE_OBJ = js_obj;//结算方    报价费目
                                        //tccs_item.OPERATIONCD102 = "";//费用项目操作   比较高低值时H、L，顺序检查
                                        //tccs_item.COST_PULL_STRATEGY = "";//成本拉式策略   COST时传2，其他不传
                                        //tccs_item.SOURCE_CHARGE = "";//费用源    COST时传3，其他不传
                                        //if ("STND" == tccs_item.TCCALCRESINS040)
                                        //{
                                        if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                        {
                                            tccs_item.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                        }
                                        //}
                                        //tccs_item.CLCRESBAS036 = "";//计算解析基础    PKG、SERVICE等
                                        tccs_item.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                        //tccs_item.RULE101 = "";//前提条件规则
                                        //tccs_item.AMOUNT = "";//简单报价金额（跟费率表 、费率表确定规则 互斥）
                                        tccs_item.CURRCODE016 = "CNY";//货币 币种

                                        //tccs_item.RATE_ID = "";//费率表 601
                                        //tccs_item.MIN_AMOUNT = "";//费目的最小值
                                        //tccs_item.MAX_AMOUNT = "";//费目的最高值
                                        //tccs_item.RULE099 = "";//费率表确定规则    报价费目
                                        //tccs_item.TARGET_ITEM_KEY = "";//目标ITEM KEY    自己编关系

                                        if (drpsf["BJFS"] + "" == "1")// AT COST
                                        {

                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                            item_cost.TCET084 = feecode;

                                            item_costlist.Add(item_cost);

                                            //tccs_item.ITEM_COST = item_costlist.ToArray();
                                            //tccs_itemlist.Add(tccs_item);
                                            continue;
                                        }
                                        else if (drpsf["BJFS"] + "" == "2")
                                        {
                                            //tccs_item.TCET084 = feecode;
                                            //tccs_itemlist.Add(tccs_item);
                                            continue;
                                        }
                                        else
                                        {
                                            tccs_item.TCET084 = feecode;
                                        }

                                        List<string> hadrate = new List<string>();

                                        foreach (DataRow zbdr in zbvaldt.Rows)
                                        {
                                            djfsrid = zbdr["DJFSRID"].ToString();
                                            gdzrid = zbdr["GDZRID"].ToString();

                                            if (hadrate.Contains(djfsrid + gdzrid))
                                            {
                                                continue;
                                            }

                                            hadrate.Add(djfsrid + gdzrid);

                                            if (!string.IsNullOrEmpty(gdzrid))
                                            {

                                            }
                                            else
                                            {
                                                //是否有MIN
                                                string minpriceSQL = "";
                                                if (!string.IsNullOrEmpty(djfsrid))
                                                {
                                                    minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID = '{1}'", feecode, djfsrid);
                                                }
                                                else
                                                {
                                                    minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID is null ", feecode);
                                                }
                                                string minprice = DataHelper.QueryValue(minpriceSQL) + "";
                                                bMIN = false;
                                                if (minprice == "1")
                                                {
                                                    bMIN = true;
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

                                                SQMTMInterface sqmtminterface = new SQMTMInterface();

                                                List<string> calccodestrc = new List<string>();
                                                List<string> calccodestrcref = new List<string>();
                                                fieldkeys = sqmtminterface.getFieldKeys(bjrid, bMIN, where, ref calccodestrc);
                                                sql = "select " + fieldkeys + " from SQM_MODEBJ_VAL where ifbjitem = '1' and FEECALCID='{0}' and STATUS='1' {1}";
                                                sql = string.Format(sql, bjrid, wheredt);
                                                DataTable bjvalsdt = null;
                                                bjvalsdt = DataHelper.QueryDataTable(sql);

                                                if (calccodestrc.Count == 0)
                                                {
                                                    if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                    {
                                                        continue;
                                                    }
                                                }

                                                if (bMIN)
                                                {
                                                    if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1)
                                                    {
                                                        if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                        {
                                                            continue;
                                                        }

                                                        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                        item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                        item_calcrule.QTY_VALUE = "1";
                                                        item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                        if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                        {
                                                            item_calcrule.QTY_UNIT_C = "EA";
                                                        }
                                                        item_calcrulelist.Add(item_calcrule);
                                                        tccs_item.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                        tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                        tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                        tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                        tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                        if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                        {
                                                            tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                        }

                                                        if (bJSFFZS)
                                                        {
                                                            tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                            tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                            string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                            string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                            if (!String.IsNullOrEmpty(djfsrid))
                                                            {
                                                                czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and DJFSRID is null ";
                                                                cztjsql += " and DJFSRID is null ";
                                                            }
                                                            if (!String.IsNullOrEmpty(gdzrid))
                                                            {
                                                                czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and GDZRID is null ";
                                                                cztjsql += " and GDZRID is null ";
                                                            }
                                                            DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                            DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                            if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                            {
                                                                tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                            }

                                                            if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                            {
                                                                List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                foreach (DataRow drcztj in dtcztj.Rows)
                                                                {
                                                                    Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                    cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                    cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                    cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                    tccs_zczxglist.Add(cztj);
                                                                }
                                                                if (tccs_zczxglist.Count > 0)
                                                                {
                                                                    tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                }
                                                            }
                                                        }

                                                        tccs_itemlist.Add(tccs_item);

                                                        continue;
                                                    }
                                                }
                                                if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1
                                                    && bjvalsdt.Rows[0][calccodestrc[0] + "ISCNT"] + "" == "是")
                                                {
                                                    if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                    {
                                                        continue;
                                                    }

                                                    List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                    Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                    item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                    item_calcrule.QTY_VALUE = "1";
                                                    item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                    if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                    {
                                                        item_calcrule.QTY_UNIT_C = "EA";
                                                    }
                                                    item_calcrulelist.Add(item_calcrule);
                                                    tccs_item.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                    tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                    tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                    tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                    if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                    {
                                                        tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                    }

                                                    if (bJSFFZS)
                                                    {
                                                        tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                        tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                        string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                        string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                        if (!String.IsNullOrEmpty(djfsrid))
                                                        {
                                                            czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                            cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                        }
                                                        else
                                                        {
                                                            czcxsql += " and DJFSRID is null ";
                                                            cztjsql += " and DJFSRID is null ";
                                                        }
                                                        if (!String.IsNullOrEmpty(gdzrid))
                                                        {
                                                            czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                            cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                        }
                                                        else
                                                        {
                                                            czcxsql += " and GDZRID is null ";
                                                            cztjsql += " and GDZRID is null ";
                                                        }
                                                        DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                        DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                        if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                        {
                                                            tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                            tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                            tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                            tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                            tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                        }

                                                        if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                        {
                                                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                            foreach (DataRow drcztj in dtcztj.Rows)
                                                            {
                                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                tccs_zczxglist.Add(cztj);
                                                            }
                                                            if (tccs_zczxglist.Count > 0)
                                                            {
                                                                tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                            }
                                                        }
                                                    }

                                                    tccs_itemlist.Add(tccs_item);

                                                    continue;
                                                }

                                                if (calccodestrc.Count > 0)
                                                {
                                                    BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                    rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                    Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                    List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                    Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                    string str_rate_id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                    rate601.RATE_ID = str_rate_id;//费率表ID，长度20
                                                    rate601.TCUSAGECD085 = "3";
                                                    rate601.TIMEZONE = "UTC+8";
                                                    rate601.TCET = feecode; //"费目代码"
                                                    rate601.VAL_INDICATOR = "A"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                    rate601.RATE_TAB_TYPE = "ZFW1";
                                                    rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                    rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                    Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                    orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                    orgdatalist.Add(orgdata);
                                                    rate601.ORG_DATA = orgdatalist.ToArray();

                                                    List<string> calcexcludelist = new List<string>();
                                                    List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                    int indx = 0;
                                                    int bcntadd = 0;

                                                    bool multiMin = bMIN && bjvalsdt.DefaultView.ToTable(true, "MINBJPRICE").Rows.Count > 1;

                                                    foreach (var ccoders in calccodestrc)
                                                    {
                                                        if (STR_ZZCFTYZ == ccoders)
                                                        {
                                                            calccodestrcref.Add(STR_ZZCFTYZ);
                                                            continue;
                                                        }

                                                        if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是")
                                                        {
                                                            bcntadd += 1;
                                                            if (bcntadd > 1)
                                                            {
                                                                continue;
                                                            }
                                                        }

                                                        bool isex = true;
                                                        string strlast = "";
                                                        foreach (DataRow bjcd in bjvalsdt.Rows)
                                                        {
                                                            if (string.IsNullOrEmpty(strlast))
                                                            {
                                                                strlast = bjcd[ccoders + "CODE"] + "";
                                                            }
                                                            else if (strlast != bjcd[ccoders + "CODE"] + "")
                                                            {
                                                                isex = false;
                                                                break;
                                                            }
                                                            if (STR_ZZCFTYZ != ccoders)
                                                            {
                                                                if ("A" == sqmtminterface.getCACL_TYP(bjcd[ccoders + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                {
                                                                    isex = false;
                                                                    break;
                                                                }
                                                                //仓租费计费数量为“是”的传到标度值里
                                                                if (feecode == "S1000CZF0001" && bjcd[ccoders + "ISCNT"] + "" == "是")
                                                                {
                                                                    isex = false;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        if (isex && !multiMin)
                                                        {
                                                            calcexcludelist.Add(ccoders);
                                                            continue;
                                                        }

                                                        indx++;

                                                        Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                        ratescale.DIMENSION_INDX = indx.ToString(); //标度维数

                                                        ratescale.CALC_BASE = ccoders; //"计算基础代码";
                                                        if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                        {
                                                            ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                        }
                                                        else
                                                        {
                                                            ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                        }
                                                        ratescale.SCALE_UOM = bjvalsdt.Rows[0][ccoders + "MSRCODE"] + "";

                                                        ratescale.INITVAL_SUPPORT = "X";
                                                        //ratescale.MINVAL_SUPPORTED = "X";
                                                        //ratescale.MAXVAL_SUPPORTED = "X";
                                                        if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是" && STR_JTLJ == bjvalsdt.Rows[0]["JTLJ"] + "")
                                                        {
                                                            ratescale.REL_FOR_WGTBRK = "X";
                                                            tccs_item.CALC_METH_CODE = "2";
                                                        }
                                                        ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                        if ("COMMODITY_CODE" == ccoders)
                                                        {
                                                            ratescale.CCODE_TYPE = "IN";
                                                        }
                                                        ratescalelist.Add(ratescale);
                                                    }
                                                    rate601.RATESCALE = ratescalelist.ToArray();

                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                    //foreach
                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                    //validity.ZNUMBER = "";//待定
                                                    validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                    validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                    validity.CURRENCY = bjvalsdt.Rows[0]["CURRENCY"] + "";//货币


                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                    int iscnt = 0;
                                                    foreach (var ccodecrr in calccodestrc)
                                                    {
                                                        if (STR_ZZCFTYZ == ccodecrr)
                                                        {
                                                            continue;
                                                        }

                                                        if (bjvalsdt.Rows[0][ccodecrr + "ISCNT"] + "" == "是")
                                                        {
                                                            iscnt++;
                                                            if (iscnt > 1)
                                                            {
                                                                calccodestrcref.Add(ccodecrr);
                                                                continue;
                                                            }
                                                            //foreach
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                            calcruleref.CALC_BASE_CODE = ccodecrr;//定价模块-费目标准报价方式-标识
                                                            quantity.QTY_UNIT_C = bjvalsdt.Rows[0][ccodecrr + "MSRCODE"] + ""; //计量单位
                                                            quantity.QTY_VALUE = "1";//默认传1
                                                            calcruleref.QUANTITY = quantity;
                                                            //calcruleref.ROUND_RULE = "";//默认为空
                                                            //不传 calcruleref.FOR_REL_SCLITM = "";
                                                            calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                            calcrulereflist.Add(calcruleref);
                                                        }
                                                    }
                                                    validity.CALCRULEREF = calcrulereflist.ToArray();


                                                    List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                    //foreach
                                                    if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow drval in bjvalsdt.Rows)
                                                        {
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dimMin = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                            int cnt = 0;
                                                            foreach (var ccoderd in calccodestrc)
                                                            {
                                                                if (calcexcludelist.Contains(ccoderd) && !multiMin)
                                                                {
                                                                    continue;
                                                                }
                                                                if (calccodestrcref.Contains(ccoderd))
                                                                {
                                                                    continue;
                                                                }

                                                                cnt++;

                                                                switch (cnt)
                                                                {
                                                                    case 1:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                        //scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                        scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM1 = scale_item1;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 2:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                        scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM2 = scale_item2;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 3:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                        scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM3 = scale_item3;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 4:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                        scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM4 = scale_item4;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 5:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                        scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM5 = scale_item5;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 6:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                        scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM6 = scale_item6;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 7:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                        scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM7 = scale_item7;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 8:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                        scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM8 = scale_item8;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 9:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                        scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM9 = scale_item9;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 10:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                        scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM10 = scale_item10;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 11:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                        scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM11 = scale_item11;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 12:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                        scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM12 = scale_item12;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 13:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                        scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM13 = scale_item13;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 14:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                        if (feecode == "S1000CZF0001")
                                                                        {
                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        else
                                                                        {
                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        }
                                                                        scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                        scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM14 = scale_item14;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    default:
                                                                        break;
                                                                }

                                                                if (multiMin)
                                                                {
                                                                    switch (cnt)
                                                                    {
                                                                        case 1:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                            scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM1 = scale_item1;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item1.CALC_TYP == "B")
                                                                            {
                                                                                scale_item1.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 2:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                            scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM2 = scale_item2;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item2.CALC_TYP == "B")
                                                                            {
                                                                                scale_item2.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 3:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                            scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM3 = scale_item3;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item3.CALC_TYP == "B")
                                                                            {
                                                                                scale_item3.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 4:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                            scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM4 = scale_item4;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item4.CALC_TYP == "B")
                                                                            {
                                                                                scale_item4.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 5:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                            scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM5 = scale_item5;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item5.CALC_TYP == "B")
                                                                            {
                                                                                scale_item5.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 6:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                            scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM6 = scale_item6;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item6.CALC_TYP == "B")
                                                                            {
                                                                                scale_item6.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 7:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                            scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM7 = scale_item7;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item7.CALC_TYP == "B")
                                                                            {
                                                                                scale_item7.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 8:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                            scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM8 = scale_item8;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item8.CALC_TYP == "B")
                                                                            {
                                                                                scale_item8.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 9:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                            scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM9 = scale_item9;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item9.CALC_TYP == "B")
                                                                            {
                                                                                scale_item9.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 10:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                            scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM10 = scale_item10;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item10.CALC_TYP == "B")
                                                                            {
                                                                                scale_item10.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 11:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                            scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM11 = scale_item11;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item11.CALC_TYP == "B")
                                                                            {
                                                                                scale_item11.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 12:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                            scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM12 = scale_item12;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item12.CALC_TYP == "B")
                                                                            {
                                                                                scale_item12.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 13:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                            scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM13 = scale_item13;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item13.CALC_TYP == "B")
                                                                            {
                                                                                scale_item13.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        case 14:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                            scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dimMin.SCALE_ITEM14 = scale_item14;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                            if (scale_item14.CALC_TYP == "B")
                                                                            {
                                                                                scale_item14.SCALE_ITEM = "1-";
                                                                            }
                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }
                                                                }
                                                            }
                                                            if (multiMin)
                                                            {
                                                                foreach (Z2FM_SQ_RATE_CREATEIT_RATERATESCALE rs in rate601.RATESCALE)
                                                                {
                                                                    if (rs.SCATYP == "B")
                                                                    {
                                                                        rs.MINVAL_SUPPORTED = "X";
                                                                    }
                                                                }
                                                                rates_dimlist.Add(rates_dimMin);
                                                            }
                                                            rates_dimlist.Add(rates_dim);
                                                        }
                                                    }
                                                    validity.RATES_DIM = rates_dimlist.ToArray();

                                                    validitylist.Add(validity);

                                                    rate601.VALIDITY = validitylist.ToArray();

                                                    rate601list.Add(rate601);

                                                    rate601create.IT_RATE = rate601list.ToArray();

                                                    Rate601Patched(ref rate601create);
                                                    //return Content(new JsonMessage { Success = false, Data = null, Code = "-1", Message = "测试" }.ToString());
                                                    Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                    if (resrate != null && resrate.Count() > 0)
                                                    {
                                                        foreach (var rr in resrate)
                                                        {
                                                            if (null != rr.MSG)
                                                            {
                                                                foreach (var rm in rr.MSG)
                                                                {
                                                                    if ("E" == rm.MSG_TYPE)
                                                                    {
                                                                        rateflag = false;
                                                                        ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                    }
                                                                }

                                                                if (!rateflag)
                                                                {
                                                                    goto rtnLabel;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    tccs_item.RATE_ID = str_rate_id;//费率表 601
                                                }
                                                else
                                                {
                                                    tccs_item.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                }

                                                if (tccs_item.TCCALCRESINS040 == "STND")
                                                {
                                                    tccs_item.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                    tccs_item.RULE101 = drpsf["CONDITION"] + "";
                                                }
                                                //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                if (islsc && "STND" == tccs_item.TCCALCRESINS040)
                                                {
                                                    tccs_item.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                }
                                                if (calccodestrcref.Count > 0)
                                                {
                                                    tccs_item.ANALYTICRELEV = "X";
                                                }
                                                if (bJSFFZS)
                                                {
                                                    tccs_item.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                    tccs_item.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                    string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                    string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                    if (!String.IsNullOrEmpty(djfsrid))
                                                    {
                                                        czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                        cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                    }
                                                    else
                                                    {
                                                        czcxsql += " and DJFSRID is null ";
                                                        cztjsql += " and DJFSRID is null ";
                                                    }
                                                    if (!String.IsNullOrEmpty(gdzrid))
                                                    {
                                                        czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                        cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                    }
                                                    else
                                                    {
                                                        czcxsql += " and GDZRID is null ";
                                                        cztjsql += " and GDZRID is null ";
                                                    }
                                                    DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                    DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                    if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                    {
                                                        tccs_item.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                        tccs_item.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                        tccs_item.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                        tccs_item.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                        tccs_item.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                    }

                                                    if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                    {
                                                        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                        foreach (DataRow drcztj in dtcztj.Rows)
                                                        {
                                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                            cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                            cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                            cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                            tccs_zczxglist.Add(cztj);
                                                        }
                                                        if (tccs_zczxglist.Count > 0)
                                                        {
                                                            tccs_item.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                        }
                                                    }
                                                }
                                                tccs_itemlist.Add(tccs_item);

                                                if (calccodestrcref.Count() > 0)
                                                {
                                                    foreach (string calcref in calccodestrcref)
                                                    {
                                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemref = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                        tccs_itemref.KEY = SQMTMInterface.genITEMKEY();
                                                        tccs_itemref.LINENR = line_auto.ToString();
                                                        line_auto++;
                                                        tccs_itemref.CALC_REF_LINE_NO = tccs_item.LINENR;
                                                        tccs_itemref.CALC_REF_TO_NO = tccs_item.LINENR;

                                                        //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                        if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                        {
                                                            tccs_itemref.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                        }
                                                        else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                        {
                                                            tccs_itemref.TCCALCRESINS040 = "COST";//指令类型 
                                                            tccs_itemref.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                            tccs_itemref.COST_PULL_STRATEGY = "2";
                                                            tccs_itemref.SOURCE_CHARGE = "3";
                                                        }
                                                        else
                                                        {
                                                            tccs_itemref.TCCALCRESINS040 = "STND";//指令类型 
                                                        }

                                                        if (drpsf["BJFS"] + "" == "1")// AT COST
                                                        {

                                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_costref = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                            item_costref.TCET084 = feecode;

                                                            item_costlist.Add(item_costref);

                                                            //tccs_itemref.ITEM_COST = item_costlist.ToArray();
                                                        }
                                                        else
                                                        {
                                                            tccs_itemref.TCET084 = feecode;
                                                        }

                                                        if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                        {
                                                            tccs_itemref.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                        }
                                                        tccs_itemref.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                        tccs_itemref.CURRCODE016 = "%";//货币 币种

                                                        bool isex = true;
                                                        string strlast = "";
                                                        foreach (DataRow bjcd in bjvalsdt.Rows)
                                                        {
                                                            if (string.IsNullOrEmpty(strlast))
                                                            {
                                                                strlast = bjcd[calcref + "CODE"] + "";
                                                            }
                                                            else if (strlast != bjcd[calcref + "CODE"] + "")
                                                            {
                                                                isex = false;
                                                                break;
                                                            }
                                                            if (STR_ZZCFTYZ != calcref)
                                                            {
                                                                if ("A" == sqmtminterface.getCACL_TYP(bjcd[calcref + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                {
                                                                    isex = false;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        if (isex)
                                                        {
                                                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                            item_calcrule.CALC_BASE_CODE = calcref;
                                                            item_calcrule.QTY_VALUE = "1";
                                                            item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";
                                                            if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                            {
                                                                item_calcrule.QTY_UNIT_C = "EA";
                                                            }
                                                            item_calcrulelist.Add(item_calcrule);
                                                            tccs_itemref.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                            tccs_itemref.AMOUNT = "100";
                                                            //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                            tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                            tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                            if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                            {
                                                                tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                            }

                                                            tccs_itemlist.Add(tccs_itemref);

                                                            continue;
                                                        }
                                                        //tccs_itemref.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                        BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                        rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                        Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                        Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                        string str_rate_id_ref = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                        rate601.RATE_ID = str_rate_id_ref;//费率表ID，长度20
                                                        rate601.TCUSAGECD085 = "3";
                                                        rate601.TIMEZONE = "UTC+8";
                                                        rate601.TCET = feecode; //"费目代码"
                                                        rate601.VAL_INDICATOR = "P"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                        rate601.RATE_TAB_TYPE = "ZFW1";
                                                        rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                        rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                        Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                        orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                        orgdatalist.Add(orgdata);
                                                        rate601.ORG_DATA = orgdatalist.ToArray();

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                        Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                        ratescale.DIMENSION_INDX = "1"; //标度维数

                                                        ratescale.CALC_BASE = calcref; //"计算基础代码";
                                                        if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                        {
                                                            ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                        }
                                                        else
                                                        {
                                                            ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                        }
                                                        ratescale.SCALE_UOM = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";

                                                        ratescale.INITVAL_SUPPORT = "X";
                                                        //ratescale.MINVAL_SUPPORTED = "X";
                                                        //ratescale.MAXVAL_SUPPORTED = "X";
                                                        ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                        if ("COMMODITY_CODE" == calcref)
                                                        {
                                                            ratescale.CCODE_TYPE = "IN";
                                                        }
                                                        ratescalelist.Add(ratescale);
                                                        rate601.RATESCALE = ratescalelist.ToArray();

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                        //foreach
                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                        //validity.ZNUMBER = "";//待定
                                                        validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                        validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                        validity.CURRENCY = "%";


                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                        //foreach
                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                        calcruleref.CALC_BASE_CODE = calcref;//定价模块-费目标准报价方式-标识
                                                        quantity.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + ""; //计量单位
                                                        quantity.QTY_VALUE = "1";//默认传1
                                                        calcruleref.QUANTITY = quantity;
                                                        //calcruleref.ROUND_RULE = "";//默认为空
                                                        //不传 calcruleref.FOR_REL_SCLITM = "";
                                                        calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                        calcrulereflist.Add(calcruleref);
                                                        validity.CALCRULEREF = calcrulereflist.ToArray();


                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                        //foreach
                                                        if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                        {
                                                            List<string> hadvallist = new List<string>();
                                                            foreach (DataRow drval in bjvalsdt.Rows)
                                                            {
                                                                if (!hadvallist.Contains(drval[calcref + "CODE"] + ""))
                                                                {
                                                                    hadvallist.Add(drval[calcref + "CODE"] + "");
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                    scale_item1.SCALE_ITEM = drval[calcref + "CODE"] + "";//标度值字符
                                                                    scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(calcref, scale_item1.SCALE_ITEM);
                                                                    scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[calcref + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                    rates_dim.RATE = "100";//金额
                                                                    rates_dim.SCALE_ITEM1 = scale_item1;
                                                                    rates_dimlist.Add(rates_dim);
                                                                }
                                                            }
                                                        }
                                                        validity.RATES_DIM = rates_dimlist.ToArray();

                                                        validitylist.Add(validity);

                                                        rate601.VALIDITY = validitylist.ToArray();

                                                        rate601list.Add(rate601);

                                                        rate601create.IT_RATE = rate601list.ToArray();

                                                        Rate601Patched(ref rate601create);
                                                        Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                        if (resrate != null && resrate.Count() > 0)
                                                        {
                                                            foreach (var rr in resrate)
                                                            {
                                                                if (null != rr.MSG)
                                                                {
                                                                    foreach (var rm in rr.MSG)
                                                                    {
                                                                        if ("E" == rm.MSG_TYPE)
                                                                        {
                                                                            rateflag = false;
                                                                            ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                        }
                                                                    }

                                                                    if (!rateflag)
                                                                    {
                                                                        goto rtnLabel;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        tccs_itemref.RATE_ID = str_rate_id_ref;

                                                        if (tccs_itemref.TCCALCRESINS040 == "STND")
                                                        {
                                                            tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                            tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                        }
                                                        //tccs_itemref.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                        if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                        {
                                                            tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                        }

                                                        tccs_itemlist.Add(tccs_itemref);
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        List<string> gdzidlist = new List<string>();
                                        foreach (DataRow drzbval in zbvaldt.Rows)
                                        {
                                            string ss = drzbval["GDZRID"] + "";
                                            if (!gdzidlist.Contains(ss))
                                            {
                                                gdzidlist.Add(ss);
                                            }
                                        }


                                        string strHLAsql = string.Format("select GDZKEY from sqm_fee_pur_ref where GDZRID = '{0}' ", gdzidlist[0]);
                                        string strHLA = (string)DataHelper.QueryValue(strHLAsql);

                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemeval = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                        tccs_itemeval.KEY = SQMTMInterface.genITEMKEY();
                                        tccs_itemeval.LINENR = line_auto.ToString();
                                        line_auto++;
                                        tccs_itemeval.TCCALCRESINS040 = "EVAL";
                                        tccs_itemeval.OPERATIONCD102 = strHLA;

                                        tccs_itemlist.Add(tccs_itemeval);

                                        foreach (string strgdzid in gdzidlist)
                                        {
                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemsum = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                            tccs_itemsum.KEY = SQMTMInterface.genITEMKEY();
                                            tccs_itemsum.LINENR = line_auto.ToString();
                                            line_auto++;
                                            tccs_itemsum.TCCALCRESINS040 = "SUM";
                                            tccs_itemsum.TARGET_ITEM_KEY = tccs_itemeval.KEY;
                                            tccs_itemlist.Add(tccs_itemsum);

                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemgdz = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                            tccs_itemgdz.KEY = SQMTMInterface.genITEMKEY();
                                            tccs_itemgdz.LINENR = line_auto.ToString();
                                            line_auto++;
                                            tccs_itemgdz.TARGET_ITEM_KEY = tccs_itemsum.KEY;//目标ITEM KEY    自己编关系
                                            //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                            if (drpsf["BJFS"] + "" == "0")// 普通报价
                                            {
                                                tccs_itemgdz.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                            }
                                            else if (drpsf["BJFS"] + "" == "1")// AT COST
                                            {
                                                tccs_itemgdz.TCCALCRESINS040 = "COST";//指令类型 
                                                tccs_itemgdz.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                tccs_itemgdz.COST_PULL_STRATEGY = "2";
                                                tccs_itemgdz.SOURCE_CHARGE = "3";
                                            }
                                            else
                                            {
                                                tccs_itemgdz.TCCALCRESINS040 = "STND";//指令类型 
                                            }

                                            if (CZJSJS_FEES.Contains(feecode))
                                            {
                                                tccs_itemgdz.ZCZJS_ROLE = js_role;//仓租结算角色   报价系统费目新增
                                            }
                                            else
                                            {
                                                tccs_itemgdz.ZSETTLE_ROLE = js_role;//结算角色   报价费目（与结算方互斥）
                                            }
                                            tccs_itemgdz.ZSETTLE_OBJ = js_obj;//结算方    报价费目
                                            //tccs_item.OPERATIONCD102 = "";//费用项目操作   比较高低值时H、L，顺序检查
                                            //tccs_item.COST_PULL_STRATEGY = "";//成本拉式策略   COST时传2，其他不传
                                            //tccs_item.SOURCE_CHARGE = "";//费用源    COST时传3，其他不传
                                            //if ("STND" == tccs_item.TCCALCRESINS040)
                                            //{
                                            if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                            {
                                                tccs_itemgdz.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                            }
                                            //}
                                            //tccs_item.CLCRESBAS036 = "";//计算解析基础    PKG、SERVICE等
                                            tccs_itemgdz.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                            //tccs_item.RULE101 = "";//前提条件规则
                                            //tccs_item.AMOUNT = "";//简单报价金额（跟费率表 、费率表确定规则 互斥）
                                            tccs_itemgdz.CURRCODE016 = "CNY";//货币 币种

                                            //tccs_item.RATE_ID = "";//费率表 601
                                            //tccs_item.MIN_AMOUNT = "";//费目的最小值
                                            //tccs_item.MAX_AMOUNT = "";//费目的最高值
                                            //tccs_item.RULE099 = "";//费率表确定规则    报价费目

                                            if (drpsf["BJFS"] + "" == "1")// AT COST
                                            {
                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                item_cost.TCET084 = feecode;

                                                item_costlist.Add(item_cost);

                                                //tccs_itemgdz.ITEM_COST = item_costlist.ToArray();
                                                //tccs_itemlist.Add(tccs_itemgdz);
                                                continue;
                                            }
                                            else
                                            {
                                                tccs_itemgdz.TCET084 = feecode;
                                            }

                                            List<string> hadrate = new List<string>();

                                            foreach (DataRow zbdr in zbvaldt.Rows)
                                            {
                                                djfsrid = zbdr["DJFSRID"].ToString();
                                                gdzrid = zbdr["GDZRID"].ToString();

                                                if (gdzrid != strgdzid)
                                                { continue; }

                                                if (hadrate.Contains(djfsrid + gdzrid))
                                                {
                                                    continue;
                                                }

                                                hadrate.Add(djfsrid + gdzrid);

                                                if (string.IsNullOrEmpty(gdzrid))
                                                {

                                                }
                                                else
                                                {
                                                    string minpriceSQL = "";
                                                    if (!string.IsNullOrEmpty(djfsrid))
                                                    {
                                                        minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID = '{1}'", feecode, djfsrid);
                                                    }
                                                    else
                                                    {
                                                        minpriceSQL = string.Format("select FSMIN from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID is null ", feecode);
                                                    }
                                                    string minprice = DataHelper.QueryValue(minpriceSQL) + "";
                                                    bMIN = false;
                                                    if (minprice == "1")
                                                    {
                                                        bMIN = true;
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

                                                    SQMTMInterface sqmtminterface = new SQMTMInterface();

                                                    List<string> calccodestrc = new List<string>();
                                                    List<string> calccodestrcref = new List<string>();
                                                    fieldkeys = sqmtminterface.getFieldKeys(bjrid, bMIN, where, ref calccodestrc);
                                                    sql = "select " + fieldkeys + " from SQM_MODEBJ_VAL where ifbjitem = '1' and FEECALCID='{0}' and STATUS='1' {1}";
                                                    sql = string.Format(sql, bjrid, wheredt);
                                                    DataTable bjvalsdt = null;
                                                    bjvalsdt = DataHelper.QueryDataTable(sql);

                                                    if (calccodestrc.Count == 0)
                                                    {
                                                        if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                        {
                                                            continue;
                                                        }
                                                    }

                                                    if (bMIN)
                                                    {
                                                        if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1)
                                                        {
                                                            if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                            {
                                                                continue;
                                                            }

                                                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                            item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                            item_calcrule.QTY_VALUE = "1";
                                                            item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                            if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                            {
                                                                item_calcrule.QTY_UNIT_C = "EA";
                                                            }
                                                            item_calcrulelist.Add(item_calcrule);
                                                            tccs_itemgdz.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                            tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                            tccs_itemgdz.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                            tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                            tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                            if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                            {
                                                                tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                            }
                                                            if (bJSFFZS)
                                                            {
                                                                tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                                tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                                string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                                string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                                if (!String.IsNullOrEmpty(djfsrid))
                                                                {
                                                                    czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                    cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and DJFSRID is null ";
                                                                    cztjsql += " and DJFSRID is null ";
                                                                }
                                                                if (!String.IsNullOrEmpty(gdzrid))
                                                                {
                                                                    czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                    cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                                }
                                                                else
                                                                {
                                                                    czcxsql += " and GDZRID is null ";
                                                                    cztjsql += " and GDZRID is null ";
                                                                }
                                                                DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                                DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                                if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                                {
                                                                    tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                    tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                    tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                    tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                    tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                                }

                                                                if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                                {
                                                                    List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                    foreach (DataRow drcztj in dtcztj.Rows)
                                                                    {
                                                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                        cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                        cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                        cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                        tccs_zczxglist.Add(cztj);
                                                                    }
                                                                    if (tccs_zczxglist.Count > 0)
                                                                    {
                                                                        tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                    }
                                                                }
                                                            }
                                                            tccs_itemlist.Add(tccs_itemgdz);

                                                            continue;
                                                        }
                                                    }
                                                    if (bjvalsdt.Rows.Count == 1 && calccodestrc.Count == 1
                                                        && bjvalsdt.Rows[0][calccodestrc[0] + "ISCNT"] + "" == "是")
                                                    {
                                                        if (IsBJZero(bjvalsdt.Rows[0]["BJPRICE"] + ""))
                                                        {
                                                            continue;
                                                        }

                                                        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                        item_calcrule.CALC_BASE_CODE = calccodestrc[0];
                                                        item_calcrule.QTY_VALUE = "1";
                                                        item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calccodestrc[0] + "MSRCODE"] + "";
                                                        if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                        {
                                                            item_calcrule.QTY_UNIT_C = "EA";
                                                        }
                                                        item_calcrulelist.Add(item_calcrule);
                                                        tccs_itemgdz.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                        tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                        tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                        tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                        if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                        {
                                                            tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                        }
                                                        if (bJSFFZS)
                                                        {
                                                            tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                            tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                            string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                            string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                            if (!String.IsNullOrEmpty(djfsrid))
                                                            {
                                                                czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                                cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and DJFSRID is null ";
                                                                cztjsql += " and DJFSRID is null ";
                                                            }
                                                            if (!String.IsNullOrEmpty(gdzrid))
                                                            {
                                                                czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                                cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                            }
                                                            else
                                                            {
                                                                czcxsql += " and GDZRID is null ";
                                                                cztjsql += " and GDZRID is null ";
                                                            }
                                                            DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                            DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                            if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                            {
                                                                tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                                tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                                tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                                tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                                tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                            }

                                                            if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                            {
                                                                List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                                foreach (DataRow drcztj in dtcztj.Rows)
                                                                {
                                                                    Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                    cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                    cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                    cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                    tccs_zczxglist.Add(cztj);
                                                                }
                                                                if (tccs_zczxglist.Count > 0)
                                                                {
                                                                    tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                                }
                                                            }
                                                        }
                                                        tccs_itemlist.Add(tccs_itemgdz);

                                                        continue;
                                                    }

                                                    if (calccodestrc.Count > 0)
                                                    {
                                                        BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                        rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                        Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                        Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                        string str_rate_id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                        rate601.RATE_ID = str_rate_id;//费率表ID，长度20
                                                        rate601.TCUSAGECD085 = "3";
                                                        rate601.TIMEZONE = "UTC+8";
                                                        rate601.TCET = feecode; //"费目代码"
                                                        rate601.VAL_INDICATOR = "A"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                        rate601.RATE_TAB_TYPE = "ZFW1";
                                                        rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                        rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                        Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                        orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                        orgdatalist.Add(orgdata);
                                                        rate601.ORG_DATA = orgdatalist.ToArray();

                                                        List<string> calcexcludelist = new List<string>();
                                                        List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                        int indx = 0;
                                                        int bcntadd = 0;

                                                        bool multiMin = bMIN && bjvalsdt.DefaultView.ToTable(true, "MINBJPRICE").Rows.Count > 1;

                                                        foreach (var ccoders in calccodestrc)
                                                        {
                                                            if (STR_ZZCFTYZ == ccoders)
                                                            {
                                                                calccodestrcref.Add(STR_ZZCFTYZ);
                                                                continue;
                                                            }

                                                            if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是")
                                                            {
                                                                bcntadd += 1;
                                                                if (bcntadd > 1)
                                                                {
                                                                    continue;
                                                                }
                                                            }

                                                            bool isex = true;
                                                            string strlast = "";
                                                            foreach (DataRow bjcd in bjvalsdt.Rows)
                                                            {
                                                                if (string.IsNullOrEmpty(strlast))
                                                                {
                                                                    strlast = bjcd[ccoders + "CODE"] + "";
                                                                }
                                                                else if (strlast != bjcd[ccoders + "CODE"] + "")
                                                                {
                                                                    isex = false;
                                                                    break;
                                                                }
                                                                if (STR_ZZCFTYZ != ccoders)
                                                                {
                                                                    if ("A" == sqmtminterface.getCACL_TYP(bjcd[ccoders + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                    {
                                                                        isex = false;
                                                                        break;
                                                                    }
                                                                    //仓租费计费数量为“是”的传到标度值里
                                                                    if (feecode == "S1000CZF0001" && bjcd[ccoders + "ISCNT"] + "" == "是")
                                                                    {
                                                                        isex = false;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            if (isex && !multiMin)
                                                            {
                                                                calcexcludelist.Add(ccoders);
                                                                continue;
                                                            }

                                                            indx++;

                                                            Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                            ratescale.DIMENSION_INDX = indx.ToString(); //标度维数

                                                            ratescale.CALC_BASE = ccoders; //"计算基础代码";
                                                            if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                            {
                                                                ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                            }
                                                            else
                                                            {
                                                                ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                            }
                                                            ratescale.SCALE_UOM = bjvalsdt.Rows[0][ccoders + "MSRCODE"] + "";

                                                            ratescale.INITVAL_SUPPORT = "X";
                                                            //ratescale.MINVAL_SUPPORTED = "X";
                                                            //ratescale.MAXVAL_SUPPORTED = "X";
                                                            if (bjvalsdt.Rows[0][ccoders + "ISCNT"] + "" == "是" && STR_JTLJ == bjvalsdt.Rows[0]["JTLJ"] + "")
                                                            {
                                                                ratescale.REL_FOR_WGTBRK = "X";
                                                                tccs_itemgdz.CALC_METH_CODE = "2";
                                                            }

                                                            ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][ccoders + "SCALE"] + "");
                                                            if ("COMMODITY_CODE" == ccoders)
                                                            {
                                                                ratescale.CCODE_TYPE = "IN";
                                                            }
                                                            ratescalelist.Add(ratescale);
                                                        }
                                                        rate601.RATESCALE = ratescalelist.ToArray();

                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                        //foreach
                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                        //validity.ZNUMBER = "";//待定
                                                        validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                        validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                        validity.CURRENCY = bjvalsdt.Rows[0]["CURRENCY"] + "";//货币


                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                        int iscnt = 0;
                                                        foreach (var ccodecrr in calccodestrc)
                                                        {
                                                            if (STR_ZZCFTYZ == ccodecrr)
                                                            {
                                                                continue;
                                                            }

                                                            if (bjvalsdt.Rows[0][ccodecrr + "ISCNT"] + "" == "是")
                                                            {
                                                                iscnt++;
                                                                if (iscnt > 1)
                                                                {
                                                                    calccodestrcref.Add(ccodecrr);
                                                                    continue;
                                                                }

                                                                //foreach
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                                calcruleref.CALC_BASE_CODE = ccodecrr;//定价模块-费目标准报价方式-标识
                                                                quantity.QTY_UNIT_C = bjvalsdt.Rows[0][ccodecrr + "MSRCODE"] + ""; //计量单位
                                                                quantity.QTY_VALUE = "1";//默认传1
                                                                calcruleref.QUANTITY = quantity;
                                                                //calcruleref.ROUND_RULE = "";//默认为空
                                                                //不传 calcruleref.FOR_REL_SCLITM = "";
                                                                calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                                calcrulereflist.Add(calcruleref);
                                                            }
                                                        }
                                                        validity.CALCRULEREF = calcrulereflist.ToArray();


                                                        List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                        //foreach
                                                        if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drval in bjvalsdt.Rows)
                                                            {
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dimMin = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                int cnt = 0;
                                                                foreach (var ccoderd in calccodestrc)
                                                                {
                                                                    if (calcexcludelist.Contains(ccoderd) && !multiMin)
                                                                    {
                                                                        continue;
                                                                    }
                                                                    if (calccodestrcref.Contains(ccoderd))
                                                                    {
                                                                        continue;
                                                                    }

                                                                    cnt++;

                                                                    switch (cnt)
                                                                    {
                                                                        case 1:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                            //scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                            scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM1 = scale_item1;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 2:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                            scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM2 = scale_item2;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 3:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                            scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM3 = scale_item3;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 4:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                            scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM4 = scale_item4;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 5:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                            scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM5 = scale_item5;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 6:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                            scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM6 = scale_item6;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 7:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                            scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM7 = scale_item7;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 8:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                            scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM8 = scale_item8;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 9:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                            scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM9 = scale_item9;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 10:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                            scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM10 = scale_item10;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 11:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                            scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM11 = scale_item11;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 12:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                            scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM12 = scale_item12;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 13:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                            scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM13 = scale_item13;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 14:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                            if (feecode == "S1000CZF0001")
                                                                            {
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            else
                                                                            {
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            }
                                                                            scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                            scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM14 = scale_item14;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }

                                                                    if (multiMin)
                                                                    {
                                                                        switch (cnt)
                                                                        {
                                                                            case 1:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM1 = scale_item1;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item1.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item1.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 2:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM2 = scale_item2;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item2.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item2.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 3:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM3 = scale_item3;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item3.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item3.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 4:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM4 = scale_item4;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item4.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item4.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 5:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM5 = scale_item5;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item5.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item5.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 6:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM6 = scale_item6;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item6.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item6.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 7:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM7 = scale_item7;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item7.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item7.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 8:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM8 = scale_item8;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item8.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item8.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 9:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM9 = scale_item9;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item9.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item9.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 10:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM10 = scale_item10;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item10.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item10.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 11:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM11 = scale_item11;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item11.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item11.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 12:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM12 = scale_item12;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item12.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item12.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 13:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM13 = scale_item13;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item13.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item13.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            case 14:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                if (feecode == "S1000CZF0001")
                                                                                {
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "ISCNT"] + "" == "是" ? "99999" : drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                else
                                                                                {
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                }
                                                                                scale_item14.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item14.SCALE_ITEM);
                                                                                scale_item14.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dimMin.SCALE_ITEM14 = scale_item14;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dimMin.RATE = drval["MINBJPRICE"] + "";//金额
                                                                                if (scale_item14.CALC_TYP == "B")
                                                                                {
                                                                                    scale_item14.SCALE_ITEM = "1-";
                                                                                }
                                                                                break;
                                                                            default:
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                                if (multiMin)
                                                                {
                                                                    foreach (Z2FM_SQ_RATE_CREATEIT_RATERATESCALE rs in rate601.RATESCALE)
                                                                    {
                                                                        if (rs.SCATYP == "B")
                                                                        {
                                                                            rs.MINVAL_SUPPORTED = "X";
                                                                        }
                                                                    }
                                                                    rates_dimlist.Add(rates_dimMin);
                                                                }
                                                                rates_dimlist.Add(rates_dim);
                                                            }
                                                        }
                                                        validity.RATES_DIM = rates_dimlist.ToArray();

                                                        validitylist.Add(validity);

                                                        rate601.VALIDITY = validitylist.ToArray();

                                                        rate601list.Add(rate601);

                                                        rate601create.IT_RATE = rate601list.ToArray();

                                                        Rate601Patched(ref rate601create);
                                                        Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                        if (resrate != null && resrate.Count() > 0)
                                                        {
                                                            foreach (var rr in resrate)
                                                            {
                                                                if (null != rr.MSG)
                                                                {
                                                                    foreach (var rm in rr.MSG)
                                                                    {
                                                                        if ("E" == rm.MSG_TYPE)
                                                                        {
                                                                            rateflag = false;
                                                                            ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                        }
                                                                    }

                                                                    if (!rateflag)
                                                                    {
                                                                        goto rtnLabel;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        tccs_itemgdz.RATE_ID = str_rate_id;//费率表 601
                                                    }
                                                    else
                                                    {
                                                        tccs_itemgdz.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                    }

                                                    if (tccs_itemgdz.TCCALCRESINS040 == "STND")
                                                    {
                                                        tccs_itemgdz.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                        tccs_itemgdz.RULE101 = drpsf["CONDITION"] + "";
                                                    }
                                                    //tccs_itemgdz.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                    if (islsc && "STND" == tccs_itemgdz.TCCALCRESINS040)
                                                    {
                                                        tccs_itemgdz.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                    }
                                                    if (calccodestrcref.Count > 0)
                                                    {
                                                        tccs_itemgdz.ANALYTICRELEV = "X";
                                                    }
                                                    if (bJSFFZS)
                                                    {
                                                        tccs_itemgdz.CALC_METH_CODE = bjvalsdt.Rows[0]["JSFFLX"] + "";
                                                        tccs_itemgdz.CALC_METH_NAME = bjvalsdt.Rows[0]["JSFF"] + "";

                                                        string czcxsql = string.Format(" SELECT * FROM SQM_BJ_CZXG  WHERE BJRID = '{0}' ", bjrid);
                                                        string cztjsql = string.Format(" SELECT * FROM SQM_BJ_CZTJ  WHERE BJRID = '{0}' ", bjrid);
                                                        if (!String.IsNullOrEmpty(djfsrid))
                                                        {
                                                            czcxsql += " and DJFSRID='" + djfsrid + "' ";
                                                            cztjsql += " and DJFSRID='" + djfsrid + "' ";
                                                        }
                                                        else
                                                        {
                                                            czcxsql += " and DJFSRID is null ";
                                                            cztjsql += " and DJFSRID is null ";
                                                        }
                                                        if (!String.IsNullOrEmpty(gdzrid))
                                                        {
                                                            czcxsql += " and GDZRID='" + gdzrid + "' ";
                                                            cztjsql += " and GDZRID='" + gdzrid + "' ";
                                                        }
                                                        else
                                                        {
                                                            czcxsql += " and GDZRID is null ";
                                                            cztjsql += " and GDZRID is null ";
                                                        }
                                                        DataTable dtczxg = DataHelper.QueryDataTable(czcxsql);
                                                        DataTable dtcztj = DataHelper.QueryDataTable(cztjsql);

                                                        if (null != dtczxg && dtczxg.Rows.Count == 1)
                                                        {
                                                            tccs_itemgdz.ZMZTS = dtczxg.Rows[0]["MZTS"] + "";
                                                            tccs_itemgdz.ZYZDSF = dtczxg.Rows[0]["YZD"] + "";
                                                            tccs_itemgdz.ZSFBY = dtczxg.Rows[0]["CZBY"] + "";
                                                            tccs_itemgdz.ZBYFY = dtczxg.Rows[0]["BYFY"] + "";
                                                            tccs_itemgdz.ZDTCK = dtczxg.Rows[0]["DTCK"] + "";
                                                        }

                                                        if (null != dtcztj && dtcztj.Rows.Count > 0)
                                                        {
                                                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG> tccs_zczxglist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG>();
                                                            foreach (DataRow drcztj in dtcztj.Rows)
                                                            {
                                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG cztj = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMTCCS_ZCZXG();
                                                                cztj.ZCONDITION_NAME = drcztj["TJMCKEY"] + "";
                                                                cztj.ZOPERATOR = drcztj["TJTYPEKEY"] + "";
                                                                cztj.ZDVALUE = drcztj["WDZ"] + "";
                                                                tccs_zczxglist.Add(cztj);
                                                            }
                                                            if (tccs_zczxglist.Count > 0)
                                                            {
                                                                tccs_itemgdz.TCCS_ZCZXG = tccs_zczxglist.ToArray();
                                                            }
                                                        }
                                                    }
                                                    tccs_itemlist.Add(tccs_itemgdz);

                                                    if (calccodestrcref.Count() > 0)
                                                    {
                                                        foreach (string calcref in calccodestrcref)
                                                        {
                                                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemref = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                                            tccs_itemref.KEY = SQMTMInterface.genITEMKEY();
                                                            tccs_itemref.LINENR = line_auto.ToString();
                                                            line_auto++;
                                                            tccs_itemref.CALC_REF_LINE_NO = tccs_itemgdz.LINENR;
                                                            tccs_itemref.CALC_REF_TO_NO = tccs_itemgdz.LINENR;
                                                            //   AT COST用“成本”    高低值比较用“行项目选择”   其他用“标准”
                                                            if (drpsf["BJFS"] + "" == "0")// 普通报价
                                                            {
                                                                tccs_itemref.TCCALCRESINS040 = "STND";//指令类型   STND/SUM/EVAL/COST 
                                                            }
                                                            else if (drpsf["BJFS"] + "" == "1")// AT COST
                                                            {
                                                                tccs_itemref.TCCALCRESINS040 = "COST";//指令类型 
                                                                tccs_itemref.CLCRESBAS036 = drpsf["JXJC"] + "";
                                                                tccs_itemref.COST_PULL_STRATEGY = "2";
                                                                tccs_itemref.SOURCE_CHARGE = "3";
                                                            }
                                                            else
                                                            {
                                                                tccs_itemref.TCCALCRESINS040 = "STND";//指令类型 
                                                            }

                                                            if (drpsf["BJFS"] + "" == "1")// AT COST
                                                            {
                                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_costref = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                                item_costref.TCET084 = feecode;

                                                                item_costlist.Add(item_costref);

                                                                //tccs_itemref.ITEM_COST = item_costlistref.ToArray();
                                                            }
                                                            else
                                                            {
                                                                tccs_itemref.TCET084 = feecode;
                                                            }

                                                            if (!string.IsNullOrEmpty(drpsf["STAGETYPE"] + ""))
                                                            {
                                                                tccs_itemref.STAGE_CAT = (drpsf["STAGETYPE"] + "").Substring(0, 1);//阶段类别    STND时传P、M、O、C、T
                                                            }
                                                            tccs_itemref.TCC_ITEM_DESCRIPTION = drpsf["OTHER_NAME"] + "";//   费用别名
                                                            tccs_itemref.CURRCODE016 = "%";//货币 币种
                                                            //tccs_itemref.AMOUNT = bjvalsdt.Rows[0]["BJPRICE"] + "";
                                                            bool isex = true;
                                                            string strlast = "";
                                                            foreach (DataRow bjcd in bjvalsdt.Rows)
                                                            {
                                                                if (string.IsNullOrEmpty(strlast))
                                                                {
                                                                    strlast = bjcd[calcref + "CODE"] + "";
                                                                }
                                                                else if (strlast != bjcd[calcref + "CODE"] + "")
                                                                {
                                                                    isex = false;
                                                                    break;
                                                                }
                                                                if (STR_ZZCFTYZ != calcref)
                                                                {
                                                                    if ("A" == sqmtminterface.getCACL_TYP(bjcd[calcref + "SCALE"] + "", bjcd["CALCTYPE"] + ""))
                                                                    {
                                                                        isex = false;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            if (isex)
                                                            {
                                                                List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE> item_calcrulelist = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE>();
                                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE item_calcrule = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_CALCRULE();

                                                                item_calcrule.CALC_BASE_CODE = calcref;
                                                                item_calcrule.QTY_VALUE = "1";
                                                                item_calcrule.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";
                                                                if (string.IsNullOrEmpty(item_calcrule.QTY_UNIT_C))
                                                                {
                                                                    item_calcrule.QTY_UNIT_C = "EA";
                                                                }
                                                                item_calcrulelist.Add(item_calcrule);
                                                                tccs_itemref.ITEM_CALCRULE = item_calcrulelist.ToArray();

                                                                tccs_itemref.AMOUNT = "100";
                                                                //tccs_item.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                                tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                                if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                                {
                                                                    tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                                }
                                                                tccs_itemlist.Add(tccs_itemref);

                                                                continue;
                                                            }

                                                            BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient rate601service = new BizTalk_RFC_TM_CRM_601_Orchestration_InboundSoapClient();
                                                            rate601service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                                                            Z2FM_SQ_RATE_CREATE rate601create = new Z2FM_SQ_RATE_CREATE();

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATE> rate601list = new List<Z2FM_SQ_RATE_CREATEIT_RATE>();

                                                            Z2FM_SQ_RATE_CREATEIT_RATE rate601 = new Z2FM_SQ_RATE_CREATEIT_RATE();
                                                            string str_rate_id_ref = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                                                            rate601.RATE_ID = str_rate_id_ref;//费率表ID，长度20
                                                            rate601.TCUSAGECD085 = "3";
                                                            rate601.TIMEZONE = "UTC+8";
                                                            rate601.TCET = feecode; //"费目代码"
                                                            rate601.VAL_INDICATOR = "P"; //A-绝对值;P-百分比值;空-绝对或百分比
                                                            rate601.RATE_TAB_TYPE = "ZFW1";
                                                            rate601.ZSETTLE_ROLE = js_role; //报价页面结算角色
                                                            rate601.ZSETTLE_OBJ = js_obj; //报价页面结算方

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA> orgdatalist = new List<Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA>();
                                                            Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA orgdata = new Z2FM_SQ_RATE_CREATEIT_RATEORG_DATA();
                                                            orgdata.ORG_UNIT = contractorslist.Count > 0 ? contractorslist[0].ORG_UNIT : ""; //contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
                                                            orgdatalist.Add(orgdata);
                                                            rate601.ORG_DATA = orgdatalist.ToArray();

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE> ratescalelist = new List<Z2FM_SQ_RATE_CREATEIT_RATERATESCALE>();
                                                            Z2FM_SQ_RATE_CREATEIT_RATERATESCALE ratescale = new Z2FM_SQ_RATE_CREATEIT_RATERATESCALE();
                                                            ratescale.DIMENSION_INDX = "1"; //标度维数

                                                            ratescale.CALC_BASE = calcref; //"计算基础代码";
                                                            if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                            {
                                                                ratescale.SCATYP = sqmtminterface.getSCATYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                            }
                                                            else
                                                            {
                                                                ratescale.SCATYP = "A"; //费目标准定价方式定义 A-绝对;B-相对  A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
                                                            }
                                                            ratescale.SCALE_UOM = bjvalsdt.Rows[0][calcref + "MSRCODE"] + "";

                                                            ratescale.INITVAL_SUPPORT = "X";
                                                            //ratescale.MINVAL_SUPPORTED = "X";
                                                            //ratescale.MAXVAL_SUPPORTED = "X";
                                                            ratescale.CALC_TYP = sqmtminterface.getCACL_TYP(bjvalsdt.Rows[0][calcref + "SCALE"] + "");
                                                            if ("COMMODITY_CODE" == calcref)
                                                            {
                                                                ratescale.CCODE_TYPE = "IN";
                                                            }
                                                            ratescalelist.Add(ratescale);
                                                            rate601.RATESCALE = ratescalelist.ToArray();

                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY> validitylist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY>();
                                                            //foreach
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY validity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITY();
                                                            //validity.ZNUMBER = "";//待定
                                                            validity.VALID_START_DT = DateTime.Parse(drpsf["BJSTARTDATE"] + "").ToString("yyyyMMdd"); //有效期开始日期
                                                            validity.VALID_END_DT = DateTime.Parse(drpsf["BJENDDATE"] + "").ToString("yyyyMMdd"); ;//有效期结束日期
                                                            validity.CURRENCY = "%";


                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF> calcrulereflist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF>();
                                                            //foreach
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF calcruleref = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREF();
                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY quantity = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYCALCRULEREFQUANTITY();
                                                            calcruleref.CALC_BASE_CODE = calcref;//定价模块-费目标准报价方式-标识
                                                            quantity.QTY_UNIT_C = bjvalsdt.Rows[0][calcref + "MSRCODE"] + ""; //计量单位
                                                            quantity.QTY_VALUE = "1";//默认传1
                                                            calcruleref.QUANTITY = quantity;
                                                            //calcruleref.ROUND_RULE = "";//默认为空
                                                            //不传 calcruleref.FOR_REL_SCLITM = "";
                                                            calcruleref.CALC_RULE_LEVEL = "R";//传R

                                                            calcrulereflist.Add(calcruleref);
                                                            validity.CALCRULEREF = calcrulereflist.ToArray();


                                                            List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM> rates_dimlist = new List<Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM>();
                                                            //foreach
                                                            if (bjvalsdt != null && bjvalsdt.Rows.Count > 0)
                                                            {
                                                                List<string> hadvallist = new List<string>();
                                                                foreach (DataRow drval in bjvalsdt.Rows)
                                                                {
                                                                    if (!hadvallist.Contains(drval[calcref + "CODE"] + ""))
                                                                    {
                                                                        hadvallist.Add(drval[calcref + "CODE"] + "");
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM rates_dim = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIM();
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1 scale_item1 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM1();
                                                                        scale_item1.SCALE_ITEM = drval[calcref + "CODE"] + "";//标度值字符
                                                                        scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(calcref, scale_item1.SCALE_ITEM);
                                                                        scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[calcref + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = "100";//金额
                                                                        rates_dim.SCALE_ITEM1 = scale_item1;
                                                                        rates_dimlist.Add(rates_dim);
                                                                    }
                                                                }
                                                            }
                                                            validity.RATES_DIM = rates_dimlist.ToArray();

                                                            validitylist.Add(validity);

                                                            rate601.VALIDITY = validitylist.ToArray();

                                                            rate601list.Add(rate601);

                                                            rate601create.IT_RATE = rate601list.ToArray();

                                                            Rate601Patched(ref rate601create);
                                                            Z2FM_SQ_RATE_CREATE_RESET_RETURN[] resrate = rate601service.Exec(rate601create);

                                                            if (resrate != null && resrate.Count() > 0)
                                                            {
                                                                foreach (var rr in resrate)
                                                                {
                                                                    if (null != rr.MSG)
                                                                    {
                                                                        foreach (var rm in rr.MSG)
                                                                        {
                                                                            if ("E" == rm.MSG_TYPE)
                                                                            {
                                                                                rateflag = false;
                                                                                ratemsg += feename + feecode + "：" + rm.MSG_TEXT + "<br>";

                                                                            }
                                                                        }

                                                                        if (!rateflag)
                                                                        {
                                                                            goto rtnLabel;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            tccs_itemref.RATE_ID = str_rate_id_ref;

                                                            if (tccs_itemref.TCCALCRESINS040 == "STND")
                                                            {
                                                                tccs_itemref.CLCRESBAS036 = bjvalsdt.Rows[0]["JXJC"] + "";
                                                                tccs_itemref.RULE101 = drpsf["CONDITION"] + "";
                                                            }
                                                            //tccs_itemref.MIN_AMOUNT = bjvalsdt.Rows[0]["MINBJPRICE"] + "";
                                                            if (islsc && "STND" == tccs_itemref.TCCALCRESINS040)
                                                            {
                                                                tccs_itemref.ITEM_DYFWLX = item_dyfwlxlist.ToArray();
                                                            }
                                                            tccs_itemlist.Add(tccs_itemref);
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (drpsf["BJFS"] + "" == "1")
                            {
                                //Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemcost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                                //tccs_itemcost.KEY = SQMTMInterface.genITEMKEY();
                                //tccs_itemcost.LINENR = line_auto.ToString();
                                //line_auto++;
                                //tccs_itemcost.TCCALCRESINS040 = "COST";//指令类型 
                                ////tccs_itemcost.CLCRESBAS036 = drpsf["JXJC"] + "";
                                //tccs_itemcost.COST_PULL_STRATEGY = "2";
                                //tccs_itemcost.SOURCE_CHARGE = "3";

                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST item_cost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                item_cost.TCET084 = feecode;

                                item_costlist.Add(item_cost);

                                //tccs_itemcost.ITEM_COST = item_costlist.ToArray();

                                //tccs_itemlist.Add(tccs_itemcost);
                            }
                        }
                        if (item_costlist.Count > 0)
                        {
                            Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM tccs_itemcost = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEM();
                            tccs_itemcost.KEY = SQMTMInterface.genITEMKEY();
                            tccs_itemcost.LINENR = line_auto.ToString();
                            line_auto++;
                            tccs_itemcost.TCCALCRESINS040 = "COST";//指令类型 
                            //tccs_itemcost.CLCRESBAS036 = drpsf["JXJC"] + "";
                            tccs_itemcost.COST_PULL_STRATEGY = "2";
                            tccs_itemcost.SOURCE_CHARGE = "3";

                            List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST> item_costlistfinal = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST>();
                            foreach (Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST costorg in item_costlist)
                            {
                                if (item_costlistfinal.Where(x => x.TCET084 == costorg.TCET084).Count() < 1)
                                {
                                    item_costlistfinal.Add(costorg);
                                    string costrefsql = string.Format(" SELECT FEECODE FROM SQM_COST_REF WHERE GROUPID IN (SELECT GROUPID FROM SQM_COST_REF WHERE FEECODE = '{0}') AND FEECODE != '{0}' ", costorg.TCET084);
                                    DataTable dtcostref = DataHelper.QueryDataTable(costrefsql);
                                    if (null != dtcostref && dtcostref.Rows.Count > 0)
                                        foreach (DataRow drothcost in dtcostref.Rows)
                                        {
                                            if (item_costlistfinal.Where(x => x.TCET084 == drothcost["FEECODE"] + "").Count() < 1)
                                            {
                                                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST costfinal = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMTCCS_ROOTTCCS_ITEMITEM_COST();
                                                costfinal.CHRGCATCD021_I = costorg.CHRGCATCD021_I;
                                                costfinal.KEY = costorg.KEY;
                                                costfinal.TCCLASS037 = costorg.TCCLASS037;
                                                costfinal.TCET084 = drothcost["FEECODE"] + "";
                                                item_costlistfinal.Add(costfinal);
                                            }
                                        }
                                }
                            }
                            //tccs_itemcost.ITEM_COST = item_costlist.ToArray();
                            tccs_itemcost.ITEM_COST = item_costlistfinal.ToArray();
                            tccs_itemlist.Add(tccs_itemcost);
                        }
                        tccs_root.TCCS_ITEM = tccs_itemlist.ToArray();
                        fag_item100.TCCS_ROOT = tccs_root;
                    }

                    fwa.FAG_ITEM = fag_itemlist.ToArray();
                    fwalist.Add(fwa);
                    FWA701Patched(ref fwalist);
                    //return Content(new JsonMessage { Success = false, Data = null, Code = "-1", Message = "测试" }.ToString());
                    Z2FM_SQ_FWA_CREATE_RES resfwa = fwa701service.Operation_1(fwalist.ToArray());//协议创建
                    Z2FM_SQ_FWA_CREATE_RESET_MSG[] MSG = resfwa.ET_MSG;
                    string item_nolist = "";

                    if (MSG == null)
                    {

                        Z2FM_SQ_FWA_CREATE_RESET_FWA[] res = resfwa.ET_FWA;
                        //写入ItemNO
                        foreach (var items in res)
                        {
                            foreach (var itm in items.FAG_ITEM)
                            {
                                if (!string.IsNullOrEmpty(itm.ITEM_TYPE) || !string.IsNullOrEmpty(itm.SERVICE_PRODUCT_ID))
                                {
                                    item_nolist += itm.ITEM_NO.TrimStart('0') + ",";
                                }
                            }
                        }
                        item_nolist = item_nolist.TrimEnd(',');
                    }
                    SQM_FWA_REF sqm_fwa_ref = new SQM_FWA_REF();
                    sqm_fwa_ref.MRID = keyvalue;
                    sqm_fwa_ref.ZVER = zver;
                    sqm_fwa_ref.FWA = bj_creatFwa;//SQMTMInterface.GenerateFWASerial(strFWAFAGRMNTID044);
                    sqm_fwa_ref.CREATEUSER = SQMHelper.getStaffKey();
                    sqm_fwa_ref.ITEMNO = item_nolist;
                    sqm_fwa_ref.DoCreate();

                    if (resfwa != null && resfwa.ET_MSG != null)
                    {
                        foreach (var fm in resfwa.ET_MSG)
                        {
                            if ("E" == fm.TYPE)
                            {
                                fwaflag = false;
                                fwamsg += fm.MESSAGE + "<br>";
                            }
                        }
                    }

                    if (fwaflag)
                    {
                        rtnfwa += resfwa.ET_FWA[0].FAGRMNTID044 + "<br>";
                        fwafagrmntid044List.Remove(resfwa.ET_FWA[0].FAGRMNTID044);
                    }
                }

            rtnLabel:
                rtnmsg += ratemsg + fwamsg;
                rtnflag = rtnflag && rateflag && fwaflag;
                if (rtnflag && hadA2S && a2sprdslist.Count > 0 && !a2sflag)
                {
                    hadA2S = false;
                    a2sflag = true;
                    goto A2SLabel;
                }
                if (rtnflag && rtnfwa.Length == 0 && rtnmsg.Length == 0)
                {
                    rtnflag = false;
                    rtnmsg = "没有可以提交TM的数据！";
                }
                if (rtnflag && rtnfwa.Length > 0)
                {
                    rtnmsg = "提交TM成功！协议号：<br>" + rtnfwa.TrimEnd();
                }

                removeFailedFWA(fwafagrmntid044List, keyvalue, zver);

                #region  销售易创建 报价接口 已提交TM
                BJWriteback.CreateQuotation wb = new Web.BJWriteback.CreateQuotation();
                BJWriteback.phCreateQuotation head = new BJWriteback.phCreateQuotation();
                BJWriteback.pbCreateQuotation[] body = new BJWriteback.pbCreateQuotation[0];
                BJWriteback.msgResponse msg = wb.CallCreateQuotation(head, body);
                #endregion

                return Content(new JsonMessage { Success = rtnflag, Data = null, Code = "1", Message = rtnmsg }.ToString());
            }
            catch (Exception ex)
            {
                removeFailedFWA(fwafagrmntid044List, keyvalue, zver);
                return Content(new JsonMessage { Success = false, Data = null, Code = "-1", Message = ex.Message }.ToString());
            }
        }

        private void removeFailedFWA(List<string> fwafailed, string mrid, string zver)
        {
            if (null == fwafailed || fwafailed.Count < 1) return;
            try
            {
                foreach (string sr in fwafailed)
                {
                    SQM_FWA_REF fwaref = SQM_FWA_REF.FindFirstByProperties(SQM_FWA_REF.Prop_FWA, sr, SQM_FWA_REF.Prop_MRID, mrid, SQM_FWA_REF.Prop_ZVER, zver);
                    if (null != fwaref)
                    {
                        fwaref.DoDelete();
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion



        private static DataTable dtdjpsfdict = new DataTable();
        private static DataTable dtbjpsfdict = new DataTable();
        private static DataTable dtverdict = new DataTable();

        [Foqus.SQTracker]
        public ActionResult QM_PriceIndex()
        {
            string sql = @"select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 order by ltrim(OBJID,'0')";
            DataTable Orgdt = DataHelper.QueryDataTable(sql);
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            ViewBag.OrgData = Orgdt;
            return View();
        }
        public ActionResult QM_Export()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }
        public ActionResult QM_SendEmail()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            #region  销售易创建 报价接口 发送客户
            BJWriteback.CreateQuotation wb = new Web.BJWriteback.CreateQuotation();
            BJWriteback.phCreateQuotation head = new BJWriteback.phCreateQuotation();
            BJWriteback.pbCreateQuotation[] body = new BJWriteback.pbCreateQuotation[0];
            BJWriteback.msgResponse msg = wb.CallCreateQuotation(head, body);
            #endregion
            return View();
        }
        [Foqus.SQTracker]
        public ActionResult QM_PriceEdit()
        {
            UpdateDict();
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            ViewBag.srvbj = ConfigHelper.AppSettings("srvbj");
            // 非标原因
            string sql = "select Rid,ReasonCode,ReasonName,MEMO from SQM_FBREASON where status='1' order by reasonCode ";
            var dt = DataHelper.QueryDataTable(sql);
            string bjtype = "0";
            //报价类型
            try
            {
                bjtype = DataHelper.QueryValue(string.Format(@"select FBPRICE from SQM_BJ_MAIN_BASIC where RID='{0}'", Request["keyValue"].ToString())) + "";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            ViewBag.bjtype = bjtype;
            ViewBag.FbReasonData = dt;

            return View();
        }
        /// <summary>
        /// 初始化字典
        /// </summary>
        private static void UpdateDict()
        {
            // 初始化字典
            dtdjpsfdict = DataHelper.QueryDataTable("select RID,PRDCODE,SRVCODE,FEECODE,ORGRID from SQM_DJ_PSF");
            dtbjpsfdict = DataHelper.QueryDataTable("select RID,VRID,BJSTATAUS,PRODUCT_CODE,SERVICE_CODE,FEE_CODE,MINSTATUS,BGFZRID from SQM_BJ_PSF");
            dtverdict = DataHelper.QueryDataTable("select RID,MRID,ZVER,STATUS,ORGRID from SQM_BJ_VER");
        }
        public ActionResult QM_FeeEdit()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }
        /// <summary>
        /// 从crm接口拿到产品信息 => 从报价系统产品扩展表取产品数据
        /// </summary>
        /// <returns></returns>
        public string GetPrdFromCrm()
        {
            var data = "";
            //BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            try
            {
                //IDbConnection conn = new OracleConnection();
                //conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                //if (conn.State != ConnectionState.Open)
                //{
                //    conn.Open();
                //}
                data = JsonHelper.GetJsonString(DataHelper.QueryDataTable(@"SELECT * FROM ( 
       SELECT '' AS  PRODUCTSCODE,SQPRODUCTNAME AS PRODUCTSNAME,CREATETIME ,BUSINESSORG AS DIVISION ,PRODUCTKEY AS PRODUCTDESCRIPTION 
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG = '海运'
       and PRODUCTKEY is not null  
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM ( 
       SELECT '' AS  PRODUCTSCODE,SQPRODUCTNAME AS PRODUCTSNAME,CREATETIME,BUSINESSORG AS DIVISION,PRODUCTKEY AS PRODUCTDESCRIPTION 
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG = '供应链' 
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM ( 
       SELECT '' AS  PRODUCTSCODE, SQPRODUCTNAME AS PRODUCTSNAME,CREATETIME,BUSINESSORG AS DIVISION,PRODUCTKEY AS PRODUCTDESCRIPTION 
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG = '空运' 
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM ( 
       SELECT '' AS  PRODUCTSCODE,SQPRODUCTNAME AS PRODUCTSNAME,CREATETIME,BUSINESSORG AS DIVISION,PRODUCTKEY AS PRODUCTDESCRIPTION 
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG like '%运输%'
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)"));
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        /// <summary>
        /// 从crm接口拿客户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult GetCustomFromCrm(string user, string customname, string mrid = "")
        {
            var customdata = "";
            string orgname = "";
            string orgcode = "";
            string orgwhere = "";
            //loginname = Oncontrol3.Web.Helpers.SessionHelper.GetSessionUser<Oncontrol3.Web.Controllers.FLD_QO_USER>().staffkey;
            //BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            try
            {
                string orgnamecode = DataHelper.QueryValue(string.Format("select distinct sbp.ORGNAME from SQM_BJ_PSF sbp left join SQM_BJ_VER sbv on sbp.VRID=sbv.RID where sbv.MRID='{0}'", mrid)) + "";
                if (!String.IsNullOrEmpty(orgnamecode))
                {
                    orgname = orgnamecode.Split('-')[0];
                    orgcode = orgnamecode.Split('-')[1];
                    orgwhere = "  and t.COMPANYID like '" + orgcode + "%'";
                }
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                customdata = JsonHelper.GetJsonString(DataHelper.QueryDataTable(string.Format("SELECT * FROM ( SELECT CUSTOMERNO as ID,  ( case when name IS NULL then enname else name end) as NAME ,CUSTOMERNO FROM CRM_CUSTOMERBASE WHERE ID IN ( SELECT * FROM( SELECT t.customerid FROM CRM_BUSINESS t WHERE t.businessfollowupid = (SELECT USERID FROM SYSUSER WHERE LOGINNAME = '{0}') and t.estatus = '启用' {1} ORDER BY t.createtime desc)) ) WHERE NAME like '%{2}%' ", user, orgwhere, customname.Trim()), conn));//and t.FOLLOWUPSTATUS = '跟进中'
            }
            catch (Exception ex)
            {

            }
            object[] data = { orgcode, orgname, customdata };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 从crm接口拿客户信息2
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult GetCustomFromCrm2(string user, string customname)
        {
            var data = "";
            //loginname = Oncontrol3.Web.Helpers.SessionHelper.GetSessionUser<Oncontrol3.Web.Controllers.FLD_QO_USER>().staffkey;
            //BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            //user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            try
            {
                //IDbConnection conn = new OracleConnection();
                //conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                //if (conn.State != ConnectionState.Open)
                //{
                //    conn.Open();
                //}
                //data = JsonHelper.GetJsonString(DataHelper.QueryDataTable(string.Format("SELECT CUSTOMERNO as ID,NAME,CUSTOMERNO FROM CRM_CUSTOMERBASE WHERE ID IN ( SELECT t.customerid FROM CRM_BUSINESS t WHERE t.estatus = '启用') and NAME like '%{0}%' ", customname.Trim()), conn));//and t.FOLLOWUPSTATUS = '跟进中'
                //结算方改取报价系统SQM_BJ_BP数据
                data = JsonHelper.GetJsonString(DataHelper.QueryDataTable(string.Format("SELECT distinct BPKEY as ID,BPNAME as NAME FROM MDM_BP")));
            }
            catch (Exception ex)
            {

            }
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 发送邮件 选择客户联系人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult SelectContact(string customid)
        {

            string sql = string.Format("SELECT  CTER.ID,CTER.CUSTOMERID,CTER.CUSTOMERNO,CTER.CUSTOMERNAME, CTER.NAME, CTWAY.CONTENT FROM CRM_CUS_CONTACTER CTER LEFT JOIN CRM_CUS_CON_CONTACTWAY CTWAY ON CTER.ID = CTWAY.CONTACTERID   WHERE CTER.CUSTOMERNO='{0}' AND CTWAY.COMMUNTOOLS = '邮箱'", customid);
            string sql_page = @"With DATASET AS( select A.*,ROWNUM As RN from ({0}) A ) select * from DATASET  WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            string countsql = string.Format("SELECT COUNT(*) FROM CRM_CUS_CONTACTER CTER LEFT JOIN CRM_CUS_CON_CONTACTWAY CTWAY ON CTER.ID = CTWAY.CONTACTERID WHERE CTER.CUSTOMERNO='{0}' AND CTWAY.COMMUNTOOLS = '邮箱'", customid);


            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var rtntotal = DataHelper.QueryValue(countsql, conn);

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var rtndata = DataHelper.QueryDataTable(sql_page, conn);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 发送邮件 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult SendEmail(string strmailto, string copy, string title, string body, string RID, string vrid)
        {
            var Sendbool = true;
            var Send = "发送成功!";
            var SE = strmailto + "," + copy;
            SE = SE.Trim(',');
            string[] Strmailtos = SE.Split(',');
            body = body.Replace("\n", "<br>");
            string mailServer = System.Configuration.ConfigurationManager.AppSettings["mailServer"];
            string mailSenderName = System.Configuration.ConfigurationManager.AppSettings["mailSender"];
            string mailAccount = System.Configuration.ConfigurationManager.AppSettings["mailAccount"];
            string mailPass = System.Configuration.ConfigurationManager.AppSettings["mailPassword"];

            for (var i = 0; i < Strmailtos.Length; i++)
            {
                try
                {
                    //创建smtpclient对象   
                    System.Net.Mail.SmtpClient client = new SmtpClient();
                    client.Host = mailServer;//163的smtp服务器是 smtp.163.com   

                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(mailAccount, mailPass);

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    string senderDisplayName = mailSenderName;//这个配置的是发件人的要显示在邮件的名称

                    MailAddress mailfrom = new MailAddress(mailAccount, senderDisplayName, encoding);//发件人邮箱地址，名称，编码UTF8
                    MailAddress mailto = new MailAddress(Strmailtos[i]);//收件人邮箱地址，名称，编码UTF8   
                    //创建mailMessage对象   
                    System.Net.Mail.MailMessage message = new MailMessage(mailfrom, mailto);
                    message.Subject = title;

                    //string str = body;
                    //string imgpattern = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
                    //string url = string.Empty;
                    //string turl = @"cid:";//替换的路径
                    //Regex res = new Regex(imgpattern, RegexOptions.IgnoreCase);
                    //MatchCollection match = res.Matches(str);//str为你要匹配的html代码
                    //if (res.IsMatch(str))
                    //{
                    //    foreach (Match item in match)
                    //    {
                    //        System.Net.Mail.Attachment attachedimg = new System.Net.Mail.Attachment(AppDomain.CurrentDomain.BaseDirectory + item.Groups["imgUrl"].Value.ToString());
                    //        message.Attachments.Add(attachedimg);
                    //        str = str.Replace(item.Groups["imgUrl"].Value.ToString(), turl + attachedimg.ContentId);
                    //    }
                    //}
                    //body = str;


                    Dictionary<string, string> AttachFile = new Dictionary<string, string>();
                    string sql = string.Format("select uploadname,uploadurl from sqm_bj_ver where mrid = '{0}'and  rid='{1}'", RID, vrid);
                    DataTable dt = DataHelper.QueryDataTable(sql);
                    string uploadname = "";
                    string uploadurl = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            uploadname = dr["UPLOADNAME"].ToString();
                            uploadurl = dr["UPLOADURL"].ToString();

                        }
                        if (uploadurl != null && uploadurl != "")
                        {
                            string pjurl = uploadurl + uploadname;
                            AttachFile.Add(uploadname, pjurl);
                            foreach (string skey in AttachFile.Keys)
                            {
                                System.Net.Mail.Attachment objFile = new System.Net.Mail.Attachment(AttachFile[skey].ToString());
                                objFile.Name = skey;
                                message.Attachments.Add(objFile);
                            }
                        }
                    }
                    SQM_BJ_VER sbv = SQM_BJ_VER.Find(vrid);
                    sbv.STATUS = "4";
                    sbv.DoUpdate();
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.BodyEncoding = encoding;
                    message.SubjectEncoding = encoding;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Sendbool = false;
                    Send = ex.Message;
                    return Content(new JsonMessage { Message = Send, Success = Sendbool }.ToString());
                }
            }

            return Content(new JsonMessage { Message = Send, Success = Sendbool }.ToString());
        }
        /// <summary>
        /// 通过多个产品代码返回多个服务信息
        /// </summary>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public ActionResult GetSrvByPrd(string postdata)
        {
            postdata = postdata.Replace('[', '(').Replace(']', ')').Replace("\"", "\'");
            string sql = string.Format("SELECT MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE WHERE MDM_PRD_SRV_REF.PRODUCTCODE IN {0}", postdata);
            var srvArray = DataHelper.QueryObjectsList(sql);
            string feeStr = "";
            for (var i = 0; i < srvArray.Count; i++)
            {
                feeStr += "'" + srvArray[i][1] + "',";
            }
            feeStr = feeStr.TrimEnd(',');
            string sql1 = string.Format("SELECT MDM_SRV_FEE_REF.SRVRQCD121,MDM_SRV_FEE_REF.TCET084,MDM_FEE.TEXTDESC FROM MDM_SRV_FEE_REF LEFT JOIN V_MDM_FEE MDM_FEE ON MDM_SRV_FEE_REF.TCET084 = MDM_FEE.TCET084 WHERE MDM_SRV_FEE_REF.SRVRQCD121 IN  ({0})", feeStr);
            var feeArray = DataHelper.QueryObjectsList(sql1);
            object[] data = { srvArray, feeArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult GetFeeBySrv(string postdata1)
        {
            postdata1 = postdata1.Replace('[', '(').Replace(']', ')').Replace("\"", "\'");
            string sql = string.Format("SELECT MDM_SRV_FEE_REF.SRVRQCD121,MDM_SRV_FEE_REF.TCET084,MDM_FEE.TEXTDESC FROM MDM_SRV_FEE_REF LEFT JOIN V_MDM_FEE MDM_FEE ON MDM_SRV_FEE_REF.TCET084 = MDM_FEE.TCET084 WHERE MDM_SRV_FEE_REF.SRVRQCD121 IN  {0}", postdata1);
            var data = DataHelper.QueryObjectsList(sql);
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 通过产品code列表页新增报价
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewPrice(string postdata, string orgrid, string orgname)
        {
            var mrid = System.Guid.NewGuid().ToString();
            var vrid = System.Guid.NewGuid().ToString();
            var random = System.Guid.NewGuid().ToString().Substring(0, 4);
            var flag = true;
            var rtnmsg = "保存成功";
            var data = "";

            var prdArray = JsonHelper.GetObject<List<PRDOBJ>>(postdata);
            //新建报价主信息
            SQM_BJ_MAIN_BASIC mainobj = new SQM_BJ_MAIN_BASIC();
            mainobj.BJNAME = "报价" + DateTime.Now.ToShortDateString().Replace("/", "") + random;
            mainobj.CREATETIME = DateTime.Now;
            mainobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            mainobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            mainobj.RID = mrid;
            data = mrid;
            mainobj.DoCreate();

            //新建报价信息对应的版本
            SQM_BJ_VER verobj = new SQM_BJ_VER();
            verobj.ZVER = "V1";
            verobj.MRID = mrid;
            verobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            verobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            verobj.STATUS = "0";
            verobj.CREATETIME = DateTime.Now;
            verobj.RID = vrid;
            verobj.ORGRID = orgrid;
            verobj.DoCreate();
            //新建报价信息对应的产品信息,psf表信息对应版本表主键
            for (var i = 0; i < prdArray.Count; i++)
            {
                SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                psfobj.VRID = vrid;
                psfobj.MRID = mrid;
                psfobj.STATUS = "1";
                psfobj.BJSTATAUS = "0";
                psfobj.CHOOSESTATUS = "0";
                psfobj.ORGCODE = orgrid;
                psfobj.ORGNAME = orgname;
                psfobj.PRODUCT_CODE = prdArray[i].prdcode;
                psfobj.PRODUCT_NAME = prdArray[i].prdname;
                psfobj.BUSINESSORG = prdArray[i].businessorg;
                psfobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                psfobj.DoCreate();
            }
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        ///选择商机进行报价
        /// </summary>
        /// <param name="CustomId"></param>
        /// <param name="CustomName"></param>
        /// <returns></returns>
        public ActionResult AddPriceByBus(string CustomId, string CustomName)
        {
            string rtnmsg = "新增成功";
            bool flag = true;
            var mrid = System.Guid.NewGuid().ToString(); //自建主表主键
            var vrid = System.Guid.NewGuid().ToString(); //自建版本表主键
            var random = System.Guid.NewGuid().ToString().Substring(0, 4);
            try
            {
                //1 通过客户id 得到对应的产品
                BJWebServiceSoapClient client = new BJWebServiceSoapClient();
                var res = client.BJ("CustomerProduct", "", CustomId);
                List<PRDFROMCRM> prdList = JsonHelper.GetObject<List<PRDFROMCRM>>(res);
                // 2 报价主表
                SQM_BJ_MAIN_BASIC mainobj = new SQM_BJ_MAIN_BASIC();
                mainobj.BJNAME = "报价" + DateTime.Now.ToShortDateString().Replace("/", "") + random;
                mainobj.CREATETIME = DateTime.Now;
                mainobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.RID = mrid;
                mainobj.FBPRICE = "0";
                mainobj.DoCreate();
                // 3 报价版本表
                SQM_BJ_VER verobj = new SQM_BJ_VER();
                verobj.ZVER = "V1";
                verobj.MRID = mrid;
                verobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.STATUS = "0";
                verobj.CREATETIME = DateTime.Now;
                verobj.RID = vrid;
                verobj.DoCreate();
                // 4 psf表插入产品
                foreach (var item in prdList)
                {
                    SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                    psfobj.VRID = vrid;
                    psfobj.PRODUCT_CODE = item.PRODUCT_CODE;
                    psfobj.PRODUCT_NAME = item.PRODUCT_NAME;
                    psfobj.MRID = mrid;
                    psfobj.DoCreate();
                }
                // 5 客户表插入数据
                SQM_BJ_BP bpobj = new SQM_BJ_BP();
                bpobj.MRID = mrid;
                bpobj.BPCODE = CustomId;
                bpobj.BPNAME = CustomName;
                bpobj.DoCreate();
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = mrid, Message = rtnmsg, Success = flag }));
        }
        public class PRDFROMCRM
        {
            public string PRODUCT_CODE;
            public string PRODUCT_NAME;
        }
        /// <summary>
        /// 得到所有的产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllPrice(string bjname)
        {
            var user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string sql = string.Format("select a.*,b.zver,b.modifytime as mdtime,b.status as sta,c.bpname from sqm_bj_main_basic a left join( select v.*, to_number(replace(v.zver, 'V', '')) from sqm_bj_ver v inner join( select max(to_number(replace(zver, 'V', ''))) vs, mrid from sqm_bj_ver where createuser = '{1}' group by mrid) a on v.mrid = a.mrid and to_number(replace(v.zver, 'V', '')) = a.vs) b on a.rid = b.mrid left join sqm_bj_bp c on a.rid = c.mrid where  (a.fbprice<>'1'or a.fbprice is null) and a.createuser = '{1}'  and a.bjname like '%{0}%'  order by a.createtime desc", bjname.Trim(), user);
            var data = DataHelper.QueryDictList(sql);
            string fwa = "";
            foreach (var item in data)
            {
                sql = string.Format("select * from(select FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}' order by CREATETIME desc) where rownum = 1", item["RID"].ToString(), item["ZVER"].ToString());
                fwa = DataHelper.QueryValue(sql) + "";
                item.Add("FWA", fwa);
            }


            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 非标报价的产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFbPrice(string bjname)
        {
            var user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string sql = string.Format("select a.*,b.zver,b.modifytime as mdtime,b.status as sta,c.bpname from sqm_bj_main_basic a left join( select v.*, to_number(replace(v.zver, 'V', '')) from sqm_bj_ver v inner join( select max(to_number(replace(zver, 'V', ''))) vs, mrid from sqm_bj_ver where createuser = '{1}' group by mrid) a on v.mrid = a.mrid and to_number(replace(v.zver, 'V', '')) = a.vs) b on a.rid = b.mrid left join sqm_bj_bp c on a.rid = c.mrid where a.fbprice='1' and a.createuser = '{1}'  and a.bjname like '%{0}%'  order by a.createtime desc", bjname.Trim(), user);
            var data = DataHelper.QueryDictList(sql);
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 通过主键得到产品、服务、费目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetAllByRid(string keyvalue, string ver)
        {
            //首先通过mrid 得到 对应的vrid
            var vrid = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, ver))[0]["RID"].ToString();
            string sql = string.Format("SELECT DISTINCT product_code, product_name,choosestatus,orgcode FROM sqm_bj_psf WHERE vrid = '{0}' and (status <> '0' or status is null)", vrid);
            var prdArray = DataHelper.QueryObjectsList(sql);// [product_code,product_name]
            string prdcodeStr = "";
            string orgcode = prdArray[0][3].ToString();//组织相同取第一个
            foreach (object[] item in prdArray)
            {
                prdcodeStr += "'" + item[0].ToString().Trim() + "',";
            }
            //string sql2 = string.Format("SELECT distinct MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME,SQM_SRV_EXT.Sord FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE LEFT JOIN SQM_SRV_EXT ON MDM_PRD_SRV_REF.SERVICETYPECODE = SQM_SRV_EXT.SERVICEKEY and  MDM_PRD_SRV_REF.PRODUCTCODE=SQM_SRV_EXT.PRODUCTCODE LEFT JOIN SQM_SRV_FEE_CONFIG ON MDM_PRD_SRV_REF.PRODUCTCODE= SQM_SRV_FEE_CONFIG.PRODCODE AND MDM_PRD_SRV_REF.SERVICETYPECODE=SQM_SRV_FEE_CONFIG.SRVCODE  WHERE MDM_PRD_SRV_REF.PRODUCTCODE IN ({0}) AND SQM_SRV_FEE_CONFIG.SRVDISP='1' order by SQM_SRV_EXT.Sord", prdcodeStr.TrimEnd(','));
            string sql2 = string.Format("select distinct trim(c.PRODCODE),trim(c.SRVCODE),trim(c.SRVNAME),e.SORD from SQM_SRV_FEE_CONFIG c left join SQM_SRV_EXT e on c.PRODCODE=e.PRODUCTCODE and c.SRVCODE=e.SERVICEKEY where  c.PRODCODE in ({0}) and c.SRVDISP = '1' order by e.Sord", prdcodeStr.TrimEnd(','));
            var srvArray = DataHelper.QueryObjectsList(sql2);// [PRODUCTCODE,SERVICETYPECODE,SERVICENAME,Sord]
            //string feeStr = "";
            //for (var i = 0; i < srvArray.Count; i++)
            //{
            //    feeStr += "'" + srvArray[i][1] + "',";
            //}
            //feeStr = feeStr.TrimEnd(',');
            //string sql3 = string.Format("With DATASET AS(select distinct mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj,qdm_fee_srv_ref.sorid from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join v_mdm_fee mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join QDM_FEE_SRV_REF ON MDM_SRV_FEE_REF.Tcet084 = QDM_FEE_SRV_REF.feecode and  MDM_SRV_FEE_REF.srvrqcd121 =QDM_FEE_SRV_REF.SERVICETYPECODE and QDM_FEE_SRV_REF.Productcode=MDM_PRD_SRV_REF.Productcode where mdm_prd_srv_ref.productcode in ({0}) and mdm_srv_fee_ref.tcet084 not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and ISLSC='1' and BGFZRID is not null and BGFZRID<>'1')) select t1.*,t2.feecatg from DATASET t1 inner join SQM_SRV_FEE_CONFIG t2 on t1.productcode=t2.Prodcode and t1.servicetypecode=t2.srvcode and t1.tcet084=t2.feecode and t2.feecatg<>'2'  order by to_number(t1.SORID)", prdcodeStr.TrimEnd(','), vrid);
            //过滤掉已做包干费的费目
            //string sql3 = string.Format("select distinct trim(c.PRODCODE),trim(c.PRODNAME),trim(c.SRVCODE),trim(c.SRVNAME),trim(c.FEECODE),trim(c.FEENAME),r.BXBJ,r.SORID,c.FEECATG,f.FSFYSMS from SQM_SRV_FEE_CONFIG c left join QDM_FEE_SRV_REF r on c.PRODCODE=r.Productcode and c.SRVCODE=r.SERVICETYPECODE and c.FEECODE=r.FEECODE left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on c.FEECODE = f.FEECODE where c.FEECATG<>'2' and c.PRODCODE in ({0}) and c.FEECODE not in(SELECT FEE_CODE FROM SQM_BJ_PSF WHERE VRID = '{1}' and ISLSC='1' and BGFZRID is not null and BGFZRID<>'1') order by r.SORID", prdcodeStr.TrimEnd(','), vrid);
            string sql3 = string.Format("select distinct trim(c.PRODCODE),trim(c.PRODNAME),trim(c.SRVCODE),trim(c.SRVNAME),trim(c.FEECODE),trim(c.FEENAME),r.BXBJ,r.SORID,c.FEECATG,f.FSFYSMS from SQM_SRV_FEE_CONFIG c left join QDM_FEE_SRV_REF r on c.PRODCODE=r.Productcode and c.SRVCODE=r.SERVICETYPECODE and c.FEECODE=r.FEECODE left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on c.FEECODE = f.FEECODE where c.FEECATG<>'2' and c.status='1' and c.PRODCODE in ({0}) order by r.SORID", prdcodeStr.TrimEnd(','), vrid);
            var feeArray = DataHelper.QueryObjectsList(sql3);// [productcode,sqproductname,servicetypecode,servicename,tcet084,textdesc,bxbj,sorid]
            List<object[]> newfeeArr = new List<object[]>(feeArray);
            foreach (var fee in feeArray)
            {
                string feecalcid = "";
                //异常费目再根据有效定价来控制是否展现
                if (fee[8].ToString() == "1")
                {
                    string sql4 = string.Format("select RID from SQM_DJ_PSF t where PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and ORGRID like'%{3}%'", fee[0].ToString(), fee[2].ToString(), fee[4].ToString(), orgcode);
                    DataTable djpsfDt = DataHelper.QueryDataTable(sql4);
                    if (djpsfDt.Rows.Count == 0)
                    {
                        newfeeArr.Remove(fee);//SQM_DJ_PSF没有就不显示
                        continue;
                    }
                    else
                    {
                        feecalcid = djpsfDt.Rows[0]["RID"].ToString();
                        string sql5 = string.Format("select RID from SQM_MODEDJ_VAL where FEECALCID='{0}' and STATUS='1' and DJSTATUS='1'", feecalcid);
                        DataTable djvalDt = DataHelper.QueryDataTable(sql5);
                        if (djvalDt.Rows.Count == 0)
                        {
                            newfeeArr.Remove(fee);//SQM_MODEDJ_VAL没有就不显示
                            continue;
                        }
                    }
                }
            }
            object[] data = { prdArray, srvArray, newfeeArr };
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult GetAllFee(string keyvalue, string ver, string pcode, string srvcode)
        {
            //首先通过mrid 得到 对应的vrid
            var vrid = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, ver))[0]["RID"].ToString();
            var dlfeeArr = DataHelper.QueryObjectsList(string.Format("select FEE_CODE from SQM_BJ_PSF where VRID='{0}' and PRODUCT_CODE='{1}' and SERVICE_CODE='{2}' and ALOENFEE='1'", vrid, pcode, srvcode));
            string where = " and 1=2 ";
            string businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", pcode)) + "";
            if (businessorg == "空运")
            {
                where = " and TCET084 like'A%' ";
            }
            else if (businessorg == "海运")
            {

                where = " and TCET084 like'O%' ";
            }
            else if (businessorg == "供应链")
            {

                where = " and TCET084 like'S%' ";
            }
            else if (businessorg == "运输")
            {

                where = " and TCET084 like'L%' ";
            }
            string sql = string.Format("select distinct  '{0}'as PRODUCTCODE,'{1}' as SERVICETYPECODE,TCET084,TEXTDESC,f.FSFYSMS from V_MDM_FEE c left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on c.TCET084 = f.FEECODE where TCET084 not in (select TCET084 from MDM_SRV_FEE_REF) and TCET084 in(select distinct trim(FEECODE) from SQM_SRV_FEE_CONFIG where PRODCODE is null and SRVCODE is null) and TEXTDESC is not null {2}  order by TEXTDESC", pcode, srvcode, where);
            var feeArray = DataHelper.QueryObjectsList(sql);
            object[] data = { feeArray, dlfeeArr };
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult verTable(string keyvalue)
        {
            string sql = string.Format("select rid,zver,status,dtfrom,dtto,to_char(dtfrom,'yyyy/MM/dd') as dtstart,to_char(dtto, 'yyyy/MM/dd') as dtend, createuser,modifytime,memo,workflow,fbreasoncode,fbreasonother,fbmemo from sqm_bj_ver where mrid = '{0}' order by createtime desc", keyvalue);
            var data = DataHelper.QueryDictList(sql);
            string fwa = "";
            foreach (var item in data)
            {
                //显示一个协议号
                //sql = string.Format("select * from(select FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}' order by CREATETIME desc) where rownum = 1", keyvalue, item["ZVER"].ToString());
                //显示所有协议号
                sql = string.Format("select LISTAGG(to_char(FWA)||'/'||to_char(ITEMNO), ';') WITHIN GROUP(ORDER BY CREATETIME desc) FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}'", keyvalue, item["ZVER"].ToString());
                fwa = DataHelper.QueryValue(sql) + "";
                item.Add("FWA", fwa);
            }



            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult showHead(string keyvalue)
        {
            string sql = string.Format("SELECT sqm_bj_main_basic.bjname,sqm_bj_biz.bizname,sqm_bj_biz.bizid,sqm_bj_biz.bizid,sqm_bj_bp.bpname,sqm_bj_bp.bpcode FROM sqm_bj_main_basic LEFT JOIN sqm_bj_biz ON sqm_bj_main_basic.rid = sqm_bj_biz.mrid LEFT JOIN sqm_bj_bp ON sqm_bj_main_basic.rid = sqm_bj_bp.mrid WHERE sqm_bj_main_basic.rid = '{0}'", keyvalue);
            var data = DataHelper.QueryDictList(sql);
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 邮件拿销售人员信息
        /// </summary>
        public ActionResult GetSalesperson()
        {
            string userid = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string sql = string.Format("select NAME,EMAIL,PHONE,HOMEPHONE from sysuser where workno='{0}'", userid);
            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var data = DataHelper.QueryDataTable(sql, conn);

            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 显示附件
        /// </summary>
        public ActionResult showFile(string RID)
        {
            string vrid = Request["vrid"] + "";
            string sql = string.Format("select uploadname,showmode,to_char(uploadtime,'yyyy-mm-dd hh24:mi:ss') as uploadtime,uploadurl from sqm_bj_ver where mrid = '{0}' and rid = '{1}'", RID, vrid);
            var data = DataHelper.QueryDictList(sql);
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 如果原来有现在没有，则删除（物理）值表跟psf 都删除，如果原来有现在还有保留原来数据   数据无效一定是0 ，有效不一定是1
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="keyvalue"></param>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public ActionResult SaveToPSF(string postdata, string keyvalue, string vrid, string prdcode, string srvcode, string aloenfee = "")
        {
            // 查询原来有的数据
            string sql = string.Format("SELECT RID,VRID,PRODUCT_CODE,SERVICE_CODE,FEE_CODE,ALOENFEE,ORGCODE,ORGNAME,BUSINESSORG FROM SQM_BJ_PSF WHERE VRID = '{0}' and (status <> '0' or status is null) and (BGFZRID is null or BGFZRID='1') and (FEECATG<>'2' OR FEECATG is null)", vrid);
            DataTable dt = DataHelper.QueryDataTable(sql);
            string orgcode = "";
            string orgname = "";
            string businessorg = "";
            if (dt.Rows.Count > 0)
            {
                orgcode = dt.Rows[0]["ORGCODE"].ToString();
                orgname = dt.Rows[0]["ORGNAME"].ToString();
            }
            // 保存进来的新数据
            List<PRD> dataArray = JsonHelper.GetObject<List<PRD>>(postdata);
            var rtnmessage = "保存成功";
            try
            {
                foreach (var p in dataArray)
                {
                    //获取该产品的事业部
                    DataRow[] sybdrs = dt.Select("PRODUCT_CODE = '" + p.prdcode + "'");
                    if (sybdrs.Length > 0)
                    {
                        businessorg = sybdrs[0]["BUSINESSORG"].ToString();
                    }
                    //查询现有的服务
                    string sersql = string.Format("select distinct SERVICE_CODE from SQM_BJ_PSF where VRID='{0}' and PRODUCT_CODE='{1}' and FEECATG='2'", vrid, p.prdcode);
                    DataTable srvDt = DataHelper.QueryDataTable(sersql);
                    if (p.srvcodes.Count == 0)// 只有产品
                    {
                        //先查询是否有这条数据
                        DataRow[] drs = dt.Select("PRODUCT_CODE = '" + p.prdcode + "' and SERVICE_CODE is null and FEE_CODE is null and VRID = '" + vrid + "'");// 取产品
                        if (drs.Length <= 0)
                        {
                            SQM_BJ_PSF srcobj = new SQM_BJ_PSF();
                            srcobj.MRID = keyvalue;
                            srcobj.VRID = vrid;
                            srcobj.CHOOSESTATUS = "0";// 非选中产品
                            srcobj.PRODUCT_CODE = p.prdcode;
                            srcobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                            srcobj.BJSTATAUS = "0";
                            srcobj.STATUS = "1";
                            srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                            srcobj.ORGCODE = orgcode;
                            srcobj.ORGNAME = orgname;
                            srcobj.BUSINESSORG = businessorg;
                            srcobj.DoCreate();
                        }
                        else
                        {
                            dt.Rows.Remove(drs[0]);// 删除共有的，剩余的就是要从表中删除的数据
                        }
                    }
                    else
                    {
                        foreach (var s in p.srvcodes)
                        {
                            foreach (var f in s.feecodes)
                            {
                                string where = "";
                                if (aloenfee == "1")
                                {
                                    where = " and ALOENFEE = '1'";
                                }
                                //先查询是否有这条数据
                                DataRow[] drs = dt.Select("PRODUCT_CODE = '" + p.prdcode + "' and SERVICE_CODE = '" + s.srvcode + "' and FEE_CODE = '" + f + "' and VRID = '" + vrid + "'" + where);// 取第一行，估计没有多行的吧
                                if (drs.Length <= 0)// 说明原来有的，现在还有，则数据不变
                                {
                                    SQM_BJ_PSF srcobj = new SQM_BJ_PSF();
                                    srcobj.MRID = keyvalue;
                                    srcobj.VRID = vrid;
                                    srcobj.CHOOSESTATUS = "1";
                                    srcobj.PRODUCT_CODE = p.prdcode;
                                    srcobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                                    srcobj.SERVICE_CODE = s.srvcode;
                                    srcobj.SERVICE_NAME = CODETONAME("srv", s.srvcode);
                                    srcobj.FEE_CODE = f;
                                    srcobj.FEE_NAME = CODETONAME("fee", f);
                                    srcobj.BJSTATAUS = "0";
                                    srcobj.STATUS = "1";
                                    if (aloenfee == "1")
                                    {
                                        srcobj.ALOENFEE = "1";
                                    }
                                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                    srcobj.ORGCODE = orgcode;
                                    srcobj.ORGNAME = orgname;
                                    srcobj.BUSINESSORG = businessorg;
                                    srcobj.DoCreate();
                                }
                                else
                                {
                                    dt.Rows.Remove(drs[0]);// 删除共有的，剩余的就是要从表中删除的数据
                                }
                            }
                            //该服务下所有的atcost费目存入SQM_BJ_PSF
                            SQM_BJ_PSF[] sbpArr = SQM_BJ_PSF.FindAllByProperties(SQM_BJ_PSF.Prop_PRODUCT_CODE, p.prdcode, SQM_BJ_PSF.Prop_SERVICE_CODE, s.srvcode, SQM_BJ_PSF.Prop_VRID, vrid, SQM_BJ_PSF.Prop_FEECATG, "2");
                            if (sbpArr.Length == 0)
                            {
                                string costsql = string.Format("select PRODCODE,SRVCODE,FEECODE from SQM_SRV_FEE_CONFIG t where PRODCODE='{0}' and SRVCODE='{1}' and FEECATG='2'", p.prdcode, s.srvcode);
                                DataTable costDt = DataHelper.QueryDataTable(costsql);
                                foreach (DataRow costdr in costDt.Rows)
                                {
                                    SQM_BJ_PSF sbpobj = new SQM_BJ_PSF();
                                    sbpobj.MRID = keyvalue;
                                    sbpobj.VRID = vrid;
                                    sbpobj.CHOOSESTATUS = "1";
                                    sbpobj.PRODUCT_CODE = p.prdcode;
                                    sbpobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                                    sbpobj.SERVICE_CODE = s.srvcode;
                                    sbpobj.SERVICE_NAME = CODETONAME("srv", s.srvcode);
                                    sbpobj.FEE_CODE = costdr["FEECODE"].ToString();
                                    sbpobj.FEE_NAME = CODETONAME("fee", costdr["FEECODE"].ToString());
                                    sbpobj.BJSTATAUS = "2";
                                    sbpobj.STATUS = "1";
                                    sbpobj.BJFS = "1";
                                    sbpobj.FEECATG = "2";
                                    sbpobj.MINSTATUS = "0";
                                    if (aloenfee == "1")
                                    {
                                        sbpobj.ALOENFEE = "1";
                                    }
                                    sbpobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                    sbpobj.ORGCODE = orgcode;
                                    sbpobj.ORGNAME = orgname;
                                    sbpobj.BUSINESSORG = businessorg;
                                    sbpobj.DoCreate();
                                }
                            }
                            else
                            {
                                DataRow[] srvdrs = srvDt.Select("SERVICE_CODE='" + s.srvcode + "'");
                                if (srvdrs.Length > 0)
                                {
                                    srvDt.Rows.Remove(srvdrs[0]);//服务存在的不做处理
                                }
                            }
                        }
                    }
                    string srvstr = "";
                    foreach (DataRow srvDr in srvDt.Rows)
                    {
                        srvstr += srvDr["SERVICE_CODE"].ToString() + ",";
                    }
                    if (!String.IsNullOrEmpty(srvstr))
                    {
                        string deletesrv = string.Format("delete from SQM_BJ_PSF where vrid='{0}' and PRODUCT_CODE='{1}' and SERVICE_CODE in('{2}') and FEECATG='2'", vrid, p.prdcode, srvstr.TrimEnd(',').Replace(",", "','"));
                        DataHelper.ExecSql(deletesrv);
                    }
                }
                // 删除psf表未保存的原有数据，并清空值表数据
                if (String.IsNullOrEmpty(aloenfee))
                {
                    DataRow[] feedrs = dt.Select("ALOENFEE is null");
                    foreach (DataRow feedr in feedrs)
                    {
                        string deletepsf = "delete from sqm_bj_psf where rid = '" + feedr["RID"] + "'";
                        string deleteval = "delete from sqm_modebj_val where feecalcid = '" + feedr["RID"] + "'";
                        DataHelper.ExecSql("begin " + deletepsf + ";" + deleteval + ";end;");
                    }
                }
                else
                {
                    DataRow[] dlfeedrs = dt.Select("PRODUCT_CODE = '" + prdcode + "' and SERVICE_CODE='" + srvcode + "' and ALOENFEE='1'");
                    foreach (DataRow dlfeedr in dlfeedrs)
                    {
                        string deletepsf = "delete from sqm_bj_psf where rid = '" + dlfeedr["RID"] + "'";
                        string deleteval = "delete from sqm_modebj_val where feecalcid = '" + dlfeedr["RID"] + "'";
                        DataHelper.ExecSql("begin " + deletepsf + ";" + deleteval + ";end;");
                    }
                }
            }
            catch (Exception ex)
            {
                rtnmessage = ex.Message;
            }
            // 更新字典
            UpdateDict();
            return Content(JsonHelper.GetJsonString(rtnmessage));
        }
        /// <summary>
        /// 检查是否存在Atcost费目 如果存在则确认掉
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckAtcost(string vrid)
        {
            try
            {
                string exist = "Y";
                string count = DataHelper.QueryValue("select count(*) from sqm_bj_psf where vrid = '" + vrid + "' and bjfs = '1' and feecatg = '2' and bjstataus != '2'") + "";// 没有筛选是否有效数据
                if (count == "0")
                {
                    exist = "N";
                }
                else
                {
                    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2' where rid in(select rid from sqm_bj_psf where bjfs = '1' and feecatg = '2' and vrid = '" + vrid + "' and bjstataus != '2')");
                }
                return Content(exist);
            }
            catch
            {
                return Content("异常");
            }
        }
        /// <summary>
        /// 确认全部费目
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmFee(string psfridArr)
        {
            DataTable dtjxjc = DataHelper.QueryDataTable("select djfsrid,djfsname,gdzrid,feecode,fsprecond,fsrslbase from sqm_fee_pur_ref where status = '1'");// 定价方式2
            DataTable dtqttj = DataHelper.QueryDataTable("select feecode,precond from sqm_fee_calc");
            DataTable dtpsf = DataHelper.QueryDataTable("select mrid,rid from sqm_bj_psf");
            List<string> dataErrorLog = new List<string>();// 高低值错误信息
            try
            {
                if (!string.IsNullOrEmpty(psfridArr))
                {
                    DataTable dt = JsonHelper.GetObject<DataTable>(psfridArr);
                    string original = "";
                    int count = 0;
                    int countwbc = 0;// 记录未保存条数
                    int sum = 0;// 记录总条数
                    foreach (DataRow dr in dt.Rows)
                    {
                        sum++;
                        string djrid = dr["DJRID"] + "";
                        string bjrid = dr["BJRID"] + "";
                        if (count == 0)
                        {
                            if (bjrid != "")
                            {
                                original = DataHelper.QueryValue("select distinct t1.original from sqm_bj_main_basic t1,sqm_bj_psf t2 where t1.rid = t2.mrid and t2.rid = '" + bjrid + "'") + "";
                            }
                            count++;
                        }
                        string str = DataHelper.QueryValue("select bjstataus || ',' || bjfs from sqm_bj_psf where rid = '" + bjrid + "' and (status <> '0' or status is null)") + "";
                        string djfs = DataHelper.QueryValue("select djfs from sqm_dj_psf where rid = '" + djrid + "'") + "";//and (status <> '0' or status is null)
                        string[] arr = str.Split(',');
                        string bjstatus = "";
                        string bjfs = "";
                        if (arr.Length > 0)
                        {
                            bjstatus = arr[0];
                            bjfs = arr[1];
                            if (bjstatus == "0")// 从定价值表拿数据
                            {
                                countwbc++;
                                //string psfstatus = "2";
                                //// 前提条件
                                //DataTable dtpsfname = DataHelper.QueryDataTable("select * from sqm_dj_psf where rid = '" + djrid + "'");
                                //string feecode = dtpsfname.Rows[0]["FEECODE"] + "";
                                //string qttj = "";
                                //DataRow[] drs = dtqttj.Select("feecode = '" + feecode + "'");
                                //if (drs.Length > 0)
                                //{
                                //    qttj = drs[0]["PRECOND"] + "";
                                //}
                                //// 高低值 -- 没组定价方式下的高低值(普通报价：非At cost/非单票单询)
                                //// 获取定价方式rid  从sqm_fee_pur_ref 获取定价方式：1纯有基础 2有基础和无基础高低值同时存在
                                //bool gdzcountnum = false;
                                //if (bjfs != "1" && bjfs != "2")
                                //{
                                //    string feename = dtpsfname.Rows[0]["FEENAME"] + "";
                                //    string srvname = dtpsfname.Rows[0]["SRVNAME"] + "";
                                //    string prdname = dtpsfname.Rows[0]["PRDNAME"] + "";
                                //    gdzcountnum = GdzCheck(feecode, "sqm_modedj_val", djrid);
                                //    if (gdzcountnum)
                                //    {
                                //        dataErrorLog.Add("\"" + prdname + " - " + srvname + " - " + feename + "\"，请维护有关高低值的报价信息");
                                //        continue;
                                //    }
                                //}
                                //DataTable djval = DataHelper.QueryDataTable("select * from sqm_modedj_val where feecalcid = '" + djrid + "' and status = '1' and DJSTATUS <> '0'");
                                //if (djval.Rows.Count > 0)
                                //{
                                //    // 判断指导价是否超限，解决可能会出现的业务问题
                                //    foreach (DataRow drdj in djval.Rows)
                                //    {
                                //        Decimal minPrice = Convert.ToDecimal(drdj["MINPRICE"] + "");
                                //        Decimal maxPrice = Convert.ToDecimal(drdj["MAXPRICE"] + "");
                                //        Decimal guidePrice = Convert.ToDecimal(drdj["GUIDEPRICE"] + "");
                                //        if (guidePrice < minPrice || guidePrice > maxPrice)
                                //        {
                                //            psfstatus = "4";
                                //            break;
                                //        }
                                //    }
                                //    // 报价值表插数  处理解析基础问题
                                //    djval.Columns.Remove("FEECALCID");
                                //    djval.Columns.Remove("BJRID");
                                //    djval.Columns["MIN"].ColumnName = "MINBJPRICE";
                                //    djval.Columns["RID"].ColumnName = "DJRID";
                                //    List<SQM_MODEBJ_VAL> listObj = TableToEntity<SQM_MODEBJ_VAL>(djval);
                                //    foreach (SQM_MODEBJ_VAL smv in listObj)
                                //    {
                                //        string jxjc = "";
                                //        DataRow[] drjxjc = dtjxjc.Select();
                                //        if (!string.IsNullOrEmpty(smv.GDZRID))
                                //        {
                                //            drjxjc = dtjxjc.Select("djfsrid = '" + smv.DJFSRID + "' and gdzrid = '" + smv.GDZRID + "'");
                                //        }
                                //        else if (!string.IsNullOrEmpty(smv.DJFSRID))
                                //        {
                                //            drjxjc = dtjxjc.Select("djfsrid = '" + smv.DJFSRID + "' and gdzrid is null");
                                //        }
                                //        if (drjxjc.Length > 0)
                                //        {
                                //            jxjc = drjxjc[0]["FSRSLBASE"] + "";
                                //        }
                                //        smv.JXJC = jxjc;
                                //        smv.FEECALCID = bjrid;
                                //        smv.BJPRICE = smv.GUIDEPRICE;
                                //        smv.IFBJITEM = "1";
                                //        smv.STATUS = "1";
                                //        smv.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                //        smv.DoCreate();
                                //    }
                                //    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '" + psfstatus + "',bjfs = '0',condition = '" + qttj + " ' where rid = '" + bjrid + "'");
                                //}
                                //else if (djfs == "1")// at cost 
                                //{
                                //    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2',bjfs = '1',condition = '" + qttj + " ' where rid = '" + bjrid + "'");
                                //}
                                //else if (djfs == "2")// 单票单询
                                //{
                                //    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2',bjfs = '2',condition = '" + qttj + " ' where rid = '" + bjrid + "'");
                                //}
                            }
                            else// 从报价值表拿数据
                            {
                                string psfstatus = "2";
                                DataTable dtpsfname = DataHelper.QueryDataTable("select * from sqm_bj_psf where rid = '" + bjrid + "'");
                                // 高低值--没组定价方式下的高低值(普通报价：非At cost/非单票单询)
                                // 获取定价方式rid  从sqm_fee_pur_ref 获取定价方式：1纯有基础 2有基础和无基础高低值同时存在
                                bool gdzcountnum = false;
                                if (bjfs != "1" && bjfs != "2")
                                {
                                    string feecode = dtpsfname.Rows[0]["FEE_CODE"] + "";
                                    string feename = dtpsfname.Rows[0]["FEE_NAME"] + "";
                                    string srvname = dtpsfname.Rows[0]["SERVICE_NAME"] + "";
                                    string prdname = dtpsfname.Rows[0]["PRODUCT_NAME"] + "";
                                    gdzcountnum = GdzCheck(feecode, "sqm_modebj_val", bjrid);
                                    if (gdzcountnum)
                                    {
                                        dataErrorLog.Add("\"" + prdname + " - " + srvname + " - " + feename + "\"，请维护有关高低值的报价信息");
                                        continue;
                                    }
                                }
                                DataTable bjval = DataHelper.QueryDataTable("select * from sqm_modebj_val where feecalcid = '" + bjrid + "' and status = '1'");
                                if (bjfs == "1")// at cost 
                                {
                                    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2',bjfs = '1' where rid = '" + bjrid + "'");
                                }
                                else if (bjfs == "2")// 单票单询
                                {
                                    DataHelper.ExecSql("update sqm_bj_psf set bjstataus = '2',bjfs = '2' where rid = '" + bjrid + "'");
                                }
                                else if (bjval.Rows.Count > 0)
                                {
                                    if (original == "")
                                    {
                                        foreach (DataRow row in bjval.Rows)
                                        {
                                            if ((row["MINPRICE"] + "") == "" && (row["MAXPRICE"] + "" == "") && (row["GUIDEPRICE"] + "" == ""))
                                            {
                                                psfstatus = "3";
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
                                                    psfstatus = "5";// 已保存点确认-> 已确认（报价超限）
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    DataHelper.ExecSql("begin update sqm_bj_psf set bjstataus = '" + psfstatus + "' where rid = '" + bjrid + "';update sqm_modebj_val set ifbjitem = '1' where feecalcid = '" + bjrid + "'; end;");
                                }
                            }
                        }
                    }
                    if (sum == countwbc)
                    {
                        return Content(new JsonMessage { Success = false, Message = "无可确认费目！费目都是未保存状态" }.ToString());
                    }
                    if (dataErrorLog.Count > 0)
                    {
                        return Content(new JsonMessage { Success = false, Message = string.Join("<BR>", dataErrorLog.ToArray()) }.ToString());
                    }
                    else
                    {
                        return Content(new JsonMessage { Success = true, Message = "确认费目成功！" }.ToString());
                    }
                }
                else
                {
                    return Content(new JsonMessage { Success = false, Message = "无可确认费目！" }.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Message = ex.Message }.ToString());
            }
        }

        /// <summary>
        /// 确认前检查高低值组数据是否都存在，如果存在一个没有数据的高低值定价（报价）方式，则不予确认
        /// </summary>
        /// <param name="feecode"></param>
        /// <param name="gdzcountnum"></param>
        private static bool GdzCheck(string feecode, string tablename, string psfrid)
        {
            bool gdzcountnum = false;
            IList<EasyDictionary> ediclist = DataHelper.QueryDictList("select distinct DJFSRID from SQM_FEE_CALC_REF where STATUS = '1' and FEECODE = '" + feecode + "' and (DJFSRID <> '' or DJFSRID is not null)");
            DataTable dtdjfswjc = DataHelper.QueryDataTable("select distinct djfsrid from sqm_fee_pur_ref where djfsrid not in(select djfsrid from sqm_fee_calc_ref where feecode = '" + feecode + "') and feecode = '" + feecode + "'");
            if (ediclist.Count > 0 || dtdjfswjc.Rows.Count > 0)
            {
                string djfsrid = "";
                string gdzrid = "";
                // 遍历定价方式1
                foreach (EasyDictionary ed in ediclist)
                {
                    int count = 0;// 记录每个高低值在值表是否存在（存在则表示有数据），如果一个高低值在值表中存在，则其它高低值必须存在
                    int countsum = 0;// 记录高低值总数
                    djfsrid = ed.Get("DJFSRID") + "";
                    // 高低值
                    IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID from SQM_FEE_PUR_REF where DJFSRID = '" + djfsrid + "' and (GDZRID is not null or GDZRID <> '')");
                    if (gdzlist.Count > 0)
                    {
                        // 遍历高低值
                        foreach (EasyDictionary gdz in gdzlist)
                        {
                            countsum++;
                            gdzrid = gdz.Get("GDZRID") + "";
                            string exist = DataHelper.QueryValue("select count(*) from " + tablename + " where gdzrid = '" + gdzrid + "' and (status <> '0' or status is null) and feecalcid = '" + psfrid + "'") + "";
                            if (exist != "0")
                            {
                                count++;
                            }
                        }
                        // 每组定价方式进行一个判断，如果存在不确认项则停止遍历
                        if (countsum > count && count > 0)
                        {
                            gdzcountnum = true;
                        }
                    }
                }
                // 遍历定价方式2
                if (!gdzcountnum)
                {
                    foreach (DataRow dr in dtdjfswjc.Rows)
                    {
                        int count = 0;// 记录每个高低值在值表是否存在（存在则表示有数据），如果一个高低值在值表中存在，则其它高低值必须存在
                        int countsum = 0;// 记录高低值总数
                        djfsrid = dr["DJFSRID"] + "";
                        // 高低值
                        IList<EasyDictionary> gdzlist = DataHelper.QueryDictList("select distinct GDZRID from SQM_FEE_PUR_REF where DJFSRID = '" + djfsrid + "' and (GDZRID is not null or GDZRID <> '')");
                        // 遍历高低值
                        foreach (EasyDictionary gdz in gdzlist)
                        {
                            countsum++;
                            gdzrid = gdz.Get("GDZRID") + "";
                            string exist = DataHelper.QueryValue("select count(*) from " + tablename + " where gdzrid = '" + gdzrid + "' and (status <> '0' or status is null) and feecalcid = '" + psfrid + "'") + "";
                            if (exist != "")
                            {
                                count++;
                            }
                        }
                        // 每组定价方式进行一个判断，如果存在不确认项则停止遍历
                        if (countsum > count && count > 0)
                        {
                            gdzcountnum = true;
                        }
                    }
                }
            }
            return gdzcountnum;
        }

        /// <summary>
        /// 通过产品 服务 费目的code在定价psf以及报价psf表里面拿rid
        /// </summary>
        /// <param name="prdcode"></param>
        /// <param name="srvcode"></param>
        /// <param name="feecode"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public string GetRid(string prdcode, string srvcode, string feecode, string alonefee, string keyvalue, string zver)
        {
            if (string.IsNullOrEmpty(zver))
            {
                zver = "V1";
            }
            string vrid = "";
            string status = "";
            string orgrid = "";
            string djrid = "";
            string bjrid = "";
            string bjstatus = "";
            string minstatus = "";
            DataRow[] drsver = dtverdict.Select("MRID = '" + keyvalue + "' and ZVER = '" + zver + "'");
            if (drsver.Length > 0)
            {
                vrid = drsver[0]["RID"] + "";
                status = drsver[0]["STATUS"] + "";
                orgrid = drsver[0]["ORGRID"] + "";
            }
            if (alonefee == "0")
            {
                DataRow[] drsdj;
                if (String.IsNullOrEmpty(orgrid))
                {
                    drsdj = dtdjpsfdict.Select("PRDCODE = '" + prdcode + "' and SRVCODE = '" + srvcode + "' and FEECODE = '" + feecode + "' and ORGRID is null");
                }
                else
                {
                    drsdj = dtdjpsfdict.Select("PRDCODE = '" + prdcode + "' and SRVCODE = '" + srvcode + "' and FEECODE = '" + feecode + "' and ORGRID like '%" + orgrid + "%'");
                }
                if (drsdj.Length > 0)
                {
                    djrid = drsdj[0]["RID"] + "";
                }
            }
            else if (alonefee == "1")
            {
                if (String.IsNullOrEmpty(orgrid))
                {
                    djrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where ALONEFEE='1' and FEECODE='{0}' and ORGRID is null", feecode)) + "";
                }
                else
                {
                    djrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where ALONEFEE='1' and FEECODE='{0}' and ORGRID like '%{1}%'", feecode, orgrid)) + "";
                }
            }
            DataRow[] drsbj = dtbjpsfdict.Select("PRODUCT_CODE = '" + prdcode + "' and SERVICE_CODE = '" + srvcode + "' and FEE_CODE = '" + feecode + "' and VRID = '" + vrid + "' and (BGFZRID='1' or BGFZRID is null)");
            if (drsbj.Length > 0)
            {
                bjrid = drsbj[0]["RID"] + "";
                bjstatus = drsbj[0]["BJSTATAUS"] + "";
                minstatus = drsbj[0]["MINSTATUS"] + "";
            }
            //string drver = string.Format("select RID from SQM_BJ_VER where MRID='{0}' and ZVER='{1}'", keyvalue, zver);
            //var vrid = DataHelper.QueryValue(drver);

            //var drsdj = DataHelper.QueryDictList(string.Format("select RID from SQM_DJ_PSF where PRDCODE='{0}' AND SRVCODE='{1}' AND FEECODE='{2}'", prdcode, srvcode, feecode));
            //string  djrid="";
            //if(drsdj.Count>0){
            //        djrid=drsdj[0]["RID"]+"";
            //}
            //string drsbj = string.Format("select  smv.rid,sbp.Bjstataus from SQM_MODEBJ_VAL smv left join Sqm_Bj_Psf sbp on smv.feecalcid=sbp.rid where sbp.vrid='{0}'", vrid);
            //DataTable dt = DataHelper.QueryDataTable(drsbj);
            //if(dt.Rows.Count>0){

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        bjrid = dr["RID"]+"";
            //        bjstatus = dr["BJSTATAUS"]+"";
            //    }
            //}

            List<string> ridList = new List<string> { djrid, bjrid, bjstatus, status, minstatus };
            return JsonHelper.GetJsonString(ridList);
        }
        //通过产品服务费目的code得到name
        public string CODETONAME(string type, string code)
        {
            var name = "";
            switch (type)
            {
                case "prd":
                    var prdlist = DataHelper.QueryDictList("SELECT SQPRODUCTNAME,PRODUCTKEY FROM SQM_PRD_EXT");
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
                    var feelist = DataHelper.QueryDictList("SELECT TCET084,TEXTDESC FROM V_MDM_FEE");
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
        public string getVerByRid(string rid)
        {
            return JsonHelper.GetJsonString(SQM_BJ_VER.TryFind(rid));
        }
        /// <summary>
        /// 保存为其他报价
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="postdata"></param>
        /// <param zver="postdata"></param>
        /// <returns></returns>
        public ActionResult copyNewOne(string keyvalue, string postdata, string zver)
        {
            string pvrid = "";
            string orgcode = "";
            string orgname = "";
            IList<EasyDictionary> list = DataHelper.QueryDictList(string.Format("SELECT distinct t1.*,t2.orgname FROM SQM_BJ_VER t1,SQM_BJ_PSF t2 WHERE t1.rid = t2.vrid and t1.MRID = '{0}' AND t1.ZVER = '{1}' AND (t2.orgname is not null or t2.orgname <> '')", keyvalue, zver));//需要复制报价版本
            if (list.Count > 0)
            {
                pvrid = list[0]["RID"] + "";
                string pvridtest = list[0].Get("RID") + "";
                orgcode = list[0]["ORGRID"] + "";
                orgname = list[0]["ORGNAME"] + "";
            }

            bool flag = true;
            string rtnmessage = "保存为其他报价成功！";
            var mrid = System.Guid.NewGuid().ToString(); //自建主表主键
            var vrid = System.Guid.NewGuid().ToString();//自建版本主键
            var random = System.Guid.NewGuid().ToString().Substring(0, 4);
            try
            {
                // 1 先创建报价主信息数据
                SQM_BJ_MAIN_BASIC mainobj = new SQM_BJ_MAIN_BASIC();
                mainobj.BJNAME = "报价" + DateTime.Now.ToShortDateString().Replace("/", "") + random;
                mainobj.CREATETIME = DateTime.Now;
                mainobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.RID = mrid;
                mainobj.DoCreate();
                // 2 创建版本信息
                SQM_BJ_VER verobj = new SQM_BJ_VER();
                verobj.ZVER = "V1";
                verobj.CREATETIME = DateTime.Now;
                verobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.MRID = mrid;
                verobj.RID = vrid;
                verobj.STATUS = "0";
                verobj.ORGRID = orgcode;
                verobj.DoCreate();
                // 3 psf表信息以及费目对应的值表信息
                var psflist = DataHelper.QueryDictList("SELECT * FROM SQM_BJ_PSF WHERE VRID = '" + pvrid + "' and choosestatus = '1' and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1')");
                string BJSTA = "";
                string IFBJCXS = "";
                List<string> psfrid = new List<string>();
                foreach (var item in psflist)
                {
                    //拼接插入字符串
                    var keystr = "";
                    var valuestr = "";
                    var newpsfrid = System.Guid.NewGuid().ToString();
                    psfrid.Add(newpsfrid);
                    string rid = item.Get("RID") + "";
                    string bgfzrid = item.Get("BGFZRID") + "";
                    if (bgfzrid == "1")
                    {
                        // 包干费
                        DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + rid + "' and (status <> '0' or status is null)");
                        if (dtbgf.Rows.Count > 0)//
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
                                        if (dr[colName] + "" != "")
                                        {
                                            value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                                        }
                                        else
                                        {
                                            value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                        }
                                    }
                                    else if (colName == "BGFZRID")// 新的paf表 主包干费rid
                                    {
                                        value += "'" + newpsfrid + "',";
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
                    }
                    foreach (string key in item.Keys)
                    {
                        keystr += key + ",";
                        if (key == "VRID")
                        {
                            valuestr += "'" + vrid + "',";
                        }
                        else if (key == "RID")
                        {
                            valuestr += "'" + newpsfrid + "',";
                        }
                        else if (key == "MRID")
                        {
                            valuestr += "'" + mrid + "',";
                        }
                        else if (key == "CREATETIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "MODIFYTIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "BJSTARTDATE" || key == "BJENDDATE")
                        {
                            if (item[key] + "" != "")
                            {
                                valuestr += "to_date('" + Convert.ToDateTime(item[key].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                            }
                            else
                            {
                                valuestr += "to_date('" + item[key] + "','yyyy/mm/dd'),";
                            }
                        }
                        else if (key == "BJFS")
                        {
                            BJSTA = "";
                            string BJFS = item[key] + "";
                            if (BJFS == "2")
                            {
                                BJSTA = "0";
                            }
                            else if (BJFS == "1")
                            {
                                BJSTA = "1";
                            }
                            valuestr += "'" + BJFS + "',";
                        }
                        else if (key == "BJSTATAUS")
                        {
                            string BJSTATAUS = item[key] + "";
                            // valuestr += "'" + psflist[0][key] + "',";
                            if (BJSTA == "0")
                            {
                                BJSTATAUS = "0";
                            }
                            else if (BJSTA == "1")
                            {
                                BJSTATAUS = item[key] + "";
                            }
                            else if (BJSTATAUS == "2")
                            {
                                BJSTATAUS = "1";
                            }
                            else if (BJSTATAUS == "5")
                            {
                                BJSTATAUS = "4";
                                IFBJCXS = "1";
                            }
                            valuestr += "'" + BJSTATAUS + "',";
                        }
                        else if (key == "IFBJCX")
                        {
                            string IFBJCX = item[key] + "";
                            if (!string.IsNullOrEmpty(IFBJCXS))
                            {
                                IFBJCX = "1";
                            }
                            valuestr += "'" + IFBJCX + "',";
                        }
                        else
                        {
                            valuestr += "'" + item[key] + "',";
                        }
                    }
                    string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                    DataHelper.ExecSql(addpsfsql);// 插入psf
                    // 查询该psf表对应的值表的所有信息
                    var valuelist = DataHelper.QueryDictList("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '" + item["RID"] + "' and (status <> '0' or status is null)");
                    if (valuelist.Count > 0)
                    {
                        string fsjtlj = "";//初始化方式阶梯累计
                        foreach (var item1 in valuelist)
                        {
                            var keystr1 = "";
                            var valuestr1 = "";
                            foreach (var key in item1.Keys)
                            {
                                //获取费目设置里面最新的方式阶梯累计
                                if (String.IsNullOrEmpty(fsjtlj))
                                {
                                    string gdzwhr = "";
                                    if (String.IsNullOrEmpty(item1["GDZRID"].ToString()))
                                    {
                                        gdzwhr = " and GDZRID is null";
                                    }
                                    else
                                    {
                                        gdzwhr = " and GDZRID='" + item1["GDZRID"].ToString() + "'";
                                    }
                                    fsjtlj = DataHelper.QueryValue(string.Format("select JTLJ from SQM_FEE_PUR_REF where DJFSRID='{0}' {1}", item1["DJFSRID"].ToString(), gdzwhr)) + "";
                                }
                                keystr1 += key + ',';
                                if (key == "RID")
                                {
                                    valuestr1 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "FEECALCID")
                                {
                                    valuestr1 += "'" + newpsfrid + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "STARTDATE" || key == "ENDDATE")
                                {
                                    if (item1[key] + "" == "")
                                    {
                                        valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                    }
                                    else
                                    {
                                        valuestr1 += "to_date('" + Convert.ToDateTime(item1[key].ToString()).ToString("yyyy/MM/dd") + "','YYYY/MM/DD'),";
                                    }
                                }
                                else if (key == "IFUPDATE")
                                {
                                    valuestr1 += "'1',";
                                }
                                else if (key == "JTLJ")
                                {
                                    valuestr1 += "'" + fsjtlj + "',";
                                }
                                else
                                {
                                    valuestr1 += "'" + item1[key] + "',";
                                }
                            }
                            string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                            DataHelper.ExecSql(addvaluesql);
                        }
                    }
                }

                for (var i = 0; psflist.Count > i; i++)
                {
                    string rid = psflist[i]["RID"] + "";

                    //复制仓租相关数据 SQM_BJ_CZXG
                    var CzxgList = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_CZXG WHERE BJRID='{0}' and (status <> '0' or status is null)", rid));
                    if (CzxgList.Count > 0)
                    {

                        foreach (var cz in CzxgList)
                        {
                            var keystr2 = "";
                            var valuestr2 = "";
                            foreach (string key in cz.Keys)
                            {
                                keystr2 += key + ",";
                                if (key == "RID")
                                {
                                    valuestr2 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "BJRID")
                                {
                                    valuestr2 += "'" + psfrid[i] + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr2 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr2 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else
                                {
                                    valuestr2 += "'" + cz[key] + "',";
                                }
                            }

                            string AddCzxg = string.Format("INSERT INTO SQM_BJ_CZXG ({0}) VALUES ({1})", keystr2.TrimEnd(','), valuestr2.TrimEnd(','));
                            DataHelper.ExecSql(AddCzxg);
                        }

                    }
                    //复制仓租条件 SQM_BJ_CZTJ
                    var CztjList = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_CZTJ WHERE BJRID='{0}' and (status <> '0' or status is null)", rid));
                    if (CztjList.Count > 0)
                    {
                        foreach (var cz in CztjList)
                        {
                            var keystr3 = "";
                            var valuestr3 = "";
                            foreach (string key in cz.Keys)
                            {
                                keystr3 += key + ",";
                                if (key == "RID")
                                {
                                    valuestr3 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "BJRID")
                                {
                                    valuestr3 += "'" + psfrid[i] + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr3 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr3 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else
                                {
                                    valuestr3 += "'" + cz[key] + "',";
                                }
                            }
                            string AddCztj = string.Format("INSERT INTO SQM_BJ_CZTJ ({0}) VALUES ({1})", keystr3.TrimEnd(','), valuestr3.TrimEnd(','));
                            DataHelper.ExecSql(AddCztj);
                        }
                    }
                }
                //List<PRD> dataArray = JsonHelper.GetObject<List<PRD>>(postdata);
                //foreach (var p in dataArray)
                //{
                //    foreach (var s in p.srvcodes)
                //    {
                //        foreach (var f in s.feecodes)
                //        {
                //            //得到psf表的信息以及对应的值表的信息
                //            var psflist = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_PSF WHERE PRODUCT_CODE = '{0}' AND SERVICE_CODE = '{1}' AND FEE_CODE = '{2}' AND VRID = '{3}' and (status <> '0' or status is null)", p.prdcode, s.srvcode, f, pvrid))[0];
                //            var rid = psflist["RID"];
                //            var valueList = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '{0}' and (status <> '0' or status is null)", rid));//值表数据
                //            //复制psf表信息
                //            var newpsfrid = System.Guid.NewGuid().ToString();//新增psf的rid
                //            // 包干费
                //            DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + rid + "' and (status <> '0' or status is null)");
                //            if (dtbgf.Rows.Count > 0)// 有包干费
                //            {
                //                string part1 = "begin ";
                //                string part2 = " end;";
                //                foreach (DataRow dr in dtbgf.Rows)
                //                {
                //                    string insert = "";
                //                    string value = "";
                //                    string bgfrid = Guid.NewGuid().ToString();
                //                    foreach (DataColumn column in dtbgf.Columns)
                //                    {
                //                        string colName = column.ColumnName + "";
                //                        insert += colName + ",";
                //                        if (colName == "CREATETIME" || colName == "MODIFYTIME")
                //                        {
                //                            value += "to_date('" + dr[colName] + "','yyyy/mm/dd hh24:mi:ss'),";
                //                        }
                //                        else if (colName == "BJSTARTDATE" || colName == "BJENDDATE")
                //                        {
                //                            if (dr[colName] + "" != "")
                //                            {
                //                                value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                //                            }
                //                            else
                //                            {
                //                                value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                //                            }
                //                        }
                //                        else if (colName == "BGFZRID")
                //                        {
                //                            value += "'" + newpsfrid + "',";
                //                        }
                //                        else if (colName == "RID")
                //                        {
                //                            value += "'" + bgfrid + "',";
                //                        }
                //                        else if (colName == "MRID")
                //                        {
                //                            value += "'" + mrid + "',";
                //                        }
                //                        else if (colName == "VRID")
                //                        {
                //                            value += "'" + vrid + "',";
                //                        }
                //                        else if (colName == "STATUS")
                //                        {
                //                            value += "'1',";
                //                        }
                //                        else if (colName == "ISLSC")
                //                        {
                //                            value += "'1',";
                //                        }
                //                        else
                //                        {
                //                            value += "'" + dr[colName] + "',";
                //                        }
                //                    }
                //                    part1 += "insert into sqm_bj_psf(" + insert.TrimEnd(',') + ") values(" + value.TrimEnd(',') + ");";
                //                }
                //                DataHelper.ExecSql(part1 + part2);
                //            }
                //            var keystr = "";
                //            var valuestr = "";
                //            //拼接插入psf表的sql语句
                //            foreach (string key in psflist.Keys)
                //            {
                //                keystr += key + ",";
                //                if (key == "RID")
                //                {
                //                    valuestr += "'" + newpsfrid + "',";
                //                }
                //                else if (key == "VRID")
                //                {
                //                    valuestr += "'" + vrid + "',";
                //                }
                //                else if (key == "MRID")
                //                {
                //                    valuestr += "'" + mrid + "',";
                //                }
                //                else if (key == "CREATETIME")
                //                {
                //                    valuestr += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                //                }
                //                else if (key == "MODIFYTIME")
                //                {
                //                    valuestr += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                //                }
                //                else if (key == "BJSTARTDATE")
                //                {
                //                    valuestr += "to_date('" + psflist[key] + "','YYYY-MM-DD hh24:mi:ss'),";
                //                }
                //                else if (key == "BJENDDATE")
                //                {
                //                    valuestr += "to_date('" + psflist[key] + "','YYYY-MM-DD hh24:mi:ss'),";
                //                }
                //                else if (key == "ORGCODE")
                //                {
                //                    valuestr += "'" + orgcode + "',";
                //                }
                //                else if (key == "ORGNAME")
                //                {
                //                    valuestr += "'" + orgname + "',";
                //                }
                //                else
                //                {
                //                    valuestr += "'" + psflist[key] + "',";
                //                }
                //            }
                //            string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                //            DataHelper.ExecSql(addpsfsql);

                //            //复制值表的信息
                //            foreach (var item in valueList)
                //            {
                //                var keystr1 = "";
                //                var valuestr1 = "";
                //                foreach (string key in item.Keys)
                //                {
                //                    keystr1 += key + ",";
                //                    if (key == "RID")
                //                    {
                //                        valuestr1 += "'" + System.Guid.NewGuid().ToString() + "',";
                //                    }
                //                    else if (key == "FEECALCID")
                //                    {
                //                        valuestr1 += "'" + newpsfrid + "',";
                //                    }
                //                    else if (key == "CREATETIME")
                //                    {
                //                        valuestr1 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                //                    }
                //                    else if (key == "MODIFYTIME")
                //                    {
                //                        valuestr1 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                //                    }
                //                    else if (key == "STARTDATE")
                //                    {
                //                        valuestr1 += "to_date('" + item[key] + "','YYYY-MM-DD hh24:mi:ss'),";
                //                    }
                //                    else if (key == "ENDDATE")
                //                    {
                //                        valuestr1 += "to_date('" + item[key] + "','YYYY-MM-DD hh24:mi:ss'),";
                //                    }
                //                    else
                //                    {
                //                        valuestr1 += "'" + item[key] + "',";
                //                    }
                //                }
                //                string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                //                DataHelper.ExecSql(addvaluesql);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmessage = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = mrid, Success = flag, Message = rtnmessage }));
        }
        /// <summary>
        /// 得到报价psf表产品服务code
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="zver"></param>
        /// <returns></returns>
        public string showPSF(string keyvalue, string zver)
        {
            //首先查询出版本的rid
            var vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver));
            //psf表信息
            //var sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1'", vrid);
            //var psfDictList = DataHelper.QueryDictList(sql);
            //return JsonHelper.GetJsonString(psfList);
            SQM_BJ_MAIN_BASIC sbmb = SQM_BJ_MAIN_BASIC.Find(keyvalue);
            DataTable dt = new DataTable();
            if (sbmb.FBPRICE == "1")
            {
                dt = DataHelper.QueryDataTable("select * from sqm_bj_psf where vrid = '" + vrid + "' and (FEECATG<>'2' or FEECATG is null) and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1') order by product_code ,service_code,fee_code");
            }
            else
            {
                dt = DataHelper.QueryDataTable("select * from sqm_bj_psf where vrid = '" + vrid + "' and choosestatus = '1' and (FEECATG<>'2' or FEECATG is null) and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1') order by product_code ,service_code,fee_code");
            }
            // 两层结构 的数据结构  思路：遍历表数据 碰到prdcode变化则生成一个产品对应服务费目数据  碰到srvcode变化则生成一个服务对应费目数据
            Dictionary<string, List<Dictionary<string, List<string>>>> psfDict = new Dictionary<string, List<Dictionary<string, List<string>>>>();
            Dictionary<string, List<string>> srv_feeDict = new Dictionary<string, List<string>>();
            List<Dictionary<string, List<string>>> psfList = new List<Dictionary<string, List<string>>>();
            List<string> feeList = new List<string>();
            string oldPrdCode = "";
            string oldSrvCode = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string prdcode = dr["PRODUCT_CODE"] + "";
                    if (oldPrdCode == "")
                    {
                        oldPrdCode = prdcode;
                    }

                    string srvcode = dr["SERVICE_CODE"] + "";
                    if (oldSrvCode == "")
                    {
                        oldSrvCode = srvcode;
                    }

                    string feecode = dr["FEE_CODE"] + "";
                    if (oldSrvCode == srvcode && oldPrdCode == prdcode)
                    {
                        feeList.Add(feecode);
                    }
                    // 如果srvcode没变，判断prdcode是否改变，如果改变则添加
                    if (oldSrvCode != srvcode || oldPrdCode != prdcode)
                    {
                        if (!srv_feeDict.ContainsKey(oldSrvCode))
                        {
                            srv_feeDict.Add(oldSrvCode, new List<string>(feeList));// 构造器方法复制，值复制
                            Dictionary<string, List<string>> srv_feeDict_copy = new Dictionary<string, List<string>>(srv_feeDict);// 构造器方法复制，值复制
                            psfList.Add(srv_feeDict_copy);
                            feeList.Clear();
                            srv_feeDict.Clear();
                            feeList.Add(feecode);// 如果srvcode改变，则要存当前srvcode对应的feecode作为feeList的第一个元素
                        }
                        oldSrvCode = srvcode;
                    }
                    if (oldPrdCode != prdcode)
                    {
                        if (!psfDict.ContainsKey(oldPrdCode))
                        {
                            psfDict.Add(oldPrdCode, new List<Dictionary<string, List<string>>>(psfList));// 构造器方法复制，将引用类型转换成值类型
                            psfList.Clear();
                        }
                        oldPrdCode = prdcode;
                    }
                }
                // 最后一个
                srv_feeDict.Add(oldSrvCode, new List<string>(feeList));// 构造器方法复制，值复制
                Dictionary<string, List<string>> srv_feeDict_copy1 = new Dictionary<string, List<string>>(srv_feeDict);// 构造器方法复制，值复制
                psfList.Add(srv_feeDict_copy1);
                psfDict.Add(oldPrdCode, new List<Dictionary<string, List<string>>>(psfList));// 构造器方法复制，将引用类型转换成值类型
            }
            ArrayList arrayList = new ArrayList();
            arrayList.Add("nomessage");
            arrayList.Add(psfDict);
            return JsonHelper.GetJsonString(arrayList);
        }
        /// <summary>
        /// 报价修改后，删除生成的报价文件或上传文件。
        /// </summary>
        public void DeleteFile(string vrid, string BJRID)
        {
            SQM_BJ_VER sbv = SQM_BJ_VER.Find(vrid);
            if (sbv.UPLOADNAME != "")
            {
                sbv.UPLOADNAME = "";
                sbv.UPLOADTIME = null;
                sbv.UPLOADURL = "";
                sbv.DoUpdate();
            }
            SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(BJRID);
            if (sbp.IFBJCX == "1")
            {

                if (sbp.BJSTATAUS == "2")
                {
                    sbp.IFBJCX = "0";
                    sbp.DoUpdate();
                }
            }

        }
        /// <summary>
        /// 根据商机匹配 自动带合同
        /// </summary>
        public ActionResult GetContrsct(string rid, string sjid, string bpcode)
        {
            sjid = sjid.TrimEnd(',');
            string sql = string.Format("select CONTRACTCODE,CONTRACTSTARTDATE,CONTRACTENDDATE from CRM_SALESCONTRACT where BUSINESSID like '%{0}%' and CUSTOMERNO='{1}' order by createtime desc", sjid, bpcode);
            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable data = DataHelper.QueryDataTable(sql, conn);

            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 修改当前版本
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="verdtto"></param>
        /// <param name="verdtfrom"></param>
        /// <param name="vermemo"></param>
        /// <returns></returns>
        public ActionResult saveVer(string rid, string verdtto, string verdtfrom, string vermemo, string contrsctnum, string bpcode)
        {
            bool flag = true;
            string rtnmessage = "保存成功";
            try
            {
                if (string.IsNullOrEmpty(contrsctnum))
                {
                    DateTime verdtfroms = DateTime.Parse(verdtfrom);
                    DateTime verdttos = DateTime.Parse(verdtto);
                    var srcobj = SQM_BJ_VER.TryFind(rid);
                    SQM_BJ_MAIN_BASIC sbmb = SQM_BJ_MAIN_BASIC.Find(srcobj.MRID);
                    DateTime Dtfroms = DateTime.Parse(sbmb.DTFROM.ToString());
                    DateTime Dttos = DateTime.Parse(sbmb.DTTO.ToString());
                    if (verdtfroms > verdttos)
                    {
                        flag = false;
                        string StartError = "保存失败:有效起始日期大于有效期截止日期。";
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = StartError }));
                    }


                    if (verdtfroms < sbmb.DTFROM)
                    {
                        flag = false;
                        string StartError = "保存失败:有效起始日不在报价有效期：" + Dtfroms.ToString("yyyy/MM/dd") + "-" + Dttos.ToString("yyyy/MM/dd") + "范围内。";
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = StartError }));
                    }
                    if (verdttos > sbmb.DTTO)
                    {
                        flag = false;
                        string StartError = "保存失败:有效截止日不在报价有效期：" + Dtfroms.ToString("yyyy/MM/dd") + "-" + Dttos.ToString("yyyy/MM/dd") + "范围内。";
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = StartError }));
                    }
                    if (verdtfroms > sbmb.DTFROM)
                    {
                        string ZverOld = srcobj.ZVER;
                        string ridOld = "";
                        string versionNew = "V" + (int.Parse(ZverOld.Substring(1)) - 1);
                        for (int i = 1; i >= 0; i++)
                        {
                            string ZVER = "";
                            string STATUS = "";
                            string sql = string.Format("select * from sqm_bj_ver where mrid='{0}' and zver='{1}'", srcobj.MRID, versionNew);
                            DataTable dt = DataHelper.QueryDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ridOld = dr["RID"] + "";
                                    ZVER = dr["ZVER"] + "";
                                    STATUS = dr["STATUS"] + "";
                                }
                                if (STATUS == "3" || STATUS == "6")
                                {
                                    ZverOld = ZVER;
                                    versionNew = "V" + (int.Parse(ZverOld.Substring(1)) - 1);
                                    if (versionNew == "V0")
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    SQM_BJ_VER sbv1 = SQM_BJ_VER.TryFind(ridOld);
                                    sbv1.DTTO = verdtfroms.AddDays(-1);
                                    if (sbv1.DTTO < sbv1.DTFROM)
                                    {
                                        sbv1.DTTO = sbv1.DTFROM;
                                    }
                                    sbv1.DoUpdate();
                                    break;
                                }
                            }
                        }

                        srcobj.DTTO = verdttos;
                        srcobj.DTFROM = verdtfroms;
                        srcobj.MEMO = vermemo;
                        srcobj.MODIFYTIME = DateTime.Now;
                        srcobj.DoUpdate();
                    }
                }
                else
                {
                    string CONTRACTSTARTDATE = "";//合同开始日期
                    string CONTRACTENDDATE = "";//合同结束日期
                    string CUSTOMERNO = "";//客户ID
                    string contrsctSql = string.Format("select CONTRACTCODE,CONTRACTSTARTDATE,CONTRACTENDDATE,CUSTOMERNO from CRM_SALESCONTRACT where CONTRACTCODE='{0}'", contrsctnum);
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    DataTable dt = DataHelper.QueryDataTable(contrsctSql, conn);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["CONTRACTSTARTDATE"].ToString() != "")
                            {
                                CONTRACTSTARTDATE = dr["CONTRACTSTARTDATE"] + "";
                            }
                            if (dr["CONTRACTENDDATE"].ToString() != "")
                            {
                                CONTRACTENDDATE = dr["CONTRACTENDDATE"] + "";
                            }
                            if (dr["CUSTOMERNO"].ToString() != "")
                            {
                                CUSTOMERNO = dr["CUSTOMERNO"] + "";
                            }

                        }
                    }
                    else
                    {
                        flag = false;
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = "输入的合同编号不存在" }));
                    }
                    if (!string.Equals(bpcode, CUSTOMERNO))
                    {
                        flag = false;
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = "保存失败：客户不匹配" }));
                    }
                    DateTime dtto = DateTime.Parse(verdtto);
                    DateTime dtfrom = DateTime.Parse(verdtfrom);
                    DateTime dspCONTRACTSTARTDATE = DateTime.Parse(CONTRACTSTARTDATE);
                    DateTime dspCONTRACTENDDATE = DateTime.Parse(CONTRACTENDDATE);

                    var srcobj = SQM_BJ_VER.TryFind(rid);
                    if (!string.IsNullOrEmpty(verdtfrom))
                    {
                        if (dtfrom >= dspCONTRACTSTARTDATE)
                        {
                            srcobj.DTFROM = DateTime.Parse(verdtfrom);
                        }
                        else
                        {
                            flag = false;
                            string StartError = "保存失败:有效起始日不在合同有效期'" + dspCONTRACTSTARTDATE.ToString("yyyy/MM/dd") + "'-'" + dspCONTRACTENDDATE.ToString("yyyy/MM/dd") + "'范围内";
                            StartError = StartError.Replace("'", "");
                            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = StartError }));
                        }

                    }
                    if (!string.IsNullOrEmpty(verdtto))
                    {
                        if (dtto <= dspCONTRACTENDDATE)
                        {
                            srcobj.DTTO = DateTime.Parse(verdtto);
                        }
                        else
                        {
                            flag = false;
                            string EndError = "保存失败:有效截止日不在合同有效期'" + dspCONTRACTSTARTDATE.ToString("yyyy/MM/dd") + "'-'" + dspCONTRACTENDDATE.ToString("yyyy/MM/dd") + "'范围内";
                            EndError = EndError.Replace("'", "");
                            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = EndError }));
                        }

                    }
                    srcobj.MEMO = vermemo;
                    srcobj.CONTRSCTNUM = contrsctnum;
                    srcobj.MODIFYTIME = DateTime.Now;
                    srcobj.DoUpdate();
                }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmessage = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = rtnmessage }));
        }

        /// <summary>
        /// 判断非标报价
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="verdtto"></param>
        /// <param name="verdtfrom"></param>
        /// <param name="vermemo"></param>
        /// <returns></returns>
        public ActionResult saveFB(string rid, string fbprice, string zver)
        {
            bool flag = true;
            string rtnmessage = "保存成功";
            try
            {
                var srcobj = SQM_BJ_MAIN_BASIC.TryFind(rid);
                if (!string.IsNullOrEmpty(fbprice))
                {
                    srcobj.FBPRICE = fbprice;
                    string UploadSql = string.Format("select RID from SQM_BJ_VER where mrid='{0}' and zver='{1}'", srcobj.RID, zver);
                    var RID = DataHelper.QueryValue(UploadSql);
                    SQM_BJ_VER sbv = SQM_BJ_VER.Find(RID);
                    if (sbv.UPLOADNAME != "")
                    {
                        sbv.UPLOADNAME = "";
                        sbv.UPLOADTIME = null;
                        sbv.UPLOADURL = "";
                        sbv.DoUpdate();
                    }
                }
                srcobj.DoUpdate();

            }
            catch (Exception ex)
            {
                flag = false;
                rtnmessage = ex.Message;
            }

            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Data = "", Message = rtnmessage }));
        }

        /// <summary>
        /// 报价文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult uploadFile(HttpContext context)
        {
            string rtnmsg = "上传成功！";
            bool flag = true;
            try
            {

            }
            catch (Exception ex)
            {
                rtnmsg = ex.Message;
                flag = false;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = rtnmsg, Success = flag }));

        }
        /// <summary>
        /// 提交审批 OA备注
        /// </summary>
        public void SetMemo(string vrid, string memo)
        {
            string mrid = "";
            string zversion = "";
            SQM_BJ_VER sbv = SQM_BJ_VER.Find(vrid);
            sbv.MEMO = memo;
            sbv.FBREASONCODE = Request["fbreason"];
            sbv.FBREASONNAME = Request["fbreasonname"];
            sbv.FBREASONOTHER = Request["fbreasonother"];
            sbv.FBMEMO = Request["fbmemo"];
            mrid = sbv.MRID;
            zversion = sbv.ZVER;
            sbv.DoUpdate();

            //销售易更新 报价接口 审批中
            var mainobj = SQM_BJ_MAIN_BASIC.TryFind(mrid);
            if (!string.IsNullOrEmpty(mainobj.XSYBJID))
            {
                #region  销售易更新 报价接口 审批中
                BJWriteBackUpdate.UpdateQuotation uwb = new Web.BJWriteBackUpdate.UpdateQuotation();
                BJWriteBackUpdate.phUpdateQuotation uhead = new BJWriteBackUpdate.phUpdateQuotation();
                uhead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                uhead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                uhead.msgId = Guid.NewGuid().ToString();

                BJWriteBackUpdate.pbUpdateQuotation _ubodys = new BJWriteBackUpdate.pbUpdateQuotation();
                BJWriteBackUpdate.pbUpdateQuotation[] ubodys = new BJWriteBackUpdate.pbUpdateQuotation[1];
                BJWriteBackUpdate.pbUpdateQuotationData[] ubody = new BJWriteBackUpdate.pbUpdateQuotationData[1];
                BJWriteBackUpdate.pbUpdateQuotationData _ubody = new BJWriteBackUpdate.pbUpdateQuotationData();

                _ubody.id = mainobj.XSYBJID;
                _ubody.customItem3__c = zversion;//报价版本 //测试
                _ubody.customItem4__c = "1";//报价状态
                ubody[0] = _ubody;
                _ubodys.data = ubody;
                ubodys[0] = _ubodys;
                uwb.CallUpdateQuotation(uhead, ubodys);
                #endregion
            }
            else
            {
                #region  销售易创建 报价接口 审批中
                BJWriteback.CreateQuotation wb = new Web.BJWriteback.CreateQuotation();
                BJWriteback.phCreateQuotation head = new BJWriteback.phCreateQuotation();
                head.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                head.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                head.msgId = Guid.NewGuid().ToString();


                BJWriteback.pbCreateQuotation _bodys = new BJWriteback.pbCreateQuotation();
                BJWriteback.pbCreateQuotation[] bodys = new BJWriteback.pbCreateQuotation[1];
                BJWriteback.pbCreateQuotationData[] body = new BJWriteback.pbCreateQuotationData[1];
                BJWriteback.pbCreateQuotationData _body = new BJWriteback.pbCreateQuotationData();

                SQM_BJ_ORG orgobj = SQM_BJ_ORG.FindFirstByProperties(SQM_BJ_ORG.Prop_MRID, mrid);
                SQM_BJ_BIZ busobj = SQM_BJ_BIZ.FindFirstByProperties(SQM_BJ_BIZ.Prop_MRID, mrid);
                SQM_BJ_BP cusobj = SQM_BJ_BP.FindFirstByProperties(SQM_BJ_BP.Prop_MRID, mrid);
                //_body.customItem1__c = mainobj.BJNAME;//报价编号  不填

                //_body.quotationTitle = "";报价名称
                _body.customItem3__c = zversion;//报价版本 
                _body.customItem4__c = "1";//报价状态
                _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "QM_Price_N/QM_PriceEdit?keyValue=" + mrid;//报价地址
                _body.customItem6__c = orgobj.ORGCODE;//mainobj.BJNAME;//操作组织 
                _body.customItem7__c = mrid;//mainobj.BJNAME;//报价ID
                _body.ownerId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey(); //mainobj.BJNAME;//所有人
                _body.entityType = "855577209438913";//业务类型写死
                _body.quotationEntityRelAccount = cusobj.BPCODE;//客户编码
                _body.quoteTime = DateToTicks(DateTime.Now).ToString();//报价时间
                _body.totalDiscountAmount = "0";//总折扣额
                _body.quotationEntityRelOpportunity = busobj.BIZID;//待增加 商机编码
                body[0] = _body;
                _bodys.data = body;
                bodys[0] = _bodys;
                BJWriteback.msgResponse msg = wb.CallCreateQuotation(head, bodys);

                string xsybjid = "";
                var jos = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(msg.list[0].originMessage);
                foreach (var itms in jos)
                {
                    var jo = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString());
                    foreach (var itm in jo)
                    {
                        if (itm.Key == "id")
                        {
                            xsybjid = itm.Value.ToString();
                        }
                    }
                }

                mainobj.XSYBJID = xsybjid;
                mainobj.DoUpdate();
                #endregion
            }
        }
        /// <summary>
        /// 版本作废
        /// </summary>
        public ActionResult deleteVer(string vrid)
        {
            bool flag = true;
            string mrid = "";
            string zversion = "";
            string rtnmessage = "作废成功";
            SQM_BJ_VER sbv = SQM_BJ_VER.Find(vrid);
            sbv.STATUS = "6";

            mrid = sbv.MRID;
            zversion = sbv.ZVER;
            sbv.DoUpdate();
            var mainobj = SQM_BJ_MAIN_BASIC.TryFind(mrid);
            //不创建商机
            if (!string.IsNullOrEmpty(mainobj.XSYBJID))
            {
                #region  作废 报价接口 
                BJWriteBackUpdate.UpdateQuotation uwb = new Web.BJWriteBackUpdate.UpdateQuotation();
                BJWriteBackUpdate.phUpdateQuotation uhead = new BJWriteBackUpdate.phUpdateQuotation();
                uhead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                uhead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                uhead.msgId = Guid.NewGuid().ToString();

                BJWriteBackUpdate.pbUpdateQuotation _ubodys = new BJWriteBackUpdate.pbUpdateQuotation();
                BJWriteBackUpdate.pbUpdateQuotation[] ubodys = new BJWriteBackUpdate.pbUpdateQuotation[1];
                BJWriteBackUpdate.pbUpdateQuotationData[] ubody = new BJWriteBackUpdate.pbUpdateQuotationData[1];
                BJWriteBackUpdate.pbUpdateQuotationData _ubody = new BJWriteBackUpdate.pbUpdateQuotationData();

                _ubody.id = mainobj.XSYBJID;
                _ubody.customItem3__c = zversion;//报价版本 //测试
                _ubody.customItem4__c = "6";//报价状态
                ubody[0] = _ubody;
                _ubodys.data = ubody;
                ubodys[0] = _ubodys;
                uwb.CallUpdateQuotation(uhead, ubodys);
                #endregion
            }
            else
            {
                #region  作废 报价接口 
                BJWriteback.CreateQuotation wb = new Web.BJWriteback.CreateQuotation();
                BJWriteback.phCreateQuotation head = new BJWriteback.phCreateQuotation();
                head.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                head.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";
                head.msgId = Guid.NewGuid().ToString();


                BJWriteback.pbCreateQuotation _bodys = new BJWriteback.pbCreateQuotation();
                BJWriteback.pbCreateQuotation[] bodys = new BJWriteback.pbCreateQuotation[1];
                BJWriteback.pbCreateQuotationData[] body = new BJWriteback.pbCreateQuotationData[1];
                BJWriteback.pbCreateQuotationData _body = new BJWriteback.pbCreateQuotationData();

                SQM_BJ_ORG orgobj = SQM_BJ_ORG.FindFirstByProperties(SQM_BJ_ORG.Prop_MRID, mrid);
                SQM_BJ_BIZ busobj = SQM_BJ_BIZ.FindFirstByProperties(SQM_BJ_BIZ.Prop_MRID, mrid);
                SQM_BJ_BP cusobj = SQM_BJ_BP.FindFirstByProperties(SQM_BJ_BP.Prop_MRID, mrid);
                //_body.customItem1__c = mainobj.BJNAME;//报价编号  不填

                //_body.quotationTitle = "";报价名称
                _body.customItem3__c = zversion;//报价版本 
                _body.customItem4__c = "1";//报价状态
                _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "QM_Price_N/QM_PriceEdit?keyValue=" + mrid;//报价地址
                _body.customItem6__c = orgobj.ORGCODE;//mainobj.BJNAME;//操作组织 
                _body.customItem7__c = mrid;//mainobj.BJNAME;//报价ID
                _body.ownerId = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey(); //mainobj.BJNAME;//所有人
                _body.entityType = "855577209438913";//业务类型写死
                _body.quotationEntityRelAccount = cusobj.BPCODE;//客户编码
                _body.quoteTime = DateToTicks(DateTime.Now).ToString();//报价时间
                _body.totalDiscountAmount = "0";//总折扣额
                _body.quotationEntityRelOpportunity = busobj.BIZID;//待增加 商机编码
                body[0] = _body;
                _bodys.data = body;
                bodys[0] = _bodys;
                BJWriteback.msgResponse msg = wb.CallCreateQuotation(head, bodys);

                string xsybjid = "";
                var jos = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(msg.list[0].originMessage);
                foreach (var itms in jos)
                {
                    var jo = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString());
                    foreach (var itm in jo)
                    {
                        if (itm.Key == "id")
                        {
                            xsybjid = itm.Value.ToString();
                        }
                    }
                }

                mainobj.XSYBJID = xsybjid;
                mainobj.DoUpdate();
                #endregion
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = rtnmessage, Success = flag }));
        }
        /// <summary>
        /// 保存新的版本
        /// </summary>
        /// <param name="mrid"></param>
        /// <param name="verdtto"></param>
        /// <param name="verdtfrom"></param>
        /// <param name="vermemo"></param>
        /// <param name="selectedArray"></param>
        /// <param name="zver"></param>
        /// <param name="pvrid"></param>
        /// <returns></returns>
        public ActionResult saveNewVer(string mrid, string verdtto, string verdtfrom, string vermemo, string selectedArray, string zver, string pvrid)
        {
            bool flag = true;
            string rtnmessage = "保存成功";
            string Zvers = GetLatestVer(mrid);
            if (Zvers != zver)
            {
                flag = false;
                rtnmessage = "另存失败:已存在新版本。";
                return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = rtnmessage, Success = flag }));
            }

            var vrid = System.Guid.NewGuid().ToString();//新版本主键
            try
            {
                //旧版本
                SQM_BJ_VER sbv = SQM_BJ_VER.TryFind(pvrid);
                //先增加版本信息
                SQM_BJ_VER versrcobj = new SQM_BJ_VER();
                versrcobj.RID = vrid;
                //versrcobj.ZVER = GetLatestVer(mrid);
                versrcobj.ZVER = Zvers;
                versrcobj.MRID = mrid;
                versrcobj.STATUS = "0";
                versrcobj.BPCODE9 = sbv.BPCODE9;
                versrcobj.ORGRID = sbv.ORGRID;
                versrcobj.CONTRSCTNUM = sbv.CONTRSCTNUM;

                versrcobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                versrcobj.CREATETIME = DateTime.Now;
                versrcobj.MODIFYTIME = DateTime.Now;
                DateTime verdtfroms = DateTime.Parse(verdtfrom);
                if (!string.IsNullOrEmpty(verdtfrom))
                {

                    versrcobj.DTFROM = verdtfroms;
                }
                if (!string.IsNullOrEmpty(verdtto))
                {
                    versrcobj.DTTO = DateTime.Parse(verdtto);
                }
                versrcobj.MEMO = vermemo;
                versrcobj.DoCreate();
                if (sbv.STATUS != "3" && sbv.STATUS != "6")
                {
                    sbv.DTTO = verdtfroms.AddDays(-1);
                    if (sbv.DTTO < sbv.DTFROM)
                    {
                        sbv.DTTO = sbv.DTFROM;
                    }
                    sbv.DoUpdate();
                }
                else
                {
                    string ZverOld = sbv.ZVER;
                    string ridOld = "";
                    string versionNew = "V" + (int.Parse(ZverOld.Substring(1)) - 1);
                    for (int i = 1; i >= 0; i++)
                    {
                        if (versionNew == "V0")
                        {
                            break;
                        }
                        string ZVER = "";
                        string STATUS = "";
                        string sql = string.Format("select * from sqm_bj_ver where mrid='{0}' and zver='{1}'", sbv.MRID, versionNew);
                        DataTable dt = DataHelper.QueryDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ridOld = dr["RID"] + "";
                                ZVER = dr["ZVER"] + "";
                                STATUS = dr["STATUS"] + "";
                            }
                            if (STATUS == "3" || STATUS == "6")
                            {
                                ZverOld = ZVER;
                                versionNew = "V" + (int.Parse(ZverOld.Substring(1)) - 1);
                                if (versionNew == "V0")
                                {
                                    break;
                                }
                            }
                            else
                            {
                                SQM_BJ_VER sbv1 = SQM_BJ_VER.TryFind(ridOld);
                                sbv1.DTTO = DateTime.Now.AddDays(-1);
                                if (sbv1.DTTO < sbv1.DTFROM)
                                {
                                    sbv1.DTTO = sbv1.DTFROM;
                                }
                                sbv1.DoUpdate();
                                break;
                            }
                        }
                    }
                }
                //插入psf表数据
                //List<PRD> dataArray = JsonHelper.GetObject<List<PRD>>(selectedArray);
                //foreach (var p in dataArray)
                //{
                //    foreach (var s in p.srvcodes)
                //    {
                //        foreach (var f in s.feecodes)
                //        {
                //得到psf表的信息以及对应的值表的信息
                //   var psflist = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_PSF WHERE (status <> '0' or status is null) and PRODUCT_CODE = '{0}' AND SERVICE_CODE = '{1}' AND FEE_CODE = '{2}' AND VRID = '{3}'", p.prdcode, s.srvcode, f, pvrid));
                //根据版本 复制psf 所有的数据信息
                //  var psflist = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_PSF WHERE (status <> '0' or status is null)  AND VRID = '{0}'", pvrid));
                //var psflist = DataHelper.QueryDictList("SELECT * FROM SQM_BJ_PSF WHERE VRID = '" + pvrid + "' and choosestatus = '1' and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1')");

                //兼容非标的
                var psflist = DataHelper.QueryDictList(string.Format(@"SELECT sbp.*
                      FROM SQM_BJ_PSF sbp
                      left join SQM_BJ_MAIN_BASIC sbmb on sbp.mrid=sbmb.rid
                     WHERE sbp.VRID = '{0}'
                       and (sbp.choosestatus = '1' or sbmb.fbprice='1')
                       and (sbp.status <> '0' or sbp.status is null)
                       and (sbp.bgfzrid is null or sbp.bgfzrid = '1')", pvrid));

                // 复制psf表信息


                string BJSTA = "";
                string IFBJCXS = "";
                List<string> psfrid = new List<string>();
                foreach (var item in psflist)
                {
                    //拼接插入字符串
                    var keystr = "";
                    var valuestr = "";
                    var newpsfrid = System.Guid.NewGuid().ToString();
                    psfrid.Add(newpsfrid);
                    string rid = item.Get("RID") + "";
                    string bgfzrid = item.Get("BGFZRID") + "";
                    if (bgfzrid == "1")
                    {
                        // 包干费
                        DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + rid + "' and (status <> '0' or status is null)");
                        if (dtbgf.Rows.Count > 0)//
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
                                        if (dr[colName] + "" != "")
                                        {
                                            value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                                        }
                                        else
                                        {
                                            value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                        }
                                    }
                                    else if (colName == "BGFZRID")// 新的paf表 主包干费rid
                                    {
                                        value += "'" + newpsfrid + "',";
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
                    }
                    foreach (string key in item.Keys)
                    {
                        keystr += key + ",";
                        if (key == "VRID")
                        {
                            valuestr += "'" + vrid + "',";
                        }
                        else if (key == "RID")
                        {
                            valuestr += "'" + newpsfrid + "',";
                        }
                        else if (key == "MRID")
                        {
                            valuestr += "'" + mrid + "',";
                        }
                        else if (key == "CREATETIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "MODIFYTIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "BJSTARTDATE" || key == "BJENDDATE")
                        {
                            if (item[key] + "" != "")
                            {
                                valuestr += "to_date('" + Convert.ToDateTime(item[key].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                            }
                            else
                            {
                                valuestr += "to_date('" + item[key] + "','yyyy/mm/dd'),";
                            }
                        }
                        else if (key == "BJFS")
                        {
                            BJSTA = "";
                            string BJFS = item[key] + "";
                            if (BJFS == "2")
                            {
                                BJSTA = "0";
                            }
                            else if (BJFS == "1")
                            {
                                BJSTA = "1";
                            }
                            valuestr += "'" + BJFS + "',";
                        }
                        else if (key == "BJSTATAUS")
                        {
                            string BJSTATAUS = item[key] + "";
                            // valuestr += "'" + psflist[0][key] + "',";
                            if (BJSTA == "0")
                            {
                                BJSTATAUS = "0";
                            }
                            else if (BJSTA == "1")
                            {
                                BJSTATAUS = item[key] + "";
                            }
                            else if (BJSTATAUS == "2")
                            {
                                BJSTATAUS = "1";
                            }
                            else if (BJSTATAUS == "5")
                            {
                                BJSTATAUS = "4";
                                IFBJCXS = "1";
                            }
                            valuestr += "'" + BJSTATAUS + "',";
                        }
                        else if (key == "IFBJCX")
                        {
                            string IFBJCX = item[key] + "";
                            if (!string.IsNullOrEmpty(IFBJCXS))
                            {
                                IFBJCX = "1";
                            }
                            valuestr += "'" + IFBJCX + "',";
                        }
                        else
                        {
                            valuestr += "'" + item[key] + "',";
                        }
                    }
                    string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                    DataHelper.ExecSql(addpsfsql);// 插入psf
                    // 查询该psf表对应的值表的所有信息
                    var valuelist = DataHelper.QueryDictList("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '" + item["RID"] + "' and (status <> '0' or status is null)");
                    if (valuelist.Count > 0)
                    {
                        string fsjtlj = "";//初始化方式阶梯累计
                        foreach (var item1 in valuelist)
                        {
                            var keystr1 = "";
                            var valuestr1 = "";
                            foreach (var key in item1.Keys)
                            {
                                //获取费目设置里面最新的方式阶梯累计
                                if (String.IsNullOrEmpty(fsjtlj))
                                {
                                    string gdzwhr = "";
                                    if (String.IsNullOrEmpty(item1["GDZRID"].ToString()))
                                    {
                                        gdzwhr = " and GDZRID is null";
                                    }
                                    else
                                    {
                                        gdzwhr = " and GDZRID='" + item1["GDZRID"].ToString() + "'";
                                    }
                                    fsjtlj = DataHelper.QueryValue(string.Format("select JTLJ from SQM_FEE_PUR_REF where DJFSRID='{0}' {1}", item1["DJFSRID"].ToString(), gdzwhr)) + "";
                                }
                                keystr1 += key + ',';
                                if (key == "RID")
                                {
                                    valuestr1 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "FEECALCID")
                                {
                                    valuestr1 += "'" + newpsfrid + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "STARTDATE" || key == "ENDDATE")
                                {
                                    if (item1[key] + "" == "")
                                    {
                                        valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                    }
                                    else
                                    {
                                        valuestr1 += "to_date('" + Convert.ToDateTime(item1[key].ToString()).ToString("yyyy/MM/dd") + "','YYYY/MM/DD'),";
                                    }
                                }
                                else if (key == "IFUPDATE")
                                {
                                    valuestr1 += "'1',";
                                }
                                else if (key == "JTLJ")
                                {
                                    valuestr1 += "'" + fsjtlj + "',";
                                }
                                else
                                {
                                    valuestr1 += "'" + item1[key] + "',";
                                }
                            }
                            string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                            DataHelper.ExecSql(addvaluesql);
                        }
                    }
                }

                for (var i = 0; psflist.Count > i; i++)
                {
                    string rid = psflist[i]["RID"] + "";

                    //复制仓租相关数据 SQM_BJ_CZXG
                    var CzxgList = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_CZXG WHERE BJRID='{0}' and (status <> '0' or status is null)", rid));
                    if (CzxgList.Count > 0)
                    {

                        foreach (var cz in CzxgList)
                        {
                            var keystr2 = "";
                            var valuestr2 = "";
                            foreach (string key in cz.Keys)
                            {
                                keystr2 += key + ",";
                                if (key == "RID")
                                {
                                    valuestr2 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "BJRID")
                                {
                                    valuestr2 += "'" + psfrid[i] + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr2 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr2 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else
                                {
                                    valuestr2 += "'" + cz[key] + "',";
                                }
                            }

                            string AddCzxg = string.Format("INSERT INTO SQM_BJ_CZXG ({0}) VALUES ({1})", keystr2.TrimEnd(','), valuestr2.TrimEnd(','));
                            DataHelper.ExecSql(AddCzxg);
                        }

                    }
                    //复制仓租相关 SQM_BJ_CZTJ
                    var CztjList = DataHelper.QueryDictList(string.Format("SELECT * FROM SQM_BJ_CZTJ WHERE BJRID='{0}' and (status <> '0' or status is null)", rid));
                    if (CztjList.Count > 0)
                    {
                        foreach (var cz in CztjList)
                        {
                            var keystr3 = "";
                            var valuestr3 = "";
                            foreach (string key in cz.Keys)
                            {
                                keystr3 += key + ",";
                                if (key == "RID")
                                {
                                    valuestr3 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "BJRID")
                                {
                                    valuestr3 += "'" + psfrid[i] + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr3 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr3 += "to_date('" + DateTime.Now.ToString() + "','YYYY-MM-DD hh24:mi:ss'),";
                                }
                                else
                                {
                                    valuestr3 += "'" + cz[key] + "',";
                                }

                            }
                            string AddCztj = string.Format("INSERT INTO SQM_BJ_CZTJ ({0}) VALUES ({1})", keystr3.TrimEnd(','), valuestr3.TrimEnd(','));
                            DataHelper.ExecSql(AddCztj);
                        }
                    }


                }


                //    }
                //}
                // }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmessage = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = rtnmessage, Success = flag }));
        }

        /// <summary>
        /// 已有报价中添加报价模板
        /// 从psf表找到某报价某版本并进行复制数据
        /// </summary>
        /// <param name="VERID"></param>
        /// <param name="TEMPLATENAME"></param>
        /// <param name="TEMPLATEJJ"></param>
        /// <param name="REMARK"></param>
        /// <returns></returns>
        public ActionResult AddBJMB(string VERID, string TEMPLATENAME, string TEMPLATEJJ, string REMARK)
        {
            bool flag = true;
            string rtnmsg = "保存成功";
            try
            {
                var newvrid = System.Guid.NewGuid().ToString();
                // 新增模板库数据，新增一个版本表的主键
                SQM_BJMB srcobj = new SQM_BJMB();
                srcobj.RID = System.Guid.NewGuid().ToString();
                srcobj.VERID = newvrid;
                srcobj.STATUS = "1";
                srcobj.TEMPLATENAME = TEMPLATENAME;
                srcobj.TEMPLATEJJ = TEMPLATEJJ;
                srcobj.REMARK = REMARK;
                srcobj.TEMPLATETYPE = "个人模板";// 将已有报价存为模板，默认为个人模板
                srcobj.MODIFYNAME = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();

                // 新增版本表信息,改数据只做数据关联，不属于任何报价
                SQM_BJ_VER versrcobj = new SQM_BJ_VER();
                versrcobj.RID = newvrid;
                versrcobj.DoCreate();
                // 插入psf表
                // 查询psf表数据，去掉包干费
                var psflist = DataHelper.QueryDictList("SELECT * FROM SQM_BJ_PSF WHERE VRID = '" + VERID + "' and choosestatus = '1' and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1')");// and bjstataus <> '0' and bjstataus is not null  这个筛选去掉，未保存费目也会成为模板
                string orgname = ((IList<EasyDictionary>)psflist)[0].Get("ORGNAME") + "";
                srcobj.ORGNAME = orgname;
                srcobj.DoCreate();
                foreach (var item in psflist)
                {
                    //拼接插入字符串
                    var keystr = "";
                    var valuestr = "";
                    var newpsfrid = System.Guid.NewGuid().ToString();
                    string rid = item.Get("RID") + "";
                    string bgfzrid = item.Get("BGFZRID") + "";
                    if (bgfzrid == "1")
                    {
                        // 包干费
                        DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + rid + "' and (status <> '0' or status is null)");
                        if (dtbgf.Rows.Count > 0)//
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
                                        if (dr[colName] + "" != "")
                                        {
                                            value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                                        }
                                        else
                                        {
                                            value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                        }
                                    }
                                    else if (colName == "BGFZRID")// 新的paf表 主包干费rid
                                    {
                                        value += "'" + newpsfrid + "',";
                                    }
                                    else if (colName == "RID")
                                    {
                                        value += "'" + bgfrid + "',";
                                    }
                                    else if (colName == "MRID")
                                    {
                                        value += "' ',";
                                    }
                                    else if (colName == "VRID")
                                    {
                                        value += "'" + newvrid + "',";
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
                    }
                    foreach (string key in item.Keys)
                    {
                        keystr += key + ",";
                        if (key == "VRID")
                        {
                            valuestr += "'" + newvrid + "',";
                        }
                        else if (key == "RID")
                        {
                            valuestr += "'" + newpsfrid + "',";
                        }
                        else if (key == "MRID")
                        {
                            valuestr += "' ',";
                        }
                        else if (key == "CREATETIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "MODIFYTIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "BJSTARTDATE" || key == "BJENDDATE")
                        {
                            if (item[key] + "" != "")
                            {
                                valuestr += "to_date('" + Convert.ToDateTime(item[key].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                            }
                            else
                            {
                                valuestr += "to_date('" + item[key] + "','yyyy/mm/dd'),";
                            }
                        }
                        else if (key == "BJFS")
                        {
                            if (item[key] + "" == "")
                            {
                                valuestr += "'0',";
                            }
                            else
                            {
                                valuestr += "'" + item[key] + "',";
                            }
                        }
                        else if (key == "BJSTATAUS")
                        {
                            if (item[key] + "" == "2")// 已确认 -> 已保存
                            {
                                valuestr += "'1',";
                            }
                            else if (item[key] + "" == "5")// 已确认(报价超限) -> 已保存(报价超限)
                            {
                                valuestr += "'4',";
                            }
                            else
                            {
                                valuestr += "'" + item[key] + "',";
                            }
                        }
                        else
                        {
                            valuestr += "'" + item[key] + "',";
                        }
                    }
                    string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                    DataHelper.ExecSql(addpsfsql);// 插入psf
                    // 查询该psf表对应的值表的所有信息
                    var valuelist = DataHelper.QueryDictList("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '" + item["RID"] + "' and (status <> '0' or status is null)");
                    if (valuelist.Count > 0)
                    {
                        foreach (var item1 in valuelist)
                        {
                            var keystr1 = "";
                            var valuestr1 = "";
                            foreach (var key in item1.Keys)
                            {
                                keystr1 += key + ',';
                                if (key == "RID")
                                {
                                    valuestr1 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "FEECALCID")
                                {
                                    valuestr1 += "'" + newpsfrid + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "STARTDATE" || key == "ENDDATE")
                                {
                                    if (item1[key] + "" == "")
                                    {
                                        valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                    }
                                    else
                                    {
                                        valuestr1 += "to_date('" + Convert.ToDateTime(item1[key].ToString()).ToString("yyyy/MM/dd") + "','YYYY/MM/DD'),";
                                    }
                                }
                                //else if (key == "GUIDEPRICE")// 置空指导价 模板里存的都是无定价报价
                                //{
                                //    valuestr1 += "'',";
                                //}
                                //else if (key == "MAXPRICE")// 置空最高价
                                //{
                                //    valuestr1 += "'',";
                                //}
                                //else if (key == "MINPRICE")// 置空最低价
                                //{
                                //    valuestr1 += "'',";
                                //}
                                else
                                {
                                    valuestr1 += "'" + item1[key] + "',";
                                }
                            }
                            string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                            DataHelper.ExecSql(addvaluesql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Message = rtnmsg, Data = "", }));
        }
        /// <summary>
        /// 模板数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MBLists()
        {
            string createname = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string PERSONALMB = Request["PERSONALMB"] + "";// 个人模板
            string PUBLICMB = Request["PUBLICMB"] + "";// 公共模板
            string TEMPLATENAME = Request["TEMPLATENAME"] + "";
            string sql = "";
            //查询条件拼接
            string wherestr = "WHERE (1=1)";
            wherestr += "and (createname <> '' or createname is not null)";
            if (PUBLICMB == "公共模板" && PERSONALMB == "个人模板")
            {
                wherestr += " and (TEMPLATETYPE = '公共模板' or (createname = '" + createname + "' and TEMPLATETYPE = '个人模板'))";
            }
            else if (PUBLICMB == "" && PERSONALMB == "个人模板")
            {
                wherestr += " and createname = '" + createname + "' and TEMPLATETYPE = '个人模板'";
            }
            else if (PUBLICMB == "公共模板" && PERSONALMB == "")
            {
                wherestr += " and TEMPLATETYPE = '公共模板'";
            }
            else if (PUBLICMB == "" && PERSONALMB == "")
            {
                wherestr += " and TEMPLATETYPE = ''";
            }
            if (TEMPLATENAME != "")
            {
                wherestr += " and TEMPLATENAME like '%" + TEMPLATENAME + "%'";
            }
            sql = string.Format("SELECT * FROM SQM_BJMB {0} order by createtime desc", wherestr);
            string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            string countsql = string.Format("SELECT COUNT(*) FROM SQM_BJMB {0}", wherestr);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 查询该版本下费目是否存在未保存 去掉包干费
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckFeeStatus(string vrid)
        {
            string psfName = "";
            if (!string.IsNullOrEmpty(vrid))
            {
                DataTable dt = DataHelper.QueryDataTable("select * from sqm_bj_psf where (status <> '0' or status is null) and vrid = '" + vrid + "' and choosestatus = '1' and (bgfzrid is null or bgfzrid = '1')");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["BJSTATAUS"] + "" == "0")// 未保存
                    {
                        psfName += "\"" + dr["PRODUCT_NAME"] + "\"-\"" + dr["SERVICE_NAME"] + "\"-\"" + dr["FEE_NAME"] + "\"<BR>";
                    }
                }
            }
            return Content(JsonHelper.GetJsonString(psfName));
        }
        /// <summary>
        /// 返回当前报价产品，模板产品，判断哪些匹配模板，哪些忽略
        /// </summary>
        /// <param name="MBvrid"></param>
        /// <param name="Pvrid"></param>
        /// <returns></returns>
        public ActionResult MatchMB(string MBvrid, string Pvrid)
        {
            DataTable currentdt = DataHelper.QueryDataTable("select distinct product_code,product_name from sqm_bj_psf where (status <> '0' or status is null) and vrid = '" + Pvrid + "'");
            DataTable mbdt = DataHelper.QueryDataTable("select distinct product_code,product_name from sqm_bj_psf where (status <> '0' or status is null) and vrid = '" + MBvrid + "'");
            //Dictionary<string,string> currentp = new Dictionary<string, string>();
            //Dictionary<string, string> mbp = new Dictionary<string, string>();
            ArrayList list = new ArrayList();
            if (currentdt.Rows.Count > 0)
            {
                //foreach (DataRow dr in currentdt.Rows)
                //{
                //    currentp[dr["PRODUCT_CODE"] + ""] = dr["PRODUCT_NAME"] + "";
                //}
                list.Add(currentdt);
            }
            if (mbdt.Rows.Count > 0)
            {
                //foreach (DataRow dr in mbdt.Rows)
                //{
                //    mbp[dr["PRODUCT_CODE"] + ""] = dr["PRODUCT_NAME"] + "";
                //}
                list.Add(mbdt);
            }
            //if (currentp.Count > 0)
            //{
            //    list.Add(currentp);
            //}
            //if(mbp.Count > 0)
            //{
            //    list.Add(mbp);
            //}
            return Content(JsonHelper.GetJsonString(list));
        }
        /// <summary>
        /// 选择模板，执行模板覆盖规则
        /// </summary>
        /// <param name="MBvrid">模板版本表rid</param>
        /// <param name="Pvrid">当前报价当前版本rid</param>
        /// <returns></returns>
        public ActionResult chooseMB(string MBvrid, string Pvrid, string Mrid)
        {
            bool flag = true;
            string rtnmsg = "选择模板成功";
            string prdcode = "";// 无定价报价时，给报价值表djrid插数据使用；因为模板没有组织，所以无定价报价（无组织）无法定位是sqm_dj_psf 表中哪一条数据
            string srvcode = "";
            string feecode = "";
            string orgcode = "";
            string orgname = "";
            try
            {
                // 首先获取一下组织信息
                DataTable dt = DataHelper.QueryDataTable("SELECT distinct ORGCODE,ORGNAME FROM SQM_BJ_PSF WHERE VRID = '" + Pvrid + "' and (orgcode is not null or orgcode <> '')");
                if (dt.Rows.Count > 0)
                {
                    orgcode = dt.Rows[0]["ORGCODE"].ToString();
                    orgname = dt.Rows[0]["ORGNAME"].ToString();
                }
                // 1 拿到模板的所有产品code
                string MBsql = string.Format("SELECT DISTINCT PRODUCT_CODE,PRODUCT_NAME FROM SQM_BJ_PSF WHERE VRID = '{0}' and (status <> '0' or status is null)", MBvrid);
                var MBres = DataHelper.QueryDictList(MBsql);
                List<string> MBPRDLIST = new List<string> { };
                Dictionary<string, string> prdcodename = new Dictionary<string, string>();
                foreach (var item in MBres)
                {
                    prdcodename.Add(item["PRODUCT_CODE"].ToString(), item["PRODUCT_NAME"].ToString());
                    MBPRDLIST.Add(item["PRODUCT_CODE"].ToString());
                }
                // 2 拿到当前报价的所有信息 
                string PAllSsql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' and (status <> '0' or status is null)", Pvrid);
                var PDataAll = DataHelper.QueryDictList(PAllSsql);
                string Psql = string.Format("SELECT DISTINCT PRODUCT_CODE FROM SQM_BJ_PSF WHERE VRID = '{0}'", Pvrid);
                var Pres = DataHelper.QueryDictList(Psql);
                List<string> PPRDLIST = new List<string> { };
                foreach (var item in Pres)
                {
                    PPRDLIST.Add(item["PRODUCT_CODE"].ToString());
                }
                // 3 判断模板产品中有没有已选报价的产品，有的话做替换操作
                foreach (var item in MBPRDLIST)
                {
                    // 当前报价包含模板中的产品
                    if (PPRDLIST.Contains(item))
                    {
                        // 删除报价值表
                        string delete_val = string.Format("delete from sqm_modebj_val where feecalcid in (select rid from sqm_bj_psf where product_code = '{0}' and vrid = '{1}' and (status <> '0' or status is null))", item, Pvrid);
                        // 将当前产品所有信息删除
                        string DELsql = string.Format("DELETE FROM SQM_BJ_PSF WHERE VRID = '{0}' AND PRODUCT_CODE = '{1}' and (status <> '0' or status is null)", Pvrid, item);
                        DataHelper.ExecSql("begin " + delete_val + ";" + DELsql + ";end;");

                        // 将模板中对应的产品数据插入 - 去掉包干费
                        string MBallsql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND PRODUCT_CODE = '{1}' and (bgfzrid is null or bgfzrid = '1')", MBvrid, item);// and (bjstataus <> '0' and bjstataus is not null) 
                        var MBallList = DataHelper.QueryDictList(MBallsql);
                        // 拼接插入的insert sql语句
                        foreach (var item2 in MBallList)
                        {
                            var keystr = "";
                            var valuestr = "";
                            var psfRid = System.Guid.NewGuid().ToString();
                            var mbpsfrid = item2["RID"];
                            string alonefee = item2["ALONEFEE"] + "";
                            // 包干费
                            DataTable dtbgf = DataHelper.QueryDataTable("select * from sqm_bj_psf where bgfzrid = '" + mbpsfrid + "' and (status <> '0' or status is null)");
                            if (dtbgf.Rows.Count > 0)//
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
                                            if (dr[colName] + "" != "")
                                            {
                                                value += "to_date('" + Convert.ToDateTime(dr[colName].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                                            }
                                            else
                                            {
                                                value += "to_date('" + dr[colName] + "','yyyy/mm/dd'),";
                                            }
                                        }
                                        else if (colName == "BGFZRID")// 新的paf表 主包干费rid
                                        {
                                            value += "'" + psfRid + "',";
                                        }
                                        else if (colName == "RID")
                                        {
                                            value += "'" + bgfrid + "',";
                                        }
                                        else if (colName == "MRID")
                                        {
                                            value += "'" + Mrid + "',";
                                        }
                                        else if (colName == "VRID")
                                        {
                                            value += "'" + Pvrid + "',";
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
                            foreach (string key in item2.Keys)
                            {
                                keystr += key + ',';
                                if (key == "RID")
                                {
                                    valuestr += "'" + psfRid + "',";
                                }
                                else if (key == "VRID")
                                {
                                    valuestr += "'" + Pvrid + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "BJSTARTDATE" || key == "BJENDDATE")
                                {
                                    if (item2[key] + "" != "")
                                    {
                                        valuestr += "to_date('" + Convert.ToDateTime(item2[key].ToString()).ToString("yyyy/MM/dd") + "','YYYY/MM/DD'),";
                                    }
                                    else
                                    {
                                        valuestr += "to_date('" + item2[key] + "','YYYY/MM/DD'),";
                                    }
                                }
                                else if (key == "MRID")
                                {
                                    valuestr += "'" + Mrid + "',";
                                }
                                else if (key == "BJSTATAUS")
                                {
                                    if (item2[key] + "" == "2")
                                    {
                                        valuestr += "'1',";// “确认”状态置为已保存
                                    }
                                    else
                                    {
                                        valuestr += "'" + item2[key] + "',";
                                    }
                                }
                                else if (key == "PRODUCT_CODE")
                                {
                                    prdcode = item2[key] + "";
                                    valuestr += "'" + item2[key] + "',";
                                }
                                else if (key == "SERVICE_CODE")
                                {
                                    srvcode = item2[key] + "";
                                    valuestr += "'" + item2[key] + "',";
                                }
                                else if (key == "FEE_CODE")
                                {
                                    feecode = item2[key] + "";
                                    valuestr += "'" + item2[key] + "',";
                                }
                                else if (key == "ORGCODE")
                                {
                                    valuestr += "'" + orgcode + "',";
                                }
                                else if (key == "ORGNAME")
                                {
                                    valuestr += "'" + orgname + "',";
                                }
                                else
                                {
                                    valuestr += "'" + item2[key] + "',";
                                }
                            }
                            string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                            DataHelper.ExecSql(addpsfsql);
                            // 插入值表数据，先查模板psf表对应的值表数据
                            string djpsfrid = DataHelper.QueryValue(string.Format("select rid from sqm_dj_psf where feecode = '{0}' and srvcode = '{1}' and prdcode = '{2}' and (orgrid is not null or orgrid <> '') and orgrid like '%{3}%'", feecode, srvcode, prdcode, orgcode)) + "";
                            DataTable dtdjval = new DataTable();
                            Dictionary<string, string> dicStr = new Dictionary<string, string>();
                            Decimal? guidePrice = null;
                            Decimal? maxPrice = null;
                            Decimal? minPrice = null;
                            if (djpsfrid == "")// 如果为空，则在sqm_dj_psf 表插入一条数据
                            {
                                DataTable dtmb = DataHelper.QueryDataTable("select * from sqm_bj_psf where rid = '" + mbpsfrid + "'");
                                dtmb.Rows[0]["ORGCODE"] = orgcode;
                                dtmb.Rows[0]["ORGNAME"] = orgname;
                                djpsfrid = CreateDJPSF(dtmb, alonefee);
                            }
                            else// 如果不为空，匹配该定价值表数据
                            {
                                dtdjval = DataHelper.QueryDataTable("select * from sqm_modedj_val where feecalcid = '" + djpsfrid + "' and (status <> '0' or status is null) and djstatus = '1'");// 已发布
                                foreach (DataRow dw in dtdjval.Rows)
                                {
                                    string str = "";
                                    string rid = "";
                                    foreach (DataColumn col in dtdjval.Columns)
                                    {
                                        string colName = @"COLUMN[0-9]+C$";// pattern : COLUMN1C
                                        if (Regex.IsMatch(col.ColumnName, colName))
                                        {
                                            str += dw[col.ColumnName];
                                        }
                                    }
                                    if (str != "")
                                    {

                                        rid = dw["RID"] + "";
                                        dicStr[rid] = str;
                                    }
                                }
                            }
                            var valuelist = DataHelper.QueryDictList("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '" + mbpsfrid + "'");
                            foreach (var item1 in valuelist)
                            {
                                string valueStr = "";// 基础数据 code串
                                bool wdjbj = false;// 判断是否为无定价报价（实际上模板的明细均为无定价报价）
                                var keystr1 = "";
                                var valuestr1 = "";
                                string rid = System.Guid.NewGuid().ToString();
                                foreach (var key in item1.Keys)
                                {
                                    keystr1 += key + ',';
                                    if (key == "RID")
                                    {
                                        valuestr1 += "'" + rid + "',";
                                    }
                                    else if (key == "FEECALCID")
                                    {
                                        valuestr1 += "'" + psfRid + "',";
                                    }
                                    else if (key == "CREATETIME")
                                    {
                                        valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                    }
                                    else if (key == "MODIFYTIME")
                                    {
                                        valuestr1 += "to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD hh24:mi:ss'),";
                                    }
                                    else if (key == "STARTDATE" || key == "ENDDATE")
                                    {
                                        if (item1[key] + "" == "")
                                        {
                                            valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                        }
                                        else
                                        {
                                            valuestr1 += "to_date('" + Convert.ToDateTime(item1[key].ToString()).ToString("yyyy/MM/dd") + "','YYYY/MM/DD'),";
                                        }
                                    }
                                    else if (key == "GUIDEPRICE")// 如果指导价为空（肯定为空），则为无定价报价，查询sqm_dj_psf 的 rid
                                    {
                                        if (item1[key] + "" == "")
                                        {
                                            wdjbj = true;
                                        }
                                        valuestr1 += "'" + item1[key] + "',";
                                    }
                                    else if (key == "DJRID")// djrid 存储定价值表rid，如果为无定价报价，则存sqm_dj_psf 的 rid
                                    {
                                        if (wdjbj)
                                        {
                                            wdjbj = true;
                                            valuestr1 += "'" + djpsfrid + "',";
                                        }
                                        else
                                        {
                                            valuestr1 += "'" + item1[key] + "',";
                                        }
                                    }
                                    else if (Regex.IsMatch(key, @"COLUMN[0-9]+C$"))
                                    {
                                        valuestr1 += "'" + item1[key] + "',";
                                        // 存储基础数据串，与定价值表数据做比较，是否是已定价数据，已定价数据带指导价、最高价、最低价
                                        valueStr += item1[key];
                                    }
                                    else
                                    {
                                        valuestr1 += "'" + item1[key] + "',";
                                    }
                                }
                                string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                                DataHelper.ExecSql(addvaluesql);
                                // 数据匹配
                                if (dicStr.ContainsValue(valueStr))
                                {
                                    foreach (KeyValuePair<string, string> kv in dicStr)
                                    {
                                        string key = kv.Key;
                                        string value = kv.Value;
                                        if (value == valueStr)
                                        {
                                            DataRow[] drs = dtdjval.Select("RID = '" + key + "'");
                                            guidePrice = Convert.ToDecimal(drs[0]["GUIDEPRICE"] + "");
                                            maxPrice = Convert.ToDecimal(drs[0]["MAXPRICE"] + "");
                                            minPrice = Convert.ToDecimal(drs[0]["MINPRICE"] + "");
                                        }
                                    }
                                    if (guidePrice != null && maxPrice != null && minPrice != null)
                                    {
                                        DataHelper.ExecSql(string.Format("update sqm_modebj_val set guideprice = {0},maxprice = {1},minprice = {2} where rid = '{3}'", guidePrice, maxPrice, minPrice, rid));
                                    }
                                }
                            }
                        }
                        rtnmsg += "<BR>产品 \"" + prdcodename[item] + "\" 信息已被覆盖";
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }

            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = rtnmsg, Success = flag }));
        }
        /// <summary>
        /// 定价psf表插数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="alonefee"></param>
        /// <returns></returns>
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
            sdp.ALONEFEE = isalone;//绑定关系
            sdp.ORGRID = sdp.ORGCODE;
            sdp.ORGCODE = sdp.ORGNAME;
            if (sdp.ORGNAME != null)
            {
                sdp.ORGNAME = sdp.ORGNAME.Split('-')[0];
            }
            sdp.DJFS = "0";//默认普通定价
            sdp.RID = djrid;
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
                sdp.CREATESOURCE = "选择模板";
                sdp.DoCreate();
            }
            return djrid;
        }
        /// <summary>
        /// 上传文件时间更新
        /// </summary>
        /// <param name="RID"></param>
        /// <returns></returns>
        public ActionResult SAVEUPLOADTIME(string RID, string UploadName, string UploadUrl)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string show = "1";
            string rtnmsg = "保存成功！";
            bool flag = true;
            try
            {
                var obj = SQM_BJ_VER.TryFind(RID);
                obj.UPLOADTIME = Convert.ToDateTime(time);
                obj.UPLOADNAME = UploadName;
                obj.UPLOADURL = UploadUrl;
                obj.SHOWMODE = show;
                obj.DoUpdate();
            }
            catch (Exception ex)
            {
                rtnmsg = ex.Message;
                flag = false;
            }

            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = time, Message = rtnmsg, Success = false, Code = show }));
        }

        /// <summary>
        /// 根据当前报价最新的zver 生成最新的
        /// </summary>
        /// <param name="mrid"></param>
        /// <returns></returns>
        public string GetLatestVer(string mrid)
        {
            string sql = string.Format("select * from sqm_bj_ver where mrid = '{0}' order by createtime desc", mrid);
            var versionOld = DataHelper.QueryDictList(sql)[0]["ZVER"].ToString();
            string versionNew = "V" + (int.Parse(versionOld.Substring(1)) + 1);
            return versionNew;
        }

        public ActionResult ChooseBP()
        {
            string user = Request["user"];
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(user))
            {
                string sql = "select distinct t2.bpcode,t2.bpname from sqm_bj_main_basic t1,sqm_bj_bp t2 where t1.rid = t2.mrid and t1.createuser = '" + user + "'";
                dt = DataHelper.QueryDataTable(sql);
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = dt, Message = "", Success = true }));
        }

        public ActionResult GetPriceFromBp(string bjname)
        {
            var user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string bpcode = Request["bpcode"];
            string sql = string.Format("select a.*,b.zver,b.modifytime as mdtime,b.status as sta,c.bpname from sqm_bj_main_basic a left join( select v.*, to_number(replace(v.zver, 'V', '')) from sqm_bj_ver v inner join( select max(to_number(replace(zver, 'V', ''))) vs, mrid from sqm_bj_ver where createuser = '{1}' group by mrid) a on v.mrid = a.mrid and to_number(replace(v.zver, 'V', '')) = a.vs) b on a.rid = b.mrid left join sqm_bj_bp c on a.rid = c.mrid where (a.fbprice<>'1'or a.fbprice is null) and a.createuser = '{1}'  and a.bjname like '%{0}%' and c.bpcode = '{2}' order by a.createtime desc", bjname, user, bpcode);
            var data = DataHelper.QueryDictList(sql);
            string fwa = "";
            foreach (var item in data)
            {
                sql = string.Format("select * from(select FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}' order by CREATETIME desc) where rownum = 1", item["RID"].ToString(), item["ZVER"].ToString());
                fwa = DataHelper.QueryValue(sql) + "";
                item.Add("FWA", fwa);
            }
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult GetFBPriceFromBp(string bjname)
        {
            var user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string bpcode = Request["bpcode"];
            string sql = string.Format("select a.*,b.zver,b.modifytime as mdtime,b.status as sta,c.bpname from sqm_bj_main_basic a left join( select v.*, to_number(replace(v.zver, 'V', '')) from sqm_bj_ver v inner join( select max(to_number(replace(zver, 'V', ''))) vs, mrid from sqm_bj_ver where createuser = '{1}' group by mrid) a on v.mrid = a.mrid and to_number(replace(v.zver, 'V', '')) = a.vs) b on a.rid = b.mrid left join sqm_bj_bp c on a.rid = c.mrid where a.fbprice='1'and a.createuser = '{1}'  and a.bjname like '%{0}%' and c.bpcode = '{2}' order by a.createtime desc", bjname, user, bpcode);
            var data = DataHelper.QueryDictList(sql);
            return Content(JsonHelper.GetJsonString(data));
        }

        public ActionResult CGSCWorkFlowXML(string rid, string vrid)//提交审批
        {
            SQM_BJ_VER sbvs = SQM_BJ_VER.TryFind(vrid);
            if (!string.IsNullOrEmpty(sbvs.REQUESTID))
            {
                string msgs = "已提交过审批";
                bool flags = false;
                return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = msgs, Success = flags }));
            }
            else
            {

                string msg = "提交成功";
                bool flag = true;
                string Rids = "";//SQM_BJ_PS RID
                string Mrid = "";// SQM_BJ_PS MRID
                string Status = "";//报价状态
                string PRODUCT_NAME = "";//产品
                string SERVICE_NAME = "";//服务
                string FEE_NAME = "";//费目
                string StartTime = "";//起始有效期
                string EndTime = "";//截止有效期
                string IFBJCX = "";//另存的超限过滤
                string BJ = "0";//判断报价区间 0.报价区间内 1.报价区间外 2.非标报价
                var userid = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                bool FBSTATUS = false;

                string FBsql = string.Format("select * from SQM_BJ_VER sbv  left join SQM_BJ_MAIN_BASIC sbmb on sbv.mrid=sbmb.rid where fbprice='1' and sbv.rid='{0}'", vrid);
                DataTable FBdt = DataHelper.QueryDataTable(FBsql);
                if (FBdt.Rows.Count > 0)
                {
                    FBSTATUS = true;
                }
                if (FBSTATUS == false)
                {

                    string sqls = string.Format("select RID,MRID,PRODUCT_NAME,SERVICE_NAME,FEE_NAME,BJSTATAUS,BJSTARTDATE,BJENDDATE,IFBJCX from SQM_BJ_PSF where vrid='{0}' and (status<>'0' or status is null) and (bgfzrid is null or bgfzrid='1') and service_name is not null and fee_name is not null", vrid);
                    DataTable Stataus = DataHelper.QueryDataTable(sqls);
                    if (Stataus.Rows.Count > 0)
                    {

                        foreach (DataRow dr in Stataus.Rows)
                        {
                            Rids = dr["RID"].ToString();
                            Mrid = dr["MRID"].ToString();
                            Status = dr["BJSTATAUS"].ToString();
                            PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                            SERVICE_NAME = dr["SERVICE_NAME"].ToString();
                            FEE_NAME = dr["FEE_NAME"].ToString();
                            StartTime = dr["BJSTARTDATE"].ToString();
                            EndTime = dr["BJENDDATE"].ToString();
                            IFBJCX = dr["IFBJCX"].ToString();

                            if (Status == "5" && string.IsNullOrEmpty(IFBJCX))
                            {
                                BJ = "1";

                            }

                            if (string.IsNullOrEmpty(StartTime))
                            {
                                SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(Rids);
                                //SQM_BJ_MAIN_BASIC sbmb= SQM_BJ_MAIN_BASIC.Find(Mrid);
                                SQM_BJ_VER sbv = SQM_BJ_VER.Find(sbp.VRID);

                                //sbp.BJSTARTDATE = sbmb.DTFROM;
                                sbp.BJSTARTDATE = sbv.DTFROM;
                                sbp.DoUpdate();
                            }
                            if (string.IsNullOrEmpty(EndTime))
                            {
                                SQM_BJ_PSF sbp = SQM_BJ_PSF.Find(Rids);
                                //SQM_BJ_MAIN_BASIC sbmb = SQM_BJ_MAIN_BASIC.Find(Mrid);
                                SQM_BJ_VER sbv = SQM_BJ_VER.Find(sbp.VRID);


                                //sbp.BJENDDATE = sbmb.DTTO;
                                sbp.BJENDDATE = sbv.DTTO;
                                sbp.DoUpdate();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(Status))
                    {
                        msg = "无报价，无法进行提交。";
                        flag = false;
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = msg, Success = flag }));
                    }
                    else if (Status == "0" || Status == "1" || Status == "3" || Status == "4")
                    {
                        string ts = "";
                        if (Status == "0")
                        {
                            ts = "未保存。";
                        }
                        else if (Status == "1")
                        {
                            ts = "未确认。";
                        }
                        else if (Status == "3")
                        {
                            ts = "报价无定价。";
                        }
                        else if (Status == "4")
                        {
                            ts = "未确认，超限。";
                        }
                        msg = "该产品：" + PRODUCT_NAME + ",服务：" + SERVICE_NAME + ",费目：" + FEE_NAME + "。提交失败:" + ts;
                        flag = false;
                        return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = msg, Success = flag }));
                    }
                    else
                    {
                        FBSTATUS = true;

                    }
                }
                if (FBSTATUS == true)
                {
                    try
                    {
                        string FlowID = "";
                        string FlowName = "";
                        string FlowKey = feilioahelper.GetFlow(vrid, rid);
                        string DCPFZR = "";
                        string CPFZR = "";
                        string FBBG = "";
                        if (FlowKey == "0")
                        {
                            FlowID = "2561";
                            FlowName = "单事业部产品报价审批";
                            CPFZR = feilioahelper.GetCPFZR(vrid) + "";
                        }
                        else if (FlowKey == "1")
                        {
                            FlowID = "2562";
                            FlowName = "跨事业部产品报价审批";
                            DCPFZR = feilioahelper.GetDCPFZR(vrid) + "";
                        }
                        else if (FlowKey == "2")
                        {
                            FlowID = "2563";
                            FlowName = "非标报价和包含包干费报价审批";
                            BJ = "2";
                            FBBG = "非标";
                            CPFZR = feilioahelper.GetCPFZR(vrid) + "";

                        }
                        else if (FlowKey == "3")
                        {
                            FlowID = "2563";
                            FlowName = "非标报价和包含包干费报价审批";
                            BJ = "2";
                            FBBG = "非标";
                            DCPFZR = feilioahelper.GetDCPFZR(vrid) + "";

                        }
                        else if (FlowKey == "4")
                        {
                            FlowID = "2563";
                            FlowName = "非标报价和包含包干费报价审批";
                            BJ = "2";
                            FBBG = "包干";
                            CPFZR = feilioahelper.GetCPFZR(vrid) + "";

                        }



                        // xmltemp = string.Format(xmltemp, sent.ID, sent.CUSTOMERNO, sent.SIMPLENAME, sent.NAME + "" == "" ? sent.ENNAME : sent.NAME, sent.ENNAME);
                        string xml = @"
<WorkflowRequestInfo>
   <requestName>[流程请求标题]</requestName>
   <requestLevel>0</requestLevel>
        <workflowBaseInfo>
            <workflowId>" + FlowID + @"</workflowId>
            <workflowName>" + FlowName + @"</workflowName>
            <workflowTypeName>" + FlowName + @"</workflowTypeName>
       </workflowBaseInfo>
    
   <creatorId>[创建人EXT1]</creatorId>
   <canView>true</canView>
   <canEdit>true</canEdit>
   <mustInputRemark>false</mustInputRemark>
   <needAffirmance>false</needAffirmance>

      <workflowMainTableInfo>
         <requestRecords>
         <weaver.workflow.webservices.WorkflowRequestTableRecord>
             <recordOrder>0</recordOrder>
             <workflowRequestTableFields>

        <weaver.workflow.webservices.WorkflowRequestTableField>
             <fieldName>SQR</fieldName>
             <fieldValue>[申请人]</fieldValue>
             <fieldOrder>0</fieldOrder>
             <isView>true</isView>
             <isEdit>true</isEdit>
             <isMand>false</isMand>
         </weaver.workflow.webservices.WorkflowRequestTableField>
             <weaver.workflow.webservices.WorkflowRequestTableField>
             <fieldName>BJBH</fieldName>
             <fieldValue>[报价编号]</fieldValue>
             <fieldOrder>0</fieldOrder>
             <isView>true</isView>
             <isEdit>true</isEdit>
             <isMand>false</isMand>
             </weaver.workflow.webservices.WorkflowRequestTableField>

       <weaver.workflow.webservices.WorkflowRequestTableField>
          <fieldName>CP</fieldName>
          <fieldValue>[产品]</fieldValue>
          <fieldOrder>0</fieldOrder>
          <isView>true</isView>
          <isEdit>true</isEdit>
          <isMand>false</isMand>
      </weaver.workflow.webservices.WorkflowRequestTableField>

     <weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>CPSSSYB</fieldName>
         <fieldValue>[产品所属事业部]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

 <weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>BSQRXM</fieldName>
         <fieldValue>[销售]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

 <weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>XSRSZZ</fieldName>
         <fieldValue>[销售人事组织]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

 <weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>YYZZ</fieldName>
         <fieldValue>[运营组织]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>FJ</fieldName>
         <fieldValue>[附报价单审批]</fieldValue>
         <fieldType>[附件名称]</fieldType>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>MIN</fieldName>
         <fieldValue>[MIN]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>

<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>BJQJ</fieldName>
         <fieldValue>[报价区间]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>


<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>SQRQ</fieldName>
         <fieldValue>[申请日期]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
     </weaver.workflow.webservices.WorkflowRequestTableField>
<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>CPFZR</fieldName>
         <fieldValue>[产品负责人]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
   </weaver.workflow.webservices.WorkflowRequestTableField>
<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>DCPFZR</fieldName>
         <fieldValue>[产品负责人-多]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
   </weaver.workflow.webservices.WorkflowRequestTableField>
<weaver.workflow.webservices.WorkflowRequestTableField>
         <fieldName>ISDJFW</fieldName>
         <fieldValue>[是否超出定价范围]</fieldValue>
         <fieldOrder>0</fieldOrder>
         <isView>true</isView>
         <isEdit>true</isEdit>
         <isMand>false</isMand>
   </weaver.workflow.webservices.WorkflowRequestTableField>
        <weaver.workflow.webservices.WorkflowRequestTableField>
            <fieldName>MEMO</fieldName>
            <fieldValue>[审批备注]</fieldValue>
            <fieldOrder>0</fieldOrder>
            <isView>true</isView>
            <isEdit>true</isEdit>
            <isMand>false</isMand>
        </weaver.workflow.webservices.WorkflowRequestTableField>
        <weaver.workflow.webservices.WorkflowRequestTableField>
            <fieldName>FBBG</fieldName>
            <fieldValue>[非标包干]</fieldValue>
            <fieldOrder>0</fieldOrder>
            <isView>true</isView>
            <isEdit>true</isEdit>
            <isMand>false</isMand>
        </weaver.workflow.webservices.WorkflowRequestTableField>
        <weaver.workflow.webservices.WorkflowRequestTableField>
            <fieldName>FBLB</fieldName>
            <fieldValue>[非标原因]</fieldValue>
            <fieldOrder>0</fieldOrder>
            <isView>true</isView>
            <isEdit>true</isEdit>
            <isMand>false</isMand>
        </weaver.workflow.webservices.WorkflowRequestTableField>
        <weaver.workflow.webservices.WorkflowRequestTableField>
            <fieldName>FBYYBCSM</fieldName>
            <fieldValue>[非标原因补充说明]</fieldValue>
            <fieldOrder>0</fieldOrder>
            <isView>true</isView>
            <isEdit>true</isEdit>
            <isMand>false</isMand>
        </weaver.workflow.webservices.WorkflowRequestTableField>
        <weaver.workflow.webservices.WorkflowRequestTableField>
            <fieldName>ISTEST</fieldName>
            <fieldValue>[是否测试系统]</fieldValue>
            <fieldOrder>0</fieldOrder>
            <isView>true</isView>
            <isEdit>true</isEdit>
            <isMand>false</isMand>
        </weaver.workflow.webservices.WorkflowRequestTableField>
        
     </workflowRequestTableFields>
    </weaver.workflow.webservices.WorkflowRequestTableRecord>
    </requestRecords>
    </workflowMainTableInfo>
 
  </WorkflowRequestInfo>";

                        string usersid = feilioahelper.GetEXT1(userid) + "";
                        xml = xml.Replace("[流程请求标题]", FlowName);//
                        xml = xml.Replace("[创建人EXT1]", usersid);
                        xml = xml.Replace("[申请人]", usersid);
                        xml = xml.Replace("[报价编号]", feilioahelper.GetBJNO(rid) + "");
                        xml = xml.Replace("[产品]", feilioahelper.GetCP(vrid) + "");
                        xml = xml.Replace("[产品所属事业部]", feilioahelper.GetCPSSSYB(vrid) + "");
                        xml = xml.Replace("[销售]", usersid);
                        string XSRSZZ = feilioahelper.GetXSRSZZ(userid) + "";
                        string YYZZ = feilioahelper.GetYYZZ(rid) + "";
                        if (XSRSZZ.Length > 4)
                        {
                            XSRSZZ = XSRSZZ.Substring(0, 4);
                        }
                        if (YYZZ.Length > 4)
                        {
                            YYZZ = YYZZ.Substring(0, 4);
                        }
                        xml = xml.Replace("[销售人事组织]", XSRSZZ + "");
                        xml = xml.Replace("[运营组织]", YYZZ + "");
                        //多个文件的获取 附件
                        string sql = "select UPLOADNAME from SQM_BJ_VER where  rid='{0}'";
                        sql = string.Format(sql, vrid);
                        DataTable dataTable = DataHelper.QueryDataTable(sql);

                        string FJ = "";
                        string FjName = "";
                        string port = System.Configuration.ConfigurationManager.AppSettings["port"];
                        if (dataTable != null && dataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string UPLOADNAME = row["UPLOADNAME"] + "";
                                if (!string.IsNullOrEmpty(UPLOADNAME))
                                {
                                    FJ += "http://" + Request.Url.Host + ":" + port + "/Excel/output/" + UPLOADNAME + "|";
                                    FjName += "http:" + UPLOADNAME + "|";
                                }
                                else
                                {
                                    FJ = "";
                                }

                            }
                        }

                        xml = xml.Replace("[附报价单审批]", FJ.TrimEnd('|'));
                        xml = xml.Replace("[附件名称]", FjName.TrimEnd('|'));

                        /*string fileId = "";
                        string fileName = "";

                        if (null != file.Id)
                        {
                            fileId = file.Id;
                            fileName = file.Name;
                            string FJurl = System.Configuration.ConfigurationManager.AppSettings["crmFJUrl"] + "?Id=" +
                                           fileId; //af70b7f6-d37e-49b6-96ca-4d016fc25198
                            xml = xml.Replace("[合同上传]", FJurl);
                            xml = xml.Replace("[附件名称]", "http: " + fileName);

                            feilioahelper.LogMsg(DateTime.Now.ToString() + " 采购合同FJurl: " + FJurl);
                        }
                        else
                        {
                            xml = xml.Replace("[合同上传]", "");
                            xml = xml.Replace("[附件名称]", "");
                        }*/

                        xml = xml.Replace("[MIN]", "");
                        string BJQJ = "";
                        if (BJ == "1")
                        {

                            BJQJ = "报价区间外";
                        }
                        else if (BJ == "2")
                        {
                            BJQJ = "";
                        }
                        else
                        {
                            BJQJ = "报价区间内";
                        }
                        string minstatus = string.Format("select * from SQM_BJ_PSF where vrid='{0}' and minstatus='1'", vrid);
                        DataTable mindt = DataHelper.QueryDataTable(minstatus);
                        if (mindt.Rows.Count > 0)
                        {
                            BJQJ = "报价区间外";
                        }
                        string IFDJFW = "";
                        if (BJQJ == "报价区间外")
                        {
                            IFDJFW = "1";
                        }
                        else if (BJQJ == "报价区间内")
                        {
                            IFDJFW = "0";
                        }
                        string FeeSql = string.Format("select RID from SQM_BJ_PSF where vrid='{0}'", vrid);
                        int NUM = 0;
                        bool FEEUNIT = false;
                        DataTable Feedt = DataHelper.QueryDataTable(FeeSql);
                        if (Feedt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in Feedt.Rows)
                            {
                                string RID = dr["RID"] + "";
                                string ValSql = string.Format(@"select FEEUNIT,count(1) as num
                                                                from(select distinct DJFSRID, GDZRID, FEEUNIT
                                                                  from SQM_MODEBJ_VAL
                                                                 where FEEUNIT is not null
                                                                 and FEECALCID = '{0}'
                                                                 ) A
                                                                 group by FEEUNIT", RID);
                                DataTable Valdt = DataHelper.QueryDataTable(ValSql);
                                if (Valdt.Rows.Count > 0)
                                {
                                    foreach (DataRow VarDr in Valdt.Rows)
                                    {
                                        NUM = Convert.ToInt32(VarDr["NUM"]);
                                        if (NUM > 1)
                                        {
                                            FEEUNIT = true;
                                            break;
                                        }
                                    }
                                }
                                if (FEEUNIT == true)
                                {
                                    break;
                                }

                            }

                        }
                        if (FEEUNIT == true)
                        {
                            BJQJ = "报价区间外";
                            IFDJFW = "1";
                        }


                        xml = xml.Replace("[报价区间]", BJQJ + "");
                        xml = xml.Replace("[产品负责人]", CPFZR);
                        xml = xml.Replace("[申请日期]", DateTime.Now.ToString("yyyy-MM-dd")); //申请日期
                        xml = xml.Replace("[是否超出定价范围]", IFDJFW);
                        xml = xml.Replace("[产品负责人-多]", DCPFZR);
                        xml = xml.Replace("[审批备注]", feilioahelper.GetMemo(vrid) + "");
                        string ISTEST = System.Configuration.ConfigurationManager.AppSettings["Test"];
                        xml = xml.Replace("[是否测试系统]", ISTEST);
                        xml = xml.Replace("[非标包干]", FBBG);
                        xml = xml.Replace("[非标原因]", sbvs.FBREASONNAME);
                        xml = xml.Replace("[非标原因补充说明]", sbvs.FBMEMO);

                        WorkflowServiceXml workflowxml = new WorkflowServiceXml();
                        workflowxml.Url = System.Configuration.ConfigurationManager.AppSettings["ServiceXMLUrl"];
                        feilioahelper.LogMsg("URL: \n" + workflowxml.Url);
                        string requestid = "";
                        requestid = workflowxml.doCreateWorkflowRequest(xml, feilioahelper.GetEXT1(userid));
                        // string sql2 = string.Format("select * from SQM_BJ_VER where MRID='{0}'",rid);
                        //string mrid = DataHelper.QueryValue(sql2).ToString();
                        if (string.IsNullOrEmpty(requestid))
                        {
                            string msgs = "提交流程失败";
                            bool flags = false;
                            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = msgs, Success = flags }));
                        }
                        else
                        {
                            sbvs.REQUESTID = requestid + "";
                            sbvs.STATUS = "1";
                            sbvs.WORKFLOW = "Flowing";
                            sbvs.DF = "0";
                            sbvs.DoUpdate();
                            feilioahelper.LogMsg(DateTime.Now + "----" + FlowName + "\nRequestId" + requestid + "\nId" + sbvs.RID);
                        }
                    }
                    catch (Exception e)
                    {
                        msg = e.Message;
                        flag = false;
                        feilioahelper.LogMsg(DateTime.Now + "," + e);
                    }
                }


                return Content(JsonHelper.GetJsonString(new JsonMessage { Data = "", Message = msg, Success = flag }));
            }
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

        //自定义实体类
        public class PRDOBJ
        {
            public string prdcode;
            public string prdname;
            public string businessorg;
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
        public static List<string> getFeeIns(string feecode, string mrid, string zver)
        {
            List<string> inslist = new List<string>();
            try
            {
                DataTable bjfsdt = null;
                DataTable insdt = new DataTable();
                string[] feecodeArr = feecode.Split(',');
                foreach (string code in feecodeArr)
                {
                    bjfsdt = DataHelper.QueryDataTable(string.Format("select distinct smv.DJFSRID,smv.GDZRID from SQM_MODEBJ_VAL smv left join SQM_BJ_PSF sbp on smv.FEECALCID=sbp.Rid left join SQM_BJ_VER sbv on sbp.VRID=sbv.RID where smv.STATUS='1' and sbv.MRID='{0}' and sbv.ZVER='{1}' and sbp.FEE_CODE='{2}'", mrid, zver, feecode));
                    foreach (DataRow bjfsdr in bjfsdt.Rows)
                    {
                        if (!String.IsNullOrEmpty(bjfsdr["GDZRID"].ToString()))
                        {
                            insdt = DataHelper.QueryDataTable("select distinct INSCODE from SQM_CALC_INS sci left join SQM_FEE_CALC_REF sfcr on sci.CALCCODE=sfcr.CALCCODE where STATUS = '1' and FEECODE = '" + code + "' and GDZRID = '" + bjfsdr["GDZRID"].ToString() + "'");
                        }
                        else
                        {
                            insdt = DataHelper.QueryDataTable("select distinct INSCODE from SQM_CALC_INS sci left join SQM_FEE_CALC_REF sfcr on sci.CALCCODE=sfcr.CALCCODE where STATUS = '1' and FEECODE = '" + code + "' and DJFSRID = '" + bjfsdr["DJFSRID"].ToString() + "'");
                        }
                        foreach (DataRow insdr in insdt.Rows)
                        {
                            inslist.Add(insdr["INSCODE"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return inslist;
        }
        //获取指令集
        public ActionResult ZLJ(string srvcode, string feecodes, string mrid, string zver)
        {
            try
            {
                List<string> inslist = getFeeIns(feecodes, mrid, zver);

                string sql = @"select distinct t1.ins_id, t1.ins_description as insname from mdm_ins t1
                left join mdm_insasn t2
                on t1.ins_id =t2.ins_id
                left join mdm_insset t3
                on t2.insset_id=t3.insset_id
                left join mdm_tsr t4
                on t4.ins_set_id=t3.insset_id
                where t4.srvrqcd121='{0}' ";
                //                string sql = @"--服务指令
                //select distinct t1.ins_id, t1.ins_description as insname from mdm_ins t1
                //left join mdm_insasn t2
                //on t1.ins_id =t2.ins_id
                //left join mdm_insset t3
                //on t2.insset_id=t3.insset_id
                //left join mdm_tsr t4
                //on t4.ins_set_id=t3.insset_id
                //where t4.srvrqcd121='{0}'
                //union 
                //--特殊的指令
                //select distinct t1.inscode,t1.insname from 
                //sqm_calc_ins t1--基础指令关系
                //left join sqm_fee_calc_ref t2--费目基础
                //on t1.calccode=t2.calccode  and t2.status='1'
                //left join sqm_fee_calc t3
                //on t3.feecode=t2.feecode
                //where t3.zlzs='1' and  t3.feecode in ({1})";
                if (inslist.Count > 0)
                {
                    string feestr = "  or t1.ins_id in( ";
                    foreach (string feeins in inslist)
                    {
                        feestr += "'" + feeins + "',";
                    }
                    feestr = feestr.Substring(0, feestr.Length - 1);
                    sql += feestr + " )";
                }
                sql = string.Format(sql, srvcode);
                DataTable zljdt = DataHelper.QueryDataTable(sql);
                return Content(JsonHelper.GetJsonString(zljdt));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //报价删除
        public ActionResult DeleteBj(string keyvalue)
        {
            bool rtnflag = true;
            string rtnmsg = "删除成功";
            string[] staArr = { "1", "2", "3", "4", "5" };
            try
            {
                DataTable staDt = DataHelper.QueryDataTable("select ZVER,STATUS from SQM_BJ_VER where MRID='" + keyvalue + "' order by STATUS desc");
                foreach (DataRow dr in staDt.Rows)
                {
                    if (staArr.Contains(dr["STATUS"].ToString()))
                    {
                        return Content(new JsonMessage { Success = false, Message = "删除失败，存在已提交审批的报价！" }.ToString());
                    }
                    else
                    {
                        string fwa = DataHelper.QueryValue(string.Format("select * from(select FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}' order by CREATETIME desc) where rownum = 1", keyvalue, dr["ZVER"].ToString())) + "";
                        if (!String.IsNullOrEmpty(fwa))
                        {
                            return Content(new JsonMessage { Success = false, Message = "删除失败，存在已提交审批的报价！" }.ToString());
                        }
                    }
                }
                List<string> sqllist = new List<string>();
                sqllist.Add("delete from SQM_BJ_MAIN_BASIC where RID='" + keyvalue + "'");//报价主表
                sqllist.Add("delete from SQM_BJ_VER where MRID='" + keyvalue + "'");//报价版本表
                sqllist.Add("delete from SQM_BJ_PSF where MRID='" + keyvalue + "'");//报价PSF表
                sqllist.Add("delete from SQM_BJ_BP where MRID='" + keyvalue + "'");//BP客户表
                sqllist.Add("delete from SQM_BJ_BIZ where MRID='" + keyvalue + "'");//商机表
                sqllist.Add("delete from SQM_BJ_ORG where MRID='" + keyvalue + "'");//组织表
                string sql = string.Join(";", sqllist.ToArray());
                sql = "begin " + sql + ";end;";
                // 插数
                DataHelper.ExecSql(sql);
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }

        public ActionResult PriceExport(string ExportFlag, string CreateDateS, string CreateDateE)
        {
            bool rtnflag = false;
            if (ExportFlag == "1")
            {
                rtnflag = true;
                string fileName = "报价导出";
                string xlsName = fileName + "-" + System.DateTime.Now.ToString("yyyMMddHHmmss");
                string sql = "";
                if (!string.IsNullOrEmpty(CreateDateS) && !string.IsNullOrEmpty(CreateDateE))
                {
                    sql = @"select 
   B.BJNAME AS 报价名称,
   F.FWA AS 协议号,
   P.ORGNAME AS 报价组织,
   BP.BPNAME AS 客户,
   P.PRODUCT_NAME AS 产品,
   max(V.ZVER) AS 版本,
   CASE  when  F.FWA is not null then '已提交TM'
     when V.STATUS='0' then '已保存'
     when V.STATUS='1' then '审批中'
     when V.STATUS='2' then '审批通过'
     when V.STATUS='3' then '审批退回'
     when V.STATUS='4' then '已发送客户'
     when V.STATUS='5' then '已提交TM'
     when V.STATUS='6' then '作废' end as 状态,
   B.CREATEID AS 销售员,
   B.CREATETIME AS 创建时间
from SQM_BJ_VER V
left join SQM_BJ_MAIN_BASIC B on V.MRID=B.RID
left join SQM_FWA_REF F on V.MRID=F.MRID and V.ZVER=F.ZVER
left join SQM_BJ_PSF P on V.RID=P.VRID
left join SQM_BJ_BP BP on V.MRID=BP.MRID
--where V.MRID='ca946897-a2f3-412c-b1cd-38c3fa436cef'
where (B.CREATETIME >= to_date('" + CreateDateS + @" ','yyyy/MM/dd') and B.CREATETIME <= to_date('" + CreateDateE + @"','yyyy/MM/dd'))
group by B.BJNAME,
   F.FWA,
   P.ORGNAME,
   BP.BPNAME,
   P.PRODUCT_NAME,
   V.STATUS,
   B.CREATEID,
   B.CREATETIME
 order by B.createtime desc";
                }
                else
                {
                    sql = @"select 
   B.BJNAME AS 报价名称,
   F.FWA AS 协议号,
   P.ORGNAME AS 报价组织,
   BP.BPNAME AS 客户,
   P.PRODUCT_NAME AS 产品,
   max(V.ZVER) AS 版本,
   CASE  when  F.FWA is not null then '已提交TM'
     when V.STATUS='0' then '已保存'
     when V.STATUS='1' then '审批中'
     when V.STATUS='2' then '审批通过'
     when V.STATUS='3' then '审批退回'
     when V.STATUS='4' then '已发送客户'
     when V.STATUS='5' then '已提交TM'
     when V.STATUS='6' then '作废' end as 状态,
   B.CREATEID AS 销售员,
   B.CREATETIME AS 创建时间
from SQM_BJ_VER V
left join SQM_BJ_MAIN_BASIC B on V.MRID=B.RID
left join SQM_FWA_REF F on V.MRID=F.MRID and V.ZVER=F.ZVER
left join SQM_BJ_PSF P on V.RID=P.VRID
left join SQM_BJ_BP BP on V.MRID=BP.MRID
--where V.MRID='ca946897-a2f3-412c-b1cd-38c3fa436cef'
group by B.BJNAME,
   F.FWA,
   P.ORGNAME,
   BP.BPNAME,
   P.PRODUCT_NAME,
   V.STATUS,
   B.CREATEID,
   B.CREATETIME
 order by B.createtime desc";
                }

                DataTable forExcelDt = DataHelper.QueryDataTable(sql);
                GetDataHelper.OutFileToDisk(forExcelDt, fileName, Server.MapPath(@"/Excel/tempexcel/") + xlsName + ".xls");
                return Content(new JsonMessage { Success = rtnflag, Message = "/Excel/tempexcel/" + xlsName + ".xls" }.ToString());
            }
            else if (ExportFlag == "2")
            {
                rtnflag = true;
                string fileName = "定价导出";
                string xlsName = fileName + "-" + System.DateTime.Now.ToString("yyyMMddHHmmss");
                string sql = "";
                if (!string.IsNullOrEmpty(CreateDateS) && !string.IsNullOrEmpty(CreateDateE))
                {
                    sql = @"select 
      B.orgname as 组织,
      B.prdname as 产品,
      B.srvname as 服务,      
      B.FEECODE as 费目代码,
      B.FEENAME  as 费目名称,
      A.CURRENCY AS 币别,
      P1.Calccode as CALCNAME1CODE,P1.CALCNAME as CALCNAME1,A.COLUMN1,
      P2.Calccode as CALCNAME2CODE,P2.CALCNAME as CALCNAME2,A.COLUMN2,
      P3.Calccode as CALCNAME3CODE,P3.CALCNAME as CALCNAME3,A.COLUMN3,
      P4.Calccode as CALCNAME4CODE,P4.CALCNAME as CALCNAME4,A.COLUMN4,
      P5.Calccode as CALCNAME5CODE,P5.CALCNAME as CALCNAME5,A.COLUMN5,
      P6.Calccode as CALCNAME6CODE,P6.CALCNAME as CALCNAME6,A.COLUMN6,
      P7.Calccode as CALCNAME7CODE,P7.CALCNAME as CALCNAME7,A.COLUMN7,
      to_char(A.STARTDATE, 'yyyy/mm/dd') as 起始时间,
      to_char(A.ENDDATE, 'yyyy/mm/dd') as 截止时间,
      A.MAXPRICE AS 最高价,
      A.MINPRICE AS 最低价,
      A.GUIDEPRICE AS 指导价指导价,
      A.COSTPRICE AS 采购价,
      A.PURPRICE AS 成本价,
      A.CALCUNIT AS 单位,
      --A.DJFSRID AS 定价方式ID,
      C.Djfsname AS 定价方式,
      --A.GDZRID AS 高低值ID,
      C.Gdzname AS 高低值,
      A.MEMO AS 费用说明
from SQM_MODEDJ_VAL A
left join SQM_DJ_PSF B
     on A.FEECALCID=B.RID
left join SQM_FEE_PUR_REF C on A.Djfsrid=C.Djfsrid and ((A.GDZRID is not null and A.GDZRID=C.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P1 on P1.VALCOL='COLUMN1' and A.DJFSRID=P1.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P1.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P2 on P2.VALCOL='COLUMN2' and A.DJFSRID=P2.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P2.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P3 on P3.VALCOL='COLUMN3' and A.DJFSRID=P3.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P3.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P4 on P4.VALCOL='COLUMN4' and A.DJFSRID=P4.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P4.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P5 on P5.VALCOL='COLUMN5' and A.DJFSRID=P5.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P5.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P6 on P6.VALCOL='COLUMN6' and A.DJFSRID=P6.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P6.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P7 on P7.VALCOL='COLUMN7' and A.DJFSRID=P7.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P7.GDZRID) or (A.GDZRID is null and 1=1))
 where A.STATUS <> '0' and (A.CREATETIME >= to_date('" + CreateDateS + @" ','yyyy/MM/dd') and A.CREATETIME <= to_date('" + CreateDateE + @"','yyyy/MM/dd'))
 ORDER BY A.CREATETIME desc
";
                }
                else
                {
                    sql = @"select 
      B.orgname as 组织,
      B.prdname as 产品,
      B.srvname as 服务,      
      B.FEECODE as 费目代码,
      B.FEENAME  as 费目名称,
      A.CURRENCY AS 币别,
      P1.Calccode as CALCNAME1CODE,P1.CALCNAME as CALCNAME1,A.COLUMN1,
      P2.Calccode as CALCNAME2CODE,P2.CALCNAME as CALCNAME2,A.COLUMN2,
      P3.Calccode as CALCNAME3CODE,P3.CALCNAME as CALCNAME3,A.COLUMN3,
      P4.Calccode as CALCNAME4CODE,P4.CALCNAME as CALCNAME4,A.COLUMN4,
      P5.Calccode as CALCNAME5CODE,P5.CALCNAME as CALCNAME5,A.COLUMN5,
      P6.Calccode as CALCNAME6CODE,P6.CALCNAME as CALCNAME6,A.COLUMN6,
      P7.Calccode as CALCNAME7CODE,P7.CALCNAME as CALCNAME7,A.COLUMN7,
      to_char(A.STARTDATE, 'yyyy/mm/dd') as 起始时间,
      to_char(A.ENDDATE, 'yyyy/mm/dd') as 截止时间,
      A.MAXPRICE AS 最高价,
      A.MINPRICE AS 最低价,
      A.GUIDEPRICE AS 指导价指导价,
      A.COSTPRICE AS 采购价,
      A.PURPRICE AS 成本价,
      A.CALCUNIT AS 单位,
      --A.DJFSRID AS 定价方式ID,
      C.Djfsname AS 定价方式,
      --A.GDZRID AS 高低值ID,
      C.Gdzname AS 高低值,
      A.MEMO AS 费用说明
from SQM_MODEDJ_VAL A
left join SQM_DJ_PSF B
     on A.FEECALCID=B.RID
left join SQM_FEE_PUR_REF C on A.Djfsrid=C.Djfsrid and ((A.GDZRID is not null and A.GDZRID=C.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P1 on P1.VALCOL='COLUMN1' and A.DJFSRID=P1.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P1.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P2 on P2.VALCOL='COLUMN2' and A.DJFSRID=P2.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P2.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P3 on P3.VALCOL='COLUMN3' and A.DJFSRID=P3.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P3.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P4 on P4.VALCOL='COLUMN4' and A.DJFSRID=P4.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P4.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P5 on P5.VALCOL='COLUMN5' and A.DJFSRID=P5.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P5.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P6 on P6.VALCOL='COLUMN6' and A.DJFSRID=P6.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P6.GDZRID) or (A.GDZRID is null and 1=1))
left join SQM_FEE_CALC_REF P7 on P7.VALCOL='COLUMN7' and A.DJFSRID=P7.DJFSRID and ((A.GDZRID is not null and A.GDZRID=P7.GDZRID) or (A.GDZRID is null and 1=1))
 where A.STATUS <> '0'
 ORDER BY A.CREATETIME desc
";
                }

                DataTable forExcelDt = DataHelper.QueryDataTable(sql);
                GetDataHelper.OutFileToDisk(forExcelDt, fileName, Server.MapPath(@"/Excel/tempexcel/") + xlsName + ".xls");
                return Content(new JsonMessage { Success = rtnflag, Message = "/Excel/tempexcel/" + xlsName + ".xls" }.ToString());
            }
            else if (ExportFlag == "3")
            {
                rtnflag = true;
                string fileName = "统计导出";
                string sql = "";
                string xlsName = fileName + "-" + System.DateTime.Now.ToString("yyyMMddHHmmss");
                if (!string.IsNullOrEmpty(CreateDateS) && !string.IsNullOrEmpty(CreateDateE))
                {
                    sql = @"select 
case when ACTIONNAME='QM_PriceIndex' then '报价主页'
 when ACTIONNAME='QM_PriceEdit' then '报价详细页'
 when ACTIONNAME='FMBJ' then '费目报价页' end as 页面,
CREATEUSER AS 工号，CREATETIME AS 时间 from SQM_TRACKER t where (CREATETIME >= to_date('" + CreateDateS + " ','yyyy/MM/dd') and CREATETIME <= to_date('" + CreateDateE + "','yyyy/MM/dd')) order by CREATETIME desc";
                }
                else
                {
                    sql = @"select 
case when ACTIONNAME='QM_PriceIndex' then '报价主页'
 when ACTIONNAME='QM_PriceEdit' then '报价详细页'
 when ACTIONNAME='FMBJ' then '费目报价页' end as 页面,
CREATEUSER AS 工号，CREATETIME AS 时间 from SQM_TRACKER t order by CREATETIME desc";
                }

                DataTable forExcelDt = DataHelper.QueryDataTable(sql);
                GetDataHelper.OutFileToDisk(forExcelDt, fileName, Server.MapPath(@"/Excel/tempexcel/") + xlsName + ".xls");
                return Content(new JsonMessage { Success = rtnflag, Message = "/Excel/tempexcel/" + xlsName + ".xls" }.ToString());
            }

            return Content(new JsonMessage { Success = rtnflag, Message = "" }.ToString());
        }
        /// <summary>
        /// 判断同一个产品下是否存在同一费目即是包干费又是报价费目
        /// </summary>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public ActionResult BGFandBJFM(string vrid)
        {
            bool success = false;
            List<string> data = new List<string>();
            string sql_bgf = string.Format("select distinct product_code,product_name,service_code,service_name,fee_code,fee_name,vrid from sqm_bj_psf where bgfzrid <> '1' and bgfzrid is not null and vrid = '{0}'", vrid);
            string sql_bj = string.Format("select distinct product_code,product_name,service_code,service_name,fee_code,fee_name,vrid from sqm_bj_psf where (bgfzrid = '1' or bgfzrid is null) and vrid = '{0}'", vrid);
            DataTable bgf = DataHelper.QueryDataTable(sql_bgf);// 该版本所有包干费，去重
            DataTable bj = DataHelper.QueryDataTable(sql_bj);// 该版本所有报价费目（不包含包干费）
            if (bgf.Rows.Count > 0)
            {
                foreach (DataRow dr in bj.Rows)
                {
                    string product_code = dr["PRODUCT_CODE"] + "";
                    string service_code = dr["SERVICE_CODE"] + "";
                    string fee_code = dr["FEE_CODE"] + "";
                    DataRow[] drs = bgf.Select(string.Format("PRODUCT_CODE = '{0}' and SERVICE_CODE = '{1}' and FEE_CODE = '{2}'", product_code, service_code, fee_code));//同一服务下报价费目与包干费重复
                    if (drs.Length > 0)
                    {
                        success = true;
                        data.Add("产品：" + product_code + "--服务：" + service_code + "--费目：" + fee_code);
                    }
                }
            }
            return Content(new JsonMessage { Success = success, Data = data }.ToString());
        }
    }
}

