using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Newtonsoft.Json;
using Oncontrol3.Web.Helpers;
using Oncontrol3.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Oncontrol3.Web.FWA702;
using Oncontrol3.Web.FWA703;
using System.Web.Helpers;

namespace Oncontrol3.Web.Controllers
{
    public class SendPriceController : BaseController
    {
       public ActionResult PriceIndex()
        {
            string kygh = getAppSetting("KYGH");
            string hygh = getAppSetting("HYGH");
            string gylgh = getAppSetting("GYLGH");
            string ysgh = getAppSetting("YSGH");
            string  feesql = @"select TCET084,TEXTDESC from V_MDM_FEE";
            DataTable Feedt = DataHelper.QueryDataTable(feesql);
            ViewBag.FeeData = Feedt;
            ViewBag.kygh = kygh;
            ViewBag.hygh = hygh;
            ViewBag.gylgh = gylgh;
            ViewBag.ysgh = ysgh;
            //string djfsrid = string.Empty;
            //if (String.IsNullOrEmpty(djfsrid) && DJFSdt.Rows.Count > 0)
            //{
            //    djfsrid = DJFSdt.Rows[0]["DJFSRID"].ToString();
            //}
           // ViewBag.djfsrid = djfsrid;
            return View();
        }
        private string getAppSetting(string str)
        {
            return ConfigHelper.AppSettings(str);
        }
        /// <summary>
        ///根据feecode获取定价方式数据
        /// </summary>
        /// <param name="feecode"></param>
        /// <returns></returns>
        public ActionResult GetDjfs(string feecode)
        {
            string sql= @"SELECT  DISTINCT r.djfsrid,  r.DJFSNAME FROM SQM_FEE_PUR_REF r
                        left JOIN SQM_FEE_CALC f
                        on f.rid = r.feerid
                        WHERE r.status = '1' AND r.FEECODE=" + "'"+feecode+"'";
          //  DataTable DJFSdt = DataHelper.QueryDataTable(sql);
          //  ViewBag.DJFSData = DJFSdt;
            var djfsArray = DataHelper.QueryObjectsList(sql);
            object[] data = { djfsArray };
            return Content(JsonHelper.GetJsonString(data));
          //  return View();

        }
        /// <summary>
        ///  获取报价清单数据
        /// </summary>
        /// <param name="feecode"></param>
        /// <param name="djfsrid"></param>
        /// <returns></returns>
        public ActionResult GetPriceDetail(string djfsrid)
        {
             // djfsrid = "7194f936-5e0f-818d-3132-89a47d434595";  //测试数据
            ////string sqlVer = @"SELECT r.Mrid,r.zver,r.fwa,r.createuser,v.modifytime FROM SQM_FWA_REF r LEFT JOIN SQM_BJ_VER v
            //                ON r.Mrid = v.mrid";
            // WHERE sqm_bj_main_basic.rid = '70867e24-1c8b-40b9-b6d2-abada8b39146'";
            string bjrid = string.Empty;
            string mrid = string.Empty;
            // 20200414 add dz
            string feecode = string.Empty;
            //20200414 end dz
            DataTable bjridDt = null;
            DataTable bjpsfDt = null;
            DataTable dtbjDetail = null;
            // 获取 bjrid   BJSTATUS = '1'   1-已保存
            djfsrid = djfsrid.Trim();

            //通过报价方式的RID 获取 报价费目明细表 中的 1、FEECALCID：报价方式费目的行ID； 2、DJFSRID：报价方式的RID ；3、BJSTATUS：报价状态（0未保存，1-已保存，2-已确认，3-已审批，4-）
            string getbjridSql = @"SELECT distinct t.FEECALCID,t.DJFSRID,BJSTATUS FROM SQM_MODEBJ_VAL t WHERE BJSTATUS = '1' AND DJFSRID = " + "'" +  djfsrid+ "'";
            bjridDt = DataHelper.QueryDataTable(getbjridSql);
            if (bjridDt.Rows.Count>0)
            {
                //获取 报价方式费目的行ID
                bjrid = bjridDt.Rows[0]["FEECALCID"].ToString();
                //获取MRID：mrid和FEE_CODE：费目代码  通过SQM_BJ_PSF（产品服务费目表）与SQM_BJ_MAIN_BASIC（主表）之间的关联和已报价方式费目的行ID（FEECALCID）作为条件 获取mrid和费目代码
                string getMRIDsql = @"select p.BJSTATAUS,p.OTHER_NAME,p.OTHER_NAME_EN,p.MRID,p.VRID,p.FEE_CODE,p.FEE_NAME
                                    from SQM_BJ_PSF p left join SQM_BJ_MAIN_BASIC b on p.MRID=b.RID where p.RID ='" + bjrid + "'";
                bjpsfDt = DataHelper.QueryDataTable(getMRIDsql);

                if (bjpsfDt != null && bjpsfDt.Rows.Count>0)
                {
                    //获取主表 RID
                    mrid = bjpsfDt.Rows[0]["MRID"].ToString();
                    // 20200414 add dz
                    //获取费目代码
                    feecode = bjpsfDt.Rows[0]["FEE_CODE"].ToString();
                    //20200414 end dz
                    #region 用于测试的SQL  已注释 
                    //string sqlstr = @"SELECT distinct  sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime
                    //        FROM sqm_bj_main_basic 
                    //         LEFT JOIN sqm_bj_bp ON sqm_bj_main_basic.rid = sqm_bj_bp.mrid 
                    //         LEFT JOIN SQM_BJ_VER v on v.mrid=sqm_bj_bp.mrid
                    //         LEFT JOIN sqm_bj_psf psf on psf.MRID=V.mrid
                    //         WHERE sqm_bj_main_basic.rid='" + mrid + "'" + "order by v.zver desc";
                    //测试数据 MRID="cd12d66c-d91a-45d8-b862-571117deee0d"
                    //string sqlstr = @" SELECT sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime,r.FWA,r.ITEMNO
                    //        FROM sqm_bj_main_basic  
                    //         LEFT JOIN sqm_bj_bp ON sqm_bj_main_basic.rid = sqm_bj_bp.mrid 
                    //         LEFT JOIN SQM_BJ_VER v on v.mrid=sqm_bj_bp.mrid
                    //         LEFT JOIN sqm_bj_psf psf on psf.MRID=V.mrid
                    //        LEFT JOIN SQM_FWA_REF r on psf.MRID=r.MRID
                    //         WHERE sqm_bj_main_basic.rid='cd12d66c-d91a-45d8-b862-571117deee0d' AND r.FWA IS NOT NULL
                    //         GROUP BY sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime,r.FWA,r.ITEMNO"; 

                    //20200414 edit dz "+ mrid + "  and "+ feecode + "'
                    //已报价主表（sqm_bj_main_basic）作为主数据表，左连接 报价BP表（sqm_bj_bp）、版本表（SQM_BJ_VER）、产品服务费目表（sqm_bj_psf）、报价主表和FWA关联表（SQM_FWA_REF）
                    string sqlstr = @" SELECT r.mrid,sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime,r.fwa,r.itemno,psf.condition,psf.jxjc,psf.stagetype
                           FROM sqm_bj_main_basic
                            LEFT JOIN sqm_bj_bp ON sqm_bj_main_basic.rid = sqm_bj_bp.mrid
                             LEFT JOIN SQM_BJ_VER v on v.mrid = sqm_bj_bp.mrid
                             LEFT JOIN sqm_bj_psf psf on psf.MRID = V.mrid
                             LEFT JOIN SQM_FWA_REF r on psf.mrid = r.mrid
                             WHERE sqm_bj_main_basic.rid = '"+ mrid + "' AND r.fwa IS NOT NULL  AND psf.fee_code='"+ feecode + "'";
                    //20200414 end dz  

                    #endregion
                    //string sqlstr = @"SELECT sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime,r.FWA,r.ITEMNO
                    //        FROM sqm_bj_main_basic  
                    //         LEFT JOIN sqm_bj_bp ON sqm_bj_main_basic.rid = sqm_bj_bp.mrid 
                    //         LEFT JOIN SQM_BJ_VER v on v.mrid=sqm_bj_bp.mrid
                    //         LEFT JOIN sqm_bj_psf psf on psf.MRID=V.mrid
                    //        LEFT JOIN SQM_FWA_REF r on psf.MRID=r.MRID
                    //         WHERE sqm_bj_main_basic.rid='" + mrid + "'" + " AND r.FWA IS NOT NULL" +
                    //    " GROUP BY sqm_bj_main_basic.bjname, psf.orgname,sqm_bj_bp.bpname,v.zver, psf.product_name, v.createuser,v.modifytime,r.FWA,r.ITEMNO";
                    dtbjDetail = DataHelper.QueryDataTable(sqlstr);
                        //var list = DataHelper.HqlQueryList(sqlstr);
                    }
            }
            if (dtbjDetail != null && dtbjDetail.Rows.Count>1)
            {
                string jsonStr = DataTableToJson(dtbjDetail);
                return Content(jsonStr);
            }
            else
            {
                return Content("");
            }
        }
        public static string DataTableToJson(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        static readonly string STR_VERSION_NO = "0000";
        public ActionResult SubmitTM(string postdata)
        {
            //获取协议接口703：BizTalk_RFC_TM_CRM_703_Orchestration_InboundSoapClient  传入参数：
            //修改协议接口702：BizTalk_RFC_TM_CRM_702_Orchestration_InboundSoapClient
            // PriceSubmitModel submitMode = JsonConvert.DeserializeObject<PriceSubmitModel>(postdata);//json数据转为对象
            // JavaScriptSerializer json = new JavaScriptSerializer();
            // var submitMode =(PriceSubmitModel) json.Deserialize<PriceSubmitModel>(postdata);
            bool fwaflag = true;//TM执行修改的返回标记
            string fwamsg =string.Empty;//TM执行修改的返回消息
            string[] checkArray = new string[7];
            postdata = postdata.Remove(postdata.Length - 1, 1).Remove(0, 1);
            checkArray = postdata.Split(',');
            PriceSubmitModel submitModel = new PriceSubmitModel();
            submitModel.MRID = checkArray[0];
            submitModel.FWA = checkArray[1];
            submitModel.ZVER = checkArray[2];
            submitModel.ITEMNO = checkArray[3];
            submitModel.JXJC = checkArray[4];
            submitModel.CONDITION = checkArray[5];
            submitModel.STAGETYPE = checkArray[6];
            
            try
            {

            List<string> KeysList = new List<string>();
                //获取协议
                //查找TM中的协议号
                string strMRID = submitModel.MRID.Remove(submitModel.MRID.Length - 1, 1).Remove(0, 1);//去掉字符串首尾的双引号
                string strFWA = submitModel.FWA.Remove(submitModel.FWA.Length - 1, 1).Remove(0, 1);
                string strZVER = submitModel.ZVER.Remove(submitModel.ZVER.Length - 1, 1).Remove(0, 1);
                string sqlhadfwapre = string.Format(" SELECT * FROM ( SELECT FWA FROM SQM_FWA_REF WHERE MRID = '{0}' and FWA LIKE '{1}%' ORDER BY FWA DESC ) WHERE  ROWNUM<=1  ", strMRID, strFWA);
           string  strFWA703 = DataHelper.QueryValue(sqlhadfwapre) + "";
                
            BizTalk_RFC_TM_CRM_703_Orchestration_InboundSoapClient fwa703service = new BizTalk_RFC_TM_CRM_703_Orchestration_InboundSoapClient();
           fwa703service.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);
            Z2FM_SQ_FWA_SEARCH fwasrch703 = new Z2FM_SQ_FWA_SEARCH();
            fwasrch703.IV_FAGRMNTID044 = strFWA703;//协议号
            fwasrch703.IV_VERSION_NO = STR_VERSION_NO;//版本号
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
            //  fwa702.VALIDITY_START = DateTime.Parse(dtBJMB.Rows[0]["DTFROM"] + "").ToString("yyyyMMdd");//有效期开始日期   报价有效期
            //  fwa702.VALIDITY_END = DateTime.Parse(dtBJMB.Rows[0]["DTTO"] + "").ToString("yyyyMMdd");//有效期结束日期 报价有效期
            string ACTION_U = "U";

            if (null != fwa702.FAG_ITEM)//ITEMS导入表类型
            {
                foreach (var item2 in fwa702.FAG_ITEM)
                {
                    item2.ACTION = ACTION_U;
                    //"0000-00-00"->"00000000"  2019-8-5    DLC放开注释

                    if (null != item2.TCCS_ROOT && null != item2.TCCS_ROOT.TCCS_ITEM)//计算单ROOT导入表类型 
                    {
                        item2.TCCS_ROOT.ACTION = ACTION_U;
                        foreach (var item4 in item2.TCCS_ROOT.TCCS_ITEM)//计算单ITEM导入表类型
                        {
                            item4.ACTION = ACTION_U;
                            item4.CLCRESBAS036 = submitModel.JXJC;
                            item4.STAGE_CAT = submitModel.STAGETYPE;
                            item4.RULE101 = submitModel.CONDITION;
                            if (null != item4.ITEM_CALCRULE)//ITEM_CALCRULE导入表结构
                            {
                                foreach (var item5 in item4.ITEM_CALCRULE)
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
                else
                {
                    return Content(new JsonMessage { Success = true, Data = null, Code = "1", Message = "修改协议成功" }.ToString());
                }
            }
            catch (Exception ex702)
            {
                return Content(new  { fwaflag="E", fwamsg = ex702.Message }.ToString());
            }
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
    }
}
