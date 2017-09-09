using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    private Image crosshair;
    public GameObject exitButton;

	// Use this for initialization
	void Start () {
        crosshair = GetComponentInChildren<Image>();
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
            if (crosshair.enabled)
            {
                crosshair.enabled = false;
            }
        }
        else
        {
            if (exitButton.activeSelf)
            {
                exitButton.SetActive(false);
            }
            if (!crosshair.enabled)
            {
                crosshair.enabled = true;
            }
        }
	}
    
}
