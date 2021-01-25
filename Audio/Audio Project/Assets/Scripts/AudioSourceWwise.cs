using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to play a wwise event
public class AudioSourceWwise : MonoBehaviour
{

    public AK.Wwise.Event playEvent = new AK.Wwise.Event(); // event to play a sound in the game
   
    // Start is called before the first frame update
    void Start()
    {
        playEvent.Post(gameObject); // post in game sound
    }
}