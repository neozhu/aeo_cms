using Aim;
using Aim.Data;
using Aim.Portal.Web;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Oncontrol3.Web.Controllers
{
    public class Mobel_DJController : BaseController
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string token)
        {
            string[] tmpArr = { str, token };            //Array.Sort(tmpArr);            
            string tmpStr = String.Join("", tmpArr);
            return (FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "MD5").ToUpper());
        }
        public ActionResult Index()
        {
            string RID = Request["RID"] + "";
            if (!string.IsNullOrEmpty(RID))
            {
                string[] tmpArr = { RID };
                Array.Sort(tmpArr);
                string tmpStr = String.Join("", tmpArr);
                string hash = Encrypt(tmpStr, System.Configuration.ConfigurationManager.AppSettings["SSOTOKEN"]);
                SSOService login = new SSOService();
                login.Url = System.Configuration.ConfigurationManager.AppSettings["SSOURL"];
                SSO_LOGIN user = login.GetLoginInfo(hash);
                if (user == null)
                {
                    return View("Index");
                }
                else
                {
                    FLD_QO_USER qousernew = new FLD_QO_USER();
                    FLD_QO_USER qouserold = SessionHelper.GetSessionUser<FLD_QO_USER>();
                    qousernew.staffkey = user.STAFFKEY;
                    if (qouserold.staffkey != qousernew.staffkey
                        && !string.IsNullOrEmpty(qousernew.staffkey))
                    {
                        SessionHelper.AddSessionUser<FLD_QO_USER>(qousernew);
                        CookieHelper.SetCookie(qousernew.staffkey);
                    }
                }
            }

            return View("Index");
        }

        public ActionResult DJIndex()
        {
            ViewBag.org = Request["org"];
            return View("DJIndex");
        }

        /// <summary>
        /// 服务页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SrvIndex()
        {
            string prdcode = Request["prdcode"];
            ViewBag.prdcode = prdcode;
            return View("SrvIndex");
        }
        public ActionResult FeeIndex()
        {
            //string feecode = Request["feecode"];
            ViewBag.feecode = Request["feecode"];
            ViewBag.srvcode = Request["srvcode"];
            ViewBag.prdcode = Request["prdcode"];
            ViewBag.djrid = Request["djrid"];
            string feename = DataHelper.QueryValue("select feename from sqm_dj_psf where rid='" + Request["djrid"] + "'")+"";
            ViewBag.feename = feename;
            return View("FeeIndex");
        }

        /// <summary>
        /// 当前产品下的服务，费目列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Srvlist()
        {
            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string prodcode = Request["prdcode"];
            try
            {
                if (string.IsNullOrEmpty(prodcode))
                    throw new Exception("未获取到产品代号");
                #region sql
                string srvSql = @"
SELECT distinct 
                spe.PRODUCTKEY    as PRODUCTCODE,
                ms.SERVICETYPE    as SERVICETYPE,
                ms.SERVICENAME    as SRVNAME,
                spe.SQPRODUCTNAME as PRDNAME
  FROM SQM_PRD_EXT spe
  LEFT JOIN MDM_PRD_SRV_REF mpsr
    ON spe.PRODUCTKEY = mpsr.PRODUCTCODE
  LEFT JOIN MDM_SERVICE ms
    ON mpsr.SERVICETYPECODE = ms.SERVICETYPE
  LEFT JOIN MDM_SRV_FEE_REF msfr
    ON mpsr.SERVICETYPECODE = msfr.SRVRQCD121
  LEFT JOIN V_MDM_FEE mf
    ON msfr.TCET084 = mf.TCET084
  LEFT JOIN SQM_DJ_PSF sdp
    ON spe.PRODUCTKEY = sdp.prdcode
   and ms.SERVICETYPE = sdp.srvcode
   and mf.TCET084 = sdp.feecode
   and sdp.DJFS is not null
 WHERE spe.STATUS = '1'
   and mf.TEXTDESC is not null
   and sdp.prdcode = '{0}'
";
                srvSql = string.Format(srvSql, prodcode);
                string feeSql = @"
SELECT distinct 
                spe.PRODUCTKEY    as PRODUCTCODE,
                spe.SQPRODUCTNAME as PRDNAME,
                ms.SERVICETYPE    as SERVICETYPE,
                ms.SERVICENAME    as SRVNAME,
                mf.TCET084        as FEECODE,
                mf.TEXTDESC       as FEENAME,
               -- sdp.ORGNAME,
                listagg(to_char( sdp.orgrid),'|') within group(order by sdp.orgrid)
                OVER (PARTITION BY mf.TCET084) AS orgrids,
                listagg(to_char(sdp.rid),'|') within group(order by sdp.orgrid)  --定价表的rid 
                over (Partition by mf.TCET084) as djrids,
                spe.BUSINESSORG,
                spe.STATUS,
                sdp.DJFS
  FROM SQM_PRD_EXT spe
  LEFT JOIN MDM_PRD_SRV_REF mpsr
    ON spe.PRODUCTKEY = mpsr.PRODUCTCODE
  LEFT JOIN MDM_SERVICE ms
    ON mpsr.SERVICETYPECODE = ms.SERVICETYPE
  LEFT JOIN MDM_SRV_FEE_REF msfr
    ON mpsr.SERVICETYPECODE = msfr.SRVRQCD121
  LEFT JOIN V_MDM_FEE mf
    ON msfr.TCET084 = mf.TCET084
  LEFT JOIN SQM_DJ_PSF sdp
    ON spe.PRODUCTKEY = sdp.prdcode
   and ms.SERVICETYPE = sdp.srvcode
   and mf.TCET084 = sdp.feecode
   and sdp.DJFS is not null
 WHERE spe.STATUS = '1'
   and mf.TEXTDESC is not null
   and sdp.prdcode='{0}'
";
                feeSql = string.Format(feeSql, prodcode);
                #endregion
                // [PRODUCTCODE,SERVICETYPECODE,SERVICENAME,Sord]
                var srvArray = DataHelper.QueryObjectsList(srvSql);
                // [pN,pc,sN,sC,fN,fC,orgjds,djrids]
                var feeArray = DataHelper.QueryObjectsList(feeSql);
                object[] obj = { srvArray, feeArray };
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 废
        /// 当前产品下的服务、费目列表  
        /// </summary>
        /// <returns></returns>
        public ActionResult SrvList2()
        {
            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string prdcode = Request["prdcode"];
            //if (!string.IsNullOrEmpty(prdcode)) {
            //    return null;
            //}
            try
            {
                string sql1 = @"
select distinct t1.prodcode as PRODUCTCODE ,t1.srvcode as SERVICETYPECODE ,t1.srvname as SERVICENAME
from sqm_srv_fee_config t1
right join sqm_dj_psf t2
on t1.prodcode=t2.prdcode
where t1.srvcode=t2.srvcode
and t1.feecode=t2.feecode 
and t2.status='1'  and t2.orgrid is not null and t1.prodcode = '{0}' order by t1.srvname
";
//select distinct prodcode as PRODUCTCODE ,srvcode as SERVICETYPECODE ,srvname as SERVICENAME
//from sqm_srv_fee_config where prodcode = '{0}' order by SERVICETYPECODE
                  sql1 = string.Format(sql1, prdcode);
                //sql1 = string.Format(sql1, "AA07");
                var srvArray = DataHelper.QueryObjectsList(sql1);// [PRODUCTCODE,SERVICETYPECODE,SERVICENAME,Sord]
               
                //select * from sqm_srv_fee_config t1 where t1.prodcode = 'AA07' and t1.srvcode='A00004' and t1.feecode='ALDYSFJG'
                //相同产品 - 服务 - 费目的同一组织只能有一个定价 ---->src: SQM_PUR: PurIndex: 487
                string sql2 = @"
select distinct t1.prodcode,
t1.prodname ,
t1.srvcode ,
t1.srvname ,
t1.feecode ,
t1.feename ,
listagg(to_char( t2.orgrid),'|') within group(order by t2.orgrid) 
OVER (PARTITION BY t1.feecode) AS orgrids,
listagg(to_char(t2.rid),'|') within group(order by t2.orgrid)  --定价表的rid 
over (Partition by t1.feecode) as djrids
from sqm_srv_fee_config t1
right join sqm_dj_psf t2
on t1.prodcode=t2.prdcode
where t1.srvcode=t2.srvcode
and t1.feecode=t2.feecode 
and t2.status='1'  and t2.orgrid is not null and t1.prodcode = '{0}' order by t1.feename
";
                sql2 = string.Format(sql2, prdcode);
                //sql2 = string.Format(sql2, "AA07");
                var feeArray = DataHelper.QueryObjectsList(sql2);// [productcode,sqproductname,servicetypecode,servicename,tcet084,textdesc,bxbj,sorid]
                object[] data = { srvArray, feeArray };
                return Content(JsonHelper.GetJsonString(data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        public string ProdList()
        {
            //事业部
            string mandt = Request["mandt"];
            string user = Request["user"];
            string prdname = Request["prdname"];
            try
            {
                //string sql = "select distinct prdname,prdcode,businessorg from sqm_dj_psf  where createuser='{0}' ";
                string sql = @"
select distinct t1.prodcode as PRODUCTKEY ,t1.prodname as SQPRODUCTNAME,t2.BUSINESSORG
from sqm_srv_fee_config t1
left join sqm_prd_ext t2
on t1.prodcode=t2.productkey
where PRODUCTKEY is not null
";
                //sql = string.Format(sql, user);
                if (!string.IsNullOrEmpty(mandt))
                {
                    sql += " and t2.BUSINESSORG='" + mandt + "'";
                }
                if (!string.IsNullOrEmpty(prdname))
                {
                    sql += " and SQPRODUCTNAME like '%" + prdname + "%'";
                }
                sql += " order by t2.BUSINESSORG";
                DataTable dt = DataHelper.QueryDataTable(sql);
                return JsonHelper.GetJsonString(dt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 当前费目下的定价方式
        /// </summary>
        /// <returns></returns>
        public ActionResult DJFSList()
        {
            string prdcode = Request["prdcode"];
            string srvcode = Request["srvcode"];
            string feecode = Request["feecode"];
            string djrid = Request["djrid"];
            try
            {
                
                string sql = @"With DATASET AS(
                           select sfc.RID from SQM_FEE_CALC sfc 
                           left join SQM_DJ_PSF sdf on sfc.FEECODE=sdf.FEECODE
                           where sdf.RID='" + djrid + "') select distinct sfpr.DJFSRID,sfpr.DJFSNAME,sfpr.FSSORT from DATASET t1 left join SQM_FEE_PUR_REF sfpr on t1.RID=sfpr.feerid  and sfpr.STATUS='1' where DJFSRID is not null order by cast(sfpr.FSSORT as int) asc,sfpr.DJFSNAME asc";
                var djfsArray = DataHelper.QueryObjectsList(sql);
                return Content(JsonHelper.GetJsonString(djfsArray));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 当前产品下当前定价方式下的计算方式
        /// </summary>
        /// <returns></returns>
        public ActionResult PSFdetail()
        {
            string prdcode = Request["prdcode"];
            string feecode = Request["feecode"];
            string srvcode = Request["srvcode"];
            string djfsrid = Request["djfsrid"];
            string gdzkey = Request["gdzkey"];
            string gdzrid = Request["gdzrid"];
            string djrid = Request["djrid"];

            try
            {
                    string wheregdz = "";
                    DataTable gdzdt = null;
                    
                    //if (string.IsNullOrEmpty(gdzrid) && !string.IsNullOrEmpty(gdzkey)&& gdzkey != "" && gdzkey != "0")
                    if (gdzkey != "0")
                    {
                        //获取定价方式下的高低值
                        string gdzsql = @"select djfsrid,gdzrid,gdzkey,gdzname from sqm_fee_pur_ref where djfsrid='{0}' and feecode='{1}' and sqm_fee_pur_ref.status='1' ";
                        gdzdt = DataHelper.QueryDataTable(string.Format(gdzsql, djfsrid,feecode));
                    }
                    //高低值rid不为空
                    if (!string.IsNullOrEmpty(gdzrid))
                    {
                        wheregdz += " and gdzrid='" + gdzrid + "'";
                    }
                    //为空，默认中第一行
                    else if (gdzdt != null && gdzdt.Rows.Count > 0&&((gdzdt.Rows[0]["gdzkey"]+"")!="0"))
                    {
                        wheregdz += " and gdzrid='" + gdzdt.Rows[0]["GDZRID"] + "'";
                    }
                    else
                    {
                        wheregdz += " and (gdzrid='0' or gdzrid is null)";
                    }
                    //该费目的计算基础列表
                    string jsjcsql = @"select calccode,calcname,valcol from sqm_fee_calc_ref sfcr 
where sfcr.djfsrid='{0}' and sfcr.status='1'";
                    jsjcsql = string.Format(jsjcsql+wheregdz, djfsrid);
                    var jsjcArray = DataHelper.QueryObjectsList(jsjcsql);
                    //已经定价，查询MODEDJ_VAL表
                    string djvalSql = @"select smv.* from  sqm_modedj_val smv 
where smv.djfsrid='{0}' and smv.status='1' and smv.djstatus='1' and smv.feecalcid='{1}'";
                    djvalSql = string.Format(djvalSql+wheregdz, djfsrid,djrid);
                    DataTable valdt = DataHelper.QueryDataTable(djvalSql);
                    //ViewBag.valdt = valdt;
                    //var valArray = DataHelper.QueryObjectsList(djvalSql);
                    //结果
                    var obj = new { flag = true, jsjcArray = jsjcArray, valdt = valdt, gdzdt = gdzdt };
                    return Content(JsonHelper.GetJsonString(obj));
            }
            catch
            {
                throw;
            }

        }
    }
}