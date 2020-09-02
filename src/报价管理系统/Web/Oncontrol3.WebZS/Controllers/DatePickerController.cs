using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using BaseDLL;
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
    public class DatePickerController : BaseController
    {
        class SearchBP
        {
            public string Search_data { get; set; }
        }
        private DataTable GetPageDataOracle(Pagination pagination, string tempsql, string orderby, bool asc = true)
        {
            //DataTable dtcount= TeraDataHelper.getTable("SELECT COUNT(1) AS totalcount FROM (" + tempsql + ") t");
            //pagination.records = dtcount.Rows.Count > 0 ? int.Parse(dtcount.Rows[0]["totalcount"].ToString()) : 0;
            pagination.records = 100;
            string sort = asc ? "ASC" : "DESC";
            string sql_page = "{0} QUALIFY ROW_NUMBER() over (ORDER BY {1} {2}) between ({3}) and ({4})";
            sql_page = string.Format(sql_page, tempsql, orderby, sort, (pagination.page - 1) * pagination.rows + 1, (pagination.page - 1) * pagination.rows + pagination.rows);
            return TeraDataHelper.getTable(sql_page);
        }

        #region 订购方代码
        public ActionResult Index()
        {
            ViewData["p_key"] = Request.Params["p_key"];
            ViewData["has_id"] = Request.Params["has_id"];
            return View("Index");
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Lists(Pagination pagination, string queryJson)
        {
            SearchBP sbp = new SearchBP();
            if (!string.IsNullOrEmpty(queryJson))
            {
                sbp = JsonHelper.GetObject<SearchBP>(queryJson);
            }
            string has_id = Request.QueryString["has_id"];
            string sql = string.Format("SELECT Client_Id,Client_CN_Name,Client_EN_Name FROM PV_PMART.DIM_CLIENT_BASE_INFO WHERE (Client_Id LIKE '%{0}%' OR Client_CN_Name LIKE '%{0}%' OR Client_EN_Name LIKE '%{0}%')", sbp.Search_data);
            if (!String.IsNullOrEmpty(has_id))
            {
                has_id = has_id.Replace(",","','");
                sql = sql + " and Client_Id not in('" + has_id + "')";
            }
            DataTable dtsearch = GetPageDataOracle(pagination, sql, "Client_Id");
            var data = new
            {
                rows = dtsearch,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(JsonHelper.GetJsonString(data));
        }
        #endregion

        #region 代运订单类型代码
        public ActionResult Traffic_OrdType() 
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Traffic_OrdType_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Traffic_Order_Type_Cd,Traffic_Order_Type_Name FROM PV_PMART.DIM_TRAFFIC_ORDER_TYPE WHERE (Traffic_Order_Type_Cd LIKE '%{0}%' OR Traffic_Order_Type_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Traffic_Order_Type_Cd not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Traffic_Order_Type_Cd");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
        #endregion

        #region 销售组织代码
         public ActionResult Sale_Organ()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Sale_Organ_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Agent_Run_Organ_Id,Agent_Run_Organ_Name FROM PV_PMART.DIM_AGENT_RUN_ORGAN_INFO WHERE (Agent_Run_Organ_Id LIKE '%{0}%' OR Agent_Run_Organ_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Agent_Run_Organ_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Agent_Run_Organ_Id");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
        #endregion

        #region 统计日期
         public ActionResult Stats_Dt()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Stats_Dt_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT DISTINCT SUBSTR(CAST(Calendar_dt AS VARCHAR(20)),1,6) YM_DT FROM PV_PMART.DIM_DATE Where YM_DT LIKE '%{0}%'", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and YM_DT not in('" + has_id + "')";
             }
             DataTable dtsearch = TeraDataHelper.getTable(sql + " ORDER BY YM_DT ASC");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 统计年份
         public ActionResult Stats_Year()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Stats_Year_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT DISTINCT T_YEAR FROM PV_PMART.DIM_DATE Where CAST(T_YEAR AS VARCHAR(10)) LIKE '%{0}%'", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and T_YEAR not in('" + has_id + "')";
             }
             DataTable dtsearch = TeraDataHelper.getTable(sql + " ORDER BY T_YEAR ASC");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 代运订单编号
         public ActionResult Traffic_OrdCode()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Traffic_OrdCode_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("Select Traffic_Order_Id From PV_PMART.MRT_ORDER_SALE_GP_MTHLY Where Run_Dt = current_date-2 and Traffic_Order_Id LIKE '%{0}%'", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Traffic_Order_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = TeraDataHelper.getTable(sql + " ORDER BY Traffic_Order_Id ASC");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 货运订单类型代码
         public ActionResult Freight_OrderType()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Freight_OrderType_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Freight_Order_Type_Cd,Freight_Order_Type_Name FROM PV_PDATA.CDE_FREIGHT_ORDER_TYPE WHERE (Freight_Order_Type_Cd LIKE '%{0}%' OR Freight_Order_Type_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Freight_Order_Type_Cd not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Freight_Order_Type_Cd");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region FO/FOO/FB
         public ActionResult Freight_OrdCode()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Freight_OrdCode_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("Select DISTINCT Freight_Order_Id From PV_PMART.MRT_ROLLING_ARAP_DAILY Where Run_Dt = current_date-2 and Freight_Order_Id LIKE '%{0}%'", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Freight_Order_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = TeraDataHelper.getTable(sql + " ORDER BY Freight_Order_Id ASC");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 服务产品编号
         public ActionResult Service_ProCode()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Service_ProCode_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Service_Product_Id,Service_Product_Name FROM PV_PMART.DIM_SERVICE_PRODUCT_INFO WHERE (Service_Product_Id LIKE '%{0}%' OR Service_Product_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Service_Product_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Service_Product_Id");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 财务公司编号
         public ActionResult Fin_CompanyCode()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Fin_CompanyCode_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Fin_Company_Id,Fin_Company_Name FROM PV_PMART.DIM_COMPANY_BASE_INFO WHERE (Fin_Company_Id LIKE '%{0}%' OR Fin_Company_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Fin_Company_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Fin_Company_Id");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 服务产品类型代码
         public ActionResult Service_ProductType()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Service_ProductType_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Service_Product_Type_Cd,Service_Product_Type_Name FROM PV_PMART.DIM_SERVICE_PRODUCT_TYPE WHERE (Service_Product_Type_Cd LIKE '%{0}%' OR Service_Product_Type_Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Service_Product_Type_Cd not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Service_Product_Type_Cd");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 应收、应付审核员
         public ActionResult Level_Auditor()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult Level_Auditor_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT Employee_Id,Name FROM PV_PMART.DIM_EMPLOYEE_BASE_INFO WHERE (Employee_Id LIKE '%{0}%' OR Name LIKE '%{0}%')", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and Employee_Id not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "Employee_Id");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion

        #region 发票号码
         public ActionResult JS_InvoiceId()
         {
             ViewData["p_key"] = Request.Params["p_key"];
             ViewData["has_id"] = Request.Params["has_id"];
             return View();
         }
         [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
         public ActionResult JS_InvoiceId_Lists(Pagination pagination, string queryJson)
         {
             SearchBP sbp = new SearchBP();
             if (!string.IsNullOrEmpty(queryJson))
             {
                 sbp = JsonHelper.GetObject<SearchBP>(queryJson);
             }
             string has_id = Request.QueryString["has_id"];
             string sql = string.Format("SELECT A.Reference_Invoice_Num FROM( SELECT DISTINCT Reference_Invoice_Num FROM PV_PMART.COM_ACC_VOUCHER_LINE_DAILY UNION SELECT '已收'  as Reference_Invoice_Num FROM PV_PMART.DIM_INVOICE_CATEGORY  ) A WHERE A.Reference_Invoice_Num LIKE '%{0}%' ", sbp.Search_data);
             if (!String.IsNullOrEmpty(has_id))
             {
                 has_id = has_id.Replace(",", "','");
                 sql = sql + " and A.Reference_Invoice_Num not in('" + has_id + "')";
             }
             DataTable dtsearch = GetPageDataOracle(pagination, sql, "A.Reference_Invoice_Num");
             var data = new
             {
                 rows = dtsearch,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(JsonHelper.GetJsonString(data));
         }
         #endregion
    }
}
