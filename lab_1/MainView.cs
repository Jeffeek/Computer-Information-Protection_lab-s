using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab_1
{
    class MainView
    {
        private void StartView(IView view)
        {
            view.Start();
        }

        public MainView()
        {
            Console.WriteLine("1. Зарегистрироваться");
            Console.WriteLine("2. Войти");
            Console.WriteLine("3. Забыл пароль");
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                switch (answer)
                {
                    case 1:
                    {
                        StartView(new RegistrationView());
                        break;
                    }

                    case 2:
                    {
                        StartView(new LoginView());
                        break;
                    }

                    case 3:
                    {
                        StartView(new ForgotPassView());
                        break;
                    }

                    default:
                    {
                        Console.WriteLine("Нет такого варианта ответа!");
                        Console.WriteLine("Консоль закроется через 5 секунд");
                        Thread.Sleep(500);
                        Process.GetCurrentProcess().CloseMainWindow();
                        break;
                    }
                }
            }
        }
    }
}
