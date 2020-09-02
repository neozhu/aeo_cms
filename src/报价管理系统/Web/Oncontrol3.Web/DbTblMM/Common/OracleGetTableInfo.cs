using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Model;

namespace Aim.OnControl.Web.DbTblMM
{
    public class OracleGetTableInfo : ITableInfo
    {

        public SYSTBLMM GetTableInfo(string TblName, string DbNameOrOwner)
        {
            string sql = @"select 
                             TABLE_NAME, OWNER,t.TABLESPACE_NAME,t.READ_ONLY,t.TABLE_LOCK, t.PARTITIONED,t.NUM_ROWS,t.BLOCKS,t.AVG_ROW_LEN
                             from SYS.ALL_TABLES t where 1=1  ";
            if (!string.IsNullOrEmpty(DbNameOrOwner))
            {
                sql += " and owner='" + DbNameOrOwner + "' and TABLE_NAME='" + TblName + "' ";
            }

            EasyDictionary Edic = DataHelper.QueryDictList(sql).FirstOrDefault();
            if (Edic != null)
            {
                SYSTBLMM Ent = new SYSTBLMM();
                Ent.TBLCODE = Edic["TABLE_NAME"] + "";
                Ent.TBLSPACE = Edic["TABLESPACE_NAME"] + "";
                Ent.TBLTYPE = Edic["PARTITIONED"] + "";   //是否分区
                Ent.TBLOWNER = Edic["OWNER"] + "";
                return Ent;
            }
            else
            {
                return null;
            }
        }

        public List<SYSTBLCLNSMM> GetAllTBLFiledInfo(string TblName, string DbNameOrOwner)
        {
            string sql = @" SELECT t.TABLE_NAME,t.COLUMN_NAME,t.DATA_TYPE,t.DATA_LENGTH,t.NULLABLE,t.HIDDEN_COLUMN
                            FROM user_tab_cols t
                            WHERE table_name = '" + TblName + "' ";

            IList<EasyDictionary> DicList = DataHelper.QueryDictList(sql);
            List<SYSTBLCLNSMM> LEnts = null;
            foreach (var item in DicList)
            {
                if (LEnts == null) LEnts = new List<SYSTBLCLNSMM>();
                SYSTBLCLNSMM Ent = new SYSTBLCLNSMM();
                Ent.CLNCODE = item["COLUMN_NAME"] + "";
                Ent.CLNDATATYPE = item["DATA_TYPE"] + "";
                Ent.CLNLEN = item["DATA_LENGTH"] + "";
                LEnts.Add(Ent);
            }
            return LEnts;
        }

        public List<SYSTBLMM> GetAllTableObject(string DbNameOrOwner)
        {
            string sql = @"select 
                             TABLE_NAME, OWNER,t.TABLESPACE_NAME,t.READ_ONLY,t.TABLE_LOCK, t.PARTITIONED,t.NUM_ROWS,t.BLOCKS,t.AVG_ROW_LEN
                             from SYS.ALL_TABLES t ";
            if (!string.IsNullOrEmpty(DbNameOrOwner))
            {
                sql = sql + " where owner='" + DbNameOrOwner + "'";
            }
            IList<EasyDictionary> DicList = DataHelper.QueryDictList(sql);
            List<SYSTBLMM> LEnts = null;
            foreach (var item in DicList)
            {
                if (LEnts == null) LEnts = new List<SYSTBLMM>();
                SYSTBLMM Ent = new SYSTBLMM();
                Ent.TBLCODE = item["TABLE_NAME"] + "";
                Ent.TBLSPACE = item["TABLESPACE_NAME"] + "";
                Ent.TBLTYPE = item["PARTITIONED"] + "";   //是否分区
                Ent.TBLOWNER = item["OWNER"] + "";
                LEnts.Add(Ent);
            }
            return LEnts;
        }
    }
}