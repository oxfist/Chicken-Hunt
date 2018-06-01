using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance = null;

    public GameplayConfiguration gameplayConfig;
    public int Collectables { get; set; }
    public int HP { get; set; }

    private bool bonusGiven = false;

    private void Awake() {
        if (!instance) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Collectables = 0;
        HP = gameplayConfig.initialHP;
    }

    private void Update() {
        // Give 1 HP each time the player collects 3 collectables.
        if (Collectables != 0 && Collectables % 3 == 0 && !bonusGiven) {
            HP += 1;
            bonusGiven = true;
        }
        if (Collectables % 3 == 1) {
            bonusGiven = false;
        }
    }

    public void ResetScore() {
        Collectables = 0;
        HP = gameplayConfig.initialHP;
    }
}
