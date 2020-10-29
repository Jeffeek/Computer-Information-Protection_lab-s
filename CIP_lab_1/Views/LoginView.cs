using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using CIP_lab_1.Models;
using CIP_lab_1.Workers;

namespace CIP_lab_1.Views
{
    class LoginView : IView
    {
        public void Start()
        {
            var login = TypeLogin();
            var pass = TypePassword();
            if (CheckLoginAndPass(login, pass))
            {
                var passwordWorker = new RandomPasswordTripleDES();
                var fileWorker = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json");
                var profilesList = fileWorker.Reader.GetProfiles();
                var newpass = passwordWorker.GeneratePassword();
                Console.WriteLine($"Ваш пароль для следующего вашего входа: {newpass}");
                var profile = profilesList.Single(x => x.Login == login);
                profile.Password = passwordWorker.Encrypt(newpass, login);
                fileWorker.Writer.RewriteAll(profilesList);
                Console.WriteLine("Пользователь успешно вошел в ЧАТ");
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
            var list = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json").Reader.GetProfiles();
            Profile profile = list.SingleOrDefault(x => x.Login == login);
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
            var list = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json").Reader.GetProfiles(true);
            Profile profile = list.SingleOrDefault(x => x.Login == login && x.Password == pass);
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
