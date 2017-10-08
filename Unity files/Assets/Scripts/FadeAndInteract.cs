using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndInteract : MonoBehaviour
{

    private bool displayInfo;
    private Animator _animator;
    private GameObject crosshair;

    // Use this for initialization
    void Start()
    {
        crosshair = GameObject.Find("crosshair");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayInfo && !CharacterController.isInMenu)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_animator.GetBool("open"))
                {
                    _animator.SetBool("open", false);
                }
                else
                {
                    _animator.SetBool("open", true);
                }

            }
        }
        if (CharacterController.isInMenu)
        {
            displayInfo = false;
            crosshair.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        crosshair.GetComponent<Animator>().SetBool("isActivated", true);
        displayInfo = true;
    }

    void OnMouseExit()
    {
        crosshair.GetComponent<Animator>().SetBool("isActivated", false);
        displayInfo = false;
    }

}
