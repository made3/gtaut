using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelefonWählscheibe : MonoBehaviour {

    private int dialCounter = 0 ;
    [SerializeField]
    private int[] correctNumber = {0,1,7,6,6,8,9};
    public int maxNumberLenght;
    [HideInInspector]
    public int[] dialedNumbers;
    public int currentHoverNumber;

    [SerializeField]
    private Text dialedNumbersText;

    private NumberRecognition latestHoveredNumber;

    private bool correctNumberEntered = false;
    [SerializeField]
    private GameObject doorToOpen;

    // Use this for initialization
    void Start () {
        Array.Resize(ref dialedNumbers, maxNumberLenght);
        //resetDialedNumber();
        currentHoverNumber = -1;
    }

    // Update is called once per frame
    void Update () {

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("TelefonTaste"))
            {
                if(latestHoveredNumber == null)
                {
                    latestHoveredNumber = hit.collider.GetComponent<NumberRecognition>();
                    latestHoveredNumber.OnEnter();
                }
                else if(hit.collider.GetComponent<NumberRecognition>().GetInstanceID() != latestHoveredNumber.GetInstanceID())
                {
                    latestHoveredNumber.OnExit();
                    latestHoveredNumber = hit.collider.GetComponent<NumberRecognition>();
                    latestHoveredNumber.OnEnter();
                }
                else if(hit.collider.GetComponent<NumberRecognition>().GetInstanceID() == latestHoveredNumber.GetInstanceID())
                {
                    latestHoveredNumber = hit.collider.GetComponent<NumberRecognition>();
                    latestHoveredNumber.OnEnter();
                }
            }
            else if(latestHoveredNumber != null)
            {
                latestHoveredNumber.OnExit();
            }
        }
        else
        {
            latestHoveredNumber.OnExit();
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (currentHoverNumber != -1)
            {
                dialedNumbers[dialCounter] = currentHoverNumber;
                dialCounter++;

                if (dialCounter >= maxNumberLenght)
                {
                    if (arraysAreEqual(dialedNumbers, correctNumber))
                    {
                        if (!correctNumberEntered)
                        {
                            Debug.Log("Correct number");
                            resetDialedNumber();
                            GetComponent<AudioSource>().Play();

                            correctNumberEntered = true;
                            dialCounter = 0;
                        }
                    }
                    else
                    {
                        Debug.Log("Wrong number");
                        resetDialedNumber();
                        dialCounter = 0;
                    }
                }

                dialedNumbersText.text = "";

                for (int i = 0; i < dialCounter; i++)
                {
                    dialedNumbersText.text += " " + dialedNumbers[i];
                }
                for (int i = 0; i < maxNumberLenght - dialCounter; i++)
                {
                    dialedNumbersText.text += " _";
                }
            }
        }
        if (!GetComponent<AudioSource>().isPlaying && correctNumberEntered)
        {
            doorToOpen.GetComponent<AudioSource>().Play();
            doorToOpen.GetComponentInChildren<OpenRotation>().isLocked = false;
        }

    }

    private void resetDialedNumber()
    {
        for(int i = 0; i < maxNumberLenght; i++)
        {
            latestHoveredNumber.OnExit();
            dialedNumbers[i] = -1;
        }
    }

    private bool arraysAreEqual(int[] x, int[] z)
    {
        for (int i = 0; i < maxNumberLenght; i++)
        {
            if (x[i] != z[i])
            {
                return false;
            }
        }
        return true;
    }
}
