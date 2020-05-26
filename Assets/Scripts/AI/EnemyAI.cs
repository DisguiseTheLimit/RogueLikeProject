using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform[] patrolPoints;
    public Transform target;

    Transform currentPatrolPoint;
    Transform meleeTag;
    Transform lavaTag;

    PlayerController targetPlayer;

    Rigidbody2D rigidbody;

    public float speed;
    public float chaseRange;
    public float delay;
    public float damageDelayTime;
    public float meleeDelayTime;

    public int damage;
    public int health;
    public int maxHealth;

    int currentPatrolIndex;

    bool attackDelay = false;
    bool meleeDelay = false;
    bool damageDelay = false;
    bool weaponSwitching = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];

        targetPlayer = StageManager.Instance.Player;

        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if we have reached the patrol point
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            //We have reached the patrol point - get the next one
            //Check to see if we have anymore patrol points - if not go back to the beginning
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        //Turn to face the current patrol point
        //Finding the direction Vector that points to the patrolpoint
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
        transform.Translate(patrolPointDir.normalized * Time.deltaTime * speed);
        //Figure out the rotation in degrees that we need to turn towards
        //float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 0f;
        //Made the rotation that we need to face
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //Apply the rotation to our transform 
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        //Chasing Player AI
        //Get the distance to the target and check to see if it is close enough to chase
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < chaseRange)
        {
            //Start chasing the target - turn and move towards the target
            Vector3 targetDir = target.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 0f;
            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        }

        if (Vector2.Distance(transform.position, targetPlayer.healthController.Position) < 1) // If the distance between the enemy and player is less than 1 and if the bool "AttackDelay" is false
                                                                             // execute the "DamagePlayer" function from the PlayerController script passing 1 as the damage value
                                                                             // then after this execute the function "StartDelay"
        {
            if (!attackDelay)
            {
                targetPlayer.healthController.DamagePlayer(damage);
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