using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject dinosaurTile;

    [SerializeField] private Dinosaur dinosaurType;

    [SerializeField] private Sprite[] dinoArray;


    enum Dinosaur
    {
        Brachiosaurus,
        Stegosaurus,
        Trex,
        Triceratops
    }

    public void Init()
    {
        InitTile();
        InitDinosaur();
    }

    private void InitTile()
    {
        renderer.flipX = getRandomBoolean();
        renderer.flipY = getRandomBoolean();
    }
    private void InitDinosaur()
    {
        Dinosaur randomDinosaur = getRandomDinosaurType();
        dinosaurTile.GetComponent<SpriteRenderer>().sprite = dinoArray[(int)randomDinosaur];
        dinosaurType = randomDinosaur;
    }

    private Dinosaur getRandomDinosaurType()
    {
        return (Dinosaur)Random.Range(0, System.Enum.GetValues(typeof(Dinosaur)).Length);
    }

    private bool getRandomBoolean()
    {
        return Random.value > 0.5f;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}