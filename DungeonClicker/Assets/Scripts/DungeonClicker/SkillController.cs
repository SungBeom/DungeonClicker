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

/// 메인 병합용 스킬 컨트룰러
/// using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class SkillController : MonoBehaviour
//{
//    static int SkillSelected;
//    public float damage;
//    int skillCheck;
//
//    void Start()
//    {
//        damage = DungeonManager.Instance.characterLists[DungeonManager.Instance.cc.selected].SkilDamage[SkillSelected];
//    }
//
//    void OnCollisionEnter2D(Collision2D col)
//    {
//        //damage = DungeonManager.Instance.characterLists[DungeonManager.Instance.cc.selected].SkilDamage[SkillSelected]; // 충돌 시 데미지를 들고 오기 때문에 투사체를 던진 후 캐릭터를 변경 시 데미지가 변경될 확률이 높다
//        skillCheck = DungeonManager.Instance.characterLists[DungeonManager.Instance.cc.selected].SkillCheck[int.Parse(name.Substring(name.Length - 1)) - 1];
//        Debug.Log(damage);
//
//        if (skillCheck == 0)
//        {
//            if (col.transform.tag == "Enemy")
//            {
//                Debug.Log("단일 기술");
//                if (col.gameObject.GetComponent<SmallEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<SmallEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<MiddleEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<MiddleEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<LargeEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<LargeEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<BossEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<BossEnemy>().GainDamage(damage);
//                }
//                gameObject.SetActive(false);
//            }
//        }
//        else
//        {
//            if (col.transform.tag == "Enemy")
//            {
//                Debug.Log("범위 기술");
//                if (col.gameObject.GetComponent<SmallEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<SmallEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<MiddleEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<MiddleEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<LargeEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<LargeEnemy>().GainDamage(damage);
//                }
//                else if (col.gameObject.GetComponent<BossEnemy>() != null)
//                {
//                    col.gameObject.GetComponent<BossEnemy>().GainDamage(damage);
//                }
//            }
//        }
//    }
//
//    public void Selected(int num)
//    {
//        SkillSelected = num;
//    }
//}