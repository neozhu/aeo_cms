using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Common;
using System.Text;
using System.Threading;

namespace Aim.Portal.Web.CommonPages
{
    public partial class DownLoad : BasePage
    {
        public DownLoad()
        {
            this.IsCheckLogon = false;
        }
        string fileId = String.Empty;

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.EnableViewState = false;   // 禁用ViewState
            fileId = Request["id"];
            Stream iStream = null;

            try
            {
                if (!String.IsNullOrEmpty(fileId))
                {
                    FileItem file = FileItem.Find(fileId);

                    // Buffer to read 10K bytes in chunk:
                    byte[] buffer = new Byte[10000];

                    // Length of the file:
                    int length;

                    // Total bytes to read:
                    long dataToRead;

                    // Identify the file to download including its path.
                    string filepath = file.FilePath;

                    // Identify the file name.
                    string filename = System.IO.Path.GetFileName(filepath);
                    if (Request.UserAgent.Contains("MSIE") || Request.UserAgent.Contains("msie"))
                    {
                        // 如果客户端使用 Microsoft Internet Explorer，则需要编码
                        filename = ToHexString(filename.Replace(fileId + "_", ""));     // 如果使用  fileName =Server.UrlEncode(fileName);  则会出现上文中出现的情况
                    }

                    // Open the file.
                    iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read, System.IO.FileShare.Read);

                    // Total bytes to read:
                    dataToRead = iStream.Length;

                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

                    // Read the bytes.
                    while (dataToRead > 0)
                    {
                        // Verify that the client is connected.
                        if (Response.IsClientConnected)
                        {
                            // Read the data in buffer.
                            length = iStream.Read(buffer, 0, 10000);

                            // Write the data to the current output stream.
                            Response.OutputStream.Write(buffer, 0, length);

                            // Flush the data to the HTML output.
                            Response.Flush();

                            buffer = new Byte[10000];
                            dataToRead = dataToRead - length;
                        }
                        else
                        {
                            //prevent infinite loop if user disconnects
                            dataToRead = -1;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language=JScript>");
                Response.Write("alert(\"" + ex.Message + ",下载Id=" + fileId + " 的文件时发生系统级错误\");");
                Response.Write("</script>");
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }

                Response.End();
            }
        }

        /// <summary>
        /// 为字符串中的非英文字符编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///指定 一个字符是否应该被编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 为非英文字符串编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }


        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();

                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
