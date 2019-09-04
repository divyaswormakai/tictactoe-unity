using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] btns;
    List<string> done;
    List<string> undone;
    void Start()
    {
        done = new List<string>();
        undone = new List<string>();

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                string temp = i.ToString() + j.ToString();
                undone.Add(temp);
            }
        }
    }

    public void Check()
    {
        
    }
}
