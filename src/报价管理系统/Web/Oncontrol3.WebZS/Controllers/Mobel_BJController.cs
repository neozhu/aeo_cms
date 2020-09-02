using Aim;
using Aim.Data;
using Aim.Portal.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class Mobel_BJController:BaseController
    {

        /// <summary>
        /// 报价首页 查看商机信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BJIndex() {
            return View("BJIndex");
        }
        public ActionResult BpIndex() {
            ViewBag.bpcode = Request["bpcode"];
            return View("BPIndex");
        }
        public ActionResult SJIndex() {
            ViewBag.sjcode = Request["sjcode"];
            return View("SJIndex");

        }
        public ActionResult BJDeatil() {
            string bjcode = Request["bjcode"];
            return View("BJDeatil");
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BpList() {
            try
            {
                string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                //查询报价主表中存在的客户，
                string sql = @"select distinct sbp.bpname,sbp.bpcode from sqm_bj_main_basic sbmb 
left join sqm_bj_bp sbp
on sbp.mrid=sbmb.rid 
left join sqm_bj_biz sbiz
on sbiz.mrid=sbmb.rid where sbp.bpcode is not null and sbiz.bizid is not null and sbmb.createuser='{0}' ";
                string wherestr = "";
                string bpname = Request["bpname"];
                if (!string.IsNullOrEmpty(bpname))
                {
                    wherestr += "and sbp.bpname like '%{"+bpname+"}%'";
                }
                sql += wherestr + " order by sbp.bpcode";
                sql = string.Format(sql, user);
                //sql = string.Format(sql, "admin");
                var bpdt = DataHelper.QueryObjectsList(sql);
                return Content(JsonHelper.GetJsonString(bpdt));
            }
            catch {
                throw;
            }
            
        }

        /// <summary>
        /// 商机列表
        /// </summary>
        /// <returns></returns>
        public ActionResult SJList() {
            try {
                string user= Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                string bpcode = Request["bpcode"];
                string sql = @"select distinct sbiz.bizname,sbiz.bizid,sbp.bpname from sqm_bj_main_basic sbmb 
left join sqm_bj_bp sbp
on sbp.mrid=sbmb.rid
left join sqm_bj_biz sbiz
on sbiz.mrid = sbmb.rid 
where sbmb.createuser='{0}' and sbp.bpcode is not null and sbiz.bizid is not null 
and sbp.bpcode='{1}'";
                sql = string.Format(sql,user,bpcode);
                //sql = string.Format(sql,"admin",bpcode);
                var sjlist = DataHelper.QueryObjectsList(sql);
                return Content(JsonHelper.GetJsonString(sjlist));
            } catch
            {
                throw;
            }
        }

        /// <summary>
        /// 商机下对应的所有报价信息,
        /// </summary>
        /// <returns></returns>
        public ActionResult BJList() {
            string sjcode = Request["sjcode"];
            string user= Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            
            try {
                string sql = @"select  sbiz.bizname,sbmb.bjname,sbv.rid,sbv.zver,sbv.modifytime,sbv.mrid,sbv.status  from sqm_bj_main_basic sbmb 
left join sqm_bj_biz sbiz
on sbiz.mrid = sbmb.rid  and sbmb.createuser='{0}'
left join sqm_bj_ver sbv
on sbv.mrid=sbmb.rid
where sbiz.bizid='{1}' 
order by sbmb.bjname ,sbv.zver desc";
                sql = string.Format(sql,user,sjcode);
                //sql = string.Format(sql,"admin",sjcode);
                var bjlist = DataHelper.QueryObjectsList(sql);
                return Content(JsonHelper.GetJsonString(bjlist));
            } catch
            {
                throw;
            }
        }

    }
}