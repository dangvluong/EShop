using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Helper
{
    public static class SiteHelper
    {
        public static byte[] HashPassword(string plaintext)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-512");
            return algorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plaintext));
        }
        public static string FormatCurrencyString(int input)
        {
            return input.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
        }
    }
}
