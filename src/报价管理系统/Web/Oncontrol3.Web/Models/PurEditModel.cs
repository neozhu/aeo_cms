using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Models
{
    public class PurEditModel
    {
        public string RID { get; set; }
        public string COSTRID { get; set; }
        public string S2id_autogen1 { get; set; }
        public string s2id_autogen1_search { get; set; }

        public string DJFSRID { get; set; }
        public string s2id_autogen2 { get; set; }

        public string s2id_autogen2_search { get; set; }
        public string GDZKEY { get; set; }

        public string s2id_autogen3 { get; set; }
        public string s2id_autogen3_search { get; set; }

        public string CALCTYPE { get; set; }
        public string CALCUNIT { get; set; }

        public string s2id_autogen4 { get; set; }
        public string s2id_autogen4_search { get; set; }

        public string CURRENCY { get; set; }
        public string MIN { get; set; }

        public string PURPRICE { get; set; }
        public string COSTPRICE { get; set; }

        public string MAXPRICE { get; set; }
        public string MINPRICE { get; set; }

        public string GUIDEPRICE { get; set; }
        public DateTime STARTDATE { get; set; }

        public DateTime ENDDATE { get; set; }
        public string s2id_autogen5 { get; set; }

        public string[] CUSTOMERSNO { get; set; }
        public string MEMO { get; set; }

        public string MODIFYUSER { get; set; }
    }
}