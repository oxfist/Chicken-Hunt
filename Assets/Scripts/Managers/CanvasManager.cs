using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public GameObject levelCompleteCanvas;

    private GameObject scoreHPCanvas;
    private Text scoreText;
    private Text hpText;

    private void Start() {
        scoreHPCanvas = GameObject.Find("ScoreHPCanvas");
        hpText = GameObject.Find("HPText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    private void Update() {
        if (!GameManager.instance.LevelCompleted) {
            hpText.text = ScoreManager.instance.HP.ToString();
            scoreText.text = ScoreManager.instance.Collectables.ToString();
        } else {
            // Deactivate score and HP HUD when the level is completed.
            scoreHPCanvas.SetActive(false);
            levelCompleteCanvas.SetActive(true);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void Retry() {
        // If the Retry option has been selected, wait 5 frames then trigger
        // retry on the Game Manager.
        StartCoroutine(RetryDelay(5));
    }

    private IEnumerator RetryDelay(int frames) {
        while (frames > 0) {
            frames -= 1;
            yield return null;
        }
        GameManager.instance.Retry();
    }
}
