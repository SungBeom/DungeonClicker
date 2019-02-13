using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    public float hp;
    public Slider myHp; //  자신의 슬라이더
    public Slider hpUI; //  좌측 상단 표시용 슬라이더
    
    public void Start()
    {
        if(hpUI != null)    // myHp가 없는 경우는 있어도 hpUI가 없는 경우는 없다 -> 캐릭터는 2개 다 존재 몬스터는 ui만 있거나 없음
        {
            hpUI.value = hp;
            if(myHp != null){
                myHp.value = hp;
            }
        }
    }

    public void GainDamage(float damage)
    {
        hp -= damage;

        if (myHp != null)
        {
            myHp.value = hp;
        }
        //hpUI.value = hp;
        if(hp <= 0)
        {
            //gameObject.transform.root.GetComponent<Animation>().Play("Die");    // 이 부분 에러남
            Destroy(gameObject);
        }
    }
}
