using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EvilOven_PlayerMovement : MonoBehaviour {

    public Animator animator;

    [Header("HP Attributes")]
    public float maxHP;
    public Slider sliderHP;
    public float currentHP;
    public Image fill;
    public Text focusedEnemyName;

    [Header("Cursors")]
    public Texture2D cursorAttackTexture;
    public Texture2D cursorAttackTextureClick;
    public Texture2D cursorEatTexture;
    public Texture2D cursorEatTextureClick;
    public Texture2D cursorDestroyTexture;
    public Texture2D cursorDestroyTextureClick;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private EvilOven_GameManager gameManager;

    private NavMeshAgent navAgent;
    private GameObject focusedEnemy;

    private bool isDead;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EvilOven_GameManager>();
        maxHP /= 100;
        currentHP = maxHP;
        sliderHP.value = currentHP;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        focusedEnemy = null;
    }

    // Update is called once per frame
    void Update () {
        animator.SetFloat("velocity", navAgent.velocity.magnitude);
        if (currentHP > 1)
        {
            currentHP = 1;
        }else if(currentHP <= 0 && !isDead)
        {
            animator.SetTrigger("isDead");
            isDead = true;
            GetComponentInChildren<ParticleSystem>().startColor = Color.Lerp(Color.green, Color.black, 0.6f);
            GetComponentInChildren<ParticleSystem>().Play();
            Invoke("gameOver", 1);
        }
        if (sliderHP.value > 0.3)
        {
            fill.color = Color.red;
        }
        sliderHP.value = currentHP;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (focusedEnemy != null )
        {
            focusedEnemyName.text = focusedEnemy.GetComponentInChildren<Text>().text;
            navAgent.SetDestination(focusedEnemy.transform.position);
        }
        else
        {
            focusedEnemyName.text = "None";
        }

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide))
        {
            Cursor.SetCursor(cursorAttackTexture, hotSpot, cursorMode);
            if (Input.GetMouseButton(0) && gameManager.currentState == EvilOven_GameManager.gameStates.playing)
            {
                Cursor.SetCursor(cursorAttackTextureClick, hotSpot, cursorMode);
                focusedEnemy = hit.transform.gameObject;
            }
        }
        else if(Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Pickup"), QueryTriggerInteraction.Collide))
        {
            Cursor.SetCursor(cursorEatTexture, hotSpot, cursorMode);
            if (Input.GetMouseButton(0) && gameManager.currentState == EvilOven_GameManager.gameStates.playing)
            {
                Cursor.SetCursor(cursorEatTextureClick, hotSpot, cursorMode);
            }
        }
        else if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide))
        {
            Cursor.SetCursor(cursorDestroyTexture, hotSpot, cursorMode);
            if (Input.GetMouseButton(0) && gameManager.currentState == EvilOven_GameManager.gameStates.playing)
            {
                Cursor.SetCursor(cursorDestroyTextureClick, hotSpot, cursorMode);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        if (Input.GetMouseButtonDown(0) && gameManager.currentState == EvilOven_GameManager.gameStates.playing)
        {
            
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Ground")))
            {
                navAgent.SetDestination(hit.point);
                focusedEnemy = null;
            }
        }
        if (gameManager.currentState == EvilOven_GameManager.gameStates.lost || gameManager.currentState == EvilOven_GameManager.gameStates.won)
        {
            navAgent.SetDestination(transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soße"))
        {
            animator.SetBool("isSwimming", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Soße"))
        {
            animator.SetBool("isSwimming", false);
        }
    }

    private void gameOver()
    {
        gameManager.currentState = EvilOven_GameManager.gameStates.lost;
        gameManager.justSwitched = true;
    }

}
