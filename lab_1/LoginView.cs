using System;
using System.Diagnostics;
using System.Threading;

namespace lab_1
{
    class LoginView : IView
    {
        public void Start()
        {
            string login = "";
            string pass = "";
            Console.WriteLine("Введите логин: ");
            TypeLogin(ref login);
            Console.WriteLine("Введите пароль: ");
            TypePassword(ref pass);
            if (CheckPassword(login, pass))
                Console.WriteLine("Пользователь успешно вошел в ЧАТ");
        }

        private void TypePassword(ref string pass)
        {
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed))
                Start();
            pass = typed;
        }

        private void TypeLogin(ref string login)
        {
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed) || !CheckLogin(typed))
                Start();
            login = typed;
        }

        private bool CheckLogin(string login)
        {
            var list = FileDesirializer.GetProfilesFromFile();
            Profile profile = list.Find(x => x.Login == login);
            if (profile == null)
            {
                Console.WriteLine("Такого логина нет!");
                return false;
            }
            Console.WriteLine("Такой логин есть!");
            return true;
        }

        private bool CheckPassword(string login, string pass)
        {
            var list = FileDesirializer.GetProfilesFromFile();
            string encrypted = PasswordEncoding.Encrypt(pass);
            Profile profile = list.Find(x => x.Login == login && x.Password == encrypted);
            if (profile == null)
            {
                Console.WriteLine("Такого пользователя нет! Зарегистрировать? y/n");
                char input = Char.ToLower((char)Console.Read());
                if (input == 'y')
                {
                    var regview = new RegistrationView();
                    regview.Start();
                }
                else if (input == 'n')
                {
                    Console.WriteLine("Консоль закроется через 5 секунд");
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {
                    Console.WriteLine("Неверный ответ!");
                    Console.WriteLine("Консоль закроется через 5 секунд");
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().CloseMainWindow();
                }

                return false;
            }

            return true;
        }
    }
}
