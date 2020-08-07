using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    Vector3 mp;

    private void Start()
    {
        mp = Input.mousePosition;
    }

    void Update ()
    {
        if (mp == Input.mousePosition)
            return;
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vector.x, vector.y, 0);
        mp = Input.mousePosition;
	}
}
