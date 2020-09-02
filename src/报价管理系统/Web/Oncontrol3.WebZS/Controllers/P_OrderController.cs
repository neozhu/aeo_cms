using Aim;
using Aim.Portal;
using Aim.Portal.Model;
using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class P_OrderController : Controller
    {
        protected string Interfact_User = System.Configuration.ConfigurationManager.AppSettings["Interfact_User"];
        protected string Interface_Pwd = System.Configuration.ConfigurationManager.AppSettings["Interface_Pwd"];
        protected int Interface_Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Interface_Timeout"] == null ? "0" : System.Configuration.ConfigurationManager.AppSettings["Interface_Timeout"]);
        //
        // GET: /Order/

        public ActionResult Index()
        {

            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);
            ViewBag.OrderType = user.Pk_zw;
            return View();
        }
        public ActionResult SearchOut()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interfact_User, Interface_Pwd);
            KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
            head.FUNCTION = "search";
            head.BUSINESS = user.Server_IAGUID;
            head.LOCATION = user.Server_Seed;
            head.ORDERTYPE = user.ThreeDESKEY;
            KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
            req.HEADER = head;

            KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
            item.inputuser = user.LoginName;
            item.ref01 = user.Ext2;//厂别;
            item.ref05 = Request.QueryString["ITEMCODE"];//料号
            item.refpo = Request.QueryString["SEQ"];//批次号
            item.isinput = Request.QueryString["IMPORTSTATE"];//是否导入
            item.ref06 = Request.QueryString["WORKNO"];//工单号
            item.asn = Request.QueryString["Kitting"];// KittingID
            item.status = Request.QueryString["RSTATE"];//是否删除
            item.ref03 = Request.QueryString["PDLINE"];//PDLINE
            item.ref18 = Request.QueryString["MSTATE"];//是否过账
            item.begindate = Request.QueryString["CreateDateS"];//开始时间
            item.enddate = Request.QueryString["CreateDateE"];//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM;

            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(JsonHelper.GetJsonString(retItems));
        }

        public ActionResult SearchIn()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interfact_User, Interface_Pwd);
            KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
            head.FUNCTION = "search";
            head.BUSINESS = user.Server_IAGUID;
            head.LOCATION = user.Server_Seed;
            head.ORDERTYPE = user.ThreeDESKEY;
            KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
            req.HEADER = head;

            KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
            item.inputuser = user.LoginName;
            item.ref01 = user.Ext2;//厂别;
            item.ref05 = Request.QueryString["ITEMCODE"];//料号
            item.refpo = Request.QueryString["SEQ"];//批次号
            item.isinput = Request.QueryString["IMPORTSTATE"];//是否导入
            item.ref06 = Request.QueryString["WORKNO"];//工单号
            item.asn = Request.QueryString["Kitting"];// KittingID
            item.status = Request.QueryString["RSTATE"];//是否删除
            item.ref03 = Request.QueryString["PDLINE"];//PDLINE
            item.ref18 = Request.QueryString["MSTATE"];//是否过账
            item.begindate = Request.QueryString["CreateDateS"];//开始时间
            item.enddate = Request.QueryString["CreateDateE"];//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM;

            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(JsonHelper.GetJsonString(retItems));
        }


        public ActionResult OrderTrace()
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"];
            if (id != "")
            {
                return View("OrderTrace", "_LayoutFrame");
            }
            else
                return View();
        }

        public ActionResult TraceDetail()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interfact_User, Interface_Pwd);
            KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
            head.FUNCTION = "search";
            head.BUSINESS = user.Server_IAGUID;
            head.LOCATION = user.Server_Seed;
            head.ORDERTYPE = user.ThreeDESKEY;
            KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
            req.HEADER = head;

            KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
            item.inputuser = user.LoginName;
            item.ref01 = user.Ext2;//厂别;
            item.ref05 = Request.QueryString["ITEMCODE"];//料号
            item.refpo = Request.QueryString["SEQ"];//批次号
            item.isinput = Request.QueryString["IMPORTSTATE"];//是否导入
            item.ref06 = Request.QueryString["WORKNO"];//工单号
            item.asn = Request.QueryString["Kitting"];// KittingID
            item.status = Request.QueryString["RSTATE"];//是否删除
            item.ref03 = Request.QueryString["PDLINE"];//PDLINE
            item.ref18 = Request.QueryString["MSTATE"];//是否过账
            item.begindate = Request.QueryString["CreateDateS"];//开始时间
            item.enddate = Request.QueryString["CreateDateE"];//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM;

            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(JsonHelper.GetJsonString(retItems));
        }


        public ActionResult StockTrace()
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"];
            if (id != "")
            {
                return View("OrderTrace", "_LayoutFrame");
            }
            else
                return View();
        }
        public ActionResult StockDetail()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interfact_User, Interface_Pwd);
            KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
            head.FUNCTION = "search";
            head.BUSINESS = user.Server_IAGUID;
            head.LOCATION = user.Server_Seed;
            head.ORDERTYPE = user.ThreeDESKEY;
            KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
            req.HEADER = head;

            KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
            item.inputuser = user.LoginName;
            item.ref01 = user.Ext2;//厂别;
            item.ref05 = Request.QueryString["ITEMCODE"];//料号
            item.refpo = Request.QueryString["SEQ"];//批次号
            item.isinput = Request.QueryString["IMPORTSTATE"];//是否导入
            item.ref06 = Request.QueryString["WORKNO"];//工单号
            item.asn = Request.QueryString["Kitting"];// KittingID
            item.status = Request.QueryString["RSTATE"];//是否删除
            item.ref03 = Request.QueryString["PDLINE"];//PDLINE
            item.ref18 = Request.QueryString["MSTATE"];//是否过账
            item.begindate = Request.QueryString["CreateDateS"];//开始时间
            item.enddate = Request.QueryString["CreateDateE"];//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM;

            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(JsonHelper.GetJsonString(retItems));
        }

        public ActionResult Export()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interfact_User, Interface_Pwd);
            KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
            head.FUNCTION = "search";
            head.BUSINESS = user.Server_IAGUID;
            head.LOCATION = user.Server_Seed;
            head.ORDERTYPE = user.ThreeDESKEY;
            KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
            req.HEADER = head;

            KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
            item.inputuser = user.LoginName;
            item.ref01 = user.Ext2;//厂别;
            item.ref05 = Request.QueryString["ITEMCODE"];//料号
            item.refpo = Request.QueryString["SEQ"];//批次号
            item.isinput = Request.QueryString["IMPORTSTATE"];//是否导入
            item.ref06 = Request.QueryString["WORKNO"];//工单号
            item.asn = Request.QueryString["Kitting"];// KittingID
            item.status = Request.QueryString["RSTATE"];//是否删除
            item.ref03 = Request.QueryString["PDLINE"];//PDLINE
            item.ref18 = Request.QueryString["MSTATE"];//是否过账
            item.begindate = Request.QueryString["CreateDateS"];//开始时间
            item.enddate = Request.QueryString["CreateDateE"];//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM;

            Workbook workbook = new Workbook(Server.MapPath("/filetemplates/KittingExport.xlsx"));
            Worksheet sheet = workbook.Worksheets[0];
            for (int i = 0; i < retItems.Length; i++)
            {
                KittingIO.DT_WIS_PORTAL1001_RESITEM kit = retItems[i];
                sheet.Cells.Rows[i + 1][0].PutValue(kit.REFPO);
                sheet.Cells.Rows[i + 2][0].PutValue(kit.INPUTDATE);
                sheet.Cells.Rows[i + 3][0].PutValue(kit.REF01);
                sheet.Cells.Rows[i + 4][0].PutValue(kit.REFPOLINE);
                sheet.Cells.Rows[i + 5][0].PutValue(kit.REF03);
                sheet.Cells.Rows[i + 6][0].PutValue(kit.REF02);
                sheet.Cells.Rows[i + 7][0].PutValue(kit.REF04);
                sheet.Cells.Rows[i + 8][0].PutValue(kit.REF07);
                sheet.Cells.Rows[i + 9][0].PutValue(kit.REF05);
                sheet.Cells.Rows[i + 10][0].PutValue(kit.REF08);
                sheet.Cells.Rows[i + 11][0].PutValue(kit.REF10);
                sheet.Cells.Rows[i + 12][0].PutValue(kit.REF11);
                sheet.Cells.Rows[i + 13][0].PutValue(kit.QTY1);
                sheet.Cells.Rows[i + 14][0].PutValue(kit.QTY3);
                sheet.Cells.Rows[i + 15][0].PutValue(kit.QTY5);
                sheet.Cells.Rows[i + 16][0].PutValue(kit.ACK_TIME);
                sheet.Cells.Rows[i + 17][0].PutValue(kit.REF18);
                sheet.Cells.Rows[i + 18][0].PutValue(kit.REF12);
                sheet.Cells.Rows[i + 19][0].PutValue(kit.REF13);
                sheet.Cells.Rows[i + 20][0].PutValue(kit.DATE1);
                sheet.Cells.Rows[i + 21][0].PutValue(kit.NOTES1);
                sheet.Cells.Rows[i + 22][0].PutValue(kit.REF06);
                sheet.Cells.Rows[i + 23][0].PutValue(kit.INPUTUSER);
                sheet.Cells.Rows[i + 24][0].PutValue(kit.ISINPUT == "Y" ? "是" : "否");
                sheet.Cells.Rows[i + 25][0].PutValue(kit.ASN);
                sheet.Cells.Rows[i + 26][0].PutValue(kit.ZSOHZT);
                sheet.Cells.Rows[i + 27][0].PutValue(kit.ZWMSDH);
                sheet.Cells.Rows[i + 28][0].PutValue(kit.ZCPH);
                sheet.Cells.Rows[i + 29][0].PutValue(kit.ZDDFYRQ);
            }
            string path = "/KittingUpload/" + user.LoginName + "_export_kitting_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            workbook.Save(Server.MapPath(path));
            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(path);
        }

    }
}
