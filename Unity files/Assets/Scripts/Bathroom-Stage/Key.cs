﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable {

    [SerializeField]
    public OpenRotation bathroomDoorOpenRotation;

    [SerializeField]
    private GameManager.GameStage nextStage;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    
    public void OnInteractionPressed()
    {
        bathroomDoorOpenRotation.isLocked = false;
        gameObject.SetActive(false);
        GameManager.currentStage = nextStage;
    }
}