using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    float Damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        //damage = gameObject.transform.root.Find("StatusController").GetComponent<StatusController>().AttackDamage;
        Damage = DungeonManager.Instance.characterLists[CharacterController.selected].AttackDamage;
        //Debug.Log(gameObject.transform.root);
        //Debug.Log(damage);
        //float damage = StatusController.Instance.AttackDamage;
        if (col.transform.tag == "Enemy")
        {
            //Debug.Log("때려서 충돌");
            col.gameObject.GetComponent<EnemyHp>().GainDamage(Damage);
        }
        else if(col.transform.tag == "Boss")
        {
            //col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<BossHp>().GainDamage(damage);
            //DungeonGameManager.Instance.MapEnemyController.GetComponent<MapEnemyController>().BossGetInjured(Damage);
            DungeonManager.Instance.BossInjured(Damage);
        }
    }
}
