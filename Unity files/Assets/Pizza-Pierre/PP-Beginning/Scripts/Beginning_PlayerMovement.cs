using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beginning_PlayerMovement : MonoBehaviour {

    public float speed;
    public float speedShift;

    public float speedShot;

    public int maxHp;
    private int hp;

    public GameObject jetpack;
    public Text jetUI;
    public float jetpackStrength;
    private bool ownsJetpack;
    private bool activatedJetpack;
    public int jetpackTime;
    public ParticleSystem leftRocket;
    public ParticleSystem rightRocket;
    private ParticleSystem[] rockets;

    public float jumpHeight;
    public Beginning_GameManager Beginning_GameManager;

    public GameObject shotPoint;
    public GameObject shot;

    private GameObject gesichtModel;

    public bool isGrounded;
    private UnityEngine.CharacterController characterController;

    private Vector3 startPosition;
    public bool isDead = false;

    private Rigidbody rb;

    private float timer;

    void Start()
    {
        hp = maxHp;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<UnityEngine.CharacterController>();
        gesichtModel = GameObject.Find("gesicht");
        timer = jetpackTime+1;
    }

    private void Update()
    {
        isGrounded = (Physics.Raycast(transform.position, -gesichtModel.transform.up, characterController.height / 1.8f));
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal*2, 0.0f, moveVertical*2);
        gesichtModel.transform.LookAt(transform.position + rb.velocity);
        if (ownsJetpack && Input.GetKeyDown(KeyCode.R))
        {
            activatedJetpack = true;
            jetUI.text = "";
            Invoke("StopJetpack", jetpackTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(movement * speedShift);
        }
        else
        {
            rb.AddForce(movement * speed);
        }
        if (Input.GetButton("Jump"))
        {
            if (isGrounded && !activatedJetpack)
            {
                rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }
            else if (activatedJetpack)
            {
                rb.AddForce(new Vector3(0, jetpackStrength, 0));
            }
        }

        if (activatedJetpack)
        {
            startCounter();
            if (Input.GetButton("Jump"))
            {
                Instantiate(leftRocket, GameObject.Find("JetpackPlayer").transform);
                Instantiate(rightRocket, GameObject.Find("JetpackPlayer").transform);
                leftRocket.Play();
                rightRocket.Play();
            }
            else
            {
                
                leftRocket.Stop();
                rightRocket.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject tmpShot = Instantiate(shot, shotPoint.transform.position, new Quaternion(0, 0, 0, 0));
            tmpShot.transform.LookAt(transform.position + rb.velocity);
            
            if(Vector3.Distance(rb.velocity, Vector3.zero) > 1.5f)
            {
                tmpShot.GetComponent<Rigidbody>().velocity = rb.velocity * speedShot;
            }
            else
            {
                tmpShot.GetComponent<Rigidbody>().velocity = rb.velocity.normalized * speedShot * 2;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!collision.gameObject.GetComponent<EnemyMovement>().isDead)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity * 8, ForceMode.Impulse);
                hp--;
                Beginning_GameManager.minusLifeUI();
                if (hp <= 0)
                {
                    PlayerRespawn();
                }
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Beginning_GameManager.addPoint();
        }
        if (other.gameObject.CompareTag("Jetpack"))
        {
            other.gameObject.SetActive(false);
            jetpack.SetActive(true);
            ownsJetpack = true;
            jetUI.text = "Press R to jet";
        }
        if(other.gameObject.CompareTag("Deathzone"))
        {
            hp--;
            Beginning_GameManager.minusLifeUI();
            PlayerRespawn();
        }
    }



    public void StopJetpack()
    {
        leftRocket.Stop();
        rightRocket.Stop();
        jetUI.text = "";
        timer = jetpackTime +1;
        ownsJetpack = false;
        activatedJetpack = false;
        jetpack.SetActive(false);
    }

    public void PlayerRespawn()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        transform.position = startPosition;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyMovement>().EnemyRespawn();
        }
        if(hp <= 0)
        {
            PlayerDeath();
        }
    }
    
    public void PlayerDeath()
    {
        if (!isDead)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
        hp = maxHp;
    }

    private void startCounter()
    {
        timer -= Time.deltaTime;
        jetUI.text = ((int)timer).ToString();
    }

    public void resetHits()
    {
        hp = maxHp;
    }

}
