using System.Collections;
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

        //TODO
	    this.GameState = GameState.InitDefault(100);
	}

	// Update is called once per frame
    private void Update ()
	{

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
