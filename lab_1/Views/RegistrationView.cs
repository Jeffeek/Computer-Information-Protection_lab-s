using System;
using System.Linq;
using lab_1.Models;
using lab_1.Workers;

namespace lab_1.Views
{
    class RegistrationView : IView
    {
        public void Start()
        {
            Console.WriteLine("Введите логин: ");
            string login = GetTypedLogin();
            Console.WriteLine("Введите пароль: ");
            string password = GetTypedPassword();
            password = PasswordWorker.Encrypt(password, login);
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
            FileWorker.AddNewProfile(profile);
            Console.WriteLine("Новый пользователь добавлен!\nТеперь вы можете войти");
            var loginView = new LoginView();
            loginView.Start();
        }

        private string GetTypedPassword()
        {
            string pass = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(pass) || String.IsNullOrEmpty(pass) || pass.Length < 8)
            {
                Console.WriteLine("Возникла ошибка. PS минимум символов для пароля 8 символов");
                return GetTypedPassword();
            }
            return pass;
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
            var list = FileWorker.GetProfilesFromFile().Select(x => x.SecretWord);
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
            string login = Console.ReadLine();
            var list = FileWorker.GetProfilesFromFile();
            if (list.SingleOrDefault(x => x.Login == login) == null)
            {
                return login;
            }
            else
            {
                Console.WriteLine("Такой логин уже существует! Повторите попытку!");
                return GetTypedLogin();
            }
        }
    }
}
