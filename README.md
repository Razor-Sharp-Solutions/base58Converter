# base58Converter
A simple converter to and from base58. 

## Usage
The converter class has three properties:
* ByteArray
* Number
* Base58String

To encode and decode simply set any of the properites and the conversion will be done.

E.g.
```c#
Base58Converter.ByteArray = new byte[] { 0x27, 0x1f, 0x35, 0xc1 };
```

Afterwards:
```c#
Console.WriteLine(Base58Converter.Base58String)
```
Will be:
>21111a

## Remarks
* Changing a single byte in the array will not alter anything.
* The Number property is a big endian BigInt, so, as base 58, 1112 and 2 will both evalute to 1 decimal. The byte array will keep the 0x0 bytes correctly.


