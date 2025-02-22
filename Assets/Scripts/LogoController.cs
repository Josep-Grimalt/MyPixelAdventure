using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour {
    void Start() {
        Invoke(nameof(LoadMainMenu), 3f);
    }

    void LoadMainMenu() {
        SceneManager.LoadScene(1);
    }
}
