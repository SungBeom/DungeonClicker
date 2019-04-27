using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGameManager : MonoBehaviour
{
    private static DungeonGameManager instance;
    public static DungeonGameManager Instance {
        get { return instance; }
    }

    //public Manage manage;
    public CharacterList[] characterList;
    public GameObject GameOver;
    public GameObject GameClear;
    
    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // 게임 클리어를 호출하기 위한 메소드
    public void CallGameClear()
    {
        GameClear.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // 게임 패배시 게임오버를 호출하기 위한 메소드
    public void CallGameOver()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // 캐릭터의 정보를 담기위한 클래스
    [System.Serializable]
    public class CharacterList
    {
        public GameObject CharacterPrefab;  // 캐릭터의 프리팹(애니메이션, 애니메이터가 내장)
        public float AttackDamage;          // 캐릭터 기본공격 데미지
        public float[] SkilDamage;          // 캐릭터가 사용하는 3개의 스킬에 해당하는 데미지
        public float Hp;                    // 캐릭터의 체력
        public float Reinforcement;         // 캐릭터 강화 횟수
        public bool life = true;            // 캐릭터 생존 여부를 구분하기 위한 플래그값 -> 현재 필요한지 고려중
        public Sprite CharacterImage;       // 캐릭터 변경시 사용할 이미지
        public Sprite[] SkillImages;        // 캐릭터가 사용하는 3개의 스킬에 해당하는 이미지
        public int Type;                    // 캐릭터가 해당하는 속성 (ex)0 = 불. 1 = 물, 2 = 풀
    }

    public float AttackDamage;
    public float[] SkilDamage;
    public float Hp;
    public float Reinforcement = 1;

    public void ReceiveData(CharacterList infos)
    {
        AttackDamage = infos.AttackDamage * Reinforcement;

        for (int i = 0; i < infos.SkilDamage.Length; i++)
        {
            SkilDamage[i] = infos.SkilDamage[i] * Reinforcement;
        }

        Hp = infos.Hp * Reinforcement;
    }
}