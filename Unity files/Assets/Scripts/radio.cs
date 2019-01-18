using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable {

    public float songFadingSpeed;
    public float rauschenFadingSpeed;

    private AudioSource _rauschen;
    private AudioSource _song1;
    private AudioSource _song2;

    [SerializeField]
    private int channel;
    private bool isFading;

    private bool justStarted;
    private bool rauschGoUp;

    private IEnumerator rauschCoroutine;
    private IEnumerator songCoroutine;

    private AudioSource[] audioSources;

    [SerializeField]
    private AudioClip _song1Clear;
    private AudioClip _song1Hollow;

    [SerializeField]
    private AudioClip _song2Clear;
    private AudioClip _song2Hollow;

    [HideInInspector]
    public bool isHollow = true;

    [SerializeField]
    private bool playOnAwake;

    // Use this for initialization
    void Start () {
        rauschCoroutine = RauschCoroutine();
        songCoroutine = SongCoroutine();
        audioSources = GetComponents<AudioSource>();
        _rauschen = audioSources[0];
        _rauschen.volume = 0;
        _song1 = audioSources[1];
        _song2 = audioSources[2];

        _song1Hollow = _song1.clip;
        _song2Hollow = _song2.clip;

        if (playOnAwake)
        {
            channel++;
            StartCoroutine(songCoroutine);
            //OnInteractionPressed();
        }
        //DontDestroyOnLoad(this.gameObject);
    }
    
    public void SwapSoundFiles()
    {
        float tmpTime = _song1.time;

        if (isHollow) _song1.clip = _song1Clear;
        else _song1.clip = _song1Hollow;
         _song1.time = tmpTime;

        if (isHollow) _song2.clip = _song2Clear;
        else _song2.clip = _song2Hollow;
        _song2.time = tmpTime;
        
        isHollow = !isHollow;
    }

    IEnumerator SongCoroutine()
    {
        while (true)
        {
            switch (channel)
            {
                case 0:
                    if (_song1.volume > 0)
                    {
                        _song1.volume -= Time.deltaTime * songFadingSpeed;
                    }
                    else if(_song2.volume > 0)
                    {
                        _song2.volume -= Time.deltaTime * songFadingSpeed;
                    }
                    else
                    {
                        StopCoroutine(songCoroutine);
                    }
                    break;
                case 1:
                    if(_song1.volume < 1)
                    {
                        _song1.volume += Time.deltaTime * songFadingSpeed;
                        _song2.volume -= Time.deltaTime * songFadingSpeed;
                    }
                    else
                    {
                        StopCoroutine(songCoroutine);
                    }
                    break;
                case 2:
                    if (_song2.volume < 1)
                    {
                        _song2.volume += Time.deltaTime * songFadingSpeed;
                        _song1.volume -= Time.deltaTime * songFadingSpeed;
                    }
                    else
                    {
                        StopCoroutine(songCoroutine);
                    }
                    break;
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
                    channel++;
                    if(channel >= audioSources.Length)
                    {
                        channel = 0;
                    }
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
