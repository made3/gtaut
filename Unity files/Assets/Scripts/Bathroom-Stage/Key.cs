using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    [SerializeField]
    public OpenRotation doorOpenRotation;

    [SerializeField]
    private GameManager.GameStage nextStage;

    [SerializeField]
    private bool playAudioAttachedToDoor = true; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetInstanceID() == doorOpenRotation.gameObject.GetInstanceID())
        {
            if (playAudioAttachedToDoor)
            {
                doorOpenRotation.GetComponentInParent<AudioSource>().Play();
            }
            doorOpenRotation.isLocked = false;
            gameObject.SetActive(false);
            GameManager.currentStage = nextStage;
        }
    }
}
