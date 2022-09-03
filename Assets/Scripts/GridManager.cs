using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private GameObject pointPrefab;

    [SerializeField] private Transform cam;

    [SerializeField] private GameObject gameBoard;

    private Dictionary<Vector2, Tile> tiles;

    private Dictionary<Vector2, GameObject> points;

    public int Width
    {
        get { return width; }
    }

    public int Height
    {
        get { return height; }
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        points = new Dictionary<Vector2, GameObject>();

        gameBoard.transform.position = InitPosition((float)width, (float)height);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = CreateTile(x, y);


                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        for (int x = -width * width; x < width * width + 2; x++)
        {
            for (int y = -height * height; y < height * height + 2; y++)
            {
                points[new Vector2(x, y)] = CreatePoint(x, y);
            }
        }

        cam.transform.position = InitPosition((float)width, (float)height);
        gameBoard.transform.Rotate(0, 0, 45, Space.Self);
    }

    private Vector3 InitPosition(float width, float height)
    {
        return new Vector3(width / 2 - 0.5f, height / 2 - 0.5f, -10);
    }

    private GameObject CreatePoint(int x, int y)
    {
        var spawnedPoint = Instantiate(pointPrefab, new Vector3(x, y), Quaternion.identity);
        spawnedPoint.name = $"Point {x} {y}";

        spawnedPoint.transform.SetParent(gameBoard.transform);

        return spawnedPoint;
    }

    private Tile CreateTile(int x, int y)
    {
        var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
        spawnedTile.Position = new Vector2(x, y);

        spawnedTile.Init();
        spawnedTile.transform.SetParent(gameBoard.transform);

        return spawnedTile;
    }

    public Tile GetTile(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    public GameObject GetPoint(Vector2 pos)
    {
        if (points.TryGetValue(pos, out var point))
        {
            return point;
        }

        return null;
    }

    public List<Vector2> GetPoints(List<Tile> tiles)
    {
        List<Vector2> result = new List<Vector2>();

        foreach (Tile t in tiles)
        {
            result.Add(new Vector2(t.Position.x, t.Position.y));
        }

        return result;
    }
}
