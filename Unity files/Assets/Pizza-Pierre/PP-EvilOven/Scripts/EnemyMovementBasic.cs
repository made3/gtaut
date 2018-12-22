using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class EnemyMovementBasic : MonoBehaviour
{

    public float maxHP;
    public float damageIn;
    public float damageOut;
    public float sliderColorChangeBorder;
    public ParticleSystem splatter;
    protected GameObject splatterParent;
    protected float currentHP;
    protected Stopwatch attackingTime;
    protected int tmpAttackingTime;

    public GameObject localCanvas;
    protected Text nameUI;
    protected Slider sliderHP;
    protected Image fill;

    public GameObject player;
    public long timeToGoHome;

    protected NavMeshAgent navAgent;
    protected Animator animator;
    protected bool isFollowing;
    protected bool isGoingHome;

    protected Vector3 startPosition;

    protected Stopwatch goHomeTimer;

    protected NameGenerator nameGen;
    protected bool isDead;

    protected Quaternion rotation;
    protected EvilOven_GameManager gameManager;

    // Use this for initialization
    public void Start()
    {
        nameUI = localCanvas.GetComponentInChildren<Text>();
        sliderHP = localCanvas.GetComponentInChildren<Slider>();
        fill = localCanvas.GetComponentsInChildren<Image>()[1];
        splatterParent = GameObject.Find("EnemyParticles");
        player = GameObject.Find("Player");

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EvilOven_GameManager>();
        damageOut /= 100;
        damageIn /= 100;
        tmpAttackingTime = 0;
        nameGen = gameObject.AddComponent<NameGenerator>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        goHomeTimer = new Stopwatch();
        attackingTime = new Stopwatch();
        timeToGoHome *= 1000;
        maxHP /= 100;
        currentHP = maxHP;
        sliderHP.value = maxHP;
        rotation = sliderHP.transform.rotation;
    }

    // Update is called once per frame
    public void Update()
    {

        localCanvas.transform.rotation = rotation;
        animator.SetFloat("velocity", navAgent.velocity.magnitude);

        if (isGoingHome && transform.position == startPosition)
        {
            isGoingHome = false;
        }

        if (isFollowing)
        {
            navAgent.SetDestination(player.transform.position);
        }
        else
        {
            if (goHomeTimer.ElapsedMilliseconds > timeToGoHome && !isGoingHome)
            {
                isGoingHome = true;
                goHomeTimer.Reset();
                navAgent.SetDestination(startPosition);
            }
        }
        if (isDead)
        {
            navAgent.SetDestination(transform.position);
        }
    }

        public void getRekt()
    {
        Destroy(this.gameObject);
        gameManager.enemyCounter--;
        if(gameManager.enemyCounter == 0)
        {
            gameManager.currentState = EvilOven_GameManager.gameStates.won;
            gameManager.justSwitched = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerFightKreis"))
        {
            animator.SetTrigger("isAttacking");
            other.gameObject.GetComponentInParent<EvilOven_PlayerMovement>().animator.SetTrigger("isAttacking");
            if ((int)attackingTime.ElapsedMilliseconds / 1000 > tmpAttackingTime && !isDead && other.gameObject.GetComponentInParent<EvilOven_PlayerMovement>().currentHP > 0)
            {
                other.gameObject.GetComponentInParent<EvilOven_PlayerMovement>().currentHP -= damageOut;
                if (other.gameObject.GetComponentInParent<EvilOven_PlayerMovement>().currentHP <= sliderColorChangeBorder / 100)
                {
                    other.gameObject.GetComponentInParent<EvilOven_PlayerMovement>().fill.color = Color.Lerp(Color.red, Color.black, 0.5f);
                }
                takeDamage();
                tmpAttackingTime++;
            }
        }
    }

    public void takeDamage()
    {
        currentHP -= damageIn;
        if (currentHP <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            Instantiate(splatter, transform).transform.parent = splatterParent.transform;
            Invoke("getRekt", 2);
        }
        sliderHP.value = currentHP;
        if(sliderHP.value <= sliderColorChangeBorder/100)
        {
            fill.color = Color.Lerp(Color.red, Color.black, 0.5f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUmkreis"))
        {
            isFollowing = true;
            goHomeTimer.Reset();
        }
        else if (other.CompareTag("Soße"))
        {
            //animator.SetBool("isSwimming", true);
        }
        else if (other.CompareTag("PlayerFightKreis"))
        {
            attackingTime.Start();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUmkreis"))
        {
            isFollowing = false;
            navAgent.SetDestination(transform.position);
            if (transform.position != startPosition)
            {
                isGoingHome = false;
                goHomeTimer.Reset();
                goHomeTimer.Start();
            }
        }
        else if (other.CompareTag("Soße"))
        {
            animator.SetBool("isSwimming", false);
        }
        else if (other.CompareTag("PlayerFightKreis"))
        {
            attackingTime.Stop();
        }

    }
}
