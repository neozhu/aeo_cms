using System;
using Aim;
using Aim.Data;
using Aim.Portal.Web;
using System.Web.Mvc;
using System.Data;
using Com.Feiliks.QDM;
using Oncontrol3.Web.Helpers;

namespace Oncontrol3.Web.Controllers
{
    //[AuthorLogin]
    public class SQM_DL_PURController : BaseController
    {
        private string kygh = ConfigHelper.AppSettings("KYGH");
        private string hygh = ConfigHelper.AppSettings("HYGH");
        private string gylgh = ConfigHelper.AppSettings("GYLGH");
        private string ysgh = ConfigHelper.AppSettings("YSGH");
        private string workno = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
        //
        // GET: /SQM_DJ_PSF/
        public ActionResult Index()
        {
            //查询条件拼接
            string wherestr = "";
            string wheregh = " and (";
            //工号权限控制
            if (kygh.Contains(workno))
            {
                //wheregh += " FEECODE like 'A%' or ";
                wheregh += " TCET084 like 'A%' or ";
            }
            if (hygh.Contains(workno))
            {
                wheregh += " TCET084 like 'O%' or ";
            }
            if (gylgh.Contains(workno))
            {
                wheregh += " TCET084 like 'S%' or ";
            }
            if (ysgh.Contains(workno))
            {
                wheregh += " TCET084 like 'L%' or ";
            }
            if (kygh.Contains(workno) || hygh.Contains(workno) || gylgh.Contains(workno) || ysgh.Contains(workno))
            {
                wherestr += wheregh.TrimEnd(' ').TrimEnd('r').TrimEnd('o') + ")";
            }
            else
            {
                wherestr += " and 1=2 ";
            }
            string sql = @"select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 order by ltrim(OBJID,'0')";
            DataTable Orgdt = DataHelper.QueryDataTable(sql);
            sql = @"select TCET084,TEXTDESC from V_MDM_FEE f where not exists(select * from MDM_SRV_FEE_REF s where f.TCET084=s.TCET084)" + wherestr;
            //sql = @"select distinct FEECODE,FEENAME from SQM_SRV_FEE_CONFIG where PRODCODE is null and SRVCODE is null " + wherestr;
            DataTable Feedt = DataHelper.QueryDataTable(sql);
            ViewBag.OrgData = Orgdt;
            ViewBag.FeeData = Feedt;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        //配置表
//        public ActionResult Lists()
//        {
//            //查询条件拼接
//            string wherestr = "";
//            string wheregh = " and (";
//            //工号权限控制
//            if (kygh.Contains(workno))
//            {
//                wheregh += " A.FEECODE like 'A%' or ";
//            }
//            if (hygh.Contains(workno))
//            {
//                wheregh += " A.FEECODE like 'O%' or ";
//            }
//            if (gylgh.Contains(workno))
//            {
//                wheregh += " A.FEECODE like 'S%' or ";
//            }
//            if (ysgh.Contains(workno))
//            {
//                wheregh += " A.FEECODE like 'L%' or ";
//            }
//            if (kygh.Contains(workno) || hygh.Contains(workno) || gylgh.Contains(workno) || ysgh.Contains(workno))
//            {
//                wherestr += wheregh.TrimEnd(' ').TrimEnd('r').TrimEnd('o') + ")";
//            }
//            else
//            {
//                wherestr += " and 1=2 ";
//            }
//            var feecode = Request["FEECODE"].ToString();
//            var orgrid = Request["ORGRID"].ToString();
//            if (feecode != "")
//            {
//                wherestr += "AND A.FEECODE = '" + feecode + "'";
//            }
//            if (orgrid != "")
//            {
//                wherestr += "AND A.ORGRID like '%" + orgrid + "%'";
//            }
//            string sql_from = @" from V_MDM_FEE mf 
//                left join SQM_DJ_PSF sdp on  mf.TCET084=sdp.Feecode and sdp.DJFS is not null
//                where mf.TCET084 not in (select TCET084 from MDM_SRV_FEE_REF) and mf.TEXTDESC is not null ";
//            string sql_feild = @"SELECT distinct sdp.RID,mf.TCET084 as FEECODE,mf.TEXTDESC as FEENAME,sdp.DJFS,sdp.ORGNAME,sdp.ORGRID,sdp.MODIFYUSER,sdp.MODIFYTIME ";
//            string sql_order = @"ORDER BY case when sdp.MODIFYTIME is null then 0 else 1 end desc, sdp.MODIFYTIME desc";
//            string sql_page = string.Format(" WHERE RN between {0} and {1} ", (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
//            //设置分页
//            string sql = "With DATASET AS( select A.*,ROWNUM As RN from ({0}{1}{2}) A inner join SQM_SRV_FEE_CONFIG t2 on A.FEECODE=t2.feecode and t2.feecatg<>'2' where 1=1 {3}) select * from DATASET ";
//            sql = string.Format(sql, sql_feild, sql_from, sql_order, wherestr);
//            string sql_all = sql + sql_page;
//            //数据数量
//            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
//            var rtntotal = DataHelper.QueryValue(countsql);
//            var rtndata = DataHelper.QueryDataTable(sql_all);
//            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
//            return Content(JsonHelper.GetJsonString(obj));
//        }
        public ActionResult Lists()
        {
            //查询条件拼接
            string wherestr = "";
            string wheregh = " and (";
            //工号权限控制
            if (kygh.Contains(workno))
            {
                wheregh += " FEECODE like 'A%' or ";
            }
            if (hygh.Contains(workno))
            {
                wheregh += " FEECODE like 'O%' or ";
            }
            if (gylgh.Contains(workno))
            {
                wheregh += " FEECODE like 'S%' or ";
            }
            if (ysgh.Contains(workno))
            {
                wheregh += " FEECODE like 'L%' or ";
            }
            if (kygh.Contains(workno) || hygh.Contains(workno) || gylgh.Contains(workno) || ysgh.Contains(workno))
            {
                wherestr += wheregh.TrimEnd(' ').TrimEnd('r').TrimEnd('o') + ")";
            }
            else
            {
                wherestr += " and 1=2 ";
            }
            var feecode = Request["FEECODE"].ToString();
            var orgrid = Request["ORGRID"].ToString();
            if (feecode != "")
            {
                wherestr += "AND FEECODE = '" + feecode + "'";
            }
            if (orgrid != "")
            {
                wherestr += "AND ORGRID like '%" + orgrid + "%'";
            }
            string sql_from = @" from V_MDM_FEE mf 
                left join SQM_DJ_PSF sdp on  mf.TCET084=sdp.Feecode and sdp.DJFS is not null
                where not exists(select * from MDM_SRV_FEE_REF s where mf.TCET084=s.TCET084) and mf.TEXTDESC is not null ";
            string sql_feild = @"SELECT distinct sdp.RID,mf.TCET084 as FEECODE,mf.TEXTDESC as FEENAME,sdp.DJFS,sdp.ORGNAME,sdp.ORGRID,sdp.MODIFYUSER,sdp.MODIFYTIME ";
            string sql_order = @"ORDER BY case when sdp.MODIFYTIME is null then 0 else 1 end desc, sdp.MODIFYTIME desc";
            string sql_page = string.Format(" WHERE RN between {0} and {1} ", (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            //设置分页
            string sql = "With DATASET AS( select A.*,ROWNUM As RN from ({0}{1}{2}) A where 1=1 {3}) select * from DATASET ";
            sql = string.Format(sql, sql_feild, sql_from, sql_order, wherestr);
            string sql_all = sql + sql_page;
            //数据数量
            string countsql = string.Format("SELECT COUNT (*) from ({0})", sql);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_all);
            var obj = new { draw = Request["draw"], data = rtndata, recordsTotal = rtntotal, recordsFiltered = rtntotal };
            return Content(JsonHelper.GetJsonString(obj));
        }
        //
        // GET: /SQM_DJ_PSF/
        public ActionResult SaveDjPsf(SQM_DJ_PSF sdp)
        {
            bool rtnflag = true;
            string rtnmsg = "";
            try
            {
                string djrid = System.Guid.NewGuid().ToString();
                SQM_DJ_PSF sdpnew = SQM_DJ_PSF.FindFirstByProperties(SQM_DJ_PSF.Prop_FEECODE, sdp.FEECODE, SQM_DJ_PSF.Prop_ALONEFEE, "1");
                if (sdpnew != null)
                {
                    sdpnew.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdpnew.DoUpdate();
                    rtnmsg = sdpnew.RID;
                }
                else
                {
                    sdp.RID = djrid;
                    sdp.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    sdp.ALONEFEE = "1";//是否无绑定关系费目（1-是，0-否）
                    sdp.CREATESOURCE = "独立费目定价";
                    sdp.DoCreate();
                    rtnmsg = djrid;
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
    }
}

