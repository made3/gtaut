using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goggle : MonoBehaviour {

    public float goggleSpeed;
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private float lerpTmp;

	// Use this for initialization
	void Start () {
        nextPosition = Random.insideUnitCircle * 0.18f;

    }
	
	// Update is called once per frame
	void Update () {
        lerpTmp += goggleSpeed * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(currentPosition, nextPosition,lerpTmp);
        if(lerpTmp >= 1)
        {
            lerpTmp = 0;
            currentPosition = transform.localPosition;
            nextPosition = Random.insideUnitCircle * 0.18f;
        }
	}
}
