using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectControl : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("명중");
        //Debug.Log(col.gameObject.transform);
        col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().GainDamage(damage);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
