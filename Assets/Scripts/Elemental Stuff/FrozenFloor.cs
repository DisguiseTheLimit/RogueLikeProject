using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenFloor : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    EnemyController enemyController;

    public bool isTouchingPlayer = false;
    public bool isTouchingEnemy = false;

    void Update()
    {
        if (isTouchingPlayer)
        {
            playerController.speed = playerController.speed + 1;
        }

        if (isTouchingEnemy)
        {
            enemyController.speed = enemyController.speed + 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.tag == "Player")
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
}
