using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Monster
{
    public int scale = 1;
    public bool CanAttack = false;
    bool FindPlayer = false;
    int ran = 0; //0-> idle 1-> walk

    public bool IsAirboned = false;

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Hit", false);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        IsAirboned = false;
    }

    public void Airbon()
    {
        IsAirboned = true;
        GetComponent<Animator>().SetBool("Hit", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        transform.position += new Vector3(0, 2, 0);
        StartCoroutine("Timer");
    }
    IEnumerator ChangeMovement()
    {
        ran = Random.Range(0, 3);
        if (ran == 0)
            GetComponent<Animator>().SetBool("Walk", false);
        else
            GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSeconds(3);
        StartCoroutine("ChangeMovement");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindPlayer = true;
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindPlayer = true;
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindPlayer = false;
            StartCoroutine("ChangeMovement");
        }
    }


    void Start()
    {
        StartCoroutine("ChangeMovement");
    }
    public bool CantMove = false;
    void Update()
    {
        if (GameObject.Find("Yasuo_").GetComponent<Player>().Target == gameObject)
        {
            GameObject.Find("Yasuo_").GetComponent<Player>().DisplayTargetInfo();
        }

        if (GetComponent<MonsterInfo>().hp > 0)
        {
            transform.parent.GetChild(2).localScale = new Vector3(
                0.15f / GetComponent<MonsterInfo>().maxHp * GetComponent<MonsterInfo>().hp,
                transform.parent.GetChild(2).localScale.y,
                transform.parent.GetChild(2).localScale.z
                );
            if (!CantMove)
            {
                if (!GetComponent<Animator>().GetBool("Hit"))
                {
                    if (FindPlayer)
                    {
                        if (Vector2.Distance(GameObject.Find("Yasuo_").transform.position, transform.position) >= 1.5f)
                        {
                            Vector3 v = Vector3.zero;
                            GetComponent<Animator>().SetBool("Walk", true);
                            if (GameObject.Find("Yasuo_").transform.position.x > transform.position.x)
                            {
                                transform.localScale = new Vector3(scale, scale, scale);
                                v = Vector3.right;
                            }
                            else if (GameObject.Find("Yasuo_").transform.position.x < transform.position.x)
                            {
                                transform.localScale = new Vector3(-scale, scale, scale);
                                v = Vector3.left;
                            }
                            transform.Translate(v * Time.deltaTime * 3);
                        }
                    }
                    else
                    {
                        Vector3 v = Vector3.zero;
                        if (ran == 1)
                        {
                            transform.localScale = new Vector3(scale, scale, scale);
                            v = Vector3.right;
                        }
                        else if (ran == 2)
                        {
                            transform.localScale = new Vector3(-scale, scale, scale);
                            v = Vector3.left;
                        }
                        if (transform.position.x <= GameObject.Find("EndLeft").transform.position.x)
                            v = Vector3.right;
                        if (transform.position.x >= GameObject.Find("EndRight").transform.position.x)
                            v = Vector3.left;
                        transform.Translate(v * Time.deltaTime * 2);
                    }
                    for (int i = 1; i <= 2; i++)
                        transform.parent.GetChild(i).position = transform.position + new Vector3(0, 1, 0);
                }
            }
            
        }
        else
        {
            GetComponent<MonsterInfo>().hp = 0;
            transform.parent.GetChild(2).localScale = new Vector3(
                0,
                transform.parent.GetChild(2).localScale.y,
                transform.parent.GetChild(2).localScale.z
                );
            GetComponent<Animator>().SetBool("IsDie", true);
        }
    }
}