using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        //TODO send commitment bytes
    }

    public void CommitUnitsTrainAction(ushort count)
    {
        byte[] commitmentTypeBytes = this.ShortToBytes(2); // 2 for unit building
        byte[] unitsCountBytes = this.ShortToBytes(count);

        byte[] commitment = this.GetCommitment(this.JoinArrays(commitmentTypeBytes, unitsCountBytes));

        //TODO send commitment bytes
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
}
