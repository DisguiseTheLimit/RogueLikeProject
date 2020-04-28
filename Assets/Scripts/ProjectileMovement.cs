﻿using System.Collections;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(1);
        }
    }
}
