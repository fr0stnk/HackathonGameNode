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

    public GameState State { get; private set; }

    // Use this for initialization
    private void Start ()
	{
	    Random.InitState(1);
	}

	// Update is called once per frame
    private void Update ()
	{

	}

    public void SetState(GameState state)
    {
        this.State = state;
        this.UIManager.OnGameStateChanged(state);

        Debug.Log($"State changed to {state}");
    }
}

public enum GameState
{
    Custle, Map
}
