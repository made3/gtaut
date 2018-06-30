using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Collector_GameManager : MonoBehaviour {

    public enum States { Menu, Playing, Won, Lost}
    public States currentState;
    public GameObject[] menuUI;
    public GameObject[] playingUI;
    public GameObject[] endUI;

    private bool justSwitched;

    public GameObject lifeUIFP;

    [Header("Buttons")]
    public UnityEngine.UI.Button menuStart;
    public UnityEngine.UI.Button menuExit;
    public UnityEngine.UI.Button endRestart;
    public UnityEngine.UI.Button endExit;

    // Use this for initialization
    void Start () {
        currentState = States.Menu;
        menuStart.onClick.AddListener(onClickStart);
        menuExit.onClick.AddListener(onClickExit);
        endRestart.onClick.AddListener(onClickRestart);
        endExit.onClick.AddListener(onClickExit);
    }

    // Update is called once per frame
    void Update () {
        switch (currentState)
        {
            case (States.Menu):

                break;
            case (States.Playing):
                if (!justSwitched)
                {
                    foreach (GameObject g in menuUI)
                    {
                        g.SetActive(false);
                    }
                    foreach (GameObject g in playingUI)
                    {
                        g.SetActive(true);
                    }
                    justSwitched = true;
                }
                break;
            case (States.Lost):
                if (justSwitched)
                {
                    endUI[2].GetComponent<Text>().text = "YOU LOST!";
                    foreach (GameObject g in playingUI)
                    {
                        g.SetActive(false);
                        lifeUIFP.SetActive(false);
                    }
                    foreach (GameObject g in endUI)
                    {
                        g.SetActive(true);
                    }
                    justSwitched = false;
                }
                break;
            case (States.Won):
                if (justSwitched)
                {
                    endUI[2].GetComponent<Text>().text = "YOU WON!";
                    foreach (GameObject g in playingUI)
                    {
                        g.SetActive(false);
                        lifeUIFP.SetActive(false);
                    }
                    foreach (GameObject g in endUI)
                    {
                        g.SetActive(true);
                    }
                    justSwitched = false;
                }
                break;
        }
	}

    void onClickStart()
    {
        currentState = States.Playing;
    }

    void onClickRestart()
    {
        SceneManager.LoadScene(0);
    }

    void onClickExit()
    {
        Application.Quit();
    }

}
