using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    [SerializeField] private GameObject _gameBoard;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        _gameBoard.transform.position = InitPosition((float)_width, (float)_height);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init();
                spawnedTile.transform.SetParent(_gameBoard.transform);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = InitPosition((float)_width, (float)_height);
        _gameBoard.transform.Rotate(0, 0, 45, Space.Self);
    }

    private Vector3 InitPosition(float width, float height)
    {
        return new Vector3(width / 2 - 0.5f, height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}
