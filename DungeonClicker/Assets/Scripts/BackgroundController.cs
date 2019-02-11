using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Map[] map;
    Renderer[] renderer = new Renderer[3];
    public int selected = 0;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //renderer[i] = map[0].distance[i].GetComponent<Renderer>();
            //renderer[i] = map[selected].mapObject.GetComponent<Renderer>();
            renderer[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }
    }

    void Update()
    {
        /*map[0].distance[0].transform.Translate(-1 * Time.deltaTime, 0, 0);
        map[0].distance[1].transform.Translate(-1 * Time.deltaTime * 2, 0, 0);
        map[0].distance[2].transform.Translate(-1 * Time.deltaTime * 3, 0, 0);*/

        // for 문을 통해 컨트룰 할 수 있게 변경하자
        map[0].offset = Time.time * map[0].speed[0];
        renderer[0].material.SetTextureOffset("_MainTex", new Vector2(map[0].offset * map[0].speed[0], 0));
        renderer[1].material.SetTextureOffset("_MainTex", new Vector2(map[0].offset * map[0].speed[1], 0));
        renderer[2].material.SetTextureOffset("_MainTex", new Vector2(map[0].offset * map[0].speed[2], 0));
    }

    [System.Serializable]
    public class Map
    {
        public GameObject mapObject;
        //public GameObject[] distance;
        public float[] speed;
        public float offset;
    }
}
