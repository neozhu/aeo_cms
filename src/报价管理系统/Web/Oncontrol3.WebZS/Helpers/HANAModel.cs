using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    public class Z1T_QO_TRQROT
    {
        public string TRQ_TYPE { get; set; }
        public string TRQ_ID { get; set; }
        public DateTime? ORDER_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string SALES_ORG_ID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
        public string RID { get; set; }
        public string Status { get; set; }
        public string Memo { get; set; }
        public string MANDT { get; set; }
        public string DB_KEY { get; set; }
    }

    public class Z1T_QO_TRQITM
    {
        public string TRQ_ID { get; set; }
        public string TOR_ID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
        public string RID { get; set; }
        public string Status { get; set; }
        public string Memo { get; set; }
        public string MANDT { get; set; }
        public string DB_KEY { get; set; }
        public string PARENT_KEY { get; set; }
        public string ITEM_ID { get; set; }
        public string ITEM_TYPE { get; set; }
        public string TRANSSRVREQ_CODE { get; set; }
    }
}