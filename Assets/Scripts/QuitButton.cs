using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
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
        Quit();
    }

    public void Quit()
    {
        if (healthController.health == 0)
        {
            offOn = true;
        }

        if (offOn)
        {
            gameObject.SetActive(true);
            Debug.Log("Quit Successful");
            //Application.Quit(); 
        }
        offOn = false;
    }
}