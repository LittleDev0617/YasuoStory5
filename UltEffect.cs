using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltEffect : MonoBehaviour
{
    public GameObject FirstSlashE;
    public GameObject SecSlashE;
    public GameObject FinishSlashE;
    public GameObject Effect;
    public GameObject Effect1;
    public GameObject Effect2;
    public float avX = 0;
    public GameObject[] Monsters;

    GameObject CFE;
    GameObject CSE;
    GameObject CE;
    GameObject CFiE;

    public void FirstSlash()
    {
        if (GameObject.Find("Yasuo_").transform.localScale.x > 0)
            CFE = Instantiate(FirstSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x + 1.5f,
                    GameObject.Find("Yasuo_").transform.position.y + 2.5f, 0
                    ), Quaternion.identity);
        else
            CFE = Instantiate(FirstSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x - 0.3f,
                    GameObject.Find("Yasuo_").transform.position.y + 2.2f, 0
                    ), Quaternion.identity);

        foreach (GameObject item in Monsters)
        {
            try
            {

                Player player = GameObject.Find("Yasuo_").GetComponent<Player>();

                bool critical = player.Critical(); 
                int dmg = critical ? player.stats.UltDmg * 2 : player.stats.UltDmg;
                item.GetComponent<MonsterInfo>().hp -= dmg;
                player.HP += (int)(dmg * player.stats.Blood * 0.01f);

                GameObject obj;
                if (Random.Range(0, 2) == 0)
                {
                    obj = Instantiate(Effect1, item.transform.position, Quaternion.identity);
                }
                else
                {
                    obj = Instantiate(Effect2, item.transform.position, Quaternion.identity);
                }
                if (critical)
                    obj.GetComponent<SpriteRenderer>().color = Color.red;
            }
            catch (System.Exception)
            {

            }
        }
    }
    public void SecondSlash()
    {
        if (GameObject.Find("Yasuo_").transform.localScale.x > 0)
        {
            CSE = Instantiate(SecSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x + 0.9f,
                    GameObject.Find("Yasuo_").transform.position.y + 2.3f, 0
                    ), Quaternion.Euler(0, 0, 93));
            CE = Instantiate(Effect, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x + 1.2f,
                    GameObject.Find("Yasuo_").transform.position.y + 2f, 0
                    ), Quaternion.Euler(0, 0, 93));
        }
        else
        {
            CSE = Instantiate(SecSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x - 1.7f,
                    GameObject.Find("Yasuo_").transform.position.y + 2.1f, 0
                    ), Quaternion.Euler(0, 0, 93));
            CE = Instantiate(Effect, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x - 1.3f,
                    GameObject.Find("Yasuo_").transform.position.y + 1.2f, 0
                    ), Quaternion.Euler(0, 0, 93));
        }
        foreach (GameObject item in Monsters)
        {
            try
            {

                item.GetComponent<MonsterInfo>().hp -= 50;
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(Effect1, item.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Effect2, item.transform.position, Quaternion.identity);
                }
            }
            catch (System.Exception)
            {
                
            }
        }
    }

    public void FinishSlash()
    {
        if (GameObject.Find("Yasuo_").transform.localScale.x > 0)
            CFiE = Instantiate(FinishSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x + 1.3f,
                    GameObject.Find("Yasuo_").transform.position.y + 2f, 0
                    ), Quaternion.Euler(0, 0, 45));
        else
            CFiE = Instantiate(FinishSlashE, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x - 0.5f,
                    GameObject.Find("Yasuo_").transform.position.y + 2f, 0
                    ), Quaternion.Euler(0, 0, 45));
        foreach (GameObject item in Monsters)
        {
            try
            {
                if (item.name.Contains("Wolf"))
                {
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    item.GetComponent<Wolf>().CantMove = false;
                }
                else if (item.name.Contains("Bird"))
                {
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    item.GetComponent<Bird>().CantMove = false;
                }
                item.GetComponent<MonsterInfo>().hp -= 200;
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(Effect1, item.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Effect2, item.transform.position, Quaternion.identity);
                }

            }
            catch (System.Exception)
            {
            }
        }
        Destroy(CFE, 0.2f);
        Destroy(CSE, 0.3f);
        Destroy(CE, 0.4f);
        Destroy(CFiE, 0.5f);
    }
}
