using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour {

    public ParticleSystem knuspers;
    public float regenerationHP;

	// Use this for initialization
	void Start () {
        regenerationHP /= 100;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EvilOven_PlayerMovement>().currentHP += regenerationHP;
            Destroy(this.gameObject);
            knuspers.Play();
        }
    }
    
}
