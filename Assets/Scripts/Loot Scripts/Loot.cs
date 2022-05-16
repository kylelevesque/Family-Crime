using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    GameObject player;

    [SerializeField] private int lootWorth;
    [SerializeField] private AudioClip soundEffect;

    bool timeToDestroySelf = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (timeToDestroySelf)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerStats>().moneyInWallet += lootWorth;
            MainManager.Instance.playerWallet += lootWorth;
            AudioManager.instance.PlaySound(soundEffect, transform.position);

            timeToDestroySelf = true;
        }
    }
}
