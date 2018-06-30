using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

    public bool isPickable;

	// Use this for initialization
	void Start () {
        Invoke("bePickable", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void bePickable()
    {
        isPickable = true;
    }
}
