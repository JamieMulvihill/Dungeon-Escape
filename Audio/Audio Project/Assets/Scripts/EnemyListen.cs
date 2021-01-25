using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls how the enemy hears the player
public class EnemyListen : MonoBehaviour
{
    EnemyMovement enemy;
    EnemyShouting enemyShout;
    EnemySensing enemySense;
    PlayerMovement player;

    public float noiseHeard;
    public float curiousNoiseValue = 2;

    public Vector3 distanceToPlayer;
    public Vector3 soundSource;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<EnemyMovement>();
        enemyShout = GetComponent<EnemyShouting>();
        enemySense = GetComponent<EnemySensing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive) // if the player is dead, enemy has won
        {
            enemy.hasKilled = true;
        }

        distanceToPlayer = player.transform.position - transform.position; // vector from player to enemy
        noiseHeard = player.noiseLevel / distanceToPlayer.magnitude; // noise value is set the players noise level divided by the magnitude of vector

        if (noiseHeard > curiousNoiseValue) // if the noise heard is higher than curious noise to check 
        {
            soundSource = player.transform.position; // store the position of player when noise is made
            enemy.ChaseIntruder(soundSource); // set the enemy to chase, and move towards sound source
            if (!enemy.intruderDetected)
            {
                if (!enemySense.isSensing)
                {
                    enemyShout.EnemyIsShouting(); // set enemy to roar before chasing the player
                }

                else
                {
                    enemySense.FoundPlayer(); // set the enemy to have found player
                }
            }
        }
    }
}
