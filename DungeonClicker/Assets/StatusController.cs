using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    int selected = CharacterController.selected;
    public Sprite character;
    public float AttackDamage;
    public float[] SkilDamage;
    public float Hp;

    void Awake()
    {
        selected = CharacterController.selected;
        GameManager.CharacterList infos = GameManager.Instance.characterList[selected];
        ReceiveData(infos);
    }

    public void ReceiveData(GameManager.CharacterList infos)
    {
        //selected = CharacterController.selected;
        character = infos.character;
        AttackDamage = infos.AttackDamage;

        for(int i = 0; i < infos.SkilDamage.Length; i++)
        {
            SkilDamage[i] = infos.SkilDamage[i];
        }

        Hp = infos.Hp;
    }
}   