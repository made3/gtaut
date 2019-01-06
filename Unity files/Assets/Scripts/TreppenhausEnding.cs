using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreppenhausEnding : MonoBehaviour {

    [SerializeField]
    private Image fadeScreen;
    [SerializeField]
    private float fadeSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        while(fadeScreen.color.a < 1)
        {
            print(fadeScreen.color);
            fadeScreen.color = fadeScreen.color + new Color(0, 0, 0, 0.01f * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(0);
    }
}
