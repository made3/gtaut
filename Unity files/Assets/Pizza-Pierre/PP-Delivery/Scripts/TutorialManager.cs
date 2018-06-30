using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject[] tutorialUIs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name){
            case ("Movement"):
                tutorialUIs[0].SetActive(true);
                break;
            case ("BrokkoliGegner"):
                tutorialUIs[1].SetActive(true);
                break;
            case ("AnanasGegner"):
                tutorialUIs[2].SetActive(true);
                break;
            case ("TomateGegner"):
                tutorialUIs[3].SetActive(true);
                break;
            case ("Swim"):
                tutorialUIs[4].SetActive(true);
                break;
            case ("Ice"):
                tutorialUIs[5].SetActive(true);
                break;
            case ("Sauce"):
                tutorialUIs[6].SetActive(true);
                break;
            case ("Finish"):
                tutorialUIs[7].SetActive(true);
                break;
            case ("Secret"):
                tutorialUIs[8].SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case ("Movement"):
                tutorialUIs[0].SetActive(false);
                break;
            case ("BrokkoliGegner"):
                tutorialUIs[1].SetActive(false);
                break;
            case ("AnanasGegner"):
                tutorialUIs[2].SetActive(false);
                break;
            case ("TomateGegner"):
                tutorialUIs[3].SetActive(false);
                break;
            case ("Swim"):
                tutorialUIs[4].SetActive(false);
                break;
            case ("Ice"):
                tutorialUIs[5].SetActive(false);
                break;
            case ("Sauce"):
                tutorialUIs[6].SetActive(false);
                break;
            case ("Finish"):
                tutorialUIs[7].SetActive(false);
                break;
            case ("Secret"):
                tutorialUIs[8].SetActive(false);
                break;

        }
    }
}
