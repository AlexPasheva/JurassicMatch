using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [SerializeField] private List<Tile> tilesToMove;

    Dictionary<Vector2, Direction> directions;

    //[SerializeField] private List<Tile> tilesToMoveList; //visualisation purposes

    [SerializeField] private List<Vector2> trajectory;

    private enum Direction
    {
        SouthEast,
        NorthEast,
        SouthWest,
        NorthWest
    }

    void Start()
    {
        tilesToMove = new List<Tile>(tilesToMove);

        directions = new Dictionary<Vector2, Direction>();

        directions[new Vector2(0, -1)] = Direction.SouthEast;
        directions[new Vector2(-1, 0)] = Direction.SouthWest;
        directions[new Vector2(1, 0)] = Direction.NorthEast;
        directions[new Vector2(0, 1)] = Direction.NorthWest;
    }

    private void MoveTiles(Queue<Tile> _tilesToMove)
    {
        tilesToMove = new List<Tile>(_tilesToMove);

        RandomizeTiles(tilesToMove);

        Direction generationDirection = ComputeTileGenerationDirection();
        Tile currentTile = tilesToMove[0];
        int x = (int)currentTile.Position.x;
        int y = (int)currentTile.Position.y;

        switch (generationDirection)
        {
            case Direction.SouthEast:
                tilesToMove = FillTilesToMoveFromSouthEast(currentTile);
                MoveDestroyedTiles(_tilesToMove, new Vector2(currentTile.Position.x, -1));
                break;

            case Direction.SouthWest:
                tilesToMove = FillTilesToMoveFromSouthWest(currentTile);
                MoveDestroyedTiles(_tilesToMove, new Vector2(-1, currentTile.Position.y));
                break;

            case Direction.NorthEast:
                tilesToMove = FillTilesToMoveFromNorthEast(currentTile);
                MoveDestroyedTiles(_tilesToMove, new Vector2(gridManager.Width, currentTile.Position.y));
                break;

            case Direction.NorthWest:
                tilesToMove = FillTilesToMoveFromNorthWest(currentTile);
                MoveDestroyedTiles(_tilesToMove, new Vector2(currentTile.Position.x, gridManager.Height));
                break;
        }


    }

    private List<Tile> FillTilesToMoveFromSouthEast(Tile currentTile)
    {
        Tile firstTile = currentTile;
        int y = (int)firstTile.Position.y;

        List<Tile> leftOverTiles = new List<Tile>();

        while (y > 0)
        {
            Vector2 nextTilePosition = new Vector2(currentTile.Position.x, currentTile.Position.y - 1);
            currentTile = gridManager.GetTile(new Vector2(currentTile.Position.x, currentTile.Position.y - 1));

            if (!currentTile.IsSelected)
            {
                leftOverTiles.Add(currentTile);
            }
            y--;
        }

        return leftOverTiles.Concat(tilesToMove).ToList();
    }

    private List<Tile> FillTilesToMoveFromSouthWest(Tile currentTile)
    {
        Tile firstTile = currentTile;
        int x = (int)firstTile.Position.x;

        List<Tile> leftOverTiles = new List<Tile>();

        while (x > 0)
        {
            Vector2 nextTilePosition = new Vector2(currentTile.Position.x - 1, currentTile.Position.y);
            currentTile = gridManager.GetTile(nextTilePosition);

            if (!currentTile.IsSelected)
            {
                leftOverTiles.Add(currentTile);
            }
            x--;
        }

        return leftOverTiles.Concat(tilesToMove).ToList();
    }

    private List<Tile> FillTilesToMoveFromNorthEast(Tile currentTile)
    {
        Tile firstTile = currentTile;
        int x = (int)firstTile.Position.x;

        List<Tile> leftOverTiles = new List<Tile>();

        while (x < gridManager.Height - 1)
        {
            Vector2 nextTilePosition = new Vector2(currentTile.Position.x + 1, currentTile.Position.y);
            currentTile = gridManager.GetTile(nextTilePosition);
            if (!currentTile.IsSelected)
            {
                leftOverTiles.Add(currentTile);
            }
            x++;
        }

        return leftOverTiles.Concat(tilesToMove).ToList();
    }
    private List<Tile> FillTilesToMoveFromNorthWest(Tile currentTile)
    {
        Tile firstTile = currentTile;
        int y = (int)firstTile.Position.y;

        List<Tile> leftOverTiles = new List<Tile>();

        while (y < gridManager.Width - 1)
        {
            Vector2 nextTilePosition = new Vector2(currentTile.Position.x, currentTile.Position.y + 1);
            currentTile = gridManager.GetTile(nextTilePosition);
            if (!currentTile.IsSelected)
            {
                leftOverTiles.Add(currentTile);
            }
            y++;
        }

        return leftOverTiles.Concat(tilesToMove).ToList();

    }

    private void MoveDestroyedTiles(Queue<Tile> destroyedTiles, Vector2 pos)
    {
        if (pos.x == -1)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x + i, pos.y)).transform.position;
                i--;
            }
        }
        else if (pos.x == gridManager.Width)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x + i, pos.y)).transform.position;
                i++;
            }
        }
        else if (pos.y == -1)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x, pos.y + i)).transform.position;
                i--;
            }
        }
        else if (pos.y == gridManager.Height)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x, pos.y + i)).transform.position;
                i++;
            }
        }

    }

    private Direction ComputeTileGenerationDirection()
    {
        Vector2 generateTilesFromDirection = tilesToMove[0].Position - tilesToMove[1].Position;
        return directions[generateTilesFromDirection];
    }

    private void RandomizeTiles(List<Tile> tiles)
    {
        foreach (Tile t in tiles)
        {
            t.Init();
        }
    }

    void Update()
    {
        //tilesToMoveList = new List<Tile>(tilesToMove);
    }

    private void OnEnable()
    {
        Selector.moveTiles += MoveTiles;
    }

    private void OnDisable()
    {
        Selector.moveTiles -= MoveTiles;
    }
}
