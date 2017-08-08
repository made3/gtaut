using UnityEngine;
using UnityEngine.UI;

public class UIOpenFenster : MonoBehaviour {

    public string myString;
    public Text myText1;
    public float fadeTime;
    public bool displayInfo;
    private Animator _animator;
    public bool isOpen;
    private int randomAnimation;
    public ParticleSystem schnee;

    // Use this for initialization
    void Start()
    {
        myText1.color = Color.clear;
        _animator = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        FadeText();

        if (displayInfo)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen)
                {
                    randomAnimation = Random.Range(1, 3);
                    _animator.SetBool("open"+randomAnimation, true);
                    isOpen = true;
                    schnee.Play();
                }
                else
                {
                    _animator.SetBool("open"+randomAnimation, false);
                    isOpen = false;
                    schnee.Stop();
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
                myText1.text = "Press E to close";
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText1.text = myString;
                myText1.color = Color.Lerp(myText1.color, Color.white, fadeTime * Time.deltaTime);
            }
        }
        else
        {
            myText1.color = Color.Lerp(myText1.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }


}
