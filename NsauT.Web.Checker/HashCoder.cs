﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace NsauT.Web.Checker
{
    internal class HashCoder
    {
        public string GetSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, input);

                //Console.WriteLine($"The SHA256 hash of {input} is: {hash}.");

                //Console.WriteLine("Verifying the hash...");

                //if (VerifyHash(sha256Hash, input, hash))
                //{
                //    Console.WriteLine("The hashes are the same.");
                //}
                //else
                //{
                //    Console.WriteLine("The hashes are not same.");
                //}

                return hash;
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
