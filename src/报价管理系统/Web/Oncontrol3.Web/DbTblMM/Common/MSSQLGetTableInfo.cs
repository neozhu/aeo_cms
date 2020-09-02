using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aim.Data;
using Aim.Portal.Model;
using System.Data;
using Aim.Portal.Model;

namespace Aim.OnControl.Web.DbTblMM
{
    public class MSSQLGetTableInfo : ITableInfo
    {
        public SYSTBLMM GetTableInfo(string TblName, string DbNameOrOwner)
        {
            string sql = @"select C.name,create_date,modify_date,PTB.value Tbldesc from 
	                           sys.objects as C
	                           LEFT JOIN sys.extended_properties PTB
		                            ON PTB.class=1 
	                           AND C.[object_id]=PTB.major_id
                             where c.name='" + TblName + "'";

            if (!string.IsNullOrEmpty(DbNameOrOwner))
            {
                sql = "use " + DbNameOrOwner + ";" + sql;
            }
            EasyDictionary Edic = DataHelper.QueryDictList(sql).FirstOrDefault();
            if (Edic != null)
            {

                sql = "exec sp_spaceused N'" + TblName + "'";
                if (!string.IsNullOrEmpty(DbNameOrOwner))
                {
                    sql = "use " + DbNameOrOwner + ";" + sql;
                }
                DataTable dt = DataHelper.QueryDataTable(sql);

                SYSTBLMM ent = new SYSTBLMM();
                ent.TBLCODE = Edic["name"] + "";
                ent.TBLCOMMENT = Edic["Tbldesc"] + "";
                ent.TBLCREATETIME = DateTime.Parse(Edic["create_date"] + "");
                ent.TBLMODIFYTIME = DateTime.Parse(Edic["modify_date"] + "");
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        ent.TBLSPACE = (dt.Rows[0]["data"] + "").Replace("KB", "");
                    }
                    catch { }
                }
                return ent;
            }
            else
            {
                return null;
            }
        }


        public List<SYSTBLCLNSMM> GetAllTBLFiledInfo(string TblName, string DbNameOrOwner)
        {
            ///MS2005 以上版本
            string sql = @"SELECT 
	                        TableName=CASE WHEN C.column_id=1 THEN O.name ELSE N'' END,
	                        TableDesc=ISNULL(CASE WHEN C.column_id=1 THEN PTB.[value] END,N''),
	                        Column_id=C.column_id,ColumnName=C.name,
	                        PrimaryKey=ISNULL(IDX.PrimaryKey,N''),
	                        [IDENTITY]=CASE WHEN C.is_identity=1 THEN N'√'ELSE N'' END,
	                        Computed=CASE WHEN C.is_computed=1 THEN N'√'ELSE N'' END,
	                        Type=T.name,Length=C.max_length,
	                        Precision=C.precision,
	                        Scale=C.scale,
	                        NullAble=CASE WHEN C.is_nullable=1 THEN N'√'ELSE N'' END,
	                        [Default]=ISNULL(D.definition,N''),
	                        ColumnDesc=ISNULL(PFD.[value],N''),
	                        IndexName=ISNULL(IDX.IndexName,N''),
	                        IndexSort=ISNULL(IDX.Sort,N''),
	                        Create_Date=O.Create_Date,
	                        Modify_Date=O.Modify_date
                        FROM sys.columns C
                        INNER JOIN sys.objects O
                            ON C.[object_id]=O.[object_id]
	                        AND O.type='U'
	                        AND O.is_ms_shipped=0
                        INNER JOIN sys.types T
                            ON C.user_type_id=T.user_type_id
                        LEFT JOIN sys.default_constraints D
	                        ON C.[object_id]=D.parent_object_id
		                        AND C.column_id=D.parent_column_id
		                        AND C.default_object_id=D.[object_id]
                        LEFT JOIN sys.extended_properties PFD
	                        ON PFD.class=1 
		                        AND C.[object_id]=PFD.major_id 
		                        AND C.column_id=PFD.minor_id
                        -- AND PFD.name='Caption' 
                        LEFT JOIN sys.extended_properties PTB
	                        ON PTB.class=1 
		                        AND PTB.minor_id=0 
		                        AND C.[object_id]=PTB.major_id
                        -- AND PFD.name='Caption'  

                        LEFT JOIN -- 索引及主键信息
                        (
                        SELECT 
	                        IDXC.[object_id],IDXC.column_id,
	                        Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')
	                        WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END,
	                        PrimaryKey=CASE WHEN IDX.is_primary_key=1 THEN N'√'ELSE N'' END,
	                        IndexName=IDX.Name
                        FROM sys.indexes IDX
                        INNER JOIN sys.index_columns IDXC
	                        ON IDX.[object_id]=IDXC.[object_id] AND IDX.index_id=IDXC.index_id
                        LEFT JOIN sys.key_constraints KC
	                        ON IDX.[object_id]=KC.[parent_object_id] AND IDX.index_id=KC.unique_index_id
                        INNER JOIN 
                        (
	                        SELECT 
		                        [object_id], Column_id, index_id=MIN(index_id)
	                        FROM sys.index_columns
	                        GROUP BY [object_id], Column_id
                        ) IDXCUQ
	                        ON IDXC.[object_id]=IDXCUQ.[object_id]
		                        AND IDXC.Column_id=IDXCUQ.Column_id
	                        AND IDXC.index_id=IDXCUQ.index_id
                        ) IDX
                        ON C.[object_id]=IDX.[object_id] AND C.column_id=IDX.column_id
                         WHERE O.name=N'{0}'
                        ORDER BY O.name,C.column_id";
            sql = string.Format(sql, TblName);
            if (!string.IsNullOrEmpty(DbNameOrOwner))
            {
                sql = "use " + DbNameOrOwner + ";" + sql;
            }
            IList<EasyDictionary> list = DataHelper.QueryDictList(sql);
            List<SYSTBLCLNSMM> EntList = null;
            foreach (var v in list)
            {
                if (EntList == null) EntList = new List<SYSTBLCLNSMM>();
                SYSTBLCLNSMM cln = new SYSTBLCLNSMM();
                cln.CLNCODE = v["ColumnName"] + "";
                cln.CLNCOMMENT = v["ColumnDesc"] + "";
                cln.CLNLEN = v["Length"] + "";
                //  cln.CLNCREATETIME = DateTime.Parse(v["Create_Date"] + "");
                cln.CLNDATATYPE = v["Type"] + "";
                EntList.Add(cln);
            }
            return EntList;
        }

        public List<SYSTBLMM> GetAllTableObject(string DbNameOrOwner)
        {
            string sql = @"select C.name,create_date,modify_date,PTB.value Tbldesc from 
	                           sys.objects as C
	                           LEFT JOIN sys.extended_properties PTB
		                            ON PTB.class=1 
	                           AND C.[object_id]=PTB.major_id  ";
            if (!string.IsNullOrEmpty(DbNameOrOwner))
            {
                sql = "use " + DbNameOrOwner + ";" + sql;
            }
            IList<EasyDictionary> LDic = DataHelper.QueryDictList(sql);
            List<SYSTBLMM> LEnts = null;
            foreach (var item in LDic)
            {
                if (LEnts == null) LEnts = new List<SYSTBLMM>();
                SYSTBLMM Ent = new SYSTBLMM();
                Ent.TBLCODE = item["name"] + "";
                Ent.TBLCREATETIME = DateTime.Parse(item["create_date"] + "");
                Ent.TBLCOMMENT = item["Tbldesc"] + "";
                LEnts.Add(Ent);
            }
            return LEnts;
        }

    }
}