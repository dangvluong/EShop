﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class Helper
    {
        public static byte[] HashPassword(string plaintext)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-512");
            return algorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plaintext));
        }
    }
}