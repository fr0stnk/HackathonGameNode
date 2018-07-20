using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button CastleView;
    public Button MapView;

    //==========Castle buttons
    public Button BarracksButton;

    public Button GoldMineButton;

    //==========Map buttons

    public GameManager GameManager;

    /// <summary>Object that should be shown in castle view.</summary>
    public List<GameObject> CastleViewObjects;

    /// <summary>Object that should be shown in map view.</summary>
    public List<GameObject> MapViewObjects;

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

    public void OnGameStateChanged(GameState state)
    {
        List<GameObject> toHide = state == GameState.Custle ? this.MapViewObjects : this.CastleViewObjects;
        List<GameObject> toShow = state == GameState.Custle ? this.CastleViewObjects : this.MapViewObjects;

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
