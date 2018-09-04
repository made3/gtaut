using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startmenu : MonoBehaviour {

    private int currentPage;

    // 0 is Start, 1 is Controls, 2 is Credits
    private int currentSelection;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentPage = anim.GetInteger("currentPage");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Horizontal"))
        {
            print("BLIBLA");
        }

        if(currentPage == 1)
        {

        }
        if (Input.GetButtonDown("Horizontal"))
        {
            if (currentPage > 0) currentPage--;
            anim.SetInteger("currentPage", currentPage);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentPage < 3) currentPage++;
            anim.SetInteger("currentPage", currentPage);
        }
    }
}
