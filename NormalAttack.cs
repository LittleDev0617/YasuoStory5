using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    bool EasterFlower = false;
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
        else if (collision.gameObject.name == "EasterFlower")
        {
            EasterFlower = true;
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
        else if (collision.gameObject.name == "EasterFlower")
        {
            EasterFlower = false;
        }
    }

    public GameObject Effect1;
    public GameObject Effect2;

    public IEnumerator Timer(GameObject item)
    {
        yield return new WaitForSeconds(0.3f);
        if(!item.GetComponent<Animator>().GetBool("IsDie"))
            item.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void NormalEffect()
    {
        if (Monsters.Count > 0) 
        {
            foreach (GameObject item in Monsters)
            {
                GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTarget(item);
                item.GetComponent<Animator>().SetBool("Hit", true);
                StartCoroutine(Timer(item));
                if (item.transform.localScale.x > 0)
                    item.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * 2);
                else
                    item.transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * 2);
                Player player = GameObject.Find("Yasuo_").GetComponent<Player>();
                bool critical = player.Critical();
                int dmg = critical ? player.stats.NormalDmg * 2 : player.stats.NormalDmg;
                item.GetComponent<MonsterInfo>().hp -= dmg;
                player.HP += (int)(dmg * player.stats.Blood * 0.01f);
                if (player.ItemList.Contains("스태틱"))
                    player.staticCharge += 5;
                GameObject obj;
                if (Random.Range(0, 2) == 0)
                {
                    obj = Instantiate(Effect1, item.transform.position, Quaternion.identity);
                }
                else
                {
                    obj = Instantiate(Effect2, item.transform.position, Quaternion.identity);
                }
                if(critical)
                    obj.GetComponent<SpriteRenderer>().color = Color.red;
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
