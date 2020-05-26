using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int currentWeapon;

    public Transform[] weapons;

    public static bool reloadDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        
        changeWeapon(1);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !reloadDelay)
        {
            changeWeapon(0);
        }

        if (Input.GetKey("r") && ProjectileShooter.ammoCount < 1000)
        {
            StartCoroutine(ReloadDelayTime());
        }
    }

    public void changeWeapon(int num)
    {
        currentWeapon = num;
        for (int i  = 0; i < weapons.Length; i++)
        {
            if (i == num)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator ReloadDelayTime()
    {
        reloadDelay = true;
        yield return new WaitForSeconds(4);
        reloadDelay = false;
    }
}
