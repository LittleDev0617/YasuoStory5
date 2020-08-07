using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QSkillEffect : MonoBehaviour
{
    List<GameObject> Monsters;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monsters")
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

    public GameObject tornado;
    public GameObject effect;
    public GameObject Effect1;
    public GameObject Effect2;

    public IEnumerator Timer(GameObject item)
    {
        yield return new WaitForSeconds(1);
        item.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void Q3Effect()
    {
        if(transform.parent.localScale.x > 0)
            Instantiate(tornado, transform.parent.position + new Vector3(1.2f, -0.5f, 0), Quaternion.identity);
        else
            Instantiate(tornado, transform.parent.position + new Vector3(-1.2f, -0.5f, 0), Quaternion.identity);
    }

    public void QEffect()
    {
        if (Monsters.Count > 0)
        {            
            GameObject.Find("Yasuo_").GetComponent<Player>().QStack++;
            GameObject.Find("QS").GetComponent<Text>().text = GameObject.Find("Yasuo_").GetComponent<Player>().QStack + "";
            float avX = 0;
            int cnt = 0;
            float sum = 0;
            foreach (GameObject item in Monsters)
            {
                sum += item.transform.position.x;
                cnt++;
            }
            avX = sum / cnt;

            Instantiate(effect,
               new Vector3(
                           avX,
                           GameObject.Find("Yasuo_").transform.position.y + 0.4f,
                           0
                ), Quaternion.identity);

            foreach (GameObject item in Monsters)
            {
                GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTarget(item);
                item.GetComponent<Animator>().SetBool("Hit",true);
                StartCoroutine(Timer(item));
                if(item.transform.localScale.x > 0)
                    item.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * 2);
                else
                    item.transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * 2);
                Player player = GameObject.Find("Yasuo_").GetComponent<Player>();

                bool critical = player.Critical(); 
                int dmg = critical ? player.stats.QDmg * 2 : player.stats.QDmg;
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
                if (critical)
                    obj.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }



    void Start ()
    {
        Monsters = new List<GameObject>();
	}
	
	void Update ()
    {
		
	}
}
