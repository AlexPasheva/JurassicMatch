using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinosaurCollector : MonoBehaviour
{
    enum State
    {
        COLLECTING,
        EVERYTHING_COLLECTED
    }
    private State currentState = State.COLLECTING;

    public Dictionary<Dinosaur, int> dinosaurList;
    public GameObject[] slots;

    DinosaurCollector(Dictionary<Dinosaur, int> dinosaurList)
    {
        this.dinosaurList = dinosaurList;
    }

    void Start()
    {
        var slots = GameObject.FindGameObjectsWithTag("Slot");
        dinosaurList = new Dictionary<Dinosaur, int>();
        dinosaurList[Dinosaur.Brachiosaurus] = 30;
        dinosaurList[Dinosaur.Stegosaurus] = 50;
        dinosaurList[Dinosaur.Trex] = 40;
        dinosaurList[Dinosaur.Triceratops] = 20;
        DisplayItems();
    }

    private void DisplayItems()
    {
        foreach (var dinosaur in dinosaurList)
        {
            slots[(int)dinosaur.Key].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dinosaur.Value.ToString();
        }
    }

    public void DeductDinosaurs(Dinosaur _dino, int _dinoNumber)
    {
        if (currentState == State.COLLECTING)
        {
            dinosaurList[_dino] -= _dinoNumber;
            if (dinosaurList[_dino] < 0)
            {
                dinosaurList[_dino] = 0;
            }
        }

        if (AimIsReached())
        {
            currentState = State.EVERYTHING_COLLECTED;
        }

        DisplayItems();

    }

    public bool EverythingIsCollected()
    {
        return (currentState == State.EVERYTHING_COLLECTED);
    }

    private bool AimIsReached()
    {
        foreach (var dinosaur in dinosaurList)
        {
            if (dinosaur.Value != 0)
            {
                return false;
            }
        }

        return true;
    }

    private void OnEnable()
    {
        Selector.deductDinosaurs += DeductDinosaurs;
    }

    private void OnDisable()
    {
        Selector.deductDinosaurs -= DeductDinosaurs;
    }

}