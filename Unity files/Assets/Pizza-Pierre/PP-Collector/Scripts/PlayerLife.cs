using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {

    public Collector_GameManager gameManager;
    private GameObject[] lifeIcons;
    private GameObject[] lifeIcons2;
    public GameObject lifeIconsParent;
    public GameObject lifeIconsParent2;
    public CameraManager cameraManager;
    public GameObject lifeText;
    public Canvas playerCanvas;
    public Canvas completeCanvas;
    private bool justSwitched = false;

    // Use this for initialization
    void Start () {

        lifeIcons = new GameObject[lifeIconsParent.transform.childCount];
        for (int i = 0; i < lifeIconsParent.transform.childCount; i++)
        {
            lifeIcons[i] = lifeIconsParent.transform.GetChild(i).gameObject;
        }
        lifeIcons2 = new GameObject[lifeIconsParent2.transform.childCount];
        for (int i = 0; i < lifeIconsParent2.transform.childCount; i++)
        {
            lifeIcons2[i] = lifeIconsParent2.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update () {
        if (cameraManager.isFPActive)
        {
            if (!justSwitched)
            {
                justSwitched = !justSwitched;
                lifeIconsParent.transform.parent.gameObject.SetActive(!lifeIconsParent.transform.parent.gameObject.activeSelf);
                lifeIconsParent2.transform.parent.gameObject.SetActive(!lifeIconsParent2.transform.parent.gameObject.activeSelf);
            }
        }
        else
        {
            if (justSwitched)
            {
                justSwitched = !justSwitched;
                lifeIconsParent.transform.parent.gameObject.SetActive(!lifeIconsParent.transform.parent.gameObject.activeSelf);
                lifeIconsParent2.transform.parent.gameObject.SetActive(!lifeIconsParent2.transform.parent.gameObject.activeSelf);
            }

        }

        if (lifeIcons.Length <= 0 || lifeIcons2.Length <= 0)
        {
            gameManager.currentState = Collector_GameManager.States.Lost;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if(gameManager.currentState == Collector_GameManager.States.Playing)
            {
                Destroy(lifeIcons[lifeIcons.Length - 1]);
                Array.Resize(ref lifeIcons, lifeIcons.Length - 1);

                Destroy(lifeIcons2[lifeIcons2.Length - 1]);
                Array.Resize(ref lifeIcons2, lifeIcons2.Length - 1);
            }
        }
    }
}
