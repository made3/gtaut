using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState { Menu, Playing, Calling}
    public static GameState currentState;

	// Use this for initialization
	void Start () {
        currentState = GameState.Playing;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if(currentState != GameState.Menu)
            {
                menuTransition();
                currentState = GameState.Menu;
            }
            else
            {
                menuTransition();
                currentState = GameState.Playing;
            }
        }
	}

    public void menuTransition()
    {
        if (currentState == GameState.Menu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
