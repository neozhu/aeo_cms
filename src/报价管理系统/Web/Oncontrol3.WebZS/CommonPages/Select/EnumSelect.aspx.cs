using System;
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

namespace CRM.Web.CommonPages.Select
{
    public partial class EnumSelect : BaseListPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestActionString == "batchdelete")
            {

            }
            else
            {
                DoSelect();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void DoSelect()
        {
            string CompanyId = RequestData.Get<string>("CompanyId");
            if (!string.IsNullOrEmpty(CompanyId))
            {
                SysEnumeration sysenum = SysEnumeration.FindAllByProperty("Code", Request.QueryString["EnumKey"]).FirstOrDefault();
                this.PageState.Add("P_SupplierList", SysEnumeration.FindAll(Expression.Sql("ParentId='" + sysenum.EnumerationID + "' and CompanyId='" + CompanyId + "'")));
            }
            else
            {
                this.PageState.Add("P_SupplierList", SysEnumeration.GetEnumDictList(Request.QueryString["EnumKey"]));
            }
        }
    }
}


