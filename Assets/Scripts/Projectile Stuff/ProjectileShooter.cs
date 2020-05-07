using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private float fireRate;

    [SerializeField]
    private ProjectileMovement projectile;

    [SerializeField]
    Collider2D collider;

    private float spawnTime;

    public AudioSource gunShot;

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




