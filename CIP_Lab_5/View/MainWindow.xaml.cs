using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CIP_Lab_5.Model;

namespace CIP_Lab_5.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rsa = new RSA();
            var primes = GeneratePrimes();
            rsa.Encrypt(primes.Item1, primes.Item2, Decrypted.Text);
        }

        private (long, long) GeneratePrimes()
        {
            var rnd = new Random();
            long p, q;
            int num = rnd.Next(100, 1000);
            bool isPrime = false;
            while (!isPrime)
            {
                if (IsPrime(num))
                    isPrime = true;
                else
                    num = rnd.Next(100, 1000);
            }

            p = num;
            isPrime = false;
            num = rnd.Next(100, 1000);
            while (!isPrime)
            {
                if (IsPrime(num))
                    isPrime = true;
                else
                    num = rnd.Next(100, 1000);
            }
            q = num;

            return (p, q);
        }

        private bool IsPrime(long num)
        {
            // если n > 1
            if (num > 1)
            {
                // в цикле перебираем числа от 2 до n - 1
                for (int i = 2; i < num; i++)
                    if (num % i == 0) // если n делится без остатка на i - возвращаем false (число не простое)
                        return false;

                // если программа дошла до данного оператора, то возвращаем true (число простое) - проверка пройдена
                return true;
            }
            else // иначе возвращаем false (число не простое)
                return false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileWorker close = new FileWorker($"{Directory.GetCurrentDirectory()}\\Files\\CloseKey.txt");
            var nums = close.Read().Split().Select(long.Parse).ToArray();
            var rsa = new RSA();
            rsa.Decrypt(nums[0], nums[1]);

        }
    }
}
