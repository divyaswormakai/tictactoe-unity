using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnPress : MonoBehaviour
{
    public Sprite circle;
    public Sprite cross;

    btnAnimation test;
    static int turn = 1;
    gameController gC;
    int count = 0;
    void Start()
    {
        gC = FindObjectOfType<gameController>();
        test = FindObjectOfType<btnAnimation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Main");
    }

    public void AssignNumber(Button gameobject)
    {
        gC.Mark(gameobject.name,turn);
        int newTurn = 0;
        if (turn == 1)
        {
            gameobject.GetComponentInChildren<Image>().sprite = circle;  
            newTurn = 2;
        }
        else
        {
            gameobject.GetComponentInChildren<Image>().sprite = cross;
            newTurn = 1;
        }
        test.toggle(turn);

        gameobject.interactable = false;

        count++;
        
        if (count > 4)
        {
            gC.Check(turn);
        }

        turn = newTurn;
    }

    public Sprite GetSprite(int turn)
    {
        if (turn == 1)
        {
            return circle;
        }
        return cross;
    }
}
