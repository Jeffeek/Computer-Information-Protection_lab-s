using System;
using System.Security.Cryptography;
using System.Text;

namespace lab_1
{
    public static class PasswordEncoding
    {
        public static string Encrypt(string input)
        {
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash);
        }

        public static string Decrypt(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }
    }
}
