using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //public Animator animator;

    public Character[] character;
    public int selected = 0;
    public int skilIndex;
    int temp = 0;

    void Start()
    {
        //Instantiate(character[0].character, transform.position, transform.rotation);
        character[selected].character.SetActive(true);
        //ani = gameObject.GetComponent<Animation>();
    }

    public void Change()
    {
        character[temp].character.SetActive(false);
        character[selected].character.SetActive(true);
        temp = selected;
    }

    public void Select(int num)
    {
        selected = num;
        Change();
    }
    
    public void DeathChange()
    {
        for(int i = 0; i < character.Length; i++)
        {
            Debug.Log("접근");
            //character[1].character.SetActive(true);   // 이렇게 하면 되긴 하는데 의미가 없다 로그를 찍어보자
            //Debug.Log(character[i].character.gameObject);

            if (character[i].character == null)
            {
                Debug.Log("if 내부 접근");
                continue;
            }

            character[i].character.SetActive(true);
            temp = i;
            break;
        }
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()
    {
        character[temp].character.GetComponent<Animator>().Play("Attack");
    }

    public void Shield()
    {
        character[temp].character.GetComponent<Animator>().Play("Shield");
    }

    public void Special()
    {
        character[temp].character.GetComponent<Animator>().Play("Special");
    }
    // 캐릭터 별로 스킬을 등록하자 -> 그냥 각각의 인덱스로 구분하면 될듯
    public void Skil()
    {
        character[temp].character.GetComponent<Animator>().Play("skill_1"); // 이렇게 이름으로 하면 고정되버린다
    }

    IEnumerator Delay()
    {
        Debug.Log("실행");
        yield return new WaitForSeconds(0.3f);
        DeathChange();  // 이 부분이 실행이 안됨
        Debug.Log("실행 완료");
    }

    /*public void SkilSet(int skilIndex)
    {
        character[temp].character.GetComponent<Animation>().Play(character[temp].characterSkil[skilIndex])
    }*/
    // 인스펙터에서 애니메이션이 들어가지않는다
    [System.Serializable]
    public class Character
    {
        public GameObject character;
        public Animation[] characterSkil;
    }
}