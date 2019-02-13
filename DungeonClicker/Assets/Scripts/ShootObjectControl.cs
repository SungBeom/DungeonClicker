using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectControl : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.GetComponent<HpControl>().GainDamage(damage);
        Debug.Log(gameObject);
        //gameObject.SetActive(false);
    }
}
