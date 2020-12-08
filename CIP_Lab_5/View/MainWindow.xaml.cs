using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using CIP_Lab_5.Model;

namespace CIP_Lab_5.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
            'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
            'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
            'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', '0' };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_d.Text.Length > 0 && textBox_n.Text.Length > 0)
            {
                long d = Convert.ToInt64(textBox_d.Text);
                long n = Convert.ToInt64(textBox_n.Text);
                List<string> input = new List<string>();
                FileWorker sr = new FileWorker("Files\\Encrypted.txt");
                input.AddRange(sr.Read().Replace("\r\n", "\n").Split('\n'));
                string result = RSA_Dedoce(input, d, n);
                StreamWriter sw = new StreamWriter("Files\\Decrypted.txt");
                sw.WriteLine(result);
                sw.Close();

                Process.Start("Files\\Decrypted.txt");
            }
            else
                MessageBox.Show("Введите секретный ключ!");
        }

        private void ButtonCrypt_Click(object sender, RoutedEventArgs e)
        {
            if ((textBox_p.Text.Length > 0) && (textBox_q.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox_p.Text);
                long q = Convert.ToInt64(textBox_q.Text);

                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                    FileWorker sr = new FileWorker("Files\\TextToEncrypt.txt");
                    var s = sr.Read();
                    sr.Path = $"Files\\OpenKey.txt";
                    sr.Write($"{p} {q}");
                    s = s.ToUpper();
                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long d = Calculate_d(m);
                    long e_ = Calculate_e(d, m);
                    List<string> result = RSA_Endoce(s, e_, n);
                    string wr = String.Join(Environment.NewLine, result);
                    textBox_d.Text = d.ToString();
                    textBox_n.Text = n.ToString();
                    sr.Path = "Files\\CloseKey.txt";
                    sr.Write($"{d} {n}");
                    Process.Start("Files\\Encrypted.txt");
                }
                else
                    MessageBox.Show("p или q - не простые числа!");
            }
            else
                MessageBox.Show("Введите p и q!");
        }

        //проверка: простое ли число?
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

        //зашифровать
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            BigInteger n_ = new BigInteger((int) n);
            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int) e);
                bi %= n_;
                result.Add(bi.ToString());
            }

            return result;
        }

        //расшифровать
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";
            BigInteger bi;
            BigInteger n_ = new BigInteger((int) n);
            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int) d);
                bi %= n_;
                int index = Convert.ToInt32(bi.ToString());
                result += characters[index].ToString();
            }

            return result;
        }

        //вычисление параметра d. d должно быть взаимно простым с m
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

        //вычисление параметра e
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

        private void btnLoadOpen_Click(object sender, RoutedEventArgs e)
        {
            var file = new FileWorker($"Files\\OpenKey.txt");
            var text = file.Read();
            if (text.Length <= 0) return;
            var nums = text.Split().Select(long.Parse).ToArray();
            textBox_q.Text = nums[0].ToString();
            textBox_p.Text = nums[1].ToString();
        }

        private void btnLoadClose_Click(object sender, RoutedEventArgs e)
        {
            var file = new FileWorker($"Files\\CloseKey.txt");
            var text = file.Read();
            if (text.Length == 0) return;
            var nums = text.Split().Select(long.Parse).ToArray();
            textBox_d.Text = nums[0].ToString();
            textBox_n.Text = nums[1].ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            long q = GeneratePrivateKey();
            textBox_q.Text = q.ToString();
            long p = GeneratePrivateKey();
            textBox_p.Text = p.ToString();
        }

        private long GeneratePrivateKey()
        {
            var rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            long random = rnd.Next(100, 300);
            while (!IsTheNumberSimple(random))
                random = rnd.Next(100, 300);
            return random;
        }
    }
}
