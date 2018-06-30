using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzonesLeftRight : MonoBehaviour {

    public GameObject worldwidth;

	// Use this for initialization
	void Start () {
        transform.position = worldwidth.transform.position;
        GetComponents<BoxCollider>()[0].center = new Vector3(-worldwidth.GetComponent<BoxCollider>().size.x - 200,0,0);
        GetComponents<BoxCollider>()[1].center = new Vector3(worldwidth.GetComponent<BoxCollider>().size.x + 200, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
