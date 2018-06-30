using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector_Mais : MonoBehaviour {

    private Vector3 targetPosition;
    public float shotSpeed;

    // Use this for initialization
    void Start () {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.LookAt(targetPosition);
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * Time.deltaTime * shotSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
