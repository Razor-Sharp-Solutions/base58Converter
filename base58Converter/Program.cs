using System;
using System.Linq;
using System.Numerics;

namespace base58Converter
{
	class Program
	{
		static string base58Charset = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
		static string result = "";

		static void Main(string[] args)
		{
			Encode(new BigInteger(123456789));
			Console.WriteLine(result);
			Console.WriteLine(Decode());
			Console.ReadKey();
		}

		static void Encode(BigInteger t)
		{
			result = result.Insert(0, base58Charset[(int)t % 58].ToString());
			if (t / 58 is BigInteger next && next != 0)
				Encode(next);
		}

		static BigInteger Decode() => result.Reverse()
											.Select((val, dex) => new Tuple<char, int>(val, dex))
											.Aggregate(new BigInteger(), (agg, base58tuple) =>
												  agg += base58Charset.IndexOf(base58tuple.Item1) * (int)Math.Pow(58, base58tuple.Item2));
	}
}
