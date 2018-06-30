using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Delivery_GameManager : MonoBehaviour {

    public enum GameState { menu, playing, lost, won}
    public static GameState currentState;
    public GameObject Endbuttons;
    public GameObject wonUI;
    public GameObject lostUI;
    public Text timer;
    public Stopwatch timerStopwatch;

	// Use this for initialization
	void Start () {
        timerStopwatch = new Stopwatch();
        timerStopwatch.Start();
        Endbuttons.GetComponentsInChildren<UnityEngine.UI.Button>()[0].onClick.AddListener(onReplayPressed);
        Endbuttons.GetComponentsInChildren<UnityEngine.UI.Button>()[1].onClick.AddListener(onMenuPressed);
        Endbuttons.GetComponentsInChildren<UnityEngine.UI.Button>()[2].onClick.AddListener(onExitPressed);
        Endbuttons.SetActive(false);
        currentState = GameState.playing;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case GameState.menu:
                break;
            case GameState.playing:
                timer.text = "Timer: " + (float)timerStopwatch.ElapsedMilliseconds / 1000;
                break;
            case GameState.won:
                timerStopwatch.Stop();
                Endbuttons.SetActive(true);
                wonUI.SetActive(true);
                break;
            case GameState.lost:
                timerStopwatch.Stop();
                Endbuttons.SetActive(true);
                lostUI.SetActive(true);
                break;

        }

    }

    private void onReplayPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void onMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
    private void onExitPressed()
    {
        Application.Quit();
    }

}
