using System.Collections;
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

    void Awake()
    {
        selected = CharacterController.selected;
        DungeonGameManager.CharacterList infos = DungeonGameManager.Instance.characterList[selected];
        ReceiveData(infos);
    }

    public void ReceiveData(DungeonGameManager.CharacterList infos)
    {
        //selected = CharacterController.selected;
        character = infos.character;
        AttackDamage = infos.AttackDamage * Reinforcement;

        for(int i = 0; i < infos.SkilDamage.Length; i++)
        {
            SkilDamage[i] = infos.SkilDamage[i] * Reinforcement;
        }

        Hp = infos.Hp * Reinforcement;
    }
}   