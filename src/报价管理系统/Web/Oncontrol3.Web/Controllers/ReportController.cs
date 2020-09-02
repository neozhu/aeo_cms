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
    public class ReportController : BaseController
    {
        //
        // GET: /SysUser/
        public ActionResult AirInIndex()
        {
            //ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            Settings.CurrentTheme = "smart-style-0";
            return View("AirInIndex");
        }

        public ActionResult AirOutIndex()
        {
            //ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            Settings.CurrentTheme = "smart-style-0";
            return View("AirOutIndex");
        }

        
     


        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult AirInListsmart()
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;
            string wherestr = "";

            string[] searchKeys = new string[] { "ZZDH", "ZFDH", "ZJYDW", "JDRQS", "JDRQE", "SALES_ORG_ID", "CONSIGNEE_ID", "TRQ_ID", "ZJHSHTHRQS", "ZJHSHTHRQE", "ZKHZBH", "ZJCKKA", "ZSBFS", "ZSBRQS", "ZSBRQE", "ZTGFS", "ZSBGQ", "FWO_VAL", "ZJDR", "ZBGDH", "ZDGF", "ZCH", "ZBJDH", "ZJHBJ", "ZBCBJ", "ZKJBJ", "ZZLQQBJ" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    SearchCriterion.AddSearch(key, Request[key].Trim(), Aim.Data.SearchModeEnum.Like);
                }
            }


            if (SearchCriterion.Searches.Searches.Count > 0)
            {
                #region 查询条件拼接

                string begindate = "";
                string enddate = "";

                var seachItem = SearchCriterion.Searches.Searches;

                foreach (var colStr in seachItem)
                {
                    
                    //接单日期
                    if (colStr.PropertyName == "JDRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "JDRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "JDRQS")
                    {
                        wherestr += " and CREATED_ON>='" + (Convert.ToDateTime(begindate + " 00:00:00").AddHours(-8)).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "JDRQE")
                    {
                        wherestr += " and CREATED_ON<='" + (Convert.ToDateTime(enddate + " 15:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    //申报日期
                    if (colStr.PropertyName == "ZSBRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "ZSBRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZSBRQS")
                    {
                        wherestr += " and ZSBRQ>='" + (Convert.ToDateTime(begindate + " 00:00:00")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "ZSBRQE")
                    {
                        wherestr += " and ZSBRQ<='" + (Convert.ToDateTime(enddate + " 23:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    //送货日期
                    if (colStr.PropertyName == "ZJHSHTHRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "ZJHSHTHRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZJHSHTHRQS")
                    {
                        wherestr += " and ZJHSHTHRQ>='" + (Convert.ToDateTime(begindate + " 00:00:00")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "ZJHSHTHRQE")
                    {
                        wherestr += " and ZJHSHTHRQ<='" + (Convert.ToDateTime(enddate + " 23:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }
                    //车号
                    if (colStr.PropertyName == "ZCH")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                        if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZCH")
                        {
                            wherestr += " and (ZCH1  like '%" + colStr.Value + "%' OR ZCH2 like '%" + colStr.Value + "%') ";
                        }
                    
                    }

                    if (colStr.PropertyName == "ZKJBJ")//快件标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZKJBJ = 'Y' ";
                        }
                    }

                    if (colStr.PropertyName == "ZBCBJ")//本仓标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZBCBJ = 'Y' ";
                        }
                    }

                    if (colStr.PropertyName == "ZJHBJ")//急货标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZJHBJ = 'Y' ";
                        }
                    }

                    if (colStr.PropertyName == "ZZLQQBJ")//资料齐全标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZZLQQBJ = 'Y' ";
                        }
                    }

                    if (colStr.PropertyName != "JDRQS" && colStr.PropertyName != "JDRQE" && colStr.PropertyName != "ZSBRQS" && colStr.PropertyName != "ZSBRQE" && colStr.PropertyName != "ZJHSHTHRQS" && colStr.PropertyName != "ZJHSHTHRQE" && colStr.PropertyName != "ZCH" && colStr.PropertyName != "FWO_VAL" && colStr.PropertyName != "ZKJBJ" && colStr.PropertyName != "ZBCBJ" && colStr.PropertyName != "ZJHBJ" && colStr.PropertyName != "ZZLQQBJ")
                    {

                        wherestr += " and " + colStr.PropertyName + " like '%" + colStr.Value + "%'";
                    }


                }

                #endregion
            }
            #region 加入权限过滤
            FLD_QO_USER qouser = SessionHelper.GetSessionUser<FLD_QO_USER>();
            string orgids = "";
            string orgidsin = "";
            try
            {
                orgids = qouser.htExt["QO_ORGID"].ToString();
            }
            catch { }

            orgids = orgids.TrimStart('[').TrimEnd(']');
            if (string.IsNullOrEmpty(orgids))
            { orgidsin = "'',"; }
            else
            {
                foreach (string strorgid in orgids.Split(','))
                {
                    orgidsin += ("'" + strorgid.TrimStart('\'').TrimEnd('\'').PadLeft(8, '0') + "',");
                }
            }

            wherestr += "and sales_org_id in (" + orgidsin.TrimEnd(',') + ")";

            #endregion

            #region 数据源
            string resultdatasql = @"select top {0} * from (select 
trqrot.mandt,--集团
to_char(trqrot.db_key) as db_key,
trqrot.sales_org_id,--销售组织
zbgzt.zbgzt,--报关状态	
zbjzt.zbjzt,--报检状态
case
when zwlzt.zwlzt is null  and cdexe.actual_date is null then '未抽单'
when zwlzt.zwlzt is null  and cdexe.actual_date is not null then '抽单'
else zwlzt.zwlzt end as zwlzt,--物流状态
trqrot.trq_id,--FWO
zitmexd1.zbgjydw as zjydw,--经营单位
butjydw.name_org1 as zjydwms,--经营单位描述
trqrot.zkhzbh,--客户自编号
trqrot.ztgfs,--通关方式
ztgfst.description_s as ztgfsms,--通关方式描述
trqrot.zzdh,--总单号
trqrot.zfdh,--分单号
pkg.qua_pcs_val,--件数
pkg.qua_pcs_uni,--件数单位
pkg.gro_wei_val,--毛重
pkg.gro_wei_uni,--毛重单位
bgsrv.zsbgq,--申报关区
zsbgqt.description as zsbfsms,--申报关区描述
trqrot.zjhshthrq,--送货日期
trqrot.created_by,--接单人账号
adrp.name_text as zjdr, --接单人
trqrot.order_party_id as zdgf,--订购方
butdgf.name_org1 as zdgfms,--订购方描述
bgsrv.zjckka,--进出口口岸
zjckkat.description as zjckkams,--进出口口岸描述
bgsrv.zsbfs,--申报方式
zsbfst.description as zsbfsms,--申报方式描述
bgexe.actual_date as zsbrq,--申报日期
trqrot.CONSIGNEE_ID,--收货方
butshf.name_org1 as zshfms,--收货方描述
trqrot.created_on,--接单日期
case trqrot.zkjbj
when 'X' then 'Y'
else 'N' end as zkjbj,--快件标记
case trqrot.zbcbj
when 'X' then 'Y'
else 'N' end as zbcbj,--本仓标记
case zrotexd1.zzlqq
when 'X' then 'Y'
else 'N' end as zzlqqbj,--资料齐全标记
case trqrot.zjhbz
when 'X' then 'Y'
else 'N' end as zjhbj,--急货标记
trqrot.service_product_id,--服务产品
fo.fo1,--运输FO1	
fo1_itm.vehicleres_id as zch1,--车号1	
fo.fo2,--运输FO2	
fo2_itm.vehicleres_id as zch2,--车号2	
zbgfw.zbgdh,--报关单号	
bgtorrot.tor_id as zsdbgfoo,--属地报关FOO	
kabgtorrot.tor_id as zkabgfoo,--口岸报关FOO	
bjtorrot.tor_id as zbjfoo,--报检FOO	
zbjfw.zbjhm as zbjdh,--报检单号	
cdtorrot.tor_id as zcdfoo,--抽单FOO	
cztorrot.tor_id as zczfoo,--场站FOO	
zzctorrot.tor_id as zzzcfoo--中转仓FOO
from SAPABAP1.""/SCMTMS/D_TRQROT"" as trqrot
left join SAPABAP1.usr21 AS usr21 on trqrot.created_by = usr21.bname and trqrot.mandt = usr21.mandt
left join SAPABAP1.adrp as adrp on usr21.persnumber=adrp.persnumber and usr21.mandt = adrp.client
left join SAPABAP1.but000 as butdgf on trqrot.order_party_id = butdgf.partner and trqrot.mandt = butdgf.client
left join SAPABAP1.but000 as butshf on trqrot.CONSIGNEE_ID = butshf.partner and trqrot.mandt = butshf.client
left join SAPABAP1.z1c_tgfst as ztgfst on trqrot.ztgfs = ztgfst.""TYPE"" and ztgfst.langu = '1' and trqrot.mandt = ztgfst.mandt
left join SAPABAP1.z1t_rootexd1 as zrotexd1 on trqrot.db_key = zrotexd1.parent_key and trqrot.mandt = zrotexd1.mandt
left join SAPABAP1.""/SCMTMS/D_TRQITM"" as pkg on trqrot.db_key = pkg.parent_key and pkg.item_type = 'PKG' and trqrot.mandt = pkg.mandt
left join SAPABAP1.""/SCMTMS/D_TRQITM"" as bgsrv on trqrot.db_key = bgsrv.parent_key and bgsrv.item_type = 'SRV' and trqrot.mandt = bgsrv.mandt
and bgsrv.zgqsx = '02' and bgsrv.transsrvreq_code = 'A00001'
left join SAPABAP1.z1t_itemexd1 as zitmexd1 on bgsrv.db_key = zitmexd1.parent_key and bgsrv.parent_key = zitmexd1.root_key and bgsrv.mandt = zitmexd1.mandt
left join SAPABAP1.Z1T_TD_SRV_TYPE as bgtd_srv on bgsrv.db_key = bgtd_srv.ref_srv_key and bgtd_srv.root_key = bgsrv.parent_key and bgsrv.mandt = bgtd_srv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as bgtorrot on bgtd_srv.ref_foo_id = bgtorrot.tor_id and bgtorrot.tor_id <> '' and bgtd_srv.mandt = bgtorrot.mandt
left join SAPABAP1.Z1T_ZBGFW as zbgfw on bgtorrot.db_key = zbgfw.parent_key and bgtorrot.mandt = zbgfw.mandt
left join SAPABAP1.but000 as butjydw on zitmexd1.zbgjydw = butjydw.partner and zitmexd1.mandt = butjydw.client
left join SAPABAP1.z1c_sbfst as zsbfst on bgsrv.zsbfs = zsbfst.""TYPE"" and zsbfst.langu = '1' and zsbfst.mandt = bgsrv.mandt
left join SAPABAP1.z1c_sbgqt as zsbgqt on bgsrv.zsbgq = zsbgqt.""TYPE"" and zsbgqt.langu = '1' and zsbgqt.mandt = bgsrv.mandt
left join SAPABAP1.z1c_sbgqt as zjckkat on bgsrv.zjckka = zjckkat.""TYPE"" and zjckkat.langu = '1' and zjckkat.mandt = bgsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TOREXE"" as bgexe on bgexe.parent_key = bgtorrot.db_key and bgexe.event_code = 'ZSDZFS' and bgexe.mandt = bgtorrot.mandt 
left join
(
select 
bgexe.parent_key as bgfoo,
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
else '无信息' end as zbgzt,bgexe.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as bgexe
inner join 
(
select 
case max(aa.num) 
when 'a' then  'ZSGWJD' 
when 'b' then  'ZSDZZD' 
when 'c' then  'ZSDZSD' 
when 'd' then  'ZSDZSJ' 
when 'e' then  'ZSDZFS' 
when 'f' then  'ZSDZGJ' 
when 'g' then  'ZSHGXY' 
when 'j'then  'ZSSWFX' 
when 'k'then  'ZSJGZT' 
else '' end as event_code,
aa.parent_key,aa.mandt
from 
(
select 
case 
when torexe.event_code = 'ZSGWJD' then 'a'
when torexe.event_code = 'ZSDZZD' then 'b'
when torexe.event_code = 'ZSDZSD' then 'c'
when torexe.event_code = 'ZSDZSJ' then 'd'
when torexe.event_code = 'ZSDZFS' then 'e'
when torexe.event_code = 'ZSDZGJ' then 'f'
when torexe.event_code = 'ZSHGXY' then 'g'
when torexe.event_code = 'ZSSWFX' then 'j'
when torexe.event_code = 'ZSJGZT' then 'k'
else '' end as num,
event_code,torexe.parent_key,torexe.mandt
from SAPABAP1.""/SCMTMS/D_TORROT"" as foo
inner join SAPABAP1.""/SCMTMS/D_TOREXE"" as torexe on torexe.parent_key = foo.db_key and torexe.mandt = foo.mandt
where foo.tor_type ='SOBG' and torexe.EXECUTION_ID <> '' and torexe.ACTUAL_DATE is not null and foo.mandt = torexe.mandt
and torexe.event_code in ('ZSGWJD','ZSDZZD','ZSDZSD','ZSDZSJ','ZSDZFS','ZSDZGJ','ZSHGXY','ZSSWFX','ZSJGZT') 
) as aa
group by aa.parent_key,aa.mandt
) as torexe on bgexe.parent_key = torexe.parent_key and bgexe.event_code = torexe.event_code and torexe.mandt = bgexe.mandt
inner join SAPABAP1.z1t_zbgfw as zbg on zbg.parent_key = bgexe.parent_key and zbg.zgqsx = '02' and zbg.mandt = bgexe.mandt
) as zbgzt on zbgzt.bgfoo = bgtorrot.db_key and zbgzt.mandt = bgtorrot.mandt

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as kabgsrv on trqrot.db_key = kabgsrv.parent_key and kabgsrv.item_type = 'SRV' and trqrot.mandt = kabgsrv.mandt
and kabgsrv.zgqsx = '01' and kabgsrv.transsrvreq_code = 'A00001'
left join SAPABAP1.Z1T_TD_SRV_TYPE as kabgtd_srv on kabgsrv.db_key = kabgtd_srv.ref_srv_key and kabgtd_srv.root_key = kabgsrv.parent_key and kabgsrv.mandt = kabgtd_srv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as kabgtorrot on kabgtd_srv.ref_foo_id = kabgtorrot.tor_id and kabgtorrot.tor_id <> '' and kabgtd_srv.mandt = kabgtorrot.mandt

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as bjsrv on trqrot.db_key = bjsrv.parent_key and bjsrv.item_type = 'SRV' and trqrot.mandt = bjsrv.mandt 
and bjsrv.transsrvreq_code = 'A00002'
left join SAPABAP1.Z1T_TD_SRV_TYPE as bjtd_srv on bjsrv.db_key = bjtd_srv.ref_srv_key and bjtd_srv.root_key = bjsrv.parent_key and bjtd_srv.mandt = bjsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as bjtorrot on bjtd_srv.ref_foo_id = bjtorrot.tor_id and bjtorrot.tor_id <> '' and bjtd_srv.mandt = bjtorrot.mandt
left join SAPABAP1.z1t_zbjfw as zbjfw on zbjfw.parent_key = bjtorrot.db_key and zbjfw.mandt = bjtorrot.mandt
left join
(
select 
bjexe.parent_key as bjfoo,ev_tyt.description_s as zbjzt,bjexe.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as bjexe
inner join 
(
select 
case max(aa.num) 
when '1' then  'ZSDZZD' 
when '2' then  'ZSDZSJ' 
when '3' then  'ZSDZFS' 
when '4' then  'ZSSJFX' 
else '' end as event_code,
aa.parent_key,aa.mandt
from 
(
select 
case 
when torexe.event_code = 'ZSDZZD' then '1'
when torexe.event_code = 'ZSDZSJ' then '2'
when torexe.event_code = 'ZSDZFS' then '3'
when torexe.event_code = 'ZSSJFX' then '4'
else '' end as num,
event_code,torexe.parent_key,torexe.mandt
from SAPABAP1.""/SCMTMS/D_TORROT"" as foo
inner join SAPABAP1.""/SCMTMS/D_TOREXE"" as torexe on torexe.parent_key = foo.db_key and torexe.mandt = foo.mandt
where foo.tor_type ='SOBJ' and torexe.EXECUTION_ID <> '' and torexe.ACTUAL_DATE is not null and foo.mandt = torexe.mandt
and torexe.event_code in ('ZSDZZD','ZSDZSJ','ZSDZFS','ZSSJFX') 
) as aa
group by aa.parent_key,aa.mandt
) as torexe on bjexe.parent_key = torexe.parent_key and bjexe.event_code = torexe.event_code and torexe.mandt = bjexe.mandt
left join SAPABAP1.""/SCMTMS/C_EV_TYT"" as ev_tyt on ev_tyt.tor_event = torexe.event_code and ev_tyt.mandt = torexe.mandt and ev_tyt.langu ='1'
) as zbjzt on zbjzt.bjfoo = bjtorrot.db_key and zbjzt.mandt = bjtorrot.mandt and bjsrv.zgqsx = '02'

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as cdsrv on trqrot.db_key = cdsrv.parent_key and cdsrv.item_type = 'SRV' and trqrot.mandt = cdsrv.mandt 
and cdsrv.transsrvreq_code = 'A00007'
left join SAPABAP1.Z1T_TD_SRV_TYPE as cdtd_srv on cdsrv.db_key = cdtd_srv.ref_srv_key and cdtd_srv.root_key = cdsrv.parent_key and cdtd_srv.mandt = cdsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as cdtorrot on cdtd_srv.ref_foo_id = cdtorrot.tor_id and cdtorrot.tor_id <> '' and cdtd_srv.mandt = cdtorrot.mandt
left join SAPABAP1.""/SCMTMS/D_TOREXE"" as cdexe on cdexe.parent_key = cdtorrot.db_key and cdexe.event_code = 'ZSCD' and cdexe.mandt = cdtorrot.mandt
left join 
(
select
case max(num) 
when '3' then  '车号' 
when '4' then  '已加封' 
when '5' then  '运输完成' 
when '6' then  '已配送' 
when '7' then  'POD' 
else '' end as zwlzt,fwo_key,mandt
from
(
select
case event_code
when 'ZZCH' then '3'
when 'ZFLIKJGQA' then '4'
when 'ZFJRJGQ' then '5'
when 'ZFXHQS' then '6'
when 'ZFPOD' then '7'
else '' end as num,
event_code,fwo_key,mandt
from
(
select distinct torexe.event_code,fwo.db_key as fwo_key,fwo.mandt
from SAPABAP1.""/SCMTMS/D_TRQROT"" as fwo
inner join SAPABAP1.""/SCMTMS/D_TRQITM"" as fwo_itm on fwo.db_key = fwo_itm.parent_key and fwo.mandt = fwo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fu_itm on fwo_itm.db_key = fu_itm.ref_trq_item_key and fwo_itm.mandt = fu_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTP"" as fu_stp on fu_itm.parent_key = fu_stp.parent_key and fu_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTS"" as stp_itm on fu_stp.db_key = stp_itm.parent_key and stp_itm.stage_type in ('Z13','Z23','Z25','Z27') and stp_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fo_itm on fo_itm.db_key = fu_stp.assgn_item_key and fu_stp.mandt = fo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as fo_rot on fo_rot.db_key = fo_itm.parent_key and fo_rot.tor_cat = 'TO' and fo_itm.mandt = fo_rot.mandt
inner join SAPABAP1.""/SCMTMS/D_TOREXE"" as torexe on torexe.parent_key = fo_rot.db_key and torexe.mandt = fo_rot.mandt
and torexe.event_code in ('ZFXHQS','ZFPOD') and torexe.EXECUTION_ID <> '' and torexe.ACTUAL_DATE is not null 
where fwo.trq_type like 'AI%%' 
union 
select 
case 
when aa.event_code is not null then aa.event_code
else 'ZZCH' end as event_code,
aa.fwo_key,aa.mandt
from
(
select foexe.event_code,fo.fwo_key,fo.mandt,toritm.vehicleres_id as zch
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
and foexe.EXECUTION_ID <> '' and foexe.ACTUAL_DATE is not null and foexe.mandt = torrot.mandt
and foexe.event_code in ('ZFLIKJGQA','ZFJRJGQ') 
) as aa where aa.event_code <> '' or aa.zch <> ''
)
)
group by fwo_key,mandt
) as zwlzt on zwlzt.fwo_key = trqrot.db_key and zwlzt.mandt = trqrot.mandt

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as czsrv on trqrot.db_key = czsrv.parent_key and czsrv.item_type = 'SRV' and trqrot.mandt = czsrv.mandt 
and czsrv.transsrvreq_code = 'A00005'
left join SAPABAP1.Z1T_TD_SRV_TYPE as cztd_srv on czsrv.db_key = cztd_srv.ref_srv_key and cztd_srv.root_key = czsrv.parent_key and cztd_srv.mandt = czsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as cztorrot on cztd_srv.ref_foo_id = cztorrot.tor_id and cztorrot.tor_id <> '' and cztd_srv.mandt = cztorrot.mandt

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as zzcsrv on trqrot.db_key = zzcsrv.parent_key and zzcsrv.item_type = 'SRV' and trqrot.mandt = zzcsrv.mandt 
and zzcsrv.transsrvreq_code = 'A00006'
left join SAPABAP1.Z1T_TD_SRV_TYPE as zzctd_srv on zzcsrv.db_key = zzctd_srv.ref_srv_key and zzctd_srv.root_key = zzcsrv.parent_key and zzctd_srv.mandt = zzcsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as zzctorrot on zzctd_srv.ref_foo_id = zzctorrot.tor_id and zzctorrot.tor_id <> '' and zzctd_srv.mandt = zzctorrot.mandt

left join 
(
select distinct min(fo_rot.tor_id) as fo1, max(fo_rot.tor_id) as fo2,fwo.db_key as fwo_key,fwo.mandt
from SAPABAP1.""/SCMTMS/D_TRQROT"" as fwo
inner join SAPABAP1.""/SCMTMS/D_TRQITM"" as fwo_itm on fwo.db_key = fwo_itm.parent_key and fwo.mandt = fwo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fu_itm on fwo_itm.db_key = fu_itm.ref_trq_item_key and fwo_itm.mandt = fu_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTP"" as fu_stp on fu_itm.parent_key = fu_stp.parent_key and fu_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTS"" as stp_itm on fu_stp.db_key = stp_itm.parent_key and stp_itm.stage_type in ('Z25','Z27') and stp_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fo_itm on fo_itm.db_key = fu_stp.assgn_item_key and fu_stp.mandt = fo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as fo_rot on fo_rot.db_key = fo_itm.parent_key and fo_rot.tor_cat = 'TO' and fo_itm.mandt = fo_rot.mandt
where fwo.trq_type like 'AI%%'
group by fwo.db_key,fwo.mandt
) as fo on fo.fwo_key = trqrot.db_key and fo.mandt = trqrot.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as fo1_rot on fo1_rot.tor_id = fo.fo1 and fo1_rot.mandt = fo.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as fo2_rot on fo2_rot.tor_id = fo.fo1 and fo2_rot.mandt = fo.mandt
left join SAPABAP1.""/SCMTMS/D_TORITE"" as fo1_itm on fo1_itm.parent_key = fo1_rot.db_key and fo1_itm.item_cat = 'AVR' and fo1_itm.mandt = fo1_rot.mandt
left join SAPABAP1.""/SCMTMS/D_TORITE"" as fo2_itm on fo2_itm.parent_key = fo2_rot.db_key and fo2_itm.item_cat = 'AVR' and fo2_itm.mandt = fo2_rot.mandt
where trqrot.trq_type like 'AI%%' 
order by trqrot.trq_id desc
) where 1=1  " + wherestr;

            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 " + wherestr;

            if (this.Request["FWO_VAL"] == "")
            {
                resultdatasql = string.Format(resultdatasql, '0');
                resultdatacountsql = string.Format(resultdatacountsql, '0');
            }
            else
            {
                resultdatasql = string.Format(resultdatasql, this.Request["FWO_VAL"]);
                resultdatacountsql = string.Format(resultdatacountsql, this.Request["FWO_VAL"]);
            }
            #endregion

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
        
        
        public ActionResult AirOutListsmart()
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;
            string wherestr = "";

            string[] searchKeys = new string[] { "ZKHZBH", "ZBGDH", "ZHDFYD", "JDRQS", "JDRQE", "SHIPPER_ID", "ZSBGQ", "ZSBGQ", "ZMDG", "ZJHSHTHRQS", "ZJHSHTHRQE", "ZDGF", "ZTGFS", "TRQ_ID", "ZSBRQS", "ZSBRQE", "SALES_ORG_ID", "ZSBFS", "ZSBFS", "FWO_VAL", "ZJHHBRQS", "ZJHHBRQE", "ZPCFS", "ZJDR", "ZCH1", "ZKJBJ", "ZJHBJ" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    SearchCriterion.AddSearch(key, Request[key].Trim(), Aim.Data.SearchModeEnum.Like);
                }
            }


            if (SearchCriterion.Searches.Searches.Count > 0)
            {
                #region 查询条件拼接

                string begindate = "";
                string enddate = "";

                var seachItem = SearchCriterion.Searches.Searches;
                foreach (var colStr in seachItem)
                {
                    //接单日期
                    if (colStr.PropertyName == "JDRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "JDRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "JDRQS")
                    {
                        wherestr += " and CREATED_ON>='" + (Convert.ToDateTime(begindate + " 00:00:00").AddHours(-8)).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "JDRQE")
                    {
                        wherestr += " and CREATED_ON<='" + (Convert.ToDateTime(enddate + " 15:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    //申报日期
                    if (colStr.PropertyName == "ZSBRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "ZSBRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZSBRQS")
                    {
                        wherestr += " and ZSBRQ>='" + (Convert.ToDateTime(begindate + " 00:00:00")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "ZSBRQE")
                    {
                        wherestr += " and ZSBRQ<='" + (Convert.ToDateTime(enddate + " 23:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    //提货日期
                    if (colStr.PropertyName == "ZJHSHTHRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "ZJHSHTHRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZJHSHTHRQS")
                    {
                        wherestr += " and ZJHSHTHRQ>='" + (Convert.ToDateTime(begindate + " 00:00:00")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "ZJHSHTHRQE")
                    {
                        wherestr += " and ZJHSHTHRQ<='" + (Convert.ToDateTime(enddate + " 23:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }
                    //航班日期
                    if (colStr.PropertyName == "ZJHHBRQS")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            begindate = colStr.Value.ToString();
                        }
                    }

                    if (colStr.PropertyName == "ZJHHBRQE")
                    {
                        if (colStr.Value.ToString() != "")
                        {
                            enddate = colStr.Value.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(begindate) && colStr.PropertyName == "ZJHHBRQS")
                    {
                        wherestr += " and ZJHHBRQ>='" + (Convert.ToDateTime(begindate + " 00:00:00")).ToString("yyyyMMddHHmmss") + "'";
                    }

                    if (!string.IsNullOrEmpty(enddate) && colStr.PropertyName == "ZJHHBRQE")
                    {
                        wherestr += " and ZJHHBRQ<='" + (Convert.ToDateTime(enddate + " 23:59:59")).ToString("yyyyMMddHHmmss") + "'";
                    }
                    if (colStr.PropertyName == "ZKJBJ")//快件标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZKJBJ = 'Y' ";
                        }
                    }
                    if (colStr.PropertyName == "ZJHBJ")//急货标记
                    {
                        if (colStr.Value.ToString() == "Y")
                        {
                            //begindate = colStr.Value.ToString();
                            wherestr += " and ZJHBJ = 'Y' ";
                        }
                    }
                    //日期条件外的其他查询参数
                    if (colStr.PropertyName != "JDRQS" && colStr.PropertyName != "JDRQE" && colStr.PropertyName != "ZSBRQS" && colStr.PropertyName != "ZSBRQE" && colStr.PropertyName != "ZJHSHTHRQS" && colStr.PropertyName != "ZJHSHTHRQE" && colStr.PropertyName != "ZJHHBRQS" && colStr.PropertyName != "ZJHHBRQE" && colStr.PropertyName != "FWO_VAL" && colStr.PropertyName != "ZKJBJ" && colStr.PropertyName != "ZJHBJ")
                    {

                        wherestr += " and " + colStr.PropertyName + " like '%" + colStr.Value + "%'";
                    }


                }

                #endregion
            }
            #region 加入权限过滤
            FLD_QO_USER qouser = SessionHelper.GetSessionUser<FLD_QO_USER>();
            string orgids = "";
            string orgidsin = "";
            try
            {
                orgids = qouser.htExt["QO_ORGID"].ToString();
            }
            catch { }

            orgids = orgids.TrimStart('[').TrimEnd(']');
            if (string.IsNullOrEmpty(orgids))
            { orgidsin = "'',"; }
            else
            {
                foreach (string strorgid in orgids.Split(','))
                {
                    orgidsin += ("'" + strorgid.TrimStart('\'').TrimEnd('\'').PadLeft(8, '0') + "',");
                }
            }

            wherestr += "and sales_org_id in (" + orgidsin.TrimEnd(',') + ")";

            #endregion

            #region 数据源
            string resultdatasql = @"select top {0} * from (select 
trqrot.mandt,--集团
zbgzt.zbgzt,--报关状态	
trqrot.shipper_id,--发货方
butfhf.name_org1 as zfhfms,--发货方描述
trqrot.zkhzbh,--客户自编号
pkg.qua_pcs_val,--数量
pkg.qua_pcs_uni,--数量单位
pkg.gro_wei_val,--毛重
pkg.gro_wei_uni,--毛重单位
pkg.gro_vol_val,--体积
pkg.gro_vol_uni,--体积单位
trqrot.ztgfs,--通关方式
ztgfst.description_s as ztgfsms,--通关方式描述
zrotexd1.zpcfs,--配舱方式
case zrotexd1.zpcfs
when '01' then '客户自配'
when '02' then '我司订舱-直客'
when '03' then '我司订舱-同行'
when '04' then '客户指定船公司' 
when '05' then '客户指定货代'
when '06' then '我司平台代配舱'
else '' end as zpcfsms,--配舱方式描述 
trqrot.zhdfyd,--货代分运单
trqrot.zmdg,--目的港
loct.descr40 as zmdgms,--目的港描述
trqrot.zjhshthrq,--提货日期
bgexe.actual_date as zsbrq,--申报日期
trqrot.zjhhbrq,--航班日期
trqrot.created_on,--接单日期
trqrot.created_by,--接单人账号
adrp.name_text as zjdr, --接单人
bgsrv.zsbgq,--申报关区
zsbgqt.description as zsbfsms,--申报关区描述
bgsrv.zsbfs,--申报方式
zsbfst.description as zsbfsms,--申报方式描述
trqrot.trq_id,--FWO
trqrot.order_party_id as zdgf,--订购方
butdgf.name_org1 as zdgfms,--订购方描述
trqrot.sales_org_id,--销售组织
case trqrot.zjhbz
when 'X' then 'Y'
else 'N' end as zjhbj,--急货标记
case trqrot.zkjbj
when 'X' then 'Y'
else 'N' end as zkjbj,--快件标记
trqrot.service_product_id,--服务产品
fo.fo1,--运输FO1	
fo1_itm.vehicleres_id as zch1,--车号1	
zbgfw.zbgdh,--报关单号	
bgtorrot.tor_id as zsdbgfoo,--属地报关FOO	
cztorrot.tor_id as zczfoo--场站FOO

from SAPABAP1.""/SCMTMS/D_TRQROT"" as trqrot
left join SAPABAP1.usr21 AS usr21 on trqrot.created_by = usr21.bname and trqrot.mandt = usr21.mandt
left join SAPABAP1.adrp as adrp on usr21.persnumber=adrp.persnumber and usr21.mandt = adrp.client
left join SAPABAP1.but000 as butdgf on trqrot.order_party_id = butdgf.partner and trqrot.mandt = butdgf.client
left join SAPABAP1.but000 as butfhf on trqrot.shipper_id = butfhf.partner and trqrot.mandt = butfhf.client
left join SAPABAP1.z1c_tgfst as ztgfst on trqrot.ztgfs = ztgfst.""TYPE"" and ztgfst.langu = '1' and trqrot.mandt = ztgfst.mandt
left join SAPABAP1.z1t_rootexd1 as zrotexd1 on trqrot.db_key = zrotexd1.parent_key and trqrot.mandt = zrotexd1.mandt
left join SAPABAP1.""/SAPAPO/LOC"" as loc on loc.locno = trqrot.zmdg and trqrot.mandt = loc.mandt
left join SAPABAP1.""/SAPAPO/LOCT"" as loct on loct.locid = loc.locid and loc.mandt = loct.mandt and loct.spras = '1'
left join SAPABAP1.""/SCMTMS/D_TRQITM"" as pkg on trqrot.db_key = pkg.parent_key and pkg.item_type = 'PKG' and trqrot.mandt = pkg.mandt
left join SAPABAP1.""/SCMTMS/D_TRQITM"" as bgsrv on trqrot.db_key = bgsrv.parent_key and bgsrv.item_type = 'SRV' and trqrot.mandt = bgsrv.mandt
and bgsrv.zgqsx = '02' and bgsrv.transsrvreq_code = 'A00001'
left join SAPABAP1.z1c_sbfst as zsbfst on bgsrv.zsbfs = zsbfst.""TYPE"" and zsbfst.langu = '1' and zsbfst.mandt = bgsrv.mandt
left join SAPABAP1.z1c_sbgqt as zsbgqt on bgsrv.zsbgq = zsbgqt.""TYPE"" and zsbgqt.langu = '1' and zsbgqt.mandt = bgsrv.mandt
left join SAPABAP1.Z1T_TD_SRV_TYPE as bgtd_srv on bgsrv.db_key = bgtd_srv.ref_srv_key and bgtd_srv.root_key = bgsrv.parent_key and bgsrv.mandt = bgtd_srv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as bgtorrot on bgtd_srv.ref_foo_id = bgtorrot.tor_id and bgtorrot.tor_id <> '' and bgtd_srv.mandt = bgtorrot.mandt
left join SAPABAP1.Z1T_ZBGFW as zbgfw on bgtorrot.db_key = zbgfw.parent_key and bgtorrot.mandt = zbgfw.mandt
left join SAPABAP1.""/SCMTMS/D_TOREXE"" as bgexe on bgexe.parent_key = bgtorrot.db_key and bgexe.event_code = 'ZSDZFS' and bgexe.mandt = bgtorrot.mandt 
left join
(
select 
bgexe.parent_key as bgfoo,
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
else '无信息' end as zbgzt,bgexe.mandt
from SAPABAP1.""/SCMTMS/D_TOREXE"" as bgexe
inner join 
(
select 
case max(aa.num) 
when 'a' then  'ZSGWJD' 
when 'b' then  'ZSDZZD' 
when 'c' then  'ZSDZSD' 
when 'd' then  'ZSDZSJ' 
when 'e' then  'ZSDZFS' 
when 'f' then  'ZSDZGJ' 
when 'g' then  'ZSHGXY' 
when 'j'then  'ZSSWFX' 
when 'k'then  'ZSJGZT' 
else '' end as event_code,
aa.parent_key,aa.mandt
from 
(
select 
case 
when torexe.event_code = 'ZSGWJD' then 'a'
when torexe.event_code = 'ZSDZZD' then 'b'
when torexe.event_code = 'ZSDZSD' then 'c'
when torexe.event_code = 'ZSDZSJ' then 'd'
when torexe.event_code = 'ZSDZFS' then 'e'
when torexe.event_code = 'ZSDZGJ' then 'f'
when torexe.event_code = 'ZSHGXY' then 'g'
when torexe.event_code = 'ZSSWFX' then 'j'
when torexe.event_code = 'ZSJGZT' then 'k'
else '' end as num,
event_code,torexe.parent_key,torexe.mandt
from SAPABAP1.""/SCMTMS/D_TORROT"" as foo
inner join SAPABAP1.""/SCMTMS/D_TOREXE"" as torexe on torexe.parent_key = foo.db_key and torexe.mandt = foo.mandt
where foo.tor_type ='SOBG' and torexe.EXECUTION_ID <> '' and torexe.ACTUAL_DATE is not null and foo.mandt = torexe.mandt
and torexe.event_code in ('ZSGWJD','ZSDZZD','ZSDZSD','ZSDZSJ','ZSDZFS','ZSDZGJ','ZSHGXY','ZSSWFX','ZSJGZT') 
) as aa
group by aa.parent_key,aa.mandt
) as torexe on bgexe.parent_key = torexe.parent_key and bgexe.event_code = torexe.event_code and torexe.mandt = bgexe.mandt
inner join SAPABAP1.z1t_zbgfw as zbg on zbg.parent_key = bgexe.parent_key and zbg.zgqsx = '02' and zbg.mandt = bgexe.mandt
) as zbgzt on zbgzt.bgfoo = bgtorrot.db_key and zbgzt.mandt = bgtorrot.mandt

left join SAPABAP1.""/SCMTMS/D_TRQITM"" as czsrv on trqrot.db_key = czsrv.parent_key and czsrv.item_type = 'SRV' and trqrot.mandt = czsrv.mandt 
and czsrv.transsrvreq_code = 'A00005'
left join SAPABAP1.Z1T_TD_SRV_TYPE as cztd_srv on czsrv.db_key = cztd_srv.ref_srv_key and cztd_srv.root_key = czsrv.parent_key and cztd_srv.mandt = czsrv.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as cztorrot on cztd_srv.ref_foo_id = cztorrot.tor_id and cztorrot.tor_id <> '' and cztd_srv.mandt = cztorrot.mandt

left join 
(
select distinct min(fo_rot.tor_id) as fo1 ,fwo.db_key as fwo_key,fwo.mandt
from SAPABAP1.""/SCMTMS/D_TRQROT"" as fwo
inner join SAPABAP1.""/SCMTMS/D_TRQITM"" as fwo_itm on fwo.db_key = fwo_itm.parent_key and fwo.mandt = fwo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fu_itm on fwo_itm.db_key = fu_itm.ref_trq_item_key and fwo_itm.mandt = fu_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTP"" as fu_stp on fu_itm.parent_key = fu_stp.parent_key and fu_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORSTS"" as stp_itm on fu_stp.db_key = stp_itm.parent_key and stp_itm.stage_type in ('Z25','Z27') and stp_itm.mandt = fu_stp.mandt
inner join SAPABAP1.""/SCMTMS/D_TORITE"" as fo_itm on fo_itm.db_key = fu_stp.assgn_item_key and fu_stp.mandt = fo_itm.mandt
inner join SAPABAP1.""/SCMTMS/D_TORROT"" as fo_rot on fo_rot.db_key = fo_itm.parent_key and fo_rot.tor_cat = 'TO' and fo_itm.mandt = fo_rot.mandt
where fwo.trq_type like 'AE%%'
group by fwo.db_key,fwo.mandt
) as fo on fo.fwo_key = trqrot.db_key and fo.mandt = trqrot.mandt
left join SAPABAP1.""/SCMTMS/D_TORROT"" as fo1_rot on fo1_rot.tor_id = fo.fo1 and fo1_rot.mandt = fo.mandt
left join SAPABAP1.""/SCMTMS/D_TORITE"" as fo1_itm on fo1_itm.parent_key = fo1_rot.db_key and fo1_itm.item_cat = 'AVR' and fo1_itm.mandt = fo1_rot.mandt
where trqrot.trq_type like 'AE%%' 
order by trqrot.trq_id desc
) where 1=1   " + wherestr;

            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 " + wherestr;

            if (this.Request["FWO_VAL"] == "")
            {
                resultdatasql = string.Format(resultdatasql, '0');
                resultdatacountsql = string.Format(resultdatacountsql, '0');
            }
            else
            {
                resultdatasql = string.Format(resultdatasql, this.Request["FWO_VAL"]);
                resultdatacountsql = string.Format(resultdatacountsql, this.Request["FWO_VAL"]);
            }

            #endregion

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
        public ActionResult AirinBgzt()//报关状态
        {
            int PageIndex = SearchCriterion.CurrentPageIndex;
            int PageSize = SearchCriterion.PageSize;
            int totalRows = 0;
            int totalPages = 0;

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
) where 1=1";

            #endregion
            string resultdatacountsql = @"select count(*) ""总计"" from(" + resultdatasql + ") where 1=1 ";

            List<Hashtable> htlist = HanaConectionHelper.LoadListWithPage(PageIndex, PageSize, resultdatasql, resultdatacountsql, "TM", out totalRows, out totalPages, true);
           
            return Content(JsonHelper.GetJsonString(htlist));
        }

    }
}
