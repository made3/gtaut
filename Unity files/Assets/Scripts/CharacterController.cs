using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance = null;

    public float speed = 10.0F;
    public float speedRunning;
    public float speedCrouching;

    [SerializeField]
    private float crouchHeight = 1;

    public float crouchSmoothing = 10.0f;
    private Vector3 crouchEndpoint;
    private float tmpLerpVariable;

    // Statemachine Booleans

    // Movement
    public bool isMoving;
    public bool isWalking;
    public bool isCrouching;
    public bool isRunning;

    public bool inCrouchTransition;
    public bool insRunTransition;

    public static bool isInMenu;
    public static bool isCalling;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        Cursor.visible = false;
    }

    public GameObject GetPlayer()
    {
        return gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentState == GameManager.GameState.Playing)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

            if (inCrouchTransition)
            {
                CrouchTransition();
            }

            CheckState();
        }
    }


    public void CheckState()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (!inCrouchTransition)
            {
                //switcher(inCrouchTransition);
                inCrouchTransition = true;
                crouchEndpoint = Camera.main.transform.position;
                CrouchTransition();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //switcher(isRunning);
            isRunning = true;
            RunTransition();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //switcher(isRunning);
            isRunning = false;
            RunTransition();
        }
    }

    public void RunTransition()
    {
        if (isRunning)
        {
            speed += speedRunning;
        }
        else
        {
            speed -= speedRunning;
        }
    }

    public void CrouchTransition()
    {
        if (isCrouching)
        {
            tmpLerpVariable = 1 / crouchSmoothing;
            Camera.main.transform.position += new Vector3(0, tmpLerpVariable, 0);

            if (Camera.main.transform.position.y >= (crouchEndpoint + new Vector3(0, crouchHeight, 0)).y)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, (crouchEndpoint + new Vector3(0, crouchHeight, 0)).y, Camera.main.transform.position.z);
                //switcher(inCrouchTransition);
                inCrouchTransition = false;
                isCrouching = false;
                speed += speedCrouching;
            }
        }
        else
        {
            tmpLerpVariable = 1 / crouchSmoothing;
            Camera.main.transform.position -= new Vector3(0, tmpLerpVariable, 0);

            if (Camera.main.transform.position.y <= (crouchEndpoint - new Vector3(0, crouchHeight, 0)).y)
            {
                Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, (crouchEndpoint - new Vector3(0, crouchHeight, 0)).y, Camera.main.transform.position.z);
                //switcher(inCrouchTransition);
                inCrouchTransition = false;
                isCrouching = true;
                speed -= speedCrouching;
            }
        }
    }

    public void Switcher(bool toSwitch)
    {
        Debug.Log(toSwitch);
        if (toSwitch)
        {
            toSwitch = false;
        }
        else
        {
            toSwitch = true;
        }
    }

}
