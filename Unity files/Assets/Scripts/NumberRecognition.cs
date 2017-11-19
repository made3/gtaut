using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NumberRecognition : MonoBehaviour {

    private Animator _animator;
    public TelefonWählscheibe _telefonWählscheibe;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
    }

    void OnMouseEnter()
    {
        _telefonWählscheibe.currentHoverNumber = Int16.Parse(gameObject.name);
        _animator.SetInteger("whichKringel", UnityEngine.Random.Range(1, 4));
    }

    private void OnMouseExit()
    {
        _telefonWählscheibe.currentHoverNumber = -1;
        _animator.SetInteger("whichKringel", 0);
    }

    // Update is called once per frame
    void Update () {
	}
}
