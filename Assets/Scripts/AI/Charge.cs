using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    //[SerializeField]
    //EnemyController enemyController;

    [SerializeField]
    EnemyAI enemyAI;

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
        //defaultSpeed = enemyController.speed;
        defaultSpeed = enemyAI.speed;
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(normalDuration);
            enemyAI.speed = 0;
            //enemyController.speed = 0;
            yield return new WaitForSeconds(chargeTime);
            enemyAI.speed = chargeSpeed;
            //enemyController.speed = chargeSpeed;
            yield return new WaitForSeconds(chargeDuration);
            enemyAI.speed = defaultSpeed;
            //enemyController.speed = defaultSpeed;
        }
    }
}
