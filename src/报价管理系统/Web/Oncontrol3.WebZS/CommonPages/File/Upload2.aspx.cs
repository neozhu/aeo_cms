using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Web.CommonPages.File
{
    public partial class Upload2 : System.Web.UI.Page
    {
        public string UploadServiceUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.Host.Contains("221.224.21.25"))
            {
                UploadServiceUrl = Aim.Common.ConfigurationHosting.SystemConfiguration.AppSettings["LinkFileUpload2"];
            }
            else if (Request.Url.Host.Contains("crm.feili.com"))
            {
                UploadServiceUrl = Aim.Common.ConfigurationHosting.SystemConfiguration.AppSettings["LinkFileUpload3"];
            }
            else
            {
                UploadServiceUrl = Aim.Common.ConfigurationHosting.SystemConfiguration.AppSettings["LinkFileUpload"];
            }
        }
    }
}