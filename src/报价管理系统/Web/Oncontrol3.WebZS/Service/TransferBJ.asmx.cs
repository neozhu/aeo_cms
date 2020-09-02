using Aim.Data;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Oncontrol3.Web.Service
{
    /// <summary>
    /// TransferBJ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class TransferBJ : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string Transfer(string fromuser, string touser, string UserName, string PassWord)
        //{
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    ResultVO result = new ResultVO();
        //    if (UserName == "transferbaojia" && PassWord == "60380CB15A78103A13E5EE01CD70FD08")
        //    {
        //        try
        //        {
        //            string sql = "";// @"update sqm_bj_main_basic a set a.affiliation='" + touser + "' where a.affiliation= '" + fromuser + "' and a.rid='" + rid + "'";
        //            // 插数
        //            DataHelper.ExecSql(sql);
        //            result.IsSuccess = true;
        //            result.Message = "转移成功！";
        //            result.Data = "";
        //            return js.Serialize(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            result.IsSuccess = false;
        //            result.Message = ex.Message;
        //            result.Data = "";
        //            return js.Serialize(result);
        //        }
        //    }
        //    else
        //    {
        //        result.IsSuccess = false;
        //        result.Message = "对不起，你没有权限访问报价删除接口！";
        //        result.Data = "";
        //        return js.Serialize(result);
        //    }
        //}
        [WebMethod]
        public string Transfer(string fromuser, string touser, string UserName, string PassWord, string rid)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ResultVO result = new ResultVO();
            if (UserName == "transferbaojia" && PassWord == "60380CB15A78103A13E5EE01CD70FD08")
            {
                try
                {
                    string sql = @"update sqm_bj_main_basic a set a.affiliation='" + touser + "' where a.affiliation= '" + fromuser + "' and a.rid='" + rid + "'";
                    // 插数
                    DataHelper.ExecSql(sql);
                    result.IsSuccess = true;
                    result.Message = "转移成功！";
                    result.Data = "";
                    return js.Serialize(result);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Data = "";
                    return js.Serialize(result);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "对不起，你没有权限访问报价删除接口！";
                result.Data = "";
                return js.Serialize(result);
            }
        }
    }
}
