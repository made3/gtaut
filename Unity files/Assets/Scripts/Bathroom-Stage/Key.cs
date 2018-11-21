using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable {

    [SerializeField]
    public OpenRotation bathroomDoorOpenRotation;

    [SerializeField]
    private GameManager.GameStage nextStage;

    [SerializeField]
    private bool playAudioAttachedToDoor = true; 

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    
    public void OnInteractionPressed()
    {
        if (playAudioAttachedToDoor)
        {
            bathroomDoorOpenRotation.GetComponentInParent<AudioSource>().Play();
        }
        bathroomDoorOpenRotation.isLocked = false;
        gameObject.SetActive(false);
        GameManager.currentStage = nextStage;
    }
}
