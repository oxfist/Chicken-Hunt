using UnityEngine;

public class JumpScript : MonoBehaviour {
    [Range(0, 1)]
    [Tooltip("Time in seconds to reach maximum jump height")]
    public float timeToReachHeight;

    [Range(1, 5)]
    [Tooltip("Accelerator for max height jump")]
    public float fallAccelerator = 3f;

    [Range(1, 5)]
    [Tooltip("Accelerator for non-max height jump")]
    public float lowJumpAccelerator = 2f;

    private Animator animator;
    private Rigidbody2D body;

	private void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
	}

    private void Update() {
        if (Falling()) {
            body.velocity += FallingVelocity();
        } else if (InMidJump() && NotHoldingJump()) {
            body.velocity += LowFallingVelocity();
        }
    }

    private bool Falling() {
        return body.velocity.y < 0f;
    }

    private bool NotHoldingJump() {
        return !Input.GetButton("Jump");
    }

    private bool InMidJump() {
        return body.velocity.y > 0;
    }

    private Vector2 LowFallingVelocity() {
        return Vector2.up * Physics2D.gravity.y * (lowJumpAccelerator - 1) * Time.deltaTime;
    }

    private Vector2 FallingVelocity() {
        return Vector2.up * Physics2D.gravity.y * (fallAccelerator - 1) * Time.deltaTime;
    }

    public void Jump() {
        animator.SetBool("bGround", false);
        body.velocity = Vector2.up * GetJumpVelocity(timeToReachHeight, Physics.gravity.y);
    }

    private float GetJumpVelocity(float timeToReachHeight, float gravity) {
        return -(timeToReachHeight * gravity);
    }
}
