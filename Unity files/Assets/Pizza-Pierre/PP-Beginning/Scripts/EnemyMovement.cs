using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject player;
    public GameObject enemyModel;
    private Beginning_GameManager Beginning_GameManager;
    private Rigidbody rb;
    private Vector3 startPosition;
    public int hits;
    public int hp;
    public bool isDead;

	// Use this for initialization
	void Start () {
        Beginning_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Beginning_GameManager>();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Beginning_GameManager.currentState == Beginning_GameManager.State.Won || Beginning_GameManager.currentState == Beginning_GameManager.State.Lost)
        {
            hits = 0;
            isDead = false;
        }
        if (!isDead)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
            enemyModel.transform.LookAt(transform.position + rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deathzone"))
        {
            EnemyRespawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            hits++;
            if (hits >= hp)
            {
                EnemyDeath();
            }
        }
    }

    public void EnemyRespawn()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        transform.position = startPosition;
    }

    public void EnemyDeath()
    {
        isDead = true;
    }
}
