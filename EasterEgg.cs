using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public GameObject shop;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.name == "EasterFlower" && collision.gameObject.name == "NormalAttackRange" && Input.GetKey(KeyCode.LeftControl))
        {
            collision.gameObject.transform.parent.parent.transform.position = GameObject.Find("Teleport_1").transform.position;
        }
        else if (gameObject.name == "house" && collision.gameObject.name == "NormalAttackRange" && Input.GetKey(KeyCode.LeftControl))
        {
            shop.SetActive(true);
        }
        else if (gameObject.name == "return" && collision.gameObject.name == "NormalAttackRange" && Input.GetKey(KeyCode.LeftControl))
        {
            GameObject.Find("Yasuo_").transform.position = GameObject.Find("ReturnPlace").transform.position;
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
    }
}
