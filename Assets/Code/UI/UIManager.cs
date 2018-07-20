using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button CastleView;
    public Button MapView;

    public GameManager GameManager;

    // Use this for initialization
    public void Start ()
    {
		this.CastleView.onClick.AddListener(() =>
		{
		    this.GameManager.SetState(GameState.Custle);
        });

        this.MapView.onClick.AddListener(() =>
        {
            this.GameManager.SetState(GameState.Map);
        });
    }

    private void Update ()
	{

	}
}
