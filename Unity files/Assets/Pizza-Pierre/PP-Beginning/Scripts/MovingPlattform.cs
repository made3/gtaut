using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlattform : MonoBehaviour {

    public Vector3 startPosition;
    public float movementSpeed;
    public Vector3 targetPosition;

    private bool switcher;
    private float x = 0;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        targetPosition += startPosition;
    }
	
	// Update is called once per frame
	void Update () {
        move(targetPosition);
    }

    private void move(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(startPosition, targetPos, x);

        if (x <= 0)
        {
            switcher = true;
        }
        else if(x >= 1)
        {
            switcher = false;
        }
        if (switcher)
        {
            x += Time.deltaTime * movementSpeed;
        }
        else
        {
            x -= Time.deltaTime * movementSpeed;
        }

    }
}
