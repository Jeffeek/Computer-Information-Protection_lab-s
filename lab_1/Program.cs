using System;
using lab_1.Views;

namespace lab_1
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
