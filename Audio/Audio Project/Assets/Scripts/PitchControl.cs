using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchControl : MonoBehaviour
{
    public GameObject listner;
    public AK.Wwise.RTPC pitchShiftValue = new AK.Wwise.RTPC(); // wwise RTPC used to control the pitch of a sound in the game 

    public float maxDistance = 0;
    public float lowPassMax = 1;

    // Update is called once per frame
    void Update()
    {
        PitchControlRaycast();
    }

    void PitchControlRaycast()
    {
        // create vector from sound source to the player
        // then set RTPC value to be the minumun of the result of the magnitude of the vector as a percentage of the attenuation max distance or attenuation max distance
        Vector3 direction = listner.transform.position - gameObject.transform.position;
        pitchShiftValue.SetValue(gameObject, Mathf.Min(maxDistance, (((direction.magnitude / maxDistance) * 100))));  
    }
}
