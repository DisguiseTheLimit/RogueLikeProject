using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int speed;
    Transform TargetPlayer;
    Transform Projectile;

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, TargetPlayer.position) > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPlayer.position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, Projectile.position) < 1)
        {
            Destroy(gameObject);
        }
    }
}