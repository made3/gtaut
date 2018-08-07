using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDrawer : MonoBehaviour {

    private Transform[] leds;

    [SerializeField]
    private OpenPosition drawer;

    private void Start()
    {
        leds = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (drawer.isLocked)
        {
            int i = 1;
            int j = 0;
            foreach (bool b in GameManager.PCState)
            {
                if (b)
                {
                    j++;
                    Material mymat = leds[i].gameObject.GetComponent<Renderer>().material;
                    mymat.color = Color.green;
                    mymat.SetColor("_EmissionColor", Color.green);
                }
                i++;
            }

            if (j == 4)
            {
                drawer.isLocked = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameManager.PCState[0] = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameManager.PCState[1] = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            GameManager.PCState[2] = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            GameManager.PCState[3] = true;
        }

    }
}
