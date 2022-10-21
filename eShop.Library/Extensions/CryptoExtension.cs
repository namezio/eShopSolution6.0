using System.Security.Cryptography;
using System.Text;

namespace eShop.Library.Extensions;

public static class CryptoExtension
{
    public static string Md5(this string str)
    {
        using var provider = MD5.Create();
        var builder = new StringBuilder();
        foreach (var b in provider.ComputeHash(Encoding.UTF8.GetBytes(str)))
            builder.Append(b.ToString("x2").ToLower());

        return builder.ToString();
    }
}