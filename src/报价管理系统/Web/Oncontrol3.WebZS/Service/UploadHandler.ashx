<%@ WebHandler Language="C#" Class="UploadHandler" %>
using System;
using System.Web;
using System.IO;
using Oncontrol3.Web.Helpers;

public class UploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["myfile"];
        string uploadRoot = HttpContext.Current.Server.MapPath("~/userfiles") + "/";
        string TempFile="TempFile";

        string guid = @context.Request["guid"];
        string command = @context.Request["command"];
        string contextstr = "";

        //路径：bimg/yyyy/MM/dd
        DateTime dt = DateTime.Now;
        string folder = dt.ToString("yyyy") + "" + dt.ToString("MM");
        string path = command + "/" + folder;

        string ext = GetExtension(file.FileName);
        string filenameguid = guid + ext;


        




        if (file != null && !string.IsNullOrEmpty(guid))
        {
            switch (command)
            {
                case "LICXls":{
                //立刻网导入
                string fullPath=uploadRoot+TempFile+"/"+dt.ToString("yyyyMMdd")+"/"+command+"/";
                    FileHelper.PathExist(fullPath, "create");
                    file.SaveAs(fullPath + filenameguid);
                    contextstr = ("{result:1,guid:\"" + guid + "\",msg:\"已上传\",fpath:\"" + TempFile+"/"+dt.ToString("yyyyMMdd")+"/"+command + "/" + filenameguid + "\"}");
                    break;
                    }
                case "EDWXls":
                    {
                        //EDW-FO补充金额导入
                        string fullPath = uploadRoot + TempFile + "/" + dt.ToString("yyyyMMdd") + "/" + command + "/";
                        FileHelper.PathExist(fullPath, "create");
                        file.SaveAs(fullPath + filenameguid);
                        contextstr = ("{result:1,guid:\"" + guid + "\",msg:\"已上传\",fpath:\"" + TempFile + "/" + dt.ToString("yyyyMMdd") + "/" + command + "/" + filenameguid + "\"}");
                        break;
                    }
                default:
                    contextstr = ("{result:0,guid:\"" + guid + "\",msg:\"接收文件异常\"}");
                    break;
            }

        }
        else
        {
            contextstr = ("{result:0,guid:\"" + guid + "\",msg:\"接收文件异常\"}");
        }
        context.Response.Write(contextstr);
    }

    //获取扩展名
    private string GetExtension(string filename)
    {
        return (Path.GetExtension(filename).ToUpper());
    }

    //获取文件类型
    private string GetFileType(string filename)
    {
        string ext = GetExtension(filename);
        switch (ext)
        {
            case ".JPG":
            case ".BMP":
            case ".JPEG":
            case ".GIF":
            case ".PNG":
                return ("IMAGE");
            case ".SWF":
                return ("FLASH");
            case ".DOC":
            case ".DOCX":
                return ("WORD");
            case ".XLS":
            case ".XLSX":
                return ("EXCEL");
            case ".PPT":
            case ".PPTX":
                return ("POWERPOINTER");
            case ".ASPX":
            case ".JS":
            case ".ASHX":
            case ".EXE":
            case ".BAT":
                return ("SCRIPT");
            case ".RAR":
            case ".7Z":
            case ".ZIP":
                return ("PACK");
            default:
                return ("UNKNOWN");
        }
    }
    public bool IsReusable
    {
        get { return false; }
    }

}