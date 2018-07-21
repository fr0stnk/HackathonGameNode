﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <remarks>Set in editor.</remarks>
    public Map Map;

    /// <remarks>Set in editor.</remarks>
    public UIManager UIManager;

    public CurrentGameScreen Screen { get; private set; }

    public GameState GameState { get; private set; }

    // Use this for initialization
    private void Start ()
	{
	    Random.InitState(1);

        this.SetState(CurrentGameScreen.Custle);

        //TODO
	    this.GameState = GameState.InitDefault(100);
	}

    private float UpdateUIInSeconds = 0;

	// Update is called once per frame
    private void Update ()
    {
        this.UpdateUIInSeconds -= Time.deltaTime;

        if (this.UpdateUIInSeconds <= 0)
        {
            this.UpdateUIInSeconds = 0.1f;

            this.UIManager.UpdateUI();
        }

        //TODO imitate blocks being mined
    }

    public void SetState(CurrentGameScreen screen)
    {
        this.Screen = screen;
        this.UIManager.OnGameStateChanged(screen);
    }
}

public enum CurrentGameScreen
{
    Custle, Map
}
