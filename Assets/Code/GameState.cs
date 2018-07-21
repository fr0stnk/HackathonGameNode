using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int CurrentBlockNumber { get; private set; }

    public int GoldMineLevel;

    public int BarracksLevel;

    public int GoldCount;

    public int UnitsCount;

    public BuildingUpgradeJob CurrentUpgradeJob;

    public UnitsBuildJob UnitsBuildJob;

    public Attack CurrentAttack;

    private GoldMine goldMine = new GoldMine();
    private Barracks barracks = new Barracks();

    public static GameState InitDefault(int blockNumber)
    {
        var state = new GameState()
        {
            CurrentBlockNumber = blockNumber,
            GoldMineLevel = 0,
            BarracksLevel = 0,
            GoldCount = 20
        };

        return state;
    }

    public void BuildUnits(int count)
    {
        Debug.Log("Build units " + count);

        if (count < 0)
        {
            Debug.Log("Number is low");
            return;
        }

        // 1 unit = 5 gold
        if (count * 5 > this.GoldCount)
        {
            Debug.Log("Not enough gold");
            return;
        }

        this.GoldCount -= count * 5;

        if (this.UnitsBuildJob == null)
            this.UnitsBuildJob = new UnitsBuildJob() {UnitsLeftToBuild = 0, UnitsPerBlockBuildTime = this.barracks.UnitsProductionSpeedPerBlock[this.BarracksLevel] };

        this.UnitsBuildJob.UnitsLeftToBuild += count;

        Object.FindObjectOfType<GameManager>().Commitments.CommitUnitsTrainAction((ushort)count);
    }

    public void GetUpgradePriceAndTime(BuildingType buildingType, out int cost, out int time, out int nextLevel)
    {
        nextLevel = buildingType == BuildingType.Barracks ? this.BarracksLevel + 1 : this.GoldMineLevel + 1;
        cost = buildingType == BuildingType.Barracks ? this.barracks.UpgradePrices[nextLevel] : this.goldMine.UpgradePrices[nextLevel];
        time = buildingType == BuildingType.Barracks ? this.barracks.BuildTimes[nextLevel] : this.goldMine.BuildTimes[nextLevel];
    }

    public void UpgradeBuilding(BuildingType buildingType)
    {
        Debug.Log("Upgrade: " + buildingType);

        int cost;
        int time;
        int nextLevel;
        this.GetUpgradePriceAndTime(buildingType, out cost, out time, out nextLevel);

        if (cost > this.GoldCount)
        {
            Debug.Log("Not enough gold");
            return;
        }

        if (this.CurrentUpgradeJob != null)
        {
            Debug.Log("Already building smth");
            return;
        }

        Debug.Log("Upgrade started");

        this.CurrentUpgradeJob = new BuildingUpgradeJob()
        {
            BuildingType = buildingType,
            LevelAfterUpgrade = nextLevel,
            UpgradeFinishesByBlock = this.CurrentBlockNumber + time
        };

        Object.FindObjectOfType<GameManager>().Commitments.CommitBuildingUpgradeAction(buildingType);

        this.GoldCount -= cost;
    }

    /// <remarks>No reorgs expected.</remarks>
    public void UpdateState(int newBlockNumber)
    {
        int blocksPassed = newBlockNumber - this.CurrentBlockNumber;
        if (blocksPassed <= 0)
            return;

        int oldBlockNumber = this.CurrentBlockNumber;
        this.CurrentBlockNumber = newBlockNumber;

        var goldMineWasUpgraded = false;
        var goldMineUpgradedBlocksAgo = 0;

        // Upgrades.
        if (this.CurrentUpgradeJob != null && this.CurrentBlockNumber >= this.CurrentUpgradeJob.UpgradeFinishesByBlock)
        {
            // Upgrade level.
            if (this.CurrentUpgradeJob.BuildingType == BuildingType.Barracks)
                this.BarracksLevel = this.CurrentUpgradeJob.LevelAfterUpgrade;
            else
            {
                this.GoldMineLevel = this.CurrentUpgradeJob.LevelAfterUpgrade;
                goldMineWasUpgraded = true;
                goldMineUpgradedBlocksAgo = newBlockNumber - oldBlockNumber;
            }

            this.CurrentUpgradeJob = null;
        }

        // Update gold.
        if (!goldMineWasUpgraded)
        {
            int goldMined = blocksPassed * this.goldMine.GoldProductionSpeedPerBlock[this.GoldMineLevel];
            this.GoldCount += goldMined;
        }
        else
        {
            int goldMined = (blocksPassed - goldMineUpgradedBlocksAgo) * this.goldMine.GoldProductionSpeedPerBlock[this.GoldMineLevel - 1];
            goldMined += goldMineUpgradedBlocksAgo * this.goldMine.GoldProductionSpeedPerBlock[this.GoldMineLevel];

            this.GoldCount += goldMined;
        }

        // Update units.
        if (this.UnitsBuildJob != null)
        {
            int maxCreated = this.UnitsBuildJob.UnitsPerBlockBuildTime * blocksPassed;

            if (maxCreated >= this.UnitsBuildJob.UnitsLeftToBuild)
            {
                int unitsBuilt = this.UnitsBuildJob.UnitsLeftToBuild;
                this.UnitsBuildJob = null;

                this.UnitsCount += unitsBuilt;
            }
            else
            {
                this.UnitsBuildJob.UnitsLeftToBuild -= maxCreated;
                this.UnitsCount += maxCreated;
            }
        }
    }

    public class Attack
    {
        public Coordinate AttackingWho;
        public int HowManyUnits;
    }
}
