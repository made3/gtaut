using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DesktopIconsParent : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

    [SerializeField]
    private GameObject selectionPrefab;
    private GameObject currentSelection;
    [SerializeField]
    private StartMenuButton startMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        DeactivateIconMarker();
    }

    public void DeactivateIconMarker()
    {
        foreach (DesktopIcon icon in transform.parent.GetComponentsInChildren<DesktopIcon>())
        {
            if (icon.isSelected)
            {
                icon.ToggleIconMarked(false);
            }
        }
        if (startMenu.isSelected)
        {
            startMenu.ToggleIconMarked();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //currentSelection = Instantiate(selectionPrefab, transform.parent);
        //currentSelection.transform.localPosition = new Vector3(eventData.pressPosition.x * 1.9f - Screen.width, eventData.pressPosition.y * 1.9f - Screen.height, 0);
        //currentSelection.transform.localPosition = eventData.pressPosition * 2 - new Vector2(Screen.width, Screen.height);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //currentSelection.transform.localScale = (eventData.position - eventData.pressPosition) / transform.parent.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Destroy(currentSelection);
    }
}
