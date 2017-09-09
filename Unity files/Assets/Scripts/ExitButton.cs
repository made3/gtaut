using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

    public Button exitButton;

	// Use this for initialization
	void Start () {
        exitButton.onClick.AddListener(ExitGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ExitGame()
    {
        Application.Quit();
    }
}
