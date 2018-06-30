using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopClock : MonoBehaviour {

    private Text timeText;

	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timeText.text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute.ToString("D2");
	}
}
