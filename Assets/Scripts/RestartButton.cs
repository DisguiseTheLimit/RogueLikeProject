using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
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
        Restart();
    }

    public void Restart()
    {
        if (healthController.health <= 0)
        {
            offOn = true;
        }

        if (offOn)
        {
            gameObject.SetActive(true);
            Debug.Log("Restart Successful");
            //SceneManager.LoadScene("JordanTest");
        }
        offOn = false;
    }
}