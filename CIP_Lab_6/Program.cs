using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ElGamalExt;

namespace CIP_Lab_6
{
    class Program
    {
	    private static string _toLoadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "from.txt");
	    private static string _toSaveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "to.txt");
        static async Task Main(string[] args)
        {
	        var loaded = File.ReadAllText(_toLoadFilePath); 
	        Console.WriteLine("Текст для шифровки: ");
	        Console.WriteLine(loaded);
	        Console.Write("Шифрование...");
	        var encrypted = await Crypt(loaded);
	        Console.WriteLine("Сохранение..");
	        File.WriteAllBytes(_toSaveFilePath, encrypted.Item1);
	        Console.WriteLine("Готово!");
	        await Task.Delay(2000);
	        Console.Clear();
	        await Task.Delay(1000); 
	        Environment.Exit(0);
        }

        static async Task<(byte[], ElGamalParameters)> Crypt(string value)
        {
	        for(int i = 0; i < 10; i++)
	        {
		        await Task.Delay(100);
		        Console.Write('.');
	        }
	        var elGamal = new ElGamalManaged();
	        var settings = elGamal.ExportParameters(true);
	        Console.Clear();
	        var encrypted = elGamal.EncryptData(Encoding.UTF8.GetBytes(value));
	        return (encrypted, settings);
        }
    }
}
