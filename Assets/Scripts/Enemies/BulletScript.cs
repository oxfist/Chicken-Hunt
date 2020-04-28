using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public GameplayConfiguration gameplayConfig;
    public GameObject Shooter { get; set; }
    public Color TrajectoryColor { get; set; }

    private GameObject player;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private WitchScript witchScript;

    private void Start() {
        player = GameObject.Find("Player");
        initialPosition = transform.position;
        targetPosition = player.transform.position;
        // Checking for the existence of the shooter is necessary when
        // inside a not-sandbox scene.
        if (Shooter)
            witchScript = Shooter.GetComponent<WitchScript>();
	}
	
	private void Update() {
        // Move towards position of target saved at spawn.
        transform.position += (targetPosition - initialPosition).normalized * gameplayConfig.bulletSpeed * Time.deltaTime;
	}

    private void OnBecameInvisible() {
        if (Shooter) {
            // Tell the shooter she can shoot again.
            witchScript.ShotOutOfBounds();
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = TrajectoryColor;
        Gizmos.DrawLine(transform.position, targetPosition);
    }
}
