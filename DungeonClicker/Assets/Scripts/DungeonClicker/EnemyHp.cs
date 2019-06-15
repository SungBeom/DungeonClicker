﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float hp;

    public void GainDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Debug.Log(hp);
            //DungeonGameManager.Instance.Gold += 10;
            //Destroy(gameObject.transform.root.gameObject);
            gameObject.SetActive(false);
            DungeonManager.Instance.mc.Pool.Enqueue(gameObject);
        }
    }
}