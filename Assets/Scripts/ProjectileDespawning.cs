using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawning : MonoBehaviour
{

    Transform projectileTagWall;
    Transform projectileTagDoor;

    void Start()
    {
        projectileTagWall = GameObject.FindWithTag("Wall").GetComponent<Transform>();
        projectileTagDoor = GameObject.FindWithTag("Door").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, projectileTagWall.position) < 1)
        {
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, projectileTagDoor.position) < 1)
        {
            Destroy(gameObject);
        }
    }
}
