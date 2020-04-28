using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10.0f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    Collider2D collider;

    public int damage = 1;

    void Start()
    {
        rb.velocity = transform.up * projectileSpeed;
    }

    public void IgnoreCollision(Collider2D other)
    {
        Physics2D.IgnoreCollision(collider, other);
    }

    //void OnCollisionEnter(Collision other)
    //{
    //if (other.gameObject.tag == "Wall")
    //{
    //Destroy(gameObject);
    //}
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //WallController wallcontroller = collision.gameObject.GetComponent<WallController>();

        //if (wallcontroller != null)
        //{
            //wallcontroller.TakeDamage(damage);
        //}

        //Destroy(gameObject);
    //}
}
