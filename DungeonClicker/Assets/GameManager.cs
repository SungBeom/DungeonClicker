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
        CharacterData.Add(characterList[0]);
        //characterList[0].AttackDamage = 100;
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
