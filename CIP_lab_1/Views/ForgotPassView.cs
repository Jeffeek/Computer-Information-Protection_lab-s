using System;
using System.IO;
using System.Linq;
using CIP_lab_1.Models;
using CIP_lab_1.Views;
using CIP_lab_1.Workers;

namespace CIP_lab_1.Views
{
    class ForgotPassView : IView
    {
        public void Start()
        {
            Console.WriteLine("Я так понимаю вы забыли свой пароль? \n" +
                              "Не волнуйтесь. Просто введите секретное слово и ваш логин," +
                              " которое вы вписывали при регистрации \n" +
                              "и вы получите ваш пароль");

            string login = GetTypedLogin();
            string secretWord = GetTypedSecretWord();
            CheckUserSecretWordAndLogin(secretWord, login);
        }

        private string GetTypedLogin()
        {
            Console.WriteLine("Введите ваш логин: ");
            string login = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(login))
            {
                Console.WriteLine("Введен некорректный логин! Повторите попытку");
                return GetTypedLogin();
            }

            return login;
        }

        private void CheckUserSecretWordAndLogin(string secret, string login)
        {
            var fileWorker = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json");
            var list = fileWorker.Reader.GetProfiles(true);
            Profile profile = list.SingleOrDefault(x => x.SecretWord == secret && x.Login == login);
            if (profile == null)
            {
                Console.WriteLine("Пользователя с таким секретным словом и логином нет!");
                Start();
            }
            else
            {
                Console.WriteLine("Нашли вас!");
                Console.WriteLine($"Ваш логин: {profile.Login}");
                Console.WriteLine($"Ваш пароль: {profile.Password}");
                LoginView view = new LoginView();
                view.Start();
            }
        }

        private string GetTypedSecretWord()
        {
            Console.WriteLine("Введите секретное слово: ");
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed))
                return GetTypedSecretWord();
            return typed;
        }
    }
}
