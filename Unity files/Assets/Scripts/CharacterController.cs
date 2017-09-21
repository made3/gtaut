using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    public float speed = 10.0F;
    public float speedRunning;
    public float speedCrouching;

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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInMenu && !isCalling)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

            if (inCrouchTransition)
            {
                crouchTransition();
            }
        }
        
        checkState();
    }


    public void checkState()
    {
        if (!isInMenu && !isCalling)
        {

            if (Input.GetButtonDown("Crouch"))
            {
                if (!inCrouchTransition)
                {
                    //switcher(inCrouchTransition);
                    inCrouchTransition = true;
                    crouchEndpoint = transform.position;
                    crouchTransition();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //switcher(isRunning);
                isRunning = true;
                runTransition();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //switcher(isRunning);
                isRunning = false;
                runTransition();
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {


            if (!isCalling)
            {
                //switcher(isInMenu);
                if (isInMenu)
                {
                    isInMenu = false;
                }
                else
                {
                    isInMenu = true;
                }
                menuTransition();
            }
            else
            {
                isCalling = false;
            }
        }
    }

    public void runTransition()
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

    public void crouchTransition()
    {
        if (isCrouching)
        {
            tmpLerpVariable = Mathf.Lerp(0, (crouchEndpoint + new Vector3(0,1,0)).y, 1f / crouchSmoothing);
            transform.position += new Vector3(0, tmpLerpVariable, 0);
            //transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y + 1, 1f / crouchSmoothing), transform.position.z);
            if (transform.position.y >= (crouchEndpoint + new Vector3(0, 1, 0)).y)
            {
                transform.position = new Vector3(transform.position.x, (crouchEndpoint + new Vector3(0, 1, 0)).y, transform.position.z);
                //switcher(inCrouchTransition);
                inCrouchTransition = false;
                isCrouching = false;
                speed += speedCrouching;
            }
        }
        else
        {
            tmpLerpVariable = Mathf.Lerp(0, (crouchEndpoint - new Vector3(0, 1, 0)).y, 1f / crouchSmoothing);
            transform.position -= new Vector3(0, tmpLerpVariable, 0);
            //transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y - 1, 1f / crouchSmoothing), transform.position.z);
            if (transform.position.y <= (crouchEndpoint - new Vector3(0, 1, 0)).y)
            {
                transform.position = new Vector3 (transform.position.x, (crouchEndpoint - new Vector3(0, 1, 0)).y, transform.position.z);
                //switcher(inCrouchTransition);
                inCrouchTransition = false;
                isCrouching = true;
                speed -= speedCrouching;
            }
        }
    }



    // Evtl auslagern
    public void menuTransition()
    {
        if (isInMenu)
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

    public void switcher(bool toSwitch)
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
