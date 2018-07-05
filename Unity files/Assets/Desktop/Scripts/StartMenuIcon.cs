using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isOpen = false;
    [HideInInspector]
    public bool isSelected = false;
    private bool isScene = false;

    [SerializeField] Sprite iconNormal;
    [SerializeField] Sprite iconSelected;

    [SerializeField]
    private GameObject appToOpen;
    [SerializeField]
    private string sceneToOpen;
    [SerializeField]
    private Transform canvas;

    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        if (appToOpen == null)
        {
            isScene = true;
        }
        else
        {
            appToOpen.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOpen)
        {
            OpenApp();
        }
    }

    public void ToggleIconMarked(bool resetOtherIcons)
    {
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
            // activate item on bottom bar
            isOpen = true;
        }
        else
        {
            SceneManager.LoadScene(sceneToOpen);
        }
    }

    public void CloseApp()
    {
        appToOpen.SetActive(false);
        // delete item on bottom bar
        isOpen = false;
    }

    public void MinimizeApp()
    {
        appToOpen.SetActive(false);
        // Change icon on bottom bar
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleIconMarked(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleIconMarked(false);
    }
}
