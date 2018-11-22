using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public class Startmenu_Camera : MonoBehaviour
{

    Vector2 mouseLook;
    Vector2 mouseLookRaw;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 5.0f;

    private Vector3 startPosition;

    [SerializeField]
    private PostProcessingProfile zoomProfile;

    private PostProcessingProfile startProfile;

    [SerializeField]
    private PostProcessingBehaviour postPro;

    private void Start()
    {
        startPosition = Camera.main.gameObject.transform.position;
        startProfile = postPro.profile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Camera.main.gameObject.transform.position += Camera.main.gameObject.transform.forward/6;
            postPro.profile = zoomProfile;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Camera.main.gameObject.transform.position = startPosition;
            postPro.profile = startProfile;
        }

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseLookRaw += md;
        md.x *= sensitivity;
        md.y *= sensitivity;
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        if(mouseLook.x > 40) mouseLook.x = 40;
        else if(mouseLook.x < -40) mouseLook.x = -40;

        if (mouseLook.y > 6) mouseLook.y = 6;
        else if (mouseLook.y < -55) mouseLook.y = -55;

        transform.localRotation = Quaternion.Euler(-mouseLook.y, mouseLook.x, transform.localRotation.eulerAngles.z);
    }
}
