using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Transform canvas;
    public ButtonControll[] Btn;
    public Transform HpBar;                                  // 캐릭터 체력 Hp바
    private GameObject go;
    //ArrayList CharacterPrefabs { get; } = new ArrayList();   // ArrayList 사용시 접근을 할수는 있으나 이게 효율적인가? -> 배열로 변경 후 앞 뒤가 비어있는지를 확인하는 방식으로 진행해볼예정
    public GameObject[] CharacterPrefabs;

    public static int selected = 0;
    public static int characterCount;
    public float []Hp;
    //public bool[] SkillFlag;
    int temp = 0;
    //int skillCheck;

    delegate void Skill();
    Skill[] skills;

    void Start()
    {
        InitCharacter();
        ChangeHp();
        for (int i = 0; i < characterCount; i++)  // 던전매니저에서 처음에 정보를 받아오자
        {
            for(int j =0; j < DungeonManager.Instance.characterLists[i].SkillFlag.Length; j++)
            {
                DungeonManager.Instance.characterLists[i].SkillFlag[j] = true;
            }
            go = Instantiate(DungeonManager.Instance.characterLists[i].CharacterPrefab);
            go.transform.parent = canvas;
            Hp[i] = DungeonManager.Instance.characterLists[i].Hp;
            CharacterPrefabs[i] = go;
            go.transform.GetChild(5).gameObject.SetActive(false);
            //CharacterPrefabs.Add(go);
        }

        //((GameObject)CharacterPrefabs[selected]).SetActive(true);
        CharacterPrefabs[selected].transform.GetChild(5).gameObject.SetActive(true);

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

    void InitCharacter()
    {
        characterCount = 3;
        for (int i = 0; i < characterCount; i++)
        {
            Hp[i] = DungeonManager.Instance.characterLists[i].Hp;
            //CharacterPrefabs[i].GetComponent<BoxCollider2D>().enabled = true;
            Btn[i].CharacterChangeBtn.interactable = true;
            DungeonManager.Instance.characterLists[i].life = true;
        }
    }

    public void GetInjured(float Damage)
    {
        Hp[selected] -= Damage;
        HpBar.GetComponent<Slider>().value = Hp[selected];

        if (Hp[selected] <= 0)
        {
            DeathChange();
        }
    }

    public void ChangeHp()
    {
        HpBar.GetComponent<Slider>().maxValue = DungeonManager.Instance.characterLists[selected].Hp;
        HpBar.GetComponent<Slider>().value = Hp[selected];
    }

    public void Change()
    {
        //CharacterPrefabs[temp].GetComponent<Animator>().SetTrigger("Exit_t");
        CharacterPrefabs[temp].transform.GetChild(5).gameObject.SetActive(false);
        CharacterPrefabs[selected].transform.GetChild(5).gameObject.SetActive(true);
        ChangeHp();
        temp = selected;
    }

    public void Select(int num)
    {
        selected = num;
        if (selected != temp){ Change(); }
    }
    
    public void DeathChange()
    {
        --characterCount;
        CharacterPrefabs[selected].GetComponent<BoxCollider2D>().enabled = false;
        CharacterPrefabs[selected].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Die_t");
        Btn[selected].CharacterChangeBtn.interactable = false;
        //Destroy(CharacterPrefabs[selected].gameObject, 0.5f);
        DungeonManager.Instance.characterLists[selected].life = false;
        CharacterPrefabs[selected].SetActive(false);

        //비효율적인 캐릭터 교체 방식임 캐릭터가 사망시 그 다음차레의 캐릭터로 변경하는 방식으로 변경
        // 자신 인덱스 기준으로 앞에 인덱스를 먼저 체크 앞 배열 인덱스에 캐릭터가 있을경우 교체 없을경우 뒤 인덱스를 체크 
        // 만약 자신이 0번 인덱스일 경우 해당 배열의 마지막 인덱스를 확인 있을 경우 그 캐릭터와 교체 아닐경우 뒤 인덱스를 체크
        // 만약 자신이 마지막 인덱스일 경우 앞에 배열 인덱스 캐릭터의 존재를 체크하고 없으면 해당 배열의 최초 인덱스 체크
        // 아래에 있는 if-else 문은 임시로써 변경 예정임

        if (characterCount == 0)
        {
            DungeonManager.Instance.CallGameOver();
        }
        else if(selected == 0)
        {
            if(DungeonManager.Instance.characterLists[2].life != false)
            {
                CharacterPrefabs[2].SetActive(true);
                selected = 2;
            }
            else
            {
                CharacterPrefabs[1].SetActive(true);
                selected = 1;
            }
        }
        else if(selected == 2)
        {
            if(DungeonManager.Instance.characterLists[1].life != false)
            {
                CharacterPrefabs[1].SetActive(true);
                selected = 1;
            }
            else
            {
                CharacterPrefabs[0].SetActive(true);
                selected = 0;
            }
        }
        else
        {
            if(DungeonManager.Instance.characterLists[0].life != false)
            {
                CharacterPrefabs[0].SetActive(true);
                selected = 0;
            }
            else
            {
                CharacterPrefabs[2].SetActive(true);
                selected = 2;
            }
        }
        temp = selected;
        ChangeHp();
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[0] == true)
            StartCoroutine(AttackDelay(0.2f));
    }

    public void Shield()
    {
        //CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Shield_t");
        StartCoroutine(ShieldTime());
    }

    public void UseSkill(int index)
    {
        skills[index]();
    }

    // 델리게이트로 구성하였음
    void Skill_1()    // 스킬 딜레이가 적용되지않는 현상 발견
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[1] == true)
        {
            StartCoroutine(SkilDelay_1(5f));
        }
    }
    void Skill_2()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[2] == true)
        {
            StartCoroutine(SkilDelay_2(5f));
        }
    }
    void Skill_3()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[3] == true)
        {
            StartCoroutine(SkilDelay_3(5f));
        }
    }

    IEnumerator AttackDelay(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[0] = false;
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Attack_t");
        CharacterPrefabs[selected].transform.Find("Weapon").gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        CharacterPrefabs[selected].transform.Find("Weapon").gameObject.SetActive(false);
        //yield return new WaitForSeconds(time);
        DungeonManager.Instance.characterLists[selected].SkillFlag[0] = true;
    }
    // 딜레이 부분 하나로 통합 할수있지 않을까?
    IEnumerator SkilDelay_1(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[1] = false;
        //StartCoroutine(CoolTime(1, time));
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill1_t");
        CharacterPrefabs[selected].transform.Find("Skill_1").gameObject.SetActive(true);
        Debug.Log("체크 1");
        yield return new WaitForSeconds(1f);
        Debug.Log("체크 2");
        CharacterPrefabs[selected].transform.Find("Skill_1").gameObject.SetActive(false);
        Debug.Log("체크 3");
        yield return new WaitForSeconds(time-1);
        Debug.Log("체크 4");

        //yield return new WaitForSeconds(time);
        Debug.Log(DungeonManager.Instance.characterLists[selected].SkillFlag[1]);
        DungeonManager.Instance.characterLists[selected].SkillFlag[1] = true;
        Debug.Log(DungeonManager.Instance.characterLists[selected].SkillFlag[1]);
    }

    IEnumerator SkilDelay_2(float time)
    {
        //DungeonManager.Instance.characterLists[selected].SkillFlag[2] = false;
        StartCoroutine(CoolTime(2, time));
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill2_t");
        CharacterPrefabs[selected].transform.Find("Skill_2").gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        CharacterPrefabs[selected].transform.Find("Skill_2").gameObject.SetActive(false);
        //yield return new WaitForSeconds(time);
        //DungeonManager.Instance.characterLists[selected].SkillFlag[2] = true;
    }

    IEnumerator SkilDelay_3(float time)
    {
        //DungeonManager.Instance.characterLists[selected].SkillFlag[3] = false;
        StartCoroutine(CoolTime(3, time));
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill3_t");
        CharacterPrefabs[selected].transform.Find("Skill_3").gameObject.SetActive(true);
        // 투사체 혹은 오브젝트에 충돌이 일어났는지 체크가 필요함
        // 충돌시 바로 오브젝트가 setActive(false)가 됨 -> 해당 오브젝트 내부에서 판단
        // 충돌이 없을시 정해진 시간후 사라짐
        // 충돌한 스킬이 단일스킬인지 아닌지의 체크도 필요함 -> 해당 오브젝트 내부에서 판단

        yield return new WaitForSeconds(1f);
        CharacterPrefabs[selected].transform.Find("Skill_3").gameObject.SetActive(false);
        //DungeonManager.Instance.characterLists[selected].SkillFlag[3] = true;
    }

    // 회피 방식 변경할것
    IEnumerator ShieldTime()
    {
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Enemy";
        //CharacterPrefabs[selected].transform.tag = "Enemy";
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Shield_t");
        CharacterPrefabs[selected].transform.Find("Shield").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        CharacterPrefabs[selected].transform.Find("Shield").gameObject.SetActive(false);
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Player";
        //CharacterPrefabs[selected].transform.tag = "Player";
    }

    IEnumerator CoolTime(int index,float time)  // 일단 미사용
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[index] = false;
        yield return new WaitForSeconds(time);
        Debug.Log("탈출");
        DungeonManager.Instance.characterLists[selected].SkillFlag[index] = true;
    }

    [System.Serializable]
    public class ButtonControll
    {
        public Button CharacterChangeBtn;
    }
}

// 캐릭터 컨트룰에 HP 컨트룰도 합칠수 있지 않을까?