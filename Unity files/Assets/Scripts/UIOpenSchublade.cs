using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIOpenSchublade : MonoBehaviour {

    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;
    private Animator _animator;
    private AudioSource audiosrc;
    public bool isOpen;

    // Use this for initialization
    void Start () {
        myText.color = Color.clear;
        _animator = GetComponent<Animator>();
        isOpen = false;
        audiosrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        FadeText();

        if (displayInfo)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen)
                {
                    _animator.SetBool("open", false);
                    audiosrc.Play();
                    isOpen = false;
                }
                else
                {
                    _animator.SetBool("open", true);
                    audiosrc.Play();
                    isOpen = true;
                }
                
            }
        }
    }

    void OnMouseOver()
    {
        displayInfo = true;
    }

    void OnMouseExit()
    {
        displayInfo = false;
    }

    void FadeText()
    {
        if (displayInfo)
        {
            if (isOpen)
            {
                myText.text = "Press E to close";
                myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText.text = myString;
                myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
            }
        }
        else
        {
            myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }

}
