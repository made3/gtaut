using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EvilOven_GameManager : MonoBehaviour {
    
    public ParticleSystem knusper;

    public int enemyCounter;

    private Vector2 tmpSpawnPoint;

    public GameObject playerUmkreis;

    public enum gameStates { menu, playing, won, lost}

    public gameStates currentState;

    public bool justSwitched;

    [Header("Spawners")]

    public int anzahlBurger;

    public GameObject burgersParent;
    public GameObject burger;

    public int anzahlBrokkoli;
    public GameObject brokkoli;
    public int anzahlAnanas;
    public GameObject ananas;
    public int anzahlTomate;
    public GameObject tomate;



    [Header("Title Screen")]

    public GameObject startingButtons;
    public GameObject title;

    [Header("Playing Screen")]

    public GameObject minimap;
    public GameObject hpBar;
    public GameObject hpText;
    public GameObject focusedEnemy;
    public GameObject ofenBar;

    [Header("Ending Screen")]

    public Image endingOverlay;
    public Color won;
    public Color lost;
    public float fadingSpeed;
    public GameObject endingButtons;
    public Text wonLostText;
    private Color startColor;
    private float lerpTmp;

	// Use this for initialization
	void Start () {
        startColor = endingOverlay.color;
        justSwitched = true;

        for (int i = 0; i < anzahlBrokkoli; i++)
        {
            tmpSpawnPoint = Random.insideUnitCircle * 27;
            while (Vector2.Distance(playerUmkreis.transform.position, tmpSpawnPoint) < playerUmkreis.GetComponent<SphereCollider>().radius+1f)
            {
                tmpSpawnPoint = Random.insideUnitCircle * 27;
            }
            Instantiate(brokkoli, new Vector3(tmpSpawnPoint.x, 0, tmpSpawnPoint.y), Quaternion.identity);

        }
        for (int i = 0; i < anzahlAnanas; i++)
        {

            tmpSpawnPoint = Random.insideUnitCircle * 27;

            while (Vector2.Distance(playerUmkreis.transform.position, tmpSpawnPoint) < playerUmkreis.GetComponent<SphereCollider>().radius+1f)
            {
                tmpSpawnPoint = Random.insideUnitCircle * 27;
            }
            Instantiate(ananas, new Vector3(tmpSpawnPoint.x, 0, tmpSpawnPoint.y), Quaternion.identity);
        }
        for (int i = 0; i < anzahlTomate; i++)
        {
            tmpSpawnPoint = Random.insideUnitCircle * 27;

            while (Vector2.Distance(playerUmkreis.transform.position, tmpSpawnPoint) < playerUmkreis.GetComponent<SphereCollider>().radius+1f)
            {
                tmpSpawnPoint = Random.insideUnitCircle * 27;
            }
            Instantiate(tomate, new Vector3(tmpSpawnPoint.x, 0, tmpSpawnPoint.y), Quaternion.identity);
        }

        enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;

        endingButtons.GetComponentsInChildren<UnityEngine.UI.Button>()[0].onClick.AddListener(onRestartClick);
        endingButtons.GetComponentsInChildren<UnityEngine.UI.Button>()[1].onClick.AddListener(onExitClick);
        startingButtons.GetComponentsInChildren <UnityEngine.UI.Button>()[0].onClick.AddListener(onStartClick);
        startingButtons.GetComponentsInChildren<UnityEngine.UI.Button>()[1].onClick.AddListener(onExitClick);

        currentState = gameStates.menu;

        burger.GetComponentInChildren<Burger>().knuspers = knusper;
		for(int i = 0; i < anzahlBurger; i++)
        {
            tmpSpawnPoint = Random.insideUnitCircle * 27;
            Instantiate(burger, new Vector3(tmpSpawnPoint.x, 0, tmpSpawnPoint.y), Quaternion.identity).transform.parent = burgersParent.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Desktop");
        }
        switch (currentState)
        {
            case gameStates.menu:
                Menu();
                break;
            case gameStates.playing:
                Playing();
                break;
            case gameStates.won:
                Won();
                break;
            case gameStates.lost:
                Lost();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Menu()
    {
        if (justSwitched)
        {
            startingButtons.GetComponent<Animator>().SetBool("scale", true);
            justSwitched = false;
        }
    }
    void Playing()
    {
        if (justSwitched)
        {
            minimap.SetActive(true);
            hpBar.SetActive(true);
            hpText.SetActive(true);
            focusedEnemy.SetActive(true);
            ofenBar.SetActive(true);
            title.SetActive(false);
            justSwitched = false;
        }

    }
    void Won()
    {
        if (justSwitched)
        {
            wonLostText.text = "You are still vomit-free!";
            wonLostText.gameObject.SetActive(true);
            endingButtons.SetActive(true);
            endingButtons.GetComponent<Animator>().SetBool("scale", true);
            if (!GameManager.PCState[1])
            {
                GameManager.ChangePCState(2);
            }
            justSwitched = false;
        }
        endingOverlay.color = Color.Lerp(startColor, won, lerpTmp += Time.deltaTime * fadingSpeed);
    }
    void Lost()
    {
        if (justSwitched)
        {
            wonLostText.text = "Oh no, you vomited!";
            wonLostText.gameObject.SetActive(true);
            endingButtons.SetActive(true);
            endingButtons.GetComponent<Animator>().SetBool("scale", true);
            justSwitched = false;
        }
        endingOverlay.color = Color.Lerp(startColor, lost, lerpTmp += Time.deltaTime * fadingSpeed);
    }

    void onStartClick()
    {
        currentState = gameStates.playing;
        justSwitched = true;
        startingButtons.SetActive(false);
    }

    void onRestartClick()
    {
        SceneManager.LoadScene("PPEvilOven");
    }
    void onExitClick()
    {
        SceneManager.LoadScene("Desktop");
    }

}
