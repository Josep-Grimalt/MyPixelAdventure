using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEnemyType1Follower : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    private float speed = 1f;

    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 1f)
        {
            currentWaypointIndex++;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints[currentWaypointIndex].transform.position.x, transform.position.y), Time.deltaTime * speed);
    }
}
