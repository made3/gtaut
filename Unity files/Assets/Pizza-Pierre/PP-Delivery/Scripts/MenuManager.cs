using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Light sceneLight;
    private Color startColor;
    private float lastColor;
    private float nextColor;
    private float tmpColor;
    private float tmpLerp = 0;
    public float lightSpeed = 1;

    public UnityEngine.UI.Button tutorial;
    public UnityEngine.UI.Button level;
    public UnityEngine.UI.Button exit;

    // Use this for initialization
    void Start () {
        tutorial.onClick.AddListener(onTutorialPressed);
        level.onClick.AddListener(onLevelPressed);
        exit.onClick.AddListener(onExitPressed);
        startColor = sceneLight.color;
        lastColor = startColor.g *255;
        changeNextColor();
    }
	
	// Update is called once per frame
	void Update () {

        tmpColor = Mathf.Lerp(lastColor, nextColor, tmpLerp);
        tmpLerp += Time.deltaTime * lightSpeed;
        sceneLight.color = new Color(startColor.r, tmpColor/255, startColor.b);

        if(tmpLerp >= 1)
        {
            lastColor = nextColor;
            changeNextColor();
            tmpLerp = 0;
        }

	}

    private void onTutorialPressed()
    {
        SceneManager.LoadScene(1);
    }
    private void onLevelPressed()
    {
        SceneManager.LoadScene(2);
    }
    private void onExitPressed()
    {
        Application.Quit();
    }

    private void changeNextColor()
    {
        nextColor = Random.Range(150, 230);
    }
}
