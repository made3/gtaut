using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour {

    private int cheeseAmount;
    private int sausageAmount;
    private int meatAmount;
    private int sauceAmount;

    public UnityEngine.UI.Button inventoryButton;
    public UnityEngine.UI.Button[] itemButtons;
    public GameObject inventoryUI;
    public Text cheeseText;
    public Text sausageText;
    public Text meatText;
    public Text sauceText;

    [Header("Prefabs")]

    public GameObject pickupParent;
    public GameObject cheesePrefab;
    public GameObject sausagePrefab;
    public GameObject meatPrefab;
    public GameObject saucePrefab;

    public bool isInventoryOpen;

	// Use this for initialization
	void Start () {
        inventoryButton.onClick.AddListener(onClickInventory);
        foreach(UnityEngine.UI.Button button in itemButtons)
        {
            button.onClick.AddListener(onClickItem);
        }
    }
	
	// Update is called once per frame
	void Update () {

        checkForNumberPressed();

        if (Input.GetKeyDown(KeyCode.R))
        {
            openInventory();
        }
        if (isInventoryOpen)
        {
            cheeseText.text = cheeseAmount.ToString();
            sausageText.text = sausageAmount.ToString();
            meatText.text = meatAmount.ToString();
            sauceText.text = sauceAmount.ToString();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") && other.gameObject.GetComponent<Pickups>().isPickable)
        {
            switch (other.gameObject.name)
            {
                case ("Käse"):
                    cheeseAmount++;
                    break;
                case ("Wurst"):
                    sausageAmount++;
                    break;
                case ("Fleisch"):
                    meatAmount++;
                    break;
                case ("Soße"):
                    sauceAmount++;
                    break;
            }
            inventoryButton.GetComponent<Animator>().SetTrigger("itemAdded");
            Destroy(other.gameObject);
        }
    }

    void checkForNumberPressed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventSystem.current.SetSelectedGameObject(itemButtons[0].gameObject);
            onClickItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventSystem.current.SetSelectedGameObject(itemButtons[1].gameObject);
            onClickItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventSystem.current.SetSelectedGameObject(itemButtons[2].gameObject);
            onClickItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventSystem.current.SetSelectedGameObject(itemButtons[3].gameObject);
            onClickItem();
        }
    }

        public void openInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void onClickInventory()
    {
        openInventory();
    }

    private void onClickItem()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case ("Cheese"):
                if (cheeseAmount > 0)
                {
                    Instantiate(cheesePrefab, transform.position, transform.rotation, pickupParent.transform).gameObject.name = "Käse";
                    cheeseAmount--;
                }
                break;
            case ("Sausage"):
                if (sausageAmount > 0)
                {
                    Instantiate(sausagePrefab, transform.position, transform.rotation, pickupParent.transform).gameObject.name = "Wurst";
                    sausageAmount--;
                }
                break;
            case ("Meat"):
                if (meatAmount > 0)
                {
                    Instantiate(meatPrefab, transform.position, transform.rotation, pickupParent.transform).gameObject.name = "Fleisch";
                    meatAmount--;
                }
                break;
            case ("Sauce"):
                if (sauceAmount > 0)
                {
                    Instantiate(saucePrefab, transform.position, transform.rotation, pickupParent.transform).gameObject.name = "Soße";
                    sauceAmount--;
                }
                break;

        }
    }
}
