using Aim;
using Aim.Data;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.QDM;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.OracleClient;
using Oncontrol3.Web.RATE601;
using Oncontrol3.Web.FWA701;
using Oncontrol3.Web.FWA702;
using Oncontrol3.Web.FWA703;
using Com.Feiliks.QDM.Model;
using Newtonsoft.Json;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class QM_FBPriceController : BaseController
    {

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

        static readonly List<string> A2S_PRDS = new List<string>() { "AA12", "AA13", "AA14" };  // 这些空运产品特殊处理：协议号走供应链规则，逻辑仍然走空运

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
                //GetContractcode(keyvalue, zver);
                //bsbmt = !string.IsNullOrEmpty(DataHelper.QueryValue(string.Format("SELECT CONTRSCTNUM FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' AND STATUS = '4'", keyvalue, zver)) + "");
                //if (!bsbmt)
                //{
                //    rtnmsg = "具有有效合同才能提交TM";
                //    rtnflag = false;
                //    goto rtnLabel;
                //}

                #region 原来校验合同是否存在变更
                //获取crm销售合同编号
                //GetContractcode(keyvalue, zver);
                //bsbmt = !string.IsNullOrEmpty(DataHelper.QueryValue(string.Format("SELECT CONTRSCTNUM FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}' AND STATUS = '4'", keyvalue, zver)) + "");
                //if (!bsbmt)
                //{
                //    rtnmsg = "具有有效合同才能提交TM";
                //    rtnflag = false;
                //    goto rtnLabel;
                //}

                //改由接口校验

                string jyht = @"select bp.bpcode,
                                       bp.bpname,
                                       biz.bizname,
                                       biz.bizid,
                                       org.orgname,
                                       org.orgcode,
                                       v.dtfrom,
                                       v.dtto
                          from sqm_bj_main_basic t
                          join sqm_bj_bp bp
                            on t.rid = bp.mrid
                          join sqm_bj_biz biz
                            on t.rid = biz.mrid
                          join sqm_bj_org org
                            on t.rid = org.mrid
                          join sqm_bj_ver v
                            on t.rid = v.mrid
                         where v.mrid='" + keyvalue + "' and upper(v.zver)='" + zver + "'";
                DataTable datajyht = DataHelper.QueryDataTable(jyht);//报价产品代码首字母

                if (datajyht.Rows.Count > 0)
                {
                    for (int i = 0; i < datajyht.Rows.Count; i++)
                    {
                        DateTime jyht_start = DateTime.Parse(datajyht.Rows[i]["DTFROM"] + "");
                        DateTime jyht_end = DateTime.Parse(datajyht.Rows[i]["DTTO"] + "");
                        bool isyxht = XsyHelper.VididateHt(datajyht.Rows[i]["BPCODE"].ToString(), datajyht.Rows[i]["ORGCODE"].ToString(), datajyht.Rows[i]["BIZID"].ToString(), jyht_start, jyht_end);
                        if (!isyxht)
                        {
                            rtnmsg = "报价周期不在合同周期范围内,请检查销售易合同周期与报价的周期是否一致";
                            rtnflag = false;
                            goto rtnLabel;
                        }
                    }

                }
                else
                {
                    rtnmsg = "具有有效合同才能提交TM";
                    rtnflag = false;
                    goto rtnLabel;
                }

            #endregion

            A2SLabel:

                //首先查询出版本的rid
                var vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver));
                //psf表信息
                sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' ORDER BY PRODUCT_CODE ", vrid);


                string sybsql = string.Format("select distinct substr(PRODUCT_CODE, 0, 1) syb from  ( SELECT PRODUCT_CODE FROM SQM_BJ_PSF WHERE VRID = '{0}' ORDER BY PRODUCT_CODE)", vrid);
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
                    fwa.ZBJLX = "02";//报价类型 01 标准报价，02 非标报价

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

                                //非标的服务不传
                                //if (fwa.FAGTYPEID103 == "Z101")
                                //{
                                //    foreach (DataRow dr in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                                //    {
                                //        Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM fag_item702 = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEM();
                                //        fag_item100702.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                                //        fag_item100702.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                                //        fag_item702.ACTION = ACTION_C;
                                //        prdcode = dr["PRODUCT_CODE"] + "";
                                //        srvcode = dr["SERVICE_CODE"] + "";

                                //        //fag_item.SERVICE_PRODUCT_ID = prdcode;//服务产品 报价的产品代码（供应链不传）
                                //        fag_item702.SERVICE_TYPE = srvcode;//服务类型   报价的服务代码
                                //        fag_item702.PAR_KEY = strHex32;// fag_item100.KEY;//上层ITEM KEY    即FAG_ITEM的KEY

                                //        List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL> ins_list = new List<Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL>();
                                //        string sqlins = string.Format("SELECT INS_ID FROM MDM_INSASN WHERE INSSET_ID IN ( SELECT INS_SET_ID FROM MDM_TSR WHERE SRVRQCD121 = '{0}' ) ", srvcode);
                                //        int seq = 100;
                                //        DataTable dtins = DataHelper.QueryDataTable(sqlins);
                                //        if (null != dtins && dtins.Rows.Count > 0)
                                //        {
                                //            foreach (DataRow drins in dtins.Rows)
                                //            {
                                //                seq += 10;
                                //                Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL();
                                //                ins.ACTION = ACTION_C;
                                //                ins.SEQ_NUMBER = seq.ToString();
                                //                ins.INS_ID = drins["INS_ID"] + "";
                                //                ins_list.Add(ins);
                                //            }
                                //        }
                                //        foreach (DataRow drps in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'" + " AND " + "SERVICE_CODE = '" + srvcode + "'"))
                                //        {
                                //            List<string> feeins = getFeeIns(drps["FEE_CODE"] + "", keyvalue, zver);
                                //            if (null != feeins && feeins.Count > 0)
                                //            {
                                //                foreach (string fins in feeins)
                                //                {
                                //                    seq += 10;
                                //                    Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_MODIFYIT_FWAFAG_ITEMINS_DETAIL();
                                //                    ins.ACTION = ACTION_C;
                                //                    ins.SEQ_NUMBER = seq.ToString();
                                //                    ins.INS_ID = fins;
                                //                    ins_list.Add(ins);
                                //                }
                                //            }
                                //        }
                                //        if (ins_list.Count > 0)
                                //        {
                                //            fag_item702.INS_DETAIL = ins_list.ToArray();
                                //        }

                                //        fag_itemlist702.Add(fag_item702);
                                //    }
                                //}

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
                                                tccs_item.CURRCODE016 = string.IsNullOrEmpty(drpsf["BJTCURR"] + "") ? "CNY" : drpsf["BJTCURR"] + "";//"CNY";//货币 币种

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
                                                            orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM1 = scale_item1;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 2:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM2 = scale_item2;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 3:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM3 = scale_item3;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 4:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM4 = scale_item4;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 5:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM5 = scale_item5;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 6:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM6 = scale_item6;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 7:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM7 = scale_item7;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 8:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM8 = scale_item8;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 9:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM9 = scale_item9;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 10:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM10 = scale_item10;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 11:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM11 = scale_item11;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 12:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM12 = scale_item12;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 13:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                rates_dim.SCALE_ITEM13 = scale_item13;
                                                                                //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                break;
                                                                            case 14:
                                                                                Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                                    tccs_itemgdz.CURRCODE016 = string.IsNullOrEmpty(drpsf["BJTCURR"] + "") ? "CNY" : drpsf["BJTCURR"] + "";//"CNY";//货币 币种

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
                                                                orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                                                                    scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                                    scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM1 = scale_item1;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 2:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                                    scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                                    scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM2 = scale_item2;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 3:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                                    scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                                    scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM3 = scale_item3;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 4:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                                    scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                                    scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM4 = scale_item4;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 5:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                                    scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                                    scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM5 = scale_item5;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 6:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                                    scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                                    scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM6 = scale_item6;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 7:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                                    scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                                    scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM7 = scale_item7;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 8:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                                    scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                                    scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM8 = scale_item8;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 9:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                                    scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                                    scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM9 = scale_item9;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 10:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                                    scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                                    scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM10 = scale_item10;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 11:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                                    scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                                    scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM11 = scale_item11;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 12:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                                    scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                                    scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM12 = scale_item12;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 13:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                                    scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                                    scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                                    scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                                    rates_dim.SCALE_ITEM13 = scale_item13;
                                                                                    //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                                    rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                                    break;
                                                                                case 14:
                                                                                    Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                                    scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                    orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                            #region 20191112 Rank Update
                            //SQM_FWA_REF sqm_fwa_ref702 = new SQM_FWA_REF();
                            //sqm_fwa_ref702.MRID = keyvalue;
                            //sqm_fwa_ref702.ZVER = zver;
                            //sqm_fwa_ref702.FWA = fwa702.FAGRMNTID044;
                            //sqm_fwa_ref702.CREATEUSER = SQMHelper.getStaffKey();
                            //sqm_fwa_ref702.DoCreate();
                            //fwamsg += "修改协议成功：" + fwa702.FAGRMNTID044 + "<br>";
                            Z2FM_SQ_FWA_MODIFY_RESET_FWA[] res702 = resfwa702.ET_FWA;
                            //写入ItemNO
                            string item_no_modifylist = "";
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
                            item_no_modifylist = item_no_modifylist.TrimEnd(',');
                            SQM_FWA_REF sqm_fwa_ref702 = new SQM_FWA_REF();
                            sqm_fwa_ref702.MRID = keyvalue;
                            sqm_fwa_ref702.ZVER = zver;
                            sqm_fwa_ref702.FWA = fwa702.FAGRMNTID044;
                            sqm_fwa_ref702.CREATEUSER = SQMHelper.getStaffKey();
                            sqm_fwa_ref702.ITEMNO = item_no_modifylist;
                            sqm_fwa_ref702.DoCreate();
                            fwamsg += "修改协议成功：" + fwa702.FAGRMNTID044 + "<br>";
                            #endregion

                            var mainobj = SQM_BJ_MAIN_BASIC.TryFind(keyvalue);//报价主数据信息
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
                                _ubody.lockStatus = "2";
                                _ubody.id = mainobj.XSYBJID;
                                _ubody.customItem3__c = zver;//报价版本 //测试
                                _ubody.customItem4__c = InterfaceFormat.FormatStatusXSY("5", "Z");//报价状态
                                ubody[0] = _ubody;
                                _ubodys.data = ubody;
                                ubodys[0] = _ubodys;
                                uwb.CallUpdateQuotation(uhead, ubodys);
                                #endregion
                            }
                            else
                            {
                                #region  已提交 报价接口 
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
                                _body.lockStatus = "2";
                                //_body.quotationTitle = "";报价名称
                                _body.customItem3__c = zver;//报价版本 
                                _body.customItem4__c = InterfaceFormat.FormatStatusXSY("5", "Z");//报价状态
                                _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + keyvalue + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion=" + zver;//报价地址//报价地址
                                _body.customItem6__c = orgobj.ORGCODE;//mainobj.BJNAME;//操作组织 
                                _body.customItem7__c = keyvalue;//mainobj.BJNAME;//报价ID
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
                            continue;
                            //return Content(new JsonMessage { Success = true, Data = null, Code = "1", Message = "修改协议成功" }.ToString());
                        }
                        catch (Exception ex702)
                        {
                            return Content(new JsonMessage { Success = false, Data = null, Code = "1", Message = ex702.Message }.ToString());
                        }
                    }
                    #region 20191112 Rank Update
                    strFWAFAGRMNTID044 += DateTime.Now.ToString("yyyy").Substring(2, 2);
                    //SQM_FWA_REF sqm_fwa_ref = new SQM_FWA_REF();
                    //sqm_fwa_ref.MRID = keyvalue;
                    //sqm_fwa_ref.ZVER = zver;
                    //sqm_fwa_ref.FWA = SQMTMInterface.GenerateFWASerial(strFWAFAGRMNTID044);
                    //sqm_fwa_ref.CREATEUSER = SQMHelper.getStaffKey();
                    //sqm_fwa_ref.DoCreate();
                    //fwafagrmntid044List.Clear();
                    //fwafagrmntid044List.Add(sqm_fwa_ref.FWA);
                    //fwa.FAGRMNTID044 = sqm_fwa_ref.FWA;
                    string bj_creatFwa = SQMTMInterface.GenerateFWASerial(strFWAFAGRMNTID044);
                    fwafagrmntid044List.Clear();
                    fwafagrmntid044List.Add(bj_creatFwa);//(sqm_fwa_ref.FWA);
                    fwa.FAGRMNTID044 = bj_creatFwa;//sqm_fwa_ref.FWA;
                    #endregion
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

                        //非标的服务不传
                        //if (fwa.FAGTYPEID103 == "Z101")
                        //{
                        //    foreach (DataRow dr in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'"))
                        //    {
                        //        Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM fag_item = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEM();
                        //        fag_item100.VALIDITY_START = DateTime.Parse(dtBJBV.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");
                        //        fag_item100.VALIDITY_END = DateTime.Parse(dtBJBV.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");
                        //        prdcode = dr["PRODUCT_CODE"] + "";
                        //        srvcode = dr["SERVICE_CODE"] + "";

                        //        //fag_item.SERVICE_PRODUCT_ID = prdcode;//服务产品 报价的产品代码（供应链不传）
                        //        fag_item.SERVICE_TYPE = srvcode;//服务类型   报价的服务代码
                        //        fag_item.PAR_KEY = strHex32;// fag_item100.KEY;//上层ITEM KEY    即FAG_ITEM的KEY

                        //        List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL> ins_list = new List<Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL>();
                        //        string sqlins = string.Format("SELECT INS_ID FROM MDM_INSASN WHERE INSSET_ID IN ( SELECT INS_SET_ID FROM MDM_TSR WHERE SRVRQCD121 = '{0}' ) ", srvcode);
                        //        int seq = 100;
                        //        DataTable dtins = DataHelper.QueryDataTable(sqlins);
                        //        if (null != dtins && dtins.Rows.Count > 0)
                        //        {
                        //            foreach (DataRow drins in dtins.Rows)
                        //            {
                        //                seq += 10;
                        //                Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL();
                        //                ins.SEQ_NUMBER = seq.ToString();
                        //                ins.INS_ID = drins["INS_ID"] + "";
                        //                ins_list.Add(ins);
                        //            }
                        //        }
                        //        foreach (DataRow drps in dtpsf.Select("PRODUCT_CODE = '" + product_code + "'" + " AND " + "SERVICE_CODE = '" + srvcode + "'"))
                        //        {
                        //            List<string> feeins = getFeeIns(drps["FEE_CODE"] + "", keyvalue, zver);
                        //            if (null != feeins && feeins.Count > 0)
                        //            {
                        //                foreach (string fins in feeins)
                        //                {
                        //                    seq += 10;
                        //                    Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL ins = new Z2FM_SQ_FWA_CREATEIT_FWAFAG_ITEMINS_DETAIL();
                        //                    ins.SEQ_NUMBER = seq.ToString();
                        //                    ins.INS_ID = fins;
                        //                    ins_list.Add(ins);
                        //                }
                        //            }
                        //        }
                        //        if (ins_list.Count > 0)
                        //        {
                        //            fag_item.INS_DETAIL = ins_list.ToArray();
                        //        }

                        //        fag_itemlist.Add(fag_item);
                        //    }
                        //}

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

                            bool islsc = drpsf["ISLSC"] + "" == "1";//是否包干费
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
                                        tccs_item.CURRCODE016 = string.IsNullOrEmpty(drpsf["BJTCURR"] + "") ? "CNY" : drpsf["BJTCURR"] + "";// "CNY";//货币 币种

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
                                                    orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                                                        scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                        scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM1 = scale_item1;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 2:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                        scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                        scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM2 = scale_item2;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 3:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                        scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                        scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM3 = scale_item3;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 4:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                        scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                        scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM4 = scale_item4;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 5:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                        scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                        scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM5 = scale_item5;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 6:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                        scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                        scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM6 = scale_item6;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 7:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                        scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                        scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM7 = scale_item7;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 8:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                        scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                        scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM8 = scale_item8;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 9:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                        scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                        scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM9 = scale_item9;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 10:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                        scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                        scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM10 = scale_item10;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 11:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                        scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                        scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM11 = scale_item11;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 12:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                        scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                        scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM12 = scale_item12;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 13:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                        scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                        scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                        scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                        rates_dim.SCALE_ITEM13 = scale_item13;
                                                                        //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                        rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                        break;
                                                                    case 14:
                                                                        Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                        scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                        orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                            tccs_itemgdz.CURRCODE016 = string.IsNullOrEmpty(drpsf["BJTCURR"] + "") ? "CNY" : drpsf["BJTCURR"] + "";//"CNY";//货币 币种

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
                                                        orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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
                                                                            scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item1.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item1.SCALE_ITEM);
                                                                            scale_item1.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM1 = scale_item1;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 2:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2 scale_item2 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM2();
                                                                            scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item2.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item2.SCALE_ITEM);
                                                                            scale_item2.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM2 = scale_item2;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 3:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3 scale_item3 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM3();
                                                                            scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item3.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item3.SCALE_ITEM);
                                                                            scale_item3.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM3 = scale_item3;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 4:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4 scale_item4 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM4();
                                                                            scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item4.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item4.SCALE_ITEM);
                                                                            scale_item4.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM4 = scale_item4;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 5:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5 scale_item5 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM5();
                                                                            scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item5.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item5.SCALE_ITEM);
                                                                            scale_item5.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM5 = scale_item5;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 6:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6 scale_item6 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM6();
                                                                            scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item6.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item6.SCALE_ITEM);
                                                                            scale_item6.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM6 = scale_item6;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 7:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7 scale_item7 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM7();
                                                                            scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item7.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item7.SCALE_ITEM);
                                                                            scale_item7.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM7 = scale_item7;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 8:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8 scale_item8 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM8();
                                                                            scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item8.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item8.SCALE_ITEM);
                                                                            scale_item8.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM8 = scale_item8;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 9:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9 scale_item9 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM9();
                                                                            scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item9.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item9.SCALE_ITEM);
                                                                            scale_item9.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM9 = scale_item9;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 10:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10 scale_item10 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM10();
                                                                            scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item10.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item10.SCALE_ITEM);
                                                                            scale_item10.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM10 = scale_item10;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 11:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11 scale_item11 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM11();
                                                                            scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item11.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item11.SCALE_ITEM);
                                                                            scale_item11.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM11 = scale_item11;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 12:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12 scale_item12 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM12();
                                                                            scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item12.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item12.SCALE_ITEM);
                                                                            scale_item12.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM12 = scale_item12;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 13:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13 scale_item13 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM13();
                                                                            scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
                                                                            scale_item13.SCALE_ITEM_RAW = sqmtminterface.getDBKEY(ccoderd, scale_item13.SCALE_ITEM);
                                                                            scale_item13.CALC_TYP = sqmtminterface.getCACL_TYP(drval[ccoderd + "SCALE"] + "", drval["CALCTYPE"] + "");
                                                                            rates_dim.SCALE_ITEM13 = scale_item13;
                                                                            //rates_dim.ZERO_RATE = drval["BJPRICE"] + "";//零费率
                                                                            rates_dim.RATE = drval["BJPRICE"] + "";//金额

                                                                            break;
                                                                        case 14:
                                                                            Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14 scale_item14 = new Z2FM_SQ_RATE_CREATEIT_RATEVALIDITYRATES_DIMSCALE_ITEM14();
                                                                            scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item1.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item2.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item3.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item4.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item5.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item6.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item7.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item8.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item9.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item10.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item11.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item12.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item13.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                                                scale_item14.SCALE_ITEM = drval[ccoderd + "CODE"] + "";//标度值字符
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
                                                            orgdata.ORG_UNIT = contractorslist[0].ORG_UNIT;  //代运组织（根据产品事业部）
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

                    #region 20191112 Rank Update
                    string item_nolist = "";
                    if (resfwa.ET_MSG != null)
                    {
                        Z2FM_SQ_FWA_CREATE_RESET_FWA[] res = resfwa.ET_FWA;
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
                    }
                    item_nolist = item_nolist.TrimEnd(',');
                    SQM_FWA_REF sqm_fwa_ref = new SQM_FWA_REF();
                    sqm_fwa_ref.MRID = keyvalue;
                    sqm_fwa_ref.ZVER = zver;
                    sqm_fwa_ref.FWA = bj_creatFwa;//SQMTMInterface.GenerateFWASerial(strFWAFAGRMNTID044);
                    sqm_fwa_ref.CREATEUSER = SQMHelper.getStaffKey();
                    sqm_fwa_ref.ITEMNO = item_nolist;
                    sqm_fwa_ref.DoCreate();
                    #endregion
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
                    var mainobj1 = SQM_BJ_MAIN_BASIC.TryFind(keyvalue);//报价主数据信息
                                                                       //不创建商机
                    if (!string.IsNullOrEmpty(mainobj1.XSYBJID))
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
                        _ubody.lockStatus = "2";
                        _ubody.id = mainobj1.XSYBJID;
                        _ubody.customItem3__c = zver;//报价版本 //测试
                        _ubody.customItem4__c = InterfaceFormat.FormatStatusXSY("5", "Z");//报价状态
                        ubody[0] = _ubody;
                        _ubodys.data = ubody;
                        ubodys[0] = _ubodys;
                        uwb.CallUpdateQuotation(uhead, ubodys);
                        #endregion
                    }
                    else
                    {
                        #region  已提交 报价接口 
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
                        _body.lockStatus = "2";
                        //_body.quotationTitle = "";报价名称
                        _body.customItem3__c = zver;//报价版本 
                        _body.customItem4__c = InterfaceFormat.FormatStatusXSY("5", "Z");//报价状态
                        _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + keyvalue + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion=" + zver;//报价地址
                        _body.customItem6__c = orgobj.ORGCODE;//mainobj.BJNAME;//操作组织 
                        _body.customItem7__c = keyvalue;//mainobj.BJNAME;//报价ID
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

                        mainobj1.XSYBJID = xsybjid;
                        mainobj1.DoUpdate();
                        #endregion
                    }
                }

                removeFailedFWA(fwafagrmntid044List, keyvalue, zver);
                return Content(new JsonMessage { Success = rtnflag, Data = null, Code = "1", Message = rtnmsg }.ToString());
            }
            catch (Exception ex)
            {
                removeFailedFWA(fwafagrmntid044List, keyvalue, zver);
                return Content(new JsonMessage { Success = false, Data = null, Code = "-1", Message = ex.Message }.ToString());
            }
        }
        public long DateToTicks(DateTime? time)
        {
            return ((time.HasValue ? time.Value.Ticks : DateTime.Parse("1990-01-01").Ticks) - 621355968000000000) / 10000;
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

    }
}

