using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class btnAnimation : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle(int turn)
    {
        string clip = "";
        if (turn == 1)
        {
            clip = "cross";
        }
        else
        {
            clip = "circle";
        }
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.Play(clip);
    }
}
