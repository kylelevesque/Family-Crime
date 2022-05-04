using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBackup : MonoBehaviour
{
    public float speed = 5f;
    public float waitTime = 0.3f;
    public float turnSpeed = 90;
    public float timeToSpotPlayer = 0.5f;

    public UnityEngine.Rendering.Universal.Light2D spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;
    Color originalSpotlightColor;
    float playerVisibleTimer;

    public Transform pathHolder;
    Transform player;
    Vector3[] waypoints;

    public float attackRange = 1;

    bool playerSpotted;

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
        StartCoroutine(FollowPath(waypoints));
    }


    private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            playerSpotted = true;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
            playerSpotted = false;

        }

        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            Debug.Log("Spotted PLayer");
        }
    }

    //handles if Guard can See player - distance, angle, and linecast
    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (transform.position - player.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(-transform.up, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics2D.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //handles Guard movement
    IEnumerator FollowPath(Vector3[] waypoints)
    {
        //this section and while loop handle patrolling
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        while (!playerSpotted)
        {


            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];

                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }

        while (playerSpotted)
        {
            yield return StartCoroutine(TurnToFace(player.position));

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                transform.position = transform.position;
            }
            yield return null;
        }

    }

    //handles guard facing
    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = Mathf.Atan2(-dirToLookTarget.x, dirToLookTarget.y) * Mathf.Rad2Deg;

        while (Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle) > 0.05f || Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle) < -0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, 1) * angle;

            yield return null;
        }
    }

    //gizmos for visualization
    private void OnDrawGizmos()
    {
        Vector2 startPosition = pathHolder.GetChild(0).position;
        Vector2 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
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
