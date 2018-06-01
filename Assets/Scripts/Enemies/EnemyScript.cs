using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public static int Damage = 1;
    [HideInInspector] public bool facingRight = true;

    private GameObject player;

	private void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        if (player)
            FacePlayer();
    }

    private void FacePlayer() {
        bool playerIsLeft = player.GetComponent<Transform>().position.x < transform.position.x;

        if (playerIsLeft && facingRight)
            Flip();
        if (!playerIsLeft && !facingRight)
            Flip();
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
