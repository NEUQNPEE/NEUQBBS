using System.Security.Cryptography;
using System.Text;


namespace WebApiDemoCommon;

/// <summary>
/// MD5 哈希帮助类
/// </summary>
public static class Md5Helper
{
    /// <summary>
    /// 将字符串转换为 MD5 哈希值
    /// </summary>
    /// <param name="str">待转换的字符串</param>
    /// <returns>MD5 哈希值的十六进制字符串表示</returns>
    public static string ToMd5(this string str)
    {
        byte[] output = MD5.HashData(Encoding.Default.GetBytes(str));
        var md5Str = BitConverter.ToString(output).Replace("-", "");
        return md5Str;
    }
}
