using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    public float speed = 10.0F;
    public float speedRunning;
    public float speedCrouching;


    // Statemachine Booleans

    // Movement
    public bool isMoving;
    public bool isWalking;
    public bool isCrouching;
    public bool isRunning;

    public bool inCrouchTransition;
    public bool insRunTransition;

    public static bool isInMenu;



    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isInMenu)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);
        }
        checkState();
    }


    public void checkState()
    {
        if (!isInMenu)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                //switcher(inCrouchTransition);
                inCrouchTransition = true;
                crouchTransition();
                //switcher(inCrouchTransition);
                inCrouchTransition = false;
                //switcher(isCrouching);
                if (isCrouching)
                {
                    isCrouching = false;
                }
                else
                {
                    isCrouching = true;
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
            // TODO: Bewegungsanimation einfügen
            transform.position = transform.position + new Vector3(0, 1, 0);
            speed += speedCrouching;
        }
        else
        {
            // TODO: Bewegungsanimation einfügen
            transform.position = transform.position - new Vector3(0, 1, 0);
            speed -= speedCrouching;
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
