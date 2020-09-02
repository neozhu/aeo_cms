using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    public class DBKeyHelper
    {
        public static string ToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c).ToUpper();
        }

        //public static byte[] HexToBytes(string str)
        //{
        //    if (str.Length == 0 || str.Length % 2 != 0)
        //        return new byte[0];

        //    byte[] buffer = new byte[str.Length / 2];
        //    char c;
        //    for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
        //    {
        //        // Convert first half of byte
        //        c = str[sx];
        //        buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

        //        // Convert second half of byte
        //        c = str[++sx];
        //        buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
        //    }

        //    return buffer;
        //}

        public static string HexToBytes(string str)
        {
            if (str.Length == 0 || str.Length % 2 != 0)
                return string.Empty;

            byte[] buffer = new byte[str.Length / 2];
            char c;
            for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
            {
                // Convert first half of byte
                c = str[sx];
                buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

                // Convert second half of byte
                c = str[++sx];
                buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
            }

            return buffer.ToString();
        }
        
        //public static byte[] HexStringToBytes(string hexStr)
        //{
        //    if (string.IsNullOrEmpty(hexStr))
        //    {
        //        return new byte[0];
        //    }

        //    if (hexStr.StartsWith("0x"))
        //    {
        //        hexStr = hexStr.Remove(0, 2);
        //    }

        //    var count = hexStr.Length;

        //    if (count % 2 == 1)
        //    {
        //        throw new ArgumentException("Invalid length of bytes:" + count);
        //    }

        //    var byteCount = count / 2;
        //    var result = new byte[byteCount];
        //    for (int ii = 0; ii < byteCount; ++ii)
        //    {
        //        var tempBytes = Byte.Parse(hexStr.Substring(2 * ii, 2), System.Globalization.NumberStyles.HexNumber);
        //        result[ii] = tempBytes;
        //    }

        //    return result;
        //}
        //public static string BytesTohexString(byte[] bytes)
        //{
        //    if (bytes == null || bytes.Count() < 1)
        //    {
        //        return string.Empty;
        //    }

        //    var count = bytes.Count();

        //    var cache = new StringBuilder();
        //    cache.Append("0x");
        //    for (int ii = 0; ii < count; ++ii)
        //    {
        //        var tempHex = Convert.ToString(bytes[ii], 16).ToUpper();
        //        cache.Append(tempHex.Length == 1 ? "0" + tempHex : tempHex);
        //    }

        //    return cache.ToString();
        //}
        //public static string byteToHexStr(byte[] bytes)
        //{
        //    string returnStr = "";
        //    if (bytes != null)
        //    {
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            returnStr += bytes[i].ToString("X2");
        //        }
        //    }
        //    return returnStr;
        //}
        //public static string base64ToHex(string base64)
        //{
        //    byte[] byteArray = Convert.FromBase64String(base64);
        //    string bytetohexstr = byteToHexStr(byteArray);
        //    return bytetohexstr;
        //}
    }
}