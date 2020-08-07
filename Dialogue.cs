using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public string[] Sentences;
    Queue<string> sent;
    GameObject dialogue;

    IEnumerator DisplayText(string sentence)
    {
        dialogue.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (char letter in sentence)
        {
            dialogue.transform.GetChild(0).GetComponent<Text>().text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }

    void DisplayNextSentence()
    {
        if(sent.Count == 0)
        {
            return;
        }

        string sentence = sent.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayText(sentence));
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.T))
        {
            dialogue.SetActive(true);
            foreach (string item in Sentences)
            {
                sent.Enqueue(item);
            }

            DisplayNextSentence();
        }
    }

    void Start ()
    {
        sent = new Queue<string>();
        dialogue = GameObject.Find("Dialogue Box");
        dialogue.SetActive(false);
	}
	
	void Update ()
    {
        if (dialogue.active && Input.GetKeyDown(KeyCode.Return))
            DisplayNextSentence();
	}
}
