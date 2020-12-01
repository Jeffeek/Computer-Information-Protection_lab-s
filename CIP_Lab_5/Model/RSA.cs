using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CIP_Lab_5.Model
{
    public class RSA
    {
        private char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
            'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
            'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
            'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', '0' };

        public void Encrypt(long p, long q, string text)
        {
            if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
            {
                FileWorker writer = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\Encrypted.txt");
                FileWorker writerClose = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\CloseKey.txt");
                FileWorker writerOpen = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\OpenKey.txt");
                string s = text.ToUpper();
                //открытые
                long n = p * q;
                long m = (p - 1) * (q - 1);
                // закрытые
                long d = Calculate_d(m); 
                long e = Calculate_e(d, m);

                string close = $"{d} {e}";
                string open = $"{n} {m}";

                List<string> result = RSA_Endoce(s, e, n);
                writer.Write(String.Join(" ", result).ToUpper());
                writerClose.Write(close);
                writerOpen.Write(open);
            }
        }

        public void Decrypt(long d, long n)
        {
            FileWorker reader = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\Encrypted.txt");
            var input = reader.Read().Split().ToList();
            var result = RSA_Dedoce(input, d, n);
            FileWorker writer = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\Decrypted.txt");
            writer.Write(result.ToUpper());
        }

        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";


            foreach (string item in input)
            {
                long index = long.Parse(item);
                var b = BigInteger.Pow(new BigInteger(index), (int)d);

                index %= n;

                int bindex = Convert.ToInt32(index.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        private long Calculate_e(long d, long m)
        {
            long e = 10;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }
    }
}
