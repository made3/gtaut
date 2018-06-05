using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour, IInteractable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnInteractionPressed()
    {
        if (true) // If player has the key
        {
            gameObject.GetComponent<OpenRotation>().isLocked = false;
        }
    }
}
