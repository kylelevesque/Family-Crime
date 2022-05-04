using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        playerStats.health = Mathf.Clamp(playerStats.health, 0, 1);

        if (playerStats.health <= 0)
        {
            Debug.Log("Player Died");
        }
    }
}
