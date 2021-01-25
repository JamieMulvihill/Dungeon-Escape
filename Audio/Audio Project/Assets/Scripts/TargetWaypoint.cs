using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWaypoint : MonoBehaviour
{
    private int maxTargets = 3;
    private int currentTarget = 0;
    private bool foundTarget = false;
    public GameObject musicGameObject;

    public AK.Wwise.Event pickUpSuccess = new AK.Wwise.Event(); // Wwise event for when player finds a pick up
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "waypoint") {
            pickUpSuccess.Post(musicGameObject); // post event
            if (currentTarget < maxTargets)
            {
                currentTarget++;
                foundTarget = true;
            }
        }
    }
    public int GetCurrentTarget() {
        return currentTarget;
    }
    public bool GetFoundBool() {
        return foundTarget;
    }
    public void SetFoundBool(bool foundStatus) {
        foundTarget = foundStatus;
    }
}
