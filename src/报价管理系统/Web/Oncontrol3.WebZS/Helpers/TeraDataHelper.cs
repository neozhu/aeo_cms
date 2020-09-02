using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Teradata.Client.Provider;

namespace BaseDLL
{
    public static class TeraDataHelper
    {
        public static string TDConnstring = ConfigurationManager.AppSettings["Teradata_Connect_String"];
        public static int CommandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"]);
        //public static string EDWID = ConfigurationManager.AppSettings["EDWID"];

        /// <summary>
        /// TD中查询sql
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <returns>返回DataRow</returns>
        public static DataSet getRows(string sqlString)
        {
            DataSet ds = new DataSet();
            try
            {
                TdDataAdapter adapter = new TdDataAdapter(sqlString, TDConnstring);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //log.writeLog("执行sql 报错,报错信息为：" + ex.Message + ", 报错语句为：‘" + sqlString + "’", "失败");
                //MessageBox.Show(ex.Message);
                //throw;
            }
            return ds;
        }

        /// <summary>

        /// 获取TD的数据

        /// </summary>

        /// <param name="sqlstr">sql语句：查询语句</param>

        /// <returns>返回数据表 DataTable</returns>
        public static DataTable getTable(string sqlStr)
        {
            TdConnection con = new TdConnection();
            DataTable dataTable = new DataTable();
            con.ConnectionString = TDConnstring;
            try
            {
                TdCommand cmd = con.CreateCommand();
                cmd.CommandTimeout = CommandTimeout;
                cmd.CommandText = sqlStr;

                // Create the TdDataAdapter object. It retrieves the data from database and fill a single data table 
                // with in a dataset. It also capable of reconciling the changes to database.                    
                TdDataAdapter adapter = new TdDataAdapter();
                adapter.ReturnProviderSpecificTypes = true;
                adapter.SelectCommand = cmd;


                // Create a DataTable object and it represents one table of in-memory data.               

                adapter.ReturnProviderSpecificTypes = true;
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                //log.writeLog("执行sql 报错,报错信息为：" + ex.Message + ", 报错语句为：‘" + sqlStr + "’", "失败");
                //MessageBox.Show(ex.Message);
                //throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dataTable;
        }

        public static void executeTD_Num(string sqlString)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                return;
            }
            try
            {
                using (TdConnection cn = new TdConnection(TDConnstring))
                {
                    cn.Open();
                    TdCommand cmd = cn.CreateCommand();
                    cmd.CommandTimeout = 20000;
                    cmd.CommandText = sqlString;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

                //log.writeLog("执行sql 报错,报错信息为：" + ex.Message + ", 报错语句为：‘" + sqlString + "’", "失败");
                
            }

        }
    }
}
