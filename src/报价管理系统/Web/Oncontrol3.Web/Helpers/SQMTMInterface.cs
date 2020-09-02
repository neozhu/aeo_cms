using Aim.Data;
using Oncontrol3.Web.FWA701;
using Oncontrol3.Web.RATE601;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Oncontrol3.Web
{
    public class SQMTMInterface
    {
        #region  报价业务接口逻辑

        public class rateSTRCVAL
        {
            public List<string> CalcSTRC;
            public DataTable CalcVAL;
        }

        /// <summary>
        /// getPSFCV
        /// </summary>
        /// <param name="keyvalue">报价主表RID</param>
        /// <param name="zver">版本号</param>
        /// <returns></returns>
        public bool getPSFCV(string keyvalue, string zver, ref List<rateSTRCVAL> ratestrcvallist)
        {
            bool flag = true;
            keyvalue = "67c80c60-5a2d-4b";
            //keyvalue = "19d11524-26cc-43";//飞力环境
            zver = "V1";
            string bjrid = "";
            string prdcode = "";
            string srvcode = "";
            string feecode = "";
            string gdzrid = "";
            string djfsrid = "";
            string sql = "";
            string fieldkeys = "";
            bool min = false;

            try
            {
                //首先查询出版本的rid
                var vrid = DataHelper.QueryValue(string.Format("SELECT RID FROM SQM_BJ_VER WHERE MRID = '{0}' AND ZVER = '{1}'", keyvalue, zver));
                //psf表信息
                sql = string.Format("SELECT * FROM SQM_BJ_PSF WHERE VRID = '{0}' AND CHOOSESTATUS = '1'", vrid);
                DataTable dt = DataHelper.QueryDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    bjrid = dr["RID"].ToString();
                    prdcode = dr["PRODUCT_CODE"].ToString();
                    srvcode = dr["SERVICE_CODE"].ToString();
                    feecode = dr["FEE_CODE"].ToString();

                    //获取报价值表的数据
                    sql = @"select * from SQM_MODEBJ_VAL t where FEECALCID='" + bjrid + "'";
                    DataTable zbdt = DataHelper.QueryDataTable(sql);
                    foreach (DataRow zbdr in zbdt.Rows)
                    {
                        djfsrid = zbdr["DJFSRID"].ToString();
                        gdzrid = zbdr["GDZRID"].ToString();
                        //是否有MIN
                        string minprice = DataHelper.QueryValue("select MINPRICE from SQM_FEE_CALC where FEECODE='" + feecode + "'") + "";
                        if (minprice == "1")
                        {
                            min = true;
                        }
                        string where = "";
                        string wheredt = "";
                        if (!String.IsNullOrEmpty(djfsrid))
                        {
                            where += " and r.DJFSRID='" + djfsrid + "' ";
                            wheredt += " and DJFSRID='" + djfsrid + "' ";
                        }
                        else
                        {
                            where += " and r.DJFSRID is null ";
                            wheredt += " and DJFSRID is null ";
                        }
                        if (!String.IsNullOrEmpty(gdzrid))
                        {
                            where += " and r.GDZRID='" + gdzrid + "' ";
                            wheredt += " and GDZRID='" + gdzrid + "' ";
                        }
                        else
                        {
                            where += " and r.GDZRID is null ";
                            wheredt += " and GDZRID is null ";
                        }
                        List<string> calccodestrc = new List<string>();
                        fieldkeys = getFieldKeys(bjrid, min, where, ref calccodestrc);
                        sql = "select " + fieldkeys + " from SQM_MODEBJ_VAL where FEECALCID='{0}' and STATUS='1' {1}";
                        sql = string.Format(sql, bjrid, wheredt);
                        DataTable zbsjdt = null;
                        zbsjdt = DataHelper.QueryDataTable(sql);

                        rateSTRCVAL rate = new rateSTRCVAL();
                        rate.CalcSTRC = calccodestrc;
                        rate.CalcVAL = zbsjdt;
                        ratestrcvallist.Add(rate);
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                //throw;
                return flag;
            }
        }
        public string getFieldKeys(string bjrid, bool min, string where, ref List<string> calccodestrc)
        {
            try
            {
                string filedkeys = "JSFFLX,JSFF,JTLJ,JXJC,CALCUNIT,CURRENCY,CALCTYPE,";
                string sql = @"select r.CALCCODE,r.CALCNAME, r.SCALE,r.VALCOL,r.MSRCODE,r.ISCNT from SQM_FEE_CALC_REF r
                        left join SQM_BJ_PSF p on r.feecode=p.fee_code
                        where p.Rid='{0}' and r.STATUS='1' {1} order by r.SORD asc";
                sql = string.Format(sql, bjrid, where);
                DataTable FCREFdt = DataHelper.QueryDataTable(sql);
                if (FCREFdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in FCREFdt.Rows)
                    {
                        calccodestrc.Add(dr["CALCCODE"] + "");//标度结构

                        filedkeys += "'" + dr["CALCNAME"].ToString() + "',";
                        filedkeys += dr["VALCOL"].ToString() + " as " + dr["CALCCODE"] + ",";
                        filedkeys += dr["VALCOL"].ToString() + "C as " + dr["CALCCODE"] + "CODE,";
                        filedkeys += "'" + dr["SCALE"].ToString() + "'" + " as " + dr["CALCCODE"] + "SCALE,";
                        filedkeys += "'" + dr["MSRCODE"].ToString() + "'" + " as " + dr["CALCCODE"] + "MSRCODE,";
                        filedkeys += "'" + dr["ISCNT"].ToString() + "'" + " as " + dr["CALCCODE"] + "ISCNT,";
                    }
                }
                //if (min)
                //{
                    filedkeys += "MINBJPRICE,";
                //}
                filedkeys += "PURPRICE,COSTPRICE,MAXPRICE,MINPRICE,GUIDEPRICE,to_char(STARTDATE,'yyyy/mm/dd') as STARTDATE,to_char(ENDDATE,'yyyy/mm/dd') as ENDDATE,BJSTATUS,OVERSTATUS,BJPRICE,MEMO,SPRICE";
                return filedkeys;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        public string getSCATYP(string scatyp)
        {
            //A-基础标度 (>=);B-标度上限 (<=);X-相同标度 (=)
            if (scatyp == ">=")
            {
                return "A";
            }
            else if (scatyp == "<=")
            {
                return "B";
            }
            else
            {
                return "X";
            }
        }

        public string getCACL_TYP(string scatyp, string calctype = "")
        {
            //A-绝对;B-相对
            if (scatyp == "=")
            {
                return "A";
            }
            else if (scatyp == ">=" || scatyp == "<=")
            {
                if (string.IsNullOrEmpty(calctype))
                {
                    return "B";
                }
                else
                {
                    return calctype;
                }
            }
            else
            {
                return "A";
            }
        }

        public string getDBKEY(string code,string codeval)
        {
            string rtn = "";

            if ("DESTLOC_ZONE" == code || "SOURCELOC_ZONE" == code)
            {
                rtn = (string)DataHelper.QueryValue("select column1 from  mdm_calc_value where mdkey = 'DESTLOC_ZONE' and column2 = '" + codeval + "'");
            }

            return rtn;
        }

        public string GenerateRandomCode(int length = 3)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
        public string genRateId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffffff"); // +GenerateRandomCode();
        }
        public static string genITEMKEY()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
        public static string GenerateFWASerial(string prefxFWA)
        {
            string sql = string.Format(" SELECT * FROM ( SELECT FWA FROM SQM_FWA_REF WHERE FWA LIKE '{0}%' ORDER BY FWA DESC ) WHERE  ROWNUM<=1 ", prefxFWA);
            string fwano = DataHelper.QueryValue(sql) + "";
            if (String.IsNullOrEmpty(fwano))
            {
                return prefxFWA + "001";
            }
            else
            {
                //int nonew = int.Parse(fwano.Substring(10, 3));
                int nonew = int.Parse(fwano.Substring(fwano.Length - 3, 3));//获取协议号后三位
                if (nonew < 9)
                {
                    return prefxFWA + "00" +  (nonew + 1).ToString();
                }
                else if (nonew >= 9 && nonew < 99)
                {
                    return prefxFWA + "0" + (nonew + 1).ToString();
                }
                else if (nonew >= 99 && nonew < 999)
                {
                    return prefxFWA + (nonew + 1).ToString();
                }
                else
                {
                    return prefxFWA + (nonew + 1).ToString();
                }
            }
        }
    }
}