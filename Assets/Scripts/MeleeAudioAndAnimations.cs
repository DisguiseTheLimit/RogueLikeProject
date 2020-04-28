using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAudioAndAnimations : MonoBehaviour
{

    public AudioSource meleeSlash;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            meleeSlash.Play();
        }
    }
}
