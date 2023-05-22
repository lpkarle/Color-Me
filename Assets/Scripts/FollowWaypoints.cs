using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints;

    float speed = 2.5f;

    bool startWalking = true;

    private int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Slime>().startWalking)
            Move();
    }

    private void Move()
    {
        this.transform.LookAt(waypoints[waypointIndex]);
        
        if (waypointIndex <= waypoints.Count- 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position,
               speed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
                waypointIndex += 1;
        }
    }
}
