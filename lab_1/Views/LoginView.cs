using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using lab_1.Models;
using lab_1.Workers;

namespace lab_1.Views
{
    class LoginView : IView
    {
        public void Start()
        {
            var login = TypeLogin();
            var pass = TypePassword();
            if (CheckLoginAndPass(login, pass))
            {
                var list = FileWorker.GetProfilesFromFile();
                Console.WriteLine("Пользователь успешно вошел в ЧАТ");
                var newpass = PasswordWorker.GenerateNewPassword();
                Console.WriteLine($"Ваш пароль для следующего вашего входа: {newpass}");
                var profile = list.Single(x => x.Login == login);
                profile.Password = PasswordWorker.Encrypt(newpass, login);
                FileWorker.RefreshAll(list);
            }
                
            else
            {
                //Console.WriteLine("Некорректные данные! Даю еще одну попытку..");
                //Start();
            }
        }

        private string TypePassword()
        {
            Console.WriteLine("Введите пароль: ");
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed))
                return TypePassword();
            return typed;
        }

        private string TypeLogin()
        {
            Console.WriteLine("Введите логин: ");
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed) || !CheckLogin(typed))
                return TypeLogin();
            return typed;
        }

        private bool CheckLogin(string login)
        {
            var list = FileWorker.GetProfilesFromFile();
            Profile profile = list.Find(x => x.Login == login);
            if (profile == null)
            {
                Console.WriteLine("Такого логина нет!");
                return false;
            }
            Console.WriteLine("Такой логин есть!");
            return true;
        }

        private bool CheckLoginAndPass(string login, string pass)
        {
            var list = FileWorker.GetProfilesFromFile();
            Profile profile = list.Find(x => x.Login == login && x.Password == pass);
            if (profile == null)
            {
                Console.WriteLine("А пароль то.. неправильный!");
                Console.WriteLine("Консоль закроется через 5 секунд");
                Thread.Sleep(5000);
                Process.GetCurrentProcess().CloseMainWindow();
            }

            return true;
        }
    }
}
