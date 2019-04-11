﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    public float hp;
    public Transform myHp; //  자신의 슬라이더
    Slider MyHp;
    //public Slider hpUI; //  좌측 상단 표시용 슬라이더
    //public GameObject characterController;
    //public Text GameOver;
    
    public void Start()
    {
        MyHp = myHp.GetComponent<Slider>();
        hp = gameObject.transform.root.gameObject.transform.GetChild(0).GetComponent<StatusController>().Hp; // 스테이터스 컨트룰에서 HP값 받아오기
        //hp = DungeonGameManager.Instance.characterList[selected]
        //Debug.Log(hp);
        MyHp.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        MyHp.value = hp;

       if(hp <= 0)
        {
            CharacterController.characterCount--; //여기보단 컨트룰러에서 관리하는게 나을거 같음 -> 여기서 해당 정보로 움직이는게 있으니 여기가 나을수도
            //gameObject.transform.root.gameObject.layer = 11;
            gameObject.transform.root.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10),ForceMode2D.Impulse);
            //gameObject.tag = "untagged";
            if (CharacterController.characterCount == 0) //여기서 공중으로 상승시키면 될듯
            {
                //GameOver.gameObject.SetActive(true);
                gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");
                StartCoroutine(Delay());
                Destroy(gameObject.transform.root.gameObject, 0.5f);
            }

            //gameObject.transform.GetComponent<Rigidbody2D>() = new Vector2(0, 1);
            DungeonGameManager.Instance.manage.controller[0].GetComponent<CharacterController>().DeathChange();
            gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");
            Destroy(gameObject.transform.root.gameObject, 0.5f);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.485f);
        Time.timeScale = 0.0f;
    }
}
