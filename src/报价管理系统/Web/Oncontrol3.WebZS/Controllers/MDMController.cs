using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.MDM;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class MDMController : Controller
    {
        public ActionResult GetDatas()
        {
            if (!string.IsNullOrEmpty(this.Request["mdmkey"]))
            {
                string sql = "";
                string mdmkey = this.Request["mdmkey"].ToString() + "";
                string mdmfieldname = this.Request["mdmfieldname"].ToString() + "";
                string ifZh = this.Request.QueryString["language"];
                string srch = (this.Request["q"].ToString() + "").ToUpper();
                MDM_MAIN_BASIC mdmbasic = MDM_MAIN_BASIC.FindFirstByProperties(MDM_MAIN_BASIC.Prop_MDKEY, mdmkey);
                if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "B")
                {
                    string selcolumns = "";
                    string wherecolumns = "";
                    string langucolumns = "";
                    //foreach (string fn in mdmfieldname.Split(','))
                    string[] fnarr = mdmfieldname.Split(',').ToArray();
                    for (int i = 0; i < fnarr.Length; i++)
                    {
                        MDM_MAIN_STRC mdmsctr = MDM_MAIN_STRC.FindFirstByProperties(MDM_MAIN_STRC.Prop_MDKEY, mdmkey, MDM_MAIN_STRC.Prop_FIELDNAME, fnarr[i]);
                        if (null != mdmsctr)
                        {
                            int idx = (int)mdmsctr.POSITION;
                            if (i == 0)
                            {
                                selcolumns += ("COLUMN" + idx.ToString() + " CODE,");
                            }
                            if (i == 1)
                            {
                                selcolumns += ("COLUMN" + idx.ToString() + " NAME,");
                            }

                            if (string.IsNullOrEmpty(wherecolumns))
                            {
                                wherecolumns += " AND ( COLUMN" + idx.ToString() + " LIKE '%" + srch + "%' ";
                            }
                            else
                            {
                                wherecolumns += " OR COLUMN" + idx.ToString() + " LIKE '%" + srch + "%' ";
                            }
                        }
                    }
                    wherecolumns += " ) ";
                    selcolumns = selcolumns.TrimEnd(',');

                    //langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + mdmkey + "' and fieldname in ( 'LANGU','SPRAS') ").ToString() + " ='1'";
                    string language = "E";
                    if (ifZh == "zh")
                    {
                        language = "1";
                    }
                    langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + mdmkey + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " = '" + language + "'";

                    sql = string.Format("SELECT {0} FROM MDM_MIAN_VALUE WHERE  mdkey = '{1}' {2} AND {3} AND ROWNUM <= 20 ORDER BY CODE ", selcolumns, mdmkey, wherecolumns, langucolumns);
                }
                else if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "A")
                {
                    //类型为A 的都没有结构表 默认列2是语言  列3是id 列4是描述
                    sql = string.Format("SELECT COLUMN3 CODE, COLUMN4 NAME FROM MDM_MIAN_VALUE WHERE  mdkey = '{0}' AND COLUMN2 = '1' AND ( COLUMN3 LIKE '%{1}%' OR COLUMN4 LIKE '%{1}%') AND ROWNUM <= 20 ORDER BY CODE ", mdmkey, srch);
                }
                else if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "C" && mdmbasic.MDKEY.ToUpper() != "FLIGHT_CODE")
                {
                    //类型为C 的都没有结构表  列2是id 列3是描述
                    sql = string.Format("SELECT COLUMN2 CODE, COLUMN3 NAME FROM MDM_MIAN_VALUE WHERE  mdkey = '{0}' AND ( COLUMN2 LIKE '%{1}%' OR COLUMN3 LIKE '%{1}%') AND ROWNUM <= 20 ORDER BY CODE ", mdmkey, srch);
                }

                DataTable mdmdt = DataHelper.QueryDataTable(sql);

                string content = JsonHelper.GetJsonString(mdmdt);
                return Content(content);
            }
            else
                return Content("");
        }
        public ActionResult GetDataJC()
        {
            if (!string.IsNullOrEmpty(this.Request["mdmkey"]))
            {
                string sql = "";
                string mdmkey = this.Request["mdmkey"].ToString() + "";
                string mdmfieldname = this.Request["mdmfieldname"].ToString() + "";
                string ifZh = this.Request.QueryString["language"];
                string srch = (this.Request["q"].ToString() + "").ToUpper();
                MDM_CALC_BASIC mdmbasic = MDM_CALC_BASIC.FindFirstByProperties(MDM_CALC_BASIC.Prop_MDKEY, mdmkey);
                if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "B")
                {
                    string selcolumns = "";
                    string wherecolumns = "";
                    string wheretj = "";
                    string langucolumns = "";
                    string[] fnarr = mdmfieldname.Split(',').ToArray();
                    for (int i = 0; i < fnarr.Length; i++)
                    {
                        MDM_CALC_STRC mdmsctr = MDM_CALC_STRC.FindFirstByProperties(MDM_CALC_STRC.Prop_MDKEY, mdmkey, MDM_CALC_STRC.Prop_FIELDNAME, fnarr[i]);
                        if (null != mdmsctr)
                        {
                            int idx = (int)mdmsctr.POSITION;
                            if (i == 0)
                            {
                                selcolumns += ("COLUMN" + idx.ToString() + " CODE,");
                            }
                            if (i == 1)
                            {
                                selcolumns += ("COLUMN" + idx.ToString() + " NAME,");
                            }

                            if (string.IsNullOrEmpty(wherecolumns))
                            {
                                wherecolumns += " AND ( COLUMN" + idx.ToString() + " LIKE '%" + srch + "%' ";
                            }
                            else
                            {
                                wherecolumns += " OR COLUMN" + idx.ToString() + " LIKE '%" + srch + "%' ";
                            }
                            //匹配的优先显示
                            if (!String.IsNullOrEmpty(srch) && string.IsNullOrEmpty(wheretj))
                            {
                                wheretj += " or ( COLUMN" + idx.ToString() + "='" + srch + "' ";
                            }
                            else if (!String.IsNullOrEmpty(srch) && !String.IsNullOrEmpty(wheretj))
                            {
                                wheretj += " OR COLUMN" + idx.ToString() + "='" + srch + "' ";
                            }
                        }
                    }
                    wherecolumns += " ) ";
                    wheretj += " ) ";
                    selcolumns = selcolumns.TrimEnd(',');

                    string language = "E";
                    if (ifZh == "zh")
                    {
                        language = "1";
                    }
                    //是否多语言
                    string position = DataHelper.QueryValue("SELECT position FROM MDM_CALC_STRC where mdkey = '" + mdmkey + "' and fieldname in ( SELECT distinct fieldname FROM MDM_CALC_STRC where ddtext = '语言代码' ) ") + "";
                    if (!String.IsNullOrEmpty(position))
                    {
                        langucolumns = " AND COLUMN" + position + " = '" + language + "'";
                    }
                    if (!String.IsNullOrEmpty(srch))
                    {
                        sql = string.Format(" SELECT CODE, NAME FROM  (SELECT distinct mdkey,{0} FROM MDM_CALC_VALUE WHERE mdkey = '{1}' {2} {3} {4} ORDER BY CODE) WHERE mdkey = '{1}' AND ROWNUM <= 20 ORDER BY CODE", selcolumns, mdmkey, wherecolumns, langucolumns, wheretj);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0} FROM MDM_CALC_VALUE WHERE  mdkey = '{1}' {2} {3} AND ROWNUM <= 20 ORDER BY CODE ", selcolumns, mdmkey, wherecolumns, langucolumns);
                    }
                }
                else if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "A")
                {
                    //类型为A 的都没有结构表 默认列2是语言  列3是id 列4是描述
                    sql = string.Format("SELECT COLUMN3 CODE, COLUMN4 NAME FROM MDM_CALC_VALUE WHERE  mdkey = '{0}' AND COLUMN2 = '1' AND ( COLUMN3 LIKE '%{1}%' OR COLUMN4 LIKE '%{1}%') AND ROWNUM <= 20 ORDER BY CODE ", mdmkey, srch);
                }
                else if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "C" && mdmbasic.MDKEY.ToUpper() != "FLIGHT_CODE")
                {
                    //类型为C 的都没有结构表  列2是id 列3是描述
                    if (!String.IsNullOrEmpty(srch))
                    {
                        sql = string.Format(" SELECT CODE, NAME FROM  (SELECT distinct mdkey,COLUMN2 CODE, COLUMN3 NAME FROM MDM_CALC_VALUE WHERE mdkey = '{0}' AND (COLUMN2='{1}' OR COLUMN3='{1}') or (COLUMN2 LIKE '%{1}%' OR COLUMN3 LIKE '%{1}%') ORDER BY CODE) WHERE mdkey = '{0}' AND ROWNUM <= 20 ORDER BY CODE", mdmkey, srch);
                    }
                    else
                    {
                        sql = string.Format("SELECT COLUMN2 CODE, COLUMN3 NAME FROM MDM_CALC_VALUE WHERE  mdkey = '{0}' AND ( COLUMN2 LIKE '%{1}%' OR COLUMN3 LIKE '%{1}%') AND ROWNUM <= 20 ORDER BY CODE ", mdmkey, srch);
                    }
                }

                DataTable mdmdt = DataHelper.QueryDataTable(sql);

                string content = JsonHelper.GetJsonString(mdmdt);
                return Content(content);
            }
            else
                return Content("");
        }

        public ActionResult GetMDMDescription(string mdmkey, string mdmfieldname, string srch)
        {
            try
            {
                string sql = "";

                if (string.IsNullOrEmpty(srch))
                {
                    return Content("");
                }

                MDM_MAIN_BASIC mdmbasic = MDM_MAIN_BASIC.FindFirstByProperties(MDM_MAIN_BASIC.Prop_MDKEY, mdmkey);
                if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "B")
                {
                    string selcolumns = "";
                    string wherecolumns = "";
                    string langucolumns = "";
                    //foreach (string fn in mdmfieldname.Split(','))
                    string[] fnarr = mdmfieldname.Split(',').ToArray();
                    for (int i = 0; i < 2; i++)
                    {
                        MDM_MAIN_STRC mdmsctr = MDM_MAIN_STRC.FindFirstByProperties(MDM_MAIN_STRC.Prop_MDKEY, mdmkey, MDM_MAIN_STRC.Prop_FIELDNAME, fnarr[i]);
                        if (null != mdmsctr)
                        {
                            int idx = (int)mdmsctr.POSITION;
                            if (i == 0)
                            {
                                if (string.IsNullOrEmpty(wherecolumns))
                                {
                                    wherecolumns += " AND COLUMN" + idx.ToString() + " = '" + srch + "' ";
                                }
                            }
                            if (i == 1)
                            {
                                selcolumns += ("COLUMN" + idx.ToString() + " NAME,");
                            }
                        }
                    }
                    selcolumns = selcolumns.TrimEnd(',');

                    langucolumns = " COLUMN" + DataHelper.QueryValue("SELECT position FROM MDM_MAIN_STRC where mdkey = '" + mdmkey + "' and fieldname in ( SELECT distinct fieldname FROM MDM_MAIN_STRC where ddtext = '语言代码' ) ").ToString() + " ='1'";

                    sql = string.Format("SELECT {0} FROM MDM_MIAN_VALUE WHERE  mdkey = '{1}' {2} AND {3} ", selcolumns, mdmkey, wherecolumns, langucolumns);
                }
                else if (null != mdmbasic && mdmbasic.MDTYPE.ToUpper() == "A")
                {
                    //类型为A 的都没有结构表 默认列2是语言  列3是id 列4是描述
                    sql = string.Format("SELECT COLUMN4 NAME FROM MDM_MIAN_VALUE WHERE  mdkey = '{0}' AND COLUMN2 = '1' AND COLUMN3 = '{1}' ", mdmkey, srch);
                }

                string mdmname = (string)DataHelper.QueryValue(sql) + "";

                return Content(mdmname);
            }
            catch { }

            return Content("");
        }

        public ActionResult GetDatasDAIYUNXIEYI(string q)
        {
            string qstr = this.Request["q"].ToString() + "";

            //HqlSearchCriterion sc = new HqlSearchCriterion();
            //sc.AllowPaging = true;
            //sc.PageSize = 20;

            //string[] searchKeys = new string[] { SysUser.Prop_LoginName, SysUser.Prop_Name };
            //foreach (string key in searchKeys)
            //{
            //    if (!string.IsNullOrEmpty(qstr))
            //    {
            //        Type valueType = typeof(SysUser).GetProperty(key).PropertyType;
            //        if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
            //        {
            //            sc.AddSearch(key, int.Parse(qstr.Trim()), Aim.Data.SearchModeEnum.Equal);
            //        }
            //        else
            //            sc.AddSearch(key, Convert.ChangeType(qstr.Trim(), valueType), Aim.Data.SearchModeEnum.Like);
            //    }
            //}
            //SysUser.FindAll(sc);

            string sql = string.Format("SELECT * FROM SysUser WHERE (LoginName LIKE '%{0}%' OR Name LIKE '%{0}%') AND rownum<20 ORDER BY LoginName DESC", qstr);
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }

        public ActionResult GetDatasBP(string q)
        {
            string qstr = this.Request["q"].ToString() + "";
            // 取bpkey 为7位的值
            string sql = string.Format("SELECT RID, BPKEY CODE, BPNAME NAME FROM MDM_BP WHERE length(BPKEY) = 7 AND (BPKEY LIKE '%{0}%' OR BPNAME LIKE '%{0}%' OR BPKEY LIKE '%{1}%' OR BPNAME LIKE '%{1}%') AND (STATUS is null or STATUS = '') AND LENGTH(BPKEY) != 9 AND rownum<20 ORDER BY BPKEY ASC", qstr.ToLower(), qstr.ToUpper());
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }
        public ActionResult GetDatasBPBJ(string q)
        {
            string qstr = this.Request["q"].ToString() + "";
            // 取bpkey 为7位的值
            string sql = string.Format("SELECT BPKEY CODE, BPNAME NAME FROM MDM_BP WHERE (BPKEY LIKE '%{0}%' OR BPNAME LIKE '%{0}%' OR BPKEY LIKE '%{1}%' OR BPNAME LIKE '%{1}%') AND rownum<20 ORDER BY BPKEY ASC", qstr.ToLower(), qstr.ToUpper());
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }

        public ActionResult GetBPDescription(string qstr)
        {
            string content = "";
            try
            {
                string sql = string.Format("SELECT BPNAME FROM MDM_BP WHERE BPKEY = '{0}'", qstr.ToUpper());
                content = (string)DataHelper.QueryValue(sql);
            }
            catch { }

            return Content(content);
        }

        //public ActionResult GetDatasLOC(string q, string loctype)
        //{
        //    string qstr = this.Request["q"].ToString() + "";
        //    string strWhereLOCTYPE = "";
        //    if (!string.IsNullOrEmpty(loctype))
        //    {
        //        strWhereLOCTYPE = " AND LOCTYPE ='" + loctype + "' ";
        //    }
        //    //过滤掉运输区域1005
        //    string sql = string.Format(@"SELECT LOCID RID , LOCNO CODE, DESCR40 NAME FROM MDM_LOC WHERE (LOCNO LIKE '%{0}%' OR DESCR40 LIKE '%{0}%' OR LOCNO LIKE '%{1}%' OR DESCR40 LIKE '%{1}%') AND LOCTYPE !='1005' {2} AND rownum<20 ORDER BY LOCNO ASC", qstr.ToLower(), qstr.ToUpper(), strWhereLOCTYPE);
        //    DataTable dtsearch = DataHelper.QueryDataTable(sql);

        //    string content = JsonHelper.GetJsonString(dtsearch);
        //    return Content(content);
        //}
        public ActionResult GetDatasLOC(string q, string loctype)
        {
            string qstr = this.Request["q"] + "";
            string mdmloctype = Request["mdmloctype"] + "";
            if (qstr.IndexOf("(") >= 0 && qstr.IndexOf(")") >= 0)
            {
                qstr = qstr.Split('(')[0];
            }
            string strWhereLOCTYPE = "";
            if (!string.IsNullOrEmpty(loctype))
            {
                strWhereLOCTYPE = " AND LOCTYPE ='" + loctype + "' ";
            }
            if (!string.IsNullOrEmpty(mdmloctype))
            {
                if (mdmloctype == "HY")
                {
                    mdmloctype = "1100";
                }
                else if (mdmloctype == "KY")
                {
                    mdmloctype = "1110";
                }
                strWhereLOCTYPE = " and LOCTYPE = '" + mdmloctype + "' ";
            }
            string sql = string.Format(@"with m1 as(
select a.locno RID,a.descr40 CODE,b.descr40 NAME from (select * from mdm_loc where rowid in(select min(rowid) from MDM_LOC group by locno having count(*)>1) {6})a 
join (select * from mdm_loc where rowid in(select max(rowid) from MDM_LOC group by locno having count(*)>1) {7}) b 
on a.locno = b.locno
union all select locno,descr40,null from MDM_LOC where locno in(select locno from MDM_LOC group by locno having count(*) = 1 ) {8}) select * from m1
where rownum < 20 and ( m1.RID like '%{0}%' or m1.RID like '%{1}%' or m1.CODE like '%{2}%' or m1.CODE like '%{3}%' or m1.NAME like '%{4}%' or m1.NAME like '%{5}%')", qstr.ToLower(), qstr.ToUpper(), qstr.ToLower(), qstr.ToUpper(), qstr.ToLower(), qstr.ToUpper(), strWhereLOCTYPE, strWhereLOCTYPE, strWhereLOCTYPE);
            //string sql = string.Format("select code as CODE,name as NAME  from (select wm_concat(to_char(descr40)) name , locno code from mdm_loc group by locno) where rownum < 20 and ( code like '%{0}%' or code like '%{1}%' or name like '%{2}%' or name like '%{3}%')", qstr.ToLower(), qstr.ToUpper(), qstr.ToLower(), qstr.ToUpper());
            //过滤掉运输区域1005
            //            string sql = string.Format(@"with m1 as(select t1.locno locno,descr40 from MDM_LOC t1,(select count(*),locno from MDM_LOC having count(*) > 1 group by locno) t2 where t1.locno = t2.locno and loctype != '1005' 
            //),m2 as(select t1.locno locno,to_char(descr40) as EN,'' as ZH from MDM_LOC t1,(select count(*),locno from MDM_LOC having count(*) < 2 group by locno) t2 where t1.locno = t2.locno and loctype != '1005' 
            //),m3 as(select locno,max(r) as descr from (select m1.locno as locno,wm_concat(to_char(descr40)) over (partition by m1.locno order by descr40) r from m1) group by locno 
            //),m4 as(select m3.locno as LOCNO,REGEXP_SUBSTR(wm_concat(m3.descr), '[^,]+', 1, 1, 'i') as EN,REGEXP_SUBSTR(wm_concat(m3.descr), '[^,]+', 1, 2, 'i')  as ZH from m3 group by m3.locno union select * from m2) 
            //select LOCNO as RID,EN as CODE,ZH as NAME from m4 where (locno like '%{0}%' or EN like '%{0}%' or ZH like '%{0}%' or locno like '%{1}%' or EN like '%{1}%' or ZH like '%{1}%') and rownum < 20 order by locno asc", qstr.ToLower(), qstr.ToUpper(), strWhereLOCTYPE);
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }

        public ActionResult GetLOCDescription(string qstr)
        {
            string content = "";
            try
            {
                string sql = string.Format("SELECT DESCR40 FROM MDM_LOC WHERE LOCNO = '{0}'", qstr.ToUpper());
                content = (string)DataHelper.QueryValue(sql);
            }
            catch { }

            return Content(content);
        }

        public ActionResult GetDatasProduct(string q)
        {
            string qstr = this.Request["q"].ToString() + "";
            //过滤掉运输区域1005
            string sql = string.Format("SELECT PRODUCTKEY CODE, PRODUCTNAME NAME FROM MDM_PRODUCT WHERE (PRODUCTKEY LIKE '%{0}%' OR PRODUCTNAME LIKE '%{0}%' OR PRODUCTKEY LIKE '%{1}%' OR PRODUCTNAME LIKE '%{1}%') AND rownum<20 ORDER BY PRODUCTKEY ASC", qstr.ToLower(), qstr.ToUpper());
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }
        public ActionResult GetProductDescription(string qstr)
        {
            string content = "";
            try
            {
                string sql = string.Format("SELECT PRODUCTNAME FROM MDM_PRODUCT WHERE PRODUCTKEY = '{0}'", qstr.ToUpper());
                content = (string)DataHelper.QueryValue(sql);
            }
            catch { }

            return Content(content);
        }

        //public ActionResult GetDatasORG(string q)
        //{
        //    string qstr = this.Request["q"].ToString() + "";

        //    FLD_QO_USER qouser = SessionHelper.GetSessionUser<FLD_QO_USER>();
        //    string orgids = "";
        //    try
        //    {
        //        orgids = qouser.htExt["QO_ORGID"].ToString();
        //    }
        //    catch { }

        //    orgids = orgids.TrimStart('[').TrimEnd(']');
        //    orgids = string.IsNullOrEmpty(orgids) ? "''" : orgids;

        //    string sql = string.Format("SELECT ltrim(OBJID,'0') RID, ORGKEY CODE, ORGNAME NAME FROM V_MDM_ORG WHERE (ORGKEY LIKE '%{0}%' OR ORGNAME LIKE '%{0}%' OR ORGKEY LIKE '%{1}%' OR ORGNAME LIKE '%{1}%') AND ltrim(OBJID,'0') IN ({2}) AND rownum<20 ORDER BY ORGKEY ASC", qstr.ToLower(), qstr.ToUpper(), orgids);
        //    DataTable dtsearch = DataHelper.QueryDataTable(sql);

        //    string content = JsonHelper.GetJsonString(dtsearch);
        //    return Content(content);
        //}

        public ActionResult GetDatasORG(string q, string spo)
        {
            string qstr = this.Request["q"].ToString() + "";
            string spostr = this.Request["spo"].ToString() + "";
            string filterstr = " ";

            if (spostr.ToUpper() == "P")
            {
                filterstr = " AND PFLG = 'X' ";
            }
            else if (spostr.ToUpper() == "S")
            {
                filterstr = " AND SFLG = 'X' ";
            }

            string sql = string.Format("SELECT ltrim(OBJID,'0') RID, ORGKEY CODE, ORGNAME NAME FROM V_MDM_ORG WHERE (ORGKEY LIKE '%{0}%' OR ORGNAME LIKE '%{0}%' OR ORGKEY LIKE '%{1}%' OR ORGNAME LIKE '%{1}%') {2}  AND rownum<20 ORDER BY ORGKEY ASC", qstr.ToLower(), qstr.ToUpper(), filterstr);
            DataTable dtsearch = DataHelper.QueryDataTable(sql);

            string content = JsonHelper.GetJsonString(dtsearch);
            return Content(content);
        }

        public ActionResult GetDatasLock(string ztype, string zlock_internal, string zlock_external)
        {
            DataTable result = new DataTable();
            result = DataHelper.QueryDataTable(" SELECT lower(FIELD) LOCKFIELD FROM QO_ET_SDPZ WHERE 1 != 1");

            DataTable dtzlock_internal = new DataTable();
            DataTable dtzlock_external = new DataTable();
            if (zlock_internal + "" == "01")
            {
                string sqlzlock_internal = string.Format("SELECT lower(FIELD) LOCKFIELD FROM QO_ET_SDPZ WHERE ZTYPE_YW = (  SELECT ZTYPE_YW FROM QO_ET_SDPZ_S WHERE ZTYPE = '{0}' ) AND ZLOCK_INTERNAL = '{1}'", ztype, zlock_internal);
                dtzlock_internal = DataHelper.QueryDataTable(sqlzlock_internal);
            }
            if (zlock_external + "" == "01")
            {
                string sqlzlock_external = string.Format("SELECT lower(FIELD) LOCKFIELD FROM QO_ET_SDPZ WHERE ZTYPE_YW = (  SELECT ZTYPE_YW FROM QO_ET_SDPZ_S WHERE ZTYPE = '{0}' ) AND ZLOCK_EXTERNAL = '{1}'", ztype, zlock_external);
                dtzlock_external = DataHelper.QueryDataTable(sqlzlock_external);
            }

            if (dtzlock_internal != null && dtzlock_internal.Rows.Count > 0)
            {
                result.Merge(dtzlock_internal);
            }
            if (dtzlock_external != null && dtzlock_external.Rows.Count > 0)
            {
                result.Merge(dtzlock_external);
            }

            //rn Content(new JsonMessage { Success = true, Data = Distinct(result, new string[] { "LOCKFIELD" }), Code = "1", Message = "" }.ToString());
            return Content("");
        }

        public static DataTable Distinct(DataTable dt, string[] filedNames)
        {
            DataView dv = dt.DefaultView;
            DataTable DistTable = dv.ToTable("DISTRSLT", true, filedNames);
            return DistTable;
        }
    }
}
