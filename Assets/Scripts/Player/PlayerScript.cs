using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public GameObject gameOverCanvas;
    public GameObject scoreHPCanvas;
    public GameplayConfiguration gameplayConfig;
    public LayerMask whatIsGround;
    public Transform groundCheck;


    public static bool alive = true;

    private JumpScript jumpScript;
    private Animator animator;
    private Rigidbody2D body;
    private bool grounded = false;
    private bool facingRight = true;
    private bool invulnerable = false;
    private readonly float groundRadius = 0.1f;

    public bool Crouching() {
        return animator.GetBool("bCrouching");
    }

    private void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        jumpScript = GetComponent<JumpScript>();
	}

	private void Update() {
        if (alive && !GameManager.instance.LevelCompleted) {
            if (ScoreManager.instance.HP <= 0 || transform.position.y < -10) {
                Die();
            }

            if (grounded && Input.GetAxisRaw("Vertical") < 0f) {
                Crouch();
            }

            if (Crouching() && Input.GetAxisRaw("Vertical") > -1f) {
                StandUp();
            }

            // If player goes out of screen at the right side of the level,
            // the level is completed.
            if (transform.position.x >= 118) {
                GameManager.instance.LevelCompleted = true;
            }

            grounded = Physics2D.OverlapCircle(groundCheck.position,
                                               groundRadius, whatIsGround);
            animator.SetBool("bGround", grounded);
            animator.SetFloat("fVSpeed", body.velocity.y);

            if (JumpOnGroundNotCrouching()) {
                Jump();
            }

            if (NotMoving()) {
                StopRunningAnimation();
            }

            if (RunningNotCrouching()) {
                StartRunningAnimation();
            }

            // Set velocity to zero when crouching so the player doesn't slide.
            if (Crouching()) {
                body.velocity = StopVelocity();
            }

            // Move the player.
            if (!Crouching()) {
                body.velocity = MoveVelocity(body.velocity);
            }

            CheckFacingDirection();
        }

        if (GameManager.instance.LevelCompleted) {
            body.velocity = StopVelocity();
        }
    }

    private Vector2 MoveVelocity(Vector2 currentVelocity) {
        return new Vector2(Input.GetAxis("Horizontal") * gameplayConfig.playerSpeed, currentVelocity.y);
    }

    private Vector2 StopVelocity() {
        return Vector2.zero;
    }

    private void StopRunningAnimation() {
        animator.SetBool("bRunning", false);
    }

    private void StartRunningAnimation() {
        animator.SetBool("bRunning", true);
    }

    private void Jump() {
        jumpScript.Jump();
    }

    private bool RunningNotCrouching() {
        return !Crouching() && Input.GetAxisRaw("Horizontal") != 0;
    }

    private bool NotMoving() {
        return Input.GetAxisRaw("Horizontal") == 0;
    }

    private bool JumpOnGroundNotCrouching() {
        return grounded && Input.GetButtonDown("Jump") && !Crouching();
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void Crouch() {
        animator.SetBool("bCrouching", true);
    }

    private void StandUp() {
        animator.SetBool("bCrouching", false);
    }

    private void CheckFacingDirection() {
        bool movingLeft = Input.GetAxis("Horizontal") < 0;
        bool movingRight = Input.GetAxis("Horizontal") > 0;

        if (movingLeft && facingRight)
            Flip();
        if (movingRight && !facingRight)
            Flip();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if (alive && !invulnerable) {
            if (otherCollider.tag == "Enemy") {
                ScoreManager.instance.HP -= EnemyScript.Damage;
                animator.SetTrigger("tDamage");
                TriggerIFrames(gameplayConfig.playerIFrames);
            } else if (otherCollider.tag == "Bullet") {
                ScoreManager.instance.HP -= gameplayConfig.bulletDamage;
                animator.SetTrigger("tDamage");
                // Afte player is hit, activate iFrames;
                TriggerIFrames(gameplayConfig.playerIFrames);
            }
        }
    }

    private void TriggerIFrames(int frames) {
        StartCoroutine(WaitFrames(frames));
    }

    private IEnumerator WaitFrames(int frames) {
        invulnerable = true;
        while (frames > 0) {
            frames -= 1;
            yield return null;
        }
        invulnerable = false;
    }

    private void Die() {
        alive = false;
        body.velocity = Vector2.zero;
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        animator.SetBool("bAlive", false);
        gameOverCanvas.SetActive(true);
        scoreHPCanvas.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }
}
