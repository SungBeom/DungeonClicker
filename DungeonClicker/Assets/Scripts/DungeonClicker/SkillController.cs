using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    static int SkillSelected;
    float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        damage = DungeonGameManager.Instance.SkilDamage[SkillSelected];

        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(damage);
        }
        else if (col.transform.tag == "Boss")
        {
            col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<BossHp>().GainDamage(damage);
        }
    }

    public void Selected(int num)
    {
        SkillSelected = num;
    }
}
