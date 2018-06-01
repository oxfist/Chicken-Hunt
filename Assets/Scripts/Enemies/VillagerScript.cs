using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour {
    public GameplayConfiguration gameplayConfig;

    private Animator anim;
    private bool facingRight;

	private void Start() {
        anim = GetComponent<Animator>();
        facingRight = GetComponent<EnemyScript>().facingRight;
	}

    // True if the sprite just changed facing direction, false otherwise.
    private bool FacingSameDirection() {
        return facingRight == GetComponent<EnemyScript>().facingRight;
    }

    private void Update() {
        if (PlayerScript.alive && !GameManager.instance.LevelCompleted) {
            // If the enemy just changed facing direction, go to "tired" state.
            if (!FacingSameDirection()) {
                facingRight = GetComponent<EnemyScript>().facingRight;
                anim.SetBool("bTired", true);
            }
            // If the enemy is not tired and the sprite is visible, walk
            // towards the player.
            if (!anim.GetBool("bTired") && GetComponent<Renderer>().isVisible) {
                transform.Translate((facingRight ? 1 : -1) * Vector2.right * gameplayConfig.enemySpeed * Time.deltaTime);
            }
        } else {
            anim.SetBool("bTired", true);
        }
    }

    private void OnBecameInvisible() {
        anim.enabled = false;
        anim.SetBool("bTired", true);
    }

    private void OnBecameVisible() {
        anim.enabled = true;
    }
}