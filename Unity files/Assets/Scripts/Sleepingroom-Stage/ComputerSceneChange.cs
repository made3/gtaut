using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Moves player in front of PC as soon as he interacts with it and switches the scene to the Desktop scene.
/// </summary>

public class ComputerSceneChange : MonoBehaviour, IInteractable {

    private bool hasInteracted = false;

    private bool hasReachedPosition = false;

    private bool hasStartedScene = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject chair;

    [SerializeField]
    private Transform chairPosition;

    [SerializeField]
    private float movementSpeedChair;

    [SerializeField]
    private Transform pointInFrontOfPC;

    [SerializeField]
    private float movementSpeedTowardsPC;

    [SerializeField]
    private float endingCameraFOV;

    [SerializeField]
    private float movementSpeedFov;

    [SerializeField]
    private GameObject bathroomKey;

    // Use this for initialization
    void Start () {
        
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    // Update is called once per frame
    void Update () {
        if (hasInteracted)
        {
            float chairSpeed = movementSpeedChair * Time.deltaTime;
            chair.transform.position = Vector3.MoveTowards(chair.transform.position, new Vector3(chairPosition.position.x, chair.transform.position.y, chairPosition.position.z), chairSpeed);
            float speed = movementSpeedTowardsPC * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(pointInFrontOfPC.position.x, player.transform.position.y, pointInFrontOfPC.position.z), speed);
            //player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, pointInFrontOfPC.rotation, speed);


            if(Vector3.Distance(player.transform.position, pointInFrontOfPC.position) <= 0.2f)
            {
                hasReachedPosition = true;
                hasInteracted = false;
            }
        }
        if (hasStartedScene)
        {
            float fovSpeed = movementSpeedFov * Time.deltaTime;
            Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, endingCameraFOV, fovSpeed);
            if(Camera.main.fieldOfView - endingCameraFOV <= 0.2f)
            {
                Cursor.visible = true;
                SceneManager.LoadScene("Desktop");
            }
        }
    }

    public void OnInteractionPressed()
    {
        // Move player in front of pc, zoom into the pc display
        // After that switch scene to Desktop scene
        if (!hasReachedPosition)
        {
            hasInteracted = true;
            GameManager.instance.setCurrentGameState(GameManager.GameState.OnPC);
        }
        else
        {
            hasStartedScene = true;
        }
    }

    public void UpdateSavedValues()
    {
        bathroomKey.GetComponent<Key>().bathroomDoorOpenRotation.isLocked = false;
        bathroomKey.SetActive(false);
        //chair.transform.SetPositionAndRotation(chairPosition.position, chairPosition.rotation);
        chair.transform.position = new Vector3(chairPosition.position.x, chair.transform.position.y, chairPosition.position.z);
        player.transform.position = pointInFrontOfPC.position;
    }
}
