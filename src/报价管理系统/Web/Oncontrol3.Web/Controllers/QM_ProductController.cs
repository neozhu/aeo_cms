using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.MDM;
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
using Oncontrol3.Web.ZSyncPSF;
using System.Data.OracleClient;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>
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

    public class postdata
    {
        public string feecode { get; set; }

        public string servicetypecode { get; set; }
    }
    public class QM_ProductController : BaseController
    {
        [AllowAnonymous]
        public ActionResult QM_ProductIndex()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }

        [AllowAnonymous]
        public ActionResult QM_ProductEdit()
        {
            return View();
        }

        public ActionResult GetFormJson(string keyValue)
        {
            {   //新增通过有效期区间判断产品状态逻辑
                bool flag = false;
                var nowTime = DateTime.Now;
                var statusObj = SQM_PRD_EXT.TryFind(keyValue);
                if (statusObj == null)
                {
                    SQM_PRD_EXT obj_ext = new SQM_PRD_EXT();
                    obj_ext.PRODUCTKEY = keyValue;
                    obj_ext.DoCreate();
                }
                statusObj = SQM_PRD_EXT.TryFind(keyValue);
                string statusSql = string.Format("SELECT DTFROM,DTTO,STATUS FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}'", keyValue);
                var statusData = DataHelper.QueryDataTable(statusSql);
                if (statusData.Rows.Count > 0)
                {
                    for (var i = 0; i < statusData.Rows.Count; i++)
                    {
                        var dtto = DateTime.Parse(statusData.Rows[i]["DTTO"].ToString());
                        var dtfrom = DateTime.Parse(statusData.Rows[i]["DTFROM"].ToString());

                        if (dtfrom < nowTime && nowTime < dtto && statusData.Rows[i]["STATUS"].ToString() == "1")
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        statusObj.STATUS = "1";
                    }
                    else
                    {
                        statusObj.STATUS = "0";
                    }
                    statusObj.DoUpdate();
                };


                string sql = string.Format(" SELECT  MDM_PRODUCT.PRODUCTKEY,MDM_PRODUCT.PRODUCTNAME,MDM_PRODUCT.CREATETIME,MDM_PRODUCT.MEMO,SQM_PRD_EXT.MODIFYTIME,SQM_PRD_EXT.BUSINESSTYPE,SQM_PRD_EXT.BUSINESSORG,SQM_PRD_EXT.PRODUCTMANAGERID,SQM_PRD_EXT.PRODUCTMANAGERNAME,SQM_PRD_EXT.DEAGIRATE,SQM_PRD_EXT.SORD,SQM_PRD_EXT.SQPRODUCTNAME,SQM_PRD_EXT.CREATEUSER,SQM_PRD_EXT.MODIFYUSER FROM MDM_PRODUCT LEFT JOIN SQM_PRD_EXT ON MDM_PRODUCT.PRODUCTKEY = SQM_PRD_EXT.PRODUCTKEY WHERE MDM_PRODUCT.PRODUCTKEY LIKE '%{0}%' ", keyValue);
                var data = DataHelper.QueryDataTable(sql);
                DataTable data2 = null;
                try
                {
                    if (data.Rows.Count > 0)
                    {
                        //根据工号得到用户名 从crm里面取值
                        IDbConnection conn = new OracleConnection();
                        conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        string namesql= "select loginname as workno,name from sysuser where loginname='" + data.Rows[0]["CREATEUSER"] + "' or loginname='" + data.Rows[0]["MODIFYUSER"] + "'";
                        ///whereStr += " loginname='" + data.Rows[0]["CREATEUSER"] + "' or loginname='" + data.Rows[0]["MODIFYUSER"] + "'";
                        data2 = DataHelper.QueryDataTable(namesql,conn);
                    }
                }
                catch
                {

                }

                //return Content(JsonHelper.GetJsonStringFromDataTable(data));
                return Content(JsonHelper.GetJsonString(new {data,data2 }));
            }
        }
        /// <summary>
        /// 查询有效期区间列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetDateJson()
        {
            string keyValue = Request["ProductCode"].ToString();
            string sql = string.Format(" SELECT RID,PRODUCTKEY,DTFROM, DTTO, STATUS,MEMO FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}' ORDER BY CREATETIME DESC", keyValue);
            var data = DataHelper.QueryDataTable(sql);
            string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            var countsql = string.Format(" SELECT COUNT (*)  FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}' ORDER BY CREATETIME DESC", keyValue);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            //没有数据 新增一行空白数据便于新增操作
            if(rtndata.Rows.Count == 0)
            {
                DataRow dr = rtndata.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = DateTime.Now;
                dr[3] = DateTime.Now;
                dr[4] = "";
                dr[5] = "";
                rtndata.Rows.Add(dr);
            }
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));

        }
        /// <summary>
        /// 通过主键得到有效时间区间
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public ActionResult GetDateByRID(string rid)
        {
            bool rtnflag = true;
            string rtnmsg = "查询成功";
            SQM_PRD_EXT_PERIOD targetobj_ext_period = null;
            try
            {
                targetobj_ext_period = SQM_PRD_EXT_PERIOD.TryFind(rid);
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj_ext_period, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 修改或新增数据
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="postdata_ext"></param>
        /// <param name="keyValue"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(string postdata, string postdata_ext, string postdata_ext_period,string keyValue)
        {

            //新增通过有效期区间判断产品状态逻辑 start
            if (!string.IsNullOrEmpty(keyValue))
            {
                bool flag1 = false;
                var nowTime = DateTime.Now;
                var statusObj = SQM_PRD_EXT.TryFind(keyValue);
                if (statusObj == null)
                {
                    SQM_PRD_EXT obj_ext = new SQM_PRD_EXT();
                    obj_ext.PRODUCTKEY = keyValue;
                    obj_ext.DoCreate();
                }
                statusObj = SQM_PRD_EXT.TryFind(keyValue);
                string statusSql = string.Format("SELECT DTFROM,DTTO,STATUS FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}'", keyValue);
                var statusData = DataHelper.QueryDataTable(statusSql);
                if (statusData.Rows.Count > 0)
                {
                    for (var i = 0; i < statusData.Rows.Count; i++)
                    {
                        var dtto = DateTime.Parse(statusData.Rows[i]["DTTO"].ToString());
                        var dtfrom = DateTime.Parse(statusData.Rows[i]["DTFROM"].ToString());

                        if (dtfrom < nowTime && nowTime < dtto && statusData.Rows[i]["STATUS"].ToString() == "1")
                        {
                            flag1 = true;
                        }
                    }
                    if (flag1)
                    {
                        statusObj.STATUS = "1";
                    }
                    else
                    {
                        statusObj.STATUS = "0";
                    }
                    statusObj.DoUpdate();
                };
            }
            //end
            bool rtnflag =  true;
            string rtnmsg = "保存成功";
            MDM_PRODUCT targetobj = null;
            MDM_PRODUCT srcobj = null;
            SQM_PRD_EXT targetobj_ext = null;
            SQM_PRD_EXT srcobj_ext = null;
            SQM_PRD_EXT_PERIOD targetobj_ext_period = null;
            SQM_PRD_EXT_PERIOD srcobj_ext_period = null;
            BJWebServiceSoapClient client = new BJWebServiceSoapClient();
            try
            {
                srcobj = JsonHelper.GetObject<MDM_PRODUCT>(postdata);
                srcobj_ext = JsonHelper.GetObject<SQM_PRD_EXT>(postdata_ext);
                CRM_PRODUCT crmProduct = new CRM_PRODUCT();
                crmProduct.PRODUCTDESCRIPTION = srcobj_ext.PRODUCTKEY;
                crmProduct.PRODUCTSNAME = srcobj_ext.SQPRODUCTNAME;
                crmProduct.DIVISION = srcobj_ext.BUSINESSORG;
                crmProduct.FATHERID = "";//业务类型
                crmProduct.FATHERNAME = srcobj_ext.BUSINESSTYPE;
                crmProduct.PRODUCTUSER = srcobj_ext.PRODUCTMANAGERNAME;//产品经理
                crmProduct.PRODUCTUSERID = srcobj_ext.PRODUCTMANAGERID;
                crmProduct.MLV = srcobj_ext.DEAGIRATE;//毛利率
                crmProduct.STATUS = srcobj_ext.STATUS;//状态 
                srcobj_ext_period = JsonHelper.GetObject<SQM_PRD_EXT_PERIOD>(postdata_ext_period);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    targetobj = MDM_PRODUCT.TryFind(keyValue);
                    targetobj_ext = SQM_PRD_EXT.TryFind(keyValue);
                    DataHelper.MergeData<MDM_PRODUCT>(targetobj, srcobj);
                    targetobj.ZFFWZH = "X";
                    targetobj.DoUpdate();
                    if (targetobj_ext == null)
                    {
                        targetobj_ext = srcobj_ext;
                        targetobj_ext.MODIFYTIME = DateTime.Now;
                        targetobj_ext.CREATETIME = DateTime.Now;
                        targetobj_ext.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        targetobj_ext.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        targetobj_ext.DoCreate();
                       
                    }
                    else
                    {
                        targetobj_ext.PRODUCTKEY = srcobj_ext.PRODUCTKEY;
                        targetobj_ext.SQPRODUCTNAME = srcobj_ext.SQPRODUCTNAME;
                        targetobj_ext.BUSINESSORG = srcobj_ext.BUSINESSORG;
                        targetobj_ext.BUSINESSTYPE = srcobj_ext.BUSINESSTYPE;
                        targetobj_ext.PRODUCTMANAGERID = srcobj_ext.PRODUCTMANAGERID;
                        targetobj_ext.PRODUCTMANAGERNAME = srcobj_ext.PRODUCTMANAGERNAME;
                        targetobj_ext.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        targetobj_ext.MEMO = srcobj_ext.MEMO;
                        targetobj_ext.MODIFYTIME = DateTime.Now;
                        targetobj_ext.DoUpdate();
                    }
                }
                else
                {
                    //新增产品时创建时间取修改时间
                    srcobj.CREATETIME = srcobj.MODIFYTIME;
                    targetobj = srcobj;
                    targetobj.ZFFWZH = "X";
                    targetobj.DoCreate();
                    targetobj_ext = srcobj_ext;
                    //targetobj_ext.MODIFYTIME = DateTime.Now;
                    targetobj_ext.CREATETIME = DateTime.Now;
                    targetobj_ext.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    //targetobj_ext.MODIFYUSER = Oncontrol3.Web.Helpers.SessionHelper.GetSessionUser<Oncontrol3.Web.Controllers.FLD_QO_USER>().staffkey;
                    targetobj_ext.PRODUCTKEY = srcobj_ext.PRODUCTKEY;
                    targetobj_ext.SQPRODUCTNAME = srcobj_ext.SQPRODUCTNAME;
                    targetobj_ext.BUSINESSORG = srcobj_ext.BUSINESSORG;
                    targetobj_ext.BUSINESSTYPE = srcobj_ext.BUSINESSTYPE;
                    targetobj_ext.PRODUCTMANAGERID = srcobj_ext.PRODUCTMANAGERID;
                    targetobj_ext.PRODUCTMANAGERNAME = srcobj_ext.PRODUCTMANAGERNAME;
                    targetobj_ext.DoCreate();
                    //targetobj_ext_period.DoCreate();
                }
                //var flag = client.MDMPRODUCT(crmProduct);
            }
            catch (Exception ex)
            {
                rtnflag = false; 
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 编辑有效期区间
        /// </summary>
        /// <param name="postdata_ext_period"></param>
        /// <returns></returns>
        public ActionResult SubmitDate(string postdata_ext_period,string rid,string keyValue)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            
                string sqlAll = string.Format("SELECT RID ,DTTO,DTFROM FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}'", keyValue);
                var dataAll = DataHelper.QueryDataTable(sqlAll);
                for (var i = 0; i < dataAll.Rows.Count; i++)
                {
                    var dtto = dataAll.Rows[i]["DTTO"].ToString();
                    if (dataAll.Rows[i]["DTTO"].ToString() == "" || dataAll.Rows[i]["DTFROM"].ToString() == "")
                    {
                        var RID = dataAll.Rows[i]["RID"];
                        var obj_del = SQM_PRD_EXT_PERIOD.TryFind(RID);
                        obj_del.DoDelete();
                    }
                }
            
            SQM_PRD_EXT_PERIOD targetobj_ext_period = null;
            SQM_PRD_EXT_PERIOD srcobj_ext_period = null;
            try
            {
                srcobj_ext_period = JsonHelper.GetObject<SQM_PRD_EXT_PERIOD>(postdata_ext_period);
                //通过有无主键来判断做新建还是修改操作
                if (string.IsNullOrEmpty(rid))
                {
                    srcobj_ext_period.PRODUCTKEY = keyValue;
                    targetobj_ext_period = srcobj_ext_period;
                    targetobj_ext_period.DoCreate();
                }
                else
                {
                    targetobj_ext_period = SQM_PRD_EXT_PERIOD.TryFind(rid);
                    DataHelper.MergeData<SQM_PRD_EXT_PERIOD>(targetobj_ext_period, srcobj_ext_period);
                    targetobj_ext_period.DoUpdate();
                }
                //有效区间校验逻辑
                var statusObj = SQM_PRD_EXT.TryFind(keyValue);
                if (statusObj == null)
                {
                    SQM_PRD_EXT obj_ext = new SQM_PRD_EXT();
                    obj_ext.PRODUCTKEY = keyValue;
                    obj_ext.DoCreate();
                }
                var nowTime = DateTime.Now;
                bool flag = true;
                statusObj = SQM_PRD_EXT.TryFind(keyValue);
                string statusSql = string.Format("SELECT DTFROM,DTTO,STATUS FROM SQM_PRD_EXT_PERIOD WHERE PRODUCTKEY = '{0}'", keyValue);
                var statusData = DataHelper.QueryDataTable(statusSql);
                if (statusData.Rows.Count > 0)
                {
                    for (var i = 0; i < statusData.Rows.Count; i++)
                    {
                        var dtto = DateTime.Parse(statusData.Rows[i]["DTTO"].ToString());
                        var dtfrom = DateTime.Parse(statusData.Rows[i]["DTFROM"].ToString());

                        if (dtfrom < nowTime && nowTime < dtto && statusData.Rows[i]["STATUS"].ToString() == "1")
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        statusObj.STATUS = "1";
                    }
                    else
                    {
                        statusObj.STATUS = "0";
                    }
                    statusObj.DoUpdate();
                };
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj_ext_period, Code = "1", Message = rtnmsg }.ToString());

        }
        /// <summary>
        /// 通过产品代码得到所对应的服务以及服务对应的费目
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult PRDTOSRV ()
        {
            string ProductCode = Request["ProductCode"].ToString();
            string sql;
            //查询条件拼接
            string wherestr = "";
            string servicename = Request["SERVICENAME"].ToString();
            string textdesc = Request["TEXTDESC"].ToString();
            if (servicename != "")
            {
                wherestr += "AND (MDM_PRD_SRV_REF.SERVICETYPECODE like '%" + servicename + "%' or MDM_SERVICE.SERVICENAME like '%" + servicename + "%')";
            }
            if (textdesc != "")
            {
                wherestr += "AND (MDM_SRV_FEE_REF.TCET084 like '%" + textdesc + "%' or MDM_FEE.TEXTDESC like '%" + textdesc + "%')";
            }
            sql = string.Format("SELECT MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME,MDM_SRV_FEE_REF.RID,MDM_SRV_FEE_REF.TCET084,MDM_FEE.TEXTDESC,MDM_FEE.MEMO,SQM_SRV_EXT.SORD AS SRVSORD, QDM_FEE_SRV_REF.SORID AS FEESORD,QDM_FEE_SRV_REF.BXBJ FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE LEFT JOIN MDM_SRV_FEE_REF ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SRV_FEE_REF.SRVRQCD121 LEFT JOIN V_MDM_FEE MDM_FEE ON MDM_FEE.TCET084 = MDM_SRV_FEE_REF.TCET084 LEFT JOIN QDM_FEE_SRV_REF ON MDM_SRV_FEE_REF.Tcet084 = QDM_FEE_SRV_REF.feecode and  MDM_SRV_FEE_REF.srvrqcd121 =QDM_FEE_SRV_REF.SERVICETYPECODE and QDM_FEE_SRV_REF.Productcode=MDM_PRD_SRV_REF.Productcode LEFT JOIN SQM_SRV_EXT ON SQM_SRV_EXT.SERVICEKEY=MDM_PRD_SRV_REF.SERVICETYPECODE and SQM_SRV_EXT.Productcode = MDM_PRD_SRV_REF.PRODUCTCODE  WHERE MDM_PRD_SRV_REF.PRODUCTCODE = '{0}' {1} ORDER BY MDM_PRD_SRV_REF.SERVICETYPECODE ASC", ProductCode, wherestr);
            //设置分页
            string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //数据数量
            string countsql = string.Format("SELECT COUNT (*) FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE LEFT JOIN MDM_SRV_FEE_REF ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SRV_FEE_REF.SRVRQCD121 LEFT JOIN V_MDM_FEE MDM_FEE ON MDM_FEE.TCET084 = MDM_SRV_FEE_REF.TCET084 LEFT JOIN QDM_FEE_SRV_REF ON MDM_SRV_FEE_REF.Tcet084 = QDM_FEE_SRV_REF.feecode and  MDM_SRV_FEE_REF.srvrqcd121 = QDM_FEE_SRV_REF.SERVICETYPECODE and QDM_FEE_SRV_REF.Productcode=MDM_PRD_SRV_REF.Productcode LEFT JOIN SQM_SRV_EXT ON SQM_SRV_EXT.SERVICEKEY=MDM_PRD_SRV_REF.SERVICETYPECODE and SQM_SRV_EXT.Productcode = MDM_PRD_SRV_REF.PRODUCTCODE  WHERE MDM_PRD_SRV_REF.PRODUCTCODE = '{0}' {1}", ProductCode, wherestr);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql);

            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));

        }
        /// <summary>
        /// 得到服务排序字段
        /// </summary>
        /// <param name="srvcode"></param>
        /// <returns></returns>
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        
        public ActionResult GETSRVSORD(string srvcode,string productcode)
        {
            bool rtnflag = true;
            string rtnmsg = "查询成功";
            string SORD = "";
            //SQM_SRV_EXT targetobj_srv = null;
            //try
            //{
            //    targetobj_srv = SQM_SRV_EXT.TryFind(srvcode);
            //}
            //catch (Exception ex)
            //{
            //    rtnflag = false;
            //    rtnmsg = ex.Message;
            //}
            string srcobjSql = string.Format("select * from SQM_SRV_EXT where PRODUCTCODE='{0}' and SERVICEKEY='{1}'", productcode, srvcode);
            DataTable targetobj_srv = DataHelper.QueryDataTable(srcobjSql);
            if (targetobj_srv.Rows.Count>0)
            {
                foreach (DataRow dr in targetobj_srv.Rows)
                {
                    SORD = dr["SORD"] + "";
                }
            }

            return Content(new JsonMessage { Success = rtnflag, Data = SORD, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 得到费目排序字段
        /// </summary>
        /// <param name="feecode"></param>
        /// <returns></returns>
        public ActionResult GETFEESORD (string feecode)
        {
            bool rtnflag = true;
            string rtnmsg = "查询成功";
            QDM_FEE_SRV_REF targetobj_fee = null;
            string FEECODE = "FeeCode";
            try
            {
                var resulet = QDM_FEE_SRV_REF.FindAllByProperty(FEECODE, feecode);
                targetobj_fee = resulet[0];
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj_fee, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 保存费目排序字段
        /// </summary>
        /// <param name="feecode"></param>
        /// <param name="feesord"></param>
        /// <returns></returns>
        public ActionResult SAVEFEESORT(string rid, string feesord, string tcet084, string servicetypecode, string productcode)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";

            QDM_FEE_SRV_REF targetobj = new QDM_FEE_SRV_REF();

            try
            {
               // var srcobj = QDM_FEE_SRV_REF.TryFind(rid);
                //if (srcobj != null)
                //{
                //    srcobj.SORID = feesord;
                //    targetobj = srcobj;
                //    targetobj.DoUpdate();
                //}
                string srcobjSql = string.Format("select * from QDM_FEE_SRV_REF where PRODUCTCODE='{0}' and SERVICETYPECODE='{1}' and FEECODE='{2}'", productcode, servicetypecode, tcet084);
                DataTable dt =DataHelper.QueryDataTable(srcobjSql);
                if (dt.Rows.Count>0)
                {
                    string update = string.Format("update QDM_FEE_SRV_REF set SORID='{3}' where PRODUCTCODE='{0}' and SERVICETYPECODE='{1}' and FEECODE='{2}'", productcode, servicetypecode, tcet084, feesord);
                    DataHelper.ExecSql(update);
                }
                else
                {
                    targetobj.RID = rid;
                    targetobj.SORID = feesord;
                    targetobj.FeeCode = tcet084;
                    targetobj.ServiceTypeCode = servicetypecode;
                    targetobj.ProductCode = productcode;
                    targetobj.DoCreate();
                }

            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = rtnmsg }.ToString());
        }
    
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult SAVESRVSORT(string srvcode, string srvsord, string productcode)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";

            SQM_SRV_EXT targetobj = null;

            try
            {
                //var srcobj = SQM_SRV_EXT.TryFind(srvcode);
                //if(srcobj != null)
                //{
                //    srcobj.SORD = Decimal.Parse(srvsord);
                //    targetobj = srcobj;
                //    targetobj.DoUpdate();
                //}
                string srcobjSql = string.Format("select * from SQM_SRV_EXT where PRODUCTCODE='{0}' and SERVICEKEY='{1}'", productcode, srvcode);
                DataTable dt = DataHelper.QueryDataTable(srcobjSql);
                if (dt.Rows.Count > 0)
                {
                    string update = string.Format("update SQM_SRV_EXT set SORD='{2}' where PRODUCTCODE='{0}' and SERVICEKEY='{1}' ", productcode, srvcode, Decimal.Parse(srvsord));
                    DataHelper.ExecSql(update);
                }
                else
                {
                    targetobj = new SQM_SRV_EXT();
                    targetobj.SERVICEKEY = srvcode;
                    targetobj.RID = System.Guid.NewGuid().ToString();
                    targetobj.ProductCode = productcode;
                    targetobj.SORD = Decimal.Parse(srvsord);
                    targetobj.DoCreate();
                }
                

            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 保存必选费目标记修改
        /// </summary>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public ActionResult DOFEEBXBJ(string rid, string servicetypecode, string bxbj, string tcet084, string productcode)
        {
            bool rtnflag = true;
            string rtnmsg = "操作成功";
            QDM_FEE_SRV_REF targetobj = new QDM_FEE_SRV_REF();
            try
            {
                //var srcobj = QDM_FEE_SRV_REF.TryFind(rid);
                //if (srcobj != null)
                //{
                //    srcobj.BXBJ = bxbj;
                //    targetobj = srcobj;
                //    targetobj.DoUpdate();
                //}
                string srcobjSql = string.Format("select * from QDM_FEE_SRV_REF where PRODUCTCODE='{0}' and SERVICETYPECODE='{1}' and FEECODE='{2}'", productcode, servicetypecode, tcet084);
                DataTable dt =DataHelper.QueryDataTable(srcobjSql);
                if (dt.Rows.Count > 0)
                {
                    string update = string.Format("update QDM_FEE_SRV_REF set BXBJ='{3}' where PRODUCTCODE='{0}' and SERVICETYPECODE='{1}' and FEECODE='{2}'", productcode, servicetypecode, tcet084, bxbj);
                    DataHelper.ExecSql(update);
                }
                else
                {
                    targetobj.RID = rid;
                    targetobj.BXBJ = bxbj;
                    targetobj.FeeCode = tcet084;
                    targetobj.ServiceTypeCode = servicetypecode;
                    targetobj.ProductCode = productcode;
                    targetobj.DoCreate();
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj, Code = "1", Message = rtnmsg }.ToString());

        }
        /// <summary>
        /// 维护排序字段
        /// </summary>
        /// <param name="prdkey"></param>
        /// <param name="SORD"></param>
        /// <returns></returns>
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]

        public ActionResult SAVESORT(string prdkey,string postdata_ext)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";

            SQM_PRD_EXT targetobj_ext = null;
            SQM_PRD_EXT srcobj_ext = null;

            try
            {
                srcobj_ext = JsonHelper.GetObject<SQM_PRD_EXT>(postdata_ext);
                targetobj_ext = SQM_PRD_EXT.TryFind(prdkey);
                DataHelper.MergeData<SQM_PRD_EXT>(targetobj_ext, srcobj_ext);
                targetobj_ext.DoUpdate();

            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj_ext, Code = "1", Message = rtnmsg }.ToString());
        }
        /// <summary>
        /// 通过主键查找SQM_PRD_EXT数据
        /// </summary>
        /// <returns></returns>
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult FindByPK(string prdkey)
        {
            bool rtnflag = true;
            string rtnmsg = "查询成功";
            SQM_PRD_EXT targetobj_ext = null;
            try
            {
                targetobj_ext = SQM_PRD_EXT.TryFind(prdkey);
                if(targetobj_ext == null)
                {
                    SQM_PRD_EXT obj_ext = new SQM_PRD_EXT();
                    obj_ext.PRODUCTKEY = prdkey;
                    obj_ext.DoCreate();
                }
                targetobj_ext = SQM_PRD_EXT.TryFind(prdkey);
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = targetobj_ext, Code = "1", Message = rtnmsg }.ToString());
        }


        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string sql;
            //查询条件拼接
            string wherestr = "";
            var status = Request["STATUS"].ToString();
            var businessorg = Request["BUSINESSORG"].ToString();
            var productmanagername = Request["PRODUCTMANAGERNAME"].ToString();
            var createdates = Request["CreateDateS"].ToString();
            var createdatee = Request["CreateDateE"].ToString();
            var sqproductname = Request["SQPRODUCTNAME"].ToString();
            if (status != "")
            {
                wherestr += "AND SQM_PRD_EXT.STATUS = '" + status + "'";
            }
            if (businessorg != "")
            {
                wherestr += "AND SQM_PRD_EXT.BUSINESSORG = '" + businessorg + "'";
            }
            if (productmanagername != "")
            {
                wherestr += "AND SQM_PRD_EXT.PRODUCTMANAGERNAME = '" + productmanagername + "'";
            }
            if (sqproductname != "")
            {
                wherestr += "AND SQM_PRD_EXT.SQPRODUCTNAME LIKE '%" + sqproductname  + "%'";
            }
            if(createdates != "")
            {
                wherestr += "AND MDM_PRODUCT.CREATETIME >= to_date('" + createdates + "  00:00:00', 'yyyy-mm-dd hh24:mi:ss')";
            }
            if(createdatee != "")
            {
                wherestr += "AND MDM_PRODUCT.CREATETIME <= to_date('" + createdatee + "  00:00:00', 'yyyy-mm-dd hh24:mi:ss')";
            }
            sql = string.Format("SELECT   MDM_PRODUCT.ZFFWZH, MDM_PRODUCT.PRODUCTKEY,MDM_PRODUCT.PRODUCTNAME,MDM_PRODUCT.CREATETIME,MDM_PRODUCT.MODIFYTIME,MDM_PRODUCT.MEMO,SQM_PRD_EXT.BUSINESSTYPE,SQM_PRD_EXT.BUSINESSORG,SQM_PRD_EXT.PRODUCTMANAGERID,SQM_PRD_EXT.PRODUCTMANAGERNAME,SQM_PRD_EXT.DEAGIRATE,SQM_PRD_EXT.SORD,SQM_PRD_EXT.STATUS,SQM_PRD_EXT.SQPRODUCTNAME FROM MDM_PRODUCT LEFT JOIN SQM_PRD_EXT ON MDM_PRODUCT.PRODUCTKEY = SQM_PRD_EXT.PRODUCTKEY  WHERE  MDM_PRODUCT.ZFFWZH = 'X' AND  MDM_PRODUCT.PRODUCTKEY LIKE '%{0}%'" + wherestr + "ORDER BY SQM_PRD_EXT.MODIFYTIME DESC Nulls Last", Request["PRODUCTKEY"].Trim(), Request["SQPRODUCTNAME"].Trim(), Request["sort"].Trim(), Request["order"].Trim());
            //设置分页
            string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //数据数量
            string countsql = string.Format("SELECT COUNT (*)  FROM MDM_PRODUCT LEFT JOIN SQM_PRD_EXT ON MDM_PRODUCT.PRODUCTKEY = SQM_PRD_EXT.PRODUCTKEY WHERE  MDM_PRODUCT.ZFFWZH = 'X' AND MDM_PRODUCT.PRODUCTKEY LIKE '%{0}%' " + wherestr, Request["PRODUCTKEY"].Trim(), Request["SQPRODUCTNAME"].Trim());
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
         
        }

        public ActionResult SyncPSF(string pcode)
        {
            bool rtnflag = true;
            string rtnmsg = "同步成功";

            try
            {
                BizTalk_RFC_Z2FM_RFC_MD_SERVICE_TCET_Orchestration_InboundSoapClient psfservice = new BizTalk_RFC_Z2FM_RFC_MD_SERVICE_TCET_Orchestration_InboundSoapClient();
                psfservice.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 180000);

                Z2FM_RFC_MD_SERVICE_TCET service_tcet = new Z2FM_RFC_MD_SERVICE_TCET();
                IT_IN inop = new IT_IN();
                inop.SIGN = "I";
                inop.OPTION = "EQ";
                inop.LOW = (pcode + "").ToUpper();
                inop.HIGH = "";

                service_tcet.Add(inop);

                Z2FM_RFC_MD_SERVICE_TCET_RES ret = psfservice.Exec(service_tcet);

                if (null == ret || ret.Count() == 0)
                {
                    rtnmsg = "未从TM获取到数据。";
                    return Content(new JsonMessage { Success = rtnflag, Data = null, Code = "1", Message = rtnmsg }.ToString());
                }

                //删除产品绑定服务
                MDM_PRD_SRV_REF[] psrefArrDEL = MDM_PRD_SRV_REF.FindAllByProperties(MDM_PRD_SRV_REF.Prop_ProductCode, pcode);
                List<string> psrvlist = new List<string>();
                foreach (var psrvref in psrefArrDEL)
                {
                    if (!psrvlist.Contains(psrvref.ServiceTypeCode))
                    {
                        psrvlist.Add(psrvref.ServiceTypeCode);
                    }
                    
                    psrvref.DoDelete();
                }
                //删除服务绑定费目
                foreach (var srvcode in psrvlist)
                {
                    MDM_SRV_FEE_REF[] sfrefArrDEL = MDM_SRV_FEE_REF.FindAllByProperties(MDM_SRV_FEE_REF.Prop_SRVRQCD121, srvcode);
                    foreach (var sfee in sfrefArrDEL)
                    {
                        sfee.DoDelete();
                    }
                }

                // 绑定产品服务费目关系
                foreach (var ot in ret)
                {
                    //ot.
                    string prd = ot.SERVICE_PRODUCT_ID;
                    string srv = ot.SERVICE_TYPE;
                    string fee = ot.TCET084;

                    MDM_PRD_SRV_REF[] psrefArr = MDM_PRD_SRV_REF.FindAllByProperties(MDM_PRD_SRV_REF.Prop_ProductCode, prd, MDM_PRD_SRV_REF.Prop_ServiceTypeCode, srv);

                    if (psrefArr == null || psrefArr.Count() == 0)
                    {
                        MDM_PRD_SRV_REF psref = new MDM_PRD_SRV_REF();
                        psref.ProductCode = prd;
                        psref.ServiceTypeCode = srv;
                        psref.DoCreate();
                    }

                    MDM_SRV_FEE_REF[] sfrefArr = MDM_SRV_FEE_REF.FindAllByProperties(MDM_SRV_FEE_REF.Prop_SRVRQCD121, srv, MDM_SRV_FEE_REF.Prop_TCET084, fee);

                    if (sfrefArr == null || sfrefArr.Count() == 0)
                    {
                        MDM_SRV_FEE_REF sfref = new MDM_SRV_FEE_REF();
                        sfref.SRVRQCD121 = srv;
                        sfref.TCET084 = fee;
                        sfref.CREATETIME = DateTime.Now;
                        sfref.DoCreate();
                    }
                    
                }

                return Content(new JsonMessage { Success = rtnflag, Data = ret, Code = "1", Message = rtnmsg }.ToString());
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Data = pcode, Code = "1", Message = rtnmsg }.ToString());

        }
        //删除
        public ActionResult dodelete(string PROKEY)
        {
            bool rtnflag = true;
            string rtnmsg = "删除成功";
            try
            {
                string sql = string.Format("select * from MDM_PRODUCT mp left join sqm_dj_psf sdp on sdp.prdcode=mp.productkey where sdp.prdcode='{0}'",PROKEY);
                var count = DataHelper.QueryDataTable(sql);
                if (count.Rows.Count > 1)
                {
                    rtnmsg ="产品已有定价，不能删除";
                }
                else
                {
                    MDM_PRODUCT mpt = MDM_PRODUCT.TryFind(PROKEY);
                    mpt.ZFFWZH = "";
                    mpt.DoDelete2();
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
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QM_ProductList()
        {
            return View("QM_ProductList");
        }
        
        /// <summary>
        /// 产品列表显示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="RID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetData_ProductList(string id = "", string RID = "")
        {
            try
            {
                var treeID = id;//点击节点后传入参数
                string whereStr = "";
                int PageIndex = String.IsNullOrEmpty(Request.Params.Get("page")) ? 0 : int.Parse(Request.Params.Get("page"));
                int PageSize = String.IsNullOrEmpty(Request.Params.Get("rows")) ? 0 : int.Parse(Request.Params.Get("rows"));

                string[] Keys = { "PRODUCTKEY", "SQPRODUCTNAME", "BUSINESSORG", "PRODUCTMANAGERNAME", "Status" }; 
                
                string psSql = String.Empty;
                if (!String.IsNullOrEmpty(treeID) && treeID.IndexOf("#") > 0)//获取子节点
                {
                    var treeIDs = treeID.Split('#');
                    var level = treeIDs[0];
                    switch (level)
                    {
                        //2级节点：服务
                        case "1":
                            {
                                #region 服务数据源
                                string srvsql = @"select 
                                ms.servicetype,
                                ms.servicename,
                                ms.status,
                                ms.memo 
                                from mdm_service ms
                                left join mdm_prd_srv_ref mpsr
                                on mpsr.servicetypecode=ms.servicetype
                                where mpsr.productcode='{0}'
                                and ms.servicename is not null
                                order by ms.createtime desc ";
                                srvsql = string.Format(srvsql, treeIDs[1]);
                                #endregion
                                //设置分页
                                string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
                                sql_page = string.Format(sql_page, srvsql, (PageIndex - 1) * PageSize + 1, (PageIndex - 1) * PageSize + PageSize);
                                DataTable data = DataHelper.QueryDataTable(srvsql);
                                var rows = data.AsEnumerable().Select(n => new
                                {
                                    state = "closed",
                                    _parentId = "1#" + treeIDs[1],
                                    _treeID = "2#" + (String.IsNullOrEmpty(n["servicetype"].ToString()) ? "empty" : n["servicetype"]) + "#1#" + treeIDs[1],
                                    Product = n["servicename"],
                                    code = n["servicetype"],
                                    org = "-",
                                    manager = "-",
                                    status = n["status"],
                                    memo = n["memo"]
                                }).ToList();
                                return Json(new { rows = rows }, JsonRequestBehavior.AllowGet);
                            }
                        case "2":
                            {
                                //三级节点：
                                #region 费目数据源
                                string feesql = @"-- 服务下的费目数据
                                select 
                                mf.tcet084 feecode,
                                mf.textdesc feename,
                                mf.status,
                                mf.memo
                                from 
                                v_mdm_fee mf
                                left join mdm_srv_fee_ref msfr
                                on msfr.tcet084 = mf.tcet084
                                where mf.tcet084 is not null
                                and msfr.srvrqcd121='{0}'
                                order by mf.tcet084 asc";
                                #endregion
                                //2#O00003#1#T0728-PRO01
                                feesql = string.Format(feesql, treeIDs[1]);
                                //设置分页
                                string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
                                sql_page = string.Format(sql_page, feesql, (PageIndex - 1) * PageSize + 1, (PageIndex - 1) * PageSize + PageSize);
                                DataTable data_prod = DataHelper.QueryDataTable(feesql);
                                var rows = data_prod.AsEnumerable().Select(n => new
                                {
                                    state = "closed",
                                    _parentId = "2#" + treeIDs[1] + "#1#" + treeIDs[3],
                                    _treeID = "3#" + (String.IsNullOrEmpty(n["feecode"].ToString()) ? "empty" : n["feecode"]) + "#2#" + treeIDs[3] + "#1#" + treeIDs[1],
                                    Product = n["feename"],
                                    code = n["feecode"],
                                    org = "-",
                                    manager = "-",
                                    status = n["status"],
                                    memo = n["memo"]
                                }).ToList();
                                return Json(new { rows = rows }, JsonRequestBehavior.AllowGet);
                            }
                        default:
                            return Json(new { rows = "" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else//id=Null
                {
                    if (!string.IsNullOrEmpty(Request["PRODUCTKEY"].ToString()))
                        whereStr += " and spe.productkey like '%"+ Request["PRODUCTKEY"].ToString() + "%'";
                    if(!string.IsNullOrEmpty(Request["SQPRODUCTNAME"].ToString()))
                        whereStr += " and spe.sqproductname like '%" + Request["SQPRODUCTNAME"].ToString() + "%'";
                    if(!string.IsNullOrEmpty(Request["BUSINESSORG"].ToString()))
                        whereStr += " and spe.businessorg ='" + Request["BUSINESSORG"].ToString() + "'";
                    if (!string.IsNullOrEmpty(Request["PRODUCTMANAGERNAME"].ToString()))
                        whereStr += " and spe.productmanagername like '%" + Request["PRODUCTMANAGERNAME"].ToString() + "%'";
                    if (!string.IsNullOrEmpty(Request["Status"].ToString()))
                        whereStr += " and spe.Status ='" + Request["Status"].ToString() + "'";
                    #region 默认加载产品数据源
                    psSql = @"select
                                spe.productkey, --代码
                                spe.businessorg, --事业部
                                spe.sqproductname, --产品名
                                spe.productmanagername,--产品经理
                                spe.status,--状态
                                spe.memo--备注
                                from sqm_prd_ext spe
                                left join mdm_product mp
                                on spe.productkey=mp.productkey and mp.zffwzh='X'
                                where  spe.sqproductname is not null and spe.status='1' {0} 
                                order by spe.createtime desc,spe.productkey asc";
                    #endregion
                    psSql = string.Format(psSql, whereStr);
                    //设置分页
                    string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
                    sql_page = string.Format(sql_page, psSql, (PageIndex - 1) * PageSize + 1, (PageIndex - 1) * PageSize + PageSize);
                    DataTable data = DataHelper.QueryDataTable(sql_page);

                    string sql_count = string.Format("select count(1) as COUNT from ({0})", psSql);
                    DataTable data_count = DataHelper.QueryDataTable(sql_count);
                    int total = data_count.AsEnumerable().Select(n => new {
                        COUNT = int.Parse(n["COUNT"].ToString())
                    }).Sum(n => n.COUNT);

                    var rows = data.AsEnumerable().Select(n => new
                    {
                        state = "closed",
                        _treeID = "1#" + n["productkey"],
                        Product = n["sqproductname"],
                        code = n["productkey"],
                        org = n["businessorg"],
                        manager = n["productmanagername"],
                        status = n["status"],
                        memo = n["memo"]
                    }).ToList();
                    return Json(new { rows = rows, total = total }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { title = "", errorMsg = "查询出错了，请联系管理员!" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
    
}
