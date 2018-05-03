using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRotation : MonoBehaviour {

    private enum PivotPosition { left, right, top, bottom }

    [SerializeField]
    private PivotPosition pivotPosition;

    private enum CurrentAction { open, close }

    private CurrentAction currentAction = CurrentAction.close;

    private bool isIdling = true;

    [Header("Speed")]

    [SerializeField]
    private float speed;

    private float currentLerpTime;

    [Header("Angle")]

    [SerializeField]
    private float maxAngle;

    [SerializeField]
    [Tooltip("When activated it uses the randomPlusMinus variable at every activation to have a different endpoint")]
    private bool randomAngleVariation;

    [SerializeField]
    [Tooltip("Adds or subtracts the random number from the maxAngle variable")]
    [Range(1,10)]
    private float randomPlusMinusAngle;

    private Quaternion maxRotation;

    private Quaternion startRotation;

    // Use this for initialization
    void Start () {
        startRotation = transform.rotation;

        switch (pivotPosition)
        {
            case PivotPosition.left:
                maxRotation = Quaternion.Euler(transform.eulerAngles.x, maxAngle, transform.eulerAngles.z);
                break;
            case PivotPosition.right:
                maxRotation = Quaternion.Euler(transform.eulerAngles.x, -maxAngle, transform.eulerAngles.z);
                break;
            case PivotPosition.top:
                maxRotation = Quaternion.Euler(maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                break;
            case PivotPosition.bottom:
                maxRotation = Quaternion.Euler(-maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                break;
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentAction == CurrentAction.open)
            {/*
                if (randomAngleVariation)
                {
                    if (Random.value > 0.5f)
                    {
                        maxRotation = startRotation;
                    }
                    else
                    {
                        maxRotation = startRotation;
                    }
                }*/
                currentAction = CurrentAction.close;
            }
            else if(currentAction == CurrentAction.close)
            {/*
                if (randomAngleVariation)
                {
                    if (Random.value > 0.5f)
                    {
                        maxRotation = Quaternion.Euler(transform.eulerAngles.x, maxAngle + randomPlusMinusSpeed, transform.eulerAngles.z);
                    }
                    else
                    {
                        maxRotation = Quaternion.Euler(transform.eulerAngles.x, maxAngle - randomPlusMinusSpeed, transform.eulerAngles.z);
                    }
                }*/
                currentAction = CurrentAction.open;
            }

            if (isIdling) isIdling = !isIdling;
        }
        if (!isIdling)
        {
            Debug.Log(currentLerpTime);
            if (currentAction == CurrentAction.open)
            {
                currentLerpTime += speed * Time.deltaTime;
            }
            else if(currentAction == CurrentAction.close)
            {
                currentLerpTime -= speed * Time.deltaTime;
            }
            transform.rotation = Quaternion.Lerp(startRotation, maxRotation, currentLerpTime);

            if (currentLerpTime > 1)
            {
                currentLerpTime = 1;
                isIdling = !isIdling;
            }
            else if(currentLerpTime < 0)
            {
                currentLerpTime = 0;
                isIdling = !isIdling;
            }
        }
    }
}
