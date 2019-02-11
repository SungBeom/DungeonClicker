using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseSkill : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // 스킬에 특정 인덱스가 붙어 있다면 전체 스킬들을 다 등록 해 놓고 원하는 스킬만 골라 사용이 가능하지않을까?
    Animation ani;
    List<string> aniArray;
    int index = 0;

    public int selected;

    void Start()
    {
        aniArray = new List<string>();
        InsertArray();
    }

    public void InsertArray()
    {
        foreach(AnimationState state in ani)
        {
            aniArray.Add(state.name);
            index++;
        }
    }

    public void OnPointerDown(PointerEventData eventData)   
    {
        // selected 값을 변경하고 받을 수 있는 방법을 생각해 보자.
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ani.Play(aniArray[selected]);
    }
}
