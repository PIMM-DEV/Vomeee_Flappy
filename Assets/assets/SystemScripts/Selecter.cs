using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selecter : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform recttransform;
    public Vector2 newPosition;
    int currselection;

    int[] posYs = new int[3] { 85, 0, -85 };

    public GameObject AbilityPanel;
    public GameObject JumpPanel;

    public Image this_image;

    public bool is_being_controlled;

    public Gameplay gameplay;

    void Start()
    {
        currselection = 0;
        is_being_controlled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && is_being_controlled)
        {
            if (currselection == 0) { }
            else
            {
                currselection--;
                recttransform.anchoredPosition = new Vector2(-150, posYs[currselection]);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && is_being_controlled)
        {
            if (currselection == 2) { }
            else
            {
                currselection++;
                recttransform.anchoredPosition = new Vector2(-150, posYs[currselection]);
            }

        }

        if(Input.GetKeyDown(KeyCode.Return) && is_being_controlled)
        {
            is_being_controlled = false;

            if(currselection == 0) //jump 선택
            {
                JumpPanel.SetActive(true);
                
            }

            if (currselection == 1) //ability 선택
            {
                AbilityPanel.SetActive(true);

            }

            if (currselection == 2) //ability 선택
            {
                gameplay.Stay();

            }

            gameplay.TurnProcess();
        }
    }
}
