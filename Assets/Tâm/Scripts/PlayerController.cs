using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float jumpForce = 10f;

    Rigidbody2D rb;
    Collider2D col;
    Animator animator;

    public Collider2D feetBoxCast;

    public bool canJump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.1f * Time.deltaTime; ;
        }
    }

    void Move()
    {
		float horizontalMove = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        animator.SetFloat("speed", horizontalMove);
	}

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    void Attack()
    {

    }

    bool isMoving()
    {
        return rb.velocity.x > Mathf.Epsilon;
    }

    //bool IsAttacking()
    //{
    //    return true;
    //}
}
