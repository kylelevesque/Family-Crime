using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackTime;
    float timeToAttack;
    public float attackRadius;

    public Transform attackLocation;
    public LayerMask enemies;



    private void Start()
    {
        timeToAttack = attackTime;
    }
    private void Update()
    {
        timeToAttack -= Time.deltaTime;

        if(timeToAttack <= 0)
        {
            if(Input.GetButton("Fire1"))
            {
                
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRadius, enemies);

                if(damage.Length == 1)
                {
                    Destroy(damage[0].gameObject);
                    MainManager.Instance.playerKills++;
                }
            }
            timeToAttack = attackTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRadius);
    }

}
