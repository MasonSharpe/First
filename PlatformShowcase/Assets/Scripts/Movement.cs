using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int level;
    float invinsTimer = 0;

    public int health = 100;
    float jumpJuice = 10;
    float speedJuice = 10;

    public Slider healthSlider;
    public Slider jumpSlider;
    public Slider speedSlider;
    public GameObject UI;

    public RuntimeAnimatorController player;
    public RuntimeAnimatorController playerJ;
    public RuntimeAnimatorController playerS;
    // Start is called before the first frame update
    void Start()
    {
        UI.GetComponent<Canvas>().enabled = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, 300 * jumpSpeed));
            grounded = false;
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Camera.main.fieldOfView = 70 + (gameObject.transform.position.y);
        float moveX = Input.GetAxis("Horizontal");
        invinsTimer -= Time.deltaTime;
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
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
        if (Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.P))
        {
            if (jumpJuice > 0)
            {
                GetComponent<Animator>().runtimeAnimatorController = playerJ;
                jumpJuice -= 0.12f;
                rb.gravityScale = 0.2f;
            }
            else
            {
                rb.gravityScale = 1f;
                jumpJuice = -5;
                GetComponent<Animator>().runtimeAnimatorController = player;
            }
        }
        else
        {
            rb.gravityScale = 1f;
        }
        if (Input.GetKey(KeyCode.P) && !Input.GetKey(KeyCode.O))
        {
            if (speedJuice > 0)
            {
                GetComponent<Animator>().runtimeAnimatorController = playerS;
                speedJuice -= 0.12f;
                rb.gravityScale = 2f;
                moveSpeed = 10;
            }
            else
            {
                moveSpeed = 5;
                rb.gravityScale = 1f;
                speedJuice = -5;
                GetComponent<Animator>().runtimeAnimatorController = player;
            }
        }
        else
        {
            moveSpeed = 5;
        }
        if (!Input.GetKey(KeyCode.P) && !Input.GetKey(KeyCode.O))
        {
            GetComponent<Animator>().runtimeAnimatorController = player;
        }
            if (jumpJuice < 10)
        {
            jumpJuice += 0.05f;
        }
        if (speedJuice < 10)
        {
            speedJuice += 0.05f;
        }
        healthSlider.value = health;
        jumpSlider.value = jumpJuice;
        speedSlider.value = speedJuice;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        } else if (collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene("Level" + (level + 1));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damage" && invinsTimer <= 0)
        {
            health -= 15;
            invinsTimer = 1;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
