using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    [SerializeField]
    private GameObject crosshair;

    [SerializeField]
    private GameObject menuParent;

	// Use this for initialization
	void Start () {
        menuParent.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (CharacterController.isInMenu)
        {
            if (!menuParent.activeSelf)
            {
                menuParent.SetActive(true);
            }
            if (crosshair.activeSelf)
            {
                crosshair.SetActive(false);
            }
        }
        else
        {
            if (menuParent.activeSelf)
            {
                menuParent.SetActive(false);
            }
            if (!crosshair.activeSelf)
            {
                crosshair.SetActive(true);
            }
        }
    }
    
}
