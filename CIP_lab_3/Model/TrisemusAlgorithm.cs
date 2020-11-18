using System;
using System.Linq;

namespace CIP_lab_3.Model
{
    public class TrisemusAlgorithm
    {
        private int _columnsCount, _rowsCount;
        private char[] russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_,.".ToUpper().ToCharArray();
        private char[] englishAlphabet = "abcdefghijklmnopqrstuvwxyz_,.".ToUpper().ToCharArray();
        private char[] _alphabet;
        private char[,] _table;
        private string _message;
        private string _key;

        public TrisemusAlgorithm(string message, string key, int columnsCount, bool isEnglish = false)
        {
            _alphabet = isEnglish ? englishAlphabet : russianAlphabet;
            _message = message;
            _key = key;
            key = GetNormalizedKey();
            _key = key;
            message = GetNormalizedMessage();
            _message = message;
            _columnsCount = columnsCount;
            _rowsCount = _alphabet.Length / _columnsCount;
            CheckIsValidKey();
            CheckIsValidTable();
            _table = new char[_rowsCount,_columnsCount];
            FillTable();
        }

        private void CheckIsValidTable()
        {
            if (_columnsCount <= 1) 
                throw new ArgumentException(nameof(_columnsCount), "Количество колонок не может быть меньше либо равно 1");
            bool isValidTable = true;
            isValidTable &= _rowsCount > 1 && _rowsCount * _columnsCount == _alphabet.Length;
            if (!isValidTable) throw new ArgumentException("Ошибка создания таблицы");
        }

        private void CheckIsValidKey()
        {
            if (_key == null) throw new ArgumentNullException(nameof(_key));
            if (_key.Length < 1 || _key.Length > 10) throw new ArgumentException("Длина ключа не может быть меньше 1 или равной или большей 10 символам");
        }

        private string GetNormalizedKey()
        {
            return string.Concat(_key
                                    .ToUpper()
                                    .Replace(" ", "_")
                                    .Distinct()
                                    .Except(_message));
        }

        private string GetNormalizedMessage() => 
                                                _message
                                                        .ToUpper()
                                                        .Replace(" ", "_");

        public string Encrypt()
        {
            FillTable();
            var result = new char[_message.Length];
            for (var k = 0; k < _message.Length; k++)
            {
                char symbol = _message[k];
                for (var i = 0; i < _rowsCount; i++)
                {
                    for (var j = 0; j < _columnsCount; j++)
                    {
                        if (symbol == _table[i, j])
                        {
                            symbol = _table[(i + 1) % _rowsCount, j];
                            i = _rowsCount;
                            break;
                        }
                    }
                }
                result[k] = symbol;
            }

            return String.Concat(result);
        }

        private void FillTable()
        {
            for (var i = 0; i < _key.Length; i++)
            {
                _table[i / _columnsCount, i % _columnsCount] = _key[i];
            }

            var newAlphabet = String.Concat(_alphabet.Except(_key));
            for (var i = 0; i < newAlphabet.Length; i++)
            {
                int position = i + _key.Length;
                _table[position / _columnsCount, position % _columnsCount] = newAlphabet[i];
            }
        }

        public string Decrypt()
        {
            //FillTable();
            var result = new char[_message.Length];
            for (int i = 0; i < _message.Length; i++)
                result[i] = _message[i];
            for (var k = 0; k < result.Length; k++)
            {
                char symbol = result[k];
                for (var i = 0; i < _rowsCount; i++)
                {
                    for (var j = 0; j < _columnsCount; j++)
                    {
                        if (symbol == _table[i, j])
                        {
                            if (i - 1 > -1)
                                symbol = _table[i - 1, j];
                            else
                                symbol = _table[_rowsCount - 1, j];
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
