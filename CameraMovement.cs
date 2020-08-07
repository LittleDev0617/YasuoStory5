using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) &&
            transform.position.x <= GameObject.Find("CEndRight").transform.position.x &&
            GameObject.Find("Yasuo_").transform.position.x >= transform.position.x)
        {
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * 4);
        }
        if (Input.GetKey(KeyCode.LeftArrow) &&
            transform.position.x >= GameObject.Find("CEndLeft").transform.position.x &&
            GameObject.Find("Yasuo_").transform.position.x <= transform.position.x)
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * 4);
        }
        transform.position =  new Vector3(transform.position.x, GameObject.Find("Yasuo_").transform.position.y + 1, transform.position.z);
    }
}
