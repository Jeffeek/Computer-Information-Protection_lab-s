using System;
using System.Diagnostics;
using System.Threading;

namespace CIP_lab_1.Views
{
    class MainView : IView
    {
        private void StartView(IView view)
        {
            view.Start();
        }

        public void Start()
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
                        Thread.Sleep(5000);
                        Process.GetCurrentProcess().CloseMainWindow();
                        break;
                    }
                }
            }
        }
    }
}
