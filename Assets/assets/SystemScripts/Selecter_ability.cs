using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Selecter_ability : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform recttransform;
    int[] posYs = new int[] {305, 225, 145, 65, -15, -95};
    public int currselection;
    public Gameplay gameplay;

    public GameObject OriginalUI;

    public Selecter act_selecter;


    void Start()
    {
        recttransform.anchoredPosition = new Vector2(150, 305);
        currselection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(currselection == 0) { }
            else
            {
                currselection--;
                recttransform.anchoredPosition = new Vector2(150, posYs[currselection]);
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if(currselection == 5) { }
            else
            {
                currselection++;
                recttransform.anchoredPosition = new Vector2(150, posYs[currselection]);
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(currselection == 0)
            {
                gameplay.Ability_Guard();

                
            }
            else if(currselection == 1) 
            {
                gameplay.Ability_Cure();

                
            }
            else if (currselection == 2)
            {
                //gameplay.Ability_Stop();

                
            }
            else if (currselection == 3)
            {
                //gameplay.Ability_Boost();

                
            }
            else if (currselection == 4)
            {
                gameplay.Ability_Cure();

                
            }
            else if (currselection == 5)
            {
                gameplay.Ability_Cure();

                
            }

            act_selecter.is_being_controlled = true;
            OriginalUI.SetActive(false);
        }
    }


}
