using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web.UI;
using NHibernate.Criterion;

namespace CRM.Web
{
    public partial class FrmCompanySel : BaseListPage
    {
        public FrmCompanySel()
        {
            base.IsCheckLogon = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCriterion.PageSize = 100;
            if (!SearchCriterion.Orders.Exists(en => en.PropertyName == "Code"))
                SearchCriterion.Orders.Add(new OrderCriterionItem("Code", true));

            SysGroup[] ents = SysGroup.FindAll(SearchCriterion, Expression.Sql("corpCode is not null"));
            this.PageState.Add("DataList", ents);
        }
    }
}

