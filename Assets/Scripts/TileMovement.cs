using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    int count = 0;
    [SerializeField] private GridManager gridManager;

    [SerializeField] private List<Tile> tilesToMove;

    private Dictionary<Vector2, Direction> directions;

    [SerializeField] private Dictionary<Tile, Queue<Vector2>> paths;

    private int destroyedTilesCount;

    //[SerializeField] private List<Tile> tilesToMoveList; //visualisation purposes

    [SerializeField] private List<Vector2> trajectory;

    private enum Direction
    {
        SouthEast,
        NorthEast,
        SouthWest,
        NorthWest
    }

    Dictionary<Tile, int> j;

    void Start()
    {
        tilesToMove = new List<Tile>(tilesToMove);
        paths = new Dictionary<Tile, Queue<Vector2>>();
        j = new Dictionary<Tile, int>();
        directions = new Dictionary<Vector2, Direction>();

        directions[new Vector2(0, -1)] = Direction.SouthEast;
        directions[new Vector2(-1, 0)] = Direction.SouthWest;
        directions[new Vector2(1, 0)] = Direction.NorthEast;
        directions[new Vector2(0, 1)] = Direction.NorthWest;
    }

    private void MoveTilesToDesignatedPoint()
    {
        if (paths.Count != 0 && paths.Values.Last().Count != 0) // dali posledniq tile w opashkata ima oshte put da hodi
        {
            foreach (Tile t in tilesToMove)
            {
                Vector2 targetPoint = new Vector2();
                if (paths.Count > 0)
                {
                    targetPoint = paths[t].Peek();
                }
                t.transform.position = Vector2.MoveTowards(t.transform.position, gridManager.GetPoint(targetPoint).transform.position, Time.deltaTime * 5f);

                if (Vector2.Distance(t.transform.position, gridManager.GetPoint(targetPoint).transform.position) < .00001f)
                {
                    t.Position = targetPoint;
                    paths[t].Dequeue();
                }
            }
        }
        else
        {
            paths.Clear();
            tilesToMove.Clear();
            j.Clear();
        }
    }

    void Update()
    {

        if (paths.Count != 0)
            MoveTilesToDesignatedPoint();
    }

    private void MoveTiles(Queue<Tile> _tilesToMove)
    {
        tilesToMove = new List<Tile>(_tilesToMove);
        List<Vector2> destroyedTilesPositions = gridManager.GetPoints(tilesToMove);
        destroyedTilesPositions.Reverse();

        destroyedTilesCount = tilesToMove.Count;

        RandomizeTiles(tilesToMove);

        Direction generationDirection = ComputeTileGenerationDirection();
        Tile currentTile = tilesToMove[0];

        LineUpTiles(currentTile, generationDirection, _tilesToMove);

        List<Vector2> tilesToMovePositions = gridManager.GetPoints(tilesToMove);

        paths = SetToEveryTileMovementPath(destroyedTilesPositions.Concat(tilesToMovePositions).ToList());
    }

    private void LineUpTiles(Tile currentTile, Direction generationDirection, Queue<Tile> _tilesToMove)
    {
        int x = (int)currentTile.Position.x;
        int y = (int)currentTile.Position.y;

        switch (generationDirection)
        {
            case Direction.SouthEast:
                tilesToMove = FillTilesToMoveFromSouthEast(currentTile);
                SetDestroyedTilesPosition(_tilesToMove, new Vector2(currentTile.Position.x, -1));
                break;

            case Direction.SouthWest:
                tilesToMove = FillTilesToMoveFromSouthWest(currentTile);
                SetDestroyedTilesPosition(_tilesToMove, new Vector2(-1, currentTile.Position.y));
                break;

            case Direction.NorthEast:
                tilesToMove = FillTilesToMoveFromNorthEast(currentTile);
                SetDestroyedTilesPosition(_tilesToMove, new Vector2(gridManager.Width, currentTile.Position.y));
                break;

            case Direction.NorthWest:
                tilesToMove = FillTilesToMoveFromNorthWest(currentTile);
                SetDestroyedTilesPosition(_tilesToMove, new Vector2(currentTile.Position.x, gridManager.Height));
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

            if (currentTile != null && !currentTile.IsSelected)
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

    private void SetDestroyedTilesPosition(Queue<Tile> destroyedTiles, Vector2 pos)
    {
        if (pos.x == -1)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x + i, pos.y)).transform.position;
                t.Position = new Vector2(pos.x + i, pos.y);
                i--;
            }
        }
        else if (pos.x == gridManager.Width)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x + i, pos.y)).transform.position;
                t.Position = new Vector2(pos.x + i, pos.y);
                i++;
            }
        }
        else if (pos.y == -1)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x, pos.y + i)).transform.position;
                t.Position = new Vector2(pos.x, pos.y + i);
                i--;
            }
        }
        else if (pos.y == gridManager.Height)
        {
            int i = 0;
            foreach (Tile t in destroyedTiles)
            {
                t.transform.position = gridManager.GetPoint(new Vector2(pos.x, pos.y + i)).transform.position;
                t.Position = new Vector2(pos.x, pos.y + i);
                i++;
            }
        }

    }

    private Direction ComputeTileGenerationDirection()
    {
        Vector2 generateTilesFromDirection = tilesToMove[0].Position - tilesToMove[1].Position;
        return directions[generateTilesFromDirection];
    }

    Dictionary<Tile, Queue<Vector2>> SetToEveryTileMovementPath(List<Vector2> tilesToMovePoints)
    {
        Dictionary<Tile, Queue<Vector2>> path = new Dictionary<Tile, Queue<Vector2>>();

        int index = 0;

        for (int i = destroyedTilesCount - 1; i < tilesToMovePoints.Count - 1; i++)
        {
            Tile currentTile = tilesToMove[index];
            index++;
            path[currentTile] = new Queue<Vector2>();
            for (int j = 0; j < destroyedTilesCount; j++)
            {
                path[currentTile].Enqueue(tilesToMovePoints[i - j]);
            }
        }

        return path;
    }

    private void RandomizeTiles(List<Tile> tiles)
    {
        foreach (Tile t in tiles)
        {
            t.Init();
        }
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
