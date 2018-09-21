using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kugelstoßpendel : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSrc;
    
    public void PlaySound()
    {
        audioSrc.Play(0);
    }
}
