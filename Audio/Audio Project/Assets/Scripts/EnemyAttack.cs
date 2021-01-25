using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Animator animator;
    public GameObject attackCollider;
    EnemyMovement enemy;
    EnemySensing enemySense;
    EnemySound sound;
    PlayerMovement player;
    public float afterAttackWait = 4f;
    public bool attackAnimEnded;

    public AK.Wwise.Switch attackingSwitch = new AK.Wwise.Switch(); // wwise switch for the enemy attack sound
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<EnemyMovement>();
        enemySense = GetComponent<EnemySensing>();
        sound = GetComponent<EnemySound>();

        attackCollider.SetActive(false);
        animator = GetComponent<Animator>();
        animator.speed = 1.25f;
        animator.SetBool("isAttacking", false);
    }

    public void SetEnemyAttacking()
    {
        enemy.Stop();
        enemy.isWalking = false;
        animator.SetBool("isAttacking", true);
        sound.SetSwitchValue(attackingSwitch); // set enemy sounds switch to attacking switch
        sound.PlayEnemySound(); // play enemy sounds event

        StartCoroutine(ResetAfterAttack(4f));
    }


    IEnumerator ResetAfterAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        enemy.isAttacking = false;
        animator.SetBool("isAttacking", false);
        attackCollider.SetActive(false);
        enemy.SenseForIntruder();

        if (enemy.intruderDetected)
        {
            enemy.intruderDetected = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (enemy.isAttacking)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemy.attackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    player.isAlive = false;

                }
            }
        }
    }
}
