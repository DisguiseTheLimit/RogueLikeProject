using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the enemy character's AI

public class NonDamagingEnemyController : MonoBehaviour
{

    public float speed;

    public float delay;

    public float damageDelayTime;

    public float meleeDelayTime;

    public int health;
    public int maxHealth;

    HealthController targetPlayer;

    Rigidbody2D rigidbody;

    Transform meleeTag;
    Transform lavaTag;
    bool attackDelay = false;
    bool meleeDelay = false;
    bool damageDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = StageManager.Instance.Player;

        //targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Used to find what GameObject has the tag "Player" and then references that GameObject's 
        // transform position

        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();
        lavaTag = GameObject.FindWithTag("Lava").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPlayer.Position) > 0) // If the distance between the enemy and player is more than 0 move towards the player 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.Position, speed * Time.deltaTime); // by using enemy's and player's position and speed to calculate
        }

        if (Vector2.Distance(transform.position, targetPlayer.Position) < 1)
        {
            if (!attackDelay)
            {
                targetPlayer.SpeedDecrease();
                StartCoroutine(StartDelay());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Vector2.Distance(transform.position, meleeTag.position) < 1 && health <= maxHealth)
            {
                if (!meleeDelay)
                {
                    health--;
                    StartCoroutine(MeleeStartDelay());
                }
            }

            if (Vector2.Distance(transform.position, meleeTag.position) < 1 && health == 0)
            {
                Destroy(gameObject);
            }
        }

        if (Vector2.Distance(transform.position, lavaTag.position) < 1)
        {
            if (!damageDelay)
            {
                TakeDamage(1);
                StartCoroutine(StartDelay());
            }
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartDelay() // Sets the bool "AttackDelay" to true, then waits for a certain amount of seconds which is specified in the inspector in unity, then sets 
                             // the bool "AttackDelay" to false
    {
        attackDelay = true;
        yield return new WaitForSeconds(delay);
        attackDelay = false;
    }

    IEnumerator DamageDelay()
    {
        damageDelay = true;
        yield return new WaitForSeconds(damageDelayTime);
        damageDelay = false;
    }

    IEnumerator MeleeStartDelay()
    {
        meleeDelay = true;
        yield return new WaitForSeconds(meleeDelayTime);
        meleeDelay = false;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }
}