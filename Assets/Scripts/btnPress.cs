using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPress : MonoBehaviour
{
    // Start is called before the first frame update
    static int turn = 1;
    gameController gC;
    void Start()
    {
        gC = FindObjectOfType<gameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignNumber(Button gameobject)
    {
        if (turn == 1)
        {
            gameobject.GetComponentInChildren<Text>().text = "X";
            turn = 2;
        }
        else
        {
            gameobject.GetComponentInChildren<Text>().text = "O";
            turn = 1;
        }
        gC.Check();
    }
}
