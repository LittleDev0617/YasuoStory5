using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectM : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (gameObject.name.Contains("공격이펙트"))
        {
            if (GameObject.Find("Yasuo_").transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(0.3f, 0.3f);
                transform.position = new Vector3(transform.position.x - 1.8f, transform.position.y, 0);
            }
        }
        if (gameObject.name.Contains("QEffect"))
            Destroy(gameObject, 0.6f);
        else if (gameObject.name.Contains("EQ"))
            Destroy(gameObject, 1.5f);
        else
            Destroy(gameObject, 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("Wolf"))
        {
            transform.position = new Vector3(
                GameObject.Find("Yasuo_").transform.position.x,
                GameObject.Find("Yasuo_").transform.position.y,
                0
                );
        }
    }
}