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

    public Commitments Commitments { get; private set; }

    private Vector3 cachedCamPosition;

    // Use this for initialization
    private void Start ()
	{
	    Random.InitState(1);

	    this.cachedCamPosition = Camera.main.transform.position;
        this.Commitments = new Commitments();

        this.SetState(CurrentGameScreen.Custle);

	    this.GameState = GameState.InitDefault(this.CurrentBlock);
	}

    private float UpdateUIInSeconds = 0;

    private float MineBlockInSeconds = 0;
    private int CurrentBlock = 10;

    private const int SecondsPerBlock = 5;

    // Update is called once per frame
    private void Update ()
    {
        this.UpdateUIInSeconds -= Time.deltaTime;
        this.MineBlockInSeconds -= Time.deltaTime;

        if (this.UpdateUIInSeconds <= 0)
        {
            this.UpdateUIInSeconds = 0.1f;

            this.UIManager.UpdateUI();
        }

        if (this.MineBlockInSeconds <= 0)
        {
            this.MineBlockInSeconds = SecondsPerBlock;

            this.CurrentBlock++;
            this.GameState.UpdateState(this.CurrentBlock);

            //Debug.Log("Block mined: " + this.CurrentBlock);
        }
    }

    public void SetState(CurrentGameScreen screen)
    {
        this.Screen = screen;
        this.UIManager.OnGameStateChanged(screen);

        Camera.main.transform.position = cachedCamPosition;

        Object.FindObjectOfType<CameraControllerScript>().DoMovement = screen == CurrentGameScreen.Map;
    }
}

public enum CurrentGameScreen
{
    Custle, Map
}
