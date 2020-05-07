using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// This script controls the enemy character's AI

public class EnemyController : MonoBehaviour
{
    
    public float speed;

    //public Vector3 direction;
    
    public float delay;

    public int damage;

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
    bool weaponSwitching = false;
    //public bool canWalk;

    // Start is called before the first frame update
    void Start()
    {
        //direction = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f)).normalized;
        //transform.Rotate(direction);

        targetPlayer = StageManager.Instance.Player;

        //targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Used to find what GameObject has the tag "Player" and then references that GameObject's 
        // transform position

        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();
        //lavaTag = GameObject.FindWithTag("Lava").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = transform.position + direction * speed * Time.deltaTime;
        //rigidbody.MovePosition(newPos);
        
        //RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);

        //if (hitRight.collider.tag == "Wall" && canWalk)
        //{
            //canWalk = false;
            //transform.Translate(Vector3.up * 1f * Time.deltaTime);
            //canWalk = true;
        //}

        if (Vector2.Distance(transform.position, targetPlayer.Position) > 0) // If the distance between the enemy and player is more than 0 move towards the player 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.Position, speed * Time.deltaTime); // by using enemy's and player's position and speed to calculate
        }

        if (Vector2.Distance(transform.position, targetPlayer.Position) < 1) // If the distance between the enemy and player is less than 1 and if the bool "AttackDelay" is false
                                                                             // execute the "DamagePlayer" function from the PlayerController script passing 1 as the damage value
                                                                             // then after this execute the function "StartDelay"
        {
            if (!attackDelay)
            {
                targetPlayer.DamagePlayer(damage);
                StartCoroutine(StartDelay());
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponSwitching = true;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponSwitching = false;
        }

        if (Input.GetMouseButtonDown(0) && weaponSwitching)
        {
            if (Vector2.Distance(transform.position, meleeTag.position) < 1 && health <= maxHealth)
            {
                if (!meleeDelay)
                {
                    TakeDamage(5);
                    StartCoroutine(MeleeStartDelay());
                }
            }

            if (Vector2.Distance(transform.position, meleeTag.position) < 1 && health == 0)
            {
                Destroy(gameObject);
            }
        }

        //if (Vector2.Distance(transform.position, lavaTag.position) < 1)
        //{
            //if (!damageDelay)
            //{
                //TakeDamage(1);
                //StartCoroutine(StartDelay());
            //}
        //}

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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
        //Debug.Log("Collision Detected");
        //if(collision.gameObject.tag == "Wall")
        //{
            //direction = collision.contacts[0].normal;
            //direction = Quaternion.AngleAxis(Random.Range(-70.0f, 70.0f), Vector3.forward) * direction;
            //transform.rotation = Quaternion.LookRotation(direction);
        //}
    //}
}