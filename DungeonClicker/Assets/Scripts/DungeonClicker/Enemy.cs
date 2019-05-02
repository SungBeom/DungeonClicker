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
        //MapEnemyController 에서 값을 위에 해당하는 값을 받아오는 형식으로 하자
        //MapEnemyController 에서 해당하는 값에 어떻게 접근을 하지?
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * -Speed * Time.timeScale);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DungeonGameManager.Instance.Injured(Damage);
            GetComponent<Rigidbody2D>().AddForce(transform.right * AntiElasticity);
        }
        else if(col.gameObject.tag == "Weapon")
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * AntiElasticity);
        }
    }
}
