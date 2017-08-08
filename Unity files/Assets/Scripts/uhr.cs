using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uhr : MonoBehaviour {

    public GameObject kleinerZeiger;
    public GameObject grosserZeiger;
    public GameObject groessererZeiger;
    private int sec;
    private int min;
    private int hour;
    private int minCounter;
    private int hourCounter;

    // Use this for initialization
    void Start () {
        sec = 60;
        min = 60;
        hour = 12;
        minCounter = 0;
        hourCounter = 0;
        StartCoroutine(waitASecond());

    }

    IEnumerator waitASecond()
    {
        while (true)
        {
            for (int i = 0; i < sec; i++)
            {
                yield return new WaitForSeconds(1f);
                waitASecond();
                kleinerZeiger.transform.Rotate(new Vector3(0, 0, -6));
            }
            kleinerZeiger.transform.Rotate(new Vector3(0, 0, 360));
            if(minCounter < min)
            {
                grosserZeiger.transform.Rotate(new Vector3(0, 0, -6));
            }
            else
            {
                grosserZeiger.transform.Rotate(new Vector3(0, 0, 360));
                minCounter = 0;

                if(hourCounter < hour)
                {
                    groessererZeiger.transform.Rotate(new Vector3(0, 0, -30));
                }
                else
                {
                    groessererZeiger.transform.Rotate(new Vector3(0, 0, 360));
                    hourCounter = 0;
                }
            }
        }
    }
}
