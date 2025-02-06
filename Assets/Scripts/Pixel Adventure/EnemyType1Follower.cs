using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Follower : MonoBehaviour {
    private Animator anim;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    private float speed = 1f;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        anim.SetBool("walk", true);
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, this.transform.position) < 1f) {
            currentWaypointIndex++;
            transform.localScale = new Vector3(1, 1, 1);
            if (currentWaypointIndex >= waypoints.Length) {
                currentWaypointIndex = 0;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        } 
        transform.position = Vector2.MoveTowards(transform.position, new Vector2 (waypoints[currentWaypointIndex].transform.position.x, transform.position.y), Time.deltaTime * speed);
    }
}
