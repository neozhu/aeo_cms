using Aim;
using Aim.Data;
using Aim.Portal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class SQM_CPZSController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 通过主键得到产品、服务、费目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetAllByRid(string sybcode)
        {
            string businesstype = "";
            if (sybcode=="01")
            {
                businesstype = "空运";
            }
            else if (sybcode=="02")
            {
                businesstype = "海运";
            }
            else if (sybcode=="03")
            {
                businesstype = "供应链";
            }
            else if (sybcode == "04")
            {
                businesstype = "运输";
            }

            string sql = string.Format("SELECT  DISTINCT PRODUCTKEY,SQPRODUCTNAME FROM SQM_PRD_EXT WHERE BUSINESSORG = '{0}'", businesstype);
            var prdArray = DataHelper.QueryObjectsList(sql);
            string prdcodeStr = "";

            foreach (object[] item in prdArray)
            {
                prdcodeStr += "'" + item[0] + "',";
            }

            string sql2 = string.Format("SELECT MDM_PRD_SRV_REF.PRODUCTCODE,MDM_PRD_SRV_REF.SERVICETYPECODE,MDM_SERVICE.SERVICENAME FROM MDM_PRD_SRV_REF LEFT JOIN MDM_SERVICE ON MDM_PRD_SRV_REF.SERVICETYPECODE = MDM_SERVICE.SERVICETYPE WHERE MDM_PRD_SRV_REF.PRODUCTCODE IN ({0})", prdcodeStr.TrimEnd(','));
            var srvArray = DataHelper.QueryObjectsList(sql2);
            string feeStr = "";
            for (var i = 0; i < srvArray.Count; i++)
            {
                feeStr += "'" + srvArray[i][1] + "',";
            }
            feeStr = feeStr.TrimEnd(',');
            string sql3 = string.Format("select mdm_prd_srv_ref.productcode,sqm_prd_ext.sqproductname, mdm_prd_srv_ref.servicetypecode,mdm_service.servicename, mdm_srv_fee_ref.tcet084,mdm_fee.textdesc,qdm_fee_srv_ref.bxbj from mdm_prd_srv_ref left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype left join v_mdm_fee mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid where mdm_prd_srv_ref.productcode in ({0}) order by qdm_fee_srv_ref.SORID", prdcodeStr.TrimEnd(','));
            var feeArray = DataHelper.QueryObjectsList(sql3);
            object[] data = { prdArray, srvArray, feeArray };
            return Content(JsonHelper.GetJsonString(data));
        }
    }
}