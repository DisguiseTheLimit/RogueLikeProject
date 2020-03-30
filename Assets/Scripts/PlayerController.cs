using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script controls the movement of the player character

public class PlayerController : MonoBehaviour
{

    public int speed;
    public int RotationSpeed;
    Rigidbody2D rigidbody;
    Transform Enemy;
    public static int health = 6;
    public static int MaximumHealth = 6;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => MaximumHealth; set => MaximumHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Player' GameObject
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // References and accesses the 'Horizontal' input in Unity's input manager
        float vertical = Input.GetAxis("Vertical"); // References and accesses the 'Vertical' input in Unity's input manager

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); // Used to calculate what the velocity of the player will be on the X and Y axes

        if (Input.GetKey (KeyCode.W))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 0, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 90, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 180, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -90, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -45, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -135, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -225, RotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -315, RotationSpeed * Time.deltaTime));
        }
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void KillPlayer()
    {

    }

    public static void HealPlayer(int HealAmount)
    {
        health = Mathf.Min(MaxHealth, health + HealAmount);
    }
}