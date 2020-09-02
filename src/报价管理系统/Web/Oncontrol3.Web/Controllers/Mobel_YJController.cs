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
    public class Mobel_YJController : BaseController
    {

        /// <summary>
        /// 报价首页 查看商机信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OceanIndex()
        {
            return View();
        }
        public ActionResult OceanDetails()
        {
            return View();
        }

        public ActionResult AirIndex()
        {
            return View();
        }
        public ActionResult AirDetails()
        {
            return View();
        }

        //public ActionResult BpIndex() {
        //    ViewBag.bpcode = Request["bpcode"];
        //    return View("BPIndex");
        //}
        //public ActionResult SJIndex() {
        //    ViewBag.sjcode = Request["sjcode"];
        //    return View("SJIndex");

        //}
        //public ActionResult BJDeatil() {
        //    string bjcode = Request["bjcode"];
        //    return View("BJDeatil");
        //}
        #region 海运运价
        public ActionResult OceLists()
        {
            string sjcode = Request["sjcode"];
            int oldpage = int.Parse(Request["oldpage"]);
            int newpage = int.Parse(Request["newpage"]);
            string qsg = Request["qsg"];
            string mdg = Request["mdg"];
            string fp = Request["fp"];
            string dir = Request["dir"];
            string cgs = Request["cgs"];
            string khrr = Request["khrr"];

            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();

            try
            {
                string sql_where = "";
                string sql_order = "";
                if (fp == "1")
                {
                    sql_order += " order by GP20 desc";
                }
                else if (fp == "0")
                {
                    sql_order += " order by HC desc";
                }
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                if (qsg.Length > 0)
                {
                    sql_where += " and qyg like '%" + qsg + "%'";
                }
                if (mdg.Length > 0)
                {
                    sql_where += " and mdg like'%" + mdg + "%'";
                }
                if (dir.Length > 0)
                {
                    sql_where += "and ( zzg is null or zzg='') ";
                }
                if (cgs.Length > 0)
                {
                    sql_where += "and cgs='" + cgs + "'";
                }
                if (khrr.Length > 0)
                {
                    sql_where += "and khr like '%" + khrr + "%'";
                }
                //string sql = @"select a1.* from (select t.*,rownum rn from SQM_COST_HY t where rownum<=" + newpage + " "+ sql_where + " ) a1 where rn>" + oldpage + "";
                string sql = @"select a1.*
  from (select tt.*, rownum rn
          from (select t.* from SQM_COST_HY t where 1=1 " + sql_where + @") tt
         where rownum <= " + newpage + @") a1
 where rn > " + oldpage + " " + sql_order;
                //sql = string.Format(sql, user, sjcode);

                //sql = string.Format(sql,"admin",sjcode);
                var bjlist = DataHelper.QueryDataTable(sql);
                //var obj = new { li = bjlist, pagesizes = 3 };
                var obj = bjlist;
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch
            {
                throw;
            }
        }
        public ActionResult OcePage()
        {
            string sjcode = Request["sjcode"] + "";
            int oldpage = int.Parse(Request["oldpage"]);
            int newpage = int.Parse(Request["newpage"]);
            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string qsg = Request["qsg"] + "";
            string mdg = Request["mdg"] + "";
            string fp = Request["fp"] + "";
            string dir = Request["dir"] + "";
            string cgs = Request["cgs"] + "";
            string khrr = Request["khrr"] + "";
            try
            {
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                string sql_where = "";
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                if (qsg.Length > 0)
                {
                    sql_where += " and qyg like '%" + qsg + "%'";
                }
                if (mdg.Length > 0)
                {
                    sql_where += " and mdg like'%" + mdg + "%'";
                }
                if (dir.Length > 0)
                {
                    sql_where += "and ( zzg is null or zzg='') ";
                }
                if (cgs.Length > 0)
                {
                    sql_where += "and cgs='" + cgs + "'";
                }
                if (khrr.Length > 0)
                {
                    sql_where += "and khr like '%" + khrr + "%'";
                }
                string countsql = @"select count(*) from SQM_COST_HY where 1=1 " + sql_where;
                int pagesize = 0;
                int count = int.Parse(DataHelper.QueryValue(countsql).ToString());
                if (count % 20 != 0)
                {
                    pagesize = count / 20 + 1;
                }
                else
                {
                    pagesize = count / 20;
                }

                //sql = string.Format(sql, user, sjcode);

                //sql = string.Format(sql,"admin",sjcode);
                var obj = new { counts = count, pagesizes = pagesize };
                //var obj = 3;// pagesize;
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch
            {
                throw;
            }
        }
        public ActionResult OceCgslist()
        {
            string sql = @"select distinct CGS from SQM_COST_HY";
            var bjlist = DataHelper.QueryDataTable(sql);
            List<SelectModel> sm = new List<SelectModel>();
            for (int i = 0; i < bjlist.Rows.Count; i++)
            {
                SelectModel smm = new SelectModel();
                smm.text = bjlist.Rows[i]["CGS"].ToString();
                smm.value = bjlist.Rows[i]["CGS"].ToString();
                sm.Add(smm);
            }
            return Content(JsonHelper.GetJsonString(sm));
        }
        #endregion
        #region 空运运价
        public ActionResult AirLists()
        {
            string sjcode = Request["sjcode"];
            int oldpage = int.Parse(Request["oldpage"]);
            int newpage = int.Parse(Request["newpage"]);
            string qsg = Request["qsg"];
            string mdg = Request["mdg"];
            string fp = Request["fp"];
            string dir = Request["dir"];
            string HKGS = Request["HKGS"];
            string khrr = Request["khrr"];

            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();

            try
            {
                string sql_where = "";
                string sql_order = "";
                if (fp == "1")
                {
                    sql_order += " order by WEIGHTXY45MC desc";
                }
                else if (fp == "0")
                {
                    // sql_order += " order by HC desc";
                }
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                if (qsg.Length > 0)
                {
                    sql_where += " and qyg like '%" + qsg + "%'";
                }
                if (mdg.Length > 0)
                {
                    sql_where += " and mdg like'%" + mdg + "%'";
                }
                if (dir.Length > 0)
                {
                    sql_where += "and ( zzg is null or zzg='') ";
                }
                if (HKGS.Length > 0)
                {
                    sql_where += "and HKGS='" + HKGS + "'";
                }
                if (khrr.Length > 0)
                {
                    sql_where += "and khr like '%" + khrr + "%'";
                }
                //string sql = @"select a1.* from (select t.*,rownum rn from SQM_COST_HY t where rownum<=" + newpage + " "+ sql_where + " ) a1 where rn>" + oldpage + "";
                string sql = @"select a1.*
  from (select tt.*, rownum rn
          from (select t.* from SQM_COST_KYGJ t where 1=1 " + sql_where + @") tt
         where rownum <= " + newpage + @") a1
 where rn > " + oldpage + " " + sql_order;
                //sql = string.Format(sql, user, sjcode);

                //sql = string.Format(sql,"admin",sjcode);
                var bjlist = DataHelper.QueryDataTable(sql);
                //var obj = new { li = bjlist, pagesizes = 3 };
                var obj = bjlist;
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch
            {
                throw;
            }
        }
        public ActionResult AirPage()
        {
            string sjcode = Request["sjcode"];
            int oldpage = int.Parse(Request["oldpage"]);
            int newpage = int.Parse(Request["newpage"]);
            string user = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
            string qsg = Request["qsg"];
            string mdg = Request["mdg"];
            string fp = Request["fp"];
            string dir = Request["dir"];
            string HKGS = Request["HKGS"];
            string khrr = Request["khrr"];
            try
            {
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                string sql_where = "";
                oldpage = oldpage * 20;
                newpage = newpage * 20;
                if (qsg.Length > 0)
                {
                    sql_where += " and qyg like '%" + qsg + "%'";
                }
                if (mdg.Length > 0)
                {
                    sql_where += " and mdg like'%" + mdg + "%'";
                }
                if (dir.Length > 0)
                {
                    sql_where += "and ( zzg is null or zzg='') ";
                }
                if (HKGS.Length > 0)
                {
                    sql_where += "and HKGS='" + HKGS + "'";
                }
                if (khrr.Length > 0)
                {
                    sql_where += "and khr like '%" + khrr + "%'";
                }
                string countsql = @"select count(*) from SQM_COST_KYGJ where 1=1 " + sql_where;
                int pagesize = 0;
                int count = int.Parse(DataHelper.QueryValue(countsql).ToString());
                if (count % 20 != 0)
                {
                    pagesize = count / 20 + 1;
                }
                else
                {
                    pagesize = count / 20;
                }

                //sql = string.Format(sql, user, sjcode);

                //sql = string.Format(sql,"admin",sjcode);
                var obj = new { counts = count, pagesizes = pagesize };
                //var obj = 3;// pagesize;
                return Content(JsonHelper.GetJsonString(obj));
            }
            catch
            {
                throw;
            }
        }
        public ActionResult AirHkgslist()
        {
            string sql = @"select distinct HKGS from SQM_COST_KYGJ";
            var bjlist = DataHelper.QueryDataTable(sql);
            List<SelectModel> sm = new List<SelectModel>();
            for (int i = 0; i < bjlist.Rows.Count; i++)
            {
                SelectModel smm = new SelectModel();
                smm.text = bjlist.Rows[i]["HKGS"].ToString();
                smm.value = bjlist.Rows[i]["HKGS"].ToString();
                sm.Add(smm);
            }
            return Content(JsonHelper.GetJsonString(sm));
        }
        #endregion
        private class SelectModel
        {
            public string value { get; set; }
            public string text { get; set; }
        }
    }
}