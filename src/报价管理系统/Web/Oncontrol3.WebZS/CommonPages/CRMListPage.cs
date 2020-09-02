using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aim;
using Newtonsoft.Json;
using Aim.Common;
using Aim.Common.Authentication;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web.UI;

namespace CRM.Web
{
    public class CRMListPage : CRMBasePage
    {
        #region 私有成员

        #endregion

        #region 属性

        /// <summary>
        /// 是否允许分页
        /// </summary>
        public bool AllowPaging
        {
            get { return SearchCriterion.AllowPaging; }
            set { SearchCriterion.AllowPaging = value; }
        }

        /// <summary>
        /// 是否为订阅连接
        /// </summary>
        public bool IsDirectQuery
        {
            get
            {
                return !String.IsNullOrEmpty(RequestData.Get<string>("mode")) &&
                       RequestData.Get<string>("mode").Trim().ToLower() == "subs";
            }
        }
        /// <summary>
        /// 订阅查询条件对象
        /// </summary>
        public List<SubscribeQuery> SubscribeQuerys { get; set; }

        #endregion

        #region 构造函数

        public CRMListPage()
        {
            SearchCriterion.AllowPaging = true;
            SearchCriterion.GetRecordCount = true;
        }

        #endregion

        #region 事件


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.IsDirectQuery)
            {
                List<SubscribeQuery> sqs = new List<SubscribeQuery>();
                string subsStr = RequestData.Get<string>("subs");
                try
                {
                    //List<object> entObjList = JsonHelper.GetObject<List<object>>(subsStr);
                    //List<SubscribeQuery> ents = entStrings.Select(tent => JsonHelper.GetObject<SubscribeQuery>(tent) as SubscribeQuery).ToList();
                    List<SubscribeQuery> ents = JsonHelper.GetObject<List<SubscribeQuery>>(subsStr);
                    SubscribeQuerys = ents;

                    //设置为初始查询条件
                    //SearchCriterion.SetSearch();
                    foreach (SubscribeQuery item in SubscribeQuerys)
                    {
                        SearchCriterion.SetSearch(item.id, item.value, SearchModeEnum.Like);
                    }

                }
                catch (Exception)
                {
                }
            }
        }

        protected override void Page_PreRender(object sender, EventArgs e)
        {
            PageState.Add(SearchCriterionStateKey, SearchCriterion);

            base.Page_PreRender(sender, e);
        }

        #endregion
    }

    public class SubscribeQuery
    {
        public string id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
}
