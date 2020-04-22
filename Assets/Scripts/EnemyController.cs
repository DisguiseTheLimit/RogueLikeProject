using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script controls the enemy character's AI

public class EnemyController : MonoBehaviour
{
    public float speed;
    
    public float delay;

    public float meleeDelayTime;

    public int health;
    public int maxHealth;

    HealthController targetPlayer;

    //Transform projectile;
    Transform meleeTag;
    Transform projectileTag;
    bool attackDelay = false;
    bool meleeDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = StageManager.Instance.Player;

        //targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Used to find what GameObject has the tag "Player" and then references that GameObject's 
        // transform position

        //projectile = GameObject.FindWithTag("Projectile").GetComponent<Transform>(); // Used to find what GameObject has the tag "Projectile" and then references that GameObject's 
        // transform position 

        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();

        projectileTag = GameObject.FindWithTag("Projectile").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, targetPlayer.Position) > 0) // If the distance between the enemy and player is more than 0 move towards the player 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.Position, speed * Time.deltaTime); // by using enemy's and player's position and speed to calculate
        }

        //if (Vector2.Distance(transform.position, projectile.position) < 1 && health <= maxHealth) // If the distance between the enemy and the projectile is less than 1 destroy the enemy
        //{
        //    health--;
        //}

        //if (Vector2.Distance(transform.position, projectile.position) < 1 && health == 0) // If the distance between the enemy and the projectile is less than 1 destroy the enemy
        //{
        //    Destroy(gameObject);
        //}

        if (Vector2.Distance(transform.position, targetPlayer.Position) < 1) // If the distance between the enemy and player is less than 1 and if the bool "AttackDelay" is false
                                                                             // execute the "DamagePlayer" function from the PlayerController script passing 1 as the damage value
                                                                             // then after this execute the function "StartDelay"
        {
            if (!attackDelay)
            {
                targetPlayer.DamagePlayer(1);
                StartCoroutine(StartDelay());
            }
        }

        if (Input.GetMouseButton(1))
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

        if (Vector2.Distance(transform.position, projectileTag.position) < 1 && health <= maxHealth)
        {
            health--;
        }

        if (Vector2.Distance(transform.position, projectileTag.position) < 1 && health == 0)
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

    IEnumerator MeleeStartDelay()
    {
        meleeDelay = true;
        yield return new WaitForSeconds(meleeDelayTime);
        meleeDelay = false;
    }
}