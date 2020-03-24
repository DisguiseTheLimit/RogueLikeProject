using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the enemy character's AI

public class EnemyController : MonoBehaviour
{

    public int speed;
    Transform TargetPlayer;
    Transform Projectile;

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Used to find what GameObject has the tag "Player" and then references that GameObject's 
                                                                                             // transform position

        Projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Transform>(); // Used to find what GameObject has the tag "Projectile" and then references that GameObject's 
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
    }
}