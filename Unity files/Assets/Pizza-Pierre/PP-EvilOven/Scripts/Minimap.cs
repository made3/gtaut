using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject player;
    private float startHeight;

	// Use this for initialization
	void Start () {
        startHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, startHeight, player.transform.position.z);
    }
}
