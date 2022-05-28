using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private Queue<Tile> selectedElements;
    [SerializeField] private List<Tile> selectedElementsList;

    [SerializeField] private Tile previousTile;

    public static event Action<Queue<Tile>> moveTiles;

    void Start()
    {
        selectedElements = new Queue<Tile>();
        selectedElementsList = new List<Tile>();
    }

    void Update()
    {
        selectedElementsList = new List<Tile>(selectedElements); // this is just a hack. Will probably delete later
    }

    private void Select(Tile tile)
    {
        if ((selectedElements.Count > 0) && !CanBeAddedToCurrentChain(tile))
        {
            if (IsMatch(tile))
            {
                Debug.Log("Bam");
                foreach (Tile t in selectedElements)
                {
                    Debug.Log($"Destroyed tile {t.name}");
                    t.transform.position = new Vector2(42, 42);

                    moveTiles?.Invoke(selectedElements);
                }
            }

            foreach (Tile t in selectedElements)
            {
                t.IsSelected = false;
            }

            selectedElements = new Queue<Tile>();
        }

        selectedElements.Enqueue(tile);
        tile.IsSelected = true;
        previousTile = tile;
    }

    private bool IsMatch(Tile tile)
    {
        return tile == previousTile && selectedElements.Count >= 3;
    }

    private bool IsAdjecent(Tile first, Tile second)
    {
        return ((second.Position.x == first.Position.x + 1 && second.Position.y == first.Position.y) ||
        (second.Position.x == first.Position.x - 1 && second.Position.y == first.Position.y) ||
        (second.Position.y == first.Position.y + 1 && second.Position.x == first.Position.x) ||
        (second.Position.y == first.Position.y - 1 && second.Position.x == first.Position.x));
    }

    private bool CanBeAddedToCurrentChain(Tile tile)
    {
        return (tile.DinosaurType == previousTile.DinosaurType &&
                                     IsAdjecent(tile, previousTile) &&
                                     !selectedElements.Contains(tile) &&
                                     tile != previousTile);
    }

    private void OnEnable()
    {
        Tile.tileClicked += Select;
    }

    private void OnDisable()
    {
        Tile.tileClicked -= Select;
    }

}