using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints;

    private readonly float speed = 2.5f;

    private int waypointIndex = 0;

    void Update()
    {
        if (GetComponent<Slime>().startWalking)
            Move();
    }

    private void Move()
    {
        transform.LookAt(waypoints[waypointIndex]);
        
        if (waypointIndex <= waypoints.Count- 1)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, waypoints[waypointIndex].transform.position,
               speed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
                waypointIndex += 1;
        }
    }
}
