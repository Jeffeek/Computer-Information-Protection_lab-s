using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class RegistrationView : IView
    {
        public void Start()
        {
            Console.WriteLine("Введите логин: ");
            string name = GetTypedLogin();
            Console.WriteLine("Введите пароль: ");
            string password = GetTypedLogin();
            Console.WriteLine("Введите секретную фразу для восстановления пароля:");
            string secretPhrase = GetSecretPhrase();
        }

        private string GetSecretPhrase()
        {
            var list = FileDesirializer.GetProfilesFromFile().Select(x => x.SecretWord);
            string phrase = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phrase) || list.Contains(phrase))
            {
                Console.WriteLine("Данную секретную фразу использовать нельзя :(\nВведите еще раз:");
                return GetSecretPhrase();
            }
            return phrase;
        }

        private string GetTypedLogin()
        {
            string login = Console.ReadLine();
            var list = FileDesirializer.GetProfilesFromFile();
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
