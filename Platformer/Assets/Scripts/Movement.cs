using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float jumpSpeed = 1.0f;
    bool grounded = false;
    Animator animator;
    SpriteRenderer spriteR;
    Rigidbody2D rb;
    Rigidbody2D feet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, 300 * jumpSpeed));
            grounded = false;
            animator.SetTrigger("Jump");
        }
        if (rb.velocity.y < -0.1f && !grounded)
        {
            animator.SetTrigger("Fall");
        }
        animator.SetFloat("xInput", moveX);
        animator.SetBool("Grounded", grounded);
        if (moveX < 0)
        {
            spriteR.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteR.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
