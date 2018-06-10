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

    private Vector3 startPosition;

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
        currentSelection = Instantiate(selectionPrefab, transform.parent);
        startPosition = Input.mousePosition;
        currentSelection.transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentSelection.transform.localScale = (Input.mousePosition - startPosition) / transform.parent.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(currentSelection);
    }
}
