using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Shoot shoot;
    public int selected = 0;
    GameObject gameObject;

    // 발사 후 이동 시킬 것
    public void ShootObject()
    {
        gameObject = Instantiate(shoot.shootPrefab[selected], transform.position, transform.rotation);
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 300f);
        // SgameObject.transform.Translate(Vector3.right);
        // gameObject.transform.Translate(Vector3.right);
    }

    public void Update()
    {
        if (gameObject != null)
        {
            Debug.Log("Test");
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0f) * 100f);
            //gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(1f, 0f) * 100f);
            gameObject.transform.Translate(Vector3.right / 10.0f);
        }
    }

    [System.Serializable]
    public class Shoot
    {
        public GameObject[] shootPrefab;
    }
}
