using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<Dinosaur, int> dinosaurList;
    public GameObject[]  slots;
    
    // Start is called before the first frame update
    void Start()
    {
        var slots = GameObject.FindGameObjectsWithTag("Slot");
        DisplayItems();
        dinosaurList = new Dictionary<Dinosaur, int>();
    }

    private void DisplayItems()
    {
        foreach (var dinosaur in dinosaurList)
        {
            slots[(int)dinosaur.Key].transform.GetChild(1).GetComponent<Text>().text = dinosaur.Value.ToString();
        }
    }

    private void DeductDinosaurs(Dinosaur _dino, int _dinoNumber)
    {
        dinosaurList[_dino] -= _dinoNumber;
        if (dinosaurList[_dino] < 0)
            dinosaurList[_dino] = 0;
        
        DisplayItems();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}
