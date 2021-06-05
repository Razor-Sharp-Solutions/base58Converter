using RazorSharp.Converters;
using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Base58Converter.ByteArray = new byte[] { 0, 0, 0, 0 };
			Console.ReadKey();
		}
	}
}
