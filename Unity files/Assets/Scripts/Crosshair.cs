using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If in range of an interactable object the crosshair turns into an "e"
/// If "e" is pressed it will start a function on the focused object
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

                if (Input.GetButtonDown("Interact"))
                {
                    foreach(IInteractable script in hit.collider.gameObject.GetComponents<IInteractable>())
                    {
                        script.OnInteractionPressed();
                    }
                }

                if(hit.collider.gameObject.GetInstanceID() != lastFocusedObject.GetInstanceID())
                {
                    lastFocusedObject = hit.collider.gameObject;
                }
            }
            else
            {
                animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (animStateInfo.normalizedTime > 1)
                {
                    anim.Play("Crosshair", 0, 1);
                }
                anim.SetFloat("speed", -1);
            }
        }
        else
        {
            animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.normalizedTime > 1)
            {
                anim.Play("Crosshair", 0, 1);
            }
            anim.SetFloat("speed", -1);
        }
    }
}
