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
				value.ToList().ForEach(ch => { if (!_base58Charset.Contains(ch)) throw new ArgumentException($"Character {ch} is not a valid base58 symbol!"); });
				_base58String = value;

				Decode();

				_byteArray = _BE_number.ToByteArray();

				if (_BE_number.CompareTo(BigInteger.Zero) < 0)
					_BE_number = new BigInteger(1, _byteArray);

				ArrayZeroPad();

				_LE_number = new BigInteger(_byteArray.Reverse().ToArray());
			}
		}

		private static BigInteger _BE_number;
		public static BigInteger BE_Number 
		{ 
			get => _BE_number; 
			set
			{
				_BE_number = value;
				_byteArray = _BE_number.ToByteArray();
				_LE_number = new BigInteger(_byteArray.Reverse().ToArray());
				_base58String = string.Empty;
				Encode(_BE_number);
			}
		}

		private static BigInteger _LE_number;
		public static BigInteger LE_Number
		{
			get => _LE_number;
			set
			{
				_LE_number = value;
				_byteArray = _LE_number.ToByteArray().Reverse().ToArray();
				_BE_number = new BigInteger(_byteArray);
				_base58String = string.Empty;
				Encode(_BE_number);
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
				_BE_number = new BigInteger(_byteArray);
				_LE_number = new BigInteger(_byteArray.Reverse().ToArray());
				_base58String = string.Empty;
				Encode(_BE_number);
			} 
		}

		private static void Encode(BigInteger source)
		{
			_base58String = _base58String.Insert(0, _base58Charset[source.Mod(new BigInteger("58")).IntValue].ToString());
			if (source.Divide(new BigInteger("58")) is BigInteger next && next.IntValue != 0)
				Encode(next);
			else
				Base58ZeroPad();
		}

		private static void Decode() => _BE_number = Base58String
											.Reverse()
											.Select((val, dex) => new Tuple<char, int>(val, dex))
											.Aggregate(BigInteger.Zero, (agg, base58tuple) =>
												  agg.Add(new BigInteger(_base58Charset.IndexOf(base58tuple.Item1).ToString()).Multiply(new BigInteger("58").Pow(base58tuple.Item2))));
		private static void Base58ZeroPad()
		{
			foreach (byte by in _byteArray)
				if (by == 0 && _byteArray.Length > 1)
					if (_BE_number.IntValue == 0 && (_base58String.Length == _byteArray.Length))
						break;
					else
						_base58String = _base58String.Insert(0, "1");
				else
					break;
		}

		private static void ArrayZeroPad()
		{
			foreach (char sy in _base58String)
					if (sy == '1' && _base58String.Length > 1 && _byteArray.Length < _base58String.Length)
						_byteArray = new byte[1].Concat(_byteArray).ToArray();
					else
						break;
		}

		public static void PrintOut() => Console.WriteLine($"Bytes: {BitConverter.ToString(ByteArray)}\nBE: {BE_Number}\nLE: {LE_Number}\nBase58: {Base58String}");
	}
}
