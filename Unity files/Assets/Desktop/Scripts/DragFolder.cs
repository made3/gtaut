using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragFolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField]
    private GameObject folderToDrag;

    private Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var screenPoint = (Input.mousePosition);
        screenPoint.z = 1; //distance of the plane from the camera
        offset = Camera.main.ScreenToWorldPoint(screenPoint) - folderToDrag.transform.position;
        offset = new Vector3(offset.x, offset.y, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {

        var screenPoint = (Input.mousePosition);
        screenPoint.z = 1; //distance of the plane from the camera
        folderToDrag.transform.position = Camera.main.ScreenToWorldPoint(screenPoint) - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector3.zero;
    }

}
