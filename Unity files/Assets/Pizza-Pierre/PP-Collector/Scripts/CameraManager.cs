using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject mainCameraObject;
    public SkinnedMeshRenderer playerMeshRenderer;
    public GameObject playerArme;
    public PlayerAbilities playerAbilities;
    public GameObject cursorText;

    public bool isFPActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            isFPActive = !isFPActive;
            if (isFPActive)
            {
                playerAbilities.isFocusing = false;
                cursorText.SetActive(false);
            }
            playerArme.SetActive(!playerArme.activeSelf);
            if(playerMeshRenderer.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.On)
            {
                playerMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            else
            {
                playerMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            mainCameraObject.SetActive(!mainCameraObject.activeSelf);
        }
	}
}
