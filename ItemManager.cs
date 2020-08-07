using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public void ShowSell(int i)
    {
        GameObject.Find("Cell_" + i).transform.GetChild(0).gameObject.SetActive(true);
    }
    public void HideSell(int i)
    {
        GameObject.Find("Cell_" + i).transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SellItem(int i)
    {
        Player player = GameObject.Find("Yasuo_").GetComponent<Player>();
        Image img = GameObject.Find("Cell_" + i).GetComponent<Image>();
        if (img.color == Color.white)
        {
            int ind = player.ItemStrings.IndexOf(img.sprite.name);
            for (int k = 0; k < player.ItemList.Count; k++)
            {
                Image img1 = GameObject.Find("Cell_" + k).GetComponent<Image>();
                img1.sprite = null;
                img1.color = new Color(0.2830189f, 0.2710039f, 0.2710039f);
            }
            player.ItemList.RemoveAt(i);
            player.DeleteItem(player.ItemStrings[ind]);
            player.Money += player.ItemMoneyList[ind];
            player.DisplayItem();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
