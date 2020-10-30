using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIP_lab_2.Model
{
    public class MagicSquare
    {
        private char[,] _square;
        private string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public MagicSquare()
        {
            _square = new char[5,5];
        }

        private char[,] GetSquare(string key)
        {
            var newAlphabet = _alphabet;
            //удаляем из алфавита все символы которые содержит ключ
            for (int i = 0; i < key.Length; i++)
            {
                newAlphabet = newAlphabet.Replace(key[i].ToString(), "");
            }

            //добавляем пароль в начало алфавита, а в конец дополнительные знаки
            //для того чтобы избежать пустых ячеек
            newAlphabet = key + newAlphabet + "0123456789!@#$%^&*)_+-=<>?,.";


            var index = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index < newAlphabet.Length)
                    {
                        _square[i, j] = newAlphabet[index];
                        index++;
                    }
                }
            }

            return _square;
        }

        //поиск символа в двухмерном массиве
        private bool FindSymbol(char[,] symbolsTable, char symbol, out int column, out int row)
        {
            var l = symbolsTable.GetUpperBound(0) + 1;
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (symbolsTable[i, j] == symbol)
                    {
                        //значение найдено
                        row = i;
                        column = j;
                        return true;
                    }
                }
            }

            //если ничего не нашли
            row = -1;
            column = -1;
            return false;
        }

        public string Encrypt(string text, string password)
        {
            text = text.ToLower();
            password = password.ToLower();
            var outputText = "";
            var square = GetSquare(password);
            var m = text.Length;
            var coordinates = new int[m * 2];
            for (int i = 0; i < m; i++)
            {
                if (FindSymbol(square, text[i], out int columnIndex, out int rowIndex))
                {
                    coordinates[i] = columnIndex;
                    coordinates[i + m] = rowIndex;
                }
            }

            for (int i = 0; i < m * 2; i += 2)
            {
                outputText += square[coordinates[i + 1], coordinates[i]];
            }

            return outputText;
        }

        public string Decrypt(string text, string password)
        {
            var outputText = "";
            var square = GetSquare(password);
            var m = text.Length;
            var coordinates = new int[m * 2];
            var j = 0;
            for (int i = 0; i < m; i++)
            {
                if (FindSymbol(square, text[i], out int columnIndex, out int rowIndex))
                {
                    coordinates[j] = columnIndex;
                    coordinates[j + 1] = rowIndex;
                    j += 2;
                }
            }

            for (int i = 0; i < m; i++)
            {
                outputText += square[coordinates[i + m], coordinates[i]];
            }

            return outputText;
        }
    }
}
