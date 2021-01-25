using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public AK.Wwise.Bank menuBank; // wwise soundbank for the in menu sounds
    public AK.Wwise.Event menuMusic = new AK.Wwise.Event(); // wwise event for playing menu music
    private void Awake()
    {
        menuBank.Load(); // on awake load the menu music sound bank
    }
    private void Start()
    {
        menuMusic.Post(gameObject); // post the music event
    }

    public void ButtonStart() {
        AkSoundEngine.StopAll(); // when the player clicks start, stop all the sounds so bank can be unloaded
        menuBank.Unload(); // unload sound bank
        SceneManager.LoadScene(1); // load level scene
    }
    public void ButtonQuit()
    {
        AkSoundEngine.StopAll(); // when the player clicks start, stop all the sounds so bank can be unloaded
        menuBank.Unload(); // unload sound bank
        Application.Quit(); // close application
    }
}
