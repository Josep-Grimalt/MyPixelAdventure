using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        if(player.position.y < -6.75f) return;

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
