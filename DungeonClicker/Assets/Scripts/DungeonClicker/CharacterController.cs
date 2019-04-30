using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public GameObject canvas;
    public ButtonControll Btn;
    public Transform HpBar;                                  // 캐릭터 체력 Hp바
    private GameObject go;
    //ArrayList CharacterPrefabs { get; } = new ArrayList();   // ArrayList 사용시 접근을 할수는 있으나 이게 효율적인가? -> 배열로 변경 후 앞 뒤가 비어있는지를 확인하는 방식으로 진행해볼예정
    public GameObject[] CharacterPrefabs;

    public static int selected = 0;
    public static int characterCount;
    public float []Hp;
    int temp = 0;

    delegate void Skill();
    Skill[] skills;

    void Awake()
    {
        characterCount = DungeonGameManager.Instance.characterList.Length;
        for (int i = 0; i < characterCount; i++)
        {
            Hp[i] = DungeonGameManager.Instance.characterList[i].Hp;
        }
    }

    void Start()
    {
        ChangeHp();
        for (int i = 0; i < characterCount; i++)  // 던전매니저에서 처음에 정보를 받아오자
        {
            go = Instantiate(DungeonGameManager.Instance.characterList[i].CharacterPrefab);
            go.transform.parent = canvas.transform;
            Hp[i] = DungeonGameManager.Instance.characterList[i].Hp;
            CharacterPrefabs[i] = go;
            go.SetActive(false);
            //CharacterPrefabs.Add(go);
        }

        //((GameObject)CharacterPrefabs[selected]).SetActive(true);
        CharacterPrefabs[selected].SetActive(true);

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

    public void DeleteArrayList()
    {
        //CharacterPrefabs.RemoveAt(selected);    // 장소를 제거하면 안됨 버튼 위치가 바뀌어 버림
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
        HpBar.GetComponent<Slider>().maxValue = DungeonGameManager.Instance.characterList[selected].Hp;
        HpBar.GetComponent<Slider>().value = Hp[selected];
    }

    public void Change()
    {
        //((GameObject)CharacterPrefabs[temp]).SetActive(false);
        //((GameObject)CharacterPrefabs[selected]).SetActive(true);
        CharacterPrefabs[temp].SetActive(false);
        CharacterPrefabs[selected].SetActive(true);
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
        ((GameObject)CharacterPrefabs[selected]).GetComponent<BoxCollider2D>().enabled = false;
        ((GameObject)CharacterPrefabs[selected]).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        ((GameObject)CharacterPrefabs[selected]).GetComponent<Animator>().SetTrigger("Die_t");
        //DeleteArrayList();
        //selected = (selected + CharacterPrefabs.Count) % CharacterPrefabs.Count;    // Divide 0 에러 발생 이유를 확인해보아야함
        //CharacterPrefabs[selected].GetComponent<BoxCollider2D>().enabled = false;
        //CharacterPrefabs[selected].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        //CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Die_t");
        //CharacterPrefabs[selected].SetActive(false);
        // 버튼 interactive 끄면 될듯
        ChangeHp();
        //((GameObject)CharacterPrefabs[selected]).SetActive(true);
        //비효율적인 캐릭터 교체 방식임 캐릭터가 사망시 그 다음차레의 캐릭터로 변경하는 방식으로 변경
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()
    {
        ((GameObject)CharacterPrefabs[temp]).GetComponent<Animator>().Play("Attack");
        StartCoroutine(AttackDelay(0.3f));
    }

    public void Shield()
    {
        ((GameObject)CharacterPrefabs[temp]).GetComponent<Animator>().Play("Shield");
        StartCoroutine(ShieldTime());
    }

    public void UseSkill(int index)
    {
        skills[index]();
    }

    // 델리게이트로 구성하였음
    void Skill_1()    // 스킬 딜레이가 적용되지않는 현상 발견
    {
        //((GameObject)CharacterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_1_t");
        CharacterPrefabs[temp].GetComponent<Animator>().SetTrigger("Skill_1_t");
        StartCoroutine(SkilDelay_1(0.7f));
    }
    void Skill_2()
    {
        //((GameObject)CharacterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_2_t");
        CharacterPrefabs[temp].GetComponent<Animator>().SetTrigger("Skill_2_t");
        StartCoroutine(SkilDelay_2(1.0f));
    }
    void Skill_3()
    {
        //((GameObject)CharacterPrefabs[temp]).GetComponent<Animator>().SetTrigger("Skill_3_t");
        CharacterPrefabs[temp].GetComponent<Animator>().SetTrigger("Skill_3_t");
        StartCoroutine(SkilDelay_3(5.0f));
    }

    /*IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);        
        DeathChange();
    }*/

    IEnumerator AttackDelay(float time)
    {
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        CharacterPrefabs[temp].transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        CharacterPrefabs[temp].transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }
    // 딜레이 부분 하나로 통합 할수있지 않을까?
    IEnumerator SkilDelay_1(float time)
    {
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        CharacterPrefabs[temp].transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        CharacterPrefabs[temp].transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_2(float time)
    {
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        CharacterPrefabs[temp].transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        CharacterPrefabs[temp].transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_3(float time)
    {
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        CharacterPrefabs[temp].transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        //((GameObject)CharacterPrefabs[temp]).transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        CharacterPrefabs[temp].transform.GetChild(5).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator ShieldTime()
    {
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Enemy";
        CharacterPrefabs[temp].transform.tag = "Enemy";
        yield return new WaitForSeconds(1.0f);
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Player";
        CharacterPrefabs[temp].transform.tag = "Player";
    }

    [System.Serializable]
    public class ButtonControll
    {
        public GameObject Board;
    }
}

// 캐릭터 컨트룰에 HP 컨트룰도 합칠수 있지 않을까?