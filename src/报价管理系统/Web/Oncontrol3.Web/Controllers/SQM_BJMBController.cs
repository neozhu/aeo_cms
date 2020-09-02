using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Oncontrol3.Web.Models;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Security;
using NHibernate.Criterion;
using Oncontrol3.Web;
using System.Security.Principal;
using System.Runtime.Remoting.Contexts;
using Aim;
using Aim.Data;
using Aim.Portal.Web;
using Com.Feiliks.MDM;
using Com.Feiliks.QDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.OracleClient;
using Oncontrol3.Web.Helpers;
using System.Collections;
using System.Reflection;

//using static Oncontrol3.Web.Controllers.QM_PriceController;
//using Oncontrol3.Web.BJServiceReference;

namespace Oncontrol3.Web.Controllers
{

    public class SQM_BJMBController : BaseController
    {
        [AllowAnonymous]
        public ActionResult SQM_BJMBIndex()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            DataTable Orgdt = DataHelper.QueryDataTable("select ltrim(OBJID,'0') RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(ltrim(OBJID,'0'))=4 order by ltrim(OBJID,'0')");
            //DataTable Orgdt = DataHelper.QueryDataTable("select RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(RID)=4 order by RID");
            ViewBag.ORG = Orgdt;
            return View();
        }

        [AllowAnonymous]
        public ActionResult SQM_BJMBEdit()
        {
            UpdateDict();
            ViewBag.manager = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select distinct code from sqm_hkydic where type = 'mb'"));
            ViewBag.feedic = JsonHelper.GetJsonString(DataHelper.QueryDataTable("select distinct TCET084 as feecode,TEXTDESC as feename from V_MDM_FEE where TCET084 not in (select TCET084 from MDM_SRV_FEE_REF)"));
            //DataTable Orgdt = DataHelper.QueryDataTable("select RID,ORGNAME from V_MDM_ORG where SFLG is null AND length(RID)=4 order by RID");
            //ViewBag.ORG = Orgdt;
            ViewBag.srvbj = ConfigHelper.AppSettings("srvbj");
            return View();
        }

        private static DataTable dtdjpsfdict = new DataTable();
        private static DataTable dtbjpsfdict = new DataTable();
        private static DataTable dtverdict = new DataTable();
        /// <summary>
        /// 保存模板头部信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitForm(string RID, string ORGRID, string postdata)
        {
            string rtnmsg = "保存成功！";
            bool flag = true;
            bool ifCreate = false;
            SQM_BJMB mbobj = new SQM_BJMB();
            if (!string.IsNullOrEmpty(postdata))
            {
                mbobj = JsonHelper.GetObject<SQM_BJMB>(postdata);
            }
            try
            {
                //通过有无rid来判断做新增还是修改操作
                if (RID == "")
                {
                    ifCreate = true;
                    //新增
                    RID = System.Guid.NewGuid().ToString();
                    mbobj.RID = RID;
                    mbobj.TEMPLATENAME = "模板A" + DateTime.Now.ToString("yyMMdd") + DataHelper.QueryValue("select seq_mb_number.NEXTVAL from dual");// 添加默认模板名称  格式：模板+A180101+5位流水号 A表示20
                    mbobj.TEMPLATETYPE = "个人模板";
                    mbobj.STATUS = "1";// 启用
                    mbobj.MODIFYNAME = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    mbobj.ORGNAME = ORGRID;
                    mbobj.DoCreate();
                }
                else
                {
                    //修改
                    SQM_BJMB targetobj = SQM_BJMB.TryFind(RID);
                    targetobj.TEMPLATENAME = mbobj.TEMPLATENAME;
                    targetobj.TEMPLATETYPE = mbobj.TEMPLATETYPE;
                    targetobj.STATUS = mbobj.STATUS;
                    targetobj.SORD = mbobj.SORD;
                    targetobj.REMARK = mbobj.REMARK;
                    targetobj.MODIFYNAME = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    targetobj.DoUpdate();
                }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            if (ifCreate)
            {
                return Content(JsonHelper.GetJsonString(mbobj));
            }
            else
            {
                return Content(JsonHelper.GetJsonString(new JsonMessage { Data = RID, Message = rtnmsg, Success = flag }));
            }
        }

        /// <summary>
        /// 基本数据获取
        /// </summary>
        /// <param name="RID"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string RID)
        {
            string sql = string.Format("SELECT * FROM SQM_BJMB WHERE RID = '{0}'", RID);
            var data = DataHelper.QueryDictList(sql)[0];
            return Content(JsonHelper.GetJsonString(data));
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()// 模板名称 状态 模板类型
        {
            string sql;
            //查询条件拼接
            string wherestr = "WHERE (1=1) ";
            string status = Request["STATUS"] + "";//模板类型
            string TEMPLATENAME = Request["TEMPLATENAME"] + "";//模板名称
            string PERSONALMB = Request["PERSONALMB"] + "";//个人模板
            string PUBLICMB = Request["PUBLICMB"] + "";//公共模板
            // 添加账号筛选
            string createname = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();

            wherestr += "and (createname <> '' or createname is not null)";
            if (PUBLICMB == "公共模板" && PERSONALMB == "个人模板")
            {
                wherestr += " and (TEMPLATETYPE = '公共模板' or (createname = '" + createname + "' and TEMPLATETYPE = '个人模板'))";
            }
            else if (PUBLICMB == "" && PERSONALMB == "个人模板")
            {
                wherestr += " and createname = '" + createname + "' and TEMPLATETYPE = '个人模板'";
            }
            else if (PUBLICMB == "公共模板" && PERSONALMB == "")
            {
                wherestr += " and TEMPLATETYPE = '公共模板'";
            }
            else if (PUBLICMB == "" && PERSONALMB == "")
            {
                wherestr += " and TEMPLATETYPE = ''";
            }
            if (TEMPLATENAME != "")
            {
                wherestr += " and TEMPLATENAME like '%" + TEMPLATENAME + "%'";
            }
            if (status != "")
            {
                wherestr += " AND STATUS = '" + status + "'";
            }

            sql = string.Format("SELECT * FROM SQM_BJMB " + wherestr + "ORDER BY CREATETIME desc,{0} {1}", Request["sort"].Trim(), Request["order"].Trim());
            // 设置分页
            string sql_page = "With DATASET AS(select A.*,ROWNUM As RN from ({0}) A) select * from DATASET WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            // 数据数量
            string countsql = string.Format("SELECT COUNT(*) FROM (" + sql + ")");
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            var obj = new
            {
                draw = Request["draw"],
                data = rtndata,
                recordsTotal = rtntotal,
                recordsFiltered = rtntotal
            };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 模板产品服务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GETMBPSF()
        {
            var rid = Request["RID"];
            var vrid = SQM_BJMB.TryFind(rid).VERID;
            string sql = string.Format("SELECT * FROM sqm_bj_psf WHERE VRID = '{0}'", vrid);
            //分页
            string sql_page = "With DATASET AS( select A.*,ROWNUM As RN from ({0}) A) select * from DATASET  WHERE RN between {1} and {2}";
            sql_page = string.Format(sql_page, sql, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + 1, (SearchCriterion.CurrentPageIndex - 1) * SearchCriterion.PageSize + SearchCriterion.PageSize);
            string countsql = string.Format("SELECT COUNT (*)  FROM sqm_bj_psf WHERE VRID = '{0}'", vrid);
            var rtntotal = DataHelper.QueryValue(countsql);
            var rtndata = DataHelper.QueryDataTable(sql_page);
            var obj = new
            {
                draw = Request["draw"],
                data = rtndata,
                recordsTotal = rtntotal,
                recordsFiltered = rtntotal
            };
            return Content(JsonHelper.GetJsonString(obj));
        }
        /// <summary>
        /// 选择产品服务费目保存到模板中
        /// </summary>
        /// <param name="postdata"></param>
        /// <param name="keyvalue"></param>
        /// <param name="prdcode">未绑定关系费目使用</param>
        /// <param name="srvcode">未绑定关系费目使用</param>
        /// <param name="orgcode">组织机构添加使用</param>
        /// <param name="orgname">组织机构添加使用</param>
        /// <param name="aloenfee"></param>
        /// <returns></returns>
        public ActionResult SaveToMBPSF(string postdata, string keyvalue, string prdcode, string srvcode, string orgcode, string orgname, string aloenfee = "")
        {
            string vrid = System.Guid.NewGuid().ToString();// 版本表主键
            var mbobj = SQM_BJMB.TryFind(keyvalue);
            // 判断有无版本表数据 新建模板，相当于没有报价
            if (mbobj != null)
            {
                if (mbobj.VERID == null)
                {
                    mbobj.VERID = vrid;
                    mbobj.DoUpdate();
                    SQM_BJ_VER verobj = new SQM_BJ_VER();
                    verobj.RID = vrid;
                    verobj.DoCreate();
                }
                else
                {
                    vrid = mbobj.VERID;
                }
            }
            // 查询原来有的数据
            string sql = string.Format("SELECT RID,VRID,PRODUCT_CODE,SERVICE_CODE,FEE_CODE,ALOENFEE,ORGCODE,ORGNAME FROM SQM_BJ_PSF WHERE VRID = '{0}' and (status <> '0' or status is null) and (FEECATG<>'2' OR FEECATG is null)", vrid);
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (dt.Rows.Count > 0)// 如果有数据说明是修改，如果没有数据，则orgcode、orgname
            {
                orgcode = dt.Rows[0]["ORGCODE"].ToString();
                orgname = dt.Rows[0]["ORGNAME"].ToString();
            }
            // 保存进来的新数据
            List<QM_PriceController.PRD> dataArray = JsonHelper.GetObject<List<QM_PriceController.PRD>>(postdata);
            var rtnmessage = "保存成功";
            try
            {
                foreach (var p in dataArray)
                {
                    //查询现有的服务
                    string sersql = string.Format("select distinct SERVICE_CODE from SQM_BJ_PSF where VRID='{0}' and PRODUCT_CODE='{1}' and FEECATG='2'", vrid, p.prdcode);
                    DataTable srvDt = DataHelper.QueryDataTable(sersql);
                    foreach (var s in p.srvcodes)
                    {
                        foreach (var f in s.feecodes)
                        {
                            string where = "";
                            if (aloenfee == "1")
                            {
                                where = " and ALOENFEE = '1'";
                            }
                            //先查询是否有这条数据
                            DataRow[] drs = dt.Select("PRODUCT_CODE = '" + p.prdcode + "' and SERVICE_CODE = '" + s.srvcode + "' and FEE_CODE = '" + f + "' and VRID = '" + vrid + "'" + where);// 取第一行，估计没有多行的吧
                            if (drs.Length <= 0)// 说明原来有的，现在还有，则数据不变
                            {
                                SQM_BJ_PSF srcobj = new SQM_BJ_PSF();
                                srcobj.MRID = keyvalue;
                                srcobj.VRID = vrid;
                                srcobj.CHOOSESTATUS = "1";
                                srcobj.PRODUCT_CODE = p.prdcode;
                                srcobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                                srcobj.SERVICE_CODE = s.srvcode;
                                srcobj.SERVICE_NAME = CODETONAME("srv", s.srvcode);
                                srcobj.FEE_CODE = f;
                                srcobj.FEE_NAME = CODETONAME("fee", f);
                                srcobj.BJSTATAUS = "0";
                                srcobj.STATUS = "1";
                                if (aloenfee == "1")
                                {
                                    srcobj.ALOENFEE = "1";
                                }
                                srcobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                srcobj.ORGCODE = orgcode;
                                srcobj.ORGNAME = orgname;
                                srcobj.DoCreate();
                            }
                            else
                            {
                                dt.Rows.Remove(drs[0]);// 删除共有的，剩余的就是要从表中删除的数据
                            }
                        }
                        //该服务下所有的atcost费目存入SQM_BJ_PSF
                        SQM_BJ_PSF[] sbpArr = SQM_BJ_PSF.FindAllByProperties(SQM_BJ_PSF.Prop_PRODUCT_CODE, p.prdcode, SQM_BJ_PSF.Prop_SERVICE_CODE, s.srvcode, SQM_BJ_PSF.Prop_VRID, vrid, SQM_BJ_PSF.Prop_FEECATG, "2");
                        if (sbpArr.Length == 0)
                        {
                            string costsql = string.Format("select PRODCODE,SRVCODE,FEECODE from SQM_SRV_FEE_CONFIG t where PRODCODE='{0}' and SRVCODE='{1}' and FEECATG='2'", p.prdcode, s.srvcode);
                            DataTable costDt = DataHelper.QueryDataTable(costsql);
                            foreach (DataRow costdr in costDt.Rows)
                            {
                                SQM_BJ_PSF sbpobj = new SQM_BJ_PSF();
                                sbpobj.MRID = keyvalue;
                                sbpobj.VRID = vrid;
                                sbpobj.CHOOSESTATUS = "1";
                                sbpobj.PRODUCT_CODE = p.prdcode;
                                sbpobj.PRODUCT_NAME = CODETONAME("prd", p.prdcode);
                                sbpobj.SERVICE_CODE = s.srvcode;
                                sbpobj.SERVICE_NAME = CODETONAME("srv", s.srvcode);
                                sbpobj.FEE_CODE = costdr["FEECODE"].ToString();
                                sbpobj.FEE_NAME = CODETONAME("fee", costdr["FEECODE"].ToString());
                                sbpobj.BJSTATAUS = "2";
                                sbpobj.STATUS = "1";
                                sbpobj.BJFS = "1";
                                sbpobj.FEECATG = "2";
                                sbpobj.MINSTATUS = "0";
                                if (aloenfee == "1")
                                {
                                    sbpobj.ALOENFEE = "1";
                                }
                                sbpobj.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                                sbpobj.ORGCODE = orgcode;
                                sbpobj.ORGNAME = orgname;
                                sbpobj.DoCreate();
                            }
                        }
                        else
                        {
                            DataRow[] srvdrs = srvDt.Select("SERVICE_CODE='" + s.srvcode + "'");
                            if (srvdrs.Length > 0)
                            {
                                srvDt.Rows.Remove(srvdrs[0]);//服务存在的不做处理
                            }
                        }
                    }
                    string srvstr = "";
                    foreach (DataRow srvDr in srvDt.Rows)
                    {
                        srvstr += srvDr["SERVICE_CODE"].ToString() + ",";
                    }
                    if (!String.IsNullOrEmpty(srvstr))
                    {
                        string deletesrv = string.Format("delete from SQM_BJ_PSF where vrid='{0}' and PRODUCT_CODE='{1}' and SERVICE_CODE in('{2}') and FEECATG='2'", vrid, p.prdcode, srvstr.TrimEnd(',').Replace(",", "','"));
                        DataHelper.ExecSql(deletesrv);
                    }
                }
                // 删除psf表未保存的原有数据，并清空值表数据 保留产品（只剩一行数据）
                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        string deletepsf = "delete from sqm_bj_psf where rid = '" + dr["RID"] + "'";
                //        string deleteval = "delete from sqm_modebj_val where feecalcid = '" + dr["RID"] + "'";
                //        DataHelper.ExecSql("begin " + deletepsf + ";" + deleteval + ";end;");
                //    }
                //}
                // 删除psf表未保存的原有数据，并清空值表数据
                if (String.IsNullOrEmpty(aloenfee))
                {
                    DataRow[] feedrs = dt.Select("ALOENFEE is null");
                    foreach (DataRow feedr in feedrs)
                    {
                        string deletepsf = "delete from sqm_bj_psf where rid = '" + feedr["RID"] + "'";
                        string deleteval = "delete from sqm_modebj_val where feecalcid = '" + feedr["RID"] + "'";
                        DataHelper.ExecSql("begin " + deletepsf + ";" + deleteval + ";end;");
                    }
                }
                else
                {
                    DataRow[] dlfeedrs = dt.Select("PRODUCT_CODE = '" + prdcode + "' and SERVICE_CODE='" + srvcode + "' and ALOENFEE = '1'");
                    foreach (DataRow dlfeedr in dlfeedrs)
                    {
                        string deletepsf = "delete from sqm_bj_psf where rid = '" + dlfeedr["RID"] + "'";
                        string deleteval = "delete from sqm_modebj_val where feecalcid = '" + dlfeedr["RID"] + "'";
                        DataHelper.ExecSql("begin " + deletepsf + ";" + deleteval + ";end;");
                    }
                }
            }
            catch (Exception ex)
            {
                rtnmessage = ex.Message;
            }
            // 更新字典
            UpdateDict();
            return Content(JsonHelper.GetJsonString(vrid));
        }
        /// <summary>
        /// 根据code得到name（描述）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string CODETONAME(string type, string code)
        {
            var name = "";
            switch (type)
            {
                case "prd":
                    var prdlist = DataHelper.QueryDictList("SELECT SQM_PRD_EXT.SQPRODUCTNAME,SQM_PRD_EXT.PRODUCTKEY FROM SQM_PRD_EXT");
                    foreach (var prditem in prdlist)
                    {
                        if (prditem.Get("PRODUCTKEY").ToString() == code)
                        {
                            name = prditem.Get("SQPRODUCTNAME").ToString();
                        }
                    }
                    break;
                case "srv":
                    var srvlist = DataHelper.QueryDictList("SELECT MDM_SERVICE.SERVICETYPE,MDM_SERVICE.SERVICENAME FROM MDM_SERVICE");
                    foreach (var srvitem in srvlist)
                    {
                        if (srvitem.Get("SERVICETYPE").ToString() == code)
                        {
                            name = srvitem.Get("SERVICENAME").ToString();
                        }
                    }
                    break;
                case "fee":
                    var feelist = DataHelper.QueryDictList("SELECT MDM_FEE.TCET084,MDM_FEE.TEXTDESC FROM V_MDM_FEE");
                    foreach (var feeitem in feelist)
                    {
                        if (feeitem.Get("TCET084").ToString() == code)
                        {
                            name = feeitem.Get("TEXTDESC").ToString();
                        }
                    }
                    break;
            }
            return name;
        }
        /// <summary>
        /// 得到报价psf表产品服务code
        /// </summary>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public string showPSF(string vrid)
        {
            //psf表信息
            DataTable dt = DataHelper.QueryDataTable("select * from sqm_bj_psf where vrid = '" + vrid + "' and (status <> '0' or status is null) and (bgfzrid is null or bgfzrid = '1') order by product_code,service_code,fee_code");// and bjstataus is not null
            // 两层结构 的数据结构  思路：遍历表数据 碰到prdcode变化则生成一个产品对应服务费目数据  碰到srvcode变化则生成一个服务对应费目数据
            Dictionary<string, List<Dictionary<string, List<string>>>> psfDict = new Dictionary<string, List<Dictionary<string, List<string>>>>();
            Dictionary<string, List<string>> srv_feeDict = new Dictionary<string, List<string>>();
            List<Dictionary<string, List<string>>> psfList = new List<Dictionary<string, List<string>>>();
            List<string> feeList = new List<string>();
            string oldPrdCode = "";
            string oldSrvCode = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string prdcode = dr["PRODUCT_CODE"] + "";
                    if (oldPrdCode == "")
                    {
                        oldPrdCode = prdcode;
                    }

                    string srvcode = dr["SERVICE_CODE"] + "";
                    if (oldSrvCode == "")
                    {
                        oldSrvCode = srvcode;
                    }

                    string feecode = dr["FEE_CODE"] + "";
                    if (oldSrvCode == srvcode && oldPrdCode == prdcode)
                    {
                        feeList.Add(feecode);
                    }
                    // 如果srvcode没变，判断prdcode是否改变，如果改变则添加
                    if (oldSrvCode != srvcode || oldPrdCode != prdcode)
                    {
                        if (!srv_feeDict.ContainsKey(oldSrvCode))
                        {
                            srv_feeDict.Add(oldSrvCode, new List<string>(feeList));// 构造器方法复制，值复制
                            Dictionary<string, List<string>> srv_feeDict_copy = new Dictionary<string, List<string>>(srv_feeDict);// 构造器方法复制，值复制
                            psfList.Add(srv_feeDict_copy);
                            feeList.Clear();
                            srv_feeDict.Clear();
                            feeList.Add(feecode);// 如果srvcode改变，则要存当前srvcode对应的feecode作为feeList的第一个元素
                        }
                        oldSrvCode = srvcode;
                    }
                    if (oldPrdCode != prdcode)
                    {
                        if (!psfDict.ContainsKey(oldPrdCode))
                        {
                            psfDict.Add(oldPrdCode, new List<Dictionary<string, List<string>>>(psfList));// 构造器方法复制，将引用类型转换成值类型
                            psfList.Clear();
                        }
                        oldPrdCode = prdcode;
                    }
                }
                // 最后一个
                srv_feeDict.Add(oldSrvCode, new List<string>(feeList));// 构造器方法复制，值复制
                Dictionary<string, List<string>> srv_feeDict_copy1 = new Dictionary<string, List<string>>(srv_feeDict);// 构造器方法复制，值复制
                psfList.Add(srv_feeDict_copy1);
                psfDict.Add(oldPrdCode, new List<Dictionary<string, List<string>>>(psfList));// 构造器方法复制，将引用类型转换成值类型
            }
            ArrayList arrayList = new ArrayList();
            arrayList.Add("nomessage");
            arrayList.Add(psfDict);
            return JsonHelper.GetJsonString(arrayList);
        }
        public ActionResult GetAllByRid(string vrid, string orgcode)
        {
            //从crm表拿出所有已经在产品模块中维护的产品 -- 现在取报价系统产品扩展表产品数据
            //IDbConnection conn = new OracleConnection
            //{
            //    ConnectionString = ConfigHelper.AppSettings("connection_crm")
            //};
            //if (conn.State != ConnectionState.Open)
            //{
            //    conn.Open();
            //}
            var prdDataTable = DataHelper.QueryDataTable(@"SELECT * FROM (
       SELECT PRODUCTKEY as PRDCODE,
              SQPRODUCTNAME as PRDNAME,
              CREATETIME,
              BUSINESSORG AS DIVISION,
              '' AS PRODUCTSCODE
       FROM SQM_PRD_EXT 
       WHERE BUSINESSORG = '海运' 
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM (
       SELECT PRODUCTKEY as PRDCODE,
              SQPRODUCTNAME as PRDNAME,
              CREATETIME,
              BUSINESSORG AS DIVISION,
              '' AS PRODUCTSCODE
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG = '供应链' 
       and PRODUCTKEY is not null
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM ( 
       SELECT PRODUCTKEY as PRDCODE,
              SQPRODUCTNAME as PRDNAME,
              CREATETIME,
              BUSINESSORG AS DIVISION,
              '' AS PRODUCTSCODE 
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG = '空运' 
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)
UNION ALL 
SELECT * FROM ( 
       SELECT PRODUCTKEY as PRDCODE,
              SQPRODUCTNAME as PRDNAME,
              CREATETIME,
              BUSINESSORG AS DIVISION,
              '' AS PRODUCTSCODE
       FROM SQM_PRD_EXT  
       WHERE BUSINESSORG like '%运输%'
       and PRODUCTKEY is not null 
       and status = '1'
       ORDER BY CREATETIME DESC
)");
            //产品code字符串拼接
            string prdStr = "";
            for (var i = 0; i < prdDataTable.Rows.Count; i++)
            {
                prdStr += "'" + prdDataTable.Rows[i]["PRDCODE"].ToString().Trim() + "',";
            }
            //产品，服务，费目查询
            //            string sqlAll = string.Format(@"With DATASET AS(select distinct mdm_prd_srv_ref.productcode as PRDCODE,
            //       sqm_prd_ext.sqproductname as PRDNAME, 
            //       mdm_prd_srv_ref.servicetypecode as SRVCODE,
            //       mdm_service.servicename as SRVNAME, 
            //       mdm_srv_fee_ref.tcet084 as FEECODE,
            //       mdm_fee.textdesc as FEENAME,
            //       qdm_fee_srv_ref.bxbj,
            //       qdm_fee_srv_ref.sorid 
            //from mdm_prd_srv_ref 
            //left join mdm_srv_fee_ref on mdm_srv_fee_ref.srvrqcd121 = mdm_prd_srv_ref.servicetypecode 
            //left join sqm_prd_ext on mdm_prd_srv_ref.productcode = sqm_prd_ext.productkey 
            //left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype 
            //left join V_MDM_FEE mdm_fee on mdm_srv_fee_ref.tcet084 = mdm_fee.tcet084 
            //left join qdm_fee_srv_ref on qdm_fee_srv_ref.rid = mdm_srv_fee_ref.rid 
            //where mdm_prd_srv_ref.productcode in ({0})
            //order by qdm_fee_srv_ref.SORID) select t1.*,t2.feecatg from DATASET t1 inner join SQM_SRV_FEE_CONFIG t2 on t1.PRDCODE=t2.Prodcode and t1.SRVCODE=t2.srvcode and t1.FEECODE=t2.feecode and t2.feecatg<>'2'", prdStr.TrimEnd(','));
            string sqlAll = string.Format(@"select distinct c.PRODCODE as PRDCODE,c.PRODNAME as PRDNAME,c.SRVCODE,c.SRVNAME,c.FEECODE,c.FEENAME,r.BXBJ,r.SORID,c.FEECATG,f.FSFYSMS from SQM_SRV_FEE_CONFIG c left join QDM_FEE_SRV_REF r on c.PRODCODE=r.Productcode and c.SRVCODE=r.SERVICETYPECODE and c.FEECODE=r.FEECODE left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on c.FEECODE = f.FEECODE where c.FEECATG<>'2' and c.PRODCODE in({0}) order by r.SORID", prdStr.TrimEnd(','));
            var feeArray = DataHelper.QueryDictList(sqlAll);
            List<EasyDictionary> newfeeArr = new List<EasyDictionary>(feeArray);
            foreach (var fee in feeArray)
            {
                string feecalcid = "";
                //异常费目再根据有效定价来控制是否展现
                if (fee["FEECATG"].ToString() == "1")
                {
                    string sql4 = string.Format("select RID from SQM_DJ_PSF t where PRDCODE='{0}' and SRVCODE='{1}' and FEECODE='{2}' and ORGRID like'%{3}%'", fee["PRDCODE"].ToString(), fee["SRVCODE"].ToString(), fee["FEECODE"].ToString(), orgcode);
                    DataTable djpsfDt = DataHelper.QueryDataTable(sql4);
                    if (djpsfDt.Rows.Count == 0)
                    {
                        newfeeArr.Remove(fee);//SQM_DJ_PSF没有就不显示
                        continue;
                    }
                    else
                    {
                        feecalcid = djpsfDt.Rows[0]["RID"].ToString();
                        string sql5 = string.Format("select RID from SQM_MODEDJ_VAL where FEECALCID='{0}' and STATUS='1' and DJSTATUS='1'", feecalcid);
                        DataTable djvalDt = DataHelper.QueryDataTable(sql5);
                        if (djvalDt.Rows.Count == 0)
                        {
                            newfeeArr.Remove(fee);//SQM_MODEDJ_VAL没有就不显示
                            continue;
                        }
                    }
                }
            }

//            string sqlPS = string.Format(@"select distinct mdm_prd_srv_ref.productcode as PRDCODE,
//       mdm_prd_srv_ref.servicetypecode as SRVCODE,
//       mdm_service.servicename as SRVNAME,
//       SQM_SRV_EXT.Sord
//from mdm_prd_srv_ref
//left join mdm_service on mdm_prd_srv_ref.servicetypecode = mdm_service.servicetype
//LEFT JOIN SQM_SRV_EXT ON MDM_PRD_SRV_REF.SERVICETYPECODE = SQM_SRV_EXT.SERVICEKEY 
//LEFT JOIN SQM_SRV_FEE_CONFIG ON MDM_PRD_SRV_REF.PRODUCTCODE= SQM_SRV_FEE_CONFIG.PRODCODE AND MDM_PRD_SRV_REF.SERVICETYPECODE=SQM_SRV_FEE_CONFIG.SRVCODE 
//where mdm_prd_srv_ref.productcode in ({0}) AND SQM_SRV_FEE_CONFIG.SRVDISP='1' order by SQM_SRV_EXT.Sord", prdStr.TrimEnd(','));
            string sqlPS = string.Format(@"select distinct c.PRODCODE as PRDCODE,c.SRVCODE,c.SRVNAME,e.SORD from SQM_SRV_FEE_CONFIG c left join SQM_SRV_EXT e on c.PRODCODE=e.PRODUCTCODE and c.SRVCODE=e.SERVICEKEY where  c.PRODCODE in({0}) and c.SRVDISP = '1' order by e.Sord", prdStr.TrimEnd(','));
            var srvArray = DataHelper.QueryDictList(sqlPS);
            // 已选的产品
            var SelectedPRDArr = DataHelper.QueryDataTable("select distinct product_code,product_name from sqm_bj_psf where vrid = '" + vrid + "' and (status <> '0' or status is null)");
            object[] data = { prdDataTable, srvArray, newfeeArr, SelectedPRDArr };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 通过产品 服务 费目、组织的code在定价psf以及报价psf表里面拿rid
        /// </summary>
        /// <param name="prdcode"></param>
        /// <param name="srvcode"></param>
        /// <param name="feecode"></param>
        /// <param name="orgrid"></param>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public string GetRid(string prdcode, string srvcode, string feecode, string orgrid, string alonefee, string vrid)
        {
            string djrid = "";
            string bjrid = "";
            string bjstatus = "";
            if (alonefee == "0")
            {
                //DataRow[] drsdj = dtdjpsfdict.Select("PRDCODE = '" + prdcode + "' and SRVCODE = '" + srvcode + "' and FEECODE = '" + feecode + "' and (ORGRID is null or ORGRID = '')");
                DataRow[] drsdj = dtdjpsfdict.Select("PRDCODE = '" + prdcode + "' and SRVCODE = '" + srvcode + "' and FEECODE = '" + feecode + "' and orgrid = '" + orgrid + "'");
                if (drsdj.Length > 0)
                {
                    djrid = drsdj[0]["RID"] + "";
                }
            }
            else
            {
                djrid = DataHelper.QueryValue(string.Format("select RID from SQM_DJ_PSF where ALONEFEE = '1' and FEECODE = '{0}' and ORGRID is null", feecode)) + "";
            }
            DataRow[] drsbj = dtbjpsfdict.Select("PRODUCT_CODE = '" + prdcode + "' and SERVICE_CODE = '" + srvcode + "' and FEE_CODE = '" + feecode + "' and VRID = '" + vrid + "'");
            if (drsbj.Length > 0)
            {
                bjrid = drsbj[0]["RID"] + "";
                bjstatus = drsbj[0]["BJSTATAUS"] + "";
            }
            List<string> ridList = new List<string> { djrid, bjrid, bjstatus };
            return JsonHelper.GetJsonString(ridList);
        }
        /// <summary>
        /// 初始化字典
        /// </summary>
        private static void UpdateDict()
        {
            // 初始化字典
            dtdjpsfdict = DataHelper.QueryDataTable("select RID,PRDCODE,SRVCODE,FEECODE,ORGRID from sqm_dj_psf");
            dtbjpsfdict = DataHelper.QueryDataTable("select RID,VRID,BJSTATAUS,PRODUCT_CODE,SERVICE_CODE,FEE_CODE from sqm_bj_psf");
            dtverdict = DataHelper.QueryDataTable("select rid,mrid,zver from sqm_bj_ver");
        }
        /// <summary>
        /// 已有报价中添加报价模板
        /// </summary>
        /// <param name="VERID"></param>
        /// <param name="TEMPLATENAME"></param>
        /// <param name="TEMPLATEJJ"></param>
        /// <param name="REMARK"></param>
        /// <returns></returns>
        public ActionResult SaveToAnother(string MBRID, string TEMPLATETYPE, string TEMPLATENAME, string TEMPLATEJJ, string REMARK)
        {
            bool flag = true;
            string rtnmsg = "保存成功";
            try
            {
                // 根据模板rid查找版本表数据
                string oldvrid = DataHelper.QueryValue("select verid from sqm_bjmb where rid = '" + MBRID + "'") + "";
                string newvrid = System.Guid.NewGuid().ToString();
                // 新增模板库数据，新增一个版本表的主键
                SQM_BJMB srcobj = new SQM_BJMB();
                srcobj.RID = System.Guid.NewGuid().ToString();
                srcobj.VERID = newvrid;
                srcobj.STATUS = "1";
                srcobj.TEMPLATENAME = TEMPLATENAME;
                srcobj.TEMPLATEJJ = TEMPLATEJJ;
                srcobj.REMARK = REMARK;
                srcobj.TEMPLATETYPE = TEMPLATETYPE;
                srcobj.MODIFYNAME = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                srcobj.DoCreate();
                // 新增版本表
                SQM_BJ_VER versrcobj = new SQM_BJ_VER();
                versrcobj.RID = newvrid;
                versrcobj.DoCreate();
                //插入psf表
                //查询psf表需要数据
                var psflist = DataHelper.QueryDictList("SELECT * FROM SQM_BJ_PSF WHERE VRID = '" + oldvrid + "' and (status <> '0' or status is null)");
                foreach (var item in psflist)
                {
                    //拼接插入字符串
                    var keystr = "";
                    var valuestr = "";
                    var newpsfrid = System.Guid.NewGuid().ToString();
                    foreach (string key in item.Keys)
                    {
                        keystr += key + ",";
                        if (key == "VRID")
                        {
                            valuestr += "'" + newvrid + "',";
                        }
                        else if (key == "RID")
                        {
                            valuestr += "'" + newpsfrid + "',";
                        }
                        else if (key == "MRID")
                        {
                            valuestr += "' ',";
                        }
                        else if (key == "CREATETIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString() + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "MODIFYTIME")
                        {
                            valuestr += "to_date('" + DateTime.Now.ToString() + "','YYYY/MM/DD hh24:mi:ss'),";
                        }
                        else if (key == "BJSTARTDATE" || key == "BJENDDATE")
                        {
                            if (item[key] + "" != "")
                            {
                                valuestr += "to_date('" + Convert.ToDateTime(item[key].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                            }
                            else
                            {
                                valuestr += "to_date('" + item[key] + "','yyyy/mm/dd'),";
                            }
                        }
                        else if (key == "BJFS")
                        {
                            if (item[key] + "" == "")
                            {
                                valuestr += "'0',";
                            }
                            else
                            {
                                valuestr += "'" + item[key] + "',";
                            }
                        }
                        else if (key == "BJSTATAUS")
                        {
                            if (item[key] + "" == "2")// 已确认 -> 已保存
                            {
                                valuestr += "'1',";
                            }
                            else if (item[key] + "" == "5")// 已确认(报价超限) -> 已保存(报价超限)
                            {
                                valuestr += "'4',";
                            }
                            else
                            {
                                valuestr += "'" + item[key] + "',";
                            }
                        }
                        else
                        {
                            valuestr += "'" + item[key] + "',";
                        }
                    }
                    string addpsfsql = string.Format("INSERT INTO SQM_BJ_PSF ({0}) VALUES ({1})", keystr.TrimEnd(','), valuestr.TrimEnd(','));
                    DataHelper.ExecSql(addpsfsql);// 插入psf
                    // 查询该psf表对应的值表的所有信息
                    var valuelist = DataHelper.QueryDictList("SELECT * FROM SQM_MODEBJ_VAL WHERE FEECALCID = '" + item["RID"] + "' and (status <> '0' or status is null)");
                    if (valuelist.Count > 0)
                    {
                        foreach (var item1 in valuelist)
                        {
                            var keystr1 = "";
                            var valuestr1 = "";
                            foreach (var key in item1.Keys)
                            {
                                keystr1 += key + ',';
                                if (key == "RID")
                                {
                                    valuestr1 += "'" + System.Guid.NewGuid().ToString() + "',";
                                }
                                else if (key == "FEECALCID")
                                {
                                    valuestr1 += "'" + newpsfrid + "',";
                                }
                                else if (key == "CREATETIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString() + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "MODIFYTIME")
                                {
                                    valuestr1 += "to_date('" + DateTime.Now.ToString() + "','YYYY/MM/DD hh24:mi:ss'),";
                                }
                                else if (key == "STARTDATE")
                                {
                                    valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                }
                                else if (key == "ENDDATE")
                                {
                                    valuestr1 += "to_date('" + item1[key] + "','YYYY/MM/DD'),";
                                }
                                else
                                {
                                    valuestr1 += "'" + item1[key] + "',";
                                }
                            }
                            string addvaluesql = string.Format("INSERT INTO SQM_MODEBJ_VAL ({0}) VALUES ({1})", keystr1.TrimEnd(','), valuestr1.TrimEnd(','));
                            DataHelper.ExecSql(addvaluesql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                rtnmsg = ex.Message;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Success = flag, Message = rtnmsg, Data = "", }));
        }
        /// <summary>
        /// 未绑定关系费目
        /// </summary>
        /// <param name="vrid"></param>
        /// <param name="pcode"></param>
        /// <param name="srvcode"></param>
        /// <returns></returns>
        public ActionResult GetAllFee(string vrid, string pcode, string srvcode)
        {
            var dlfeeArr = DataHelper.QueryObjectsList(string.Format("select FEE_CODE from SQM_BJ_PSF where VRID='{0}' and PRODUCT_CODE='{1}' and SERVICE_CODE='{2}' and ALOENFEE='1'", vrid, pcode, srvcode));
            string where = " and 1=2 ";
            string businessorg = DataHelper.QueryValue(string.Format("select BUSINESSORG from SQM_PRD_EXT where PRODUCTKEY='{0}' and BUSINESSORG is not null", pcode)) + "";
            if (businessorg == "空运")
            {
                where = " and TCET084 like 'A%' ";
            }
            else if (businessorg == "海运")
            {
                where = " and TCET084 like 'O%' ";
            }
            else if (businessorg == "供应链")
            {
                where = " and TCET084 like 'S%' ";
            }
            else if (businessorg == "运输")
            {
                where = " and TCET084 like 'L%' ";
            }
            string sql = string.Format("select distinct  '{0}'as PRODUCTCODE,'{1}' as SERVICETYPECODE,TCET084,TEXTDESC,f.FSFYSMS from V_MDM_FEE c left join (select FEECODE,to_char(wm_concat(to_char(FSFYSM))) as FSFYSMS from (select distinct FEECODE, FSFYSM from SQM_FEE_PUR_REF where FSFYSM is not null) group by FEECODE) f on c.TCET084 = f.FEECODE where TCET084 not in (select TCET084 from MDM_SRV_FEE_REF) and TCET084 in(select distinct FEECODE from SQM_SRV_FEE_CONFIG where PRODCODE is null and SRVCODE is null) and TEXTDESC is not null {2}  order by TEXTDESC", pcode, srvcode, where);
            var feeArray = DataHelper.QueryObjectsList(sql);
            object[] data = { feeArray, dlfeeArr };
            return Content(JsonHelper.GetJsonString(data));
        }
        public static List<string> getFeeIns(string feecode, string vrid)
        {
            List<string> inslist = new List<string>();
            try
            {
                DataTable bjfsdt = null;
                DataTable insdt = new DataTable();
                string[] feecodeArr = feecode.Split(',');
                foreach (string code in feecodeArr)
                {
                    bjfsdt = DataHelper.QueryDataTable(string.Format("select distinct smv.DJFSRID,smv.GDZRID from SQM_MODEBJ_VAL smv left join SQM_BJ_PSF sbp on smv.FEECALCID = sbp.Rid where smv.STATUS='1' and sbp.VRID = '{0}' and sbp.FEE_CODE='{1}'", vrid, feecode));
                    foreach (DataRow bjfsdr in bjfsdt.Rows)
                    {
                        if (!String.IsNullOrEmpty(bjfsdr["GDZRID"].ToString()))
                        {
                            insdt = DataHelper.QueryDataTable("select distinct INSCODE from SQM_CALC_INS sci left join SQM_FEE_CALC_REF sfcr on sci.CALCCODE=sfcr.CALCCODE where STATUS = '1' and FEECODE = '" + code + "' and GDZRID = '" + bjfsdr["GDZRID"].ToString() + "'");
                        }
                        else
                        {
                            insdt = DataHelper.QueryDataTable("select distinct INSCODE from SQM_CALC_INS sci left join SQM_FEE_CALC_REF sfcr on sci.CALCCODE=sfcr.CALCCODE where STATUS = '1' and FEECODE = '" + code + "' and DJFSRID = '" + bjfsdr["DJFSRID"].ToString() + "'");
                        }
                        foreach (DataRow insdr in insdt.Rows)
                        {
                            inslist.Add(insdr["INSCODE"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return inslist;
        }
        // 获取指令集
        public ActionResult ZLJ(string srvcode, string feecodes, string vrid)
        {
            try
            {
                List<string> inslist = getFeeIns(feecodes, vrid);
                string sql = @"select distinct t1.ins_id, t1.ins_description as insname from mdm_ins t1
                left join mdm_insasn t2
                on t1.ins_id =t2.ins_id
                left join mdm_insset t3
                on t2.insset_id=t3.insset_id
                left join mdm_tsr t4
                on t4.ins_set_id=t3.insset_id
                where t4.srvrqcd121='{0}' ";
                if (inslist.Count > 0)
                {
                    string feestr = "  or t1.ins_id in( ";
                    foreach (string feeins in inslist)
                    {
                        feestr += "'" + feeins + "',";
                    }
                    feestr = feestr.Substring(0, feestr.Length - 1);
                    sql += feestr + " )";
                }
                sql = string.Format(sql, srvcode);
                DataTable zljdt = DataHelper.QueryDataTable(sql);
                return Content(JsonHelper.GetJsonString(zljdt));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// DataTable转换成实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    try
                    {
                        if (p.GetSetMethod() != null) // 包含set方法的属性执行赋值
                        {
                            if (dt.Columns.Contains(p.Name))// DataTable是否包含该属性
                            {
                                //if (row[p.Name] is Int64)
                                //{
                                //    p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                                //    continue;
                                //}
                                if (p.PropertyType.FullName.ToString().IndexOf("String") >= 0)
                                {
                                    p.SetValue(entity, row[p.Name].ToString() == "" ? null : row[p.Name], null);
                                }
                                else if (p.PropertyType.FullName.ToString().IndexOf("DateTime") >= 0)
                                {
                                    if (row[p.Name].ToString() == "")
                                    {
                                        p.SetValue(entity, null, null);
                                    }
                                    else
                                    {
                                        p.SetValue(entity, Convert.ToDateTime(row[p.Name]), null);
                                    }
                                }
                                else if (p.PropertyType.FullName.ToString().IndexOf("Decimal") >= 0)
                                {
                                    if (!string.IsNullOrEmpty(row[p.Name].ToString()))
                                    {
                                        p.SetValue(entity, Convert.ToDecimal(row[p.Name]), null);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                list.Add(entity);
            }
            return list;
        }
    }

}
