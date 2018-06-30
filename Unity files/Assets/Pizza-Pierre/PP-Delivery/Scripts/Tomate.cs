using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tomate : MonoBehaviour
{

    private Animator anim;
    private UnityEngine.CharacterController charController;
    public GameObject player;
    public float movingSpeed;
    public float focusRange;

    public bool isPingPongActive;
    public bool walkRight;
    private bool isDead;

    public GameObject schissPrefab;
    public Transform schissParent;
    public Transform schissSpawnPoint;
    private Stopwatch timeToShit;
    public float bombDropDelay;
    private int tmpShitTime = 0;

    public float Gravity = 1.5f;
    private Vector3 gravity = Vector3.zero;

    private Vector3 startPosition;


    // Use this for initialization
    void Start()
    {
        timeToShit = new Stopwatch();
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
    void Update()
    {
        if (isDead)
        {
            gravity = new Vector3 (0, -Gravity,0);
        }
        else
        {
            gravity = Vector3.zero;
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
                if (!timeToShit.IsRunning)
                {
                    timeToShit.Start();
                }

                if (timeToShit.ElapsedMilliseconds >= tmpShitTime * 1000)
                {
                    Instantiate(schissPrefab, schissSpawnPoint.position, schissPrefab.transform.rotation, schissParent);
                    tmpShitTime += (int) bombDropDelay;
                }

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
                if (timeToShit.IsRunning)
                {
                    timeToShit.Reset();
                    timeToShit.Stop();
                    tmpShitTime = 0;
                }
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
    }

    public void die()
    {
        if (!isDead)
        {
            anim.SetTrigger("isDead");
            isDead = true;
            Invoke("destroyMe", 1.5f);
        }
    }

    private void destroyMe()
    {
        Destroy(this.gameObject);
    }
}
