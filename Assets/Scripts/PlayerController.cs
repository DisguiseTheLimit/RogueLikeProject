using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the movement of the player character

public class PlayerController : MonoBehaviour
{

    int speed = 10;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Player' GameObject
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // References and accesses the 'Horizontal' input in Unity's input manager
        float vertical = Input.GetAxis("Vertical"); // References and accesses the 'Vertical' input in Unity's input manager

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); // Used to calculate what the velocity of the player will be on the X and Y axes
    }
}