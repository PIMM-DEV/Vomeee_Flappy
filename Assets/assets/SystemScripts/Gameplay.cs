using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public int currentRound = 0;
    public int PlayerHP = 100;
    public float ATB = 0;
    public float ATB_Multiplier = 1.0f;
    public float ATB_Increase;

    public int[] enemyActType = new int[]{1,2,3,4};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ATB_Increase = ATB_Multiplier * 1.0f;
        ATB += ATB_Increase;


    }


    void Jump_Rock()
    {

    }
    void Jump_Scissor()
    {

    }
    void Jump_Paper()
    {

    }

    void Stay()
    {

    }

    public void Ability_Guard()
    {

    }

    public void Ability_Cure()
    {

    }

    public void Determine_Enemy_Act()
    {

    }
}
