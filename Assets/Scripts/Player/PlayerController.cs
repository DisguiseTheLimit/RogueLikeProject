using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int RotationSpeed;
    private Rigidbody2D rigidbody;
    private Vector2 moveVelocity;
    //Transform Enemy;
    

    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        //Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        animate = GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Player' GameObject
    }

    // Update is called once per frame
    public void Move(Vector2 moveInput)
    {
        moveVelocity = moveInput.normalized * speed;

        //rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); // Used to calculate what the velocity of the player will be on the X and Y axes

        if (Input.GetKey(KeyCode.W)) // If "W" key is pressed the Player's rigidbody rotates to the 0 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 0, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Up");
        }

        if (Input.GetKeyUp(KeyCode.W)) // If "W" key is 
        {
            //animate.ResetTrigger("Run_Up");
        }

        if (Input.GetKey(KeyCode.A)) // If "A" key is pressed the Player's rigidbody rotates to the 90 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 90, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Left");
        }

        if (Input.GetKeyUp(KeyCode.A)) // If "A" key is 
        {

        }

        if (Input.GetKey(KeyCode.S)) // If "S" key is pressed the Player's rigidbody rotates to the 180 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, 180, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Down");
        }

        if (Input.GetKeyUp(KeyCode.S)) // If "S" key is 
        {

        }

        if (Input.GetKey(KeyCode.D)) // If "D" key is pressed the Player's rigidbody rotates to the -90 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -90, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Right");
        }

        if (Input.GetKeyUp(KeyCode.D)) // If "D" key is 
        {

        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) // If "W" and "D" keys are pressed the Player's rigidbody rotates to the -45 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -45, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Top_Right");
        }

        if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.D)) // If "W" and "D" keys are 
        {

        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) // If "S" and "D" keys are pressed the Player's rigidbody rotates to the -135 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -135, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Bottom_Right");
        }

        if (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.D)) // If "S" and "D" keys are 
        {

        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) // If "A" and "S" keys are pressed the Player's rigidbody rotates to the -225 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -225, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Bottom_Left");
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.S)) // If "A" and "S" keys are 
        {

        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) // If "A" and "W" keys are pressed the Player's rigidbody rotates to the -315 coordinate in accordance with the rotation speed that is set.
        {
            //rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, -315, RotationSpeed * Time.deltaTime));
            animate.SetTrigger("Run_Top_Left");
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.W)) // If "A" and "W" keys are 
        {

        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveVelocity * Time.fixedDeltaTime);
    }
}