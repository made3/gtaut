using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning_JetpackEyes : MonoBehaviour {

    public GameObject firstEye;
    private Vector3 firstEyeStartPosition;
    public GameObject secondEye;
    private Vector3 secondEyeStartPosition;
    public float movementSpeed;


    private float radius = 0.0562648f;

    private Vector3 firstPositionFirstEye;
    private Vector3 secondPositionFirstEye;
    private Vector3 firstPositionSecondEye;
    private Vector3 secondPositionSecondEye;


    private float x = 0;

    // Use this for initialization
    void Start () {
        firstEyeStartPosition = firstEye.transform.localPosition;
        secondEyeStartPosition = secondEye.transform.localPosition;

        firstPositionFirstEye = firstEyeStartPosition;
        firstPositionSecondEye = secondEyeStartPosition;

        secondPositionFirstEye = Random.insideUnitCircle * radius;
        secondPositionFirstEye += firstEyeStartPosition;

        secondPositionSecondEye = Random.insideUnitCircle * radius;
        secondPositionSecondEye += secondEyeStartPosition;
    }

    // Update is called once per frame
    void Update () {

        firstEye.transform.localPosition = Vector3.Lerp(firstPositionFirstEye, secondPositionFirstEye, x);
        secondEye.transform.localPosition = Vector3.Lerp(firstPositionSecondEye, secondPositionSecondEye, x);

        if (x >= 1)
        {
            x = 0;
            firstPositionFirstEye = secondPositionFirstEye;
            firstPositionSecondEye = secondPositionSecondEye;
            getNewRandoms();
        }
        x += Time.deltaTime * movementSpeed;
    }

    private void getNewRandoms()
    {
        secondPositionFirstEye = Random.insideUnitCircle * radius;
        secondPositionFirstEye += firstEyeStartPosition;

        secondPositionSecondEye = Random.insideUnitCircle * radius;
        secondPositionSecondEye += secondEyeStartPosition;
    }
}
