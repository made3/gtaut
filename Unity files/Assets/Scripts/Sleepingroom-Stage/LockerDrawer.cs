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
    }
}
