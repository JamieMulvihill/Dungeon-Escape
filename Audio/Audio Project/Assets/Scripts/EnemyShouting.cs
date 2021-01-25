using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShouting : MonoBehaviour
{
    Animator animator;
    EnemyMovement enemy;
    public float shoutingTime = 2.4f;
    public bool isShouting = false;
    
    public EnemySound sound; // Script that controls the enemy sounds
    public AK.Wwise.Switch shoutingSwitch = new AK.Wwise.Switch(); // wwise sitch for when the enemy is shouting

    void Start()
    {
        enemy = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        animator.speed = 1.25f;
    }

    public void EnemyIsShouting()
    {
        if (!enemy.intruderDetected && !enemy.hasKilled)
        {
            enemy.isAttacking = false;
            enemy.isWalking = false;
            enemy.isShouting = true;
            animator.SetBool("isShouting", true);
            sound.SetSwitchValue(shoutingSwitch); // set the switch value for the enemy to be shouting
            sound.PlayEnemySound(); // post event
            StartCoroutine(Shouting(shoutingTime));
            enemy.intruderDetected = true;
        }
    }

    IEnumerator Shouting(float duration)
    {
        yield return new WaitForSeconds(duration); // when the enemy is finished shouting
        isShouting = false;
        animator.SetBool("isShouting", false);
        enemy.isWalking = true;
        animator.SetBool("isWalking", true);
        enemy.isShouting = false;
    }
}
