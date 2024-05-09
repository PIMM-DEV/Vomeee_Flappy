using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image ������Ʈ�� ���� �ʿ�
using TMPro;

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

    public Selecter actPanelSelecter; //�ϴ� �г� ������ Ŭ����.

    public int boost; //�ν�Ʈ.
    public float enemyPower = 10.0f; //�� ���� �����


    public int playerCondition; //���� == 0 , ���� == 1, �� == 2, 3 --> Ư������

    public EnemyAct enemyAct; //�ν��Ͻ��� Ŭ���� ����� ����
    public GameObject enemyPrefab; //����ϴ� ��
    public GameObject enemyInstance; //�� ���� �ν��Ͻ�

    public GameObject player; //�÷��̾� ������Ʈ
    public string playerSkillName;

    public bool collided;


    void Start()
    {
        boost = 1;
        turn = 0;
        currentRound = 0;
        enemyPower = 10.0f;
        //collided = false;

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
        collided = false;

        if (enemyInstance == null)
        {
            SpawnEnemy();
        }
        actPanelSelecter.is_being_controlled = true;
        playerCondition = 3;
        
    } //-> ui�Է�����

    public Rigidbody2D EnemyRB; //������ �Ҵ�
    public BoxCollider2D EnemyBoxCollider;
    public Rigidbody2D PlayerRB;

    void SpawnEnemy()
    {
        // �� �ν��Ͻ�ȭ
        enemyInstance = Instantiate(enemyPrefab, new Vector2(0, -25 + enemyPower * 0.05f), Quaternion.identity);

        enemyAct = enemyInstance.GetComponent<EnemyAct>();
        EnemyRB = enemyInstance.GetComponent<Rigidbody2D>();
        EnemyBoxCollider = enemyInstance.GetComponent<BoxCollider2D>();
        enemyAct.gameplay = this;

    }

    

    public void TurnProcess() //�ൿ ������ ���
    {//�÷��̾�� ���ÿϷ�
        enemyAct.SelectAbilities();
        

        int enemyAtkValue = enemyAct.final_atk_value; //���� ���� ���� ���ݿ� ���� ���ݷ�
        int enemyAtkType = enemyAct.atk_Type; //��������� Ÿ��.
        string enemySkillName = enemyAct.atk_name;

        ShowSkillUI(enemyAtkType, enemySkillName);

       //���̻����� ���ڵ��� ����
       if((enemyAtkType == 0 && playerCondition == 2) || 
            (enemyAtkType == 1 && playerCondition == 0) || 
                (enemyAtkType == 2 && playerCondition == 1)) //�÷��̾��� �¸�
       {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y + 0.5f * boost) ; //�÷��̾� ���.
            ATB += ATB_Multiplier * 30;
            EnemyRB.velocity = new Vector2(30, -5); //�������
            
        }
        
        else if ((enemyAtkType == 0 && playerCondition == 0) ||
             (enemyAtkType == 1 && playerCondition == 1) ||
                 (enemyAtkType == 2 && playerCondition == 2)) //���������� ���º�.
        {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y + 0.5f * boost); //�÷��̾� ���.
            ATB += ATB_Multiplier * 20;
            StartCoroutine(WaitForCollide(3f));

            if (collided == true)
            {
                PlayerHP -= (int)(HP_Decrease_multiplier * enemyAtkValue); 
            }

        }

       else if ((enemyAtkType == 2 && playerCondition == 0) ||
                (enemyAtkType == 1 && playerCondition == 2) ||
                (enemyAtkType == 0 && playerCondition == 1)) //�÷��̾� �й�
       {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y - 0.5f * boost); //�÷��̾� �ϰ�
            ATB += ATB_Multiplier * 10;
            StartCoroutine(WaitForCollide(1f));

            if (collided == true)
            {
                PlayerHP -= (int)(HP_Decrease_multiplier * enemyAtkValue);
            }
       }
        else
        {
            StartCoroutine(WaitForCollide(1f));
            if (collided == true)
            {
                PlayerHP -= (int)(HP_Decrease_multiplier * enemyAtkValue);
            }
        }

        StartCoroutine(WaitForTurnEnd(1f));
        
       

    }
    IEnumerator WaitForCollide(float delay)
    {
        EnemyMove();
        Debug.Log("on");
        yield return new WaitForSeconds(delay);
        Debug.Log("off");
        // 3�� �� ������ �ʿ��� ��� ���⿡ �߰��ϸ� �˴ϴ�.
        // �浹 ����� Ȯ���ϴ� �޼ҵ�
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  // �÷��̾���� �浹 ����
        {
            collided = true;
        }
    }

    IEnumerator WaitForTurnEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnEnd();
    }

    public void EnemyMove()
    {
        EnemyRB.velocity = new Vector2(-100, -5);
    }

    public GameObject playerSkillUI;
    public GameObject EnemySkillUI;
    public Canvas playerSkillCanvas;
    public Canvas enemySkillCanvas;
    public void ShowSkillUI(int enemyAtkType, string enemySkillName)
    {
        // �÷��̾� ��ų UI ó��
        Image[] playerImages = playerSkillCanvas.GetComponentsInChildren<Image>();
        foreach (Image img in playerImages)
        {
            img.color = playerCondition switch
            {
                0 => Color.red,
                1 => Color.green,
                2 => Color.blue,
                _ => Color.yellow
            };
        }

        Text[] playerTexts = playerSkillCanvas.GetComponentsInChildren<Text>();
        foreach (Text txt in playerTexts)
        {
            txt.text = playerSkillName;
        }

        // �� ��ų UI ó��
        Image[] enemyImages = enemySkillCanvas.GetComponentsInChildren<Image>();
        foreach (Image img in enemyImages)
        {
            img.color = enemyAtkType switch
            {
                0 => Color.red,
                1 => Color.green,
                2 => Color.blue,
                _ => Color.yellow
            };
        }

        Text[] enemyTexts = enemySkillCanvas.GetComponentsInChildren<Text>();
        foreach (Text txt in enemyTexts)
        {
            txt.text = enemySkillName;
        }

        StartCoroutine(ShowAndHideUI(3f)); // UI�� �����ְ� ����� Coroutine ����
    }

    IEnumerator ShowAndHideUI(float delay)
    {
        playerSkillUI.SetActive(true);
        EnemySkillUI.SetActive(true);
        yield return new WaitForSeconds(delay); // 3�ʰ� ���
        playerSkillUI.SetActive(false);
        EnemySkillUI.SetActive(false);
    }

    public void TurnEnd() //��� ��
    {
        StartCoroutine(JustWait(5f));
        if (enemyInstance != null)
        {
            Destroy(enemyInstance);
        }
    }

    IEnumerator JustWait(float delay)
    {
        yield return new WaitForSeconds(delay); // 3�ʰ� ���
        Debug.Log("11234");
        
        TurnStart();
    }

    #endregion

    #region PlayerAct

    public void Jump_Rock()
    {
        playerCondition = 0;
        playerSkillName = "Jump Rock";
        TurnProcess();
    }
    public void Jump_Scissor()
    {
        playerCondition = 1;
        playerSkillName = "Jump Scissor";
        TurnProcess();
    }
    public void Jump_Paper()
    {
        playerCondition = 2;
        playerSkillName = "Jump Paper";
        TurnProcess();
    }

    public void Stay()
    {
        playerSkillName = "Stay";
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
