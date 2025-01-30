using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMainMenu : MonoBehaviour {
    public float parallaxEffect;
    private bool changeDir = false;
    private float range = 10f;

    void Update() {
        if (transform.position.x > range || transform.position.x < -range) {
            changeDir = !changeDir;
        }

        if (changeDir)
            transform.position += new Vector3(parallaxEffect * Time.fixedDeltaTime , transform.position.y, transform.position.z);
        else 
            transform.position -= new Vector3(parallaxEffect * Time.fixedDeltaTime , transform.position.y, transform.position.z);
    }
}
