using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioHollow : MonoBehaviour {

    [SerializeField]
    private OpenRotation doorBad;

    [SerializeField]
    private OpenRotation doorSchlafzimmer;

    [SerializeField]
    private GameObject radio;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            switch (other.gameObject.name)
            {
                case "Bad":
                    if(doorBad.isClosed || doorSchlafzimmer.isClosed)
                    {
                        if (!radio.GetComponent<Radio>().isHollow)
                        {
                            print("A");
                            radio.GetComponent<Radio>().SwapSoundFiles();
                        }
                    }
                    else
                    {
                        if (radio.GetComponent<Radio>().isHollow)
                        {
                            print("B");
                            radio.GetComponent<Radio>().SwapSoundFiles();
                        }
                    }
                    break;
                case "Schlafzimmer":
                    if (doorSchlafzimmer.isClosed)
                    {
                        if (!radio.GetComponent<Radio>().isHollow)
                        {
                            print("C");
                            if (!radio.GetComponent<Radio>().isHollow) radio.GetComponent<Radio>().SwapSoundFiles();
                        }
                    }
                    else
                    {
                        if (radio.GetComponent<Radio>().isHollow)
                        {
                            print("D");
                            radio.GetComponent<Radio>().SwapSoundFiles();
                        }
                    }
                    break;
                case "Küche":

                    break;
            }
        }
    }
}
