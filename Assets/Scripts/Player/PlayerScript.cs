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
    private Animator anim;
    private Rigidbody2D rb2d;
    private bool grounded = false;
    private bool facingRight = true;
    private bool invulnerable = false;
    private readonly float groundRadius = 0.1f;

    public bool Crouching() {
        return anim.GetBool("bCrouching");
    }

    private void Start() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        jumpScript = GetComponent<JumpScript>();
	}

	private void Update() {
        if (alive && !GameManager.instance.LevelCompleted) {
            if (ScoreManager.instance.HP <= 0 || transform.position.y < -10)
                Die();
            if (grounded && Input.GetAxisRaw("Vertical") < 0f)
                Crouch();
            if (Crouching() && Input.GetAxisRaw("Vertical") > -1f)
                StandUp();

            // If player goes out of screen at the right side of the level,
            // the level is completed.
            if (transform.position.x >= 118)
                GameManager.instance.LevelCompleted = true;
        }
    }

    private void FixedUpdate() {
        if (alive && !GameManager.instance.LevelCompleted) {
            grounded = Physics2D.OverlapCircle(groundCheck.position,
                                               groundRadius, whatIsGround);
            anim.SetBool("bGround", grounded);
            anim.SetFloat("fVSpeed", rb2d.velocity.y);

            if (grounded && Input.GetButtonDown("Jump") && !Crouching())
                jumpScript.Jump();
            if (Input.GetAxisRaw("Horizontal") == 0)
                anim.SetBool("bRunning", false);
            if (!Crouching() && Input.GetAxisRaw("Horizontal") != 0)
                anim.SetBool("bRunning", true);

            // Set velocity to zero when crouching so the player doesn't slide.
            if (Crouching())
                rb2d.velocity = Vector2.zero;

            // Move the player.
            if (!Crouching())
                rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * gameplayConfig.playerSpeed, rb2d.velocity.y);

            CheckFacingDirection();
        }
        if (GameManager.instance.LevelCompleted)
            rb2d.velocity = Vector2.zero;
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void Crouch() {
        anim.SetBool("bCrouching", true);
    }

    private void StandUp() {
        anim.SetBool("bCrouching", false);
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
                anim.SetTrigger("tDamage");
                TriggerIFrames(gameplayConfig.playerIFrames);
            } else if (otherCollider.tag == "Bullet") {
                ScoreManager.instance.HP -= gameplayConfig.bulletDamage;
                anim.SetTrigger("tDamage");
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
        rb2d.velocity = Vector2.zero;
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        anim.SetBool("bAlive", false);
        gameOverCanvas.SetActive(true);
        scoreHPCanvas.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }
}
