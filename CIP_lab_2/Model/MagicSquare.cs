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
        private string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private string englishAlphabet = "abcdefghijklmnopqrstuvwxyz";
        private string _alphabet;

        public MagicSquare(bool isEnglish=false)
        {
            _square = new char[5,5];
            _alphabet = isEnglish ? englishAlphabet : russianAlphabet;
        }

        private char[,] GetSquare(string key)
        {
            var newAlphabet = _alphabet;
            for (int i = 0; i < key.Length; i++)
            {
                newAlphabet = newAlphabet.Replace(key[i].ToString(), "");
            }

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

        private bool FindSymbol(char[,] symbolsTable, char symbol, out int column, out int row)
        {
            var l = symbolsTable.GetUpperBound(0) + 1;
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (symbolsTable[i, j] == symbol)
                    {
                        row = i;
                        column = j;
                        return true;
                    }
                }
            }

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
            password = password.ToLower();
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
