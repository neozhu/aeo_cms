using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Helpers
{
    public class CreateEXTDetails
    {
        public string INFONAME { get; set; }

        public string INFOCODE { get; set; }

        public string ServiceType { get; set; }

        public string ServiceTypeCode { get; set; }

        public SelectList CheckBoxList { get; set; }

        public string selectedvalues{get;set;}

        public string FIELDNAME { get; set; }

        public string MDMKEY { get; set; }

        public string RID { get; set; }

        public string SORD { get; set; }
    }
}