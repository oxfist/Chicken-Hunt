using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    public GameplayConfiguration gameplayConfig;

    private PlayerScript playerScript;
    private Vector2 velocity;
    private float offsetTimer;

    private void Start() {
        playerScript = player.GetComponent<PlayerScript>();
        transform.position = new Vector3(player.transform.position.x,
                                         player.transform.position.y, -10f);
        offsetTimer = gameplayConfig.cameraOffsetTimer;
    }

    private void LateUpdate() {
        if (PlayerScript.alive) {
            if (playerScript.Crouching()) {
                // If the player crouches, lower the camera.
                offsetTimer -= Time.deltaTime;
                if (offsetTimer <= 0)
                    OffsetCamera(-2f);
            } else if (LookingUp()) {
                // If the player is standing and Up is pressed, raise the
                // camera.
                offsetTimer -= Time.deltaTime;
                if (offsetTimer <= 0)
                    OffsetCamera(2f);
            } else {
                offsetTimer = gameplayConfig.cameraOffsetTimer;
            }
            // Change position of camera with a smooth damp between the
            // current position and the player position.
            Vector2 desiredPos = Vector2.SmoothDamp(transform.position,
                                                    player.transform.position,
                                                    ref velocity,
                                                    gameplayConfig.cameraSmoothSpeed);

            // Ensure camera on the x axis doesn't go beyond limits.
            desiredPos.x = Mathf.Clamp(desiredPos.x, -51.66f, 105.77f);
            // Ensure camera on the y axis doesn't go below 0;
            desiredPos.y = Mathf.Clamp(desiredPos.y, 0f, 8.27f);
            transform.position = new Vector3(desiredPos.x, desiredPos.y,
                                                transform.position.z);
        }
    }

    private bool LookingUp() {
        return !playerScript.Crouching() && Input.GetAxisRaw("Vertical") == 1;
    }

    private void OffsetCamera(float yOffset) {
        float newX = transform.position.x;

        // If the player is crouching consider the X coordinate fixed, but
        // if the player is either moving or running, calculate the base
        // coordinate with the normal movement of the camera.
        if (yOffset < 0f) {
            newX = transform.position.x;
        } else {
            newX = Mathf.SmoothDamp(transform.position.x,
                                    player.transform.position.x,
                                    ref velocity.x,
                                    gameplayConfig.cameraSmoothSpeed);
        }

        // Smoothing the camera movement when looking up or down.
        float newY = Mathf.SmoothDamp(transform.position.y,
                                      player.transform.position.y + yOffset,
                                      ref velocity.y,
                                      gameplayConfig.cameraSmoothSpeed);

        // Ensure offset when looking up or down doesn't move the camera out
        // of bounds.
        newX = Mathf.Clamp(newX, -51.66f, 105.77f);
        newY = Mathf.Clamp(newY, 0, 8.27f);
        Vector2 desiredPos = new Vector2 {
            x = newX,
            y = newY
        };
        transform.position = new Vector3(desiredPos.x, desiredPos.y,
                                         transform.position.z);
    }
}
