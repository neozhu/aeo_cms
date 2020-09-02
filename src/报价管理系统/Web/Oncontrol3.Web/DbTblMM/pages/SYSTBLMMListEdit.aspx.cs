using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Portal.Model;
using Aim.OnControl.Web.DbTblMM;

namespace Aim.OnControl.Web
{
    public partial class SYSTBLMMListEdit : BasePage
    {
        private IList<SYSTBLMM> ents = null;
        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            SYSTBLMM ent = null;
            switch (this.RequestActionString)
            {

                case "batchsave":
                    DoBatchSave();
                    break;
                case "reflash":
                    GetTableInfo();
                    break;
                case "afterEdit":
                    {
                        string recJson = RequestData.Get("recJson") + "";
                        SYSTBLMM TblEnt = JsonHelper.GetObject<SYSTBLMM>(recJson) as SYSTBLMM;
                        if (TblEnt != null)
                        {
                            TblEnt.Update();
                            this.PageState.Add("state", 1);
                        }
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取表的信息
        /// </summary>
        private void GetTableInfo()
        {
            DbTblMM.DbTypeEnum Enum = DbTblMM.DbTypeEnum.oralce;
            try
            {
                string verifSQL = "select 'T' T; ";
                DataHelper.QueryValue(verifSQL);
                Enum = DbTblMM.DbTypeEnum.mssql;
            }
            catch
            {
                Enum = DbTblMM.DbTypeEnum.oralce;
            }
            finally
            {
                this.PageState.Add("DbTypeEnum", Enum.ToString());
                OracleTbl(Enum);
                this.PageState.Add("statue", "1");
            }
        }


        private void OracleTbl(DbTblMM.DbTypeEnum Enum)
        {
            ITableInfo ITbl = null;
            string DbNameOrOwner = string.Empty;

            if (Enum == DbTblMM.DbTypeEnum.oralce)
            {
                ITbl = new OracleGetTableInfo();
                DbNameOrOwner = "ONCONTROL";
            }
            else if (Enum == DbTblMM.DbTypeEnum.mssql)
            {
                ITbl = new MSSQLGetTableInfo();
                DbNameOrOwner = "";  //数据库名
            }

            IList<SYSTBLMM> OriEnts = SYSTBLMM.FindAll();
            IList<SYSTBLCLNSMM> OriClnEnts = SYSTBLCLNSMM.FindAll();

            List<SYSTBLMM> List = ITbl.GetAllTableObject(DbNameOrOwner);
            foreach (var item in List)
            {
                string guid = Guid.NewGuid().ToString();
                if (OriEnts.Where(ten => ten.TBLCODE == item.TBLCODE).Count() > 0) { }
                else item.DoCreate();

                List<SYSTBLCLNSMM> Clns = ITbl.GetAllTBLFiledInfo(item.TBLCODE, DbNameOrOwner);
                foreach (var v in Clns)
                {
                    if (OriClnEnts.Where(ten => ten.CLNCODE == v.CLNCODE).Count() > 0) { }
                    else
                    {
                        v.REFTBLKEY = item.ID;
                        v.REFTBLCODE = item.TBLCODE;
                        v.DoCreate();
                    }
                }

            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        private void DoSelect()
        {
            ents = SYSTBLMM.FindAll(SearchCriterion);
            this.PageState.Add("SYSTBLMMList", ents);
        }

        /// <summary>
        /// 批量保存
        /// </summary>
        [ActiveRecordTransaction]
        private void DoBatchSave()
        {
            IList<string> entStrList = RequestData.GetList<string>("data");

            if (entStrList != null && entStrList.Count > 0)
            {
                IList<SYSTBLMM> ents = entStrList.Select(tent => JsonHelper.GetObject<SYSTBLMM>(tent) as SYSTBLMM).ToList();

                foreach (SYSTBLMM ent in ents)
                {
                    if (ent != null)
                    {
                        SYSTBLMM tent = ent;

                        if (String.IsNullOrEmpty(tent.ID))
                        {
                            tent.CREATEID = UserInfo.UserID;
                            tent.CREATENAME = UserInfo.Name;
                        }
                        else
                        {
                            tent = DataHelper.MergeData(SYSTBLMM.Find(tent.ID), tent);
                        }

                        tent.DoSave();
                    }
                }
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        [ActiveRecordTransaction]
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");

            if (idList != null && idList.Count > 0)
            {
                SYSTBLMM.DoBatchDelete(idList.ToArray());
            }
        }

        #endregion
    }
}

