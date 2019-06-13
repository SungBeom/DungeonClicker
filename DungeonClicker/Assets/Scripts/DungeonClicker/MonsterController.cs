using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    int mainMapSelected;
    int subStage;

    void Start()
    {
        mainMapSelected = DungeonManager.Instance.mainMapSelected;
        subStage = DungeonManager.Instance.subStage;
    }

    public class Enemy  // 기본값은 클래스일 필요가 없을거같다
    {
        public int hp;
        public int damage;
    }
}
