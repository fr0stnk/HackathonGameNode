﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button CastleView;
    public Button MapView;

    public Text CurrentBlockText;

    //==========Castle
    public Button BarracksButton;
    public Button UpgradeBarracksButton;

    public Button GoldMineButton;
    public Button UpgradeGoldMineButton;

    public Text GoldMineUpgradeInfo, BarracksUpgradeInfo;

    //====Labels

    public Text GoldCountText;
    public Text UnitsCountText;

    public Text UpgradesText;

    public Text BuildingUnitsText;

    public Text AttacksText;

    public Text GoldMineLevelText;
    public Text BarracksLevelText;

    //==========Map

    public GameManager GameManager;

    /// <summary>Object that should be shown in castle view.</summary>
    public List<GameObject> CastleViewObjects;

    /// <summary>Object that should be shown in map view.</summary>
    public List<GameObject> MapViewObjects;


    public void UpdateUI()
    {
        GameState gameState = this.GameManager.GameState;

        this.CurrentBlockText.text = "Current Block: " +  gameState.CurrentBlockNumber.ToString();

        //Castle view ===============

        this.GoldCountText.text = gameState.GoldCount.ToString();
        this.UnitsCountText.text = gameState.UnitsCount.ToString();

        this.GoldMineLevelText.text = gameState.GoldMineLevel.ToString();
        this.BarracksLevelText.text = gameState.BarracksLevel.ToString();

        // Upgrade cost
        {
            int cost;
            int time;
            int nextLevel;
            gameState.GetUpgradePriceAndTime(BuildingType.Barracks, out cost, out time, out nextLevel);

            this.BarracksUpgradeInfo.text = "Price: " + cost + "  Time: " + time;
        }
        {
            int cost;
            int time;
            int nextLevel;
            gameState.GetUpgradePriceAndTime(BuildingType.GoldMine, out cost, out time, out nextLevel);

            this.GoldMineUpgradeInfo.text = "Price: " + cost + "  Time: " + time;
        }

        //TODO AttacksText BuildingUnitsText UpgradesText
    }

    // Use this for initialization
    public void Start ()
    {
		this.CastleView.onClick.AddListener(() =>
		{
		    this.GameManager.SetState(CurrentGameScreen.Custle);
        });

        this.MapView.onClick.AddListener(() =>
        {
            this.GameManager.SetState(CurrentGameScreen.Map);
        });

        // Upgrade

        this.UpgradeBarracksButton.onClick.AddListener(() =>
        {
            this.GameManager.GameState.UpgradeBuilding(BuildingType.Barracks);
        });

        this.UpgradeGoldMineButton.onClick.AddListener(() =>
        {
            this.GameManager.GameState.UpgradeBuilding(BuildingType.GoldMine);
        });
    }

    public void OnGameStateChanged(CurrentGameScreen screen)
    {
        List<GameObject> toHide = screen == CurrentGameScreen.Custle ? this.MapViewObjects : this.CastleViewObjects;
        List<GameObject> toShow = screen == CurrentGameScreen.Custle ? this.CastleViewObjects : this.MapViewObjects;

        this.HodeObjects(toHide);
        this.ShowObjects(toShow);
    }

    private void HodeObjects(List<GameObject> objects)
	{
	    foreach (GameObject obj in objects)
            obj.SetActive(false);
	}

    private void ShowObjects(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
            obj.SetActive(true);
    }
}
