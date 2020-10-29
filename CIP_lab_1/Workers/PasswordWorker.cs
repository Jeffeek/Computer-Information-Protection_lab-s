using System;
using System.Security.Cryptography;
using System.Text;

namespace CIP_lab_1.Workers
{
    public class RandomPasswordTripleDES
    {
        public static RandomPasswordTripleDES Get()
        {
            return new RandomPasswordTripleDES();
        }

        public string GeneratePassword()
        {
            var sb = new StringBuilder();
            string[] arr =
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J",
                "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d",
                "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z",
                "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y"
            };
            Random rnd = new Random();
            var max = rnd.Next(7, 10);
            for (int i = 0; i < max; i++)
                sb.Append(arr[rnd.Next(0, 57)]);

            return sb.ToString();
        }

        public string Encrypt(string input, string hash)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = Encoding.UTF8.GetBytes(input);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string input, string hash)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(input);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
