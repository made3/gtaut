using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOpenDoor : MonoBehaviour {

    public string myString;
    public Text myText1;
    public Text myText2;
    public float fadeTime;
    public bool displayInfo;
    private Animator _animator;
    public bool isOpen;

    // Use this for initialization
    void Start()
    {
        myText1.color = Color.clear;
        myText2.color = Color.clear;
        _animator = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        FadeText();

        if (displayInfo)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen)
                {
                    _animator.SetBool("open", false);
                    isOpen = false;
                }
                else
                {
                    _animator.SetBool("open", true);
                    isOpen = true;
                }

            }
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
                myText1.text = "Press E to close";
                myText2.text = "Press E to close";
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
                myText2.color = Color.Lerp(myText2.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText1.text = myString;
                myText2.text = myString;
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
                myText2.color = Color.Lerp(myText2.color, Color.white, fadeTime * Time.deltaTime);
            }
        }
        else
        {
            myText1.color = Color.Lerp(myText1.color, Color.clear, fadeTime * Time.deltaTime);
            myText2.color = Color.Lerp(myText2.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }


}
