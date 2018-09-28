using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelefonWählscheibe : MonoBehaviour, IInteractable {

    public GameObject _character;
    private Vector3 tmpLerpVector;
    public float smoothness;
    public GameObject escToExit;

    [SerializeField]
    private Transform telefonPosition;

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

    // Use this for initialization
    void Start () {
        Array.Resize(ref dialedNumbers, maxNumberLenght);
        resetDialedNumber();
        currentHoverNumber = -1;
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.currentState == GameManager.GameState.Calling)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("TelefonTaste"))
                {
                    latestHoveredNumber = hit.collider.GetComponent<NumberRecognition>();
                    latestHoveredNumber.OnEnter();
                }
                else if(latestHoveredNumber != null)
                {
                    latestHoveredNumber.OnExit();
                }
            }

            if (!CharacterController.isCalling)
            {
                GetComponent<BoxCollider>().enabled = false;
                CharacterController.isCalling = true;
            }
            if (!escToExit.activeSelf)
            {
                escToExit.SetActive(true);
            }
            tmpLerpVector = Vector3.Lerp(_character.transform.position, telefonPosition.position, 1f / smoothness);
            _character.transform.position = tmpLerpVector;

            if (Input.GetButtonDown("Cancel"))
            {
                GetComponent<BoxCollider>().enabled = true;
                CharacterController.isCalling = true;
                escToExit.SetActive(false);
                GameManager.currentState = GameManager.GameState.Playing;
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
                            Debug.Log("Correct number");
                            //resetDialedNumber();
                            dialCounter = 0;
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
        }
    }

    public void OnInteractionPressed()
    {
        if(GameManager.currentState != GameManager.GameState.Calling)
        {
            GameManager.currentState = GameManager.GameState.Calling;
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
