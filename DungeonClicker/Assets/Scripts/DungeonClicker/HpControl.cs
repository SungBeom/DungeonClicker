using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    public float hp;
    public Transform myHp; // 던전 캔버스 안에 있는 좌측 슬라이더
    Slider MyHp;

    public void Start()
    {
        MyHp = myHp.GetComponent<Slider>();
        hp = DungeonGameManager.Instance.characterList[CharacterController.selected].Hp;
        MyHp.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        MyHp.value = hp;

       if(hp <= 0)
        {
            CharacterController.characterCount--;

            gameObject.transform.parent.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);   // 현재로썬 parent.parent가 최선으로 보임 다른 방안이 있을시 변경할것
            gameObject.transform.parent.parent.GetComponent<Animator>().SetTrigger("Die_t");

            if (CharacterController.characterCount == 0) //여기서 공중으로 상승시키면 될듯
            {
                StartCoroutine(Delay());
                DungeonGameManager.Instance.CallGameOver();
            }
            else
            {
                // CharacterController에 있는 DeathChange를 호출해야함
            }
            Destroy(gameObject.transform.parent.parent.gameObject, 0.5f);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.485f);
        Time.timeScale = 0.0f;
    }
}
