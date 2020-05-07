using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    //[SerializeField]
    //HealthController healthController;

    [SerializeField]
    PlayerController playerController;

    public HealthController healthController;

    public EnemyController enemyController;

    public bool isTouchingPlayer = false;
    public bool isTouchingEnemy = false;
    public bool damageDelay = false;

    void Update()
    {
        if(isTouchingPlayer)
        {
            if (!damageDelay)
            {
                healthController.DamagePlayer(1);
                playerController.speed = playerController.speed / 2;
                StartCoroutine(DamageDelay());
            }
        }

        if (isTouchingEnemy)
        {
            if (!damageDelay)
            {
                enemyController.TakeDamage(1);
                enemyController.speed = enemyController.speed / 2;
                StartCoroutine(DamageDelay());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D enter)
    {
        if(enter.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }

        if (enter.gameObject.tag == "Enemy")
        {
            isTouchingEnemy = true;
        }
    }

    void OnTriggerExit2D(Collider2D exit)
    {
        if (exit.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
            playerController.speed = 7;
        }

        if (exit.gameObject.tag == "Enemy")
        {
            isTouchingEnemy = false;
            enemyController.speed = 4;
        }
    }

    IEnumerator DamageDelay()
    {
        damageDelay = true;
        yield return new WaitForSeconds(2);
        damageDelay = false;
    }
}
