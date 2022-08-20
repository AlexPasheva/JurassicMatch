using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _dinosaurTile;

    [SerializeField] private Dinosaur _dinosaurType;

    [SerializeField] private Sprite[] _dinoArray;


    

    public void Init()
    {
        InitTile();
        InitDinosaur();
    }

    private void InitTile()
    {
        _renderer.flipX = getRandomBoolean();
        _renderer.flipY = getRandomBoolean();
    }
    private void InitDinosaur()
    {
        Dinosaur randomDinosaur = getRandomDinosaurType();
        _dinosaurTile.GetComponent<SpriteRenderer>().sprite = _dinoArray[(int)randomDinosaur];
        _dinosaurType = randomDinosaur;
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
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}