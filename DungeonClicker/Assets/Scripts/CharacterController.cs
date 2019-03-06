using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //public Animator animator;

    public Character[] character;
    public static int selected = 0;
    public int skilIndex;
    public static int characterCount = 3;
    int temp = 0;
    //AnimationClip ani;

    void Start()
    {
        characterCount = 3;
        character[selected].character.SetActive(true);
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
            //character[1].character.SetActive(true);   // 이렇게 하면 되긴 하는데 의미가 없다 로그를 찍어보자
            //Debug.Log(character[i].character.gameObject);

            if (character[i].character.gameObject == null || character[i].character.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().hp <= 0)
            {
                continue;
            }

            character[i].character.SetActive(true);
            temp = i;
            break;
        }
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()    // 공격시 근접 무기라면 그냥 공격을 하고 원거리 무기라면 instantiate를 사용하여 발사시키는 형식으로 함수만들것
    {
        character[temp].character.GetComponent<Animator>().Play("Attack");
        StartCoroutine(AttackDelay(0.3f));
    }

    public void Shield()
    {
        character[temp].character.GetComponent<Animator>().Play("Shield");
        StartCoroutine(ShieldTime());
    }

    public void Special()
    {
        character[temp].character.GetComponent<Animator>().Play("Special");
        //character[temp].character.GetComponent<Collider2D>().enabled = false;
    }
    // 캐릭터 별로 스킬을 등록하자 -> 그냥 각각의 인덱스로 구분하면 될듯
    public void Skil(int num)
    {
        //character[temp].character.GetComponent<Animator>().Play("skill_1"); // 이렇게 이름으로 하면 고정되버린다
        //character[temp].character.GetComponent<Animation>().Animations[num];
        //character[temp].character.GetComponent<Animation>().Play(character[temp].characterSkil[num]);
        //ani = character[temp].characterSkil[num];
    }

    // 위에 구현막혀서 임시 구현
    public void Skil_1()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_1");
    }
    public void Skil_2()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_2");
    }
    public void Skil_3()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_3");
    }
    public void Skil_4()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_4");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        DeathChange();
    }

    IEnumerator AttackDelay(float time)
    {
        character[temp].Weapon.SetActive(true);
        yield return new WaitForSeconds(time);
        character[temp].Weapon.SetActive(false);
    }

    IEnumerator ShieldTime()
    {
        character[temp].character.transform.tag = "Enemy";
        yield return new WaitForSeconds(1.0f);
        character[temp].character.transform.tag = "Player";
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
        public GameObject Weapon;   
    }
}