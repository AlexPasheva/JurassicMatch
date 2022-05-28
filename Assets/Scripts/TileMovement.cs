using System.Collections;
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
        //tilesToMoveList = new List<Tile>(tilesToMove);

        directions[new Vector2(-1, 0)] = Direction.SouthEast;
        directions[new Vector2(0, -1)] = Direction.SouthWest;
        directions[new Vector2(0, 1)] = Direction.NorthEast;
        directions[new Vector2(1, 0)] = Direction.NorthWest;
    }

    private void MoveTiles(Queue<Tile> _tilesToMove)
    {
        tilesToMove = new List<Tile>(_tilesToMove);
        Direction generationDirection = ComputeTileGenerationDirection();

        switch (generationDirection)
        {
            case Direction.SouthEast:

                Tile currentTile = tilesToMove[0];
                int x = (int)currentTile.Position.x;

                while (x < 9)
                {
                    Vector2 nextTilePosition = new Vector2(currentTile.Position.x + 1, currentTile.Position.y);
                    currentTile = gridManager.GetTile(nextTilePosition);
                    tilesToMove.Insert(0, currentTile);
                    x++;
                }

                break;
            case Direction.SouthWest:
                break;
            case Direction.NorthEast:
                break;
            case Direction.NorthWest:
                break;

        }
    }

    private Direction ComputeTileGenerationDirection()
    {
        Vector2 generateTilesFromDirection = tilesToMove[0].Position - tilesToMove[1].Position;
        return directions[generateTilesFromDirection];
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
