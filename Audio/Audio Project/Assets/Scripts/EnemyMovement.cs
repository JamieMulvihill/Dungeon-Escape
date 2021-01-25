using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

// Script to control the enemies movemnt

public class EnemyMovement : MonoBehaviour
{
    Animator animator;
    EnemySound sound;
    EnemyShouting enemyShout;
    EnemyAttack enemyAttack;
    EnemySensing enemySense;

    public GameObject player;
    public NavMeshAgent enemy; // nav mesh agent so enemy can move
    public GameObject[] enemyWaypoints = new GameObject[5];
    public Vector3 intruderSoundSource;
    public int currentWaypoint;
    public bool isWalking, isAttacking, isShouting, hasKilled, intruderDetected, isEating;
    
    public float listeningRange = 35;
    public float stepSoundGap = 0f;
    public float stepSoundRate = .7165f;
    public float animationSpeed = 1.25f;
    public float attackRange = 10f;

    public AK.Wwise.Switch eatingSwitch = new AK.Wwise.Switch(); // wwise switch to set the enemy sounds to the eating switch
    public AK.Wwise.Event stompSound = new AK.Wwise.Event(); // wwise event for the enemy stomp/footstep sounds


    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<EnemySound>();
        enemyShout = GetComponent<EnemyShouting>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemySense = GetComponent<EnemySensing>();
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
        animator.SetBool("isWalking", true);
        enemy.SetDestination(enemyWaypoints[currentWaypoint].transform.position);
        isAttacking = false;
        isWalking = true;
        isEating = false;
        isShouting = false;
        hasKilled = false;
        currentWaypoint = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKilled && !isEating) // if enemy has killed and isnt eating
        {
            EatPlayer(); // call the eat player function
        }
        if (!hasKilled) { 
            EnemyMove();
            AttackCheck();
            NextWaypoint();
        }
    }

    void EatPlayer() {
        
        isAttacking = false;
        isWalking = false;
        isShouting = false;
        intruderDetected = false;
        animator.SetBool("isShouting", false);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isWalking", isWalking);
        sound.StopEnemySound(); // stop whichever sound is currently playing for the enemy sounds event
        sound.SetSwitchValue(eatingSwitch); // set the enemy switch value to eating switch
        sound.PlayEnemySound(); // play event
        isEating = true;
    }

    public void ChaseIntruder(Vector3 intruderSound) {
        intruderSoundSource = intruderSound;
    }

    public void SenseForIntruder() {
        Stop();
        enemySense.SensingForPlayer();
    }

    void EnemyMove()
    {
        if (isWalking && !isAttacking)
        {
            if (!intruderDetected)
                enemy.SetDestination(enemyWaypoints[currentWaypoint].transform.position); // se destination for enemy to move
           
            else if(intruderDetected)
                enemy.SetDestination(intruderSoundSource);

            stepSoundGap += Time.deltaTime;
            if (stepSoundGap > stepSoundRate) //comtrolling the rate that enemy stomps sounds are played
            {
                stompSound.Post(gameObject); // post the enemy stomp event
                stepSoundGap = 0f;
            }
        }
    }

    void AttackCheck() {
        if (intruderDetected && (transform.position - intruderSoundSource).magnitude <= attackRange && !isAttacking) {
            enemyAttack.SetEnemyAttacking();
            isAttacking = true;
            Stop();
        }
    }

    void NextWaypoint()
    {
        if ((enemyWaypoints[currentWaypoint].transform.position - gameObject.transform.position).magnitude < 10)
        {
            SenseForIntruder();
            currentWaypoint = Random.Range(0, 4);
        }
    }

    public void Stop() {
        enemy.SetDestination(transform.position);
    }
}
