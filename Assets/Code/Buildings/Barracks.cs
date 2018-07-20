using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    /// <summary>How many units can be produced per block on each level.</summary>
    public List<int> UnitsProductionSpeedPerBlock;


    // Use this for initialization
    public void Start ()
    {
        this.UnitsProductionSpeedPerBlock = new List<int>() { 1, 2, 4, 8, 16};
        this.BuildTimes = new List<int>() { 5, 10, 20, 40, 80};
        this.UpgradePrices = new List<int>() { 50, 100, 200, 400, 800 };
    }
}
