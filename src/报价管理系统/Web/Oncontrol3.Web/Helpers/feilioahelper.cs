using System;
using System.Linq;
//using System.Data.OracleClient;
using System.IO;
using Aim.Data;
using Aim.Portal.Model;
using Foqus;
//using OA_WS.OAWorkflowService;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;
using System.Data;


namespace Oncontrol3.Web.Helpers
{
    public static class feilioahelper
    {
        // public static readonly IDbConnection connOracleWF = new OracleConnection(ConfigurationManager.AppSettings["FEILIOAWF"]);
        // public static readonly IDbConnection connOracleHRM = new OracleConnection(ConfigurationManager.AppSettings["FEILIOAHRM"]);

        private static Log log = new Log(AppDomain.CurrentDomain.BaseDirectory + @"/testlog/oainsertLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

        public static void LogMsg(string msg)
        {
            log.log(msg);
        }

        public static int? ToIntOrNull(object data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }
        //public static string GenInsertSQL(object obj)
        //{
        //    string colsName = "";
        //    string colsValues = "";
        //    PropertyInfo[] pis = obj.GetType().GetProperties();
        //    foreach (PropertyInfo pi in pis)
        //    {
        //        if (pi.Name.ToUpper() == "ID")          // 不插入自动增长列
        //            continue;

        //        if (pi.GetValue(obj, null) == null)     // null值不插入
        //            continue;

        //        if (pi.CanWrite && pi.Name.ToUpper() != "REQUESTID" && pi.Name != "ID"
        //            && pi.Name.ToUpper() != "FTRIGGERFLAG" && pi.Name.ToUpper() != "BH")
        //        {
        //            //colsName += pi.Name + ",";
        //            //colsValues += "'" + pi.GetValue(obj, null) + "',";
        //            //colsName += "[" + pi.Name + "],";
        //            colsName += pi.Name + ",";
        //            //if (pi.PropertyType.ToString().Contains("String") || pi.PropertyType.ToString().Contains("Date"))
        //            //    colsValues += "'" + pi.GetValue(obj, null) + "',";
        //            if (pi.PropertyType.ToString().Contains("String"))
        //            {
        //                colsValues += "'" + pi.GetValue(obj, null) + "',";
        //            }
        //            else if (pi.PropertyType.ToString().Contains("Date"))
        //            {
        //                colsValues += "to_date('" + pi.GetValue(obj, null) + "','yyyy/mm/dd HH24:MI:SS'),";
        //            }
        //            else
        //            {
        //                colsValues += pi.GetValue(obj, null) + ",";
        //            }
        //        }
        //    }
        //    string sqlInsert = "INSERT INTO feilioa2." + obj.GetType().Name + " (" + colsName.TrimEnd(',') + ") VALUES (" + colsValues.TrimEnd(',') + ")";
        //    //string sqlInsert = "INSERT INTO " + obj.GetType().Name + " (" + colsName.TrimEnd(',') + ") VALUES (" + colsValues.TrimEnd(',') + ")";
        //    // LogMsg(sqlInsert);
        //    return sqlInsert;
        //}

        //public static string GetACCOUNTTYPE(string companyid, string workno)
        //{
        //    string ACCOUNTTYPE = "0";
        //    try
        //    {
        //        string data3 = DataHelper.QueryValue("select description from sysgroup where groupid='" + companyid + "'") + "";
        //        if (connOracleHRM.State == ConnectionState.Closed)
        //        {
        //            connOracleHRM.Open();
        //        }
        //        string gsjc = (string)DataHelper.ExecSql("select subcompanyname from feilioa.HRMSUBCOMPANY t where id=(select subcompanyid1 from feilioa.HRMRESOURCE t where loginid='" +
        //               workno + "' and ACCOUNTTYPE is null and rownum=1)", feilioahelper.connOracleHRM) + "";
        //        if (gsjc != data3)
        //        {
        //            //是兼职
        //            ACCOUNTTYPE = "1";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMsg("GetACCOUNTTYPE ERROR" + ",companyid:" + companyid + ",workno:" + workno + " Exception:" + ex.Message);
        //    }
        //    return ACCOUNTTYPE;
        //}

        //public static int? GetMANAGERId(string workno)
        //{
        //    try
        //    {
        //        string sql = String.Format("select MANAGERID from feilioa.hrmresource where  LOGINID = '{0}'", workno);
        //        if (connOracleHRM.State == ConnectionState.Closed)
        //        {
        //            connOracleHRM.Open();
        //        }
        //        return ToIntOrNull(DataHelper.QueryValue(sql, feilioahelper.connOracleHRM));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMsg("GetMANAGERId ERROR" + ",workno:" + workno + " Exception:" + ex.Message);
        //        return null;
        //    }
        //}
        //public static int? GetUserId(string workno)
        //{
        //    try
        //    {
        //        string sql = String.Format("select ID from feilioa.hrmresource where  LOGINID = '{0}'", workno);
        //        if (connOracleHRM.State == ConnectionState.Closed)
        //        {
        //            connOracleHRM.Open();
        //        }
        //        return ToIntOrNull(DataHelper.QueryValue(sql, feilioahelper.connOracleHRM));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMsg("GetUserId ERROR" + ",workno:" + workno + " Exception:" + ex.Message);
        //        return null;
        //    }
        //}
        /// <summary>
        /// 获取人员的EXT1
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetEXT1(string userid)
        {
            try
            {
                string sql = "SELECT EXT1 FROM SYSUSER WHERE WORKNO='" + userid + "'";
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                object ext1 = DataHelper.QueryValue(sql, conn);
                return ext1 == null ? 0 : Convert.ToInt32(ext1);

            }
            catch (Exception ex)
            {
                LogMsg("GetEXT1 ERROR" + ",WORKNO:" + userid + " Exception:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// 获取报价编号
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static string GetBJNO(string rid)
        {
            try
            {
                string sql = "SELECT BJNAME FROM SQM_BJ_MAIN_BASIC WHERE RID='" + rid + "'";

                var BJNAME = DataHelper.QueryValue(sql);
                return BJNAME == null ? "" : BJNAME.ToString();

            }
            catch (Exception ex)
            {
                LogMsg("GetBJNO ERROR" + ",RID:" + rid + " Exception:" + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// 获取产品
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static string GetCP(string vrid)
        {
            try
            {
                string sql = string.Format("SELECT PRODUCT_NAME FROM SQM_BJ_PSF WHERE VRID='{0}'  AND CHOOSESTATUS = '1' ", vrid);

                var PNAME = DataHelper.QueryValue(sql);
                return PNAME == null ? "" : PNAME.ToString();

            }
            catch (Exception ex)
            {
                LogMsg("PNAME ERROR" + ",MRID:" + vrid + " Exception:" + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// 获取产品所属事业部
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static string GetCPSSSYB(string vrid)
        {
            try
            {
                string sql = "SELECT distinct spe.BUSINESSORG FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='" + vrid + "'";

                string BUSINESSORG = DataHelper.QueryValue(sql).ToString();
                return BUSINESSORG == null ? "" : BUSINESSORG;

            }
            catch (Exception ex)
            {
                LogMsg("BUSINESSORG ERROR" + ",MRID:" + vrid + " Exception:" + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// 获取销售人事组织
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static string GetXSRSZZ(string userid)
        {
            try
            {
                string sql = "SELECT PK_CORP FROM SYSUSER WHERE WORKNO='" + userid + "'";
                IDbConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var CORP = DataHelper.QueryValue(sql, conn);
                return CORP == null ? "" : CORP.ToString();

            }
            catch (Exception ex)
            {
                LogMsg("GetEXT1 ERROR" + ",USERID:" + userid + " Exception:" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 获取运营组织
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static string GetYYZZ(string rid)
        {
            try
            {
                string sql = "SELECT ORGCODE FROM SQM_BJ_ORG WHERE MRID='" + rid + "'";
                var ORGCODE = DataHelper.QueryValue(sql);
                return ORGCODE == null ? "" : ORGCODE.ToString();

            }
            catch (Exception ex)
            {
                LogMsg("ORGCODE ERROR" + ",MRID:" + rid + " Exception:" + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// 获取负责人
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static int GetCPFZR(string vrid,string rid)
        {
            string Businessopgs = "";
            object WORKNO = "";
            try
            {
                string sql = "";
                //判断是否非标
                string sqls = string.Format("select * from SQM_BJ_MAIN_BASIC t where FBPRICE ='1' and rid='{0}'", rid);
                DataTable FBPRICE = DataHelper.QueryDataTable(sqls);
                if (FBPRICE.Rows.Count > 0)
                {
                    sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}'", vrid);
                }
                else {
                    sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}' and sbp.fee_name is not null", vrid);
                }

                DataTable BUSINESSORG = DataHelper.QueryDataTable(sql);
                if (BUSINESSORG.Rows.Count > 0)
                {
                    foreach (DataRow dr in BUSINESSORG.Rows)
                    {
                        Businessopgs = dr["BUSINESSORG"].ToString();
                        if (Businessopgs == "空运")
                        {
                            //WORKNO = 10000563;
                            WORKNO = 369;

                        }
                        else if (Businessopgs == "海运")
                        {
                            //WORKNO = 10000509;
                            // WORKNO = 329;
                            WORKNO = 2144;
                        }
                        else if (Businessopgs == "供应链")
                        {
                            // WORKNO = 10000621;
                            WORKNO = 421;
                        }
                        else if (Businessopgs == "运输")
                        {
                            // WORKNO = 10000195;
                            WORKNO = 154;
                        }
                        else
                        {
                            WORKNO = 0;
                        }
                    }
                }

                //string sqls = string.Format("SELECT USERID  FROM SYSUSER  WHERE WORKNO='{0}'", WORKNO);
                //IDbConnection conn = new OracleConnection();
                //conn.ConnectionString = ConfigHelper.AppSettings("connection_crm");
                //if (conn.State != ConnectionState.Open)
                //{
                //    conn.Open();
                //}
                //object USERID = DataHelper.QueryValue(sqls, conn);

                return WORKNO == null ? 0 : Convert.ToInt32(WORKNO);

            }
            catch (Exception ex)
            {
                LogMsg("BUSINESSORG ERROR" + ",MRID:" + vrid + " Exception:" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        ///  是否超出定价范围
        /// </summary>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public static string ISDJFW(string vrid)
        {
            string Status = "";
            string DJFW = "";
            string sqls = string.Format("select distinct BJSTATAUS from SQM_BJ_PSF where vrid='{0}' and (status<>'0' or status is null)", vrid);
            DataTable Stataus = DataHelper.QueryDataTable(sqls);
            if (Stataus.Rows.Count > 0)
            {

                foreach (DataRow dr in Stataus.Rows)
                {
                    Status = dr["BJSTATAUS"].ToString();

                    if (Status == "5")
                    {
                        DJFW = "1";
                        break;
                    }
                    else
                    {
                        DJFW = "0";
                    }
                }
            }

            return DJFW;
        }

        /// <summary>
        ///  跨事业部 流程
        /// </summary>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public static string GetFlow(string vrid, string rid)
        {
            string Flow = "0";
            try
            {
                string sql = "";
                string sqlst = string.Format("select * from SQM_BJ_MAIN_BASIC t where FBPRICE ='1' and rid='{0}'", rid);
                DataTable FBPRICEst = DataHelper.QueryDataTable(sqlst);
                if (FBPRICEst.Rows.Count > 0)
                {
                    sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}'", vrid);
                }
                else
                {
                    sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}' and sbp.fee_name is not null", vrid);
                }

                //string sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}' and sbp.fee_name is not null", vrid);
                DataTable BUSINESSORG = DataHelper.QueryDataTable(sql);
                if (BUSINESSORG.Rows.Count >= 2)
                {
                    Flow = "1";//多事业部
                }

                string sqls = string.Format("select * from SQM_BJ_MAIN_BASIC t where FBPRICE ='1' and rid='{0}'", rid);
                DataTable FBPRICE = DataHelper.QueryDataTable(sqls);
                if (FBPRICE.Rows.Count > 0)
                {
                    Flow = "2";//非标
                }
                string sqls2 = string.Format("SELECT  RID  FROM SQM_BJ_PSF  where ISLSC='1' and vrid='{0}'", vrid);
                DataTable FEENAME = DataHelper.QueryDataTable(sqls2);
                if (FEENAME.Rows.Count > 0)
                {
                    Flow = "4";//含有包干费
                }
                if (BUSINESSORG.Rows.Count >= 2 && FBPRICE.Rows.Count > 0)
                {
                    Flow = "3";//多事业部并且非标
                }

                return Flow;
            }
            catch (Exception ex)
            {
                LogMsg("跨事业部流程 ERROR" + ",vrid:" + vrid + "，rid " + rid + " 。Exception:" + ex.Message);
                return Flow;
            }
        }
        /// <summary>
        /// 审批备注 OA
        /// </summary>
        public static string GetMemo(string vrid)
        {

            string MEMO = "";
            string sql = string.Format("select MEMO from SQM_BJ_VER where rid='{0}'", vrid);
            DataTable dt = DataHelper.QueryDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MEMO"].ToString() != "")
                    {
                        MEMO = dr["MEMO"].ToString();
                    }
                }
            }

            return MEMO;
        }
        /// <summary>
        ///  跨事业部 多产品负责人
        /// </summary>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public static string GetDCPFZR(string vrid,string rid)
        {
            string Businessopgs = "";
            string WORKNO = "";
            string sql = "";
            string sqls = string.Format("select * from SQM_BJ_MAIN_BASIC t where FBPRICE ='1' and rid='{0}'", rid);
            DataTable FBPRICE = DataHelper.QueryDataTable(sqls);
            if (FBPRICE.Rows.Count > 0)
            {
                sql=string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}'", vrid);
            }
            else
            {
                sql=string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}' and sbp.fee_name is not null", vrid);
            }

            //string sql = string.Format("SELECT distinct spe.BUSINESSORG  FROM SQM_BJ_PSF sbp left join SQM_PRD_EXT spe on spe.PRODUCTKEY=sbp.PRODUCT_CODE WHERE sbp.VRID='{0}' and sbp.fee_name is not null", vrid);

            DataTable BUSINESSORG = DataHelper.QueryDataTable(sql);
            if (BUSINESSORG.Rows.Count > 0)
            {
                foreach (DataRow dr in BUSINESSORG.Rows)
                {
                    Businessopgs = dr["BUSINESSORG"].ToString();
                    if (Businessopgs == "空运")
                    {
                        //WORKNO = 10000563;
                        WORKNO += 369 + ",";

                    }
                    else if (Businessopgs == "海运")
                    {
                        //WORKNO = 10000509;
                        //WORKNO += 329 + ",";
                        WORKNO += 2144 + ",";
                    }
                    else if (Businessopgs == "供应链")
                    {
                        // WORKNO = 10000621;
                        WORKNO += 421 + ",";
                    }
                    else if (Businessopgs == "运输")
                    {
                        // WORKNO = 10000195;
                        WORKNO += 154 + ",";
                    }
                    else
                    {
                        WORKNO = "0,";
                    }
                }
            }

            WORKNO = WORKNO.TrimEnd(',');

            return WORKNO == null ? "" : WORKNO;
        }


        /// <summary>
        ///  获取人员的EXT2
        /// </summary>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public static int? GetEXT2(string USERID)
        {
            try
            {
                string sql = "SELECT EXT2 FROM SYSUSER WHERE USERID='" + USERID + "'";
                return ToIntOrNull(DataHelper.QueryValue(sql));
            }
            catch (Exception ex)
            {
                LogMsg("GetUserId ERROR" + ",USERID:" + USERID + " Exception:" + ex.Message);
                return null;
            }
        }

        public static string getZZ(string companyid)
        {
            if (String.IsNullOrEmpty(companyid)) { return ""; }
            if (companyid.Length < 4) { return companyid; }
            string zz = companyid.Substring(0, 4);
            if (companyid.Contains('-'))
            {
                try
                {
                    zz = companyid.Split('-')[1];
                }
                catch (Exception ex)
                {
                    LogMsg("getZZ ERROR" + ",companyid:" + companyid + " Exception:" + ex.Message);
                }
            }
            return zz;
        }

        public static void getCompanyDept(SysUser userent, ref string DeptId, ref string DeptName, ref string CompanyId, ref string CompanyName)
        {
            if (userent.Pk_corp + "" == "" || userent.Pk_deptdoc + "" == "") return;

            SysGroup Company = SysGroup.Find(userent.Pk_corp + "");
            SysGroup Dept = SysGroup.Find(userent.Pk_deptdoc + "");
            CompanyId = Company.GroupID;
            CompanyName = Company.Name;
            DeptId = Dept.GroupID;
            DeptName = Dept.Name;
        }

        public static string getCompany(SysUser userent)
        {
            if (userent.Pk_corp + "" == "" || userent.Pk_deptdoc + "" == "") return "";

            SysGroup Company = SysGroup.Find(userent.Pk_corp + "");
            SysGroup Dept = SysGroup.Find(userent.Pk_deptdoc + "");
            return Company.GroupID;
        }

        public static string getDept(SysUser userent)
        {
            if (userent.Pk_corp + "" == "" || userent.Pk_deptdoc + "" == "") return "";

            SysGroup Dept = SysGroup.Find(userent.Pk_deptdoc + "");
            return Dept.GroupID;
        }
        //登录验证
        //public static FileItem getFileItem(string type, string formid)
        //{
        //    FileItem file = new FileItem();
        //    try
        //    {


        //        switch (type)
        //        {
        //            case "销售合同":
        //                CRM_SALESCONTRACT cs = CRM_SALESCONTRACT.TryFind(formid);
        //                if (null != cs)
        //                {
        //                    string FULLNAME = "";
        //                    string sql = "SELECT * FROM CRM_PACT_HTFILE WHERE PACT_ID='{0}'";
        //                    sql = string.Format(sql, cs.ID);
        //                    DataTable dataTable = DataHelper.QueryDataTable(sql);
        //                    if (dataTable != null && dataTable.Rows.Count > 0)
        //                    {
        //                        FULLNAME = dataTable.Rows[0]["FULLNAME"] + "";
        //                        FULLNAME = FULLNAME.Length > 36 ? FULLNAME.Substring(0, 36) : FULLNAME;
        //                        file = FileItem.Find(FULLNAME);
        //                    }
        //                }
        //                break;
        //            case "销售合同附件":
        //                CRM_SALESCONTRACT cs2 = CRM_SALESCONTRACT.TryFind(formid);
        //                if (null != cs2)
        //                {
        //                    string FULLNAME = "";
        //                    string sql = "SELECT * FROM CRM_PACT_FILE WHERE PACT_ID='{0}'";
        //                    sql = string.Format(sql, cs2.ID);
        //                    DataTable dataTable = DataHelper.QueryDataTable(sql);
        //                    if (dataTable != null && dataTable.Rows.Count > 0)
        //                    {
        //                        FULLNAME = dataTable.Rows[0]["FULLNAME"] + "";
        //                        FULLNAME = FULLNAME.Length > 36 ? FULLNAME.Substring(0, 36) : FULLNAME;
        //                        file = FileItem.Find(FULLNAME);

        //                    }
        //                }
        //                break;
        //            case "采购合同":
        //                CRM_STOCKPACT STOCK = CRM_STOCKPACT.TryFind(formid);
        //                if (null != STOCK)
        //                {
        //                    string FULLNAME = "";
        //                    string sql = "select * from CRM_STOCK_FILE where  STOCK_ID='{0}'";
        //                    sql = string.Format(sql, STOCK.ID);
        //                    DataTable dataTable = DataHelper.QueryDataTable(sql);
        //                    if (dataTable != null && dataTable.Rows.Count > 0)
        //                    {
        //                        FULLNAME = dataTable.Rows[0]["FULLNAME"] + "";
        //                        FULLNAME = FULLNAME.Length > 36 ? FULLNAME.Substring(0, 36) : FULLNAME;
        //                        file = FileItem.Find(FULLNAME);
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //       LogMsg(DateTime.Now.ToString()+type+"获取File报错"+e);
        //    }
        //    return file;

        //}
        //获取文件的路径
        public static string getfilepath(FileItem fileItem)
        {
            string pathOfFileItem = fileItem.Path;
            string getFolderKeyByPath = "SELECT FOLDERKEY FROM FILEFOLDER WHERE PATH='{0}'";
            getFolderKeyByPath = string.Format(getFolderKeyByPath, pathOfFileItem);
            string folderKey = DataHelper.QueryValue<string>(getFolderKeyByPath);
            string path = "";
            string getPath = "SELECT ROOTPATH FROM FileModule WHERE NAME='{0}'";
            getPath = string.Format(getPath, folderKey);
            path = DataHelper.QueryValue<string>(getPath);
            path = Path.Combine(path, pathOfFileItem);
            path = Path.Combine(path, fileItem.Id);
            path = path + "_" + fileItem.Name;// Path.Combine(path, "_" + fileItem.Name);
            LogMsg("fileItem中的Path:" + pathOfFileItem + "folderkey:" + folderKey + "path:" + path);
            return path;
        }

        public static string GetFormatHTNAME(string htname)
        {
            string[] tszifu = { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "！", "@", "#", "￥", "%", "…", "&", "*", "（", "）", "—", "+", " " };
            foreach (var item in tszifu)
            {
                if (item == "…" || item == "—")
                {
                    htname = htname.Replace(item, "");
                    while (htname.Contains(item))
                    {
                        htname = htname.Replace(item, "");
                    }
                }
                else
                {
                    if (htname.Contains(item))
                    {
                        htname = htname.Replace(item, "");
                    }
                }
            }
            return htname;
        }
    }
}