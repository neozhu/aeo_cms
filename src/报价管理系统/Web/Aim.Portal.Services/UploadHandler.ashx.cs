using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.SessionState;

using System.ServiceModel;


namespace Aim.Portal.Services
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0 && context.Request.Form["FolderKey"] != null)
            {
                for (int j = 0; j < context.Request.Files.Count; j++)
                {
                    HttpPostedFile uploadFile = context.Request.Files[j];
                    if (uploadFile.ContentLength > 0)
                    {
                        string CustomParamer = "FolderKey:" + context.Request.Form["FolderKey"].ToString();
                        string filePath = new UploadService().TemporaryUploadFolder;
                        ///判断路径是否存在
                        if (Directory.Exists(filePath) == false){
                            Directory.CreateDirectory(filePath);
                        }
                        uploadFile.SaveAs(filePath + "/" + uploadFile.FileName);
                        string filename = new UploadService().FinishedFile(uploadFile.FileName, CustomParamer);
                        if (context.Session["Context"] != null)
                        {
                            context.Session["Context"] = context.Session["Context"].ToString().Substring(0, context.Session["Context"].ToString().Length - 1) + "," + filename;
                        }
                        else
                        {
                            context.Session.Remove("Context");
                            context.Session["Context"] = filename + ",";
                        }
                    }
                }
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write("1");
            }
            else
            {
                context.Response.ContentType = "text/plain";
                if (context.Session["Context"] != null && context.Request.Form["FolderKey"] == null)
                {
                    String callbackFunName = context.Request.QueryString["callbackparam"].ToString();
                    context.Response.Write(callbackFunName + "([{ name:'" + "," + context.Session["Context"].ToString() + "'}])");
                    context.Session.Remove("Context");
                }
              
            }
         }  
        

  
      
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}