using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mais : MonoBehaviour {

    public float shotSpeed;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * shotSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GetHit"))
        {
            if(other.gameObject.name == "brokkoli" || other.gameObject.name == "ananas")
            {
                other.gameObject.GetComponent<Brokkoli>().die();
            }
            else
            {
                other.gameObject.GetComponent<Tomate>().die();
            }
        }else if(!other.CompareTag("Pickup") && other.gameObject.layer != LayerMask.GetMask("Schiss"))
        {
            Destroy(this.gameObject);
        }
    }

}
