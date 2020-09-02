using Aim.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    [Serializable]
    public class ResultVO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

    }
    public class XsyHelper
    {
        /// <summary>
        /// 销售易合同校验
        /// </summary>
        public static bool VididateHt(string cuscode, string orgcode, string buscode, DateTime dt_start, DateTime dt_end)
        {
            try
            {
                if (buscode.ToString().Length == 36)
                {
                    string sqlcc = @"select * from crm_business t where t.id='" + buscode + "'";
                    IDbConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    DataTable data_bus = DataHelper.QueryDataTable(sqlcc, conn);
                    buscode = data_bus.Rows.Count > 0 ? data_bus.Rows[0]["BUSINESSNO"].ToString() : "";
                    GetDataBySqlFromXsy.CrmSearchBySql buscode_new = new GetDataBySqlFromXsy.CrmSearchBySql();
                    GetDataBySqlFromXsy.phCrmSearchBySql codehead_new = new GetDataBySqlFromXsy.phCrmSearchBySql();
                    codehead_new.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                    codehead_new.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];
                    GetDataBySqlFromXsy.pbCrmSearchBySql codebody_new = new GetDataBySqlFromXsy.pbCrmSearchBySql();
                    GetDataBySqlFromXsy.pbCrmSearchBySql[] ucodebody_new = new GetDataBySqlFromXsy.pbCrmSearchBySql[1];
                    codebody_new.query = @"select customItem195__c from opportunity where customItem196__c='" + buscode + "'";
                    ucodebody_new[0] = codebody_new;
                    GetDataBySqlFromXsy.msgResponse codemsg = buscode_new.CallCrmSearchBySql(codehead_new, ucodebody_new);
                    var joscode_new = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(codemsg.list[0].originMessage);
                    foreach (var itms in joscode_new)
                    {
                        if (itms.Key == "records")
                        {
                            var jocode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString().Replace("[", "").Replace("]", ""));
                            foreach (var itm in jocode)
                            {
                                if (itm.Key == "customItem195__c")
                                {
                                    buscode = itm.Value.ToString();
                                }
                            }
                        }
                    }
                }
                //string cuscode = "FTLS001";
                //string orgcode = "1100";
                //string buscode = "SJ-2019123093831";
                string accountId = "";//客户ID
                string orgid = "";//组织ID
                string opportunityId = "";//商机ID
                                          //取客户ID
                GetDataBySqlFromXsy.CrmSearchBySql code = new GetDataBySqlFromXsy.CrmSearchBySql();
                GetDataBySqlFromXsy.phCrmSearchBySql codehead = new GetDataBySqlFromXsy.phCrmSearchBySql();
                codehead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                codehead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];
                GetDataBySqlFromXsy.pbCrmSearchBySql codebody = new GetDataBySqlFromXsy.pbCrmSearchBySql();
                GetDataBySqlFromXsy.pbCrmSearchBySql[] ucodebody = new GetDataBySqlFromXsy.pbCrmSearchBySql[1];
                codebody.query = @"select id from account where customItem219__c='" + cuscode + "'";
                ucodebody[0] = codebody;
                GetDataBySqlFromXsy.msgResponse cusmsg = code.CallCrmSearchBySql(codehead, ucodebody);
                var joscode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(cusmsg.list[0].originMessage);
                foreach (var itms in joscode)
                {
                    if (itms.Key == "records")
                    {
                        var jocode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString().Replace("[", "").Replace("]", ""));
                        foreach (var itm in jocode)
                        {
                            if (itm.Key == "id")
                            {
                                accountId = itm.Value.ToString();
                            }
                        }
                    }
                }
                //组织ID
                code = new GetDataBySqlFromXsy.CrmSearchBySql();
                codehead = new GetDataBySqlFromXsy.phCrmSearchBySql();
                codehead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                codehead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];
                codebody = new GetDataBySqlFromXsy.pbCrmSearchBySql();
                ucodebody = new GetDataBySqlFromXsy.pbCrmSearchBySql[1];
                codebody.query = @"select id from customEntity14__c where customItem2__c='" + (orgcode.Split('-').Length>1? orgcode.Split('-')[0]: orgcode) + "'";
                ucodebody[0] = codebody;
                GetDataBySqlFromXsy.msgResponse orgmsg = code.CallCrmSearchBySql(codehead, ucodebody);
                joscode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(orgmsg.list[0].originMessage);
                foreach (var itms in joscode)
                {
                    if (itms.Key == "records")
                    {
                        var jocode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString().Replace("[", "").Replace("]", ""));
                        foreach (var itm in jocode)
                        {
                            if (itm.Key == "id")
                            {
                                orgid = itm.Value.ToString();
                            }
                        }
                    }
                }
                //取商机ID
                code = new GetDataBySqlFromXsy.CrmSearchBySql();
                codehead = new GetDataBySqlFromXsy.phCrmSearchBySql();
                codehead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                codehead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];
                codebody = new GetDataBySqlFromXsy.pbCrmSearchBySql();
                ucodebody = new GetDataBySqlFromXsy.pbCrmSearchBySql[1];
                codebody.query = @"select id from opportunity where customItem195__c='" + buscode + "' and customItem197__c ='" + orgid + "'";
                ucodebody[0] = codebody;
                GetDataBySqlFromXsy.msgResponse busmsg = code.CallCrmSearchBySql(codehead, ucodebody);
                joscode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(busmsg.list[0].originMessage);
                foreach (var itms in joscode)
                {
                    if (itms.Key == "records")
                    {
                        var jocode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(itms.Value.ToString().Replace("[", "").Replace("]", ""));
                        foreach (var itm in jocode)
                        {
                            if (itm.Key == "id")
                            {
                                opportunityId = itm.Value.ToString();
                            }
                        }
                    }
                }

                //取合同
                code = new GetDataBySqlFromXsy.CrmSearchBySql();
                codehead = new GetDataBySqlFromXsy.phCrmSearchBySql();
                codehead.username = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_USER"];//"OFFER";
                codehead.password = System.Configuration.ConfigurationManager.AppSettings["XSY_INTERFACE_PASS"];
                codebody = new GetDataBySqlFromXsy.pbCrmSearchBySql();
                ucodebody = new GetDataBySqlFromXsy.pbCrmSearchBySql[1];
                codebody.query = @"select customItem152__c,customItem153__c,title, startDate,endDate,customItem169__c,customItem168__c
                             from contract where opportunityId='" + opportunityId + "' and accountId='" + accountId + "'";
                ucodebody[0] = codebody;
                GetDataBySqlFromXsy.msgResponse htmsg = code.CallCrmSearchBySql(codehead, ucodebody);
                joscode = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(htmsg.list[0].originMessage);
                List<HT> listOrder = new List<HT>();
                foreach (var itms in joscode)
                {
                    if (itms.Key == "records")
                    {
                        listOrder = JsonConvert.DeserializeObject<IList<HT>>(itms.Value.ToString());
                    }
                }
                if (listOrder.Count > 0)
                {
                    List<HTZH> _list_htzh = new List<HTZH>();
                    foreach (var itm in listOrder)
                    {
                        HTZH htzh = new HTZH();
                        htzh.customItem152__c = itm.customItem152__c;
                        htzh.customItem153__c = itm.customItem153__c;
                        htzh.startDate = GetTime(itm.startDate, itm.customItem169__c, itm.customItem168__c, false);
                        htzh.endDate = GetTime(itm.endDate, itm.customItem169__c, itm.customItem168__c, true);
                        htzh.title = itm.title;
                        _list_htzh.Add(htzh);
                    }
                    var resultorderby = _list_htzh.OrderBy(r => r.startDate).ToList();
                    var resultdescorderby = _list_htzh.OrderByDescending(r => r.endDate).ToList();

                    if (dt_start >= resultorderby[0].startDate && dt_end <= resultdescorderby[0].endDate)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static DateTime GetTime(string time, string yq, string dw, bool isAddYq)
        {
            int yq_num = !string.IsNullOrEmpty(yq)?int.Parse(yq):0;
            DateTime dt = DateTime.Now;
            time = time.Length > 0 ? time.Substring(0, 10) : time;
            try
            {
                if (isAddYq)
                {
                    if (!string.IsNullOrEmpty(yq) && yq != "0")
                    {
                        switch (dw)
                        {
                            case "3"://"年":
                                dt = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture).AddYears(yq_num);
                                break;
                            case "2":// "月":
                                dt = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture).AddMonths(yq_num);
                                break;
                            case "1":// "日":
                                dt = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture).AddDays(yq_num);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        dt = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
                else
                {
                    dt = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
            catch
            {

            }
            return dt;
        }
    }

    /// <summary>
    /// 接口合同数据
    /// </summary>
    public class HT
    {
        public string customItem152__c { get; set; }//合同账期 
        public string customItem153__c { get; set; }//合同编码
        public string title { get; set; }//合同名称
        public string startDate { get; set; }//合同开始日期
        public string endDate { get; set; }//合同中止日期
        public string customItem169__c { get; set; }//合同延期
        public string customItem168__c { get; set; }//延期单位

    }
    /// <summary>
    /// 转换后合同数据
    /// </summary>
    public class HTZH
    {
        public string customItem152__c { get; set; }//合同账期 
        public string customItem153__c { get; set; }//合同编码
        public string title { get; set; }//合同名称
        public DateTime startDate { get; set; }//合同开始日期
        public DateTime endDate { get; set; }//合同中止日期
        public string customItem169__c { get; set; }//合同延期
        public string customItem168__c { get; set; }//延期单位

    }
}