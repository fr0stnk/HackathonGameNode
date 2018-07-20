using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : Building {

    /// <summary>How much gold it produces per block on each level.</summary>
    public List<int> GoldProductionSpeedPerBlock;

    // Use this for initialization
    public void Start()
    {
        this.GoldProductionSpeedPerBlock = new List<int>() { 10, 20, 40, 80, 200 };
        this.BuildTimes = new List<int>() { 2, 4, 8, 16, 32 };
        this.UpgradePrices = new List<int>() { 50, 100, 200, 400, 800 };
    }
}
