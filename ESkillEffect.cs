using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESkillEffect : MonoBehaviour
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
            if (GameObject.Find("Yasuo_").transform.localScale.x > 0)
                Instantiate(effect, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x + 2.2f,
                    GameObject.Find("Yasuo_").transform.position.y, 0
                    ), Quaternion.Euler(0, 0, 48));
            else
                Instantiate(effect, new Vector3(
                    GameObject.Find("Yasuo_").transform.position.x - 2.2f,
                    GameObject.Find("Yasuo_").transform.position.y, 0
                    ), Quaternion.Euler(0, 0, 48));
            foreach (GameObject item in Monsters)
            {
                GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTarget(item);
                item.GetComponent<Animator>().SetBool("Hit", true);
                
                StartCoroutine(Timer(item));
                if (item.transform.localScale.x > 0)
                    item.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * 2);
                else
                    item.transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * 2);
                item.GetComponent<MonsterInfo>().hp -= GameObject.Find("Yasuo_").GetComponent<Player>().stats.EDmg;

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
