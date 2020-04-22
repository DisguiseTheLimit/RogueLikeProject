using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    public int health = 99999;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
