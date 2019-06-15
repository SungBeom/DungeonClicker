using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    static int SkillSelected;
    float Damage;
    int skillCheck;

    void OnCollisionEnter2D(Collision2D col)
    {
        //damage = DungeonGameManager.Instance.SkilDamage[SkillSelected];
        Damage = DungeonManager.Instance.characterLists[CharacterController.selected].SkilDamage[SkillSelected];
        skillCheck = DungeonManager.Instance.characterLists[CharacterController.selected].SkillCheck[int.Parse(name.Substring(name.Length - 1)) - 1];

        if (skillCheck == 0)
        {
            if (col.transform.tag == "Enemy")
            {
                Debug.Log("단일 기술");
                col.gameObject.GetComponent<EnemyHp>().GainDamage(Damage);
                gameObject.SetActive(false);
            }
            else if (col.transform.tag == "Boss")
            {
                DungeonManager.Instance.BossInjured(Damage);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (col.transform.tag == "Enemy")
            {
                Debug.Log("범위 기술");
                col.gameObject.GetComponent<EnemyHp>().GainDamage(Damage);
            }
            else if (col.transform.tag == "Boss")
            {
                DungeonManager.Instance.BossInjured(Damage);
            }
        }
    }

    public void Selected(int num)
    {
        SkillSelected = num;
    }
}
