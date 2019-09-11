using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    public Button[] btns;
    public TextMeshProUGUI info,winnertxt;
    public Button replay;
    public Image winner;

    List<int> doneCircle;
    List<int> doneCross;
    bool winFlag = false;
    int count = 0;
    void Start()
    {
        doneCircle = new List<int>();
        doneCross = new List<int>();

        info.text = "Players' Turn";
    }

    public void Mark(string n,int turn)
    {
        if(turn == 1)
        {
            doneCircle.Add(int.Parse(n));
            info.text = "Players' turn";
        }
        else
        {
            doneCross.Add(int.Parse(n));
            info.text = "Players' turn";
        }
        count++;
    }

    public void Check(int turn)
    {
        if (count >= 9)
        {
            replay.gameObject.SetActive(true);
        }
        List<int> currList = new List<int>();
        List<bool> boolList = new List<bool>();
        if(turn == 1)
        {
            currList = doneCircle;
        }
        else
        {
            currList = doneCross;
        }
        for(int i = 0; i < currList.Count; i++)
        {
            boolList.Add(false);
        }
        Subset(currList, 3, 0, 0, boolList);
        if (winFlag)
        {
            foreach(Button btn in btns)
            {
                btn.interactable = false;
            }
            replay.gameObject.SetActive(true);

            winner.sprite = FindObjectOfType<btnPress>().GetSprite(turn);
            winner.gameObject.SetActive(true);
            winnertxt.gameObject.SetActive(true);
        }
    }

    public void Subset(List<int> A, int k, int start, int currLen, List<bool> used)
    {
        List<int> temp = new List<int>();
        if (currLen == k)
        {
            for (int i = 0; i < A.Count; i++)
            {
                if (used[i] == true)
                {
                    temp.Add(A[i]);
                }
            }
            int sum = 0;
            foreach (int j in temp)
            {
                sum += j;
            }
            if(sum == 15)
            {
                winFlag = true;
            }
            return ;
        }

        if (start == A.Count)
        {
            return;
        }
        // For every index we have two options,
        // 1.. Either we select it, means put true in used[] and make currLen+1
        used[start] = true;
        Subset(A, k, start + 1, currLen + 1, used);
        // 2.. OR we dont select it, means put false in used[] and dont increase
        // currLen
        used[start] = false;
        Subset(A, k, start + 1, currLen, used);
    }

}
