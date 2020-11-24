using System;

namespace CIP_lab_4.Model
{
    public class WiegenerAlgorithm
    {
        private char[] russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя,.".ToCharArray();
        private char[] englishAlphabet = "abcdefghijklmnopqrstuvwxyz,.".ToCharArray();
        private char[] _alphabet;

        public WiegenerAlgorithm(bool isEnglish)
        {
            _alphabet = isEnglish ? englishAlphabet : russianAlphabet;
        }

        public string Encrypt(string message, string key)
        {
            message = message.ToLower().Trim();
            key = key.ToLower().Trim();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in message)
            {
                int c = (Array.IndexOf(_alphabet, symbol) +
                         Array.IndexOf(_alphabet, key[keyword_index])) % _alphabet.Length;

                result += _alphabet[c];

                keyword_index++;

                if ((keyword_index + 1) == key.Length)
                    keyword_index = 0;
            }

            return result;
        }

        public string Decrypt(string message, string key)
        {
            message = message.ToLower().Trim();
            key = key.ToLower().Trim();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in message)
            {
                int p = (Array.IndexOf(_alphabet, symbol) + _alphabet.Length -
                         Array.IndexOf(_alphabet, key[keyword_index])) % _alphabet.Length;

                result += _alphabet[p];

                keyword_index++;

                if ((keyword_index + 1) == key.Length)
                    keyword_index = 0;
            }

            return result;
        }
    }
}
