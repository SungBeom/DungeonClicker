using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public int SubStage;    // 현재 셀렉트 스테이지를 알기 위한 값
    public int Difficulty;  // 1 = 노멀, 2 = 하드
    public MapControll mapController;
    public Material[] material;
    public GameObject[] rd;
    private Renderer[] mapRenders;

    void Start()
    {
        mapRenders = new Renderer[rd.Length];
        for(int i = 0; i < rd.Length; i++)
        {
            mapRenders[i] = rd[i].GetComponent<Renderer>();
        }
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 speed = new Vector2(mapController.offset * mapController.speed[i], 0);
            mapRenders[i].material.mainTextureOffset = speed;
            //rd[i].material.SetTextureOffset("_MainTex", new Vector2(mapController.offset * mapController.speed[i], 0));
        }
    }

    public void MoveMap(int mainMapSelected)
    {
        int a = 0;
        int i = mainMapSelected;
        int j = i;
        for(i = (mainMapSelected/3); i<(j+3); i++)
        {
            //rd[a] = material[i];
            //mapController.map.transform.GetChild(a).GetComponent<Material>() = mapController.mapImage[i];
            //mapController.mapImage[i]
            a++;
        }
    }

    [System.Serializable]
    public class MapControll
    {
        public GameObject map;
        public float[] speed;
        public float offset;
        public Sprite[] mapImage;
    }
}
