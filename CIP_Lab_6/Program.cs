using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ElGamalExt;

namespace CIP_Lab_6
{
    class Program
    {
	    //private static string _toLoadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "from.txt");
	    private static string _toSaveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "to.txt");
	    private static ElGamalManaged ElGamal = new ElGamalManaged();
	    private static ElGamalParameters Parameters;

	    static void Main(string[] args)
	    {
		    Parameters = ElGamal.ExportParameters(true);
            Console.WriteLine("Текст для шифровки: ");
            var toEncrypt = Console.ReadLine();
            Console.Write("Шифрование...");
            var encrypted = Encrypt(toEncrypt);
            Console.WriteLine("Готово!");
            File.WriteAllBytes(_toSaveFilePath, encrypted);
            Console.WriteLine($"Результат: {Encoding.UTF8.GetString(encrypted)}");
            Console.Write("Расшифрование...");
            var decrypted = Decrypt(encrypted);
            Console.WriteLine("Готово!");
            Console.WriteLine($"Результат: {Encoding.UTF8.GetString(decrypted)}");

            Console.ReadKey();
        }

	    private static byte[] Encrypt(string value) => ElGamal.EncryptData(Encoding.UTF8.GetBytes(value));

	    private static byte[] Decrypt(byte[] data) => ElGamal.DecryptData(data);
    }
}
