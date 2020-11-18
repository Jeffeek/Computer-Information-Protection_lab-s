using System;
using System.IO;
using System.Linq;

namespace shitty_trisemus
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz,.* ".ToUpper().ToCharArray();
            Console.WriteLine("Count of symbols(with separators): " + alphabet.Length);
            int rows = 0, columns;
            bool isValidTable;
            do
            {
                Console.Write("Type the column count in table: ");
                isValidTable = int.TryParse(Console.ReadLine(), out columns) && columns > 1;
                if (!isValidTable)
                {
                    Console.WriteLine("Loh. You should write the num > 1");
                }
                else
                {
                    rows = alphabet.Length / columns;
                    isValidTable &= rows > 1 && rows * columns == alphabet.Length;
                    if (!isValidTable)
                    {
                        Console.WriteLine("Not valid");
                    }
                }
            }
            while (!isValidTable);

            char[] keyWord;
            bool isValidKeyWord;
            do
            {
                Console.Write("Enter keyword: ");
                keyWord = Console.ReadLine().ToUpper().Distinct().ToArray();
                isValidKeyWord = keyWord.Length > 0 && keyWord.Length < 10;
                if (!isValidKeyWord)
                {
                    Console.WriteLine("After checking there are mistakes! Retype it");
                }
                else
                {
                    isValidKeyWord &= !keyWord.Except(alphabet).Any();
                    if (!isValidKeyWord)
                    {
                        Console.WriteLine("Lol. There shouldn't be symbols which are not in the alphabet");
                    }
                }
            }
            while (!isValidKeyWord);
            for (int i = 0; i < keyWord.Length; i++)
            {
                if (keyWord[i] == ' ')
                    keyWord[i] = '_';
                Console.Write(keyWord[i]);
            }
            Console.WriteLine();

            var table = new char[rows, columns];

            for (var i = 0; i < keyWord.Length; i++)
            {
                table[i / columns, i % columns] = keyWord[i];
            }

            alphabet = alphabet.Except(keyWord).ToArray();

            for (var i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            string message;
            bool isValidMessage;
            do
            {
                Console.Write("Enter the message: ");
                message = Console.ReadLine().ToUpper();
                isValidMessage = !string.IsNullOrEmpty(message);
                if (!isValidMessage)
                {
                    Console.WriteLine("The message is empty");
                }
            }
            while (!isValidMessage);

            message = message.Replace(" ", "_");

            var result = new char[message.Length];

            for (var k = 0; k < message.Length; k++)
            {
                char symbol = message[k];
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            symbol = table[(i + 1) % rows, j];
                            i = rows; 
                            break; 
                        }
                    }
                }
                result[k] = symbol;
            }

            Console.WriteLine("Encrypted: " + String.Concat(result));
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}//encrypted.txt"))
                writer.Write(result);

            for (var k = 0; k < result.Length; k++)
            {
                char symbol = result[k];
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            if (i - 1 > -1)
                                symbol = table[i - 1, j]; 
                            else
                                symbol = table[rows - 1, j];
                            i = rows; 
                            break; 
                        }
                    }
                }
                result[k] = symbol;
            }

            Console.WriteLine("Decrypted message: " + String.Concat(result));
            Console.ReadKey();
        }
    }
}
