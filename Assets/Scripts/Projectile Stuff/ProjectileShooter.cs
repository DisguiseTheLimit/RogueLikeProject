using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private float fireRate;

    [SerializeField]
    private ProjectileMovement projectile;

    [SerializeField]
    Collider2D collider;

    private float spawnTime;

    public static float ammoCount = 1000;

    public Transform ammoText;

    public AudioSource gunShot;

    public void Update()
    {
        ammoText.GetComponent<Text>().text = ammoCount.ToString();
        if (ammoCount <= 0)
        {
            if (Input.GetKey("r"))
            {
                Debug.Log("Reload Success");
                ammoCount = 1000;
                ammoText.GetComponent<Text>().text = ammoCount.ToString();
            }
        }
    }

    public void AmmoCount()
    {
        //Debug.Log("Ammo Count Change Successful");
        ammoCount -= 1;
    }

    //public void Reload()
    //{
            //if (Input.GetKey("r"))
            //{
                //Debug.Log("Reload Success");
                //ammoText.GetComponent<Text>().text = ammoCount.ToString();
            //}
    //}

    void Spawn(Vector3 positionSpawn, Quaternion rotateSpawn)
    {
        ProjectileMovement spawned = Instantiate(projectile, positionSpawn, rotateSpawn);

        if (collider != null)
        {
            spawned.IgnoreCollision(collider);
        }
    }

    public bool TryShoot(Vector2 target)
    {
        if(Time.time >= spawnTime)
        {
            gunShot.Play();
            Spawn(transform.position, Quaternion.FromToRotation(Vector3.up, target - (Vector2)transform.position));
            spawnTime = Time.time + fireRate;
            return true;
        }
        return false;
    }
}




