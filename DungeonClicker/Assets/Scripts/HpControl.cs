using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    public float hp;
    public Transform myHp; //  자신의 슬라이더
    Slider MyHp;
    public Slider hpUI; //  좌측 상단 표시용 슬라이더
    public GameObject characterController;
    
    public void Start()
    {
        MyHp = myHp.GetComponent<Slider>();
        MyHp.value = hp;
        //hpUI.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        Debug.Log(MyHp);
        MyHp.value = hp;
        //hpUI.value = hp;

        if(hp <= 0)
        {
            gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");   // 이 부분 에러남
            Destroy(gameObject.transform.root.gameObject, 0.5f);
        }
    }
}
