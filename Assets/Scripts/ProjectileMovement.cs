using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10.0f;

    [SerializeField]
    private Rigidbody2D rb;
    
    void Start()
    {
        rb.velocity = transform.up * projectileSpeed;
    }
}
