using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    
    // To determine which games are completed already and which are not.
    public static bool[] PCState = new bool[4];

    public enum GameState { Menu, Playing, Calling, OnPC}
    public static GameState currentState;

    public enum GameStage { Bathroom, Sleepingroom, Kitchen}
    public static GameStage currentStage;

    [SerializeField]
    private bool cheatsActive;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene == SceneManager.GetSceneByName("MafiaRoom"))
        {
            if(currentStage == GameStage.Sleepingroom)
            {
                ComputerSceneChange computerScript = GameObject.Find("computer").GetComponent<ComputerSceneChange>();
                //ComputerSceneChange[] computerScript = FindObjectsOfType(typeof(ScriptableObject)) as ComputerSceneChange[];
                computerScript.UpdateSavedValues();
            }
        }
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
                currentState = GameState.Menu;
                MenuTransition(true);
            }
            else
            {
                currentState = GameState.Playing;
                MenuTransition(false);
            }
        }

        if (cheatsActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                GameManager.PCState[0] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                GameManager.PCState[1] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                GameManager.PCState[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                GameManager.PCState[3] = true;
            }
        }
    }

    public void SetCurrentGameState(GameState state)
    {
        currentState = state;
    }

    public void MenuTransition(bool isOpening)
    {
        if (isOpening)
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

    public static void ChangePCState(int gameIndex)
    {
        PCState[gameIndex - 1] = true;
        // Play sound
        // Change lock on desk from red to green
    }
}
