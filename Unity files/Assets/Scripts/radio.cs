using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable {

    public float songFadingSpeed;
    public float rauschenFadingSpeed;
    private AudioSource _rauschen;
    private float tmpRauschTime;
    private AudioSource _song1;
    [SerializeField]
    private bool isOn;
    private bool isFading;
    private bool startSong;

    private bool justStarted;

    private bool rauschGoUp;

    private IEnumerator rauschCoroutine;
    private IEnumerator songCoroutine;

    // Use this for initialization
    void Start () {
        rauschCoroutine = RauschCoroutine();
        songCoroutine = SongCoroutine();
        var audioSources = GetComponents<AudioSource>();
        _rauschen = audioSources[1];
        _rauschen.volume = 0;
        _song1 = audioSources[0];
        //_song1.volume = 0;
        tmpRauschTime = 0;    }

    IEnumerator SongCoroutine()
    {
        while (true)
        {
            if (isOn)
            {
                if (_song1.volume <= 1)
                {
                    _song1.volume = _song1.volume + Time.deltaTime * songFadingSpeed;
                }
                else
                {
                    isOn = false;
                    StopCoroutine(songCoroutine);
                }
            }
            else
            {
                if (_song1.volume >= 0)
                {
                    _song1.volume = _song1.volume - Time.deltaTime * songFadingSpeed;
                }
                else
                {
                    isOn = false;
                    StopCoroutine(songCoroutine);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RauschCoroutine()
    {
        while (isFading)
        {
            if (rauschGoUp)
            {
                _rauschen.volume += Time.deltaTime * rauschenFadingSpeed;
                if (_rauschen.volume >= 1)
                {
                    rauschGoUp = false;
                    justStarted = false;
                    isOn = !isOn;
                    if (!justStarted)
                    {
                        justStarted = true;
                        StartCoroutine(songCoroutine);
                    }
                }
            }
            else
            {
                _rauschen.volume -= Time.deltaTime * rauschenFadingSpeed;
                if(_rauschen.volume <= 0)
                {
                    isFading = false;
                    StopCoroutine(rauschCoroutine);                    
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnInteractionPressed()
    {
        if (!isFading)
        {
            rauschGoUp = true;
            isFading = true;
            StartCoroutine(rauschCoroutine);
        }
    }
}
