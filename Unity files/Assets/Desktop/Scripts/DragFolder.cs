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
        offset = Input.mousePosition - folderToDrag.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        folderToDrag.transform.position = Input.mousePosition - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector3.zero;
    }

}
