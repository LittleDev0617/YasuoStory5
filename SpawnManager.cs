using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public int Wave = 1;

    int waveWolf = 1;
    int waveBird = 1;

    int MwaveWolf = 1;
    int MwaveBird = 1;

    float WspawnTime = 3f;
    float BspawnTime = 5f;
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;

    void SpawnMonster1()
    {
        Instantiate(Monster1, transform.position, Quaternion.identity);
    }

    void SpawnMonster2()
    {
        Instantiate(Monster2, transform.position, Quaternion.identity);
    }

    void SpawnMonster3()
    {
        Instantiate(Monster3, transform.position, Quaternion.identity);
    }
    
    IEnumerator SpawnWolf()
    {
        while(true)
        {
            if (waveWolf == 0)
            {
                break;
            }
            waveWolf--;            
            GameObject tmp = Instantiate(Monster1, transform.position, Quaternion.identity).transform.GetChild(0).gameObject;
            tmp.GetComponent<MonsterInfo>().Dmg += 5 * (Wave - 1);
            tmp.GetComponent<MonsterInfo>().maxHp += 25 * (Wave - 1);
            tmp.GetComponent<MonsterInfo>().hp += 25 * (Wave - 1);
            tmp.GetComponent<MonsterInfo>().money += 5 * (Wave - 1);
            yield return new WaitForSeconds(WspawnTime);
        }
        StopCoroutine("SpawnWolf");
    }

    IEnumerator SpawnBird()
    {
        while (true)
        {
            if (waveBird == 0)
            {
                break;
            }
            waveBird--;
            GameObject tmp = Instantiate(Monster2, transform.position, Quaternion.identity).transform.GetChild(0).gameObject;
            if (Wave % 5 == 0 && waveBird == 0)
            {
                tmp.GetComponent<MonsterInfo>().Dmg += 20 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().maxHp += 100 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().hp += 100 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().money += 80 * (Wave - 1);
                tmp.transform.localScale = new Vector3(2,2,2);
            }
            else
            {
                tmp.GetComponent<MonsterInfo>().Dmg += 5 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().maxHp += 25 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().hp += 25 * (Wave - 1);
                tmp.GetComponent<MonsterInfo>().money += 5 * (Wave - 1);
            }
            yield return new WaitForSeconds(BspawnTime);
        }
        StopCoroutine("SpawnBird");
    }

    void Start()
    {
        StartCoroutine(SpawnWolf());
        StartCoroutine(SpawnBird());
    }

    void Update()
    {
        if(GameObject.Find("Yasuo_").GetComponent<Player>().KillW == MwaveWolf &&
            GameObject.Find("Yasuo_").GetComponent<Player>().KillB == MwaveBird)
        {
            GameObject.Find("Yasuo_").GetComponent<Player>().KillW = 0;
            GameObject.Find("Yasuo_").GetComponent<Player>().KillB = 0;
            MwaveWolf += 2;
            MwaveBird++;

            waveWolf = MwaveWolf;
            waveBird = MwaveBird;

            WspawnTime -= 0.2f;
            BspawnTime -= 0.2f;
            Wave++;
            GameObject.Find("Wave").GetComponent<Text>().text = "Wave	: " + Wave;
            StartCoroutine(SpawnWolf());
            StartCoroutine(SpawnBird());
        }
    }
}
