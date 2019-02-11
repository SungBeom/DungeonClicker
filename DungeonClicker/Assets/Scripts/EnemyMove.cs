using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    // 부딪힌 다음 튕겨나오는 것을 구현해야함
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right *-10f);  
    }

    /*void OnCollisionEnter(Collision col)
    {
        Debug.Log(col);
        col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<Slider>().value -= 10;
    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col);
        //col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<Slider>().value -= 10;
        //col.transform.GetComponent<HpControl>().GainDamage(30);
        col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().GainDamage(30);
    }

    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit);
        hit.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<Slider>().value -= 10;
    }*/
}
