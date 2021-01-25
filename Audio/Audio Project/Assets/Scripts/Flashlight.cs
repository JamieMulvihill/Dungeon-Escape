using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool isOn = false;
    public GameObject flashlightSurce;

    public AK.Wwise.Switch flashlightSwitch; // wwise switch for setting the player sounds to flashlight
    public AK.Wwise.Event playerSounds = new AK.Wwise.Event(); // event for player player sounds
    void Update()
    {
        if (Input.GetButtonDown("CKey")) {

            if (!isOn)
            {
                flashlightSurce.SetActive(true); // turn on flash light
                flashlightSwitch.SetValue(gameObject); // set switch value to flashlight
                playerSounds.Post(gameObject); // post player sounds event
                isOn = true;
            }
            else if (isOn) {
                flashlightSurce.SetActive(false); // turn off flashlight
                flashlightSwitch.SetValue(gameObject); // set switch to flashlight
                playerSounds.Post(gameObject); // post player sounds event
                isOn = false;

            }
        }
        
    }
}
