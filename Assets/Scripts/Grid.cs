using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;

    private Tile[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new Tile[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = new Tile(i, j);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetTile(int x, int y, Tile value)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y]?.ToString();
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null)
        {
            OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SeTile(Vector3 worldPosition, Tile value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetTile(x, y, value);
    }

    public Tile GetTile(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(Tile);
        }
    }

    public Tile GetTile(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetTile(x, y);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
}
