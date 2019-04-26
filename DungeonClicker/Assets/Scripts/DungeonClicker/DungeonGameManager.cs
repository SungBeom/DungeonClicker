using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGameManager : MonoBehaviour
{
    private static DungeonGameManager instance;
    public static DungeonGameManager Instance {
        get { return instance; }
    }

    public Manage manage;
    public CharacterList[] characterList;
    public GameObject GameOver;
    public GameObject GameClear;

    int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }
    int food;
    public int Food
    {
        get { return food; }
        set { food = value; }
    }
    int jewelry;
    public int Jewelry
    {
        get { return jewelry; }
        set { jewelry = value; }
    }
    
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
        for(int i = 0; i < manage.controller.Length; i++)
        {
            manage.controller[i].gameObject.SetActive(true);
        }

        /*for (int i = 0; i < GameManager.Instance.characterList.Length; i++)  // 현재 이부분은 메인과 합쳐졌을때 필요한 부분임
        {
            characterList[i].CharacterPrefab = GameManager.Instance.characterList[i].CharacterPrefab;
            characterList[i].AttackDamage = GameManager.Instance.characterList[i].AttackDamage;
            for (int j = 0; j < 3; j++)
            {
                characterList[i].SkilDamage[j] = GameManager.Instance.characterList[i].SkilDamage[j];
            }
            characterList[i].Hp = GameManager.Instance.characterList[i].Hp;
        }*/
    }

    public void CallGameClear()
    {
        GameClear.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CallGameOver()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ReceiveInfo()
    {

    }

    [System.Serializable]
    public class Manage
    {
        public GameObject[] controller;
    }

    [System.Serializable]
    public class CharacterList
    {
        public GameObject CharacterPrefab;
        public float AttackDamage;
        public float[] SkilDamage;
        public float Hp;
        public float Reinforcement;
        public Sprite CharacterImage;   // 캐릭터 변경시 사용할 이미지
        public int Type;
    }

    public float AttackDamage;
    public float[] SkilDamage;
    public float Hp;
    public float Reinforcement = 1;

    public void ReceiveData(CharacterList infos)
    {
        //selected = CharacterController.selected;
        AttackDamage = infos.AttackDamage * Reinforcement;

        for (int i = 0; i < infos.SkilDamage.Length; i++)
        {
            SkilDamage[i] = infos.SkilDamage[i] * Reinforcement;
        }

        Hp = infos.Hp * Reinforcement;
    }
}