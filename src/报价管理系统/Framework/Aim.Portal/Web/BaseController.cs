using Aim.Data;
using Aim.Portal.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aim.Portal.Web
{
    public class BaseController : Controller
    {

        public HqlSearchCriterion SearchCriterion = new HqlSearchCriterion();
        public BaseController()
        {
        }
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.Request["page"] != null)
            {
                //自动分页
                SearchCriterion.AllowPaging = true;
                SearchCriterion.PageSize = int.Parse(requestContext.HttpContext.Request["rows"]);
                SearchCriterion.CurrentPageIndex = int.Parse(requestContext.HttpContext.Request["page"]);
            }
            else
            {
                SearchCriterion.AllowPaging = false;
            }
            if (requestContext.HttpContext.Request["start"] != null)
            {
                //自动分页
                SearchCriterion.AllowPaging = true;
                SearchCriterion.PageSize = int.Parse(requestContext.HttpContext.Request["length"]);
                SearchCriterion.CurrentPageIndex = int.Parse(requestContext.HttpContext.Request["start"]) / SearchCriterion.PageSize+1;
            }
            else
            {
                SearchCriterion.AllowPaging = false;
            }
            if (requestContext.HttpContext.Request["sort"] != null)
            {
                //排序
                SearchCriterion.AutoOrder = false;
                SearchCriterion.SetOrder(requestContext.HttpContext.Request["sort"], requestContext.HttpContext.Request["order"] == "asc" ? true : false);
            }
            return base.BeginExecute(requestContext, callback, state);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            // 错误日志编写    
            string controllerNamer = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            string exception = filterContext.Exception.ToString();
            Exception ex = filterContext.Exception;

            /*--写事件日志开始--*/

            if (ex is MessageException)
            {
                // MessageException mex = ex as MessageException;
            }
            else
            {
                string message = ex.Message + ex.InnerException != null ? "" : ex.InnerException.Message == null ? "" : ex.InnerException.Message;
                string strace = ex.StackTrace + ex.InnerException != null ? "" : ex.InnerException.StackTrace == null ? "" : ex.InnerException.StackTrace;
                LogService.Log(String.Format("Message:{0}\r\n\r\nStackTrace:{1}", message, strace), LogTypeEnum.Error);

                /*--写事件日志结束--*/
            }
            // 执行基类中的OnException    
            base.OnException(filterContext);
        }

        /// <summary>
        /// 获取目标数据表单源数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetTargetData<T>() where T : EntityBase<T>
        {
            T data = default(T);
            if (Request.Form[EntityBase<T>.PrimaryKeyName] != null && Request.Form[EntityBase<T>.PrimaryKeyName].Trim() != ""
                || Request.RequestContext.RouteData.Values["id"] != null && Request.RequestContext.RouteData.Values["id"].ToString() != "")
            {
                object primaryKeyValue = Request.Form[EntityBase<T>.PrimaryKeyName] == null ? Request.RequestContext.RouteData.Values["id"].ToString() : Request.Form[EntityBase<T>.PrimaryKeyName];

                data = EntityBase<T>.AutoFind(primaryKeyValue);
            }

            return data;
        }

        /// <summary>
        /// 融合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected T GetMergedData<T>(T data) where T : EntityBase<T>, new()
        {
            T target = default(T);

            target = this.GetTargetData<T>();

            T postedData = data;

            if (target != null && postedData != null)
            {
                target = DataHelper.MergeData(target, postedData, Request.Form.Keys);
            }

            return target;
        }

        public IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = Convert.ToInt32(DataHelper.QueryValue("select count(1) from (" + sql + ") t"));
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "NAME";
            string asc = search.Orders.Count <= 0 || search.Orders[0].Ascending ? " asc" : " desc";

            string pagsql = @"select * from(
                    select rownum r,t.* from (
                    select * from ({2})  order by  {0} {1}
                    )t)
                    where r between {3} and {4}";

            pagsql = string.Format(pagsql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pagsql);
            return dicts;
        }

    }
}
