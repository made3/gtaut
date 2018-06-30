using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class EvilOven_CameraMovement : MonoBehaviour {

    public GameObject player;
    public float scrollSpeed;
    public float lerpBackSpeed;
    public float zoomSpeed;
    public Vector2 zoomOffsetRange;
    public long cameraResetTime;

    private bool isFreeScrolling;
    private Vector3 cameraOffset;

    private Vector3 zoomOffset;
    private bool isLerping;

    private float tmpLerp = 0;
    private collidedEdge tmpCollidedEdge;
    private Vector3 tmpPositionScrolling;
    private Stopwatch cameraSetbackTimer;
    private bool isManuallyGoingHome;

    private enum collidedEdge { top, topRight, right, bottomRight, bottom, bottomLeft, left, topLeft, none }

	// Use this for initialization
	void Start () {
        cameraOffset = transform.position - new Vector3( 0, 0.5f, 0);
        cameraSetbackTimer = new Stopwatch();
        cameraResetTime *= 1000;
    }
	
	// Update is called once per frame
	void Update () {

        #region Zooming

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoomOffset.y > zoomOffsetRange.x)
            {
                zoomOffset += new Vector3(0, -zoomSpeed, zoomSpeed);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoomOffset.y < zoomOffsetRange.y)
            {
                zoomOffset += new Vector3(0, zoomSpeed, -zoomSpeed);
            }
            if (isFreeScrolling)
            {
                if (!isLerping)
                {
                    touchedEdge();
                }
            }
        }
        #endregion

        tmpCollidedEdge = determineFreeScrolling();
        if (Input.GetKeyDown(KeyCode.F))
        {
            isManuallyGoingHome = true;
        }
        if (!isFreeScrolling)
        {
            transform.position = player.transform.position;
        }
        else
        {
            moveCameraAccordingToEdge();
            
            if (cameraSetbackTimer.ElapsedMilliseconds >= cameraResetTime || isManuallyGoingHome)
            {
                isLerping = true;
                transform.position = Vector3.Lerp(tmpPositionScrolling, player.transform.position - new Vector3(0, 0.5f, 0), tmpLerp);
                tmpLerp += Time.deltaTime * lerpBackSpeed;
                if(tmpLerp >= 1)
                {
                    tmpLerp = 0;
                    isFreeScrolling = false;
                    cameraSetbackTimer.Stop();
                    isManuallyGoingHome = false;
                }
            }
            transform.position += new Vector3(0, 0.5f, 0);
        }

        transform.position += cameraOffset + zoomOffset;
    }

    void moveCameraAccordingToEdge()
    {
        switch (tmpCollidedEdge)
        {
            case collidedEdge.top:
                tmpPositionScrolling += new Vector3(0, 0, scrollSpeed);
                break;
            case collidedEdge.topRight:
                tmpPositionScrolling += new Vector3(scrollSpeed / 2, 0, scrollSpeed / 2);
                break;
            case collidedEdge.right:
                tmpPositionScrolling += new Vector3(scrollSpeed, 0, 0);
                break;
            case collidedEdge.bottomRight:
                tmpPositionScrolling += new Vector3(scrollSpeed / 2, 0, -scrollSpeed / 2);
                break;
            case collidedEdge.bottom:
                tmpPositionScrolling += new Vector3(0, 0, -scrollSpeed);
                break;
            case collidedEdge.bottomLeft:
                tmpPositionScrolling += new Vector3(-scrollSpeed / 2, 0, -scrollSpeed / 2);
                break;
            case collidedEdge.left:
                tmpPositionScrolling += new Vector3(-scrollSpeed, 0, 0);
                break;
            case collidedEdge.topLeft:
                tmpPositionScrolling += new Vector3(-scrollSpeed / 2, 0, scrollSpeed / 2);
                break;
        }
        transform.position = tmpPositionScrolling;
    }

    private collidedEdge determineFreeScrolling()
    {
        if(Input.mousePosition.y == Screen.height-1)
        {
            touchedEdge();
            if (Input.mousePosition.x == 0)
            {
                return collidedEdge.topLeft;
            }else if(Input.mousePosition.x == Screen.width-1)
            {
                return collidedEdge.topRight;
            }
            else
            {
                return collidedEdge.top;
            }
        }else if(Input.mousePosition.x == 0)
        {
            touchedEdge();
            return collidedEdge.left;
        }else if(Input.mousePosition.x == Screen.width-1)
        {
            touchedEdge();
            return collidedEdge.right;
        }else if(Input.mousePosition.y == 0)
        {
            touchedEdge();
            if (Input.mousePosition.x == 0)
            {
                return collidedEdge.bottomLeft;
            }else if(Input.mousePosition.x == Screen.width-1)
            {
                return collidedEdge.bottomRight;
            }
            else
            {
                return collidedEdge.bottom;
            }
        }
        else
        {
            return collidedEdge.none;
        }
    }

    private void touchedEdge()
    {
        if (!isFreeScrolling)
        {
            tmpPositionScrolling = player.transform.position - new Vector3(0, 0.5f, 0);
        }
        if(tmpLerp > 0)
        {
            tmpPositionScrolling = transform.position - cameraOffset - zoomOffset;
            tmpLerp = 0;
        }
        cameraSetbackTimer.Reset();
        cameraSetbackTimer.Start();
        isLerping = false;
        isFreeScrolling = true;
        isManuallyGoingHome = false;
    }
}
