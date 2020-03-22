using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    //public Rigidbody2D ProjectilePrefab;
    //public float AttackSpeed = 0.5f;
    //public float ShotDelay;
    //public float ProjectileSpeed = 500;
    int speed = 10;
    bool objectDestroyed = false;
    //public Transform Player;
    
    
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetButton("ShootHorizontal") && GetComponent<BoxCollider2D>())
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        if (Input.GetButton("ShootVertical") && GetComponent<BoxCollider2D>())
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        if (Input.GetKey("w"))
        {
            
                transform.Translate(0, speed * Time.deltaTime, 0);
            
            
        }

        if (Input.GetKey("s"))
        {
           
                transform.Translate(0, -speed * Time.deltaTime, 0);
            
        }

        if (Input.GetKey("a"))
        {
           
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            
        }

        if (Input.GetKey("d"))
        {
            
                transform.Translate(speed * Time.deltaTime, 0, 0);
            
        }

        if (Input.GetKey("up")) 
        {

            //transform.position = Player.position;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            DestroyObjectDelayed();
            objectDestroyed = true;



        }

        if (Input.GetKey("down"))
        {

            //transform.position = Player.position;
            transform.localEulerAngles = new Vector3(0, 0, -90);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            DestroyObjectDelayed();
            objectDestroyed = true;


        }

        if (Input.GetKey("left"))
        {

            //transform.position = Player.position;
            transform.localEulerAngles = new Vector3(0, 0, 180);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            DestroyObjectDelayed();
            objectDestroyed = true;


        }

        if (Input.GetKey("right"))
        {


            //transform.position = Player.position;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            DestroyObjectDelayed();
            objectDestroyed = true;
            
            
        }

        //if (objectDestroyed == true)
        //{
            //for (int i = 0; i < 1; i++)
            //{
                //Instantiate(gameObject);
            //}
            //objectDestroyed = false;
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

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, 1);
        objectDestroyed = true;
    }

    
}
