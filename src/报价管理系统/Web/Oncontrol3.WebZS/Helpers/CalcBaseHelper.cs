using Aim.Data;
using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    /// <summary>
    ///  计算基础对应计量单位、值的字段长度校验
    /// <author>
    ///		<name>DLC</name>
    ///		<date>2018.08.14</date>
    /// </author>
    /// </summary>
    public class CalcBaseHelper
    {
        /// <summary>
        /// 判断计算基础单位是否必选
        /// </summary>
        /// <param name="CALCCODE">计算基础CODE</param>
        /// <param name="MSRUNIT">计算基础单位</param>
        /// <returns></returns>
        public static string HasUnit(string CALCCODE, string MSRUNIT)
        {
            string returnStr = "0";
            try
            {
                string hasUnit = DataHelper.QueryValue("select HASUNIT from SQM_CALC_BASE where CALC_BASE='" + CALCCODE + "'") + "";
                if (hasUnit == "1" && String.IsNullOrEmpty(MSRUNIT))
                {
                    returnStr = "1";
                }
                else if (String.IsNullOrEmpty(hasUnit) && !String.IsNullOrEmpty(MSRUNIT))
                {
                    returnStr = "2";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnStr;
        }
        /// <summary>
        /// 判断计算基础数值长度及小数位
        /// </summary>
        /// <param name="calccode">计算基础CODE</param>
        /// <param name="dataval">计算基础数值</param>
        /// <returns></returns>
        public static string CheckData(string calccode, string dataval)
        {
            string checkVal = "";
            try
            {
                int datalen;
                int pointlen;
                int jc;
                double maxdata;
                string sql = @"select DATALEN,POINTLEN from SQM_CALC_BASE where CALC_BASE='" + calccode + "'";
                DataTable datadt = DataHelper.QueryDataTable(sql);
                if (datadt.Rows.Count > 0)
                {
                    Regex rexint = new Regex(@"^\d+$");
                    Regex rexpoint = new Regex(@"^\d+\.\d+$");
                    datalen = Convert.ToInt32(datadt.Rows[0]["DATALEN"].ToString() == "" ? "0" : datadt.Rows[0]["DATALEN"].ToString());
                    pointlen = Convert.ToInt32(datadt.Rows[0]["POINTLEN"].ToString() == "" ? "0" : datadt.Rows[0]["POINTLEN"].ToString());
                    if (pointlen == 0)
                    {
                        if (rexpoint.IsMatch(dataval))
                        {
                            checkVal = "3";//无小数位的数值输入了小数点
                        }
                        else if (dataval.Length > datalen)
                        {
                            checkVal = "1";//无小数位的长度超限
                        }
                    }
                    else
                    {
                        if (!rexint.IsMatch(dataval) && !rexpoint.IsMatch(dataval))
                        {
                            checkVal = "3";//有小数位的数值不是数字类型
                        }
                        else
                        {
                            jc = datalen - pointlen;
                            maxdata = Math.Pow(10, jc) - Math.Pow(0.1, pointlen);
                            if (maxdata < Convert.ToDouble(dataval))
                            {
                                checkVal = "2";//有小数位的长度超限
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return checkVal;
        }
    }
}
