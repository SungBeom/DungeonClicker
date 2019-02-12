//  스테이지 별 몬스터의 체력이 변경 되어야 한다

//  일반 몹들이 가져야 할 속성 목록
//  1.hp 2.damage 3.공격 스킬 1개
//  보스 몹은 오브젝트 풀링이 아닌 instanciate 를 사용하자
//  스킬 여러가지 와 hp, 기본 dagame 가 필요 하고 이를 사용 할 알고리즘이 필요하다

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public Stage[] stage;

    [System.Serializable]
    public class Stage
    {
        public GameObject[] monsterPrefab;
        public int[] monsterCount;
        //public int[] damage;    //  데미지와 hp는 따로 스크립트로 제작하는게 나으려나?
    }
}