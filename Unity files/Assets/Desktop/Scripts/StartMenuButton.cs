using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuButton : MonoBehaviour, IPointerClickHandler {

    public bool isSelected;
    [SerializeField]
    private Sprite iconNormal;
    [SerializeField]
    private Sprite iconSelected;

    private Image imageComponent;

    [SerializeField]
    private GameObject startMenu;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = iconNormal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleIconMarked();
    }

    public void ToggleIconMarked()
    {
        startMenu.SetActive(!startMenu.activeSelf);

        if (!isSelected)
        {
            imageComponent.sprite = iconSelected;
        }
        else
        {
            imageComponent.sprite = iconNormal;
        }
        isSelected = !isSelected;
    }
}
