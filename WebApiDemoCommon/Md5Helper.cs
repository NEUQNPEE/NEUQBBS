using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemoCommon
{
    public static class Md5Helper
    {
        public static string ToMd5(this string str)
        {
            byte[] output = MD5.HashData(Encoding.Default.GetBytes(str));
            var md5Str = BitConverter.ToString(output).Replace("-", "");
            return md5Str;
        }
    }
}