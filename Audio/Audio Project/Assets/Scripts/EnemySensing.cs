using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensing : MonoBehaviour
{
    Animator animator;
    
    public float sensingTime;
    public float senseResult = 0f;
    public float maxSensingRange = 300f;
    public bool isSensing = false;

    public PlayerMovement player;
    EnemyMovement enemy;
    EnemyShouting enemyShout;
    
    EnemySound sound; // Script that controls the enemy sounds
    public AK.Wwise.Switch sensingSwitch = new AK.Wwise.Switch(); // wwise swiotch for setting the enemy to sensing
    public AK.Wwise.RTPC sensingSoundValue = new AK.Wwise.RTPC(); // wwise RTPC to control the timestrech effect of the sensing sound

    void Start()
    {
        sound = GetComponent<EnemySound>();
        enemyShout = GetComponent<EnemyShouting>();
        enemy = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        animator.speed = 1.25f;
    }

    void CalculateSensingRhthym() {

        Vector3 enemyToPlayer = player.transform.position - transform.position; // get vector from player to enemy
        float sense = enemyToPlayer.magnitude / maxSensingRange * 100; // calculate magnitude of vector as percentage of the enmies max sensing range
        if (sense > 100)
            sense = 100; // clip the result to 100

        sensingSoundValue.SetValue(gameObject, sense); // set the RTPC value for time strech to the sense result
    }

    private void Update()
    {
        CalculateSensingRhthym();
    }

    public void SensingForPlayer() {
        isSensing = true;
        enemy.isWalking = false;
        animator.SetBool("isWalking", false);
        sound.SetSwitchValue(sensingSwitch); // set the enemy sounds switch to the sensing switch
        sound.PlayEnemySound(); // play enemy sounds
        StartCoroutine(Sensing(sensingTime));
    }

    public void FoundPlayer() {

        if (!enemy.intruderDetected && !enemy.isAttacking) // if the player has been detected by the enemy
        {
            StopCoroutine(Sensing(0));
            sound.StopEnemySound();  // stop the sensing sound
            enemyShout.EnemyIsShouting();
            isSensing = false;
        }
    }

    IEnumerator Sensing(float duration)
    {
        yield return new WaitForSeconds(duration); // when the enemy is finsihed sensing
        sound.StopEnemySound(); // as the sensing sound is a synth sound, sound must be stopped manually
        enemy.isWalking = true;
        animator.SetBool("isWalking", true);
        isSensing = false;
    }
}
