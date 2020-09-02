using System;
using Castle.ActiveRecord;
using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using System.Web.Mvc;
using Aim.Portal;
using System.Data;
using Oncontrol3.Web.Helpers;
using BaseDLL;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using Aspose.Cells;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Com.Feiliks.QDM;
using System.Web;

namespace Oncontrol3.Web.Controllers
{
    # region  取值字段枚举类
    public enum VALCOLNAME
    {
        COLUMN1,
        COLUMN2,
        COLUMN3,
        COLUMN4,
        COLUMN5,
        COLUMN6,
        COLUMN7,
        COLUMN8,
        COLUMN9,
        COLUMN10,
        COLUMN11,
        COLUMN12,
        COLUMN13,
        COLUMN14,
        COLUMN15,
        COLUMN16,
        COLUMN17,
        COLUMN18,
        COLUMN19,
        COLUMN20,
        COLUMN21,
        COLUMN22,
        COLUMN23,
        COLUMN24,
        COLUMN25,
        COLUMN26,
        COLUMN27,
        COLUMN28,
        COLUMN29,
        COLUMN30,
        COLUMN31,
        COLUMN32,
        COLUMN33,
        COLUMN34,
        COLUMN35,
        COLUMN36,
        COLUMN37,
        COLUMN38,
        COLUMN39,
        COLUMN40,
        COLUMN41,
        COLUMN42,
        COLUMN43,
        COLUMN44,
        COLUMN45,
        COLUMN46,
        COLUMN47,
        COLUMN48,
        COLUMN49,
        COLUMN50,
    }
    #endregion
    //[AuthorLogin]
    public partial class FEEController : BaseController
    {
        //
        // GET: /SQM_FEE_CALC/
        public ActionResult FEEIndex()
        {
            //if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            //{
            //    Response.Redirect("/Account/Login");
            //}
            string sql = @"select TCET084,TEXTDESC from V_MDM_FEE";
            //string sql = @"select distinct FEECODE,FEENAME from SQM_SRV_FEE_CONFIG where FEECATG<>'2'";
            DataTable dt = DataHelper.QueryDataTable(sql);
            ViewBag.Data = dt;
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult FEELists()
        {
            string[] searchKeys = new string[] { "FEECODE" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request[key]))
                {
                    Type valueType = typeof(SQM_FEE_CALC).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request[key].Trim(), valueType), Aim.Data.SearchModeEnum.Equal);
                }
            }
            if (!string.IsNullOrEmpty(Request["CreateDateS"]))
            {
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CREATETIME", DateTime.Parse(Request["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var total = ActiveRecordMediator.Count(typeof(SQM_FEE_CALC), SearchCriterion.GetDetachedCriteriaWithoutOrder<SQM_FEE_CALC>());
            var obj = new { draw = Request["draw"], data = SQM_FEE_CALC.FindAll(SearchCriterion), recordsTotal = total, recordsFiltered = total };
            //多表关联时根据sql去检索数据
            //            string sql = @"select c.RID,c.FEENAME,c.FEECODE,c.CACLUNIT,c.MINPRICE,c.PRECOND,c.RSLBASE,c.ALLOWCACLOFFER,r.CALCCODE,r.CALCNAME,r.ISCNT,r.MEMO 
            //                from SQM_FEE_CALC c
            //                left join SQM_FEE_CALC_REF r on c.FEECODE=r.FEECODE and c.CACLUNIT=r.CACLUNIT";
            //            //因为oracle大小写敏感,新建的表字段最好都统一大写,包括实体类
            //            var obj = new { draw = Request["draw"], data = base.GetPageData(sql, SearchCriterion), recordsTotal = SearchCriterion.RecordCount, recordsFiltered = SearchCriterion.RecordCount };
            return Content(JsonHelper.GetJsonString(obj));
        }
        //
        // GET: /SQM_FEE_CALC/Create
        public ActionResult FEECreate()
        {
            try
            {
                string id = Request.QueryString["id"];
                string djfsrid = Request.QueryString["djfsrid"];
                string gdzkey = Request.QueryString["gdzkey"];
                string gdzrid = Request.QueryString["gdzrid"];
                DataTable djfsdt = null;
                DataTable fftypedt = null;
                DataTable djfsdatadt = null;
                string fsrslbase = "";
                string fsmin = "0";
                string fsdisp = "1";
                string fssort = "";
                string jsfflx = "";
                string jsff = "";
                string calcstr = "";
                string fssetcustomer = "0";
                //费目主数据
                string sql = @"select distinct TCET084,TEXTDESC from V_MDM_FEE";
                //string sql = @"select distinct FEECODE,FEENAME from SQM_SRV_FEE_CONFIG where FEECATG<>'2'";
                DataTable dt = DataHelper.QueryDataTable(sql);
                ViewBag.Data = dt;
                SQM_FEE_CALC ent = new SQM_FEE_CALC();
                if (!String.IsNullOrEmpty(id))
                {
                    sql = @"select distinct DJFSRID,DJFSNAME,FSSORT from SQM_FEE_PUR_REF where STATUS='1' and FEERID='" + id + "' and FEECODE is not null order by cast(FSSORT as int) asc, DJFSNAME asc";
                    djfsdt = DataHelper.QueryDataTable(sql);
                    if (String.IsNullOrEmpty(djfsrid) && djfsdt.Rows.Count > 0)
                    {
                        djfsrid = djfsdt.Rows[0]["DJFSRID"].ToString();
                    }
                    sql = @"select distinct GDZKEY,GDZRID,FSMIN,FSPRECOND,FSRSLBASE,JSFFLX,JSFF,JTLJ,FSDISP,FSSETCUSTOMER,FSSORT from SQM_FEE_PUR_REF where STATUS='1' and FEERID='" + id + "' and FEECODE is not null and DJFSRID='" + djfsrid + "'";
                    djfsdatadt = DataHelper.QueryDataTable(sql);
                    if (djfsdatadt.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(gdzrid))
                        {
                            gdzrid = djfsdatadt.Rows[0]["GDZRID"].ToString();
                        }
                        gdzkey = djfsdatadt.Rows[0]["GDZKEY"].ToString();
                        fsrslbase = djfsdatadt.Rows[0]["FSRSLBASE"].ToString();
                        //if (!String.IsNullOrEmpty(djfsdatadt.Rows[0]["FSMIN"].ToString()))
                        //{
                        fsmin = djfsdatadt.Rows[0]["FSMIN"].ToString();
                        fsdisp = djfsdatadt.Rows[0]["FSDISP"].ToString();
                        fssort = djfsdatadt.Rows[0]["FSSORT"].ToString();
                        //}
                        jsfflx = djfsdatadt.Rows[0]["JSFFLX"].ToString();
                        jsff = djfsdatadt.Rows[0]["JSFF"].ToString();
                        fssetcustomer = djfsdatadt.Rows[0]["FSSETCUSTOMER"].ToString();
                    }
                    else
                    {
                        gdzkey = "0";
                    }
                    ent = SQM_FEE_CALC.Find(id);
                    if (ent.JSFFZS == "1")
                    {
                        fftypedt = DataHelper.QueryDataTable("select distinct FFTYPE,FFTYPENAME from SQM_JSFF ORDER BY case when FFTYPE is null then 0 else 1 end asc, FFTYPE asc");
                    }
                }
                ViewBag.fftypedt = fftypedt;
                ViewBag.fsmin = fsmin;
                ViewBag.fsdisp = fsdisp;
                ViewBag.fssort = fssort;
                ViewBag.djfsdt = djfsdt;
                ViewBag.fsrslbase = fsrslbase;
                ViewBag.fsrslbase = fsrslbase;
                ViewBag.fsrslbase = fsrslbase;
                ViewBag.gdzkey = gdzkey;
                ViewBag.jsfflx = jsfflx;
                ViewBag.jsff = jsff;
                ViewBag.calcstr = DtToStr();
                ViewBag.fssetcustomer = fssetcustomer;
                return View("FEECreate", ent);
            }
            catch (System.Exception e)
            {
                string ee = e.Message;
                throw;
            }
        }
        /// <summary>
        /// 判断是否存在有效的定价、报价
        /// </summary>
        /// <param name="djfsrid"></param>
        /// <param name="gdzrid"></param>
        /// <returns></returns>
        public bool HasBjDj(string djfsrid, string gdzrid)
        {
            bool ifBjDj = false;
            string data = "";
            try
            {
                if (!String.IsNullOrEmpty(gdzrid))
                {
                    data = DataHelper.QueryValue("select RID from SQM_MODEDJ_VAL where STATUS='1' and GDZRID='" + gdzrid + "'") + "";
                    if (!String.IsNullOrEmpty(data))
                    {
                        ifBjDj = true;
                    }
                }
                else if (!String.IsNullOrEmpty(djfsrid))
                {
                    data = DataHelper.QueryValue("select RID from SQM_MODEDJ_VAL where STATUS='1' and DJFSRID='" + djfsrid + "'") + "";
                    if (!String.IsNullOrEmpty(data))
                    {
                        ifBjDj = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ifBjDj;
        }
        [AllowAnonymous]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SQM_FEE_CALC.TryFind(keyValue);
            return Content(JsonHelper.GetJsonString(data));
        }
        //
        // POST: /SQM_FEE_CALC/Create
        public ActionResult FEECreateData(SQM_FEE_CALC ent)
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            string code = "";
            try
            {
                string rid = Request["id"].ToString();
                string djfsrid = Request["DJFSRID"].ToString();
                string djfsname = Request["DJFSNAME"].ToString();
                string strdjfsrid = Request["STRDJFSRID"].ToString();
                string strdjfsname = Request["STRDJFSNAME"].ToString();
                string strgdzrid = Request["STRGDZRID"].ToString();
                string strgdzname = Request["STRGDZNAME"].ToString();
                string gdzkey = Request["GDZKEY"].ToString();
                string jsfflx = Request["JSFFLX"].ToString();
                string jsff = Request["JSFF"].ToString();
                string jtlj = Request["JTLJ"].ToString();
                string fsdisp = Request["FSDISP"].ToString();
                string fssort = Request["FSSORT"].ToString();
                string currentgdzrid = Request["CURRENTGDZRID"].ToString();
                string fsfysm = Request["FSFYSM"].ToString();
                string fsfysm_en = Request["FSFYSM_EN"].ToString();//英文费用说明
                string feeunit_en = Request["FEEUNIT_EN"].ToString();//英文费目单位
                string sight_fsfysm = Request["SIGHT_FSFYSM"].ToString();//场景费用说明
                string fssetcustomer = Request["FSSETCUSTOMER"].ToString() == "" ? "0" : Request["FSSETCUSTOMER"].ToString();//是否指定客户


                //if (string.IsNullOrEmpty(fssetcustomer))
                //{
                //    fssetcustomer = "0";
                //}
                if (string.IsNullOrEmpty(rid))
                {
                    //没有rid ,先插入主数据
                    var data = SQM_FEE_CALC.FindAllByProperties(SQM_FEE_CALC.Prop_FEECODE, ent.FEECODE);
                    if (data.Length > 0)
                    {
                        return Content(new JsonMessage { Success = false, Message = "该费目已存在，请确认！" }.ToString());
                    }
                    ent.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    ent.DoCreate();
                    data = SQM_FEE_CALC.FindAllByProperties(SQM_FEE_CALC.Prop_FEECODE, ent.FEECODE);
                    code = data[0].RID;
                    rid = code;
                }

                if (!String.IsNullOrEmpty(rid))
                {
                    //判断该DJFSRID下有没有有效的定价数据
                    if (HasBjDj(djfsrid, null))
                    {
                        string oldfsmin = DataHelper.QueryValue("select distinct FSMIN from SQM_FEE_PUR_REF where DJFSRID='" + djfsrid + "'") + "";
                        string fsminval = ent.MINPRICE;
                        if (oldfsmin == "0" && fsminval != oldfsmin)
                        {
                            return Content(new JsonMessage { Success = false, Message = "已存在有效的定价数据，最低收费请维护\"无\"！" }.ToString());
                        }
                        else if (oldfsmin == "1" && fsminval != oldfsmin)
                        {
                            return Content(new JsonMessage { Success = false, Message = "已存在有效的定价数据，最低收费请维护\"有\"！" }.ToString());
                        }
                    }
                    //code = "1";
                    code = rid;
                    SQM_FEE_CALC erd = SQM_FEE_CALC.Find(rid);
                    if (ent.MULBJFS == "0")
                    {
                        erd.MINPRICE = ent.MINPRICE;
                        erd.RSLBASE = ent.RSLBASE;
                    }
                    erd.CACLUNIT = ent.CACLUNIT;
                    erd.CACLCODE = ent.CACLCODE;
                    erd.PRECOND = ent.PRECOND;
                    erd.ALLOWCACLOFFER = ent.ALLOWCACLOFFER;
                    erd.MULBJFS = ent.MULBJFS;
                    erd.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                    erd.DoUpdate();
                    if (ent.MULBJFS == "0")
                    {
                        DataHelper.ExecSql("update SQM_FEE_PUR_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "'");
                        DataHelper.ExecSql("update SQM_FEE_CALC_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "' and DJFSRID is not null");
                        //DataHelper.ExecSql("update SQM_FEE_CALC_REF set STATUS='1' where FEECODE='" + ent.FEECODE + "' and DJFSRID is null");
                    }
                    else
                    {
                        //其他方式置为失效
                        DataHelper.ExecSql("update SQM_FEE_PUR_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "' and DJFSRID='" + djfsrid + "' and GDZKEY<>'" + gdzkey + "'");
                        DataHelper.ExecSql("update SQM_FEE_CALC_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "' and DJFSRID is null");
                        //保存维护的定价方式
                        SQM_FEE_PUR_REF sfpr = null;
                        SQM_FEE_PUR_REF newsfpr = new SQM_FEE_PUR_REF();
                        string[] DjfsRidArr = strdjfsrid.Split(',');
                        string[] DjfsNameArr = strdjfsname.Split(',');
                        for (int i = 0; i < DjfsRidArr.Length; i++)
                        {
                            if (String.IsNullOrEmpty(DjfsRidArr[i]))
                            {
                                continue;
                            }
                            sfpr = SQM_FEE_PUR_REF.FindFirstByProperties(SQM_FEE_PUR_REF.Prop_FEECODE, ent.FEECODE, SQM_FEE_PUR_REF.Prop_DJFSRID, DjfsRidArr[i]);
                            if (sfpr != null)
                            {
                                sfpr.DJFSNAME = DjfsNameArr[i];
                                sfpr.FSSETCUSTOMER = fssetcustomer;//指定客户标记
                                sfpr.FSFYSM_EN = Request["FSFYSM_EN"].ToString();//英文费用说明
                                sfpr.FEEUNIT_EN = Request["FEEUNIT_EN"].ToString();//英文费目单位
                                sfpr.SIGHT_FSFYSM = Request["SIGHT_FSFYSM"].ToString();//场景费用说明
                                sfpr.DoUpdate();
                            }
                            else
                            {
                                newsfpr.FEERID = rid;
                                newsfpr.FEECODE = ent.FEECODE;
                                newsfpr.DJFSRID = DjfsRidArr[i];
                                newsfpr.DJFSNAME = DjfsNameArr[i];
                                newsfpr.STATUS = "1";
                                newsfpr.FSSETCUSTOMER = fssetcustomer;//指定客户标记
                                newsfpr.FSFYSM_EN = Request["FSFYSM_EN"].ToString();//英文费用说明
                                newsfpr.FEEUNIT_EN = Request["FEEUNIT_EN"].ToString();//英文费目单位
                                newsfpr.SIGHT_FSFYSM = Request["SIGHT_FSFYSM"].ToString();//场景费用说明
                                newsfpr.DoCreate();
                            }
                        }
                        if (gdzkey == "0")
                        {
                            sfpr = SQM_FEE_PUR_REF.FindFirstByProperties(SQM_FEE_PUR_REF.Prop_FEECODE, ent.FEECODE, SQM_FEE_PUR_REF.Prop_DJFSRID, djfsrid);
                            if (sfpr.JTLJ != jtlj)
                            {
                                UpdateBjJtlj(jtlj, djfsrid, "");
                            }
                            sfpr.FSMIN = ent.MINPRICE;
                            sfpr.FSRSLBASE = ent.RSLBASE;
                            sfpr.JSFFLX = jsfflx;
                            sfpr.JSFF = jsff;
                            sfpr.JTLJ = jtlj;
                            sfpr.FSDISP = fsdisp;
                            sfpr.FSSORT = fssort;
                            sfpr.DJFSNAME = djfsname;
                            sfpr.GDZKEY = gdzkey;
                            sfpr.FEEUNIT = Request["FEEUNIT"];//费目单位
                            sfpr.FSJDLB = Request["FSJDLB"];
                            sfpr.FSFYSM = fsfysm;//费目说明
                            sfpr.FSFYSM_EN = fsfysm_en;
                            sfpr.SIGHT_FSFYSM = sight_fsfysm;//场景费用说明
                            sfpr.FEEUNIT_EN = feeunit_en;
                            //sfpr.FSSETCUSTOMER = fssetcustomer;//指定客户标记
                            sfpr.DoUpdate();
                        }
                        else
                        {
                            //其他方式置为失效
                            DataHelper.ExecSql("update SQM_FEE_PUR_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "' and DJFSRID='" + djfsrid + "' and GDZKEY<>'" + gdzkey + "'");
                            string[] GdzRidArr = strgdzrid.Split(',');
                            string[] GdzNameArr = strgdzname.Split(',');
                            for (int i = 0; i < GdzRidArr.Length; i++)
                            {
                                sfpr = SQM_FEE_PUR_REF.FindFirstByProperties(SQM_FEE_PUR_REF.Prop_FEECODE, ent.FEECODE, SQM_FEE_PUR_REF.Prop_DJFSRID, djfsrid, SQM_FEE_PUR_REF.Prop_GDZRID, GdzRidArr[i]);
                                if (sfpr != null)
                                {
                                    if (sfpr.JTLJ != jtlj)
                                    {
                                        UpdateBjJtlj(jtlj, djfsrid, GdzRidArr[i]);
                                    }
                                    sfpr.FSMIN = ent.MINPRICE;
                                    sfpr.DJFSNAME = djfsname;
                                    sfpr.FSDISP = fsdisp;
                                    sfpr.FSSETCUSTOMER = fssetcustomer;
                                    sfpr.FSSORT = fssort;
                                    sfpr.GDZNAME = GdzNameArr[i];
                                    //判断当前高低值rid和正在编辑的高低值rid是否相同，相同即更改计算方法
                                    if (!string.IsNullOrEmpty(currentgdzrid) && currentgdzrid == GdzRidArr[i])
                                    {
                                        sfpr.FSRSLBASE = ent.RSLBASE; //建议解析基础存到了高低值下
                                        sfpr.JSFFLX = jsfflx;
                                        sfpr.JSFF = jsff;
                                        sfpr.JTLJ = jtlj;
                                        sfpr.FEEUNIT = Request["FEEUNIT"];//费目单位
                                        sfpr.FSJDLB = Request["FSJDLB"];
                                        sfpr.FSFYSM = fsfysm;//费用说明
                                        // sfpr.FSSETCUSTOMER = fssetcustomer;//指定客户标记
                                        sfpr.FSFYSM_EN = fsfysm_en;//英文费用说明
                                        sfpr.SIGHT_FSFYSM = sight_fsfysm;//场景费用说明
                                        sfpr.FEEUNIT_EN = feeunit_en;//英文费目单位
                                    }
                                    sfpr.DoUpdate();
                                }
                                else
                                {
                                    DataHelper.ExecSql("update SQM_FEE_PUR_REF set STATUS='0' where FEECODE='" + ent.FEECODE + "' and DJFSRID='" + djfsrid + "' and GDZRID is null");
                                    newsfpr = new SQM_FEE_PUR_REF();
                                    newsfpr.FEERID = rid;
                                    newsfpr.FEECODE = ent.FEECODE;
                                    newsfpr.DJFSRID = djfsrid;
                                    newsfpr.DJFSNAME = djfsname;
                                    newsfpr.FSRSLBASE = ent.RSLBASE;
                                    newsfpr.JSFFLX = jsfflx;
                                    newsfpr.JSFF = jsff;
                                    newsfpr.FSDISP = fsdisp;
                                    newsfpr.FSSORT = fssort;
                                    newsfpr.FSMIN = ent.MINPRICE;
                                    newsfpr.GDZRID = GdzRidArr[i];
                                    newsfpr.GDZKEY = gdzkey;
                                    newsfpr.GDZNAME = GdzNameArr[i];
                                    newsfpr.STATUS = "1";
                                    newsfpr.JTLJ = jtlj;
                                    newsfpr.FEEUNIT = Request["FEEUNIT"];//费目单位
                                    newsfpr.FSJDLB = Request["FSJDLB"];
                                    newsfpr.FSFYSM = fsfysm;//费用说明
                                    newsfpr.FSSETCUSTOMER = fssetcustomer;//指定客户标记;
                                    newsfpr.FSFYSM_EN = fsfysm_en;//英文费用说明
                                    newsfpr.SIGHT_FSFYSM = sight_fsfysm;//场景费用说明
                                    newsfpr.FEEUNIT_EN = feeunit_en;//英文费目单位
                                    newsfpr.DoCreate();
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    var data = SQM_FEE_CALC.FindAllByProperties(SQM_FEE_CALC.Prop_FEECODE, ent.FEECODE);
                //    if (data.Length > 0)
                //    {
                //        return Content(new JsonMessage { Success = false, Message = "该费目已存在，请确认！" }.ToString());
                //    }
                //    ent.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                //    ent.DoCreate();
                //    data = SQM_FEE_CALC.FindAllByProperties(SQM_FEE_CALC.Prop_FEECODE, ent.FEECODE);
                //    code = data[0].RID;
                //}
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Code = code, Message = rtnmsg }.ToString());
        }
        //
        // GET: /SQM_FEE_CALC/Delete/5
        public ActionResult FEEDelete()
        {
            try
            {
                string id = Request.QueryString["id"];
                SQM_FEE_CALC ent = SQM_FEE_CALC.Find(id);
                SQM_FEE_CALC_REF[] sfcrs = SQM_FEE_CALC_REF.FindAllByProperties(SQM_FEE_CALC_REF.Prop_FEECODE, ent.FEECODE);
                foreach (SQM_FEE_CALC_REF sfcr in sfcrs)
                {
                    sfcr.Delete();
                }
                ent.Delete();
            }
            catch (Exception ex)
            {
                return Content("删除出现异常:" + ex.Message);
            }
            return Content("删除成功!");
        }
        public ActionResult FeeCalcRef()
        {
            bool rtnflag = true;
            string rtnmsg = "保存成功";
            try
            {
                SQM_FEE_CALC_REF sfcr = new SQM_FEE_CALC_REF();
                SQM_FEE_PUR_REF sfpr = new SQM_FEE_PUR_REF();
                string feecode = Request["FEECODE"].ToString();
                string caclunit = Request["CACLUNIT"].ToString();
                string feerid = Request["FEERID"].ToString();
                string djfsrid = Request["DJFSRID"].ToString();
                string djfsname = Request["DJFSNAME"].ToString();
                string gdzrid = Request["GDZRID"].ToString();
                string gdzkey = Request["GDZKEY"].ToString();
                string gdzname = Request["GDZNAME"].ToString();
                string mulbjfs = Request["MULBJFS"].ToString();
                string fsrslbase = Request["FSRSLBASE"].ToString();
                string fsprecond = Request["FSPRECOND"].ToString();
                string fsmin = Request["FSMIN"].ToString();
                string fsdisp = Request["FSDISP"].ToString();
                string fssort = Request["FSSORT"].ToString();
                string html = Request["html"].ToString();
                string[] strArr = html.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string refArrs in strArr)
                {
                    string[] refArr = refArrs.Split(',');
                    if (String.IsNullOrEmpty(refArr[0].Trim()))
                    {
                        //判断存在有效的定价数据
                        bool hasBjDj = HasBjDj(djfsrid, gdzrid);
                        if (hasBjDj)
                        {
                            return Content(new JsonMessage { Success = false, Message = "存在有效的定价数据，不能新增计算基础！" }.ToString());
                        }
                    }
                }
                //无高低值比较
                if (gdzkey == "0")
                {
                    //保存费目与定价方式关系
                    if (!String.IsNullOrEmpty(djfsrid))
                    {
                        var refdata = SQM_FEE_PUR_REF.FindAllByProperties(SQM_FEE_PUR_REF.Prop_FEERID, feerid, SQM_FEE_PUR_REF.Prop_DJFSRID, djfsrid);
                        if (refdata.Length > 0)
                        {
                            DataHelper.ExecSql("update SQM_FEE_PUR_REF set DJFSNAME='" + djfsname + "' where FEERID='" + feerid + "' and DJFSRID='" + djfsrid + "'");
                        }
                        else
                        {
                            sfpr.FEERID = feerid;
                            sfpr.FEECODE = feecode;
                            sfpr.GDZKEY = gdzkey;
                            sfpr.DJFSRID = djfsrid;
                            sfpr.DJFSNAME = djfsname;
                            sfpr.FSRSLBASE = fsrslbase;
                            sfpr.FSPRECOND = fsprecond;
                            sfpr.FSMIN = fsmin;
                            sfpr.FSDISP = fsdisp;
                            sfpr.FSSORT = fssort;
                            sfpr.STATUS = "1";
                            sfpr.DoCreate();
                        }
                    }
                }//有高低值比较
                else
                {
                    //保存费目与定价方式及高低值的关系
                    var refdata = SQM_FEE_PUR_REF.FindAllByProperties(SQM_FEE_PUR_REF.Prop_FEERID, feerid, SQM_FEE_PUR_REF.Prop_DJFSRID, djfsrid, SQM_FEE_PUR_REF.Prop_GDZRID, gdzrid);
                    if (refdata.Length > 0)
                    {
                        DataHelper.ExecSql("update SQM_FEE_PUR_REF set DJFSNAME='" + djfsname + "',GDZNAME='" + gdzname + "' where FEERID='" + feerid + "' and DJFSRID='" + djfsrid + "' and GDZRID='" + gdzrid + "'");
                    }
                    else
                    {
                        sfpr.FEERID = feerid;
                        sfpr.FEECODE = feecode;
                        sfpr.DJFSRID = djfsrid;
                        sfpr.DJFSNAME = djfsname;
                        sfpr.FSRSLBASE = fsrslbase;
                        sfpr.FSPRECOND = fsprecond;
                        sfpr.FSMIN = fsmin;
                        sfpr.FSDISP = fsdisp;
                        sfpr.FSSORT = fssort;
                        sfpr.GDZRID = gdzrid;
                        sfpr.GDZKEY = gdzkey;
                        sfpr.GDZNAME = gdzname;
                        sfpr.STATUS = "1";
                        sfpr.DoCreate();
                    }
                }
                //其他方式置为失效
                DataHelper.ExecSql("update SQM_FEE_PUR_REF set STATUS='0' where FEECODE='" + feecode + "' and DJFSRID='" + djfsrid + "' and GDZKEY<>'" + gdzkey + "'");
                DataHelper.ExecSql("update SQM_FEE_CALC_REF set STATUS='0' where FEECODE='" + feecode + "' and DJFSRID='" + djfsrid + "' and GDZKEY<>'" + gdzkey + "'");
                if (mulbjfs == "1")
                {
                    DataHelper.ExecSql("update SQM_FEE_CALC_REF set STATUS='0' where FEECODE='" + feecode + "' and DJFSRID is null");
                }
                foreach (string refArrs in strArr)
                {
                    string[] refArr = refArrs.Split(',');
                    //判断计算基础单位是否必填
                    string hasUnit = DataHelper.QueryValue("select HASUNIT from SQM_CALC_BASE where CALC_BASE='" + refArr[2].Trim() + "'") + "";
                    if (hasUnit == "1" && String.IsNullOrEmpty(refArr[3].Trim()))
                    {
                        return Content(new JsonMessage { Success = false, Message = "请维护计量单位！" }.ToString());
                    }
                    else if (String.IsNullOrEmpty(hasUnit) && !String.IsNullOrEmpty(refArr[3].Trim()))
                    {
                        return Content(new JsonMessage { Success = false, Message = "计量单位必须为空，请确认！" }.ToString());
                    }
                    if (!String.IsNullOrEmpty(refArr[0].Trim()))
                    {
                        SQM_FEE_CALC_REF fcr = SQM_FEE_CALC_REF.Find(refArr[0].Trim());
                        fcr.MSRCODE = DataHelper.QueryValue("select MSEHI from MDM_MSR_UNIT where DDTEXT is not null and DDTEXT='" + refArr[3].Trim() + "'") + "";
                        fcr.MSRUNIT = refArr[3].Trim();
                        fcr.ISCNT = refArr[4].Trim();
                        fcr.ISSEARCH = refArr[5].Trim();
                        fcr.MEMO = refArr[7].Trim();
                        fcr.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        fcr.DoUpdate();
                    }
                    else
                    {
                        DataTable valcoldt = null;
                        DataTable calccodedt = null;
                        string scale = "";
                        string valcolrk = "";
                        string wheredjfs = "";
                        string wheregdz = "";
                        if (mulbjfs == "0")
                        {
                            wheredjfs = " and DJFSRID is null";
                            wheregdz = " and GDZRID is null";
                        }
                        else
                        {
                            wheredjfs = " and DJFSRID='" + djfsrid + "'";
                            if (gdzkey == "0")
                            {
                                wheregdz = " and GDZRID is null";
                            }
                            else
                            {
                                wheregdz = " and GDZRID='" + gdzrid + "'";
                            }
                        }
                        string valcolsql = string.Format("select VALCOL from SQM_FEE_CALC_REF where STATUS='1' and FEECODE='{0}' {1} {2} order by VALCOL", feecode, wheredjfs, wheregdz);
                        string calccodesql = string.Format("select RID from SQM_FEE_CALC_REF where STATUS='1' and FEECODE='{0}' and CALCCODE='{1}' {2} {3}", feecode, refArr[2].Trim(), wheredjfs, wheregdz);
                        calccodedt = DataHelper.QueryDataTable(calccodesql);
                        if (calccodedt.Rows.Count > 0)
                        {
                            return Content(new JsonMessage { Success = false, Message = "计算基础重复，请确认！" }.ToString());
                        }
                        valcoldt = DataHelper.QueryDataTable(valcolsql);
                        if (valcoldt.Rows.Count > 0)
                        {
                            foreach (string valcol in Enum.GetNames(typeof(VALCOLNAME)))
                            {
                                bool conblag = false;
                                bool breblag = false;
                                foreach (DataRow valcoldr in valcoldt.Rows)
                                {
                                    if (valcol == valcoldr["VALCOL"].ToString())
                                    {
                                        conblag = true;
                                        break;
                                    }
                                    else
                                    {
                                        valcolrk = valcol;
                                        breblag = true;
                                    }
                                }
                                if (conblag)
                                {
                                    continue;
                                }
                                if (breblag)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            valcolrk = "COLUMN1";
                        }
                        sfcr.CALCNAME = DataHelper.QueryValue("select DESCRIPTION from MDM_CALC_BASE where not regexp_like(DESCRIPTION,'([a-z])') and CALC_BASE='" + refArr[2].Trim() + "'") + "";
                        sfcr.CALCCODE = refArr[2].Trim();
                        sfcr.MSRCODE = DataHelper.QueryValue("select MSEHI from MDM_MSR_UNIT where DDTEXT is not null and DDTEXT='" + refArr[3].Trim() + "'") + "";
                        sfcr.MSRUNIT = refArr[3].Trim();
                        sfcr.ISCNT = refArr[4].Trim();
                        sfcr.ISSEARCH = refArr[5].Trim();
                        sfcr.SORD = Convert.ToInt32(valcolrk.Replace("COLUMN", ""));
                        if (refArr[6].Trim() == "等于")
                        {
                            scale = "=";
                        }
                        else if (refArr[6].Trim() == "大于等于")
                        {
                            scale = ">=";
                        }
                        else if (refArr[6].Trim() == "小于等于")
                        {
                            scale = "<=";
                        }
                        sfcr.SCALE = scale;
                        sfcr.VALCOL = valcolrk;
                        sfcr.MEMO = refArr[7].Trim();
                        sfcr.FEECODE = feecode;
                        sfcr.CACLUNIT = caclunit;
                        sfcr.DJFSRID = djfsrid;
                        sfcr.GDZRID = gdzrid;
                        sfcr.GDZKEY = gdzkey;
                        sfcr.STATUS = "1";
                        sfcr.MODIFYUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                        sfcr.DoCreate();
                    }
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        public ActionResult getRefData()
        {
            try
            {
                string wherestr = "";
                string FEECODE = Request["FEECODE"].ToString();
                string DJFSRID = Request["DJFSRID"].ToString();
                string GDZRID = Request["GDZRID"].ToString();
                string GDZKEY = Request["GDZKEY"].ToString();
                string JsonString = string.Empty;
                if (!String.IsNullOrEmpty(DJFSRID))
                {
                    wherestr += " and DJFSRID='" + DJFSRID + "'";
                }
                else
                {
                    wherestr += " and DJFSRID is null";
                }
                if (GDZKEY == "0")
                {
                    wherestr += " and GDZRID is null";
                }
                else
                {
                    wherestr += " and GDZRID='" + GDZRID + "'";
                }
                string sql = @"select RID,FEECODE,CALCNAME,SORD,CALCCODE,ISCNT,ISSEARCH,MSRUNIT,SCALE,Memo from SQM_FEE_CALC_REF where STATUS='1' and FEECODE='" + FEECODE + "' {0} order by SORD asc";
                DataTable dt = DataHelper.QueryDataTable(string.Format(sql, wherestr));
                JsonString = JsonConvert.SerializeObject(dt);
                return Content(JsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //获取计算基础数据
        public ActionResult getCalcName()
        {
            try
            {
                string JsonString = string.Empty;
                string sql = @"select CALC_BASE,DESCRIPTION from MDM_CALC_BASE where not regexp_like(DESCRIPTION,'([a-z])')";
                DataTable dt = DataHelper.QueryDataTable(sql);
                JsonString = JsonConvert.SerializeObject(dt);
                return Content(JsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //获取标度计量单位数据
        public ActionResult getMsrUnit()
        {
            try
            {
                string JsonString = string.Empty;
                string sql = @"select MSEHI,DDTEXT from MDM_MSR_UNIT where DDTEXT is not null";
                DataTable dt = DataHelper.QueryDataTable(sql);
                JsonString = JsonConvert.SerializeObject(dt);
                return Content(JsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult delFeeCalcRef()
        {
            bool rtnflag = true;
            string rtnmsg = "删除成功";
            try
            {
                string rids = Request["RIDS"].ToString();
                string djfsrid = Request["djfsrid"].ToString();
                string gdzrid = Request["gdzrid"].ToString();
                //判断存在有效的定价数据
                bool hasBjDj = HasBjDj(djfsrid, gdzrid);
                if (hasBjDj)
                {
                    return Content(new JsonMessage { Success = false, Message = "存在有效的定价数据，不能删除计算基础！" }.ToString());
                }
                foreach (string rid in rids.Split(','))
                {
                    DataHelper.ExecSql("delete from SQM_FEE_CALC_REF where RID='" + rid + "'");
                }
            }
            catch (Exception ex)
            {
                rtnflag = false;
                rtnmsg = ex.Message;
            }
            return Content(new JsonMessage { Success = rtnflag, Message = rtnmsg }.ToString());
        }
        public ActionResult delDjfs()
        {
            bool rtnflag = true;
            string rtnmsg = "删除成功";
            try
            {
                string djfsrid = Request["djfsrid"].ToString();
                string gdzrid = Request["gdzrid"].ToString();
                //判断存在有效的定价数据
                bool hasBjDj = HasBjDj(djfsrid, gdzrid);
                if (hasBjDj)
                {
                    return Content(new JsonMessage { Success = false, Message = "存在有效的定价数据，不能删除该定价方式！" }.ToString());
                }
                if (!String.IsNullOrEmpty(gdzrid))
                {
                    DataHelper.ExecSql(string.Format("update SQM_FEE_PUR_REF set STATUS='0' where GDZRID='{0}'", gdzrid));
                    DataHelper.ExecSql(string.Format("update SQM_FEE_CALC_REF set STATUS='0' where GDZRID='{0}'", gdzrid));
                }
                else if (!String.IsNullOrEmpty(djfsrid))
                {
                    DataHelper.ExecSql(string.Format("update SQM_FEE_PUR_REF set STATUS='0' where DJFSRID='{0}'", djfsrid));
                    DataHelper.ExecSql(string.Format("update SQM_FEE_CALC_REF set STATUS='0' where DJFSRID='{0}'", djfsrid));
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
        /// 通过feerid,djfsrid,gdzkey获取高低值数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetGdzData(string feecode, string djfsrid, string gdzkey)
        {
            string sql = string.Format("SELECT  DISTINCT GDZRID, GDZNAME FROM SQM_FEE_PUR_REF WHERE STATUS='1' and FEECODE = '{0}' and DJFSRID='{1}' and GDZKEY='{2}' order by GDZNAME asc", feecode, djfsrid, gdzkey);
            var gdzArray = DataHelper.QueryObjectsList(sql);

            object[] data = { gdzArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        public ActionResult GetFSData(string djfsrid, string feecode)
        {
            //string sql = @"select distinct GDZKEY,FSMIN,FSPRECOND,FSRSLBASE,JSFFLX,JSFF,FSDISP,FSSORT from SQM_FEE_PUR_REF where STATUS='1' and FEECODE='" + feecode + "' and DJFSRID='" + djfsrid + "'";

            string sql = @"select distinct GDZKEY,FSMIN,FSPRECOND,FSRSLBASE,JSFFLX,JSFF,FSDISP,FSSORT,FSSETCUSTOMER,FSFYSM_EN,FEEUNIT_EN,SIGHT_FSFYSM  from SQM_FEE_PUR_REF where STATUS='1' and FEECODE='" + feecode + "' and DJFSRID='" + djfsrid + "'";
            var fsArray = DataHelper.QueryObjectsList(sql);

            object[] data = { fsArray };
            return Content(JsonHelper.GetJsonString(data));
        }
        /// <summary>
        /// 选择计算方法类型后带出计算方法
        /// </summary>
        /// <param name="jsfftype"></param>
        /// <returns></returns>
        public ActionResult getJsff(string jsfftype, string djfsrid, string feecode)
        {
            string where = "";
            string jsff = "";
            DataTable jsffdt = null;
            try
            {
                if (!String.IsNullOrEmpty(jsfftype))
                {
                    where = " where FFTYPE='" + jsfftype + "'";
                }
                else
                {
                    where = " where FFTYPE is null";
                }
                jsffdt = DataHelper.QueryDataTable(string.Format("select distinct FFCODE,FFNAME from SQM_JSFF {0} ORDER BY case when FFCODE is null then 0 else 1 end asc, FFCODE asc", where));
                jsff = DataHelper.QueryValue(string.Format("select distinct JSFF from SQM_FEE_PUR_REF where FEECODE='{0}' and DJFSRID='{1}'", feecode, djfsrid)) + "";
            }
            catch (Exception)
            {
                throw;
            }
            object[] data = { jsffdt, jsff };
            return Content(JsonHelper.GetJsonString(data));
        }

        /// <summary>
        /// 改变高低值时改变计算方法
        /// </summary>
        /// <returns></returns>
        public ActionResult JSFFByGdzRid(string djfsrid, string gdzrid, string gdzkey)
        {
            if (djfsrid != "")
            {
                string gdzstr = "";
                string sql = "select jsfflx,jsff, jtlj,FSRSLBASE,FEEUNIT,FSJDLB,FSFYSM from sqm_fee_pur_ref where djfsrid = '" + djfsrid + "' ";
                if (gdzkey == "0")
                {
                    gdzstr = " and gdzrid is null ";
                }
                else
                {
                    if (!string.IsNullOrEmpty(gdzrid) && gdzrid != "null")
                    {
                        gdzstr = " and gdzrid='" + gdzrid + "'";
                    }
                    else
                    {
                        gdzstr = " and gdzrid is null ";
                    }
                }

                //string sql = string.Format("select jsfflx,jsff, from sqm_fee_pur_ref where djfsrid = '{0}' and gdzrid ='{1}'",djfsrid,gdzrid);
                DataTable dt = DataHelper.QueryDataTable(sql + gdzstr);
                return Content(JsonHelper.GetJsonString(dt));
            }
            return null;
        }
        //报价提交审批之前更新阶梯累计 
        public void UpdateBjJtlj(string jtlj, string djfsrid, string gdzrid)
        {
            string gdzwhr = "";
            if (!String.IsNullOrEmpty(gdzrid))
            {
                gdzwhr = " and smv.GDZRID='" + gdzrid + "'";
            }
            else
            {
                gdzwhr = " and smv.GDZRID is null";
            }
            string updatesql = string.Format("update SQM_MODEBJ_VAL set JTLJ='{0}' where FEECALCID in(select distinct FEECALCID from SQM_MODEBJ_VAL smv left join SQM_BJ_PSF sbp on smv.FEECALCID=sbp.RID left join SQM_BJ_VER sbv on sbp.VRID=sbv.RID where sbv.MRID is not null and (sbv.STATUS='0' or sbv.STATUS is null) and smv.DJFSRID='{1}' {2})", jtlj, djfsrid, gdzwhr);
            DataHelper.ExecSql(updatesql);
        }
        /// <summary>
        /// Dt转String
        /// </summary>
        /// <returns></returns>
        public string DtToStr()
        {
            string Str = string.Empty;
            try
            {
                // 非数字型计算基础计费数量必须为否，数字型计算基础计费数量可为是也能为否
                DataTable dtdata = DataHelper.QueryDataTable("select distinct CALC_BASE from SQM_CALC_BASE where ISNUM='0'");
                foreach (DataRow dr in dtdata.Rows)
                {
                    Str += dr["CALC_BASE"].ToString() + ",";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Str;
        }
    }
}

