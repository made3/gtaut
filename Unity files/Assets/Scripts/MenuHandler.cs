﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    private GameObject crosshair;
    public GameObject exitButton;

	// Use this for initialization
	void Start () {
        crosshair = GameObject.Find("crosshair");
        exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (CharacterController.isInMenu)
        {
            if (!exitButton.activeSelf)
            {
                exitButton.SetActive(true);
            }
            if (crosshair.activeSelf)
            {
                crosshair.SetActive(false);
            }
        }
        else
        {
            if (exitButton.activeSelf)
            {
                exitButton.SetActive(false);
            }
            if (!crosshair.activeSelf)
            {
                crosshair.SetActive(true);
            }
        }
    }
    
}
