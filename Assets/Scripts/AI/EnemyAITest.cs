using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITest : MonoBehaviour
{

    private Animator myAnim;

    private Transform target;
    public Transform homePos;
    Transform meleeTag;

    HealthController targetPlayer;

    public int damage;
    public int health;
    public int maxHealth;

    public float delay;
    public float damageDelayTime;
    public float meleeDelayTime;

    bool attackDelay = false;
    bool meleeDelay = false;
    bool damageDelay = false;
    bool weaponSwitching = false;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float minRange;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        targetPlayer = StageManager.Instance.Player;
        meleeTag = GameObject.FindWithTag("Melee").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome();
        }

        if (Vector2.Distance(transform.position, targetPlayer.Position) <= 1.1) // If the distance between the enemy and player is less than 1 and if the bool "AttackDelay" is false
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

        if (health == 0)
        {
            Destroy(gameObject);
        }
    
}

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
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