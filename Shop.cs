using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    int curSlide = 1;
    public GameObject Alert;
    public GameObject First;
    public GameObject Second;
    public void BuyItem(string name)
    {
        int item_price = int.Parse(GameObject.Find("Item_" + name).transform.GetChild(5).GetComponent<Text>().text);
        Player player = GameObject.Find("Yasuo_").GetComponent<Player>();
        int player_gold = player.Money;
        if (player_gold < item_price)
        {
            Alert.transform.GetChild(2).GetComponent<Text>().text = "현재 보유하신 골드가 아이템 \n가격보다 낮습니다(T.T)";
            Alert.SetActive(true);            
        }
        else if(player.ItemList.Count == 14)
        {
            Alert.transform.GetChild(2).GetComponent<Text>().text = "아이템 개수는 최대 12개입니다. 필요없는 아이템을 팔아주세요!";
            Alert.SetActive(true);
        }
        else
        {
            player.Money -= item_price;
            player.AddItem(name);
        }
    }

    public void Next_Shop()
    {
        if (curSlide != 2)
        {
            curSlide++;
            First.SetActive(false);
            Second.SetActive(true);
        }
    }
    public void Previous_Shop()
    {
        if (curSlide != 1)
        {
            curSlide--;
            Second.SetActive(false);
            First.SetActive(true);
        }
    }
    public void Close_Alert()
    {
        Alert.SetActive(false);
    }

    public void Close_Shop()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void Open_Shop()
    {
        gameObject.SetActive(true);
    }


}
