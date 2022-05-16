using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FamilyUIManager : MonoBehaviour
{

    [SerializeField] GameObject secondMenu;
    [SerializeField] GameObject firstMenu;
    [SerializeField] TMP_Text playerCash;

    private void Start()
    {
        secondMenu.SetActive(false);  
    }

    private void Update()
    {
        playerCash.text = "Player Cash: " + MainManager.Instance.playerWallet;
    }
    public void ToGame()
    {
        MainManager.Instance.currentLevel++;
        ManageScenes.Instance.EnterStealthLevel(MainManager.Instance.currentLevel);
    }

    public void ActivateSecondMenu()
    {
        secondMenu.SetActive(true);
    }
}
