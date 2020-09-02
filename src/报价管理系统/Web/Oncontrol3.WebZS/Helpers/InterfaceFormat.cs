using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    public static class InterfaceFormat
    {
        ///// <summary>
        ///// 销售易、报价系统状态转换方法
        ///// </summary>
        ///// <param name="status">状态编码</param>
        ///// <param name="direction">方向：Z:正向;N：逆向</param>
        ///// <returns></returns>
        public static string FormatStatusXSY(string status, string direction)
        {
            //销售易：3->已保存; 4->审批中; 5->审批通过; 6->审批退回; 7->已发送客户; 8->已提交TM; 9->作废
            //报价 : 0->已保存;1->审批中;2->审批通过;3->审批退回;4->已发送客户;5->已提交TM;6->作废
            string restatus = "";
            if (direction.ToUpper() == "Z")
            {
                switch (status)
                {
                    case "0":
                        restatus = "3";//已保存
                        break;
                    case "1":
                        restatus = "4";//审批中
                        break;
                    case "2":
                        restatus = "5";//审批通过
                        break;
                    case "3":
                        restatus = "6";//审批退回
                        break;
                    case "4":
                        restatus = "7";//已发送客户
                        break;
                    case "5":
                        restatus = "8";//已提交TM
                        break;
                    case "6":
                        restatus = "9";//作废
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case "3":
                        restatus = "0";//已保存
                        break;
                    case "4":
                        restatus = "1";//审批中
                        break;
                    case "5":
                        restatus = "2";//审批通过
                        break;
                    case "6":
                        restatus = "3";//审批退回
                        break;
                    case "7":
                        restatus = "4";//已发送客户
                        break;
                    case "8":
                        restatus = "5";//已提交TM
                        break;
                    case "9":
                        restatus = "6";//作废
                        break;
                }
            }
            return restatus;
        }
    }
}