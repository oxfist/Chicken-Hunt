using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {
    private bool active;

    private void Start() {
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Player"  && active) {
            active = false;
            if (ScoreManager.instance)
                ScoreManager.instance.Collectables += 1;
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
