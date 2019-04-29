using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Damage;            // 공격 데미지
    public float Hp;                // 체력
    public float Speed;             // 움직임 속도
    public float Gold;              // 사망시 주는 골드
    public float AntiElasticity;    // 반탄력

    void Start()
    {

    }
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * -Speed * Time.timeScale);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") { return; }
        else if (col.gameObject.tag == "Player")
        {
            //col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().GainDamage(Damage);
            DungeonGameManager.Instance.CharacterController.GetComponent<CharacterController>().GetInjured(Damage);
            GetComponent<Rigidbody2D>().AddForce(transform.right * AntiElasticity);
        }
        else if(col.gameObject.tag == "Boss") { return; }
        else { return; }
    }
}

// Hp 관련과 병합하는것도 나쁘지 않을것 같다
