using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    static int SkillSelected;
    float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        //damage = DungeonGameManager.Instance.SkilDamage[SkillSelected];
        damage = DungeonGameManager.Instance.characterList[CharacterController.selected].SkilDamage[SkillSelected];

        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(damage);
        }
        else if (col.transform.tag == "Boss")
        {
            DungeonGameManager.Instance.MapEnemyController.GetComponent<MapEnemyController>().BossGetInjured(damage);
        }
    }

    public void Selected(int num)
    {
        SkillSelected = num;
    }
}
