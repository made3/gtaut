using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startmenu : MonoBehaviour {

    private int currentPage;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentPage = anim.GetInteger("currentPage");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPage > 0) currentPage--;
            anim.SetInteger("currentPage", currentPage);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPage < 3) currentPage++;
            anim.SetInteger("currentPage", currentPage);
        }
    }
}
