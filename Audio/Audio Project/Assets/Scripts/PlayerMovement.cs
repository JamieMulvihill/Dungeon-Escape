using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public TargetWaypoint targetWaypoint;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float stepSoundGap = 0f;
    public float stepSoundRate = .5f;
    public float stepSoundRange = 35f;
    public float noiseLevel = 0;
    public float waterNoiseScale = 5;
    public Transform groundCheck;
    public LayerMask groundMask;
    public EnemyMovement enemy;

    Vector3 velocity;
    bool isGrounded;
    bool isWalking;
    bool inWater;

    public bool isAlive;
    public bool hasDied;
    public bool hasEscaped;
    public bool isEscaping;

    // wwise switches to control which sound should be playing by the footstep event
    public AK.Wwise.Switch grassSwitch;
    public AK.Wwise.Switch walkSwitch;
    public AK.Wwise.Switch waterSwitch;

    // wwise switch to control which sound should be playing by the player Sound event
    public AK.Wwise.Switch deathSwitch;

    public AK.Wwise.RTPC tipToeCompressor = new AK.Wwise.RTPC(); // wwise RTPC to set the Compressor effect value of the footstep event
    public AK.Wwise.Event footstepSound = new AK.Wwise.Event(); // wwise event to play footsteps, controlled by switch
    public AK.Wwise.Event playerSound = new AK.Wwise.Event(); // wwise event to play player action sounds like flashlight and death, controlled by switch

    private void Start()
    {
        isAlive = true;
        hasEscaped = false;
        isEscaping = false;
        inWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        Death();

        if (targetWaypoint.GetCurrentTarget() == 3) {
            isEscaping = true;
        }

        if (isAlive && !hasEscaped)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (Mathf.Abs(x) > 0.01f || Mathf.Abs(z) > 0.01f)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
                stepSoundGap = 0;
            }

            if (isWalking)
            {
                TipToeCheck(); // check is the player holding the shift key
                controller.Move(move * speed * Time.deltaTime);
                stepSoundGap += Time.deltaTime * (speed / 12); // calculating the how ofteen the footstep sounds should be played
                if (stepSoundGap > stepSoundRate)
                {
                    footstepSound.Post(gameObject); // post footstep event

                    stepSoundGap = 0f;
                }
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void TipToeCheck() {

        if (Input.GetKey("left shift"))
        {
            speed = 10f;
            stepSoundRate = .2f;
            tipToeCompressor.SetValue(gameObject, 100); // set RTPC compressor value too too for tip toe
            noiseLevel = 10;
        }
        else if (!Input.GetKey("left shift"))
        {
            speed = 24f;
            stepSoundRate = .5f;
            tipToeCompressor.SetValue(gameObject, 0); // set RTPC compressor value back to 0
            noiseLevel = 125;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water") {

            waterSwitch.SetValue(gameObject); // setting footstep event to water switch
            noiseLevel *= waterNoiseScale;
            inWater = true;
        }
        if (other.tag == "Grass")
        {
            grassSwitch.SetValue(gameObject); // setting footstep event to grass switch
        }
        if (other.tag == "Exit")
        {

            hasEscaped = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Water")
        {
            waterSwitch.SetValue(gameObject); // setting footstep event to water switch
            inWater = true;
        }
        if (other.tag == "Grass")
        {
            grassSwitch.SetValue(gameObject); // setting footstep event to grass switch
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            walkSwitch.SetValue(gameObject); // setting footstep event to normal switch
            noiseLevel /= waterNoiseScale;
            inWater = false;
        }
        if (other.tag == "Grass")
        {

            walkSwitch.SetValue(gameObject); // setting footstep event to normal switch
        }
    }

    void Death() {
        if (!isAlive && !hasDied) {
            deathSwitch.SetValue(gameObject); // setting player sound event to death switch
            playerSound.Post(gameObject); // posting death sound event
            hasDied = true;
        }
    }
}
