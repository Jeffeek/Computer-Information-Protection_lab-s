using System;

namespace CIP_OKR1_Wheatstone
{
	class Program
	{
		static void Main(string[] args)
		{
			var cypher = new Wheatstone("писька", "жопка");
			var toEncrypt = "я бы бы не было да было..";
			var result = cypher.Encrypt(toEncrypt);
			var decryptResult = cypher.Decrypt(result);
		}
	}
}
