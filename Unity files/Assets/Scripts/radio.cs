using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class radio : MonoBehaviour {

    public string myString;
    public Text myText1;
    public float fadeTime;
    public bool displayInfo;
    private AudioSource _audioSource;
    public bool isOn;


    // Use this for initialization
    void Start () {
        myText1.color = Color.clear;
        _audioSource = GetComponent<AudioSource>();
        isOn = false;
    }

    // Update is called once per frame
    void Update () {

        FadeText();

        if (displayInfo)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOn)
                {
                    _audioSource.Pause();
                    isOn = false;
                }
                else
                {
                    _audioSource.Play();
                    isOn = true;
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
            if (isOn)
            {
                myText1.text = "Press E to turn off";
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText1.text = myString;
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
            }
        }
        else
        {
            myText1.color = Color.Lerp(myText1.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}
