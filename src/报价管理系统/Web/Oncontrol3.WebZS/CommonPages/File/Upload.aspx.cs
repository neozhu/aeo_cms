using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Utilities;

using System.Xml.Linq;
using Aim.CacheManager;
using System.Web.Caching;


namespace Aim.Examining.Web.CommonPages.File
{
    public partial class Upload : System.Web.UI.Page
    {

        public string UploadServiceUrl
        {
            get
            {
                return ViewState["ploadServiceUrl"].ToString();
            }
            set
            {
                ViewState["ploadServiceUrl"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            UploadServiceUrl = System.Configuration.ConfigurationManager.AppSettings["LinkFileUpload"].ToString();

        }
    }
 }