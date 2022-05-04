using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //stats
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float waitTime = 1f;
    public float turnSpeed = 90;
    public float timeToSpotPlayer = 0.5f;
    public float attackRange = 1;

    private float speed;

    //light and awareness
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;
    Color originalSpotlightColor;
    float playerVisibleTimer;

    bool playerSpotted;

    //access to player
    public Transform pathHolder;
    Transform player;
    Vector3[] waypoints;

    //patrol variables
    int targetWaypointIndex;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        viewAngle = spotlight.pointLightOuterAngle;
        originalSpotlightColor = spotlight.color;

        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }

        //init at first position
        transform.position = waypoints[0];
        targetWaypointIndex = 1;

        playerSpotted = false;
    }


    private void Update()
    {
        if(CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            playerSpotted = true;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
            playerSpotted = false;

            speed = patrolSpeed;
            Patrol(waypoints);
        }

        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if(playerVisibleTimer >= timeToSpotPlayer)
        {
            Debug.Log("Spotted Player");
            speed = chaseSpeed;
            Chase();
            
            if(Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Attack();
            }
        }
    }

    //handles if Guard can See player - distance, angle, and linecast
    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (transform.position - player.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(-transform.up, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle/2f)
            {
                if(!Physics2D.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }



    //handles Guard movement
    void Patrol(Vector3[] waypoints)
    {
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        TurnToFace(targetWaypoint);

        if(transform.position == targetWaypoint)
        {
            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];

            TurnToFace(targetWaypoint);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

    }

    //handles guard chasing player
    void Chase()
    {
        TurnToFace(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        Debug.Log("Attacked Player");
        player.gameObject.GetComponent<PlayerStats>().health -= 0.3f;
    }

    //handles guard facing
    void TurnToFace (Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = Mathf.Atan2(-dirToLookTarget.x, dirToLookTarget.y) * Mathf.Rad2Deg;

        while (Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle) > 0.05f || Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle) < -0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,0,1) * angle;
        }
    }

    //gizmos for visualization
    private void OnDrawGizmos()
    {
        Vector2 startPosition = pathHolder.GetChild(0).position;
        Vector2 previousPosition = startPosition;

        foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.2f);
            Gizmos.DrawLine(previousPosition, waypoint.position);

            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

    }
}
