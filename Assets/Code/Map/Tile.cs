using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Coordinate Coordinate;

    public TileType TileType;

	private void Start ()
	{

	}

    private void Update ()
	{

	}

    private void OnMouseDown()
    {
        Debug.Log("ClickedTile");
    }
}

public enum TileType
{
    UserCity, Envir1, Envir2
}
