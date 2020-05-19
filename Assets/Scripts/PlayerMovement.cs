using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    public Rigidbody2D rb;

    
    public Vector2 movement;

    private Quaternion currentRotation;


    void Update()
    {
        Debug.Log(" Movement script Update");
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Debug.Log(" Movement script FixedUpdate");

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}

