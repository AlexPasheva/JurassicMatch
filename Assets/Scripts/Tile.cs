using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject dinosaurTile;

    [SerializeField] private Dinosaur dinosaurType;

    [SerializeField] private Sprite[] dinoArray;

    private bool isSelected;
    private Vector2 position;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;

            if (value == false)
            {
                DisableHighlight();
            }
        }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public static event Action<Tile> tileClicked;

    public enum Dinosaur
    {
        Brachiosaurus,
        Stegosaurus,
        Trex,
        Triceratops
    }

    public Dinosaur DinosaurType
    {
        get { return dinosaurType; }
    }

    public void Init()
    {
        InitTile();
        InitDinosaur();
    }

    private void InitTile()
    {
        spriteRenderer.flipX = getRandomBoolean();
        spriteRenderer.flipY = getRandomBoolean();

        highlight.GetComponent<SpriteRenderer>().sortingLayerName = "Dinosaur";
    }
    private void InitDinosaur()
    {
        Dinosaur randomDinosaur = getRandomDinosaurType();
        dinosaurTile.GetComponent<SpriteRenderer>().sprite = dinoArray[(int)randomDinosaur];
        dinosaurType = randomDinosaur;
    }

    private Dinosaur getRandomDinosaurType()
    {
        return (Dinosaur)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Dinosaur)).Length);
    }

    private bool getRandomBoolean()
    {
        return UnityEngine.Random.value > 0.5f;
    }

    private void EnableHighlight()
    {
        highlight.SetActive(true);
    }

    private void DisableHighlight()
    {
        if (!isSelected)
        {
            highlight.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        EnableHighlight();
    }

    void OnMouseExit()
    {
        DisableHighlight();
    }

    void OnMouseDown()
    {
        tileClicked?.Invoke(this);
    }

}