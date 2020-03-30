using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the enemy character's AI

public class EnemyController : MonoBehaviour
{

    public int speed;
    Transform TargetPlayer;
    Transform Projectile;
    private bool AttackDelay = false;
    public float Delay;

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Used to find what GameObject has the tag "Player" and then references that GameObject's 
                                                                                             // transform position

        Projectile = GameObject.FindWithTag("Projectile").GetComponent<Transform>(); // Used to find what GameObject has the tag "Projectile" and then references that GameObject's 
                                                                                               // transform position 
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, TargetPlayer.position) > 0) // If the distance between the enemy and player is more than 0 move towards the player 
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPlayer.position, speed * Time.deltaTime); // by using enemy's and player's position and speed to calculate
        }

        if(Vector2.Distance(transform.position, Projectile.position) < 1) // If the distance between the enemy and the projectile is less than 1 destroy the enemy
        {
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, TargetPlayer.position) < 1) // If the distance between the enemy and player is less than 1 and if the bool "AttackDelay" is false
                                                                             // execute the "DamagePlayer" function from the PlayerController script passing 1 as the damage value
                                                                             // then after this execute the function "StartDelay"
        {
            if (!AttackDelay)
            PlayerController.DamagePlayer(1);
            StartCoroutine(StartDelay());
        }
    }

    private IEnumerator StartDelay() // Sets the bool "AttackDelay" to true, then waits for a certain amount of seconds which is specified in the inspector in unity, then sets 
                                     // the bool "AttackDelay" to false
    {                                       
        AttackDelay = true;
        yield return new WaitForSeconds(Delay);
        AttackDelay = false;
    }
}