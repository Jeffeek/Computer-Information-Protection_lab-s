using System;
using System.IO;
using System.Linq;
using CIP_lab_1.Models;
using CIP_lab_1.Workers;

namespace CIP_lab_1.Views
{
    class RegistrationView : IView
    {
        public void Start()
        {
            var passwordWorker = new RandomPasswordTripleDES();
            var fileWorker  = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json");
            string password = passwordWorker.GeneratePassword();
            Console.WriteLine("Введите логин: ");
            string login = GetTypedLogin();
            Console.WriteLine($"Ваш пароль: {password}");
            password = passwordWorker.Encrypt(password, login);
            Console.WriteLine("Введите секретную фразу для восстановления пароля:");
            string secretPhrase = GetTypedSecretPhrase();
            Console.WriteLine("Введите ФИО");
            string fullName = GetTypedFullName();
            var profile = new Profile()
            {
                FullName = fullName,
                SecretWord = secretPhrase,
                Password = password,
                Login = login
            };

            fileWorker.Writer.AddNewProfile(profile);
            Console.WriteLine("Новый пользователь добавлен!\nТеперь вы можете войти");
            var loginView = new LoginView();
            loginView.Start();
        }

        private string GetTypedFullName()
        {
            string name = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrEmpty(name))
                return GetTypedLogin();
            return name;
        }

        private string GetTypedSecretPhrase()
        {
            var fileWorker = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json");
            var list = fileWorker.Reader.GetProfiles().Select(x => x.SecretWord);
            string phrase = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phrase) || list.Contains(phrase))
            {
                Console.WriteLine("Данную секретную фразу использовать нельзя :(\nВведите еще раз:");
                return GetTypedSecretPhrase();
            }
            return phrase;
        }

        private string GetTypedLogin()
        {
            var fileWorker = new FileWorker<Profile>($"{Directory.GetCurrentDirectory()}//profiles.json");
            string login = Console.ReadLine();
            var list = fileWorker.Reader.GetProfiles();
            if (list.SingleOrDefault(x => x.Login == login) == null)
            {
                return login;
            }

            Console.WriteLine("Такой логин уже существует! Повторите попытку!");
            return GetTypedLogin();
        }
    }
}
