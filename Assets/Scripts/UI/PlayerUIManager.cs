using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUIManager : MonoBehaviour
{
    public TMP_Text playerHealth;
    public TMP_Text playerWallet;

    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        playerHealth.text = "Health: " + playerStats.health;
        playerWallet.text = "Wallet: " + playerStats.moneyInWallet;
    }
}
