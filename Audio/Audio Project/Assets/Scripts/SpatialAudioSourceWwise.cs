using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudioSourceWwise : MonoBehaviour
{
    public bool isColliding = false;
    public AK.Wwise.Event wwiseEvent = new AK.Wwise.Event(); // wwise event for playing spatial sound

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || isColliding) { return; }

        isColliding = true;
        wwiseEvent.Post(gameObject); // if player enters collider post
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || !isColliding) { return; }
        isColliding = false;
        wwiseEvent.Stop(gameObject); // if player leaves collider stop
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" || isColliding) { return; }

        isColliding = true;
        wwiseEvent.Post(gameObject);
    }

    // function to manually stop the event if needed
    public void StopWwiseEvent() {
        wwiseEvent.Stop(gameObject); 
    }
}