using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAI : MonoBehaviour
{

    Rigidbody2D rigidbody;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float startTimeBetweenShots;
    public float meleeDelayTime;
    public float delay;
    public float damageDelayTime;
    private float timeBetweenShots;
    
    public int health;
    public int maxHealth;

    public GameObject projectile;
    private Transform player;
    Transform meleeTag;
    Transform lavaTag;

    bool attackDelay = false;
    bool meleeDelay = false;
    bool damageDelay = false;
    bool weaponSwitching = false;

    // Start is called before the first frame update
    void Start()
    {
        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance) {

            transform.position = this.transform.position;

        } else if (Vector2.Distance(transform.position, player.position) < retreatDistance) {

            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

        }

        if (timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
            //Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 90));
            //Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, -90));
            //Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, -180));
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
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