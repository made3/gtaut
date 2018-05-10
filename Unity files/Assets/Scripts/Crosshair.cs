using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If in range of an interactable object the crosshair turns into an "e"
/// </summary>

public class Crosshair : MonoBehaviour {

    [SerializeField]
    private float maxRange;

    private GameObject lastFocusedObject;

    private Animator anim;
    private AnimatorStateInfo animStateInfo;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        lastFocusedObject = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxRange))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if(animStateInfo.normalizedTime < 0)
                {
                    anim.Play("Crosshair", 0, 0);
                }
                anim.SetFloat("speed", 1);
                

                if(hit.collider.gameObject.GetComponent<OpenPosition>() != null)
                {
                    hit.collider.gameObject.GetComponent<OpenPosition>().isInFocus = true;
                }
                else if(hit.collider.gameObject.GetComponent<OpenRotation>() != null)
                {
                    hit.collider.gameObject.GetComponent<OpenRotation>().isInFocus = true;
                }

                if(hit.collider.gameObject.GetInstanceID() != lastFocusedObject.GetInstanceID())
                {
                    lastFocusedObject = hit.collider.gameObject;
                }
            }
        }
        else
        {
            if (lastFocusedObject.GetComponent<OpenPosition>() != null)
            {
                if (lastFocusedObject.GetComponent<OpenPosition>().isInFocus)
                {
                    print("Changed for: " + lastFocusedObject.name);
                    lastFocusedObject.GetComponent<OpenPosition>().isInFocus = false;
                }
            }
            else if (lastFocusedObject.GetComponent<OpenRotation>() != null)
            {
                if (lastFocusedObject.GetComponent<OpenRotation>().isInFocus)
                {
                    print("Changed for: " + lastFocusedObject.name);
                    lastFocusedObject.GetComponent<OpenRotation>().isInFocus = false;
                }
            }

            animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.normalizedTime > 1)
            {
                anim.Play("Crosshair", 0, 1);
            }
            anim.SetFloat("speed", -1);
        }
    }
}
