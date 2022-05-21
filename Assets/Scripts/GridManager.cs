using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform cam;

    [SerializeField] private GameObject gameBoard;

    private Dictionary<Vector2, Tile> tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        gameBoard.transform.position = InitPosition((float)width, (float)height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init();
                spawnedTile.transform.SetParent(gameBoard.transform);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        cam.transform.position = InitPosition((float)width, (float)height);
        gameBoard.transform.Rotate(0, 0, 45, Space.Self);
    }

    private Vector3 InitPosition(float width, float height)
    {
        return new Vector3(width / 2 - 0.5f, height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}
