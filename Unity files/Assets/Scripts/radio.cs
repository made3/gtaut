using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class radio : MonoBehaviour {

    public string myString;
    public Text myText1;
    public float textFadingSpeed;
    public bool displayInfo;
    public float songFadingSpeed;
    public float rauschenFadingSpeed;
    private AudioSource _rauschen;
    private float tmpRauschTime;
    private AudioSource _song1;
    private bool isOn;
    private bool isFading;
    private bool startSong;


    // Use this for initialization
    void Start () {
        myText1.color = Color.clear;

        var audioSources = GetComponents<AudioSource>();
        _rauschen = audioSources[1];
        _rauschen.volume = 0;
        _song1 = audioSources[0];
        //_song1.volume = 0;
        tmpRauschTime = 0;

        isOn = false;
        isFading = false;
        startSong = false;
    }

    // Update is called once per frame
    void Update () {

        FadeText();
        activateRadio();

    }

    void activateRadio()
    {
        if (displayInfo && !isFading)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOn)
                {
                    isOn = false;
                    isFading = true;
                }
                else
                {
                    isOn = true;
                    isFading = true;
                }

            }
        }

        if (isOn && isFading)
        {
            playNoise();

            if (startSong)
            {
                if (_song1.volume < 1)
                {
                    _song1.volume = _song1.volume + Time.deltaTime * songFadingSpeed;
                }
                else
                {
                    tmpRauschTime = 0;
                    startSong = false;
                    isFading = false;
                }
            }
        }

        if (!isOn && isFading)
        {
            
            playNoise();
            
            if(startSong)
            {
                if (_song1.volume > 0)
                {
                    _song1.volume = _song1.volume - Time.deltaTime * songFadingSpeed;
                }
                else
                {
                    tmpRauschTime = 0;
                    startSong = false;
                    isFading = false;
                }
            }
        }
    }

    void playNoise()
    {
        if (tmpRauschTime < 1)
        {
            tmpRauschTime += Time.deltaTime;
            _rauschen.volume = _rauschen.volume + Time.deltaTime * rauschenFadingSpeed;
            if (_rauschen.volume >= 0.2 && !isOn)
            {
                startSong = true;
            }
        }
        else if(tmpRauschTime >= 1)
        {
            _rauschen.volume = _rauschen.volume - Time.deltaTime * rauschenFadingSpeed;
            if(_rauschen.volume <= 0.5 && isOn)
            {
                startSong = true;
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
        if (displayInfo && !isFading)
        {
            if (isOn)
            {
                myText1.text = "Press E to turn off";
                myText1.color = Color.Lerp(myText1.color, Color.white, textFadingSpeed * Time.deltaTime);
            }
            else
            {
                myText1.text = myString;
                myText1.color = Color.Lerp(myText1.color, Color.white, textFadingSpeed * Time.deltaTime);
            }
        }
        else
        {
            myText1.color = Color.Lerp(myText1.color, Color.clear, textFadingSpeed * Time.deltaTime);
        }
    }
}
