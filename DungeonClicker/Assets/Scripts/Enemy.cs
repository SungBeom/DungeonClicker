using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Damage;
    public float Hp;
    public float Speed;

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * -Speed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") { return; }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().GainDamage(Damage);
            GetComponent<Rigidbody2D>().AddForce(transform.right * 300f);
        }
        else
        {
            return;
        }
    }

    void OnBecameInvisible()    // 이게 왜 적용이 안될까
    {
        Destroy(gameObject);
    }
}
