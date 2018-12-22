using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSound : MonoBehaviour {

    [SerializeField]
    private bool hasIntro = true;

    [SerializeField]
    private AudioClip intro;

    [SerializeField]
    private AudioClip loop;

    [SerializeField]
    private AudioClip loopFaster;

    [SerializeField, Tooltip("After seconds the faster loop track will be played. 0 for no faster loop")]
    private float getFasterAfterSeconds = 0;

    private AudioSource audioSrc;

    private bool isFaster = false;
    
    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();

        if (hasIntro)
        {
            audioSrc.clip = intro;
            audioSrc.loop = false;
            audioSrc.Play();
        }

        if(getFasterAfterSeconds != 0)
        {
            gameObject.AddComponent<AudioSource>().clip = loopFaster;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!audioSrc.isPlaying)
        {
            if(audioSrc.clip == intro)
            {
                DeactivateFaster();
            }
        }
    }

    public void StartTimer()
    {
        Invoke("ActivateFaster", getFasterAfterSeconds);
    }

    private void DeactivateFaster()
    {
        audioSrc.clip = loop;
        audioSrc.loop = true;
        audioSrc.Play();
        isFaster = false;
    }

    private void ActivateFaster()
    {
        audioSrc.clip = loopFaster;
        audioSrc.loop = true;
        audioSrc.Play();
        isFaster = true;
    }
}
