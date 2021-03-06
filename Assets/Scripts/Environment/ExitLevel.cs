using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainManager.Instance.currentLevel++;
                ManageScenes.Instance.ExitStealthLevel(MainManager.Instance.currentLevel);
            }
        }
    }
}
