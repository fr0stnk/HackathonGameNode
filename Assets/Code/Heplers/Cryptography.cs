using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Cryptography
{
    private static SHA256 sha256 = new SHA256Cng();

    public static byte[] SHA256(byte[] bytes)
    {
        byte[] output = sha256.ComputeHash(bytes);

        return output;
    }

    public static string SHA256EncodedToBase58(byte[] bytes)
    {
        byte[] output = SHA256(bytes);

        string encoded = Base58Encoding.Encode(output);

        return encoded;
    }

    public static byte[] StringToBytes(string data)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(data);

        return bytes;
    }

    public static string BytesToString(byte[] bytes)
    {
        string data = Encoding.ASCII.GetString(bytes);

        return data;
    }
}
