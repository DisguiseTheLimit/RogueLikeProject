using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    Image heart;
        
    void Start()
    {
        HealthController player = StageManager.Instance.Player.healthController;

        player.HealthChanged += PlayerHealthChanged;
    }

    void PlayerHealthChanged(HealthController player)
    {
        heart.fillAmount = (float)player.Health / player.MaxHealth;
    }
}