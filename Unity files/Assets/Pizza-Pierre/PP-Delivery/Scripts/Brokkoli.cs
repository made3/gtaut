using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brokkoli : MonoBehaviour {

    private Animator anim;
    private UnityEngine.CharacterController charController;
    public GameObject player;
    public float movingSpeed;
    public float focusRange;

    public bool isPingPongActive;
    public bool walkRight;
    private bool isDead;


    public float Gravity = 1.5f;
    private Vector3 gravity = Vector3.zero;

    private Vector3 startPosition;


    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        anim = GetComponent<Animator>();
        charController = GetComponent<UnityEngine.CharacterController>();
        if (walkRight)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        if (isPingPongActive)
        {
            anim.SetBool("isWalking", true);
        }
    }

        // Update is called once per frame
        void Update () {
        if (!charController.isGrounded)
        {
            gravity += new Vector3(0, -Gravity, 0) * Time.deltaTime;
        }

        if (isPingPongActive)
        {
            if (walkRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            charController.Move(this.transform.forward * Time.deltaTime * movingSpeed + gravity);
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < focusRange)
            {
                if (!anim.GetBool("isWalking"))
                {
                    anim.SetBool("isIdling", false);
                    anim.SetBool("isWalking", true);
                }
                if (transform.position.x - player.transform.position.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                charController.Move(this.transform.forward * Time.deltaTime * movingSpeed + gravity);
            }
            else
            {
                if (anim.GetBool("isWalking"))
                {
                    anim.SetBool("isIdling", true);
                    anim.SetBool("isWalking", false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            walkRight = !walkRight;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*
        if (hit.gameObject.CompareTag("Ground"))
        {
            if (isPingPongActive)
            {
                if(name == "brokkoli")
                {
                    Debug.Log(transform.position + " und " + startPosition + hit.gameObject.GetComponent<BoxCollider>().size);
                }
            }
        }*/
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.gameObject.GetComponent<Player>().getHit();
            anim.SetTrigger("isAttacking");
        }
    }

    public void die()
    {
        if (!isDead)
        {
            anim.SetTrigger("isDead");
            isDead = true;
            Invoke("destroyMe", 1);
        }
    }

    private void destroyMe()
    {
        Destroy(this.gameObject);
    }
}
