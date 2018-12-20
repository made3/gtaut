using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private bool isHoldingObject = false;

    private SpringJoint currentHeldJoint;

    private GameObject currentHoldingPoint;
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;

        if (!isHoldingObject)
        {
            if (Physics.Raycast(ray, out hit, 3))
            {
                if (hit.collider.CompareTag("Pickupable") && Input.GetKeyDown(KeyCode.E))
                {
                    currentHoldingPoint = Instantiate(new GameObject(), hit.point, Quaternion.identity);
                    currentHoldingPoint.name = "CurrentHoldingPoint";
                    currentHoldingPoint.transform.parent = transform;
                    currentHoldingPoint.AddComponent<Rigidbody>().isKinematic = true;
                    currentHoldingPoint.GetComponent<Rigidbody>().useGravity = false;
                    currentHeldJoint = hit.collider.gameObject.AddComponent<SpringJoint>();
                    currentHeldJoint.connectedBody = currentHoldingPoint.GetComponent<Rigidbody>();
                    currentHeldJoint.autoConfigureConnectedAnchor = false;
                    currentHeldJoint.connectedAnchor = new Vector3(0, 0, 0);
                    currentHeldJoint.anchor = new Vector3(0, 0f, 0);
                    currentHeldJoint.spring = 100;
                    currentHeldJoint.damper = 0.05f;

                    //Use gravity when picked up for items like the calendar
                    hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    isHoldingObject = true;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(currentHoldingPoint);
                Destroy(currentHeldJoint);
                isHoldingObject = false;
            }
        }

	}
}
