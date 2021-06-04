using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace base58Converter
{
	public class Program
	{
		private static string _base58Charset = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
		public static string Base58String { get; set; } = "";
		public static BigInteger Number { get; set; }

		private static ObservableCollection<byte> _internal;
		private static ObservableCollection<byte> Internal
		{
			get => _internal;
			set
			{

			}
		}

		private static byte[] _byteArray;
		public static byte[] ByteArray 
		{ 
			get => _byteArray;
			set
			{
				_byteArray = value;
			} 
		}

		static void Main(string[] args)
		{
			ByteArray = new byte[2];
			ByteArray[1] = 5;
			ByteArray[0] = 10;
			
			Console.WriteLine($"{BitConverter.ToString(ByteArray)}");
			Encode(new BigInteger(123456789));
			Console.WriteLine(Base58String);
			Console.WriteLine(Decode());
			Console.ReadKey();
		}

		static void Encode(BigInteger t)
		{
			Base58String = Base58String.Insert(0, _base58Charset[(int)t % 58].ToString());
			if (t / 58 is BigInteger next && next != 0)
				Encode(next);
		}

		static BigInteger Decode() => Base58String
											.Reverse()
											.Select((val, dex) => new Tuple<char, int>(val, dex))
											.Aggregate(new BigInteger(), (agg, base58tuple) =>
												  agg += _base58Charset.IndexOf(base58tuple.Item1) * (int)Math.Pow(58, base58tuple.Item2));
	}
}
