using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public UnityEngine.UI.Button settingsButton;
    public GameObject settingsUI;
    public Train[] trainScripts;
    public Collector_Brokkoli brokkoliScript;

    private Slider trainSpeedSlider;
    private Slider fireRateSlider;

    // Use this for initialization
    void Start () {
        settingsButton.onClick.AddListener(onSettingsClicked);
        trainSpeedSlider = settingsUI.GetComponentsInChildren<Slider>()[0];
        fireRateSlider = settingsUI.GetComponentsInChildren<Slider>()[1];
    }
	
	// Update is called once per frame
	void Update () {
        if (settingsUI.activeSelf)
        {
            foreach(Train train in trainScripts)
            {
                train.trainSpeed = trainSpeedSlider.value;
            }
            brokkoliScript.fireRate = fireRateSlider.value;
        }
	}

    void onSettingsClicked()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
    }
}
