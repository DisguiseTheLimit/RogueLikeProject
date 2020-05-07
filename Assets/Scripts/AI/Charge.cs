using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    float chargeTime;

    [SerializeField]
    float chargeSpeed;

    [SerializeField]
    float chargeDuration;

    [SerializeField]
    float normalDuration;

    float defaultSpeed;

    void Awake()
    {
        defaultSpeed = enemyController.speed;
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(normalDuration);
            enemyController.speed = 0;
            yield return new WaitForSeconds(chargeTime);
            enemyController.speed = chargeSpeed;
            yield return new WaitForSeconds(chargeDuration);
            enemyController.speed = defaultSpeed;
        }
    }
}
