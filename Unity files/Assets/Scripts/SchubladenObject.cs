using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchubladenObject : MonoBehaviour {

    private Transform parent;

    private void Awake()
    {
        parent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Schublade"))
        {
            print("Exited");
            transform.SetParent(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Schublade"))
        {
            print("Exited");
            transform.SetParent(parent);
        }
    }
}
