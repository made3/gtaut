using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    private Transform[] waypoints;
    public float trainSpeed;
    public GameObject waypointsParent;
    private int currentWaypoint;

	// Use this for initialization
	void Start () {
        currentWaypoint = 0;

        waypoints = new Transform[waypointsParent.transform.childCount];
        for (int i = 0; i < waypointsParent.transform.childCount; i++)
        {
            waypoints[i] = waypointsParent.transform.GetChild(i).gameObject.transform;
        }
        waypointsParent.transform.position = new Vector3(waypointsParent.transform.position.x, transform.position.y, waypointsParent.transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {
        if (currentWaypoint < waypoints.Length)
        {
            if(currentWaypoint != waypoints.Length - 1)
            {
                transform.forward = Vector3.RotateTowards(transform.forward, waypoints[currentWaypoint + 1].position - transform.position, trainSpeed * Time.deltaTime, 0.0f);

                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint + 1].position, trainSpeed * Time.deltaTime);

                if (transform.position == waypoints[currentWaypoint + 1].position)
                {
                    currentWaypoint++;
                }

            }
            else
            {
                transform.forward = Vector3.RotateTowards(transform.forward, waypoints[0].position - transform.position, trainSpeed * Time.deltaTime, 0.0f);

                transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, trainSpeed * Time.deltaTime);

                if (transform.position == waypoints[0].position)
                {
                    currentWaypoint++;
                }

            }


        }
        else
        {
            currentWaypoint = 0;
        }
	}
}
