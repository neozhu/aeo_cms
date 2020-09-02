using Aim;
using Aim.Data;
using Com.Feiliks.QDM;
using Com.Feiliks.QDM.Model;
using Oncontrol3.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using static Oncontrol3.Web.Controllers.QM_Price_NController;

namespace Oncontrol3.Web.Helpers
{
    public static class FLDSSO
    {
        private static string Encrypt(string str, string token)
        {
            string[] tmpArr = { str, token };            //Array.Sort(tmpArr);            
            string tmpStr = String.Join("", tmpArr);
            return (FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "MD5").ToUpper());
        }
        public static string SaveNEWSJBJ(CustomerModel csmodel,BusinessModel bsmodel,OrgModel orgmodel, ContrsctModel contrsctmodel, List<BJProduct> _productList)
        {

            var data = "";
            var mrid = System.Guid.NewGuid().ToString();
            var vrid = System.Guid.NewGuid().ToString();
            var random = System.Guid.NewGuid().ToString().Substring(0, 4);


            try
            {
                #region 新建主表信息
                //1 新建主表信息
                SQM_BJ_MAIN_BASIC mainobj = new SQM_BJ_MAIN_BASIC();
                mainobj.RID = mrid;
                mainobj.BJNAME = "报价" + DateTime.Now.ToShortDateString().Replace("/", "") + random;
                mainobj.CREATETIME = DateTime.Now;
                mainobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                mainobj.AFFILIATION = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                //首次进来不填值 rank 20191031 
                //mainobj.DTFROM = DateTime.Parse(dtfrom);
                //mainobj.DTTO = DateTime.Parse(dtto);
                //mainobj.MEMO = memo;
                data = mrid;
                mainobj.DoCreate();
                #endregion

                #region 填客户数据
                //2 新建客户表信息
                SQM_BJ_BP srcobj_bp1 = new SQM_BJ_BP();
                srcobj_bp1.BPCODE = csmodel.cuscode;
                srcobj_bp1.BPNAME = csmodel.cusname;
                srcobj_bp1.INNER = csmodel.Inner;
                srcobj_bp1.MRID = mrid;
                srcobj_bp1.DoCreate();
                #endregion

                #region 新建商机表信息
                //3 新建商机表信息
                /*
                 注：由之前一个客户对应的多条商机->切换成->一个客户的一条商机进行报价,所以商机不用轮循产出结果
                 */
                //string bizstr = "";
                //List<businessClass> bl = JsonHelper.GetObject<List<businessClass>>(busArray);
                //for (int j = 0; j < bl.Count; j++)
                //{
                //    bizstr += "'" + bl[j].buscode + "',";
                //    SQM_BJ_BIZ bizobj = new SQM_BJ_BIZ();
                //    bizobj.BIZNAME = bl[j].busname;
                //    bizobj.BIZID = bl[j].buscode;
                //    bizobj.MRID = mrid;
                //    bizobj.DoCreate();
                //}

                //3 第一次进入落地商机数据（由于销售易的报价是基于商机的，所以不用循环遍历）
                SQM_BJ_BIZ bizobj = new SQM_BJ_BIZ();
                bizobj.BIZNAME = bsmodel.busname;
                bizobj.BIZID = bsmodel.buscode;
                bizobj.MRID = mrid;
                bizobj.DoCreate();
                #endregion

                #region 新增组织表
                //4 新增组织表信息 存组织代码、组织名称
                SQM_BJ_ORG orgobj = new SQM_BJ_ORG();
                orgobj.MRID = mrid;
                orgobj.ORGCODE = orgmodel.orgcode;
                orgobj.ORGNAME = orgmodel.orgname;
                orgobj.DoCreate();
                #endregion

                #region 新增版本表信息
                // 5 新增版本表信息
                SQM_BJ_VER verobj = new SQM_BJ_VER();
                verobj.MRID = mrid;
                verobj.ZVER = "V1";
                verobj.RID = vrid;
                /*首次进来不录
                 * 1、9位码（来源TM里数据->同步数据所得）
                 * 2、起始日期、结束日期（由页面再次选择所得）
                 */
                //verobj.BPCODE9 = bpcode9;
                //verobj.DTFROM = DateTime.Parse(dtfrom);
                //verobj.DTTO = DateTime.Parse(dtto);
                verobj.CONTRSCTNUM = contrsctmodel.contrsctnum;//合同编码
                verobj.ORGRID = orgmodel.orgcode.Substring(0, 4);
                verobj.CREATEUSER = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATEID = Oncontrol3.Web.Helpers.SQMHelper.getStaffKey();
                verobj.CREATETIME = DateTime.Now;
                verobj.STATUS = "0";
                verobj.DoCreate();
                #endregion


                #region 根据商机带出产品并且插入psf表中 插入产品
                /*
                 * 6 根据商机带出产品并且插入psf表中
                 插入产品信息,根据商机编号，调取接口，拿到产品对应的数据
                 */
                for (var i = 0; i < _productList.Count; i++)
                {
                    string prdcode = _productList[i].PRODUCTSNAME.ToString();
                    //string prdname = _productList[i].PRODUCTSNAME.ToString();
                    //string EqualProduct = "";
                    //string EQUALDESCRIPTION = "";
                    //string REFSql = string.Format("SELECT EqualProduct,EQUALDESCRIPTION FROM SQM_CRM_PRODUCT_REF WHERE Product='{0}'", prdcode);
                    //DataTable dt = DataHelper.QueryDataTable(REFSql);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        EqualProduct = dr["EqualProduct"] + "";
                    //        EQUALDESCRIPTION = dr["EQUALDESCRIPTION"] + "";
                    //    }
                    //    //7 新建psf表
                    //    SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                    //    psfobj.MRID = mrid;
                    //    psfobj.VRID = vrid;
                    //    psfobj.STATUS = "1";
                    //    psfobj.BJSTATAUS = "0";
                    //    psfobj.CHOOSESTATUS = "0";
                    //    psfobj.ORGCODE = orgmodel.orgcode.Substring(0, 4);
                    //    psfobj.ORGNAME = orgmodel.orgname + "-" + orgmodel.orgcode.Substring(0, 4);
                    //    psfobj.PRODUCT_CODE = EqualProduct;
                    //    psfobj.PRODUCT_NAME = EQUALDESCRIPTION;
                    //    psfobj.BUSINESSORG = _productList[i].DIVISION;
                    //    psfobj.DoCreate();
                    //}

                    SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                    psfobj.MRID = mrid;
                    psfobj.VRID = vrid;
                    psfobj.STATUS = "1";
                    psfobj.BJSTATAUS = "0";
                    psfobj.CHOOSESTATUS = "0";
                    psfobj.ORGCODE = orgmodel.orgcode.Substring(0, 4);
                    psfobj.ORGNAME = orgmodel.orgname + "-" + orgmodel.orgcode.Substring(0, 4);
                    psfobj.PRODUCT_CODE = prdcode;
                    psfobj.PRODUCT_NAME = _productList[i].PRODUCTDESCRIPTION;
                    psfobj.BUSINESSORG = _productList[i].DIVISION;
                    psfobj.DoCreate();
                }


                ////6 根据商机带出产品并且插入psf表中
                ////crm 中取数据
                //IDbConnection conn = new OracleConnection();
                //conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                //if (conn.State != ConnectionState.Open)
                //{
                //    conn.Open();
                //}
                ////var sql = string.Format("SELECT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,CRM_BUS_PRODUCTINFO.PRODUCT_NAME,CRM_PRODUCT.PRODUCTSNAME,CRM_PRODUCT.PRODUCTDESCRIPTION FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE FOLLOWUPSTATUS = '跟进中' AND ID IN ({0}))", bizstr.TrimEnd(','));
                //var sql = string.Format(@"SELECT CRM_BUS_PRODUCTINFO.PRODUCT_CODE,
                //                            CRM_BUS_PRODUCTINFO.PRODUCT_NAME,
                //                            CRM_PRODUCT.PRODUCTSNAME,
                //                            CRM_PRODUCT.PRODUCTDESCRIPTION 
                //                            FROM CRM_BUS_PRODUCTINFO LEFT JOIN CRM_PRODUCT ON CRM_BUS_PRODUCTINFO.PRODUCT_ID = CRM_PRODUCT.ID 
                //                            WHERE BUSSINESS_ID IN(SELECT ID FROM CRM_BUSINESS WHERE ID IN ({0}))", bizstr.TrimEnd(','));
                //var prddt = DataHelper.QueryDataTable(sql, conn);
                //if (prddt.Rows.Count > 0)
                //{
                //    for (var k = 0; k < prddt.Rows.Count; k++)
                //    {
                //        string prdcode = prddt.Rows[k]["PRODUCTDESCRIPTION"].ToString();
                //        string prdname = prddt.Rows[k]["PRODUCTSNAME"].ToString();
                //        string EqualProduct = "";
                //        string EQUALDESCRIPTION = "";
                //        string REFSql = string.Format("SELECT EqualProduct,EQUALDESCRIPTION FROM SQM_CRM_PRODUCT_REF WHERE Product='{0}'", prdcode);
                //        DataTable dt = DataHelper.QueryDataTable(REFSql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            foreach (DataRow dr in dt.Rows)
                //            {
                //                EqualProduct = dr["EqualProduct"] + "";
                //                EQUALDESCRIPTION = dr["EQUALDESCRIPTION"] + "";
                //            }
                //            //7 新建psf表
                //            SQM_BJ_PSF psfobj = new SQM_BJ_PSF();
                //            psfobj.MRID = mrid;
                //            psfobj.VRID = vrid;
                //            psfobj.ORGCODE = orgcode.Substring(0, 4);
                //            psfobj.ORGNAME = orgname + "-" + orgcode.Substring(0, 4);
                //            //psfobj.PRODUCT_CODE = prdcode;
                //            //psfobj.PRODUCT_NAME = prdname;
                //            psfobj.PRODUCT_CODE = EqualProduct;
                //            psfobj.PRODUCT_NAME = EQUALDESCRIPTION;
                //            psfobj.DoCreate();
                //        }
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                data = "";
            }
            return data;
        }


        /// <summary>
        /// string RID = Request["RID"] + "";
        /// </summary>
        /// <param name="RID">单点登录RID</param>
        public static void Valid(string RID)
        {
            try
            {
                if (!string.IsNullOrEmpty(RID))
                {
                    string[] tmpArr = { RID };
                    Array.Sort(tmpArr);
                    string tmpStr = String.Join("", tmpArr);
                    string hash = Encrypt(tmpStr, System.Configuration.ConfigurationManager.AppSettings["SSOTOKEN"]);
                    SSOService login = new SSOService();
                    login.Url = System.Configuration.ConfigurationManager.AppSettings["SSOURL"];
                    SSO_LOGIN user = login.GetLoginInfo(hash);
                    if (null != user)
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
            }
            catch (Exception)
            {
            }

        }
    }
    #region Model
    /*
     * string contrsctnum, 
     * *
    */
    public class businessClass
    {
        //商机编码
        public string buscode { get; set; }
        //商机名称
        public string busname { get; set; }
    }

    /// <summary>
    /// 客户Model
    /// </summary>
    public class CustomerModel
    {
        //客户编码
        public string cuscode { get; set; }
        //客户名称
        public string cusname { get; set; }
        //客户类型
        public string Inner { get; set; }
    }
    /// <summary>
    /// 商机Model
    /// </summary>
    public class BusinessModel
    {
        //商机编码
        public string buscode { get; set; }
        //商机名称
        public string busname { get; set; }
    }
    /// <summary>
    /// 组织Model
    /// </summary>
    public class OrgModel
    {
        //组织编码
        public string orgcode { get; set; }
        //组织名称
        public string orgname { get; set; }
    }
    /// <summary>
    /// 合同Model
    /// </summary>
    public class ContrsctModel
    {
        //合同编码
        public string contrsctnum { get; set; }
        //合同名称
        public string contrsctname { get; set; }
    }
    /// <summary>
    /// 合同Model
    /// </summary>
    public class ProductModel
    {
        public string PRODUCTSCODE { get; set; }//产品代码目前没有用到
        public string PRODUCTSNAME { get; set; }//产品名称
        //string CREATETIME { get; set; }
        public string DIVISION { get; set; }//类型
        public string PRODUCTDESCRIPTION { get; set; }//产品编码
    }
    #region  报价Model
    /// <summary>
    /// 产品Model
    /// </summary>
    public class BJProduct
    {
        public string PRODUCTSCODE { get; set; }//产品代码目前没有用到
        public string PRODUCTSNAME { get; set; }//产品名称
        //string CREATETIME { get; set; }
        public string DIVISION { get; set; }//类型
        public string PRODUCTDESCRIPTION { get; set; }//产品编码
    };
    #endregion
    #endregion
}