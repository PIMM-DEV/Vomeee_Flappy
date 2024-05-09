using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform recttransform;
    public Vector2 newPosition;
    int currselection;

    int[] posYs = new int[3] { 85, 0, -85 };


    void Start()
    {
        currselection = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currselection == 0) { }
            else
            {
                currselection--;
                recttransform.anchoredPosition = new Vector2(-150, posYs[currselection]);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currselection == 2) { }
            else
            {
                currselection++;
                recttransform.anchoredPosition = new Vector2(-150, posYs[currselection]);
            }

        }

        if(Input.GetKeyDown(KeyCode.Return))
        {

        }
    }
}
