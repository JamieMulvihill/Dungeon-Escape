using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour
{
    public Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
    }

    // Update is called once per frame
    void FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 2, false);
    }
}
