using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public int currentRound = 0;
    public int PlayerHP = 100;
    public float HP_Decrease_multiplier = 1.0f;

    public float ATB = 0;
    public float ATB_Multiplier = 1.0f;
    public float ATB_Increase;
    

    public int[] enemyActType = new int[]{1,2,3,4};

    public int turn = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ATB_Increase = ATB_Multiplier * 0.1f;
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
        ATB -= 50;
        StartCoroutine(ChangeDmgMultiplier(0.5f, 25f)); //5초동안 DMG 배율 감소


    }
    IEnumerator ChangeDmgMultiplier(float multiplier_change, float time)
    {
        float originalDmgMultiplier = HP_Decrease_multiplier;

        HP_Decrease_multiplier = multiplier_change;

        yield return new WaitForSeconds(time);

        HP_Decrease_multiplier = originalDmgMultiplier;
    }

    public void Ability_Cure()
    {
        PlayerHP += 30;
    }

    public void Ability_Stop()
    {

    }

    public void Determine_Enemy_Act()
    {

    }
}
