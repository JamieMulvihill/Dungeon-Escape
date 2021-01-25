using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occulsion : MonoBehaviour
{

    public GameObject listner;
    public float maxDistance = 0;
    public float lowPassMax = 1;
    public float maxVolume = 1;
    public float compressFloat = 0;

    public AK.Wwise.RTPC occulusionLowPass = new AK.Wwise.RTPC(); // RTPC to controll low pass filter for occlusion
    public AK.Wwise.RTPC compressorValue = new AK.Wwise.RTPC(); // RTPC to control Compressor effect for occlusion
    int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        // set the layers for the ray to hit
        layerMask = 1 << LayerMask.NameToLayer("Ignore");
        layerMask |= 1 << LayerMask.NameToLayer("Occlude");
        layerMask |= 1 << LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        OcculusionRaycast();
    }

    void OcculusionRaycast() {


        // create vector from sound source to the player
        // cast ray along vector
        // check if ray has hit an object
        Vector3 direction = listner.transform.position - gameObject.transform.position;
        RaycastHit hitObject;
        bool hasHit;
        hasHit = Physics.Raycast(gameObject.transform.position, direction, out hitObject, maxDistance, layerMask);

        if (hasHit)
        {
            // check if the hit object is on the occlude layer and the object is not itself
            if (hitObject.collider.gameObject.layer == LayerMask.NameToLayer("Occlude") && hitObject.collider.tag != gameObject.tag)
            {
                // check if the hit object is a wall and if so set the compressor value scale to be the compressor value of the hit wall
                float wallCompressorScale = 0f;
                if ((hitObject.collider.gameObject.GetComponent<WallProperties>())){
                    wallCompressorScale = hitObject.collider.gameObject.GetComponent<WallProperties>().GetCompressorValue();

                    // set compressor result to the minumun value  of the
                    // magnitude of the vector between player and sound source as a percentage of the attenuation max distance multiplied by the compressor scale of the wall
                    // or attenuation max distance
                    compressFloat = Mathf.Min(maxDistance, (((direction.magnitude / maxDistance) * 100) * wallCompressorScale)); 
                }

                //  Set occlusio lowpass RTPC based on magnitude of the vector between player and sound source divdied by max distance of attenuation
                occulusionLowPass.SetValue(gameObject, Mathf.Max(1f, .5f + (direction.magnitude / maxDistance))); 
                compressorValue.SetValue(gameObject, compressFloat); // set compressor RTPC to be compress result
            }
            else {
               
                // if the ray has not hit an object before the player apply no occlusio and set RTPC values to 0
                occulusionLowPass.SetValue(gameObject, 0);
                compressorValue.SetValue(gameObject, 0);
            }
        }
    }
}
