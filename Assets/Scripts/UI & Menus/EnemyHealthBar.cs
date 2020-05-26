using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    Vector3 localScale;

    [SerializeField]
    EnemyAITest enemyAITest;

    [SerializeField]
    float healthLength = 3f;

    float healthScale = 0f;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        healthScale = healthLength / enemyAITest.health;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = healthScale * enemyAITest.health;
        transform.localScale = localScale;
    }
}
