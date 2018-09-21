using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startmenu : MonoBehaviour
{

    private int currentPage;

    // 0 is Start, 1 is Controls, 2 is Credits
    private int currentSelection = 0;

    [SerializeField]
    private GameObject selectionMarker;

    [SerializeField]
    private Transform[] markerPositions;

    [SerializeField]
    private GameObject loadingScreen;

    private Animator anim;

    private bool swapScene = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        currentPage = anim.GetInteger("currentPage");
    }

    // Update is called once per frame
    void Update()
    {
        // Solo-Check because the Loading Screen needs to be active first, then the Scene can start switching. Cant happen in the same frame.
        if (swapScene)
        {
            SceneManager.LoadScene("MafiaRoom");
        }

        if (Input.GetButtonDown("Submit"))
        {
            if(currentSelection == 0)
            {
                loadingScreen.SetActive(true);
                swapScene = true;
            }
            else
            {
                currentPage = 2;
                anim.SetInteger("currentPage", currentPage);
                selectionMarker.SetActive(false);
            }
        }

        if (currentPage == 1)
        {
            if (Input.GetButtonDown("Up"))
            {
                if (currentSelection > 0)
                {
                    currentSelection--;
                    selectionMarker.transform.position = markerPositions[currentSelection].position;
                }
            }
            if (Input.GetButtonDown("Down"))
            {
                if (currentSelection < 2)
                {
                    currentSelection++;
                    selectionMarker.transform.position = markerPositions[currentSelection].position;
                }
            }
        }

        if (Input.GetButtonDown("Left"))
        {
            if (currentPage > 0) currentPage--;
            anim.SetInteger("currentPage", currentPage);
            if (currentPage == 1)
            {
                selectionMarker.SetActive(true);
            }
            else
            {
                selectionMarker.SetActive(false);
            }
        }
        else if (Input.GetButtonDown("Right"))
        {
            if (currentPage < 3) currentPage++;
            anim.SetInteger("currentPage", currentPage);
            if (currentPage == 1)
            {
                selectionMarker.SetActive(true);
            }
            else
            {
                selectionMarker.SetActive(false);
            }
        }
    }
}