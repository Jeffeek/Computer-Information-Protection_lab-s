using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class ForgotPassView : IView
    {
        public void Start()
        {
            Console.WriteLine("Я так понимаю вы забыли свой пароль? \n" +
                              "Не волнуйтесь. Просто введите секретное слово," +
                              " которое вы вписывали при регистрации \n" +
                              "и вы получите ваш пароль");

            string secretWord = GetTypedSecretWord();
            CheckUserSecretWord(secretWord);
        }

        private void CheckUserSecretWord(string secret)
        {
            var list = FileDesirializer.GetProfilesFromFile();
            Profile profile = list.Find(x => x.SecretWord == secret);
            if (profile == null)
            {
                Console.WriteLine("Пользователя с таким секретным словом нет!");
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
            Console.WriteLine();
        }

        private string GetTypedSecretWord()
        {
            Console.WriteLine("Введите секретное слово: ");
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed))
                throw new Exception("Некорректное значение!");
            return typed;
        }
    }
}
