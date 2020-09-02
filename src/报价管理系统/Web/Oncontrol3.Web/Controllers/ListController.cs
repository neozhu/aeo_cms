using Aim;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Com.Feiliks.QDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oncontrol3.Web.GetDataFromXSYByCustomerNo;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Oncontrol3.Web.Controllers
{
    public class FLD_QO_USER
    {
        public string staffkey = "";
        public Hashtable htExt = new Hashtable();

        public FLD_QO_USER() { }
    }

    public class ListController : Controller
    {
        public ActionResult Index()
        {
            if (PortalService.CurrentUserInfo != null)
            {
                string userName = PortalService.CurrentUserInfo.Name;
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            string staffkey = SessionHelper.GetSessionUser<FLD_QO_USER>().staffkey;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 报价对接销售易
        /// </summary>
        /// <returns></returns>
        public ActionResult SSOXSY()
        {
            string UserId = Request.QueryString["UserId"];//用户ID
            string Token = Request.QueryString["Token"];//Request.QueryString["access_token"];//Token
            string CustomerNo = Request.QueryString["CustomerNo"];//客户编号YPYYP01
            string BussinessNo = Request.QueryString["BussinessNo"];//商机编码IMP-20151019
            string DateSpan = Request.QueryString["DateSpan"];// DateTime.Now.ToString("yyyyMMddhhmmssfff");//20191024112209547

            string HbString = UserId + CustomerNo + BussinessNo + DateSpan;
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(HbString, "MD5").ToUpper();//6F6B3B618EFBDC82248049E2EE70746B

            string url = System.Configuration.ConfigurationManager.AppSettings["XSYUSERINFOURL"];
            bool isSuccess = VilToke(url, Token);
            if (isSuccess)
            {

                string Rid = Guid.NewGuid().ToString();
                BJ_SSO_LOGIN bl = new BJ_SSO_LOGIN();
                bl.RID = Rid;
                bl.STAFFKEY = UserId;
                //bl.CUSTOMERNO = CustomerNo;
                bl.SYSTEMKEY = "销售易接入";
                bl.CREATEUSER = UserId;
                bl.CREATETIME = DateTime.Now;
                bl.DateSpan = DateSpan;
                bl.HASH = Token;//超过长度
                bl.DoCreate();

                FLD_QO_USER qousernew = new FLD_QO_USER();
                FLD_QO_USER qouserold = SessionHelper.GetSessionUser<FLD_QO_USER>();
                //if (string.IsNullOrEmpty(qouser.staffkey))
                {
                    {
                        qousernew.staffkey = UserId;
                        if (qouserold.staffkey != qousernew.staffkey
                            && !string.IsNullOrEmpty(qousernew.staffkey))
                        {
                            SessionHelper.AddSessionUser<FLD_QO_USER>(qousernew);
                            CookieHelper.SetCookie(qousernew.staffkey);
                        }
                    }
                }

                //调取接口直接落地报价主信息、版本信息、产品信息
                //正式的时候通过接口 取数据落地


                GetDataFromXSYByCustomerNo.CreateGetCRM data = new Web.GetDataFromXSYByCustomerNo.CreateGetCRM();
                GetDataFromXSYByCustomerNo.phCreateGetCRM head = new GetDataFromXSYByCustomerNo.phCreateGetCRM();
                GetDataFromXSYByCustomerNo.pbCreateGetCRM body = new GetDataFromXSYByCustomerNo.pbCreateGetCRM();

                head.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                head.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];//"ab8b5021362521933a44c053833becb3";

                GetDataFromXSYByCustomerNo.pbCreateGetCRM[] bodyList = new GetDataFromXSYByCustomerNo.pbCreateGetCRM[1];
                body.customerno = CustomerNo;
                body.businessno = BussinessNo;
                bodyList[0] = body;
                msgResponse content = data.CallCreateGetCRM(head, bodyList);
                msgResponse[] list = content.list;
                JsonParser jo = JsonConvert.DeserializeObject<JsonParser>(list[0].originMessage);

                List<Customer> CustomerModel = jo.CustomerModel;
                List<Org> Org = jo.OrgModel;
                List<Business> Business = jo.BusinessModel;
                List<Contrsct> Contrsc = jo.ContrsctModel;
                List<Product> Product = jo.ProductModel;

                #region 落地主数据 
                //客户-理论上一条商机只有一个客户
                CustomerModel cusmodel = new CustomerModel();
                if (CustomerModel != null && CustomerModel.Count > 0)
                {
                    cusmodel.cuscode = CustomerModel[0].customItem219__c;
                    cusmodel.cusname = CustomerModel[0].accountName;
                }
                else
                {
                    return Content("找不到客户,请检查客户信息");
                }
                //商机
                BusinessModel busmodel = new BusinessModel();
                if (Business != null && Business.Count > 0)
                {
                    busmodel.buscode = Business[0].customItem195__c;
                    busmodel.busname = Business[0].opportunityName;
                }
                else
                {
                    return Content("找不到商机,请检查商机信息");
                }
                //组织

                OrgModel orgmodel = new OrgModel();
                if (Org != null && Org.Count > 0)
                {
                    orgmodel.orgcode = Org[0].customItem2__c;
                    orgmodel.orgname = Org[0].name;
                }
                else
                {
                    return Content("找不到组织,请检查组织信息");
                }

                //合同

                ContrsctModel contrsctmodel = new ContrsctModel();
                contrsctmodel.contrsctnum = Contrsc != null && Contrsc.Count > 0 ? Contrsc[0].customItem153__c : "";
                contrsctmodel.contrsctname = Contrsc != null && Contrsc.Count > 0 ? Contrsc[0].customItem155__c : "";
                //产品列表
                //BJProduct[] cpList = new BJProduct[Product.Count];
                List<BJProduct> cpList = new List<BJProduct>();
                foreach (var itm in Product)
                {
                    string prdcode = itm.customItem127__c;
                    string REFSql = string.Format("SELECT EqualProduct,EQUALDESCRIPTION FROM SQM_CRM_PRODUCT_REF WHERE Product='{0}'", prdcode);
                    DataTable dt = DataHelper.QueryDataTable(REFSql);
                    if (dt.Rows.Count > 0)
                    {
                        BJProduct cp = new BJProduct();
                        cp.PRODUCTSCODE = "";
                        cp.PRODUCTSNAME = itm.productName;
                        cp.DIVISION = itm.customItem128__c;
                        cp.PRODUCTDESCRIPTION = itm.customItem127__c;
                        cpList.Add(cp);
                    }

                }

                if (cpList.Count <= 0)
                {
                    return Content("没有匹配到相关产品,请检查客户产品是否在报价系统已配置");
                }
                string bjrid = FLDSSO.SaveNEWSJBJ(cusmodel, busmodel, orgmodel, contrsctmodel, cpList);
                #endregion
                return Redirect("/QM_Price_N/QM_PriceEdit?keyValue=" + bjrid + "&SystemFrom=XSY&zversion=V1");
                //f58eac40d8a43d8082cffb304ceb50b97e4dd010a42a9989322e4ffa812d8200.ODUxODU2ODk2NzA1MjQy
                //http://localhost:2066/List/SSOXSY?UserId=20000024&CustomerNo=YPYYP01&BussinessNo=IMP-20151019&DateSpan=20191024112209547&Token=6F6B3B618EFBDC82248049E2EE70746B
            }
            else
            {
                return Content("验证不通过,请联系管理员");
            }
        }

        public ActionResult TestBJSSO()
        {
            return View();
        }


        public ActionResult SSO()
        {
            string U = Request.QueryString["u"];
            string TS = Request.QueryString["ts"];
            string N = Request.QueryString["n"];
            string S = Request.QueryString["s"];
            string returnUrl = Request.QueryString["sqmurl"] + "";
            //string username = Request.QueryString["username"];

            int timespan = 600;

            SSOHelper sso = new SSOHelper();

            string Token = sso.GetToken("_TM");
            FLD_QO_USER qousernew = new FLD_QO_USER();
            FLD_QO_USER qouserold = SessionHelper.GetSessionUser<FLD_QO_USER>();
            //if (string.IsNullOrEmpty(qouser.staffkey))
            {
                //if (sso.VilidateUrl(U, TS, N, S, Token, timespan))
                {
                    qousernew.staffkey = U;
                    //string sql = string.Format("select t.*, t.rowid from SSO_USER_ATTR t where SYSTEMATTRKEY='QO' and STATUS='1' and STAFFKEY = '{0}' ", U);
                    //DataTable dt = DataHelper.QueryDataTable(sql);
                    //if (dt != null)
                    //{
                    //    foreach (DataRow item in dt.Rows)
                    //    {
                    //        qouser.htExt.Add(item["ATTRKEY"].ToString(), item["ATTRVAL"].ToString());
                    //    }
                    //}
                    //qouser.htExt.Add("username", username);
                    if (qouserold.staffkey != qousernew.staffkey
                        && !string.IsNullOrEmpty(qousernew.staffkey))
                    {
                        SessionHelper.AddSessionUser<FLD_QO_USER>(qousernew);
                        CookieHelper.SetCookie(qousernew.staffkey);
                    }
                }
                //else
                //{
                //    SessionHelper.AddSessionUser<FLD_QO_USER>(qouser);
                //}

                //sso.VilidateUrl("012010080", "20170908165700", "3212", "3EF038D41A394B5E0ECB467969142D9F", Token, timespan);
            }

            FLDSSO.Valid(Request["RID"] + "");
            return View();
        }

        public ActionResult SSOSQM()
        {
            string U = Request.QueryString["u"];
            string TS = Request.QueryString["ts"];
            string N = Request.QueryString["n"];
            string S = Request.QueryString["s"];
            string returnUrl = Request.QueryString["sqmurl"] + "";
            //string username = Request.QueryString["username"];

            int timespan = 600;

            SSOHelper sso = new SSOHelper();

            string Token = sso.GetToken("_TM");
            FLD_QO_USER qousernew = new FLD_QO_USER();
            FLD_QO_USER qouserold = SessionHelper.GetSessionUser<FLD_QO_USER>();
            //if (string.IsNullOrEmpty(qouser.staffkey))
            {
                //if (sso.VilidateUrl(U, TS, N, S, Token, timespan))
                {
                    qousernew.staffkey = U;
                    //string sql = string.Format("select t.*, t.rowid from SSO_USER_ATTR t where SYSTEMATTRKEY='QO' and STATUS='1' and STAFFKEY = '{0}' ", U);
                    //DataTable dt = DataHelper.QueryDataTable(sql);
                    //if (dt != null)
                    //{
                    //    foreach (DataRow item in dt.Rows)
                    //    {
                    //        qouser.htExt.Add(item["ATTRKEY"].ToString(), item["ATTRVAL"].ToString());
                    //    }
                    //}
                    //qouser.htExt.Add("username", username);
                    if (qouserold.staffkey != qousernew.staffkey
                        && !string.IsNullOrEmpty(qousernew.staffkey))
                    {
                        SessionHelper.AddSessionUser<FLD_QO_USER>(qousernew);
                        CookieHelper.SetCookie(qousernew.staffkey);
                    }
                }
                //else
                //{
                //    SessionHelper.AddSessionUser<FLD_QO_USER>(qouser);
                //}

                //sso.VilidateUrl("012010080", "20170908165700", "3212", "3EF038D41A394B5E0ECB467969142D9F", Token, timespan);
            }

            if (returnUrl.Contains("/QM_Price/QM_PriceIndex"))
            {
                return RedirectToAction("Index", "List");
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                string strLeft = "";
                string strRight = "";
                string contorlName = "";
                string funcName = "";
                if (returnUrl.Contains("?"))
                {
                    strLeft = returnUrl.Substring(1, returnUrl.IndexOf("?") - 1);
                    strRight = returnUrl.Substring(returnUrl.IndexOf("?") + 1);
                    contorlName = strLeft.Substring(0, strLeft.IndexOf("/"));
                    funcName = strLeft.Substring(strLeft.IndexOf("/") + 1);
                    //string para = strRight.Substring(0, strRight.IndexOf("="));
                    return RedirectToAction(funcName, contorlName, new { REPORTKEY = strRight.Substring(strRight.IndexOf("=") + 1) });
                }
                else
                {
                    strLeft = returnUrl.Substring(1);
                    contorlName = strLeft.Substring(0, strLeft.IndexOf("/"));
                    funcName = strLeft.Substring(strLeft.IndexOf("/") + 1);
                    return RedirectToAction(funcName, contorlName);
                }
            }

            return RedirectToAction("Login", "Account");
        }

        //校验是否是否销售易的Token
        public bool VilToke(string url, string token)
        {
            bool isCG = false;
            //string url = "https://api-tencent.xiaoshouyi.com/data/v1/objects/user/info";
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //string data = "{\n\"token\": \"" + token + "\n}";

            //byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            //request.ContentLength = byteData.Length;
            //using (Stream postStream = request.GetRequestStream())
            //{
            //    postStream.Write(byteData, 0, byteData.Length);
            //}
            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    Console.WriteLine(reader.ReadToEnd());
            //}

            //token = "1595d54bb3a55c84b4818b4651c362f13bdc5a4c6269e96a967e343418901d5b.ODUxODU2ODk2NzA1MjQy";
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 800000;//设置请求超时时间，单位为毫秒
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", "Bearer " + token);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            if (result.Contains("id"))
            {
                isCG = true;
            }
            return isCG;
        }
    }
    public class JsonParser
    {
        public List<Customer> CustomerModel;
        public List<Business> BusinessModel;
        public List<Org> OrgModel;
        public List<Contrsct> ContrsctModel;
        public List<Product> ProductModel;
    }
    /// <summary>
    /// 客户Model
    /// </summary>
    public class Customer
    {
        //客户编码
        public string customItem219__c { get; set; }
        //客户名称
        public string accountName { get; set; }
    }
    /// <summary>
    /// 商机Model
    /// </summary>
    public class Business
    {
        //商机编码
        public string customItem195__c { get; set; }
        //商机名称
        public string opportunityName { get; set; }
    }
    /// <summary>
    /// 组织Model
    /// </summary>
    public class Org
    {
        //组织编码
        public string customItem2__c { get; set; }
        //组织名称
        public string name { get; set; }
    }
    /// <summary>
    /// 合同Model
    /// </summary>
    public class Contrsct
    {
        //合同编码
        public string customItem153__c { get; set; }
        //合同名称
        public string customItem155__c { get; set; }
    }
    /// <summary>
    /// 合同Model
    /// </summary>
    public class Product
    {
        public string customItem127__c { get; set; } //编号
        public string customItem128__c { get; set; } //类型
        public string productName { get; set; } //名称
    }
}
