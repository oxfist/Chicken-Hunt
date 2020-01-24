using UnityEngine;

public class JumpScript : MonoBehaviour {
    [Range(0, 1)]
    [Tooltip("Time in seconds to reach maximum jump height")]
    public float timeToReachHeight;

    private Animator animator;
    private Rigidbody2D body;

	void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
	}

    public void Jump() {
        animator.SetBool("bGround", false);
        body.velocity = Vector2.up * GetJumpVelocity(timeToReachHeight, Physics.gravity.y);
    }

    private float GetJumpVelocity(float timeToReachHeight, float gravity) {
        return -(timeToReachHeight * gravity);
    }
}
