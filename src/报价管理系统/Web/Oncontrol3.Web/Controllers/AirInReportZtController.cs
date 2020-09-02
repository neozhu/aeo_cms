using Aim;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Security;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class AirInReportController : BaseController
    {
        public ActionResult ZbgztIndex()
        {
            return View("ZbgztIndex");
        }

        public ActionResult AirinBgzt()//报关状态
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;
            string wherestr = "";

            string[] searchKeys = new string[] { "DBKEY",};
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    SearchCriterion.AddSearch(key, Request[key].Trim(), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (SearchCriterion.Searches.Searches.Count > 0)
            {
              string begindate = "";
                string enddate = "";

                var seachItem = SearchCriterion.Searches.Searches;

                foreach (var colStr in seachItem)
                {
                    if (colStr.PropertyName == "DBKEY")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(begindate))
                    {
                        wherestr += " and FWO_KEY='" + begindate + "'";
                    }
                }
            }

            #region 数据源
            string resultdatasql = @"select* from (select
case 
when bgexe.event_code = 'ZSGWJD' then '接单'
when bgexe.event_code = 'ZSDZZD' then '制单'
when bgexe.event_code = 'ZSDZSD' then '审单'
when bgexe.event_code = 'ZSDZSJ' then '输机'
when bgexe.event_code = 'ZSDZFS' then '发送'
when bgexe.event_code = 'ZSDZGJ' then '过机'
when bgexe.event_code = 'ZSHGXY' and zbg.zsfbgsd = '' and zbg.zsfbggd = '' then '查验'
when bgexe.event_code = 'ZSHGXY' and zbg.zsfbgsd = 'X' and zbg.zsfbggd = '' then '删单'
when bgexe.event_code = 'ZSHGXY' and zbg.zsfbggd = 'X' then '改单'
when bgexe.event_code = 'ZSSWFX' then '放行'
when bgexe.event_code = 'ZSJGZT' then '结关'
else '无信息' end as zbgzt,
bgexe.actual_date,
bgtd_srv.root_key as fwo_key,
bgtd_srv.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as bgexe
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as foo on bgexe.parent_key = foo.db_key and foo.tor_type ='SOBG' and bgexe.mandt = foo.mandt
inner join SAPABAP1.z1t_zbgfw as zbg on zbg.parent_key = foo.db_key and zbg.zgqsx = '02' and zbg.mandt = foo.mandt
inner join SAPABAP1.Z1T_TD_SRV_TYPE as bgtd_srv on bgtd_srv.ref_foo_id = foo.tor_id and foo.tor_id <> '' and bgtd_srv.mandt = foo.mandt
where bgexe.event_code in ('ZSGWJD','ZSDZZD','ZSDZSD','ZSDZSJ','ZSDZFS','ZSDZGJ','ZSHGXY','ZSSWFX','ZSJGZT')
) where 1=1" + wherestr;

            #endregion
            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 ";

            List<Hashtable> htlist = HanaConectionHelper.LoadListWithPage(PageIndex, PageSize, resultdatasql, resultdatacountsql, "TM", out totalRows, out totalPages, true);


            DataTable dt = new DataTable();

            if (htlist.Count > 0)
            {
                foreach (string name in htlist[0].Keys)
                    dt.Columns.Add(name);
                
                foreach (Hashtable item in htlist)
                
                   dt.Rows.Add(new ArrayList(item.Values).ToArray());

            }
            //由于hashtable直接json的时候，时间和重量等数据吐到前面全部为空，所以这里，先转DT。
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = totalRows, recordsFiltered = totalRows };
            return Content(JsonHelper.GetJsonString(obj));
        }


        public ActionResult ZbjztIndex()//报检状态
        {
            return View("ZbjztIndex");
        }

        public ActionResult AirinBjzt()//报检状态
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;
            string wherestr = "";

            string[] searchKeys = new string[] { "DBKEY", };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    SearchCriterion.AddSearch(key, Request[key].Trim(), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (SearchCriterion.Searches.Searches.Count > 0)
            {
                string begindate = "";
                string enddate = "";

                var seachItem = SearchCriterion.Searches.Searches;

                foreach (var colStr in seachItem)
                {
                    if (colStr.PropertyName == "DBKEY")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(begindate))
                    {
                        wherestr += " and FWO_KEY='" + begindate + "'";
                    }
                }
            }

            #region 数据源
            string resultdatasql = @"select* from (select
ev_tyt.description_s as zbjzt,
bjexe.actual_date,
bjtd_srv.root_key as fwo_key,
bjexe.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as bjexe
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as foo on bjexe.parent_key = foo.db_key and foo.tor_type ='SOBJ' and bjexe.mandt = foo.mandt
inner join SAPABAP1.Z1T_TD_SRV_TYPE as bjtd_srv on bjtd_srv.ref_foo_id = foo.tor_id and foo.tor_id <> '' and bjtd_srv.mandt = foo.mandt
inner join SAPABAP1.""/SCMTMS/C_EV_TYT"" as ev_tyt on ev_tyt.tor_event = bjexe.event_code and ev_tyt.mandt = bjexe.mandt and ev_tyt.langu ='1'
where bjexe.event_code in ('ZSDZZD','ZSDZSJ','ZSDZFS','ZSSJFX') 

) where 1=1"+ wherestr;

            #endregion
            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 ";

            List<Hashtable> htlist = HanaConectionHelper.LoadListWithPage(PageIndex, PageSize, resultdatasql, resultdatacountsql, "TM", out totalRows, out totalPages, true);

            DataTable dt = new DataTable();

            if (htlist.Count > 0)
            {
                foreach (string name in htlist[0].Keys)
                    dt.Columns.Add(name);

                foreach (Hashtable item in htlist)

                    dt.Rows.Add(new ArrayList(item.Values).ToArray());

            }
            //由于hashtable直接json的时候，时间和重量等数据吐到前面全部为空，所以这里，先转DT。
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = totalRows, recordsFiltered = totalRows };
            return Content(JsonHelper.GetJsonString(obj));
        }

        public ActionResult ZwlztIndex()//物流状态
        {
            return View("ZwlztIndex");
        }

        public ActionResult AirinWlzt()//物流状态
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;
            string wherestr = "";
            string[] searchKeys = new string[] { "DBKEY", };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    SearchCriterion.AddSearch(key, Request[key].Trim(), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (SearchCriterion.Searches.Searches.Count > 0)
            {
                string begindate = "";
                string enddate = "";

                var seachItem = SearchCriterion.Searches.Searches;

                foreach (var colStr in seachItem)
                {
                    if (colStr.PropertyName == "DBKEY")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(begindate))
                    {
                        wherestr += " and FWO_KEY='" + begindate + "'";
                    }
                }
            }

            #region 数据源
            string resultdatasql = @"select* from (select
case event_code
when 'ZSCD' then  '抽单' 
when 'ZZCH' then  '车号' 
when 'ZFLIKJGQA' then  '已加封' 
when 'ZFJRJGQ' then  '运输完成' 
when 'ZFXHQS' then  '已配送' 
when 'ZFPOD' then  'POD' 
else '未抽单' end as zwlzt,
actual_date,
fwo_key,
mandt
from
(
select torexe.event_code, max(torexe.ACTUAL_DATE) as actual_date, fwo.db_key as fwo_key,fwo.mandt
from SAPABAP1.""/SCMTMS/D_TRQROT"" as fwo
inner join SAPABAP1.""/SCMTMS/D_TRQITM"" as fwo_itm on fwo.db_key = fwo_itm.parent_key and fwo.mandt = fwo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fu_itm on fwo_itm.db_key = fu_itm.ref_trq_item_key and fwo_itm.mandt = fu_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTP"" as fu_stp on fu_itm.parent_key = fu_stp.parent_key and fu_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTS"" as stp_itm on fu_stp.db_key = stp_itm.parent_key and stp_itm.stage_type in ('Z13','Z23','Z25','Z27') and stp_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fo_itm on fo_itm.db_key = fu_stp.assgn_item_key and fu_stp.mandt = fo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as fo_rot on fo_rot.db_key = fo_itm.parent_key and fo_rot.tor_cat = 'TO' and fo_itm.mandt = fo_rot.mandt
inner join SAPABAP1.""/SCMTMS/D_TOREXE"" as torexe on torexe.parent_key = fo_rot.db_key and torexe.mandt = fo_rot.mandt
and torexe.event_code in ('ZFXHQS','ZFPOD') and torexe.ACTUAL_DATE is not null 
where fwo.trq_type like 'AI%%' 
group by fwo.db_key ,fwo.mandt,torexe.event_code

union
select 
case 
when aa.event_code <> '' then aa.event_code
else 'ZZCH' end as event_code,aa.actual_date,
aa.fwo_key,aa.mandt
from
(
select foexe.event_code,foexe.actual_date,fo.fwo_key,fo.mandt,toritm.vehicleres_id as zch
from
(
select min(fo_rot.tor_id) as fo,fwo.db_key as fwo_key,fwo.mandt
from SAPABAP1.""/SCMTMS/D_TRQROT"" as fwo
inner join SAPABAP1.""/SCMTMS/D_TRQITM"" as fwo_itm on fwo.db_key = fwo_itm.parent_key and fwo.mandt = fwo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fu_itm on fwo_itm.db_key = fu_itm.ref_trq_item_key and fwo_itm.mandt = fu_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTP"" as fu_stp on fu_itm.parent_key = fu_stp.parent_key and fu_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTS"" as stp_itm on fu_stp.db_key = stp_itm.parent_key and stp_itm.stage_type in ('Z13','Z23','Z25','Z27') and stp_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fo_itm on fo_itm.db_key = fu_stp.assgn_item_key and fu_stp.mandt = fo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as fo_rot on fo_rot.db_key = fo_itm.parent_key and fo_rot.tor_cat = 'TO' and fo_itm.mandt = fo_rot.mandt
where fwo.trq_type like 'AI%%'
group by fwo.db_key,fwo.mandt
) as fo
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as torrot on torrot.tor_id = fo.fo and fo.mandt = torrot.mandt
left join SAPABAP1.""/SCMTMS/D_TORITE"" as toritm on toritm.parent_key = torrot.db_key and toritm.item_cat = 'AVR' and toritm.vehicleres_id <> '' and toritm.mandt = torrot.mandt
left join SAPABAP1.""/SCMTMS/D_TOREXE"" as foexe on foexe.parent_key = torrot.db_key
and foexe.ACTUAL_DATE is not null and foexe.mandt = torrot.mandt
and foexe.event_code in ('ZFLIKJGQA','ZFJRJGQ') 
) as aa where aa.event_code <> '' or aa.zch <> ''

union
select cdexe.event_code,cdexe.actual_date,cdtd_srv.root_key as fwo_key,cdexe.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as cdexe
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as foo on cdexe.parent_key = foo.db_key and foo.tor_type ='SOCD' and cdexe.mandt = foo.mandt
inner join SAPABAP1.Z1T_TD_SRV_TYPE as cdtd_srv on cdtd_srv.ref_foo_id = foo.tor_id and foo.tor_id <> '' and cdtd_srv.mandt = foo.mandt
where cdexe.event_code = 'ZSCD' 
)
) where 1=1"+wherestr;

            #endregion
            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 ";

            List<Hashtable> htlist = HanaConectionHelper.LoadListWithPage(PageIndex, PageSize, resultdatasql, resultdatacountsql, "TM", out totalRows, out totalPages, true);

            DataTable dt = new DataTable();

            if (htlist.Count > 0)
            {
                foreach (string name in htlist[0].Keys)
                    dt.Columns.Add(name);

                foreach (Hashtable item in htlist)

                    dt.Rows.Add(new ArrayList(item.Values).ToArray());

            }
            //由于hashtable直接json的时候，时间和重量等数据吐到前面全部为空，所以这里，先转DT。
            var obj = new { draw = Request["draw"], data = dt, recordsTotal = totalRows, recordsFiltered = totalRows };
            return Content(JsonHelper.GetJsonString(obj));
        }
    }
}
