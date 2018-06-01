using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteCanvasScript : MonoBehaviour {
    public GameplayConfiguration gameplayConfig;

    private Text messageText;

	private void Start() {
        messageText = GetComponentInChildren<Text>();
        messageText.text = GetMessageFromScore();
	}

    private string GetMessageFromScore() {
        if (ScoreManager.instance.Collectables == 0)
            return gameplayConfig.levelCompleteMessages[0];
        else if (ScoreManager.instance.Collectables < 4)
            return gameplayConfig.levelCompleteMessages[1];
        else if (ScoreManager.instance.Collectables == 4)
            return gameplayConfig.levelCompleteMessages[2];
        else if (ScoreManager.instance.Collectables < 8)
            return gameplayConfig.levelCompleteMessages[3];
        else
            return gameplayConfig.levelCompleteMessages[4];
    }
}
