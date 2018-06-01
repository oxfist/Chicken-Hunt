using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchScript : MonoBehaviour {
    public GameObject bulletPrefab;

    private new Renderer renderer;
    private bool ready = true;

    private void Start() {
        renderer = GetComponent<Renderer>();
    }

    private void Update () {
        if (PlayerScript.alive && !GameManager.instance.LevelCompleted) {
            // Only shoot when sprite is visible and there is no other existing
            // bullet.
            if (renderer.isVisible && ready) {
                ready = false;
                Shoot();
            }
        }
	}

    private void Shoot() {
        if (bulletPrefab) {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = transform.position;
            bullet.GetComponent<BulletScript>().Shooter = gameObject;
        }
    }

    public void ShotOutOfBounds() {
        ready = true;
    }
}
