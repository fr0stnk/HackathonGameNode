using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public List<Tile> Tiles;

    /// <summary>The tile types to sprites.</summary>
    /// <remarks>Set in editor.</remarks>
    public List<TileTypeToSprite> TileTypesToSprites;

    public GameObject TilePrefab;

    private readonly Coordinate StartPoint = new Coordinate(500,500);

	// Use this for initialization
    private void Start ()
	{
        this.Tiles = new List<Tile>();

        // Test TODO
        this.Add9TilesToMap();
    }

	// Update is called once per frame
    private void Update ()
    {

	}

    /// <summary>Creates 9 tiles and puts them on a map.</summary>
    private void Add9TilesToMap()
    {
        Coordinate leftTopCorner = this.FindCoordinateFor9TilesCreation();

        var coordinates = new List<Coordinate>();

        for (int x = leftTopCorner.X; x < leftTopCorner.X + 3; ++x)
        {
            for (int y = leftTopCorner.Y; y > leftTopCorner.Y - 3; --y)
            {
                coordinates.Add(new Coordinate(x,y));
            }
        }

        foreach (Coordinate coordinate in coordinates)
        {
            var tileType = (TileType)Random.Range(0, this.TileTypesToSprites.Count);

            GameObject obj = Instantiate(this.TilePrefab, new Vector3(coordinate.X, coordinate.Y), Quaternion.identity);

            var tile = obj.GetComponent<Tile>();
            tile.Coordinate = coordinate;
            tile.TileType = tileType;

            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.TileTypesToSprites.Single(x => x.TileType == tileType).Sprite;
        }
    }

    /// <summary>Finds coordinate for placing square made of 9 tiles. Coordinate for left top square is returned.</summary>
    private Coordinate FindCoordinateFor9TilesCreation()
    {
        if (this.Tiles.Count == 0)
            return this.StartPoint;

        //TODO fill it deterministically in a way that we never generate next layer before prev was filled completely
        return null;
    }
}

[Serializable]
public class TileTypeToSprite
{
    public TileType TileType;
    public Sprite Sprite;
}