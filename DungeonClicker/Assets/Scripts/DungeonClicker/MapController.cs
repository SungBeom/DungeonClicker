using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public int SubStage;    // 현재 셀렉트 스테이지를 알기 위한 값
    public int Difficulty;  // 1 = 노멀, 2 = 하드

    public void MoveMap()
    {

    }

    public class MapControll
    {
        public GameObject Map;
        public int[] SubMap;    // 해당하는 값만큼 몬스터들의 HP, Damage 등에 곱한다
    }
}
