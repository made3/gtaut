using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public static GameObject selectedGameObject;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GetComponent<DesktopIcon>().isSelected)
        {
            GetComponent<DesktopIcon>().ToggleIconMarked(true);
        }
        selectedGameObject = gameObject;
        startPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectedGameObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == null)
        {
            transform.position = startPosition;
        }
    }
}
