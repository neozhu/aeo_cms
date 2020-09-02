using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Models
{
    
    public class PriceSubmitModel
    {
        //   // var BJNAME = $(this).closest('tr').find('td').eq(1).text();//报价名称
        //    //var ORGNAME = $(this).closest('tr').find('td').eq(2).text();//报价组织
        //    //var BPNAME = $(this).closest('tr').find('td').eq(3).text();//客户
        //    var ZVER = $(this).closest('tr').find('td').eq(4).text();//最新版本
        //    //var PRODUCT_NAME = $(this).closest('tr').find('td').eq(5).text();//产品
        //   // var CREATEUSER = $(this).closest('tr').find('td').eq(6).text();//销售员
        //    var FWA = $(this).closest('tr').find('td').eq(7).text();//FWA号
        //    var ITEMNO = $(this).closest('tr').find('td').eq(8).text();// 项目号
        //   // var MODIFYTIME = $(this).closest('tr').find('td').eq(9).text();//版本更新日期    
        //要更新的三个字段  CONDITION,JXJC,STAGETYPE
        public string MRID { get; set; }
        public string ZVER { get; set; }
        public string FWA { get; set; }
        public string ITEMNO { get; set; }
        public string CONDITION { get; set; }//前提条件
        public string JXJC { get; set; }//建议解析基础
        public string STAGETYPE { get; set; }//阶段类别
    }
}