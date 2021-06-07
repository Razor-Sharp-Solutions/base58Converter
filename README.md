# base58Converter
A simple converter to and from base58. 

## Usage
The converter class has four properties:
* ByteArray
* BE_Number (big endian serialization)
* LE_Number (little endian serialization)
* Base58String

To encode and decode simply set any of the properites and the conversion will be done. Setting the ByteArray, BE_Number or LE_Number will encode the value into a base58 string and update the rest of the properties. Setting the Base58String will decode it and update all three properties. The byte array will always be in big endian order, most significant byte first. 

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
* Changing a single byte in the array will not alter anything, at the moment you need to change the entire array in order to trigger the setter. Will be fixed in the next version.



