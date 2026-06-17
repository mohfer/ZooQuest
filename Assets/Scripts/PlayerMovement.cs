using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        animator.SetBool("IsMoving", movement != Vector2.zero);

        if (movement != Vector2.zero)
        {
            // Horizontal lebih dominan
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                animator.SetInteger("Direction", 0); // Side

                sr.flipX = movement.x < 0;
            }
            // Vertical lebih dominan
            else
            {
                if (movement.y > 0)
                {
                    animator.SetInteger("Direction", 1); // Back
                }
                else
                {
                    animator.SetInteger("Direction", 2); // Front
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}