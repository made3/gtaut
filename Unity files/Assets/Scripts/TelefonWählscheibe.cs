using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelefonWählscheibe : MonoBehaviour {

    private Animator _animator;
    public GameObject _character;
    private Vector3 tmpLerpVector;
    public float smoothness;
    public GameObject escToExit;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (_animator.GetBool("open"))
        {
            if (!CharacterController.isCalling)
            {
                CharacterController.isCalling = true;
            }
            if (!escToExit.activeSelf)
            {
                escToExit.SetActive(true);
            }

            tmpLerpVector = Vector3.Lerp(_character.transform.position, new Vector3(-1.2f, 2.6f, 0.8f), 1f / smoothness);
            _character.transform.position = tmpLerpVector;

            if (Input.GetButtonDown("Cancel"))
            {
                escToExit.SetActive(false);
                _animator.SetBool("open", false);
            }

            // Tasten wählen
        }
	}
}
