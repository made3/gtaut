using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Collector_Brokkoli : MonoBehaviour {

    private bool isInRange = false;
    public float rotationSpeed;
    private float rotationLerpTmp = 0;
    private float rotationTmp;
    private float currentRotation;
    private float tmpBla;

    public GameObject maisSpawnPoint;
    public GameObject maisPrefab;
    public float fireRate;
    public float detectionRange;
    public GameObject player;
    private Stopwatch stopwatch;
    private bool firstShot;
    public GameObject bullets;

    // Use this for initialization
    void Start () {
        rotationTmp = Random.Range(0, 360);
        stopwatch = new Stopwatch();
        stopwatch.Start();
        currentRotation = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update () {
        randomRotation();
        checkRangeForPlayer();
        if (isInRange)
        {
            transform.LookAt(player.transform);
            //transform.forward = Vector3.RotateTowards(transform.forward, waypoints[0].position - transform.position, trainSpeed * Time.deltaTime, 0.0f);

            if (!firstShot)
            {
                shoot();
                firstShot = true;
            }
        }
	}

    void randomRotation()
    {
        rotationLerpTmp += rotationSpeed * Time.deltaTime;
        tmpBla = Mathf.LerpAngle(currentRotation, rotationTmp, rotationLerpTmp);
        transform.rotation = Quaternion.Euler(0,tmpBla,0);
        if(rotationLerpTmp >= 1)
        {
            rotationLerpTmp = 0;
            rotationTmp = Random.Range(0, 360);
            currentRotation = transform.rotation.eulerAngles.y;
        }
    }

    public void shoot()
    {
        Instantiate(maisPrefab, maisSpawnPoint.transform.position, maisSpawnPoint.transform.rotation, bullets.transform);

        if (isInRange)
        {
            Invoke("shoot", fireRate);
        }
    }

    void checkRangeForPlayer()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
            firstShot = false;
        }
    }

}
