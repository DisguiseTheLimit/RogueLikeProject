using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("s"))
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
