using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Vector3 cameraOffset;
    private Vector3 zoom = Vector3.zero;
    private Player player;

    private void Start()
    {
        player = target.gameObject.GetComponent<Player>();
        cameraOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 destination = target.position + cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            checkForZoom();

            transform.position = Vector3.SmoothDamp(transform.position, transform.position + zoom, ref velocity, 0.05f);

        }

    }

    private void checkForZoom()
    {
        if (!Player.isSwimming)
        {
            if (player.isFalling)
            {
                zoom = Vector3.zero;
            }
            else
            {
                if (player.isJumping)
                {
                    zoom = new Vector3(0, 0, -0.5f);
                }
            }
        }
    }
}
