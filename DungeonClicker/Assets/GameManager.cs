using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
        get { return instance; }
    }
    public Manage manage;

    int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }
    int Food;
    int Jewelry;
    
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
    }

    [System.Serializable]
    public class Manage
    {
        public GameObject[] controller;
    }
}
