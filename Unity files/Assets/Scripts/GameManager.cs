﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum GameState { Menu, Playing, Calling, OnPC}
    public static GameState currentState;

    public enum GameStage { Bathroom, Sleepingroom, Kitchen}
    public static GameStage currentStage;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        currentState = GameState.Playing;
        currentStage = GameStage.Bathroom;
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

    public void setCurrentGameState(GameState state)
    {
        currentState = state;
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
