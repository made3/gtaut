using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomKey : MonoBehaviour, IInteractable {

    [SerializeField]
    private OpenRotation bathroomDoorOpenRotation;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    


    public void OnInteractionPressed()
    {
        bathroomDoorOpenRotation.isLocked = false;
        gameObject.SetActive(false);
    }
}
