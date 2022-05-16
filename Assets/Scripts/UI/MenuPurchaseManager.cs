using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPurchaseManager : MonoBehaviour
{
    public void PurchaseUpgrade()
    {
        if (CheckWealth())
        {
            Debug.Log("Upgrade Purchased");
            MainManager.Instance.playerWallet -= 100;
        }
        else if (!CheckWealth())
        {
            Debug.Log("No Money");
        }
    }

    public void PurchaseAilment()
    {
        if (CheckWealth())
        {
            Debug.Log("Ailment Purchased");
            MainManager.Instance.playerWallet -= 100;
        }
        else if (!CheckWealth())
        {
            Debug.Log("No Money");
        }
    }

    bool CheckWealth()
    {
        if(MainManager.Instance.playerWallet >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
