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

    public void OnEnter()
    {
        _telefonWählscheibe.currentHoverNumber = Int16.Parse(gameObject.name);
        _animator.SetInteger("whichKringel", UnityEngine.Random.Range(1, 4));
    }

    public void OnExit()
    {
        _telefonWählscheibe.currentHoverNumber = -1;
        _animator.SetInteger("whichKringel", 0);
    }
}
