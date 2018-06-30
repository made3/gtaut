using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerClickHandler {

    public Button exitButton;

	// Use this for initialization
	void Start () {

   }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ExitGame()
    {
        Application.Quit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
