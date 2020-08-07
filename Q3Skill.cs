using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q3Skill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monsters")
        {
            GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTarget(collision.gameObject);
            if (collision.gameObject.name.Contains("Wolf") && !collision.gameObject.GetComponent<Wolf>().IsAirboned)
            {
                collision.gameObject.GetComponent<Wolf>().Airbon();
            }
            else if (collision.gameObject.name.Contains("Bird") && !collision.gameObject.GetComponent<Bird>().IsAirboned)
            {
                collision.gameObject.GetComponent<Bird>().Airbon();
            }
            Player player = GameObject.Find("Yasuo_").GetComponent<Player>();

            collision.gameObject.GetComponent<MonsterInfo>().hp -= player.Critical() ? (player.stats.QDmg+40) * 2 : player.stats.QDmg + 40;
        }
    }

    string mov = "";

    void Start()
    {
        if (GameObject.Find("Yasuo_").transform.localScale.x > 0)
            mov = "right";
        else
            mov = "left";
        Destroy(gameObject, 3);
    }

    void Update()
    {
        if (mov == "right")
            transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime);
        else
            transform.Translate(new Vector3(-10, 0, 0) * Time.deltaTime);
    }
}
