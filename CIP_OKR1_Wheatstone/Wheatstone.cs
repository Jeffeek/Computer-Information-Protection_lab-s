using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CIP_OKR1_Wheatstone
{
    public class Wheatstone
    {
        private string _firstKey;
        private string _secondKey;
        private char[,] _firstMap;
        private char[,] _secondMap;
        private string _alphabet;
        private int _alphabetFactor;

        public Wheatstone(string firstKey, string secondKey, string alphabet = null)
        {
            _alphabet = alphabet ?? "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.";
            _firstKey = String.Concat(firstKey.Distinct());
            _secondKey = String.Concat(secondKey.Distinct());
            _alphabetFactor = (int)Math.Ceiling(Math.Sqrt(_alphabet.Length));
            _firstMap = new char[_alphabetFactor, _alphabetFactor];
            _secondMap = new char[_alphabetFactor, _alphabetFactor];

            var newAlphabet1 = _firstKey.Aggregate(_alphabet, (current, t) => current.Replace(t.ToString(), ""));
            var newAlphabet2 = _secondKey.Aggregate(_alphabet, (current, t) => current.Replace(t.ToString(), ""));

            newAlphabet1 = _firstKey + newAlphabet1;
            newAlphabet2 = _secondKey + newAlphabet2;

            var index = 0;
            for (var i = 0; i < _alphabetFactor; i++)
            {
                for (var j = 0; j < _alphabetFactor; j++)
                {
                    if (index >= newAlphabet1.Length) break;
                    _firstMap[i, j] = newAlphabet1[index];
                    index++;
                }

                if (index >= newAlphabet1.Length) break;
            }

            index = 0;
            for (var i = 0; i < _alphabetFactor; i++)
            {
                for (var j = 0; j < _alphabetFactor; j++)
                {
                    if (index >= newAlphabet2.Length) break;
                    _secondMap[i, j] = newAlphabet2[index];
                    index++;
                }

                if (index >= newAlphabet1.Length) break;
            }
        }

        public string Encrypt(string text)
        {
            text = text.ToLower();

            var result_text = "";

            if (text.Length % 2 != 0)
                text += ' ';

            var length = text.Length / 2;
            var k = 0;
            var bigram = new char[length, 2];
            var kripto_bigram = new char[length, 2];

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    bigram[i, j] = text[k];
                    k++;
                }
            }

            var step = 0;
            while (step < length)
            {
                var cortege1 = FindIndexes(bigram[step, 0], _firstMap);
                var cortege2 = FindIndexes(bigram[step, 1], _secondMap);

                kripto_bigram[step, 0] = _firstMap[cortege1.Item1, cortege2.Item2];
                kripto_bigram[step, 1] = _secondMap[cortege2.Item1, cortege1.Item2];

                step++;
            }


            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    result_text += kripto_bigram[i, j].ToString();
                }
            }

            return result_text;
        }

        public string Decrypt(string text)
        {
            var result_text = "";

            var length = text.Length / 2;
            var k = 0;

            var bigram = new char[length, 2];
            var kripto_bigram = new char[length, 2];

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    bigram[i, j] = text[k];
                    k++;
                }
            }

            var step = 0;
            while (step < length)
            {
                var cortege1 = FindIndexes(bigram[step, 0], _firstMap);
                var cortege2 = FindIndexes(bigram[step, 1], _secondMap);

                kripto_bigram[step, 0] = _firstMap[cortege1.Item1, cortege2.Item2];
                kripto_bigram[step, 1] = _secondMap[cortege2.Item1, cortege1.Item2];

                step++;
            }


            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    result_text += kripto_bigram[i, j].ToString();
                }
            }


            return result_text;
        }

        private (int, int) FindIndexes(char symbol, char[,] matrix)
        {
            for (var i = 0; i < _alphabetFactor; i++)
            {
                for (var j = 0; j < _alphabetFactor; j++)
                {
                    if (symbol == matrix[i, j])
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception();
        }
    }
}
