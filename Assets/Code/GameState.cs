using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int CurrentBlockNumber;

    public int GoldMineLevel;

    public int BarracksLevel;

    public int GoldCount;

    public int UnitsCount;

    //TODO
    public BuildingUpgradeJob CurrentUpgradeJob;

    //TODO
    public UnitsBuildJob UnitsBuildJob;

    private GoldMine mine = new GoldMine();
    private Barracks barracks = new Barracks();

    public static GameState InitDefault(int blockNumber)
    {
        GameState state = new GameState()
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
        //TODO check count if not too big

    }

    public void UpgradeBuilding(BuildingType buildingType)
    {
        //TODO check count if not too big

    }

    //TODO call when we are aware about a new block mined
    /// <remarks>No reorgs expected.</remarks>
    public void UpdateState(int newBlockNumber)
    {
        int blocksPassed = this.CurrentBlockNumber - newBlockNumber;
        if (blocksPassed <= 0)
            return;

        int oldBlockNumber = this.CurrentBlockNumber;
        this.CurrentBlockNumber = newBlockNumber;

        var goldMineWasUpgraded = false;
        var goldMineUpgradedBlocksAgo = 0;

        // Upgrades.
        if (this.CurrentUpgradeJob != null && this.CurrentUpgradeJob.UpgradeFinishesByBlock >= this.CurrentBlockNumber)
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
            int goldMined = blocksPassed * this.mine.GoldProductionSpeedPerBlock[this.GoldMineLevel];
            this.GoldCount += goldMined;
        }
        else
        {
            int goldMined = (blocksPassed - goldMineUpgradedBlocksAgo) * this.mine.GoldProductionSpeedPerBlock[this.GoldMineLevel - 1];
            goldMined += goldMineUpgradedBlocksAgo * this.mine.GoldProductionSpeedPerBlock[this.GoldMineLevel];

            this.GoldCount += goldMined;
        }

        // Update units.
        if (this.UnitsBuildJob != null)
        {
            int maxCreated = this.UnitsBuildJob.UnitsPerBlockBuildTime * blocksPassed;

            if (maxCreated > this.UnitsBuildJob.UnitsLeftToBuild)
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
}
