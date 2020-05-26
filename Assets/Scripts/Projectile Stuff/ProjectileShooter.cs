using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private float fireRate;

    [SerializeField]
    private ProjectileMovement projectile;

    [SerializeField]
    Collider2D collider;

    private float spawnTime;
    public static float ammoCount = 1000;

    public Text ammoText;

    public AudioSource gunShot;
    public AudioSource reloadSound;

    public static bool reloadDelay = false;

    public void Update()
    {
        
        if (ammoCount < 1000)
        {
            if (Input.GetKey("r"))
            {
                Debug.Log("Reload Success");
                reloadSound.Play();
                StartCoroutine(ReloadDelayTime());
                ammoCount = 1000;
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = ammoCount.ToString();
        }
    }

    public IEnumerator ReloadDelayTime()
    {
        reloadDelay = true;
        yield return new WaitForSeconds(4);
        reloadDelay = false;
    }

    public void AmmoCount()
    {
        //Debug.Log("Ammo Count Change Successful");
        ammoCount -= 1;
    }

    //public void Reload()
    //{
            //if (Input.GetKey("r"))
            //{
                //Debug.Log("Reload Success");
                //ammoText.GetComponent<Text>().text = ammoCount.ToString();
            //}
    //}

    void Spawn(Vector3 positionSpawn, Quaternion rotateSpawn)
    {
        ProjectileMovement spawned = Instantiate(projectile, positionSpawn, rotateSpawn);

        if (collider != null)
        {
            spawned.IgnoreCollision(collider);
        }
    }

    public bool TryShoot(Vector2 target)
    {
        if(Time.time >= spawnTime)
        {
            gunShot.Play();
            Spawn(transform.position, Quaternion.FromToRotation(Vector3.up, target - (Vector2)transform.position));
            spawnTime = Time.time + fireRate;
            return true;
        }
        return false;
    }
}




