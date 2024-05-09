using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    public List<Ability> abilities;

    public float enemyPower;
    public int numberOfAbilitiesToSelect;

    public int atk_Value;

    public int final_atk_value;
    public int atk_Type;
    public string atk_name;

    public Gameplay gameplay;
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        InitializeAbilities();
        //SelectAbilities();
    }

    private void SetValues()
    {
        enemyPower = gameplay.enemyPower;
        numberOfAbilitiesToSelect = (int)enemyPower / 10;

        if(numberOfAbilitiesToSelect > 6 )
        {
            numberOfAbilitiesToSelect = 6;
        } //최대 허용 갯수
    }

    private void InitializeAbilities()
    {
        List<Ability> allAbilities = new List<Ability>
        {
        // 모든 가능한 능력을 여기에 추가
    
        new Break_R(),
        new Break_S(),
        new Break_P(),
        new Onslaught_R(),
        new Onslaught_S(),
        new Onslaught_P()

        //new Ex_Skill()
        };

        abilities = allAbilities.OrderBy(x => Random.value).Take(numberOfAbilitiesToSelect).ToList();
        abilities.Add(new Rush_R());
        abilities.Add(new Rush_S());
        abilities.Add(new Rush_P());
        //list완성
    }



    public void SelectAbilities() //gameplay에서 직접 실행.
    {
        ShuffleAbilities(); //섞고
        final_atk_value = abilities[0].getAtkValue(enemyPower); // 1번 스킬 활성화
        atk_Type = abilities[0].getAtkType();
        atk_name = abilities[0].getSkillName();
    }

    private void ShuffleAbilities()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            Ability temp = abilities[i];
            int randomIndex = Random.Range(0, abilities.Count);
            abilities[i] = abilities[randomIndex];
            abilities[randomIndex] = temp;
        }
    }

}

public abstract class Ability
{
    public abstract int getAtkValue (float enemyPower);
    public abstract int getAtkType ();

    public abstract string getSkillName();
}

public class Rush_R : Ability
{
    public override int getAtkValue(float enemyPower)
    {
        // 추가적인 활성화 로직

        return (int)enemyPower; // 곱하기 1배
    }

    public override int getAtkType()
    {
        return 0;
    }

    public override string getSkillName()
    {
        return "Rush Rock";
    }
}

public class Rush_S : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower; // 곱하기 1배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 1;
    }

    public override string getSkillName()
    {
        return "Rush Scissor";
    }
}

public class Rush_P : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower; // 곱하기 1배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 2;
    }
    public override string getSkillName()
    {
        return "Rush Paper";
    }
}

public class Break_R : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower * 2; // 곱하기 2배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 0;
    }
    public override string getSkillName()
    {
        return "Breaker Rock";
    }
}

public class Break_S : Ability
{
    public override int getAtkValue(float enemyPower)
    {
       return (int)enemyPower * 2;
    }

    public override int getAtkType()
    {
        return 1;
    }
    public override string getSkillName()
    {
        return "Breaker Scissor";
    }
}

public class Break_P : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower * 2; // 곱하기 2배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 2;
    }
    public override string getSkillName()
    {
        return "Breaker Paper";
    }
}

public class Onslaught_R : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower * 3; // 곱하기 3배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 0;
    }
    public override string getSkillName()
    {
        return "Onslaught Rock";
    }
}

public class Onslaught_S : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower * 3; // 곱하기 3배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 1;
    }
    public override string getSkillName()
    {
        return "Onslaught Scissor";
    }
}

public class Onslaught_P : Ability
{
    public override int getAtkValue(float enemyPower)
    {

        return (int)enemyPower * 3; // 곱하기 3배
        // 추가적인 활성화 로직
    }

    public override int getAtkType()
    {
        return 2;
    }
    public override string getSkillName()
    {
        return "Onslaught Paper";
    }
}