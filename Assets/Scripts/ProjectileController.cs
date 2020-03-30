using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script control how the projectiles that the player shoots work

public class ProjectileController : MonoBehaviour
{

    //public Rigidbody2D ProjectilePrefab;
    //public float ShotDelay;
    int ShootingSpeed = 10;
    public float MoveSpeed = 10.0f;
    Rigidbody2D rigidbody;
    public GameObject projectile;
    bool AlreadyExecuted = false;
    //bool ObjectDestroyed = false;
   
   
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // References and accesses the Rigidbody2D that is attached to the 'Projectile' GameObject
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("ShootHorizontal") && GetComponent<BoxCollider2D>()) // If "ShootHorizontal" is activated and the GameObject touches a BoxCollider destroy the BoxCollider 
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        if (Input.GetButton("ShootVertical") && GetComponent<BoxCollider2D>()) // If "ShootVertical" is activated and the GameObject touches a BoxCollider destroy the BoxCollider 
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        //float horizontal = Input.GetAxis("Horizontal"); // References and accesses the 'Horizontal' input in Unity's input manager
        //float vertical = Input.GetAxis("Vertical"); // References and accesses the 'Vertical' input in Unity's input manager

        //rigidbody.velocity = new Vector3(horizontal * MoveSpeed, vertical * MoveSpeed, 0); // Used to calculate what the moving velocity of the projectile will be on the X and Y axes

        float ShootHorizontal = Input.GetAxis("ShootHorizontal"); // References and accesses the 'ShootHorizontal' input in Unity's input manager
        float ShootVertical = Input.GetAxis("ShootVertical"); // References and accesses the 'ShootVertical' input in Unity's input manager

        rigidbody.velocity = new Vector3(ShootHorizontal * ShootingSpeed, ShootVertical * ShootingSpeed, 0); // Used to calculate what the shooting velocity of the projectile will be on the X and Y axes
        
        //transform.Rotate(0, -Input.GetAxis("Horizontal") * MoveSpeed, 0);

        // if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 && !AlreadyExecuted)
        //{
        //transform.Rotate(0, 0, 90);
        //AlreadyExecuted = true;
        //DestroyObjectDelayed();

        //}


        //if (projectile.transform.position.y < 0 && !AlreadyExecuted)
        //{
        //transform.Rotate(0, 0, -90);
        //AlreadyExecuted = true;
        //DestroyObjectDelayed();

        //}


        //if (projectile.transform.position.x > 0 && !AlreadyExecuted)
        //{
        //transform.Rotate(0, 0, 0);
        //AlreadyExecuted = true;
        //DestroyObjectDelayed();

        //}


        //if (projectile.transform.position.x < 0 && !AlreadyExecuted)
        //{
        //transform.Rotate(0, 0, 180);
        //AlreadyExecuted = true;
        //DestroyObjectDelayed();

        //}


        //if (ObjectDestroyed && AlreadyExecuted)
        //{
        //Instantiate(gameObject);
        //AlreadyExecuted = false;
        //}

        //if (Input.GetKeyDown("ShootHorizontal"))
        //{
        //transform.localEulerAngles = new Vector3(0, 0, 90);
        //transform.Translate(ShootingSpeed * Time.deltaTime, 0, 0);
        //DestroyObjectDelayed();
        //}

        //if (Input.GetKeyDown("ShootVertical"))
        //{ 
        //transform.localEulerAngles = new Vector3(0, 0, -90);
        //transform.Translate(ShootingSpeed * Time.deltaTime, 0, 0);
        //DestroyObjectDelayed();
        //}

        //if (Input.GetKeyDown("left"))
        //{ 
        //transform.localEulerAngles = new Vector3(0, 0, 180);
        //transform.Translate(ShootingSpeed * Time.deltaTime, 0, 0);
        //DestroyObjectDelayed();
        //}

        //if (Input.GetKeyDown("right"))
        //{
        //transform.localEulerAngles = new Vector3(0, 0, 0);
        //transform.Translate(ShootingSpeed * Time.deltaTime, 0, 0);
        //DestroyObjectDelayed();
        //} 
    }

    //void DespawnProjectile()
    //{
    //Destroy(gameObject);
    //}

    //void DespawnScriptInstance()
    //{
    //Destroy(this);
    //}

    //void DestroyComponent()
    //{
    //Destroy(GetComponent<Rigidbody2D>());
    //}

    void DestroyObjectDelayed() // Destroys the 'Projectile' GameObject 1 second after this function is executed
    {
        Destroy(gameObject, 1);
        //ObjectDestroyed = true;
        
    }
}
