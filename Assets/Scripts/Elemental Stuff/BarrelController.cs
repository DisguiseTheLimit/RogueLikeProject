using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    HealthController healthController;

    public bool isTouching = false;

    Transform gameObject;
    Transform playerTag;
    Transform enemyTag;

    void Start()
    {
        playerTag = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemyTag = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
    }

    void Update()
    {
        if (isTouching)
        {
            Destroy(gameObject);
            if (Vector2.Distance(transform.position, playerTag.position) < 3) 
            {
                healthController.DamagePlayer(2);
            }
            if (Vector2.Distance(transform.position, enemyTag.position) < 3)
            {
                enemyController.TakeDamage(1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.tag == "Projectile")
        {
            isTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D exit)
    {
        if (exit.gameObject.tag == "Projectile")
        {
            isTouching = false;
        }
    }
}
