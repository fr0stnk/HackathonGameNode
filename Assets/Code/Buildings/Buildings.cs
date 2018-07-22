using System.Collections;
using System.Collections.Generic;

public class Building
{
    /// <summary>Build times per level (level- index in the list).</summary>
    public List<int> BuildTimes;

    /// <summary>Upgrade prices per level (level- index in the list).</summary>
    public List<int> UpgradePrices;
}

public class Barracks : Building
{
    /// <summary>How many units can be produced per block on each level.</summary>
    public List<int> UnitsProductionSpeedPerBlock;

    public Barracks()
    {
        this.UnitsProductionSpeedPerBlock = new List<int>() { 1, 2, 3, 4, 5 };
        this.BuildTimes = new List<int>() { 2, 4, 6, 8, 10 };
        this.UpgradePrices = new List<int>() { 50, 100, 150, 300, 500 };
    }
}

public class GoldMine : Building
{
    /// <summary>How much gold it produces per block on each level.</summary>
    public List<int> GoldProductionSpeedPerBlock;

    public GoldMine()
    {
        this.GoldProductionSpeedPerBlock = new List<int>() { 30, 50, 80, 150, 200 };
        this.BuildTimes = new List<int>() { 2, 4, 6, 8, 10 };
        this.UpgradePrices = new List<int>() { 50, 100, 200, 400, 800 };
    }
}

public enum BuildingType : ushort
{
    Barracks, GoldMine
}

public class BuildingUpgradeJob
{
    public BuildingType BuildingType;

    public int LevelAfterUpgrade;

    public int UpgradeFinishesByBlock;
}

public class UnitsBuildJob
{
    public int UnitsLeftToBuild;

    public int UnitsPerBlockBuildTime;
}

