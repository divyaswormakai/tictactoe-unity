using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class gameControllerAI : MonoBehaviour
{
    public Button[] btns;
    public TextMeshProUGUI winnertxt;
    public Button replay;
    public Image winner;

    List<int> doneCircle;
    List<int> doneCross;

    // 0 for null 1 for player and 2 for bot
    int[] boardStates;
    bool winFlag = false;
    int count = 0;

    int currCrossIndex = 0;

    btnPressAI btnScript;

    void Start()
    {
        doneCircle = new List<int>();
        doneCross = new List<int>();

        boardStates = new int[10];

        btnScript = FindObjectOfType<btnPressAI>();

        for (int i = 0; i < 9; i++)
        {
            btns[i].GetComponentInChildren<Image>().sprite = null;
            boardStates[i] = 0;
        }
    }

    //This is setting og image for the player
    public void Mark(string n)
    {

        doneCircle.Add(int.Parse(n));
        boardStates[int.Parse(n)] = 1;
        count++;
        print("Done-circle");
        foreach (int i in doneCircle) { print(i); }
        print("--------");
        foreach (int i in boardStates) { print(i); }
    }

    //this is setting for the bot
    public void MarkInt(int index)
    {
        doneCross.Add(index);
        count++;
        btns[currCrossIndex].GetComponentInChildren<Image>().sprite = btnScript.GetSprite(2);
        boardStates[index] = 2;

        print("Done-Cross");
        // foreach (int i in doneCross) { print(i); }
        btns[currCrossIndex].interactable = false;
    }

    public void Check(int turn)
    {
        if (count >= 9)
        {
            replay.gameObject.SetActive(true);
        }
        List<int> currList = new List<int>();
        List<bool> boolList = new List<bool>();
        if (turn == 1)
        {
            currList = doneCircle;
        }
        else
        {
            currList = doneCross;
        }
        for (int i = 0; i < currList.Count; i++)
        {
            boolList.Add(false);
        }
        Subset(currList, 3, 0, 0, boolList);
        //If won
        if (winFlag)
        {
            foreach (Button btn in btns)
            {
                btn.interactable = false;
            }
            replay.gameObject.SetActive(true);

            winner.sprite = btnScript.GetSprite(turn);
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
            if (sum == 15)
            {
                winFlag = true;
            }
            return;
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

    //Check if the moves still remaining
    bool IsMoveLeft()
    {
        for (int i = 0; i < 9; i++)
        {
            if (btns[i].GetComponentInChildren<Image>().sprite == null)
            {
                return true;
            }
        }
        return false;
    }

    int Evaluate()
    {
        Sprite temp1, temp2, temp3;
        //row wise
        for (int i = 0; i <= 6; i += 3)
        {
            temp1 = btns[i].GetComponentInChildren<Image>().sprite;
            temp2 = btns[i + 1].GetComponentInChildren<Image>().sprite;
            temp3 = btns[i + 2].GetComponentInChildren<Image>().sprite;
            if (temp1 == temp2 && temp2 == temp3 && temp1 != null)
            {
                if (temp1.name == "cross3")
                {
                    return 10;
                }
                if (temp1.name == "circle3")
                {
                    return -10;
                }
            }
        }

        //columnwise
        for (int i = 0; i < 3; i++)
        {
            temp1 = btns[i].GetComponentInChildren<Image>().sprite;
            temp2 = btns[i + 3].GetComponentInChildren<Image>().sprite;
            temp3 = btns[i + 6].GetComponentInChildren<Image>().sprite;
            if (temp1 == temp2 && temp2 == temp3 && temp1 != null)
            {
                if (temp1.name == "cross3")
                {
                    return 10;
                }
                if (temp1.name == "circle3")
                {
                    return -10;
                }
            }
        }

        //diagonal
        temp1 = btns[0].GetComponentInChildren<Image>().sprite;
        temp2 = btns[4].GetComponentInChildren<Image>().sprite;
        temp3 = btns[8].GetComponentInChildren<Image>().sprite;
        if (temp1 == temp2 && temp2 == temp3 && temp1 != null)
        {
            if (temp1.name == "cross3")
            {
                return 10;
            }
            if (temp1.name == "circle3")
            {
                return -10;
            }
        }

        temp1 = btns[2].GetComponentInChildren<Image>().sprite;
        temp2 = btns[4].GetComponentInChildren<Image>().sprite;
        temp3 = btns[6].GetComponentInChildren<Image>().sprite;
        if (temp1 == temp2 && temp2 == temp3 && temp1 != null)
        {
            if (temp1.name == "cross3")
            {
                return 10;
            }
            if (temp1.name == "circle3")
            {
                return -10;
            }
        }

        return 0;
    }

    int MiniMax(int depth, bool isMax, int alpha, int beta)
    {
        int score = Evaluate();

        if (score == 10 || score == -10)
        {
            return score;
        }

        if (!IsMoveLeft())
        {
            return 0;
        }

        if (isMax)
        {
            int best = -1000;
            for (int i = 0; i < 9; i++)
            {
                //-----------------------replace btns with value arrays here too
                if (btns[i].GetComponentInChildren<Image>().sprite == null)       //empty
                {
                    btns[i].GetComponentInChildren<Image>().sprite = btnScript.GetSprite(2);          //set the value to cross

                    best = Math.Max(best, MiniMax(depth + 1, !isMax, alpha, beta));

                    btns[i].GetComponentInChildren<Image>().sprite = null;      //undo

                    alpha = Math.Max(alpha, best);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return best;
        }

        else
        {
            int best = 1000;
            for (int i = 0; i < 9; i++)
            {
                if (btns[i].GetComponentInChildren<Image>().sprite == null)       //empty
                {
                    btns[i].GetComponentInChildren<Image>().sprite = btnScript.GetSprite(1);          //set the value to cross

                    best = Math.Min(best, MiniMax(depth + 1, !isMax, alpha, beta));

                    btns[i].GetComponentInChildren<Image>().sprite = null;      //undo

                    beta = Math.Min(beta, best);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return best;
        }
    }

    //getsprite 1 ie circle is minimizing factor and 2 ie cross is maximizing
    public int FindBestSolution()
    {
        int bestVal = -1000;
        int bestMove = -1;

        for (int i = 0; i < btns.Length; i++)
        {
            //------------------------set value of array instead of image here
            if (btns[i].GetComponentInChildren<Image>().sprite == null)          //Emptyy
            {
                btns[i].GetComponentInChildren<Image>().sprite = btnScript.GetSprite(2);          //set the value to cross

                int moveVal = MiniMax(0, false, -1000, 1000);

                btns[i].GetComponentInChildren<Image>().sprite = null;      //undo

                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }
        currCrossIndex = bestMove;
        return Int32.Parse(btns[bestMove].name);

    }
}