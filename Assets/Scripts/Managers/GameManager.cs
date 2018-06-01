using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool LevelCompleted { get; set; }

    public static GameManager instance = null;

    private void Awake() {
        if (!instance) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Retry() {
        LevelCompleted = false;
        ScoreManager.instance.ResetScore();
        PlayerScript.alive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
