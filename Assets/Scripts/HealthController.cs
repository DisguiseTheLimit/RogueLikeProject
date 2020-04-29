using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void PlayerEventHandler(HealthController controller);

public class HealthController : MonoBehaviour
{
    public Vector2 Position => transform.position;

    public int Health => health;
    public int MaxHealth => maximumHealth;

    public int health = 6;
    public int maximumHealth = 6;

    public float restartDelay = 1f;

    public event PlayerEventHandler HealthChanged;
    public event PlayerEventHandler Killed;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    //Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            player.SetActive(false);
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage; // The "health" int variable is minused by the "damage" int variable.

        HealthChanged?.Invoke(this);

        if (Health <= 0) // If the "Health" int variable is less than or equal to 0 execute "KillPlayer".
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Killed?.Invoke(this);
    }

    public void HealPlayer(int HealAmount)
    {
        health = Mathf.Min(MaxHealth, health + HealAmount); // 

        HealthChanged?.Invoke(this);
    }

    //void Restart()
    //{
        //SceneManager.LoadScene("JordanTest");
    //}
}
