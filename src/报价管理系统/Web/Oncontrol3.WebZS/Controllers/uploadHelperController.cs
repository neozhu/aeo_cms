using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.QDM;
using Com.Feiliks.MDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oncontrol3.Web.ServiceReference1;
using Com.Feiliks.QDM.Model;
using System.Collections;
using System.IO;
using System.Web.Security;

namespace Oncontrol3.Web.Controllers
{
    public class uploadHelperController : BaseController
    {
        public uploadHelperController() { }
        /// <summary>
        /// 报价文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessRequest()
        {
            string msg = "上传成功！";
            bool flag = true;
            var oFile = System.Web.HttpContext.Current.Request.Files["fileImport"];
            string fileName = oFile.FileName;
            string FileUrl = "";
            try
            {
                //上传的目录
                string path = System.Web.HttpContext.Current.Request.MapPath("~/Excel/output/");
                string fileType = Path.GetExtension(fileName).ToLower();
                string fName = Path.GetFileNameWithoutExtension(fileName);
                //fileName = fName + System.DateTime.Now.ToString("yyyyMMddhhmmsss")+fileType;//防止文件名重复，文件重命名
                fileName = feilioahelper.GetFormatHTNAME(fName.TrimEnd('|')) + System.DateTime.Now.ToString("yyyyMMddhhmmsss") + fileType;//防止文件名重复，文件重命名
                FileUrl = path;
                //feilioahelper.GetFormatHTNAME(fName.TrimEnd('|');
                oFile.SaveAs(path + fileName);
            }
            catch(Exception ex)
            {
                msg = ex.Message;
                flag = false;
            }
            return Content(JsonHelper.GetJsonString(new JsonMessage { Data = fileName,Code="5", Success = flag, Message = FileUrl }));
        }
    }
}
