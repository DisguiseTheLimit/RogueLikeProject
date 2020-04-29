using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    HealthController healthController;

    bool offOn = false;

    void Start()
    {
        if (!offOn)
        {
            gameObject.SetActive(false);
        }   
    }

    void Update()
    {
        GameOverText();
    }

    public void GameOverText()
    {
        if (healthController.health == 0)
        {
            offOn = true;
        }

        if (offOn)
        {
            gameObject.SetActive(true);
            Debug.Log("Game Over");
        }
        offOn = false;
    }
}