using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script controls the movement of the player character

public delegate void PlayerEventHandler(PlayerController controller);

public class PlayerController : MonoBehaviour
{
    public int Health => health; 
    public int MaxHealth => maximumHealth;

    public Vector2 Position => transform.position;

    public int speed;
    public int RotationSpeed;
    Rigidbody2D rigidbody;
    //Transform Enemy;
    public int health = 6;
    public int maximumHealth = 6;

    public event PlayerEventHandler HealthChanged;
    public event PlayerEventHandler Killed;

    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        //Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        animate = GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Player' GameObject
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // References and accesses the 'Horizontal' input in Unity's input manager
        float vertical = Input.GetAxis("Vertical"); // References and accesses the 'Vertical' input in Unity's input manager

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); // Used to calculate what the velocity of the player will be on the X and Y axes

        if (Input.GetKey (KeyCode.W)) // If "W" key is pressed the Player's rigidbody rotates to the 0 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 0, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Up");
        }

        if (Input.GetKey(KeyCode.A)) // If "A" key is pressed the Player's rigidbody rotates to the 90 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 90, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Left");
        }

        if (Input.GetKey(KeyCode.S)) // If "S" key is pressed the Player's rigidbody rotates to the 180 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 180, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Down");
        }

        if (Input.GetKey(KeyCode.D)) // If "D" key is pressed the Player's rigidbody rotates to the -90 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -90, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Right");
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) // If "W" and "D" keys are pressed the Player's rigidbody rotates to the -45 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -45, RotationSpeed * Time.deltaTime));
            //animate.SetTrigger("Run_Top_Right");
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) // If "S" and "D" keys are pressed the Player's rigidbody rotates to the -135 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -135, RotationSpeed * Time.deltaTime));
            //animate.SetTrigger("Run_Bottom_Right");
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) // If "A" and "S" keys are pressed the Player's rigidbody rotates to the -225 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -225, RotationSpeed * Time.deltaTime));
            //animate.SetTrigger("Run_Bottom_Left");
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) // If "A" and "W" keys are pressed the Player's rigidbody rotates to the -315 coordinate in accordance with the rotation speed that is set.
        {
            rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -315, RotationSpeed * Time.deltaTime));
            //animate.SetTrigger("Run_Top_Left");
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage; // The "health" int variable is minused by the "damage" int variable.

        HealthChanged?.Invoke(this);

        if (Health <= 0) // If the "Health" int variable is less than or equal to 0 execute "KillPlayer".
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Killed?.Invoke(this);
    }

    public void HealPlayer(int HealAmount)
    {
        health = Mathf.Min(MaxHealth, health + HealAmount); // 

        HealthChanged?.Invoke(this);
    }
}