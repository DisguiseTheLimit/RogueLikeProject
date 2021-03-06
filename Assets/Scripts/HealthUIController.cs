﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    Image heart;
        
    void Start()
    {
        PlayerController player = StageManager.Instance.Player;

        player.HealthChanged += PlayerHealthChanged;
    }

    void PlayerHealthChanged(PlayerController player)
    {
        heart.fillAmount = (float)player.Health / player.MaxHealth;
    }
}