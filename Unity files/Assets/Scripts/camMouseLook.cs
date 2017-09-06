using UnityEngine;
using System.Collections;

public class camMouseLook : MonoBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public float sensitivityMenu = 0.4f;

    private float usedSensitivity;

    GameObject character;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;
        usedSensitivity = sensitivity;

    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController.isInMenu)
        {
            usedSensitivity = sensitivityMenu;
        }
        else
        {
            usedSensitivity = sensitivity;
        }
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md.x *= usedSensitivity;
        md.y *= usedSensitivity;
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}
