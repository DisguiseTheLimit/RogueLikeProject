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
    public HealthController healthController;
    public Transform melee;

    //Transform Enemy;


    Animator animate;
    string currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        animate = GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Player' GameObject
    }

    //Animation method
    void Animate(string animation)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            if (animate != null)
            {
                animate.Play(animation);
            }  
        }
    }

    // Update is called once per frame
    public void Move(Vector2 moveInput)
    {
        moveVelocity = moveInput.normalized * speed;

        if (moveInput == Vector2.zero)
        {
            Animate("Idle_Animation");
            return;
        }

        if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
        {
            if (moveInput.y > 0)
            {
                Animate("Run_Up");
            }
            else
            {
                Animate("Run_Down");
            }
        }
        else
        {
            if (moveInput.x > 0)
            {
                Animate("Run_Right");
            }
            else
            {
                Animate("Run_Left");
            }

        }

    }
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveVelocity * Time.fixedDeltaTime);
    }
}