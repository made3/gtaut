using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schiss : MonoBehaviour {

    public float shotSpeed;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * Time.deltaTime * shotSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().getHit();
        }
        Destroy(this.gameObject);
    }

}
