﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    private static StatusController instance;
    public static StatusController Instance
    {
        get { return instance; }
        set
        {
            instance = value;
        }
    }

    int selected = CharacterController.selected;
    public Sprite character;
    public float AttackDamage;
    public float[] SkilDamage;
    public float Hp;
    public float Reinforcement = 1;
    // 속성과 설명을 표현 할 방법을 찾아야 함
    void Start()
    {
        selected = CharacterController.selected;
        DungeonManager.CharacterList infos = DungeonManager.Instance.characterLists[selected];
        ReceiveData(infos);
    }


    public void ReceiveData(DungeonManager.CharacterList infos)
    {
        //selected = CharacterController.selected;
        AttackDamage = infos.AttackDamage * Reinforcement;

        for(int i = 0; i < infos.SkilDamage.Length; i++)
        {
            SkilDamage[i] = infos.SkilDamage[i] * Reinforcement;
        }

        Hp = infos.Hp * Reinforcement;
    }
}   