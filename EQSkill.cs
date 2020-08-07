using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EQSkill : MonoBehaviour
{
    List<GameObject> Monsters;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monsters")
        {
            if (!Monsters.Contains(collision.gameObject))
            {
                Monsters.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monsters")
        {
            if (!Monsters.Contains(collision.gameObject))
            {
                Monsters.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monsters")
        {
            Monsters.Remove(collision.gameObject);
        }
    }

    public GameObject effect;
    public GameObject Effect1;
    public GameObject Effect2;

    public IEnumerator Timer(GameObject item)
    {
        yield return new WaitForSeconds(1);
        item.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void EEffect()
    {
        if (Monsters.Count > 0)
        {
            if (GameObject.Find("Yasuo_").GetComponent<Player>().QStack < 2)
            {
                GameObject.Find("Yasuo_").GetComponent<Player>().QStack++;
                GameObject.Find("QS").GetComponent<Text>().text = GameObject.Find("Yasuo_").GetComponent<Player>().QStack + "";
            }
            foreach (GameObject item in Monsters)
            {
                Instantiate(effect, transform.position + new Vector3(0,0.7f,0), Quaternion.identity);
                GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTarget(item);
                item.GetComponent<Animator>().SetBool("Hit", true);
                if (GameObject.Find("Yasuo_").GetComponent<Player>().QStack == 2)
                {
                    if (item.name.Contains("Wolf") && !item.GetComponent<Wolf>().IsAirboned)
                    {
                        item.GetComponent<Wolf>().Airbon();
                    }
                    else if (item.name.Contains("Bird") && !item.GetComponent<Bird>().IsAirboned)
                    {
                        item.GetComponent<Bird>().Airbon();
                    }
                    item.GetComponent<MonsterInfo>().hp -= GameObject.Find("Yasuo_").GetComponent<Player>().stats.QDmg + 40;
                    GameObject.Find("Yasuo_").GetComponent<Player>().QStack = 0;
                    GameObject.Find("QS").GetComponent<Text>().text = GameObject.Find("Yasuo_").GetComponent<Player>().QStack + "";
                }
                StartCoroutine(Timer(item));
                if (item.transform.localScale.x > 0)
                    item.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * 2);
                else
                    item.transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * 2);
                item.GetComponent<MonsterInfo>().hp -= GameObject.Find("Yasuo_").GetComponent<Player>().stats.EQDmg;

                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(Effect1, item.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Effect2, item.transform.position, Quaternion.identity);
                }
            }
        }
    }



    void Start()
    {
        Monsters = new List<GameObject>();
    }

    void Update()
    {

    }
}
