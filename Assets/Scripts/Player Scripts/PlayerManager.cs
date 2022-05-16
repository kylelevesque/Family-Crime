using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    PlayerStats playerStats;

    public bool isCrouched = false;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        MainManager.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        playerStats.health = Mathf.Clamp(playerStats.health, 0, 1);

        if (playerStats.health <= 0)
        {
            ManageScenes.Instance.EnterStealthLevel(MainManager.Instance.currentLevel);
        }
    }

    public void PlayerIsCrouched(int toggle)
    {
        if(toggle == 0)
        {
            isCrouched = true;
        }
        else if (toggle == 1)
        {
            isCrouched = false;
        }
    }
}
