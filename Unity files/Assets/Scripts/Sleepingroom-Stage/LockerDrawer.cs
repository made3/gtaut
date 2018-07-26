using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDrawer : MonoBehaviour {

    private Transform[] leds;

    [SerializeField]
    private OpenRotation sleepingRoomDoor;

    private void Start()
    {
        leds = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update () {
        int i = 0;
        int j = 0;
        foreach(bool b in GameManager.PCState)
        {
            if (b)
            {
                j++;
                //leds[i].gameObject.GetComponent<Renderer>().material.color = Color.green;
                Material mymat = leds[i].gameObject.GetComponent<Renderer>().material;
                mymat.color = Color.green;
                mymat.SetColor("_EmissionColor", Color.green);

            }
            i++;
        }
        if(j == 3)
        {
            sleepingRoomDoor.isLocked = false;
        }
    }
}
