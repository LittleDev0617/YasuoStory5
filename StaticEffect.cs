using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Monster")
            collision.gameObject.GetComponent<MonsterInfo>().hp -= 25 * GameObject.Find("Spawner").GetComponent<SpawnManager>().Wave;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
