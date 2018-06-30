using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour {

    public ParticleSystem tomatensplatter;
    public GameObject tomatenParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("isAttacking");
            Invoke("moveMe", 1);
        }
    }

    private void moveMe()
    {
        Instantiate(tomatensplatter, transform).transform.parent = tomatenParticles.transform;
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}
