using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    public GameObject heart;
    private float HeartFill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HeartFill = (float)PlayerController.Health;
        HeartFill = HeartFill / PlayerController.MaxHealth;
        heart.GetComponent<Image>().fillAmount = HeartFill;
    }
}