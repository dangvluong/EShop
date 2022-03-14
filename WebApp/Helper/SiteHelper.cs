using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Helper
{
    public class SiteHelper
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
        public static string CreateToken(int length)
        {
            string pattern = "qwertyuiopasdfghjklzxcvbnm1234567890";
            char[] arrayToken = new char[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                arrayToken[i] = pattern[rand.Next(pattern.Length)];
            }
            return string.Join("", arrayToken); 
        }
    }
}
