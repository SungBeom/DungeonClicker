using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<object> CharacterData = new List<object>();
    private static GameManager instance;
    public static GameManager Instance {
        get { return instance; }
    }

    public Manage manage;
    public CharacterList[] characterList;

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

        // 리스트 값을 데이터베이스로 올리고 내리는 메소드가 필요 나중에 물어볼것
        // 시작 시 리스트 값을 내려 받기
        // 리스트의 인덱스를 이용하여 해당 값들이 변경 가능함을 확인하였음
        CharacterData.Add(characterList[0]);
        //characterList[0].AttackDamage = 100;
    }

    // 다른 씬으로 데이터를 보내기 위한 함수
    public void SendInfo()
    {

    }

    // 다른 씬에서 보낸 데이터를 받기위한 함수
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
    }
}