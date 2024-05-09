using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public int currentRound; //���� ����
    public int PlayerHP = 100; //�÷��̾� ü��
    public float HP_Decrease_multiplier = 1.0f; //ü�°��� ����

    public float ATB = 0; //�ൿ ������, AbilityPoint.
    public float ATB_Multiplier = 1.0f; //ATB ������ ����
    public float ATB_Increase; //���� ��·�.
    

    public int[] enemyActType = new int[]{1,2,3,4};

    public int turn;

    public Selecter actPanelSelecter;

    public int boost; //�ν�Ʈ.
    public float enemyPower = 10.0f; //�� ���� �����


    int playerCondition; //���� == 0 , ���� == 1, �� == 2, 3 --> Ư������

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

    public void TurnStart() //�ൿ �� �ؾ� �� ��
    {
        turn++;
        enemyPower += 1.0f;
        SpawnEnemy();
        actPanelSelecter.is_being_controlled = true;
        playerCondition = 3;
        
    } //-> ui�Է�����

    void SpawnEnemy()
    {
        // �� �ν��Ͻ�ȭ
        enemyInstance = Instantiate(enemyPrefab, new Vector2(0, -15), Quaternion.identity);

        enemyAct = enemyInstance.GetComponent<EnemyAct>();
        enemyAct.gameplay = this;

    }

    public void TurnProcess() //�ൿ ������ ���
    {//�÷��̾�� ���ÿϷ�
        

        int enemyAtkValue = enemyAct.final_atk_value; //���� ���� ���� ���ݿ� ���� ���ݷ�
        int enemyAtkType = enemyAct.atk_Type; //��������� Ÿ��.

       // if((enemyAtkType == 0) { }

    }

    public void TurnEnd() //��� ��
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
        StartCoroutine(ChangeDmgMultiplier(0.5f, 25f)); //25�ʵ��� DMG ���� ����


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
