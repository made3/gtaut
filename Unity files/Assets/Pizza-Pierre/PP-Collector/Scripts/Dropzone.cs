using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropzone : MonoBehaviour {

    private int pickupsInZone = 0;

    private GameObject[] pickups;
    public GameObject pickupParent;
    public Collector_GameManager gameManager;
    public Text score;
    private int pickupsTotalLength;

    // Use this for initialization
    void Start () {
        pickups = new GameObject[pickupParent.transform.childCount];
        for (int i = 0; i < pickupParent.transform.childCount; i++)
        {
            pickups[i] = pickupParent.transform.GetChild(i).gameObject;
        }
        pickupsTotalLength = pickups.Length;
        updateScore();
    }

    // Update is called once per frame
    void Update () {

        pickupsInZone = 0;
        foreach(Collider c in Physics.OverlapSphere(new Vector3(0, 0, 0), 10f))
        {
            if (c.gameObject.CompareTag("Pickup"))
            {
                pickupsInZone++;
            }
        }
        if (pickupsInZone == pickups.Length)
        {
            gameManager.currentState = Collector_GameManager.States.Won;
        }
        updateScore();
    }

    void updateScore()
    {
        score.text = pickupsInZone + "/" + pickupsTotalLength + " Food on Pizza";
    }

}
