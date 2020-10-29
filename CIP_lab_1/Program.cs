using System;
using CIP_lab_1.Views;

namespace CIP_lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IView view = new MainView();
            view.Start();
            Console.ReadKey();
        }
    }
}
