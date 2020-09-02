using Aim;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class QM_Price_TMController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QMPriceTM(string FWA)
        {
            ViewBag.FWA = FWA;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FWA"></param>
        /// <returns></returns>
        public ActionResult GetQMPriceTMAddress(string FWA)
        {
            //以分号拆分字符串，获取每组FWA号和项目号
            var arrs = FWA.Split(';');
            //结算调取报价单PDF文件 url地址
            List<string> listurl = new List<string>();
            foreach (var arr in arrs)
            {
                //以逗号查分字符串，获取FWA好和项目号
                var arrfwa = arr.Split(',');
                if (arrfwa.Length > 0)
                {
                    string sql = "select mrid, zver from SQM_FWA_REF where fwa='" + arrfwa[0] + "'";
                    if (arrfwa.Length > 1 && !string.IsNullOrEmpty(arrfwa[1]))
                    {
                        string itemno = arrfwa[1];
                        itemno = Convert.ToInt32(itemno).ToString();
                        sql = sql + " and instr(itemno,'" + itemno + "')>0";
                    }
                    //根据fwa和项目号itemno 获取数据
                    var data = DataHelper.QueryDictList(sql);
                    var IP = GetAddressIP();
                    foreach (var item in data)
                    {
                        string sqlstr = "select uploadurl,uploadname from sqm_bj_ver where mrid ='{0}' and zver = '{1}'";
                        sqlstr = string.Format(sqlstr, item["MRID"].ToString(), item["ZVER"].ToString());
                        var list = DataHelper.QueryDictList(sqlstr);
                        foreach (var l in list)
                        {
                            var upload = IP + "/Excel/output/" + l["UPLOADNAME"].ToString();
                            
                            listurl.Add(upload);
                        }
                    }
                }
            }
            //ViewData["UPLOADURL"] = listurl;
            return Content(JsonHelper.GetJsonString(listurl));
        }

        /// <summary>
        /// 获取服务器的IP地址和端口号
        /// </summary>
        /// <returns></returns>
        private string GetAddressIP()
        {
            //string server_name = Request.ServerVariables["SERVER_NAME"];//运行脚本的服务器主机名，DNS或IP地址
            string server_port = Request.ServerVariables["SERVER_PORT"];//服务器端口

            ///获取服务端
            string AddressIP = string.Empty;
            int port = 0;
            IPEndPoint iep;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    iep = new IPEndPoint(_IPAddress, 80);
                    port = iep.Port;
                }
            }
            return "http://" + AddressIP + ":" + server_port;
        }
    }
}
