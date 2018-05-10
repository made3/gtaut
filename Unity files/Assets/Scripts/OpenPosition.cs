using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPosition : MonoBehaviour
{

    private enum Direction { horizontalX, horizontalZ, vertical }

    [SerializeField]
    private Direction direction;

    [SerializeField]
    private bool invertDirection;

    private enum CurrentAction { open, close }

    private CurrentAction currentAction = CurrentAction.close;

    private bool isIdling = true;

    [Header("Speed")]

    [SerializeField]
    private float speed;

    private float currentLerpTime;

    [Header("Length")]

    [SerializeField]
    private float maxLength;

    [SerializeField]
    private bool isRandomRangeActive;

    private Vector3 maxPosition;
    
    private Vector3 startPosition;

    [SerializeField]
    [Range(0,20)]
    [Tooltip("Higher value means less difference")]
    private float randomRangeFactor;

    private float randomVariation;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        DetermineMaxPosition();

        // Layer is not used in code anyway, but might be used somewhen later
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    void DetermineMaxPosition()
    {
        if (!isRandomRangeActive)
        {
            randomVariation = 0;
        }

        switch (direction)
        {
            case Direction.horizontalX:
                if (!invertDirection)
                {
                    maxPosition = new Vector3(startPosition.x + maxLength + randomVariation, startPosition.y, startPosition.z);
                }
                else
                {
                    maxPosition = new Vector3(startPosition.x - maxLength + randomVariation, startPosition.y, startPosition.z);
                }
                break;
            case Direction.horizontalZ:
                if (!invertDirection)
                {
                    maxPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + maxLength + randomVariation);
                }
                else
                {
                    maxPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - maxLength + randomVariation);
                }
                break;
            case Direction.vertical:
                if (!invertDirection)
                {
                    maxPosition = new Vector3(startPosition.x, startPosition.y + maxLength + randomVariation, startPosition.z);
                }
                else
                {
                    maxPosition = new Vector3(startPosition.x, startPosition.y - maxLength + randomVariation, startPosition.z);
                }
                break;
        }
    }

    public void OnInteractionPressed()
    {
        if (currentAction == CurrentAction.open)
        {
            currentAction = CurrentAction.close;
        }
        else if (currentAction == CurrentAction.close)
        {

            // Set random value, which gets subtracted or added everytime the object opens

            if (isRandomRangeActive)
            {
                randomVariation = Random.Range(0, maxLength / randomRangeFactor);

                if (Random.value > 0.5f)
                {
                    randomVariation = -randomVariation;
                }

                DetermineMaxPosition();
            }

            currentAction = CurrentAction.open;
        }

        if (isIdling) isIdling = !isIdling;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isIdling)
        {
            if (currentAction == CurrentAction.open)
            {
                currentLerpTime += speed * Time.deltaTime;
            }
            else if (currentAction == CurrentAction.close)
            {
                currentLerpTime -= speed * Time.deltaTime;
            }

            transform.position = Vector3.Lerp(startPosition, maxPosition, currentLerpTime);

            if (currentLerpTime > 1)
            {
                currentLerpTime = 1;
                isIdling = !isIdling;
            }
            else if (currentLerpTime < 0)
            {
                currentLerpTime = 0;
                isIdling = !isIdling;
            }
        }
    }
}
