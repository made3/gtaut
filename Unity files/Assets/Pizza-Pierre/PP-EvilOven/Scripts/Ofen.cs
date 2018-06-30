using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Ofen : MonoBehaviour {

    public float movingSpeed;
    public EvilOven_GameManager gameManager;
    public GameObject player;
    public float ofenDamage;
    public Slider ofenSlider;
    public Light ofenLight;
    public Light directionalLight;
    public float LightColorFadingSpeed;

    private Color globalStartColor;
    private float lerpTmp;
    private bool startColorLerp;

    private Stopwatch stopwatch;
    private float tmpDamageTime;
    public float timeToBurn;

    private float currentIntensity;
    private float nextIntensity;
    private float tmpLerpCount;
    public float lightLerpSpeed;

    // Use this for initialization
    void Start () {
        globalStartColor = directionalLight.color;
        stopwatch = new Stopwatch();
        tmpDamageTime = timeToBurn;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EvilOven_GameManager>();
        currentIntensity = ofenLight.intensity;
        nextIntensity = Random.Range(80, 220);
    }
	
	// Update is called once per frame
	void Update () {

        if(tmpLerpCount <= 1)
        {
            ofenLight.intensity = Mathf.Lerp(currentIntensity, nextIntensity, tmpLerpCount);
            tmpLerpCount += Time.deltaTime * lightLerpSpeed;
        }
        else
        {
            tmpLerpCount = 0;
            currentIntensity = nextIntensity;
            nextIntensity = Random.Range(80, 220);
        }


        if (gameManager.currentState == EvilOven_GameManager.gameStates.playing)
        {
            ofenSlider.value = transform.position.z;
            if (transform.position.z <= 80)
            {
                stopwatch.Start();
            }
            if (stopwatch.ElapsedMilliseconds / 100 > tmpDamageTime * 10)
            {
                startColorLerp = true;
                player.GetComponent<EvilOven_PlayerMovement>().currentHP -= ofenDamage;
                tmpDamageTime += timeToBurn;
            }
            if (startColorLerp)
            {
                directionalLight.color = Color.Lerp(globalStartColor, Color.red, lerpTmp += Time.deltaTime * LightColorFadingSpeed);
            }
            if (transform.position.z > 0)
            {
                transform.position -= new Vector3(0, 0, Time.deltaTime * movingSpeed);
            }
        }
    }
}
