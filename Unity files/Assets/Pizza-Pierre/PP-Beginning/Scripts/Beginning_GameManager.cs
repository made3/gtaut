using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Beginning_GameManager : MonoBehaviour {

    private int points;
    private int maxPoints;

    private int health;
    private int currentUIhp = 0;

    public Beginning_PlayerMovement playerMovement;

    public Text scoreUI;
    public Text submitUI;
    public GameObject submitUIGO;
    private float x = 0;
    public Vector3 fontMinSize;
    public float movementSpeed;
    public Vector3 fontMaxSize;
    private Vector3 tmpFontSize;

    public GameObject wonUI;
    public State currentState;
    private GameObject[] coinAmount;

    private GameObject[] jetpacks;
    public GameObject lifeIcon;
    private GameObject[] lifeUI;

    public ParticleSystem konfetti;
    private bool switcher;

    public enum State { Menu, Playing, Won, Lost}

    void Start()
    {
        konfetti.Stop();
        currentState = State.Menu;
        for(int i = 0; i < playerMovement.maxHp; i++)
        {
            GameObject.Instantiate(lifeIcon);
        }
        lifeUI = GameObject.FindGameObjectsWithTag("LifeUI");
        Array.Sort(lifeUI, CompareObNames);
        for (int i = 0; i < playerMovement.maxHp; i++)
        {
            lifeUI[i].transform.SetParent(GameObject.Find("LifeUI").transform, false);
            lifeUI[i].transform.localPosition += new Vector3(230 * i, 0, 0);
        }
        Array.Reverse(lifeUI);
        coinAmount = GameObject.FindGameObjectsWithTag("Coin");
        jetpacks = GameObject.FindGameObjectsWithTag("Jetpack");
        maxPoints = coinAmount.Length;
        scoreUI.text = "CHEDDAR: " + points + " / " + maxPoints;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Desktop");
        }
        if(currentState == State.Playing)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().useGravity = true;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        if (currentState == State.Won || currentState == State.Lost || currentState == State.Menu)
        {
            if (Input.GetButtonDown("Submit"))
            {
                konfetti.Stop();
                wonUI.SetActive(false);
                if(currentState == State.Lost)
                {
                    playerMovement.PlayerDeath();
                }else if(currentState == State.Won)
                {
                    playerMovement.resetHits();
                }
                foreach (GameObject c in coinAmount)
                {
                    c.SetActive(true);
                }
                foreach (GameObject j in jetpacks)
                {
                    j.SetActive(true);
                }
                foreach (GameObject lui in lifeUI)
                {
                    currentUIhp = 0;
                    lui.SetActive(true);
                }
                points = 0;
                scoreUI.text = "CHEDDAR: " + points + " / " + maxPoints;
                submitUI.text = "";
                currentState = State.Playing;
                playerMovement.StopJetpack();
            }
            else
            {
                if(currentState == State.Menu)
                {
                    submitUI.text = "Enter to start";
                }
                else if(currentState == State.Won)
                {
                    konfetti.Play();
                    wonUI.GetComponent<Text>().text = "YOU WON!";
                    wonUI.SetActive(true);
                    submitUI.text = "Enter to restart";

                    // GAME WON
                    if (!GameManager.PCState[0])
                    {
                        GameManager.ChangePCState(1);
                    }
                }
                else if(currentState == State.Lost)
                {
                    wonUI.GetComponent<Text>().text = "YOU LOST!";
                    wonUI.SetActive(true);
                    submitUI.text = "Enter to restart";
                }


                fontAnimation();

                GameObject.FindGameObjectWithTag("Player").GetComponent<Beginning_PlayerMovement>().PlayerRespawn();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().useGravity = false;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        if (playerMovement.isDead)
        {
            currentState = State.Lost;
        }
        if (points == maxPoints)
        {
            currentState = State.Won;
        }
    }

    public void addPoint()
    {
        points++;
        scoreUI.text = "CHEDDAR: " + points + " / " + maxPoints;
    }

    public void minusLifeUI()
    {
        if(currentUIhp < lifeUI.Length)
        {
            lifeUI[currentUIhp].SetActive(false);
            currentUIhp++;
        }
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    private void fontAnimation()
    {
        tmpFontSize = Vector3.Lerp(fontMinSize, fontMaxSize, x);

        if (x >= 1)
        {
            switcher = true;
        }
        else if (x <= 0)
        {
            switcher = false;
        }
        if (!switcher)
        {
            x += Time.deltaTime * movementSpeed;
        }
        else
        {
            x -= Time.deltaTime * movementSpeed;
        }
        submitUIGO.transform.localScale = tmpFontSize;
    }
}
