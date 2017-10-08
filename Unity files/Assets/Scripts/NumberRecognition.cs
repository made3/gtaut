using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NumberRecognition : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
    }

    void OnMouseOver()
    {

        _animator.SetInteger("whichKringel", Random.Range(1, 4));
    }

    private void OnMouseExit()
    {
        
    }

    // Update is called once per frame
    void Update () {
	}
}
