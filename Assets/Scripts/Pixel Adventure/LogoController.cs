using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour {
    void Start() {
        Invoke("loadMainMenu",3f);
    }

    void loadMainMenu() {
        SceneManager.LoadScene(1);
    }
}
