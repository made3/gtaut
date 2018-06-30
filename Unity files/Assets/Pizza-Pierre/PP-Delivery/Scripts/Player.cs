using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class Player : MonoBehaviour
{

    private Animator anim;

    [Header("Life")]

    public int life = 2;
    public GameObject lifeUI;
    public GameObject lifeText;

    [Header("Fight")]

    public bool isInvincible;
    private Stopwatch invincibleStopwatch;
    private int invincibleCounter = 0;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Pickups")]

    public Pickups currentPickup;
    public enum Pickups { None, Gatling, Jetpack };
    public bool isPickupActive;
    private Stopwatch pickupStopwatch;
    public int pickupDuration;
    public GameObject pickupPressE;
    public GameObject pickupJetpackUI;
    public GameObject pickupGatlingUI;
    public Slider pickupSlider;
    public GameObject gatlingGun;
    public GameObject jetpack;
    private ParticleSystem.EmissionModule emissionModule;
    public GameObject maisPrefab;
    public Transform maisParent;
    public Transform maisSpawnTransform;

    [Header("Movement")]

    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    private Vector3 move = Vector3.zero;
    private bool canDoubleJump = true;
    private UnityEngine.CharacterController characterController = null;
    public bool isJumping = false;
    private float lastYPos;
    public bool isFalling = false;
    private bool landed = false;
    public float JumpForce = 2f;
    public static bool isSwimming;
    public float swimmingTime;
    private Stopwatch swimmingStopwatch;
    public Slider swimmingSlider;
    public GameObject swimmingBubbles;
    private bool isOnIce;
    private bool isOnSticky;
    private bool isSlowingDown;
    private Stopwatch slipperingTimer;
    private Vector3 lastInput;

    [Header("Physics")]

    public float Gravity = 9.81f;
    private Vector3 gravity = Vector3.zero;

    private enum AnimState
    {
        Idling,
        Walking,
        Jumping,
        Swimming
    };

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < life; i++)
        {
            Instantiate(lifeUI, lifeText.transform).transform.localPosition = new Vector3(450 * i + 200, 0, 0);
        }
        pickupStopwatch = new Stopwatch();
        swimmingStopwatch = new Stopwatch();
        slipperingTimer = new Stopwatch();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        invincibleStopwatch = new Stopwatch();
        anim = GetComponent<Animator>();
        characterController = GetComponent<UnityEngine.CharacterController>();
        if(characterController == null)
        {
            Debug.LogError("CharacterController ist null bei Player Script.");
        }
        characterController.detectCollisions = true;
        pickupSlider.maxValue = pickupDuration;
        pickupDuration *= 1000;
        swimmingSlider.maxValue = swimmingTime;
        swimmingTime *= 1000;
        emissionModule = jetpack.GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        checkForInvincible();

        pickupThings();

        if (isSwimming)
        {
            canDoubleJump = true;

            swimmingSlider.value = (float)swimmingStopwatch.ElapsedMilliseconds / 1000;
            if(swimmingStopwatch.ElapsedMilliseconds >= swimmingTime)
            {
                swimmingStopwatch.Stop();
                getHitFromWater();
            }
        }

        if (transform.position.y - lastYPos < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        lastYPos = transform.position.y;

        if(lastInput.x > Mathf.Abs(Input.GetAxis("Horizontal")))
        {
            isSlowingDown = true;
        }
        else
        {
            isSlowingDown = false;
        }
        lastInput.x = Mathf.Abs(Input.GetAxis("Horizontal"));

        // Mathf.Abs gibt den Absolutwerk zurück, immer positiv.
        move = Mathf.Abs(Input.GetAxis("Horizontal")) * this.transform.forward * Time.deltaTime;

        if (isSlowingDown && isOnIce && characterController.isGrounded)
        {
            if (!slipperingTimer.IsRunning)
            {
                slipperingTimer.Start();
            }
            if(slipperingTimer.ElapsedMilliseconds <= 1000)
            {
                move = 1 * this.transform.forward * Time.deltaTime;
            }
            else
            {
                slipperingTimer.Reset();
                slipperingTimer.Stop();
                isSlowingDown = false;
            }
        }else if (isSlowingDown && isOnSticky && characterController.isGrounded)
        {
            move = new Vector3(0, 0, 0);
        }

        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= runSpeed;
            if (anim.GetBool("isWalking"))
            {
                anim.speed = 2f;
            }

        }
        else
        {
            move *= walkSpeed;
            anim.speed = 1;
        }

        if (!characterController.isGrounded)
        {
            gravity += new Vector3(0, -Gravity, 0) * Time.deltaTime;
        }
        else
        {
            // LANDED NÖTIG, DA SICH SONST GRAVITATION AUFBAUT, WENN MAN FÄLLT

            if (landed)
            {
                gravity = Vector3.zero;
                landed = false;
            }

        }
        if (isJumping)
        {
            landed = true;
            gravity = Vector3.zero;
            gravity.y = JumpForce;
            isJumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if(isPickupActive && currentPickup == Pickups.Jetpack)
            {

            }
            else
            {
                if (characterController.isGrounded)
                {
                    canDoubleJump = true;
                    isJumping = true;
                }
                else if (canDoubleJump)
                {
                    canDoubleJump = false;
                    isJumping = true;
                }
            }

        }
        
        move += gravity;

        if(Delivery_GameManager.currentState == Delivery_GameManager.GameState.playing)
        {
            characterController.Move(move);
        }

        UpdateAnimation();

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup") && currentPickup == Pickups.None)
        {
            if (other.gameObject.name.Contains("Gatling"))
            {
                currentPickup = Pickups.Gatling;
                gatlingGun.SetActive(true);
                pickupGatlingUI.SetActive(true);
            }
            else if (other.gameObject.name.Contains("Jetpack"))
            {
                currentPickup = Pickups.Jetpack;
                jetpack.SetActive(true);
                pickupJetpackUI.SetActive(true);
            }
            pickupPressE.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            Delivery_GameManager.currentState = Delivery_GameManager.GameState.won;
        }else if(other.gameObject.layer == 4)
        {
            isSwimming = true;
            swimmingStopwatch.Start();
            swimmingBubbles.SetActive(true);
            Gravity /= 2;
            JumpForce /= 4;
            SwitchAnim(AnimState.Swimming);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            isSwimming = false;
            swimmingStopwatch.Stop();
            swimmingStopwatch.Reset();
            swimmingBubbles.SetActive(false);
            swimmingSlider.value = 0;
            Gravity *= 2;
            JumpForce *= 4;
            SwitchAnim(AnimState.Jumping);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("BrokkoliHead"))
        {
            isJumping = true;
            hit.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("isDead");
            if(hit.gameObject.transform.parent.name == "brokkoli")
            {
                hit.gameObject.transform.parent.GetComponent<Brokkoli>().die();
            }else if(hit.gameObject.transform.parent.name == "tomate")
            {
                hit.gameObject.transform.parent.GetComponent<Tomate>().die();
            }
            //StartCoroutine(killEnemy(hit.gameObject.transform.parent.gameObject));
        }
        if (hit.gameObject.CompareTag("GetHit"))
        {
            getHit();
        }
        if (hit.gameObject.CompareTag("Death"))
        {
            life = 0;
            getHit();
        }
        if (hit.gameObject.CompareTag("Eiswürfel"))
        {
            if (!isOnIce)
            {
                isOnIce = true;
            }
        }else if (hit.gameObject.CompareTag("Soße"))
        {
            if (!isOnSticky)
            {
                isOnSticky = true;
            }
        }
        else
        {
            if (isOnSticky)
            {
                isOnSticky = false;
            }
            if (isOnIce)
            {
                isOnIce = false;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (isJumping)
        {
            SwitchAnim(AnimState.Jumping);
        }
        if (Mathf.Approximately(move.x, 0f))
        {
            SwitchAnim(AnimState.Idling);
        }
        else
        {
            SwitchAnim(AnimState.Walking);
        }
    }

    private void SwitchAnim(AnimState state)
    {
        if(isSwimming)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isSwimming", true);
        }
        else
        {
            switch (state)
            {
                case AnimState.Idling:
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdling", true);
                    break;
                case AnimState.Walking:
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdling", false);
                    break;
                case AnimState.Jumping:
                    anim.SetBool("isSwimming", false);
                    anim.SetTrigger("isJumping");
                    break;
            }
        }
    }

    public IEnumerator killEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(enemy);
    }
    public void getHit()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            invincibleStopwatch.Start();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                GetComponent<ParticleSystem>().Play();
                if (life <= 1)
                {
                    life--;
                    lifeText.GetComponentsInChildren<Image>()[life].gameObject.SetActive(false);
                    anim.SetTrigger("isDead");
                    Invoke("lost", 1);
                }
                else
                {
                    life--;
                    lifeText.GetComponentsInChildren<Image>()[life].gameObject.SetActive(false);
                    anim.SetTrigger("gotHit");
                }
            }
        }
    }

    private void getHitFromWater()
    {
        getHit();
        Invoke("getHitFromWater", 8);
    }

    private void checkForInvincible()
    {
        if (isInvincible)
        {
            if(invincibleStopwatch.ElapsedMilliseconds >= invincibleCounter)
            {
                skinnedMeshRenderer.enabled = !skinnedMeshRenderer.enabled;
                invincibleCounter += 150;
                if(invincibleCounter >= 1200)
                {
                    invincibleStopwatch.Stop();
                    invincibleStopwatch.Reset();
                    invincibleCounter = 0;
                    isInvincible = false;
                }
            }
        }
    }

    private void pickupThings()
    {
        if(currentPickup != Pickups.None && !isPickupActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupPressE.SetActive(false);
                isPickupActive = true;
                pickupStopwatch.Start();

                if(currentPickup == Pickups.Jetpack)
                {
                    Gravity /= 2;
                }else if(currentPickup == Pickups.Gatling)
                {
                    gatlingGun.GetComponent<Animator>().SetBool("isActive", true);
                }
            }
        }

        if (isPickupActive)
        {
            pickupSlider.value = (float)pickupStopwatch.ElapsedMilliseconds / 1000;

            if (currentPickup == Pickups.Jetpack)
            {
                if (Input.GetButton("Jump"))
                {
                    emissionModule.rateOverTime = 20;
                    jetpack.GetComponent<ParticleSystem>().Play();
                    gravity -= new Vector3(0, -Gravity, 0) * Time.deltaTime * 1.5f;
                }
                else
                {
                    emissionModule.rateOverTime = 0;
                }
            }
            else if(currentPickup == Pickups.Gatling)
            {
                Instantiate(maisPrefab, maisSpawnTransform.position, transform.rotation, maisParent);
            }

            if(pickupStopwatch.ElapsedMilliseconds >= pickupDuration)
            {
                if (currentPickup == Pickups.Jetpack)
                {
                    Gravity *= 2;
                    pickupJetpackUI.SetActive(false);
                    jetpack.SetActive(false);
                }else if(currentPickup == Pickups.Gatling)
                {
                    pickupGatlingUI.SetActive(false);
                    gatlingGun.SetActive(false);
                }
                pickupSlider.value = 0;
                isPickupActive = false;
                currentPickup = Pickups.None;
                pickupStopwatch.Stop();
                pickupStopwatch.Reset();
            }
        }
        else
        {
            jetpack.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void lost()
    {
        Delivery_GameManager.currentState = Delivery_GameManager.GameState.lost;
    }

}
