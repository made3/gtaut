using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelefonWählscheibe : MonoBehaviour {

    private Animator _animator;
    public GameObject _character;
    private Vector3 tmpLerpVector;
    public float smoothness;

    public static bool isCalling;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (_animator.GetBool("open"))
        {
            if (!isCalling)
            {
                isCalling = true;
            }
            tmpLerpVector = Vector3.Lerp(_character.transform.position, new Vector3(-1.2f, 2.6f, 0.8f), 1f / smoothness);
            _character.transform.position = tmpLerpVector;

            if (Input.GetButtonDown("Cancel"))
            {
                _animator.SetBool("open", false);
                isCalling = false;
            }
            // Tasten wählen
        }
	}
}
