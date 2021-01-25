using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public GameObject[] waypoints = new GameObject[3];
    public GameObject player;
    public GameObject door;
    public int currTarg;
   
    private TargetWaypoint target;
    private SpatialAudioSourceWwise audioSource;
    // Start is called before the first frame update
    void Start()
    {
        target = player.GetComponent<TargetWaypoint>();
        waypoints[target.GetCurrentTarget()].SetActive(true);
        audioSource = waypoints[target.GetCurrentTarget()].transform.GetChild(2).GetComponent<SpatialAudioSourceWwise>();
        currTarg = 0;

    }

    // Update is called once per frame
    void Update()
    {
        currTarg = target.GetCurrentTarget();
        if (target.GetFoundBool()) {

           
            
            if (target.GetCurrentTarget() < 3)
            {
                waypoints[target.GetCurrentTarget() - 1].SetActive(false);
                audioSource.StopWwiseEvent();
                audioSource = waypoints[target.GetCurrentTarget()].transform.GetChild(2).GetComponent<SpatialAudioSourceWwise>();
                waypoints[target.GetCurrentTarget()].SetActive(true);

                target.SetFoundBool(false);
            }
            else {
                waypoints[target.GetCurrentTarget() - 1].SetActive(false);
                audioSource.StopWwiseEvent();
                door.SetActive(false);
            }
        }
    }
}
