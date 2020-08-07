using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Bird>().CanAttack = true;
            transform.parent.GetComponent<Animator>().SetBool("Find", true);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Bird>().CanAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Bird>().CanAttack = false;
        }
    }
    void Start()
    {

    }

    void Update()
    {
        if (transform.parent.GetComponent<Bird>().CanAttack)
            transform.parent.GetComponent<Animator>().SetBool("Find", true);
        else 
            transform.parent.GetComponent<Animator>().SetBool("Find", false);
    }
}
