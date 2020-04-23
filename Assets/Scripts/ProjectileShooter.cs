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

    void Spawn(Vector3 positionSpawn, Quaternion rotateSpawn)
    {
        ProjectileMovement spawned = Instantiate(projectile, positionSpawn, rotateSpawn);

        if (collider != null)
        {
            spawned.IgnoreCollision(collider);
        }
    }

    public bool TryShoot()
    {
        if(Time.time >= spawnTime)
        {
            Spawn(transform.position, transform.rotation);
            spawnTime = Time.time + fireRate;
            return true;
        }

        return false;
    }
}




