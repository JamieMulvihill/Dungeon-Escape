using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject loseUi;
    public GameObject winUi;
    bool bacameActive = false;
    public Image fadeLoseImage;
    public Image fadeWinImage;
    public MusicManager musicManager; // controls the in game sound bank

    void Start()
    {
        fadeLoseImage.canvasRenderer.SetAlpha(0.0f);
        fadeWinImage.canvasRenderer.SetAlpha(0.0f);
    }

    void FadeIn(Image fadeImage)
    {
        fadeImage.CrossFadeAlpha(1, 2, false);
    }

    // Update is called once per frame
    void Update()
    {

        // Draw UI based on whether player has won or lost
        if (!player.isAlive && !bacameActive) {
            Cursor.lockState = CursorLockMode.None;
            loseUi.SetActive(true);
            FadeIn(fadeLoseImage);
            bacameActive = true;
        }

        if (player.hasEscaped && !bacameActive)
        {
            Cursor.lockState = CursorLockMode.None;
            winUi.SetActive(true);
            FadeIn(fadeWinImage);
            bacameActive = true;
        }
    }

    public void ButtonReturn()
    {
        // when the player clicks on the return button
        AkSoundEngine.StopAll(); // stop all sounds being player so soundbank can be unloaded
        musicManager.gameBank.Unload(); // unload sound bank
        SceneManager.LoadScene(0); // load menu scene
    }
}
