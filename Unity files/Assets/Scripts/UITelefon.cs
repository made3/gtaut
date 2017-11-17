﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITelefon : MonoBehaviour
{

    private Text myText1;
    private Text myText2;
    public string firstText = "Press E to use";
    public string secondText = "Use the Keyboard to dial numbers";
    public float fadeTime = 10;
    private bool displayInfo;
    private Animator _animator;
    private bool isOpen;
    private bool moreTexts;

    // Use this for initialization
    void Start()
    {
        myText1 = GetComponentsInChildren<Text>()[0];
        if (GetComponentsInChildren<Text>().Length > 1)
        {
            moreTexts = true;
            myText2 = GetComponentsInChildren<Text>()[1];
            myText2.color = Color.clear;
        }
        myText1.color = Color.clear;
        _animator = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterController.isInMenu)
        {
            FadeText();
        }
        if (displayInfo && !CharacterController.isInMenu)
        {

            if (Input.GetButtonDown("Cancel"))
            {
                if (isOpen)
                {
                    _animator.SetBool("open", false);
                    isOpen = false;
                }
            }else if (Input.GetKeyDown(KeyCode.E))
            {
                if(!isOpen)
                {
                    _animator.SetBool("open", true);
                    isOpen = true;
                }
            }
        }
        if (CharacterController.isInMenu)
        {
            displayInfo = false;
            FadeText();
        }
    }

    void OnMouseOver()
    {
        displayInfo = true;
    }

    void OnMouseExit()
    {
        displayInfo = false;
    }

    void FadeText()
    {
        if (displayInfo)
        {
            if (isOpen)
            {
                myText1.text = secondText;
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
                if (moreTexts)
                {
                    myText2.color = Color.Lerp(myText2.color, Color.white, fadeTime * Time.deltaTime);
                    myText2.text = secondText;
                }
            }
            else
            {
                myText1.text = firstText;
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
                if (moreTexts)
                {
                    myText2.text = firstText;
                    myText2.color = Color.Lerp(myText2.color, Color.white, fadeTime * Time.deltaTime);
                }
            }
        }
        else
        {
            myText1.color = Color.Lerp(myText1.color, Color.clear, fadeTime * Time.deltaTime);
            if (moreTexts)
            {
                myText2.color = Color.Lerp(myText2.color, Color.clear, fadeTime * Time.deltaTime);
            }
        }
    }
}