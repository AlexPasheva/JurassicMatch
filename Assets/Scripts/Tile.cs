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


    enum Dinosaur {
        Stegosaurus,
        Velociraptor
    }

    public void Init()
    {
        _renderer.flipX = getRandomBoolean();
        _renderer.flipY = getRandomBoolean();
        _dinosaurTile.GetComponent<SpriteRenderer>().sprite = _dinoArray[Random.Range(0,_dinoArray.Length)];

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