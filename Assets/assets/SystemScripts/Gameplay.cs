using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public int currentRound; //현재 라운드
    public int PlayerHP = 100; //플레이어 체력
    public float HP_Decrease_multiplier = 1.0f; //체력감소 배율

    public float ATB = 0; //행동 게이지, AbilityPoint.
    public float ATB_Multiplier = 1.0f; //ATB 증가량 배율
    public float ATB_Increase; //실제 상승량.
    

    public int[] enemyActType = new int[]{1,2,3,4};

    public int turn;

    public Selecter actPanelSelecter;

    public int boost; //부스트.
    public float enemyPower = 10.0f; //적 고유 대미지


    int playerCondition; //바위 == 0 , 가위 == 1, 보 == 2, 3 --> 특수상태

    public EnemyAct enemyAct;
    public GameObject enemyPrefab;
    public GameObject enemyInstance;

    void Start()
    {
        boost = 1;
        turn = 0;
        currentRound = 0;
        enemyPower = 10.0f;

        TurnStart();
    }

    void Update()
    {
        


    }

    #region TurnManagement

    public void TurnStart() //행동 전 해야 할 일
    {
        turn++;
        enemyPower += 1.0f;
        SpawnEnemy();
        actPanelSelecter.is_being_controlled = true;
        playerCondition = 3;
        
    } //-> ui입력으로

    void SpawnEnemy()
    {
        // 적 인스턴스화
        enemyInstance = Instantiate(enemyPrefab, new Vector2(0, -15), Quaternion.identity);

        enemyAct = enemyInstance.GetComponent<EnemyAct>();
        enemyAct.gameplay = this;

    }

    public void TurnProcess() //행동 선택후 대결
    {//플레이어는 선택완료
        

        int enemyAtkValue = enemyAct.final_atk_value; //현재 적의 현재 공격에 대한 공격력
        int enemyAtkType = enemyAct.atk_Type; //현재공격의 타입.

       // if((enemyAtkType == 0) { }

    }

    public void TurnEnd() //대결 후
    {

    }

    #endregion

    #region PlayerAct

    public void Jump_Rock()
    {
        playerCondition = 0;
        TurnProcess();
    }
    public void Jump_Scissor()
    {
        playerCondition = 1;
        TurnProcess();
    }
    public void Jump_Paper()
    {
        playerCondition = 2;
        TurnProcess();
    }

    public void Stay()
    {
        TurnProcess();
    }

    public void Ability_Guard()
    {
        ATB -= 50;
        StartCoroutine(ChangeDmgMultiplier(0.5f, 25f)); //25초동안 DMG 배율 감소


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
        ATB -= 60;
        PlayerHP += 30;
    }

    public void Ability_Stop()
    {

    }

    #endregion

    #region enemyAct

    
    public void DetermineEnemyAct()
    {
        
    }



    #endregion
}
