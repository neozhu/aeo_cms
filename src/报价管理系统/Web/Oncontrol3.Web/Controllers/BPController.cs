using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return View("Index");
        }
        private DataTable GetPageDataOracle(Pagination pagination, string tempsql, string orderby, bool asc = true)
        {
            pagination.records = Convert.ToInt32(DataHelper.QueryValue<decimal>("SELECT COUNT(1) FROM (" + tempsql + ") t"));
            string sort = asc ? "ASC" : "DESC";
            string sql_page = "WITH DATASET AS( SELECT A.*,ROWNUM AS RN FROM ({0}) A ORDER BY {1} {2}) SELECT * FROM DATASET  WHERE RN BETWEEN {3} AND {4}";
            sql_page = string.Format(sql_page, tempsql, orderby, sort, (pagination.page - 1) * pagination.rows + 1, (pagination.page - 1) * pagination.rows + pagination.rows);
            return DataHelper.QueryDataTable(sql_page);
        }

        class SearchBP
        {
            public string BPKey { get; set; }
            public string BPName { get; set; }
        }

         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists(Pagination pagination, string queryJson)
        {
            SearchBP sbp = new SearchBP();
            if (!string.IsNullOrEmpty(queryJson))
            {
                sbp = JsonHelper.GetObject<SearchBP>(queryJson);
            }
            string sql = string.Format("SELECT * FROM MDM_BP WHERE (BPKey LIKE '%{0}%' AND BPName LIKE '%{1}%') ORDER BY BPKey ASC", sbp.BPKey, sbp.BPName);

            DataTable dtsearch = GetPageDataOracle(pagination, sql, "BPKey");

            var data = new
            {
                rows = dtsearch,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(JsonHelper.GetJsonString(data));
        }
    }
}
