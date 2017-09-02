using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float walkingSpeed = 3f;
    public float runningSpeed = 4f;
    public float crouchingSpeed = 1f;
    private float speed;
    public bool isCrouched;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        isCrouched = false;
        speed = walkingSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        if (isCrouched)
        {
            speed = crouchingSpeed;
        }
        else
        {
            speed = walkingSpeed;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = runningSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkingSpeed;
            }
        }
        
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        

        transform.Translate(straffe, 0, translation);

        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouched)
            {
                transform.position = transform.position + new Vector3(0, 1, 0);
                isCrouched = false;
            }
            else
            {
                transform.position = transform.position - new Vector3(0, 1, 0);
                isCrouched = true;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

	}
}
