using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField]
    private float firerate;

    [SerializeField]
    private GameObject prefabSpawn;

    [SerializeField]
    private Vector3 currentPosition;

    private Quaternion currentRotation;

    private float spawnTime;
    void Start()
    {
        spawnTime = firerate;
        currentPosition = transform.position;
        currentRotation = transform.rotation;
    }

    void Update()
    {
        currentPosition = transform.position;
        currentRotation = transform.rotation;

        if (Input.GetMouseButton(0) && Time.time >= firerate)
        {
            Spawn(currentPosition, currentRotation);
            firerate = Time.time + spawnTime;
        }
 
    }

    void Spawn(Vector3 positionSpawn, Quaternion rotateSpawn)
    {
        
        Instantiate(prefabSpawn, positionSpawn, rotateSpawn);
    }

}




