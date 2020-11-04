using System;
using System.Linq;

namespace CIP_lab_3.Model
{
    public class TrisemusAlgorithm
    {
        private int _columnsCount, _rowsCount;
        private string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_,.";
        private string englishAlphabet = "abcdefghijklmnopqrstuvwxyz_,.";
        private string _alphabet;

        public TrisemusAlgorithm(bool isEnglish = false)
        {
            _alphabet = isEnglish ? englishAlphabet : russianAlphabet;
        }

        private void CheckIsValidTable()
        {
            if (_columnsCount < 1) throw new ArgumentException(nameof(_columnsCount), "Количество колонок не может быть меньше 1");
            if (!(_rowsCount > 1 && _rowsCount * _columnsCount == _alphabet.Length)) throw new ArgumentException("Ошибка создания таблицы");
        }

        private void CheckIsValidKey(string key)
        {
            if (key.Length < 1 || key.Length >= _alphabet.Length) throw new ArgumentException("Длина ключа не может быть меньше 1 или равной или большей алфавиту"); 
        }

        private string GetNormilizedKey(string key)
        {
            return string.Concat(key.ToLower().Replace(" ", "_").Distinct());
        }

        private string GetNormalizedMessage(string text) => text.ToLower().Replace(" ", "_");

        public string Encrypt(string text, string key)
        {
            _columnsCount = (int)Math.Ceiling(Math.Sqrt(text.Length));
            _rowsCount = (int)Math.Ceiling(text.Length / (double)_columnsCount);
            //CheckIsValidTable();
            key = GetNormilizedKey(key);
            CheckIsValidKey(key);
            text = GetNormalizedMessage(text);
            var table = new char[_rowsCount, _columnsCount];
            for (var i = 0; i < key.Length; i++)
            {
                table[i / _columnsCount, i % _columnsCount] = key[i];
            }

            var newAlphabet = _alphabet.Except(key).ToArray();
            for (var i = 0; i < newAlphabet.Length; i++)
            {
                int position = i + key.Length;
                table[position / _columnsCount, position % _columnsCount] = newAlphabet[i];
            }

            var result = new char[text.Length];
            for (var k = 0; k < text.Length; k++)
            {
                char symbol = text[k];
                for (var i = 0; i < _rowsCount; i++)
                {
                    for (var j = 0; j < _columnsCount; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            symbol = table[(i + 1) % _rowsCount, j];
                            i = _rowsCount; 
                            break; 
                        }
                    }
                }
                result[k] = symbol;
            }

            return String.Concat(result);
        }
    }
}
