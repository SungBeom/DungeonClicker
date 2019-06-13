using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public int mainMap = 1;
    public int subStage = 3;    // 현재 셀렉트 스테이지를 알기 위한 값
    public int difficulty;  // 1 = 노멀, 2 = 하드
    public MapControll[] mapController;
    public GameObject[] rd;
    public GameObject stagePanel;
    float[] targetOffset;

    void Start()
    {
        targetOffset = new float[3];
        StartCoroutine("ShowPanel");
        MoveMap(mainMap);
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            targetOffset[i] += Time.deltaTime * mapController[i].speed[i];
            //Vector2 speed = new Vector2(mapController.offset * mapController.speed[i], 0);
            //mapRenders[i].material.mainTextureOffset = new Vector2(targetOffset[i],0);
            //rd[i].GetComponent<Material>().mainTextureOffset = new Vector2(targetOffset[i], 0);
            rd[i].GetComponent<Image>().material.mainTextureOffset = new Vector2(targetOffset[i], 0);
            //rd[i].material.SetTextureOffset("_MainTex", new Vector2(mapController.offset * mapController.speed[i], 0));
        }
    }

    public void MoveMap(int mainMapSelected)
    {
        for(int i = 0; i < 3; i++)
        {
            rd[i].GetComponent<Image>().material = mapController[mainMapSelected].mapMaterial[i];   // 스프라이트 말고 그냥 마테리얼들을 등록해놓고 변경하면 어떨까?
        }
    }

    IEnumerator ShowPanel()
    {
        stagePanel.SetActive(true);
        stagePanel.GetComponent<Text>().text = "Stage " + (mainMap + 1).ToString() + "- " + (subStage + 1).ToString();
        yield return new WaitForSeconds(2.0f);
        stagePanel.SetActive(false);
    }

    [System.Serializable]
    public class MapControll
    {
        public float[] speed;
        public Material[] mapMaterial;
    }
}
