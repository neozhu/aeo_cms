using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sap.Data.Hana;
using System.Reflection;

namespace Oncontrol3.Web
{
    public class HanaConectionHelper
    {
        public static string GenInsertSQL(object obj)
        {
            string colsName = "";
            string colsValues = "";
            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                //if (pi.Name.ToUpper() == "ID")          // 不插入自动增长列
                //    continue;

                if (pi.GetValue(obj, null) == null)     // null值不插入
                    continue;

                if (pi.CanWrite) //&& pi.Name.ToUpper() != "REQUESTID")
                {
                    //colsName += pi.Name + ",";
                    //colsValues += "'" + pi.GetValue(obj, null) + "',";
                    //colsName += "[" + pi.Name + "],";
                    colsName += pi.Name + ",";
                    if (pi.PropertyType.ToString().Contains("String") || pi.PropertyType.ToString().Contains("Date"))
                        colsValues += "'" + pi.GetValue(obj, null) + "',";
                    else
                        colsValues += pi.GetValue(obj, null) + ",";
                }
            }
            string sqlInsert = "INSERT INTO SAPABAP1." + obj.GetType().Name + " (" + colsName.TrimEnd(',') + ") VALUES (" + colsValues.TrimEnd(',') + ")";
            return sqlInsert;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public static List<Hashtable> GetHashtableList(string strSQL, string connstr)
        {
            String ConectionString = "";
            ConectionString = ConfigurationManager.AppSettings[connstr + "_CONNSTRING"];
            HanaConnection conn = new HanaConnection(ConectionString);
            conn.Open();
            HanaCommand cmd = new HanaCommand(strSQL, conn);
            HanaDataReader reader = cmd.ExecuteReader();
            List<Hashtable> list = DbReaderToHash(ref reader);
            reader.Close();
            conn.Close();
            return list;
        }


        private static List<Hashtable> DbReaderToHash(ref HanaDataReader reader)
        {

            var list = new List<Hashtable>();
            while (reader.Read())
            {
                var item = new Hashtable();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var value = reader[i];
                    item[name] = value;
                }
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 获取当前页数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SQLStr"></param>
        /// <param name="CountSQL"></param>
        /// <param name="connstr"></param>
        /// <param name="totalRows"></param>
        /// <param name="totalPages"></param>
        /// <param name="isLimit"></param>
        /// <returns></returns>
        public static List<Hashtable> LoadListWithPage(int PageIndex, int PageSize, string SQLStr, string CountSQL, string connstr, out int totalRows, out int totalPages, bool isLimit = true)
        {
            var list = new List<Hashtable>();
            List<Hashtable> countList = GetHashtableList(CountSQL, connstr);
            int totalRow = Convert.ToInt32(countList[0]["总计"].ToString());
            PageIndex = InitPage(totalRow, PageIndex, PageSize, out totalPages);
            string sql = "";
            int startPage = 0;
            int endPage = 0;
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }
            startPage = (PageIndex - 1) * PageSize;
            endPage = (PageIndex - 1) * PageSize + PageSize;
            if (isLimit)
            {
                sql = SQLStr + " LIMIT " + PageSize + " OFFSET " + startPage + "";
            }
            else
            {
                sql = SQLStr;
            }

            list = GetHashtableList(sql, connstr);
            totalRows = totalRow;
            return list;

        }

        public static int InitPage(int totalRows, int pageIndex, int pageSize, out int totalPages)
        {
            if (pageIndex <= 0) { pageIndex = 1; }
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }
            return pageIndex;
        }

        public static List<Hashtable> ExecuteReader(string sql, Hashtable param, string connstr)
        {
            String ConectionString = ConfigurationManager.AppSettings[connstr + "_CONNSTRING"];

            HanaConnection conn = new HanaConnection(ConectionString);
            conn.Open();
            HanaCommand cmd = new HanaCommand(sql, conn);

            foreach (string key in param.Keys)
            {
                HanaParameter p = cmd.CreateParameter();
                p.ParameterName = key;
                p.Value = param[key];
                cmd.Parameters.Add(p);
            }

            HanaDataReader reader = cmd.ExecuteReader();
            List<Hashtable> list = DbReaderToHash(ref reader);
            reader.Close();
            conn.Close();
            return list;
        }

        public static int ExecuteNonQuery(string sql, string connstr)
        {
            String ConectionString = ConfigurationManager.AppSettings[connstr + "_CONNSTRING"];

            HanaConnection conn = new HanaConnection(ConectionString);
            conn.Open();
            HanaCommand cmd = new HanaCommand(sql, conn);

            int rtn = cmd.ExecuteNonQuery();
            conn.Close();
            return rtn;
        }
    }
}