using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //public Animator animator;

    public Character[] character;
    public int selected = 0;
    int temp = 0;

    void Start()
    {
        //Instantiate(character[0].character, transform.position, transform.rotation);
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
        character[temp].character.GetComponent<Animator>().Play("skill_1");
    }

    [System.Serializable]
    public class Character
    {
        public GameObject character;
        public Animation[] characterSkil;
    }
}
