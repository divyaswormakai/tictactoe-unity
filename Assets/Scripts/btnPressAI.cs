using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnPressAI : MonoBehaviour
{
    public Sprite circle;
    public Sprite cross;

    gameControllerAI gC;
    int count = 0;
    void Start()
    {
        gC = FindObjectOfType<gameControllerAI>();
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
        SceneManager.LoadScene("BotMain");
    }

    public void AssignNumber(Button gameobject)
    {
        gC.Mark(gameobject.name);
        gameobject.GetComponentInChildren<Image>().sprite = circle;
        
        gameobject.interactable = false;

        if (count > 4)
        {
            gC.Check(1);
            
        }
        count++;
        //Bot Turn
        int temp = gC.FindBestSolution();
        gC.MarkInt(temp);
        print(temp);
        if (count > 3)
        {
            gC.Check(2);
        }

        count ++;

    }
    //gcMark(btn.name,2) assign sprite of cross set interactable as false and gc.Check(2)

    public Sprite GetSprite(int turn)
    {
        if (turn == 1)
        {
            return circle;
        }
        return cross;
    }
}
