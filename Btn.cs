using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour
{   
    public void Change_Image(Sprite img)
    {
        if (gameObject.name.Contains("Close"))
            transform.parent.gameObject.SetActive(false);
        Debug.Log(gameObject.name);
        GetComponent<Image>().sprite = img;
    }
    public void onClick()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
