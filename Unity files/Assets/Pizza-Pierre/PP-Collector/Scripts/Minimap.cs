using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector_Minimap : MonoBehaviour {

    private float startHeight;
    public GameObject player;

    // Use this for initialization
    void Start () {
        startHeight = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, startHeight, player.transform.position.z);
    }
}
