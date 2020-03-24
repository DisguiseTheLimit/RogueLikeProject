using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script control how the projectiles that the player shoots work

public class ProjectileController : MonoBehaviour
{

    //public Rigidbody2D ProjectilePrefab;
    //public float ShotDelay;
    int ShootingSpeed = 100;
    int MoveSpeed = 10;
    Rigidbody2D rigidbody;
   
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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * MoveSpeed, vertical * MoveSpeed, 0);

        float ShootHorizontal = Input.GetAxis("ShootHorizontal");
        float ShootVertical = Input.GetAxis("ShootVertical");

        rigidbody.velocity = new Vector3(ShootHorizontal * ShootingSpeed, ShootVertical * ShootingSpeed, 0);

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
        
    }
}
