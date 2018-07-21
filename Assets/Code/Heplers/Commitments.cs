using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Commitments
{
    private Random random;

    public Commitments()
    {
        this.random = new System.Random();
    }

    public void CommitBuildingUpgradeAction(BuildingType buildingType)
    {
        byte[] commitmentTypeBytes = this.ShortToBytes(1); // 1 for buildings
        byte[] buildingTypeBytes = this.ShortToBytes((ushort)buildingType);

        byte[] commitment = this.GetCommitment(this.JoinArrays(commitmentTypeBytes, buildingTypeBytes));

        string hex = ByteArrayToHexString(commitment);

        API.Instance.SignAndSendCommitment(hex);
    }

    public void CommitUnitsTrainAction(ushort count)
    {
        byte[] commitmentTypeBytes = this.ShortToBytes(2); // 2 for unit building
        byte[] unitsCountBytes = this.ShortToBytes(count);

        byte[] commitment = this.GetCommitment(this.JoinArrays(commitmentTypeBytes, unitsCountBytes));

        string hex = ByteArrayToHexString(commitment);

        API.Instance.SignAndSendCommitment(hex);
    }

    private byte[] GetCommitment(byte[] data)
    {
        byte[] blindingFactor = this.RandomBytes(32);

        byte[] commitmentBytes = Cryptography.SHA256(this.JoinArrays(data, blindingFactor));

        return commitmentBytes;
    }

    private byte[] JoinArrays(byte[] data1, byte[] data2)
    {
        List<byte> dataBytes = data1.ToList();
        dataBytes.AddRange(data2);

        return dataBytes.ToArray();
    }

    private byte[] RandomBytes(int count)
    {
        var arr = new byte[count];
        this.random.NextBytes(arr);

        return arr;
    }

    private byte[] ShortToBytes(ushort value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);

        return bytes;
    }

    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
    public static byte[] HexStringToByteArray(String hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }
}
