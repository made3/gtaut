using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isInteractable;
    private GameObject crosshair;
    public GameObject player;

    // Use this for initialization
    void Start () {
        crosshair = GameObject.Find("crosshair");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnMouseOver()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= 3)
        {
            crosshair.GetComponent<Animator>().SetBool("isActivated", true);
            isInteractable = true;
        }
        else
        {
            crosshair.GetComponent<Animator>().SetBool("isActivated", false);
            isInteractable = true;
        }
    }

    void OnMouseExit()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 3)
        {
            crosshair.GetComponent<Animator>().SetBool("isActivated", false);
            isInteractable = false;
        }
    }
}
