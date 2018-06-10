using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler {

    private bool isOpen = false;
    [HideInInspector]
    public bool isSelected = false;
    private bool isScene = false;

    [SerializeField] Sprite iconNormal;
    [SerializeField] Sprite iconSelected;

    [SerializeField]
    private Transform canvas;

    [Header("App")]
    [SerializeField]
    private GameObject appToOpen;
    [SerializeField]
    private Transform startBarParent;
    [SerializeField]
    private Sprite startBarIcon;
    [SerializeField]
    private Sprite startBarIconSelected;
    [SerializeField]
    private GameObject startBarIconPrefab;
    private GameObject startBarIconObject;
    [SerializeField]
    private bool isMinimizable;

    private bool isMinimized = false;

    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = iconNormal;
        if (appToOpen == null)
        {
            isScene = true;
        }
        else
        {
            appToOpen.SetActive(false);
        }

        if (name.Contains(".jpg"))
        {
            appToOpen.GetComponentInChildren<Text>().text = name;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            if (!isOpen)
            {
                OpenApp();
                ToggleIconMarked(false);
            }
            else if (isMinimized)
            {
                MinimizeAppToggle();
            }
        }
        else
        {
            ToggleIconMarked(true);
        }
    }
    
    public void ToggleIconMarked(bool resetOtherIcons)
    {
        if (resetOtherIcons)
        {
            canvas.GetComponentInChildren<DesktopIconsParent>().DeactivateIconMarker();
        }

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

    public void OpenApp()
    {

        if (!isScene)
        {
            appToOpen.SetActive(true);
            if (isMinimized)
            {
                startBarIconObject = Instantiate(startBarIconPrefab, startBarParent);
                startBarIconObject.GetComponent<Image>().sprite = startBarIcon;
                RectTransform rt = startBarIconObject.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(startBarIcon.rect.width, rt.rect.height);
                startBarIconObject.GetComponent<Button>().onChangeEvent.AddListener(MinimizeAppToggle);
            }
            isOpen = true;
        }
        else
        {
            //StartScene
        }
    }

    public void CloseApp()
    {
        appToOpen.SetActive(false);
        if (isMinimizable)
        {
            Destroy(startBarIconObject);
        }
        isOpen = false;
    }

    public void MinimizeAppToggle()
    {
        if (isMinimized)
        {
            appToOpen.SetActive(true);
            startBarIconObject.GetComponent<Image>().sprite = startBarIcon;
        }
        else
        {
            appToOpen.SetActive(false);
            startBarIconObject.GetComponent<Image>().sprite = startBarIconSelected;
        }

        isMinimized = !isMinimized;
    }
}
