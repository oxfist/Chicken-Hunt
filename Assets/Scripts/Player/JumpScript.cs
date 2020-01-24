using UnityEngine;

public class JumpScript : MonoBehaviour {
    [Range(1, 10)]
    public float jumpVelocity;

    private Animator animator;
    private Rigidbody2D body;

	void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
	}

    public void Jump() {
        animator.SetBool("bGround", false);
        body.velocity = Vector2.up * jumpVelocity;
    }
}
