using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public GameObject canvas;
    private GameObject go;
    public ButtonControll Btn;
    public static int selected = 0;
    public int skilIndex;
    public static int characterCount = 3;
    int temp = 0;
    int SkilNum = 0;
    ArrayList characterPrefabs = new ArrayList();   // ArrayList 사용시 접근을 할수는 있으나 이게 효율적인가?
    //List<GameObject>
    delegate void Skill();
    Skill[] skills;
    //delegate void Delay();
    //Delay[] delays;

    void Start()
    {
        characterCount = 3;

        for (int i = 0; i < DungeonGameManager.Instance.characterList.Length; i++)
        {
            go = Instantiate(DungeonGameManager.Instance.characterList[i].CharacterPrefab);
            go.transform.parent = canvas.transform;
            go.SetActive(false);
            characterPrefabs.Add(go);
        }
    
        ((GameObject)characterPrefabs[selected]).SetActive(true);

        skills = new Skill[]
        {
            new Skill(Skill_1),
            new Skill(Skill_2),
            new Skill(Skill_3)
        };

        /*delays = new Delay[]  // 코루틴은 딜리게이트 불가능인가?
        {
            new Delay(AttackDelay(0.3f)),
            new Delay(ShieldTime),
            new Delay(skill)
        };*/
    }

    public void Change()
    {
        ((GameObject)characterPrefabs[temp]).SetActive(false);
        ((GameObject)characterPrefabs[selected]).SetActive(true);
        temp = selected;
    }

    public void Select(int num)
    {
        selected = num;
        if (selected != temp){ Change(); }
    }
    
    public void DeathChange()
    {
        /*Debug.Log(characterCount);
        for(int i = 0; i < character.Length; i++)
        {
            //리스트 사용 방법으로 변경 가능하면 바꿀것
            //character[1].character.SetActive(true);   // 이렇게 하면 되긴 하는데 의미가 없다 로그를 찍어보자
            //Debug.Log(character[i].character.gameObject);

            for(int j = 0; j<character.Length; j++)
            {
                if (character[j].character.gameObject == null || character[j].character.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().hp <= 0)
                {
                    //Btn.Board.transform.GetChild(j).GetComponent<Button>().gameObject.SetActive(false);
                    Btn.Board.transform.GetChild(j).GetComponent<Button>().interactable = false;
                }
            }

            if (character[i].character.gameObject == null || character[i].character.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().hp <= 0)
            {
                //Debug.Log(Btn.Board.transform.GetChild(i).GetComponent<Button>().gameObject);
                //Btn.Board.transform.GetChild(i).GetComponent<Button>().gameObject.SetActive(false);
                continue;
            }

            //--characterCount;
            character[i].character.SetActive(true);
            temp = i;
            selected = i;
            break;
        }
        if (characterCount == 0)
        {
            DungeonGameManager.Instance.GameOver.SetActive(true);
        }*/
        ///////////////////////////////////////
        //비효율적인 캐릭터 교체 방식임 캐릭터가 사망시 그 다음차레의 캐릭터로 변경하는 방식으로 변경
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()
    {
        //character[temp].character.GetComponent<Animator>().Play("Attack");
        ((GameObject)characterPrefabs[temp]).GetComponent<Animator>().Play("Attack");
        StartCoroutine(AttackDelay(0.3f));
    }

    public void Shield()
    {
        ((GameObject)characterPrefabs[temp]).GetComponent<Animator>().Play("Shield");
        StartCoroutine(ShieldTime());
    }

    public void UseSkill(int index)
    {
        skills[index]();
    }

    // SKill_1~3 모두 같은 기능 쿨타임과 플레이하는 애니메이션 이름이 다름 이걸 통합해서 하는 방법은 없는가?
    void Skill_1()    // 스킬 딜레이가 적용되지않는 현상 발견
    {
        ((GameObject)characterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_1_t");
        StartCoroutine(SkilDelay_1(0.7f));
    }
    void Skill_2()
    {
        ((GameObject)characterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_2_t");
        StartCoroutine(SkilDelay_2(1.0f));
    }
    void Skill_3()
    {
        ((GameObject)characterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_3_t");
        StartCoroutine(SkilDelay_3(5.0f));
    }

    /*IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);        
        DeathChange();
    }*/

    IEnumerator AttackDelay(float time)
    {
        ((GameObject)characterPrefabs[temp]).transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        ((GameObject)characterPrefabs[temp]).transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }
    // 딜레이 부분 하나로 통합 할수있지 않을까?
    IEnumerator SkilDelay_1(float time)
    {
        ((GameObject)characterPrefabs[temp]).transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        ((GameObject)characterPrefabs[temp]).transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_2(float time)
    {
        ((GameObject)characterPrefabs[temp]).transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        ((GameObject)characterPrefabs[temp]).transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_3(float time)
    {
        ((GameObject)characterPrefabs[temp]).transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        ((GameObject)characterPrefabs[temp]).transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator ShieldTime()
    {
        ((GameObject)characterPrefabs[temp]).transform.tag = "Enemy";
        yield return new WaitForSeconds(1.0f);
        ((GameObject)characterPrefabs[temp]).transform.tag = "Player";
    }

    [System.Serializable]
    public class Character
    {
        public GameObject character;
    }

    [System.Serializable]
    public class ButtonControll
    {
        public GameObject Board;
    }
}