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
using Com.Feiliks.QDM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.OracleClient;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class QM_SJBJController : BaseController
    {
        [AllowAnonymous]
        public ActionResult QM_SJBJIndex()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }

        [AllowAnonymous]
        public ActionResult QM_SJBJEdit()
        {
            return View();
        }
        /// <summary>
        /// 得到当前账号对应的所有客户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [AllowAnonymous]

        public ActionResult getcustomer(string loginname)
        {
            string data = "";
            BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            try
            {
                data = client.BJ("客户", loginname, "");
            }
            catch (Exception e)
            {

            }
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 主表主键拿到商机客户和组织
        /// </summary>
        /// <param name="mrid"></param>
        /// <returns></returns>
        public ActionResult showAllData(string mrid, string zversion)
        {
            string rtnmsg = "返回成功";
            bool flag = true;
            var data = "";
            try
            {
                string sql = string.Format("select sqm_bj_main_basic.rid,sqm_bj_main_basic.dtto,sqm_bj_main_basic.dtfrom,sqm_bj_main_basic.bjtcurr,sqm_bj_main_basic.memo,sqm_bj_main_basic.bjname,sqm_bj_bp.bpname,sqm_bj_bp.bpcode,sqm_bj_biz.bizname,sqm_bj_biz.bizid,sqm_bj_org.orgname,sqm_bj_org.orgcode,sqm_bj_ver.bpcode9 from sqm_bj_main_basic left join sqm_bj_biz on sqm_bj_main_basic.rid=sqm_bj_biz.mrid left join sqm_bj_bp on sqm_bj_main_basic.rid=sqm_bj_bp.mrid left join sqm_bj_org on sqm_bj_main_basic.rid=sqm_bj_org.mrid left join sqm_bj_ver on sqm_bj_main_basic.rid=sqm_bj_ver.mrid and sqm_bj_ver.zver='{1}' where sqm_bj_main_basic.rid = '{0}'", mrid, zversion);
                data = JsonHelper.GetJsonString(DataHelper.QueryDictList(sql));
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = data, Message = rtnmsg, Success = flag }));
        }

        /// <summary>
        /// 通过选中的客户查出对应的所有商机
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="customerid">客户no customerno</param>
        /// <returns></returns>
        public ActionResult getbusiness(string loginname, string customerid)
        {
            //不允许选择不同组织下的商机；
            //BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            //CRM 数据库查询商机
            string sql = string.Format("SELECT * FROM CRM_BUSINESS WHERE BUSINESSFOLLOWUPID = (SELECT USERID FROM SYSUSER WHERE SYSUSER.LOGINNAME='{0}')  AND CUSTOMERNO = '{1}'", loginname, customerid);
            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var dt = DataHelper.QueryDataTable(sql, conn);
            return Content(JsonHelper.GetJsonString(dt));
        }
        public ActionResult GetContrsctDate(string businessid, string customerno)
        {
            string sql = string.Format("select CONTRACTCODE,CONTRACTSTARTDATE,CONTRACTENDDATE from CRM_SALESCONTRACT where BUSINESSID like '%{0}%' and CUSTOMERNO='{1}' order by createtime desc", businessid, customerno);
            IDbConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable data = DataHelper.QueryDataTable(sql, conn);

            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult getBpcode9(string loginname, string customerid, string mrid, string busArray)
        {
            string sql = "";
            string gylnum = "0";
            DataTable bpdt = null;
            try
            {
                if (!String.IsNullOrEmpty(mrid))
                {
                    string countnum = DataHelper.QueryValue(string.Format("select count(1) as countnum from  SQM_BJ_VER v left join SQM_BJ_PSF p on v.RID=p.VRID left join SQM_PRD_EXT e on p.PRODUCT_CODE =e.PRODUCTKEY where v.MRID='{0}' and e.businessorg='供应链'", mrid)) + "";
                    if (countnum != "0")
                    {
                        gylnum = "1";
                    }
                }
                if (gylnum == "0")
                {
                    //CRM 数据库查询商机
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string bizstr = "";
                    List<businessClass> bl = JsonHelper.GetObject<List<businessClass>>(busArray);
                    for (int j = 0; j < bl.Count; j++)
                    {
                        bizstr += "'" + bl[j].buscode + "',";
                    }
                    sql = string.Format("SELECT CRM_PRODUCT.DIVISION FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE ID IN ({0}))", bizstr.TrimEnd(','));
                    var prddt = DataHelper.QueryDataTable(sql, conn);
                    //判断客户下面是否存在供应链产品
                    foreach (DataRow dr in prddt.Rows)
                    {
                        if (dr["DIVISION"].ToString() == "供应链")
                        {
                            gylnum = "1";
                            break;
                        }
                    }
                }
                //获取该客户下的9位码
                if (gylnum == "1" && !String.IsNullOrEmpty(customerid))
                {
                    bpdt = DataHelper.QueryDataTable("select BPKEY from MDM_BP where BPKEY like '" + customerid + "%' and BPKEY<>'" + customerid + "'");
                }
            }
            catch (Exception)
            {
                throw;
            }
            object[] data = { gylnum, bpdt };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 获取币种
        /// </summary>
        /// <returns></returns>
        public ActionResult Tuccr()
        {
            DataTable BZdt = new DataTable();
            try {
                BZdt = DataHelper.QueryDataTable("select WAERS AS NAME,KTEXT AS VALUE from MDM_WAERS");
            }
            catch (Exception)
            {
                throw;
            }
            object[] data = { BZdt };
            return Content(JsonHelper.GetJsonString(data));
        }
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
        public ActionResult SaveSJBJ(string keyvalue, string zversion, string cuscode, string cusname, string busArray, string orgcode, string orgname, string priceName, string dtfrom, string dtto, string contrsctnum, string memo, string bpcode9,string WAERS)
        {
            var flag = true;
            var rtnmessga = "保存成功";
            string code = "1";
            string vrid = "";
            try
            {
                // 保存时校验所选商机带出来的产品是否与当前报价所选产品匹配（商机产品包含所选产品）
                // 查询当前报价所选产品(勾选产品)
                DataTable dtprdcode = DataHelper.QueryDataTable("select distinct product_code from sqm_bj_psf where vrid = (select rid from sqm_bj_ver where mrid = '" + keyvalue + "' and zver = '" + zversion + "')");//choosestatus = '1' and
                // 得到商机
                string bizstr = "";
                List<businessClass> b = JsonHelper.GetObject<List<businessClass>>(busArray);
                for (int j = 0; j < b.Count; j++)
                {
                    bizstr += "'" + b[j].buscode + "',";
                }

                //var sql = string.Format("SELECT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,CRM_BUS_PRODUCTINFO.PRODUCT_NAME,CRM_PRODUCT.PRODUCTSNAME,CRM_PRODUCT.PRODUCTDESCRIPTION FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE FOLLOWUPSTATUS = '跟进中' AND ID IN ({0}))", bizstr.TrimEnd(','));
                // 根据商机带出产品
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var sql = string.Format(@"SELECT DISTINCT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,
CRM_BUS_PRODUCTINFO.PRODUCT_NAME,
CRM_PRODUCT.PRODUCTSNAME,
CRM_PRODUCT.PRODUCTDESCRIPTION 
FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID 
WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE ID IN ({0}))", bizstr.TrimEnd(','));
                DataTable dtprdsj = DataHelper.QueryDataTable(sql, conn);
                if (dtprdsj.Rows.Count > 0)
                {
                    //foreach (DataRow drsj in dtprdsj.Rows)
                    //{
                    //    string sjprdcode = drsj["PRODUCTDESCRIPTION"] + "";
                    //for (int i = dtprdcode.Rows.Count - 1; i >= 0; i--)
                    //{
                    //    string prdcode = dtprdcode.Rows[i]["PRODUCT_CODE"] + "";


                    //if (sjprdcode == prdcode)
                    //{
                    //    dtprdcode.Rows.RemoveAt(i);
                    //    break;
                    //}
                    //    }

                    // }
                    //if (dtprdcode.Rows.Count > 0)
                    //{
                    //    string prdcode = "";
                    //    foreach (DataRow dr in dtprdcode.Rows)
                    //    {
                    //        prdcode += "\"" + CODETONAME("prd", dr["PRODUCT_CODE"] + "") + "\",";
                    //    }
                    //    return Content(new JsonMessage { Success = true, Message = "商机未包含产品", Code = "3", Data = prdcode.TrimEnd(',') }.ToString());
                    //}
                    bool Flag = false;
                    foreach (DataRow drsj in dtprdsj.Rows)
                    {

                        string sjprdcode = drsj["PRODUCTDESCRIPTION"] + "";
                        string REFSql = string.Format("SELECT EqualProduct FROM SQM_CRM_PRODUCT_REF WHERE Product='{0}'", sjprdcode);
                        DataTable dt = DataHelper.QueryDataTable(REFSql);
                        if (dt.Rows.Count > 0)
                        {

                            foreach (DataRow refdt in dt.Rows)
                            {
                                string EqualProduct = refdt["EqualProduct"] + "";
                                for (int i = dtprdcode.Rows.Count - 1; i >= 0; i--)
                                {
                                    string prdcode = dtprdcode.Rows[i]["PRODUCT_CODE"] + "";
                                    if (EqualProduct == prdcode)
                                    {
                                        Flag = true;
                                    }

                                }
                            }
                        }
                        //商机下面的产品任意一个匹配上就行
                        //else
                        //{
                        //    return Content(new JsonMessage { Success = false, Message = "所选商机下有0个产品" }.ToString());
                        //}

                    }
                    if (Flag == false)
                    {
                        string prdcode = "";
                        foreach (DataRow dr in dtprdcode.Rows)
                        {
                            prdcode += "\"" + CODETONAME("prd", dr["PRODUCT_CODE"] + "") + "\",";
                        }
                        return Content(new JsonMessage { Success = true, Message = "商机未包含产品", Code = "3", Data = prdcode.TrimEnd(',') }.ToString());
                    }


                }
                else
                {
                    return Content(new JsonMessage { Success = false, Message = "所选商机下有0个产品" }.ToString());
                }
                //客户新增或者修改
                if (SQM_BJ_BP.FindAllByProperty("MRID", keyvalue).Length > 0)
                {
                    //修改
                    var srcobj_bp = SQM_BJ_BP.FindAllByProperty("MRID", keyvalue)[0];
                    srcobj_bp.BPCODE = cuscode;
                    srcobj_bp.BPNAME = cusname;
                    srcobj_bp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj_bp.DoUpdate();
                }
                else
                {
                    //新增
                    SQM_BJ_BP srcobj_bp1 = new SQM_BJ_BP();
                    srcobj_bp1.BPCODE = cuscode;
                    srcobj_bp1.BPNAME = cusname;
                    srcobj_bp1.MRID = keyvalue;
                    srcobj_bp1.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj_bp1.DoCreate();
                }
                //商机新增或者修改
                if (SQM_BJ_BIZ.FindAllByProperty("MRID", keyvalue).Length > 0)
                {
                    //修改，先移除之前的数据，再做新增
                    var Array = SQM_BJ_BIZ.FindAllByProperty("MRID", keyvalue);
                    for (int i = 0; i < Array.Length; i++)
                    {
                        var srcobj = SQM_BJ_BIZ.TryFind(Array[i].RID);
                        srcobj.MRID = "zfbj";
                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        srcobj.DoUpdate();
                    }
                    List<businessClass> bl = JsonHelper.GetObject<List<businessClass>>(busArray);
                    for (int j = 0; j < bl.Count; j++)
                    {
                        SQM_BJ_BIZ srcobj = new SQM_BJ_BIZ();
                        srcobj.BIZNAME = bl[j].busname;
                        srcobj.BIZID = bl[j].buscode;
                        srcobj.MRID = keyvalue;
                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        srcobj.DoCreate();
                    }
                }
                else
                {
                    //新增
                    List<businessClass> bl = JsonHelper.GetObject<List<businessClass>>(busArray);
                    for (int j = 0; j < bl.Count; j++)
                    {
                        SQM_BJ_BIZ srcobj = new SQM_BJ_BIZ();
                        srcobj.BIZNAME = bl[j].busname;
                        srcobj.BIZID = bl[j].buscode;
                        srcobj.MRID = keyvalue;
                        srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        srcobj.DoCreate();
                    }
                }
                //组织新增或者修改
                if (SQM_BJ_ORG.FindAllByProperty("MRID", keyvalue).Length > 0)
                {
                    //修改
                    SQM_BJ_ORG srcobj = SQM_BJ_ORG.FindAllByProperty("MRID", keyvalue)[0];
                    srcobj.ORGCODE = orgcode;
                    srcobj.ORGNAME = orgname;
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoUpdate();
                }
                else
                {
                    //新增
                    SQM_BJ_ORG srcobj = new SQM_BJ_ORG();
                    srcobj.MRID = keyvalue;
                    srcobj.ORGCODE = orgcode;
                    srcobj.ORGNAME = orgname;
                    srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    srcobj.DoCreate();
                }
                //报价版本信息 DLC
                SQM_BJ_VER versrcobj = SQM_BJ_VER.FindFirstByProperties(SQM_BJ_VER.Prop_MRID, keyvalue, SQM_BJ_VER.Prop_ZVER, zversion);
                vrid = versrcobj.RID;
                versrcobj.DTFROM = DateTime.Parse(dtfrom);
                versrcobj.DTTO = DateTime.Parse(dtto);
                if (zversion == "V1")
                {
                  versrcobj.CONTRSCTNUM = contrsctnum;
                }
                versrcobj.BPCODE9 = bpcode9;
                versrcobj.MEMO = memo;
                versrcobj.MODIFYTIME = DateTime.Now;
                versrcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                versrcobj.DoUpdate();
                //报价主信息
                var mainobj = SQM_BJ_MAIN_BASIC.TryFind(keyvalue);
                mainobj.DTFROM = DateTime.Parse(dtfrom);
                mainobj.DTTO = DateTime.Parse(dtto);
                mainobj.BJNAME = priceName;
                mainobj.MEMO = memo;
                mainobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.BJTCURR = WAERS;
                mainobj.DoUpdate();
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

                string xsybjid = "";
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
                    _ubody.lockStatus = "";
                    _ubody.id = mainobj.XSYBJID;
                    _ubody.customItem3__c = zversion;//报价版本 //测试
                    _ubody.customItem4__c = InterfaceFormat.FormatStatusXSY("0", "Z");//报价状态
                    _ubody.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + keyvalue + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion=" + zversion;
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
                    _body.lockStatus = "";
                    //_body.quotationTitle = "";报价名称
                    _body.customItem3__c = zversion;//报价版本 
                    _body.customItem4__c = InterfaceFormat.FormatStatusXSY("0", "Z");//报价状态
                    _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + keyvalue + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion=" + zversion;//报价地址
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
                    #endregion
                }
                mainobj.XSYBJID = xsybjid;
                mainobj.BJTCURR = WAERS;
                mainobj.DoUpdate();
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
        /// <summary>
        /// 通过选择商机新建报价
        /// </summary>
        /// <param name="cuscode"></param>
        /// <param name="cusname"></param>
        /// <param name="busArray"></param>
        /// <param name="orgcode"></param>
        /// <param name="orgname"></param>
        /// <param name="priceName"></param>
        /// <param name="dtfrom"></param>
        /// <param name="dtto"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public ActionResult SaveNEWSJBJ(string cuscode, string cusname, string busArray, string orgcode, string orgname, string priceName, string dtfrom, string dtto, string contrsctnum, string memo, string bpcode9,string WAERS)
        {
            var data = "";
            string rtnmsg = "新增报价成功！";
            bool flag = true;
            var mrid = System.Guid.NewGuid().ToString();//自建主表主键
            var vrid = System.Guid.NewGuid().ToString();//自建版本表主键主键
            try
            {
                //1 新建主表信息
                SQM_BJ_MAIN_BASIC mainobj = new SQM_BJ_MAIN_BASIC();
                mainobj.RID = mrid;
                mainobj.BJNAME = priceName;
                mainobj.CREATETIME = DateTime.Now;
                mainobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.AFFILIATION = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.DTFROM = DateTime.Parse(dtfrom);
                mainobj.DTTO = DateTime.Parse(dtto);
                mainobj.MEMO = memo;
                mainobj.BJTCURR = WAERS;//币种
                mainobj.DoCreate();
                //2 新建客户表信息
                SQM_BJ_BP srcobj_bp1 = new SQM_BJ_BP();
                srcobj_bp1.BPCODE = cuscode;
                srcobj_bp1.BPNAME = cusname;
                srcobj_bp1.MRID = mrid;
                srcobj_bp1.DoCreate();
                //3 新建商机表信息
                string bizstr = "";
                List<businessClass> bl = JsonHelper.GetObject<List<businessClass>>(busArray);
                for (int j = 0; j < bl.Count; j++)
                {
                    bizstr += "'" + bl[j].buscode + "',";
                    SQM_BJ_BIZ bizobj = new SQM_BJ_BIZ();
                    bizobj.BIZNAME = bl[j].busname;
                    bizobj.BIZID = bl[j].buscode;
                    bizobj.MRID = mrid;
                    bizobj.DoCreate();
                }
                //4 新增组织表信息
                SQM_BJ_ORG orgobj = new SQM_BJ_ORG();
                orgobj.MRID = mrid;
                orgobj.ORGCODE = orgcode;
                orgobj.ORGNAME = orgname;
                orgobj.DoCreate();
                // 5 新增版本表信息
                SQM_BJ_VER verobj = new SQM_BJ_VER();
                verobj.MRID = mrid;
                verobj.ZVER = "V1";
                verobj.RID = vrid;
                verobj.BPCODE9 = bpcode9;
                verobj.DTFROM = DateTime.Parse(dtfrom);
                verobj.DTTO = DateTime.Parse(dtto);
                verobj.CONTRSCTNUM = contrsctnum;
                verobj.ORGRID = orgcode.Substring(0, 4);
                verobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATETIME = DateTime.Now;
                verobj.DoCreate();
                //6 根据商机带出产品并且插入psf表中
                //crm 中取数据
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                //var sql = string.Format("SELECT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,CRM_BUS_PRODUCTINFO.PRODUCT_NAME,CRM_PRODUCT.PRODUCTSNAME,CRM_PRODUCT.PRODUCTDESCRIPTION FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE FOLLOWUPSTATUS = '跟进中' AND ID IN ({0}))", bizstr.TrimEnd(','));
                var sql = string.Format(@"SELECT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,
CRM_BUS_PRODUCTINFO.PRODUCT_NAME,
CRM_PRODUCT.PRODUCTSNAME,
CRM_PRODUCT.PRODUCTDESCRIPTION 
FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID 
WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE ID IN ({0}))", bizstr.TrimEnd(','));
                var prddt = DataHelper.QueryDataTable(sql, conn);
                if (prddt.Rows.Count > 0)
                {
                    for (var k = 0; k < prddt.Rows.Count; k++)
                    {
                        string prdcode = prddt.Rows[k]["PRODUCTDESCRIPTION"].ToString();
                        string prdname = prddt.Rows[k]["PRODUCTSNAME"].ToString();
                        string EqualProduct = "";
                        string EQUALDESCRIPTION = "";
                        string REFSql = string.Format("SELECT EqualProduct,EQUALDESCRIPTION FROM SQM_CRM_PRODUCT_REF WHERE Product='{0}'", prdcode);
                        DataTable dt = DataHelper.QueryDataTable(REFSql);
                        if (dt.Rows.Count > 0)
                        {
                            foreach(DataRow dr in dt.Rows){
                                EqualProduct = dr["EqualProduct"] + "";
                                EQUALDESCRIPTION = dr["EQUALDESCRIPTION"] + "";
                            }
                            //7 新建psf表
                            SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                            psfobj.MRID = mrid;
                            psfobj.VRID = vrid;
                            psfobj.ORGCODE = orgcode.Substring(0, 4);
                            psfobj.ORGNAME = orgname + "-" + orgcode.Substring(0, 4);
                            //psfobj.PRODUCT_CODE = prdcode;
                            //psfobj.PRODUCT_NAME = prdname;
                            psfobj.PRODUCT_CODE = EqualProduct;
                            psfobj.PRODUCT_NAME = EQUALDESCRIPTION;
                            psfobj.DoCreate();
                        }
                    }
                }

                string xsybjid = "";
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
                    _ubody.lockStatus = "";
                    _ubody.id = mainobj.XSYBJID;
                    _ubody.customItem3__c = "V1";//报价版本 //测试
                    _ubody.customItem4__c = InterfaceFormat.FormatStatusXSY("0", "Z");//报价状态
                    _ubody.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + mrid + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion="+ "V1";
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

                    SQM_BJ_ORG orgobj1 = SQM_BJ_ORG.FindFirstByProperties(SQM_BJ_ORG.Prop_MRID, mrid);
                    SQM_BJ_BIZ busobj = SQM_BJ_BIZ.FindFirstByProperties(SQM_BJ_BIZ.Prop_MRID, mrid);
                    SQM_BJ_BP cusobj = SQM_BJ_BP.FindFirstByProperties(SQM_BJ_BP.Prop_MRID, mrid);
                    //_body.customItem1__c = mainobj.BJNAME;//报价编号  不填
                    _body.lockStatus = "";
                    //_body.quotationTitle = "";报价名称
                    _body.customItem3__c = "V1";//报价版本 
                    _body.customItem4__c = InterfaceFormat.FormatStatusXSY("0", "Z");//报价状态
                    _body.customItem5__c = System.Configuration.ConfigurationManager.AppSettings["XSY_BACK_URL"] + "List/SSOXSY_FOREDIT?keyValue=" + mrid + "&UserId=" + Oncontrol3.Web.Helpers.SQMHelper.getStaffKey() + "&zversion=" + "V1";//报价地址
                    _body.customItem6__c = orgobj1.ORGCODE;//mainobj.BJNAME;//操作组织 
                    _body.customItem7__c = mrid;//mainobj.BJNAME;//报价ID
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
                    #endregion
                }
                mainobj.XSYBJID = xsybjid;
                mainobj.BJTCURR = WAERS;
                mainobj.DoUpdate();
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = flag, Message = rtnmsg, Data = mrid }.ToString());
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
        public class businessClass
        {
            public string buscode { get; set; }
            public string busname { get; set; }
        }
    }
}
