using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderScript : MonoBehaviour {
    public GameObject gameManager;
    public GameObject scoreManager;

    private void Awake() {
        if (!GameManager.instance) {
            Instantiate(gameManager);
            Instantiate(scoreManager);
        }
    }
}
