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
    /// DeleteBJ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DeleteBJ : System.Web.Services.WebService
    {
        [WebMethod]
        public string DeleteBj(string keyvalue, string UserName, string PassWord)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ResultVO result = new ResultVO();
            if (UserName == "deletebaojia" && PassWord == "60380CB15A78103A13E5EE01CD70FD08")
            {
                
                string[] staArr = { "1", "2", "3", "4", "5" };
                try
                {
                    DataTable staDt = DataHelper.QueryDataTable("select ZVER,STATUS from SQM_BJ_VER where MRID='" + keyvalue + "' order by STATUS desc");
                    foreach (DataRow dr in staDt.Rows)
                    {
                        if (staArr.Contains(dr["STATUS"].ToString()))
                        {
                            result.IsSuccess = false;
                            result.Message = "删除失败，存在已提交审批的报价！";
                            result.Data = "";
                            return js.Serialize(result);
                        }
                        else
                        {
                            string fwa = DataHelper.QueryValue(string.Format("select * from(select FWA from SQM_FWA_REF where mrid = '{0}' and ZVER='{1}' order by CREATETIME desc) where rownum = 1", keyvalue, dr["ZVER"].ToString())) + "";
                            if (!String.IsNullOrEmpty(fwa))
                            {
                                result.IsSuccess = false;
                                result.Message = "删除失败，存在已提交审批的报价！";
                                result.Data = "";
                                return js.Serialize(result);
                            }
                        }
                    }

                    string REFSql = string.Format("SELECT * FROM SQM_BJ_MAIN_BASIC WHERE RID='{0}'", keyvalue);
                    DataTable dt = DataHelper.QueryDataTable(REFSql);
                    if (dt.Rows.Count > 0)
                    {
                        List<string> sqllist = new List<string>();
                        sqllist.Add("delete from SQM_BJ_MAIN_BASIC where RID='" + keyvalue + "'");//报价主表
                        sqllist.Add("delete from SQM_BJ_VER where MRID='" + keyvalue + "'");//报价版本表
                        sqllist.Add("delete from SQM_BJ_PSF where MRID='" + keyvalue + "'");//报价PSF表
                        sqllist.Add("delete from SQM_BJ_BP where MRID='" + keyvalue + "'");//BP客户表
                        sqllist.Add("delete from SQM_BJ_BIZ where MRID='" + keyvalue + "'");//商机表
                        sqllist.Add("delete from SQM_BJ_ORG where MRID='" + keyvalue + "'");//组织表
                        string sql = string.Join(";", sqllist.ToArray());
                        sql = "begin " + sql + ";end;";
                        // 插数
                        DataHelper.ExecSql(sql);
                        result.IsSuccess = true;
                        result.Message = "删除成功！";
                        result.Data = "";
                        return js.Serialize(result);
                    }
                    else {
                        result.IsSuccess = false;
                        result.Message = "没有找到对应的报价信息！";
                        result.Data = "";
                        return js.Serialize(result);
                    }
                    
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
