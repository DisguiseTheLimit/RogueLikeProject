using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAudioAndAnimations : MonoBehaviour
{

    public AudioSource meleeSlash;

    bool weaponSwitching = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponSwitching = true;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponSwitching = false;
        }

        if (Input.GetMouseButtonDown(0) && weaponSwitching)
        {
            meleeSlash.Play();
        }
    }
}
