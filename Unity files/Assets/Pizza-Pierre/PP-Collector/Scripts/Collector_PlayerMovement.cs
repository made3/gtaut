using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
public class Collector_PlayerMovement : MonoBehaviour {

    public CameraManager cameraManager;
    public float speed;
    public GameObject firstPersonCamera;

    private Animator animatorMain;
    public Animator animatorArms;
    private NavMeshAgent navAgent;
    private Vector2 mouseLook;
    private Vector2 smoothV;
    public Collector_GameManager gameManager;

    private RaycastHit2D checkForUIRaycast;

    [Header("First Person")]

    public int maxHeightAngle;
    public int minHeightAngle;
    public float usedSensitivity;
    public float smoothing = 2.0f;

    [Header("Mouse UI")]

    public GameObject textPrefab;

    // Use this for initialization
    void Start () {
        animatorMain = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        if (gameManager.currentState == Collector_GameManager.States.Playing)
        {
            if (!cameraManager.isFPActive)
            {
                TPUpdate();
            }
            else
            {
                FPUpdate();
            }
        }
    }

    void TPUpdate()
    {
        animatorMain.SetFloat("velocity", navAgent.velocity.magnitude);

        checkMousePosition();
        
        if (navAgent.isStopped)
        {
            navAgent.SetDestination(transform.position);
            navAgent.isStopped = false;
        }
        Ray ray = cameraManager.mainCameraObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {

            checkForUIRaycast = Physics2D.Raycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y), Vector2.one);

            if(checkForUIRaycast.collider == null)
            {
                if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Ground")))
                {
                    //GetComponent<PlayerAbilities>().isFocusing = false;
                    navAgent.SetDestination(hit.point);
                }
            }
        }
    }

    void FPUpdate()
    {

        if (!navAgent.isStopped)
        {
            Vector3 axis;
            float angle;
            transform.rotation.ToAngleAxis(out angle, out axis);
            mouseLook = new Vector2(angle, 0);

            navAgent.isStopped = true;
        }

        float strafe = Input.GetAxis("Vertical");
        float translation = Input.GetAxis("Horizontal");
        if (strafe != 0 || translation != 0)
        {
            animatorMain.SetFloat("velocity", 1);
            animatorArms.SetBool("isWalking", true);
        }
        else
        {
            animatorMain.SetFloat("velocity", 0);
            animatorArms.SetBool("isWalking", false);
        }

        strafe *= speed;
        strafe *= Time.deltaTime;
        translation *= speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, strafe, Space.Self);

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md.x *= usedSensitivity;
        md.y *= usedSensitivity;
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        if(mouseLook.y > maxHeightAngle)
        {
            mouseLook.y = maxHeightAngle;
        }else if(mouseLook.y < minHeightAngle)
        {
            mouseLook.y = minHeightAngle;
        }

        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
        firstPersonCamera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
    }

    void checkMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Pickup"))
            {
                textPrefab.transform.position = Input.mousePosition + new Vector3(Screen.width/15, -Screen.height/15,0);
                textPrefab.GetComponent<Text>().text = "pick up";
                textPrefab.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    textPrefab.GetComponent<Animator>().SetTrigger("isPressed");
                }
            }else if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                if(GetComponent<PlayerAbilities>().canAttack)
                {
                    textPrefab.transform.position = Input.mousePosition + new Vector3(Screen.width / 15, -Screen.height / 15, 0);
                    textPrefab.GetComponent<Text>().text = "destroy";
                    textPrefab.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<PlayerAbilities>().isFocusing = true;
                        textPrefab.GetComponent<Animator>().SetTrigger("isPressed");
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<PlayerAbilities>().isFocusing = false;
                }
                if (textPrefab.activeSelf) textPrefab.SetActive(false);
            }

        }

    }
}

