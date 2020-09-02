using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.ActiveRecord;
using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Security;
using NHibernate.Criterion;
using System.Web.Mvc;

using OnControl.Model;
using Oncontrol3.Web;
using Aspose.Cells;
using System.Data;
using Aim.Portal;
using System.Net;

namespace OnControl.Web
{
    [AuthorLogin]
    public partial class P_KITTINGController : BaseController
    {
        log4net.ILog Logs = log4net.LogManager.GetLogger("Info");
        protected string Interface_User = System.Configuration.ConfigurationManager.AppSettings["Interface_User"];
        protected string Interface_Pwd = System.Configuration.ConfigurationManager.AppSettings["Interface_Pwd"];
        protected int Interface_Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Interface_Timeout"] == null ? "0" : System.Configuration.ConfigurationManager.AppSettings["Interface_Timeout"]);
        //
        // GET: /P_KITTING/
        public ActionResult Index()
        {
            ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//状态枚举,下拉框用
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));//列表显示用
            return View();
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists()
        {
            string[] searchKeys = new string[] { "Code", "Name" };
            foreach (string key in searchKeys)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[key]))
                {
                    Type valueType = typeof(P_KITTING).GetProperty(key).PropertyType;
                    if (valueType.FullName.ToLower().IndexOf("system.int") >= 0)
                    {
                        SearchCriterion.AddSearch(key, int.Parse(Request.QueryString[key].Trim()), Aim.Data.SearchModeEnum.Equal);
                    }
                    else
                        SearchCriterion.AddSearch(key, Convert.ChangeType(Request.QueryString[key].Trim(), valueType), Aim.Data.SearchModeEnum.Like);
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CreateDateS"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateS"]), Aim.Data.SearchModeEnum.GreaterThanEqual);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CreateDateE"]))
            {
                SearchCriterion.AddSearch("CreateDate", DateTime.Parse(Request.QueryString["CreateDateE"]), Aim.Data.SearchModeEnum.LessThanEqual);
            }
            var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            return Content(JsonHelper.GetJsonString(obj));
        }

        //根据接口查询数据
        public ActionResult SearchLists()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interface_User, Interface_Pwd);
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
            item.begindate = Request.QueryString["CreateDateS"].Replace("-", "").Replace("/", "");//开始时间
            item.enddate = Request.QueryString["CreateDateE"].Replace("-", "").Replace("/", ""); ;//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM == null ? new KittingIO.DT_WIS_PORTAL1001_RESITEM[0] : res.ITEM;
            var list = new List<KittingIO.DT_WIS_PORTAL1001_RESITEM>();
            foreach (KittingIO.DT_WIS_PORTAL1001_RESITEM itemt in retItems)
            {
                Logs.Info("ITEM:" + itemt.ZLEVEL);
                if (itemt.ZLEVEL.Trim() == "2")
                    list.Add(itemt);
            }
            //list = (from m in list where m.ZLEVEL.Trim() == "2" orderby m.REFPO descending select m).ToList();
            Logs.Info("ITEM:" + list.Count);
            //var datas = from m in retItems where m.ZLEVEL.Trim() == "2" orderby m.REFPO descending select m;
            var obj = new { rows = list, total = list.Count() };
            return Content(JsonHelper.GetJsonString(obj));
            //return Content(JsonHelper.GetJsonString());
        }

        //
        // GET: /P_KITTING/Create

        public ActionResult Create()
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            return View();
        }

        //
        // POST: /P_KITTING/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(P_KITTING ent)//多对象form时使用(FormCollection collection)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            try
            {
                ent.DoCreate();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "名称已被占用,请返回修改。");
                return View(ent);
            }
        }

        //
        // GET: /P_KITTING/Edit/5

        public ActionResult Edit(string id)
        {
            ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            P_KITTING ent = P_KITTING.Find(id);
            return View("Create", ent);
        }

        //
        // POST: /P_KITTING/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, P_KITTING data)//多对象form就用FormCollection formdatas获取数据
        {
            try
            {

                P_KITTING ent = this.GetMergedData<P_KITTING>(data);
                ent.DoUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /P_KITTING/Delete/5

        public ActionResult Reset()
        {
            try
            {
                string pos = Request.QueryString["po"];
                pos = pos.TrimEnd(',');
                string poline = Request.QueryString["poline"];
                poline = poline.TrimEnd(',');
                SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);
                KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
                service.Timeout = Interface_Timeout;
                service.Credentials = new NetworkCredential(Interface_User, Interface_Pwd);
                KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
                head.FUNCTION = "delete";
                head.BUSINESS = user.Server_IAGUID;
                head.LOCATION = user.Server_Seed;
                head.ORDERTYPE = user.ThreeDESKEY;
                for (int i = 0; i < pos.Split(',').Length; i++)
                {
                    KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
                    req.HEADER = head;
                    List<KittingIO.DT_WIS_PORTAL1001_REQITEM> items = new System.Collections.Generic.List<KittingIO.DT_WIS_PORTAL1001_REQITEM>();

                    KittingIO.DT_WIS_PORTAL1001_REQITEM ritem = new KittingIO.DT_WIS_PORTAL1001_REQITEM();
                    ritem.REFPO = pos.Split(',')[i];
                    ritem.REFPOLINE = poline.Split(',')[i];
                    ritem.ZLEVEL = "2";
                    ritem.INPUTUSER = user.LoginName; ;
                    items.Add(ritem);
                    req.ITEM = items.ToArray();
                    KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
                    item.begindate = "";
                    req.ITEM2 = item;
                    KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
                    KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
                    //ret.STATUS //SUCCESS/FAIL
                    if (ret.STATUS.ToUpper() == "FAIL")
                    {
                        throw new System.Exception(ret.MESSAGE);
                    }
                    else
                    {
                        DataHelper.ExecSql("update p_kitting set SYNCSTATE = 'delete' where seq='" + pos.Split(',')[i] + "' and itemseq='" + poline.Split(',')[i] + "' and CREATENAME='" + PortalService.CurrentUserInfo.Name + "'");
                    }
                }

            }
            catch (Exception ex)
            {
                return Content("作废出现异常:" + ex.Message);
            }
            return Content("作废成功!");
        }
        public ActionResult FileUpload()
        {
            return View("FileUpload", "_LayoutFrame");
        }

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public ActionResult PostExcelData()
        {
            string info = string.Empty;
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    //获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    System.IO.Stream stream = file.InputStream;
                    DataTable dt = GetDataFromExcel(stream).Tables[0];
                    string message = "";
                    SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);
                    if (string.IsNullOrEmpty(user.Ext1))
                    {
                        ViewBag.UploadMsg = "上传失败,未指定帐户Kitting校验类型!";
                    }
                    else if (P_KITTING.CheckDataRules(user, dt, ref message))
                    {
                        try
                        {
                            ImportDatas(dt);
                            ViewBag.UploadMsg = "上传成功!";
                        }
                        catch (Exception e)
                        {
                            ViewBag.UploadMsg = "上传失败! " + e.Message;
                        }
                    }
                    else
                        ViewBag.UploadMsg = message;

                    //定义文件存放的目标路径
                    //string targetDir = System.Web.HttpContext.Current.Server.MapPath("~/KittingUpLoad");
                    //创建目标路径
                    //System.IO.Directory.CreateDirectory(targetDir);
                    //组合成文件的完整路径
                    //string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file.FileName));
                    //保存上传的文件到指定路径中
                    //file.SaveAs(path);
                    return View("FileUpload", "_LayoutFrame");
                }
                else
                {
                    info = "上传失败";
                    ViewBag.UploadMsg = info;
                    return View("FileUpload", "_LayoutFrame");
                }
            }
            catch
            {
                info = "上传失败";
                ViewBag.UploadMsg = info;
                return View("FileUpload", "_LayoutFrame");
            }
        }

        private void ImportDatas(DataTable dt)
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);
            string seq = DateTime.Now.ToString("yyyyMMdd");
            DataTable dtSeq = DataHelper.QueryDataTable("select Max(SEQ) from P_KITTING");
            if (dtSeq.Rows.Count > 0 && dtSeq.Rows[0][0].ToString().StartsWith(seq))
            {
                int start = int.Parse(DataHelper.QueryValue("SELECT seqcode.Nextval from dual").ToString());// int.Parse(dtSeq.Rows[0][0].ToString().Replace(seq, ""));
                seq += (start + 1).ToString("000000");
            }
            else
            {
                DataHelper.ExecSql("drop sequence seqcode");
                DataHelper.ExecSql("create sequence seqcode minvalue 1 maxvalue 99999999 START WITH 2");
                seq = seq + "000001";
            }
            Logs.Info("Save开始");
            //項次	厂别	PD Line	移转类型	工单号	站别	Item Line	   料號	品名	Keeper Code	
            //From 庫別	To 庫別	需求數量	交货Building	送貨碼頭	请求发货时间	備註	LOTNO
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (TransactionScope ts = new Castle.ActiveRecord.TransactionScope())
            {
                foreach (DataRow row in dt.Rows)
                {
                    P_KITTING kitting = new P_KITTING();
                    kitting.SEQ = decimal.Parse(seq);
                    kitting.CREATEID = PortalService.CurrentUserInfo.UserID;
                    kitting.CREATENAME = PortalService.CurrentUserInfo.Name;
                    kitting.CREATETIME = DateTime.Now;
                    kitting.ITEMSEQ = row[0].ToString();
                    kitting.FACTORYTYPE = row[1].ToString();
                    kitting.PDLINE = row[2].ToString();
                    kitting.TRANSTYPE = row[3].ToString();
                    kitting.WORKNO = row[4].ToString();
                    kitting.STATIONTYPE = row[5].ToString();
                    kitting.ITEMLINE = row[6].ToString();
                    kitting.ITEMCODE = row[7].ToString();
                    kitting.ITEMNAME = row[8].ToString();
                    kitting.KEEPERCODE = row[9].ToString();
                    kitting.FROMTYPE = row[10].ToString();
                    kitting.TOTYPE = row[11].ToString();
                    kitting.NEEDQTY = decimal.Parse(row[12].ToString());
                    kitting.GIVEBUILDING = row[13].ToString();
                    kitting.SENDPORT = row[14].ToString();
                    kitting.REQSENDTIME = DateTime.Parse(row[15].ToString());
                    kitting.REMARK = row[16].ToString();
                    kitting.LOTNO = row[17].ToString();
                    kitting.Create();
                }

                Logs.Info("接口开始");

                KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
                service.Timeout = Interface_Timeout;
                service.Credentials = new NetworkCredential(Interface_User, Interface_Pwd);
                //FUNCTION	功能方法	insert / search / delete（三选一）		
                //BUSINESS	业务类型	固定值为：wistronwh		
                //LOCATION	客户区域	wks / wcq / wcd / wok（四选一）		
                //ORDERTYPE	订单类型	固定值：calloff		
                KittingIO.DT_WIS_PORTAL1001_REQHEADER head = new KittingIO.DT_WIS_PORTAL1001_REQHEADER();
                head.FUNCTION = "insert";
                head.BUSINESS = user.Server_IAGUID;
                head.LOCATION = user.Server_Seed;
                head.ORDERTYPE = user.ThreeDESKEY;
                KittingIO.DT_WIS_PORTAL1001_REQ req = new KittingIO.DT_WIS_PORTAL1001_REQ();
                req.HEADER = head;
                List<KittingIO.DT_WIS_PORTAL1001_REQITEM> items = new System.Collections.Generic.List<KittingIO.DT_WIS_PORTAL1001_REQITEM>();
                KittingIO.DT_WIS_PORTAL1001_REQITEM item1 = new KittingIO.DT_WIS_PORTAL1001_REQITEM();
                item1.REFPO = seq;
                item1.ZLEVEL = "1";
                item1.INPUTUSER = user.LoginName;
                item1.INPUTDATE = now;
                items.Add(item1);
                foreach (DataRow row in dt.Rows)
                {
                    //批次号	1.refpo	2.refpo//项次		2.refpoline//厂别		2.ref1
                    //PD LINE		2.ref3//转料形态		2.ref2//工单号		2.ref6//站别		2.ref4//ITEM LINE		2.ref4
                    //料号		2.ref5//品名		2.ref8//KeeperCode		2.ref9//FROM库别		2.ref10//TO库别		2.ref11
                    //需求数量		2.qty1//交货Building		2.ref12//送货码头		2.ref13//WKS需求时间		2.date1
                    //备注		2.notes1//LOTNO		2.ref14//导入人	页面权限获取	2.inputuser//导入时间	1.inputdate	2.inputdate
                    KittingIO.DT_WIS_PORTAL1001_REQITEM ritem = new KittingIO.DT_WIS_PORTAL1001_REQITEM();
                    ritem.REFPO = seq;
                    ritem.ZLEVEL = "2";
                    ritem.REFPOLINE = row[0].ToString();
                    ritem.REF01 = row[1].ToString();
                    ritem.REF03 = row[2].ToString();
                    ritem.REF02 = row[3].ToString();
                    ritem.REF06 = row[4].ToString();
                    ritem.REF04 = row[5].ToString();
                    ritem.REF07 = row[6].ToString();
                    ritem.REF05 = row[7].ToString();
                    ritem.REF08 = row[8].ToString();
                    ritem.REF09 = row[9].ToString();
                    ritem.REF10 = row[10].ToString();
                    ritem.REF11 = row[11].ToString();
                    ritem.QTY1 = row[12].ToString();
                    ritem.REF12 = row[13].ToString();
                    ritem.REF13 = row[14].ToString();
                    ritem.DATE1 = DateTime.Parse(row[15].ToString()).ToString("yyyyMMddHHmmss");
                    ritem.NOTES1 = row[16].ToString();
                    ritem.REF14 = row[17].ToString();
                    ritem.INPUTUSER = user.LoginName;
                    ritem.INPUTDATE = now;
                    items.Add(ritem);
                }
                req.ITEM = items.ToArray();
                KittingIO.DT_WIS_PORTAL1001_REQITEM2 item = new KittingIO.DT_WIS_PORTAL1001_REQITEM2();
                item.begindate = "";
                req.ITEM2 = item;

                Logs.Info("接口访问开始..");
                Logs.Info("接口User:" + Interface_User + "--Pwd:" + Interface_Pwd);
                KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
                Logs.Info("接口返回");
                KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
                Logs.Info("接口返回:" + res.RETURN.STATUS);
                //ret.STATUS //SUCCESS/FAIL
                if (ret.STATUS.ToUpper() == "FAIL")
                {
                    Logs.Info("接口返回异常.." + ret.MESSAGE);
                    throw new System.Exception(ret.MESSAGE);
                }
            };


        }

        public ActionResult Export()
        {
            SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

            KittingIO.SI_WIS_PORTAL1001Service service = new KittingIO.SI_WIS_PORTAL1001Service();
            service.Timeout = Interface_Timeout;
            service.Credentials = new NetworkCredential(Interface_User, Interface_Pwd);
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
            item.begindate = Request.QueryString["CreateDateS"].Replace("-", "").Replace("/", "");//开始时间
            item.enddate = Request.QueryString["CreateDateE"].Replace("-", "").Replace("/", ""); ;//结束时间
            req.ITEM2 = item;
            KittingIO.DT_WIS_PORTAL1001_RES res = service.SI_WIS_PORTAL1001(req);
            KittingIO.DT_WIS_PORTAL1001_RESRETURN ret = res.RETURN;
            KittingIO.DT_WIS_PORTAL1001_RESITEM[] retItems = res.ITEM == null ? new KittingIO.DT_WIS_PORTAL1001_RESITEM[0] : res.ITEM;
            var list = new List<KittingIO.DT_WIS_PORTAL1001_RESITEM>();
            foreach (KittingIO.DT_WIS_PORTAL1001_RESITEM itemt in retItems)
            {
                Logs.Info("ITEM:" + itemt.ZLEVEL);
                if (itemt.ZLEVEL.Trim() == "2")
                    list.Add(itemt);
            }
            Workbook workbook = new Workbook(Server.MapPath("/filetemplates/KittingExport.xlsx"));
            Worksheet sheet = workbook.Worksheets[0];
            for (int i = 1; i <= list.Count; i++)
            {
                KittingIO.DT_WIS_PORTAL1001_RESITEM kit = list[i-1];
                sheet.Cells.Rows[i][0].PutValue(kit.REFPO);
                sheet.Cells.Rows[i][1].PutValue(kit.INPUTDATE);
                sheet.Cells.Rows[i][2].PutValue(kit.REF01);
                sheet.Cells.Rows[i][3].PutValue(kit.REFPOLINE);
                sheet.Cells.Rows[i][4].PutValue(kit.REF03);
                sheet.Cells.Rows[i][5].PutValue(kit.REF02);
                sheet.Cells.Rows[i][6].PutValue(kit.REF04);
                sheet.Cells.Rows[i][7].PutValue(kit.REF07);
                sheet.Cells.Rows[i][8].PutValue(kit.REF05);
                sheet.Cells.Rows[i][9].PutValue(kit.REF08);
                sheet.Cells.Rows[i][10].PutValue(kit.REF10);
                sheet.Cells.Rows[i][11].PutValue(kit.REF11);
                sheet.Cells.Rows[i][12].PutValue(kit.QTY1);
                sheet.Cells.Rows[i][13].PutValue(kit.QTY3);
                sheet.Cells.Rows[i][14].PutValue(kit.QTY5);
                sheet.Cells.Rows[i][15].PutValue(kit.ACK_TIME);
                sheet.Cells.Rows[i][16].PutValue(kit.REF18);
                sheet.Cells.Rows[i][17].PutValue(kit.REF12);
                sheet.Cells.Rows[i][18].PutValue(kit.REF13);
                sheet.Cells.Rows[i][19].PutValue(kit.DATE1);
                sheet.Cells.Rows[i][20].PutValue(kit.NOTES1);
                sheet.Cells.Rows[i][21].PutValue(kit.REF06);
                sheet.Cells.Rows[i][22].PutValue(kit.INPUTUSER);
                sheet.Cells.Rows[i][23].PutValue(kit.ISINPUT == "Y" ? "是" : "否");
                sheet.Cells.Rows[i][24].PutValue(kit.ASN);
                sheet.Cells.Rows[i][25].PutValue(kit.ZSOHZT);
                sheet.Cells.Rows[i][26].PutValue(kit.ZWMSDH);
                sheet.Cells.Rows[i][27].PutValue(kit.ZCPH);
                sheet.Cells.Rows[i][28].PutValue(kit.ZDDFYRQ);
            }
            string path = "/KittingUpload/" + user.LoginName + "_export_kitting_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            workbook.Save(Server.MapPath(path));
            //var obj = new { rows = P_KITTING.FindAll(SearchCriterion), total = ActiveRecordMediator.Count(typeof(P_KITTING), SearchCriterion.GetDetachedCriteriaWithoutOrder<P_KITTING>()) };
            //return Content(JsonHelper.GetJsonString(obj));
            return Content(path);
        }

        #region 获取excel数据
        /// <summary>
        /// 获取excel全部数据
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>DataSet</returns>
        private DataSet GetDataFromExcel(System.IO.Stream stream)
        {
            Cells cells;
            Workbook workbook = new Workbook(stream);
            DataSet excel_ds = new DataSet("myDS");//创建数据集
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                cells = workbook.Worksheets[i].Cells;
                DataTable dtnew = new DataTable(workbook.Worksheets[i].Name);//创建数据表
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    dtnew.Columns.Add(new DataColumn("col" + j, typeof(string)));
                }
                excel_ds.Tables.Add(dtnew);//把数据表添加到数据集中
                DataRow dr;
                for (int k = 1; k < cells.MaxDataRow + 1; k++)
                {
                    dr = dtnew.NewRow();
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        dr[j] = cells[k, j].StringValue.Trim();
                    }
                    excel_ds.Tables[workbook.Worksheets[i].Name].Rows.Add(dr);
                }
            }
            return excel_ds;

        }
        #endregion

    }
}

