using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RazorSharp.Converters
{
	public class Base58Converter
	{
		private static string _base58Charset = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

		private static string _base58String = string.Empty; 
		public static string Base58String
		{ 
			get => _base58String; 
			set
			{
				_base58String = value;

				Decode();

				_byteArray = _number.ToByteArray();

				foreach (char sy in _base58String)
					if (sy == '1' && _base58String.Length > 1 && _byteArray.Length < _base58String.Length)
						_byteArray = new byte[1].Concat(_byteArray).ToArray();
					else
						break;

			}
		}

		private static BigInteger _number;
		public static BigInteger Number 
		{ 
			get => _number; 
			set
			{
				_number = value;
				_byteArray = _number.ToByteArray();
				_base58String = string.Empty;
				Encode(_number);
			}
		}


		private static byte[] _byteArray;
		public static byte[] ByteArray 
		{ 
			get => _byteArray;
			set
			{
				if (value is null)
					throw new ArgumentNullException();
				_byteArray = value;
				_number = new BigInteger(_byteArray);
				_base58String = string.Empty;
				Encode(_number);
				foreach (byte by in _byteArray)
					if (by == 0 && _byteArray.Length > 1)
						if (_number.IntValue == 0 && (_base58String.Length == _byteArray.Length))
							break;
						else
							_base58String = _base58String.Insert(0, "1");
					else
						break;
			} 
		}

		static void Encode(BigInteger source)
		{
			_base58String = _base58String.Insert(0, _base58Charset[source.Mod(new BigInteger("58")).IntValue].ToString());
			if (source.Divide(new BigInteger("58")) is BigInteger next && next.IntValue != 0)
				Encode(next);
		}

		static void Decode() => _number = Base58String
											.Reverse()
											.Select((val, dex) => new Tuple<char, int>(val, dex))
											.Aggregate(new BigInteger(new byte[1]), (agg, base58tuple) =>
												  agg.Add(new BigInteger(_base58Charset.IndexOf(base58tuple.Item1).ToString()).Multiply(new BigInteger("58").Pow(base58tuple.Item2))));
	}
}
