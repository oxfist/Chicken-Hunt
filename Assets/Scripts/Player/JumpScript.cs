using UnityEngine;

public class JumpScript : MonoBehaviour {
    [Tooltip("Maximum jump height in arbitrary unit")]
    public float maxJumpHeight;

    [Range(0, 1)]
    [Tooltip("Time in seconds to reach maximum jump height")]
    public float timeToReachHeight;

    private Animator animator;
    private Rigidbody2D body;
    private float jumpingGravity;

	private void Start() {
        jumpingGravity = GetJumpingGravity(maxJumpHeight, timeToReachHeight);
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
        return Vector2.up * (jumpingGravity + 1) * Time.deltaTime;
    }

    private Vector2 FallingVelocity() {
        return Vector2.up * jumpingGravity * Time.deltaTime;
    }

    public void Jump() {
        animator.SetBool("bGround", false);
        body.velocity = Vector2.up * GetJumpVelocity(maxJumpHeight, timeToReachHeight);
    }

    private float GetJumpingGravity(float maxJumpHeight, float timeToReachHeight) {
        return (-2 * maxJumpHeight) / Mathf.Pow(timeToReachHeight, 2);
    }

    private float GetJumpVelocity(float maxJumpHeight, float timeToReachHeight) {
        return (2 * maxJumpHeight) / timeToReachHeight;
    }
}
