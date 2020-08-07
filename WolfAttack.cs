using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Wolf>().CanAttack = true;
            transform.parent.GetComponent<Animator>().SetBool("Find", true);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Wolf>().CanAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Wolf>().CanAttack = false;
        }
    }
    void Start ()
    {
		
	}
	
	void Update ()
    {
		if(transform.parent.GetComponent<Wolf>().CanAttack)
            transform.parent.GetComponent<Animator>().SetBool("Find", true);
    }
}
