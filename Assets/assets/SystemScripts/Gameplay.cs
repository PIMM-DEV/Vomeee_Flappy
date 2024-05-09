using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image 컴포넌트를 위해 필요
using TMPro;

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

    public Selecter actPanelSelecter; //하단 패널 포인터 클래스.

    public int boost; //부스트.
    public float enemyPower = 10.0f; //적 고유 대미지


    public int playerCondition; //바위 == 0 , 가위 == 1, 보 == 2, 3 --> 특수상태

    public EnemyAct enemyAct; //인스턴스의 클래스 저장용 변수
    public GameObject enemyPrefab; //사용하는 적
    public GameObject enemyInstance; //그 적의 인스턴스

    public GameObject player; //플레이어 오브젝트
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

    public void TurnStart() //행동 전 해야 할 일
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
        
    } //-> ui입력으로

    public Rigidbody2D EnemyRB; //프리팹 할당
    public BoxCollider2D EnemyBoxCollider;
    public Rigidbody2D PlayerRB;

    void SpawnEnemy()
    {
        // 적 인스턴스화
        enemyInstance = Instantiate(enemyPrefab, new Vector2(0, -25 + enemyPower * 0.05f), Quaternion.identity);

        enemyAct = enemyInstance.GetComponent<EnemyAct>();
        EnemyRB = enemyInstance.GetComponent<Rigidbody2D>();
        EnemyBoxCollider = enemyInstance.GetComponent<BoxCollider2D>();
        enemyAct.gameplay = this;

    }

    

    public void TurnProcess() //행동 선택후 대결
    {//플레이어는 선택완료
        enemyAct.SelectAbilities();
        

        int enemyAtkValue = enemyAct.final_atk_value; //현재 적의 현재 공격에 대한 공격력
        int enemyAtkType = enemyAct.atk_Type; //현재공격의 타입.
        string enemySkillName = enemyAct.atk_name;

        ShowSkillUI(enemyAtkType, enemySkillName);

       //아이사츠는 닌자들의 예절
       if((enemyAtkType == 0 && playerCondition == 2) || 
            (enemyAtkType == 1 && playerCondition == 0) || 
                (enemyAtkType == 2 && playerCondition == 1)) //플레이어의 승리
       {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y + 0.5f * boost) ; //플레이어 상승.
            ATB += ATB_Multiplier * 30;
            EnemyRB.velocity = new Vector2(30, -5); //사라져라
            
        }
        
        else if ((enemyAtkType == 0 && playerCondition == 0) ||
             (enemyAtkType == 1 && playerCondition == 1) ||
                 (enemyAtkType == 2 && playerCondition == 2)) //가위바위보 무승부.
        {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y + 0.5f * boost); //플레이어 상승.
            ATB += ATB_Multiplier * 20;
            StartCoroutine(WaitForCollide(3f));

            if (collided == true)
            {
                PlayerHP -= (int)(HP_Decrease_multiplier * enemyAtkValue); 
            }

        }

       else if ((enemyAtkType == 2 && playerCondition == 0) ||
                (enemyAtkType == 1 && playerCondition == 2) ||
                (enemyAtkType == 0 && playerCondition == 1)) //플레이어 패배
       {
            Vector2 currentPlayerPosition = player.transform.position;
            player.transform.position = new Vector2(currentPlayerPosition.x, currentPlayerPosition.y - 0.5f * boost); //플레이어 하강
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
        // 3초 후 로직이 필요할 경우 여기에 추가하면 됩니다.
        // 충돌 결과를 확인하는 메소드
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  // 플레이어와의 충돌 감지
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
        // 플레이어 스킬 UI 처리
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

        // 적 스킬 UI 처리
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

        StartCoroutine(ShowAndHideUI(3f)); // UI를 보여주고 숨기는 Coroutine 실행
    }

    IEnumerator ShowAndHideUI(float delay)
    {
        playerSkillUI.SetActive(true);
        EnemySkillUI.SetActive(true);
        yield return new WaitForSeconds(delay); // 3초간 대기
        playerSkillUI.SetActive(false);
        EnemySkillUI.SetActive(false);
    }

    public void TurnEnd() //대결 후
    {
        StartCoroutine(JustWait(5f));
        if (enemyInstance != null)
        {
            Destroy(enemyInstance);
        }
    }

    IEnumerator JustWait(float delay)
    {
        yield return new WaitForSeconds(delay); // 3초간 대기
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
